using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using APLPX.Client.Contracts;
using APLPX.Entity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using DTO = APLPX.Common.Mock.Entity;
using APLPX.Client.Mock.MockEntities;

namespace APLPX.Client.Mock
{
    public class MockPricingEverydayClient : IPricingEverydayService
    {

        private readonly string connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
        private const string databaseName = "promo";

        public MockPricingEverydayClient()
        {
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(databaseName);
        }

        private MongoClient client { get; set; }
        protected MongoServer server { get; set; }
        protected MongoDatabase database { get; set; }

        //public MongoCollection<Analytic> Analytics
        //{
        //    get
        //    {
        //        return database.GetCollection<Analytic>("Analytics");
        //    }
        //}

        public MongoCollection<DTO.PricingEveryday> PricingEveryday
        {
            get
            {
                return database.GetCollection<DTO.PricingEveryday>("PricingAll_NoFilters");
            }
        }

        public MongoCollection<DTO.PricingResults> Results
        {
            get
            {
                return database.GetCollection<DTO.PricingResults>("Results");
            }
        }

        public Session<PricingEveryday> LoadPricingEveryday(Session<PricingEveryday> session)
        {
            //linkedvaluedrivers & results null check

            var pe = session.Data as PricingEveryday;
            var newPE = PricingEveryday.AsQueryable()
                .Where(x => x.Id == pe.Id).SingleOrDefault();

            if (newPE.ValueDrivers == null) { newPE.ValueDrivers = new List<DTO.PricingEverydayValueDriver>(); }
            if (newPE.LinkedValueDrivers == null ) { newPE.LinkedValueDrivers = new List<DTO.PricingEverydayLinkedValueDriver>();}
            if (newPE.Results == null) { newPE.Results = new List<DTO.PricingEverydayResult>(); }
            foreach (var item in newPE.KeyValueDriver.Groups)
            {
                if (item.MarkupRules == null) { item.MarkupRules = new List<DTO.PriceMarkupRule>(); }
                if (item.OptimizationRules == null) { item.OptimizationRules = new List<DTO.PriceOptimizationRule>();}
            }
            return new Session<PricingEveryday>
            {
                Data = new PricingEveryday
                {
                  
                    FilterGroups = (from fg in newPE.FilterGroups
                                    select new FilterGroup
                                    {
                                        Name = fg.Name,
                                        Sort = fg.Sort,
                                        Filters = (from f in fg.Filters
                                                   select new Filter
                                                   {
                                                       Code = f.Code,
                                                       Id = f.Id,
                                                       IsSelected = f.IsSelected,
                                                       Key = f.Key,
                                                       Name = f.Name,
                                                       Sort = f.Sort
                                                   }).ToList()
                                    }).ToList(),
                    Id = newPE.Id,
                    Identity = new PricingIdentity
                    {
                        Active = newPE.Identity.Active,
                        Author = newPE.Identity.Author,
                        Created = newPE.Identity.Created,
                        CreatedText = newPE.Identity.CreatedText,
                        Description = newPE.Identity.Description,
                        Edited = newPE.Identity.Edited,
                        EditedText = newPE.Identity.EditedText,
                        Editor = newPE.Identity.Editor,
                        Name = newPE.Identity.Name,
                        Notes = newPE.Identity.Notes,
                        Owner = newPE.Identity.Owner,
                        Refreshed = newPE.Identity.Refreshed,
                        RefreshedText = newPE.Identity.RefreshedText,
                        Shared = newPE.Identity.Shared,
                    },
                    KeyPriceListRule = new PricingKeyPriceListRule
                    {
                        DollarRangeLower = newPE.KeyPriceListRule.DollarRangeLower,
                        DollarRangeUpper = newPE.KeyPriceListRule.DollarRangeUpper,
                        PriceListId = newPE.KeyPriceListRule.PriceListId,
                        RoundingRules = (from ru in newPE.KeyPriceListRule.RoundingRules
                                         select new PriceRoundingRule
                                         {
                                             DollarRangeLower = ru.DollarRangeLower,
                                             DollarRangeUpper = ru.DollarRangeUpper,
                                             Id = ru.Id,
                                             Type = ru.Type,
                                             ValueChange = ru.ValueChange
                                         }).ToList(),
                        RoundingTypes = (from rt in newPE.KeyPriceListRule.RoundingTypes
                                         select new SQLEnumeration
                                         {
                                             Description = rt.Description,
                                             Name = rt.Name,
                                             Sort = rt.Sort,
                                             Value = rt.Value
                                         }).ToList()

                    },
                    KeyValueDriver = new PricingEverydayKeyValueDriver
                    {
                        Groups = (from g in newPE.KeyValueDriver.Groups
                                  select new PricingEverydayKeyValueDriverGroup
                                  {
                                      MarkupRules = (from r in g.MarkupRules
                                                     select new PriceMarkupRule
                                                     {
                                                         DollarRangeLower = r.DollarRangeLower,
                                                         DollarRangeUpper = r.DollarRangeUpper,
                                                         Id = r.Id,
                                                         PercentLimitLower = r.PercentLimitLower,
                                                         PercentLimitUpper = r.PercentLimitUpper
                                                     }).ToList(),
                                      OptimizationRules = (from o in g.OptimizationRules
                                                           select new PriceOptimizationRule
                                                           {
                                                               DollarRangeLower = o.DollarRangeLower,
                                                               DollarRangeUpper = o.DollarRangeUpper,
                                                               Id = o.Id,
                                                               PercentChange = o.PercentChange
                                                           }).ToList(),
                                      ValueDriverGroupId = g.ValueDriverGroupId
                                  }).ToList(),
                        ValueDriverId = newPE.KeyValueDriver.ValueDriverId


                    },
                    LinkedPriceListRules = (from lpr in newPE.LinkedPriceListRules
                                            select new PricingLinkedPriceListRule
                                            {
                                                PercentChange = lpr.PercentChange,
                                                PriceListId = lpr.PriceListId,
                                                RoundingRules = (from rr in lpr.RoundingRules
                                                                 select new PriceRoundingRule
                                                                 {
                                                                     DollarRangeLower = rr.DollarRangeLower,
                                                                     DollarRangeUpper = rr.DollarRangeUpper,
                                                                     Id = rr.Id,
                                                                     Type = rr.Type,
                                                                     ValueChange = rr.ValueChange
                                                                 }).ToList()
                                            }).ToList(),
                    LinkedValueDrivers = (from lvd in newPE.LinkedValueDrivers
                                          select new PricingEverydayLinkedValueDriver
                                          {
                                              Groups = (from g in lvd.Groups
                                                        select new PricingEverydayLinkedValueDriverGroup
                                                        {
                                                            PercentChange = g.PercentChange,
                                                            ValueDriverGroupId = g.ValueDriverGroupId
                                                        }).ToList()
                                          }).ToList(),
                    PriceListGroups = (from plg in newPE.PriceListGroups
                                       select new PricingEverydayPriceListGroup
                                       {
                                           Key = plg.Key,
                                           Name = plg.Name,
                                           PriceLists = (from pl in plg.PriceLists
                                                         select new PricingEverydayPriceList
                                                         {
                                                             Code = pl.Code,
                                                             Id = pl.Id,
                                                             IsKey = pl.IsKey,
                                                             IsSelected = pl.IsSelected,
                                                             Key = pl.Key,
                                                             Name = pl.Name,
                                                             Sort = pl.Sort,
                                                             Title = pl.Title
                                                         }).ToList(),
                                           Sort = plg.Sort,
                                           Title = plg.Title
                                       }).ToList(),
                    PricingModes = (from pm in newPE.PricingModes
                                    select new PricingMode
                                    {
                                        HasKeyPriceListRule = pm.HasKeyPriceListRule,
                                        HasLinkedPriceListRule = pm.HasLinkedPriceListRule,
                                        IsSelected = pm.IsSelected,
                                        Key = pm.Key,
                                        KeyPriceListGroupKey = pm.KeyPriceListGroupKey,
                                        LinkedPriceListGroupKey = pm.LinkedPriceListGroupKey,
                                        Name = pm.Name,
                                        Sort = pm.Sort,
                                        Title = pm.Title
                                    }).ToList(),
                    Results = (from r in newPE.Results
                               select new PricingEverydayResult
                               {
                                   Groups = (from g in r.Groups
                                             select new PricingResultDriverGroup
                                             {
                                                 Actual = g.Actual,
                                                 Id = g.Id,
                                                 MaxOutlier = g.MaxOutlier,
                                                 MinOutlier = g.MinOutlier,
                                                 Name = g.Name,
                                                 SalesValue = g.SalesValue,
                                                 SkuCount = g.SkuCount,
                                                 Sort = g.Sort,
                                                 Title = g.Title,
                                                 Value = g.Value
                                             }).ToList(),
                                   PriceLists = (from pl in r.PriceLists
                                                 select new PricingEverydayResultPriceList
                                                 {
                                                     Code = pl.Code,
                                                     CurrentMarkupPercent = pl.CurrentMarkupPercent,
                                                     CurrentPrice = pl.CurrentPrice,
                                                     Id = pl.Id,
                                                     IsKey = pl.IsKey,
                                                     IsSelected = pl.IsSelected,
                                                     InfluenceValueChange = pl.InfluenceValueChange,
                                                     Key = pl.Key,
                                                     KeyValueChange = pl.KeyValueChange,
                                                     Name = pl.Name,
                                                     NewMarkupPercent = pl.NewMarkupPercent,
                                                     NewPrice = pl.NewPrice,
                                                     PriceChange = pl.PriceChange,
                                                     //PriceEdit = pl.PriceEdit,
                                                     //PriceWarning = pl.PriceWarning,
                                                     ResultId = pl.ResultId,
                                                     Sort = pl.Sort,
                                                     Title = pl.Title

                                                 }).ToList(),
                                   SkuId = r.SkuId,
                                   SkuName = r.SkuName,
                                   SkuTitle = r.SkuTitle
                               }).ToList(),
                    SearchGroupKey = newPE.SearchGroupKey,
                    ValueDrivers = (from vd in newPE.ValueDrivers
                                    select new PricingEverydayValueDriver
                                    {
                                        Groups = (from g in vd.Groups
                                                  select new PricingValueDriverGroup
                                                  {
                                                      Id = g.Id,
                                                      MaxOutlier = g.MaxOutlier,
                                                      MinOutlier = g.MinOutlier,
                                                      SalesValue = g.SalesValue,
                                                      SkuCount = g.SkuCount,
                                                      Sort = g.Sort,
                                                      Value = g.Value
                                                  }).ToList(),

                                        Id = vd.Id,
                                        IsKey = vd.IsKey,
                                        IsSelected = vd.IsSelected,
                                        Key = vd.Key,
                                        Name = vd.Name,
                                        Sort = vd.Sort,
                                        Title = vd.Title
                                    }).ToList(),

                }

            };

        }

        public Session<List<PricingEveryday>> LoadList(Session<NullT> session)
        {
            var pricing = database.GetCollection<DTO.PricingEveryday>("PricingAll_NoFilters");
            var cursor = pricing.FindAll();
            cursor.SetFields(MongoDB.Driver.Builders.Fields.Include("Identity", "Id"));
            var dtos = cursor.ToList();


            //var dtos = PricingEveryday.AsQueryable().ToList();

            //foreach (var item in dtos)
            //{
            //    if (item.FilterGroups == null) { item.FilterGroups = new List<DTO.FilterGroup>(); }
            //    if (item.ValueDrivers == null) { item.ValueDrivers = new List<DTO.PricingEverydayValueDriver>(); }
            //    if (item.LinkedValueDrivers == null) { item.LinkedValueDrivers = new List<DTO.PricingEverydayLinkedValueDriver>(); }
            //    if (item.LinkedPriceListRules == null) { item.LinkedPriceListRules = new List<DTO.PricingLinkedPriceListRule>(); }

            //    if (item.Results == null) { item.Results = new List<DTO.PricingEverydayResult>(); }
            //}
            //foreach (var item in dtos.SelectMany( x => x.KeyValueDriver.Groups))
            //{
            //    if (item.MarkupRules == null) { item.MarkupRules = new List<DTO.PriceMarkupRule>(); }
            //    if (item.OptimizationRules == null) { item.OptimizationRules = new List<DTO.PriceOptimizationRule>(); }
            //}

            var list = 
                ( from dto in dtos
                             select new PricingEveryday
                                 {
                                   //FilterGroups = (from fg in dto.FilterGroups
                                   //                select new FilterGroup
                                   //                {
                                   //                    Name = fg.Name,
                                   //                    Sort = fg.Sort,
                                   //                    Filters = (from f in fg.Filters
                                   //                               select new Filter
                                   //                               {
                                   //                                   Code = f.Code,
                                   //                                   Id = f.Id,
                                   //                                   IsSelected = f.IsSelected,
                                   //                                   Key = f.Key,
                                   //                                   Name = f.Name,
                                   //                                   Sort = f.Sort
                                   //                               }).ToList()
                                   //                }).ToList(),
                                   Id = dto.Id,
                                   Identity = new PricingIdentity
                                   {
                                       Active = dto.Identity.Active,
                                       Author = dto.Identity.Author,
                                       Created = dto.Identity.Created,
                                       CreatedText = dto.Identity.CreatedText,
                                       Description = dto.Identity.Description,
                                       Edited = dto.Identity.Edited,
                                       EditedText = dto.Identity.EditedText,
                                       Editor = dto.Identity.Editor,
                                       Name = dto.Identity.Name,
                                       Notes = dto.Identity.Notes,
                                       Owner = dto.Identity.Owner,
                                       Refreshed = dto.Identity.Refreshed,
                                       RefreshedText = dto.Identity.RefreshedText,
                                       Shared = dto.Identity.Shared,
                                   },
                                   //KeyPriceListRule = new PricingKeyPriceListRule
                                   //{
                                   //    DollarRangeLower = dto.KeyPriceListRule.DollarRangeLower,
                                   //    DollarRangeUpper = dto.KeyPriceListRule.DollarRangeUpper,
                                   //    PriceListId = dto.KeyPriceListRule.PriceListId,
                                   //    RoundingRules = (from ru in dto.KeyPriceListRule.RoundingRules
                                   //                     select new PriceRoundingRule
                                   //                     {
                                   //                         DollarRangeLower = ru.DollarRangeLower,
                                   //                         DollarRangeUpper = ru.DollarRangeUpper,
                                   //                         Id = ru.Id,
                                   //                         Type = ru.Type,
                                   //                         ValueChange = ru.ValueChange
                                   //                     }).ToList(),
                                   //    RoundingTypes = (from rt in dto.KeyPriceListRule.RoundingTypes
                                   //                     select new SQLEnumeration
                                   //                     {
                                   //                         Description = rt.Description,
                                   //                         Name = rt.Name,
                                   //                         Sort = rt.Sort,
                                   //                         Value = rt.Value
                                   //                     }).ToList()

                                   //},
                                   //KeyValueDriver = new PricingEverydayKeyValueDriver
                                   //{
                                   //    Groups = (from g in dto.KeyValueDriver.Groups
                                   //              select new PricingEverydayKeyValueDriverGroup
                                   //              {
                                   //                  MarkupRules = (from r in g.MarkupRules
                                   //                                 select new PriceMarkupRule
                                   //                                 {
                                   //                                     DollarRangeLower = r.DollarRangeLower,
                                   //                                     DollarRangeUpper = r.DollarRangeUpper,
                                   //                                     Id = r.Id,
                                   //                                     PercentLimitLower = r.PercentLimitLower,
                                   //                                     PercentLimitUpper = r.PercentLimitUpper
                                   //                                 }).ToList(),
                                   //                  OptimizationRules = (from o in g.OptimizationRules
                                   //                                       select new PriceOptimizationRule
                                   //                                       {
                                   //                                           DollarRangeLower = o.DollarRangeLower,
                                   //                                           DollarRangeUpper = o.DollarRangeUpper,
                                   //                                           Id = o.Id,
                                   //                                           PercentChange = o.PercentChange
                                   //                                       }).ToList(),
                                   //                  ValueDriverGroupId = g.ValueDriverGroupId
                                   //              }).ToList(),
                                   //    ValueDriverId = dto.KeyValueDriver.ValueDriverId


                                   //},
                                   //LinkedPriceListRules = (from lpr in dto.LinkedPriceListRules
                                   //                        select new PricingLinkedPriceListRule
                                   //                        {
                                   //                            PercentChange = lpr.PercentChange,
                                   //                            PriceListId = lpr.PriceListId,
                                   //                            RoundingRules = (from rr in lpr.RoundingRules
                                   //                                             select new PriceRoundingRule
                                   //                                             {
                                   //                                                 DollarRangeLower = rr.DollarRangeLower,
                                   //                                                 DollarRangeUpper = rr.DollarRangeUpper,
                                   //                                                 Id = rr.Id,
                                   //                                                 Type = rr.Type,
                                   //                                                 ValueChange = rr.ValueChange
                                   //                                             }).ToList()
                                   //                        }).ToList(),
                                   //LinkedValueDrivers = (from lvd in dto.LinkedValueDrivers
                                   //                      select new PricingEverydayLinkedValueDriver
                                   //                      {
                                   //                          Groups = (from g in lvd.Groups
                                   //                                    select new PricingEverydayLinkedValueDriverGroup
                                   //                                    {
                                   //                                        PercentChange = g.PercentChange,
                                   //                                        ValueDriverGroupId = g.ValueDriverGroupId
                                   //                                    }).ToList()
                                   //                      }).ToList(),
                                   //PriceListGroups = (from plg in dto.PriceListGroups
                                   //                   select new PricingEverydayPriceListGroup
                                   //                   {
                                   //                       Key = plg.Key,
                                   //                       Name = plg.Name,
                                   //                       PriceLists = (from pl in plg.PriceLists
                                   //                                     select new PricingEverydayPriceList
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
                                   //PricingModes = (from pm in dto.PricingModes
                                   //                select new PricingMode
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
                                   //Results = (from r in dto.Results
                                   //           select new PricingEverydayResult
                                   //           {
                                   //               Groups = (from g in r.Groups
                                   //                         select new PricingResultDriverGroup
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
                                   //                             select new PricingEverydayResultPriceList
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
                                   SearchGroupKey = dto.SearchGroupKey
                                   ,
                                   //ValueDrivers = (from vd in dto.ValueDrivers
                                   //                select new PricingEverydayValueDriver
                                   //                {
                                   //                    Groups = (from g in vd.Groups
                                   //                              select new PricingValueDriverGroup
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


                                 }
                ).ToList();
            return new Session<List<PricingEveryday>> { Data = list };
        }


        public Session<PricingEveryday> LoadFilters(Session<PricingEveryday> session)
        {
            var p = session.Data as PricingEveryday;
            var filterGroups = PricingEveryday.AsQueryable()
                .Where(x => x.Id == p.Id).SingleOrDefault().FilterGroups;

            //a.FilterGroups = filterGroups;
            return new Session<PricingEveryday>
            {
                Data = new PricingEveryday
                {
                    Id = p.Id,
                    FilterGroups = (from fg in filterGroups
                                    select new FilterGroup
                                    {
                                        Name = fg.Name,
                                        Sort = fg.Sort,
                                        Filters = (from f in fg.Filters
                                                   select new Filter
                                                   {
                                                       Code = f.Code,
                                                       Id = f.Id,
                                                       IsSelected = f.IsSelected,
                                                       Key = f.Key,
                                                       Name = f.Name,
                                                       Sort = f.Sort
                                                   }).ToList()
                                    }).ToList()
                }
            };
        }


        public Session<PricingEveryday> LoadDrivers(Session<PricingEveryday> session)
        {
            var ent = session.Data as PricingEveryday;
            var p = PricingEveryday.AsQueryable().Where(x => x.Id == ent.Id).SingleOrDefault();
            if (p.ValueDrivers == null) { p.ValueDrivers = new List<DTO.PricingEverydayValueDriver>(); }
                
            if (p.LinkedValueDrivers == null) { p.LinkedValueDrivers = new List<DTO.PricingEverydayLinkedValueDriver>(); }
            var drivers = p.ValueDrivers.ToList();

            foreach (var item in p.KeyValueDriver.Groups)
            {
                if (item.MarkupRules == null) { item.MarkupRules = new List<DTO.PriceMarkupRule>(); }
                if ( item.OptimizationRules == null) { item.OptimizationRules = new List<DTO.PriceOptimizationRule>();}
            }

            return new Session<PricingEveryday>()
            {
                Data = new PricingEveryday
                {
                    Id = p.Id,
                    ValueDrivers =

                        (from vd in drivers
                         select new PricingEverydayValueDriver
                         {
                             Groups = (from g in vd.Groups
                                       select new PricingValueDriverGroup
                                       {
                                           Id = g.Id,
                                           MaxOutlier = g.MaxOutlier,
                                           MinOutlier = g.MinOutlier,
                                           SalesValue = g.SalesValue,
                                           SkuCount = g.SkuCount,
                                           Sort = g.Sort,
                                           Value = g.Value
                                       }).ToList(),

                             Id = vd.Id,
                             IsKey = vd.IsKey,
                             IsSelected = vd.IsSelected,
                             Key = vd.Key,
                             Name = vd.Name,
                             Sort = vd.Sort,
                             Title = vd.Title
                         }).ToList(),
                    LinkedValueDrivers = (from lvd in p.LinkedValueDrivers
                                          select new PricingEverydayLinkedValueDriver
                                          {
                                              Groups = (from g in lvd.Groups
                                                        select new PricingEverydayLinkedValueDriverGroup
                                                        {
                                                            PercentChange = g.PercentChange,
                                                            ValueDriverGroupId = g.ValueDriverGroupId
                                                        }).ToList()
                                          }).ToList(),
                    KeyValueDriver = new PricingEverydayKeyValueDriver 
                                    {
                                         Groups = (from g in p.KeyValueDriver.Groups
                                             select new PricingEverydayKeyValueDriverGroup
                                             {
                                                 MarkupRules = (from r in g.MarkupRules
                                                                select new PriceMarkupRule
                                                                {
                                                                    DollarRangeLower = r.DollarRangeLower,
                                                                    DollarRangeUpper = r.DollarRangeUpper,
                                                                    Id = r.Id,
                                                                    PercentLimitLower = r.PercentLimitLower,
                                                                    PercentLimitUpper = r.PercentLimitUpper
                                                                }).ToList(),
                                                 OptimizationRules = (from o in g.OptimizationRules
                                                                      select new PriceOptimizationRule
                                                                      {
                                                                          DollarRangeLower = o.DollarRangeLower,
                                                                          DollarRangeUpper = o.DollarRangeUpper,
                                                                          Id = o.Id,
                                                                          PercentChange = o.PercentChange
                                                                      }).ToList(),
                                                 ValueDriverGroupId = g.ValueDriverGroupId
                                             }).ToList(),
                                         ValueDriverId = p.KeyValueDriver.ValueDriverId
                                    }
                    
                }

            };
        }


        public Session<PricingEveryday> LoadPriceLists(Session<PricingEveryday> session)
        {
            var p = session.Data as PricingEveryday;
            var plists = PricingEveryday.AsQueryable().Where(x => x.Id == p.Id).SingleOrDefault().PriceListGroups.ToList();
            return new Session<PricingEveryday>()
            {
                Data = new PricingEveryday { Id = p.Id,
                                             PriceListGroups = (from plg in plists
                                                             select new PricingEverydayPriceListGroup
                                                             {
                                                                 Key = plg.Key,
                                                                 Name = plg.Name,
                                                                 PriceLists = (from pl in plg.PriceLists
                                                                               select new PricingEverydayPriceList
                                                                               {
                                                                                   Code = pl.Code,
                                                                                   Id = pl.Id,
                                                                                   IsKey = pl.IsKey,
                                                                                   IsSelected = pl.IsSelected,
                                                                                   Key = pl.Key,
                                                                                   Name = pl.Name,
                                                                                   Sort = pl.Sort,
                                                                                   Title = pl.Title
                                                                               }).ToList(),
                                                                 Sort = plg.Sort,
                                                                 Title = plg.Title
                                                             }).ToList()
                                            }
            };
        }


        public Session<PricingEveryday> LoadResults(Session<PricingEveryday> session)
        {
            var results = Results.AsQueryable()
                            .Where(x => x.PricingId == session.Data.Id).SingleOrDefault().Results;

            var payload = (from r in results
                           select new PricingEverydayResult
                           {
                               Groups = (from g in r.Groups
                                         select new PricingResultDriverGroup
                                         {
                                             Actual = g.Actual,
                                             Id = g.Id,
                                             MaxOutlier = g.MaxOutlier,
                                             MinOutlier = g.MinOutlier,
                                             Name = g.Name,
                                             SalesValue = g.SalesValue,
                                             SkuCount = g.SkuCount,
                                             Sort = g.Sort,
                                             Title = g.Title,
                                             Value = g.Value
                                         }).ToList(),
                               PriceLists = (from pl in r.PriceLists
                                             select new PricingEverydayResultPriceList
                                             {
                                                 Code = pl.Code,
                                                 CurrentMarkupPercent = pl.CurrentMarkupPercent,
                                                 CurrentPrice = pl.CurrentPrice,
                                                 Id = pl.Id,
                                                 IsKey = pl.IsKey,
                                                 IsSelected = pl.IsSelected,
                                                 InfluenceValueChange = pl.InfluenceValueChange,
                                                 Key = pl.Key,
                                                 KeyValueChange = pl.KeyValueChange,
                                                 Name = pl.Name,
                                                 NewMarkupPercent = pl.NewMarkupPercent,
                                                 NewPrice = pl.NewPrice,
                                                 PriceChange = pl.PriceChange,
                                                 //PriceEdit = pl.PriceEdit,
                                                 //PriceWarning = pl.PriceWarning,
                                                 ResultId = pl.ResultId,
                                                 Sort = pl.Sort,
                                                 Title = pl.Title
                                             }).ToList(),
                               SkuId = r.SkuId,
                               SkuName = r.SkuName,
                               SkuTitle = r.SkuTitle
                           }).ToList();

            return new Session<PricingEveryday>
            {
                Data = new PricingEveryday { Results = payload}
            };
        }


        public Session<PricingEveryday> SaveFilters(Session<PricingEveryday> session)
        {
            var a = PricingEveryday.AsQueryable().First(x => x.Id == session.Data.Id);
            var groups = (from g in session.Data.FilterGroups
                          select new DTO.FilterGroup
                          {
                              Name = g.Name,
                              Sort = g.Sort,
                              Filters = (from f in g.Filters
                                         select new DTO.Filter
                                         {
                                             Code = f.Code,
                                             Id = f.Id,
                                             IsSelected = f.IsSelected,
                                             Key = f.Key,
                                             Name = f.Name,
                                             Sort = f.Sort
                                         }).ToList()
                          }).ToList();
            a.FilterGroups.AddRange(groups);
            PricingEveryday.Save(a);
            //session.Data = newA;
            return session;
        }
        public Session<PricingEveryday> SaveIdentity(Session<PricingEveryday> session)
        {
            var param = session.Data;
            var p = PricingEveryday.AsQueryable().First(x => x.Id == session.Data.Id);


            p.Identity = new DTO.PricingIdentity
            {
                Active = param.Identity.Active,
                Author = param.Identity.Author,
                Created = param.Identity.Created,
                CreatedText = param.Identity.CreatedText,
                Description = param.Identity.Description,
                Edited = param.Identity.Edited,
                EditedText = param.Identity.EditedText,
                Editor = param.Identity.Editor,
                Name = param.Identity.Name,
                Notes = param.Identity.Notes,
                Owner = param.Identity.Owner,
                Refreshed = param.Identity.Refreshed,
                RefreshedText = param.Identity.RefreshedText,
                Shared = param.Identity.Shared
            };
            PricingEveryday.Save(session.Data);

            ////var newA  = new Analytic(a.Id, a.Identity);
            ////return new Session<Analytic>{Data = newA}; //TODO: return keys
            return new Session<PricingEveryday> { Data = null }; //TODO: return keys
        }
        public Session<PricingEveryday> SaveDrivers(Session<PricingEveryday> session)
        {
            throw new NotImplementedException();
        }
        public Session<PricingEveryday> SavePriceLists(Session<PricingEveryday> session)
        {
            throw new NotImplementedException();
        }





    }
}
