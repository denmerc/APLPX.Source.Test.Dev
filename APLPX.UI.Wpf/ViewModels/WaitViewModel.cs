using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels
{
    public class WaitViewModel : ViewModelBase
    {

        private bool _isIndicatorVisible;
        private string _statusMessage;

        public WaitViewModel(bool showIndicator = false, string statusMessage="")
        {
            IsIndicatorVisible = showIndicator;
            StatusMessage = statusMessage;
        }

        public bool IsIndicatorVisible
        {
            get { return _isIndicatorVisible; }
            set { this.RaiseAndSetIfChanged(ref _isIndicatorVisible, value); }
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
            set { this.RaiseAndSetIfChanged(ref _statusMessage, value); }
        }

    }
}
