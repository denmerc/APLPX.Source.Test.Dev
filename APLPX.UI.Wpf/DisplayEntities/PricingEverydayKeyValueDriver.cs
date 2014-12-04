using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayKeyValueDriver : DisplayEntityBase
    {
        #region Private Fields

        private int _valueDriverId;
        private List<PricingEverydayKeyValueDriverGroup> _groups;

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

        #endregion

    }
}
