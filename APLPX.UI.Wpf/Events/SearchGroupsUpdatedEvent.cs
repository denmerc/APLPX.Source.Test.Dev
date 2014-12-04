using System;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Interfaces;

namespace APLPX.UI.WPF.Events
{
    /// <summary>
    /// Notifies subscribers that an entity has been assigned to a different search group.
    /// </summary>
    public class SearchGroupsUpdatedEvent
    {
        public ISearchableEntity SourceEntity { get; private set; }
        public FeatureSearchGroup DestinationSearchGroup { get; private set; }

        public SearchGroupsUpdatedEvent(ISearchableEntity sourceEntity, FeatureSearchGroup destinationSearchGroup)
        {
            SourceEntity = sourceEntity;
            DestinationSearchGroup = destinationSearchGroup;
        }
    
    }
}
