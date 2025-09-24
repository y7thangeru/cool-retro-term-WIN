@echo off
REM Windows Installation Script for cool-retro-term
REM Run this as Administrator

echo Installing cool-retro-term for Windows 11...

REM Create program directory
if not exist "C:\Program Files\cool-retro-term" (
    mkdir "C:\Program Files\cool-retro-term"
)

REM Copy executable and dependencies
copy cool-retro-term.exe "C:\Program Files\cool-retro-term\"
copy cool-retro-term-win.bat "C:\Program Files\cool-retro-term\"

REM Create desktop shortcut (requires additional tools)
echo Installation completed!
echo You can run cool-retro-term from: C:\Program Files\cool-retro-term\cool-retro-term.exe
echo or use the batch file: C:\Program Files\cool-retro-term\cool-retro-term-win.bat

pause