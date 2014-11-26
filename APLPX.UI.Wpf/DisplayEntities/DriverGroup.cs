using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class DriverGroup : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private short _value;
        private decimal _minOutlier;
        private decimal _maxOutlier;
        private short _sort;

        #endregion

        #region Constructors

        public DriverGroup()
        {
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public short Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }

        public decimal MinOutlier
        {
            get { return _minOutlier; }
            set { this.RaiseAndSetIfChanged(ref _minOutlier, value); }
        }

        public decimal MaxOutlier
        {
            get { return _maxOutlier; }
            set { this.RaiseAndSetIfChanged(ref _maxOutlier, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        #endregion

    }
}
