using System;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using DTO = APLPX.Entity;

namespace APLPX.UI.WPF.ViewModels
{
    /// <summary>
    /// View model for searching and filtering entitiels.
    /// </summary>
    public class SearchViewModel : ViewModelBase
    {
        private ISearchableEntity _selectedEntity;
        private IDisposable _selectedEntityChangedSubscription;
        private IDisposable _selectedGroupChangedSubscription;

        private bool _isDisposed;

        #region Constructor and Initialization

        public SearchViewModel(ModuleFeature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException("feature");
            }

            SelectedFeature = feature;

            InitializeEventHandlers();
            InitializeCommands();
        }

        private void InitializeEventHandlers()
        {
            var selectedSearchGroupChanged = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedSearchGroup);
            _selectedGroupChangedSubscription = selectedSearchGroupChanged.Subscribe(m => OnSelectedSearchGroupChanged(m));

            var selectedEntityChanged = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedEntity);
            _selectedEntityChangedSubscription = selectedEntityChanged.Subscribe(m => OnSelectedEntityChanged(m));
        }

        private void InitializeCommands()
        {
            IObservable<bool> canExecute = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedSearchGroup, (searchGroup) => CreateNewEntityCanExecute(searchGroup));
            CreateNewEntityCommand = ReactiveCommand.Create(canExecute);
            this.WhenAnyObservable(vm => vm.CreateNewEntityCommand).Subscribe(val => CreateNewEntityExecuted(val));

            canExecute = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedEntity, (entity) => CopyOrEditCanExecute(entity));
            CopyEntityCommand = ReactiveCommand.Create(canExecute);
            this.WhenAnyObservable(vm => vm.CopyEntityCommand).Subscribe(val => CopyEntityExecuted(val));

            EditEntityCommand = ReactiveCommand.Create(canExecute);
            this.WhenAnyObservable(vm => vm.EditEntityCommand).Subscribe(val => EditEntityExecuted(val));

            canExecute = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedSearchGroup, (searchGroup) => RenameSearchGroupCanExecute(searchGroup));
            RenameSearchGroupCommand = ReactiveCommand.Create(canExecute);
            this.WhenAnyObservable(vm => vm.RenameSearchGroupCommand).Subscribe(val => RenameSearchGroupExecuted(val));

            Commands.Add(new DisplayEntities.Action { Command = CreateNewEntityCommand, Name = "New", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsNew });
            Commands.Add(new DisplayEntities.Action { Command = CopyEntityCommand, Name = "Copy", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsCopy });
            Commands.Add(new DisplayEntities.Action { Command = EditEntityCommand, Name = "Edit", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsEdit });
        }

        #endregion

        #region Properties

        public ReactiveCommand<object> CreateNewEntityCommand
        {
            get;
            private set;
        }

        public ReactiveCommand<object> CopyEntityCommand
        {
            get;
            private set;
        }

        public ReactiveCommand<object> EditEntityCommand
        {
            get;
            private set;
        }

        public ReactiveCommand<object> RenameSearchGroupCommand
        {
            get;
            private set;
        }

        public bool IsSearchFilterSelected
        {
            get
            {
                bool result = false;

                if (SelectedFeature != null)
                {
                    result = (SelectedFeature.SelectedSearchGroup != null && SelectedFeature.FilteredSearchableEntities != null && SelectedFeature.FilteredSearchableEntities.Count > 0);
                }

                return result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether search detail should be displayed.
        /// </summary>
        public bool IsDetailDisplayed
        {
            get
            {
                bool result = false;

                if (SelectedFeature != null)
                {
                    result = (SelectedFeature.SelectedSearchGroup != null && SelectedFeature.SelectedEntity != null);
                }

                return result;
            }
        }

        public string NewEntityCaption
        {
            get
            {
                string result = String.Format("New {0}...", SelectedFeature.Classification);
                return result;
            }
        }

        #endregion

        #region Event Handlers

        private void OnSelectedEntityChanged(ISearchableEntity entity)
        {
            _selectedEntity = entity;
            this.RaisePropertyChanged("IsDetailDisplayed");
        }

        private void OnSelectedSearchGroupChanged(FeatureSearchGroup searchGroup)
        {
            this.RaisePropertyChanged("IsSearchFilterSelected");
        }

        #endregion

        #region Command Handlers

        private bool CreateNewEntityCanExecute(FeatureSearchGroup searchGroup)
        {
            bool result = (searchGroup != null && searchGroup.CanSearchKeyChange);

            return result;
        }

        private void CreateNewEntityExecuted(object parameter)
        {
            //int searchGroupId = SelectedFeature.SelectedSearchGroup.SearchGroupId;
            //var response = AnalyticServices.LoadAnalytic(_selectedEntity as DisplayEntities.Analytic, 0, searchGroupId);

            //  TODO: replace this logic with a direct call to Client Services
            var parentGroup = parameter as FeatureSearchGroup;
            if (parentGroup == null)
            {
                parentGroup = SelectedFeature.SelectedSearchGroup;
            }

            if (parentGroup != null)
            {
                EventAggregator notifier = ((EventAggregator)App.Current.Resources["EventManager"]);
                notifier.Publish(parentGroup);
            }
        }

        private bool CopyOrEditCanExecute(ISearchableEntity entity)
        {
            bool result = (entity != null);

            return result;
        }

        private void CopyEntityExecuted(object parameter)
        {
            base.ShowMessageBox("Copy coming soon...", System.Windows.MessageBoxImage.Information);
            GetEntityFromService(DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsCopy);
        }

        private void EditEntityExecuted(object parameter)
        {
            base.ShowMessageBox("Edit coming soon...", System.Windows.MessageBoxImage.Information);
            GetEntityFromService(DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsEdit);
        }
 
        private bool RenameSearchGroupCanExecute(FeatureSearchGroup searchGroup)
        {
            bool result = (searchGroup != null && searchGroup.CanNameChange);

            return result;
        }

        private void RenameSearchGroupExecuted(object parameter)
        {
            var parentGroup = parameter as FeatureSearchGroup;
            if (parentGroup != null)
            {
                //TODO: implement
            }
        }

        private void GetEntityFromService(DTO.ModuleFeatureStepActionType actionType)
        {
            SelectedFeature.RestoreSelectedSearchGroup();
            int searchGroupId = _selectedEntity.OwningSearchGroupId;
            int entityId = _selectedEntity.Id;

            //TODO: update client services method to take actionType parameter.
            //var response = base.AnalyticServices.LoadAnalytic(SelectedAnalytic, entityId, searchGroupId, (int)actionType);
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_selectedGroupChangedSubscription != null)
                    {
                        _selectedGroupChangedSubscription.Dispose();
                        _selectedGroupChangedSubscription = null;
                    }
                    if (_selectedEntityChangedSubscription != null)
                    {
                        _selectedEntityChangedSubscription.Dispose();
                        _selectedEntityChangedSubscription = null;
                    }
                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }

}

