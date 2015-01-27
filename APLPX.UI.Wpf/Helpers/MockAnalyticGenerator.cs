using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;

using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    /// Generates sample display entities for Analytics.
    /// </summary>
    public static class MockAnalyticGenerator
    {
        #region Private Fields

        private static string[] _analyticNames = { "Admin - Everyday - All Filters - Movement Only", "Admin - Everyday - Movement & Markup", "Admin - Everyday - Movement & Days On Hand", 
                                                   "Analyst - Promo - Movement Markup Days On Hand ", "Analyst - Promo - Movement with Outliers", "Admin - Everyday - Movement with Manual Groups", 
                                                   "Analyst - Promo - Movement & Markup w/Man Grps ", "Admin - Movement, Markup, & DOH w/Manual Groups", 
                                                   "Admin - Movement, MarkUp, & DOH w/Custom - 1 prod Description", "Analyst - Promo - All Drivers One-to-many Analytic", 
                                                   "Approver - Everyday - All Drivers One-to-One Analysis", "Approver - Everyday - Movement & Markup w/small Filters", 
                                                   "Approver - Everyday - Movement & DOH w/small Filters", "Approver - Everyday - Movement w/Pontiac Filters", 
                                                   "Approver - Everyday - Move. w/Caddy filt & 15 grps" };

        private static string[] _ownerNames = { "Admin User Active", "Admin User Active", "Admin User Active", "Analyst User Active", "Analyst User Active", 
                                                "Admin User Active", "Analyst User Active", "Admin User Active", "Admin User Active", "Analyst User Active", 
                                                "Approver User Active", "Approver User Active",  "Approver User Active", "Approver User Active", "Approver User Active" };

        #endregion

        public static Analytic GetSampleAnalytic(int id)
        {
            var result = new Analytic();

            result.Id = id + 1;
            string name = _analyticNames[id];
            result.Identity.Name = name;
            result.Identity.Description = String.Format("Sample description for {0}", name);
            result.Identity.Notes = String.Format("Here are are some sample notes that were entered for this item (\"{0}\").", name);
            result.Identity.IsActive = true;
            result.Identity.Shared = false;

            result.Identity.Created = DateTime.Now.AddDays(-10);
            result.Identity.Edited = DateTime.Now.AddDays(-2);
            result.Identity.Refreshed = DateTime.Now;

            result.Identity.Owner = _ownerNames[id];
            result.Identity.Author = _ownerNames[id];
            result.Identity.Editor = _ownerNames[id];

            result.FilterGroups = MockFilterGenerator.GetFilterGroupsComplete();
            result.PriceListGroups = GetAnalyticPriceListGroups();
            result.ValueDrivers = new ReactiveList<AnalyticValueDriver>(GetAnalyticDrivers());

            //Default for display purposes.
            result.SelectedFilterGroup = result.FilterGroups.FirstOrDefault();

            return result;
        }

        public static List<DisplayEntities.Analytic> GetSampleAnalytics()
        {
            var list = new List<DisplayEntities.Analytic>();

            Analytic entity = new Analytic { Id = 1, SearchGroupKey = "FoldersAnalytics76" };
            entity.Identity = new AnalyticIdentity { Name = "Admin - Everyday - All Filters - Movement Only", Description = "Admin - Everyday - All Filters - Movement Only" };
            list.Add(entity);

            entity = new Analytic { Id = 2, SearchGroupKey = "FoldersAnalytics76" };
            entity.Identity = new AnalyticIdentity { Name = "Admin - Everyday - Movement & Markup", Description = "Admin - Everyday - Movement &Markup" };
            list.Add(entity);

            entity = new Analytic { Id = 3, SearchGroupKey = "FoldersAnalytics76" };
            entity.Identity = new AnalyticIdentity { Name = "Admin - Everyday - Movement & Days On Hand", Description = "Admin - Everyday - Movement &Days On Hand" };
            list.Add(entity);

            entity = new Analytic { Id = 6, SearchGroupKey = "FoldersAnalytics75" };
            entity.Identity = new AnalyticIdentity { Name = "Admin - Everyday - Movement with Manual Groups", Description = "Admin - Everyday - Movement with Manual Groups" };
            list.Add(entity);

            entity = new Analytic { Id = 8, SearchGroupKey = "FoldersAnalytics74" };
            entity.Identity = new AnalyticIdentity { Name = "Admin - Movement, Markup, & DOH w/Manual Groups", Description = "Admin - Movement, Markup, & DOH with Manual Groups" };
            list.Add(entity);

            entity = new Analytic { Id = 9, SearchGroupKey = "FoldersAnalytics73" };
            entity.Identity = new AnalyticIdentity { Name = "Admin - Movement, MarkUp, & DOH w/Custom - 1 prod", Description = "All value drivers with Custom groups - with only 1 intersection " };
            list.Add(entity);

            //Note: the following entities, which already tagged to a user folder, are also tagged to the Recent folder.
            entity = new Analytic { Id = 1, SearchGroupKey = "RecentAnalytics" };
            entity.Identity = new AnalyticIdentity { Name = "Admin - Everyday - All Filters - Movement Only", Description = "Admin - Everyday - All Filters - Movement Only" };
            list.Add(entity);

            entity = new Analytic { Id = 8, SearchGroupKey = "RecentAnalytics" };
            entity.Identity = new AnalyticIdentity { Name = "Admin - Movement, Markup, & DOH w/Manual Groups", Description = "Admin - Movement, Markup, & DOH with Manual Groups" };
            list.Add(entity);

            entity = new Analytic { Id = 9, SearchGroupKey = "RecentAnalytics" };
            entity.Identity = new AnalyticIdentity { Name = "Admin - Movement, MarkUp, & DOH w/Custom - 1 prod", Description = "All value drivers with Custom groups - with only 1 intersection " };
            list.Add(entity);

            foreach (DisplayEntities.Analytic analytic in list)
            {
                analytic.Identity.IsActive = true;
                analytic.Identity.Owner = "Admin User";
                analytic.Identity.Editor = "APL Administrator";
                analytic.Identity.Author = "APL Administrator";
                analytic.Identity.Notes = "Optional notes...";

                analytic.FilterGroups = MockFilterGenerator.GetFilterGroupsComplete();
                analytic.PriceListGroups = GetAnalyticPriceListGroups();
                analytic.ValueDrivers = new ReactiveList<AnalyticValueDriver>(GetAnalyticDrivers());

            }

            return list;
        }

        public static List<FeatureSearchGroup> GetAnalyticSearchGroups()
        {
            string myFolders = "Folders Analytics";

            var list = new List<FeatureSearchGroup>();

            list.Add(new FeatureSearchGroup { SearchGroupKey = "RecentAnalytics", Name = "Recent Analytics", ItemCount = 3, ParentName = "Recent Analytics", Sort = 1 });
            list.Add(new FeatureSearchGroup { SearchGroupKey = "FoldersAnalytics73", Name = "Engine", ItemCount = 1, ParentName = myFolders, Sort = 10 });
            list.Add(new FeatureSearchGroup { SearchGroupKey = "FoldersAnalytics74", Name = "Apparel", ItemCount = 1, ParentName = myFolders, Sort = 20 });
            list.Add(new FeatureSearchGroup { SearchGroupKey = "FoldersAnalytics75", Name = "Nuts-Bolts", ItemCount = 1, ParentName = myFolders, Sort = 30 });
            list.Add(new FeatureSearchGroup { SearchGroupKey = "FoldersAnalytics76", Name = "Interior Parts", ItemCount = 3, ParentName = myFolders, Sort = 40 });
            list.Add(new FeatureSearchGroup { SearchGroupKey = "FoldersAnalytics77", Name = "Sheet Metal Parts", ItemCount = 0, ParentName = myFolders, Sort = 50 });
            list.Add(new FeatureSearchGroup { SearchGroupKey = "FoldersAnalytics78", Name = "Moldings", ItemCount = 0, ParentName = myFolders, Sort = 60 });

            var editableGroups = list.Where(item => item.ParentName.Equals(myFolders));
            foreach (FeatureSearchGroup editableGroup in editableGroups)
            {
                editableGroup.CanNameChange = true;
                editableGroup.CanSearchKeyChange = true;
            }

            return list;
        }

        #region Price Lists

        private static List<PriceList> GetPriceLists(int groupIndex)
        {
            var result = new List<PriceList>();

            result.Add(new PriceList { Id = 1, Key = 1, Code = "0", Name = "Cost", IsSelected = false, Sort = 1 });
            result.Add(new PriceList { Id = 4, Key = 2, Code = "L", Name = "Retail List price", IsSelected = true, Sort = 2 });
            result.Add(new PriceList { Id = 2, Key = 3, Code = "LF", Name = "Retail List price *FUTURE PRICE*", IsSelected = false, Sort = 3 });
            result.Add(new PriceList { Id = 3, Key = 4, Code = "R", Name = "Retail Sale price", IsSelected = false, Sort = 4 });
            result.Add(new PriceList { Id = 5, Key = 5, Code = "RF", Name = "Retail Sale price *FUTURE PRICE*", IsSelected = false, Sort = 5 });
            result.Add(new PriceList { Id = 6, Key = 6, Code = "C", Name = "Dealer Sugg Retail", IsSelected = false, Sort = 6 });
            result.Add(new PriceList { Id = 7, Key = 7, Code = "J", Name = "Jobber - Trim Shop", IsSelected = false, Sort = 7 });

            if (groupIndex == 0)
            {
                result.Add(new PriceList { Id = 8, Key = 8, Code = "D", Name = "Dealer Std Price", IsSelected = false, Sort = 8 });
                result.Add(new PriceList { Id = 9, Key = 9, Code = "1", Name = "Dealer 1", IsSelected = false, Sort = 9 });
                result.Add(new PriceList { Id = 10, Key = 10, Code = "2", Name = "Dealer 2", IsSelected = false, Sort = 10 });
                result.Add(new PriceList { Id = 10, Key = 11, Code = "3", Name = "Dealer 3", IsSelected = false, Sort = 11 });
                result.Add(new PriceList { Id = 12, Key = 12, Code = "4", Name = "Dealer 4", IsSelected = false, Sort = 12 });
            }

            return result;
        }

        private static List<AnalyticPriceListGroup> GetAnalyticPriceListGroups()
        {
            var result = new List<AnalyticPriceListGroup>();
            string[] grouoNames = { "Regular" };

            for (int groupIndex = 0; groupIndex < grouoNames.Length; groupIndex++)
            {
                string name = grouoNames[groupIndex];
                AnalyticPriceListGroup group = new AnalyticPriceListGroup { Name = name, Sort = (short)groupIndex, Title = name + " title" };
                List<PriceList> priceLists = GetPriceLists(groupIndex);
                group.PriceLists = new ReactiveList<PriceList>(priceLists);

                result.Add(group);
            }

            return result;
        }

        #endregion

        #region Value Drivers

        private static List<AnalyticValueDriver> GetAnalyticDrivers()
        {
            var result = new List<AnalyticValueDriver>();

            string[] driverNames = { "Markup", "Movement", "Days On Hand" };

            AnalyticValueDriver driver;
            for (int driverIndex = 0; driverIndex < driverNames.Length; driverIndex++)
            {
                var analyticResults = GetAnalyticResults(driverNames[driverIndex]);

                AnalyticValueDriverGroup group;
                driver = new AnalyticValueDriver { Id = driverIndex + 21, Name = driverNames[driverIndex], Sort = (short)driverIndex, Results = analyticResults };
                //Auto generated
                var mode = new AnalyticValueDriverMode
                {
                    Name = "Auto Generated groups",
                    Key = 29,
                    Sort = 0,
                    IsSelected = true
                };
                int minOutlier = 0;
                for (int groupIndex = 1; groupIndex <= 3; groupIndex++)
                {
                    minOutlier += 1;
                    group = new AnalyticValueDriverGroup { Id = groupIndex, Value = (short)groupIndex, MinOutlier = minOutlier, MaxOutlier = minOutlier + 1, Sort = (short)groupIndex };
                    mode.Groups.Add(group);
                }
                driver.Modes.Add(mode);

                //User defined
                mode = new AnalyticValueDriverMode
                {
                    Name = "User defined groups",
                    Key = 30,
                    Sort = 1,
                    IsSelected = false
                };
                minOutlier = 0;
                for (int groupIndex = 1; groupIndex <= 5; groupIndex++)
                {
                    minOutlier += 1;
                    group = new AnalyticValueDriverGroup { Id = groupIndex, Value = (short)groupIndex, MinOutlier = minOutlier, MaxOutlier = minOutlier + 1, Sort = (short)groupIndex };
                    mode.Groups.Add(group);
                }
                driver.Modes.Add(mode);

                //Simulate mode and driver selections.
                driver.SelectedMode = driver.Modes[driverIndex % 2];
                driver.IsSelected = (driverIndex % 2 == 0);

                result.Add(driver);
            }

            //Add in disabled "teaser" drivers for demo only.
            driver = new AnalyticValueDriver { Id = 97, Key = 37, Name = "Days Lead Time", IsDisplayOnly = true, Sort = 5 };
            result.Add(driver);

            driver = new AnalyticValueDriver { Id = 99, Key = 38, Name = "In Stock Ratio", IsDisplayOnly = true, Sort = 6 };
            result.Add(driver);

            driver = new AnalyticValueDriver { Id = 99, Key = 39, Name = "Trend", IsDisplayOnly = true, Sort = 7 };
            result.Add(driver);

            return result;
        }

        #endregion

        #region Results

        private static List<AnalyticResult> GetAnalyticResults()
        {
            var list = new List<AnalyticResult>();

            list.Add(new AnalyticResult { DriverName = "Markup", MinOutlier = 1645M, MaxOutlier = 19880M, Id = 1, SalesValue = "16020.43", SkuCount = 20 });
            list.Add(new AnalyticResult { DriverName = "Markup", MinOutlier = 1215.26M, MaxOutlier = 1563M, Id = 2, SalesValue = "7574.79", SkuCount = 18 });
            list.Add(new AnalyticResult { DriverName = "Markup", MinOutlier = 802.9M, MaxOutlier = 1181M, Id = 3, SalesValue = "34918", SkuCount = 27 });
            list.Add(new AnalyticResult { DriverName = "Markup", MinOutlier = 331.67M, MaxOutlier = 799M, Id = 4, SalesValue = "67442.4", SkuCount = 42 });
            list.Add(new AnalyticResult { DriverName = "Markup", MinOutlier = -24.95M, MaxOutlier = 289.7M, Id = 5, SalesValue = "16182.75", SkuCount = 19 });
            list.Add(new AnalyticResult { DriverName = "Movement", MinOutlier = 2298M, MaxOutlier = 2298M, Id = 1, SalesValue = "6848", SkuCount = 1 });
            list.Add(new AnalyticResult { DriverName = "Movement", MinOutlier = 674M, MaxOutlier = 674M, Id = 2, SalesValue = "4010.3", SkuCount = 1 });
            list.Add(new AnalyticResult { DriverName = "Movement", MinOutlier = 217M, MaxOutlier = 411M, Id = 3, SalesValue = "17583", SkuCount = 3 });
            list.Add(new AnalyticResult { DriverName = "Movement", MinOutlier = 102M, MaxOutlier = 179M, Id = 4, SalesValue = "35942", SkuCount = 12 });
            list.Add(new AnalyticResult { DriverName = "Movement", MinOutlier = 0M, MaxOutlier = 98M, Id = 5, SalesValue = "77734", SkuCount = 109 });
            list.Add(new AnalyticResult { DriverName = "Days On Hand", MinOutlier = 21M, MaxOutlier = 90541M, Id = 1, SalesValue = "74986.08", SkuCount = 58 });
            list.Add(new AnalyticResult { DriverName = "Days On Hand", MinOutlier = 13M, MaxOutlier = 178M, Id = 4, SalesValue = "1143.12", SkuCount = 1 });
            list.Add(new AnalyticResult { DriverName = "Days On Hand", MinOutlier = 48M, MaxOutlier = 141M, Id = 5, SalesValue = "65989.34", SkuCount = 67 });

            return list;
        }

        private static List<AnalyticResult> GetAnalyticResults(string name)
        {
            var list = new List<AnalyticResult>();
            switch (name)
            {
                case "Markup":
                    list.Add(new AnalyticResult { DriverName = "Markup", MinOutlier = 1645M, MaxOutlier = 19880, Id = 1, SalesValue = "16020.43", SkuCount = 20 });
                    list.Add(new AnalyticResult { DriverName = "Markup", MinOutlier = 1215.26M, MaxOutlier = 1563, Id = 2, SalesValue = "7574.79", SkuCount = 18 });
                    list.Add(new AnalyticResult { DriverName = "Markup", MinOutlier = 802.9M, MaxOutlier = 1181, Id = 3, SalesValue = "34918", SkuCount = 27 });
                    list.Add(new AnalyticResult { DriverName = "Markup", MinOutlier = 331.67M, MaxOutlier = 799, Id = 4, SalesValue = "67442.4", SkuCount = 42 });
                    list.Add(new AnalyticResult { DriverName = "Markup", MinOutlier = -24.95M, MaxOutlier = 289.7M, Id = 5, SalesValue = "16182.75", SkuCount = 19 });
                    break;

                case "Movement":
                    list.Add(new AnalyticResult { DriverName = "Movement", MinOutlier = 2298M, MaxOutlier = 2298M, Id = 1, SalesValue = "6848", SkuCount = 1 });
                    list.Add(new AnalyticResult { DriverName = "Movement", MinOutlier = 674M, MaxOutlier = 674M, Id = 2, SalesValue = "4010.3", SkuCount = 1 });
                    list.Add(new AnalyticResult { DriverName = "Movement", MinOutlier = 217M, MaxOutlier = 411M, Id = 3, SalesValue = "17583", SkuCount = 3 });
                    list.Add(new AnalyticResult { DriverName = "Movement", MinOutlier = 102M, MaxOutlier = 179M, Id = 4, SalesValue = "35942", SkuCount = 12 });
                    list.Add(new AnalyticResult { DriverName = "Movement", MinOutlier = 0M, MaxOutlier = 98M, Id = 5, SalesValue = "77734", SkuCount = 109 });
                    break;

                case "Days On Hand":
                    list.Add(new AnalyticResult { DriverName = "Days On Hand", MinOutlier = 21, MaxOutlier = 90541, Id = 1, SalesValue = "74986.08", SkuCount = 58 });
                    list.Add(new AnalyticResult { DriverName = "Days On Hand", MinOutlier = 13, MaxOutlier = 178, Id = 4, SalesValue = "1143.12", SkuCount = 1 });
                    list.Add(new AnalyticResult { DriverName = "Days On Hand", MinOutlier = 48, MaxOutlier = 141, Id = 5, SalesValue = "65989.34", SkuCount = 67 });
                    break;

                default:
                    break;
            }


            return list;
        }

        #endregion

    }
}
