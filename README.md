<div align="center">

# 🖥️ YOLO Projekat Windows
### *Centralni Komandni Panel za Autonomnu Sistemsku Kontrolu*

[![WinUI 3](https://img.shields.io/badge/Framework-WinUI_3-38bdf8?style=for-the-badge&logo=windows&logoColor=white)](https://learn.microsoft.com/en-us/windows/apps/winui/winui3/)
[![ONNX Runtime](https://img.shields.io/badge/AI-ONNX_Runtime-075985?style=for-the-badge&logo=onnx&logoColor=white)](https://onnxruntime.ai/)
[![C#](https://img.shields.io/badge/Language-C%23-38bdf8?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![License: MIT](https://img.shields.io/badge/License-MIT-94a3b8?style=for-the-badge)](https://opensource.org/licenses/MIT)

---

<p align="center">
  <b>YOLO Vozilo Windows</b> je profesionalni desktop klijent projektovan za maksimalnu stabilnost i preciznost. 
  <br>Koristeći <b>WinUI 3</b>, aplikacija donosi besprekorno Windows 11 iskustvo uz integraciju moćnih AI engine-ova direktno na klijentskoj strani.
</p>



</div>

## 🚀 Ključne Komponente

### 📺 Video & AI Inteligentni Sistem
* **High-Speed Video Feed:** Optimizovan striming uz vizuelni "Offline Overlay" i automatsku dijagnostiku frejmova.
* **YOLOv8 ONNX Engine:** Lokalna inferencija putem `Microsoft.ML.OnnxRuntime` za trenutnu detekciju objekata bez eksternih API poziva.
* **OpenCV Pre-Processing:** Napredna obrada slike pomoću `OpenCvSharp` (grayscale, filtriranje šuma) radi maksimizovanja OCR preciznosti.
* **Tesseract OCR:** Inteligentno očitavanje tekstualnih komandi i registarskih oznaka direktno sa video izvora.

### 🎮 Precizna Kontrola i Telemetrija
* **Precision Keyboard Drive:** Optimizovan **WASD** sistem sa ugrađenom logikom za debouncing (sprečavanje zagušenja komandi).
* **Real-time Telemetry Log:** Dinamički panel koji beleži mrežnu latenciju, status AI skeniranja i zdravlje sistema.
* **Smart Reconnect:** Autonomni sistem za oporavak veze koji održava stabilan WebSocket kanal u svim uslovima.

---

## 🛠 Tehnološki Stack

| Segment | Tehnologija | Uloga |
| :--- | :--- | :--- |
| **UI Framework** | WinUI 3 (Fluent Design) | Moderni Windows App SDK |
| **AI Inference** | ONNX Runtime | YOLOv8 Model Execution |
| **Computer Vision** | OpenCvSharp 4 | Image Filtering & Analysis |
| **OCR Engine** | Tesseract.NET | Text Recognition |
| **Networking** | WebSockets (Async) | Low-Latency Command Channel |
| **Shell Integration** | WinUIEx | Napredno upravljanje prozorima |

---

## 🔧 Konfiguracija i Upravljanje

Klijent se povezuje na jezgro sistema (Raspberry Pi 5) putem sledećih protokola:

> [!TIP]
> Za najbolje performanse AI analize, preporučuje se korišćenje računara sa namenskom grafičkom karticom (GPU) radi ubrzanja ONNX Runtime-a.

* **WebSocket (Telemetrija):** `ws://192.168.4.1:1606`
* **HTTP (Video Stream):** `http://192.168.4.1:1607/capture`

### ⌨️ Prečice Kontrolera
- **WASD:** Kretanje vozila (Automatska `stop` komanda na otpuštanje tastera).
- **TAB:** Toggle video strima.
- **Y / O:** Ručno pokretanje YOLO detekcije ili OCR skeniranja.

---

## 🎨 Vizuelni Identitet

Aplikacija prati **Fluent Design** standarde, usklađene sa web portalom:
* **Accent Color:** `#38bdf8` (Electric Blue) za statusne indikatore.
* **UI Style:** Mica/Acrylic efekti (Glassmorphism) na bočnim panelima.
* **Feedback:** Dinamički kolor-kodirani log (Plava: Info, Zelena: AI Success, Crvena: Error).

---

<div align="center">

**Autor:** Danilo Stoletović • **Mentor:** Dejan Batanjac  
**ETŠ „Nikola Tesla“ Niš • 2026**

</div>
