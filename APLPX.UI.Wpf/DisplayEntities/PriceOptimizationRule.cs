using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PriceOptimizationRule : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private decimal _dollarRangeLower;
        private decimal _dollarRangeUpper;
        private int _percentChange;
        private short _sort;

        #endregion

        #region Constructors

        public PriceOptimizationRule()
        {
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public decimal DollarRangeLower
        {
            get { return _dollarRangeLower; }
            set { this.RaiseAndSetIfChanged(ref _dollarRangeLower, value); }
        }

        public decimal DollarRangeUpper
        {
            get { return _dollarRangeUpper; }
            set { this.RaiseAndSetIfChanged(ref _dollarRangeUpper, value); }
        }

        public int PercentChange
        {
            get { return _percentChange; }
            set { this.RaiseAndSetIfChanged(ref _percentChange, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        #endregion
    }
}
