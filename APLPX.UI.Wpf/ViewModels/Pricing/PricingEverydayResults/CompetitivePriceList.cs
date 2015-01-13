using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class CompetitivePriceList
    {
        private string _name;
        private string _currentPrice;
        private string _currentMarkup;
        private string _optimizedPrice;
        private string _competitorPrice;
        private string _competitorPriceDifference;
        private string _asOf;
        private string _editType;
        private string _finalPrice;

        public CompetitivePriceList(string name, string currentPrice, string currentMarkup, string optimizedPrice, string competitorPrice, string competitorPriceDifference, string asOf, string editType, string finalPrice)
        {
            Name = name;
            CurrentPrice = currentPrice;
            CurrentMarkup = currentMarkup;
            OptimizedPrice = optimizedPrice;
            CompetitorPrice = competitorPrice;
            CompetitorPriceDifference = competitorPriceDifference;
            AsOf = asOf;
            EditType = editType;
            FinalPrice = finalPrice;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string CurrentPrice
        {
            get { return _currentPrice; }
            set { _currentPrice = value; }
        }

        public string CurrentMarkup
        {
            get { return _currentMarkup; }
            set { _currentMarkup = value; }
        }

        public string OptimizedPrice
        {
            get { return _optimizedPrice; }
            set { _optimizedPrice = value; }
        }

        public string CompetitorPrice
        {
            get { return _competitorPrice; }
            set { _competitorPrice = value; }
        }

        public string CompetitorPriceDifference
        {
            get { return _competitorPriceDifference; }
            set { _competitorPriceDifference = value; }
        }

        public string AsOf
        {
            get { return _asOf; }
            set { _asOf = value; }
        }

        public string EditType
        {
            get { return _editType; }
            set { _editType = value; }
        }

        public string FinalPrice
        {
            get { return _finalPrice; }
            set { _finalPrice = value; }
        }
    }
}
