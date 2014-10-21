using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Display entity for a lookup item.
    /// </summary>
    public class SQLEnumeration : DisplayEntityBase
    {
        #region Private Fields

        private short _sortOrder;
        private short _value;
        private string _name;
        private string _description;

        #endregion

        #region Constructors

        public SQLEnumeration()
        {
        }

        #endregion

        #region Properties

        public short SortOrder
        {
            get { return _sortOrder; }
            set { this.RaiseAndSetIfChanged(ref _sortOrder, value); }
        }

        public short Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Description
        {
            get { return _description; }
            set { this.RaiseAndSetIfChanged(ref _description, value); }
        }


        #endregion

    }
}
