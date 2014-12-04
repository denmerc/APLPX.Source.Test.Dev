using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class AnalyticPriceListGroup : PriceListGroup
    {

        #region Private Fields

        private List<PriceList> _priceLists;

        #endregion

        #region Constructors

        public AnalyticPriceListGroup()
        {
            PriceLists = new List<PriceList>();
        }

        #endregion

        #region Properties

        public List<PriceList> PriceLists
        {
            get { return _priceLists; }
            set { this.RaiseAndSetIfChanged(ref _priceLists, value); }
        }

        #endregion
    }
}
