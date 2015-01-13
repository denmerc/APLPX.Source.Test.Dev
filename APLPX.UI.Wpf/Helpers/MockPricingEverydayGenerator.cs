using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;
using DTO = APLPX.Client.Entity;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    /// Generates sample display entities for Pricing Everyday.
    /// </summary>
    public static class MockPricingEverydayGenerator
    {
        #region Private Fields

        private static Random _random = new Random();

        private static string[] _pricingEverydayNames = { "Admin - Everyday - All Filters - Movement Only", "Admin - Everyday - Movement & Days On Hand", 
                                                          "Admin - Everyday - Movement & Markup", "Admin - Everyday - Movement with Manual Groups", 
                                                          "Admin - Everyday - MarkUp, & DOH w/Custom - 1 Prod", "Admin - Everyday - Movement, Markup, & DOH+ManGrps", 
                                                          "Analyst Everyday All Drivers One-to-many Analytic1", "Analyst Everyday All Drivers one-to-many Analytic2", 
                                                          "Analyst Everyday All Drivers one-to-many Analytic3", "Analyst - Everyday - Movement & Markup w/ManGrps", 
                                                          "Analyst - Everyday - Movement with Outliers", "Approver - All Drivers - one-to-one Analytic", 
                                                          "Approver - Everyday - Movement & DOH w/small Filt", "Approver - Everyday - Movement & Markup w/sml Filt", 
                                                          "Approver - Everyday - Movement w/Pontiac Filters", "Approver - Everyday - Move. w/Caddy filt & 15 grps", 
                                                          "Analyst - Everyday - All Driver Analytic mismatch", "Approver - Everyday - all driver mismatch", 
                                                          "Admin - Everyday - Analytic Mismatch + global" };


        private static string[] _ownerNames = { "Admin User Active", "Admin User Active", "Admin User Active", "Analyst User Active", "Analyst User Active", 
                                                "Admin User Active", "Analyst User Active", "Admin User Active", "Admin User Active", "Analyst User Active", 
                                                "Approver User Active", "Approver User Active",  "Approver User Active", "Approver User Active", "Approver User Active" };

        #endregion

        public static PricingEveryday GetSamplePricingEveryday(int id)
        {
            var result = new PricingEveryday { Id = id + 1 };

            result.Identity = GetPricingIdentity(_pricingEverydayNames[id], _ownerNames[id]);
            
            result.FilterGroups = MockFilterGenerator.GetFilterGroupsComplete();

            //Price lists
            result.PricingModes = GetPricingModes();
            foreach (PricingMode mode in result.PricingModes)
            {
                result.PriceListGroups = GetEverydayPriceListGroups(mode);
            }

            result.KeyPriceListRule = new PricingKeyPriceListRule { DollarRangeLower = 10.25M, DollarRangeUpper = 115.00M, RoundingRules = GetRoundingRules() };
            result.LinkedPriceListRules = GetLinkedPriceListRules(result);

            var drivers = GetEverydayValueDrivers();
            result.ValueDrivers = new ReactiveList<PricingEverydayValueDriver>(drivers);

            //Value drivers
            PricingEverydayValueDriver keyDriver = result.ValueDrivers.FirstOrDefault(driver => driver.IsKey);
            if (keyDriver != null)
            {
                PricingEverydayKeyValueDriver key = GetEverydayKeyValueDriver(keyDriver);
                result.KeyValueDriver = key;
            }
            var selectedNonKey = result.ValueDrivers.Where(driver => !driver.IsKey && driver.IsSelected);
            var linkedDrivers = GetEverydayLinkedValueDrivers(selectedNonKey);

            result.LinkedValueDrivers = new ObservableCollection<PricingEverydayLinkedValueDriver>(linkedDrivers);

            //Results
            result.Results = GetEverydayResults();

            //Select default item for display purposes.
            result.SelectedFilterGroup = result.FilterGroups.FirstOrDefault();

            return result;
        }

        private static PricingIdentity GetPricingIdentity(string name, string ownerName)
        {
            PricingIdentity result = new PricingIdentity();
            result.AnalyticName = "Analytic 5244";
            result.Name = name;
            result.Description = String.Format("Sample description for {0}", name);
            result.Notes = String.Format("Here are are some sample notes that were entered for this item (\"{0}\").", name);
            result.Active = true;
            result.Shared = false;

            result.Created = DateTime.Now.AddDays(-8);
            result.Edited = DateTime.Now.AddDays(-3);
            result.Refreshed = DateTime.Now;
            result.Owner = ownerName;
            result.Author = ownerName;
            result.Editor = ownerName;

            return result;
        }

        #region Price Lists

        private static List<PricingMode> GetPricingModes()
        {
            var result = new List<PricingMode>();
            result.Add(new PricingMode
            {
                Key = 19,
                Name = "One key",
                Title = "Only one price list is used.",
                IsSelected = false,
                HasKeyPriceListRule = true,
                HasLinkedPriceListRule = false,
                KeyPriceListGroupKey = 7,
                LinkedPriceListGroupKey = 0,
                Sort = 1
            });
            result.Add(new PricingMode
            {
                Key = 20,
                Name = "Global key",
                Title = "All non-Key Price Lists update based on a user set percentage applied to the Key Price List.",
                IsSelected = true,
                HasKeyPriceListRule = true,
                HasLinkedPriceListRule = true,
                KeyPriceListGroupKey = 7,
                LinkedPriceListGroupKey = 7,
                Sort = 2

            });
            result.Add(new PricingMode
            {
                Key = 21,
                Name = "Global key +",
                Title = "Individual SKUs in non-Key Price Lists retain their existing percentage ratio between themselves and the Key Price List.",
                IsSelected = false,
                HasKeyPriceListRule = true,
                HasLinkedPriceListRule = false,
                KeyPriceListGroupKey = 7,
                LinkedPriceListGroupKey = 7,
                Sort = 3

            });
            result.Add(new PricingMode
            {
                Key = 22,
                Name = "Cascade",
                Title = "All non-Key Price Lists update based on a percentage of the Price List above.",
                IsSelected = false,
                HasKeyPriceListRule = true,
                HasLinkedPriceListRule = true,
                KeyPriceListGroupKey = 7,
                LinkedPriceListGroupKey = 7,
                Sort = 4

            });

            return result;
        }

        private static List<PricingEverydayPriceListGroup> GetEverydayPriceListGroups(PricingMode mode)
        {
            var result = new List<PricingEverydayPriceListGroup>();

            for (int groupIndex = 1; groupIndex <= 2; groupIndex++)
            {
                PricingEverydayPriceListGroup group = null;
                bool isLinkedGroup = (groupIndex > 1);
                int key = mode.KeyPriceListGroupKey;
                int linkedKey = mode.LinkedPriceListGroupKey;

                if (isLinkedGroup)
                {
                    group = new PricingEverydayPriceListGroup { Name = "Price List Group " + groupIndex, Sort = (short)groupIndex, Key = key };
                    var priceLists = GetEverydayPriceLists(isLinkedGroup);
                    group.PriceLists = new ReactiveList<PricingEverydayPriceList>(priceLists);
                    result.Add(group);
                }
                else if (linkedKey > 0 && linkedKey != key)
                {
                    group = new PricingEverydayPriceListGroup { Name = "Price List Group " + groupIndex, Sort = (short)groupIndex, Key = linkedKey };
                    var priceLists = GetEverydayPriceLists(isLinkedGroup);
                    group.PriceLists = new ReactiveList<PricingEverydayPriceList>(priceLists);
                    result.Add(group);
                }
            }

            return result;
        }

        private static List<PricingEverydayPriceList> GetEverydayPriceLists(bool isLinkedGroup)
        {
            List<PricingEverydayPriceList> result = new List<PricingEverydayPriceList>();
            result.Add(new PricingEverydayPriceList { Id = 8, Key = 1, Code = "0", Name = "Cost", Title = "Cost", IsSelected = false, IsKey = false, Sort = 1 });
            result.Add(new PricingEverydayPriceList { Id = 6, Key = 2, Code = "L", Name = "Retail List price", Title = "Retail List price", IsSelected = true, IsKey = true, Sort = 2 });
            result.Add(new PricingEverydayPriceList { Id = 10, Key = 3, Code = "LF", Name = "Retail List price *FUTURE PRICE*", Title = "LF", IsSelected = false, IsKey = false, Sort = 3 });
            result.Add(new PricingEverydayPriceList { Id = 7, Key = 4, Code = "R", Name = "Retail Sale price", Title = "Retail Sale price", IsSelected = true, IsKey = false, Sort = 4 });
            result.Add(new PricingEverydayPriceList { Id = 9, Key = 5, Code = "RF", Name = "Retail Sale price *FUTURE PRICE*", Title = "RF", IsSelected = false, IsKey = false, Sort = 5 });
            result.Add(new PricingEverydayPriceList { Id = 11, Key = 6, Code = "C", Name = "Dealer Sugg Retail", Title = "Dealer Sugg Retail", IsSelected = false, IsKey = false, Sort = 6 });
            result.Add(new PricingEverydayPriceList { Id = 5, Key = 7, Code = "J", Name = "Jobber - Trim Shop", Title = "Jobber - Trim Shop", IsSelected = true, IsKey = false, Sort = 7 });
            result.Add(new PricingEverydayPriceList { Id = 12, Key = 8, Code = "D", Name = "Dealer Std Price", Title = "Dealer Std Price", IsSelected = false, IsKey = false, Sort = 8 });
            result.Add(new PricingEverydayPriceList { Id = 1, Key = 9, Code = "1", Name = "Dealer 1", Title = "Dealer 1", IsSelected = true, IsKey = false, Sort = 9 });

            if (isLinkedGroup)
            {
                result.Add(new PricingEverydayPriceList { Id = 2, Key = 10, Code = "2", Name = "Dealer 2", Title = "Dealer 2", IsSelected = true, IsKey = false, Sort = 10 });
                result.Add(new PricingEverydayPriceList { Id = 3, Key = 11, Code = "3", Name = "Dealer 3", Title = "Dealer 3", IsSelected = true, IsKey = false, Sort = 11 });
                result.Add(new PricingEverydayPriceList { Id = 4, Key = 12, Code = "4", Name = "Dealer 4", Title = "Dealer 4", IsSelected = true, IsKey = false, Sort = 12 });
            }

            return result;
        }

        private static List<PricingLinkedPriceListRule> GetLinkedPriceListRules(PricingEveryday priceRoutine)
        {
            var rules = new List<PricingLinkedPriceListRule>();

            var uniquePriceListIds = priceRoutine.PriceListGroups.SelectMany(grp => grp.PriceLists).Select(pl => pl.Id).Distinct();
            foreach (int id in uniquePriceListIds)
            {
                int percentChange = _random.Next(3, 15);
                var roundingRules = GetRoundingRules();
                var rule = new PricingLinkedPriceListRule { PercentChange = percentChange, PriceListId = id, RoundingRules = roundingRules };
                rules.Add(rule);
            }
            return rules;
        }

        #endregion

        #region Value Drivers

        private static List<PricingEverydayValueDriver> GetEverydayValueDrivers()
        {
            var drivers = new List<PricingEverydayValueDriver>();

            var driver = new PricingEverydayValueDriver { Id = 16, Key = 34, Name = "Markup", IsSelected = true, IsKey = false, Sort = 2 };
            driver.Groups.Add(new PricingValueDriverGroup { Id = 76, Value = 1, SkuCount = 20, SalesValue = "16,020.43", MinOutlier = 1600, MaxOutlier = 99999, Sort = 1 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 77, Value = 2, SkuCount = 18, SalesValue = "7,574.79", MinOutlier = 1200, MaxOutlier = 1599, Sort = 2 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 78, Value = 3, SkuCount = 27, SalesValue = "34,898.17", MinOutlier = 800, MaxOutlier = 1199, Sort = 3 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 79, Value = 4, SkuCount = 42, SalesValue = "67,442.4", MinOutlier = 300, MaxOutlier = 799, Sort = 4 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 80, Value = 5, SkuCount = 19, SalesValue = "16,182", MinOutlier = 0, MaxOutlier = 299, Sort = 5 });
            drivers.Add(driver);

            driver = new PricingEverydayValueDriver { Id = 17, Key = 35, Name = "Movement", IsSelected = true, IsKey = true, Sort = 3 };
            driver.Groups.Add(new PricingValueDriverGroup { Id = 81, Value = 1, SkuCount = 1, SalesValue = "6,848", MinOutlier = 100, MaxOutlier = 2999, Sort = 1 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 82, Value = 2, SkuCount = 1, SalesValue = "4,010", MinOutlier = 500, MaxOutlier = 999, Sort = 2 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 83, Value = 3, SkuCount = 3, SalesValue = "17,583", MinOutlier = 200, MaxOutlier = 499, Sort = 3 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 84, Value = 4, SkuCount = 12, SalesValue = "35,942", MinOutlier = 100, MaxOutlier = 199, Sort = 4 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 85, Value = 5, SkuCount = 109, SalesValue = "77,734.23", MinOutlier = 0, MaxOutlier = 99, Sort = 5 });
            drivers.Add(driver);

            driver = new PricingEverydayValueDriver { Id = 18, Key = 36, Name = "Days On Hand", IsSelected = true, IsKey = false, Sort = 4 };
            driver.Groups.Add(new PricingValueDriverGroup { Id = 86, Value = 1, SkuCount = 58, SalesValue = "74,986", MinOutlier = 300, MaxOutlier = 365, Sort = 1 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 87, Value = 2, SkuCount = 0, SalesValue = "0", MinOutlier = 235, MaxOutlier = 299, Sort = 2 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 88, Value = 3, SkuCount = 0, SalesValue = "0", MinOutlier = 200, MaxOutlier = 234, Sort = 3 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 89, Value = 4, SkuCount = 1, SalesValue = "1,143", MinOutlier = 165, MaxOutlier = 199, Sort = 4 });
            driver.Groups.Add(new PricingValueDriverGroup { Id = 90, Value = 5, SkuCount = 67, SalesValue = "65,989", MinOutlier = 0, MaxOutlier = 164, Sort = 5 });
            drivers.Add(driver);

            return drivers;
        }

        private static PricingEverydayKeyValueDriver GetEverydayKeyValueDriver(PricingEverydayValueDriver sourceDriver)
        {
            var keyDriver = new PricingEverydayKeyValueDriver { ValueDriverId = sourceDriver.Id };

            foreach (PricingValueDriverGroup group in sourceDriver.Groups)
            {
                var keyDriverGroup = new PricingEverydayKeyValueDriverGroup { ValueDriverGroupId = group.Id, ValueDriverGroupValue = group.Value };
                keyDriverGroup.OptimizationRules = GetPriceOptimizationRules();
                keyDriverGroup.MarkupRules = GetMarkupRules();
                keyDriver.Groups.Add(keyDriverGroup);
            }

            return keyDriver;
        }

        private static List<PricingEverydayLinkedValueDriver> GetEverydayLinkedValueDrivers(IEnumerable<PricingEverydayValueDriver> sourceDrivers)
        {
            var result = new List<PricingEverydayLinkedValueDriver>();

            int index = 0;
            foreach (PricingEverydayValueDriver sourceDriver in sourceDrivers)
            {
                var linkedDriver = new PricingEverydayLinkedValueDriver { ValueDriverId = sourceDriver.Id, Name = sourceDriver.Name };

                //For display purposes, just create a single driver group from the source driver.
                var sourceGroup = sourceDriver.Groups[0];
                linkedDriver.Groups.Add(new PricingEverydayLinkedValueDriverGroup
                {
                    ValueDriverGroupId = sourceGroup.Id,
                    PercentChange = 0.1M + index,
                    ValueDriverGroupValue = sourceGroup.Value
                });
                result.Add(linkedDriver);
                index++;
            }

            return result;
        }

        #endregion

        #region Pricing Rules

        private static List<SQLEnumeration> GetRoundingRuleTypes()
        {
            var list = new List<SQLEnumeration>();

            list.Add(new SQLEnumeration { Name = "Round Up", Description = "Psychological rounding up to change value", Value = 53, Sort = 1 });
            list.Add(new SQLEnumeration { Name = "Round Near", Description = "Psychological rounding nearest to change value", Value = 54, Sort = 2 });

            return list;
        }

        public static List<PricingRoundingTemplate> GetRoundingTemplates()
        {
            var list = new List<PricingRoundingTemplate>();

            string[] names = { "Interior", "Moldings-Chrome", "Apparel", "Engine Parts" };

            for (short i = 0; i < names.Length; i++)
            {
                var roundingRules = GetRoundingRules().Take(10 - i).ToList();
                string name = names[i];
                PricingRoundingTemplate template = new PricingRoundingTemplate { Id = i, Name = name, Description = "Rounding template for " + name, Rules = roundingRules, Sort = i };
                list.Add(template);
            }

            return list;
        }

        private static List<PriceRoundingRule> GetRoundingRules()
        {
            var ruleTypes = GetRoundingRuleTypes();

            var list = new List<PriceRoundingRule>();

            list.Add(new PriceRoundingRule { Id = 101, DollarRangeLower = 0.01M, DollarRangeUpper = 20M, ValueChange = 0.89M, Type = ruleTypes[1].Value, RoundingTypes = ruleTypes });
            list.Add(new PriceRoundingRule { Id = 102, DollarRangeLower = 20.01M, DollarRangeUpper = 25M, ValueChange = 0.99M, Type = ruleTypes[1].Value, RoundingTypes = ruleTypes });
            list.Add(new PriceRoundingRule { Id = 103, DollarRangeLower = 25.01M, DollarRangeUpper = 30M, ValueChange = 0.79M, Type = ruleTypes[1].Value, RoundingTypes = ruleTypes });
            list.Add(new PriceRoundingRule { Id = 104, DollarRangeLower = 30.01M, DollarRangeUpper = 75M, ValueChange = 0.99M, Type = ruleTypes[1].Value, RoundingTypes = ruleTypes });
            list.Add(new PriceRoundingRule { Id = 105, DollarRangeLower = 75.01M, DollarRangeUpper = 100M, ValueChange = 0.99M, Type = ruleTypes[1].Value, RoundingTypes = ruleTypes });
            list.Add(new PriceRoundingRule { Id = 106, DollarRangeLower = 100.01M, DollarRangeUpper = 110M, ValueChange = 0.99M, Type = ruleTypes[0].Value, RoundingTypes = ruleTypes });
            list.Add(new PriceRoundingRule { Id = 107, DollarRangeLower = 110.01M, DollarRangeUpper = 500M, ValueChange = 0.99M, Type = ruleTypes[0].Value, RoundingTypes = ruleTypes });
            list.Add(new PriceRoundingRule { Id = 108, DollarRangeLower = 500.01M, DollarRangeUpper = 800M, ValueChange = 0.99M, Type = ruleTypes[0].Value, RoundingTypes = ruleTypes });
            list.Add(new PriceRoundingRule { Id = 109, DollarRangeLower = 800.01M, DollarRangeUpper = 1000M, ValueChange = 0.99M, Type = ruleTypes[0].Value, RoundingTypes = ruleTypes });
            list.Add(new PriceRoundingRule { Id = 110, DollarRangeLower = 1000.01M, DollarRangeUpper = 99999M, ValueChange = 0.99M, Type = ruleTypes[0].Value, RoundingTypes = ruleTypes });

            return list;
        }

        private static List<PriceMarkupRule> GetMarkupRules()
        {
            var list = new List<PriceMarkupRule>();
            list.Add(new PriceMarkupRule { Id = 21, DollarRangeLower = 0.01M, DollarRangeUpper = 12M, PercentLimitLower = 60, PercentLimitUpper = 999999 });
            list.Add(new PriceMarkupRule { Id = 22, DollarRangeLower = 12.01M, DollarRangeUpper = 25M, PercentLimitLower = 50, PercentLimitUpper = 999999 });
            list.Add(new PriceMarkupRule { Id = 23, DollarRangeLower = 25.01M, DollarRangeUpper = 35.23M, PercentLimitLower = 45, PercentLimitUpper = 99999 });
            list.Add(new PriceMarkupRule { Id = 24, DollarRangeLower = 35.24M, DollarRangeUpper = 50.34M, PercentLimitLower = 42, PercentLimitUpper = 99999 });
            list.Add(new PriceMarkupRule { Id = 25, DollarRangeLower = 50.35M, DollarRangeUpper = 65.45M, PercentLimitLower = 41, PercentLimitUpper = 99999 });
            list.Add(new PriceMarkupRule { Id = 26, DollarRangeLower = 65.46M, DollarRangeUpper = 149.99M, PercentLimitLower = 38, PercentLimitUpper = 99999 });
            list.Add(new PriceMarkupRule { Id = 27, DollarRangeLower = 150M, DollarRangeUpper = 350M, PercentLimitLower = 32, PercentLimitUpper = 99999 });
            list.Add(new PriceMarkupRule { Id = 28, DollarRangeLower = 350.01M, DollarRangeUpper = 400.99M, PercentLimitLower = 25, PercentLimitUpper = 99999 });
            list.Add(new PriceMarkupRule { Id = 29, DollarRangeLower = 401M, DollarRangeUpper = 10799.99M, PercentLimitLower = 20, PercentLimitUpper = 99999 });
            list.Add(new PriceMarkupRule { Id = 30, DollarRangeLower = 10800M, DollarRangeUpper = 10801M, PercentLimitLower = 15, PercentLimitUpper = 99999 });

            return list;
        }

        private static List<PriceOptimizationRule> GetPriceOptimizationRules()
        {
            var list = new List<PriceOptimizationRule>();
            list.Add(new PriceOptimizationRule { Id = 21, DollarRangeLower = 0.01M, DollarRangeUpper = 12M, PercentChange = 7 });
            list.Add(new PriceOptimizationRule { Id = 22, DollarRangeLower = 12.01M, DollarRangeUpper = 25M, PercentChange = 6 });
            list.Add(new PriceOptimizationRule { Id = 23, DollarRangeLower = 25.01M, DollarRangeUpper = 35.23M, PercentChange = 6 });
            list.Add(new PriceOptimizationRule { Id = 24, DollarRangeLower = 35.24M, DollarRangeUpper = 50.34M, PercentChange = 5 });
            list.Add(new PriceOptimizationRule { Id = 25, DollarRangeLower = 50.35M, DollarRangeUpper = 65.45M, PercentChange = 5 });
            list.Add(new PriceOptimizationRule { Id = 26, DollarRangeLower = 65.46M, DollarRangeUpper = 149.99M, PercentChange = 4 });
            list.Add(new PriceOptimizationRule { Id = 27, DollarRangeLower = 150M, DollarRangeUpper = 350M, PercentChange = 3 });
            list.Add(new PriceOptimizationRule { Id = 28, DollarRangeLower = 350.01M, DollarRangeUpper = 400.99M, PercentChange = 2 });
            list.Add(new PriceOptimizationRule { Id = 29, DollarRangeLower = 401M, DollarRangeUpper = 10799.99M, PercentChange = 1 });
            list.Add(new PriceOptimizationRule { Id = 30, DollarRangeLower = 10800M, DollarRangeUpper = 10801M, PercentChange = 1 });

            return list;
        }

        #endregion

        #region Pricing Results

        private static List<PricingEverydayResult> GetEverydayResults()
        {
            List<PricingEverydayResultPriceList> priceLists = GetEverydayResultPricelists();
            var list = new List<PricingEverydayResult>();
            list.Add(new PricingEverydayResult { SkuId = 123, SkuName = "CE01402", SkuTitle = "1959-60 Cadillac Pitman Arm", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 126, SkuName = "PSP0013", SkuTitle = "Pump, Power Steering, 75-76 Cadillac, New", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 288, SkuName = "CE12161", SkuTitle = "1968-70 Cadillac Power Steering Pump & Reservoir", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 604, SkuName = "CE11863", SkuTitle = "1954-56 Cadillac 3 Front Low Profile Springs", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 612, SkuName = "CE01363", SkuTitle = "Outer Tie Rod, 1961-63 PONT/1963-69 Cadillac/1963 Skylark", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 696, SkuName = "CE11871", SkuTitle = "1961-64 Cadillac 2 Front Low Profile Springs", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 959, SkuName = "PSP0053", SkuTitle = "Pump, Power Steering, 60-67 Multi, New", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 991, SkuName = "CE01472", SkuTitle = "1961-64 Cadillac Rear Control Arm Bushing, upper", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 1352, SkuName = "CH28562", SkuTitle = "Flaming River Tilt Steering Column Acc 1-48 x 3/4 - DD SS U-Joint", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 1757, SkuName = "CE12163", SkuTitle = "1975-76 Cadillac Power Steering Pump & Reservoir", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 1771, SkuName = "CE01523", SkuTitle = "1976 Seville (diesel) Front Coil Springs", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 2123, SkuName = "ADD0943", SkuTitle = "1971-76 Eldorado/Bonneville/Catalina 1 Rear Anti-Sway Bar", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 2165, SkuName = "CE03380", SkuTitle = "1965-67 Cadillac Transmission Mount exc. 1967 Eldorado", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 2337, SkuName = "C980100", SkuTitle = "64-7 KYB REAR GAS-A-JUST SHOCK", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 2379, SkuName = "ADD0930", SkuTitle = "1965-70 Eldorado 3/4 Rear Anti-Sway Bar", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 2516, SkuName = "CE01334", SkuTitle = "1967-76 Eldorado Upper Control Arm Bushing", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 2601, SkuName = "CE12041", SkuTitle = "1975-76 Seville Gas-A-Just Shocks", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 2608, SkuName = "CE01398", SkuTitle = "1976 Cadillac Seville Idler Arm", PriceLists = priceLists });
            list.Add(new PricingEverydayResult { SkuId = 2738, SkuName = "CE12164", SkuTitle = "1976 Cadillac Eldorado, CC, Limo & Series 75 Power Steering Pump & Reservoir", PriceLists = priceLists });

            return list;
        }

        private static List<PricingEverydayResultPriceList> GetEverydayResultPricelists()
        {
            var list = new List<PricingEverydayResultPriceList>();

            list.Add(new PricingEverydayResultPriceList
            {
                ResultId = 111,
                CurrentPrice = 12.15M,
                NewPrice = 13.25M,
                CurrentMarkupPercent = 25,
                NewMarkupPercent = 26,
                KeyValueChange = 2.5M,
                InfluenceValueChange = 3.1M,
                PriceChange = 4.02M,
                IsKey = false,
                Id = 1011,
                Key = 10,
                Code = "Code1",
                Name = "Retail List price",
                Title = "Retail List price",
                IsSelected = true,
                Sort = 1
            });
            list.Add(new PricingEverydayResultPriceList
            {
                ResultId = 112,
                CurrentPrice = 12.45M,
                NewPrice = 14.03M,
                CurrentMarkupPercent = 24,
                NewMarkupPercent = 27,
                KeyValueChange = 3.2M,
                InfluenceValueChange = 2.8M,
                PriceChange = 5.6M,
                IsKey = false,
                Id = 1012,
                Key = 11,
                Code = "Code2",
                Name = "Retail Sale price",
                Title = "Retail Sale price",
                IsSelected = false,
                Sort = 2
            });
            list.Add(new PricingEverydayResultPriceList
            {
                ResultId = 113,
                CurrentPrice = 12.75M,
                NewPrice = 14.81M,
                CurrentMarkupPercent = 23,
                NewMarkupPercent = 28,
                KeyValueChange = 3.9M,
                InfluenceValueChange = 2.5M,
                PriceChange = 7.18M,
                IsKey = false,
                Id = 1013,
                Key = 12,
                Code = "Code3",
                Name = "Jobber – Trim Shop",
                Title = "Jobber – Trim Shop",
                IsSelected = true,
                Sort = 3
            });
            list.Add(new PricingEverydayResultPriceList
            {
                ResultId = 114,
                CurrentPrice = 13.05M,
                NewPrice = 15.59M,
                CurrentMarkupPercent = 22,
                NewMarkupPercent = 29,
                KeyValueChange = 4.6M,
                InfluenceValueChange = 2.2M,
                PriceChange = 8.76M,
                IsKey = true,
                Id = 1014,
                Key = 13,
                Code = "Code4",
                Name = "Dealer 1",
                Title = "Dealer 1",
                IsSelected = false,
                Sort = 4
            });
            list.Add(new PricingEverydayResultPriceList
            {
                ResultId = 115,
                CurrentPrice = 13.35M,
                NewPrice = 16.37M,
                CurrentMarkupPercent = 21,
                NewMarkupPercent = 30,
                KeyValueChange = 5.3M,
                InfluenceValueChange = 1.9M,
                PriceChange = 10.34M,
                IsKey = false,
                Id = 1015,
                Key = 14,
                Code = "Code5",
                Name = "Dealer 2",
                Title = "Dealer 2",
                IsSelected = false,
                Sort = 5
            });

            //Set sample edit and warning values for some of the price lists.
            list[0].PriceEdit.Type = DTO.PricingResultsEditType.DefaultPrice;
            list[1].PriceWarning.Type = DTO.PricingResultsWarningType.MarkupBelow;

            return list;
        }

        #endregion
    }
}
