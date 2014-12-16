using System;
using System.Collections.Generic;
using System.Linq;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Interfaces;
using DTO = APLPX.Client.Entity;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    /// Helper class for generating mock Module display objects, including their child objects.
    /// </summary>
    public class DisplayModuleGenerator
    {
        private static Random _random = new Random();

        #region Modules, Features, and Steps

        public static List<Module> CreateSampleModules()
        {
            string[] names = { "Planning", 
                               "Tracking", 
                               "Reporting", 
                               "Administration" };

            DTO.ModuleType[] types = { DTO.ModuleType.Planning, 
                                       DTO.ModuleType.Tracking, 
                                       DTO.ModuleType.Reporting, 
                                       DTO.ModuleType.Administration };

            var modules = new List<Module>();
            for (int i = 0; i < names.Length; i++)
            {
                Module module = new Module { Name = names[i], TypeId = types[i], Title = names[i] + " short description..." };
                module.Features = CreateSampleFeatures(module);
                modules.Add(module);
            }

            return modules;
        }

        private static List<ModuleFeature> CreateSampleFeatures(Module module)
        {
            var features = new List<ModuleFeature>();

            switch (module.TypeId)
            {
                case APLPX.Client.Entity.ModuleType.Planning:
                    features.Add(new ModuleFeature { Name = "Home", TypeId = DTO.ModuleFeatureType.PlanningHome });
                    features.Add(new ModuleFeature { Name = "Analytics", TypeId = DTO.ModuleFeatureType.PlanningAnalytics, Title = "Planning Value Driver Analytics " });
                    features.Add(new ModuleFeature { Name = "Everyday Pricing", TypeId = DTO.ModuleFeatureType.PlanningEverydayPricing, Title = "Planning Everyday Price changes and updates" });
                    features.Add(new ModuleFeature { Name = "Promotion Pricing", TypeId = DTO.ModuleFeatureType.PlanningPromotionPricing, Title = "Planning Promotion Price changes and updates" });
                    features.Add(new ModuleFeature { Name = "Kit Pricing", TypeId = DTO.ModuleFeatureType.PlanningKitPricing, Title = "Planning Kit Price changes and updates" });
                    break;

                case APLPX.Client.Entity.ModuleType.Tracking:
                    features.Add(new ModuleFeature { Name = "Home", TypeId = DTO.ModuleFeatureType.TrackingHome, Title = "Tracking home page" });
                    features.Add(new ModuleFeature { Name = "Approvals" });
                    features.Add(new ModuleFeature { Name = "Workflows" });
                    break;

                case APLPX.Client.Entity.ModuleType.Reporting:
                    features.Add(new ModuleFeature { Name = "Home", TypeId = DTO.ModuleFeatureType.ReportingHome, Title = "Reporting home page" });
                    features.Add(new ModuleFeature { Name = "Reporting1" });
                    features.Add(new ModuleFeature { Name = "Reporting2" });
                    features.Add(new ModuleFeature { Name = "Reporting3" });
                    break;

                case APLPX.Client.Entity.ModuleType.Administration:
                    features.Add(new ModuleFeature { Name = "Users", TypeId = DTO.ModuleFeatureType.AdministrationUserMaintenance });
                    features.Add(new ModuleFeature { Name = "Price Lists" });
                    features.Add(new ModuleFeature { Name = "Rules" });
                    features.Add(new ModuleFeature { Name = "Rollback" });
                    features.Add(new ModuleFeature { Name = "Processes" });
                    break;
                default:
                    break;
            }

            PopulateDefaultStepTypes(features);

            foreach (ModuleFeature feature in features)
            {
                feature.Steps = GetSampleSteps(module.Name, feature);
                feature.SearchGroups = GetSampleSearchGroups(feature);
                feature.SearchableEntities = GetSampleSearchEntities(feature);
            }

            return features;
        }

        private static List<ModuleFeatureStep> GetSampleSteps(string moduleName, ModuleFeature feature)
        {
            var steps = new List<ModuleFeatureStep>();

            if (moduleName == "Planning")
            {
                //steps.Add(new ModuleFeatureStep { Name = "Planning Home", TypeId = DTO.ModuleFeatureStepType.PlanningHomeMyHomePage });
                if (feature.TypeId == DTO.ModuleFeatureType.PlanningAnalytics)
                {
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "Search Analytics", Title = "Search saved Analytics", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsSearchAnalytics, Sort = 1 });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "1) Identity", Title = "Identify Analytics with a unique name and description", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsIdentity, Sort = 2, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "2) Filters", Title = "Configure Analytics product filters & define a product set", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsFilters, Sort = 3, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "3) Price Lists", Title = "Configure Analytics price list metrics & aggregation", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsPriceLists, Sort = 4, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "4) Value Drivers", Title = "Configure Analytics Value Driver metrics and optimization", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsValueDrivers, Sort = 5, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "5) Results", Title = "Compare Analytics result set & view reports", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsResults, Sort = 6, });
                }
                else if (feature.TypeId == DTO.ModuleFeatureType.PlanningEverydayPricing)
                {
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "Search Everyday", Title = "Search saved Pricing campaigns", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingSearchEveryday, Sort = 1, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "1) Identity", Title = "Identify Price routines with a unique name and description", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingIdentity, Sort = 2, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "2) Filters", Title = "Configure Price routine product filters & define a product set", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingFilters, Sort = 3, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "3) Price Lists", Title = "Configure Price routine price lists ", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingPriceLists, Sort = 4, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "4) Rounding", Title = "Configure Price routine rounding", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingRounding, Sort = 5, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "5) Strategy", Title = "Configure Price routine optimization strategy", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingStrategy, Sort = 6, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "6) Results", Title = "Compare and edit Price routine results", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingResults, Sort = 7, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "7) Forecast", Title = "Create a Price routine forecast", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingForecast, Sort = 8, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "8) Approval", Title = "Submit this Price routine for approval", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingApproval, Sort = 9, });
                }
                else if (feature.TypeId == DTO.ModuleFeatureType.PlanningPromotionPricing)
                {
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "Search Promotions", Title = "Search saved Pricing campaigns", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingSearchPromotions, Sort = 1, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "1) Identity", Title = "Identify Price routines with a unique name and description", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingIdentity, Sort = 2, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "2) Filters", Title = "Configure Price routine product filters & define a product set", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingFilters, Sort = 3, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "3) Price Lists", Title = "Configure Price routine price lists ", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingPriceLists, Sort = 4, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "4) Rounding", Title = "Configure Price routine rounding", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingRounding, Sort = 5, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "5) Strategy", Title = "Configure Price routine optimization strategy", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingStrategy, Sort = 6, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "6) Results", Title = "Compare and edit Price routine results", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingResults, Sort = 7, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "7) Forecast", Title = "Create a Price routine forecast", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingForecast, Sort = 8, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "8) Approval", Title = "Submit this Price routine for approval", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingApproval, Sort = 9, });
                }
                else if (feature.TypeId == DTO.ModuleFeatureType.PlanningKitPricing)
                {
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "Search Promotions", Title = "Search saved Pricing campaigns", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingSearchKits, Sort = 1, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "1) Identity", Title = "Identify Price routines with a unique name and description", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingIdentity, Sort = 2, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "2) Filters", Title = "Configure Price routine product filters & define a product set", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingFilters, Sort = 3, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "3) Price Lists", Title = "Configure Price routine price lists ", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingPriceLists, Sort = 4, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "4) Rounding", Title = "Configure Price routine rounding", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingRounding, Sort = 5, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "5) Strategy", Title = "Configure Price routine optimization strategy", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingStrategy, Sort = 6, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "6) Results", Title = "Compare and edit Price routine results", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingResults, Sort = 7, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "7) Forecast", Title = "Create a Price routine forecast", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingForecast, Sort = 8, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "8) Approval", Title = "Submit this Price routine for approval", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingApproval, Sort = 9, });

                }
                //else
                //{
                //    steps.Add(new ModuleFeatureStep { Name = "Search", Title = "Search saved price routines", Sort = 1 }); //TODO: add enum value for search.
                //    steps.Add(new ModuleFeatureStep { Name = "Identity", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingIdentity, Title = "Identify Price routines with a unique name and description", Sort = 2 });
                //    steps.Add(new ModuleFeatureStep { Name = "Filters", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingFilters, Title = "Configure Price routine product filters & define a product set", Sort = 3 });
                //    steps.Add(new ModuleFeatureStep { Name = "Price Lists", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingPriceLists, Title = "Configure Price routine price lists ", Sort = 4 });
                //    steps.Add(new ModuleFeatureStep { Name = "Rounding", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingRounding, Title = "Configure Price routine rounding", Sort = 5 });
                //    steps.Add(new ModuleFeatureStep { Name = "Strategy", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingStrategy, Title = "Configure Price routine optimization strategy", Sort = 6 });
                //    steps.Add(new ModuleFeatureStep { Name = "Results", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingResults, Title = "Compare and edit Price routine results", Sort = 7 });
                //    steps.Add(new ModuleFeatureStep { Name = "Forecast", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingForecast, Title = "Create a Price routine forecast", Sort = 8 });
                //    steps.Add(new ModuleFeatureStep { Name = "Request Approval", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingApproval, Title = "Submit this Price routine for approval", Sort = 9 });
                //}

                steps.ForEach(step => step.Actions = GetSampleActions(step));
            }

            return steps;
        }

        private static void PopulateDefaultStepTypes(List<ModuleFeature> features)
        {
            //Populate the Analytics feature:
            ModuleFeature feature = features.Where(f => f.TypeId == DTO.ModuleFeatureType.PlanningAnalytics).FirstOrDefault();
            if (feature != null)
            {
                feature.LandingStepType = DTO.ModuleFeatureStepType.PlanningAnalyticsSearchAnalytics;
                feature.ActionStepType = DTO.ModuleFeatureStepType.PlanningAnalyticsIdentity;
            }

            //Populate the price routine features:      
            feature = features.Where(f => f.TypeId == DTO.ModuleFeatureType.PlanningEverydayPricing).FirstOrDefault();
            if (feature != null)
            {
                feature.LandingStepType = DTO.ModuleFeatureStepType.PlanningEverydayPricingSearchEveryday;
                feature.ActionStepType = DTO.ModuleFeatureStepType.PlanningEverydayPricingIdentity;
            }

            feature = features.Where(f => f.TypeId == DTO.ModuleFeatureType.PlanningPromotionPricing).FirstOrDefault();
            if (feature != null)
            {
                feature.LandingStepType = DTO.ModuleFeatureStepType.PlanningPromotionPricingSearchPromotions;
                feature.ActionStepType = DTO.ModuleFeatureStepType.PlanningPromotionPricingIdentity;
            }

            feature = features.Where(f => f.TypeId == DTO.ModuleFeatureType.PlanningKitPricing).FirstOrDefault();
            if (feature != null)
            {
                feature.LandingStepType = DTO.ModuleFeatureStepType.PlanningKitPricingSearchKits;
                feature.ActionStepType = DTO.ModuleFeatureStepType.PlanningKitPricingIdentity;
            }
        }

        #endregion

        #region Search Groups and Entities

        private static List<FeatureSearchGroup> GetSampleSearchGroups(ModuleFeature feature)
        {
            var searchGroups = new List<FeatureSearchGroup>();

            if (feature.Name == "Kits")
            {
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Recent", Name = "This Week", SearchKey = "searchKey001", Sort = 1, ItemCount = 2 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Recent", Name = "Last Week", SearchKey = "searchKey002", Sort = 2, ItemCount = 0 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Recent", Name = "Older", SearchKey = "searchKey003", Sort = 3, ItemCount = 6 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "John Doe", SearchKey = "seerchKey101", Sort = 4, ItemCount = 4 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "Jane Doe", SearchKey = "seerchKey102", Sort = 5, ItemCount = 4 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Scenarios", Name = "Scenario #1", SearchKey = "searchKey112", Sort = 6, ItemCount = 4 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Scenarios", Name = "Scenario #1A", SearchKey = "searchKey113", Sort = 7, ItemCount = 2 });
            }
            else if (feature.Name == "Everyday")
            {
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Recent", Name = "Recent", SearchKey = "searchKey001", Sort = 1, ItemCount = 2 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "User A", SearchKey = "searchKey101", Sort = 2, ItemCount = 0 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "User B", SearchKey = "searchKey102", Sort = 3, ItemCount = 0 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 1", SearchKey = "searchKey111", Sort = 4, ItemCount = 4 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 2", SearchKey = "searchKey112", Sort = 5, ItemCount = 2 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 3", SearchKey = "searchKey113", Sort = 6, ItemCount = 0 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 4", SearchKey = "searchKey114", Sort = 7, ItemCount = 3 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 4", SearchKey = "searchKey115", Sort = 8, ItemCount = 2 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 5", SearchKey = "searchKey116", Sort = 9, ItemCount = 1 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 6", SearchKey = "searchKey117", Sort = 10, ItemCount = 3 });
            }
            else
            {
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Recent", Name = "Recent", SearchKey = "searchKey001", Sort = 1, ItemCount = 5 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "User 1", SearchKey = "searchKey101", Sort = 2, ItemCount = 2 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "User 2", SearchKey = "searchKey102", Sort = 3, ItemCount = 1 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "User 3", SearchKey = "searchKey103", Sort = 4, ItemCount = 3 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 1", SearchKey = "searchKey111", Sort = 5, ItemCount = 3 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 2", SearchKey = "searchKey112", Sort = 6, ItemCount = 7 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 3", SearchKey = "searchKey113", Sort = 7, ItemCount = 0 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 4", SearchKey = "searchKey114", Sort = 8, ItemCount = 2 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 5", SearchKey = "searchKey115", Sort = 9, ItemCount = 1 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "My Folders", Name = "Folder 6", SearchKey = "searchKey116", Sort = 10, ItemCount = 3 });
            }

            //Make "My Folder names" editable.
            var editables = searchGroups.Where(sg => sg.ParentName == "My Folders").ToList();
            editables.ForEach(item => item.CanNameChange = true);
            editables.ForEach(item => item.CanSearchKeyChange = true);
            return searchGroups;
        }

        private static List<ISearchableEntity> GetSampleSearchEntities(ModuleFeature feature)
        {
            var searchEntities = new List<ISearchableEntity>();

            foreach (FeatureSearchGroup searchGroup in feature.SearchGroups)
            {
                for (int i = 0; i < searchGroup.ItemCount; i++)
                {
                    ISearchableEntity entity = GetSearchEntity(feature);
                    if (entity != null)
                    {
                        entity.SearchKey = searchGroup.SearchKey;
                        searchEntities.Add(entity);
                    }
                }
            }
            return searchEntities;
        }

        #endregion

        private static List<FilterGroup> GetSampleFilterGroups()
        {
            var result = new List<FilterGroup>();

            for (int groupIndex = 1; groupIndex <= 3; groupIndex++)
            {
                FilterGroup group = new FilterGroup { Name = "Filter Group " + groupIndex, Sort = (short)groupIndex };
                for (int filterIndex = 1; filterIndex <= 5; filterIndex++)
                {
                    string id = String.Format("{0}-{1}", groupIndex, filterIndex);
                    Filter filter = new Filter
                    {
                        Name = "Filter " + id,
                        Code = "Code " + id,
                        Key = (groupIndex * 100) + filterIndex,
                        Sort = (short)filterIndex,
                        IsSelected = (filterIndex == 1)
                    };
                    group.Filters.Add(filter);
                }
                result.Add(group);
            }

            return result;
        }

        #region Price Lists

        private static List<AnalyticPriceListGroup> GetSampleAnalyticPriceListGroups()
        {
            var result = new List<AnalyticPriceListGroup>();

            for (int groupIndex = 1; groupIndex <= 3; groupIndex++)
            {
                AnalyticPriceListGroup group = new AnalyticPriceListGroup { Name = "Price List Group " + groupIndex, Sort = (short)groupIndex };
                group.PriceLists = GetSamplePriceLists(groupIndex, 3);
                result.Add(group);
            }

            return result;
        }

        private static List<PricingEverydayPriceListGroup> GetSamplePricingEverydayPriceListGroups(PricingMode mode)
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
                    group.PriceLists = GetSampleEverydayPriceLists(isLinkedGroup);
                    result.Add(group);
                }
                else if (linkedKey > 0 && linkedKey != key)
                {
                    group = new PricingEverydayPriceListGroup { Name = "Price List Group " + groupIndex, Sort = (short)groupIndex, Key = linkedKey };
                    group.PriceLists = GetSampleEverydayPriceLists(isLinkedGroup);
                    result.Add(group);
                }
            }

            return result;
        }

        private static List<PricingLinkedPriceListRule> GetSampleLinkedPriceListRules(PricingEveryday priceRoutine)
        {
            var rules = new List<PricingLinkedPriceListRule>();

            var uniquePriceListIds = priceRoutine.PriceListGroups.SelectMany(grp => grp.PriceLists).Select(pl => pl.Id).Distinct();
            foreach (int id in uniquePriceListIds)
            {
                int percentChange = _random.Next(3, 15);
                var rule = new PricingLinkedPriceListRule { PercentChange = percentChange, PriceListId = id };
                rules.Add(rule);
            }
            return rules;
        }

        private static List<PriceList> GetSamplePriceLists(int groupIndex, int count)
        {
            var result = new List<PriceList>();

            for (int priceListIndex = 1; priceListIndex <= count; priceListIndex++)
            {
                string id = String.Format("{0}-{1}", groupIndex, priceListIndex);
                PriceList priceList = new PriceList
                {
                    Name = "Price List " + id,
                    Code = "Code " + id,
                    Key = (groupIndex * 100) + priceListIndex,
                    Sort = (short)priceListIndex,
                    IsSelected = (priceListIndex == 1)
                };
                result.Add(priceList);
            }

            return result;
        }

        private static List<PricingEverydayPriceList> GetSampleEverydayPriceLists(bool isLinkedGroup)
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

        private static PriceListGroup GetPriceListGroup(ISearchableEntity entity)
        {
            PriceListGroup result = null;
            if (entity is Analytic)
            {
                result = new AnalyticPriceListGroup();
            }

            else if (entity is PricingEveryday)
            {
                result = new PricingEverydayPriceListGroup();
            }
            return result;
        }

        #endregion

        private static List<AnalyticValueDriver> GetSampleAnalyticDrivers()
        {
            var result = new List<AnalyticValueDriver>();

            string[] driverNames = { "Markup", "Movement", "Days On Hand" };

            for (int driverIndex = 0; driverIndex < driverNames.Length; driverIndex++)
            {
                ValueDriverGroup group;
                var driver = new AnalyticValueDriver { Id = driverIndex + 21, Name = driverNames[driverIndex], Sort = (short)driverIndex };
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
                    group = new ValueDriverGroup { Id = groupIndex, Value = (short)groupIndex, MinOutlier = minOutlier, MaxOutlier = minOutlier + 1, Sort = (short)groupIndex };
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
                    group = new ValueDriverGroup { Id = groupIndex, Value = (short)groupIndex, MinOutlier = minOutlier, MaxOutlier = minOutlier + 1, Sort = (short)groupIndex };
                    mode.Groups.Add(group);
                }
                driver.Modes.Add(mode);

                result.Add(driver);
            }

            return result;
        }


        /// <summary>
        /// Factory method for getting searchable entities.
        /// </summary>
        /// <param name="feature"></param>
        /// <returns>A concrete instance that implements ISearchableEntity.</returns>
        private static ISearchableEntity GetSearchEntity(ModuleFeature feature)
        {
            int id = _random.Next(101, 10001);

            ISearchableEntity result = null;
            switch (feature.TypeId)
            {
                case APLPX.Client.Entity.ModuleFeatureType.PlanningAnalytics:
                    result = GetSampleAnalytic(id);
                    break;

                case APLPX.Client.Entity.ModuleFeatureType.PlanningEverydayPricing:
                    result = GetSamplePricingEveryday(id);
                    break;

                case APLPX.Client.Entity.ModuleFeatureType.PlanningPromotionPricing:
                    result = GetSamplePricingPromotion(id);
                    break;

                case APLPX.Client.Entity.ModuleFeatureType.PlanningKitPricing:
                    result = GetSamplePricingKits(id);
                    break;

                case APLPX.Client.Entity.ModuleFeatureType.AdministrationUserMaintenance:
                    result = GetSampleUser(id);
                    break;

                default:
                    break;
            }

            if (result != null)
            {
                result.Id = id;
            }

            return result;
        }

        private static Analytic GetSampleAnalytic(int id)
        {
            var result = new Analytic();
            string name = "Analytic #" + id;
            result.Identity.Name = name;
            result.Identity.Description = String.Format("Sample description for {0}", name);
            result.Identity.Notes = String.Format("Here are are some sample notes that were entered for this item (\"{0}\").", name);
            result.Identity.Created = DateTime.Now.AddDays(-10);
            result.Identity.Edited = DateTime.Now.AddDays(-2);
            result.Identity.Owner = name + " Owner";

            result.FilterGroups = GetSampleFilterGroups();
            result.PriceListGroups = GetSampleAnalyticPriceListGroups();
            result.ValueDrivers = GetSampleAnalyticDrivers();

            return result;
        }

        private static PricingEveryday GetSamplePricingEveryday(int id)
        {
            var result = new PricingEveryday();
            result.Identity = GetSamplePricingIdentity("Pricing Everyday", id);

            result.FilterGroups = GetSampleFilterGroups();
            result.PricingModes = GetSamplePricingModes();
            foreach (PricingMode mode in result.PricingModes)
            {
                result.PriceListGroups = GetSamplePricingEverydayPriceListGroups(mode);
            }

            result.KeyPriceListRule = new PricingKeyPriceListRule { DollarRangeLower = 10.25M, DollarRangeUpper = 115.00M };
            result.LinkedPriceListRules = GetSampleLinkedPriceListRules(result);

            return result;
        }

        private static PricingPromotion GetSamplePricingPromotion(int id)
        {
            var result = new PricingPromotion();
            result.Identity = GetSamplePricingIdentity("Pricing Promotion", id);

            return result;
        }

        private static PricingKits GetSamplePricingKits(int id)
        {
            var result = new PricingKits();
            result.Identity = GetSamplePricingIdentity("Pricing Kits", id);

            return result;
        }

        private static PricingIdentity GetSamplePricingIdentity(string entityTypeName, int id)
        {
            PricingIdentity result = new PricingIdentity();
            string name = String.Format("{0} #{1}", entityTypeName, id);

            result.Name = name;
            result.Description = String.Format("Sample description for {0}", name);
            result.Notes = String.Format("Here are are some sample notes that were entered for this item (\"{0}\").", name);
            result.Created = DateTime.Now.AddDays(-8);
            result.Edited = DateTime.Now.AddDays(-3);
            result.Owner = name + " Owner";

            return result;
        }

        private static User GetSampleUser(int id)
        {
            var result = new User();
            string name = "User #" + id;

            result.Identity.Name = name;
            result.Identity.FirstName = String.Format("{0} First Name", name);
            result.Identity.LastName = String.Format("{0} Last Name", name);
            result.Identity.Created = DateTime.Now.AddDays(-21);
            result.Identity.Edited = DateTime.Now.AddDays(-11);

            return result;
        }

        private static List<DisplayEntities.Action> GetSampleActions(ModuleFeatureStep step)
        {
            var result = new List<DisplayEntities.Action>();
            switch (step.TypeId)
            {
                case DTO.ModuleFeatureStepType.Null:
                    break;

                case DTO.ModuleFeatureStepType.PlanningAnalyticsSearchAnalytics:
                    result.Add(new DisplayEntities.Action { Name = "Copy", ParentName = "Copy", Title = "Workflow View Step Action, Copy Analytics", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsCopy, Sort = 3, });
                    result.Add(new DisplayEntities.Action { Name = "Edit", ParentName = "Edit", Title = "Workflow View Step Action, Edit Analytics", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsEdit, Sort = 2, });
                    result.Add(new DisplayEntities.Action { Name = "New", ParentName = "New", Title = "Workflow View Step Action, New Analytics", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsNew, Sort = 1, });
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingSearchEveryday:
                    result.Add(new DisplayEntities.Action { Name = "Copy", ParentName = "Copy", Title = "Workflow View Step Action, Copy Everyday", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingSearchEverydayCopy, Sort = 3, });
                    result.Add(new DisplayEntities.Action { Name = "Edit", ParentName = "Edit", Title = "Workflow View Step Action, Edit Everyday", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingSearchEverydayEdit, Sort = 2, });
                    result.Add(new DisplayEntities.Action { Name = "New", ParentName = "New", Title = "Workflow View Step Action, New Everyday", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingSearchEverydayNew, Sort = 1, });
                    break;

                case DTO.ModuleFeatureStepType.PlanningPromotionPricingSearchPromotions:
                    result.Add(new DisplayEntities.Action { Name = "Copy", ParentName = "Copy", Title = "Workflow View Step Action, Copy Promotions", TypeId = DTO.ModuleFeatureStepActionType.PlanningPromotionPricingSearchPromotionsCopy, Sort = 3, });
                    result.Add(new DisplayEntities.Action { Name = "Edit", ParentName = "Edit", Title = "Workflow View Step Action, Edit Promotions", TypeId = DTO.ModuleFeatureStepActionType.PlanningPromotionPricingSearchPromotionsEdit, Sort = 2, });
                    result.Add(new DisplayEntities.Action { Name = "New", ParentName = "New", Title = "Workflow View Step Action, New Promotions", TypeId = DTO.ModuleFeatureStepActionType.PlanningPromotionPricingSearchPromotionsNew, Sort = 1, });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningAnalyticsIdentity:
                    result.Add(new DisplayEntities.Action { Name = "Cancel", Title = "Discard all changes since the last save.", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsIdentityCancel, Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsIdentitySave, Sort = 4 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingIdentity:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingIdentitySave, Sort = 4 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningAnalyticsFilters:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsFiltersSave, Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Run", Title = "Run calculations", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsFiltersRun, Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Cancel", Title = "Discard all changes since the last save.", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsFiltersCancel, Sort = 3 });
                    break;
                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingFilters:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingFiltersSave, Sort = 4 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningAnalyticsPriceLists:
                    result.Add(new DisplayEntities.Action { Name = "Run", Title = "Run calculations", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsPriceListsRun, Sort = 3 });
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsPriceListsSave, Sort = 4 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningAnalyticsValueDrivers:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversSave, Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Cancel", Title = "Discard all changes since the last save.", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversCancel, Sort = 2 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingPriceLists:
                    result.Add(new DisplayEntities.Action { Name = "Run", Title = "Run calculations", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingPriceListsSave, Sort = 1 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingRounding:
                    result.Add(new DisplayEntities.Action { Name = "Run", Title = "Run calculations", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingRoundingSave, Sort = 1 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningAnalyticsResults:
                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingResults:
                    result.Add(new DisplayEntities.Action { Name = "Show Table", Title = "Show the results in a table.", Sort = 1 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingStrategy:
                    result.Add(new DisplayEntities.Action { Name = "Clear", Title = "Discard all changes since the last save.", Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Full Screen", Title = "Maximize this screen", Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Edit", Title = "Edit this item", Sort = 3 });
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", Sort = 4 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingForecast:
                    result.Add(new DisplayEntities.Action { Name = "Add Forecast", Title = "Add Forecast (title goes here)", Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Delete", Title = "Delete (title goes here)", Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Full Screen", Title = "Maximize this screen", Sort = 2 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingApproval:
                    result.Add(new DisplayEntities.Action { Name = "Export", Title = "Export (title goes here)", Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Options", Title = "Options (title goes here)", Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Submit", Title = "Submit (title goes here)", Sort = 3 });
                    break;

                default:
                    result.Add(new DisplayEntities.Action { Name = "Step 1", Title = "Step 1 (title goes here)", Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Step 2", Title = "Step 2 (title goes here)", Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Step 3", Title = "Step 3 (title goes here)", Sort = 3 });
                    break;
            }

            return result;
        }

        private static List<PricingMode> GetSamplePricingModes()
        {
            var result = new List<PricingMode>();
            result.Add(new PricingMode
            {
                Key = 19,
                Name = "One key ",
                Title = "Single price list key type selected ",
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
                Name = "Global key ",
                Title = "Global price list key type selected, user defined percent change ",
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
                Name = "Global key + ",
                Title = "Global price list key + selected, using existing percent change ",
                IsSelected = false,
                HasKeyPriceListRule = true,
                HasLinkedPriceListRule = true,
                KeyPriceListGroupKey = 7,
                LinkedPriceListGroupKey = 7,
                Sort = 3

            });
            result.Add(new PricingMode
            {
                Key = 22,
                Name = "Cascade ",
                Title = "Cascading price list key type selected, using hierarchical percent change ",
                IsSelected = false,
                HasKeyPriceListRule = true,
                HasLinkedPriceListRule = true,
                KeyPriceListGroupKey = 7,
                LinkedPriceListGroupKey = 8,
                Sort = 4

            });

            return result;
        }

        #region Pricing Rules

        private static List<PriceRoundingRule> GetRoundingRules()
        {
            var list = new List<PriceRoundingRule>();
            var lookups = GetRoundingRuleTypes();

            for (int i = 0; i < 10; i++)
            {              
                list.Add(new PriceRoundingRule { Id = 101, DollarRangeLower = 0.01M, DollarRangeUpper = 20M, ValueChange = 0.89M,  Type = 1, RoundingTypes = lookups });
                list.Add(new PriceRoundingRule { Id = 102, DollarRangeLower = 20.01M, DollarRangeUpper = 25M, ValueChange = 0.99M, Type = 2, RoundingTypes = lookups });
                list.Add(new PriceRoundingRule { Id = 103, DollarRangeLower = 25.01M, DollarRangeUpper = 30M, ValueChange = 0.79M,Type = 3, RoundingTypes = lookups });
                list.Add(new PriceRoundingRule { Id = 104, DollarRangeLower = 30.01M, DollarRangeUpper = 75M, ValueChange = 0.99M,Type = 1, RoundingTypes = lookups });
                list.Add(new PriceRoundingRule { Id = 105, DollarRangeLower = 75.01M, DollarRangeUpper = 100M, ValueChange = 0.99M,Type = 2, RoundingTypes = lookups });
                list.Add(new PriceRoundingRule { Id = 106, DollarRangeLower = 100.01M, DollarRangeUpper = 110M, ValueChange = 0.99M,Type = 1, RoundingTypes = lookups });
                list.Add(new PriceRoundingRule { Id = 107, DollarRangeLower = 110.01M, DollarRangeUpper = 500M, ValueChange = 0.99M,Type = 3, RoundingTypes = lookups });
                list.Add(new PriceRoundingRule { Id = 108, DollarRangeLower = 500.01M, DollarRangeUpper = 800M, ValueChange = 0.99M,Type = 2, RoundingTypes = lookups });
                list.Add(new PriceRoundingRule { Id = 109, DollarRangeLower = 800.01M, DollarRangeUpper = 1000M, ValueChange = 0.99M,Type = 1, RoundingTypes = lookups });
                list.Add(new PriceRoundingRule { Id = 110, DollarRangeLower = 1000.01M, DollarRangeUpper = 99999M, ValueChange = 0.99M, Type = 3, RoundingTypes = lookups });
            }

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

        private static List<SQLEnumeration> GetRoundingRuleTypes()
        {
            var list = new List<SQLEnumeration>();

            list.Add(new SQLEnumeration { Name = "Round Nearest", Description = "Round to the nearest value.", Value = 1, Sort = 1 });
            list.Add(new SQLEnumeration { Name = "Round Up", Description = "Round up to the next value.", Value = 2, Sort = 2 });
            list.Add(new SQLEnumeration { Name = "Round Down", Description = "Round down to the next value.", Value = 3, Sort = 3 });

            return list;
        }

        #endregion

        #region Pricing Results

        List<PricingEverydayResult> GetPricingEverydayResults()
        {
            var list = new List<PricingEverydayResult>();
            list.Add(new PricingEverydayResult { SkuId = 123, SkuName = "CE01402", SkuTitle = "1959-60 Cadillac Pitman Arm" });
            list.Add(new PricingEverydayResult { SkuId = 126, SkuName = "PSP0013", SkuTitle = "Pump, Power Steering, 75-76 Cadillac, New" });
            list.Add(new PricingEverydayResult { SkuId = 288, SkuName = "CE12161", SkuTitle = "1968-70 Cadillac Power Steering Pump & Reservoir" });
            list.Add(new PricingEverydayResult { SkuId = 604, SkuName = "CE11863", SkuTitle = "1954-56 Cadillac 3 Front Low Profile Springs" });
            list.Add(new PricingEverydayResult { SkuId = 612, SkuName = "CE01363", SkuTitle = "Outer Tie Rod, 1961-63 PONT/1963-69 Cadillac/1963 Skylark" });
            list.Add(new PricingEverydayResult { SkuId = 696, SkuName = "CE11871", SkuTitle = "1961-64 Cadillac 2 Front Low Profile Springs" });
            list.Add(new PricingEverydayResult { SkuId = 959, SkuName = "PSP0053", SkuTitle = "Pump, Power Steering, 60-67 Multi, New" });
            list.Add(new PricingEverydayResult { SkuId = 991, SkuName = "CE01472", SkuTitle = "1961-64 Cadillac Rear Control Arm Bushing, upper" });
            list.Add(new PricingEverydayResult { SkuId = 1352, SkuName = "CH28562", SkuTitle = "Flaming River Tilt Steering Column Acc 1-48 x 3/4 - DD SS U-Joint" });
            list.Add(new PricingEverydayResult { SkuId = 1757, SkuName = "CE12163", SkuTitle = "1975-76 Cadillac Power Steering Pump & Reservoir" });
            list.Add(new PricingEverydayResult { SkuId = 1771, SkuName = "CE01523", SkuTitle = "1976 Seville (diesel) Front Coil Springs" });
            list.Add(new PricingEverydayResult { SkuId = 2123, SkuName = "ADD0943", SkuTitle = "1971-76 Eldorado/Bonneville/Catalina 1 Rear Anti-Sway Bar" });
            list.Add(new PricingEverydayResult { SkuId = 2165, SkuName = "CE03380", SkuTitle = "1965-67 Cadillac Transmission Mount exc. 1967 Eldorado" });
            list.Add(new PricingEverydayResult { SkuId = 2337, SkuName = "C980100", SkuTitle = "64-7 KYB REAR GAS-A-JUST SHOCK" });
            list.Add(new PricingEverydayResult { SkuId = 2379, SkuName = "ADD0930", SkuTitle = "1965-70 Eldorado 3/4 Rear Anti-Sway Bar" });
            list.Add(new PricingEverydayResult { SkuId = 2516, SkuName = "CE01334", SkuTitle = "1967-76 Eldorado Upper Control Arm Bushing" });
            list.Add(new PricingEverydayResult { SkuId = 2601, SkuName = "CE12041", SkuTitle = "1975-76 Seville Gas-A-Just Shocks" });
            list.Add(new PricingEverydayResult { SkuId = 2608, SkuName = "CE01398", SkuTitle = "1976 Cadillac Seville Idler Arm" });
            list.Add(new PricingEverydayResult { SkuId = 2738, SkuName = "CE12164", SkuTitle = "1976 Cadillac Eldorado, CC, Limo & Series 75 Power Steering Pump & Reservoir" });  


            return list;
        }

        List<PricingEverydayResultPriceList> GetPricingEverydayResultPricelists()
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
                Name = "Name1",
                Title = "Title1",
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
                Name = "Name2",
                Title = "Title2",
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
                Name = "Name3",
                Title = "Title3",
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
                Name = "Name4",
                Title = "Title4",
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
                Name = "Name5",
                Title = "Title5",
                IsSelected = false,
                Sort = 5
            });

            return list;
        }
        #endregion
    }

}
