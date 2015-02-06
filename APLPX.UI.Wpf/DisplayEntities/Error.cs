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
        private string _source;

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

        public string Source
        {
            get { return _source; }
            set { this.RaiseAndSetIfChanged(ref _source, value); }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            string result = String.Format("{0}:Message={1};Source={2}", GetType().Name, Message, Source);

            return result;
        }

        #endregion
    }
}
