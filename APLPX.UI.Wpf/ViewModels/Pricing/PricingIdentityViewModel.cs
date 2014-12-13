using System;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    /// <summary>
    /// View model for price routine identity-related views.
    /// </summary>
    public class PricingIdentityViewModel : ViewModelBase
    {
        private ISearchableEntity _priceRoutine;

        public PricingIdentityViewModel(ISearchableEntity entity)
        {
            _priceRoutine = entity;
        }

        public ISearchableEntity PriceRoutine
        {
            get { return _priceRoutine; }
            private set
            {
                _priceRoutine = value;
            }
        }
    }
}
