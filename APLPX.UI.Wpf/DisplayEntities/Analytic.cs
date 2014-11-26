using System;
using System.Collections.Generic;

using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Analytic : DisplayEntityBase, ISearchableEntity
    {
        #region Private Fields

        private int _id;
        private AnalyticIdentity _identity;
        private List<AnalyticDriver> _drivers;
        private List<FilterGroup> _filterGroups;
        private List<PriceListGroup> _priceListGroups;
        private FilterGroup _selectedFilterGroup;
        private string _searchKey;

        #endregion

        #region Constructors

        public Analytic()
        {
            Identity = new AnalyticIdentity();
            Drivers = new List<AnalyticDriver>();
            FilterGroups = new List<FilterGroup>();           
            PriceListGroups = new List<PriceListGroup>();            
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
  
        public AnalyticIdentity Identity
        {
            get { return _identity; }
            set { this.RaiseAndSetIfChanged(ref _identity, value); }
        }

        public FilterGroup SelectedFilterGroup
        {
            get { return _selectedFilterGroup; }
            set { this.RaiseAndSetIfChanged(ref _selectedFilterGroup, value); }
        }

        #endregion

        #region ISearchableEntity

        public string SearchKey
        {
            get { return _searchKey; }
            set { _searchKey = value; }
        }

        public string EntityTypeName
        {
            get { return this.GetType().Name; }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            string identityDescription = "Identity=null";
            if (identityDescription != null)
            {
                identityDescription = String.Format("Name={0};Owner={1}", Identity.Name, Identity.Owner);
            }

            string result = String.Format("{0}:Id={1};{2}", GetType().Name, Id, identityDescription);

            return result;
        }

        #endregion

    }
}
