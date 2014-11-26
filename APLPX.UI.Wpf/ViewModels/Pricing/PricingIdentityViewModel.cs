using System;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    /// <summary>
    /// View model for price routine identity-related views.
    /// </summary>
    public class PricingIdentityViewModel:ViewModelBase
    {
        private Display.Pricing _priceRoutine;

        public PricingIdentityViewModel(Display.Pricing pricing)
        {
            _priceRoutine = pricing;
        }

        public Display.Pricing PriceRoutine
        {
            get { return _priceRoutine; }
            private set
            {
                _priceRoutine = value;
            }
        }
    }
}
