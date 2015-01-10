using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayKeyValueDriver : DisplayEntityBase
    {
        #region Private Fields

        private int _valueDriverId;
        private List<PricingEverydayKeyValueDriverGroup> _groups;
        private PricingEverydayKeyValueDriverGroup _selectedGroup;

        #endregion

        #region Constructors

        public PricingEverydayKeyValueDriver()
        {
            Groups = new List<PricingEverydayKeyValueDriverGroup>();
        }

        #endregion

        #region Properties

        public int ValueDriverId
        {
            get { return _valueDriverId; }
            set { this.RaiseAndSetIfChanged(ref _valueDriverId, value); }
        }

        public List<PricingEverydayKeyValueDriverGroup> Groups
        {
            get { return _groups; }
            set { this.RaiseAndSetIfChanged(ref _groups, value); }
        }

        public PricingEverydayKeyValueDriverGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set { this.RaiseAndSetIfChanged(ref _selectedGroup, value); }
        }

        #endregion

    }
}
