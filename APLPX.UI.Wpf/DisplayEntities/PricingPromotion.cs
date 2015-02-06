using System;
using System.Collections.Generic;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingPromotion : DisplayEntityBase, ISearchableEntity, IFilterContainer
    {
        #region Private Fields

        private int _id;
        private PricingIdentity _identity;
        private ReactiveList<FilterGroup> _filterGroups;
        private FeatureSearchGroup _searchGroup;
        private int _searchGroupId;
        private int _owningSearchGroupId;
        private string _searchGroupKey;        
        private bool _canNameChange;
        private bool _canSearchKeyChange;        
        private FilterGroup _selectedFilterGroup;

        #endregion

        #region Constructors

        public PricingPromotion()
        {
            Identity = new PricingIdentity();
            FilterGroups = new ReactiveList<FilterGroup>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public int SearchGroupId
        {
            get { return _searchGroupId; }
            set { this.RaiseAndSetIfChanged(ref _searchGroupId, value); }
        }

        public string SearchGroupKey
        {
            get { return _searchGroupKey; }
            set { this.RaiseAndSetIfChanged(ref _searchGroupKey, value); }
        }

        public PricingIdentity Identity
        {
            get { return _identity; }
            set { this.RaiseAndSetIfChanged(ref _identity, value); }
        }

        public ReactiveList<FilterGroup> FilterGroups
        {
            get { return _filterGroups; }
            set { this.RaiseAndSetIfChanged(ref _filterGroups, value); }
        }

        #endregion

        #region ISearchableEntity       

        public FeatureSearchGroup SearchGroup
        {
            get { return _searchGroup; }
            set { this.RaiseAndSetIfChanged(ref _searchGroup, value); }
        }

        public int OwningSearchGroupId
        {
            get { return _owningSearchGroupId; }
            set { this.RaiseAndSetIfChanged(ref _owningSearchGroupId, value); }
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

        public FilterGroup SelectedFilterGroup
        {
            get { return _selectedFilterGroup; }
            set { this.RaiseAndSetIfChanged(ref _selectedFilterGroup, value); }
        }

        #endregion

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

    }
}
