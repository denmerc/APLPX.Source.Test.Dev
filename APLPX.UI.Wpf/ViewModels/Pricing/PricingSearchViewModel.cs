using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive.Linq;
using APLPX.UI.WPF.ViewModels.Events;

using System.Windows;
using APLPX.Server.Data;

using APLPX.UI.WPF.DisplayEntities;



namespace APLPX.UI.WPF.ViewModels
{
    public class PricingSearchViewModel: ViewModelBase
    {
        public PricingSearchViewModel(ModuleFeature selectedFeature)
        {
            SelectedFeature = selectedFeature;
        }

        private ModuleFeature _selectedFeature;
        public ModuleFeature SelectedFeature { get { return _selectedFeature; } set { this.RaiseAndSetIfChanged(ref _selectedFeature, value); } }


    }
}
