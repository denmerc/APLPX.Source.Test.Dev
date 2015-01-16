using System;
using System.Collections.Generic;

using DTO = APLPX.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Advisor display and client entities.
    /// </summary>
    public static class AdvisorMapper
    {
        public static Display.Advisor ToDisplayEntity(this DTO.ModuleFeatureStepAdvisor dto)
        {
            var displayEntity = new Display.Advisor();

            displayEntity.Sort = dto.Sort;
            displayEntity.Message = dto.Message;

            return displayEntity;
        }

        public static DTO.ModuleFeatureStepAdvisor ToDto(this Display.Advisor displayEntity)
        {
            var dto = new DTO.ModuleFeatureStepAdvisor(
                                            displayEntity.Sort,
                                            displayEntity.Message);

            return dto;
        }
    }
}
