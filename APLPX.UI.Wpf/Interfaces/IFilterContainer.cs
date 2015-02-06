using System;
using System.Collections.Generic;
using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;

namespace APLPX.UI.WPF.Interfaces
{
    /// <summary>
    /// Interface for entities that contain filters and filter groups.
    /// </summary>
    public interface IFilterContainer
    {
        /// <summary>
        /// Gets/sets the list of <see cref="FilterGroup"/>s.
        /// </summary>
        ReactiveList<FilterGroup> FilterGroups { get; set; }

        /// <summary>
        /// Gets/sets the currently selected <see cref="FilterGroup"/>.
        /// </summary>
        FilterGroup SelectedFilterGroup { get; set; }
    }
}
