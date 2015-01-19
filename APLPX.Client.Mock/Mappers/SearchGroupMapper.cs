using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
using Display = APLPX.Common.Mock.Entity;


namespace APLPX.Client.Mock.Mappers
{
    /// <summary>
    /// Extension methods for mapping SearchGroup display and client entities.
    /// </summary>
    public static class SearchGroupMapper
    {
        public static Display.FeatureSearchGroup ToDisplayEntity(this DTO.FeatureSearchGroup dto)
        {
            var displayEntity = new Display.FeatureSearchGroup();

            displayEntity.SearchId = dto.SearchGroupId;
            displayEntity.SearchGroupKey = dto.SearchGroupKey;
            displayEntity.ItemCount = (short)dto.ItemCount;
            displayEntity.Name = dto.Name;
            displayEntity.ParentName = dto.ParentName;
            displayEntity.IsNameChanged = dto.IsNameChanged;
            displayEntity.CanNameChange = dto.CanNameChange;
            displayEntity.CanSearchGroupChange = dto.CanSearchGroupChange;
            displayEntity.IsSearchGroupChanged = dto.IsSearchGroupChanged;
            displayEntity.Sort = dto.Sort;

            return displayEntity;
        }

        public static DTO.FeatureSearchGroup ToDto(this Display.FeatureSearchGroup displayEntity)
        {
            var dto = new DTO.FeatureSearchGroup(
                                    displayEntity.Name, 
                                    displayEntity.ItemCount,
                                    displayEntity.SearchId,
                                    displayEntity.SearchGroupKey,
                                    displayEntity.ParentName,
                                    displayEntity.IsNameChanged,
                                    displayEntity.IsSearchGroupChanged,
                                    displayEntity.CanNameChange,
                                    displayEntity.CanSearchGroupChange,
                                    displayEntity.Sort);

            return dto;
        }
    }
}
