using System.ComponentModel;
using Newtonsoft.Json;

namespace CoolRetroTerm.Models
{
    /// <summary>
    /// Terminal configuration settings
    /// </summary>
    public class TerminalSettings : INotifyPropertyChanged
    {
        private string _fontFamily = "Consolas";
        private int _fontSize = 14;
        private string _foregroundColor = "#00FF00";
        private string _backgroundColor = "#000000";
        private double _opacity = 0.9;
        private bool _enableScanlines = true;
        private bool _enableGlow = true;
        private bool _enableCurvature = true;
        private int _scanlineIntensity = 50;
        private int _glowIntensity = 30;

        [JsonProperty("fontFamily")]
        public string FontFamily
        {
            get => _fontFamily;
            set
            {
                _fontFamily = value;
                OnPropertyChanged(nameof(FontFamily));
            }
        }

        [JsonProperty("fontSize")]
        public int FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }

        [JsonProperty("foregroundColor")]
        public string ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = value;
                OnPropertyChanged(nameof(ForegroundColor));
            }
        }

        [JsonProperty("backgroundColor")]
        public string BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        [JsonProperty("opacity")]
        public double Opacity
        {
            get => _opacity;
            set
            {
                _opacity = value;
                OnPropertyChanged(nameof(Opacity));
            }
        }

        [JsonProperty("enableScanlines")]
        public bool EnableScanlines
        {
            get => _enableScanlines;
            set
            {
                _enableScanlines = value;
                OnPropertyChanged(nameof(EnableScanlines));
            }
        }

        [JsonProperty("enableGlow")]
        public bool EnableGlow
        {
            get => _enableGlow;
            set
            {
                _enableGlow = value;
                OnPropertyChanged(nameof(EnableGlow));
            }
        }

        [JsonProperty("enableCurvature")]
        public bool EnableCurvature
        {
            get => _enableCurvature;
            set
            {
                _enableCurvature = value;
                OnPropertyChanged(nameof(EnableCurvature));
            }
        }

        [JsonProperty("scanlineIntensity")]
        public int ScanlineIntensity
        {
            get => _scanlineIntensity;
            set
            {
                _scanlineIntensity = value;
                OnPropertyChanged(nameof(ScanlineIntensity));
            }
        }

        [JsonProperty("glowIntensity")]
        public int GlowIntensity
        {
            get => _glowIntensity;
            set
            {
                _glowIntensity = value;
                OnPropertyChanged(nameof(GlowIntensity));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Create a deep copy of the settings
        /// </summary>
        public TerminalSettings Clone()
        {
            return new TerminalSettings
            {
                FontFamily = this.FontFamily,
                FontSize = this.FontSize,
                ForegroundColor = this.ForegroundColor,
                BackgroundColor = this.BackgroundColor,
                Opacity = this.Opacity,
                EnableScanlines = this.EnableScanlines,
                EnableGlow = this.EnableGlow,
                EnableCurvature = this.EnableCurvature,
                ScanlineIntensity = this.ScanlineIntensity,
                GlowIntensity = this.GlowIntensity
            };
        }
    }
}