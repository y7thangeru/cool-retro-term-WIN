# cool-retro-term

|> Default Amber|C:\ IBM DOS|$ Default Green|
|---|---|---|
|![Default Amber Cool Retro Term](https://user-images.githubusercontent.com/121322/32070717-16708784-ba42-11e7-8572-a8fcc10d7f7d.gif)|![IBM DOS](https://user-images.githubusercontent.com/121322/32070716-16567e5c-ba42-11e7-9e64-ba96dfe9b64d.gif)|![Default Green Cool Retro Term](https://user-images.githubusercontent.com/121322/32070715-163a1c94-ba42-11e7-80bb-41fbf10fc634.gif)|

## Description
cool-retro-term is a terminal emulator which mimics the look and feel of the old cathode tube screens.
It has been designed to be eye-candy, customizable, and reasonably lightweight.

It uses the QML port of qtermwidget (Konsole): https://github.com/Swordfish90/qmltermwidget.

This terminal emulator works under Linux, macOS, and Windows 11 and requires Qt5. It's suggested that you stick to the latest LTS version.

**Windows 11 Support**: This fork includes native Windows 11 PowerShell integration while maintaining the same retro visual effects.

Settings such as colors, fonts, and effects can be accessed via context menu.

## Screenshots
![Image](<https://i.imgur.com/TNumkDn.png>)
![Image](<https://i.imgur.com/hfjWOM4.png>)
![Image](<https://i.imgur.com/GYRDPzJ.jpg>)

## Install

If you want to get a hold of the latest version, just go to the Releases page and grab the latest AppImage (Linux) or dmg (macOS).

Alternatively, most distributions such as Ubuntu, Fedora or Arch already package cool-retro-term in their official repositories.

## Building

Check out the wiki and follow the instructions on how to build it on [Linux](https://github.com/Swordfish90/cool-retro-term/wiki/Build-Instructions-(Linux)) and [macOS](https://github.com/Swordfish90/cool-retro-term/wiki/Build-Instructions-(macOS)).

### Windows 11 Building

To build on Windows 11:

1. Install Qt 5.15+ (Qt Creator recommended)
2. Install Visual Studio Build Tools or Community Edition  
3. Clone the repository:
   ```
   git clone --recursive https://github.com/y7thangeru/cool-retro-term-WIN.git
   ```
4. Open the project in Qt Creator or build from command line:
   ```
   qmake
   nmake
   ```
5. The executable will be created in the build directory

**PowerShell Integration**: The Windows version automatically detects Windows 11 and uses PowerShell as the default shell while maintaining all the original retro visual effects.
