using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private System.Int16 _sort;
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
            set { this.RaiseAndSetIfChanged(ref _type, value); }
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

        public System.Int16 Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        public List<SQLEnumeration> RoundingTypes
        {
            get { return _roundingTypes; }
            set { this.RaiseAndSetIfChanged(ref _roundingTypes, value); }
        }

        #endregion

    }
}
