using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class AnalyticPriceListGroup : PriceListGroup
    {

        #region Private Fields

        private List<PriceList> _priceLists;
        private string _typeName;

        #endregion

        #region Constructors

        public AnalyticPriceListGroup()
        {
            PriceLists = new List<PriceList>();
        }

        #endregion

        #region Properties

        public string TypeName 
        {
            get { return _typeName; }
            set { this.RaiseAndSetIfChanged(ref _typeName, value); }
        }
        public List<PriceList> PriceLists
        {
            get { return _priceLists; }
            set { this.RaiseAndSetIfChanged(ref _priceLists, value); }
        }

        #endregion
    }
}
