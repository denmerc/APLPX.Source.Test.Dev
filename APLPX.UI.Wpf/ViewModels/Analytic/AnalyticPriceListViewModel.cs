using System;
using System.Collections.Generic;
using Display = APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels.Analytic
{
    public class AnalyticPriceListViewModel : ViewModelBase
    {
        private Display.Analytic _entity;
        private List<Display.AnalyticPriceListGroup> _priceListGroups;

        public AnalyticPriceListViewModel(Display.Analytic entity, List<Display.AnalyticPriceListGroup> priceListGroups)
        {
            Entity = entity;
            PriceListGroups = priceListGroups;
        }

        public Display.Analytic Entity
        {
            get { return _entity; }
            private set { _entity = value; }
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
    }
}
