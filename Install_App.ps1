# This script must be run as Administrator to install to the LocalMachine store
if (-not ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    Write-Warning "Please run this script as Administrator!"
    Exit
}

$CertPath = Join-Path $PSScriptRoot "yolo_venicle_TemporaryKey.pfx"
$Password = "YourSecurePassword123" # Replace with your actual password or ask for input

# 1. Import the certificate to the Trusted People store
Write-Host "Importing certificate to Trusted People..." -ForegroundColor Cyan
$SecurePassword = ConvertTo-SecureString -String $Password -Force -AsPlainText
Import-PfxCertificate -FilePath $CertPath -CertStoreLocation Cert:\LocalMachine\TrustedPeople -Password $SecurePassword

# 2. Find and install the MSIX package
$MsixPath = Get-ChildItem -Path "$PSScriptRoot\*.msix" -Recurse | Select-Object -First 1
if ($MsixPath) {
    Write-Host "Installing App: $($MsixPath.Name)..." -ForegroundColor Green
    Add-AppxPackage -Path $MsixPath.FullName
} else {
    Write-Error "MSIX package not found in this folder."
}

Write-Host "Done! You can now run the app from the Start Menu." -ForegroundColor Green
Pause