using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping AnalyticResult display and client entities.
    /// </summary>
    public static class ResultMapper
    {
        #region Analytic Result

        public static Display.AnalyticResult ToDisplayEntity(this DTO.AnalyticResult dto)
        {
            var displayEntity = new Display.AnalyticResult();

            displayEntity.Group = dto.Group;
            displayEntity.MinValue = dto.MinValue;
            displayEntity.MaxValue = dto.MaxValue;
            displayEntity.SalesValue = dto.SalesValue;
            displayEntity.Sort = dto.Sort;

            return displayEntity;
        }

        public static DTO.AnalyticResult ToDto(this Display.AnalyticResult displayEntity)
        {
            var dto = new DTO.AnalyticResult(
                                        displayEntity.Group,
                                        displayEntity.MinValue,
                                        displayEntity.MaxValue,
                                        displayEntity.SalesValue,
                                        displayEntity.Sort);

            return dto;
        }

        #endregion

        #region Pricing Result

        public static Display.PricingResult ToDisplayEntity(this DTO.PricingResult dto)
        {
            var displayEntity = new Display.PricingResult();

            //TODO: map when client class properties are added.
            //displayEntity.Group = dto.Group;
            //displayEntity.MinValue = dto.MinValue;
            //displayEntity.MaxValue = dto.MaxValue;
            //displayEntity.SalesValue = dto.SalesValue;
            //displayEntity.Sort = dto.Sort;

            return displayEntity;
        }

        public static DTO.PricingResult ToDto(this Display.PricingResult displayEntity)
        {
            var dto = new DTO.PricingResult();

            //TODO: map when client class properties are added.
            return dto;
        }

        #endregion

    }
}
