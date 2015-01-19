using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
using Display = APLPX.Common.Mock.Entity;


namespace APLPX.Client.Mock.Mappers
{
    /// <summary>
    /// Extension methods for mapping Value display and client entities.
    /// </summary>
    public static class ValueMapper
    {

        #region Filters

        public static Display.FilterValue ToDisplayEntity(this Client.Filter.Value dto)
        {
            var result = new Display.FilterValue();

            result.Id = dto.Id;
            result.Key = dto.Key;
            result.Code = dto.Code;
            result.Name = dto.Name;
            result.IsSelected = dto.Included;

            return result;
        }


        public static Client.Filter.Value ToDTO(this Display.FilterValue displayEntity)
        {
            var result = new APLPX.Client.Entity.Filter.Value
            (
                displayEntity.Id,
                displayEntity.Key,
                displayEntity.Code,
                displayEntity.Name,
                displayEntity.IsSelected
            );

            return result;
        }

        #endregion

        #region PriceLists

        public static Display.PriceListValue ToDisplayEntity(this Client.PriceList.Value dto)
        {
            var result = new Display.PriceListValue();

            result.Id = dto.Id;
            result.Key = dto.Key;
            result.Code = dto.Code;
            result.Name = dto.Name;
            result.IsSelected = dto.Included;

            return result;
        }

        public static Client.PriceList.Value ToDTO(this Display.PriceListValue displayEntity)
        {
            var result = new Client.PriceList.Value
            (
                displayEntity.Id,
                displayEntity.Key,
                displayEntity.Code,
                displayEntity.Name,
                displayEntity.IsSelected
            );

            return result;
        }

        #endregion
    }
}
