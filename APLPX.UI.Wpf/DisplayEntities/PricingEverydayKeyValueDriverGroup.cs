using System;
using System.Collections.Generic;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayKeyValueDriverGroup : DisplayEntityBase
    {
        #region Private Fields

        private int _valueDriverGroupId;
        private short _valueDriverGroupValue;
        private List<PriceMarkupRule> _markupRules;
        private List<PriceOptimizationRule> _optimizationRules;

        private bool _areOptimizationsApplied;

        #endregion

        #region Constructors

        public PricingEverydayKeyValueDriverGroup()
        {
            MarkupRules = new List<PriceMarkupRule>();
            OptimizationRules = new List<PriceOptimizationRule>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets/sets the Id of the <see cref="ValueDriverGroup"/> related to this object.
        /// </summary>
        public int ValueDriverGroupId
        {
            get { return _valueDriverGroupId; }
            set { this.RaiseAndSetIfChanged(ref _valueDriverGroupId, value); }
        }

        /// <summary>
        /// Gets/sets the Value of the <see cref="ValueDriverGroup"/> related to this object.
        /// </summary>
        public short ValueDriverGroupValue
        {
            get { return _valueDriverGroupValue; }
            set { this.RaiseAndSetIfChanged(ref _valueDriverGroupValue, value); }
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

        /// <summary>
        /// Gets a value indicating whether optimizations have bee applied to this group.
        /// </summary>
        public bool AreOptimizationsApplied
        {
            get { return _areOptimizationsApplied; }
            set { this.RaiseAndSetIfChanged(ref _areOptimizationsApplied, value); }
        }

        #endregion

    }
}
