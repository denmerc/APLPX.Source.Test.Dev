using System;
using System.Collections.ObjectModel;
using System.Linq;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels
{
    /// <summary>
    /// View model for Filter-related views.
    /// </summary>
    public class FilterViewModel : ViewModelBase
    {
        private IFilterContainer _entity;
        private IDisposable _selectAllFilterSubscription;
        private bool _isDisposed;

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

            _selectAllFilterSubscription = this.WhenAnyObservable(vm => vm.SelectAllFiltersCommand).Subscribe(item => SelectAllFiltersCommandExecuted(item));
            Entity.FilterGroups.ItemChanged.Subscribe(g => OnFilterGroupChanged(g));
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

        public ObservableCollection<Error> ValidationResults
        {
            get
            {
                var errors = Entity.FilterGroups.SelectMany(grp => grp.ValidationResults);

                var result = new ObservableCollection<Error>(errors);
                return result;
            }
        }

        #endregion   

        #region Command and Event Handlers

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

        private void OnFilterGroupChanged(IReactivePropertyChangedEventArgs<FilterGroup> g)
        {
            this.RaisePropertyChanged("ValidationResults");
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
                        _selectAllFilterSubscription = null;
                    }
                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
