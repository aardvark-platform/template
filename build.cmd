@echo off
setlocal enableextensions enabledelayedexpansion
PUSHD %~dp0



if exist boot.fsx (
    if NOT exist .paket\FAKE (
        IF NOT exist .paket\nuget.exe (
            powershell write-host -fore Green downloading nuget.exe
            powershell $progressPreference = 'silentlyContinue'; Invoke-WebRequest "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile ".paket\nuget.exe"
            if errorlevel 1 (
              exit /b %errorlevel%
            )
        )

        powershell write-host -fore Green downloading FAKE
        .paket\nuget.exe install FAKE -ExcludeVersion -OutputDirectory .paket -NonInteractive -Verbosity quiet
        if errorlevel 1 (
          exit /b %errorlevel%
        )
        del .paket\nuget.exe
    )

    powershell write-host -fore Green bootstrapping project
    .paket\FAKE\tools\FAKE.exe --removeLegacyFakeWarning run boot.fsx
    if errorlevel 1 (
      exit /b %errorlevel%
    )
    rd /S /Q .paket\FAKE
    del boot.fsx
)

if NOT exist .paket\paket.exe (
    .paket\paket.bootstrapper.exe
    if errorlevel 1 (
      exit /b %errorlevel%
    )
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




