using System;
using System.Windows;
using APLPX.UI.WPF.ViewModels;

namespace APLPX.UI.WPF.Views
{
    /// <summary>
    /// View for a simple input box.
    /// </summary>
    public partial class InputBox : Window
    {
        public InputBox(InputBoxViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
