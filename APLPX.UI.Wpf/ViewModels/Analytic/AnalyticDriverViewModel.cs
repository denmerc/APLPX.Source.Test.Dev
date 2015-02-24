using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.DisplayServices;
using APLPX.UI.WPF.Validation;
using ReactiveUI;
using DTO = APLPX.Entity;

namespace APLPX.UI.WPF.ViewModels.Analytic
{
    /// <summary>
    /// View model for analytic driver-related views.
    /// </summary>
    public class AnalyticDriverViewModel : ViewModelBase
    {
        #region Private Fields

        private DisplayEntities.Analytic _entity;
        private AnalyticDisplayServices _analyticDisplayService;

        private IDisposable _driverChangedSubscription;
        private IDisposable _modeChangedSubscription;
        private IDisposable _selectedChangedSubscription;

        private bool _isDisposed;

        #endregion

        #region Constructor and Initialization

        public AnalyticDriverViewModel(DisplayEntities.Analytic entity, ModuleFeature feature, AnalyticDisplayServices analyticDisplayService)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (feature == null)
            {
                throw new ArgumentNullException("feature");
            }
            if (analyticDisplayService == null)
            {
                throw new ArgumentNullException("analyticDisplayService");
            }

            Entity = entity;
            SelectedFeature = feature;
            _analyticDisplayService = analyticDisplayService;

            if (entity.SelectedValueDriver != null)
            {
                //Prepare the selected driver for the bound view.
                entity.EnsureModeIsSelected(entity.SelectedValueDriver);
                entity.SetRunResultsSelectedDriverOnly();
            }

            InitializeEventHandlers();
            InitializeCommands();
        }

        private void InitializeEventHandlers()
        {
            var selectedValueDriverChanged = this.WhenAnyValue(vm => vm.Entity.SelectedValueDriver);
            _selectedChangedSubscription = selectedValueDriverChanged.Subscribe(driver => OnSelectedDriverChanged(driver));

            var selectedValueDriverModeChanged = this.WhenAnyValue(vm => vm.Entity.SelectedValueDriver.SelectedMode);
            _modeChangedSubscription = selectedValueDriverModeChanged.Subscribe(mode => OnSelectedDriverModeChanged(mode));

            //TODO: should be any driver, not just the selected one
            var dirtyChanged = this.WhenAnyValue(vm => vm.Entity.SelectedValueDriver.IsDirty);
            _driverChangedSubscription = dirtyChanged.Subscribe(isDirty => OnDriverDirtyChanged(isDirty));

            this.Entity.ValueDrivers.ChangeTrackingEnabled = true;
            this.Entity.ValueDrivers.ItemChanged.Where(v => v.PropertyName == "IsSelected" ||
                                                            v.PropertyName == "MinOutlier" ||
                                                            v.PropertyName == "MaxOutlier" ||
                                                            v.PropertyName == "SelectedMode" ||
                                                            v.PropertyName == "GroupCount")
                                                .Subscribe(v => OnDriverChanged(v));
        }

        private void InitializeCommands()
        {
            //At least one value driver must be marked Selected in order to save.
            IObservable<bool> canExecute = this.WhenAnyValue(vm => vm.IsAnyValueDriverIncluded, vm => vm.IsAnyValueDriverDirty, (included, dirty) => SaveCanExecute(included, dirty));
            SaveCommand = ReactiveCommand.Create(canExecute);
            this.WhenAnyObservable(vm => vm.SaveCommand).Subscribe(val => SaveExecuted(val));

            RunCommand = ReactiveCommand.Create(canExecute);
            this.WhenAnyObservable(vm => vm.RunCommand).Subscribe(val => RunExecuted(val));

            Commands.Add(new DisplayEntities.Action { Command = SaveCommand, Name = "Save", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversSave });
            Commands.Add(new DisplayEntities.Action { Command = RunCommand, Name = "Run", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversRun });
        }

        #endregion

        #region Properties

        public ReactiveCommand<object> SaveCommand
        {
            get;
            private set;
        }

        public ReactiveCommand<object> RunCommand
        {
            get;
            private set;
        }

        public DisplayEntities.Analytic Entity
        {
            get { return _entity; }
            private set
            {
                if (_entity != value)
                {
                    _entity = value;
                    this.RaisePropertyChanged("Entity");

                    if (_entity.SelectedValueDriver == null &&
                        _entity.ValueDrivers != null &&
                        _entity.ValueDrivers.Count > 0)
                    {
                        //Select the first value driver by default.
                        _entity.SelectedValueDriver = _entity.ValueDrivers[0];
                    }
                }
            }
        }

        public bool IsValueDriverSelected
        {
            get { return (Entity != null && Entity.SelectedValueDriver != null); }
        }

        public bool IsValueDriverModeSelected
        {
            get
            {
                bool result = false;

                if (Entity != null && Entity.SelectedValueDriver != null)
                {
                    result = (Entity.SelectedValueDriver.SelectedMode != null);
                }

                return result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether any ValueDriver has unsaved changes.
        /// </summary>
        public bool IsAnyValueDriverDirty
        {
            get
            {
                bool result = Entity.ValueDrivers.Any(driver => driver.IsDirty);

                return result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether any ValueDriver is marked IsSelected.
        /// </summary>
        public bool IsAnyValueDriverIncluded
        {
            get
            {
                bool result = Entity.ValueDrivers.Any(driver => driver.IsSelected);

                return result;
            }
        }

        public bool IsDriverInUserEntryMode
        {
            get
            {
                bool result = (IsValueDriverModeSelected && !Entity.SelectedValueDriver.SelectedMode.IsAutoGenerated);
                return result;
            }
        }

        public ObservableCollection<Error> ValidationResults
        {
            get
            {
                var errors = Entity.ValueDrivers.GetAllValidationErrors();

                var result = new ObservableCollection<Error>(errors);
                return result;
            }
        }

        #endregion

        #region Command and Event Handlers

        private void RunExecuted(object parameter)
        {

        }

        private bool SaveCanExecute(bool isAnyDriverSelected, bool isDirty)
        {
            bool canExecute = isAnyDriverSelected && isDirty && ValidationResults.Count == 0;

            return canExecute;
        }

        private void SaveExecuted(object parameter)
        {

        }

        private void OnDriverChanged(IReactivePropertyChangedEventArgs<AnalyticValueDriver> args)
        {
            this.RaisePropertyChanged("IsAnyValueDriverIncluded");
            this.RaisePropertyChanged("ValidationResults");
        }

        private void OnDriverDirtyChanged(bool isDirty)
        {
            if (isDirty)
            {
                SelectedFeature.SelectedStep.IsCompleted = false;
                SelectedFeature.DisableRemainingSteps();
            }

            //Update dependent calculated properties.
            this.RaisePropertyChanged("IsAnyValueDriverDirty");
        }

        private void OnSelectedDriverChanged(AnalyticValueDriver driver)
        {
            this.RaisePropertyChanged("IsValueDriverSelected");
        }

        private void OnSelectedDriverModeChanged(AnalyticValueDriverMode mode)
        {
            this.RaisePropertyChanged("IsValueDriverModeSelected");
            this.RaisePropertyChanged("IsDriverInUserEntryMode");            
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_driverChangedSubscription != null)
                    {
                        _driverChangedSubscription.Dispose();
                    }
                    if (_modeChangedSubscription != null)
                    {
                        _modeChangedSubscription.Dispose();
                    }
                    if (_selectedChangedSubscription != null)
                    {
                        _selectedChangedSubscription.Dispose();
                    }
                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
