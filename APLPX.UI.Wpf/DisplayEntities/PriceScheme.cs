using System;
using System.Collections.Generic;

using APLPX.Client.Display;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PriceScheme : DisplayEntityBase
    {
        #region Private Fields

        private PricingMode _mode; //TODO: move namespace
        private List<PriceList> _priceLists;

        #endregion

        #region Constructors

        public PriceScheme()
        {
            PriceLists = new List<PriceList>();
        }

        #endregion

        #region Properties

        public PricingMode Mode
        {
            get { return _mode; }
            set { this.RaiseAndSetIfChanged(ref _mode, value); }
        }

        public List<PriceList> PriceLists
        {
            get { return _priceLists; }
            private set { this.RaiseAndSetIfChanged(ref _priceLists, value); }
        }

        #endregion

    }
}
