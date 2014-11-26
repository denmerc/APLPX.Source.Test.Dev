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

            if (dto.FilterGroups != null)
            {
                foreach (var filterGroup in dto.FilterGroups)
                {
                    displayEntity.FilterGroups.Add(filterGroup.ToDisplayEntity());
                }
            }

            if (dto.Drivers != null)
            {
                foreach (var driver in dto.Drivers)
                {
                    displayEntity.Drivers.Add(driver.ToDisplayEntity());
                }
            }

            if (dto.PriceListGroups != null)
            {
                foreach (var priceListGroup in dto.PriceListGroups)
                {
                    displayEntity.PriceListGroups.Add(priceListGroup.ToDisplayEntity());
                }
            }

            if (dto.Results != null)
            {
                foreach (var result in dto.Results)
                {
                    displayEntity.Results.Add(result.ToDisplayEntity());
                }
            }

            return displayEntity;
        }

        public static DTO.Pricing ToDto(this Display.Pricing displayEntity)
        {

            var filterGroups = new List<DTO.FilterGroup>();
            foreach (var filterGroup in displayEntity.FilterGroups)
            {
                filterGroups.Add(filterGroup.ToDto());
            }

            var drivers = new List<DTO.PricingDriver>();
            foreach (var driver in displayEntity.Drivers)
            {
                drivers.Add(driver.ToDto());
            }

            var priceListGroups = new List<DTO.PriceListGroup>();
            foreach (var priceListGroup in displayEntity.PriceListGroups)
            {
                priceListGroups.Add(priceListGroup.ToDto());
            }

            var results = new List<DTO.PricingResult>();
            foreach (var result in displayEntity.Results)
            {
                results.Add(result.ToDto());
            }

            var dto = new DTO.Pricing(
                                displayEntity.Id,
                                displayEntity.Identity.ToDto(),
                                drivers,
                                priceListGroups,
                                filterGroups,
                                results);

            return dto;
        }
    }
}
