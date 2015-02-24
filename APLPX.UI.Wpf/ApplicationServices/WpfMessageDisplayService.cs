using System;
using System.Globalization;
using System.Windows;
using APLPX.UI.WPF.Interfaces;
using APLPX.UI.WPF.ViewModels;
using APLPX.UI.WPF.Views;

namespace APLPX.UI.WPF.ApplicationServices
{
    /// <summary>
    /// Default WPF provider for the <see cref="IMessageDisplayService"/> interface. 
    /// Shows messages and obtains responses via WPF MessageBoxes and InputBoxes.
    /// to the user.
    /// </summary>    
    /// <remarks>Adapted from WPF Application Framework: https://waf.codeplex.com/ </remarks>
    public class WpfMessageDisplayService : IMessageDisplayService
    {
        private static MessageBoxResult MessageBoxResult { get { return MessageBoxResult.None; } }

        private static MessageBoxOptions MessageBoxOptions
        {
            get
            {
                return (CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft) ? MessageBoxOptions.RtlReading : MessageBoxOptions.None;
            }
        }

        /// <summary>
        /// Shows a message.
        /// </summary>
        /// <param name="owner">The window, if any,  that owns this Message Window.</param>
        /// <param name="message">The message.</param>
        public void ShowMessage(object owner, string message)
        {
            Window ownerWindow = owner as Window;
            if (ownerWindow != null)
            {
                MessageBox.Show(ownerWindow, message, ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Information,
                    MessageBoxResult, MessageBoxOptions);
            }
            else
            {
                MessageBox.Show(message, ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Information,
                    MessageBoxResult, MessageBoxOptions);
            }
        }

        /// <summary>
        /// Shows a message as a warning.
        /// </summary>
        /// <param name="owner">The window, if any,  that owns this Message Window.</param>
        /// <param name="message">The message.</param>
        public void ShowWarning(object owner, string message)
        {
            Window ownerWindow = owner as Window;
            if (ownerWindow != null)
            {
                MessageBox.Show(ownerWindow, message, ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Warning,
                    MessageBoxResult, MessageBoxOptions);
            }
            else
            {
                MessageBox.Show(message, ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Warning,
                    MessageBoxResult, MessageBoxOptions);
            }
        }

        /// <summary>
        /// Shows a message as an error.
        /// </summary>
        /// <param name="owner">The window, if any,  that owns this Message Window.</param>
        /// <param name="message">The message.</param>
        public void ShowError(object owner, string message)
        {
            Window ownerWindow = owner as Window;
            if (ownerWindow != null)
            {
                MessageBox.Show(ownerWindow, message, ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Error,
                    MessageBoxResult, MessageBoxOptions);
            }
            else
            {
                MessageBox.Show(message, ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Error,
                    MessageBoxResult, MessageBoxOptions);
            }
        }

        /// <summary>
        /// Shows the specified question and gets a yes/no/cancel response.
        /// </summary>
        /// <param name="owner">The window, if any,  that owns this Message Window.</param>
        /// <param name="message">The question.</param>
        /// <returns><c>true</c> for yes, <c>false</c> for no and <c>null</c> for cancel.</returns>
        public bool? ShowQuestion(object owner, string message)
        {
            bool? result = null; 
            MessageBoxResult response;

            Window ownerWindow = owner as Window;
            if (ownerWindow != null)
            {
                response = MessageBox.Show(ownerWindow, message, ApplicationInfo.ProductName, MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question, MessageBoxResult.Cancel, MessageBoxOptions);
            }
            else
            {
                response = MessageBox.Show(message, ApplicationInfo.ProductName, MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question, MessageBoxResult.Cancel, MessageBoxOptions);
            }
            if (response == MessageBoxResult.Yes)
            {
                result = true;
            }
            else if (response == MessageBoxResult.No)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Shows aquestion and gets a yes/no response.
        /// </summary>
        /// <param name="owner">The window , if any, that owns this Message Window.</param>
        /// <param name="message">The question.</param>
        /// <returns><c>true</c> for yes and <c>false</c> for no.</returns>
        public bool ShowYesNoQuestion(object owner, string message)
        {
            Window ownerWindow = owner as Window;
            MessageBoxResult response;
            if (ownerWindow != null)
            {
                response = MessageBox.Show(ownerWindow, message, ApplicationInfo.ProductName, MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions);
            }
            else
            {
                response = MessageBox.Show(message, ApplicationInfo.ProductName, MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions);
            }

            return (response == MessageBoxResult.Yes);
        }

        /// <summary>
        /// Shows an input box and gets the string entered by the user.
        /// </summary>
        /// <param name="owner">The window, if any,  that owns the InputBox window.</param>
        /// <param name="title">The title to display for the input box.</param>
        /// <param name="originalText">The initial text to display in the entry portion of the input box.</param>
        /// <returns>The entered text, as a string; an empty string if the user cancels.</returns>
        public string ShowInputBox(object owner, string title, string originalText)
        {
            string newText = String.Empty;

            InputBoxViewModel viewModel = new InputBoxViewModel(title, originalText);
            InputBox view = new InputBox(viewModel);

            bool completed = view.ShowDialog() ?? false;
            if (completed)
            {
                newText = viewModel.Text.Trim();
            }

            return newText;
        }
    }

}
