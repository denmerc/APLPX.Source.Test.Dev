using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Configuration;
using MongoDB.Driver.Builders;
//using Domain = APLPX.Client.Display;
using ENT = APLPX.Entity;
using DTO = APLPX.Common.Mock.Entity;
using APLPX.Client.Contracts;
using APLPX.Client.Mock.Mappers;

namespace APLPX.Client.Mock.Proxies
{
    public class MockUserClient : IUserService
    {

        public MockUserClient()
        {
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(databaseName);

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private readonly string connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
        private const string databaseName = "promo";
        //private const string TagsCollectionName = "tags";
        //public MongoDatabase Database;

        private MongoClient client { get; set; }
        protected MongoServer server { get; set; }
        protected MongoDatabase database { get; set; }

        public MongoCollection<DTO.User> Users
        {
            get
            {
                return database.GetCollection<DTO.User>("User");
            }
        }

        public MongoCollection<DTO.Module> Modules
        {
            get
            {
                return database.GetCollection<DTO.Module>("AllModules_Role");
            }
        }

        public MongoCollection<DTO.SessionList> Sessions
        {
            get
            {
                return database.GetCollection<DTO.SessionList>("Sessions");
            }
        }

        public List<DTO.FilterGroup> FilterGroups
        {
            get
            {
                return database.GetCollection<DTO.FilterGroup>("FilterGroups").AsQueryable().ToList();
            }
        }


        public ENT.Session<ENT.NullT> Initialize(ENT.Session<ENT.NullT> session)
        {
            return new ENT.Session<ENT.NullT>();
        }

        public ENT.Session<ENT.NullT> Authenticate(ENT.Session<ENT.NullT> session)
        {
            
            try
            {
                
                var user = Users.AsQueryable()
                    .Where(x => x.Credential.Login == session.User.Credential.Login 
                                && x.Credential.OldPassword == session.User.Credential.OldPassword)
                                .FirstOrDefault();
                if (user == null)
                {
                    return new ENT.Session<ENT.NullT>()
                    {
                        SessionOk = false
                    };
                }

                //var qLicMods = Query.ElemMatch("Roles", Query.EQ("Id", 3));
                //var qLicFeatures = Query.ElemMatch("Features.Roles" , Query.EQ( "Id" , 3));
                //var q = Query.And(new IMongoQuery[] {qLicFeatures, qLicMods });
                //var lModsandFeats = Modules.Find(qLicFeatures);
                
                //find  session based on 
                var owner = user.Identity.FirstName + " " + user.Identity.LastName;
                var s = Sessions.AsQueryable().Where(x => x.Owner == owner).FirstOrDefault();

                //var modules = Modules.AsQueryable().ToList();
              
                //var licensedMods = from m in modules
                //                   where m.Roles.Any(r => r.Id == user.Role.Id)
                //                         select new Module
                //                         {
                //                            Type = m.Type,
                //                            Name =  m.Name,
                //                            Title = m.Title,
                //                            Sort = m.Sort,
                //                            Roles = m.Roles,
                //                            Features = m.Features.Where( fe => fe.Roles.Any( r => r.Id == user.Role.Id)).ToList()
                //                         };

                foreach (var item in s.Modules.SelectMany(f => f.Features))
	            {
                    if (item.SearchGroups == null) { item.SearchGroups = new List<DTO.FeatureSearchGroup>(); }
	            }

                //foreach (var item in s.Analytics)
                //{
                //    if (item.FilterGroups == null) { item.FilterGroups = new List<DTO.FilterGroup>(); }
                //    if (item.PriceListGroups == null) { item.PriceListGroups = new List<DTO.AnalyticPriceListGroup>(); }
                //    if (item.ValueDrivers == null) { item.ValueDrivers = new List<DTO.AnalyticValueDriver>(); }

                //}


                //foreach (var item in s.Pricing)
                //{
                //    if (item.FilterGroups == null) { item.FilterGroups = new List<DTO.FilterGroup>(); }
                //    if (item.ValueDrivers == null) { item.ValueDrivers = new List<DTO.PricingEverydayValueDriver>(); }
                //    if (item.LinkedValueDrivers == null) { item.LinkedValueDrivers = new List<DTO.PricingEverydayLinkedValueDriver>(); }
                //    if (item.LinkedPriceListRules == null) { item.LinkedPriceListRules = new List<DTO.PricingLinkedPriceListRule>(); }

                //    if (item.Results == null) { item.Results = new List<DTO.PricingEverydayResult>(); }
                //}
                //foreach (var item in s.Pricing)
                //{
                //    if(item.KeyValueDriver != null)
                //    {
                //        foreach (var g in s.Pricing.SelectMany(x => x.KeyValueDriver.Groups))
                //        {
                //            if (g.MarkupRules == null) { g.MarkupRules = new List<DTO.PriceMarkupRule>(); }
                //            if (g.OptimizationRules == null) { g.OptimizationRules = new List<DTO.PriceOptimizationRule>(); }
                //        }
                //    }
                //}

                return new ENT.Session<ENT.NullT>()
                {
                    User = new ENT.User
                    {
                        Credential = new ENT.UserCredential { Login = user.Credential.Login },
                        Id = user.Id,
                        
                        Identity = new ENT.UserIdentity { 
                                                            Active = user.Identity.Active, 
                                                            Created = user.Identity.Created,
                                                            CreatedText = user.Identity.CreatedText,
                                                            Edited = user.Identity.Edited,
                                                            EditedText = user.Identity.EditedText,
                                                            Editor = user.Identity.Editor,
                                                            Email = user.Identity.Email,
                                                            FirstName = user.Identity.FirstName,
                                                            Greeting = user.Identity.Greeting,
                                                            LastLogin = user.Identity.LastLogin,
                                                            LastLoginText = user.Identity.LastLoginText,
                                                            LastName = user.Identity.LastName,
                                                            Name = user.Identity.Name
                                                          
                        },

                        Key = "9f8a3400-cf1b-4d0d-b157-def9c105be35",
                        Role = new ENT.UserRole { Id = user.Role.Id, Description = user.Role.Description, Name = user.Role.Name }
                        
                    },
                    Modules = s.Modules.ToDTOs(),
                    //Modules = (from mod in s.Modules
                    //           select new ENT.Module
                    //           {
                    //               Name = mod.Name,
                    //               Sort = mod.Sort,
                    //               Title = mod.Title,
                    //               Type = mod.Type,
                    //               Features = (from f in mod.Features
                    //                           select new ENT.ModuleFeature
                    //                           {
                    //                               ActionStepType = f.ActionStepType,
                    //                               LandingStepType = f.LandingStepType,
                    //                               Name = f.Name,
                    //                               //Roles = (from r in f.Roles
                    //                               //         select new ENT.UserRole
                    //                               //         {
                    //                               //             Description = r.Description,
                    //                               //             Id = r.Id,
                    //                               //             Name = r.Name
                    //                               //         }).ToList(),
                    //                               SearchGroups = (from sg in f.SearchGroups
                    //                                               select new ENT.FeatureSearchGroup
                    //                                               {
                    //                                                   CanNameChange = sg.CanNameChange,
                    //                                                   CanSearchGroupChange = sg.CanSearchGroupChange,
                    //                                                   IsNameChanged = sg.IsNameChanged,
                    //                                                   IsSearchGroupChanged = sg.IsSearchGroupChanged,
                    //                                                   ItemCount = sg.ItemCount,
                    //                                                   Name = sg.Name,
                    //                                                   ParentName = sg.ParentName,
                    //                                                   SearchGroupKey = sg.SearchGroupKey,
                    //                                                   Sort = sg.Sort
                    //                                               }).ToList(),
                    //                               Sort = f.Sort,
                    //                               Steps = (from st in f.Steps
                    //                                        select new ENT.ModuleFeatureStep
                    //                                        {
                    //                                            Actions = (from a in st.Actions
                    //                                                       select new ENT.ModuleFeatureStepAction
                    //                                                       {
                    //                                                           Name = a.Name,
                    //                                                           ParentName = a.ParentName,
                    //                                                           Sort = a.Sort,
                    //                                                           Title = a.Title,
                    //                                                           Type = a.Type
                    //                                                       }).ToList(),
                    //                                            Name = st.Name,
                    //                                            Sort = st.Sort,
                    //                                            Title = st.Title,
                    //                                            Type = st.Type
                    //                                        }).ToList(),
                    //                               Title = f.Title,
                    //                               Type = f.Type
                    //                           }).ToList()
                    //               //,

                    //               //Roles = (from r in mod.Roles select new ENT.UserRole { Name = r.Name }).ToList()
                    //           }).ToList(),
                    //Analytics = (from a in s.Analytics
                    //             select new ENT.Analytic
                    //             {
                    //                 Id = a.Id,
                    //                 SearchGroupKey = a.SearchGroupKey,
                    //                 Identity = new ENT.AnalyticIdentity
                    //                 {
                    //                     Active = a.Identity.Active,
                    //                     Author = a.Identity.Author,
                    //                     Created = a.Identity.Created,
                    //                     CreatedText = a.Identity.CreatedText,
                    //                     Description = a.Identity.Description,
                    //                     Edited = a.Identity.Edited,
                    //                     EditedText = a.Identity.EditedText,
                    //                     Editor = a.Identity.Editor,
                    //                     Name = a.Identity.Name,
                    //                     Notes = a.Identity.Notes,
                    //                     Owner = a.Identity.Owner,
                    //                     Refreshed = a.Identity.Refreshed,
                    //                     RefreshedText = a.Identity.RefreshedText,
                    //                     Shared = a.Identity.Shared

                    //                 },
                    //FilterGroups = (from fg in a.FilterGroups
                    //                select new ENT.FilterGroup
                    //                {
                    //                    Name = fg.Name,
                    //                    Sort = fg.Sort,
                    //                    Filters = (from f in fg.Filters
                    //                               select new ENT.Filter
                    //                               {
                    //                                   Code = f.Code,
                    //                                   Id = f.Id,
                    //                                   IsSelected = f.IsSelected,
                    //                                   Key = f.Key,
                    //                                   Name = f.Name,
                    //                                   Sort = f.Sort
                    //                               }).ToList()
                    //                }).ToList(),
                    //                 ValueDrivers = (from vd in a.ValueDrivers
                    //                                 select new ENT.AnalyticValueDriver
                    //                                 {
                    //                                     Id = vd.Id,
                    //                                     IsSelected = vd.IsSelected,
                    //                                     Key = vd.Key,
                    //                                     Name = vd.Name,
                    //                                     Sort = vd.Sort,
                    //                                     Title = vd.Title,
                    //                                     Modes = (from d in vd.Modes
                    //                                              select new ENT.AnalyticValueDriverMode
                    //                                              {
                    //                                                  Name = d.Name,
                    //                                                  Title = d.Title,
                    //                                                  Sort = d.Sort,
                    //                                                  Key = d.Key,
                    //                                                  IsSelected = d.IsSelected,
                    //                                                  Groups = (from g in d.Groups
                    //                                                            select new ENT.ValueDriverGroup
                    //                                                            {
                    //                                                                Id = g.Id,
                    //                                                                MaxOutlier = g.MaxOutlier,
                    //                                                                MinOutlier = g.MinOutlier,
                    //                                                                Sort = g.Sort,
                    //                                                                Value = g.Value
                    //                                                            }).ToList()
                    //                                              }).ToList()
                    //                                 }).ToList()
                    //             }).ToList(),
                    //Pricing = (from p in s.Pricing
                    //           select new ENT.PricingEveryday
                    //           {
                    //FilterGroups = (from fg in p.FilterGroups
                    //                select new ENT.FilterGroup
                    //                {
                    //                    Name = fg.Name,
                    //                    Sort = fg.Sort,
                    //                    Filters = (from f in fg.Filters
                    //                               select new ENT.Filter
                    //                               {
                    //                                   Code = f.Code,
                    //                                   Id = f.Id,
                    //                                   IsSelected = f.IsSelected,
                    //                                   Key = f.Key,
                    //                                   Name = f.Name,
                    //                                   Sort = f.Sort
                    //                               }).ToList()
                    //                }).ToList(),
                    //Id = p.Id,
                    //Identity = new ENT.PricingIdentity
                    //{
                    //    Active = p.Identity.Active,
                    //    Author = p.Identity.Author,
                    //    Created = p.Identity.Created,
                    //    CreatedText = p.Identity.CreatedText,
                    //    Description = p.Identity.Description,
                    //    Edited = p.Identity.Edited,
                    //    EditedText = p.Identity.EditedText,
                    //    Editor = p.Identity.Editor,
                    //    Name = p.Identity.Name,
                    //    Notes = p.Identity.Notes,
                    //    Owner = p.Identity.Owner,
                    //    Refreshed = p.Identity.Refreshed,
                    //    RefreshedText = p.Identity.RefreshedText,
                    //    Shared = p.Identity.Shared,
                    //},
                    //KeyPriceListRule = new ENT.PricingKeyPriceListRule
                    //{
                    //    DollarRangeLower = p.KeyPriceListRule.DollarRangeLower,
                    //    DollarRangeUpper = p.KeyPriceListRule.DollarRangeUpper,
                    //    PriceListId = p.KeyPriceListRule.PriceListId,
                    //    RoundingRules = (from ru in p.KeyPriceListRule.RoundingRules
                    //                     select new ENT.PriceRoundingRule
                    //                     {
                    //                         DollarRangeLower = ru.DollarRangeLower,
                    //                         DollarRangeUpper = ru.DollarRangeUpper,
                    //                         Id = ru.Id,
                    //                         Type = ru.Type,
                    //                         ValueChange = ru.ValueChange
                    //                     }).ToList(),
                    //    RoundingTypes = (from rt in p.KeyPriceListRule.RoundingTypes
                    //                     select new ENT.SQLEnumeration
                    //                     {
                    //                         Description = rt.Description,
                    //                         Name = rt.Name,
                    //                         Sort = rt.Sort,
                    //                         Value = rt.Value
                    //                     }).ToList()

                    //},
                    //KeyValueDriver = new ENT.PricingEverydayKeyValueDriver
                    //{
                    //    Groups = (from g in p.KeyValueDriver.Groups
                    //              select new ENT.PricingEverydayKeyValueDriverGroup
                    //              {
                    //                  MarkupRules = (from r in g.MarkupRules
                    //                                 select new ENT.PriceMarkupRule
                    //                                 {
                    //                                     DollarRangeLower = r.DollarRangeLower,
                    //                                     DollarRangeUpper = r.DollarRangeUpper,
                    //                                     Id = r.Id,
                    //                                     PercentLimitLower = r.PercentLimitLower,
                    //                                     PercentLimitUpper = r.PercentLimitUpper
                    //                                 }).ToList(),
                    //                  OptimizationRules = (from o in g.OptimizationRules
                    //                                       select new ENT.PriceOptimizationRule
                    //                                       {
                    //                                           DollarRangeLower = o.DollarRangeLower,
                    //                                           DollarRangeUpper = o.DollarRangeUpper,
                    //                                           Id = o.Id,
                    //                                           PercentChange = o.PercentChange
                    //                                       }).ToList(),
                    //                  ValueDriverGroupId = g.ValueDriverGroupId
                    //              }).ToList(),
                    //    ValueDriverId = p.KeyValueDriver.ValueDriverId


                    //},
                    //LinkedPriceListRules = (from lpr in p.LinkedPriceListRules
                    //                        select new ENT.PricingLinkedPriceListRule
                    //                        {
                    //                            PercentChange = lpr.PercentChange,
                    //                            PriceListId = lpr.PriceListId,
                    //                            RoundingRules = (from rr in lpr.RoundingRules
                    //                                             select new ENT.PriceRoundingRule
                    //                                             {
                    //                                                 DollarRangeLower = rr.DollarRangeLower,
                    //                                                 DollarRangeUpper = rr.DollarRangeUpper,
                    //                                                 Id = rr.Id,
                    //                                                 Type = rr.Type,
                    //                                                 ValueChange = rr.ValueChange
                    //                                             }).ToList()
                    //                        }).ToList(),
                    //LinkedValueDrivers = (from lvd in p.LinkedValueDrivers
                    //                      select new ENT.PricingEverydayLinkedValueDriver
                    //                      {
                    //                          Groups = (from g in lvd.Groups
                    //                                    select new ENT.PricingEverydayLinkedValueDriverGroup
                    //                                    {
                    //                                        PercentChange = g.PercentChange,
                    //                                        ValueDriverGroupId = g.ValueDriverGroupId
                    //                                    }).ToList()
                    //                      }).ToList(),
                    //PriceListGroups = (from plg in p.PriceListGroups
                    //                   select new ENT.PricingEverydayPriceListGroup
                    //                   {
                    //                       Key = plg.Key,
                    //                       Name = plg.Name,
                    //                       PriceLists = (from pl in plg.PriceLists
                    //                                     select new ENT.PricingEverydayPriceList
                    //                                     {
                    //                                         Code = pl.Code,
                    //                                         Id = pl.Id,
                    //                                         IsKey = pl.IsKey,
                    //                                         IsSelected = pl.IsSelected,
                    //                                         Key = pl.Key,
                    //                                         Name = pl.Name,
                    //                                         Sort = pl.Sort,
                    //                                         Title = pl.Title
                    //                                     }).ToList(),
                    //                       Sort = plg.Sort,
                    //                       Title = plg.Title
                    //                   }).ToList(),
                    //PricingModes = (from pm in p.PricingModes
                    //                select new ENT.PricingMode
                    //                {
                    //                    HasKeyPriceListRule = pm.HasKeyPriceListRule,
                    //                    HasLinkedPriceListRule = pm.HasLinkedPriceListRule,
                    //                    IsSelected = pm.IsSelected,
                    //                    Key = pm.Key,
                    //                    KeyPriceListGroupKey = pm.KeyPriceListGroupKey,
                    //                    LinkedPriceListGroupKey = pm.LinkedPriceListGroupKey,
                    //                    Name = pm.Name,
                    //                    Sort = pm.Sort,
                    //                    Title = pm.Title
                    //                }).ToList(),
                    //Results = (from r in p.Results
                    //           select new ENT.PricingEverydayResult
                    //           {
                    //               Groups = (from g in r.Groups
                    //                         select new ENT.PricingResultDriverGroup
                    //                         {
                    //                             Actual = g.Actual,
                    //                             Id = g.Id,
                    //                             MaxOutlier = g.MaxOutlier,
                    //                             MinOutlier = g.MinOutlier,
                    //                             Name = g.Name,
                    //                             SalesValue = g.SalesValue,
                    //                             SkuCount = g.SkuCount,
                    //                             Sort = g.Sort,
                    //                             Title = g.Title,
                    //                             Value = g.Value
                    //                         }).ToList(),
                    //               PriceLists = (from pl in r.PriceLists
                    //                             select new ENT.PricingEverydayResultPriceList
                    //                             {
                    //                                 Code = pl.Code,
                    //                                 CurrentMarkupPercent = pl.CurrentMarkupPercent,
                    //                                 CurrentPrice = pl.CurrentPrice,
                    //                                 Id = pl.Id,
                    //                                 IsKey = pl.IsKey,
                    //                                 IsSelected = pl.IsSelected,
                    //                                 InfluenceValueChange = pl.InfluenceValueChange,
                    //                                 Key = pl.Key,
                    //                                 KeyValueChange = pl.KeyValueChange,
                    //                                 Name = pl.Name,
                    //                                 NewMarkupPercent = pl.NewMarkupPercent,
                    //                                 NewPrice = pl.NewPrice,
                    //                                 PriceChange = pl.PriceChange,
                    //                                 PriceEdit = pl.PriceEdit,
                    //                                 PriceWarning = pl.PriceWarning,
                    //                                 ResultId = pl.ResultId,
                    //                                 Sort = pl.Sort,
                    //                                 Title = pl.Title

                    //                             }).ToList(),
                    //               SkuId = r.SkuId,
                    //               SkuName = r.SkuName,
                    //               SkuTitle = r.SkuTitle
                    //           }).ToList(),
                    //SearchGroupKey = p.SearchGroupKey,
                    //ValueDrivers = (from vd in p.ValueDrivers
                    //                select new ENT.PricingEverydayValueDriver
                    //                {
                    //                    Groups = (from g in vd.Groups
                    //                              select new ENT.PricingValueDriverGroup
                    //                              {
                    //                                  Id = g.Id,
                    //                                  MaxOutlier = g.MaxOutlier,
                    //                                  MinOutlier = g.MinOutlier,
                    //                                  SalesValue = g.SalesValue,
                    //                                  SkuCount = g.SkuCount,
                    //                                  Sort = g.Sort,
                    //                                  Value = g.Value
                    //                              }).ToList(),

                    //                    Id = vd.Id,
                    //                    IsKey = vd.IsKey,
                    //                    IsSelected = vd.IsSelected,
                    //                    Key = vd.Key,
                    //                    Name = vd.Name,
                    //                    Sort = vd.Sort,
                    //                    Title = vd.Title
                    //                }).ToList()
                    //}).ToList(),
                    //FilterGroups = (from fg in FilterGroups
                    //                select new ENT.FilterGroup
                    //                {
                    //                    Name = fg.Name,
                    //                    Sort = fg.Sort,
                    //                    Filters = (from f in fg.Filters
                    //                               select new ENT.Filter
                    //                               {
                    //                                   Code = f.Code,
                    //                                   Id = f.Id,
                    //                                   IsSelected = f.IsSelected,
                    //                                   Key = f.Key,
                    //                                   Name = f.Name,
                    //                                   Sort = f.Sort
                    //                               }).ToList()
                    //                }).ToList(),
                    //Mo<pdules = licensedMods.ToList(),
                    //Modules = lModsandFeats.ToList(), 
                    SessionOk = true
                };


            }
            catch (Exception)
            {

                return new ENT.Session<ENT.NullT>()
                {
                    SessionOk = false,
                    ClientMessage = "Failed to connect."
                };
            }
            
            //var mods = Modules.AsQueryable().SelectMany( m=> m.Features).SelectMany( f => f.Roles).Where( r => r.Id == user.Role.Id);
        }

        public ENT.Session<ENT.UserIdentity> LoadIdentity(ENT.Session<ENT.UserIdentity> session)
        {
            throw new NotImplementedException();
        }

        public ENT.Session<ENT.UserIdentity> SaveIdentity(ENT.Session<ENT.UserIdentity> session)
        {
            throw new NotImplementedException();
        }

        public ENT.Session<ENT.NullT> SavePassword(ENT.Session<ENT.NullT> session)
        {
            throw new NotImplementedException();
        }


        public ENT.Session<List<ENT.User>> LoadList(ENT.Session<ENT.NullT> session)
        {
            throw new NotImplementedException();
        }

        public ENT.Session<ENT.User> LoadUser(ENT.Session<ENT.User> session)
        {
            throw new NotImplementedException();
        }

        public ENT.Session<ENT.User> SaveUser(ENT.Session<ENT.User> session)
        {
            throw new NotImplementedException();
        }


    }



}
