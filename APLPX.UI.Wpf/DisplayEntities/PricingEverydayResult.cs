using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayResult : DisplayEntityBase
    {
        #region Private Fields

        private int _skuId;
        private string _skuName;
        private string _skuTitle;
        private PricingResultDriverGroup _groups;
        private List<PricingEverydayResultPriceList> _priceLists;

        #endregion

        #region Constructors

        public PricingEverydayResult()
        {
            Groups = new PricingResultDriverGroup();
            PriceLists = new List<PricingEverydayResultPriceList>();
        }

        #endregion

        #region Properties

        public int SkuId
        {
            get { return _skuId; }
            set { this.RaiseAndSetIfChanged(ref _skuId, value); }
        }

        public string SkuName
        {
            get { return _skuName; }
            set { this.RaiseAndSetIfChanged(ref _skuName, value); }
        }

        public string SkuTitle
        {
            get { return _skuTitle; }
            set { this.RaiseAndSetIfChanged(ref _skuTitle, value); }
        }

        public PricingResultDriverGroup Groups
        {
            get { return _groups; }
            set { this.RaiseAndSetIfChanged(ref _groups, value); }
        }

        public List<PricingEverydayResultPriceList> PriceLists
        {
            get { return _priceLists; }
            set { this.RaiseAndSetIfChanged(ref _priceLists, value); }
        }

        #endregion

    }
}
