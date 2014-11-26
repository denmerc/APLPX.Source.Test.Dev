using System;
using System.Collections.Generic;

using APLPX.Client.Entity;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Display entity for a step within a feature.
    /// </summary>
    public class ModuleFeatureStep : DisplayEntityBase
    {
        #region Private Fields

        private ModuleFeatureStepType _typeId;
        private short _sort;
        private string _name;
        private string _title;
        private bool _isEnabled;
        private bool _isVisible;

        private List<Action> _actions;
        private List<Error> _errors;
        private List<Advisor> _advisors;

        private Action _selectedAction;

        #endregion

        #region Constructors

        public ModuleFeatureStep()
        {
            Actions = new List<Action>();
            Errors = new List<Error>();
            Advisors = new List<Advisor>();

            //Set default values.
            IsEnabled = true;
            IsVisible = true;
        }

        #endregion

        #region Properties

        public ModuleFeatureStepType TypeId
        {
            get { return _typeId; }
            set { this.RaiseAndSetIfChanged(ref _typeId, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
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

        /// <summary>
        /// Gets/sets whether this step should be enabled. 
        /// </summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { this.RaiseAndSetIfChanged(ref _isEnabled, value); }
        }

        /// <summary>
        /// Gets/sets whether this step should be visible. 
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set { this.RaiseAndSetIfChanged(ref _isVisible, value); }
        }   

        /// <summary>
        /// Gets/sets the actions that apply to this step.
        /// </summary>
        public List<Action> Actions
        {
            get { return _actions; }
            set { this.RaiseAndSetIfChanged(ref _actions, value); }
        }

        /// <summary>
        /// Gets/sets the currently selected action for this step.
        /// </summary>
        public Action SelectedAction
        {
            get { return _selectedAction; }
            set { this.RaiseAndSetIfChanged(ref _selectedAction, value); }
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

        #region Overrides

        public override string ToString()
        {
            string result = String.Format("{0}:Name={1};Type={2}", GetType().Name, Name, TypeId);
            return result;
        }

        #endregion
    }
}
