using System;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels
{
    /// <summary>
    /// View model for searching and filtering entitiels.
    /// </summary>
    public class SearchViewModel : ViewModelBase
    {
        #region Constructor and Initialization

        public SearchViewModel(ModuleFeature feature)
        {
            SelectedFeature = feature;
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            var selectedSearchGroupChanged = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedSearchGroup);
            selectedSearchGroupChanged.Subscribe(m => OnSelectedSearchGroupChanged(m));

            var selectedEntityChanged = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedEntity);
            selectedEntityChanged.Subscribe(m => OnSelectedEntityChanged(m));
        }

        #endregion

        #region Properties

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

        #endregion
    }

}

