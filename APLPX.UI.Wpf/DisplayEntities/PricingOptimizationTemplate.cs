﻿using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingOptimizationTemplate : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private string _name;
        private string _description;
        private short _sort;
        private List<PriceOptimizationRule> _rules;

        #endregion

        #region Constructors

        public PricingOptimizationTemplate()
        {
            Rules = new List<PriceOptimizationRule>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Description
        {
            get { return _description; }
            set { this.RaiseAndSetIfChanged(ref _description, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        public List<PriceOptimizationRule> Rules
        {
            get { return _rules; }
            set { this.RaiseAndSetIfChanged(ref _rules, value); }
        }

        #endregion

    }
}
