using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
using Display = APLPX.Common.Mock.Entity;


namespace APLPX.Client.Mock.Mappers
{
    //TODO: rename PricingEverydayMapper

    /// <summary>
    /// Extension methods for mapping PricingEveryday display and client entities.
    /// </summary>
    public static class PricingMapper
    {
        public static Display.PricingEveryday ToDisplayEntity(this DTO.PricingEveryday dto)
        {
            var displayEntity = new Display.PricingEveryday();

            displayEntity.Id = dto.Id;
            displayEntity.SearchGroupKey = dto.SearchGroupKey;

            if (dto.Identity != null)
            {
                displayEntity.Identity = dto.Identity.ToDisplayEntity();
            }

            if (dto.FilterGroups != null)
            {
                foreach (var filterGroup in dto.FilterGroups)
                {
                    displayEntity.FilterGroups.Add(filterGroup.ToDisplayEntity());
                }
            }

            if (dto.ValueDrivers != null)
            {
                foreach (var driver in dto.ValueDrivers)
                {
                    displayEntity.ValueDrivers.Add(driver.ToDisplayEntity());
                }
            }

            if (dto.KeyValueDriver != null)
            {
                displayEntity.KeyValueDriver = dto.KeyValueDriver.ToDisplayEntity();
            }


            if (dto.LinkedValueDrivers != null)
            {
                foreach (var driver in dto.LinkedValueDrivers)
                {
                    displayEntity.LinkedValueDrivers.Add(driver.ToDisplayEntity());
                }
            }

            if (dto.PricingModes != null)
            {
                foreach (var mode in dto.PricingModes)
                {
                    displayEntity.PricingModes.Add(mode.ToDisplayEntity());
                }
            }

            if (dto.PriceListGroups != null)
            {
                foreach (var priceListGroup in dto.PriceListGroups)
                {
                    displayEntity.PriceListGroups.Add(priceListGroup.ToDisplayEntity());
                }
            }

            if (dto.KeyPriceListRule != null)
            {
                displayEntity.KeyPriceListRule = dto.KeyPriceListRule.ToDisplayEntity();
            }

            if (dto.LinkedPriceListRules != null)
            {
                foreach (var rule in dto.LinkedPriceListRules)
                {
                    displayEntity.LinkedPriceListRules.Add(rule.ToDisplayEntity());
                }
            }

            if (dto.Results != null)
            {
                foreach (DTO.PricingEverydayResult result in dto.Results)
                {
                    displayEntity.Results.Add(result.ToDisplayEntity());
                }
            }

            return displayEntity;
        }

        public static List<Display.PricingEveryday> ToDisplayEntities(this List<DTO.PricingEveryday> dtoList)
        {
            var displayList = new List<Display.PricingEveryday>();

            foreach (DTO.PricingEveryday dto in dtoList)
            {
                displayList.Add(dto.ToDisplayEntity());
            }

            return displayList;
        }

        public static DTO.PricingEveryday ToDto(this Display.PricingEveryday displayEntity)
        {
            DTO.PricingIdentity identity = displayEntity.Identity.ToDto();

            var filterGroups = new List<DTO.FilterGroup>();
            if(displayEntity.FilterGroups != null)
            { 
                foreach (var filterGroup in displayEntity.FilterGroups)
                {
                    filterGroups.Add(filterGroup.ToDto());
                }
            }

            var valueDrivers = new List<DTO.PricingEverydayValueDriver>();

            if(displayEntity.ValueDrivers != null)
            {
                foreach (var driver in displayEntity.ValueDrivers)
                {
                    valueDrivers.Add(driver.ToDto());
                }
            }
            DTO.PricingEverydayKeyValueDriver keyValueDriver = new DTO.PricingEverydayKeyValueDriver();
            if( displayEntity.KeyValueDriver != null)
            {
                keyValueDriver = displayEntity.KeyValueDriver.ToDto();
            }

            var linkedValueDrivers = new List<DTO.PricingEverydayLinkedValueDriver>();
            if (displayEntity.LinkedValueDrivers != null)
            {
                foreach (var driver in displayEntity.LinkedValueDrivers)
                {
                    linkedValueDrivers.Add(driver.ToDto());
                }
            }

            var pricingModes = new List<DTO.PricingMode>();
            if (displayEntity.PricingModes != null)
            {
                foreach (var mode in displayEntity.PricingModes)
                {
                    pricingModes.Add(mode.ToDto());
                }
            }

            var priceListGroups = new List<DTO.PricingEverydayPriceListGroup>();
            if (displayEntity.PriceListGroups != null)
            {
                foreach (var priceListGroup in displayEntity.PriceListGroups)
                {
                    priceListGroups.Add(priceListGroup.ToDto());
                }
            }

            DTO.PricingKeyPriceListRule keyPriceListRule = new DTO.PricingKeyPriceListRule();
            if ( displayEntity.KeyPriceListRule != null)
            {
                keyPriceListRule = displayEntity.KeyPriceListRule.ToDto();
            }

            var linkedPriceListRules = new List<DTO.PricingLinkedPriceListRule>();
            if (displayEntity.LinkedPriceListRules != null)
            {
                foreach (var rule in displayEntity.LinkedPriceListRules)
                {
                    linkedPriceListRules.Add(rule.ToDto());
                }
            }

            var results = new List<DTO.PricingEverydayResult>();
            if (displayEntity.Results != null)
            {
                foreach (var result in displayEntity.Results)
                {
                    results.Add(result.ToDto());
                }
            }

            DTO.PricingEveryday dto = new DTO.PricingEveryday(
                                                displayEntity.Id,
                                                displayEntity.SearchGroupKey,
                                                identity,
                                                filterGroups,
                                                valueDrivers,
                                                keyValueDriver,
                                                linkedValueDrivers,
                                                pricingModes,
                                                priceListGroups,
                                                keyPriceListRule,
                                                linkedPriceListRules,
                                                results);
            return dto;
        }

        public static List<DTO.PricingEveryday> ToDtos(this List<Display.PricingEveryday> displayList)
        {
            var dtoList = new List<DTO.PricingEveryday>();

            foreach (Display.PricingEveryday dto in displayList)
            {
                dtoList.Add(dto.ToDto());
            }

            return dtoList;
        }
    }
}
