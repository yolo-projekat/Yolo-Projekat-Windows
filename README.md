# 🖥️ YOLO Vozilo - Windows Control Center

[![Framework](https://img.shields.io/badge/Framework-WinUI_3-blue.svg)](https://learn.microsoft.com/en-us/windows/apps/winui/winui3/)
[![AI](https://img.shields.io/badge/AI-ONNX_Runtime-orange.svg)](https://onnxruntime.ai/)
[![OCR](https://img.shields.io/badge/OCR-Tesseract-green.svg)](https://github.com/tesseract-ocr/tesseract)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

**YOLO Vozilo Windows** je profesionalni desktop klijent razvijen u C# jeziku koristeći **WinUI 3 (Windows App SDK)**. Aplikacija služi kao centralni komandni panel za upravljanje robotskim vozilom u realnom vremenu, kombinujući naprednu telemetriju, video striming niske latencije i moćne AI engine-ove.



---

## ✨ Ključne Karakteristike

### 📺 Video & AI Inteligentni Sistem
* **High-Speed Video Feed:** Kontinuirano osvežavanje video signala uz vizuelni "Offline Overlay" kada veza nije aktivna.
* **YOLOv8 Support:** Integracija sa `Microsoft.ML.OnnxRuntime` za analizu okruženja u realnom vremenu putem ONNX modela.
* **Tesseract OCR:** Prepoznavanje teksta na video feedu pomoću `TesseractEngine`, omogućavajući automatizovano očitavanje znakova ili komandi.
* **Image Processing:** Napredna obrada slike pomoću `OpenCvSharp` biblioteke (konverzija u sivi ton, filtriranje) pre slanja na OCR motor radi veće preciznosti.

### 🎮 Precizna Kontrola i Telemetrija
* **Precision Keyboard Control:** Optimizovano upravljanje putem tastature (**WASD** sistem) sa ugrađenom logikom za sprečavanje zagušenja komandi (Key Debouncing).
* **System Telemetry Log:** Integrisani log sistem koji prati svaku komandu, status AI skeniranja i zdravlje mrežne konekcije u realnom vremenu.
* **Smart Reconnect:** Automatski sistem za ponovno uspostavljanje veze sa vozilom (WebSocket) u intervalima od 3 sekunde u slučaju gubitka signala.

---

## 🛠 Tehnologije i Biblioteke

| Segment | Tehnologija |
| :--- | :--- |
| **UI Framework** | WinUI 3 (Windows App SDK) |
| **AI Inference** | ONNX Runtime (za YOLOv8 model) |
| **OCR Engine** | Tesseract.NET |
| **Computer Vision** | OpenCvSharp 4 |
| **Networking** | System.Net.WebSockets & HttpClient |
| **Window Management** | WinUIEx |

---

## 🚀 Rad sa aplikacijom

### 1. Povezivanje i IP Konfiguracija
Aplikacija podrazumevano traži vozilo na sledećim adresama:
* **WebSocket (Komande):** `ws://192.168.4.1:1606`
* **HTTP (Kamera):** `http://192.168.4.1:1607/capture`

### 2. Prečice na tastaturi
* **WASD:** Kontrola kretanja (Napred, Levo, Nazad, Desno).
* **TAB:** Brzo paljenje/gašenje video strima.
* **Taster pušten:** Automatsko slanje `stop` komande vozilu radi bezbednosti.

### 3. AI Analiza
Klikom na dugmad `BtnYolo` ili `BtnOcr`, aplikacija pokreće asinhroni zadatak koji analizira trenutni frejm. Rezultati se trenutno ispisuju u sistemski log sa preciznim vremenskim pečatom.

---

## 📦 Instalacija i Setup

1. **Modeli:** Postavite vaš `yolov8n.onnx` fajl u korenski direktorijum aplikacije.
2. **Tessdata:** Uverite se da folder `./tessdata` sadrži `eng.traineddata` (ili drugi jezik po izboru) za OCR.
3. **Mreža:** Povežite računar na Wi-Fi mrežu vozila (ESP32/Raspberry Pi pristupna tačka).
4. **Runtime:** Za pokretanje je neophodno imati instaliran **Windows App SDK Runtime**.

---

## 🎨 Korisnički Interfejs (UI)
Dizajnirana u modernom Windows stilu sa fokusom na preglednost:
* 🔵 **Connected:** Plavi indikator i "CONNECTED" status u gornjem uglu.
* 🔴 **Offline:** Crveni indikator i zamućen (blurred) video ekran kada veza pukne.
* ⌨️ **Log Panel:** Skrolujući panel sa desne strane za praćenje svih akcija robota.

---

Autor: Danilo Stoletovic

Licenca: MIT
