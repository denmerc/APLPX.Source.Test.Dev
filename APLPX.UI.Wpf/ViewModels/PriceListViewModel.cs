using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Interfaces;

namespace APLPX.UI.WPF.ViewModels
{
    public class PriceListViewModel : ViewModelBase
    {
        private ISearchableEntity _owner;
        private List<PriceListGroup> _priceListGroups;

        public PriceListViewModel(ISearchableEntity owner, List<PriceListGroup> priceListGroups)
        {
            _owner = owner;
            _priceListGroups = priceListGroups;
        }

        public List<PriceListGroup> PriceListGroups
        {
            get { return _priceListGroups; }
            private set { _priceListGroups = value; }
        }
    }
}
