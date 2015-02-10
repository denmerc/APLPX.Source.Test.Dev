using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PriceList : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private int _key;
        private string _code;
        private string _name;
        private string _title;
        private bool _isSelected;
        private short _sort;
        private bool _canChangeIsSelected;

        #endregion

        #region Constructors

        public PriceList()
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

        public string Code
        {
            get { return _code; }
            set { this.RaiseAndSetIfChanged(ref _code, value); }
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
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    this.RaisePropertyChanged("IsSelected");

                    IsDirty = true;
                }
            }
        }


        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        /// <summary>
        /// Gets/sets a value indicating whether is IsSelected property of this price list can be changed.
        /// </summary>
        public bool CanChangeIsSelected
        {
            get { return _canChangeIsSelected; }
            set { this.RaiseAndSetIfChanged(ref _canChangeIsSelected, value); }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            string result = String.Format("{0}: Id={1};Key{2};Name={3};IsSelected:{4};Sort={5}", GetType().Name, Id, Key, Name, IsSelected, Sort);

            return result;
        }

        #endregion

    }
}
