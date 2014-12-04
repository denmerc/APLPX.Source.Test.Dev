using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Base class for Price List Group display entities.
    /// </summary>
    public abstract class PriceListGroup : DisplayEntityBase
    {

        #region Private Fields

        private int _key;
        private string _name;
        private string _title;       
        private short _sort;
     
        #endregion

        #region Constructors

        public PriceListGroup()
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

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        #endregion
    }
}
