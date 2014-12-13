using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Mode display and client entities.
    /// </summary>
    public static class ModeMapper
    {
        #region Analytic Mode

        public static Display.AnalyticValueDriverMode ToDisplayEntity(this DTO.AnalyticValueDriverMode dto)
        {
            var displayEntity = new Display.AnalyticValueDriverMode();

            displayEntity.Key = dto.Key;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Sort = dto.Sort;
            displayEntity.IsSelected = dto.IsSelected;

            if (dto.Groups != null)
            {
                foreach (DTO.ValueDriverGroup group in dto.Groups)
                {
                    displayEntity.Groups.Add(group.ToDisplayEntity());
                }
            }

            return displayEntity;
        }

        public static DTO.AnalyticValueDriverMode ToDto(this Display.AnalyticValueDriverMode displayEntity)
        {
            var groups = new List<DTO.ValueDriverGroup>();

            foreach (Display.ValueDriverGroup group in displayEntity.Groups)
            {
                groups.Add(group.ToDto());
            }

            var dto = new DTO.AnalyticValueDriverMode(
                                            displayEntity.Key,
                                            displayEntity.IsSelected,
                                            displayEntity.Name,
                                            displayEntity.Title,
                                            displayEntity.Sort,
                                            groups);
            return dto;
        }

        #endregion

        #region Pricing Mode

        public static Display.PricingMode ToDisplayEntity(this DTO.PricingMode dto)
        {
            var displayEntity = new Display.PricingMode();

            displayEntity.Key = dto.Key;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.IsSelected = dto.IsSelected;
            displayEntity.HasKeyPriceListRule = dto.HasKeyPriceListRule;
            displayEntity.HasLinkedPriceListRule = dto.HasLinkedPriceListRule;
            displayEntity.KeyPriceListGroupKey = dto.KeyPriceListGroupKey;
            displayEntity.LinkedPriceListGroupKey = dto.LinkedPriceListGroupKey;
            displayEntity.Sort = dto.Sort;

            return displayEntity;
        }

        public static DTO.PricingMode ToDto(this Display.PricingMode displayEntity)
        {
            var dto = new DTO.PricingMode(
                                    displayEntity.Key,
                                    displayEntity.Name,
                                    displayEntity.Title,
                                    displayEntity.IsSelected,
                                    displayEntity.HasKeyPriceListRule,
                                    displayEntity.HasLinkedPriceListRule,
                                    displayEntity.KeyPriceListGroupKey,
                                    displayEntity.LinkedPriceListGroupKey,
                                    displayEntity.Sort);
            return dto;
        }

        #endregion
    }
}
