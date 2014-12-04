using System;
using System.Collections.Generic;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEveryday : DisplayEntityBase, ISearchableEntity
    {
        #region Private Fields

        private int _id;        
        private PricingIdentity _identity;
        private List<FilterGroup> _filterGroups;
        private List<PricingEverydayValueDriver> _valueDrivers;
        private PricingEverydayKeyValueDriver _keyValueDriver;
        private List<PricingEverydayLinkedValueDriverGroup> _linkedValueDrivers;
        private List<PricingMode> _pricingModes;
        private List<PricingEverydayPriceListGroup> _priceListGroups;
        private PricingKeyPriceListRule _keyPriceListRule;
        private List<PricingLinkedPriceListRule> _linkedPriceListRules;
        private List<PricingEverydayResult> _results;
        private string _searchGroupKey;
        private string _parentKey;
        private bool _canNameChange;

        #endregion

        #region Constructors

        public PricingEveryday()
        {
            Identity = new PricingIdentity();
            FilterGroups = new List<FilterGroup>();
            ValueDrivers = new List<PricingEverydayValueDriver>();
            KeyValueDriver = new PricingEverydayKeyValueDriver();
            LinkedValueDrivers = new List<PricingEverydayLinkedValueDriverGroup>();
            PricingModes = new List<PricingMode>();
            PriceListGroups = new List<PricingEverydayPriceListGroup>();
            KeyPriceListRule = new PricingKeyPriceListRule();
            LinkedPriceListRules = new List<PricingLinkedPriceListRule>();
            Results = new List<PricingEverydayResult>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public string SearchKey
        {
            get { return _searchGroupKey; }
            set { this.RaiseAndSetIfChanged(ref _searchGroupKey, value); }
        }

        public PricingIdentity Identity
        {
            get { return _identity; }
            set { this.RaiseAndSetIfChanged(ref _identity, value); }
        }

        public List<FilterGroup> FilterGroups
        {
            get { return _filterGroups; }
            set { this.RaiseAndSetIfChanged(ref _filterGroups, value); }
        }

        public List<PricingEverydayValueDriver> ValueDrivers
        {
            get { return _valueDrivers; }
            set { this.RaiseAndSetIfChanged(ref _valueDrivers, value); }
        }

        public PricingEverydayKeyValueDriver KeyValueDriver
        {
            get { return _keyValueDriver; }
            set { this.RaiseAndSetIfChanged(ref _keyValueDriver, value); }
        }

        public List<PricingEverydayLinkedValueDriverGroup> LinkedValueDrivers
        {
            get { return _linkedValueDrivers; }
            set { this.RaiseAndSetIfChanged(ref _linkedValueDrivers, value); }
        }

        public List<PricingMode> PricingModes
        {
            get { return _pricingModes; }
            set { this.RaiseAndSetIfChanged(ref _pricingModes, value); }
        }

        public List<PricingEverydayPriceListGroup> PriceListGroups
        {
            get { return _priceListGroups; }
            set { this.RaiseAndSetIfChanged(ref _priceListGroups, value); }
        }

        public PricingKeyPriceListRule KeyPriceListRule
        {
            get { return _keyPriceListRule; }
            set { this.RaiseAndSetIfChanged(ref _keyPriceListRule, value); }
        }

        public List<PricingLinkedPriceListRule> LinkedPriceListRules
        {
            get { return _linkedPriceListRules; }
            set { this.RaiseAndSetIfChanged(ref _linkedPriceListRules, value); }
        }

        public List<PricingEverydayResult> Results
        {
            get { return _results; }
            set { this.RaiseAndSetIfChanged(ref _results, value); }
        }

        #endregion

        #region ISearchableEntity

        public string ParentKey
        {
            get { return _parentKey; }
            set { _parentKey = value; }
        }

        public string EntityTypeName
        {
            get { return GetType().Name; }
        }

        #endregion
   

        public bool CanNameChange
        {
            get { return _canNameChange; }
            set { _canNameChange = value; }
        }
    }
}
