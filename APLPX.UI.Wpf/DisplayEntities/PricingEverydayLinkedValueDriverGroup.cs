using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayLinkedValueDriverGroup : DisplayEntityBase
    {
        #region Private Fields

        private int _valueDriverGroupId;
        private decimal _percentChange;

        #endregion

        #region Constructors and Initialization

        public PricingEverydayLinkedValueDriverGroup()
        {

        }

        #endregion

        #region Properties

        public int ValueDriverGroupId
        {
            get { return _valueDriverGroupId; }
            set { this.RaiseAndSetIfChanged(ref _valueDriverGroupId, value); }
        }

        public decimal PercentChange
        {
            get { return _percentChange; }
            set { this.RaiseAndSetIfChanged(ref _percentChange, value); }
        }

        #endregion

    }
}
