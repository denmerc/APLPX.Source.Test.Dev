using System;
using System.Collections.Generic;

using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.ViewModels
{

    public class FilterViewModel : ViewModelBase
    {
        private ISearchableEntity _entity;
        private List<Display.FilterGroup> _filterGroups;

        #region Constructor and Initialization

        public FilterViewModel(ISearchableEntity entity, List<Display.FilterGroup> filterGroups)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Entity = entity;
            FilterGroups = filterGroups;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var canExecute = this.WhenAnyValue(vm => vm.SelectedFilterGroup, (group) => SelectAllFiltersCanExecute(group));
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

        public ISearchableEntity Entity
        {
            get { return _entity; }
            set { this.RaiseAndSetIfChanged(ref _entity, value); }
        }

        public List<Display.FilterGroup> FilterGroups
        {
            get { return _filterGroups; }
            private set { this.RaiseAndSetIfChanged(ref _filterGroups, value); }
        }

        /// <summary>
        /// Private convenience property. Not used for data binding.
        /// </summary>
        private Display.FilterGroup SelectedFilterGroup
        {
            get
            {
                Display.FilterGroup result = null;

                Display.Analytic analytic = Entity as Display.Analytic;
                if (analytic != null)
                {
                    result = analytic.SelectedFilterGroup;
                }
                else
                {
                    Display.PricingEveryday everyday = Entity as Display.PricingEveryday;
                    if (everyday != null)
                    {
                        result = everyday.SelectedFilterGroup;
                    }
                }

                return result;
            }
        }

        #endregion

        #region Command Handlers

        private bool SelectAllFiltersCanExecute(Display.FilterGroup filterGroup)
        {
            bool result = (filterGroup != null);

            return result;
        }

        private void SelectAllFiltersCommandExecuted(object parameter)
        {
            bool isSelected = Convert.ToBoolean(parameter);

            foreach (Display.Filter filter in SelectedFilterGroup.Filters)
            {
                filter.IsSelected = isSelected;
            }
        }

        #endregion

    }
}
