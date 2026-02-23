<div align="center">

# 🗄️ [ARHIVIRANO] YOLO Projekat Windows
### *Istorijski WinUI 3 Komandni Panel*

> [!WARNING]  
> **STATUS REPOZITORIJUMA: ARHIVIRAN (DEPRECATED)**
> 
> Ovaj repozitorijum sadrži prvobitnu **WinUI 3** verziju Windows klijenta za YOLO Projekat i više se ne održava. Kompletan klijent se trenutno razvija *od nule* (from scratch) koristeći **Windows Presentation Foundation (WPF)** u novom, odvojenom repozitorijumu.
>
> **Inženjersko objašnjenje tranzicije:** Iako WinUI 3 (Windows App SDK) nudi najmoderniji Fluent Design, njegov sistem distribucije (MSIX paketi, neophodni developerski sertifikati, kompleksan *sideloading*) pokazao se previše restriktivnim i nepraktičnim za brzo testiranje i edukativnu primenu. 
> 
> **Trade-offs (Kompromisi):** Odbacili smo nativne Windows 11 API-jeve i izvorni Mica materijal u korist **apsolutne prenosivosti i stabilnosti**. Nova WPF implementacija će omogućiti *Single-File Deployment* (čist `.exe` fajl bez potrebe za instalacijom, $O(1)$ kompleksnost pokretanja) uz zadržavanje punih performansi C# logike i hardverske akceleracije za AI inferencu.

[![WinUI 3](https://img.shields.io/badge/Framework-WinUI_3-gray?style=for-the-badge&logo=windows&logoColor=white)](https://learn.microsoft.com/en-us/windows/apps/winui/winui3/)
[![ONNX Runtime](https://img.shields.io/badge/AI-ONNX_Runtime-gray?style=for-the-badge&logo=onnx&logoColor=white)](https://onnxruntime.ai/)
[![C#](https://img.shields.io/badge/Language-C%23-gray?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)

---

<p align="center">
  <i>Istorijska arhiva: Originalni kod za WinUI 3 aplikaciju koja je služila kao prvi grafički interfejs za kontrolu YOLO vozila.</i>
</p>

</div>

## 🧩 Originalna WinUI 3 Arhitektura (Istorija)

Ovaj projekat je prvobitno koristio najnoviji Microsoft UI framework, fokusirajući se na modernu estetiku, ali uz cenu teškog održavanja i distribucije:

* **High-Speed Video Feed:** Optimizovan HTTP striming sa integrisanim "Offline Overlay" statusom.
* **YOLOv8 ONNX Engine:** Lokalna `Microsoft.ML.OnnxRuntime` inferencija za detekciju objekata.
* **Tesseract OCR & OpenCV:** Sistemi za obradu slike i prepoznavanje teksta na klijentskoj strani.
* **Asinhrona Telemetrija:** WebSocket komunikacija za niske latencije pri upravljanju (WASD).

---

## 🛠 Stari Tehnološki Stack

| Komponenta | Tehnologija | Uloga u ovoj verziji (Sada napušteno) |
| :--- | :--- | :--- |
| **UI Framework** | **WinUI 3 (Fluent Design)** | Moderni Windows 11 interfejs (Zamenjeno sa WPF) |
| **AI Inference** | **ONNX Runtime** | Izvršavanje YOLOv8 modela |
| **Computer Vision** | **OpenCvSharp 4** | Pre-processing frejmova za OCR |
| **OCR Engine** | **Tesseract.NET** | Očitavanje teksta sa kamere |
| **Networking** | **WebSockets** | Kontrolni kanal za Raspberry Pi 5 |
| **Shell Integration**| **WinUIEx** | Modifikacije prozora i naslovne trake |

---

## 🔧 Istorijska Konfiguracija

Sistem se povezivao na glavnu procesorsku jedinicu (Raspberry Pi 5) koristeći statičke mrežne rute:

* **WebSocket (Telemetrija i Kontrola):** `ws://192.168.4.1:1606`
* **HTTP (Video Stream):** `http://192.168.4.1:1607/capture`

### ⌨️ Kontrole (Legacy)
- **WASD:** Kretanje vozila uz ugrađen debouncing softverski mehanizam.
- **TAB:** Prebacivanje prikaza video strima.
- **Y / O:** Manuelno aktiviranje YOLO detekcije ili OCR skeniranja.

---

<div align="center">

**Autor:** Danilo Stoletović • **Mentor:** Dejan Batanjac  
**ETŠ „Nikola Tesla“ Niš • 2026**

</div>
