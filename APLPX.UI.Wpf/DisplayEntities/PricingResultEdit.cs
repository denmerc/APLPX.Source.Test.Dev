using System;
using APLPX.Client.Entity;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingResultEdit : DisplayEntityBase
    {
        #region Private Fields

        private string _name;
        private string _title;
        private PricingResultsEditType _type;

        #endregion

        #region Constructors

        public PricingResultEdit()
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

        public PricingResultsEditType Type
        {
            get { return _type; }
            set { this.RaiseAndSetIfChanged(ref _type, value); }
        }

        #endregion

    }
}
