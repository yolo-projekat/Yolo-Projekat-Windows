YOLO Vozilo - Windows Control Center 🖥️🏎️

YOLO Vozilo Windows je desktop klijent razvijen u C# jeziku koristeći WinUI 3 (Windows App SDK). Aplikacija služi kao centralni komandni panel za upravljanje robotskim vozilom u realnom vremenu, kombinujući naprednu telemetriju, video striming niske latencije i moćne AI engine-ove.

✨ Ključne Karakteristike
High-Speed Video Feed: Kontinuirano osvežavanje video signala sa vozila uz vizuelni "Offline Overlay" kada kamera nije aktivna.

Neural AI Integration:

YOLOv8 Support: Integracija sa Microsoft.ML.OnnxRuntime za analizu okruženja u realnom vremenu.

Tesseract OCR: Prepoznavanje teksta na video feedu pomoću TesseractEngine, omogućavajući vizuelno očitavanje znakova ili komandi.

Precision Keyboard Control: Optimizovano upravljanje putem tastature (WASD sistem) sa logikom koja sprečava zagušenje komandi (Key Debouncing).

System Telemetry Log: Ugrađeni log sistem koji prati svaku komandu, status AI skeniranja i zdravlje mrežne konekcije.

Smart Reconnect: Automatski sistem za ponovno uspostavljanje veze sa vozilom (WebSocket) u intervalima od 3 sekunde.

Image Processing: Korišćenje OpenCvSharp biblioteke za obradu slike pre slanja na OCR motor (konverzija u sivi ton, filtriranje).

🛠 Tehnologije i Biblioteke
UI Framework: WinUI 3 (Windows App SDK)

AI Inference: ONNX Runtime (za YOLOv8 model)

OCR Engine: Tesseract.NET

Computer Vision: OpenCvSharp 4

Networking: System.Net.WebSockets za komande i HttpClient za preuzimanje frejmova.

Window Management: WinUIEx (za lako centriranje i promenu veličine prozora).

🚀 Rad sa aplikacijom
1. Povezivanje i IP Konfiguracija
Aplikacija podrazumevano traži vozilo na sledećim adresama:

WebSocket (Komande): ws://192.168.4.1:1606

HTTP (Kamera): http://192.168.4.1:1607/capture

2. Prečice na tastaturi
WASD: Kontrola kretanja (Napred, Levo, Nazad, Desno).

TAB: Brzo paljenje/gašenje video strima.

Taster pušten: Automatsko slanje stop komande vozilu.

3. AI Analiza
Klikom na BtnYolo ili BtnOcr, aplikacija pokreće asinhroni zadatak koji analizira trenutni frejm. Rezultati se odmah ispisuju u sistemski log sa vremenskim pečatom.

📦 Instalacija i Setup
Modeli: Postavite vaš yolov8n.onnx fajl u korenski direktorijum aplikacije.

Tessdata: Uverite se da folder ./tessdata sadrži eng.traineddata (ili drugi jezik) za OCR.

Mreža: Povežite računar na Wi-Fi mrežu vozila (ESP32/Raspberry Pi pristupna tačka).

Runtime: Za pokretanje je potreban Windows App SDK Runtime.

🎨 Korisnički Interfejs (UI)
Dizajnirana je u Light Mode stilu sa fokusom na preglednost:

🟢 Connected: Plavi indikator i "CONNECTED" status.

🔴 Offline: Crveni indikator i zamućen video ekran.

⌨️ Log: Skrolujući panel sa desne strane za praćenje akcija.

Autor: Danilo Stoletovic

Licenca: MIT
