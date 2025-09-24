@echo off
echo Building Cool Retro Terminal - Windows WPF Edition
echo ==================================================
echo.

if not exist "src\CoolRetroTerm\CoolRetroTerm.csproj" (
    echo Error: Project file not found. Make sure you're in the repository root directory.
    pause
    exit /b 1
)

echo Restoring packages...
dotnet restore src\CoolRetroTerm\CoolRetroTerm.csproj
if errorlevel 1 (
    echo Failed to restore packages.
    pause
    exit /b 1
)

echo.
echo Building application...
dotnet build src\CoolRetroTerm\CoolRetroTerm.csproj -c Release
if errorlevel 1 (
    echo Failed to build application.
    pause
    exit /b 1
)

echo.
echo Publishing self-contained application...
dotnet publish src\CoolRetroTerm\CoolRetroTerm.csproj -c Release -r win-x64 --self-contained -o dist\
if errorlevel 1 (
    echo Failed to publish application.
    pause
    exit /b 1
)

echo.
echo ====================================================
echo Build completed successfully!
echo.
echo Executable location: dist\CoolRetroTerm.exe
echo.
echo To run the application:
echo   dist\CoolRetroTerm.exe
echo.
echo For fullscreen mode:
echo   dist\CoolRetroTerm.exe --fullscreen
echo.
echo For help:
echo   dist\CoolRetroTerm.exe --help
echo ====================================================
pause