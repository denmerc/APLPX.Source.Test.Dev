using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Error : DisplayEntityBase
    {
        #region Private Fields

        private string _message;
        private short _sort;

        #endregion

        #region Constructors

        public Error()
        {
        }

        #endregion

        #region Properties

        public string Message
        {
            get { return _message; }
            set { this.RaiseAndSetIfChanged(ref _message, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        #endregion

    }
}
