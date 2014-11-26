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
        private short _sort;        
        private bool _isSelected;
        private List<DriverMode> _modes;
        private DriverMode _mode;
        private List<AnalyticResult> _results;

        #endregion

        #region Constructors

        public AnalyticDriver()
        {
            Mode = new DriverMode();
            Modes = new List<DriverMode>();
            Results = new List<AnalyticResult>();
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

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
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

        public List<AnalyticResult> Results
        {
            get { return _results; }
            set { this.RaiseAndSetIfChanged(ref _results, value); }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            string result = String.Format("{0}:Id={1};Name={2};Key={3};IsSelected={4}", GetType().Name, Id, Name, Key, IsSelected);

            return result;
        }

        #endregion
    }
}
