using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class FilterGroup : DisplayEntityBase
    {

        #region Private Fields

        private string _name;
        private short _sort;
        private List<Filter> _filters;

        #endregion

        #region Constructors

        public FilterGroup()
        {
            Filters = new  List<Filter>();
        }

        #endregion

        #region Properties

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

        public List<Filter> Filters
        {
            get { return _filters; }
            set { this.RaiseAndSetIfChanged(ref _filters, value); }
        }

        
        #endregion
    }
}
