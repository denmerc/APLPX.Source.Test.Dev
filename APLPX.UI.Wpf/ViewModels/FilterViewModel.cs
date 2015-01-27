using System;
using System.Collections.Generic;

using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.ViewModels
{
    /// <summary>
    /// View model for Filter-related views.
    /// </summary>
    public class FilterViewModel : ViewModelBase
    {
        private IFilterContainer _entity;

        #region Constructor and Initialization

        public FilterViewModel(IFilterContainer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Entity = entity;
         
            if (entity.SelectedFilterGroup == null && entity.FilterGroups.Count > 0)
            {
                entity.SelectedFilterGroup = entity.FilterGroups[0];
            }

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var canExecute = this.WhenAnyValue(vm => vm.Entity.SelectedFilterGroup, (group) => SelectAllFiltersCanExecute(group));
            SelectAllFiltersCommand = ReactiveCommand.Create(canExecute);

            this.WhenAnyObservable(vm => vm.SelectAllFiltersCommand).Subscribe(item => SelectAllFiltersCommandExecuted(item));
        }

        #endregion

        #region Properties

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

        #endregion

        #region Command Handlers

        private bool SelectAllFiltersCanExecute(FilterGroup filterGroup)
        {
            bool result = (filterGroup != null);

            return result;
        }

        private void SelectAllFiltersCommandExecuted(object parameter)
        {
            bool isSelected = Convert.ToBoolean(parameter);

            foreach (Filter filter in Entity.SelectedFilterGroup.Filters)
            {
                filter.IsSelected = isSelected;
            }
        }

        #endregion

    }
}
