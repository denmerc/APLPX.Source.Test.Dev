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

        /// Gets a value indicating whether this mode has a key price list.
        /// NOTE: this is different from HasKeyPriceListRule. 
        /// For example, A mode can have a key price list but no key price list rule.
        /// In that case, HasKeyPriceList=true and HasKeyPriceListRule=false.
        public bool HasKeyPriceList
        {
            get
            {
                bool result = (KeyPriceListGroupKey > 0);
                return result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this mode has a linked price list.
        /// NOTE: this is different from HasLinkedPriceListRule. 
        /// For example, A mode can have a linked price list but no linked price list rule.
        /// In that case, HasLinkedPriceList=true and HasLinkedPriceListRule=false.
        /// </summary>
        public bool HasLinkedPriceList
        {
            get
            {
                bool result = (LinkedPriceListGroupKey > 0);
                return result;
            }
        }

        #endregion


    }
}
