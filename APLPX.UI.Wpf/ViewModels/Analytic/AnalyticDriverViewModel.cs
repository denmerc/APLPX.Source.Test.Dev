using System;
using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels.Analytic
{
    /// <summary>
    /// View model for analytic driver-related views.
    /// </summary>
    public class AnalyticDriverViewModel : ViewModelBase, IDisposable
    {
        #region Private Fields

        private DisplayEntities.Analytic _entity;
        private IDisposable _modeChangedSubscription;
        private IDisposable _driverChangedSubscription;
        private bool _isDisposed;

        #endregion

        #region Constructor and Initialization

        public AnalyticDriverViewModel(DisplayEntities.Analytic entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Entity = entity;

            if (entity.SelectedValueDriver != null)
            {
                entity.EnsureModeIsSelected(entity.SelectedValueDriver);
            }

            InitializeCommands();
            InitializeEventHandlers();
        }

        private void InitializeCommands()
        {
            var canExecute = this.WhenAnyValue(vm => vm.IsValueDriverModeSelected);
            AutoCalculateCommand = ReactiveCommand.Create(canExecute);
            AutoCalculateCommand.Subscribe(_ => AutoCalculateCommandExecuted(_));
        }

        private void InitializeEventHandlers()
        {
            var selectedValueDriverChanged = this.WhenAnyValue(vm => vm.Entity.SelectedValueDriver);
            _driverChangedSubscription = selectedValueDriverChanged.Subscribe(driver => OnSelectedValueDriverChanged(driver));

            var selectedValueDriverModeChanged = this.WhenAnyValue(vm => vm.Entity.SelectedValueDriver.SelectedMode);
            _modeChangedSubscription = selectedValueDriverModeChanged.Subscribe(mode => OnSelectedValueDriverModeChanged(mode));
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
                    mode.AreResultsAvailable = true;
                }
            }
            return parameter;
        }

        #endregion

        #region Event Handlers

        private object OnSelectedValueDriverChanged(AnalyticValueDriver driver)
        {
            this.RaisePropertyChanged("IsValueDriverSelected");

            return null;
        }



        private object OnSelectedValueDriverModeChanged(AnalyticValueDriverMode mode)
        {
            this.RaisePropertyChanged("IsValueDriverModeSelected");
            return null;
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
                    if (_modeChangedSubscription != null)
                    {
                        _modeChangedSubscription.Dispose();
                        _modeChangedSubscription = null;
                    }
                    if (_driverChangedSubscription != null)
                    {
                        _driverChangedSubscription.Dispose();
                        _driverChangedSubscription = null;
                    }
                }
                _isDisposed = true;
            }
        }

        #endregion
    }
}
