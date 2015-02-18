using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;


namespace APLPX.UI.WPF.ViewModels.Analytic
{
    /// <summary>
    /// View model for analytic identity-related views.
    /// </summary>
    public class AnalyticIdentityViewModel : ViewModelBase
    {
        #region Private Fields

        private Display.Analytic _analytic;
        private IDisposable _searchKeyChangedSubscription;
        private bool _isDisposed;

        #endregion

        #region Constructor

        public AnalyticIdentityViewModel(Display.Analytic analytic, Display.ModuleFeature feature)
        {
            if (analytic == null)
            {
                throw new ArgumentNullException("analytic");
            }
            if (feature == null)
            {
                throw new ArgumentNullException("feature");
            }         

            Analytic = analytic;
            base.SelectedFeature = feature;

            InitializeEventHandlers();
            InitializeCommands();
        }

        private void InitializeEventHandlers()
        {

            IObservable<bool> dirtyChanged = _analytic.WhenAnyValue(item => item.IsDirty);
            dirtyChanged.Subscribe(val => OnAnalyticIsDirtyChanged(val));

            var searchKeyChanged = _analytic.WhenAnyValue(item => item.SearchGroupKey);
            _searchKeyChangedSubscription = searchKeyChanged.Subscribe(key => OnSearchKeyChanged(key));
        }

        private void InitializeCommands()
        {
            IObservable<bool> canExecute = _analytic.WhenAnyValue(v => v.IsDirty).Where(dirty => true);
            SaveCommand = ReactiveCommand.Create(canExecute);
            this.WhenAnyObservable(vm => vm.SaveCommand).Subscribe(val => SaveExecuted(val));

            CancelCommand = ReactiveCommand.Create(canExecute);
            this.WhenAnyObservable(vm => vm.CancelCommand).Subscribe(val => CancelExecuted(val));

            Commands.Add(new Display.Action { Command = SaveCommand, Name = "Save", TypeId = Entity.ModuleFeatureStepActionType.PlanningAnalyticsIdentitySave });
            Commands.Add(new Display.Action { Command = CancelCommand, Name = "Cancel", TypeId = Entity.ModuleFeatureStepActionType.PlanningAnalyticsIdentityCancel });
        }

        #endregion

        #region Properties

        public ReactiveCommand<object> SaveCommand
        {
            get;
            private set;
        }

        public ReactiveCommand<object> CancelCommand
        {
            get;
            private set;
        }

        public Display.Analytic Analytic
        {
            get { return _analytic; }
            private set { _analytic = value; }
        }

        public List<Display.FeatureSearchGroup> AssignableFolders
        {
            get
            {
                var searchGroups = SelectedFeature.SearchGroups.Where(sg => sg.CanSearchKeyChange);
                var result = searchGroups.ToList();

                return result;
            }
        }

        #endregion

        #region Command and Event Handlers

        private void CancelExecuted(object val)
        {
        }

        private void SaveExecuted(object val)
        {
        }

        private void OnAnalyticIsDirtyChanged(bool isDirty)
        {
            if (isDirty)
            {
                SelectedFeature.SelectedStep.IsCompleted = false;
                SelectedFeature.DisableRemainingSteps();
            }
        }

        private void OnSearchKeyChanged(string searchKey)
        {
            if (SelectedFeature != null)
            {
                SelectedFeature.AssignSearchProperties();
            }
        }

        private bool SaveCanExecute(AnalyticIdentityViewModel analytic)
        {
            return Analytic.IsDirty;
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_searchKeyChangedSubscription != null)
                    {
                        _searchKeyChangedSubscription.Dispose();
                        _searchKeyChangedSubscription = null;
                    }
                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
