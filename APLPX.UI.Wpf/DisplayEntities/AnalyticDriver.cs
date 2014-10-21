using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class AnalyticDriver : DisplayEntityBase
    {

        #region Private Fields

        private int _id;
        private int _key;
        private string _name;
        private string _title;
        private short _sortOrder;        
        private bool _isSelected;
        private List<DriverMode> _modes;
        private DriverMode _mode;

        #endregion

        #region Constructors

        public AnalyticDriver()
        {
            Modes = new List<DriverMode>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public int Key
        {
            get { return _key; }
            set { this.RaiseAndSetIfChanged(ref _key, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Title
        {
            get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }

        public short SortOrder
        {
            get { return _sortOrder; }
            set { this.RaiseAndSetIfChanged(ref _sortOrder, value); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { this.RaiseAndSetIfChanged(ref _isSelected, value); }
        }

        public List<DriverMode> Modes
        {
            get { return _modes; }
            set { this.RaiseAndSetIfChanged(ref _modes, value); }
        }

        public DriverMode Mode
        {
            get { return _mode; }
            set { this.RaiseAndSetIfChanged(ref _mode, value); }
        }

        #endregion

    }
}
