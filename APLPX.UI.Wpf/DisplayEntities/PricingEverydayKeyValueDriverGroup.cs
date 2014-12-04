using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayKeyValueDriverGroup : DisplayEntityBase
    {
        #region Private Fields

        private int _valueDriverGroupId;
        private List<PriceMarkupRule> _markupRules;
        private List<PriceOptimizationRule> _optimizationRules;


        #endregion

        #region Constructors

        public PricingEverydayKeyValueDriverGroup()
        {
            MarkupRules = new List<PriceMarkupRule>();
            OptimizationRules = new List<PriceOptimizationRule>();
        }

        #endregion

        #region Properties

        public int ValueDriverGroupId
        {
            get { return _valueDriverGroupId; }
            set { this.RaiseAndSetIfChanged(ref _valueDriverGroupId, value); }
        }

        public List<PriceMarkupRule> MarkupRules
        {
            get { return _markupRules; }
            set { this.RaiseAndSetIfChanged(ref _markupRules, value); }
        }

        public List<PriceOptimizationRule> OptimizationRules
        {
            get { return _optimizationRules; }
            set { this.RaiseAndSetIfChanged(ref _optimizationRules, value); }
        }

        #endregion

    }
}
