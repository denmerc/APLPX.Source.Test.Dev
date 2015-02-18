using System;
using System.Collections.Generic;
using System.Windows;

using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class FeatureSearchGroup : DisplayEntityBase
    {
        #region Private Fields

        private int _searchGroupId;
        private string _searchGroupKey;
        private short _itemCount;
        private string _name;
        private string _parentName;
        private bool _canNameChange;
        private bool _isNameChanged;
        private bool _canSearchKeyChange;
        private bool _isSearchKeyChanged;
        private short _sort;
        private bool _isSubGroup;
        private bool _hasSubGroups;

        #endregion

        #region Constructors

        public FeatureSearchGroup()
        {
        }

        #endregion

        #region Properties

        public int SearchGroupId
        {
            get { return _searchGroupId; }
            set { this.RaiseAndSetIfChanged(ref _searchGroupId, value); }
        }

        public string SearchGroupKey
        {
            get { return _searchGroupKey; }
            set { this.RaiseAndSetIfChanged(ref _searchGroupKey, value); }
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
            set
            {
                if (_isNameChanged != value)
                {
                    _isNameChanged = value;
                    OnPropertyChanged("IsNameChanged");

                    if (_isNameChanged && !IsDirty)
                    {
                        IsDirty = true;
                    }
                }
            }
        }

        public bool IsSearchKeyChanged
        {
            get { return _isSearchKeyChanged; }
            set
            {
                if (_isSearchKeyChanged != value)
                {
                    _isSearchKeyChanged = value;
                    OnPropertyChanged("IsSearchKeyChanged");

                    if (_isSearchKeyChanged && !IsDirty)
                    {
                        IsDirty = true;
                    }
                }
            }
        }

        public bool CanNameChange
        {
            get { return _canNameChange; }
            set { this.RaiseAndSetIfChanged(ref _canNameChange, value); }
        }

        public bool CanSearchKeyChange
        {
            get { return _canSearchKeyChange; }
            set { this.RaiseAndSetIfChanged(ref _canSearchKeyChange, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        /// <summary>
        /// Indicates whether this search group is one of several search groups with the same ParentName.
        /// </summary>
        public bool IsSubGroup
        {
            get { return _isSubGroup; }
            set { this.RaiseAndSetIfChanged(ref _isSubGroup, value); }
        }

        /// <summary>
        /// Indicates whether this search group represents a group containing subgroups.
        /// Example: 
        /// 1. "My Folders" has subitems, e.g., "Folder 1", "Folder 2", etc.
        /// 2. "Shared" does not have subitems.
        /// </summary>
        public bool HasSubGroups
        {
            get { return _hasSubGroups; }
            set { this.RaiseAndSetIfChanged(ref _hasSubGroups, value); }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            object[] values = { GetType().Name, 
                                ParentName, 
                                Name, 
                                SearchGroupKey, 
                                ItemCount, 
                                IsSubGroup, 
                                HasSubGroups, 
                                Sort };

            return String.Format("{0}:ParentName={1};Name={2};GroupKey={3};ItemCount={4};IsSubGroup={5};HasSubGroups={6};Sort={7}", values);
        }

        #endregion
    }
}
