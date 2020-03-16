@echo off
setlocal enableextensions enabledelayedexpansion
PUSHD %~dp0



if exist boot.fsx (
    if NOT exist .paket\fake.exe (
        dotnet tool install fake-cli --tool-path .paket --version 5.16.0
        if errorlevel 1 (
          exit /b %errorlevel%
        )
    )

    powershell write-host -fore Green bootstrapping project
    .paket\fake.exe run boot.fsx
    if errorlevel 1 (
      exit /b %errorlevel%
    )
	del boot.fsx
	del boot.fsx.lock
)

IF NOT exist .paket\paket.exe (
	dotnet tool install Paket --tool-path .paket
)

if NOT exist paket.lock (
    echo No paket.lock found, running paket install.
    .paket\paket.exe install
)

.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

dotnet packages\build\fake-cli\tools\netcoreapp2.1\any\fake-cli.dll build %* 

