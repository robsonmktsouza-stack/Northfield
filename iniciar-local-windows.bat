@echo off
setlocal
cd /d "%~dp0"
where php >nul 2>&1
if errorlevel 1 (
  echo PHP nao encontrado no PATH. Abra o terminal do Laragon e execute este arquivo novamente.
  pause
  exit /b 1
)
echo Northfield - ambiente local (somente desenvolvimento)
echo Acesse http://127.0.0.1:8000 no navegador.
echo Pressione Ctrl+C para encerrar.
php -S 127.0.0.1:8000 -t public public/router.php
pause
