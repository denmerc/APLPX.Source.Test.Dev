using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PriceRoundingRule : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private int _type;
        private decimal _dollarRangeLower;
        private decimal _dollarRangeUpper;
        private decimal _valueChange;
        private List<SQLEnumeration> _roundingTypes;   

        #endregion

        #region Constructors

        public PriceRoundingRule()
        {
            RoundingTypes = new List<SQLEnumeration>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public int Type
        {
            get { return _type; }
            set 
            {
                if (_type!=value)
                {
                    _type = value;
                    this.RaisePropertyChanged("Type");

                    //Update dependent property.
                    this.RaisePropertyChanged("RoundingTypeName");
                }                
            }
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

        public decimal ValueChange
        {
            get { return _valueChange; }
            set { this.RaiseAndSetIfChanged(ref _valueChange, value); }
        }

        public List<SQLEnumeration> RoundingTypes
        {
            get { return _roundingTypes; }
            set { this.RaiseAndSetIfChanged(ref _roundingTypes, value); }
        }

        /// <summary>
        /// Gets the name of this rule's rounding type. 
        /// </summary>
        public string RoundingTypeName
        {
            get
            {
                string result = String.Empty;

                SQLEnumeration roundingType = RoundingTypes.Find(item => item.Value == this.Type);
                if (roundingType != null)
                {
                    result = roundingType.Name;
                }

                return result;
            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            object[] values = { GetType().Name, DollarRangeLower, DollarRangeUpper, ValueChange, Type };
            string result = String.Format("{0}:Lower=${1};Upper=${2};Change={3};Type={4}", values);

            return result;
        }

        #endregion
    }
}
