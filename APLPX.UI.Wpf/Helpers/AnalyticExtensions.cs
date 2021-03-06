﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Interfaces;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    /// Extension methods for working with Analytic-related display entities.
    /// </summary>
    public static class AnalyticExtensions
    {
        /// <summary>
        /// Creates a shallow copy of an <see cref="Analytic"/> to be used as a payload container.
        /// </summary>
        /// <param name="source">The original analytic</param>
        /// <returns>The analytic with only the payload-relevant properties populated.</returns>
        public static Analytic ToPayload(this Analytic source)
        {
            var payload = new Analytic { Id = source.Id, SearchGroupId = source.SearchGroupId };

            return payload;
        }

        /// <summary>
        /// Creates a deep copy of an <see cref="Analytic"/>.
        /// </summary> 
        public static Analytic Copy(this Analytic source)
        {
            var copy = new Analytic();

            copy.SearchGroupKey = source.SearchGroupKey;

            DateTime createdDate = DateTime.Now;
            copy.Identity.Created = createdDate;
            copy.Identity.Edited = createdDate;
            copy.Identity.Refreshed = createdDate;
            copy.Identity.IsActive = source.Identity.IsActive;
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
                FilterGroup filterGroupCopy = filterGroup.Copy();
                copy.FilterGroups.Add(filterGroupCopy);
            }

            foreach (AnalyticValueDriver driver in source.ValueDrivers)
            {
                AnalyticValueDriver driverCopy = driver.Copy();
                copy.ValueDrivers.Add(driverCopy);
            }

            foreach (AnalyticPriceListGroup item in source.PriceListGroups)
            {
                AnalyticPriceListGroup priceListGroupCopy = item.Copy();
                copy.PriceListGroups.Add(priceListGroupCopy);
            }

            copy.IsDirty = true;

            return copy;
        }


        /// <summary>
        /// Creates a copy of an <see cref="AnalyticValueDriver"/>.
        /// </summary>
        public static AnalyticValueDriver Copy(this AnalyticValueDriver source)
        {
            var copy = new AnalyticValueDriver();

            copy.Id = source.Id;
            copy.IsSelected = source.IsSelected;
            copy.Key = source.Key;
            copy.Name = source.Name;
            copy.Sort = source.Sort;

            foreach (AnalyticValueDriverMode mode in source.Modes)
            {
                copy.Modes.Add(mode.Copy());
            }
            if (source.SelectedMode != null)
            {
                copy.SelectedMode = copy.Modes.SingleOrDefault(m => m.Key == source.SelectedMode.Key);
            }

            return copy;
        }

        /// <summary>
        /// Creates a copy of an <see cref="AnalyticValueDriverMode"/>.
        /// </summary>      
        public static AnalyticValueDriverMode Copy(this AnalyticValueDriverMode source)
        {
            var copy = new AnalyticValueDriverMode();

            copy.Key = source.Key;
            copy.Name = source.Name;
            copy.Title = source.Title;
            copy.Sort = source.Sort;
            copy.IsSelected = source.IsSelected;

            foreach (AnalyticValueDriverGroup driverGroup in source.Groups)
            {
                AnalyticValueDriverGroup driverGroupCopy = driverGroup.Copy();
                copy.Groups.Add(driverGroupCopy);
            }

            return copy;
        }

        /// <summary>
        ///  Creates a copy of an <see cref="AnalyticValueDriverGroup"/>.
        /// </summary>    
        public static AnalyticValueDriverGroup Copy(this AnalyticValueDriverGroup source)
        {
            var copy = new AnalyticValueDriverGroup();

            copy.Id = source.Id;
            copy.Value = source.Value;
            copy.MinOutlier = source.MinOutlier;
            copy.MaxOutlier = source.MaxOutlier;
            copy.Sort = source.Sort;

            return copy;
        }

        /// <summary>
        /// Creates a copy of a <see cref="FilterGroup"/>.
        /// </summary>    
        public static FilterGroup Copy(this FilterGroup source)
        {
            var copy = new FilterGroup();

            copy.Sort = source.Sort;
            copy.Name = source.Name;

            foreach (Filter filter in source.Filters)
            {
                Filter filterCopy = filter.Copy();
                copy.Filters.Add(filterCopy);
            }

            return copy;
        }

        /// <summary>
        /// Creates a copy of a <see cref="Filter"/>.
        /// </summary>
        public static Filter Copy(this Filter source)
        {
            var copy = new Filter();

            copy.Code = source.Code;
            copy.Id = source.Id;
            copy.IsSelected = source.IsSelected;
            copy.Key = source.Key;
            copy.Name = source.Name;
            copy.Sort = source.Sort;

            return copy;
        }

        /// <summary>
        /// Creates a copy of a <see cref="AnalyticPriceListGroup"/>.
        /// </summary>
        public static AnalyticPriceListGroup Copy(this AnalyticPriceListGroup source)
        {
            var copy = new AnalyticPriceListGroup();

            copy.Key = source.Key;
            copy.Name = source.Name;
            copy.Title = source.Title;
            copy.Sort = source.Sort;

            foreach (PriceList priceList in source.PriceLists)
            {
                PriceList priceListCopy = priceList.Copy();
                copy.PriceLists.Add(priceListCopy);
            }

            return copy;
        }

        /// <summary>
        /// Creates a copy of a <see cref="PriceList"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static PriceList Copy(this PriceList source)
        {
            var copy = new PriceList();

            copy.Code = source.Code;
            copy.Id = source.Id;
            copy.IsSelected = source.IsSelected;
            copy.Key = source.Key;
            copy.Name = source.Name;
            copy.Sort = source.Sort;

            return copy;
        }

        /// <summary>
        /// Clears the dirty flag for a collection of <see cref="AnalyticValueDriver"/>s.
        /// </summary>  
        public static void ClearIsDirty(this IEnumerable<AnalyticValueDriver> drivers)
        {
            foreach (AnalyticValueDriver driver in drivers.Where(drv => drv.IsDirty))
            {
                foreach (AnalyticValueDriverMode mode in driver.Modes.Where(mode => mode.IsDirty))
                {
                    foreach (AnalyticValueDriverGroup driverGroup in mode.Groups.Where(grp=>grp.IsDirty))
                    {
                        driverGroup.IsDirty = false;
                    }
                    mode.IsDirty = false;
                }
                driver.IsDirty = false;
            }
        }

        public static string Dump(this IEnumerable<Analytic> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Id|SearchKey|Name|Owner");

            foreach (Analytic item in list)
            {
                sb.AppendFormat("{0}|{1}|{2}|{3}\n", item.Id, item.SearchGroupKey, item.Identity.Name, item.Identity.Owner);
            }

            string result = sb.ToString();
            return result;
        }

        public static string DumpAnalyticSearchEntities(this ModuleFeature feature)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sort|ParentName|SearchGroupName|ItemCount|SearchKey|Id|Name|Owner\n");

            var parentGroups = feature.SearchGroups.GroupBy(i => i.ParentName);
            foreach (var grouping in parentGroups)
            {
                foreach (FeatureSearchGroup searchGroup in grouping)
                {
                    var matchingEntities = feature.SearchableEntities.Where(item => item.SearchGroupKey == searchGroup.SearchGroupKey);
                    foreach (ISearchableEntity entity in matchingEntities)
                    {
                        Analytic analytic = entity as Analytic;
                        object[] values = { searchGroup.Sort, 
                                            grouping.Key, 
                                            searchGroup.Name, 
                                            searchGroup.ItemCount, 
                                            searchGroup.SearchGroupKey, 
                                            entity.Id, 
                                            analytic.Identity.Name, 
                                            analytic.Identity.Owner };

                        sb.AppendFormat("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}\n", values);
                    }
                }
            }

            string result = sb.ToString();
            return result;
        }
    }
}
