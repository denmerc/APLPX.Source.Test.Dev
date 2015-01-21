using System;
using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Interfaces
{
    /// <summary>
    /// Interface for searching and filtering entities in a standardized way.
    /// Examples: Analytics, Price Routines, Users.
    /// </summary>
    public interface ISearchableEntity
    {
        FeatureSearchGroup SearchGroup { get; set; }

        /// <summary>
        /// Gets/sets the unique ID of the fully populated entity containing this search item.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets/sets the search ID of the this item.
        /// </summary>
        int SearchGroupId { get; set; }

        /// <summary>
        /// Gets/sets the Id of the unique Search Group to which this entity belongs.
        /// Explanation: Although an entity can "appear" in several search groups, such as Recent or Shared,
        /// these are auxiliary groupings for display only. 
        /// The entity is actually assigned to only a single ("owning") Search Group.
        /// </summary>
        int OwningSearchGroupId { get; set; }

        /// <summary>
        /// Gets/sets the search key of this item.
        /// </summary>
        string SearchGroupKey { get; set; }

        /// <summary>
        /// Gets the type name of the concrete entity.
        /// </summary>
        string EntityTypeName { get; }

        /// <summary>
        /// Gets a value indicating whether the entity name can be changed by the user.
        /// </summary>
        bool CanNameChange { get; set; }

        /// <summary>
        /// Gets a value indicating whether the entity search key can be changed by the user.
        /// </summary>
        bool CanSearchKeyChange { get; set; }
    }
}
