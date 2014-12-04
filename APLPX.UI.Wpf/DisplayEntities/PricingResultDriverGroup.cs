using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingResultDriverGroup : ValueDriverGroup
    {
        #region Private Fields

        private string _name;
        private string _title;
        private string _actual;
        private int _skuCount;
        private string _salesValue;

        #endregion

        #region Constructors

        public PricingResultDriverGroup()
        {
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Title
        {
            get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }

        public string Actual
        {
            get { return _actual; }
            set { this.RaiseAndSetIfChanged(ref _actual, value); }
        }

        public int SkuCount
        {
            get { return _skuCount; }
            set { this.RaiseAndSetIfChanged(ref _skuCount, value); }
        }

        public string SalesValue
        {
            get { return _salesValue; }
            set { this.RaiseAndSetIfChanged(ref _salesValue, value); }
        }

        #endregion

    }
}
