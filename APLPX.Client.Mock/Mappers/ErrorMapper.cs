using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
using Display = APLPX.Common.Mock.Entity;


namespace APLPX.Client.Mock.Mappers
{
    /// <summary>
    /// Extension methods for mapping Error display and client entities.
    /// </summary>
    public static class ErrorMapper
    {
        public static Display.Error ToDisplayEntity(this DTO.ModuleFeatureStepError dto)
        {
            var displayEntity = new Display.Error { Message = dto.Message, Sort = dto.Sort };

            return displayEntity;
        }

        public static DTO.ModuleFeatureStepError ToDto(this Display.Error displayEntity)
        {
            var dto = new DTO.ModuleFeatureStepError(displayEntity.Sort, displayEntity.Message);

            return dto;
        }
    }
}
