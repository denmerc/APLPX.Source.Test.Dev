using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
using Display = APLPX.Common.Mock.Entity;


namespace APLPX.Client.Mock.Mappers
{
    /// <summary>
    /// Extension methods for mapping AnalyticResult display and client entities.
    /// </summary>
    public static class ResultMapper
    {
        #region Analytic Result

        public static Display.AnalyticResultValueDriverGroup ToDisplayEntity(this DTO.AnalyticResultValueDriverGroup dto)
        {
            var displayEntity = new Display.AnalyticResultValueDriverGroup();
            displayEntity.Id = dto.Id;
            displayEntity.Value = dto.Value;
            displayEntity.MinOutlier = dto.MinOutlier;
            displayEntity.MaxOutlier = dto.MaxOutlier;
            displayEntity.SalesValue = dto.SalesValue;
            displayEntity.Sort = dto.Sort;

            displayEntity.SkuCount = dto.SkuCount;
            displayEntity.SalesValue = dto.SalesValue;
            displayEntity.Run = dto.Run;

            return displayEntity;
        }

        public static DTO.AnalyticResultValueDriverGroup ToDto(this Display.AnalyticResultValueDriverGroup displayEntity)
        {
            var dto = new DTO.AnalyticResultValueDriverGroup(
                                        displayEntity.Value,
                                        displayEntity.MinOutlier,
                                        displayEntity.MaxOutlier,
                                        displayEntity.SkuCount,
                                        displayEntity.SalesValue);

            dto.Run = displayEntity.Run;

            return dto;
        }

        #endregion

        #region Pricing Everyday Result

        public static Display.PricingEverydayResult ToDisplayEntity(this DTO.PricingEverydayResult dto)
        {
            var displayEntity = new Display.PricingEverydayResult();

            displayEntity.SkuId = dto.SkuId;
            displayEntity.SkuName = dto.SkuName;
            displayEntity.SkuTitle = dto.SkuTitle;
            if (dto.Groups != null)
            {
                foreach (DTO.PricingResultDriverGroup group in dto.Groups)
                {
                    displayEntity.Groups.Add(group.ToDisplayEntity());
                }
            }

            return displayEntity;
        }

        public static DTO.PricingEverydayResult ToDto(this Display.PricingEverydayResult displayEntity)
        {
            var driverGroups = new List<DTO.PricingResultDriverGroup>();
            foreach (Display.PricingResultDriverGroup group in displayEntity.Groups)
            {
                driverGroups.Add(group.ToDto());
            }

            var priceLists = new List<DTO.PricingEverydayResultPriceList>();
            foreach (Display.PricingEverydayResultPriceList priceList in displayEntity.PriceLists)
            {
                priceLists.Add(priceList.ToDto());
            }

            var dto = new DTO.PricingEverydayResult(
                                            displayEntity.SkuId,
                                            displayEntity.SkuName,
                                            displayEntity.SkuTitle,
                                            driverGroups,
                                            priceLists);
            return dto;
        }

        public static List<DTO.PricingEverydayResult> ToDtos(this List<Display.PricingEverydayResult> displayList)
        {
            var dtoList = new List<DTO.PricingEverydayResult>();

            foreach (Display.PricingEverydayResult dto in displayList)
            {
                dtoList.Add(dto.ToDto());
            }

            return dtoList;
        }

        #endregion

    }
}
