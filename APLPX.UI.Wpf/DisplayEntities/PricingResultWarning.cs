﻿using System;
using APLPX.Entity;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingResultWarning : DisplayEntityBase
    {
        #region Private Fields

        private string _name;
        private string _title;
        private PricingResultsWarningType _type;

        #endregion

        #region Constructors

        public PricingResultWarning()
        {
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Title
        {
            get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }

        public PricingResultsWarningType Type
        {
            get { return _type; }
            set { this.RaiseAndSetIfChanged(ref _type, value); }
        }

        #endregion

    }
}
