using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CoolRetroTerm.Models;
using CoolRetroTerm.Services;

namespace CoolRetroTerm.Views
{
    /// <summary>
    /// Settings window for configuring terminal appearance and behavior
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly TerminalSettings _settings;
        private readonly TerminalSettings _originalSettings;
        private readonly FontManagerService _fontManager;

        public SettingsWindow(TerminalSettings settings)
        {
            InitializeComponent();
            
            _settings = settings;
            _originalSettings = new TerminalSettings
            {
                FontFamily = settings.FontFamily,
                FontSize = settings.FontSize,
                ForegroundColor = settings.ForegroundColor,
                BackgroundColor = settings.BackgroundColor,
                Opacity = settings.Opacity,
                EnableScanlines = settings.EnableScanlines,
                EnableGlow = settings.EnableGlow,
                EnableCurvature = settings.EnableCurvature,
                ScanlineIntensity = settings.ScanlineIntensity,
                GlowIntensity = settings.GlowIntensity
            };
            
            _fontManager = new FontManagerService();
            
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Load font families
            var fonts = _fontManager.GetMonospaceFonts();
            FontFamilyComboBox.ItemsSource = fonts;
            FontFamilyComboBox.SelectedItem = _settings.FontFamily;

            // Load current settings
            FontSizeSlider.Value = _settings.FontSize;
            ForegroundColorTextBox.Text = _settings.ForegroundColor;
            BackgroundColorTextBox.Text = _settings.BackgroundColor;
            OpacitySlider.Value = _settings.Opacity;
            EnableScanlinesCheckBox.IsChecked = _settings.EnableScanlines;
            EnableGlowCheckBox.IsChecked = _settings.EnableGlow;
            EnableCurvatureCheckBox.IsChecked = _settings.EnableCurvature;
            ScanlineIntensitySlider.Value = _settings.ScanlineIntensity;
            GlowIntensitySlider.Value = _settings.GlowIntensity;
            
            // Set color preset based on current colors
            SetColorPreset();
        }

        private void SetColorPreset()
        {
            var foreground = _settings.ForegroundColor.ToUpper();
            var background = _settings.BackgroundColor.ToUpper();

            if (background == "#000000" || background == "BLACK")
            {
                switch (foreground)
                {
                    case "#00FF00":
                    case "LIME":
                    case "GREEN":
                        ColorPresetComboBox.SelectedIndex = 0; // Green on Black
                        break;
                    case "#FFAA00":
                    case "#FFA500":
                    case "ORANGE":
                        ColorPresetComboBox.SelectedIndex = 1; // Amber on Black
                        break;
                    case "#FFFFFF":
                    case "WHITE":
                        ColorPresetComboBox.SelectedIndex = 2; // White on Black
                        break;
                    case "#0088FF":
                    case "#0000FF":
                    case "BLUE":
                        ColorPresetComboBox.SelectedIndex = 3; // Blue on Black
                        break;
                    case "#FF0000":
                    case "RED":
                        ColorPresetComboBox.SelectedIndex = 4; // Red on Black
                        break;
                }
            }
        }

        private void ColorPresetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorPresetComboBox.SelectedItem is ComboBoxItem item)
            {
                var tag = item.Tag?.ToString();
                var colorSchemes = new Dictionary<string, (string fg, string bg)>
                {
                    { "green", ("#00FF00", "#000000") },
                    { "amber", ("#FFAA00", "#000000") },
                    { "white", ("#FFFFFF", "#000000") },
                    { "blue", ("#0088FF", "#000000") },
                    { "red", ("#FF0000", "#000000") }
                };

                if (tag != null && colorSchemes.ContainsKey(tag))
                {
                    var (fg, bg) = colorSchemes[tag];
                    ForegroundColorTextBox.Text = fg;
                    BackgroundColorTextBox.Text = bg;
                }
            }
        }

        private void ForegroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            // For now, just cycle through common colors
            var colors = new[] { "#00FF00", "#FFAA00", "#FFFFFF", "#0088FF", "#FF0000" };
            var currentIndex = System.Array.IndexOf(colors, ForegroundColorTextBox.Text);
            var nextIndex = (currentIndex + 1) % colors.Length;
            ForegroundColorTextBox.Text = colors[nextIndex];
        }

        private void BackgroundColorButton_Click(object sender, RoutedEventArgs e)
        {
            // For now, just cycle through dark colors
            var colors = new[] { "#000000", "#001100", "#110000", "#000011", "#111111" };
            var currentIndex = System.Array.IndexOf(colors, BackgroundColorTextBox.Text);
            var nextIndex = (currentIndex + 1) % colors.Length;
            BackgroundColorTextBox.Text = colors[nextIndex];
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset to default values
            FontFamilyComboBox.SelectedItem = "Consolas";
            FontSizeSlider.Value = 14;
            ForegroundColorTextBox.Text = "#00FF00";
            BackgroundColorTextBox.Text = "#000000";
            OpacitySlider.Value = 0.9;
            EnableScanlinesCheckBox.IsChecked = true;
            EnableGlowCheckBox.IsChecked = true;
            EnableCurvatureCheckBox.IsChecked = true;
            ScanlineIntensitySlider.Value = 50;
            GlowIntensitySlider.Value = 30;
            ColorPresetComboBox.SelectedIndex = 0;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Apply settings
            ApplySettings();
            DialogResult = true;
            Close();
        }

        private void ApplySettings()
        {
            if (FontFamilyComboBox.SelectedItem is string selectedFont)
            {
                _settings.FontFamily = selectedFont;
            }

            _settings.FontSize = (int)FontSizeSlider.Value;
            _settings.ForegroundColor = ForegroundColorTextBox.Text;
            _settings.BackgroundColor = BackgroundColorTextBox.Text;
            _settings.Opacity = OpacitySlider.Value;
            _settings.EnableScanlines = EnableScanlinesCheckBox.IsChecked ?? false;
            _settings.EnableGlow = EnableGlowCheckBox.IsChecked ?? false;
            _settings.EnableCurvature = EnableCurvatureCheckBox.IsChecked ?? false;
            _settings.ScanlineIntensity = (int)ScanlineIntensitySlider.Value;
            _settings.GlowIntensity = (int)GlowIntensitySlider.Value;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult != true)
            {
                // Restore original settings if cancelled
                _settings.FontFamily = _originalSettings.FontFamily;
                _settings.FontSize = _originalSettings.FontSize;
                _settings.ForegroundColor = _originalSettings.ForegroundColor;
                _settings.BackgroundColor = _originalSettings.BackgroundColor;
                _settings.Opacity = _originalSettings.Opacity;
                _settings.EnableScanlines = _originalSettings.EnableScanlines;
                _settings.EnableGlow = _originalSettings.EnableGlow;
                _settings.EnableCurvature = _originalSettings.EnableCurvature;
                _settings.ScanlineIntensity = _originalSettings.ScanlineIntensity;
                _settings.GlowIntensity = _originalSettings.GlowIntensity;
            }

            base.OnClosing(e);
        }
    }
}