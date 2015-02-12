using System;
using System.Collections.Generic;
using System.Linq;
using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Validation
{
    /// <summary>
    /// Extension methods for validating common display entities, such as Filters, etc..
    /// </summary>
    public static class CommonValidations
    {
        /// <summary>
        /// Validates a collection of <see cref="FilterGroup"/>s.
        /// </summary>
        /// <returns>A list of <see cref="Error"/> objects populated with messages for each invalid item.</returns>        
        public static List<Error> GetAllValidationErrors(this IEnumerable<FilterGroup> filterGroups)
        {
            List<Error> errorList = new List<Error>();

            foreach (FilterGroup group in filterGroups)
            {
                var errors = group.GetValidationErrors();
                errorList.AddRange(errors);             
            }

            return errorList;
        }


        /// <summary>
        /// Gets a value indicating whether every item in a collection of <see cref="FilterGroup"/>s is valid. 
        /// </summary>
        /// <returns>true if all items are valid; otherwise, false.</returns>
        public static bool AreAllItemsValid(this IEnumerable<FilterGroup> filterGroups)
        {
            bool areAllValid = true;

            foreach (FilterGroup group in filterGroups)
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
