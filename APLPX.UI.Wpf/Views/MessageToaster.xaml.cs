using System;
using System.Windows;
using MahApps.Metro;

namespace APLPX.UI.WPF.Views
{
    /// <summary>
    /// Toaster style window for displaying messages.
    /// </summary>
    public partial class MessageToaster : Window
    {
        public MessageToaster()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Gets/sets the message to be displayed in this window.
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageToaster), new PropertyMetadata(null));
  

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            Close();            
        }   

    }
}
