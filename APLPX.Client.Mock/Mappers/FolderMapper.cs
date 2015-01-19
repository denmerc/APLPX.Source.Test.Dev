using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
using Display = APLPX.Common.Mock.Entity;


namespace APLPX.Client.Mock.Mappers
{
    /// <summary>
    /// Extension methods for mapping Folder display and client entities.
    /// </summary>
    public static class FolderMapper
    {
        public static Display.Folder ToDisplayEntity(this DTO.Folder dto)
        {
            var displayEntity = new Display.Folder();

            displayEntity.Id = dto.Id;
            displayEntity.Template = dto.Template;
            displayEntity.ItemCount = dto.ItemCount;
            displayEntity.Name = dto.Name;
            displayEntity.ParentName = dto.ParentName;
            displayEntity.IsNameChanged = dto.IsNameChanged;
            displayEntity.CanNameChange = dto.CanNameChange;
            displayEntity.SortOrder = dto.Sort;

            return displayEntity;
        }

        public static DTO.Folder ToDto(this Display.Folder displayEntity)
        {
            var dto = new DTO.Folder(
                                displayEntity.Id, 
                                displayEntity.Template, 
                                displayEntity.ItemCount, 
                                displayEntity.Name, 
                                displayEntity.ParentName, 
                                displayEntity.IsNameChanged, 
                                displayEntity.CanNameChange, 
                                displayEntity.SortOrder);

            return dto;
        }
    }
}
