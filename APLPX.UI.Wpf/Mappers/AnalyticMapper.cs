using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
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
            displayEntity.SearchGroupKey = dto.SearchGroupKey;
            displayEntity.SearchGroupId = dto.SearchGroupId;
            displayEntity.Id = dto.Id;

            displayEntity.Identity = dto.Identity.ToDisplayEntity();

            if (dto.FilterGroups != null)
            {
                foreach (DTO.FilterGroup filterGroup in dto.FilterGroups)
                {
                    displayEntity.FilterGroups.Add(filterGroup.ToDisplayEntity());
                }
            }

            if (dto.ValueDrivers != null)
            {
                foreach (DTO.AnalyticValueDriver driver in dto.ValueDrivers)
                {
                    displayEntity.ValueDrivers.Add(driver.ToDisplayEntity());
                }
            }

            if (dto.PriceListGroups != null)
            {
                foreach (DTO.AnalyticPriceListGroup priceListGroup in dto.PriceListGroups)
                {
                    displayEntity.PriceListGroups.Add(priceListGroup.ToDisplayEntity());
                }
            }

            displayEntity.IsDirty = false;

            return displayEntity;
        }

        public static DTO.Analytic ToDto(this Display.Analytic displayEntity)
        {
            DTO.AnalyticIdentity identity = displayEntity.Identity.ToDto();

            var filterGroups = new List<DTO.FilterGroup>();

            foreach (Display.FilterGroup filterGroup in displayEntity.FilterGroups)
            {
                filterGroups.Add(filterGroup.ToDto());
            }

            var drivers = new List<DTO.AnalyticValueDriver>();
            foreach (Display.AnalyticValueDriver driver in displayEntity.ValueDrivers)
            {
                drivers.Add(driver.ToDto());
            }

            var priceListGroups = new List<DTO.AnalyticPriceListGroup>();
            foreach (Display.AnalyticPriceListGroup priceListGroup in displayEntity.PriceListGroups)
            {
                priceListGroups.Add(priceListGroup.ToDto());
            }

            var dto = new DTO.Analytic(
                                    displayEntity.Id,
                                    displayEntity.SearchGroupId,
                                    displayEntity.SearchGroupKey,
                                    identity,
                                    drivers,
                                    priceListGroups,
                                    filterGroups);

            return dto;
        }

        public static List<Display.Analytic> ToDisplayEntities(this List<DTO.Analytic> dtoList)
        {
            var displayList = new List<Display.Analytic>();

            foreach (DTO.Analytic dto in dtoList)
            {
                displayList.Add(dto.ToDisplayEntity());
            }

            return displayList;
        }

        public static List<DTO.Analytic> ToDtos(this List<Display.Analytic> displayList)
        {
            var dtoList = new List<DTO.Analytic>();

            foreach (Display.Analytic dto in displayList)
            {
                dtoList.Add(dto.ToDto());
            }

            return dtoList;
        }
    }
}
