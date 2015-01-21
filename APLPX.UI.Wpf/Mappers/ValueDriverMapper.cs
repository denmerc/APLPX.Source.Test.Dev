using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Driver display and client entities.
    /// </summary>
    public static class ValueDriverMapper
    {
        #region Analytic Value Driver

        public static Display.AnalyticValueDriver ToDisplayEntity(this DTO.AnalyticValueDriver dto)
        {
            var displayEntity = new Display.AnalyticValueDriver();

            displayEntity.Key = dto.Key;
            displayEntity.Id = dto.Id;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Sort = dto.Sort;
            displayEntity.IsSelected = dto.IsSelected;

            if (dto.Modes != null)
            {
                foreach (DTO.AnalyticValueDriverMode mode in dto.Modes)
                {
                    displayEntity.Modes.Add(mode.ToDisplayEntity());
                }
            }

            if (dto.Results != null)
            {
                foreach (DTO.AnalyticResultValueDriverGroup result in dto.Results)
                {
                    displayEntity.Results.Add(result.ToDisplayEntity());
                }
            }

            return displayEntity;
        }

        public static List<Display.AnalyticValueDriver> ToDisplayEntities(this List<DTO.AnalyticValueDriver> dtos)
        {
            var displayList = new List<Display.AnalyticValueDriver>();

            foreach (DTO.AnalyticValueDriver moduleDTO in dtos)
            {
                displayList.Add(moduleDTO.ToDisplayEntity());
            }

            return displayList;
        }


        public static DTO.AnalyticValueDriver ToDto(this Display.AnalyticValueDriver displayEntity)
        {
            var modes = new List<DTO.AnalyticValueDriverMode>();
            displayEntity.Modes.ForEach(mode => modes.Add(mode.ToDto()));

            var results = new List<DTO.AnalyticResultValueDriverGroup>();
            foreach (Display.AnalyticResult result in displayEntity.Results)
            {
                results.Add(result.ToDto());
            }

            var dto = new DTO.AnalyticValueDriver(
                                        displayEntity.Id,
                                        displayEntity.Key,
                                        displayEntity.IsSelected,
                                        displayEntity.Name,
                                        displayEntity.Title,
                                        displayEntity.Sort,
                                        results,
                                        modes);

            return dto;
        }

        #endregion

        #region Pricing Everyday Driver

        public static Display.PricingEverydayValueDriver ToDisplayEntity(this DTO.PricingEverydayValueDriver dto)
        {
            var displayEntity = new Display.PricingEverydayValueDriver();
            displayEntity.Key = dto.Key;
            displayEntity.Id = dto.Id;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Sort = dto.Sort;
            displayEntity.IsSelected = dto.IsSelected;

            displayEntity.IsKey = dto.IsKey;

            foreach (var group in dto.Groups)
            {
                displayEntity.Groups.Add(group.ToDisplayEntity());
            }

            return displayEntity;
        }

        public static DTO.PricingEverydayValueDriver ToDto(this Display.PricingEverydayValueDriver displayEntity)
        {
            var groups = new List<DTO.PricingValueDriverGroup>();
            foreach (Display.PricingValueDriverGroup group in displayEntity.Groups)
            {
                groups.Add(group.ToDto());
            }

            var dto = new DTO.PricingEverydayValueDriver(
                                        displayEntity.Id,
                                        displayEntity.Key,
                                        displayEntity.IsSelected,
                                        displayEntity.Name,
                                        displayEntity.Title,
                                        displayEntity.Sort,
                                        displayEntity.IsKey,
                                        groups);
            return dto;
        }

        #endregion

        #region Pricing Everyday Key Value Driver

        public static Display.PricingEverydayKeyValueDriver ToDisplayEntity(this DTO.PricingEverydayKeyValueDriver dto)
        {
            var displayEntity = new Display.PricingEverydayKeyValueDriver();
            displayEntity.ValueDriverId = dto.ValueDriverId;

            foreach (DTO.PricingEverydayKeyValueDriverGroup group in dto.Groups)
            {
                displayEntity.Groups.Add(group.ToDisplayEntity());
            }

            return displayEntity;
        }

        public static DTO.PricingEverydayKeyValueDriver ToDto(this Display.PricingEverydayKeyValueDriver displayEntity)
        {
            var groups = new List<DTO.PricingEverydayKeyValueDriverGroup>();
            foreach (Display.PricingEverydayKeyValueDriverGroup group in displayEntity.Groups)
            {
                groups.Add(group.ToDto());
            }
            var dto = new DTO.PricingEverydayKeyValueDriver(displayEntity.ValueDriverId, groups);

            return dto;
        }

        #endregion

        public static Display.PricingEverydayLinkedValueDriver ToDisplayEntity(this DTO.PricingEverydayLinkedValueDriver dto)
        {
            var displayEntity = new Display.PricingEverydayLinkedValueDriver();
            displayEntity.ValueDriverId = dto.ValueDriverId;

            foreach (DTO.PricingEverydayLinkedValueDriverGroup group in dto.Groups)
            {
                displayEntity.Groups.Add(group.ToDisplayEntity());
            }

            return displayEntity;
        }

        public static List<Display.PricingEverydayLinkedValueDriver> ToDisplayEntities(this List<DTO.PricingEverydayLinkedValueDriver> dtos)
        {
            var displayList = new List<Display.PricingEverydayLinkedValueDriver>();

            foreach (DTO.PricingEverydayLinkedValueDriver moduleDTO in dtos)
            {
                displayList.Add(moduleDTO.ToDisplayEntity());
            }

            return displayList;
        }

        public static DTO.PricingEverydayLinkedValueDriver ToDto(this Display.PricingEverydayLinkedValueDriver displayEntity)
        {
            var groups = new List<DTO.PricingEverydayLinkedValueDriverGroup>();
            foreach (Display.PricingEverydayLinkedValueDriverGroup group in displayEntity.Groups)
            {
                groups.Add(group.ToDto());
            }
            var dto = new DTO.PricingEverydayLinkedValueDriver(displayEntity.ValueDriverId, groups);

            return dto;
        }
    }
}
