using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace APLX.UI.WPF.DisplayEntities
{
    public class Step : DisplayEntityBase
    {
        #region Private Fields

        private int _index;
        private bool _isEnabled;
        private bool _isDirty;
        private bool _isEnabledPrevious;
        private bool _isEnabledNext;
        private string _name;
        private string _caption;
        private bool _isValid;
        private bool _isActive;
        private bool _isWorking;
        private List<Advisor> _advisors;
        private List<Error> _errors;
        private string _headerTitle;
        private string _stepHeader;
        private int _activeStep;
        private int _entityId;

        #endregion

        #region Constructors

        public Step()
        {
            Advisors = new List<Advisor>();
            Errors = new List<Error>();
        }

        #endregion

        #region Properties

        public int Index
        {
            get { return _index; }
            set { this.RaiseAndSetIfChanged(ref _index, value); }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { this.RaiseAndSetIfChanged(ref _isEnabled, value); }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { this.RaiseAndSetIfChanged(ref _isDirty, value); }
        }

        public bool IsEnabledPrevious
        {
            get { return _isEnabledPrevious; }
            set { this.RaiseAndSetIfChanged(ref _isEnabledPrevious, value); }
        }

        public bool IsEnabledNext
        {
            get { return _isEnabledNext; }
            set { this.RaiseAndSetIfChanged(ref _isEnabledNext, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Caption
        {
            get { return _caption; }
            set { this.RaiseAndSetIfChanged(ref _caption, value); }
        }

        public bool IsValid
        {
            get { return _isValid; }
            set { this.RaiseAndSetIfChanged(ref _isValid, value); }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { this.RaiseAndSetIfChanged(ref _isActive, value); }
        }

        public bool IsWorking
        {
            get { return _isWorking; }
            set { this.RaiseAndSetIfChanged(ref _isWorking, value); }
        }

        public List<Advisor> Advisors
        {
            get { return _advisors; }
            set { this.RaiseAndSetIfChanged(ref _advisors, value); }
        }

        public List<Error> Errors
        {
            get { return _errors; }
            set { this.RaiseAndSetIfChanged(ref _errors, value); }
        }

        public string HeaderTitle
        {
            get { return _headerTitle; }
            set { this.RaiseAndSetIfChanged(ref _headerTitle, value); }
        }

        public string StepHeader
        {
            get { return _stepHeader; }
            set { this.RaiseAndSetIfChanged(ref _stepHeader, value); }
        }

        public int ActiveStep
        {
            get { return _activeStep; }
            set { this.RaiseAndSetIfChanged(ref _activeStep, value); }
        }

        public int EntityId
        {
            get { return _entityId; }
            set { this.RaiseAndSetIfChanged(ref _entityId, value); }
        }


        #endregion

    }
}
