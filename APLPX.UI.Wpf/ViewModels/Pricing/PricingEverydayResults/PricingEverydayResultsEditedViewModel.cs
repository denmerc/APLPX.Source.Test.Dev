﻿using System;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;
using System.Collections.Generic;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class PricingEverydayResultsEditedViewModel : ViewModelBase
    {
        private ISearchableEntity _priceRoutine;

        public PricingEverydayResultsEditedViewModel(ISearchableEntity entity)
        {
            _priceRoutine = entity;

        }

        public ISearchableEntity PriceRoutine
        {
            get { return _priceRoutine; }
            private set
            {
                _priceRoutine = value;
            }
        }

    }
}
