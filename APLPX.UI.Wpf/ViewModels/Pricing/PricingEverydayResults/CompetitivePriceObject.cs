using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class CompetitivePriceObject
    {
        private string _sku;
        private string _description;
        private List<CompetitivePriceList> _priceLists;


        public CompetitivePriceObject(string sku, string description, List<CompetitivePriceList> priceLists)
        {
            Sku = sku;
            Description = description;
            PriceLists = priceLists;
        }

        public string Sku
        {
            get { return _sku; }
            set { _sku = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public List<CompetitivePriceList> PriceLists
        {
            get { return _priceLists; }
            set { _priceLists = value; }
        }
    }
}
