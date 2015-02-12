using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class AnalyticValueDriver : ValueDriver, IDisposable
    {
        #region Private Fields

        private ReactiveList<AnalyticValueDriverMode> _modes;
        private AnalyticValueDriverMode _selectedMode;
        private List<AnalyticResult> _results;
        private bool _runResults;
        private bool _areResultsCurrent;

        private IDisposable _modeChangedListener;
        private bool _isDisposed;

        #endregion

        #region Constructors

        public AnalyticValueDriver()
        {
            Modes = new ReactiveList<AnalyticValueDriverMode>();
            Results = new List<AnalyticResult>();

            Modes.ChangeTrackingEnabled = true;
            _modeChangedListener = Modes.ItemChanged.Subscribe(mode => OnModeChanged(mode));
        }

        #endregion

        #region Properties

        public ReactiveList<AnalyticValueDriverMode> Modes
        {
            get { return _modes; }
            set { this.RaiseAndSetIfChanged(ref _modes, value); }
        }

        public AnalyticValueDriverMode SelectedMode
        {
            get { return _selectedMode; }
            set
            {
                if (_selectedMode != value)
                {
                    _selectedMode = value;
                    this.RaisePropertyChanged("SelectedMode");

                    if (_selectedMode != null)
                    {
                        _selectedMode.RecalculateEditableGroups();
                        UpdateModeSelectionStatus();
                    }
                }
            }
        }

        /// <summary>
        /// Gets/sets the calculated results associated with this value driver.
        /// </summary>
        public List<AnalyticResult> Results
        {
            get { return _results; }
            set { this.RaiseAndSetIfChanged(ref _results, value); }
        }

        /// <summary>
        /// Gets/sets a value indicating whether to "run" this value driver, i.e., calculate its results.
        /// </summary>
        public bool RunResults
        {
            get { return _runResults; }
            set { this.RaiseAndSetIfChanged(ref _runResults, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the results assigned to this driver's groups are valid.
        /// </summary>
        public bool AreResultsCurrent
        {
            get { return _areResultsCurrent; }
            set { this.RaiseAndSetIfChanged(ref _areResultsCurrent, value); }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Assigns each Result of this driver to its corresponding Driver Group
        /// Applies only to the selected mode (e.g., Auto or User).
        /// </summary>        
        public void AssignResultsToDriverGroups()
        {
            AnalyticValueDriverMode selectedMode = Modes.Where(item => item.IsSelected).FirstOrDefault();
            if (selectedMode != null)
            {
                foreach (AnalyticValueDriverGroup driverGroup in selectedMode.Groups)
                {
                    //Assignment is based on matching the Value property of the Result and Driver Group.
                    driverGroup.Results = Results.SingleOrDefault(result => result.Value == driverGroup.Value);
                }
            }
        }

        /// <summary>
        /// Detects property changes to any Mode contained within this value driver.
        /// </summary>  
        private void OnModeChanged(IReactivePropertyChangedEventArgs<AnalyticValueDriverMode> args)
        {
            var mode = args.Sender as AnalyticValueDriverMode;
            if (mode != null)
            {
                //A change to any of the following properties invalidates the driver's results.
                if (args.PropertyName == "MinOutlier" ||
                    args.PropertyName == "MaxOutlier" ||
                    args.PropertyName == "IsSelected" ||
                    args.PropertyName == "GroupCount"||
                    args.PropertyName == "IsDirty")
                {
                    //Update dependent properties.
                    AreResultsCurrent = false;
                    IsDirty = true;
                }

                this.RaisePropertyChanged(args.PropertyName);
            }
        }

        private void UpdateModeSelectionStatus()
        {
            foreach (AnalyticValueDriverMode mode in Modes)
            {
                mode.IsSelected = (mode == SelectedMode);
            }
        }

        #endregion

        public override List<Error> GetValidationErrors()
        {
            var result = new List<Error>();

            if (Modes.Where(m => m.IsSelected).Count() == 0)
            {
                string message = String.Format("\"{0}\" Value Driver: Please specify Auto- or user-generated.", Name);
                result.Add(new Error { Message = message });
            }

            return result;

        }

        #region IDisposable

        protected override void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_modeChangedListener != null)
                    {
                        _modeChangedListener.Dispose();
                        _modeChangedListener = null;
                    }
                    if (Modes != null)
                    {
                        Modes.ChangeTrackingEnabled = false;
                    }
                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
