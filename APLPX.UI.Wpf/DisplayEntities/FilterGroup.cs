﻿using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class FilterGroup : DisplayEntityBase
    {

        #region Private Fields

        private string _typeName;
        private List<Filter> _filters;

        #endregion

        #region Constructors

        public FilterGroup()
        {
            Filters = new  List<Filter>();
        }

        #endregion

        #region Properties

        public string TypeName
        {
            get { return _typeName; }
            set { this.RaiseAndSetIfChanged(ref _typeName, value); }
        }

        public List<Filter> Filters
        {
            get { return _filters; }
            set { this.RaiseAndSetIfChanged(ref _filters, value); }
        }


        #endregion
    }
}
