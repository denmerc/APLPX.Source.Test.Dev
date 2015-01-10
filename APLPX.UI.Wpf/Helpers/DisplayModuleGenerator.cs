using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;
using DTO = APLPX.Client.Entity;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    /// Helper class for generating mock Module display objects, including their child objects.
    /// </summary>
    public class DisplayModuleGenerator
    {
        #region Private Fields

        private static Random _random = new Random();

        private static string[] _analyticNames = { "Admin - Everyday - All Filters - Movement Only", "Admin - Everyday - Movement & Markup", "Admin - Everyday - Movement & Days On Hand", "Analyst - Promo - Movement Markup Days On Hand ", "Analyst - Promo - Movement with Outliers", "Admin - Everyday - Movement with Manual Groups", "Analyst - Promo - Movement & Markup w/Man Grps ", "Admin - Movement, Markup, & DOH w/Manual Groups", "Admin - Movement, MarkUp, & DOH w/Custom - 1 prod Description", "Analyst - Promo - All Drivers One-to-many Analytic", "Approver - Everyday - All Drivers One-to-One Analysis", "Approver - Everyday - Movement & Markup w/small Filters", "Approver - Everyday - Movement & DOH w/small Filters", "Approver - Everyday - Movement w/Pontiac Filters", "Approver - Everyday - Move. w/Caddy filt & 15 grps" };

        private static string[] _pricingEverydayNames = { "Admin - Everyday - All Filters - Movement Only", "Admin - Everyday - Movement & Days On Hand", "Admin - Everyday - Movement & Markup", "Admin - Everyday - Movement with Manual Groups", "Admin - Everyday - MarkUp, & DOH w/Custom - 1 Prod", "Admin - Everyday - Movement, Markup, & DOH+ManGrps", "Analyst Everyday All Drivers One-to-many Analytic1", "Analyst Everyday All Drivers one-to-many Analytic2", "Analyst Everyday All Drivers one-to-many Analytic3", "Analyst - Everyday - Movement & Markup w/ManGrps", "Analyst - Everyday - Movement with Outliers", "Approver - All Drivers - one-to-one Analytic", "Approver - Everyday - Movement & DOH w/small Filt", "Approver - Everyday - Movement & Markup w/sml Filt", "Approver - Everyday - Movement w/Pontiac Filters", "Approver - Everyday - Move. w/Caddy filt & 15 grps", "Analyst - Everyday - All Driver Analytic mismatch", "Approver - Everyday - all driver mismatch", "Admin - Everyday - Analytic Mismatch + global" };


        private static string[] _ownerNames = { "Admin User Active", "Admin User Active", "Admin User Active", "Analyst User Active", "Analyst User Active", "Admin User Active", "Analyst User Active", "Admin User Active", "Admin User Active", "Analyst User Active", "Approver User Active", "Approver User Active", "Approver User Active", "Approver User Active", "Approver User Active" };


        #endregion
       
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
                Module module = new Module { Name = names[i], TypeId = types[i], Title = names[i] };    //Formally "short description..."
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

            SetSourceAnalyticName(features);

            return features;
        }

        private static void SetSourceAnalyticName(List<ModuleFeature> features)
        {
            //For display purposes, use the first analytic as the source for each price routine
            Analytic sourceAnalytic = null;
            var analytics = features.FirstOrDefault(f => f.Name == "Analytics");
            if (analytics != null && analytics.SearchableEntities.Count > 0)
            {
                sourceAnalytic = analytics.SearchableEntities[0] as Analytic;
                if (sourceAnalytic != null)
                {
                    var everyday = features.FirstOrDefault(f => f.Name == "Everyday Pricing");
                    if (everyday != null)
                    {
                        foreach (PricingEveryday item in everyday.SearchableEntities)
                        {
                            item.Identity.AnalyticName = sourceAnalytic.Identity.Name;
                        }
                    }
                }
            }
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
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "7) Impact Analysis", Title = "Create a Price routine forecast", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingForecast, Sort = 8, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "8) Request Approval", Title = "Submit this Price routine for approval", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingApproval, Sort = 9, });
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
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "7) Impact Analysis", Title = "Create a Price routine forecast", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingForecast, Sort = 8, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "8) Request Approval", Title = "Submit this Price routine for approval", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingApproval, Sort = 9, });
                }
                else if (feature.TypeId == DTO.ModuleFeatureType.PlanningKitPricing)
                {
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "Search Kits", Title = "Search saved Pricing campaigns", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingSearchKits, Sort = 1, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "1) Identity", Title = "Identify Price routines with a unique name and description", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingIdentity, Sort = 2, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "2) Filters", Title = "Configure Price routine product filters & define a product set", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingFilters, Sort = 3, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "3) Price Lists", Title = "Configure Price routine price lists ", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingPriceLists, Sort = 4, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "4) Rounding", Title = "Configure Price routine rounding", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingRounding, Sort = 5, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "5) Strategy", Title = "Configure Price routine optimization strategy", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingStrategy, Sort = 6, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "6) Results", Title = "Compare and edit Price routine results", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingResults, Sort = 7, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "7) Impact Analysis", Title = "Create a Price routine forecast", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingForecast, Sort = 8, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "8) Request Approval", Title = "Submit this Price routine for approval", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingApproval, Sort = 9, });
                }
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
                //searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "John Doe", SearchKey = "seerchKey101", Sort = 4, ItemCount = 4 });
                //searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "Jane Doe", SearchKey = "seerchKey102", Sort = 5, ItemCount = 4 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Scenarios", Name = "Scenario #1", SearchKey = "searchKey112", Sort = 6, ItemCount = 4 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Scenarios", Name = "Scenario #1A", SearchKey = "searchKey113", Sort = 7, ItemCount = 2 });
            }
            else if (feature.Name == "Everyday")
            {
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Recent", Name = "Recent", SearchKey = "searchKey001", Sort = 1, ItemCount = 2 });
                //searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "User A", SearchKey = "searchKey101", Sort = 2, ItemCount = 0 });
                //searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "User B", SearchKey = "searchKey102", Sort = 3, ItemCount = 0 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 1", SearchKey = "searchKey111", Sort = 4, ItemCount = 4 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 2", SearchKey = "searchKey112", Sort = 5, ItemCount = 2 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 3", SearchKey = "searchKey113", Sort = 6, ItemCount = 0 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 4", SearchKey = "searchKey114", Sort = 7, ItemCount = 3 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 4", SearchKey = "searchKey115", Sort = 8, ItemCount = 2 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 5", SearchKey = "searchKey116", Sort = 9, ItemCount = 1 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 6", SearchKey = "searchKey117", Sort = 10, ItemCount = 3 });
            }
            else
            {
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Recent", Name = "Recent", SearchKey = "searchKey001", Sort = 1, ItemCount = 5 });
                //searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "User 1", SearchKey = "searchKey101", Sort = 2, ItemCount = 2 });
                //searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "User 2", SearchKey = "searchKey102", Sort = 3, ItemCount = 1 });
                //searchGroups.Add(new FeatureSearchGroup { ParentName = "Shared", Name = "User 3", SearchKey = "searchKey103", Sort = 4, ItemCount = 3 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 1", SearchKey = "searchKey111", Sort = 5, ItemCount = 3 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 2", SearchKey = "searchKey112", Sort = 6, ItemCount = 7 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 3", SearchKey = "searchKey113", Sort = 7, ItemCount = 0 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 4", SearchKey = "searchKey114", Sort = 8, ItemCount = 2 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 5", SearchKey = "searchKey115", Sort = 9, ItemCount = 1 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Folders", Name = "Folder 6", SearchKey = "searchKey116", Sort = 10, ItemCount = 3 });
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
                    ISearchableEntity entity = GetSearchEntity(feature, i);
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

        #region Price Lists

        private static List<AnalyticPriceListGroup> GetSampleAnalyticPriceListGroups()
        {
            var result = new List<AnalyticPriceListGroup>();
            string[] grouoNames = { "Regular" };

            for (int groupIndex = 0; groupIndex < grouoNames.Length; groupIndex++)
            {
                string name = grouoNames[groupIndex];
                AnalyticPriceListGroup group = new AnalyticPriceListGroup { Name = name, Sort = (short)groupIndex, Title = name + " title" };
                group.PriceLists = GetSamplePriceLists(groupIndex);
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
                    var priceLists = GetSampleEverydayPriceLists(isLinkedGroup);
                    group.PriceLists = new ReactiveList<PricingEverydayPriceList>(priceLists);
                    result.Add(group);
                }
                else if (linkedKey > 0 && linkedKey != key)
                {
                    group = new PricingEverydayPriceListGroup { Name = "Price List Group " + groupIndex, Sort = (short)groupIndex, Key = linkedKey };
                    var priceLists = GetSampleEverydayPriceLists(isLinkedGroup);
                    group.PriceLists = new ReactiveList<PricingEverydayPriceList>(priceLists);
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
                var roundingRules = GetRoundingRules();
                var rule = new PricingLinkedPriceListRule { PercentChange = percentChange, PriceListId = id, RoundingRules = roundingRules };
                rules.Add(rule);
            }
            return rules;
        }

        private static List<PriceList> GetSamplePriceLists(int groupIndex)
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

        #endregion

        #region Value Drivers

        private static List<AnalyticValueDriver> GetSampleAnalyticDrivers()
        {
            var result = new List<AnalyticValueDriver>();

            string[] driverNames = { "Markup", "Movement", "Days On Hand" };

            

            AnalyticValueDriver driver;
            for (int driverIndex = 0; driverIndex < driverNames.Length; driverIndex++)
            {
                var analyticResults = GetSampleAnalyticResults(driverNames[driverIndex]);

                ValueDriverGroup group;
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

        private static ReactiveList<PricingEverydayValueDriver> GetPricingEverydayValueDrivers()
        {
            var drivers = new ReactiveList<PricingEverydayValueDriver>();

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

        private static PricingEverydayKeyValueDriver GetPricingEverydayKeyValueDriver(PricingEverydayValueDriver sourceDriver)
        {
            var keyDriver = new PricingEverydayKeyValueDriver { ValueDriverId = sourceDriver.Id };

            foreach (PricingValueDriverGroup group in sourceDriver.Groups)
            {
                var keyDriverGroup = new PricingEverydayKeyValueDriverGroup { ValueDriverGroupId = group.Id };
                keyDriverGroup.OptimizationRules = GetPriceOptimizationRules();
                keyDriverGroup.MarkupRules = GetMarkupRules();
                keyDriver.Groups.Add(keyDriverGroup);
            }

            return keyDriver;
        }


        private static List<PricingEverydayLinkedValueDriver> GetPricingEverydayLinkedValueDrivers(IEnumerable<PricingEverydayValueDriver> sourceDrivers)
        {
            var result = new List<PricingEverydayLinkedValueDriver>();
            int index = 0;
            foreach (PricingEverydayValueDriver sourceDriver in sourceDrivers)
            {
                var linkedDriver = new PricingEverydayLinkedValueDriver { ValueDriverId = sourceDriver.Id, Name = sourceDriver.Name };
                linkedDriver.Groups.Add(new PricingEverydayLinkedValueDriverGroup { ValueDriverGroupId = 1, PercentChange = 0.1M + index });
                result.Add(linkedDriver);
                index++;
            }

            return result;
        }

        private static List<PricingResultDriverGroup> GetPricingResultDriverGroups()
        {
            var list = new List<PricingResultDriverGroup>();
            string title = "Product Analytic metric driver is based on Aggregate units sold";
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "0", SkuCount = 20928, SalesValue = "0", Id = 5, Value = 5, MinOutlier = 0, MaxOutlier = 8, Sort = 5 });
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "0", SkuCount = 20928, SalesValue = "0", Id = 5, Value = 5, MinOutlier = 0, MaxOutlier = 8, Sort = 5 });
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "4", SkuCount = 20928, SalesValue = "759.96", Id = 5, Value = 5, MinOutlier = 0, MaxOutlier = 8, Sort = 5 });
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "0", SkuCount = 20928, SalesValue = "0", Id = 5, Value = 5, MinOutlier = 0, MaxOutlier = 8, Sort = 5 });
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "11", SkuCount = 1916, SalesValue = "1168.09", Id = 4, Value = 4, MinOutlier = 8, MaxOutlier = 23, Sort = 4 });
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "4", SkuCount = 20928, SalesValue = "1512.32", Id = 5, Value = 5, MinOutlier = 0, MaxOutlier = 8, Sort = 5 });
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "5", SkuCount = 20928, SalesValue = "1699.9", Id = 5, Value = 5, MinOutlier = 0, MaxOutlier = 8, Sort = 5 });
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "4", SkuCount = 20928, SalesValue = "455.16", Id = 5, Value = 5, MinOutlier = 0, MaxOutlier = 8, Sort = 5 });
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "1", SkuCount = 20928, SalesValue = "173.34", Id = 5, Value = 5, MinOutlier = 0, MaxOutlier = 8, Sort = 5 });
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "0", SkuCount = 20928, SalesValue = "0", Id = 5, Value = 5, MinOutlier = 0, MaxOutlier = 8, Sort = 5 });
            list.Add(new PricingResultDriverGroup { Name = "Movement", Title = title, Actual = "0", SkuCount = 20928, SalesValue = "0", Id = 5, Value = 5, MinOutlier = 0, MaxOutlier = 8, Sort = 5 });

            return list;
        }

        #endregion

        #region Analytic Results

        private static List<AnalyticResult> GetSampleAnalyticResults()
        {
            var list = new List<AnalyticResult>();

            list.Add(new AnalyticResult { DriverName = "Markup", MinValue = "1645", MaxValue = "19880", Id = 1, SalesValue = "16020.43", SkuCount = 20 });
            list.Add(new AnalyticResult { DriverName = "Markup", MinValue = "1215.26", MaxValue = "1563", Id = 2, SalesValue = "7574.79", SkuCount = 18 });
            list.Add(new AnalyticResult { DriverName = "Markup", MinValue = "802.9", MaxValue = "1181", Id = 3, SalesValue = "34918", SkuCount = 27 });
            list.Add(new AnalyticResult { DriverName = "Markup", MinValue = "331.67", MaxValue = "799", Id = 4, SalesValue = "67442.4", SkuCount = 42 });
            list.Add(new AnalyticResult { DriverName = "Markup", MinValue = "-24.95", MaxValue = "289.7", Id = 5, SalesValue = "16182.75", SkuCount = 19 });
            list.Add(new AnalyticResult { DriverName = "Movement", MinValue = "2298", MaxValue = "2298", Id = 1, SalesValue = "6848", SkuCount = 1 });
            list.Add(new AnalyticResult { DriverName = "Movement", MinValue = "674", MaxValue = "674", Id = 2, SalesValue = "4010.3", SkuCount = 1 });
            list.Add(new AnalyticResult { DriverName = "Movement", MinValue = "217", MaxValue = "411", Id = 3, SalesValue = "17583", SkuCount = 3 });
            list.Add(new AnalyticResult { DriverName = "Movement", MinValue = "102", MaxValue = "179", Id = 4, SalesValue = "35942", SkuCount = 12 });
            list.Add(new AnalyticResult { DriverName = "Movement", MinValue = "0", MaxValue = "98", Id = 5, SalesValue = "77734", SkuCount = 109 });
            list.Add(new AnalyticResult { DriverName = "Days On Hand", MinValue = "21", MaxValue = "90541", Id = 1, SalesValue = "74986.08", SkuCount = 58 });
            list.Add(new AnalyticResult { DriverName = "Days On Hand", MinValue = "13", MaxValue = "178", Id = 4, SalesValue = "1143.12", SkuCount = 1 });
            list.Add(new AnalyticResult { DriverName = "Days On Hand", MinValue = "48", MaxValue = "141", Id = 5, SalesValue = "65989.34", SkuCount = 67 });

            return list;
        }

        private static List<AnalyticResult> GetSampleAnalyticResults(string name)
        {
            var list = new List<AnalyticResult>();
            switch (name)
            {
                case "Markup":
                    list.Add(new AnalyticResult { DriverName = "Markup", MinValue = "1645", MaxValue = "19880", Id = 1, SalesValue = "16020.43", SkuCount = 20 });
                    list.Add(new AnalyticResult { DriverName = "Markup", MinValue = "1215.26", MaxValue = "1563", Id = 2, SalesValue = "7574.79", SkuCount = 18 });
                    list.Add(new AnalyticResult { DriverName = "Markup", MinValue = "802.9", MaxValue = "1181", Id = 3, SalesValue = "34918", SkuCount = 27 });
                    list.Add(new AnalyticResult { DriverName = "Markup", MinValue = "331.67", MaxValue = "799", Id = 4, SalesValue = "67442.4", SkuCount = 42 });
                    list.Add(new AnalyticResult { DriverName = "Markup", MinValue = "-24.95", MaxValue = "289.7", Id = 5, SalesValue = "16182.75", SkuCount = 19 });
                    break;

                case "Movement":
                    list.Add(new AnalyticResult { DriverName = "Movement", MinValue = "2298", MaxValue = "2298", Id = 1, SalesValue = "6848", SkuCount = 1 });
                    list.Add(new AnalyticResult { DriverName = "Movement", MinValue = "674", MaxValue = "674", Id = 2, SalesValue = "4010.3", SkuCount = 1 });
                    list.Add(new AnalyticResult { DriverName = "Movement", MinValue = "217", MaxValue = "411", Id = 3, SalesValue = "17583", SkuCount = 3 });
                    list.Add(new AnalyticResult { DriverName = "Movement", MinValue = "102", MaxValue = "179", Id = 4, SalesValue = "35942", SkuCount = 12 });
                    list.Add(new AnalyticResult { DriverName = "Movement", MinValue = "0", MaxValue = "98", Id = 5, SalesValue = "77734", SkuCount = 109 });
                    break;

                case "Days On Hand":
                    list.Add(new AnalyticResult { DriverName = "Days On Hand", MinValue = "21", MaxValue = "90541", Id = 1, SalesValue = "74986.08", SkuCount = 58 });
                    list.Add(new AnalyticResult { DriverName = "Days On Hand", MinValue = "13", MaxValue = "178", Id = 4, SalesValue = "1143.12", SkuCount = 1 });
                    list.Add(new AnalyticResult { DriverName = "Days On Hand", MinValue = "48", MaxValue = "141", Id = 5, SalesValue = "65989.34", SkuCount = 67 });
                    break;

                default:
                    break;
            }


            return list;
        }
        #endregion


        /// <summary>
        /// Factory method for getting searchable entities.
        /// </summary>
        /// <param name="feature"></param>
        /// <returns>A concrete instance that implements ISearchableEntity.</returns>
        private static ISearchableEntity GetSearchEntity(ModuleFeature feature, int id)
        {
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

            result.Id = id + 1;
            string name = _analyticNames[id];
            result.Identity.Name = name;
            result.Identity.Description = String.Format("Sample description for {0}", name);
            result.Identity.Notes = String.Format("Here are are some sample notes that were entered for this item (\"{0}\").", name);
            result.Identity.Created = DateTime.Now.AddDays(-10);
            result.Identity.Edited = DateTime.Now.AddDays(-2);
            result.Identity.Owner = _ownerNames[id];

            result.FilterGroups = MockFilterGenerator.GetFilterGroupsComplete();
            result.PriceListGroups = GetSampleAnalyticPriceListGroups();
            result.ValueDrivers = GetSampleAnalyticDrivers();

            //Default for display purposes.
            result.SelectedFilterGroup = result.FilterGroups.FirstOrDefault();

            return result;
        }

        private static PricingEveryday GetSamplePricingEveryday(int id)
        {

            var result = new PricingEveryday();

            result.Id = id + 1;

            result.Identity = GetSamplePricingIdentity(_pricingEverydayNames[id], _ownerNames[id]);

            result.FilterGroups = MockFilterGenerator.GetFilterGroupsComplete();
            result.PricingModes = GetSamplePricingModes();
            foreach (PricingMode mode in result.PricingModes)
            {
                result.PriceListGroups = GetSamplePricingEverydayPriceListGroups(mode);
            }

            result.KeyPriceListRule = new PricingKeyPriceListRule { DollarRangeLower = 10.25M, DollarRangeUpper = 115.00M, RoundingRules = GetRoundingRules() };
            result.LinkedPriceListRules = GetSampleLinkedPriceListRules(result);
            result.ValueDrivers = GetPricingEverydayValueDrivers();

            PricingEverydayValueDriver keyDriver = result.ValueDrivers.FirstOrDefault(driver => driver.IsKey);
            if (keyDriver != null)
            {
                PricingEverydayKeyValueDriver key = GetPricingEverydayKeyValueDriver(keyDriver);
                result.KeyValueDriver = key;
            }
            var linked = result.ValueDrivers.Where(driver => !driver.IsKey && driver.IsSelected);
            var linkedDrivers = GetPricingEverydayLinkedValueDrivers(linked);

            result.LinkedValueDrivers = new ObservableCollection<PricingEverydayLinkedValueDriver>(linkedDrivers);
            result.Results = GetSamplePricingEverydayResults();

            //Default for display purposes.
            result.SelectedFilterGroup = result.FilterGroups.FirstOrDefault();

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

        private static PricingIdentity GetSamplePricingIdentity(string name, string ownerName)
        {
            PricingIdentity result = new PricingIdentity();
            result.AnalyticName = "Analytic 5244";
            result.Name = name;
            result.Description = String.Format("Sample description for {0}", name);
            result.Notes = String.Format("Here are are some sample notes that were entered for this item (\"{0}\").", name);
            result.Created = DateTime.Now.AddDays(-8);
            result.Edited = DateTime.Now.AddDays(-3);
            result.Owner = ownerName;

            return result;
        }

        private static PricingIdentity GetSamplePricingIdentity(string entityTypeName, int id)
        {
            PricingIdentity result = new PricingIdentity();
            string name = String.Format("{0} #{1}", entityTypeName, id);
            result.AnalyticName = "Analytic 5244";
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
                    result.Add(new DisplayEntities.Action { Name = "Run", Title = "Calculate results.", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsResultsRun, Sort = 3 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingPriceLists:
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingRounding:
                    //result.Add(new DisplayEntities.Action { Name = "Run", Title = "Run calculations", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingRoundingSave, Sort = 1 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningAnalyticsResults:
                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingResults:
                    result.Add(new DisplayEntities.Action { Name = "Show Table", Title = "Show the results in a table.", Sort = 1 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingStrategy:
                    result.Add(new DisplayEntities.Action { Name = "Clear", Title = "Discard all changes since the last save.", Sort = 1 });
                    //result.Add(new DisplayEntities.Action { Name = "Full Screen", Title = "Maximize this screen", Sort = 2 });
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
                Name = "One key",
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
                Name = "Global key",
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
                Name = "Global key +",
                Title = "Global price list key + selected, using existing percent change ",
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
                Title = "Cascading price list key type selected, using hierarchical percent change ",
                IsSelected = false,
                HasKeyPriceListRule = true,
                HasLinkedPriceListRule = true,
                KeyPriceListGroupKey = 7,
                LinkedPriceListGroupKey = 7,
                Sort = 4

            });

            return result;
        }

        #region Pricing Rules

        public static List<PricingRoundingTemplate> GetSampleRoundingTemplates()
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

        private static List<SQLEnumeration> GetRoundingRuleTypes()
        {
            var list = new List<SQLEnumeration>();

            list.Add(new SQLEnumeration { Name = "Round Up", Description = "Psychological rounding up to change value", Value = 53, Sort = 1 });
            list.Add(new SQLEnumeration { Name = "Round Near", Description = "Psychological rounding nearest to change value", Value = 54, Sort = 2 });

            return list;
        }

        #endregion

        #region Pricing Results

        private static List<PricingEverydayResult> GetSamplePricingEverydayResults()
        {
            List<PricingEverydayResultPriceList> priceLists = GetSamplePricingEverydayResultPricelists();
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

        private static List<PricingEverydayResultPriceList> GetSamplePricingEverydayResultPricelists()
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

            list.ForEach(item => item.PriceEdit.Type = DTO.PricingResultsEditType.DefaultPrice);
            list.ForEach(item => item.PriceWarning.Type = DTO.PricingResultsWarningType.MarkupBelow);

            return list;
        }

        #endregion
    }

}
