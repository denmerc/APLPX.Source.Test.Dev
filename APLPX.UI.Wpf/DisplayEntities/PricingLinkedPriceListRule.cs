using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingLinkedPriceListRule : DisplayEntityBase
    {
        #region Private Fields

        private int _priceListId;
        private decimal _percentChange;
        private List<PriceRoundingRule> _roundingRules;
        private List<SQLEnumeration> _roundingTypes;

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

        public decimal PercentChange
        {
            get { return _percentChange; }
            set { this.RaiseAndSetIfChanged(ref _percentChange, value); }
        }

        public List<PriceRoundingRule> RoundingRules
        {
            get { return _roundingRules; }
            set { this.RaiseAndSetIfChanged(ref _roundingRules, value); }
        }
        public List<SQLEnumeration> RoundingTypes
        {
            get { return _roundingTypes; }
            set { this.RaiseAndSetIfChanged(ref _roundingTypes, value); }
        }

        #endregion

    }
}
