using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.Views;
using MahApps.Metro.Controls;
using APLPX.UI.WPF.Themes;

namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Win32 declarations

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        // Make sure RECT is actually OUR defined struct, not the windows rect.
        public static RECT GetWindowRectangle(Window window)
        {
            RECT rect;
            GetWindowRect((new WindowInteropHelper(window)).Handle, out rect);

            return rect;
        }

        #endregion

        private EventAggregator _eventManager;

        ThemeManager manager = new ThemeManager();

        public MainWindow()
        {
            InitializeComponent();
            
            _eventManager = ((EventAggregator)App.Current.Resources["EventManager"]);

            _eventManager.GetEvent<OperationCompletedEvent>()
               .Subscribe(action =>
               {
                   ShowMessageToaster(action);
               });

            // Set the itemsource to the list of theme names.
            themePicker.ItemsSource = manager.ThemeNameList;
            // Set the start item to the default theme.
            themePicker.SelectedItem = "dark";

            
            
        }

        private void ShowMessageToaster(OperationCompletedEvent action)
        {
            double thisWindowTop = this.Top;
            double thisWindowLeft = this.Left;

            if (WindowState == WindowState.Maximized)
            {
                //Ensure this window's coordinates are correct when it is maximized.
                //Reference: https://social.msdn.microsoft.com/Forums/vstudio/en-US/078b8a70-7725-4986-8164-55efccbcfb46/window-top-and-left-values-are-not-updated-correctly-when-maximizing-a-window-in-net-4?forum=wpf
                var rect = GetWindowRectangle(this);
                thisWindowTop = rect.Top;
                thisWindowLeft = rect.Left;
            }

            MessageToaster toaster = new MessageToaster();
            toaster.Left = thisWindowLeft + this.ActualWidth - (ActionGrid.ActualWidth + CommandBar.ActualWidth);
            toaster.Top = thisWindowTop + (this.ActualHeight - toaster.Height) / 2;
            toaster.Message = action.Message;
            toaster.Show();
        }

        private void MessageCenterButton_Click(object sender, RoutedEventArgs e)
        {
            var flyout = this.Flyouts.Items[0] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;

            DetailContentScrollViewer.HorizontalAlignment = HorizontalAlignment.Stretch;
            CommandBar.Width = 400;
        }

        private void Flyout_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void MessageCenter_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void MessageCenter_IsOpenChanged(object sender, EventArgs e)
        {
            var f = sender as Flyout;
            if (f != null && f.IsOpen == false)
                CommandBar.Width = 55;
        }

        private void PropertiesButton_Click(object sender, RoutedEventArgs e)
        {
            var flyout = this.Flyouts.Items[1] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;

            DetailContentScrollViewer.HorizontalAlignment = HorizontalAlignment.Stretch;
            CommandBar.Width = 400;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        private void themePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When they change the selection, just set the theme to the selected value.
            manager.SetTheme(themePicker.SelectedValue.ToString());
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
        }
    }
}
