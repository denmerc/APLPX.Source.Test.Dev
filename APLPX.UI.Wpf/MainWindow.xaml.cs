using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.ViewModels;
using APLPX.UI.WPF.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using Ninject;

namespace APLPX.UI.WPF
{
    /// <summary>
    /// Main window for this application.
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

        public MainWindow()
        {
            InitializeComponent();

            _eventManager = ViewModelBase.Kernel.Get<EventAggregator>();
            //_eventManager = ((EventAggregator)App.Current.Resources["EventManager"]);

            _eventManager.GetEvent<OperationCompletedEvent>().Subscribe(action => ShowMessageToaster(action));
            _eventManager.GetEvent<AboutViewModel>().Subscribe(vm => ShowAboutBox(vm));

            _eventManager.GetEvent<ErrorEvent>()
               .Subscribe( evt =>
               {
                   ShowErrorDialog( evt );
               });

            var themes = MahApps.Metro.ThemeManager.AppThemes;

            var accent = MahApps.Metro.ThemeManager.Accents.First(x => x.Name == "Blue");
            var theme = MahApps.Metro.ThemeManager.AppThemes.First(x => x.Name == "BaseDark");

            MahApps.Metro.ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
        }

        private void ShowMessageToaster(OperationCompletedEvent action)
        {
            toasterPopup.ShowDialogBox(this, action.Message);
        }

        private void ShowAboutBox(AboutViewModel viewModel)
        {
            var aboutBox = new AboutBox();
            aboutBox.DataContext = viewModel;
            aboutBox.ShowDialog(); 
           
            aboutBox = null;
        }




        private async void ShowErrorDialog(ErrorEvent evt)
        {
            var dialog = (BaseMetroDialog)this.Resources["ErrorDialog"];
            dialog.DataContext = evt;

            await this.ShowMetroDialogAsync(dialog);

            await Task.Delay(5000);

            await this.HideMetroDialogAsync(dialog);
        }
    }
}
