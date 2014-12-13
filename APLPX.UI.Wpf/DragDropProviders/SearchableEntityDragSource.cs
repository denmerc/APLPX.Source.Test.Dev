using System;
using System.Windows;
using APLPX.UI.WPF.Interfaces;

namespace APLPX.UI.WPF.DragDropProviders
{
    /// <summary>
    /// Drag source implementation for an ISearchableEntity.
    /// Allows an ISearchableEntity to act as a drag source using the DragDropHelper.
    /// </summary>
    public class SearchableEntityDragSource : IDragSource
    {
        private FrameworkElement _sourceElement;

        public FrameworkElement SourceElement
        {
            get { return _sourceElement; }
            set { _sourceElement = value; }
        }

        public DragDropEffects SupportedEffects
        {
            get { return DragDropEffects.Move; }
        }

        public DataObject GetDataObject(FrameworkElement draggedElement)
        {
            DataObject data = null;

            ISearchableEntity entity = draggedElement.DataContext as ISearchableEntity;
            if (entity != null)
            {
                data = new DataObject(DataFormats.StringFormat, entity);
            }

            return data;
        }

        public UIElement GetVisualFeedback(FrameworkElement draggedElement)
        {
            return draggedElement;
        }

        public void FinishDrag(FrameworkElement draggedElement, DragDropEffects finalEffects)
        {
        }

        public bool IsDraggable(FrameworkElement draggedElement)
        {
            var entity = draggedElement.DataContext as ISearchableEntity;
            bool result = (entity != null && entity.CanSearchKeyChange);

            return result;
        }
    }

}
