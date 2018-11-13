@echo off
setlocal enableextensions enabledelayedexpansion
PUSHD %~dp0

.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)

IF exist boot.fsx (
    powershell write-host -fore Green bootstrapping project 
    SET VSPATH=''
    for /f "delims=" %%A in ('.\.paket\vswhere.exe -property installationPath') do set "VSPATH=%%A"
    SET "fsi=!VSPATH!\Common7\IDE\CommonExtensions\Microsoft\FSharp\fsi.exe"
    
    IF NOT exist "!fsi!" (
        SET "fsi=%FSHARPINSTALLDIR%\fsi.exe"
        IF NOT exist "!fsi!" (
            powershell write-host -fore Red fsi.exe not found. please install fsharp tools or Visual Studio
            exit /b 1
        )   
    )
    
    "!fsi!" "boot.fsx" 
    if errorlevel 1 (
      exit /b %errorlevel%
    )
    del "boot.fsx"
    del ".\.paket\vswhere.exe"
    .paket\paket.exe install
)

if NOT exist paket.lock (
	powershell write-host -fore Yellow  No paket.lock found, running paket install.
	.paket\paket.exe install
)

.paket\paket.exe restore --group Build
if errorlevel 1 (
  exit /b %errorlevel%
)

"packages\build\FAKE\tools\Fake.exe" "build.fsx" Dummy --fsiargs build.fsx %* 




