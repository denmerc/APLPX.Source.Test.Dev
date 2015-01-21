using System;
using System.Collections.Generic;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.ViewModels.Analytic
{
    public class AnalyticPriceListViewModel : ViewModelBase
    {
        private Display.Analytic _entity;
        private List<Display.AnalyticPriceListGroup> _priceListGroups;

        #region Constructor and Initialization

        public AnalyticPriceListViewModel(Display.Analytic entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Entity = entity;
            PriceListGroups = entity.PriceListGroups;
            if(PriceListGroups.Count > 0)
            {
                Entity.SelectedPriceListGroup = PriceListGroups[0];
            }
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var canExecute = this.WhenAnyValue(vm => vm.Entity.SelectedPriceListGroup, (plGroup) => SelectAllPriceListsCanExecute(plGroup));
            SelectAllPriceListsCommand = ReactiveCommand.Create(canExecute);

            this.WhenAnyObservable(vm => vm.SelectAllPriceListsCommand).Subscribe(item => SelectAllPriceListsExecuted(item));
        }

        #endregion

        #region Properties

        public ReactiveCommand<object> SelectAllPriceListsCommand { get; private set; }

        public Display.Analytic Entity
        {
            get { return _entity; }
            private set { this.RaiseAndSetIfChanged(ref _entity, value); }
        }

        public List<Display.AnalyticPriceListGroup> PriceListGroups
        {
            get { return _priceListGroups; }
            private set
            {
                if (_priceListGroups != value)
                {
                    _priceListGroups = value;
                    this.RaisePropertyChanged("PriceListGroups");

                    //Select the first price list group by default.
                    if (Entity.SelectedPriceListGroup == null &&
                        _priceListGroups != null && _priceListGroups.Count > 0)
                    {
                        Entity.SelectedPriceListGroup = _priceListGroups[0];
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the price list groups should be displayed.
        /// </summary>
        public bool ShowPriceListGroups
        {
            get
            {
                bool result = (PriceListGroups.Count > 1);

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
    }
}
