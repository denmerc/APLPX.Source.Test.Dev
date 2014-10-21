using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Analytic display and client entities.
    /// </summary>
    public static class AnalyticMapper
    {
        public static Display.Analytic ToDisplayEntity(this DTO.Analytic dto)
        {
            var displayEntity = new Display.Analytic();

            displayEntity.Identity = dto.Identity.ToDisplayEntity();

            foreach (DTO.FilterGroup filterGroup in dto.FilterGroups)
            {
                displayEntity.FilterGroups.Add(filterGroup.ToDisplayEntity());
            }

            foreach (DTO.AnalyticDriver driver in dto.Drivers)
            {
                displayEntity.Drivers.Add(driver.ToDisplayEntity());
            }

            foreach (DTO.PriceListGroup priceListGroup in dto.PriceListGroups)
            {
                displayEntity.PriceListGroups.Add(priceListGroup.ToDisplayEntity());
            }

            foreach (DTO.AnalyticResult result in dto.Results)
            {
                displayEntity.Results.Add(result.ToDisplayEntity());
            }

            return displayEntity;
        }

        public static DTO.Analytic ToDto(this Display.Analytic displayEntity)
        {
            DTO.AnalyticIdentity identity = displayEntity.Identity.ToDto();

            var filterGroups = new List<DTO.FilterGroup>();            
            displayEntity.FilterGroups.ForEach(item => filterGroups.Add(item.ToDto()));

            var drivers = new List<DTO.AnalyticDriver>();
            displayEntity.Drivers.ForEach(item => drivers.Add(item.ToDto()));

            var priceListGroups = new List<DTO.PriceListGroup>();
            displayEntity.PriceListGroups.ForEach(item => priceListGroups.Add(item.ToDto()));

            var results = new List<DTO.AnalyticResult>();
            displayEntity.Results.ForEach(item => results.Add(item.ToDto()));

            var dto = new DTO.Analytic(
                                    displayEntity.Id, 
                                    identity, 
                                    drivers, 
                                    priceListGroups, 
                                    filterGroups, 
                                    results);

            return dto;
        }

    }
}
