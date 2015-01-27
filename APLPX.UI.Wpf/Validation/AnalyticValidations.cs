using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Validation
{
    /// <summary>
    /// Extension methods for validating Analytic-related display entities.
    /// </summary>
    public static class AnalyticValidations
    {
        /// <summary>
        /// Validates a collection of <see cref="AnalyticPriceListGroup"/>s.
        /// </summary>        
        /// <returns>A list of <see cref="Error"/> objects populated with messages for each invalid item.</returns>
        public static List<Error> Validate(this IEnumerable<AnalyticPriceListGroup> priceListGroups)
        {
            var errors = new List<Error>();

            foreach (AnalyticPriceListGroup group in priceListGroups)
            {
                if (group.SelectedCount == 0)
                {
                    string message = String.Format("{0} Price List: At least one item must be selected.", group.Name);
                    errors.Add(new Error { Message = message });
                }
            }
            return errors;
        }

        /// <summary>
        /// Validates a collection of <see cref="AnalyticValueDriver"/>s.
        /// </summary>        
        /// <returns>A list of <see cref="Error"/> objects populated with messages for each invalid item.</returns>
        public static List<Error> Validate(this IEnumerable<AnalyticValueDriver> drivers)
        {
            var errors = new List<Error>();

            var selectedDrivers = drivers.Where(driver => driver.IsSelected);
            if (drivers.Count() > 0 && selectedDrivers.Count() == 0)
            {
                errors.Add(new Error { Message = "At least one Value Driver must be selected." });
            }
            else
            {
                foreach (AnalyticValueDriver driver in selectedDrivers)
                {
                    if (driver.Modes.Where(m => m.IsSelected).Count() == 0)
                    {
                        string message = String.Format("\"{0}\" Value Driver: Please specify Auto- or user-generated.", driver.Name);
                        errors.Add(new Error { Message = message });
                    }
                }
            }
            return errors;
        }

    }
}
