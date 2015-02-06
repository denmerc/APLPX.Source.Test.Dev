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
        public static List<Error> CheckIsValid(this IEnumerable<FilterGroup> filterGroups)
        {
            List<Error> errors = new List<Error>();

            foreach (FilterGroup group in filterGroups)
            {
                if (group.SelectedCount == 0)
                {
                    string message = String.Format("{0} filter: At least one item must be selected.", group.Name);
                    errors.Add(new Error { Message = message });
                }
            }

            return errors;
        }


    }
}
