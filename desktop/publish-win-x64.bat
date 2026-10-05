@echo off
setlocal
cd /d "%~dp0"
dotnet publish Northfield.Fiscal.Desktop\Northfield.Fiscal.Desktop.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:PublishTrimmed=false
if errorlevel 1 exit /b %errorlevel%
echo.
echo Executavel gerado em:
echo %CD%\Northfield.Fiscal.Desktop\bin\Release\net10.0-windows\win-x64\publish\Northfield.Fiscal.Desktop.exe
endlocal
