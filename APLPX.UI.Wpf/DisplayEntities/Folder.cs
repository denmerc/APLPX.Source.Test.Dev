using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Folder : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private int _template;
        private short _itemCount;
        private string _name;
        private string _parentName;
        private bool _isNameChanged;
        private bool _canNameChange;
        private short _sortOrder;

        #endregion

        #region Constructors

        public Folder()
        {
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public int Template
        {
            get { return _template; }
            set { this.RaiseAndSetIfChanged(ref _template, value); }
        }

        public short ItemCount
        {
            get { return _itemCount; }
            set { this.RaiseAndSetIfChanged(ref _itemCount, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string ParentName
        {
            get { return _parentName; }
            set { this.RaiseAndSetIfChanged(ref _parentName, value); }
        }

        public bool IsNameChanged
        {
            get { return _isNameChanged; }
            set { this.RaiseAndSetIfChanged(ref _isNameChanged, value); }
        }

        public bool CanNameChange
        {
            get { return _canNameChange; }
            set { this.RaiseAndSetIfChanged(ref _canNameChange, value); }
        }

        public short SortOrder
        {
            get { return _sortOrder; }
            set { this.RaiseAndSetIfChanged(ref _sortOrder, value); }
        }

        #endregion

    }
}
