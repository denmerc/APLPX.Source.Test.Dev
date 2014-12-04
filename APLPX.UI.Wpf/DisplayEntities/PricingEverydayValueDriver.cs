using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayValueDriver : ValueDriver
    {
        #region Private Fields

        private bool _isKey;
        private List<PricingValueDriverGroup> _groups;

        #endregion

        #region Constructors

        public PricingEverydayValueDriver()
        {
            Groups = new List<PricingValueDriverGroup>();
        }

        #endregion

        #region Properties

        public bool IsKey
        {
            get { return _isKey; }
            set { this.RaiseAndSetIfChanged(ref _isKey, value); }
        }

        public List<PricingValueDriverGroup> Groups
        {
            get { return _groups; }
            set { this.RaiseAndSetIfChanged(ref _groups, value); }
        }

        #endregion

    }
}
