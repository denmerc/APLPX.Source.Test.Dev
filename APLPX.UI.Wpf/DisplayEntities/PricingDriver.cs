using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingDriver : DisplayEntityBase
    {

        #region Private Fields

        private int _id;
        private int _key;
        private string _name;
        private string _tooltip;
        private bool _isKeyDriver;
        private int _skuCount;                
     
        #endregion

        #region Constructors

        public PricingDriver()
        {            
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

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

        public string Tooltip
        {
            get { return _tooltip; }
            set { this.RaiseAndSetIfChanged(ref _tooltip, value); }
        }

        public bool IsKeyDriver
        {
            get { return _isKeyDriver; }
            set { this.RaiseAndSetIfChanged(ref _isKeyDriver, value); }
        }
     
        public int SkuCount
        {
            get { return _skuCount; }
            set { this.RaiseAndSetIfChanged(ref _skuCount, value); }
        }


        #endregion

    }
}
