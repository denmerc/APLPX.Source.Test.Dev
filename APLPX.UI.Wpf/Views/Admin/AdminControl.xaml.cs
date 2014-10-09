using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Linq;
using APLPX.UI.WPF.ViewModels.Events;
using APLPX.UI.WPF.ViewModels;
using APLPX.UI.WPF.ViewModels.Reactive;
using Domain = APLPX.Client.Entity;

namespace APLPX.UI.WPF
{
	/// <summary>
	/// Interaction logic for HomeControl.xaml
	/// </summary>
	public partial class AdminControl : UserControl
	{
        EventAggregator Publisher = ((EventAggregator)App.Current.Resources["EventManager"]);
        public AdminControl()
		{
			this.InitializeComponent();
		}

        private void UserListItem_Selected(object sender, RoutedEventArgs e)
        {
            //UserListStack.Visibility = Visibility.Visible;
            //FilterStack.Visibility = Visibility.Collapsed;
        }

        private void UserListItem_Selected(object sender, SelectionChangedEventArgs e)
        {
            //RoleStack.Visibility = Visibility.Visible;
            //MarginStackPanel.Visibility = Visibility.Visible;
            //ProfileStack.Visibility = Visibility.Visible;
            //FilterStack.Visibility = Visibility.Collapsed;
        }

        private void RulesItem_Selected(object sender, RoutedEventArgs e)
        {
            //UserListStack.Visibility = Visibility.Collapsed;
            //FilterStack.Visibility = Visibility.Visible;
            //ProfileStack.Visibility = Visibility.Hidden;
            //RoleStack.Visibility = Visibility.Hidden;
            //MarginStackPanel.Visibility = Visibility.Hidden;
            //FilterList.ItemsSource = new[] { new { Name = "Optimization" }, new { Name = "Rounding" } }.ToList();
        }

        private void SectionListBox_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void SectionListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Domain.SectionType section = Domain.SectionType.Null;
            switch (SectionListBox.SelectedIndex)
            {
                case 7:
                    section = Domain.SectionType.AdministrationFolders;
                    MarginStackPanel.Visibility = Visibility.Visible;
                    AdminContentControl.Visibility = Visibility.Visible;
                    break;
                default:
                    MarginStackPanel.Visibility = Visibility.Collapsed;
                    AdminContentControl.Visibility = Visibility.Collapsed;
                    break;
            }

            Publisher.Publish<SectionSelectionEvent>(
                new SectionSelectionEvent
                {
                    Section = section

                });
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Publisher.Publish<FolderSettingsViewModelEvent>((FolderSettingsViewModel)((AdminViewModel)this.DataContext).SelectedSectionViewModel);


            //Publisher.Publish<FolderSettingsViewModelEvent>(new FolderSettingsViewModelEvent{vm=(FolderSettingsViewModel)((AdminViewModel)this.DataContext).SelectedSectionViewModel});
            Publisher.Publish<FolderSettingsUpdatedEvent>(new FolderSettingsUpdatedEvent());


        }
	}

    public class FolderSettingsUpdatedEvent
    {
        //public FolderSettingsViewModel vm { get; set; }
    }
}