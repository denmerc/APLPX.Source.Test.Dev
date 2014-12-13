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

        #region Sample Data generation

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
                feature.SearchableEntities = GetSampleSearchEntties(feature);
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
                else
                {
                    steps.Add(new ModuleFeatureStep { Name = "Search", Title = "Search saved price routines", Sort = 1 }); //TODO: add enum value for search.
                    steps.Add(new ModuleFeatureStep { Name = "Identity", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingIdentity, Title = "Identify Price routines with a unique name and description", Sort = 2 });
                    steps.Add(new ModuleFeatureStep { Name = "Filters", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingFilters, Title = "Configure Price routine product filters & define a product set", Sort = 3 });
                    steps.Add(new ModuleFeatureStep { Name = "Price Lists", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingPriceLists, Title = "Configure Price routine price lists ", Sort = 4 });
                    steps.Add(new ModuleFeatureStep { Name = "Rounding", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingRounding, Title = "Configure Price routine rounding", Sort = 5 });
                    steps.Add(new ModuleFeatureStep { Name = "Strategy", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingStrategy, Title = "Configure Price routine optimization strategy", Sort = 6 });
                    steps.Add(new ModuleFeatureStep { Name = "Results", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingResults, Title = "Compare and edit Price routine results", Sort = 7 });
                    steps.Add(new ModuleFeatureStep { Name = "Forecast", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingForecast, Title = "Create a Price routine forecast", Sort = 8 });
                    steps.Add(new ModuleFeatureStep { Name = "Request Approval", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingApproval, Title = "Submit this Price routine for approval", Sort = 9 });
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

        private static List<ISearchableEntity> GetSampleSearchEntties(ModuleFeature feature)
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

        private List<FilterGroup> GetSampleFilterGroups()
        {
            var result = new List<FilterGroup>();

            for (int groupIndex = 1; groupIndex <= 3; groupIndex++)
            {
                FilterGroup group = new FilterGroup { TypeName = "Filter Group " + groupIndex, Sort = (short)groupIndex };
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

        private List<AnalyticPriceListGroup> GetSamplePriceListGroups()
        {
            var result = new List<AnalyticPriceListGroup>();

            for (int groupIndex = 1; groupIndex <= 3; groupIndex++)
            {
                AnalyticPriceListGroup group = new AnalyticPriceListGroup { Name = "Price List Group " + groupIndex, Sort = (short)groupIndex };

                for (int priceListIndex = 1; priceListIndex <= 5; priceListIndex++)
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
                    group.PriceLists.Add(priceList);
                }
                result.Add(group);
            }

            return result;
        }

        private List<AnalyticValueDriver> GetSampleAnalyticDrivers()
        {
            var result = new List<AnalyticValueDriver>();
            string[] driverNames = { "Markup", "Movement", "Days On Hand" };
            for (int driverIndex = 0; driverIndex < driverNames.Length; driverIndex++)
            {
                var driver = new AnalyticValueDriver { Id = driverIndex + 21, Name = driverNames[driverIndex], Sort = (short)driverIndex };
                var mode = new AnalyticValueDriverMode
                {
                    Name = "Auto Generated groups",
                    Key = 29,
                    Sort = 0,
                    IsSelected = true
                };
                ValueDriverGroup group = new ValueDriverGroup { Id = 68, Value = 3, MinOutlier = 0, MaxOutlier = 5, Sort = 3 };

                mode = new AnalyticValueDriverMode
                {
                    Name = "User defined groups",
                    Key = 30,
                    Sort = 1,
                    IsSelected = false
                };
                int minOutlier = 0;
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

        #endregion

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
                    result = GetSamplePricingEveryday(id, "Everyday");
                    break;

                case APLPX.Client.Entity.ModuleFeatureType.PlanningPromotionPricing:
                    result = GetSamplePricingPromotion(id, "Promotion");
                    break;

                case APLPX.Client.Entity.ModuleFeatureType.PlanningKitPricing:
                    //result = GetSamplePriceRoutine(id, "Kit");
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

            result.Identity.Name = "Analytic #" + id;
            result.Identity.Description = String.Format("Sample description for Analytic #{0}", id);
            result.Identity.Notes = String.Format("Here are are some sample notes that were entered for this Analytic (#{0}).", id);
            result.Identity.Created = DateTime.Now.AddDays(-10);
            result.Identity.Edited = DateTime.Now.AddDays(-2);

            return result;
        }

        private static PricingEveryday GetSamplePricingEveryday(int id, string name)
        {
            var result = new PricingEveryday();

            result.Identity.Name = String.Format("{0} Pricing #{1}", name, id);
            result.Identity.Description = String.Format("Sample description for {0} Pricing #{1}", name, id);
            result.Identity.Notes = String.Format("These are some sample notes that were entered for this {0} Pricing #{1}", name, id);
            result.Identity.Created = DateTime.Now.AddDays(-8);
            result.Identity.Edited = DateTime.Now.AddDays(-3);

            return result;
        }

        private static PricingPromotion GetSamplePricingPromotion(int id, string name)
        {
            var result = new PricingPromotion();

            result.Identity.Name = String.Format("{0} Price Routine #{1}", name, id);
            result.Identity.Description = String.Format("Sample description for {0} Price Routine #{1}", name, id);
            result.Identity.Notes = String.Format("These are some sample notes that were entered for this {0} Price Routine #{1}", name, id);
            result.Identity.Created = DateTime.Now.AddDays(-8);
            result.Identity.Edited = DateTime.Now.AddDays(-3);

            return result;
        }

        private static User GetSampleUser(int id)
        {
            var result = new User();

            result.Identity.Name = "User #" + id;
            result.Identity.FirstName = String.Format("User #{0} First Name", id);
            result.Identity.LastName = String.Format("User #{0} Last Name", id);
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


                    result.Add(new DisplayEntities.Action { Name = "Clear", Title = "Discard all changes since the last save.", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsIdentityCancel, Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsIdentitySave, Sort = 4 });
                    result.Add(new DisplayEntities.Action { Name = "Add Analytic", Title = "Add a new Analytic", Sort = 1 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingIdentity:
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningAnalyticsFilters:
                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingFilters:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", Sort = 4 });
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningAnalyticsPriceLists:
                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningAnalyticsValueDrivers:
                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingPriceLists:
                case APLPX.Client.Entity.ModuleFeatureStepType.PlanningEverydayPricingRounding:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", Sort = 1 });
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

    }

}
