using System;
using System.Collections.Generic;
using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels.Analytic
{
    /// <summary>
    /// View model for analytic driver-related views.
    /// </summary>
    public class AnalyticDriverViewModel : ViewModelBase
    {
        private List<AnalyticDriver> _drivers;

        public AnalyticDriverViewModel(List<AnalyticDriver> drivers)
        {
            Drivers = drivers;
        }

        public List<AnalyticDriver> Drivers
        {
            get { return _drivers; }
            private set { this.RaiseAndSetIfChanged(ref _drivers, value); }
        }
    }
}
