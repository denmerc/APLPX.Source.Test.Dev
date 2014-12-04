using System;
using System.Windows;

namespace APLPX.UI.WPF.Interfaces
{
    public interface IDropTarget
    {
        FrameworkElement TargetElement { get; set; }

        bool IsValidDropOperation(IDataObject sourceObject, FrameworkElement target);

        UIElement GetVisualFeedback(IDataObject obj);

        void OnDropCompleted(IDataObject dataObject, FrameworkElement target, Point dropPoint);
    }

}
