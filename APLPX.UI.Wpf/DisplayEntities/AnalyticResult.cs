using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class AnalyticResult : ValueDriverGroup
    {
        #region Private Fields
 
        private string _salesValue;      
        private int _skuCount;
        private bool _run;
        private string _driverName;

        #endregion

        #region Constructors

        public AnalyticResult()
        {
        }

        #endregion

        #region Properties

        //TODO: review where/how this property is used. Not in client entity model.
        public string DriverName
        {
            get { return _driverName; }
            set { this.RaiseAndSetIfChanged(ref _driverName, value); }
        }
   
        public string SalesValue
        {
            get { return _salesValue; }
            set { this.RaiseAndSetIfChanged(ref _salesValue, value); }
        }

        public int SkuCount
        {
            get { return _skuCount; }
            set { this.RaiseAndSetIfChanged(ref _skuCount, value); }
        }

        public bool Run
        {
            get { return _run; }
            set { this.RaiseAndSetIfChanged(ref _run, value); }
        }

        #endregion

    }
}
