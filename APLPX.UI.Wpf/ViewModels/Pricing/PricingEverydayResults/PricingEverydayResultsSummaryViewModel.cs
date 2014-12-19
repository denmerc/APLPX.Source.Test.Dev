using System;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;
using System.Collections.Generic;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class PricingEverydayResultsSummaryViewModel : ViewModelBase
    {
        private ISearchableEntity _priceRoutine;

        public PricingEverydayResultsSummaryViewModel(ISearchableEntity entity)
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
