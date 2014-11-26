using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Display = APLPX.UI.WPF.DisplayEntities;


namespace APLPX.UI.WPF.ViewModels.Analytic
{
    public class AnalyticResultsViewModel : ViewModelBase
    {
        Random rnd = new Random();
        public AnalyticResultsViewModel(Display.Analytic entity)
        {
            //SelectedAnalytic = (Domain.Analytic)entity;
            var results = from d in entity.Drivers
                          from r in d.Results
                          select new Display.AnalyticResult
                          {
                              DriverName = d.Name,
                              Group = r.Group,
                              MinValue = r.MinValue,
                              MaxValue = r.MaxValue,
                              SalesValue = r.SalesValue,
                              SkuCount = rnd.Next(1000000)
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
