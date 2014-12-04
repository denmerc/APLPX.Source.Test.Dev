using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PriceOptimizationRule : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private decimal _dollarRangeLower;
        private decimal _dollarRangeUpper;
        private decimal _percentChange;
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

        public decimal PercentChange
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
