using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;
using DTO = APLPX.Client.Entity;


namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class PricingResultsViewModel : ViewModelBase
    {
        public PricingResultsViewModel(List<DTO.PricingEverydayResult> results)
        {
            Results = results;
        }


        private List<DTO.PricingEverydayResult> _results;
        public List<DTO.PricingEverydayResult> Results { 
            get { return _results; } set { this.RaiseAndSetIfChanged(ref _results, value); } }

    }
}
