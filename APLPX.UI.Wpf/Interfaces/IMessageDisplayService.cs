using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLPX.UI.WPF.Interfaces
{
    /// <summary>
    /// Simple service for showing messages to the user and obtaining a response where applicable. 
    /// </summary>
    /// <remarks>Adapted from WPF Application Framework: https://waf.codeplex.com/ </remarks>
    public interface IMessageDisplayService
    {
        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="owner">The window that owns this Message Window.</param>
        /// <param name="message">The message.</param>
        void ShowMessage(object owner, string message);

        /// <summary>
        /// Shows the message as warning.
        /// </summary>
        /// <param name="owner">The window that owns this Message Window.</param>
        /// <param name="message">The message.</param>
        void ShowWarning(object owner, string message);

        /// <summary>
        /// Shows the message as error.
        /// </summary>
        /// <param name="owner">The window that owns this Message Window.</param>
        /// <param name="message">The message.</param>
        void ShowError(object owner, string message);

        /// <summary>
        /// Shows the specified question.
        /// </summary>
        /// <param name="owner">The window that owns this Message Window.</param>
        /// <param name="message">The question.</param>
        /// <returns><c>true</c> for yes, <c>false</c> for no and <c>null</c> for cancel.</returns>
        bool? ShowQuestion(object owner, string message);

        /// <summary>
        /// Shows the specified yes/no question.
        /// </summary>
        /// <param name="owner">The window that owns this Message Window.</param>
        /// <param name="message">The question.</param>
        /// <returns><c>true</c> for yes and <c>false</c> for no.</returns>
        bool ShowYesNoQuestion(object owner, string message);

        /// <summary>
        /// Shows an input box for a simple string.
        /// </summary>
        /// <param name="owner">The window that owns this Message Window.</param>
        /// <param name="title">The title to display for the input box.</param>
        /// <param name="text">The initial text to display in the entry portion of the input box</param>
        /// <returns>The entered text, as a string.</returns>
        string ShowInputBox(object owner, string title, string text);
    }
}

