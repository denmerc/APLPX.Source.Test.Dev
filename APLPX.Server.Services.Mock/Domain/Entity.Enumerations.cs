using System;
using System.Runtime.Serialization;

namespace APLPX.Mock.Entity
{
    [DataContract]
    public enum ModuleType
    {
        #region Common view types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        Startup = 86,
        [EnumMember]
        Planning = 87,
        [EnumMember]
        Tracking = 88,
        [EnumMember]
        Reporting = 89,
        [EnumMember]
        Administration = 90
    }

    [DataContract]
    public enum ModuleFeatureType
    {
        #region Common view types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        StartupLogin = 91,
        [EnumMember]
        PlanningHome = 92,
        [EnumMember]
        PlanningAnalytics = 96,
        [EnumMember]
        PlanningEverydayPricing = 100,
        [EnumMember]
        PlanningPromotionPricing = 104,
        [EnumMember]
        PlanningKitPricing = 106,
        [EnumMember]
        TrackingHome = 93,
        [EnumMember]
        ReportingHome = 94,
        [EnumMember]
        AdministrationHome = 95,
        [EnumMember]
        AdministrationUserMaintenance = 99,
    }

    [DataContract]
    public enum ModuleFeatureStepType
    {
        #region Common view types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        StartupLoginInitialization = 108, // Step Initialization
        [EnumMember]
        StartupLoginAuthentication = 118, // Step 1) Authentication
        [EnumMember]
        StartupLoginChangepassword = 124, // Step 2) Change password

        [EnumMember]
        PlanningHomeDashboard = 109, // Step Dashboard
        [EnumMember]
        PlanningAnalyticsSearchAnalytics = 113, // Step Search Analytics
        [EnumMember]
        PlanningAnalyticsIdentity = 119, // Step 1) Identity
        [EnumMember]
        PlanningAnalyticsFilters = 125, // Step 2) Filters
        [EnumMember]
        PlanningAnalyticsPriceLists = 130, // Step 3) Price Lists
        [EnumMember]
        PlanningAnalyticsValueDrivers = 135, // Step 4) Value Drivers
        [EnumMember]
        PlanningAnalyticsResults = 139, // Step 5) Results

        [EnumMember]
        PlanningEverydayPricingSearchEveryday = 115, // Step Search Everyday
        [EnumMember]
        PlanningEverydayPricingIdentity = 121, // Step 1) Identity
        [EnumMember]
        PlanningEverydayPricingFilters = 127, // Step 2) Filters
        [EnumMember]
        PlanningEverydayPricingPriceLists = 132, // Step 3) Price Lists
        [EnumMember]
        PlanningEverydayPricingRounding = 136, // Step 4) Rounding
        [EnumMember]
        PlanningEverydayPricingStrategy = 140, // Step 5) Strategy
        [EnumMember]
        PlanningEverydayPricingResults = 143, // Step 6) Results
        [EnumMember]
        PlanningEverydayPricingForecast = 146, // Step 7) Forecast
        [EnumMember]
        PlanningEverydayPricingApproval = 149, // Step 8) Approval

        [EnumMember]
        PlanningPromotionPricingSearchPromotions = 116, // Step Search Promotions
        [EnumMember]
        PlanningPromotionPricingIdentity = 122, // Step 1) Identity
        [EnumMember]
        PlanningPromotionPricingFilters = 128, // Step 2) Filters
        [EnumMember]
        PlanningPromotionPricingPriceLists = 133, // Step 3) Price Lists
        [EnumMember]
        PlanningPromotionPricingRounding = 137, // Step 4) Rounding
        [EnumMember]
        PlanningPromotionPricingStrategy = 141, // Step 5) Strategy
        [EnumMember]
        PlanningPromotionPricingResults = 144, // Step 6) Results
        [EnumMember]
        PlanningPromotionPricingForecast = 147, // Step 7) Forecast
        [EnumMember]
        PlanningPromotionPricingApproval = 150, // Step 8) Approval

        [EnumMember]
        PlanningKitPricingSearchKits = 117, // Step Search Kits
        [EnumMember]
        PlanningKitPricingIdentity = 123, // Step 1) Identity
        [EnumMember]
        PlanningKitPricingFilters = 129, // Step 2) Filters
        [EnumMember]
        PlanningKitPricingPriceLists = 134, // Step 3) Price Lists
        [EnumMember]
        PlanningKitPricingRounding = 138, // Step 4) Rounding
        [EnumMember]
        PlanningKitPricingStrategy = 142, // Step 5) Strategy
        [EnumMember]
        PlanningKitPricingResults = 145, // Step 6) Results
        [EnumMember]
        PlanningKitPricingForecast = 148, // Step 7) Forecast
        [EnumMember]
        PlanningKitPricingApproval = 151, // Step 8) Approval

        [EnumMember]
        TrackingHomeDashboard = 110, // Step Dashboard

        [EnumMember]
        ReportingHomeDashboard = 111, // Step Dashboard

        [EnumMember]
        AdministrationHomeDashboard = 112, // Step Dashboard
        [EnumMember]
        AdministrationUserMaintenanceSearchUsers = 114, // Step Search Users
        [EnumMember]
        AdministrationUserMaintenanceUserLogin = 120, // Step 1) User Login
        [EnumMember]
        AdministrationUserMaintenanceUserIdentity = 126, // Step 2) User Identity
        [EnumMember]
        AdministrationUserMaintenanceUserRole = 131, // Step 3) User Role
    }

    [DataContract]
    public enum ModuleFeatureStepActionType
    {
        #region Common view types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        PlanningAnalyticsSearchAnalyticsNew = 152, // Workflow View Step Action, New Analytics
        [EnumMember]
        PlanningAnalyticsSearchAnalyticsEdit = 185, // Workflow View Step Action, Edit Analytics
        [EnumMember]
        PlanningAnalyticsSearchAnalyticsCopy = 217, // Workflow View Step Action, Copy Analytics
        [EnumMember]
        PlanningAnalyticsIdentitySave = 156, // Workflow View Step Action, Save Analytics Identity
        [EnumMember]
        PlanningAnalyticsIdentityNext = 189, // Workflow View Step Action, Next Analytics Identity
        [EnumMember]
        PlanningAnalyticsFiltersSave = 160, // Workflow View Step Action, Save Analytics Filters
        [EnumMember]
        PlanningAnalyticsFiltersPrevious = 193, // Workflow View Step Action, Previous Analytics Filters
        [EnumMember]
        PlanningAnalyticsFiltersNext = 221, // Workflow View Step Action, Next Analytics Filters
        [EnumMember]
        PlanningAnalyticsPriceListsSave = 164, // Workflow View Step Action, Save Analytics Price Lists
        [EnumMember]
        PlanningAnalyticsPriceListsPrevious = 197, // Workflow View Step Action, Previous Analytics Price Lists
        [EnumMember]
        PlanningAnalyticsPriceListsNext = 225, // Workflow View Step Action, Next Analytics Price Lists
        [EnumMember]
        PlanningAnalyticsValueDriversSave = 168, // Workflow View Step Action, Save Analytics Value Drivers
        [EnumMember]
        PlanningAnalyticsValueDriversPrevious = 201, // Workflow View Step Action, Previous Analytics Value Drivers
        [EnumMember]
        PlanningAnalyticsValueDriversNext = 229, // Workflow View Step Action, Next Analytics Value Drivers
        [EnumMember]
        PlanningAnalyticsResultsPrevious = 172, // Workflow View Step Action, Previous Analytics Results

        [EnumMember]
        PlanningEverydayPricingSearchEverydayNew = 153, // Workflow View Step Action, New Everyday
        [EnumMember]
        PlanningEverydayPricingSearchEverydayEdit = 186, // Workflow View Step Action, Edit Everyday
        [EnumMember]
        PlanningEverydayPricingSearchEverydayCopy = 218, // Workflow View Step Action, Copy Everyday
        [EnumMember]
        PlanningEverydayPricingIdentitySave = 157, // Workflow View Step Action, Save Everyday Pricing Identity
        [EnumMember]
        PlanningEverydayPricingIdentityNext = 190, // Workflow View Step Action, Next Everyday Pricing Identity
        [EnumMember]
        PlanningEverydayPricingFiltersSave = 161, // Workflow View Step Action, Save Everyday Pricing Filters
        [EnumMember]
        PlanningEverydayPricingFiltersPrevious = 194, // Workflow View Step Action, Previous Everyday Pricing Filters
        [EnumMember]
        PlanningEverydayPricingFiltersNext = 222, // Workflow View Step Action, Next Everyday Pricing Filters
        [EnumMember]
        PlanningEverydayPricingPriceListsSave = 165, // Workflow View Step Action, Save Everyday Pricing Price Lists
        [EnumMember]
        PlanningEverydayPricingPriceListsPrevious = 198, // Workflow View Step Action, Previous Everyday Pricing Price Lists
        [EnumMember]
        PlanningEverydayPricingPriceListsNext = 226, // Workflow View Step Action, Next Everyday Pricing Price Lists
        [EnumMember]
        PlanningEverydayPricingRoundingSave = 169, // Workflow View Step Action, Save Everyday Pricing Rounding
        [EnumMember]
        PlanningEverydayPricingRoundingPrevious = 202, // Workflow View Step Action, Previous Everyday Pricing Rounding
        [EnumMember]
        PlanningEverydayPricingRoundingNext = 230, // Workflow View Step Action, Next Everyday Pricing Rounding
        [EnumMember]
        PlanningEverydayPricingStrategySave = 173, // Workflow View Step Action, Save Everyday Pricing Strategy
        [EnumMember]
        PlanningEverydayPricingStrategyPrevious = 205, // Workflow View Step Action, Previous Everyday Pricing Strategy
        [EnumMember]
        PlanningEverydayPricingStrategyNext = 233, // Workflow View Step Action, Next Everyday Pricing Strategy
        [EnumMember]
        PlanningEverydayPricingResultsWarnings = 176, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsRounding = 208, // Workflow View Step Action, Rounding Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsOptions = 236, // Workflow View Step Action, Options Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsPrevious = 242, // Workflow View Step Action, Previous Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingForecastNew = 179, // Workflow View Step Action, New Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingForecastSave = 211, // Workflow View Step Action, Save Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingForecastDelete = 239, // Workflow View Step Action, Delete Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingForecastPrevious = 245, // Workflow View Step Action, Previous Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingForecastNext = 248, // Workflow View Step Action, Next Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingApprovalSubmit = 182, // Workflow View Step Action, Submit Everyday Pricing Approval
        [EnumMember]
        PlanningEverydayPricingApprovalPrevious = 214, // Workflow View Step Action, Previous Everyday Pricing Approval

        [EnumMember]
        PlanningPromotionPricingSearchPromotionsNew = 154, // Workflow View Step Action, New Promotions
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsEdit = 187, // Workflow View Step Action, Edit Promotions
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsCopy = 219, // Workflow View Step Action, Copy Promotions
        [EnumMember]
        PlanningPromotionPricingIdentitySave = 158, // Workflow View Step Action, Save Promotion Pricing Identity
        [EnumMember]
        PlanningPromotionPricingIdentityNext = 191, // Workflow View Step Action, Next Promotion Pricing Identity
        [EnumMember]
        PlanningPromotionPricingFiltersSave = 162, // Workflow View Step Action, Save Promotion Pricing Filters
        [EnumMember]
        PlanningPromotionPricingFiltersPrevious = 195, // Workflow View Step Action, Previous Promotion Pricing Filters
        [EnumMember]
        PlanningPromotionPricingFiltersNext = 223, // Workflow View Step Action, Next Promotion Pricing Filters
        [EnumMember]
        PlanningPromotionPricingPriceListsSave = 166, // Workflow View Step Action, Save Promotion Pricing Price Lists
        [EnumMember]
        PlanningPromotionPricingPriceListsPrevious = 199, // Workflow View Step Action, Previous Promotion Pricing Price Lists
        [EnumMember]
        PlanningPromotionPricingPriceListsNext = 227, // Workflow View Step Action, Next Promotion Pricing Price Lists
        [EnumMember]
        PlanningPromotionPricingRoundingSave = 170, // Workflow View Step Action, Save Promotion Pricing Rounding
        [EnumMember]
        PlanningPromotionPricingRoundingPrevious = 203, // Workflow View Step Action, Previous Promotion Pricing Rounding
        [EnumMember]
        PlanningPromotionPricingRoundingNext = 231, // Workflow View Step Action, Next Promotion Pricing Rounding
        [EnumMember]
        PlanningPromotionPricingStrategySave = 174, // Workflow View Step Action, Save Promotion Pricing Strategy
        [EnumMember]
        PlanningPromotionPricingStrategyPrevious = 206, // Workflow View Step Action, Previous Promotion Pricing Strategy
        [EnumMember]
        PlanningPromotionPricingStrategyNext = 234, // Workflow View Step Action, Next Promotion Pricing Strategy
        [EnumMember]
        PlanningPromotionPricingResultsWarnings = 177, // Workflow View Step Action, Warnings Promotion Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsRounding = 209, // Workflow View Step Action, Rounding Promotion Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsOptions = 237, // Workflow View Step Action, Options Promotion Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsPrevious = 243, // Workflow View Step Action, Previous Promotion Pricing Results
        [EnumMember]
        PlanningPromotionPricingForecastNew = 180, // Workflow View Step Action, New Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingForecastSave = 212, // Workflow View Step Action, Save Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingForecastDelete = 240, // Workflow View Step Action, Delete Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingForecastPrevious = 246, // Workflow View Step Action, Previous Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingForecastNext = 249, // Workflow View Step Action, Next Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingApprovalSubmit = 183, // Workflow View Step Action, Submit Promotion Pricing Approval
        [EnumMember]
        PlanningPromotionPricingApprovalPrevious = 215, // Workflow View Step Action, Previous Promotion Pricing Approval

        [EnumMember]
        PlanningKitPricingSearchKitsNew = 155, // Workflow View Step Action, New Kits
        [EnumMember]
        PlanningKitPricingSearchKitsEdit = 188, // Workflow View Step Action, Edit Kits
        [EnumMember]
        PlanningKitPricingSearchKitsCopy = 220, // Workflow View Step Action, Copy Kits
        [EnumMember]
        PlanningKitPricingIdentitySave = 159, // Workflow View Step Action, Save Kit Pricing Identity
        [EnumMember]
        PlanningKitPricingIdentityNext = 192, // Workflow View Step Action, Next Kit Pricing Identity
        [EnumMember]
        PlanningKitPricingFiltersSave = 163, // Workflow View Step Action, Save Kit Pricing Filters
        [EnumMember]
        PlanningKitPricingFiltersPrevious = 196, // Workflow View Step Action, Previous Kit Pricing Filters
        [EnumMember]
        PlanningKitPricingFiltersNext = 224, // Workflow View Step Action, Next Kit Pricing Filters
        [EnumMember]
        PlanningKitPricingPriceListsSave = 167, // Workflow View Step Action, Save Kit Pricing Price Lists
        [EnumMember]
        PlanningKitPricingPriceListsPrevious = 200, // Workflow View Step Action, Previous Kit Pricing Price Lists
        [EnumMember]
        PlanningKitPricingPriceListsNext = 228, // Workflow View Step Action, Next Kit Pricing Price Lists
        [EnumMember]
        PlanningKitPricingRoundingSave = 171, // Workflow View Step Action, Save Kit Pricing Rounding
        [EnumMember]
        PlanningKitPricingRoundingPrevious = 204, // Workflow View Step Action, Previous Kit Pricing Rounding
        [EnumMember]
        PlanningKitPricingRoundingNext = 232, // Workflow View Step Action, Next Kit Pricing Rounding
        [EnumMember]
        PlanningKitPricingStrategySave = 175, // Workflow View Step Action, Save Kit Pricing Strategy
        [EnumMember]
        PlanningKitPricingStrategyPrevious = 207, // Workflow View Step Action, Previous Kit Pricing Strategy
        [EnumMember]
        PlanningKitPricingStrategyNext = 235, // Workflow View Step Action, Next Kit Pricing Strategy
        [EnumMember]
        PlanningKitPricingResultsWarnings = 178, // Workflow View Step Action, Warnings Kit Pricing Results
        [EnumMember]
        PlanningKitPricingResultsRounding = 210, // Workflow View Step Action, Rounding Kit Pricing Results
        [EnumMember]
        PlanningKitPricingResultsOptions = 238, // Workflow View Step Action, Options Kit Pricing Results
        [EnumMember]
        PlanningKitPricingResultsPrevious = 244, // Workflow View Step Action, Previous Kit Pricing Results
        [EnumMember]
        PlanningKitPricingForecastNew = 181, // Workflow View Step Action, New Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingForecastSave = 213, // Workflow View Step Action, Save Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingForecastDelete = 241, // Workflow View Step Action, Delete Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingForecastPrevious = 247, // Workflow View Step Action, Previous Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingForecastNext = 250, // Workflow View Step Action, Next Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingApprovalSubmit = 184, // Workflow View Step Action, Submit Kit Pricing Approval
        [EnumMember]
        PlanningKitPricingApprovalPrevious = 216, // Workflow View Step Action, Previous Kit Pricing Approval
    }
}

