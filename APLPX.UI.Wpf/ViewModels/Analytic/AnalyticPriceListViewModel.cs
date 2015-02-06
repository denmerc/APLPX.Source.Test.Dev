using System;
using System.Collections.ObjectModel;
using System.Linq;
using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.ViewModels.Analytic
{
    public class AnalyticPriceListViewModel : ViewModelBase
    {
        private Display.Analytic _entity;
        private IDisposable _selectAllPriceListsSubscription;
        private IDisposable _priceListChangedSubscription;
        private bool _isDisposed;

        #region Constructor and Initialization

        public AnalyticPriceListViewModel(Display.Analytic entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Entity = entity;
            if (Entity.PriceListGroups.Count > 0 && Entity.SelectedPriceListGroup == null)
            {
                Entity.SelectedPriceListGroup = Entity.PriceListGroups[0];
            }

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var canExecute = this.WhenAnyValue(vm => vm.Entity.SelectedPriceListGroup, (plGroup) => SelectAllPriceListsCanExecute(plGroup));
            SelectAllPriceListsCommand = ReactiveCommand.Create(canExecute);

            _selectAllPriceListsSubscription = this.WhenAnyObservable(vm => vm.SelectAllPriceListsCommand).Subscribe(item => SelectAllPriceListsExecuted(item));

            _priceListChangedSubscription = this.Entity.PriceListGroups.ItemChanged.Subscribe(plg => OnPriceListChanged(plg));
        }

        private void OnPriceListChanged(IReactivePropertyChangedEventArgs<AnalyticPriceListGroup> plg)
        {
            this.RaisePropertyChanged("ValidationResults");
        }

        #endregion

        #region Properties

        public ReactiveCommand<object> SelectAllPriceListsCommand { get; private set; }

        public Display.Analytic Entity
        {
            get { return _entity; }
            private set { this.RaiseAndSetIfChanged(ref _entity, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the price list groups should be displayed.
        /// </summary>
        public bool ShowPriceListGroups
        {
            get
            {
                bool result = (Entity.PriceListGroups.Count > 1);

                return result;
            }
        }

        public ObservableCollection<Error> ValidationResults
        {
            get
            {
                var errors = Entity.PriceListGroups.SelectMany(g => g.ValidationResults);

                var result = new ObservableCollection<Error>(errors);
                return result;
            }
        }

        #endregion

        #region Command Handlers

        private bool SelectAllPriceListsCanExecute(Display.AnalyticPriceListGroup priceListGroup)
        {
            return (priceListGroup != null);
        }

        private void SelectAllPriceListsExecuted(object parameter)
        {
            bool isSelected = Convert.ToBoolean(parameter);

            foreach (Display.PriceList priceList in Entity.SelectedPriceListGroup.PriceLists)
            {
                priceList.IsSelected = isSelected;
            }
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_selectAllPriceListsSubscription != null)
                    {
                        _selectAllPriceListsSubscription.Dispose();
                        _selectAllPriceListsSubscription = null;
                    }
                    if (_priceListChangedSubscription != null)
                    {
                        _priceListChangedSubscription.Dispose();
                        _priceListChangedSubscription = null;
                    }
                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
