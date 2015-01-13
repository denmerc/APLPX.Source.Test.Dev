using System;
using System.Collections.Generic;
using System.Linq;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    ///  Extension methods for working with display entities.
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// Gets a three-state nullable Boolean value indicating whether all, none, or some of the items in a collection meet the specified condition.  
        /// The result is suitable for binding to a three-state checkbox, etc.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The collection to search.</param>
        /// <param name="condition">A predicate that expresses the condition.</param>
        /// <returns>   
        /// True: All items meet the condition; 
        /// False: No items meet the condition; 
        /// null: At least one, but not all, items meet the condition.</returns>
        public static bool? AreAllItemsIncluded<T>(this IEnumerable<T> items, Predicate<T> condition)
        {
            bool? result = false;

            if (items.Count() > 0)
            {
                if (items.All(item => condition(item)))
                {
                    result = true;
                }
                else if (items.Any(item => condition(item)))
                {
                    result = null;
                }
            }

            return result;
        }
    }
}
