using System;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;


namespace APLPX.UI.WPF.ViewModels.Analytic
{
    /// <summary>
    /// View model for analytic identity-related views.
    /// </summary>
    public class AnalyticIdentityViewModel : ViewModelBase
    {
        private Display.Analytic _analytic;

        public AnalyticIdentityViewModel(Display.Analytic analytic)
        {
            _analytic = analytic;
        }

        public Display.Analytic Analytic
        {
            get { return _analytic; }
            private set
            {
                _analytic = value;
            }
        }
    }
}
