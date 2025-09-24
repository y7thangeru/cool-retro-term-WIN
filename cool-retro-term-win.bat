@echo off
REM Windows batch file to launch cool-retro-term with PowerShell
REM Place this in the same directory as the cool-retro-term.exe

cd /d "%~dp0"
cool-retro-term.exe %*