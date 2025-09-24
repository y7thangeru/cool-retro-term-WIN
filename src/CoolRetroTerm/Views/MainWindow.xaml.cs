using System;
using System.Windows;
using System.Windows.Input;
using CoolRetroTerm.ViewModels;
using CoolRetroTerm.Services;

namespace CoolRetroTerm.Views
{
    /// <summary>
    /// Main window for Cool Retro Terminal
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        private bool _isFullscreen = false;
        private WindowState _previousWindowState;
        private WindowStyle _previousWindowStyle;
        private double _previousLeft, _previousTop, _previousWidth, _previousHeight;

        public MainWindow()
        {
            InitializeComponent();
            
            // Initialize view model
            var terminalService = new TerminalService();
            _viewModel = new MainViewModel(terminalService);
            DataContext = _viewModel;

            // Set up window events
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            KeyDown += OnKeyDown;
            Loaded += OnLoaded;
            Closing += OnClosing;

            // Apply initial settings
            ApplySettings();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Set up terminal service connection
            TerminalDisplay.SetTerminalService(_viewModel.TerminalService);
            
            // Focus the terminal control
            TerminalDisplay.Focus();
            
            // Start terminal session
            _viewModel.StartTerminalSession();
            
            StatusText.Text = "Terminal Ready";
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Save settings before closing
            _viewModel.SaveSettings();
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Handle global shortcuts
            if (e.Key == Key.F11)
            {
                ToggleFullscreen();
                e.Handled = true;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.OemPlus:
                    case Key.Add:
                        ZoomIn();
                        e.Handled = true;
                        break;
                    case Key.OemMinus:
                    case Key.Subtract:
                        ZoomOut();
                        e.Handled = true;
                        break;
                    case Key.D0:
                        ResetZoom();
                        e.Handled = true;
                        break;
                }
            }
        }

        #region Window Controls

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                MaximizeButton.Content = "□";
            }
            else
            {
                WindowState = WindowState.Maximized;
                MaximizeButton.Content = "❐";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(_viewModel.Settings)
            {
                Owner = this
            };

            if (settingsWindow.ShowDialog() == true)
            {
                ApplySettings();
                StatusText.Text = "Settings Updated";
            }
        }

        #endregion

        #region Display Effects

        private void ApplySettings()
        {
            var settings = _viewModel.Settings;

            // Apply scanlines
            ScanlinesOverlay.Opacity = settings.EnableScanlines ? settings.ScanlineIntensity / 100.0 * 0.3 : 0;

            // Apply glow effect
            GlowOverlay.Opacity = settings.EnableGlow ? settings.GlowIntensity / 100.0 * 0.1 : 0;

            // Apply window opacity
            CrtFrame.Opacity = settings.Opacity;

            // Update terminal display settings
            TerminalDisplay.ApplySettings(settings);
        }

        private void ToggleFullscreen()
        {
            if (!_isFullscreen)
            {
                // Store current window state
                _previousWindowState = WindowState;
                _previousWindowStyle = WindowStyle;
                _previousLeft = Left;
                _previousTop = Top;
                _previousWidth = Width;
                _previousHeight = Height;

                // Go fullscreen
                WindowStyle = WindowStyle.None;
                WindowState = WindowState.Maximized;
                Topmost = true;
                _isFullscreen = true;
                MaximizeButton.Content = "❐";
            }
            else
            {
                // Restore window
                WindowStyle = _previousWindowStyle;
                WindowState = _previousWindowState;
                Topmost = false;
                
                if (_previousWindowState != WindowState.Maximized)
                {
                    Left = _previousLeft;
                    Top = _previousTop;
                    Width = _previousWidth;
                    Height = _previousHeight;
                }

                _isFullscreen = false;
                MaximizeButton.Content = WindowState == WindowState.Maximized ? "❐" : "□";
            }
        }

        private void ZoomIn()
        {
            var settings = _viewModel.Settings;
            if (settings.FontSize < 32)
            {
                settings.FontSize += 2;
                TerminalDisplay.ApplySettings(settings);
                StatusText.Text = $"Font Size: {settings.FontSize}";
            }
        }

        private void ZoomOut()
        {
            var settings = _viewModel.Settings;
            if (settings.FontSize > 8)
            {
                settings.FontSize -= 2;
                TerminalDisplay.ApplySettings(settings);
                StatusText.Text = $"Font Size: {settings.FontSize}";
            }
        }

        private void ResetZoom()
        {
            var settings = _viewModel.Settings;
            settings.FontSize = 14;
            TerminalDisplay.ApplySettings(settings);
            StatusText.Text = "Font Size Reset";
        }

        #endregion
    }
}