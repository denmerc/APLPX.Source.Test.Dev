﻿using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Filter display and client entities.
    /// </summary>
    public static class FilterMapper
    {
        #region Filter

        public static Display.Filter ToDisplayEntity(this DTO.Filter dto)
        {
            var displayEntity = new Display.Filter();

            displayEntity.Id = dto.Id;
            displayEntity.Key = dto.Key;
            displayEntity.Code = dto.Code;
            displayEntity.Name = dto.Name;
            displayEntity.IsSelected = dto.IsSelected;
            displayEntity.Sort = dto.Sort;

            return displayEntity;
        }

        public static DTO.Filter ToDto(this Display.Filter displayEntity)
        {
            var dto = new DTO.Filter(
                                    displayEntity.Id,
                                    displayEntity.Key,
                                    displayEntity.Code,
                                    displayEntity.Name,
                                    displayEntity.IsSelected,
                                    displayEntity.Sort);

            return dto;
        }


        #endregion

        #region FilterGroup

        public static Display.FilterGroup ToDisplayEntity(this DTO.FilterGroup dto)
        {
            var displayEntity = new Display.FilterGroup();
            
            displayEntity.Sort = dto.Sort;
            displayEntity.TypeName = dto.TypeName;

            if (dto.Filters != null)
            {
                foreach (DTO.Filter filterDto in dto.Filters)
                {
                    displayEntity.Filters.Add(filterDto.ToDisplayEntity());
                }
            }            

            return displayEntity;
        }

        public static DTO.FilterGroup ToDto(this Display.FilterGroup displayEntity)
        {
            var filters = new List<DTO.Filter>();

            foreach (Display.Filter displayFilter in displayEntity.Filters)
            {
                filters.Add(displayFilter.ToDto());
            }
            
            var result = new DTO.FilterGroup(
                                        displayEntity.Sort,
                                        displayEntity.TypeName,
                                        filters);
            return result;
        }

        #endregion
    }
}
