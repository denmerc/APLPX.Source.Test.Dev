using System;
using System.ComponentModel;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels
{
    /// <summary>
    /// View model for a simple input box view.
    /// </summary>
    public class InputBoxViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _text;
        private string _title;
        private bool _isValid;

        public InputBoxViewModel(string title, string text)
        {
            Title = title;
            Text = text;
        }

        public string Text
        {
            get { return _text; }
            private set { this.RaiseAndSetIfChanged(ref _text, value); }
        }

        public string Title
        {
            get { return _title; }
            private set { this.RaiseAndSetIfChanged(ref _title, value); }
        }

        
        public bool IsValid
        {
            private set { this.RaiseAndSetIfChanged(ref _isValid, value); }
            get { return _isValid; }
        }

        #region IDataErrorInfo

        public string Error
        {
            get { return String.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName == "Text")
                {
                    if (String.IsNullOrWhiteSpace(Text))
                    {
                        result = "Entry is required.";
                    }
                }

                IsValid = String.IsNullOrWhiteSpace(result);

                return result;
            }
        }

        #endregion
    }
}
