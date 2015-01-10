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
        private PricingValueDriverGroup _selectedGroup;

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

        public PricingValueDriverGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set { this.RaiseAndSetIfChanged(ref _selectedGroup, value); }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            object[] values = { GetType().Name, Id, Name, Key, IsSelected, IsKey };
            string result = String.Format("{0}:Id={1};Name={2};Key={3};IsSelected={4};IsKey={5}", values);

            return result;
        }

        #endregion
    }
}
