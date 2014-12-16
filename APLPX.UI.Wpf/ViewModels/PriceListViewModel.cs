using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Display = APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Interfaces;

namespace APLPX.UI.WPF.ViewModels
{
    public class PriceListViewModel : ViewModelBase
    {
        private Display.Analytic _entity;
        private List<Display.AnalyticPriceListGroup> _priceListGroups;

        public PriceListViewModel(Display.Analytic entity, List<Display.AnalyticPriceListGroup> priceListGroups)
        {
            _entity = entity;
            _priceListGroups = priceListGroups;
        }

        public Display.Analytic Entity
        {
            get { return _entity; }
            private set { _entity = value; }
        }

        public List<Display.AnalyticPriceListGroup> PriceListGroups
        {
            get { return _priceListGroups; }
            private set { _priceListGroups = value; }
        }
    }
}
