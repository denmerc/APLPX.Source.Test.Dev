using System;
using System.Windows;
using System.Windows.Input;

using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.Interfaces;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    /// Helper class for implementing Drag Drop operations using attached properties instead of code-behind.
    /// </summary>
    /// <remarks>To use:
    /// 1. Implement IDragSource for the object that will represent the dragged data.
    /// 2. Implement IDropTarget for the object that will represent the destination for the dragged data.
    /// 3. In the XAML of the control hosting the drag drop, define resources for the IDragSource and IDropTarget concrete classes defined above.
    /// 4. Assign the DragSource attached property to the FrameworkElement that will provide the data to be dragged.
    /// 5. Assign the DropTarget attached property to the FrameworkElement that will receive the dragged data.        
    /// </remarks>
    public class DragDropHelper : DependencyObject
    {
        #region Drag Source attached property

        public static readonly DependencyProperty DragSourceProperty =
            DependencyProperty.RegisterAttached("DragSource", typeof(IDragSource),
            typeof(DragDropHelper), new PropertyMetadata(DragSourcePropertyChanged));

        public static IDragSource GetDragSource(DependencyObject obj)
        {
            return (IDragSource)obj.GetValue(DragSourceProperty);
        }

        public static void SetDragSource(DependencyObject obj, IDragSource value)
        {
            obj.SetValue(DragSourceProperty, value);
        }

        private static void DragSourcePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement sourceElement = source as FrameworkElement;
            if (e.NewValue != null && e.OldValue == null)
            {
                sourceElement.MouseMove += sourceElement_MouseMove;
                IDragSource dragSource = e.NewValue as IDragSource;
                dragSource.SourceElement = sourceElement;
            }
            else if (e.NewValue == null && e.OldValue != null)
            {
                sourceElement.MouseMove -= sourceElement_MouseMove;
            }
        }

        #endregion

        #region Drop Target attached property

        public static readonly DependencyProperty DropTargetProperty =
            DependencyProperty.RegisterAttached("DropTarget", typeof(IDropTarget),
            typeof(DragDropHelper), new PropertyMetadata(DropTargetPropertyChanged));

        public static IDropTarget GetDropTarget(DependencyObject obj)
        {
            return (IDropTarget)obj.GetValue(DropTargetProperty);
        }

        public static void SetDropTarget(DependencyObject obj, IDropTarget value)
        {
            obj.SetValue(DropTargetProperty, value);
        }

        private static void DropTargetPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement targetElement = source as FrameworkElement;
            if (e.NewValue != null && e.OldValue == null)
            {
                targetElement.AllowDrop = true;
                targetElement.DragEnter += targetElement_DragEnter;
                targetElement.DragLeave += targetElement_DragLeave;
                targetElement.DragOver += targetElement_DragOver;
                targetElement.GiveFeedback += targetElement_GiveFeedback;
                targetElement.Drop += targetElement_Drop;

                IDropTarget dropTarget = e.NewValue as IDropTarget;
                dropTarget.TargetElement = targetElement;
            }
            else if (e.NewValue == null && e.OldValue != null)
            {
                targetElement.AllowDrop = false;
                targetElement.DragEnter -= targetElement_DragEnter;
                targetElement.DragLeave -= targetElement_DragLeave;
                targetElement.DragOver -= targetElement_DragOver;
                targetElement.GiveFeedback -= targetElement_GiveFeedback;
                targetElement.Drop -= targetElement_Drop;
            }
        }

        #endregion

        #region Drag Drop Event Handlers

        private static void sourceElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IDragSource dragSource = DragDropHelper.GetDragSource(sender as DependencyObject);
                FrameworkElement sourceElement = sender as FrameworkElement; //dragSource.SourceElement;
                var item = sourceElement as System.Windows.Controls.ListBoxItem;

                if (dragSource.IsDraggable(sourceElement))
                {
                    DataObject data = dragSource.GetDataObject(sourceElement);
                    DragDrop.DoDragDrop(sourceElement, data, dragSource.SupportedEffects);
                }
            }
        }

        private static void targetElement_DragEnter(object sender, DragEventArgs e)
        {
        }

        private static void targetElement_DragLeave(object sender, DragEventArgs e)
        {
        }

        private static void targetElement_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;

            var destElement = sender as FrameworkElement;
            IDropTarget dropTarget = DragDropHelper.GetDropTarget(destElement);

            if (dropTarget.IsValidDropOperation(e.Data, destElement))
            {
                e.Effects = DragDropEffects.Move;
            }
            e.Handled = true;
        }

        private static void targetElement_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
        }

        private static void targetElement_Drop(object sender, DragEventArgs e)
        {
            FrameworkElement targetElement = sender as FrameworkElement;
            IDropTarget dropTarget = DragDropHelper.GetDropTarget(targetElement);

            if (e.Data.GetDataPresent(DataFormats.StringFormat, true) && targetElement != null)
            {
                Point dropPosition = e.GetPosition(targetElement);
                dropTarget.OnDropCompleted(e.Data, targetElement, dropPosition);
            }
        }

        #endregion
    }

}

