[Setup]
AppName=Yolo Venicle
AppVersion={#AppVersion}
DefaultDirName={autopf}\YoloVenicle
DefaultGroupName=Yolo Venicle
UninstallDisplayIcon={app}\yolo venicle.exe
Compression=lzma2
SolidCompression=yes
OutputDir=installer_output
OutputBaseFilename=yolo-venicle-installer

[Files]
; Note: The path "publish_exe\yolo venicle.exe" matches the output folder in your YAML
Source: "publish_exe\yolo venicle.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "publish_exe\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\Yolo Venicle"; Filename: "{app}\yolo venicle.exe"
Name: "{commondesktop}\Yolo Venicle"; Filename: "{app}\yolo venicle.exe"