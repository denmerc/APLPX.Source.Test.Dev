using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;


namespace APLPX.UI.WPF.ViewModels
{
    //Default SubModule for plannning
    public class SearchViewModel : ViewModelBase
    {
        public SearchViewModel(ModuleFeature feature)
        {
            SelectedFeature = feature;
            if (feature.SelectedStep == null)
            {
                feature.SelectedStep = feature.DefaultLandingStep;
            }

            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            var selectedSearchGroupChanged = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedSearchGroup);
            selectedSearchGroupChanged.Subscribe(m => this.RaisePropertyChanged("IsSearchFilterSelected"));
            
            var selectedEntityChanged = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedEntity);
            selectedEntityChanged.Subscribe(m => this.RaisePropertyChanged("IsDetailDisplayed"));
        }

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
                    result = (SelectedFeature.SelectedEntity != null);
                }

                return result;
            }
        }

        #endregion

    }










}

