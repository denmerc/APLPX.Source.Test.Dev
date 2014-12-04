using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    /// Extension methods for Price Routines
    /// </summary>
    public static class PricingExtensions
    {
        /// <summary>
        /// Creates a copy of an <see cref="Pricing"/> object.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Pricing Copy(this Pricing source)
        {
            Pricing copy = new Pricing();
            copy.Id = source.Id;
            copy.SearchKey = source.SearchKey;

            DateTime createdDate = DateTime.Now;
            copy.Identity.Created = createdDate;
            copy.Identity.Edited = createdDate;
            copy.Identity.Refreshed = createdDate;
            copy.Identity.Active = source.Identity.Active;
            copy.Identity.Author = source.Identity.Author;

            string copySuffix = " (Copy)";
            copy.Identity.Description = source.Identity.Description + copySuffix;
            copy.Identity.Editor = source.Identity.Editor;
            copy.Identity.Name = source.Identity.Name + copySuffix;
            copy.Identity.Notes = source.Identity.Notes + copySuffix;
            copy.Identity.Owner = source.Identity.Owner;
            copy.Identity.Shared = source.Identity.Shared;

            foreach (FilterGroup filterGroup in source.FilterGroups)
            {
                var filterGroupCopy = filterGroup.Copy();
                copy.FilterGroups.Add(filterGroupCopy);
            }

            foreach (PricingDriver driver in source.Drivers)
            {
                var driverCopy = driver.Copy();
                copy.Drivers.Add(driverCopy);
            }

            foreach (AnalyticPriceListGroup priceListGroup in source.PriceListGroups)
            {
                var priceListGroupCopy = priceListGroup.Copy();
                copy.PriceListGroups.Add(priceListGroupCopy);
            }

            copy.IsDirty = true;

            return copy;
        }

        /// <summary>
        /// Creates a copy of an <see cref="AnalyticValueDriver"/>.
        /// </summary>
        public static PricingDriver Copy(this PricingDriver source)
        {
            var copy = new PricingDriver();

            copy.Id = source.Id;            
            copy.Key = source.Key;
            copy.IsKeyDriver = source.IsKeyDriver;
            copy.Tooltip = source.Tooltip;
            copy.Name = source.Name;
            copy.SkuCount = source.SkuCount;

            return copy;
        }
    }
}
