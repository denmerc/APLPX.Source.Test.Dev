using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Display entity for an AnalyticValueDriverGroup. Acts as a container for driver group configuration (inputs) and its calculated results (output).
    /// </summary>
    public class AnalyticValueDriverGroup : ValueDriverGroup
    {
        private AnalyticResult _results;

        public AnalyticValueDriverGroup()
        {
            Results = new AnalyticResult();
        }

        /// <summary>
        /// Exposes the results corresponding to this value driver group.
        /// </summary>
        public AnalyticResult Results
        {
            get { return _results; }
            set { this.RaiseAndSetIfChanged(ref _results, value); }
        }
    }
}
