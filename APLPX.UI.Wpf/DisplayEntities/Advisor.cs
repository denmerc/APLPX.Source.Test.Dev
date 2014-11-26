using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Advisor : DisplayEntityBase
    {
        #region Private Fields

        private short _sort;
        private string _message;

        #endregion

        #region Constructors

        public Advisor()
        {
        }

        #endregion

        #region Properties

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        public string Message
        {
            get { return _message; }
            set { this.RaiseAndSetIfChanged(ref _message, value); }
        }

        #endregion

    }
}
