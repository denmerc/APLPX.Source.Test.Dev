using System;


using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class PricingEverydayStrategyViewModel : ViewModelBase
    {

        private PricingEveryday _priceRoutine;

        public PricingEverydayStrategyViewModel(PricingEveryday priceRoutine)
        {
            if (priceRoutine == null)
            {
                throw new ArgumentNullException("priceRoutine");
            }

            PriceRoutine = priceRoutine;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var canSetAsKey = this.WhenAnyValue(vm => vm.PriceRoutine.SelectedValueDriverWrapper, (wrapper) => SetKeyDriverCanExecute(wrapper));
            SetKeyDriverCommand = ReactiveCommand.Create(canSetAsKey);

            this.WhenAnyObservable(vm => vm.SetKeyDriverCommand).Subscribe(item => SetKeyDriverExecuted(item));
        }

        #region Properties

        public ReactiveCommand<object> SetKeyDriverCommand
        {
            get;
            private set;
        }

        public PricingEveryday PriceRoutine
        {
            get { return _priceRoutine; }
            private set { _priceRoutine = value; }
        }

        #endregion

        #region Command Handlers

        private bool SetKeyDriverCanExecute(PricingEverydayValueDriverWrapper wrapper)
        {
            bool result = (wrapper != null && 
                           wrapper.BaseDriver != null &&
                          !wrapper.BaseDriver.IsKey);

            return result;
        }

        private object SetKeyDriverExecuted(object parameter)
        {
            var wrapper = parameter as PricingEverydayValueDriverWrapper;
            if (wrapper != null)
            {
                PriceRoutine.SetKeyValueDriver(wrapper.BaseDriver);
            }
            return wrapper;
        }

        #endregion

    }
}
