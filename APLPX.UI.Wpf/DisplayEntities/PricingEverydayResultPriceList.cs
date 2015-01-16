using System;
using ReactiveUI;
using DTO = APLPX.Entity;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayResultPriceList : PricingEverydayPriceList
    {
        #region Private Fields

        private int _resultId;
        private decimal _currentPrice;
        private decimal _newPrice;
        private int _currentMarkupPercent;
        private int _newMarkupPercent;
        private decimal _keyValueChange;
        private decimal _influenceValueChange;
        private decimal _priceChange;
        private PricingResultEdit _priceEdit;
        private PricingResultWarning _priceWarning;

        #endregion

        #region Constructors

        public PricingEverydayResultPriceList()
        {
            PriceEdit = new PricingResultEdit();
            PriceWarning = new PricingResultWarning();
        }

        #endregion

        #region Properties

        public int ResultId
        {
            get { return _resultId; }
            set { this.RaiseAndSetIfChanged(ref _resultId, value); }
        }

        public decimal CurrentPrice
        {
            get { return _currentPrice; }
            set { this.RaiseAndSetIfChanged(ref _currentPrice, value); }
        }

        public decimal NewPrice
        {
            get { return _newPrice; }
            set { this.RaiseAndSetIfChanged(ref _newPrice, value); }
        }

        public int CurrentMarkupPercent
        {
            get { return _currentMarkupPercent; }
            set { this.RaiseAndSetIfChanged(ref _currentMarkupPercent, value); }
        }

        public int NewMarkupPercent
        {
            get { return _newMarkupPercent; }
            set { this.RaiseAndSetIfChanged(ref _newMarkupPercent, value); }
        }

        public decimal KeyValueChange
        {
            get { return _keyValueChange; }
            set { this.RaiseAndSetIfChanged(ref _keyValueChange, value); }
        }

        public decimal InfluenceValueChange
        {
            get { return _influenceValueChange; }
            set { this.RaiseAndSetIfChanged(ref _influenceValueChange, value); }
        }

        public decimal PriceChange
        {
            get { return _priceChange; }
            set { this.RaiseAndSetIfChanged(ref _priceChange, value); }
        }

        public PricingResultEdit PriceEdit
        {
            get { return _priceEdit; }
            set { this.RaiseAndSetIfChanged(ref _priceEdit, value); }
        }

        public PricingResultWarning PriceWarning
        {
            get { return _priceWarning; }
            set { this.RaiseAndSetIfChanged(ref _priceWarning, value); }
        }

        #endregion
    }
}
