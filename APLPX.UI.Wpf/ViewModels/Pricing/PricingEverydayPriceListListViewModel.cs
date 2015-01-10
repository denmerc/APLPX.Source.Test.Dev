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
        }

        #endregion

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
    }
}
