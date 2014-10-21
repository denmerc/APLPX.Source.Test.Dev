using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class DriverMode : DisplayEntityBase
    {
        #region Private Fields

        private int _key;
        private string _name;
        private short _sortOrder;
        private string _title;
        private bool _isSelected;
        private List<DriverGroup> _groups;

        #endregion

        #region Constructors

        public DriverMode()
        {
            Groups = new List<DriverGroup>();
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

        public short SortOrder
        {
            get { return _sortOrder; }
            set { this.RaiseAndSetIfChanged(ref _sortOrder, value); }
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

        public List<DriverGroup> Groups
        {
            get { return _groups; }
            set { this.RaiseAndSetIfChanged(ref _groups, value); }
        }

        #endregion

    }
}
