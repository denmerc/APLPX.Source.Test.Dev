using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Pricing display and client entities.
    /// </summary>
    public static class PricingMapper
    {
        public static Display.Pricing ToDisplayEntity(this DTO.Pricing dto)
        {
            var displayEntity = new Display.Pricing();

            displayEntity.Identity = dto.Identity.ToDisplayEntity();

            foreach (var group in dto.FilterGroups)
            {
                displayEntity.FilterGroups.Add(group.ToDisplayEntity());
            }

            foreach (var driver in dto.Drivers)
            {
                displayEntity.Drivers.Add(driver.ToDisplayEntity());
            }

            foreach (var group in dto.PriceListGroups)
            {
                displayEntity.PriceListGroups.Add(group.ToDisplayEntity());
            }

            foreach (var feature in dto.Features)
            {
                displayEntity.Features.Add(feature.ToDisplayEntity());
            }

            foreach (var result in dto.Results)
            {
                displayEntity.Results.Add(result.ToDisplayEntity());
            }
            
            return displayEntity;
        }

        public static DTO.Pricing ToDto(this Display.Pricing displayEntity)
        {
            var dto = new DTO.Pricing(displayEntity.Id, displayEntity.Identity.ToDto());
            
            foreach (var group in displayEntity.FilterGroups)
            {
                dto.FilterGroups.Add(group.ToDto());
            }

            foreach (var driver in displayEntity.Drivers)
            {
                dto.Drivers.Add(driver.ToDto());
            }

            foreach (var group in displayEntity.PriceListGroups)
            {
                dto.PriceListGroups.Add(group.ToDto());
            }
                 
            return dto;
        }
    }
}
