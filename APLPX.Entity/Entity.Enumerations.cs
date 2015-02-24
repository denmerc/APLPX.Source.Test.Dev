using System;
using System.Runtime.Serialization;

namespace APLPX.Entity
{
    [DataContract]
    public enum ModuleType
    {
        #region Common module types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        Startup = 122,
        [EnumMember]
        Planning = 123,
        [EnumMember]
        Tracking = 124,
        [EnumMember]
        Reporting = 125,
        [EnumMember]
        Administration = 126
    }

    [DataContract]
    public enum ModuleFeatureType
    {
        #region Common feature types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        StartupLogin = 127,
        [EnumMember]
        PlanningHome = 128,
        [EnumMember]
        PlanningAnalytics = 132,
        [EnumMember]
        PlanningEverydayPricing = 136,
        [EnumMember]
        PlanningPromotionPricing = 140,
        [EnumMember]
        PlanningKitPricing = 142,
        [EnumMember]
        TrackingHome = 129,
        [EnumMember]
        ReportingHome = 130,
        [EnumMember]
        AdministrationHome = 131,
        [EnumMember]
        AdministrationUserMaintenance = 135,

        [EnumMember]
        AdminHome = 200,
        [EnumMember]
        AdminUsers = 201,
        [EnumMember]
        AdminMarkUp = 202,
        [EnumMember]
        AdminOptimization = 203,
        [EnumMember]
        AdminTemplates = 204
    }

    [DataContract]
    public enum ModuleFeatureSearchGroupType
    {
        #region Common step types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        PlanningSearchFoldersAnalytics = 65, // Search Analytics
        [EnumMember]
        PlanningSearchFoldersEveryday = 66, // Search Everyday
        [EnumMember]
        PlanningSearchFoldersKits = 68, // Search Kits
        [EnumMember]
        PlanningSearchFoldersPromotion = 67 // Search Promotion    
    }

    [DataContract]
    public enum ModuleFeatureStepType
    {
        #region Common step types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        StartupLoginInitialization = 144, // Step Initialization
        [EnumMember]
        StartupLoginAuthentication = 154, // Step 1) Authentication
        [EnumMember]
        StartupLoginChangepassword = 160, // Step 2) Change password
        [EnumMember]
        PlanningHomeDashboard = 145, // Step Dashboard
        [EnumMember]
        PlanningAnalyticsSearchAnalytics = 149, // Step Search Analytics
        [EnumMember]
        PlanningAnalyticsIdentity = 155, // Step 1) Identity
        [EnumMember]
        PlanningAnalyticsFilters = 161, // Step 2) Filters
        [EnumMember]
        PlanningAnalyticsPriceLists = 166, // Step 3) Price Lists
        [EnumMember]
        PlanningAnalyticsValueDrivers = 171, // Step 4) Value Drivers
        [EnumMember]
        PlanningAnalyticsResults = 175, // Step 5) Results
        [EnumMember]
        PlanningEverydayPricingSearchEveryday = 151, // Step Search Everyday
        [EnumMember]
        PlanningEverydayPricingIdentity = 157, // Step 1) Identity
        [EnumMember]
        PlanningEverydayPricingFilters = 163, // Step 2) Filters
        [EnumMember]
        PlanningEverydayPricingPriceLists = 168, // Step 3) Price Lists
        [EnumMember]
        PlanningEverydayPricingRounding = 172, // Step 4) Rounding
        [EnumMember]
        PlanningEverydayPricingStrategy = 176, // Step 5) Strategy
        [EnumMember]
        PlanningEverydayPricingResults = 179, // Step 6) Results
        [EnumMember]
        PlanningEverydayPricingForecast = 182, // Step 7) Forecast
        [EnumMember]
        PlanningEverydayPricingApproval = 185, // Step 8) Approval
        [EnumMember]
        PlanningPromotionPricingSearchPromotions = 152, // Step Search Promotions
        [EnumMember]
        PlanningPromotionPricingIdentity = 158, // Step 1) Identity
        [EnumMember]
        PlanningPromotionPricingFilters = 164, // Step 2) Filters
        [EnumMember]
        PlanningPromotionPricingPriceLists = 169, // Step 3) Price Lists
        [EnumMember]
        PlanningPromotionPricingRounding = 173, // Step 4) Rounding
        [EnumMember]
        PlanningPromotionPricingStrategy = 177, // Step 5) Strategy
        [EnumMember]
        PlanningPromotionPricingResults = 180, // Step 6) Results
        [EnumMember]
        PlanningPromotionPricingForecast = 183, // Step 7) Forecast
        [EnumMember]
        PlanningPromotionPricingApproval = 186, // Step 8) Approval
        [EnumMember]
        PlanningKitPricingSearchKits = 153, // Step Search Kits
        [EnumMember]
        PlanningKitPricingIdentity = 159, // Step 1) Identity
        [EnumMember]
        PlanningKitPricingFilters = 165, // Step 2) Filters
        [EnumMember]
        PlanningKitPricingPriceLists = 170, // Step 3) Price Lists
        [EnumMember]
        PlanningKitPricingRounding = 174, // Step 4) Rounding
        [EnumMember]
        PlanningKitPricingStrategy = 178, // Step 5) Strategy
        [EnumMember]
        PlanningKitPricingResults = 181, // Step 6) Results
        [EnumMember]
        PlanningKitPricingForecast = 184, // Step 7) Forecast
        [EnumMember]
        PlanningKitPricingApproval = 187, // Step 8) Approval
        [EnumMember]
        TrackingHomeDashboard = 146, // Step Dashboard
        [EnumMember]
        ReportingHomeDashboard = 147, // Step Dashboard
        [EnumMember]
        AdministrationHomeDashboard = 148, // Step Dashboard
        [EnumMember]
        AdministrationUserMaintenanceSearchUsers = 150, // Step Search Users
        [EnumMember]
        AdministrationUserMaintenanceUserSecurity = 156, // Step 1) User Security
        [EnumMember]
        AdministrationUserMaintenanceUserIdentity = 162, // Step 2) User Identity
        [EnumMember]
        AdministrationUserMaintenanceUserRole = 167, // Step 3) User Role    
    
    
        [EnumMember]
        AdminHome = 199,
        [EnumMember]
        AdminUserSearch = 200,
        [EnumMember]
        AdminUserIdentity = 201,
        [EnumMember]
        AdminUserCredentials = 202,
        [EnumMember]
        AdminUserRole = 203
    }

    [DataContract]
    public enum ModuleFeatureStepActionType
    {
        #region Common action types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        PlanningAnalyticsSearchAnalyticsNew = 188, // Workflow View Step Action, New Analytics
        [EnumMember]
        PlanningAnalyticsSearchAnalyticsEdit = 227, // Workflow View Step Action, Edit Analytics
        [EnumMember]
        PlanningAnalyticsSearchAnalyticsCopy = 247, // Workflow View Step Action, Copy Analytics
        [EnumMember]
        PlanningAnalyticsIdentitySave = 192, // Workflow View Step Action, Save Analytics Identity
        [EnumMember]
        PlanningAnalyticsIdentityCancel = 231, // Workflow View Step Action, Cancel Analytics Identity
        [EnumMember]
        PlanningAnalyticsFiltersSave = 196, // Workflow View Step Action, Save Analytics Filters
        [EnumMember]
        PlanningAnalyticsFiltersRun = 232, // Workflow View Step Action, Run Analytics Results with Filter update
        [EnumMember]
        PlanningAnalyticsFiltersCancel = 251, // Workflow View Step Action, Cancel Analytics Filters
        [EnumMember]
        PlanningAnalyticsPriceListsSave = 200, // Workflow View Step Action, Save Analytics Price Lists
        [EnumMember]
        PlanningAnalyticsPriceListsRun = 233, // Workflow View Step Action, Run Analytics Results with Price Lists update
        [EnumMember]
        PlanningAnalyticsPriceListsCancel = 252, // Workflow View Step Action, Cancel Analytics Price Lists
        [EnumMember]
        PlanningAnalyticsValueDriversSave = 204, // Workflow View Step Action, Save Analytics Value Drivers
        [EnumMember]
        PlanningAnalyticsValueDriversRun = 234, // Workflow View Step Action, Run Analytics Value Drivers
        [EnumMember]
        PlanningAnalyticsValueDriversCancel = 253, // Workflow View Step Action, Cancel Analytics Value Drivers
        [EnumMember]
        PlanningAnalyticsResultsRun = 208, // Workflow View Step Action, Run Analytics Results
        [EnumMember]
        PlanningEverydayPricingSearchEverydayNew = 189, // Workflow View Step Action, New Everyday
        [EnumMember]
        PlanningEverydayPricingSearchEverydayEdit = 228, // Workflow View Step Action, Edit Everyday
        [EnumMember]
        PlanningEverydayPricingSearchEverydayCopy = 248, // Workflow View Step Action, Copy Everyday
        [EnumMember]
        PlanningEverydayPricingIdentitySave = 193, // Workflow View Step Action, Save Everyday Pricing Identity
        [EnumMember]
        PlanningEverydayPricingFiltersSave = 197, // Workflow View Step Action, Save Everyday Pricing Filters
        [EnumMember]
        PlanningEverydayPricingPriceListsSave = 201, // Workflow View Step Action, Save Everyday Pricing Price Lists
        [EnumMember]
        PlanningEverydayPricingRoundingSave = 205, // Workflow View Step Action, Save Everyday Pricing Rounding
        [EnumMember]
        PlanningEverydayPricingStrategySave = 209, // Workflow View Step Action, Save Everyday Pricing Strategy
        [EnumMember]
        PlanningEverydayPricingResultsWarnings = 212, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsWarningsHide = 241, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsWarningsShow = 221, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsRounding = 235, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsRoundingApply = 224, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsRoundingRemove = 244, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsOptions = 254, // Workflow View Step Action, Options Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingForecastNew = 215, // Workflow View Step Action, New Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingForecastSave = 238, // Workflow View Step Action, Save Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingForecastDelete = 257, // Workflow View Step Action, Delete Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingApprovalSubmit = 218, // Workflow View Step Action, Submit Everyday Pricing Approval
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsNew = 190, // Workflow View Step Action, New Promotions
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsEdit = 229, // Workflow View Step Action, Edit Promotions
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsCopy = 249, // Workflow View Step Action, Copy Promotions
        [EnumMember]
        PlanningPromotionPricingIdentitySave = 194, // Workflow View Step Action, Save Promotion Pricing Identity
        [EnumMember]
        PlanningPromotionPricingFiltersSave = 198, // Workflow View Step Action, Save Promotion Pricing Filters
        [EnumMember]
        PlanningPromotionPricingPriceListsSave = 202, // Workflow View Step Action, Save Promotion Pricing Price Lists
        [EnumMember]
        PlanningPromotionPricingRoundingSave = 206, // Workflow View Step Action, Save Promotion Pricing Rounding
        [EnumMember]
        PlanningPromotionPricingStrategySave = 210, // Workflow View Step Action, Save Promotion Pricing Strategy
        [EnumMember]
        PlanningPromotionPricingResultsWarnings = 213, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsWarningsHide = 242, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsWarningsShow = 222, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsRounding = 236, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsRoundingApply = 225, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsRoundingRemove = 245, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsOptions = 255, // Workflow View Step Action, Options Promotion Pricing Results
        [EnumMember]
        PlanningPromotionPricingForecastNew = 216, // Workflow View Step Action, New Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingForecastSave = 239, // Workflow View Step Action, Save Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingForecastDelete = 258, // Workflow View Step Action, Delete Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingApprovalSubmit = 219, // Workflow View Step Action, Submit Promotion Pricing Approval
        [EnumMember]
        PlanningKitPricingSearchKitsNew = 191, // Workflow View Step Action, New Kits
        [EnumMember]
        PlanningKitPricingSearchKitsEdit = 230, // Workflow View Step Action, Edit Kits
        [EnumMember]
        PlanningKitPricingSearchKitsCopy = 250, // Workflow View Step Action, Copy Kits
        [EnumMember]
        PlanningKitPricingIdentitySave = 195, // Workflow View Step Action, Save Kit Pricing Identity
        [EnumMember]
        PlanningKitPricingFiltersSave = 199, // Workflow View Step Action, Save Kit Pricing Filters
        [EnumMember]
        PlanningKitPricingPriceListsSave = 203, // Workflow View Step Action, Save Kit Pricing Price Lists
        [EnumMember]
        PlanningKitPricingRoundingSave = 207, // Workflow View Step Action, Save Kit Pricing Rounding
        [EnumMember]
        PlanningKitPricingStrategySave = 211, // Workflow View Step Action, Save Kit Pricing Strategy
        [EnumMember]
        PlanningKitPricingResultsWarnings = 214, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsWarningsHide = 243, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsWarningsShow = 223, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsRounding = 237, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsRoundingApply = 226, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsRoundingRemove = 246, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsOptions = 256, // Workflow View Step Action, Options Kit Pricing Results
        [EnumMember]
        PlanningKitPricingForecastNew = 217, // Workflow View Step Action, New Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingForecastSave = 240, // Workflow View Step Action, Save Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingForecastDelete = 259, // Workflow View Step Action, Delete Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingApprovalSubmit = 220, // Workflow View Step Action, Submit Kit Pricing Approval    

        [EnumMember]
        AdminUserSearchNew = 400,
        [EnumMember]
        AdminUserSearchEdit = 401,
        [EnumMember]
        AdminUserSearchRemove = 402
    }

    [DataContract]
    public enum PricingAnalyticsDriverType
    {
        #region Common action types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        DriverMarkup = 50,
        [EnumMember]
        DriverMovement = 51,
        [EnumMember]
        DriverDaysOnHand = 52
    }

    [DataContract]
    public enum AnalyticsModesType
    {
        #region Common action types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        DriverModeAutoGeneratedGroups = 57,
        [EnumMember]
        DriverModeUserDefinedGroups = 58
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

    [DataContract]
    public enum UserRoleType
    {
        #region Common action types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        AplUserRoleAdministrator = 86, // Application (create all, edit own, view all, delete own, approve assigned, schedule all, manage users, manage defaults)
        [EnumMember]
        AplUserRolePricingAnalyst = 87, // Application (create price routine, edit own, view own, delete own, approve none, schedule none)
        [EnumMember]
        AplUserRolePricingApprover = 88, // Application (create price routine, edit own, view own, delete own, approve assigned, schedule none)
        [EnumMember]
        AplUserRolePricingReviewer = 89 // Application (create none, edit none, view all, delete none, approve none, schedule none)
    }
}

