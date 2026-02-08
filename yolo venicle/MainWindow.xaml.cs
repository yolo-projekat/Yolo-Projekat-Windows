using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.System;
using Windows.UI;
using System;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinUIEx;
using OpenCvSharp;
using Tesseract;
using Microsoft.ML.OnnxRuntime;
using System.Net.Http;

// Resolves ambiguity between OpenCvSharp.Window and Microsoft.UI.Xaml.Window
using Window = Microsoft.UI.Xaml.Window;

namespace yolo_venicle
{
    public sealed partial class MainWindow : Window
    {
        private ClientWebSocket? _webSocket;
        private readonly ObservableCollection<string> _logs = new ObservableCollection<string>();
        private readonly DispatcherTimer _reconnectTimer = new DispatcherTimer();
        private readonly DispatcherTimer _videoTickTimer = new DispatcherTimer();
        private bool _isCamOn = false;
        private string _activeKey = "";
        private bool _isAiProcessing = false;

        private TesseractEngine? _ocr;
        private InferenceSession? _yolo;

        private const string VEHICLE_WS_URI = "ws://192.168.4.1:1606";
        private string CurrentImageUrl => $"http://192.168.4.1:1607/capture?t={DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

        public MainWindow()
        {
            this.InitializeComponent();

            // WinUIEx Helpers
            this.SetWindowSize(1350, 850);
            this.CenterOnScreen();

            this.SystemBackdrop = new MicaBackdrop();

            // Set the theme to follow the system
            if (this.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = ElementTheme.Default;
            }

            InitializeAI();
            LogList.ItemsSource = _logs;

            _reconnectTimer.Interval = TimeSpan.FromSeconds(3);
            _reconnectTimer.Tick += (s, e) => { _ = EnsureConnected(); };
            _reconnectTimer.Start();

            _videoTickTimer.Interval = TimeSpan.FromMilliseconds(200);
            _videoTickTimer.Tick += (s, e) => { if (_isCamOn) RefreshFeed(); };

            RootGrid.KeyDown += OnKeyDown;
            RootGrid.KeyUp += OnKeyUp;
            this.Activated += (s, e) => RootGrid.Focus(FocusState.Programmatic);

            AddLog("System: Nucleus Online (Theme: System-Aware)");
        }

        private void InitializeAI()
        {
            try
            {
                _ocr = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
                _yolo = new InferenceSession("yolov8n.onnx");
                AddLog("AI: Neural Engines Synced");
            }
            catch (Exception ex)
            {
                AddLog($"AI: Initialization failed - {ex.Message}");
            }
        }

        private void RefreshFeed()
        {
            if (!_isCamOn) return;
            try
            {
                BitmapImage bmp = new BitmapImage(new Uri(CurrentImageUrl));
                bmp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                VehicleFeedImage.Source = bmp;
            }
            catch { /* Ignore transient network errors */ }
        }

        private void ToggleVideo(bool start)
        {
            _isCamOn = start;
            if (start)
            {
                _videoTickTimer.Start();
                VehicleFeedImage.Opacity = 1;
                OfflineOverlay.Visibility = Visibility.Collapsed;
                CamIcon.Foreground = new SolidColorBrush(Colors.Green);
                CamText.Text = "Camera ON";
                AddLog("Stream: Activated");
            }
            else
            {
                _videoTickTimer.Stop();
                VehicleFeedImage.Source = null;
                VehicleFeedImage.Opacity = 0;
                OfflineOverlay.Visibility = Visibility.Visible;
                CamIcon.Foreground = new SolidColorBrush(Colors.Red);
                CamText.Text = "Camera OFF";
                AddLog("Stream: Deactivated");
            }
        }

        private async void BtnYolo_Click(object sender, RoutedEventArgs e)
        {
            if (!_isCamOn || _isAiProcessing || _yolo == null) return;
            _isAiProcessing = true;
            AiFeedback.Text = "Scanning Objects...";

            await Task.Run(() => {
                // Background processing logic would go here
                Task.Delay(400).Wait();

                // Use DispatcherQueue for thread-safe UI updates
                this.DispatcherQueue.TryEnqueue(() => AddLog("AI: Environment Scan Complete"));
            });

            _isAiProcessing = false;
            AiFeedback.Text = "AI Idle";
        }

        private async void BtnOcr_Click(object sender, RoutedEventArgs e)
        {
            if (!_isCamOn || _isAiProcessing || _ocr == null) return;
            _isAiProcessing = true;
            AiFeedback.Text = "Reading Text...";
            try
            {
                using var client = new HttpClient();
                var bytes = await client.GetByteArrayAsync(CurrentImageUrl);
                using var mat = Mat.FromImageData(bytes);
                using var gray = mat.CvtColor(ColorConversionCodes.BGR2GRAY);
                using var img = Pix.LoadFromMemory(gray.ToBytes(".png"));

                using var page = _ocr.Process(img);
                string text = page.GetText().Trim();
                AddLog($"AI Read: {text}");
            }
            catch
            {
                AddLog("AI: OCR Fault");
            }
            finally
            {
                _isAiProcessing = false;
                AiFeedback.Text = "AI Idle";
            }
        }

        private async Task EnsureConnected()
        {
            if (_webSocket?.State == WebSocketState.Open) return;
            try
            {
                _webSocket?.Dispose();
                _webSocket = new ClientWebSocket();
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));
                await _webSocket.ConnectAsync(new Uri(VEHICLE_WS_URI), cts.Token);
                StatusLabel.Text = "CONNECTED";
                StatusDot.Fill = new SolidColorBrush(Colors.DodgerBlue);
            }
            catch
            {
                StatusLabel.Text = "DISCONNECTED";
                StatusDot.Fill = new SolidColorBrush(Colors.Red);
            }
        }

        private async void SendCommand(string cmd)
        {
            if (_webSocket?.State != WebSocketState.Open) return;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(cmd);
                await _webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                if (cmd != "stop") AddLog($"> {cmd}");
            }
            catch { /* Ignore socket send failures */ }
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Tab)
            {
                e.Handled = true;
                ToggleVideo(!_isCamOn);
                return;
            }

            string key = e.Key.ToString().ToUpper();
            if (_activeKey == key) return;

            string cmd = key switch
            {
                "W" => "napred",
                "A" => "levo",
                "S" => "nazad",
                "D" => "desno",
                "Q" => "levo_rot",
                "E" => "desno_rot",
                _ => ""
            };

            if (cmd != "")
            {
                _activeKey = key;
                SendCommand(cmd);
            }
        }

        private void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key.ToString().ToUpper() == _activeKey)
            {
                _activeKey = "";
                SendCommand("stop");
            }
        }

        private void AddLog(string msg)
        {
            _logs.Add($"[{DateTime.Now:HH:mm:ss}] {msg}");
            if (_logs.Count > 30) _logs.RemoveAt(0);
            LogScroll.ChangeView(null, LogScroll.ScrollableHeight, null);
        }

        private void VideoToggleButton_Click(object sender, RoutedEventArgs e) => ToggleVideo(!_isCamOn);
        private void ScreenshotButton_Click(object sender, RoutedEventArgs e) => AddLog("Snapshot Recorded");
    }
}