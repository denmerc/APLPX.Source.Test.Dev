﻿using System;
using System.Collections.Generic;

using DTO = APLPX.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping SearchGroup display and client entities.
    /// </summary>
    public static class SearchGroupMapper
    {
        public static Display.FeatureSearchGroup ToDisplayEntity(this DTO.FeatureSearchGroup dto)
        {
            var displayEntity = new Display.FeatureSearchGroup();

            displayEntity.SearchId = dto.SearchId;
            displayEntity.SearchKey = dto.SearchGroup;
            displayEntity.ItemCount = (short)dto.ItemCount;
            displayEntity.Name = dto.Name;
            displayEntity.ParentName = dto.ParentName;
            displayEntity.IsNameChanged = dto.IsNameChanged;
            displayEntity.CanNameChange = dto.CanNameChange;
            displayEntity.CanSearchKeyChange = dto.CanSearchGroupChange;
            displayEntity.IsSearchKeyChanged = dto.IsSearchGroupChanged;
            displayEntity.Sort = dto.Sort;

            return displayEntity;
        }

        public static DTO.FeatureSearchGroup ToDto(this Display.FeatureSearchGroup displayEntity)
        {
            var dto = new DTO.FeatureSearchGroup(
                                    displayEntity.Name, 
                                    displayEntity.ItemCount,
                                    displayEntity.SearchId,
                                    displayEntity.SearchKey,
                                    displayEntity.ParentName,
                                    displayEntity.IsNameChanged,
                                    displayEntity.IsSearchKeyChanged,
                                    displayEntity.CanNameChange,
                                    displayEntity.CanSearchKeyChange,
                                    displayEntity.Sort);

            return dto;
        }
    }
}
