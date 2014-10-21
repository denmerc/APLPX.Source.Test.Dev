using System;
using System.Collections.Generic;

using APLPX.Client.Entity;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class ModuleFeatureStep : DisplayEntityBase
    {
        #region Private Fields

        private ModuleFeatureStepType _typeId;
        private short _index;
        private string _name;
        private string _title;
        private bool _isVisible;

        private List<Error> _errors;
        private List<Advisor> _advisors;

        #endregion

        #region Constructors

        public ModuleFeatureStep()
        {
            Errors = new List<Error>();
            Advisors = new List<Advisor>();
        }

        #endregion

        #region Properties

        public ModuleFeatureStepType TypeId
        {
            get { return _typeId; }
            set { this.RaiseAndSetIfChanged(ref _typeId, value); }
        }

        public short Index
        {
            get { return _index; }
            set { this.RaiseAndSetIfChanged(ref _index, value); }
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

        public bool IsVisible
        {
            get { return _isVisible; }
            set { this.RaiseAndSetIfChanged(ref _isVisible, value); }
        }

        public List<Error> Errors
        {
            get { return _errors; }
            set { this.RaiseAndSetIfChanged(ref _errors, value); }
        }

        public List<Advisor> Advisors
        {
            get { return _advisors; }
            set { this.RaiseAndSetIfChanged(ref _advisors, value); }
        }

        #endregion

    }
}
