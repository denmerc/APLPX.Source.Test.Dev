using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Base class ofr Value Driver Group display entities.
    /// </summary>
    public class ValueDriverGroup : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private short _value;
        private decimal _minOutlier;
        private decimal _maxOutlier;
        private short _sort;

        private bool _isMinValueEditable;
        private bool _isMaxValueEditable;

        #endregion

        #region Constructors

        public ValueDriverGroup()
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

        public bool IsMinValueEditable
        {
            get { return _isMinValueEditable; }
            set { this.RaiseAndSetIfChanged(ref _isMinValueEditable, value); }
        }

        public bool IsMaxValueEditable
        {
            get { return _isMaxValueEditable; }
            set { this.RaiseAndSetIfChanged(ref _isMaxValueEditable, value); }
        }

        #endregion

    }
}
