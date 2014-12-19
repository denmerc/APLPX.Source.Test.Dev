using System;
using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    /// Extension methods for Price Routines.
    /// </summary>
    public static class PricingExtensions
    {
        /// <summary>
        /// Creates a shallow copy of a <see cref="PricingEveryday"/> to be used as a payload container.
        /// </summary>
        /// <param name="source">The original PricingEveryday object.</param>
        /// <returns>The PricingEveryday object. with only the payload-relevant properties populated.</returns>
        public static PricingEveryday ToPayload(this PricingEveryday source)
        {
            var payload = new PricingEveryday { Id = source.Id };

            return payload;
        }

        //TODO: finish
        public static PricingEveryday Copy(this PricingEveryday source)
        {
            PricingEveryday copy = new PricingEveryday();
            copy.Id = 0;
            copy.SearchKey = source.SearchKey;

            copy.Identity.AnalyticsId = 0;

            DateTime createdDate = DateTime.Now;
            copy.Identity.Created = createdDate;
            copy.Identity.Edited = createdDate;
            copy.Identity.Refreshed = createdDate;

            string copySuffix = " (Copy)";
            copy.Identity.Description = source.Identity.Description + copySuffix;
            copy.Identity.Editor = source.Identity.Editor;
            copy.Identity.Name = source.Identity.Name + copySuffix;
            copy.Identity.Notes = source.Identity.Notes + copySuffix;

            copy.Identity.Active = source.Identity.Active;
            copy.Identity.Author = source.Identity.Author;
            copy.Identity.Owner = source.Identity.Owner;
            copy.Identity.Shared = source.Identity.Shared;

            foreach (FilterGroup filterGroup in source.FilterGroups)
            {
                FilterGroup filterGroupCopy = filterGroup.Copy();
                copy.FilterGroups.Add(filterGroupCopy);
            }

            foreach (PricingEverydayValueDriver driver in source.ValueDrivers)
            {
                PricingEverydayValueDriver driverCopy = driver.Copy();
                copy.ValueDrivers.Add(driverCopy);
            }

            foreach (PricingEverydayPriceListGroup priceListGroup in source.PriceListGroups)
            {
                PricingEverydayPriceListGroup priceListGroupCopy = priceListGroup.Copy();
                copy.PriceListGroups.Add(priceListGroupCopy);
            }

            copy.IsDirty = true;

            return copy;
        }

        /// <summary>
        /// Creates a copy of an <see cref="PricingEverydayValueDriver"/>.
        /// </summary>
        public static PricingEverydayValueDriver Copy(this PricingEverydayValueDriver source)
        {
            var copy = new PricingEverydayValueDriver();

            copy.Id = source.Id;
            copy.Key = source.Key;
            copy.IsSelected = source.IsSelected;
            copy.Name = source.Name;
            copy.Sort = source.Sort;
            copy.Title = source.Title;

            foreach (PricingValueDriverGroup group in source.Groups)
            {
                PricingValueDriverGroup groupCopy = group.Copy();
                copy.Groups.Add(groupCopy);
            }

            return copy;
        }

        public static PricingEverydayPriceListGroup Copy(this PricingEverydayPriceListGroup source)
        {
            var copy = new PricingEverydayPriceListGroup();

            copy.Key = source.Key;
            copy.Name = source.Name;
            copy.Title = source.Title;
            copy.Sort = source.Sort;
            foreach (PricingEverydayPriceList priceList in source.PriceLists)
            {
                PricingEverydayPriceList listCopy = priceList.Copy();
                copy.PriceLists.Add(listCopy);

            }
            return copy;
        }

        public static PricingEverydayPriceList Copy(this PricingEverydayPriceList source)
        {
            var copy = new PricingEverydayPriceList();
            copy.IsKey = source.IsKey;
            copy.Id = source.Id;
            copy.Key = source.Key;
            copy.Code = source.Code;
            copy.Name = source.Name;
            copy.Title = source.Title;
            copy.Sort = source.Sort;
            copy.IsSelected = source.IsSelected;

            return copy;
        }    
 
        public static PricingValueDriverGroup Copy(this PricingValueDriverGroup source)
        {
            var copy = new PricingValueDriverGroup();

            copy.SalesValue = source.SalesValue;
            copy.Id = source.Id;
            copy.Value = source.Value;
            copy.MinOutlier = source.MinOutlier;
            copy.MaxOutlier = source.MaxOutlier;
            copy.Sort = source.Sort;

            return copy;
        }

        public static PriceRoundingRule Copy(this PriceRoundingRule source)
        {
            var copy = new PriceRoundingRule();

            copy.DollarRangeLower = source.DollarRangeLower;
            copy.DollarRangeUpper = source.DollarRangeUpper;
            copy.Type = source.Type;
            copy.ValueChange = source.ValueChange;

            foreach (SQLEnumeration roundingType in source.RoundingTypes)
            {
                SQLEnumeration valueCopy = roundingType.Copy();
                copy.RoundingTypes.Add(valueCopy);
            }

            return copy;
        }

        public static SQLEnumeration Copy(this SQLEnumeration source)
        {
            var copy = new SQLEnumeration();  
          
            copy.Name = source.Name;
            copy.Description = source.Description;
            copy.Value = source.Value;
            copy.Sort = source.Sort;

            return copy;
        }
    }
}
