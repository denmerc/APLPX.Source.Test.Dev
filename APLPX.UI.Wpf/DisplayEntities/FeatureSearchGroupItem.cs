using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class FeatureSearchGroupItem : DisplayEntityBase
    {

        #region Private Fields

        private string _name;
        private string _parentGroupKey;
        private string _searchKey;
        private string _searchItem;
        private int _sortOrder;

        #endregion

        #region Constructors

        public FeatureSearchGroupItem()
        {
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string ParentGroupKey
        {
            get { return _parentGroupKey; }
            set { this.RaiseAndSetIfChanged(ref _parentGroupKey, value); }
        }

        public string SearchKey
        {
            get { return _searchKey; }
            set { this.RaiseAndSetIfChanged(ref _searchKey, value); }
        }

        public string SearchItem
        {
            get { return _searchItem; }
            set { this.RaiseAndSetIfChanged(ref _searchItem, value); }
        }

        public int SortOrder
        {
            get { return _sortOrder; }
            set { this.RaiseAndSetIfChanged(ref _sortOrder, value); }
        }

        #endregion






    }
}
