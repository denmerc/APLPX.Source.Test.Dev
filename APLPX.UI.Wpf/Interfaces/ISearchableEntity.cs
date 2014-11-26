using System;

namespace APLPX.UI.WPF.Interfaces
{
    /// <summary>
    /// Interface for searching and filtering entities in a standardized way.
    /// Examples: Analytics, Price Routines, Users.
    /// </summary>
    public interface ISearchableEntity
    {
        /// <summary>
        /// Gets/sets the unique ID of the fully populated entity containing this search item.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets/sets the search key of this item.
        /// </summary>
        string SearchKey { get; set; }

        /// <summary>
        /// Gets the type name of the concrete entity.
        /// </summary>
        string EntityTypeName { get; }
    }
}
