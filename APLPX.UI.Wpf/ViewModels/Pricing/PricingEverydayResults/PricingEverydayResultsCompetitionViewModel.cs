using System;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class PricingEverydayResultsCompetitionViewModel : ViewModelBase
    {

        private ObservableCollection<CompetitivePriceObject> competiveList = new ObservableCollection<CompetitivePriceObject>();

        public PricingEverydayResultsCompetitionViewModel()
        {

            List<CompetitivePriceList> testPriceList1 = new List<CompetitivePriceList>();
            CompetitivePriceList priceList1 = new CompetitivePriceList("Retail", "12.15", "25", "16.17", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            CompetitivePriceList priceList2 = new CompetitivePriceList("Peak Auto", string.Empty, string.Empty, string.Empty, "16.99", "0.82", "8/31/2014", "Include", string.Empty);
            CompetitivePriceList priceList3 = new CompetitivePriceList("Seabourne Racing Group", string.Empty, string.Empty, string.Empty, "15.99", "(0.18)", "8/31/2014", "Include", string.Empty);
            CompetitivePriceList priceList4 = new CompetitivePriceList("OC Restorations", string.Empty, string.Empty, string.Empty, "14.25", "(1.92)", "8/31/2014", "Include", string.Empty);
            CompetitivePriceList priceList5 = new CompetitivePriceList("Market", string.Empty, string.Empty, string.Empty, "15.74", "(0.43)", string.Empty, string.Empty, string.Empty);
            testPriceList1.Clear();
            testPriceList1.Add(priceList1);
            testPriceList1.Add(priceList2);
            testPriceList1.Add(priceList3);
            testPriceList1.Add(priceList4);
            testPriceList1.Add(priceList5);
            CompetitivePriceObject testObject1 = new CompetitivePriceObject("A000C678", "Cowl Induction Air Cleaner", testPriceList1);

            List<CompetitivePriceList> testPriceList2 = new List<CompetitivePriceList>();
            priceList1 = new CompetitivePriceList("Retail", "12.15", "25", "15.64", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            priceList2 = new CompetitivePriceList("Peak Auto", string.Empty, string.Empty, string.Empty, "16.49", "0.85", "8/31/2014", "Include", string.Empty);
            priceList3 = new CompetitivePriceList("Seabourne Racing Group", string.Empty, string.Empty, string.Empty, "15.99", "0.35", "8/31/2014", "Include", string.Empty);
            priceList4 = new CompetitivePriceList("OC Restorations", string.Empty, string.Empty, string.Empty, "14.29", "(1.35)", "8/31/2014", "Include", string.Empty);
            priceList5 = new CompetitivePriceList("Market", string.Empty, string.Empty, string.Empty, "15.59", "(0.05)", string.Empty, string.Empty, string.Empty);
            testPriceList2.Clear();
            testPriceList2.Add(priceList1);
            testPriceList2.Add(priceList2);
            testPriceList2.Add(priceList3);
            testPriceList2.Add(priceList4);
            testPriceList2.Add(priceList5);
            CompetitivePriceObject testObject2 = new CompetitivePriceObject("A000C679", "Wiring Harness I", testPriceList2);

            List<CompetitivePriceList> testPriceList3 = new List<CompetitivePriceList>();
            priceList1 = new CompetitivePriceList("Retail", "12.15", "25", "15.36", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            priceList2 = new CompetitivePriceList("Peak Auto", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Exclude", string.Empty);
            priceList3 = new CompetitivePriceList("Seabourne Racing Group", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Exclude", string.Empty);
            priceList4 = new CompetitivePriceList("OC Restorations", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Exclude", string.Empty);
            priceList5 = new CompetitivePriceList("Market", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            testPriceList3.Clear();
            testPriceList3.Add(priceList1);
            testPriceList3.Add(priceList2);
            testPriceList3.Add(priceList3);
            testPriceList3.Add(priceList4);
            testPriceList3.Add(priceList5);
            CompetitivePriceObject testObject3 = new CompetitivePriceObject("A000C680", "Wiring Harness II", testPriceList3);


            competiveList.Add(testObject1);
            competiveList.Add(testObject2);
            competiveList.Add(testObject3);

            //_priceRoutine = entity;

        }


        public ObservableCollection<CompetitivePriceObject> CompetiveList
        {
            get { return competiveList; }
            private set
            {
                competiveList = value;
            }
        }

        /*
        public ISearchableEntity CompetiveList
        {
            get { return _priceRoutine; }
            private set
            {
                _priceRoutine = value;
            }
        }
        */
    }
}
