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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Domain = APLPX.Client.Display;


namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for FilterStepControl.xaml
    /// </summary>
    public partial class AnalyticValueDriversStepControl : UserControl
    {
        APLPX.UI.WPF.ViewModels.Reactive.EventAggregator Publisher = ((APLPX.UI.WPF.ViewModels.Reactive.EventAggregator)App.Current.Resources["EventManager"]);

        public AnalyticValueDriversStepControl()
        {
            InitializeComponent();
        }

        private void StepContentListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ModeList.Visibility = Visibility.Collapsed;
            var a = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                FillBehavior = FillBehavior.Stop,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            var storyboard = new Storyboard();

            storyboard.Children.Add(a);
            Storyboard.SetTarget(a, ModeList);
            Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
            storyboard.Begin();


            //ResetGroups(); 
            ModeList.SelectedItem = null;
            //FilterGrid.Visibility = Visibility.Visible;
            ModeList.Visibility = Visibility.Visible;
            DetailStack.Visibility = Visibility.Hidden;
        }

        int i = 0;
        private void RowNo_IncrementValue(object sender, MahApps.Metro.Controls.NumericUpDownChangedRoutedEventArgs args)
        {
            
            //FilterGrid.Items.Add(new Domain.ValueDriver() { Group = ++i}); //TODO: should be List<Group>
            
        }

        private void RowNo_DecrementValue(object sender, MahApps.Metro.Controls.NumericUpDownChangedRoutedEventArgs args)
        {
            //FilterGrid.Items.Remove(FilterGrid.Items[--i]);
        }

        void ResetGroups()
        {
            i = 0;
            RowNo.Value = RowNo.Minimum;
            //FilterGrid.Items.Clear();
            
        }

        private void ModeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ModeList.SelectedItem != null)
            {

                Publisher.Publish<ViewModels.Events.DriverSelectedEvent>(
                    new ViewModels.Events.DriverSelectedEvent 
                    {
                        Mode = (Domain.Mode)ModeList.SelectedItem,
                        DriverType = (Domain.ValueDriverType)StepContentListBox.SelectedItem
                    });

                DetailStack.Visibility = Visibility.Collapsed;
                ResetGroups();
                var a = new DoubleAnimation
                {
                    From = 0.0,
                    To = 1.0,
                    FillBehavior = FillBehavior.Stop,
                    Duration = new Duration(TimeSpan.FromSeconds(1))
                };
                var storyboard = new Storyboard();

                storyboard.Children.Add(a);
                Storyboard.SetTarget(a, DetailStack);
                Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
                storyboard.Begin();


                DetailStack.Visibility = Visibility.Visible;
            
            }

        }
    }
}