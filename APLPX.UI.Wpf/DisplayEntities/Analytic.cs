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

        private IDisposable _valueDriverChangedListener;

        #endregion

        #region Constructors

        public Analytic()
        {
            Identity = new AnalyticIdentity();
            FilterGroups = new List<FilterGroup>();
            ValueDrivers = new ReactiveList<AnalyticValueDriver>();
            PriceListGroups = new List<AnalyticPriceListGroup>();

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

        public List<FilterGroup> FilterGroups
        {
            get { return _filterGroups; }
            set { this.RaiseAndSetIfChanged(ref _filterGroups, value); }
        }

        public ReactiveList<AnalyticValueDriver> ValueDrivers
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
                        UpdateRunResultsProperty();
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

        #region Helper Methods

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

        private void UpdateRunResultsProperty()
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

        #endregion

        #region Overrides

        public override bool Validate()
        {
            Errors.Clear();

            var errors = FilterGroups.Validate();
            errors.ForEach(item => Errors.Add(item));

            errors = PriceListGroups.Validate();
            errors.ForEach(item => Errors.Add(item));

            errors = ValueDrivers.Validate();
            errors.ForEach(item => Errors.Add(item));

            return (Errors.Count == 0);
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

    }
}
