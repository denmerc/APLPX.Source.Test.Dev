using System;
using System.Collections.Generic;

using Display = APLPX.UI.WPF.DisplayEntities;
using DTO = APLPX.Client.Entity;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Group and client entities.
    /// </summary>
    public static class DriverGroupMapper
    {
        #region Base ValueDriverGroup

        public static Display.ValueDriverGroup ToDisplayEntity(this DTO.ValueDriverGroup dto)
        {
            var displayEntity = new Display.ValueDriverGroup();

            displayEntity.Id = dto.Id;
            displayEntity.Value = dto.Value;
            displayEntity.MinOutlier = dto.MinOutlier;
            displayEntity.MaxOutlier = dto.MaxOutlier;
            displayEntity.Sort = dto.Sort;

            return displayEntity;
        }

        public static Client.Entity.ValueDriverGroup ToDto(this Display.ValueDriverGroup displayEntity)
        {
            var dto = new Client.Entity.ValueDriverGroup(
                                            displayEntity.Id,
                                            displayEntity.Value,
                                            displayEntity.MinOutlier,
                                            displayEntity.MaxOutlier,
                                            displayEntity.Sort);
            return dto;
        }

        #endregion

        #region PricingValueDriverGroup subclass

        public static Display.PricingValueDriverGroup ToDisplayEntity(this DTO.PricingValueDriverGroup dto)
        {
            var displayEntity = new Display.PricingValueDriverGroup();
            displayEntity.Id = dto.Id;
            displayEntity.Value = dto.Value;
            displayEntity.MinOutlier = dto.MinOutlier;
            displayEntity.MaxOutlier = dto.MaxOutlier;
            displayEntity.Sort = dto.Sort;

            displayEntity.SkuCount = dto.SkuCount;
            displayEntity.SalesValue = dto.SalesValue;

            return displayEntity;
        }

        public static Client.Entity.PricingValueDriverGroup ToDto(this Display.PricingValueDriverGroup displayEntity)
        {
            var dto = new Client.Entity.PricingValueDriverGroup(
                                            displayEntity.Id,
                                            displayEntity.Value,
                                            displayEntity.MinOutlier,
                                            displayEntity.MaxOutlier,
                                            displayEntity.Sort,
                                            displayEntity.SkuCount,
                                            displayEntity.SalesValue);
            return dto;
        }

        #endregion

        #region PricingResultDriverGroup

        public static Display.PricingResultDriverGroup ToDisplayEntity(this DTO.PricingResultDriverGroup dto)
        {
            var displayEntity = new Display.PricingResultDriverGroup();
            displayEntity.Id = dto.Id;
            displayEntity.Value = dto.Value;
            displayEntity.MinOutlier = dto.MinOutlier;
            displayEntity.MaxOutlier = dto.MaxOutlier;
            displayEntity.Sort = dto.Sort;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Actual = dto.Actual;

            displayEntity.SkuCount = dto.SkuCount;
            displayEntity.SalesValue = dto.SalesValue;

            return displayEntity;
        }


        public static DTO.PricingResultDriverGroup ToDto(this Display.PricingResultDriverGroup displayEntity)
        {
            var dto = new DTO.PricingResultDriverGroup(
                                            displayEntity.Id,
                                            displayEntity.Value,
                                            displayEntity.MinOutlier,
                                            displayEntity.MaxOutlier,
                                            displayEntity.Sort,
                                            displayEntity.Name,
                                            displayEntity.Title,
                                            displayEntity.Actual,
                                            displayEntity.SkuCount,
                                            displayEntity.SalesValue);
            return dto;
        }

        #endregion

        #region PricingEverydayKeyValueDriverGroup

        public static Display.PricingEverydayKeyValueDriverGroup ToDisplayEntity(this DTO.PricingEverydayKeyValueDriverGroup dto)
        {
            var displayEntity = new Display.PricingEverydayKeyValueDriverGroup();
            displayEntity.ValueDriverGroupId = dto.ValueDriverGroupId;

            if (dto.MarkupRules != null)
            {
                foreach (var rule in dto.MarkupRules)
                {
                    displayEntity.MarkupRules.Add(rule.ToDisplayEntity());
                }
            }
            if (dto.OptimizationRules != null)
            {
                foreach (var rule in dto.OptimizationRules)
                {
                    displayEntity.OptimizationRules.Add(rule.ToDisplayEntity());
                }
            }

            return displayEntity;
        }

        public static DTO.PricingEverydayKeyValueDriverGroup ToDto(this Display.PricingEverydayKeyValueDriverGroup displayEntity)
        {
            var markupRules = new List<DTO.PriceMarkupRule>();
            foreach (Display.PriceMarkupRule rule in displayEntity.MarkupRules)
            {
                markupRules.Add(rule.ToDto());
            }

            var optimizationRules = new List<DTO.PriceOptimizationRule>();
            foreach (Display.PriceOptimizationRule rule in displayEntity.OptimizationRules)
            {
                optimizationRules.Add(rule.ToDto());
            }

            var dto = new DTO.PricingEverydayKeyValueDriverGroup(
                                            displayEntity.ValueDriverGroupId,
                                            markupRules,
                                            optimizationRules);
            return dto;
        }

        #endregion

        #region PricingEverydayLinkedValueDriverGroup

        public static Display.PricingEverydayLinkedValueDriverGroup ToDisplayEntity(this DTO.PricingEverydayLinkedValueDriverGroup dto)
        {
            var displayEntity = new Display.PricingEverydayLinkedValueDriverGroup();
            displayEntity.ValueDriverGroupId = dto.ValueDriverGroupId;
            displayEntity.PercentChange = dto.PercentChange;

            return displayEntity;
        }

        public static DTO.PricingEverydayLinkedValueDriverGroup ToDto(this Display.PricingEverydayLinkedValueDriverGroup displayEntity)
        {
            var dto = new DTO.PricingEverydayLinkedValueDriverGroup(
                                            displayEntity.ValueDriverGroupId,
                                            displayEntity.PercentChange);
            return dto;
        }

        #endregion
    }
}
