﻿using System;
using System.Collections.Generic;

using DTO = APLPX.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping AnalyticResult display and client entities.
    /// </summary>
    public static class ResultMapper
    {
        #region Analytic Result

        public static Display.AnalyticResult ToDisplayEntity(this DTO.AnalyticResultValueDriverGroup dto)
        {
            var displayEntity = new Display.AnalyticResult();
            displayEntity.Id = dto.Id;
            displayEntity.Value = dto.Value;
            displayEntity.MinOutlier = dto.MinOutlier;
            displayEntity.MaxOutlier = dto.MaxOutlier;
            displayEntity.SalesValue = dto.SalesValue;
            displayEntity.Sort = dto.Sort;

            displayEntity.SkuCount = dto.SkuCount;
            displayEntity.SalesValue = dto.SalesValue;

            return displayEntity;
        }

        public static DTO.AnalyticResultValueDriverGroup ToDto(this Display.AnalyticResult displayEntity)
        {
            var dto = new DTO.AnalyticResultValueDriverGroup(
                                        displayEntity.Value,
                                        displayEntity.MinOutlier,
                                        displayEntity.MaxOutlier,
                                        displayEntity.SkuCount,
                                        displayEntity.SalesValue);        
            return dto;
        }

        public static List<Display.AnalyticResult> ToDisplayEntities(this List<DTO.AnalyticResultValueDriverGroup> dtoList)
        {
            var displayList = new List<Display.AnalyticResult>();

            foreach (DTO.AnalyticResultValueDriverGroup dto in dtoList)
            {
                displayList.Add(dto.ToDisplayEntity());
            }

            return displayList;
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

        #endregion

    }
}
