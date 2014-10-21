using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Analytic : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private AnalyticIdentity _identity;
        private List<AnalyticDriver> _drivers;
        private List<FilterGroup> _filterGroups;        
        private List<PriceListGroup> _priceListGroups;
        private List<AnalyticResult> _results;        

        #endregion

        #region Constructors

        public Analytic()
        {
            Identity = new AnalyticIdentity();
            FilterGroups = new List<FilterGroup>();
            Drivers = new List<AnalyticDriver>();
            PriceListGroups = new List<PriceListGroup>();
            Results = new List<AnalyticResult>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public List<FilterGroup> FilterGroups
        {
            get { return _filterGroups; }
            set { this.RaiseAndSetIfChanged(ref _filterGroups, value); }
        }

        public List<AnalyticDriver> Drivers
        {
            get { return _drivers; }
            set { this.RaiseAndSetIfChanged(ref _drivers, value); }
        }

        public List<PriceListGroup> PriceListGroups
        {
            get { return _priceListGroups; }
            set { this.RaiseAndSetIfChanged(ref _priceListGroups, value); }
        }

        public List<AnalyticResult> Results
        {
            get { return _results; }
            set { this.RaiseAndSetIfChanged(ref _results, value); }
        }
        public AnalyticIdentity Identity
        {
            get { return _identity; }
            set { this.RaiseAndSetIfChanged(ref _identity, value); }
        }

        #endregion

    }
}
