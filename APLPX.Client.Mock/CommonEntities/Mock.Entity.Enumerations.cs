using System;
using System.Runtime.Serialization;

namespace APLPX.Common.Mock.Entity
{
    [DataContract]
    public enum ModuleType
    {
        #region Common module types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        Startup = 121,
        [EnumMember]
        Planning = 122,
        [EnumMember]
        Tracking = 123,
        [EnumMember]
        Reporting = 124,
        [EnumMember]
        Administration = 125
    }

    [DataContract]
    public enum ModuleFeatureType
    {
        #region Common feature types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        StartupLogin = 126,

        [EnumMember]
        PlanningHome = 127,
        [EnumMember]
        PlanningAnalytics = 131,
        [EnumMember]
        PlanningEverydayPricing = 135,
        [EnumMember]
        PlanningPromotionPricing = 139,
        [EnumMember]
        PlanningKitPricing = 141,

        [EnumMember]
        TrackingHome = 128,

        [EnumMember]
        ReportingHome = 129,

        [EnumMember]
        AdministrationHome = 130,
        [EnumMember]
        AdministrationUserMaintenance = 134
    }

    [DataContract]
    public enum ModuleFeatureSearchGroupType
    {
        #region Common step types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        PlanningSearchFoldersAnalytics = 65, // Step Folders Analytics
        [EnumMember]
        PlanningSearchFoldersEveryday = 66, // Step Folders Everyday
        [EnumMember]
        PlanningSearchFoldersKits = 68, // Step Folders Kits
        [EnumMember]
        PlanningSearchFoldersPromotion = 67 // Step Folders Promotion
    }

    [DataContract]
    public enum ModuleFeatureStepType
    {
        #region Common step types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        StartupLoginInitialization = 143, // Step Initialization
        [EnumMember]
        StartupLoginAuthentication = 153, // Step 1) Authentication
        [EnumMember]
        StartupLoginChangepassword = 159, // Step 2) Change password
        
        [EnumMember]
        PlanningHomeDashboard = 144, // Step Dashboard
        [EnumMember]
        PlanningAnalyticsSearchAnalytics = 148, // Step Search Analytics
        [EnumMember]
        PlanningAnalyticsIdentity = 154, // Step 1) Identity
        [EnumMember]
        PlanningAnalyticsFilters = 160, // Step 2) Filters
        [EnumMember]
        PlanningAnalyticsPriceLists = 165, // Step 3) Price Lists
        [EnumMember]
        PlanningAnalyticsValueDrivers = 170, // Step 4) Value Drivers
        [EnumMember]
        PlanningAnalyticsResults = 174, // Step 5) Results
        
        [EnumMember]
        PlanningEverydayPricingSearchEveryday = 150, // Step Search Everyday
        [EnumMember]
        PlanningEverydayPricingIdentity = 156, // Step 1) Identity
        [EnumMember]
        PlanningEverydayPricingFilters = 162, // Step 2) Filters
        [EnumMember]
        PlanningEverydayPricingPriceLists = 167, // Step 3) Price Lists
        [EnumMember]
        PlanningEverydayPricingRounding = 171, // Step 4) Rounding
        [EnumMember]
        PlanningEverydayPricingStrategy = 175, // Step 5) Strategy
        [EnumMember]
        PlanningEverydayPricingResults = 178, // Step 6) Results
        [EnumMember]
        PlanningEverydayPricingForecast = 181, // Step 7) Forecast
        [EnumMember]
        PlanningEverydayPricingApproval = 184, // Step 8) Approval
        
        [EnumMember]
        PlanningPromotionPricingSearchPromotions = 151, // Step Search Promotions
        [EnumMember]
        PlanningPromotionPricingIdentity = 157, // Step 1) Identity
        [EnumMember]
        PlanningPromotionPricingFilters = 163, // Step 2) Filters
        [EnumMember]
        PlanningPromotionPricingPriceLists = 168, // Step 3) Price Lists
        [EnumMember]
        PlanningPromotionPricingRounding = 172, // Step 4) Rounding
        [EnumMember]
        PlanningPromotionPricingStrategy = 176, // Step 5) Strategy
        [EnumMember]
        PlanningPromotionPricingResults = 179, // Step 6) Results
        [EnumMember]
        PlanningPromotionPricingForecast = 182, // Step 7) Forecast
        [EnumMember]
        PlanningPromotionPricingApproval = 185, // Step 8) Approval
        
        [EnumMember]
        PlanningKitPricingSearchKits = 152, // Step Search Kits
        [EnumMember]
        PlanningKitPricingIdentity = 158, // Step 1) Identity
        [EnumMember]
        PlanningKitPricingFilters = 164, // Step 2) Filters
        [EnumMember]
        PlanningKitPricingPriceLists = 169, // Step 3) Price Lists
        [EnumMember]
        PlanningKitPricingRounding = 173, // Step 4) Rounding
        [EnumMember]
        PlanningKitPricingStrategy = 177, // Step 5) Strategy
        [EnumMember]
        PlanningKitPricingResults = 180, // Step 6) Results
        [EnumMember]
        PlanningKitPricingForecast = 183, // Step 7) Forecast
        [EnumMember]
        PlanningKitPricingApproval = 186, // Step 8) Approval
        
        [EnumMember]
        TrackingHomeDashboard = 145, // Step Dashboard
        
        [EnumMember]
        ReportingHomeDashboard = 146, // Step Dashboard
        
        [EnumMember]
        AdministrationHomeDashboard = 147, // Step Dashboard
        [EnumMember]
        AdministrationUserMaintenanceSearchUsers = 149, // Step Search Users
        [EnumMember]
        AdministrationUserMaintenanceUserSecurity = 155, // Step 1) User Security
        [EnumMember]
        AdministrationUserMaintenanceUserIdentity = 161, // Step 2) User Identity
        [EnumMember]
        AdministrationUserMaintenanceUserRole = 166 // Step 3) User Role
    }

    [DataContract]
    public enum ModuleFeatureStepActionType
    {
        #region Common action types...
        [EnumMember]
        Null = 0,
        #endregion

        [EnumMember]
        PlanningAnalyticsSearchAnalyticsNew = 187, // Workflow View Step Action, New Analytics
        [EnumMember]
        PlanningAnalyticsSearchAnalyticsEdit = 226, // Workflow View Step Action, Edit Analytics
        [EnumMember]
        PlanningAnalyticsSearchAnalyticsCopy = 246, // Workflow View Step Action, Copy Analytics
        [EnumMember]
        PlanningAnalyticsIdentitySave = 191, // Workflow View Step Action, Save Analytics Identity
        [EnumMember]
        PlanningAnalyticsIdentityCancel = 230, // Workflow View Step Action, Cancel Analytics Identity
        [EnumMember]
        PlanningAnalyticsFiltersSave = 195, // Workflow View Step Action, Save Analytics Filters
        [EnumMember]
        PlanningAnalyticsFiltersRun = 231, // Workflow View Step Action, Run Analytics Results with Filter update
        [EnumMember]
        PlanningAnalyticsFiltersCancel = 250, // Workflow View Step Action, Cancel Analytics Filters
        [EnumMember]
        PlanningAnalyticsPriceListsSave = 199, // Workflow View Step Action, Save Analytics Price Lists
        [EnumMember]
        PlanningAnalyticsPriceListsRun = 232, // Workflow View Step Action, Run Analytics Results with Price Lists update
        [EnumMember]
        PlanningAnalyticsPriceListsCancel = 251, // Workflow View Step Action, Cancel Analytics Price Lists
        [EnumMember]
        PlanningAnalyticsValueDriversSave = 203, // Workflow View Step Action, Save Analytics Value Drivers
        [EnumMember]
        PlanningAnalyticsValueDriversCancel = 233, // Workflow View Step Action, Cancel Analytics Value Drivers
        [EnumMember]
        PlanningAnalyticsResultsRun = 207, // Workflow View Step Action, Run Analytics Results
        
        [EnumMember]
        PlanningEverydayPricingSearchEverydayNew = 188, // Workflow View Step Action, New Everyday
        [EnumMember]
        PlanningEverydayPricingSearchEverydayEdit = 227, // Workflow View Step Action, Edit Everyday
        [EnumMember]
        PlanningEverydayPricingSearchEverydayCopy = 247, // Workflow View Step Action, Copy Everyday
        [EnumMember]
        PlanningEverydayPricingIdentitySave = 192, // Workflow View Step Action, Save Everyday Pricing Identity
        [EnumMember]
        PlanningEverydayPricingFiltersSave = 196, // Workflow View Step Action, Save Everyday Pricing Filters
        [EnumMember]
        PlanningEverydayPricingPriceListsSave = 200, // Workflow View Step Action, Save Everyday Pricing Price Lists
        [EnumMember]
        PlanningEverydayPricingRoundingSave = 204, // Workflow View Step Action, Save Everyday Pricing Rounding
        [EnumMember]
        PlanningEverydayPricingStrategySave = 208, // Workflow View Step Action, Save Everyday Pricing Strategy
        [EnumMember]
        PlanningEverydayPricingResultsWarnings = 211, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsWarningsHide = 240, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsWarningsShow = 220, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsRounding = 234, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsRoundingApply = 223, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsRoundingRemove = 243, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingResultsOptions = 252, // Workflow View Step Action, Options Everyday Pricing Results
        [EnumMember]
        PlanningEverydayPricingForecastNew = 214, // Workflow View Step Action, New Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingForecastSave = 237, // Workflow View Step Action, Save Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingForecastDelete = 255, // Workflow View Step Action, Delete Everyday Pricing Forecast
        [EnumMember]
        PlanningEverydayPricingApprovalSubmit = 217, // Workflow View Step Action, Submit Everyday Pricing Approval
        
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsNew = 189, // Workflow View Step Action, New Promotions
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsEdit = 228, // Workflow View Step Action, Edit Promotions
        [EnumMember]
        PlanningPromotionPricingSearchPromotionsCopy = 248, // Workflow View Step Action, Copy Promotions
        [EnumMember]
        PlanningPromotionPricingIdentitySave = 193, // Workflow View Step Action, Save Promotion Pricing Identity
        [EnumMember]
        PlanningPromotionPricingFiltersSave = 197, // Workflow View Step Action, Save Promotion Pricing Filters
        [EnumMember]
        PlanningPromotionPricingPriceListsSave = 201, // Workflow View Step Action, Save Promotion Pricing Price Lists
        [EnumMember]
        PlanningPromotionPricingRoundingSave = 205, // Workflow View Step Action, Save Promotion Pricing Rounding
        [EnumMember]
        PlanningPromotionPricingStrategySave = 209, // Workflow View Step Action, Save Promotion Pricing Strategy
        [EnumMember]
        PlanningPromotionPricingResultsWarnings = 212, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsWarningsHide = 241, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsWarningsShow = 221, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsRounding = 235, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsRoundingApply = 224, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsRoundingRemove = 244, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningPromotionPricingResultsOptions = 253, // Workflow View Step Action, Options Promotion Pricing Results
        [EnumMember]
        PlanningPromotionPricingForecastNew = 215, // Workflow View Step Action, New Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingForecastSave = 238, // Workflow View Step Action, Save Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingForecastDelete = 256, // Workflow View Step Action, Delete Promotion Pricing Forecast
        [EnumMember]
        PlanningPromotionPricingApprovalSubmit = 218, // Workflow View Step Action, Submit Promotion Pricing Approval
        
        [EnumMember]
        PlanningKitPricingSearchKitsNew = 190, // Workflow View Step Action, New Kits
        [EnumMember]
        PlanningKitPricingSearchKitsEdit = 229, // Workflow View Step Action, Edit Kits
        [EnumMember]
        PlanningKitPricingSearchKitsCopy = 249, // Workflow View Step Action, Copy Kits
        [EnumMember]
        PlanningKitPricingIdentitySave = 194, // Workflow View Step Action, Save Kit Pricing Identity
        [EnumMember]
        PlanningKitPricingFiltersSave = 198, // Workflow View Step Action, Save Kit Pricing Filters
        [EnumMember]
        PlanningKitPricingPriceListsSave = 202, // Workflow View Step Action, Save Kit Pricing Price Lists
        [EnumMember]
        PlanningKitPricingRoundingSave = 206, // Workflow View Step Action, Save Kit Pricing Rounding
        [EnumMember]
        PlanningKitPricingStrategySave = 210, // Workflow View Step Action, Save Kit Pricing Strategy
        [EnumMember]
        PlanningKitPricingResultsWarnings = 213, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsWarningsHide = 242, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsWarningsShow = 222, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsRounding = 236, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsRoundingApply = 225, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsRoundingRemove = 245, // Workflow View Step Action, Warnings Everyday Pricing Results
        [EnumMember]
        PlanningKitPricingResultsOptions = 254, // Workflow View Step Action, Options Kit Pricing Results
        [EnumMember]
        PlanningKitPricingForecastNew = 216, // Workflow View Step Action, New Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingForecastSave = 239, // Workflow View Step Action, Save Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingForecastDelete = 257, // Workflow View Step Action, Delete Kit Pricing Forecast
        [EnumMember]
        PlanningKitPricingApprovalSubmit = 219, // Workflow View Step Action, Submit Kit Pricing Approval
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
        AplUserRolePricingReviewer = 89, // Application (create none, edit none, view all, delete none, approve none, schedule none)
    }
}

