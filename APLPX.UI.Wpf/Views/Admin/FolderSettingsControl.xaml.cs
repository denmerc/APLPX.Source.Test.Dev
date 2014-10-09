using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace APLPX.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for FolderSettingsControl.xaml
    /// </summary>
    public partial class FolderSettingsControl : UserControl
    {
        APLPX.UI.WPF.ViewModels.Reactive.EventAggregator Publisher = ((APLPX.UI.WPF.ViewModels.Reactive.EventAggregator)App.Current.Resources["EventManager"]);
        public FolderSettingsControl()
        {
            InitializeComponent();
        }

        private void SubModuleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Publisher.Publish<FolderModuleSelectedEvent>(new FolderModuleSelectedEvent { SelectedIndex = SubModuleListBox.SelectedIndex });
                   
        }


        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        
    }

    public class FolderModuleSelectedEvent
    {
        public int SelectedIndex { get; set; }
    }
}
