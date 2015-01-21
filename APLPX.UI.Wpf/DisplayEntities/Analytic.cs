using System;
using System.Collections.Generic;

using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Analytic : DisplayEntityBase, ISearchableEntity, IFilterContainer
    {
        #region Private Fields

        private int _id;
        private AnalyticIdentity _identity;
        private List<AnalyticValueDriver> _valueDrivers;
        private List<FilterGroup> _filterGroups;
        private List<AnalyticPriceListGroup> _priceListGroups;
        private FilterGroup _selectedFilterGroup;
        private AnalyticPriceListGroup _selectedPriceListGroup;
        private AnalyticValueDriver _selectedValueDriver;
        private FeatureSearchGroup _searchGroup;
        private int _searchGroupId;
        private int _owningSearchGroupId;
        private string _searchKey;
        private bool _canNameChange;
        private bool _canSearchKeyChange;

        #endregion

        #region Constructors

        public Analytic()
        {
            Identity = new AnalyticIdentity();
            FilterGroups = new List<FilterGroup>();
            ValueDrivers = new List<AnalyticValueDriver>();
            PriceListGroups = new List<AnalyticPriceListGroup>();
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

        public List<AnalyticValueDriver> ValueDrivers
        {
            get { return _valueDrivers; }
            set { this.RaiseAndSetIfChanged(ref _valueDrivers, value); }
        }

        public List<AnalyticPriceListGroup> PriceListGroups
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

        public AnalyticPriceListGroup SelectedPriceListGroup
        {
            get { return _selectedPriceListGroup; }
            set { this.RaiseAndSetIfChanged(ref _selectedPriceListGroup, value); }
        }

        public AnalyticValueDriver SelectedValueDriver
        {
            get { return _selectedValueDriver; }
            set { this.RaiseAndSetIfChanged(ref _selectedValueDriver, value); }
        }

        #endregion

        #region ISearchableEntity

        public FeatureSearchGroup SearchGroup
        {
            get { return _searchGroup; }
            set { this.RaiseAndSetIfChanged(ref _searchGroup, value); }
        }

        public int SearchGroupId
        {
            get { return _searchGroupId; }
            set { this.RaiseAndSetIfChanged(ref _searchGroupId, value); }
        }

        /// <summary>
        /// Gets/sets the Id of the unique Search Group to which this entity belongs.
        /// Explanation: Although an entity can appear in several search groups, such as Recent or Shared,
        /// these are auxiliary groupings for display only. 
        /// The entity is actually associated with only the Owning Seach Group.
        /// </summary>
        public int OwningSearchGroupId
        {
            get { return _owningSearchGroupId; }
            set { this.RaiseAndSetIfChanged(ref _owningSearchGroupId, value); }
        }
        
        public string SearchGroupKey
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
