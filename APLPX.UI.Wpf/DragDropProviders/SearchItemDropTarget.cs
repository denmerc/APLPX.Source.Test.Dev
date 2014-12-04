using System;
using System.Windows;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.Interfaces;

namespace APLPX.UI.WPF.DragDropProviders
{
    /// <summary>
    /// Drop target implementation for a SearchItem.
    /// Allows a SearchItem display entity to act as a drop target using the DragDropHelper.
    /// </summary>
    public class SearchItemDropTarget : IDropTarget
    {
        private FrameworkElement _targetElement;

        public FrameworkElement TargetElement
        {
            get { return _targetElement; }
            set { _targetElement = value; }
        }

        public bool IsValidDropOperation(IDataObject obj, FrameworkElement target)
        {
            bool isValid = false;

            var targetSearchGroup = target.DataContext as FeatureSearchGroup;

            if (targetSearchGroup.CanNameChange && obj.GetDataPresent(DataFormats.StringFormat, true))
            {
                ISearchableEntity sourceEntity = obj.GetData(DataFormats.StringFormat) as ISearchableEntity;
                if (sourceEntity != null)
                {
                    isValid = (sourceEntity.SearchKey != targetSearchGroup.SearchKey);
                }
            }
            return isValid;
        }

        public void OnDropCompleted(IDataObject dataObject, FrameworkElement target, Point dropPoint)
        {
            if (IsValidDropOperation(dataObject, target))
            {
                var sourceEntity = dataObject.GetData(DataFormats.StringFormat) as ISearchableEntity;
                var destSearchGroup = target.DataContext as FeatureSearchGroup;
                PublishChangeNotification(sourceEntity, destSearchGroup);
            }
        }

        private void PublishChangeNotification(ISearchableEntity sourceEntity, FeatureSearchGroup destSearchGroup)
        {
            EventAggregator notifier = ((EventAggregator)App.Current.Resources["EventManager"]);
            var data = new SearchGroupsUpdatedEvent(sourceEntity, destSearchGroup);
            notifier.Publish(data);
        }

        public UIElement GetVisualFeedback(IDataObject dataObject)
        {
            var sourceEntity = dataObject.GetData(DataFormats.StringFormat) as ISearchableEntity;
            return null;
        }
    }

}
