﻿using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayPriceList : PriceList
    {
        private bool _isKey;

        public bool IsKey
        {
            get { return _isKey; }
            set { this.RaiseAndSetIfChanged(ref _isKey, value); }
        }

    }
}