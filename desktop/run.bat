@echo off
setlocal
cd /d "%~dp0"
dotnet run --project Northfield.Fiscal.Desktop\Northfield.Fiscal.Desktop.csproj
endlocal
