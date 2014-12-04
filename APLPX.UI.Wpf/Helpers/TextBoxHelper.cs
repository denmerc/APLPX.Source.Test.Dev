using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace APLPX.UI.WPF.Helpers
{
    /// <summary>
    /// Defines attached properties to extend the functionality of a textbox .
    /// </summary>    
    public class TextBoxHelper : DependencyObject
    {
        /// <summary>
        /// Gets/sets whether to select all text in a textbox when it gets focus.
        /// Example: 
        /// xmlns:helpers="clr-namespace:APLPX.UI.WPF.Helpers"
        ///<Style TargetType="TextBox">
        ///     <Setter Property="helpers:TextBoxHelper.SelectAllTextOnFocus" Value="True"/>
        ///</Style
        /// </summary>
        public static readonly DependencyProperty SelectAllTextOnFocusProperty = DependencyProperty.RegisterAttached(
            "SelectAllTextOnFocus", typeof(bool), typeof(TextBoxHelper), new PropertyMetadata(false, SelectAllTextOnFocusPropertyChanged));

        //[AttachedPropertyBrowsableForChildrenAttribute(IncludeDescendants = false)]
        //[AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetSelectAllTextOnFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectAllTextOnFocusProperty);
        }

        public static void SetSelectAllTextOnFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectAllTextOnFocusProperty, value);
        }

        private static void SelectAllTextOnFocusPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = source as TextBox;
            if (textBox != null)
            {
                if ((e.NewValue as bool?).GetValueOrDefault(false))
                {
                    textBox.GotKeyboardFocus += OnKeyboardFocusSelectText;
                    textBox.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
                }
                else
                {
                    textBox.GotKeyboardFocus -= OnKeyboardFocusSelectText;
                    textBox.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
                }
            }
        }

        private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dependencyObject = GetParentFromVisualTree(e.OriginalSource);

            TextBox textBox = dependencyObject as TextBox;
            if (textBox != null && !textBox.IsKeyboardFocusWithin)
            {
                textBox.Focus();
                e.Handled = true;
            }
        }

        private static void OnKeyboardFocusSelectText(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox textBox = e.OriginalSource as TextBox;
            if (textBox != null)
            {
                textBox.SelectAll();
                e.Handled = true;
            }            
        }

        private static DependencyObject GetParentFromVisualTree(object source)
        {
            DependencyObject parent = source as UIElement;
            while (parent != null && !(parent is TextBox))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent;
        }
    }

}
