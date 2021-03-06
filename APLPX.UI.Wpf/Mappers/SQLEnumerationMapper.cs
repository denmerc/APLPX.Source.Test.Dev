﻿using System;
using System.Collections.Generic;

using DTO = APLPX.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping SQLEnumerationMapper display and client entities.
    /// </summary>
    public static class SQLEnumerationMapper
    {
        #region SQL Enumeration

        public static Display.SQLEnumeration ToDisplayEntity(this DTO.SQLEnumeration dto)
        {
            var result = new Display.SQLEnumeration();

            result.Value = dto.Value;
            result.Name = dto.Name;
            result.Description = dto.Description;
            result.Sort = dto.Sort;                        

            return result;
        }

        public static DTO.SQLEnumeration ToDto(this Display.SQLEnumeration displayEntity)
        {
            var dto = new DTO.SQLEnumeration(
                                        displayEntity.Value,                                                                                
                                        displayEntity.Name,
                                        displayEntity.Description, 
                                        displayEntity.Sort);

            return dto;
        }

        #endregion

        #region SQLEnumeration Collection Mapping

        public static List<Display.SQLEnumeration> ToDisplayEntities(this List<DTO.SQLEnumeration> dtos)
        {
            var displayList = new List<Display.SQLEnumeration>();

            foreach (DTO.SQLEnumeration item in dtos)
            {
                displayList.Add(item.ToDisplayEntity());
            }

            return displayList;
        }

        public static List<DTO.SQLEnumeration> ToDTOList(this List<Display.SQLEnumeration> displayEntities)
        {
            var dtoList = new List<DTO.SQLEnumeration>();

            foreach (Display.SQLEnumeration item in displayEntities)
            {
                dtoList.Add(item.ToDto());
            }

            return dtoList;
        }


        #endregion
    }
}
