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
            displayEntity.Sort = dto.Sort;

            return displayEntity;
        }

        public static DTO.PriceList ToDto(this Display.PriceList displayEntity)
        {
            var dto = new DTO.PriceList(
                                    displayEntity.Id,
                                    displayEntity.Key,
                                    displayEntity.Code,
                                    displayEntity.Name,
                                    displayEntity.IsSelected,
                                    displayEntity.Sort);

            return dto;
        }

        #endregion

        #region PriceListGroup

        public static Display.AnalyticPriceListGroup ToDisplayEntity(this DTO.PriceListGroup dto)
        {
            var displayEntity = new Display.AnalyticPriceListGroup();

            //TODO: UNCOMMENT
            //displayEntity.Key = dto.Key;
            //displayEntity.Name = dto.Name;
            //displayEntity.Title = dto.Title;

            if (dto.PriceLists != null)
            {
                dto.PriceLists.ForEach(item => displayEntity.PriceLists.Add(item.ToDisplayEntity()));
            }

            return displayEntity;
        }

        public static DTO.PriceListGroup ToDto(this Display.AnalyticPriceListGroup displayEntity)
        {
            List<DTO.PriceList> priceLists = new List<DTO.PriceList>();
            foreach (Display.PriceList priceList in displayEntity.PriceLists)
            {
                priceLists.Add(priceList.ToDto());
            }

            //TODO: update w/new fields
            var dto = new DTO.PriceListGroup(displayEntity.Sort, displayEntity.Name, priceLists);

            return dto;
        }

        #endregion

    }
}
