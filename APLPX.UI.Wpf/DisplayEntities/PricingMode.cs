using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingMode : DisplayEntityBase
    {
        #region Private Fields

        private int _key;
        private string _name;
        private string _title;
        private bool _isSelected;
        private bool _hasKeyPriceListRule;
        private bool _hasLinkedPriceListRule;
        private int _keyPriceListGroupKey;
        private int _linkedPriceListGroupKey;
        private short _sort;

        #endregion

        #region Constructors

        public PricingMode()
        {
        }

        #endregion

        #region Properties

        public int Key
        {
            get { return _key; }
            set { this.RaiseAndSetIfChanged(ref _key, value); }
        }

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

        public bool IsSelected
        {
            get { return _isSelected; }
            set { this.RaiseAndSetIfChanged(ref _isSelected, value); }
        }

        public bool HasKeyPriceListRule
        {
            get { return _hasKeyPriceListRule; }
            set { this.RaiseAndSetIfChanged(ref _hasKeyPriceListRule, value); }
        }

        public bool HasLinkedPriceListRule
        {
            get { return _hasLinkedPriceListRule; }
            set { this.RaiseAndSetIfChanged(ref _hasLinkedPriceListRule, value); }
        }

        public int KeyPriceListGroupKey
        {
            get { return _keyPriceListGroupKey; }
            set { this.RaiseAndSetIfChanged(ref _keyPriceListGroupKey, value); }
        }

        public int LinkedPriceListGroupKey
        {
            get { return _linkedPriceListGroupKey; }
            set { this.RaiseAndSetIfChanged(ref _linkedPriceListGroupKey, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        #endregion


    }
}
