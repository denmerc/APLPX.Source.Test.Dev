using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingValueDriverGroup : ValueDriverGroup
    {
        private System.Int32 _skuCount;
        private System.String _salesValue;

        public System.Int32 SkuCount
        {
            get { return _skuCount; }
            set { this.RaiseAndSetIfChanged(ref _skuCount, value); }
        }

        public System.String SalesValue
        {
            get { return _salesValue; }
            set { this.RaiseAndSetIfChanged(ref _salesValue, value); }
        }

    }
}
