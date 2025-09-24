using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CoolRetroTerm.Models;
using CoolRetroTerm.Services;

namespace CoolRetroTerm.Controls
{
    /// <summary>
    /// Terminal control that handles command input/output and visual effects
    /// </summary>
    public partial class TerminalControl : UserControl
    {
        private TerminalService? _terminalService;
        private readonly List<string> _commandHistory = new();
        private int _historyIndex = -1;
        private string _currentInput = string.Empty;
        private int _promptStartIndex = 0;
        private readonly StringBuilder _outputBuffer = new();

        public TerminalControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            
            // Set up terminal output events
            TerminalOutput.GotFocus += (s, e) => UpdateCursorPosition();
            TerminalOutput.SelectionChanged += (s, e) => UpdateCursorPosition();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Focus();
            TerminalOutput.Focus();
            TerminalOutput.CaretIndex = TerminalOutput.Text.Length;
            UpdatePromptStartIndex();
        }

        public void SetTerminalService(TerminalService terminalService)
        {
            if (_terminalService != null)
            {
                _terminalService.OutputReceived -= OnOutputReceived;
                _terminalService.ErrorReceived -= OnErrorReceived;
            }

            _terminalService = terminalService;
            
            if (_terminalService != null)
            {
                _terminalService.OutputReceived += OnOutputReceived;
                _terminalService.ErrorReceived += OnErrorReceived;
            }
        }

        private void OnOutputReceived(object? sender, string output)
        {
            Dispatcher.Invoke(() =>
            {
                AppendOutput(output);
            });
        }

        private void OnErrorReceived(object? sender, string error)
        {
            Dispatcher.Invoke(() =>
            {
                AppendOutput(error, true);
            });
        }

        private void AppendOutput(string text, bool isError = false)
        {
            var color = isError ? "#FF4444" : "#00FF00";
            
            // Add the output to the terminal
            TerminalOutput.AppendText(text + Environment.NewLine);
            
            // Add new prompt
            var prompt = "C:\\Users\\User>";
            TerminalOutput.AppendText(prompt);
            
            // Update prompt start index
            UpdatePromptStartIndex();
            
            // Scroll to bottom
            TerminalScrollViewer.ScrollToEnd();
        }

        private void UpdatePromptStartIndex()
        {
            var text = TerminalOutput.Text;
            var lastPromptIndex = text.LastIndexOf("C:\\Users\\User>");
            _promptStartIndex = lastPromptIndex >= 0 ? lastPromptIndex + "C:\\Users\\User>".Length : text.Length;
        }

        private void TerminalOutput_KeyDown(object sender, KeyEventArgs e)
        {
            var currentCaretIndex = TerminalOutput.CaretIndex;
            
            // Prevent editing before the prompt
            if (currentCaretIndex < _promptStartIndex && e.Key != Key.C && e.Key != Key.A)
            {
                e.Handled = true;
                TerminalOutput.CaretIndex = TerminalOutput.Text.Length;
                return;
            }

            switch (e.Key)
            {
                case Key.Enter:
                    ProcessCommand();
                    e.Handled = true;
                    break;
                    
                case Key.Up:
                    NavigateHistory(-1);
                    e.Handled = true;
                    break;
                    
                case Key.Down:
                    NavigateHistory(1);
                    e.Handled = true;
                    break;
                    
                case Key.C when Keyboard.Modifiers == ModifierKeys.Control:
                    if (!string.IsNullOrEmpty(TerminalOutput.SelectedText))
                    {
                        Clipboard.SetText(TerminalOutput.SelectedText);
                    }
                    e.Handled = true;
                    break;
                    
                case Key.V when Keyboard.Modifiers == ModifierKeys.Control:
                    if (Clipboard.ContainsText())
                    {
                        var clipboardText = Clipboard.GetText();
                        InsertTextAtCaret(clipboardText);
                    }
                    e.Handled = true;
                    break;
                    
                case Key.L when Keyboard.Modifiers == ModifierKeys.Control:
                    ClearTerminal();
                    e.Handled = true;
                    break;
                    
                case Key.Home:
                    TerminalOutput.CaretIndex = _promptStartIndex;
                    e.Handled = true;
                    break;
                    
                case Key.Back:
                    if (currentCaretIndex <= _promptStartIndex)
                    {
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void TerminalOutput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Additional key handling if needed
        }

        private void TerminalOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCursorPosition();
        }

        private void ProcessCommand()
        {
            var fullText = TerminalOutput.Text;
            var currentCommand = fullText.Substring(_promptStartIndex).Trim();
            
            if (!string.IsNullOrEmpty(currentCommand))
            {
                // Add to command history
                _commandHistory.Add(currentCommand);
                _historyIndex = _commandHistory.Count;
                
                // Execute the command
                ExecuteCommand(currentCommand);
            }
            else
            {
                // Just add a new prompt for empty command
                TerminalOutput.AppendText(Environment.NewLine + "C:\\Users\\User>");
                UpdatePromptStartIndex();
            }
            
            TerminalScrollViewer.ScrollToEnd();
        }

        private void ExecuteCommand(string command)
        {
            TerminalOutput.AppendText(Environment.NewLine);
            
            // Handle built-in commands
            switch (command.ToLower().Split(' ')[0])
            {
                case "clear":
                case "cls":
                    ClearTerminal();
                    return;
                    
                case "help":
                    ShowHelp();
                    return;
                    
                case "echo":
                    var echoText = command.Substring(4).Trim();
                    TerminalOutput.AppendText(echoText + Environment.NewLine);
                    break;
                    
                case "exit":
                    Application.Current.Shutdown();
                    return;
                    
                default:
                    // Send to terminal service if available
                    if (_terminalService != null && _terminalService.IsSessionRunning)
                    {
                        _terminalService.SendInput(command);
                        return;
                    }
                    else
                    {
                        TerminalOutput.AppendText($"'{command}' is not recognized as an internal or external command." + Environment.NewLine);
                    }
                    break;
            }
            
            TerminalOutput.AppendText("C:\\Users\\User>");
            UpdatePromptStartIndex();
        }

        private void ShowHelp()
        {
            var helpText = @"Cool Retro Term - Available Commands:
  clear, cls    Clear the terminal screen
  help          Show this help message
  echo <text>   Display text
  exit          Close the application
  
Keyboard shortcuts:
  Ctrl+C        Copy selected text
  Ctrl+V        Paste text
  Ctrl+L        Clear screen
  F11           Toggle fullscreen
  Ctrl++        Zoom in
  Ctrl+-        Zoom out
  Ctrl+0        Reset zoom
  Up/Down       Navigate command history

";
            TerminalOutput.AppendText(helpText);
        }

        private void ClearTerminal()
        {
            TerminalOutput.Clear();
            TerminalOutput.Text = "Cool Retro Term v1.0\nWindows .NET Edition\n\nC:\\Users\\User>";
            UpdatePromptStartIndex();
            TerminalOutput.CaretIndex = TerminalOutput.Text.Length;
        }

        private void NavigateHistory(int direction)
        {
            if (_commandHistory.Count == 0) return;
            
            _historyIndex += direction;
            
            if (_historyIndex < 0)
                _historyIndex = 0;
            else if (_historyIndex >= _commandHistory.Count)
                _historyIndex = _commandHistory.Count;
            
            // Clear current input
            var beforePrompt = TerminalOutput.Text.Substring(0, _promptStartIndex);
            var commandToShow = _historyIndex < _commandHistory.Count ? _commandHistory[_historyIndex] : "";
            
            TerminalOutput.Text = beforePrompt + commandToShow;
            TerminalOutput.CaretIndex = TerminalOutput.Text.Length;
        }

        private void InsertTextAtCaret(string text)
        {
            var caretIndex = TerminalOutput.CaretIndex;
            if (caretIndex >= _promptStartIndex)
            {
                TerminalOutput.Text = TerminalOutput.Text.Insert(caretIndex, text);
                TerminalOutput.CaretIndex = caretIndex + text.Length;
            }
        }

        private void UpdateCursorPosition()
        {
            // Update visual cursor position if needed
        }

        public void ApplySettings(TerminalSettings settings)
        {
            TerminalOutput.FontFamily = new FontFamily(settings.FontFamily);
            TerminalOutput.FontSize = settings.FontSize;
            
            // Apply colors
            try
            {
                var foregroundColor = (Color)ColorConverter.ConvertFromString(settings.ForegroundColor);
                var backgroundColor = (Color)ColorConverter.ConvertFromString(settings.BackgroundColor);
                
                TerminalOutput.Foreground = new SolidColorBrush(foregroundColor);
                TerminalOutput.Background = new SolidColorBrush(backgroundColor);
                TerminalOutput.CaretBrush = new SolidColorBrush(foregroundColor);
            }
            catch
            {
                // Use defaults if color parsing fails
                TerminalOutput.Foreground = new SolidColorBrush(Colors.Lime);
                TerminalOutput.Background = Brushes.Transparent;
                TerminalOutput.CaretBrush = new SolidColorBrush(Colors.Lime);
            }
        }
    }
}