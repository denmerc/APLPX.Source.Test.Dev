using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class AnalyticValueDriver : ValueDriver
    {

        #region Private Fields
      
        private List<AnalyticValueDriverMode> _modes;
        private AnalyticValueDriverMode _selectedMode;
        private List<AnalyticResult> _results;

        //IN DEVEL: NEW LOGIC
        private AnalyticValueDriverMode _mode;
        #endregion

        #region Constructors

        public AnalyticValueDriver()
        {
            Mode = new AnalyticValueDriverMode();
            Modes = new List<AnalyticValueDriverMode>();
            Results = new List<AnalyticResult>();
        }

        #endregion

        #region Properties

        public AnalyticValueDriverMode Mode
        {
            get { return _mode; }
            set { this.RaiseAndSetIfChanged(ref _mode, value); }
        }

        public List<AnalyticValueDriverMode> Modes
        {
            get { return _modes; }
            set { this.RaiseAndSetIfChanged(ref _modes, value); }
        }

        public AnalyticValueDriverMode SelectedMode
        {
            get { return _selectedMode; }
            set { this.RaiseAndSetIfChanged(ref _selectedMode, value); }
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
