using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayPriceListGroup : PriceListGroup
    {
        private List<PricingEverydayPriceList> _priceLists;
        private ObservableCollection<PricingEverydayPriceList> _filteredPriceLists;

        public PricingEverydayPriceListGroup()
        {
            PriceLists = new List<PricingEverydayPriceList>();
        }

        public List<PricingEverydayPriceList> PriceLists
        {
            get { return _priceLists; }
            set { this.RaiseAndSetIfChanged(ref _priceLists, value); }
        }

        /// <summary>
        /// Gets the price lists in this group whose sort orders fall below that of the designated key price list.
        /// </summary>
        public ObservableCollection<PricingEverydayPriceList> FilteredPriceLists
        {
            get { return _filteredPriceLists; }
            private set { this.RaiseAndSetIfChanged(ref _filteredPriceLists, value); }
        }

        public void RecalculateFilteredPriceLists(int keyPriceListId = 0)
        {
            var result = new ObservableCollection<PricingEverydayPriceList>();

            var includedLists = PriceLists;

            if (keyPriceListId > 0)
            {
                var keyPriceList = PriceLists.Where(p => p.Id == keyPriceListId).FirstOrDefault();
                if (keyPriceList != null)
                {
                    short keySort = keyPriceList.Sort;
                    includedLists = includedLists.Where(p => p.Sort > keySort).ToList();
                }
            }

            FilteredPriceLists = new ObservableCollection<PricingEverydayPriceList>(includedLists);
        }

    }
}
