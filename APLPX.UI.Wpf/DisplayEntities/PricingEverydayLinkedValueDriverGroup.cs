using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayLinkedValueDriverGroup : DisplayEntityBase
    {
        private int _valueDriverGroupId;
        private int _percentChange;

        public int ValueDriverGroupId
        {
            get { return _valueDriverGroupId; }
            set { this.RaiseAndSetIfChanged(ref _valueDriverGroupId, value); }
        }

        public int PercentChange
        {
            get { return _percentChange; }
            set { this.RaiseAndSetIfChanged(ref _percentChange, value); }
        }

    }
}
