using APLPX.UI.WPF.ViewModels.Events;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using APLPX.UI.WPF.ViewModels.Reactive;
using Domain = APLPX.Client.Display;

namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        EventAggregator Publisher = ((EventAggregator)App.Current.Resources["EventManager"]);
        ObservableCollection<UserControl> views;
        public MainWindow()
        {
            InitializeComponent();
            //views = new ObservableCollection<UserControl>();
            //var h = new HomeVerticalControl();
            
            //views.Add(new HomeVerticalControl());
            //views.Add(new AnalyticStepsControl());

            //DataContext = views;



           // ModuleControl.Content = views[0]; //selected module
            
        }

        private void MessageCenterButton_Click(object sender, RoutedEventArgs e)
        {
            var flyout = this.Flyouts.Items[0] as Flyout;
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;

            DetailContentScrollViewer.HorizontalAlignment=HorizontalAlignment.Stretch;
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
            if(f != null && f.IsOpen==false)
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

        private void AddNewAnalyticButton_Click(object sender, RoutedEventArgs e)
        {
            ModuleControl.Content = views[1]; //go to analytic steps
        }

        private void PlanningModuleButton_Click(object sender, RoutedEventArgs e)
        {
            //HomeVerticalControl h = new HomeVerticalControl();
            //h.ModuleListBox.SelectedItem = h.ModuleListBox.Items[0];
            //h.FilterListBox.Visibility = Visibility.Visible;
            //ModuleControl.Content = h;

            //Publish SectionType & MVM changes selected sudmodule
            Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Search,
                        Section = Domain.SectionType.PlanningHomeMyHomePage
                    }
                
                );
            PlanningModuleTitle.Foreground = Brushes.White;
            AdminModuleTitle.Foreground = Brushes.Black;

        }

        private void RelatedPriceRoutineButton_Click(object sender, RoutedEventArgs e)
        {
            PricingStepsControl view = new PricingStepsControl();
            ModuleControl.Content = view;
        }

        private void AdministrationModuleButton_Click(object sender, RoutedEventArgs e)
        {

            Publisher.Publish<NavigateEvent>(
                new NavigateEvent
                {
                    Module = Domain.ModuleType.Administration,
                    SubModule = Domain.SubModuleType.AdminDefault
                }

            );

            AdminModuleTitle.Foreground = Brushes.White;
            PlanningModuleTitle.Foreground = Brushes.Black;
            //AdminControl c = new AdminControl(); //TODO: Disabled for demo
            //ModuleControl.Content = c;
        }


    }
}
