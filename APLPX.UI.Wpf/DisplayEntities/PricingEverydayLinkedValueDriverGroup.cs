using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayLinkedValueDriverGroup : DisplayEntityBase
    {
        #region Private Fields

        private int _valueDriverGroupId;
        private short _valueDriverGroupValue;
        private decimal _percentChange;

        #endregion

        #region Constructors and Initialization

        public PricingEverydayLinkedValueDriverGroup()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets/sets the Id of the <see cref="ValueDriverGroup"/> related to this object.
        /// </summary>
        public int ValueDriverGroupId
        {
            get { return _valueDriverGroupId; }
            set { this.RaiseAndSetIfChanged(ref _valueDriverGroupId, value); }
        }

        /// <summary>
        /// Gets/sets the Value of the <see cref="ValueDriverGroup"/> related to this object.
        /// </summary>
        public short ValueDriverGroupValue
        {
            get { return _valueDriverGroupValue; }
            set { this.RaiseAndSetIfChanged(ref _valueDriverGroupValue, value); }
        }

        public decimal PercentChange
        {
            get { return _percentChange; }
            set { this.RaiseAndSetIfChanged(ref _percentChange, value); }
        }

        #endregion

    }
}
