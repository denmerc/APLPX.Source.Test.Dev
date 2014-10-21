using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Error display and client entities.
    /// </summary>
    public static class ErrorMapper
    {
        public static Display.Error ToDisplayEntity(this DTO.ModuleFeatureStepError dto)
        {
            var displayEntity = new Display.Error { Message = dto.Message };            

            return displayEntity;
        }

        public static DTO.ModuleFeatureStepError ToDto(this Display.Error displayEntity)
        {
            var dto = new DTO.ModuleFeatureStepError(displayEntity.Message);

            return dto;
        }
    }
}
