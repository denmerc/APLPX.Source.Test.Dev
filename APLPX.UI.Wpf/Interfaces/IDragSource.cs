using System;
using System.Windows;

namespace APLPX.UI.WPF.Interfaces
{
    public interface IDragSource
    {
        FrameworkElement SourceElement { get; set; }

        DragDropEffects SupportedEffects { get; }

        bool IsDraggable(FrameworkElement draggedElement);

        DataObject GetDataObject(FrameworkElement draggedElement);

        //UIElement GetVisualFeedback(FrameworkElement draggedElement);

        void FinishDrag(FrameworkElement draggedElement, DragDropEffects finalEffects);

        
    }

}
