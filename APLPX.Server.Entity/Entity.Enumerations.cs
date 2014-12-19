using System;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
{
    [DataContract]
    public enum ModuleType
    {
        #region Common module types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        Startup = 103,
        [EnumMember]
        Planning = 104,
        [EnumMember]
        Tracking = 105,
        [EnumMember]
        Reporting = 106,
        [EnumMember]
        Administration = 107
    }

    [DataContract]
    public enum ModuleFeatureType
    {
        #region Common feature types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        StartupLogin = 108,
        
        [EnumMember]
        PlanningHome = 109,
        [EnumMember]
        PlanningAnalytics = 113,
        [EnumMember]
        PlanningEverydayPricing = 117,
        [EnumMember]
        PlanningPromotionPricing = 121,
        [EnumMember]
        PlanningKitPricing = 123,
        
        [EnumMember]
        TrackingHome = 110,
        
        [EnumMember]
        ReportingHome = 111,
        
        [EnumMember]
        AdministrationHome = 112,
        [EnumMember]
        AdministrationUserMaintenance = 116
    }

    [DataContract]
    public enum ModuleFeatureSearchGroupType
    {
        #region Common step types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        PlanningSearchFoldersAnalytics = 49, // Step Folders Analytics
        [EnumMember]
        PlanningSearchFoldersEveryday = 50, // Step Folders Everyday
        [EnumMember]
        PlanningSearchFoldersKits = 52, // Step Folders Kits
        [EnumMember]
        PlanningSearchFoldersPromotion = 51, // Step Folders Promotion
    }

    [DataContract]
    public enum ModuleFeatureStepType
    {
        #region Common step types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        StartupLoginInitialization = 125, // Step Initialization
        [EnumMember]
        StartupLoginAuthentication = 135, // Step 1) Authentication
        [EnumMember]
        StartupLoginChangepassword = 141, // Step 2) Change password
        
        [EnumMember]
        PlanningHomeDashboard = 126, // Step Dashboard
        
        [EnumMember]
        PlanningAnalyticsSearchAnalytics = 130, // Step Search Analytics
        [EnumMember]
        PlanningAnalyticsIdentity = 136, // Step 1) Identity
        [EnumMember]
        PlanningAnalyticsFilters = 142, // Step 2) Filters
        [EnumMember]
        PlanningAnalyticsPriceLists = 147, // Step 3) Price Lists
        [EnumMember]
        PlanningAnalyticsValueDrivers = 152, // Step 4) Value Drivers
        [EnumMember]
        PlanningAnalyticsResults = 156, // Step 5) Results
        
        [EnumMember]
        PlanningEverydayPricingSearchEveryday = 132, // Step Search Everyday
        [EnumMember]
        PlanningEverydayPricingIdentity = 138, // Step 1) Identity
        [EnumMember]
        PlanningEverydayPricingFilters = 144, // Step 2) Filters
        [EnumMember]
        PlanningEverydayPricingPriceLists = 149, // Step 3) Price Lists
        [EnumMember]
        PlanningEverydayPricingRounding = 153, // Step 4) Rounding
        [EnumMember]
        PlanningEverydayPricingStrategy = 157, // Step 5) Strategy
        [EnumMember]
        PlanningEverydayPricingResults = 160, // Step 6) Results
        [EnumMember]
        PlanningEverydayPricingForecast = 163, // Step 7) Forecast
        [EnumMember]
        PlanningEverydayPricingApproval = 166, // Step 8) Approval
        
        [EnumMember]
        PlanningPromotionPricingSearchPromotions = 133, // Step Search Promotions
        [EnumMember]
        PlanningPromotionPricingIdentity = 139, // Step 1) Identity
        [EnumMember]
        PlanningPromotionPricingFilters = 145, // Step 2) Filters
        [EnumMember]
        PlanningPromotionPricingPriceLists = 150, // Step 3) Price Lists
        [EnumMember]
        PlanningPromotionPricingRounding = 154, // Step 4) Rounding
        [EnumMember]
        PlanningPromotionPricingStrategy = 158, // Step 5) Strategy
        [EnumMember]
        PlanningPromotionPricingResults = 161, // Step 6) Results
        [EnumMember]
        PlanningPromotionPricingForecast = 164, // Step 7) Forecast
        [EnumMember]
        PlanningPromotionPricingApproval = 167, // Step 8) Approval
        
        [EnumMember]
        PlanningKitPricingSearchKits = 134, // Step Search Kits
        [EnumMember]
        PlanningKitPricingIdentity = 140, // Step 1) Identity
        [EnumMember]
        PlanningKitPricingFilters = 146, // Step 2) Filters
        [EnumMember]
        PlanningKitPricingPriceLists = 151, // Step 3) Price Lists
        [EnumMember]
        PlanningKitPricingRounding = 155, // Step 4) Rounding
        [EnumMember]
        PlanningKitPricingStrategy = 159, // Step 5) Strategy
        [EnumMember]
        PlanningKitPricingResults = 162, // Step 6) Results
        [EnumMember]
        PlanningKitPricingForecast = 165, // Step 7) Forecast
        [EnumMember]
        PlanningKitPricingApproval = 168, // Step 8) Approval
        
        [EnumMember]
        TrackingHomeDashboard = 127, // Step Dashboard
        
        [EnumMember]
        ReportingHomeDashboard = 128, // Step Dashboard
        
        [EnumMember]
        AdministrationHomeDashboard = 129, // Step Dashboard
        [EnumMember]
        AdministrationUserMaintenanceSearchUsers = 131, // Step Search Users
        [EnumMember]
        AdministrationUserMaintenanceUserSecurity = 137, // Step 1) User Security
        [EnumMember]
        AdministrationUserMaintenanceUserIdentity = 143, // Step 2) User Identity
        [EnumMember]
        AdministrationUserMaintenanceUserRole = 148 // Step 3) User Role
    }

    [DataContract]
    public enum ModuleFeatureStepActionType
    {
        #region Common action types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        PlanningAnalyticsSearchAnalyticsNew = 169, // Workflow View Step Action, New Analytics
        [EnumMember]
        PlanningAnalyticsSearchAnalyticsEdit = 208, // Workflow View Step Action, Edit Analytics
        [EnumMember]
        PlanningAnalyticsSearchAnalyticsCopy = 228, // Workflow View Step Action, Copy Analytics
        [EnumMember]
        PlanningAnalyticsIdentitySave = 173, // Workflow View Step Action, Save Analytics Identity
        [EnumMember]
        PlanningAnalyticsIdentityCancel = 212, // Workflow View Step Action, Cancel Analytics Identity
        [EnumMember]
        PlanningAnalyticsFiltersSave = 177, // Workflow View Step Action, Save Analytics Filters
        [EnumMember]
        PlanningAnalyticsFiltersRun = 213, // Workflow View Step Action, Run Analytics Results with Filter update
        [EnumMember]
        PlanningAnalyticsFiltersCancel = 232, // Workflow View Step Action, Cancel Analytics Filters
        [EnumMember]
        PlanningAnalyticsPriceListsSave = 181, // Workflow View Step Action, Save Analytics Price Lists
        [EnumMember]
        PlanningAnalyticsPriceListsRun = 214, // Workflow View Step Action, Run Analytics Results with Price Lists update
        [EnumMember]
        PlanningAnalyticsPriceListsCancel = 233, // Workflow View Step Action, Cancel Analytics Price Lists
        [EnumMember]
        PlanningAnalyticsValueDriversSave = 185, // Workflow View Step Action, Save Analytics Value Drivers
        [EnumMember]
        PlanningAnalyticsValueDriversCancel = 215, // Workflow View Step Action, Cancel Analytics Value Drivers
        [EnumMember]
        PlanningAnalyticsResultsRun = 189, // Workflow View Step Action, Run Analytics Results
        
        [EnumMember]
        PlanningEverydayPricingSearchEverydayNew = 170, // Workflow View Step Action, New Everyday
        [EnumMember]
        PlanningEverydayPricingSearchEverydayEdit = 209, // Workflow View Step Action, Edit Everyday
        [EnumMember]
        PlanningEverydayPricingSearchEverydayCopy = 229, // Workflow View Step Action, Copy Everyday
        [EnumMember]
        PlanningEverydayPricingIdentitySave = 174, // Workflow View Step Action, Save Everyday Pricing Identity
        [EnumMember]
        PlanningEverydayPricingFiltersSave = 178, // Workflow View Step Action, Save Everyday Pricing Filters
        [EnumMember]
        PlanningEverydayPricingPriceListsSave = 182, // Workflow View Step Action, Save Everyday Pricing Price Lists
        [EnumMember]
        PlanningEverydayPricingRoundingSave = 186, // Workflow View Step Action, Save Everyday Pricing Rounding
        [EnumMember]
        PlanningEverydayPricingStrategySave = 190, // Workflow View Step Action, Save Everyday Pricing Strategy
        [EnumMember]
        PlanningEverydayPricingResultsWarnings = 193, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsRounding = 216, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsOptions = 234, // Workflow View Step Action, Options Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingForecastNew = 196, // Workflow View Step Action, New Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingForecastSave = 219, // Workflow View Step Action, Save Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingForecastDelete = 237, // Workflow View Step Action, Delete Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingApprovalSubmit = 199, // Workflow View Step Action, Submit Everyday Pricing Approval
        
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsNew = 171, // Workflow View Step Action, New Promotions
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsEdit = 210, // Workflow View Step Action, Edit Promotions
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsCopy = 230, // Workflow View Step Action, Copy Promotions
        [EnumMember]
        PlanningPromotionPricingIdentitySave = 175, // Workflow View Step Action, Save Promotion Pricing Identity
        [EnumMember]
        PlanningPromotionPricingFiltersSave = 179, // Workflow View Step Action, Save Promotion Pricing Filters
        [EnumMember]
        PlanningPromotionPricingPriceListsSave = 183, // Workflow View Step Action, Save Promotion Pricing Price Lists
        [EnumMember]
        PlanningPromotionPricingRoundingSave = 187, // Workflow View Step Action, Save Promotion Pricing Rounding
        [EnumMember]
        PlanningPromotionPricingStrategySave = 191, // Workflow View Step Action, Save Promotion Pricing Strategy
        [EnumMember]
        PlanningPromotionPricingResultsWarnings = 194, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsRounding = 217, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsOptions = 235, // Workflow View Step Action, Options Promotion Pricing Results
        [EnumMember]
        PlanningPromotionPricingForecastNew = 197, // Workflow View Step Action, New Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingForecastSave = 220, // Workflow View Step Action, Save Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingForecastDelete = 238, // Workflow View Step Action, Delete Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingApprovalSubmit = 200, // Workflow View Step Action, Submit Promotion Pricing Approval
        
        [EnumMember]
        PlanningKitPricingSearchKitsNew = 172, // Workflow View Step Action, New Kits
        [EnumMember]
        PlanningKitPricingSearchKitsEdit = 211, // Workflow View Step Action, Edit Kits
        [EnumMember]
        PlanningKitPricingSearchKitsCopy = 231, // Workflow View Step Action, Copy Kits
        [EnumMember]
        PlanningKitPricingIdentitySave = 176, // Workflow View Step Action, Save Kit Pricing Identity
        [EnumMember]
        PlanningKitPricingFiltersSave = 180, // Workflow View Step Action, Save Kit Pricing Filters
        [EnumMember]
        PlanningKitPricingPriceListsSave = 184, // Workflow View Step Action, Save Kit Pricing Price Lists
        [EnumMember]
        PlanningKitPricingRoundingSave = 188, // Workflow View Step Action, Save Kit Pricing Rounding
        [EnumMember]
        PlanningKitPricingStrategySave = 192, // Workflow View Step Action, Save Kit Pricing Strategy
        [EnumMember]
        PlanningKitPricingResultsWarnings = 195, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsRounding = 218, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsOptions = 236, // Workflow View Step Action, Options Kit Pricing Results
        [EnumMember]
        PlanningKitPricingForecastNew = 198, // Workflow View Step Action, New Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingForecastSave = 221, // Workflow View Step Action, Save Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingForecastDelete = 239, // Workflow View Step Action, Delete Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingApprovalSubmit = 201 // Workflow View Step Action, Submit Kit Pricing Approval    
    }

    [DataContract]
    public enum PricingAnalyticsDriverType
    {
        #region Common action types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        DriverMarkup = 34,
        [EnumMember]
        DriverMovement = 35,
        [EnumMember]
        DriverDaysOnHand = 36
    }

    [DataContract]
    public enum AnalyticsModesType
    {
        #region Common action types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        DriverModeAutoGeneratedGroups = 41,
        [EnumMember]
        DriverModeUserDefinedGroups = 42
    }

    [DataContract]
    public enum PricingResultsEditType
    {
        #region Common action types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        ExcludeUpdate = 10,
        [EnumMember]
        DefaultPrice = 11,
        [EnumMember]
        DefaultToCurrentPrice = 12,
        [EnumMember]
        DefaultToMaxMarkup = 13,
        [EnumMember]
        DefaultToMinMarkup = 14,
        [EnumMember]
        EditNewPrice = 15,
        [EnumMember]
        EditNewMarkup = 16,
        [EnumMember]
        EditRemoveRounding = 17,
        [EnumMember]
        EditApplyRounding = 18
    }

    [DataContract]
    public enum PricingResultsWarningType
    {
        #region Common action types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        MarkupPassed = 23,
        [EnumMember]
        MarkupBelow = 24,
        [EnumMember]
        MarkupAbove = 25
    }
}

