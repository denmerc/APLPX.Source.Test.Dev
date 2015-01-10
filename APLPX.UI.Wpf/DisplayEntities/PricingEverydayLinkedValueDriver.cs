using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayLinkedValueDriver : DisplayEntityBase
    {
        #region Private Fields

        private int _valueDriverId;
        private string _name;
        private List<PricingEverydayLinkedValueDriverGroup> _groups;        

        #endregion

        #region Constructors

        public PricingEverydayLinkedValueDriver()
        {
            Groups = new List<PricingEverydayLinkedValueDriverGroup>();
        }

        #endregion

        #region Properties

        public int ValueDriverId
        {
            get { return _valueDriverId; }
            set { this.RaiseAndSetIfChanged(ref _valueDriverId, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public List<PricingEverydayLinkedValueDriverGroup> Groups
        {
            get { return _groups; }
            set { this.RaiseAndSetIfChanged(ref _groups, value); }
        }

        #endregion

    }
}
