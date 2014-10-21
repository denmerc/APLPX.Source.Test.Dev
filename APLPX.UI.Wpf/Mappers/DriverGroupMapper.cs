using System;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Group and client entities.
    /// </summary>
    public static class DriverGroupMapper
    {
        public static Display.DriverGroup ToDisplayEntity(this DTO.AnalyticDriverGroup dto)
        {
            var displayEntity = new Display.DriverGroup();

            displayEntity.Id = dto.Id;
            displayEntity.Value = dto.Value;
            displayEntity.MinOutlier = dto.MinOutlier;
            displayEntity.MaxOutlier = dto.MaxOutlier;
            displayEntity.SortOrder = dto.SortOrder;          

            return displayEntity;
        }

        public static Client.Entity.AnalyticDriverGroup ToDto(this Display.DriverGroup displayEntity)
        {
            var dto = new Client.Entity.AnalyticDriverGroup(
                                            displayEntity.Id, 
                                            displayEntity.Value, 
                                            displayEntity.MinOutlier, 
                                            displayEntity.MaxOutlier, 
                                            displayEntity.SortOrder);
            return dto;
        }
    }
}
