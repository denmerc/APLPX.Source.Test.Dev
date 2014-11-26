using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class AnalyticResult : DisplayEntityBase
    {
        #region Private Fields

        private short _group;
        private decimal _minValue;
        private decimal _maxValue;
        private string _salesValue;
        private short _sort;
        private int _skuCount;
        private string _driverName;

        #endregion

        #region Constructors

        public AnalyticResult()
        {
        }

        #endregion

        #region Properties


        public string DriverName
        {
            get { return _driverName; }
            set { this.RaiseAndSetIfChanged(ref _driverName, value); }
        }

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
        
        public string SalesValue
        {
            get { return _salesValue; }
            set { this.RaiseAndSetIfChanged(ref _salesValue, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        public int SkuCount
        {
            get { return _skuCount; }
            set { this.RaiseAndSetIfChanged(ref _skuCount, value); }
        }

        #endregion

    }
}
