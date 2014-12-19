using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class PricingEverydayStrategyViewModel : ViewModelBase
    {

        private PricingEveryday _priceRoutine;

        public PricingEverydayStrategyViewModel(PricingEveryday priceRoutine)
        {
            if (priceRoutine == null)
            {
                throw new ArgumentNullException("priceRoutine");
            }

            PriceRoutine = priceRoutine; 
        }

        public PricingEveryday PriceRoutine
        {
            get { return _priceRoutine; }
            private set { _priceRoutine = value; }
        }
    }
}
