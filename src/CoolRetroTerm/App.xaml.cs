#if !NO_WPF
using System.Windows;
using CoolRetroTerm.Views;

namespace CoolRetroTerm
{
    /// <summary>
    /// Cool Retro Terminal - Windows WPF Application
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create and show the main window
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Handle fullscreen startup parameter
            if (Environment.GetEnvironmentVariable("CRT_FULLSCREEN") == "true")
            {
                mainWindow.WindowState = WindowState.Maximized;
                mainWindow.WindowStyle = WindowStyle.None;
            }
        }
    }
}
#endif