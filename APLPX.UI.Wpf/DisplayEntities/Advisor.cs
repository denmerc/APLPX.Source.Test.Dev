using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Advisor : DisplayEntityBase
    {
        #region Private Fields

        private int _sortOrder;
        private string _message;

        #endregion

        #region Constructors

        public Advisor()
        {
        }

        #endregion

        #region Properties

        public int SortOrder
        {
            get { return _sortOrder; }
            set { this.RaiseAndSetIfChanged(ref _sortOrder, value); }
        }

        public string Message
        {
            get { return _message; }
            set { this.RaiseAndSetIfChanged(ref _message, value); }
        }

        #endregion

    }
}
