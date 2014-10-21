using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping PriceList display and client entities.
    /// </summary>
    public static class PriceListMapper
    {

        #region PriceList

        public static Display.PriceList ToDisplayEntity(this DTO.PriceList dto)
        {
            var displayEntity = new Display.PriceList();

            displayEntity.Id = dto.Id;
            displayEntity.Key = dto.Key;
            displayEntity.Code = dto.Code;
            displayEntity.Name = dto.Name;
            displayEntity.IsSelected = dto.IsSelected;

            return displayEntity;
        }

        public static DTO.PriceList ToDto(this Display.PriceList displayEntity)
        {
            var dto = new DTO.PriceList(
                                    displayEntity.Id,
                                    displayEntity.Key,
                                    displayEntity.Code,
                                    displayEntity.Name,
                                    displayEntity.IsSelected);

            return dto;
        }

        #endregion

        #region PriceListGroup

        public static Display.PriceListGroup ToDisplayEntity(this DTO.PriceListGroup dto)
        {
            var displayEntity = new Display.PriceListGroup();

            displayEntity.TypeName = dto.TypeName;
            dto.PriceLists.ForEach(item => displayEntity.PriceLists.Add(item.ToDisplayEntity()));

            return displayEntity;
        }

        public static DTO.PriceListGroup ToDto(this Display.PriceListGroup displayEntity)
        {
            List<DTO.PriceList> priceLists = new List<DTO.PriceList>();
            displayEntity.PriceLists.ForEach(item => priceLists.Add(item.ToDto()));

            var dto = new DTO.PriceListGroup(displayEntity.TypeName, priceLists);

            return dto;
        }

        #endregion

    }
}
