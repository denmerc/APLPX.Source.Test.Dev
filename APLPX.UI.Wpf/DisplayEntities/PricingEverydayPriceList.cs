using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayPriceList : PriceList
    {
        private bool _isKey;
        private PricingLinkedPriceListRule _linkedPriceListRule;

        public bool IsKey
        {
            get { return _isKey; }
            set { this.RaiseAndSetIfChanged(ref _isKey, value); }
        }

        public PricingLinkedPriceListRule LinkedPriceListRule
        {
            get { return _linkedPriceListRule; }
            set { this.RaiseAndSetIfChanged(ref _linkedPriceListRule, value); }
        }
    }
}
