using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Base class for Value Driver Mode display entities.
    /// </summary>
    public abstract class ValueDriverMode : DisplayEntityBase
    {
        #region Private Fields

        private int _key;
        private string _name;
        private short _sort;
        private string _title;
        private bool _isSelected;
      
        #endregion

        #region Constructors

        public ValueDriverMode()
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

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
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

      
        #endregion

    }
}
