using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingLinkedPriceListRule : DisplayEntityBase
    {
        #region Private Fields

        private int _priceListId;
        private int _percentChange;
        private List<PriceRoundingRule> _roundingRules;

        #endregion

        #region Constructors

        public PricingLinkedPriceListRule()
        {
            RoundingRules = new List<PriceRoundingRule>();
        }

        #endregion

        #region Properties

        public int PriceListId
        {
            get { return _priceListId; }
            set { this.RaiseAndSetIfChanged(ref _priceListId, value); }
        }

        public int PercentChange
        {
            get { return _percentChange; }
            set { this.RaiseAndSetIfChanged(ref _percentChange, value); }
        }

        public List<PriceRoundingRule> RoundingRules
        {
            get { return _roundingRules; }
            set { this.RaiseAndSetIfChanged(ref _roundingRules, value); }
        }


        #endregion

    }
}
