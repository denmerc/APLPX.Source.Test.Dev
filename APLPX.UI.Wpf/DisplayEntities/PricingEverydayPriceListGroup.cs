using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayPriceListGroup : PriceListGroup
    {
        private List<PricingEverydayPriceList> _priceLists;

        public PricingEverydayPriceListGroup()
        {
            PriceLists = new List<PricingEverydayPriceList>();
        }

        public List<PricingEverydayPriceList> PriceLists
        {
            get { return _priceLists; }
            set { this.RaiseAndSetIfChanged(ref _priceLists, value); }
        }

    }
}
