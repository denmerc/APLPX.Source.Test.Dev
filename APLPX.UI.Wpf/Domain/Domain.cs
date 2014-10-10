using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;

namespace APLPX.Client.Display
{


        public class Actions
        {
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }
            public string Key { get; set; }
            public List<Action> Items{ get; set; }
        }

        public class Action
        {
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }
            public int SortId { get; set; }
            public string Command { get; set; }
            public string CommandType { get; set; }
            public DisplayText DisplayText { get; set; }
            public List<string> Ancestors { get; set; }
            public List<string> Roles { get; set; }
            public Boolean IsEnabled { get; set; }
            public List<string> Parameters { get; set; }
        }

        public class DisplayText
        {
            public string Caption { get; set; }
            public string Header { get; set; }
            public string Tooltip { get; set; }
        }
        public class Tag
        {
            //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            //public string Id { get; set; }
            //public string Code { get; set; }
            //public string Description { get; set; }
            public string Value { get; set; }
            
        }

        public class FolderSet : ReactiveObject
        {
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }

            private List<string> _SelectedAnalyticFolders;
            public List<string> SelectedAnalyticFolders { get{ return _SelectedAnalyticFolders;} set{ this.RaiseAndSetIfChanged(ref _SelectedAnalyticFolders, value); } }
            public List<string> SelectedEverydayFolders { get; set; }
            public List<string> SelectedPromotionFolders { get; set; }
            public List<string> SelectedKitFolders { get; set; }

            public List<string> MasterAnalyticFolderList { get; set; }
            public List<string> MasterEverydayFolderList { get; set; }
            public List<string> MasterPromotionFolderList { get; set; }
            public List<string> MasterKitFolderList { get; set; }

        }

        //public enum Module
        //{
        //    Planning,
        //    Tracking,
        //    Reporting,
        //    Administration
        //}

        public enum SubModuleType
        {
            
            Analytics,
            Everyday,
            Promotions,
            Kits,
            MySettings,
            Search,
            AdminDefault
            
        }

        public enum SectionType
        {
            #region Common view types...

            #endregion

            [EnumMember]
            StartupLoginInitialization = 110, // Step 1) Initialization
            [EnumMember]
            StartupLoginAuthentication = 115, // Step 2) Authentication
            [EnumMember]
            StartupLoginChangePassword = 119, // Step 3) Change Password

            [EnumMember]
            PlanningHomeMyHomePage = 111, // Step 1) My Home Page
            [EnumMember]
            PlanningHomeMyOptimization = 177, // Step 2) My Optimization
            [EnumMember]
            PlanningHomeMyMarkuprules = 178, // Step 3) My Markup rules
            [EnumMember]
            PlanningHomeMyRoundingrules = 179, // Step 4) My Rounding rules

            [EnumMember]
            PlanningAnalyticsMyAnalytics = 112, // Step 1) My Analytics
            [EnumMember]
            PlanningAnalyticsIdentity = 116, // Step 2) Identity
            [EnumMember]
            PlanningAnalyticsFilters = 120, // Step 3) Filters
            [EnumMember]
            PlanningAnalyticsPriceLists = 123, // Step 4) Price Lists
            [EnumMember]
            PlanningAnalyticsValueDrivers = 126, // Step 5) Value Drivers
            [EnumMember]
            PlanningAnalyticsResults = 129, // Step 6) Results

            [EnumMember]
            PlanningPricingMyPricing = 113, // Step 1) My Pricing
            [EnumMember]
            PlanningPricingIdentity = 117, // Step 2) Identity
            [EnumMember]
            PlanningPricingFilters = 121, // Step 3) Filters
            [EnumMember]
            PlanningPricingPriceLists = 124, // Step 4) Price Lists
            [EnumMember]
            PlanningPricingRounding = 127, // Step 5) Rounding
            [EnumMember]
            PlanningPricingStrategy = 130, // Step 6) Strategy
            [EnumMember]
            PlanningPricingResults = 132, // Step 7) Results
            [EnumMember]
            PlanningPricingForecast = 134, // Step 8) Forecast
            [EnumMember]
            PlanningPricingApproval = 135, // Step 9) Approval

            [EnumMember]
            AdministrationUserMaintenance = 114, // Step 1) User Maintenance
            [EnumMember]
            AdministrationPricelists = 118, // Step 2) Price lists
            [EnumMember]
            AdministrationOptimization = 128, // Step 3) Optimization
            [EnumMember]
            AdministrationMarkuprules = 122, // Step 4) Markup rules
            [EnumMember]
            AdministrationRoundingrules = 125, // Step 5) Rounding rules
            [EnumMember]
            AdministrationRollback = 133, // Step 6) Rollback
            [EnumMember]
            AdministrationProcesses = 131, // Step 7) Processes
            [EnumMember]
            AdministrationFolders = 139, // Step 7) Processes,
            [EnumMember]
            Null = 0, // Step 7) Processes

        }


        public enum AnalyticStepType
        {
            Identity,
            Filters,
            ValueDrivers
        }

        public enum PricingStepType
        {
            Identity,
            Filters,
            PriceLists,
            Rounding,
            Strategy,
            Results,
            Forecasts,
            Approval
        }


        public class Session
        {
            public User User { get; set; }
            public List<Analytic> Analytics { get; set; } // TODO: last three?
            public List<PriceRoutine> PriceRoutines { get; set; }
            public List<string> Tags { get; set; }



        }

        public class User
        {

            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }
            public string Login { get; set; }
            public string Name { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public List<Role> Roles { get; set; }
            public Boolean IsAuthenticated { get; set; }

            public List<string> FavoriteATags { get; set; }
            public List<string> FavoritePTags { get; set; }
            //public List<Analytic> RecentAnalytics { get; set; }
            //public List<PriceRoutine> RecentPriceRoutines { get; set; }
            
            //public Dictionary<Action, EntityBase> RecentHistory { get; set; } //instead of ViewModelBase
            //public List<PriceRoutine> Folders { get; set; }
            //public class Role
            //{
            //    List<Action> Permissions { get; set; } //which actions can they execute
            //}

        }

        public enum Role
        {
            PricingAnalyst,
            Administrator,
            Reviewer
        }
        //[BsonKnownTypes(typeof(Filter))]
        public class Analytic : ReactiveUI.ReactiveObject //TODO: ReactiveValidatedEntity
        {
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }
            
            public string Name { get; set; }
            
            public string Status { get; set; }
            
            public string Description { get; set; }
            public DateTime LastUpdated { get; set; }
            public string LastUserUpdated { get; set; }
            public string Comments { get; set; }
            //public string Group1 { get; set; }
            //public string Group2 { get; set; }
            //public string Folder { get; set; }

            private List<string> _Tags;
            public List<string> Tags { get { return _Tags; } set { this.RaiseAndSetIfChanged(ref _Tags, value); } }
            public List<string> FavoriteTags { get; set; }
            //public IList<string> Drivers { get; set; }
            public bool Shared { get; set; }
            public List<FilterSet> Filters { get; set; }
            [BsonElement("Drivers")]
            public List<ValueDriver> Drivers { get; set; }
            public List<PriceScheme> PriceSchemes { get; set; }
            //public List<PriceRoutine> RelatedPriceRoutinesIdentities { get; set; }
            //public List<Action> Actions { get; set; } // usually navigates to step in module eg - for analytic->filters, price lists, rounding
        }

        public class PriceRoutine
        {
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }
            public PriceRoutineType @Type { get; set; } //everyday, promo, kits
            public string Name { get; set; }
            public string Description { get; set; }
            public Boolean Shared { get; set; }
            public List<string> Tags { get; set; }
            public DateTime LastUpdated { get; set; }
            public string LastUserUpdated { get; set; }
            public string Owner { get; set; }

            public Analytic Analytic { get; set; }
            public List<FilterSet> Filters { get; set; }

            public List<PriceScheme> PriceSchemes { get; set; }
            public List<ValueDriver> Drivers { get; set; }

            //public List<string> AssignedAnalytics { get; set; }
            //public List<Action> Actions { get; set; }
            //public List<RoundingScheme> RoundingSchemes { get; set; }
            //public List<PriceList> PriceLists { get; set; }
        }

        public class PriceScheme
        {
            public PricingMode PricingMode { get; set; }
            public List<PriceList> PriceLists { get; set; }
        }


        public enum PriceListType
        {
            Cost,
            Sales,

        }

        public class PriceList
        {
            public int SortId { get; set; }
            public string Type { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public Boolean IsKey { get; set; }
            public List<RoundingScheme> RoundingRules { get; set; }
        }

        public enum PricingMode
        {
            Single,
            Cascade,
            Global,
            GlobalPlus
        }
    

        public class RoundingScheme
        {
            
            public int SortId { get; set; }
            public Double Min { get; set; }
            public Double Max { get; set; }
            public string RoundToValue { get; set; }
            public string RoundingType { get; set; }
            //public string Lower { get; set; }
            //public string Upper { get; set; }
            //public int RoundTo { get; set; }
        }


        public enum PriceRoutineType
        {
            EveryDay,
            Promo,
            Kit
        }

        public enum ModuleType
        {
            Planning,
            Tracking,
            Reporting,
            Administration
        }

        public enum ValueDriverType
        {
            Movement,
            Markup,
            DaysOnHand,
            DaysLeadTime,
            InStockRatio,
            SalesTrendRatio
        }

        public class ValueDriver
        {
            public ValueDriverType @Type { get; set; }
            public Mode Mode { get; set;}
            public List<Group> Groups { get; set; }
            
        }

        public enum Mode
	    {
	        Auto,
            Manual
	    }

        public class Group
        {
            public int LineItemId { get; set; }
            public string GroupName { get; set; }
            public int SkuCount { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
            public double SalesValue { get; set; }
        }


        public enum FilterType
        {
            VendorCode,
            IsKit,
            OnSale,
            Category,
            DiscountType,
            StatusType,
            ProductType,
            StockClass
        }

        

        public class Filter : ReactiveObject
        {
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }

            private Boolean _isSelected;
            public Boolean IsSelected { get { return _isSelected; } set { this.RaiseAndSetIfChanged(ref _isSelected, value); } } //TODO: should be in viewmodel or in datastore?

            private string _code;
            [BsonElement("code")]
            public string Code { get { return _code; } set { this.RaiseAndSetIfChanged(ref _code, value); } }
            private string _description;
            [BsonElement("description")]
            public string Description { get { return _description; } set { this.RaiseAndSetIfChanged(ref _description, value); } }

            private FilterType _filterType;
            public FilterType Type { get { return _filterType; } set { this.RaiseAndSetIfChanged(ref _filterType, value); } }
        }

        public class FilterSet
        {
            public string Type { get; set; }
            public List<Filter> Items { get; set; }
        }

        public class EntityBase
        {
            
        }
    
}
