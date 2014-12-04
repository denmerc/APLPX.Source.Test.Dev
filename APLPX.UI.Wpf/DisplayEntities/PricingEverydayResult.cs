using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayResult : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private string _skuName;
        private string _skuTitle;
        private List<PricingResultDriverGroup> _groups;
        private List<PricingEverydayResultPriceList> _priceLists;
        private short _sort;

        #endregion

        #region Constructors

        public PricingEverydayResult()
        {
            Groups = new List<PricingResultDriverGroup>();
            PriceLists = new List<PricingEverydayResultPriceList>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
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

        public List<PricingResultDriverGroup> Groups
        {
            get { return _groups; }
            set { this.RaiseAndSetIfChanged(ref _groups, value); }
        }

        public List<PricingEverydayResultPriceList> PriceLists
        {
            get { return _priceLists; }
            set { this.RaiseAndSetIfChanged(ref _priceLists, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        #endregion

    }
}
