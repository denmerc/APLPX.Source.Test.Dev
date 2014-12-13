using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping price rule display and client entities.
    /// </summary>
    public static class PriceRulesMapper
    {
        #region PriceRoundingRule

        public static Display.PriceRoundingRule ToDisplayEntity(this DTO.PriceRoundingRule dto)
        {
            var displayEntity = new Display.PriceRoundingRule();

            displayEntity.Id = dto.Id;
            displayEntity.Type = dto.Type;
            displayEntity.DollarRangeLower = dto.DollarRangeLower;
            displayEntity.DollarRangeUpper = dto.DollarRangeUpper;
            displayEntity.ValueChange = dto.ValueChange;
            displayEntity.Sort = dto.Sort;

            if (dto.RoundingTypes != null)
            {
                displayEntity.RoundingTypes = dto.RoundingTypes.ToDisplayEntities();
            }

            return displayEntity;
        }

        public static DTO.PriceRoundingRule ToDto(this Display.PriceRoundingRule displayEntity)
        {
            var dto = new DTO.PriceRoundingRule(
                                    displayEntity.Id,
                                    displayEntity.Type,
                                    displayEntity.DollarRangeLower,
                                    displayEntity.DollarRangeUpper,
                                    displayEntity.ValueChange);
            return dto;
        }

        #region Collection Methods for convenience

        public static List<Display.PriceRoundingRule> ToDisplayList(this IEnumerable<DTO.PriceRoundingRule> dtoList)
        {
            var displayList = new List<Display.PriceRoundingRule>();

            foreach (var rule in dtoList)
            {
                displayList.Add(rule.ToDisplayEntity());
            }

            return displayList;
        }

        public static List<DTO.PriceRoundingRule> ToDtoList(this IEnumerable<Display.PriceRoundingRule> displayList)
        {
            var dtoList = new List<DTO.PriceRoundingRule>();

            foreach (var rule in displayList)
            {
                dtoList.Add(rule.ToDto());
            }

            return dtoList;
        }

        public static List<DTO.SQLEnumeration> ToDtoList(this IEnumerable<Display.SQLEnumeration> displayList)
        {
            var dtoList = new List<DTO.SQLEnumeration>();

            foreach (var rule in displayList)
            {
                dtoList.Add(rule.ToDto());
            }

            return dtoList;
        }

        #endregion

        #endregion

        #region PricingKeyPriceListRule

        public static Display.PricingKeyPriceListRule ToDisplayEntity(this DTO.PricingKeyPriceListRule dto)
        {
            var displayEntity = new Display.PricingKeyPriceListRule();
            displayEntity.PriceListId = dto.PriceListId;
            displayEntity.DollarRangeLower = dto.DollarRangeLower;
            displayEntity.DollarRangeUpper = dto.DollarRangeUpper;

            if (dto.RoundingRules != null)
            {
                displayEntity.RoundingRules = dto.RoundingRules.ToDisplayList();
            }

            return displayEntity;
        }

        public static DTO.PricingKeyPriceListRule ToDto(this Display.PricingKeyPriceListRule displayEntity)
        {
            List<DTO.PriceRoundingRule> roundingRules = displayEntity.RoundingRules.ToDtoList();
            List<DTO.SQLEnumeration> roundingTypes = displayEntity.RoundingTypes.ToDtoList();

            var dto = new DTO.PricingKeyPriceListRule(
                                        displayEntity.PriceListId,
                                        displayEntity.DollarRangeLower,
                                        displayEntity.DollarRangeUpper,
                                        roundingRules,
                                        roundingTypes);
            return dto;
        }

        #endregion

        #region PricingLinkedPriceListRule

        public static Display.PricingLinkedPriceListRule ToDisplayEntity(this DTO.PricingLinkedPriceListRule dto)
        {
            var displayEntity = new Display.PricingLinkedPriceListRule();
            displayEntity.PriceListId = dto.PriceListId;
            displayEntity.PercentChange = dto.PercentChange;

            if (dto.RoundingRules != null)
            {
                displayEntity.RoundingRules = dto.RoundingRules.ToDisplayList();
            }

            return displayEntity;
        }

        public static DTO.PricingLinkedPriceListRule ToDto(this Display.PricingLinkedPriceListRule displayEntity)
        {
            List<DTO.PriceRoundingRule> roundingRules = displayEntity.RoundingRules.ToDtoList();
            List<DTO.SQLEnumeration> roundingTypes = displayEntity.RoundingTypes.ToDtoList();

            var dto = new DTO.PricingLinkedPriceListRule(
                                        displayEntity.PriceListId,
                                        displayEntity.PercentChange,
                                        roundingRules,
                                        roundingTypes
                                        );

            return dto;
        }

        #endregion

        #region PriceMarkupRule

        public static Display.PriceMarkupRule ToDisplayEntity(this DTO.PriceMarkupRule dto)
        {
            var displayEntity = new Display.PriceMarkupRule();
            displayEntity.Id = dto.Id;
            displayEntity.DollarRangeLower = dto.DollarRangeLower;
            displayEntity.DollarRangeUpper = dto.DollarRangeUpper;
            displayEntity.PercentLimitLower = dto.PercentLimitLower;
            displayEntity.PercentLimitUpper = dto.PercentLimitUpper;
            displayEntity.Sort = dto.Sort;

            return displayEntity;
        }

        public static DTO.PriceMarkupRule ToDto(this Display.PriceMarkupRule displayEntity)
        {
            var dto = new DTO.PriceMarkupRule(
                                    displayEntity.Id,
                                    displayEntity.DollarRangeLower,
                                    displayEntity.DollarRangeUpper,
                                    displayEntity.PercentLimitLower,
                                    displayEntity.PercentLimitUpper,
                                    displayEntity.Sort);

            return dto;
        }

        #endregion

        #region PriceOptimizationRule

        public static Display.PriceOptimizationRule ToDisplayEntity(this DTO.PriceOptimizationRule dto)
        {
            var displayEntity = new Display.PriceOptimizationRule();
            displayEntity.Id = dto.Id;
            displayEntity.DollarRangeLower = dto.DollarRangeLower;
            displayEntity.DollarRangeUpper = dto.DollarRangeUpper;
            displayEntity.PercentChange = dto.PercentChange;
            displayEntity.Sort = dto.Sort;

            return displayEntity;
        }

        public static DTO.PriceOptimizationRule ToDto(this Display.PriceOptimizationRule displayEntity)
        {
            var dto = new DTO.PriceOptimizationRule(
                                        displayEntity.Id, 
                                        displayEntity.DollarRangeLower, 
                                        displayEntity.DollarRangeUpper, 
                                        displayEntity.PercentChange);

            return dto;
        }

        #endregion

    }
}
