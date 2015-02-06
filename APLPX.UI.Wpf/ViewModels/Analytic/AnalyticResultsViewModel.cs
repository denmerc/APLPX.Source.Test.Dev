using System;
using System.Collections.Generic;
using System.Linq;
using APLPX.UI.WPF.Helpers;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;


namespace APLPX.UI.WPF.ViewModels.Analytic
{
    /// <summary>
    /// View model for displaying Analytic value driver results.
    /// </summary>
    public class AnalyticResultsViewModel : ViewModelBase
    {
        private DisplayEntities.Analytic _entity;
        private IDisposable _itemChangedSubscription;
        private IDisposable _selectAllDriversSubscription;

        public AnalyticResultsViewModel(Display.Analytic entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Entity = entity;

            SelectAllDriversCommand = ReactiveCommand.Create();
            _selectAllDriversSubscription = SelectAllDriversCommand.Subscribe(v => SelectAllDriversExecuted(v));

            _itemChangedSubscription = Entity.ValueDrivers.ItemChanged.Subscribe(d => OnValueDriverChanged(d));

            //By default, select all drivers to re-run.
            SelectAllDriversExecuted(true);
        }

        #region Properties

        /// <summary>
        /// Command for selecting or deselecting all value drivers for running.
        /// </summary>
        public ReactiveCommand<object> SelectAllDriversCommand { get; private set; }

        public DisplayEntities.Analytic Entity
        {
            get { return _entity; }
            private set { this.RaiseAndSetIfChanged(ref _entity, value); }
        }

        /// <summary>
        /// Gets a flattened collection containing the current analytic's value drivers and their results.        
        /// </summary>
        public IEnumerable<object> DriverResultRows
        {
            get
            {
                var rows = from driver in Entity.ValueDrivers
                           from result in driver.Results
                           select new
                           {
                               Driver = driver,
                               Result = result
                           };

                return rows;
            }
        }

        /// <summary>
        /// Gets the number of value drivers whose RunResults property is true.
        /// </summary>
        public int DriversToRunCount
        {
            get
            {
                int result = Entity.ValueDrivers.Where(d => d.RunResults).Count();
                return result;
            }
        }

        /// <summary>
        /// Gets a tri-state boolean value indicating whether all value drivers have RunResults set to true.
        /// </summary>
        public bool? AreAllValueDriversSelectedToRun
        {
            get
            {
                bool? result = Entity.ValueDrivers.AreAllItemsIncluded(p => p.RunResults);
                return result;
            }
        }

        #endregion

        #region Command and Event handlers

        private void SelectAllDriversExecuted(object parameter)
        {
            bool isSelected = Convert.ToBoolean(parameter);

            foreach (Display.AnalyticValueDriver driver in Entity.ValueDrivers)
            {
                driver.RunResults = isSelected;
            }
        }

        private void OnValueDriverChanged(IReactivePropertyChangedEventArgs<Display.AnalyticValueDriver> args)
        {
            var source = args.Sender as Display.AnalyticValueDriver;
            if (source != null && args.PropertyName == "RunResults")
            {
                this.RaisePropertyChanged("DriversToRunCount");
                this.RaisePropertyChanged("AreAllValueDriversSelectedToRun");
            }
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (_itemChangedSubscription != null)
                {
                    _itemChangedSubscription.Dispose();
                    _itemChangedSubscription = null;
                }
                if (_selectAllDriversSubscription != null)
                {
                    _selectAllDriversSubscription.Dispose();
                    _selectAllDriversSubscription = null;
                }
            }
            base.Dispose(isDisposing);
        }

        #endregion

    }
}
