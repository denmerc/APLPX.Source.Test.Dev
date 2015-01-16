using System;
using System.Collections.Generic;

using DTO = APLPX.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping PriceList display and client entities.
    /// </summary>
    public static class PriceListMapper
    {
        #region PriceList (common)

        public static Display.PriceList ToDisplayEntity(this DTO.PriceList dto)
        {
            var displayEntity = new Display.PriceList();

            displayEntity.Id = dto.Id;
            displayEntity.Key = dto.Key;
            displayEntity.Code = dto.Code;
            displayEntity.Name = dto.Name;
            displayEntity.Sort = dto.Sort;
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
                                    displayEntity.Sort,
                                    displayEntity.IsSelected);

            return dto;
        }

        #endregion

        #region PricingResultEdit (common)

        public static Display.PricingResultEdit ToDisplayEntity(this DTO.PricingResultEdit dto)
        {
            var displayEntity = new Display.PricingResultEdit();

            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Type = dto.Type;

            return displayEntity;
        }

        public static DTO.PricingResultEdit ToDto(this Display.PricingResultEdit displayEntity)
        {
            var dto = new DTO.PricingResultEdit(
                                    displayEntity.Name,
                                    displayEntity.Title,
                                    displayEntity.Type);
            return dto;
        }

        #endregion

        #region PricingResultWarning (common)

        public static Display.PricingResultWarning ToDisplayEntity(this DTO.PricingResultWarning dto)
        {
            var displayEntity = new Display.PricingResultWarning();

            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Type = dto.Type;

            return displayEntity;
        }

        public static DTO.PricingResultWarning ToDto(this Display.PricingResultWarning displayEntity)
        {
            var dto = new DTO.PricingResultWarning(
                                    displayEntity.Name,
                                    displayEntity.Title,
                                    displayEntity.Type);
            return dto;
        }

        #endregion

        #region Analytic PriceListGroup

        public static Display.AnalyticPriceListGroup ToDisplayEntity(this DTO.AnalyticPriceListGroup dto)
        {
            var displayEntity = new Display.AnalyticPriceListGroup();
            displayEntity.Key = dto.Key;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Sort = dto.Sort;

            if (dto.PriceLists != null)
            {
                dto.PriceLists.ForEach(item => displayEntity.PriceLists.Add(item.ToDisplayEntity()));
            }

            return displayEntity;
        }

        public static DTO.AnalyticPriceListGroup ToDto(this Display.AnalyticPriceListGroup displayEntity)
        {
            List<DTO.PriceList> priceLists = new List<DTO.PriceList>();
            foreach (Display.PriceList priceList in displayEntity.PriceLists)
            {
                priceLists.Add(priceList.ToDto());
            }

            var dto = new DTO.AnalyticPriceListGroup(
                                        displayEntity.Key,
                                        displayEntity.Name,
                                        displayEntity.Title,
                                        displayEntity.Sort,
                                        priceLists);

            return dto;
        }

        #endregion

        #region Pricing Everyday PriceList

        public static Display.PricingEverydayPriceList ToDisplayEntity(this DTO.PricingEverydayPriceList dto)
        {
            var displayEntity = new Display.PricingEverydayPriceList();

            displayEntity.Id = dto.Id;
            displayEntity.Key = dto.Key;
            displayEntity.Code = dto.Code;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Sort = dto.Sort;
            displayEntity.IsSelected = dto.IsSelected;

            displayEntity.IsKey = dto.IsKey;

            return displayEntity;
        }

        public static DTO.PricingEverydayPriceList ToDto(this Display.PricingEverydayPriceList displayEntity)
        {
            var dto = new DTO.PricingEverydayPriceList(
                                    displayEntity.Id,
                                    displayEntity.Key,
                                    displayEntity.Code,
                                    displayEntity.Name,
                                    displayEntity.Sort,
                                    displayEntity.IsSelected,
                                    displayEntity.IsKey);

            return dto;
        }


        public static Display.PricingEverydayPriceListGroup ToDisplayEntity(this DTO.PricingEverydayPriceListGroup dto)
        {
            var displayEntity = new Display.PricingEverydayPriceListGroup();

            displayEntity.Key = dto.Key;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Sort = dto.Sort;

            if (dto.PriceLists != null)
            {
                dto.PriceLists.ForEach(item => displayEntity.PriceLists.Add(item.ToDisplayEntity()));
            }
            return displayEntity;
        }

        public static DTO.PricingEverydayPriceListGroup ToDto(this Display.PricingEverydayPriceListGroup displayEntity)
        {
            var priceLists = new List<DTO.PricingEverydayPriceList>();
            foreach (var priceList in displayEntity.PriceLists)
            {
                priceLists.Add(priceList.ToDto());
            }

            var dto = new DTO.PricingEverydayPriceListGroup(
                                            displayEntity.Key,
                                            displayEntity.Name,
                                            displayEntity.Title,
                                            displayEntity.Sort,
                                            priceLists);
            return dto;
        }

        #endregion

        #region PricingEverydayResultPriceList

        public static Display.PricingEverydayResultPriceList ToDisplayEntity(this DTO.PricingEverydayResultPriceList dto)
        {
            var displayEntity = new Display.PricingEverydayResultPriceList();

            displayEntity.ResultId = dto.ResultId;
            displayEntity.CurrentPrice = dto.CurrentPrice;
            displayEntity.NewPrice = dto.NewPrice;
            displayEntity.CurrentMarkupPercent = dto.CurrentMarkupPercent;
            displayEntity.NewMarkupPercent = dto.NewMarkupPercent;
            displayEntity.KeyValueChange = dto.KeyValueChange;
            displayEntity.InfluenceValueChange = dto.InfluenceValueChange;
            displayEntity.PriceChange = dto.PriceChange;

            if (dto.PriceEdit != null)
            {
                displayEntity.PriceEdit = dto.PriceEdit.ToDisplayEntity();
            }

            if (dto.PriceWarning != null)
            {
                displayEntity.PriceWarning = dto.PriceWarning.ToDisplayEntity();
            }

            displayEntity.IsKey = dto.IsKey;
            displayEntity.Id = dto.Id;
            displayEntity.Key = dto.Key;
            displayEntity.Code = dto.Code;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Sort = dto.Sort;
            displayEntity.IsSelected = dto.IsSelected;

            return displayEntity;
        }

        public static DTO.PricingEverydayResultPriceList ToDto(this Display.PricingEverydayResultPriceList displayEntity)
        {
            var dto = new DTO.PricingEverydayResultPriceList(displayEntity.ResultId,
                                        displayEntity.Id,
                                        displayEntity.Key,
                                        displayEntity.Code,
                                        displayEntity.Name,
                                        displayEntity.Sort,
                                        displayEntity.IsSelected,
                                        displayEntity.IsKey,
                                        displayEntity.CurrentPrice,
                                        displayEntity.NewPrice,
                                        displayEntity.CurrentMarkupPercent,
                                        displayEntity.NewMarkupPercent,
                                        displayEntity.KeyValueChange,
                                        displayEntity.InfluenceValueChange,
                                        displayEntity.PriceChange,
                                        displayEntity.PriceEdit.ToDto(),
                                        displayEntity.PriceWarning.ToDto());
            return dto;
        }

        #endregion

    }
}
