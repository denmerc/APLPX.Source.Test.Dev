using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for FilterStepControl.xaml
    /// </summary>
    public partial class AnalyticResultsControl : UserControl
    {
        public AnalyticResultsControl()
        {
            InitializeComponent();
            ProgressBar.Visibility = Visibility.Visible;
            StepContentListBox.SelectedItem = StepContentListBox.Items[0];
        }

        private void StepContentListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            //FilterGrid.Visibility = Visibility.Collapsed;
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
            
            

            Task.Delay(4000)
                
                .ContinueWith(z=>
                {
                    
                    FilterGrid.Visibility = Visibility.Visible;
                    ProgressBar.Visibility = Visibility.Collapsed;

                }, TaskScheduler.FromCurrentSynchronizationContext());
            
            
            


        }
        public static Task Delay(double milliseconds)
        {
            var tcs = new TaskCompletionSource<bool>();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += (obj, args) =>
            {
                tcs.TrySetResult(true);
            };
            timer.Interval = milliseconds;
            timer.AutoReset = false;
            timer.Start();
            return tcs.Task;
        }


    }


}
