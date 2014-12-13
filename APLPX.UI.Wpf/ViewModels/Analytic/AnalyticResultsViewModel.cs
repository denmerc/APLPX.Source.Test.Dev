using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;


namespace APLPX.UI.WPF.ViewModels.Analytic
{
    public class AnalyticResultsViewModel : ViewModelBase
    {

        public AnalyticResultsViewModel(Display.Analytic entity)
        {
            //SelectedAnalytic = (Domain.Analytic)entity;
            var results = from d in entity.ValueDrivers
                          from r in d.Results
                          select new Display.AnalyticResult
                          {
                              Id = r.Id,
                              DriverName = d.Name,
                              Value = r.Value,
                              MinValue = r.MinValue,
                              MaxValue = r.MaxValue,
                              MinOutlier = r.MinOutlier,
                              MaxOutlier = r.MaxOutlier,
                              SalesValue = r.SalesValue,
                              SkuCount = r.SkuCount
                          };

            Results = results.ToList();

        }

        private List<Display.AnalyticResult> _results;
        public List<Display.AnalyticResult> Results
        {
            get { return _results; }
            private set { this.RaiseAndSetIfChanged(ref _results, value); }
        }

    }
}
