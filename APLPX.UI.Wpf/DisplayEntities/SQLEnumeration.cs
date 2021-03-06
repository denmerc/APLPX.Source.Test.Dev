﻿using System;
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

        private short _sort;
        private int _value;
        private string _name;
        private string _description;

        #endregion

        #region Constructors

        public SQLEnumeration()
        {
        }

        #endregion

        #region Properties

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        public int Value
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

        #region Overrides

        public override string ToString()
        {
            object[] values = { GetType().Name, Name, Value, Sort };
            string result = String.Format("{0}:Name=\"{1}\";Value={2};Sort={3}", values);

            return result;
        }

        #endregion

    }
}
