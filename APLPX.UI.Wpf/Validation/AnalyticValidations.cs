using System;
using System.Collections.Generic;
using System.Linq;
using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Validation
{
    /// <summary>
    /// Extension methods for validating Analytic-related display entities.
    /// </summary>
    public static class AnalyticValidations
    {

        /// <summary>
        /// Validates an <see cref="AnalyticIdentity"/>.
        /// </summary>        
        /// <returns>A list of <see cref="Error"/> objects populated with messages for each invalid item.</returns>
        public static List<Error> GetAllValidationErrors(this AnalyticIdentity identity)
        {
            var errors = new List<Error>();
       
            return errors;
        }

        /// <summary>
        /// Validates a collection of <see cref="AnalyticPriceListGroup"/>s.
        /// </summary>        
        /// <returns>A list of <see cref="Error"/> objects populated with messages for each invalid item.</returns>
        public static List<Error> GetAllValidationErrors(this IEnumerable<AnalyticPriceListGroup> priceListGroups)
        {
            var errorList = new List<Error>();

            foreach (AnalyticPriceListGroup group in priceListGroups)
            {
                var errors = group.GetValidationErrors();
                errorList.AddRange(errors);
            }

            return errorList;
        }
        
        /// <summary>
        /// Gets a value indicating whether every item in a collection of <see cref="AnalyticPriceListGroup"/>s is valid. 
        /// </summary>
        /// <returns>true if all items are valid; otherwise, false.</returns>
        public static bool AreAllItemsValid(this IEnumerable<AnalyticPriceListGroup> priceListGroups)
        {
            bool areAllValid = true;

            foreach (AnalyticPriceListGroup group in priceListGroups)
            {
                if (!group.Validate())
                {
                    areAllValid = false;
                    break;
                }
            }
            return areAllValid;
        }

        /// <summary>
        /// Validates a collection of <see cref="AnalyticValueDriver"/>s.
        /// </summary>        
        /// <returns>A list of <see cref="Error"/> objects populated with messages for each invalid item.</returns>
        public static List<Error> GetAllValidationErrors(this IEnumerable<AnalyticValueDriver> drivers)
        {
            var errorList = new List<Error>();

            var selectedDrivers = drivers.Where(driver => driver.IsSelected);
            if (drivers.Count() > 0 && selectedDrivers.Count() == 0)
            {
                errorList.Add(new Error { Message = "At least one Value Driver must be selected." });
            }
            else
            {
                foreach (AnalyticValueDriver driver in selectedDrivers)
                {
                    var errors = driver.GetValidationErrors();
                    errorList.AddRange(errors);
                }
            }
            return errorList;
        }

        /// <summary>
        /// Gets a value indicating whether every item in a collection of <see cref="AnalyticValueDriver"/>s is valid. 
        /// </summary>  
        /// <returns></returns>
        public static bool AreAllItemsValid(this IEnumerable<AnalyticValueDriver> valueDrivers)
        {
            bool areAllValid = true;

            foreach (AnalyticValueDriver group in valueDrivers)
            {
                if (!group.Validate())
                {
                    areAllValid = false;
                    break;
                }
            }
            return areAllValid;
        }

    }
}
