using System;
using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels.Analytic
{
    /// <summary>
    /// View model for analytic driver-related views.
    /// </summary>
    public class AnalyticDriverViewModel : ViewModelBase
    {
        private DisplayEntities.Analytic _entity;
        private bool _areResultsAvailable;

        #region Constructor and Initialization

        public AnalyticDriverViewModel(DisplayEntities.Analytic entity)
        {
            Entity = entity;
            InitializeCommands();
            InitializeEventHandlers();
        }

        private void InitializeCommands()
        {
            //testing canExecute:
            var canExecute = this.WhenAnyValue(vm => vm.IsValueDriverModeSelected);
            AutoCalculateCommand = ReactiveCommand.Create(canExecute);
            AutoCalculateCommand.Subscribe(_ => AutoCalculateCommandExecuted(_));
        }

        private void InitializeEventHandlers()
        {
            var selectedValueDriverChanged = this.WhenAnyValue(vm => vm.Entity.SelectedValueDriver);
            selectedValueDriverChanged.Subscribe(driver => OnSelectedValueDriverChanged(driver));

            var selectedValueDriverModeChanged = this.WhenAnyValue(vm => vm.Entity.SelectedValueDriver.SelectedMode);
            selectedValueDriverModeChanged.Subscribe(mode => OnSelectedValueDriverModeChanged(mode));
        }

        #endregion

        #region Properties

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

        /// <summary>
        /// Gets a value indicating whether calculated results are available for the selected value driver.
        /// The bound view can use this property to show/hide results, etc.
        /// </summary>
        public bool AreResultsAvailable
        {
            get { return _areResultsAvailable; }
            private set { this.RaiseAndSetIfChanged(ref _areResultsAvailable, value); }
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

        #endregion

        #region Commands

        /// <summary>
        /// Command for triggering an auto-calculation of the value driver groups.
        /// </summary>
        public ReactiveCommand<object> AutoCalculateCommand { get; private set; }

        private object AutoCalculateCommandExecuted(object parameter)
        {
            if (Entity != null && Entity.SelectedValueDriver != null)
            {
                AnalyticValueDriverMode mode = Entity.SelectedValueDriver.SelectedMode;
                if (mode != null && mode.Groups.Count > 0)
                {
                    //TODO: for testing only. In production, will call server method.
                    mode.MockAutoCalculateDriverGroups();
                    AreResultsAvailable = true;
                }
            }
            return parameter;
        }

        #endregion

        #region Event Handlers

        private object OnSelectedValueDriverChanged(AnalyticValueDriver driver)
        {
            this.RaisePropertyChanged("IsValueDriverSelected");

            if (driver != null && driver.SelectedMode != null)
            {
                driver.SelectedMode.RecalculateEditableGroups();
            }
            return null;
        }

        private object OnSelectedValueDriverModeChanged(AnalyticValueDriverMode mode)
        {
            this.RaisePropertyChanged("IsValueDriverModeSelected");
            return null;
        }

        #endregion
    }
}
