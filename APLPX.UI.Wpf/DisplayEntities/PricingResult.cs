using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingResult : DisplayEntityBase
    {
        #region Private Fields

        private short _group;
        private decimal _minValue;
        private decimal _maxValue;
        private decimal _salesVAlue;
        private short _sort;

        #endregion

        #region Constructors

        public PricingResult()
        {
        }

        #endregion

        #region Properties

        public short Group
        {
            get { return _group; }
            set { this.RaiseAndSetIfChanged(ref _group, value); }
        }

        public decimal MinValue
        {
            get { return _minValue; }
            set { this.RaiseAndSetIfChanged(ref _minValue, value); }
        }

        public decimal MaxValue
        {
            get { return _maxValue; }
            set { this.RaiseAndSetIfChanged(ref _maxValue, value); }
        }

        public decimal SalesValue
        {
            get { return _salesVAlue; }
            set { this.RaiseAndSetIfChanged(ref _salesVAlue, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        #endregion

    }
}
