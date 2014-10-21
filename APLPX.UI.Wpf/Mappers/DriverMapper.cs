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
            displayEntity.SortOrder = dto.SortOrder;            
            displayEntity.IsSelected = dto.IsSelected;

            dto.Modes.ForEach(mode => displayEntity.Modes.Add(mode.ToDisplayEntity()));

            return displayEntity;
        }

        public static DTO.AnalyticDriver ToDto(this Display.AnalyticDriver displayEntity)
        {
            var modes = new List<DTO.AnalyticDriverMode>();
            displayEntity.Modes.ForEach(mode => modes.Add(mode.ToDto()));
            
            var result = new DTO.AnalyticDriver(displayEntity.Id, 
                                                   displayEntity.Key, 
                                                   displayEntity.Name, 
                                                   displayEntity.Title, //TODO: verify
                                                   displayEntity.SortOrder, 
                                                   displayEntity.IsSelected, 
                                                   modes);

            return result;
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
