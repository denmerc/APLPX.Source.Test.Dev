using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Driver display and client entities.
    /// </summary>
    public static class DriverMapper
    {
        #region Analytic Driver

        public static Display.AnalyticDriver ToDisplayEntity(this DTO.AnalyticDriver dto)
        {
            var displayEntity = new Display.AnalyticDriver();

            displayEntity.Id = dto.Id;
            displayEntity.Key = dto.Key;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Sort = dto.Sort;
            displayEntity.IsSelected = dto.IsSelected;

            if (dto.Modes != null)
            {
                foreach (DTO.AnalyticDriverMode mode in dto.Modes)
                {
                    displayEntity.Modes.Add(mode.ToDisplayEntity()); 
                }                
            }

            if (dto.Results != null)
            {
                foreach (DTO.AnalyticResult result in dto.Results)
                {
                    displayEntity.Results.Add(result.ToDisplayEntity());
                }
            }

            return displayEntity;
        }

        public static DTO.AnalyticDriver ToDto(this Display.AnalyticDriver displayEntity)
        {
            var modes = new List<DTO.AnalyticDriverMode>();
            displayEntity.Modes.ForEach(mode => modes.Add(mode.ToDto()));

            var results = new List<DTO.AnalyticResult>();
            foreach (Display.AnalyticResult result in displayEntity.Results)
            {
                results.Add(result.ToDto());
            }

            var dto = new DTO.AnalyticDriver(
                                        displayEntity.Id,
                                        displayEntity.Key,
                                        displayEntity.Name,
                                        displayEntity.Title,
                                        displayEntity.Sort,
                                        displayEntity.IsSelected,
                                        results,
                                        modes);

            return dto;
        }

        #endregion

        #region Pricing Driver

        public static Display.PricingDriver ToDisplayEntity(this DTO.PricingDriver dto)
        {
            var displayEntity = new Display.PricingDriver();

            //TODO: map when client class properties are added.
            //displayEntity.Id = dto.Id;
            //displayEntity.Key = dto.Key;
            //displayEntity.Name = dto.Name;
            //displayEntity.Tooltip = dto.Tooltip;
            //displayEntity.IsKeyDriver = dto.IsKeyDriver;
            //displayEntity.SkuCount = dto.SkuCount;

            return displayEntity;
        }

        public static DTO.PricingDriver ToDto(this Display.PricingDriver displayEntity)
        {
            //TODO: map when client class properties are added.
            var dto = new DTO.PricingDriver();

            return dto;
        }

        #endregion
    }
}
