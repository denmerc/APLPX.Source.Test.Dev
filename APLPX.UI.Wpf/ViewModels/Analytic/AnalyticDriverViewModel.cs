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
                //Prepare the selected driver for the bound view.
                entity.EnsureModeIsSelected(entity.SelectedValueDriver);
                entity.SetRunResultsSelectedDriverOnly();
            }

            InitializeEventHandlers();
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

        protected override void Dispose(bool isDisposing)
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

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
