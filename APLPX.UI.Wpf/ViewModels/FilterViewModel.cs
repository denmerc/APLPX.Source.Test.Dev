using System;
using System.Collections.ObjectModel;
using System.Linq;

using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using DTO = APLPX.Entity;

namespace APLPX.UI.WPF.ViewModels
{
    /// <summary>
    /// View model for Filter-related views.
    /// </summary>
    public class FilterViewModel : ViewModelBase
    {
        private IFilterContainer _entity;
        private IDisposable _filterChangedSubscription;
        private IDisposable _selectAllFilterSubscription;

        private bool _isDisposed;

        #region Constructor and Initialization

        public FilterViewModel(IFilterContainer entity, ModuleFeature feature)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (feature == null)
            {
                throw new ArgumentNullException("feature");
            }

            Entity = entity;
            SelectedFeature = feature;

            if (entity.SelectedFilterGroup == null && entity.FilterGroups.Count > 0)
            {
                entity.SelectedFilterGroup = entity.FilterGroups[0];
            }

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            IObservable<bool> canExecute = this.WhenAnyValue(vm => vm.Entity.SelectedFilterGroup, (group) => SelectAllFiltersCanExecute(group));
            SelectAllFiltersCommand = ReactiveCommand.Create(canExecute);

            _selectAllFilterSubscription = this.WhenAnyObservable(vm => vm.SelectAllFiltersCommand).Subscribe(item => SelectAllFiltersExecuted(item));
            _filterChangedSubscription = Entity.FilterGroups.ItemChanged.Subscribe(group => OnFilterGroupChanged(group));

            canExecute = this.WhenAnyValue(vm => vm.IsAnyFilterGroupDirty, (val) => SaveCanExecute(val));
            SaveCommand = ReactiveCommand.Create(canExecute);            
            this.WhenAnyObservable(vm => vm.SaveCommand).Subscribe(val => SaveExecuted(val));

            Commands.Add(new DisplayEntities.Action { Command = SaveCommand, Name = "Save", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsFiltersSave });
        }

        #endregion

        #region Properties

        public ReactiveCommand<object> SaveCommand
        {
            get;
            private set;
        }

        public ReactiveCommand<object> SelectAllFiltersCommand
        {
            get;
            private set;
        }

        public IFilterContainer Entity
        {
            get { return _entity; }
            private set { this.RaiseAndSetIfChanged(ref _entity, value); }
        }

        public ObservableCollection<Error> ValidationResults
        {
            get
            {
                var errors = Entity.FilterGroups.SelectMany(grp => grp.ValidationResults);

                var result = new ObservableCollection<Error>(errors);
                return result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether any FilterGroup has unsaved changes.
        /// </summary>
        public bool IsAnyFilterGroupDirty
        {
            get
            {
                bool result = Entity.FilterGroups.Any(grp => grp.IsDirty);

                return result;
            }
        }

        #endregion

        #region Command and Event Handlers

        private bool SaveCanExecute(bool isDirty)
        {
            return isDirty;
        }

        private void SaveExecuted(object parameter)
        {

        }

        private bool SelectAllFiltersCanExecute(FilterGroup filterGroup)
        {
            bool result = (filterGroup != null);

            return result;
        }

        private void SelectAllFiltersExecuted(object parameter)
        {
            bool isSelected = Convert.ToBoolean(parameter);

            foreach (Filter filter in Entity.SelectedFilterGroup.Filters)
            {
                filter.IsSelected = isSelected;
            }
        }

        private void OnFilterGroupChanged(IReactivePropertyChangedEventArgs<FilterGroup> args)
        {
            var source = args.Sender as FilterGroup;
            if (source != null && source.IsDirty)
            {
                SelectedFeature.SelectedStep.IsCompleted = false;
                SelectedFeature.DisableRemainingSteps();
            }

            //Update dependent calculated properties.
            this.RaisePropertyChanged("ValidationResults");
            this.RaisePropertyChanged("IsAnyFilterGroupDirty");
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_selectAllFilterSubscription != null)
                    {
                        _selectAllFilterSubscription.Dispose();
                    }
                    if (_filterChangedSubscription != null)
                    {
                        _filterChangedSubscription.Dispose();
                    }
                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
