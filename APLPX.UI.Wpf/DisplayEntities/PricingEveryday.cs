using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using APLPX.UI.WPF.Interfaces;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEveryday : DisplayEntityBase, ISearchableEntity, IFilterContainer, IDisposable
    {
        #region Private Fields

        private int _id;
        private PricingIdentity _identity;
        private List<FilterGroup> _filterGroups;

        private ReactiveList<PricingEverydayValueDriver> _valueDrivers;
        private PricingEverydayKeyValueDriver _keyValueDriver;
        private ObservableCollection<PricingEverydayLinkedValueDriver> _linkedValueDrivers;

        private List<PricingMode> _pricingModes;
        private List<PricingEverydayPriceListGroup> _priceListGroups;
        private PricingKeyPriceListRule _keyPriceListRule;
        private List<PricingLinkedPriceListRule> _linkedPriceListRules;
        private List<PricingEverydayResult> _results;

        private PricingEverydayPriceListGroup _keyPriceListGroup;
        private PricingEverydayPriceListGroup _linkedPriceListGroup;
        private PricingEverydayPriceList _selectedKeyPriceList;
        private IDisposable _linkedPriceListChangeListener;

        private FilterGroup _selectedFilterGroup;
        private PricingMode _selectedMode;
        private PricingEverydayValueDriver _selectedValueDriver;

        private FeatureSearchGroup _searchGroup;
        private int _searchGroupId;
        private int _owningSearchGroupId;
        private string _searchGroupKey;
        private bool _canNameChange;
        private bool _canSearchKeyChange;

        private bool _isDisposed;
        private IDisposable _valueDriverChangeListener;
        private List<PricingEverydayValueDriverWrapper> _valueDriversCache;

        #endregion

        #region Constructors

        public PricingEveryday()
        {
            Identity = new PricingIdentity();
            FilterGroups = new List<FilterGroup>();
            ValueDrivers = new ReactiveList<PricingEverydayValueDriver>();
            KeyValueDriver = new PricingEverydayKeyValueDriver();
            LinkedValueDrivers = new ObservableCollection<PricingEverydayLinkedValueDriver>();
            PricingModes = new List<PricingMode>();
            PriceListGroups = new List<PricingEverydayPriceListGroup>();
            KeyPriceListRule = new PricingKeyPriceListRule();
            LinkedPriceListRules = new List<PricingLinkedPriceListRule>();
            Results = new List<PricingEverydayResult>();

            _valueDriversCache = new List<PricingEverydayValueDriverWrapper>();

            ValueDrivers.ChangeTrackingEnabled = true;
            _valueDriverChangeListener = ValueDrivers.ItemChanged.Subscribe(driver => OnValueDriverItemChanged(driver));
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

        public List<FilterGroup> FilterGroups
        {
            get { return _filterGroups; }
            set { this.RaiseAndSetIfChanged(ref _filterGroups, value); }
        }

        public ReactiveList<PricingEverydayValueDriver> ValueDrivers
        {
            get { return _valueDrivers; }
            set
            {
                if (_valueDrivers != value)
                {
                    _valueDrivers = value;
                    this.RaisePropertyChanged("ValueDrivers");

                    if (_valueDrivers != null)
                    {
                        //Set default value if applicable.
                        if (SelectedValueDriver == null)
                        {
                            SelectedValueDriver = _valueDrivers.FirstOrDefault(v => v.IsKey);
                        }
                        //InitializeLinkedValueDrivers();
                        ValueDriversCache = InitializeDriverContainer();
                    }
                }
            }
        }

        public PricingEverydayKeyValueDriver KeyValueDriver
        {
            get { return _keyValueDriver; }
            set
            {
                if (_keyValueDriver != value)
                {
                    //Cache the source value driver if applicable.
                    bool isInitialSet = (_keyValueDriver != null && _keyValueDriver.ValueDriverId == 0 && value.ValueDriverId > 0);
                    _keyValueDriver = value;
                    if (isInitialSet)
                    {
                        var existing = ValueDriversCache.Find(i => i.BaseDriver.Id == _keyValueDriver.ValueDriverId);
                        if (existing != null)
                        {
                            existing.KeyDriver = KeyValueDriver;
                        }
                    }

                    //Set default value if applicable.
                    if (_keyValueDriver != null &&
                        _keyValueDriver.SelectedGroup == null)
                    {
                        _keyValueDriver.SelectedGroup = _keyValueDriver.Groups.FirstOrDefault();
                    }

                    this.RaisePropertyChanged("KeyValueDriver");
                    this.RaisePropertyChanged("NonKeyValueDrivers");
                }
            }
        }

        public ObservableCollection<PricingEverydayLinkedValueDriver> LinkedValueDrivers
        {
            get { return _linkedValueDrivers; }
            set
            {
                if (_linkedValueDrivers != value)
                {
                    _linkedValueDrivers = value;
                    if (_linkedValueDrivers != null)
                    {
                        foreach (PricingEverydayLinkedValueDriver linkedDriver in _linkedValueDrivers)
                        {
                            var existing = ValueDriversCache.Find(i => i.BaseDriver.Id == linkedDriver.ValueDriverId);
                            if (existing != null)
                            {
                                existing.LinkedDriver = linkedDriver;
                            }
                        }
                    }
                    this.RaisePropertyChanged("LinkedValueDrivers");
                }
            }
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
            private set
            {
                if (_linkedPriceListGroup != value)
                {
                    if (_linkedPriceListChangeListener != null)
                    {
                        //Clean up memory to prevent leaks.
                        _linkedPriceListChangeListener.Dispose();
                        _linkedPriceListChangeListener = null;
                    }

                    _linkedPriceListGroup = value;
                    this.RaisePropertyChanged("LinkedPriceListGroup");

                    //Listen for notifications when the IsSelected property changes for any linked price listn.
                    if (_linkedPriceListGroup != null)
                    {
                        _linkedPriceListChangeListener = _linkedPriceListGroup.PriceListSelectedChanges.Subscribe(data => OnLinkedPriceListChanged(data));
                    }
                }
            }
        }

        /// <summary>
        /// Called when the IsSelected property changes for any linked price list.
        /// </summary>
        private void OnLinkedPriceListChanged(PricingEverydayPriceList priceList)
        {
            //Update dependent calculated properties.
            this.RaisePropertyChanged("RoundingRulePriceLists");
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
                        _selectedKeyPriceList.CanChangeIsSelected = false;
                        _selectedKeyPriceList.OrdinalPosition = 0;
                    }

                    //Update dependent properties.                    
                    RecalculateLinkedPriceLists();
                }
            }
        }

        /// <summary>
        /// Recalculates the collection of linked price lists for this price routine.
        /// </summary>
        private void RecalculateLinkedPriceLists()
        {
            if (LinkedPriceListGroup != null &&
                SelectedKeyPriceList != null &&
                KeyPriceListGroup != null)
            {
                if (LinkedPriceListGroup.Key == KeyPriceListGroup.Key)
                {
                    KeyPriceListGroup.RecalculateFilteredPriceLists(SelectedKeyPriceList.Id);
                }
                else
                {
                    LinkedPriceListGroup.RecalculateFilteredPriceLists();
                }
            }
            else if (KeyPriceListGroup != null &&
                SelectedMode != null &&
                !SelectedMode.HasLinkedPriceListRule)
            {
                //In one key price list mode: no non-key lists are selected or selectable;
                // only the key price list is used.
                var nonKeyLists = KeyPriceListGroup.PriceLists.Where(item => !item.IsKey);
                foreach (PricingEverydayPriceList nonKeyList in nonKeyLists)
                {
                    nonKeyList.IsSelected = false;
                    nonKeyList.CanChangeIsSelected = false;
                }
            }
            this.RaisePropertyChanged("RoundingRulePriceLists");
        }

        /// <summary>
        /// Exposes a collection containing the price lists for which rounding rules can be set. 
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

                        //Recalculate the ordinal position so views can use as needed.
                        int ordinal = 1;
                        foreach (PricingEverydayPriceList linkedList in linkedLists)
                        {
                            linkedList.OrdinalPosition = ordinal++;
                        }
                    }

                    List<PricingEverydayPriceList> list = keyList.Union(linkedLists).ToList();
                    result = new ObservableCollection<PricingEverydayPriceList>(list);
                }

                return result;
            }
        }

        #endregion

        #region Key and Influencer Value Drivers

        public List<PricingEverydayValueDriverWrapper> ValueDriversCache
        {
            private set { _valueDriversCache = value; }
            get { return _valueDriversCache; }
        }

        public ReactiveList<PricingEverydayValueDriverWrapper> NonKeyValueDriversCache
        {
            get
            {
                var nonKey = _valueDriversCache.Where(item => !item.IsKey);
                var result = new ReactiveList<PricingEverydayValueDriverWrapper>(nonKey);
                return result;
            }
        }

        public PricingEverydayValueDriver SelectedValueDriver
        {
            get { return _selectedValueDriver; }
            set
            {
                if (_selectedValueDriver != value)
                {
                    _selectedValueDriver = value;
                    if (ValueDriversCache != null && ValueDriversCache.Count > 0)
                    {
                        SetKeyValueDriver(_selectedValueDriver);
                    }
                    this.RaisePropertyChanged("SelectedValueDriver");
                    this.RaisePropertyChanged("NonKeyValueDrivers");
                    this.RaisePropertyChanged("LinkedValueDrivers");
                    this.RaisePropertyChanged("NonKeyValueDriversCache");

                }
            }
        }

        /// <summary>
        /// Sets this price routine's key value driver to the specified value driver.
        /// </summary>
        /// <param name="newKey"></param>
        public void SetKeyValueDriver(PricingEverydayValueDriver newKey)
        {
            PricingEverydayValueDriverWrapper currentKey = null;
            if (KeyValueDriver != null)
            {
                //Clear the current key driver.
                currentKey = ValueDriversCache.Find(item => item.Id == KeyValueDriver.ValueDriverId);
                currentKey.IsKey = false;
                currentKey.IsSelected = false;
            }

            //Get the new key driver's item from the cache.
            PricingEverydayValueDriverWrapper newKeyWrapper = ValueDriversCache.Find(item => item.Id == newKey.Id);
            newKeyWrapper.IsKey = true;
            newKeyWrapper.IsSelected = true;

            if (newKeyWrapper.KeyDriver == null)
            {
                //Update the key driver concrete instance in the cache.
                var keyDriverInstance = new PricingEverydayKeyValueDriver { ValueDriverId = newKey.Id };
                foreach (PricingValueDriverGroup sourceGroup in _selectedValueDriver.Groups)
                {
                    keyDriverInstance.Groups.Add(new PricingEverydayKeyValueDriverGroup { ValueDriverGroupId = sourceGroup.Id, ValueDriverGroupValue = sourceGroup.Value });
                }
                newKeyWrapper.KeyDriver = keyDriverInstance;
            }

            KeyValueDriver = newKeyWrapper.KeyDriver;

            if (SelectedValueDriver != newKeyWrapper.BaseDriver)
            {
                SelectedValueDriver = newKeyWrapper.BaseDriver;
            }
        }

        /// <summary>
        /// Gets the collection of all non-key value drivers for this price routine. 
        /// This represents the set of candidates for Influencer value drivers.
        /// </summary>
        public ObservableCollection<PricingEverydayValueDriver> NonKeyValueDrivers
        {
            get
            {
                var nonKeyDrivers = ValueDrivers.Where(d => !d.IsKey);
                var result = new ObservableCollection<PricingEverydayValueDriver>(nonKeyDrivers);
                return result;
            }
        }

        public ObservableCollection<PricingEverydayValueDriver> SelectedNonKeyDrivers
        {
            get
            {
                var selected = NonKeyValueDrivers.Where(d => d.IsSelected);
                var result = new ObservableCollection<PricingEverydayValueDriver>(selected);
                return result;
            }
        }

        /// <summary>
        /// Occurs when a property changes for any item in the ValueDrivers collection.
        /// </summary>
        private object OnValueDriverItemChanged(IReactivePropertyChangedEventArgs<PricingEverydayValueDriver> args)
        {
            var driver = args.Sender as PricingEverydayValueDriver;

            this.RaisePropertyChanged("SelectedNonKeyDrivers");

            if (!driver.IsKey)
            {
                RecalculateLinkedValueDrivers(driver);
            }
            return driver;
        }

        private void InitializeLinkedValueDrivers()
        {
            var selected = SelectedNonKeyDrivers;
            if (selected != null)
            {
                foreach (PricingEverydayValueDriver driver in selected)
                {
                    PricingEverydayLinkedValueDriver existing = LinkedValueDrivers.FirstOrDefault(d => d.ValueDriverId == driver.Id);
                    if (existing == null)
                    {
                        //var containerItem = ValueDriversCache.Find(i => i.Id == driver.Id);
                        //if (containerItem != null && containerItem.LinkedDriver != null)
                        //{
                        //    LinkedValueDrivers.Add(containerItem.LinkedDriver);
                        //}

                        //else
                        //{
                        LinkedValueDrivers.Add(new PricingEverydayLinkedValueDriver { ValueDriverId = driver.Id, Name = driver.Name });
                        //}

                    }
                }
            }
        }

        private void RecalculateLinkedValueDrivers(PricingEverydayValueDriver driver)
        {
            PricingEverydayLinkedValueDriver existing = LinkedValueDrivers.FirstOrDefault(d => d.ValueDriverId == driver.Id);
            if (driver.IsSelected)
            {
                if (existing == null)
                {
                    PricingEverydayLinkedValueDriver linkedDriver = null;

                    PricingEverydayValueDriverWrapper cachedItem = _valueDriversCache.Find(item => item.Id == driver.Id);
                    if (cachedItem.LinkedDriver != null)
                    {
                        //Get the linked driver from the cache. 
                        linkedDriver = cachedItem.LinkedDriver;
                    }
                    else
                    {
                        //Create new linked driver and add it to the cache.
                        linkedDriver = new PricingEverydayLinkedValueDriver { ValueDriverId = driver.Id, Name = driver.Name };
                        cachedItem.LinkedDriver = linkedDriver;
                    }
                    LinkedValueDrivers.Add(linkedDriver);
                }
            }
            else if (existing != null)
            {
                LinkedValueDrivers.Remove(existing);
            }
            if (KeyValueDriver != null)
            {
                //Remove previously marked key driver from the linked drivers.
                var keyDriver = LinkedValueDrivers.FirstOrDefault(d => d.ValueDriverId == KeyValueDriver.ValueDriverId);
                if (keyDriver != null)
                {
                    LinkedValueDrivers.Remove(keyDriver);
                }
            }
        }


        private PricingEverydayValueDriverWrapper _selectedValueDriverWrapper;

        public PricingEverydayValueDriverWrapper SelectedValueDriverWrapper
        {
            get { return _selectedValueDriverWrapper; }
            set
            {
                if (_selectedValueDriverWrapper != value)
                {
                    _selectedValueDriverWrapper = value;
                    this.RaisePropertyChanged("SelectedValueDriverWrapper");
                }
            }
        }


        private List<PricingEverydayValueDriverWrapper> InitializeDriverContainer()
        {
            var list = new List<PricingEverydayValueDriverWrapper>();

            if (ValueDrivers != null && ValueDriversCache != null)
            {
                foreach (PricingEverydayValueDriver baseDriver in ValueDrivers)
                {
                    var wrapper = new PricingEverydayValueDriverWrapper(baseDriver);
                    list.Add(wrapper);
                }

                if (KeyValueDriver != null && KeyValueDriver.ValueDriverId > 0)
                {
                    var existing = list.Find(i => i.BaseDriver.Id == KeyValueDriver.ValueDriverId);
                    if (existing != null)
                    {
                        existing.KeyDriver = KeyValueDriver;
                    }
                }
                if (LinkedValueDrivers != null)
                {
                    foreach (PricingEverydayLinkedValueDriver linkedDriver in LinkedValueDrivers)
                    {
                        var existing = list.Find(i => i.BaseDriver.Id == linkedDriver.ValueDriverId);
                        if (existing != null)
                        {
                            existing.LinkedDriver = linkedDriver;
                            existing.IsSelected = true;
                        }
                    }
                }
                OnPropertyChanged("ValueDriversCache");
            }
            return list;
        }

        #endregion

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
                }

                RecalculateLinkedPriceLists();
                this.RaisePropertyChanged("SelectedKeyPriceList");
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

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_valueDriverChangeListener != null)
                    {
                        _valueDriverChangeListener.Dispose();
                        _valueDriverChangeListener = null;
                    }
                    if (_linkedPriceListChangeListener != null)
                    {
                        _linkedPriceListChangeListener.Dispose();
                        _linkedPriceListChangeListener = null;
                    }
                    if (ValueDrivers != null)
                    {
                        ValueDrivers.ChangeTrackingEnabled = false;
                    }
                    foreach (IDisposable group in FilterGroups)
                    {
                        group.Dispose();
                    }
                    foreach (PricingEverydayPriceListGroup priceListGroup in PriceListGroups)
                    {
                        priceListGroup.Dispose();
                    }
                }
                _isDisposed = true;
            }
        }

        #endregion
    }
}
