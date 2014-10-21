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
        public static Display.DriverMode ToDisplayEntity(this DTO.AnalyticDriverMode dto)
        {
            var displayEntity = new Display.DriverMode();

            displayEntity.Key = dto.Key;
            displayEntity.Name = dto.Name;
            displayEntity.SortOrder = dto.SortOrder;
            displayEntity.IsSelected = dto.IsSelected;

            foreach (var group in dto.Groups)
            {
                displayEntity.Groups.Add(group.ToDisplayEntity());
            }            

            return displayEntity;
        }

        public static DTO.AnalyticDriverMode ToDto(this Display.DriverMode displayEntity)
        {
            var groups = new List<DTO.AnalyticDriverGroup>();
            foreach (var group in displayEntity.Groups)
            {
                groups.Add(group.ToDto());
            }

            var dto = new DTO.AnalyticDriverMode(
                                            displayEntity.Key, 
                                            displayEntity.Name,
                                            displayEntity.Title, 
                                            displayEntity.SortOrder, 
                                            displayEntity.IsSelected, 
                                            groups);
            
            return dto;
        }
    }
}
