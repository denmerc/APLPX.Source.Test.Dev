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
using ReactiveUI;
using Domain = APLPX.Client.Display;


namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for FilterStepControl.xaml
    /// </summary>
    public partial class FilterStepControl : UserControl
    {
        APLPX.UI.WPF.ViewModels.Reactive.EventAggregator Publisher = ((APLPX.UI.WPF.ViewModels.Reactive.EventAggregator)App.Current.Resources["EventManager"]);

        public FilterStepControl()
        {
            InitializeComponent();

           
        }
        
        private void ListBoxItem_OnSelected(object sender, RoutedEventArgs e)
        {
            var selected = StepContentListBox.SelectedItem;
            if (selected != null)
            {
                Publisher.Publish<Domain.FilterType>(
                        (Domain.FilterType) selected
                    );
                FilterGrid.Visibility = Visibility.Collapsed;
                var a = new DoubleAnimation
                {
                    From = 0.0,
                    To = 1.0,
                    FillBehavior = FillBehavior.Stop,
                    Duration = new Duration(TimeSpan.FromSeconds(1))
                };
                var storyboard = new Storyboard();

                storyboard.Children.Add(a);
                Storyboard.SetTarget(a, FilterGrid);
                Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
                storyboard.Begin();  
                FilterGrid.Visibility = Visibility.Visible;
            }
        }   
    }


}
