using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Pricing : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private PricingIdentity _identity;
        private List<PricingDriver> _drivers;
        private List<PriceListGroup> _priceListGroups;
        private List<FilterGroup> _filterGroups;
        private List<ModuleFeature> _features;
        private List<PricingResult> _results;

        #endregion

        #region Constructors

        public Pricing()
        {
            Identity = new PricingIdentity();
            Drivers = new List<PricingDriver>();
            PriceListGroups = new List<PriceListGroup>();
            FilterGroups = new List<FilterGroup>();
            Features = new List<ModuleFeature>();
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

        public List<PriceListGroup> PriceListGroups
        {
            get { return _priceListGroups; }
            set { this.RaiseAndSetIfChanged(ref _priceListGroups, value); }
        }

        public List<FilterGroup> FilterGroups
        {
            get { return _filterGroups; }
            set { this.RaiseAndSetIfChanged(ref _filterGroups, value); }
        }

        public List<ModuleFeature> Features
        {
            get { return _features; }
            set { this.RaiseAndSetIfChanged(ref _features, value); }
        }

        public List<PricingResult> Results
        {
            get { return _results; }
            set { this.RaiseAndSetIfChanged(ref _results, value); }
        }

        #endregion

    }
}
