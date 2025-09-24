using System.ComponentModel;
using System.IO;
using CoolRetroTerm.Models;
using CoolRetroTerm.Services;

namespace CoolRetroTerm.ViewModels
{
    /// <summary>
    /// Main view model for the terminal application
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly TerminalService _terminalService;
        private readonly FontManagerService _fontManager;
        private readonly FileIOService _fileIOService;
        private TerminalSettings _settings;
        private string _statusText = "Ready";

        public MainViewModel(TerminalService terminalService)
        {
            _terminalService = terminalService;
            _fontManager = new FontManagerService();
            _fileIOService = new FileIOService();
            _settings = new TerminalSettings();

            LoadSettings();
        }

        public TerminalSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                OnPropertyChanged(nameof(Settings));
            }
        }

        public string StatusText
        {
            get => _statusText;
            set
            {
                _statusText = value;
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public FontManagerService FontManager => _fontManager;

        public void StartTerminalSession()
        {
            if (_terminalService != null && !_terminalService.IsSessionRunning)
            {
                _terminalService.StartNewSession();
                StatusText = "Terminal session started";
            }
        }

        public TerminalService TerminalService => _terminalService;

        private void LoadSettings()
        {
            // Load settings from application data directory
            var settingsPath = Path.Combine(
                _fileIOService.GetApplicationDataDirectory(),
                "settings.json");

            if (_fileIOService.FileExists(settingsPath))
            {
                try
                {
                    var json = _fileIOService.ReadFile(settingsPath);
                    if (!string.IsNullOrEmpty(json))
                    {
                        var loadedSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<TerminalSettings>(json);
                        if (loadedSettings != null)
                        {
                            Settings.FontFamily = loadedSettings.FontFamily;
                            Settings.FontSize = loadedSettings.FontSize;
                            Settings.ForegroundColor = loadedSettings.ForegroundColor;
                            Settings.BackgroundColor = loadedSettings.BackgroundColor;
                            Settings.Opacity = loadedSettings.Opacity;
                            Settings.EnableScanlines = loadedSettings.EnableScanlines;
                            Settings.EnableGlow = loadedSettings.EnableGlow;
                            Settings.EnableCurvature = loadedSettings.EnableCurvature;
                            Settings.ScanlineIntensity = loadedSettings.ScanlineIntensity;
                            Settings.GlowIntensity = loadedSettings.GlowIntensity;
                        }
                    }
                }
                catch
                {
                    // Use default settings if loading fails
                }
            }

            // Ensure we have a valid monospace font
            var bestFont = _fontManager.GetBestMonospaceFont();
            if (!string.IsNullOrEmpty(bestFont))
            {
                Settings.FontFamily = bestFont;
            }
        }

        public void SaveSettings()
        {
            var settingsPath = Path.Combine(
                _fileIOService.GetApplicationDataDirectory(),
                "settings.json");

            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(Settings, Newtonsoft.Json.Formatting.Indented);
                _fileIOService.WriteFile(settingsPath, json);
            }
            catch
            {
                // Ignore save errors for now
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}