using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PriceListGroup : DisplayEntityBase
    {

        #region Private Fields

        private string _typeName;
        private short _sort;
        private List<PriceList> _priceLists;

        #endregion

        #region Constructors

        public PriceListGroup()
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
        
        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        public List<PriceList> PriceLists
        {
            get { return _priceLists; }
            set { this.RaiseAndSetIfChanged(ref _priceLists, value); }
        }


        #endregion
    }
}
