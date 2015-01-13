using System;
using System.Collections.Generic;

using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class PricingEverydayPriceListListViewModel : ViewModelBase
    {

        #region Constructor and Initialization

        public PricingEverydayPriceListListViewModel(PricingEveryday priceRoutine)
        {
            if (priceRoutine == null)
            {
                throw new ArgumentNullException("priceRoutine");
            }

            PriceRoutine = priceRoutine;
            PriceRoutine.UpdatePriceListGroups();

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var canExecute = this.WhenAnyValue(vm => vm.PriceRoutine.LinkedPriceListGroup, (val) => SelectAllPriceListsCanExecute(val));
            SelectAllPriceListsCommand = ReactiveCommand.Create(canExecute);

            this.WhenAnyObservable(vm => vm.SelectAllPriceListsCommand).Subscribe(item => SelectAllPriceListsCommandExecuted(item));
        }

        #endregion

        #region Properties

        public ReactiveCommand<object> SelectAllPriceListsCommand
        {
            get;
            private set;
        }

        public PricingEveryday PriceRoutine
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a collection containing the current price routine.
        /// Views can use this to bind to a data grid or other items control.
        /// </summary>
        public IReadOnlyList<PricingEveryday> PriceRoutineList
        {
            get
            {
                var list = new List<PricingEveryday>();
                list.Add(PriceRoutine);

                return list;
            }
        }


        #endregion

        #region Command Handlers

        private bool SelectAllPriceListsCanExecute(PricingEverydayPriceListGroup priceListGroup)
        {
            bool result = (priceListGroup != null);

            return result;
        }

        private object SelectAllPriceListsCommandExecuted(object parameter)
        {
            bool isSelected = Convert.ToBoolean(parameter);

            foreach (PricingEverydayPriceList priceList in PriceRoutine.LinkedPriceListGroup.FilteredPriceLists)
            {
                priceList.IsSelected = isSelected;
            }

            return isSelected;
        }

        #endregion
    }
}
