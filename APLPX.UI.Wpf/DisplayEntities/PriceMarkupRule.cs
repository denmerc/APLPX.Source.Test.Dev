using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PriceMarkupRule : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private decimal _dollarRangeLower;
        private decimal _dollarRangeUpper;
        private int _percentLimitLower;
        private int _percentLimitUpper;

        #endregion

        #region Constructors

        public PriceMarkupRule()
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

        public int PercentLimitLower
        {
            get { return _percentLimitLower; }
            set { this.RaiseAndSetIfChanged(ref _percentLimitLower, value); }
        }

        public int PercentLimitUpper
        {
            get { return _percentLimitUpper; }
            set { this.RaiseAndSetIfChanged(ref _percentLimitUpper, value); }
        }


        #endregion

        #region Overrides

        public override string ToString()
        {
            object[] values = { GetType().Name, DollarRangeLower, DollarRangeLower, PercentLimitLower, PercentLimitUpper };
            string result = String.Format("{0}:Lower=${1};Upper=${2};Lower={3}%;Upper={4}%", values);

            return result;
        }

        #endregion
    }
}
