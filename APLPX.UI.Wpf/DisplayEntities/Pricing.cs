using System;
using System.Collections.Generic;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Pricing : DisplayEntityBase, ISearchableEntity
    {
        #region Private Fields

        private int _id;
        private PricingIdentity _identity;
        private List<PricingDriver> _drivers;
        private List<AnalyticPriceListGroup> _priceListGroups;
        private List<FilterGroup> _filterGroups;
        private List<PricingResult> _results;
        private string _searchKey;
        private string _parentKey;
        private bool _canNameChange;
        private bool _canSearchKeyChange;

        #endregion

        #region Constructors

        public Pricing()
        {
            Identity = new PricingIdentity();
            Drivers = new List<PricingDriver>();
            PriceListGroups = new List<AnalyticPriceListGroup>();
            FilterGroups = new List<FilterGroup>();
            Results = new List<PricingResult>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public PricingIdentity Identity
        {
            get { return _identity; }
            set { this.RaiseAndSetIfChanged(ref _identity, value); }
        }
        public List<PricingDriver> Drivers
        {
            get { return _drivers; }
            set { this.RaiseAndSetIfChanged(ref _drivers, value); }
        }

        public List<AnalyticPriceListGroup> PriceListGroups
        {
            get { return _priceListGroups; }
            set { this.RaiseAndSetIfChanged(ref _priceListGroups, value); }
        }

        public List<FilterGroup> FilterGroups
        {
            get { return _filterGroups; }
            set { this.RaiseAndSetIfChanged(ref _filterGroups, value); }
        }

        public List<PricingResult> Results
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

        public string SearchKey
        {
            get { return _searchKey; }
            set { this.RaiseAndSetIfChanged(ref _searchKey, value); }
        }

        public string EntityTypeName
        {
            get { return GetType().Name; }
        }

        public bool CanNameChange
        {
            get { return _canNameChange; }
            set { _canNameChange = value; }
        }

        public bool CanSearchKeyChange
        {
            get { return _canSearchKeyChange; }
            set { _canSearchKeyChange = value; }
        }

        #endregion
    }
}
