using System;
using System.Collections.Generic;

using DTO = APLPX.Entity;
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

            displayEntity.IsDirty = false;

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
            displayEntity.Name = dto.Name;

            if (dto.Filters != null)
            {
                foreach (DTO.Filter filterDto in dto.Filters)
                {
                    displayEntity.Filters.Add(filterDto.ToDisplayEntity());
                }
            }

            displayEntity.IsDirty = false;

            return displayEntity;
        }


        public static List<Display.FilterGroup> ToDisplayEntities(this List<DTO.FilterGroup> dtoList)
        {
            var displayList = new List<Display.FilterGroup>();

            foreach (DTO.FilterGroup dto in dtoList)
            {
                displayList.Add(dto.ToDisplayEntity());
            }

            return displayList;
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
                                        displayEntity.Name,
                                        filters);
            return result;
        }

        #endregion
    }
}
