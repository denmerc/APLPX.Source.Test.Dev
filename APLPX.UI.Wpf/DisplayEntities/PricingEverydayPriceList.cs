using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayPriceList : PriceList
    {
        private bool _isKey;        
        private PricingLinkedPriceListRule _linkedPriceListRule;
        private int _ordinalPosition;

        public bool IsKey
        {
            get { return _isKey; }
            set { this.RaiseAndSetIfChanged(ref _isKey, value); }
        }

        public PricingLinkedPriceListRule LinkedPriceListRule
        {
            get { return _linkedPriceListRule; }
            set { this.RaiseAndSetIfChanged(ref _linkedPriceListRule, value); }
        }
    
        public int OrdinalPosition
        {
            get { return _ordinalPosition; }
            set { this.RaiseAndSetIfChanged(ref _ordinalPosition, value); }
        }

        #region Overrides

        public override string ToString()
        {
            object[] values = { GetType().Name, Id, Key, Name, IsSelected, IsKey, Sort };
            string result = String.Format("{0}: Id={1};Key{2};Name={3};IsSelected:{4};IsKey={5};Sort={6}", values);

            return result;
        }

        #endregion

    }
}
