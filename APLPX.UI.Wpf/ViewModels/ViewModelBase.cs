using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain = APLPX.Client.Display;

namespace APLPX.UI.WPF.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {


        private Domain.Analytic _SelectedAnalytic;
        public Domain.Analytic SelectedAnalytic { get { return _SelectedAnalytic; } set { this.RaiseAndSetIfChanged(ref _SelectedAnalytic, value); } }
        private Domain.PriceRoutine _SelectedPriceRoutine;
        public Domain.PriceRoutine SelectedPriceRoutine { get { return _SelectedPriceRoutine; } set { this.RaiseAndSetIfChanged(ref _SelectedPriceRoutine, value); } }
        public void LoadAnalytic(object entity)
        {
            SelectedAnalytic = (Domain.Analytic)entity;
        }
        public void LoadPriceRoutine(object entity)
        {
            SelectedPriceRoutine = (Domain.PriceRoutine)entity;
        }
    }
}
