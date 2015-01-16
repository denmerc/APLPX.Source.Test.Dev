using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT = APLPX.Entity;
using DTO = APLPX.Common.Mock.Entity;


namespace APLPX.Client.Mock.MockEntities
{
    public static class Mapper
    {
        public static ENT.Analytic AnalyticToClientEntity(this DTO.Analytic dto)
        {
            return new ENT.Analytic
            {
                Id = dto.Id,
                SearchGroup = dto.SearchGroup,
                Identity = new ENT.AnalyticIdentity
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
                    Shared = dto.Identity.Shared

                },
                FilterGroups = (from fg in dto.FilterGroups
                                select new ENT.FilterGroup
                                {
                                    Name = fg.Name,
                                    Sort = fg.Sort,
                                    Filters = (from f in fg.Filters
                                               select new ENT.Filter
                                               {
                                                   Code = f.Code,
                                                   Id = f.Id,
                                                   IsSelected = f.IsSelected,
                                                   Key = f.Key,
                                                   Name = f.Name,
                                                   Sort = f.Sort
                                               }).ToList()
                                }).ToList(),
                PriceListGroups = (from pg in dto.PriceListGroups
                                   select new ENT.AnalyticPriceListGroup
                                   {
                                       Key = pg.Key,
                                       Name = pg.Name,
                                       Sort = pg.Sort,
                                       Title = pg.Title
                                   }).ToList(),
                ValueDrivers = (from vd in dto.ValueDrivers
                                select new ENT.AnalyticValueDriver
                                {
                                    Id = vd.Id,
                                    IsSelected = vd.IsSelected,
                                    Key = vd.Key,
                                    Name = vd.Name,
                                    Sort = vd.Sort,
                                    Title = vd.Title,
                                    Modes = (from d in vd.Modes
                                             select new ENT.AnalyticValueDriverMode
                                             {
                                                 Name = d.Name,
                                                 Title = d.Title,
                                                 Sort = d.Sort,
                                                 Key = d.Key,
                                                 IsSelected = d.IsSelected,
                                                 Groups = (from g in d.Groups
                                                           select new ENT.ValueDriverGroup
                                                           {
                                                               Id = g.Id,
                                                               MaxOutlier = g.MaxOutlier,
                                                               MinOutlier = g.MinOutlier,
                                                               Sort = g.Sort,
                                                               Value = g.Value
                                                           }).ToList()
                                             }).ToList()

                                }).ToList()

            };
        }

        public static ENT.PricingEveryday PricingEverydayToClientEntity(this DTO.PricingEveryday dto)
        {
            return new ENT.PricingEveryday
                           {
                               FilterGroups = (from fg in dto.FilterGroups
                                               select new ENT.FilterGroup
                                               {
                                                   Name = fg.Name,
                                                   Sort = fg.Sort,
                                                   Filters = (from f in fg.Filters
                                                              select new ENT.Filter
                                                              {
                                                                  Code = f.Code,
                                                                  Id = f.Id,
                                                                  IsSelected = f.IsSelected,
                                                                  Key = f.Key,
                                                                  Name = f.Name,
                                                                  Sort = f.Sort
                                                              }).ToList()
                                               }).ToList(),
                               Id = dto.Id,
                               Identity = new ENT.PricingIdentity
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
                               KeyPriceListRule = new ENT.PricingKeyPriceListRule
                               {
                                   DollarRangeLower = dto.KeyPriceListRule.DollarRangeLower,
                                   DollarRangeUpper = dto.KeyPriceListRule.DollarRangeUpper,
                                   PriceListId = dto.KeyPriceListRule.PriceListId,
                                   RoundingRules = (from ru in dto.KeyPriceListRule.RoundingRules
                                                    select new ENT.PriceRoundingRule
                                                    {
                                                        DollarRangeLower = ru.DollarRangeLower,
                                                        DollarRangeUpper = ru.DollarRangeUpper,
                                                        Id = ru.Id,
                                                        Type = ru.Type,
                                                        ValueChange = ru.ValueChange
                                                    }).ToList(),
                                   RoundingTypes = (from rt in dto.KeyPriceListRule.RoundingTypes
                                                    select new ENT.SQLEnumeration
                                                    {
                                                        Description = rt.Description,
                                                        Name = rt.Name,
                                                        Sort = rt.Sort,
                                                        Value = rt.Value
                                                    }).ToList()

                               },
                               KeyValueDriver = new ENT.PricingEverydayKeyValueDriver
                               {
                                   Groups = (from g in dto.KeyValueDriver.Groups
                                             select new ENT.PricingEverydayKeyValueDriverGroup
                                             {
                                                 MarkupRules = (from r in g.MarkupRules
                                                                select new ENT.PriceMarkupRule
                                                                {
                                                                    DollarRangeLower = r.DollarRangeLower,
                                                                    DollarRangeUpper = r.DollarRangeUpper,
                                                                    Id = r.Id,
                                                                    PercentLimitLower = r.PercentLimitLower,
                                                                    PercentLimitUpper = r.PercentLimitUpper
                                                                }).ToList(),
                                                 OptimizationRules = (from o in g.OptimizationRules
                                                                      select new ENT.PriceOptimizationRule
                                                                      {
                                                                          DollarRangeLower = o.DollarRangeLower,
                                                                          DollarRangeUpper = o.DollarRangeUpper,
                                                                          Id = o.Id,
                                                                          PercentChange = o.PercentChange
                                                                      }).ToList(),
                                                 ValueDriverGroupId = g.ValueDriverGroupId
                                             }).ToList(),
                                   ValueDriverId = dto.KeyValueDriver.ValueDriverId


                               },
                               LinkedPriceListRules = (from lpr in dto.LinkedPriceListRules
                                                       select new ENT.PricingLinkedPriceListRule
                                                       {
                                                           PercentChange = lpr.PercentChange,
                                                           PriceListId = lpr.PriceListId,
                                                           RoundingRules = (from rr in lpr.RoundingRules
                                                                            select new ENT.PriceRoundingRule
                                                                            {
                                                                                DollarRangeLower = rr.DollarRangeLower,
                                                                                DollarRangeUpper = rr.DollarRangeUpper,
                                                                                Id = rr.Id,
                                                                                Type = rr.Type,
                                                                                ValueChange = rr.ValueChange
                                                                            }).ToList()
                                                       }).ToList(),
                               LinkedValueDrivers = (from lvd in dto.LinkedValueDrivers
                                                     select new ENT.PricingEverydayLinkedValueDriver
                                                     {
                                                         Groups = (from g in lvd.Groups
                                                                   select new ENT.PricingEverydayLinkedValueDriverGroup
                                                                   {
                                                                       PercentChange = g.PercentChange,
                                                                       ValueDriverGroupId = g.ValueDriverGroupId
                                                                   }).ToList()
                                                     }).ToList(),
                               PriceListGroups = (from plg in dto.PriceListGroups
                                                  select new ENT.PricingEverydayPriceListGroup
                                                  {
                                                      Key = plg.Key,
                                                      Name = plg.Name,
                                                      PriceLists = (from pl in plg.PriceLists
                                                                    select new ENT.PricingEverydayPriceList
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
                               PricingModes = (from pm in dto.PricingModes
                                               select new ENT.PricingMode
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
                               Results = (from r in dto.Results
                                          select new ENT.PricingEverydayResult
                                          {
                                              Groups = (from g in r.Groups
                                                        select new ENT.PricingResultDriverGroup
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
                                                            select new ENT.PricingEverydayResultPriceList
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
                               SearchGroupKey = dto.SearchGroupKey,
                               ValueDrivers = (from vd in dto.ValueDrivers
                                               select new ENT.PricingEverydayValueDriver
                                               {
                                                   Groups = (from g in vd.Groups
                                                             select new ENT.PricingValueDriverGroup
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


                           };
        }
    }
}
