using System;
using System.Collections.Generic;
using System.Linq;

using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Interfaces;

using DTO = APLPX.Entity;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    /// Helper class for generating mock Module display objects, including their child objects.
    /// </summary>
    public class DisplayModuleGenerator
    {
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
                Module module = new Module { Name = names[i], TypeId = types[i], Title = names[i] };
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
                case DTO.ModuleType.Planning:
                    features.Add(new ModuleFeature { Name = "Home", TypeId = DTO.ModuleFeatureType.PlanningHome });
                    features.Add(new ModuleFeature { Name = "Analytics", TypeId = DTO.ModuleFeatureType.PlanningAnalytics, Title = "Planning Value Driver Analytics " });
                    features.Add(new ModuleFeature { Name = "Everyday Pricing", TypeId = DTO.ModuleFeatureType.PlanningEverydayPricing, Title = "Planning Everyday Price changes and updates" });
                    features.Add(new ModuleFeature { Name = "Promotion Pricing", TypeId = DTO.ModuleFeatureType.PlanningPromotionPricing, Title = "Planning Promotion Price changes and updates" });
                    features.Add(new ModuleFeature { Name = "Kit Pricing", TypeId = DTO.ModuleFeatureType.PlanningKitPricing, Title = "Planning Kit Price changes and updates" });
                    break;

                case DTO.ModuleType.Tracking:
                    features.Add(new ModuleFeature { Name = "Home", TypeId = DTO.ModuleFeatureType.TrackingHome, Title = "Tracking home page" });
                    features.Add(new ModuleFeature { Name = "Approvals" });
                    features.Add(new ModuleFeature { Name = "Workflows" });
                    break;

                case DTO.ModuleType.Reporting:
                    features.Add(new ModuleFeature { Name = "Home", TypeId = DTO.ModuleFeatureType.ReportingHome, Title = "Reporting home page" });
                    features.Add(new ModuleFeature { Name = "Reporting1" });
                    features.Add(new ModuleFeature { Name = "Reporting2" });
                    features.Add(new ModuleFeature { Name = "Reporting3" });
                    break;

                case DTO.ModuleType.Administration:
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

        private static List<ModuleFeatureStep> GetSampleSteps(string moduleName, ModuleFeature feature)
        {
            var steps = new List<ModuleFeatureStep>();

            switch (feature.TypeId)
            {
                case DTO.ModuleFeatureType.PlanningAnalytics:
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "Search Analytics", Title = "Search saved Analytics", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsSearchAnalytics, Sort = 1 });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "1) Identity", Title = "Identify Analytics with a unique name and description", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsIdentity, Sort = 2, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "2) Filters", Title = "Configure Analytics product filters & define a product set", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsFilters, Sort = 3, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "3) Price Lists", Title = "Configure Analytics price list metrics & aggregation", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsPriceLists, Sort = 4, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "4) Value Drivers", Title = "Configure Analytics Value Driver metrics and optimization", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsValueDrivers, Sort = 5, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "5) Results", Title = "Compare Analytics result set & view reports", TypeId = DTO.ModuleFeatureStepType.PlanningAnalyticsResults, Sort = 6, });
                    break;

                case DTO.ModuleFeatureType.PlanningEverydayPricing:
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "Search Everyday", Title = "Search saved Pricing campaigns", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingSearchEveryday, Sort = 1, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "1) Identity", Title = "Identify Price routines with a unique name and description", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingIdentity, Sort = 2, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "2) Filters", Title = "Configure Price routine product filters & define a product set", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingFilters, Sort = 3, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "3) Price Lists", Title = "Configure Price routine price lists ", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingPriceLists, Sort = 4, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "4) Rounding", Title = "Configure Price routine rounding", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingRounding, Sort = 5, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "5) Strategy", Title = "Configure Price routine optimization strategy", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingStrategy, Sort = 6, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "6) Results", Title = "Compare and edit Price routine results", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingResults, Sort = 7, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "7) Impact Analysis", Title = "Create a Price routine forecast", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingForecast, Sort = 8, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "8) Request Approval", Title = "Submit this Price routine for approval", TypeId = DTO.ModuleFeatureStepType.PlanningEverydayPricingApproval, Sort = 9, });
                    break;

                case DTO.ModuleFeatureType.PlanningPromotionPricing:
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "Search Promotions", Title = "Search saved Pricing campaigns", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingSearchPromotions, Sort = 1, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "1) Identity", Title = "Identify Price routines with a unique name and description", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingIdentity, Sort = 2, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "2) Filters", Title = "Configure Price routine product filters & define a product set", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingFilters, Sort = 3, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "3) Price Lists", Title = "Configure Price routine price lists ", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingPriceLists, Sort = 4, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "4) Rounding", Title = "Configure Price routine rounding", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingRounding, Sort = 5, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "5) Strategy", Title = "Configure Price routine optimization strategy", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingStrategy, Sort = 6, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "6) Results", Title = "Compare and edit Price routine results", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingResults, Sort = 7, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "7) Impact Analysis", Title = "Create a Price routine forecast", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingForecast, Sort = 8, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "8) Request Approval", Title = "Submit this Price routine for approval", TypeId = DTO.ModuleFeatureStepType.PlanningPromotionPricingApproval, Sort = 9, });
                    break;

                case DTO.ModuleFeatureType.PlanningKitPricing:
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "Search Kits", Title = "Search saved Pricing campaigns", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingSearchKits, Sort = 1, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "1) Identity", Title = "Identify Price routines with a unique name and description", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingIdentity, Sort = 2, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "2) Filters", Title = "Configure Price routine product filters & define a product set", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingFilters, Sort = 3, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "3) Price Lists", Title = "Configure Price routine price lists ", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingPriceLists, Sort = 4, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "4) Rounding", Title = "Configure Price routine rounding", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingRounding, Sort = 5, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "5) Strategy", Title = "Configure Price routine optimization strategy", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingStrategy, Sort = 6, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "6) Results", Title = "Compare and edit Price routine results", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingResults, Sort = 7, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "7) Impact Analysis", Title = "Create a Price routine forecast", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingForecast, Sort = 8, });
                    steps.Add(new DisplayEntities.ModuleFeatureStep { Name = "8) Request Approval", Title = "Submit this Price routine for approval", TypeId = DTO.ModuleFeatureStepType.PlanningKitPricingApproval, Sort = 9, });
                    break;

                default:
                    break;
            }

            steps.ForEach(step => step.Actions = GetSampleActions(step));

            //For demo purposes, mark all steps as completed except for the following:
            string[] notCompleted = { "7) Impact Analysis", "8) Request Approval", "Search Everyday" };

            var incompleteSteps = from step in steps
                                  where notCompleted.Contains(step.Name)
                                  select step;

            foreach (ModuleFeatureStep step in steps.Except(incompleteSteps))
            {
                step.IsCompleted = true;
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

                case DTO.ModuleFeatureStepType.PlanningAnalyticsIdentity:
                    result.Add(new DisplayEntities.Action { Name = "Cancel", Title = "Discard all changes since the last save.", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsIdentityCancel, Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsIdentitySave, Sort = 4 });
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingIdentity:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingIdentitySave, Sort = 4 });
                    break;

                case DTO.ModuleFeatureStepType.PlanningAnalyticsFilters:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsFiltersSave, Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Run", Title = "Run calculations", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsFiltersRun, Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Cancel", Title = "Discard unsaved changes.", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsFiltersCancel, Sort = 3 });
                    break;
                case DTO.ModuleFeatureStepType.PlanningEverydayPricingFilters:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingFiltersSave, Sort = 4 });
                    break;

                case DTO.ModuleFeatureStepType.PlanningAnalyticsPriceLists:
                    result.Add(new DisplayEntities.Action { Name = "Run", Title = "Run calculations", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsPriceListsRun, Sort = 3 });
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsPriceListsSave, Sort = 4 });
                    break;

                case DTO.ModuleFeatureStepType.PlanningAnalyticsValueDrivers:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversSave, Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Cancel", Title = "Discard all changes since the last save.", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversCancel, Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Run", Title = "Calculate results.", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsResultsRun, Sort = 3 });
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingPriceLists:
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingRounding:
                    result.Add(new DisplayEntities.Action { Name = "Run", Title = "Run calculations", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingRoundingSave, Sort = 1 });
                    break;

                case DTO.ModuleFeatureStepType.PlanningAnalyticsResults:
                    result.Add(new DisplayEntities.Action { Name = "Run", Title = "Workflow View Step Action, Run Analytics Results.", Sort = 1 });
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingResults:
                    result.Add(new DisplayEntities.Action { Name = "Show", ParentName = "Warnings", Title = "Show Warnings.", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingResultsWarningsShow, Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Hide", ParentName = "Warnings", Title = "Hide Warnings.", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingResultsWarningsHide, Sort = 2 });

                    result.Add(new DisplayEntities.Action { Name = "Apply", ParentName = "Rounding", Title = "Apply Rounding.", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingResultsRoundingApply, Sort = 3 });
                    result.Add(new DisplayEntities.Action { Name = "Remove", ParentName = "Rounding", Title = "Remove Rounding.", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingResultsRoundingRemove, Sort = 4 });

                    result.Add(new DisplayEntities.Action { Name = "Options", ParentName = "Options", Title = "Options", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingResultsOptions, Sort = 5 });
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingStrategy:
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save this item", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingStrategySave, Sort = 1 });
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingForecast:
                    result.Add(new DisplayEntities.Action { Name = "Add Forecast", Title = "Add forecast", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingForecastNew, Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Save", Title = "Save forecast", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingForecastSave, Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Delete", Title = "Delete forecast", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingForecastDelete, Sort = 3 });

                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingApproval:
                    result.Add(new DisplayEntities.Action { Name = "Submit", Title = "Submit", TypeId = DTO.ModuleFeatureStepActionType.PlanningEverydayPricingApprovalSubmit, Sort = 1 });
                    break;

                default:
                    result.Add(new DisplayEntities.Action { Name = "Step 1", Title = "Step 1 (title goes here)", Sort = 1 });
                    result.Add(new DisplayEntities.Action { Name = "Step 2", Title = "Step 2 (title goes here)", Sort = 2 });
                    result.Add(new DisplayEntities.Action { Name = "Step 3", Title = "Step 3 (title goes here)", Sort = 3 });
                    break;
            }

            foreach (DisplayEntities.Action action in result)
            {
                if (String.IsNullOrWhiteSpace(action.ParentName))
                {
                    action.ParentName = action.Name;
                }
            }
            return result;
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


        #endregion

        #region Search Groups and Entities

        private static List<FeatureSearchGroup> GetSampleSearchGroups(ModuleFeature feature)
        {
            var searchGroups = new List<FeatureSearchGroup>();

            if (feature.TypeId == DTO.ModuleFeatureType.PlanningAnalytics)
            {
                searchGroups = MockAnalyticGenerator.GetAnalyticSearchGroups();
            }
            else if (feature.TypeId == DTO.ModuleFeatureType.PlanningEverydayPricing ||
                     feature.TypeId == DTO.ModuleFeatureType.PlanningKitPricing ||
                     feature.TypeId == DTO.ModuleFeatureType.PlanningPromotionPricing)
            {
                string myFolders = "My Folders";
                searchGroups.Add(new FeatureSearchGroup { ParentName = "Recent", Name = "Recent", SearchKey = "searchKey001", Sort = 1, ItemCount = 2 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = myFolders, Name = "Restoration Parts", SearchKey = "searchKey111", Sort = 4, ItemCount = 6 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = myFolders, Name = "Hi Performance & Aftermarket", SearchKey = "searchKey112", Sort = 5, ItemCount = 4 });
                searchGroups.Add(new FeatureSearchGroup { ParentName = myFolders, Name = "Apparel & Novelties", SearchKey = "searchKey113", Sort = 6, ItemCount = 8 });

                //Make "My Folder" items editable and movable.
                var editableSearchGroups = searchGroups.Where(group => group.ParentName == myFolders);
                foreach (FeatureSearchGroup searchGroup in editableSearchGroups)
                {
                    searchGroup.CanNameChange = true;
                    searchGroup.CanSearchKeyChange = true;
                }
            }

            return searchGroups;
        }

        private static List<ISearchableEntity> GetSampleSearchEntities(ModuleFeature feature)
        {
            var searchEntities = new List<ISearchableEntity>();

            if (feature.TypeId == DTO.ModuleFeatureType.PlanningAnalytics)
            {
                var analytics = MockAnalyticGenerator.GetSampleAnalytics();
                var entities = analytics.Cast<ISearchableEntity>();
                searchEntities.AddRange(entities);
            }
            else
            {
                foreach (FeatureSearchGroup searchGroup in feature.SearchGroups)
                {
                    for (int id = 0; id < searchGroup.ItemCount; id++)
                    {
                        ISearchableEntity entity = GetSearchEntity(feature, id);
                        if (entity != null)
                        {
                            entity.SearchKey = searchGroup.SearchKey;
                            searchEntities.Add(entity);
                        }
                    }
                }
            }
            return searchEntities;
        }

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
                case DTO.ModuleFeatureType.PlanningAnalytics:
                    result = MockAnalyticGenerator.GetSampleAnalytic(id);
                    break;

                case DTO.ModuleFeatureType.PlanningEverydayPricing:
                    result = MockPricingEverydayGenerator.GetSamplePricingEveryday(id);
                    break;

                case DTO.ModuleFeatureType.PlanningPromotionPricing:
                    result = GetSamplePricingPromotion(id);
                    break;

                case DTO.ModuleFeatureType.PlanningKitPricing:
                    result = GetSamplePricingKits(id);
                    break;

                case DTO.ModuleFeatureType.AdministrationUserMaintenance:
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

        #endregion

        #region Price Routines and Users

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


        #endregion

    }
}
