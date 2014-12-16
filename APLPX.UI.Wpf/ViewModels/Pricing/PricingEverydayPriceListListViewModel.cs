using System;
using System.Collections.Generic;

using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class PricingEverydayPriceListListViewModel : ViewModelBase
    {
        private PricingEveryday _priceRoutine;


        #region Constructor and Initialization

        public PricingEverydayPriceListListViewModel(PricingEveryday priceRoutine)
        {
            if (priceRoutine == null)
            {
                throw new ArgumentNullException("priceRoutine");
            }

            PriceRoutine = priceRoutine;
            InitializeCommands();
            PriceRoutine.UpdatePriceListGroups();
        }

        private void InitializeCommands()
        {
            ApplyPriceRangeCommand = ReactiveCommand.Create();
            ApplyPriceRangeCommand.Subscribe(_ => ApplyPriceRangeExecuted(_));

            RecalculateLinkedPriceListsCommand = ReactiveCommand.Create();
            RecalculateLinkedPriceListsCommand.Subscribe(obj => RecalculateLinkedPriceListsExecuted(obj));
        }

        #endregion

        public PricingEveryday PriceRoutine
        {
            get { return _priceRoutine; }
            private set { _priceRoutine = value; }
        }

        public ReactiveCommand<object> RecalculateLinkedPriceListsCommand { get; private set; }

        public ReactiveCommand<object> ApplyPriceRangeCommand { get; private set; }

        #region Command Handlers

        private object ApplyPriceRangeExecuted(object parameter)
        {
            //TODO: implement logic
            if (PriceRoutine.SelectedMode.HasKeyPriceListRule)
            {
            }
            if (PriceRoutine.SelectedMode.HasLinkedPriceListRule)
            {
            }
            return parameter;
        }

        private object RecalculateLinkedPriceListsExecuted(object parameter)
        {
            PricingEverydayPriceListGroup target = PriceRoutine.LinkedPriceListGroup;
            if (target != null)
            {
                target.RecalculateFilteredPriceLists(PriceRoutine.SelectedKeyPriceList.Id);
            }

            return parameter;
        }

        #endregion
    }
}
