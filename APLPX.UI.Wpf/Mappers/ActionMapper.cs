using System;
using System.Collections.Generic;

using DTO = APLPX.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping ModuleFeatureStepAction display and client entities.
    /// </summary>
    public static class ActionMapper
    {
        public static Display.Action ToDisplayEntity(this DTO.ModuleFeatureStepAction dto)
        {
            var displayEntity = new Display.Action();

            displayEntity.Name = dto.Name;
            displayEntity.ParentName = dto.ParentName;
            displayEntity.Sort = dto.Sort;
            displayEntity.Title = dto.Title;
            displayEntity.TypeId = dto.Type;        

            return displayEntity;
        }

        public static DTO.ModuleFeatureStepAction ToDto(this Display.Action displayEntity)
        {
            var dto = new DTO.ModuleFeatureStepAction(
                                    displayEntity.Name, 
                                    displayEntity.ParentName, 
                                    displayEntity.Title, 
                                    displayEntity.Sort, 
                                    displayEntity.TypeId);

            return dto;
        }

    }
}
