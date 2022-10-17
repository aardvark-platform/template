@echo off

if exist boot.fsx (
  powershell write-host -fore Green bootstrapping project
  dotnet fsi boot.fsx
  if errorlevel 1 (
    exit /b %errorlevel%
  )
	del boot.fsx
)
dotnet tool restore

if NOT exist paket.lock (
    dotnet paket install
)

dotnet paket restore
if errorlevel 1 (
  exit /b %errorlevel%
)

dotnet build

