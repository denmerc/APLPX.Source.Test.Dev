using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace APLPX.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for ToasterPopup.xaml
    /// </summary>
    public partial class MessageToaster : UserControl
    {
        Window ParentWindow { get; set; }
        public MessageToaster()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        public void ShowDialogBox(Window parentWindow, string message)
        {
            ParentWindow = parentWindow;
            popupLabel.Content = message;
            Storyboard StatusFader = (Storyboard)Resources["StatusFader"];
            ParentWindow.IsEnabled = true;
            this.Height = 150;
            this.Width = 250;
            popup.Height = 150;
            popup.Width = 250;
            popup.IsOpen = true;
            StatusFader.Begin(popupBackground);
        }

        void StatusFader_Completed(object sender, EventArgs e)
        {
            popup.IsOpen = false;
            ParentWindow.IsEnabled = true;
        }
    }
}
