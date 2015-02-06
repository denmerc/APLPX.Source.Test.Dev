using System;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels
{
    /// <summary>
    /// View model for searching and filtering entitiels.
    /// </summary>
    public class SearchViewModel : ViewModelBase
    {
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

            CreateNewEntityCommand = ReactiveCommand.Create();
            this.WhenAnyObservable(vm => vm.CreateNewEntityCommand).Subscribe(val => CreateNewEntityExecuted(val));
        }

        private void InitializeEventHandlers()
        {
            var selectedSearchGroupChanged = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedSearchGroup);
            _selectedGroupChangedSubscription = selectedSearchGroupChanged.Subscribe(m => OnSelectedSearchGroupChanged(m));

            var selectedEntityChanged = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedEntity);
            _selectedEntityChangedSubscription = selectedEntityChanged.Subscribe(m => OnSelectedEntityChanged(m));
        }

        #endregion

        #region Properties


        public ReactiveCommand<object> CreateNewEntityCommand
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
                    result = (SelectedFeature.SelectedSearchGroup != null && SelectedFeature.FilteredSearchableEntities.Count > 0);
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
            this.RaisePropertyChanged("IsDetailDisplayed");
        }

        private void OnSelectedSearchGroupChanged(FeatureSearchGroup searchGroup)
        {
            this.RaisePropertyChanged("IsSearchFilterSelected");
        }

        private void CreateNewEntityExecuted(object parameter)
        {
            var parentGroup = parameter as FeatureSearchGroup;
            if (parentGroup != null)
            {
                EventAggregator notifier = ((EventAggregator)App.Current.Resources["EventManager"]);
                notifier.Publish(parentGroup);
            }
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

