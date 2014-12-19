using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using APLPX.UI.WPF.Interfaces;

//TODO: MUST REMOVE ALL REFERENCES TO MONGO IN DISPLAY ENTITIES!
using MongoDB.Bson.Serialization.Attributes;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class PricingEveryday : DisplayEntityBase, ISearchableEntity
    {
        #region Private Fields

        private int _id;
        private PricingIdentity _identity;
        private List<FilterGroup> _filterGroups;
        private List<PricingEverydayValueDriver> _valueDrivers;
        private PricingEverydayKeyValueDriver _keyValueDriver;
        private List<PricingEverydayLinkedValueDriver> _linkedValueDrivers;
        private List<PricingMode> _pricingModes;
        private List<PricingEverydayPriceListGroup> _priceListGroups;
        private PricingKeyPriceListRule _keyPriceListRule;
        private List<PricingLinkedPriceListRule> _linkedPriceListRules;
        private List<PricingEverydayResult> _results;

        private PricingEverydayPriceListGroup _keyPriceListGroup;
        private PricingEverydayPriceListGroup _linkedPriceListGroup;
        private PricingEverydayPriceList _selectedKeyPriceList;

        private FilterGroup _selectedFilterGroup;
        private PricingMode _selectedMode;

        private string _searchGroupKey;
        private string _parentKey;
        private bool _canNameChange;
        private bool _canSearchKeyChange;

        #endregion

        #region Constructors

        public PricingEveryday()
        {
            Identity = new PricingIdentity();
            FilterGroups = new List<FilterGroup>();
            ValueDrivers = new List<PricingEverydayValueDriver>();
            KeyValueDriver = new PricingEverydayKeyValueDriver();
            LinkedValueDrivers = new List<PricingEverydayLinkedValueDriver>();
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

        public List<PricingEverydayLinkedValueDriver> LinkedValueDrivers
        {
            get { return _linkedValueDrivers; }
            set { this.RaiseAndSetIfChanged(ref _linkedValueDrivers, value); }
        }

        public List<PricingMode> PricingModes
        {
            get { return _pricingModes; }
            set
            {
                if (_pricingModes != value)
                {
                    _pricingModes = value;
                    this.RaisePropertyChanged("PricingModes");

                    //Set default value if applicable.
                    if (_pricingModes != null && SelectedMode == null)
                    {
                        SelectedMode = _pricingModes.Where(mode => mode.IsSelected).FirstOrDefault();
                    }
                }
            }
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

        public PricingMode SelectedMode
        {
            get { return _selectedMode; }
            set
            {
                if (_selectedMode != value)
                {
                    _selectedMode = value;
                    this.RaisePropertyChanged("SelectedMode");

                    //Update dependencies.
                    UpdatePriceListGroups();
                }
            }
        }

        public FilterGroup SelectedFilterGroup
        {
            get { return _selectedFilterGroup; }
            set { this.RaiseAndSetIfChanged(ref _selectedFilterGroup, value); }
        }

        #region Key and Linked Price Lists

        /// <summary>
        /// Gets the group containing the Key price lists for the selected pricing mode.
        /// </summary>
        public PricingEverydayPriceListGroup KeyPriceListGroup
        {
            get { return _keyPriceListGroup; }
            private set
            {
                this.RaiseAndSetIfChanged(ref _keyPriceListGroup, value);
                if (_keyPriceListGroup != null && SelectedKeyPriceList == null)
                {
                    //Default the selected key price list to the first one marked IsSelected.
                    SelectedKeyPriceList = _keyPriceListGroup.PriceLists.Where(pl => pl.IsSelected).FirstOrDefault();
                }
            }
        }

        /// <summary>
        /// Gets the group containing the Linked price lists for the selected pricing mode.
        /// </summary>
        public PricingEverydayPriceListGroup LinkedPriceListGroup
        {
            get { return _linkedPriceListGroup; }
            private set { this.RaiseAndSetIfChanged(ref _linkedPriceListGroup, value); }
        }

        /// <summary>
        /// Gets/sets the selected Key price list. This price list is contained within the current KeyPriceListGroup.
        /// </summary>
        public PricingEverydayPriceList SelectedKeyPriceList
        {
            get { return _selectedKeyPriceList; }

            private set
            {
                if (_selectedKeyPriceList != value)
                {
                    //Clear the current key price list's key and selected properties.
                    if (_selectedKeyPriceList != null)
                    {
                        _selectedKeyPriceList.IsKey = false;
                        _selectedKeyPriceList.IsSelected = false;
                    }

                    _selectedKeyPriceList = value;
                    this.RaisePropertyChanged("SelectedKeyPriceList");


                    //Set the new key price list's key and selected properties.
                    if (_selectedKeyPriceList != null)
                    {
                        _selectedKeyPriceList.IsKey = true;
                        _selectedKeyPriceList.IsSelected = true;
                    }

                    //Update dependent properties.                    
                    RecalculateLinkedPriceLists();
                }
            }
        }

        private void RecalculateLinkedPriceLists()
        {
            if (LinkedPriceListGroup != null &&
                SelectedKeyPriceList != null &&
                KeyPriceListGroup != null)
            {
                if (LinkedPriceListGroup.Key == KeyPriceListGroup.Key)
                {
                    LinkedPriceListGroup.RecalculateFilteredPriceLists(SelectedKeyPriceList.Id);
                }
                else
                {
                    LinkedPriceListGroup.RecalculateFilteredPriceLists();
                }
            }
        }

        /// <summary>
        /// Exposes the price lists for which rounding rules can be set. 
        /// These are the key price list plus any other price list marked IsSelected.
        /// </summary>
        public ObservableCollection<PricingEverydayPriceList> RoundingRulePriceLists
        {
            get
            {                
                var result = new ObservableCollection<PricingEverydayPriceList>();

                //The list contains the Key price list plus all linked price lists marked IsSelected.
                if (KeyPriceListGroup != null)
                {
                    var keyList = KeyPriceListGroup.PriceLists.Where(pl => pl.IsKey);
                    var linkedLists = Enumerable.Empty<PricingEverydayPriceList>();

                    if (LinkedPriceListGroup != null)
                    {
                        linkedLists = LinkedPriceListGroup.FilteredPriceLists.Where(pl => pl.IsSelected);
                    }
                    List<PricingEverydayPriceList> list = keyList.Union(linkedLists).ToList();

                    result = new ObservableCollection<PricingEverydayPriceList>(list);
                }

                return result;
            }
        }

        #endregion

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

        #region Public Methods

        /// <summary>
        /// Sets the Key and Linked price list groups based on the currently selected Pricing Mode.
        /// </summary>
        public void UpdatePriceListGroups()
        {
            if (SelectedMode != null)
            {
                KeyPriceListGroup = PriceListGroups.Where(group => group.Key == SelectedMode.KeyPriceListGroupKey).FirstOrDefault();

                LinkedPriceListGroup = PriceListGroups.Where(group => group.Key == SelectedMode.LinkedPriceListGroupKey).FirstOrDefault();
                if (LinkedPriceListGroup != null)
                {
                    //Assign to each price list its corresponding linked rule.
                    foreach (PricingEverydayPriceList priceList in LinkedPriceListGroup.PriceLists)
                    {
                        priceList.LinkedPriceListRule = LinkedPriceListRules.FirstOrDefault(rule => rule.PriceListId == priceList.Id);
                    }
                    RecalculateLinkedPriceLists();

                    this.RaisePropertyChanged("SelectedKeyPriceList");
                }
            }
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
