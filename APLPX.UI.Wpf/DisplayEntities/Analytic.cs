using System;
using System.Collections.Generic;
using System.Linq;

using APLPX.UI.WPF.Interfaces;
using APLPX.UI.WPF.Validation;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Analytic : DisplayEntityBase, ISearchableEntity, IFilterContainer
    {
        #region Private Fields

        private int _id;
        private AnalyticIdentity _identity;
        private ReactiveList<AnalyticValueDriver> _valueDrivers;
        private ReactiveList<FilterGroup> _filterGroups;
        private ReactiveList<AnalyticPriceListGroup> _priceListGroups;
        private FilterGroup _selectedFilterGroup;
        private AnalyticPriceListGroup _selectedPriceListGroup;
        private AnalyticValueDriver _selectedValueDriver;
        private FeatureSearchGroup _searchGroup;
        private int _searchGroupId;
        private int _owningSearchGroupId;
        private string _searchKey;
        private bool _canNameChange;
        private bool _canSearchKeyChange;

        private Dictionary<int, bool> _listAreDriverResultsCurrent;

        private IDisposable _identityChangedListener;
        private IDisposable _filterChangedListener;
        private IDisposable _priceListChangedListener;
        private IDisposable _valueDriverChangedListener;
        private bool _isDisposed;

        #endregion

        #region Constructor and Initialization

        public Analytic()
        {
            Identity = new AnalyticIdentity();
            FilterGroups = new ReactiveList<FilterGroup>();
            PriceListGroups = new ReactiveList<AnalyticPriceListGroup>();
            ValueDrivers = new ReactiveList<AnalyticValueDriver>();

            InitializeChangeListeners();
        }

        private void InitializeChangeListeners()
        {
            var identityChanged = this.WhenAnyValue(v => v.Identity.IsDirty);
            _identityChangedListener = identityChanged.Subscribe(v => OnIdentityChanged(v));

            FilterGroups.ChangeTrackingEnabled = true;
            _filterChangedListener = FilterGroups.ItemChanged.Subscribe(fg => OnFilterChanged(fg));

            PriceListGroups.ChangeTrackingEnabled = true;
            _priceListChangedListener = PriceListGroups.ItemChanged.Subscribe(pl => OnPriceListChanged(pl));

            _listAreDriverResultsCurrent = new Dictionary<int, bool>();
            ValueDrivers.ChangeTrackingEnabled = true;
            _valueDriverChangedListener = ValueDrivers.ItemChanged.Subscribe(v => OnDriverChanged(v));
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public ReactiveList<FilterGroup> FilterGroups
        {
            get { return _filterGroups; }
            set { this.RaiseAndSetIfChanged(ref _filterGroups, value); }
        }

        public ReactiveList<AnalyticValueDriver> ValueDrivers
        {
            get { return _valueDrivers; }
            set { this.RaiseAndSetIfChanged(ref _valueDrivers, value); }
        }

        public ReactiveList<AnalyticPriceListGroup> PriceListGroups
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

        /// <summary>
        /// Gets/sets the Value Driver currently selected in the bound view. 
        /// Typically, this property is bound to the SelectedItem property of an items control.
        /// Note: This property is different from the Value Driver's IsSelected property,
        /// which denotes whether a Value Driver is to be included in this Analytic's calculated results.
        /// </summary>
        public AnalyticValueDriver SelectedValueDriver
        {
            get { return _selectedValueDriver; }
            set
            {
                if (_selectedValueDriver != value)
                {
                    _selectedValueDriver = value;
                    this.RaisePropertyChanged("SelectedValueDriver");

                    if (SelectedValueDriver != null)
                    {
                        SetRunResultsSelectedDriverOnly();
                        EnsureModeIsSelected(SelectedValueDriver);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a flattened collection containing this analytic's value drivers and their child modes.
        /// Can be used for diagnostics and troubleshooting.
        /// </summary>
        public IEnumerable<object> ValueDriverModeRows
        {
            get
            {
                var rows = from driver in ValueDrivers
                           from mode in driver.Modes
                           from driverGroup in mode.Groups
                           select new
                           {
                               Driver = driver,
                               Mode = mode,
                               DriverGroup = driverGroup
                           };

                return rows;
            }
        }

        /// <summary>
        /// Gets a flattened collection containing this analytic's value drivers and their child results.
        /// Can be used for diagnostics and troubleshooting.
        /// </summary>
        public IEnumerable<object> ValueDriverResultRows
        {
            get
            {
                var rows = from driver in ValueDrivers
                           from result in driver.Results
                           select new
                           {
                               Driver = driver,
                               Result = result,
                           };

                return rows;
            }
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
            set
            {
                if (_searchGroupId != value)
                {
                    _searchGroupId = value;
                    OnPropertyChanged("SearchGroupId");
                    IsDirty = true;
                }
            }
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

        #region Property Changed Listeners - contained objects

        private void OnIdentityChanged(bool isDirty)
        {
            if (isDirty && !IsDirty)
            {
                IsDirty = true;
            }
        }

        /// <summary>
        /// Detects property changes to any Value Driver within this Analytic.
        /// </summary> 
        private void OnDriverChanged(IReactivePropertyChangedEventArgs<AnalyticValueDriver> args)
        {
            if (args.PropertyName == "AreResultsCurrent" || args.PropertyName == "GroupCount")
            {
                //Update dependent properties.
                this.RaisePropertyChanged("ValueDriverModeRows");
            }
        }

        private void OnFilterChanged(IReactivePropertyChangedEventArgs<FilterGroup> args)
        {
            var filter = args.Sender as FilterGroup;
            if (filter != null)
            {
                filter.Validate();
            }
        }

        private void OnPriceListChanged(IReactivePropertyChangedEventArgs<AnalyticPriceListGroup> args)
        {
            var priceListGroup = args.Sender as AnalyticPriceListGroup;
            if (priceListGroup != null)
            {
                priceListGroup.Validate();
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Ensures that the SelectedMode is set for the specified ValueDriver.
        /// This is to support expected behavior in bound views.
        /// </summary>        
        public void EnsureModeIsSelected(AnalyticValueDriver driver)
        {
            if (driver.SelectedMode == null)
            {
                var selectedMode = driver.Modes.FirstOrDefault(mode => mode.IsSelected);
                if (selectedMode == null && driver.Modes.Count > 0)
                {
                    selectedMode = driver.Modes[0];
                }
                driver.SelectedMode = selectedMode;
            }

            if (driver.SelectedMode != null)
            {
                driver.SelectedMode.RecalculateEditableGroups();
            }
        }

        /// <summary>
        /// Restores the value of each ValueDriver's AreResultsCurrent property from this analytic's internal cache.
        /// </summary>
        public void RestoreStateAreDriverResultsCurrent()
        {
            foreach (AnalyticValueDriver driver in ValueDrivers)
            {
                driver.AreResultsCurrent = _listAreDriverResultsCurrent[driver.Key];
            }
        }

        /// <summary>
        /// Saves the current value of each ValueDriver's AreResultsCurrent property.
        /// </summary>
        public void SaveStateAreDriverResultsCurrent()
        {
            _listAreDriverResultsCurrent.Clear();
            foreach (AnalyticValueDriver driver in ValueDrivers)
            {
                _listAreDriverResultsCurrent.Add(driver.Key, driver.AreResultsCurrent);
            }
        }

        /// <summary>
        /// Sets RunResults to true for the Selected value driver and clears it for all other value drivers.      
        /// Explanation: this is a convenience method to support views that may "expect" this behavior,
        /// i.e., running only one value driver at a time.
        /// </summary>
        public void SetRunResultsSelectedDriverOnly()
        {
            if (SelectedValueDriver != null)
            {
                //Set RunResults for the selected driver.         
                SelectedValueDriver.RunResults = true;

                //Clear RunResults for the remaining drivers.
                var unselectedDrivers = ValueDrivers.Where(item => item != SelectedValueDriver);
                foreach (AnalyticValueDriver driver in unselectedDrivers)
                {
                    driver.RunResults = false;
                }
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Validates this object.
        /// </summary>
        /// <returns>A List containing an <see cref="Error"/> item for each validation error.</returns>
        public override List<Error> GetValidationErrors()
        {
            var consolidatedList = new List<Error>();

            if (SearchGroupId == 0)
            {
                consolidatedList.Add(new Error { Message = "A folder must be selected for this Analytic." });
            }

            var entityErrors = Identity.GetAllValidationErrors();
            consolidatedList.AddRange(entityErrors);
          

            entityErrors = FilterGroups.GetAllValidationErrors();
            consolidatedList.AddRange(entityErrors);

            entityErrors = PriceListGroups.GetAllValidationErrors();
            consolidatedList.AddRange(entityErrors);

            entityErrors = ValueDrivers.GetAllValidationErrors();
            consolidatedList.AddRange(entityErrors);

            return consolidatedList;
        }

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

        protected override void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_identityChangedListener != null)
                    {
                        _identityChangedListener.Dispose();
                        _identityChangedListener = null;
                    }
                    if (_valueDriverChangedListener != null)
                    {
                        _valueDriverChangedListener.Dispose();
                        _valueDriverChangedListener = null;
                    }
                    if (_priceListChangedListener != null)
                    {
                        _priceListChangedListener.Dispose();
                        _priceListChangedListener = null;
                    }
                    foreach (IDisposable driver in ValueDrivers)
                    {
                        driver.Dispose();
                    }
                    ValueDrivers.ChangeTrackingEnabled = false;

                    foreach (IDisposable group in FilterGroups)
                    {
                        group.Dispose();
                    }
                    foreach (IDisposable group in PriceListGroups)
                    {
                        group.Dispose();
                    }

                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
