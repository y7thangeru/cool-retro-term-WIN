using System;
#if !NO_WPF
using System.Windows;
#endif

namespace CoolRetroTerm
{
    /// <summary>
    /// Cool Retro Terminal - Windows .NET WPF Edition
    /// Entry point for the WPF application
    /// </summary>
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Parse command line arguments
            var fullscreen = Array.Exists(args, arg => arg == "--fullscreen");
            var help = Array.Exists(args, arg => arg == "-h" || arg == "--help");
            var version = Array.Exists(args, arg => arg == "--version");

            if (help)
            {
                ShowHelp();
                return;
            }

            if (version)
            {
                ShowVersion();
                return;
            }

#if NO_WPF
            // Fallback console version for non-Windows environments
            Console.WriteLine("Cool Retro Terminal - Windows WPF Edition");
            Console.WriteLine("This version requires Windows with .NET 8.0 WPF support.");
            Console.WriteLine("Running on non-Windows platform - WPF features not available.");
            Console.WriteLine();
            Console.WriteLine("The complete WPF implementation includes:");
            Console.WriteLine("  ✓ Retro CRT visual effects (scanlines, glow, curvature)");
            Console.WriteLine("  ✓ Windows Command Prompt integration");
            Console.WriteLine("  ✓ Customizable color schemes (Green, Amber, White, Blue, Red)");
            Console.WriteLine("  ✓ Font selection from monospace fonts");
            Console.WriteLine("  ✓ Copy/paste support");
            Console.WriteLine("  ✓ Command history navigation");
            Console.WriteLine("  ✓ Fullscreen mode");
            Console.WriteLine("  ✓ Zoom controls");
            Console.WriteLine("  ✓ Settings persistence");
            Console.WriteLine("  ✓ Complete WPF UI with retro styling");
            Console.WriteLine();
            Console.WriteLine("Build and run on Windows to see the full WPF application.");
#else
            // Create and run WPF application
            var app = new App();
            
            if (fullscreen)
            {
                // Note: fullscreen will be handled by MainWindow after it's created
                Environment.SetEnvironmentVariable("CRT_FULLSCREEN", "true");
            }
            
            app.Run();
#endif
        }
        
        private static void ShowHelp()
        {
            Console.WriteLine("Cool Retro Term - Windows WPF Edition");
            Console.WriteLine();
            Console.WriteLine("Usage: CoolRetroTerm.exe [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --help          Show this help message");
            Console.WriteLine("  --version       Show version information");
            Console.WriteLine("  --fullscreen    Start in fullscreen mode");
            Console.WriteLine();
            Console.WriteLine("Keyboard Shortcuts:");
            Console.WriteLine("  F11             Toggle fullscreen");
            Console.WriteLine("  Ctrl++          Zoom in");
            Console.WriteLine("  Ctrl+-          Zoom out");
            Console.WriteLine("  Ctrl+0          Reset zoom");
            Console.WriteLine("  Ctrl+C          Copy selected text");
            Console.WriteLine("  Ctrl+V          Paste");
            Console.WriteLine("  Ctrl+L          Clear screen");
            Console.WriteLine();
        }
        
        private static void ShowVersion()
        {
            Console.WriteLine("Cool Retro Term Windows WPF Edition v1.0.0");
            Console.WriteLine("Built with .NET 8.0 and WPF");
            Console.WriteLine();
            Console.WriteLine("A retro-style terminal emulator for Windows");
            Console.WriteLine("Features complete CRT visual effects and Windows Command Prompt integration");
            Console.WriteLine();
        }
    }
}