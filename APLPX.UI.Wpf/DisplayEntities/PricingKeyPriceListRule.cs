﻿using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingKeyPriceListRule : DisplayEntityBase
    {
        #region Private Fields

        private int _priceListId;
        private decimal _dollarRangeLower;
        private decimal _dollarRangeUpper;
        private List<PriceRoundingRule> _roundingRules;
        private List<SQLEnumeration> _roundingTypes;

        #endregion

        #region Constructors

        public PricingKeyPriceListRule()
        {
            RoundingRules = new List<PriceRoundingRule>();
            RoundingTypes = new List<SQLEnumeration>();
        }

        #endregion

        #region Properties

        public int PriceListId
        {
            get { return _priceListId; }
            set { this.RaiseAndSetIfChanged(ref _priceListId, value); }
        }

        public decimal DollarRangeLower
        {
            get { return _dollarRangeLower; }
            set { this.RaiseAndSetIfChanged(ref _dollarRangeLower, value); }
        }

        public decimal DollarRangeUpper
        {
            get { return _dollarRangeUpper; }
            set { this.RaiseAndSetIfChanged(ref _dollarRangeUpper, value); }
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
