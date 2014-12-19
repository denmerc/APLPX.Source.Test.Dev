using System;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;
using System.Collections.Generic;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class PricingEverydayResultsViewModel : ViewModelBase
    {
        private ISearchableEntity _priceRoutine;

        private List<PricingView> _views = new List<PricingView>();

        public PricingEverydayResultsViewModel(ISearchableEntity entity)
        {
            _priceRoutine = entity;

            _views.Add(new PricingView("Summary"));
            _views.Add(new PricingView("Warnings"));
            _views.Add(new PricingView("Price Delta"));
            _views.Add(new PricingView("Mark-Up Delta"));
            _views.Add(new PricingView("Price List"));
            _views.Add(new PricingView("Competition"));
            _views.Add(new PricingView("Value Driver Groups"));
            _views.Add(new PricingView("Edited"));
            _views.Add(new PricingView("Excluded"));

        }

        public ISearchableEntity PriceRoutine
        {
            get { return _priceRoutine; }
            private set
            {
                _priceRoutine = value;
            }
        }

        public List<PricingView> Views
        {
            get { return _views; }
        }
    }
}
