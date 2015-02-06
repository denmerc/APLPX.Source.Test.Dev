using System;
using System.Collections.Generic;
using System.Linq;
using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Comparers
{
    /// <summary>
    /// Comparer class for <see cref="AnalyticValueDriver"/>s.
    /// </summary>
    public class AnalyticValueDriverComparer : IEqualityComparer<AnalyticValueDriver>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="driver1">The first object to compare.</param>
        /// <param name="driver2">The second object to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(AnalyticValueDriver driver1, AnalyticValueDriver driver2)
        {
            bool areEqual = false;

            if (driver1 == null && driver2 == null)
            {
                areEqual = true;
            }
            else if (driver1 != null && driver2 != null)
            {
                if (driver1.SelectedMode.Key == driver2.SelectedMode.Key &&
                    driver1.SelectedMode.Groups.Count == driver2.SelectedMode.Groups.Count)
                {
                    foreach (AnalyticValueDriverGroup group1 in driver1.SelectedMode.Groups)
                    {
                        var group2 = driver2.SelectedMode.Groups.SingleOrDefault(grp => grp.Value == group1.Value);
                        if (group2 != null)
                        {
                            areEqual = (group2.MinOutlier == group1.MinOutlier &&
                                        group2.MaxOutlier == group1.MaxOutlier);
                        }
                        if (!areEqual)
                        {
                            break;
                        }
                    }
                }
            }

            return areEqual;
        }

        public int GetHashCode(AnalyticValueDriver obj)
        {
            int result = 0;

            if (obj != null)
            {
                result = obj.Id.GetHashCode();
            }

            return result;
        }
    }
}
