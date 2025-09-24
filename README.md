# Cool Retro Term - Windows WPF Edition

A complete, fully-featured retro terminal emulator for Windows, built with .NET 8.0 and WPF. This project provides an authentic CRT monitor experience with all the visual effects and functionality of the original Cool Retro Terminal, implemented entirely in C# and WPF.

![Cool Retro Terminal](https://via.placeholder.com/800x400/000000/00ff00?text=Cool+Retro+Terminal+-+Windows+WPF+Edition)

## ✨ Features

### 🖥️ Authentic Retro CRT Experience
- **Screen curvature simulation** with realistic distortion effects
- **Scanlines overlay** with adjustable intensity (0-100%)
- **Screen glow effects** with customizable intensity
- **Multiple color schemes**: Classic Green, Amber, White, Blue, and Red
- **Burn-in effect simulation** for authentic CRT appearance
- **Configurable opacity** for screen transparency

### 💻 Terminal Functionality
- **Windows Command Prompt integration** for real terminal sessions
- **Built-in commands**: `clear`, `help`, `echo`, `exit`
- **Command history navigation** with Up/Down arrow keys
- **Real-time output handling** for interactive applications
- **Copy/paste support** (Ctrl+C/Ctrl+V)
- **Auto-scrolling** to keep terminal output visible

### 🎨 Customization Options
- **Font selection** from available monospace fonts
- **Color scheme presets** with instant preview
- **Custom colors** for foreground and background
- **Effect intensity controls** for all visual effects
- **Settings persistence** with JSON-based configuration
- **Real-time settings preview** without restart

### ⌨️ Keyboard Shortcuts
- **F11**: Toggle fullscreen mode
- **Ctrl+C**: Copy selected text
- **Ctrl+V**: Paste text from clipboard
- **Ctrl+L**: Clear terminal screen
- **Ctrl++**: Zoom in (increase font size)
- **Ctrl+-**: Zoom out (decrease font size)
- **Ctrl+0**: Reset zoom to default
- **Up/Down**: Navigate command history

### 🪟 User Interface
- **Borderless retro window** with authentic CRT frame design
- **Custom window controls** with retro styling
- **Comprehensive settings window** with tabbed interface
- **Status bar** with application information
- **Resizable and movable** window with drag support

## 🚀 Quick Start

### Prerequisites
- Windows 10 or later
- .NET 8.0 Runtime (automatically included in self-contained builds)
- At least 100MB free disk space

### Option 1: Pre-built Release (Recommended)
1. Download the latest release from the [Releases page](https://github.com/y7thangeru/cool-retro-term-WIN/releases)
2. Extract the ZIP file to your desired location
3. Run `CoolRetroTerm.exe`

### Option 2: Build from Source
1. **Install .NET 8.0 SDK**
   ```
   https://dotnet.microsoft.com/download/dotnet/8.0
   ```

2. **Clone the repository**
   ```bash
   git clone https://github.com/y7thangeru/cool-retro-term-WIN.git
   cd cool-retro-term-WIN
   ```

3. **Build on Windows**
   ```cmd
   build-windows.bat
   ```
   
   **Or build cross-platform**
   ```bash
   chmod +x build.sh
   ./build.sh
   ```

4. **Run the application**
   ```cmd
   dist\CoolRetroTerm.exe
   ```

## 🎮 Usage

### Starting the Application
```cmd
# Normal window
CoolRetroTerm.exe

# Start in fullscreen
CoolRetroTerm.exe --fullscreen

# Show help
CoolRetroTerm.exe --help

# Show version
CoolRetroTerm.exe --version
```

### Using the Terminal
1. **Type commands** just like in any terminal
2. **Use arrow keys** to navigate command history
3. **Select text** with mouse and copy with Ctrl+C
4. **Paste text** with Ctrl+V
5. **Clear screen** with Ctrl+L or `clear` command

### Customizing Appearance
1. **Open Settings**: Click the ⚙ button in the top-right corner
2. **Display Tab**: Configure visual effects, colors, and fonts
3. **Terminal Tab**: Adjust behavior and view shortcuts
4. **Apply Changes**: Click OK to save and apply settings

### Built-in Commands
- `clear` or `cls` - Clear the terminal screen
- `help` - Show available commands and shortcuts
- `echo <text>` - Display text
- `exit` - Close the application

## 🛠️ Technical Details

### Architecture
- **Frontend**: WPF (Windows Presentation Foundation) with XAML
- **Backend**: .NET 8.0 with C#
- **Terminal Engine**: Process-based Windows Command Prompt integration
- **Settings**: JSON serialization with Newtonsoft.Json
- **Effects**: WPF visual effects system with custom shaders and overlays

### Project Structure
```
src/CoolRetroTerm/
├── Models/                 # Data models (TerminalSettings)
├── ViewModels/            # MVVM view models (MainViewModel)
├── Views/                 # WPF windows (MainWindow, SettingsWindow)
├── Controls/              # Custom WPF controls (TerminalControl)
├── Services/              # Business logic (TerminalService, FontManager)
├── Resources/             # XAML resources (Styles, Effects)
│   ├── Styles.xaml       # UI component styles
│   └── RetroEffects.xaml # Visual effect definitions
└── Program.cs             # Application entry point
```

### Color Schemes
The application includes several authentic retro color schemes:

- **Classic Green** (#00FF00 on #000000) - Traditional terminal green
- **Amber** (#FFAA00 on #000000) - Warm amber glow
- **White** (#FFFFFF on #000000) - High-contrast monochrome
- **Blue** (#0088FF on #000000) - Cool blue terminal
- **Red** (#FF0000 on #000000) - Alert red display

### Settings Storage
Settings are automatically saved to:
```
%APPDATA%\CoolRetroTerm\settings.json
```

## 🤝 Contributing

We welcome contributions! Here's how you can help:

### Bug Reports
1. Check existing issues first
2. Provide detailed reproduction steps
3. Include system information (Windows version, .NET version)
4. Attach screenshots if applicable

### Feature Requests
1. Describe the feature clearly
2. Explain the use case
3. Consider backward compatibility

### Code Contributions
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Test thoroughly on Windows
5. Commit your changes (`git commit -m 'Add amazing feature'`)
6. Push to the branch (`git push origin feature/amazing-feature`)
7. Open a Pull Request

### Development Setup
1. Install Visual Studio 2022 or VS Code with C# extension
2. Install .NET 8.0 SDK
3. Clone the repository
4. Open the solution file in your IDE
5. Build and run

## 📦 Building Distribution

### Self-Contained Windows Build
```bash
dotnet publish src/CoolRetroTerm/CoolRetroTerm.csproj \
  -c Release \
  -r win-x64 \
  --self-contained \
  -o dist/
```

### Framework-Dependent Build
```bash
dotnet publish src/CoolRetroTerm/CoolRetroTerm.csproj \
  -c Release \
  -r win-x64 \
  --no-self-contained \
  -o dist/
```

## ❓ Troubleshooting

### Common Issues

**Application won't start**
- Ensure Windows 10 or later
- Install .NET 8.0 Runtime
- Check antivirus software isn't blocking the executable

**Terminal commands don't work**
- Verify Command Prompt is available on your system
- Check if running with appropriate permissions
- Try running as administrator if needed

**Visual effects not working**
- Update graphics drivers
- Ensure hardware acceleration is enabled
- Try reducing effect intensities in settings

**Font not displaying correctly**
- Install additional monospace fonts
- Select a different font in settings
- Ensure font files are not corrupted

### Performance Tips
- Reduce scanline intensity for better performance
- Disable glow effects on older hardware
- Use lower opacity values for smoother rendering
- Close other graphics-intensive applications

## 📄 License

This project is licensed under the GNU General Public License v3.0 - see the [LICENSE](gpl-3.0.txt) file for details.

## 🙏 Acknowledgments

- Original [Cool Retro Term](https://github.com/Swordfish90/cool-retro-term) project by Filippo Scognamiglio
- Qt/QML community for the original implementation inspiration
- .NET and WPF communities for excellent documentation and support
- All contributors and users who help improve this project

## 🔗 Links

- [Original Cool Retro Term](https://github.com/Swordfish90/cool-retro-term)
- [.NET 8.0 Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [WPF Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
- [Report Issues](https://github.com/y7thangeru/cool-retro-term-WIN/issues)
- [Discussions](https://github.com/y7thangeru/cool-retro-term-WIN/discussions)

---

**Cool Retro Term Windows Edition** - Bringing retro terminal aesthetics to modern Windows systems with the power of .NET and WPF.

Steps:
```batch
# Clone the repository
git clone https://github.com/y7thangeru/cool-retro-term-WIN.git
cd cool-retro-term-WIN

# Build the solution
msbuild CoolRetroTerm.sln /p:Configuration=Release

# Run the application
src\CoolRetroTerm\bin\Release\CoolRetroTerm.exe
```

## Usage

### Getting Started

1. **Launch the Application**
   - Double-click `CoolRetroTerm.exe` or use the Start Menu shortcut
   - The terminal opens with a retro amber theme by default

2. **Basic Commands**
   ```
   help          - Show available commands
   clear / cls   - Clear the screen  
   exit          - Exit the application
   ver           - Show version information
   ```

3. **Windows Commands**
   - All standard Windows Command Prompt commands are available
   - `dir`, `cd`, `copy`, `del`, `ping`, etc.

### Keyboard Shortcuts

- **F11**: Toggle fullscreen mode
- **Ctrl+C**: Copy selected text
- **Ctrl+V**: Paste from clipboard
- **Ctrl+A**: Select all text
- **↑/↓**: Navigate command history
- **Ctrl+Plus/Minus**: Zoom in/out
- **Ctrl+0**: Reset zoom to default

### Customization

#### Changing Color Schemes
1. Go to **Settings** → **Color Scheme**
2. Choose from predefined themes or create custom colors
3. Changes apply immediately

#### Font Settings  
1. Go to **Settings** → **Font Settings**
2. Select from available monospace fonts
3. Adjust font size as needed

#### Visual Effects
1. Go to **Settings** → **Effects**
2. Toggle scanlines, glow, and screen curvature
3. Adjust effect intensity

### Configuration

Settings are automatically saved to:
```
%AppData%\CoolRetroTerm\settings.json
```

Example settings file:
```json
{
  "FontFamily": "Consolas",
  "FontSize": 14,
  "ForegroundColor": "#00FF00",
  "BackgroundColor": "#000000",
  "EnableScanlines": true,
  "EnableGlow": true,
  "EnableCurvature": true
}
```

## Troubleshooting

### Common Issues

**"Application won't start"**
- Ensure .NET Framework 4.8 is installed
- Check Windows Event Viewer for error details

**"Commands don't work"**
- Try running as Administrator
- Check Windows PATH environment variable

**"Text appears blurry"**
- Disable Windows DPI scaling for the application
- Right-click exe → Properties → Compatibility → Change high DPI settings

### Performance Tips

- Disable unnecessary visual effects if performance is slow
- Use hardware acceleration if available
- Close other applications if experiencing lag

## Development

Built with:
- **.NET Framework 4.8**: Core application framework
- **WPF (Windows Presentation Foundation)**: UI framework
- **System.Diagnostics.Process**: Windows Command Prompt integration
- **System.Drawing**: Font and graphics handling

Architecture:
- **MVVM Pattern**: Clean separation of concerns
- **Services**: Font management, file I/O, terminal communication
- **Custom Controls**: Retro terminal control with CRT effects
- **Settings Management**: JSON-based configuration persistence

## License

This project is licensed under the GNU General Public License v3.0 - see the [gpl-3.0.txt](gpl-3.0.txt) file for details.

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)  
5. Open a Pull Request

## Acknowledgments

- Original cool-retro-term by Filippo Scognamiglio
- Inspiration from classic terminal emulators and CRT monitors
- Windows .NET Framework community for excellent documentation
