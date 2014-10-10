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
using System.Timers;
using System.Threading;
using APLPX.UI.WPF.ViewModels.Events;
using Domain = APLPX.Client.Display;

namespace APLPX.UI.WPF
{
	/// <summary>
	/// Interaction logic for HomeControl.xaml
	/// </summary>
	public partial class AnalyticStepsControl : UserControl
	{
        APLPX.UI.WPF.ViewModels.Reactive.EventAggregator Publisher = ((APLPX.UI.WPF.ViewModels.Reactive.EventAggregator)App.Current.Resources["EventManager"]);
        public AnalyticStepsControl()
		{
			this.InitializeComponent();

		}
        private void AddNewAnalyticButton_Click(object sender, RoutedEventArgs e)
        {
            AnalyticStepsControl c = new AnalyticStepsControl();
            c.TitleTextBox.Text = "Analytic - New";
            this.Content = c;
        }


        private void RelatedPriceRoutineButton_Click(object sender, RoutedEventArgs e)
        {
            PricingStepsControl p = new PricingStepsControl();
            this.Content = p;
        }

        private void FilterListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            //FilterStepControl control = new FilterStepControl();
            //control.FilterGrid.Visibility = Visibility.Collapsed;
            //this.AnalyticStepContentControl.Content = control;

            //Publisher.Publish<NavigateEvent>(
            //        new NavigateEvent
            //        {
            //            Module = Domain.ModuleType.Planning,
            //            SubModule = Domain.SubModuleType.Analytics,
            //            Section = Domain.SectionType.PlanningAnalyticsFilters
                        
            //        });
        }

        private void AnalyticIdentityStepListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            //if(AnalyticStepContentControl != null)
            //{ 
            //    AnalyticIdentityStepControl c = new AnalyticIdentityStepControl();
            //    this.AnalyticStepContentControl.Content = c;
            //}
        }

        private void PriceListItem_Selected(object sender, RoutedEventArgs e)
        {
            //if (AnalyticStepContentControl != null)
            //{
            //    PriceListStepControl c = new PriceListStepControl();
            //    this.AnalyticStepContentControl.Content = c;
            //}
        }

        private void ValueDriverItem_Selected(object sender, RoutedEventArgs e)
        {
            //if (AnalyticStepContentControl != null)
            //{
            //    AnalyticValueDriversStepControl c = new AnalyticValueDriversStepControl();
            //    this.AnalyticStepContentControl.Content = c;
            //}          
        }

        private void ResultItem_Selected(object sender, RoutedEventArgs e)
        {
            //this.AnalyticStepContentControl.Content = null;
        }

        private void RunAnalytic_Click(object sender, RoutedEventArgs e)
        {
            (StepListBox.Items[4] as ListBoxItem).Visibility = Visibility.Visible;
            StepListBox.SelectedItem = StepListBox.Items[4];
            this.AnalyticStepContentControl.Content = new AnalyticResultsControl();
        }

        private void StepListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



            //((ListBoxItem) StepListBox.SelectedItem)
            var step = ((ListBoxItem)StepListBox.SelectedItem).Tag.ToString();
            switch (step)
            {
                case "1": //identity
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Analytics,
                        Section = Domain.SectionType.PlanningAnalyticsIdentity
                        
                    });
                    break;
                case "2" : //filters
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Analytics,
                        Section = Domain.SectionType.PlanningAnalyticsFilters

                    });
                    break; 
                case "3" : //price lists
                    break;
                case "4" : //value driver
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Analytics,
                        Section = Domain.SectionType.PlanningAnalyticsValueDrivers

                    });
                    break;
                case "5" : //results
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Analytics,
                        Section = Domain.SectionType.PlanningAnalyticsResults

                    });
                    break;
                default:
                    break;
            }
            //if (AnalyticStepContentControl != null)
            //{
            //    if (StepListBox.SelectedItem != StepListBox.Items[4])
            //    {

            //        (StepListBox.Items[4] as ListBoxItem).Visibility = Visibility.Collapsed;
            //    }
            //}
        }

        private void CreatePriceRoutineButton_Click(object sender, RoutedEventArgs e)
        {
            PricingStepsControl view = new PricingStepsControl();
            PricingIdentityStepControl subView = new PricingIdentityStepControl();
            subView.NameTextBox.Text = "Price Routine for " + this.TitleTextBox.Text;
            subView.AnalyticTextBox.Text = this.TitleTextBox.Text;
            view.StepContentControl.Content = subView;
            this.Content = view;
        }

        private void SaveAnalytic_Click(object sender, RoutedEventArgs e)
        {

            switch (StepListBox.SelectedIndex)
	        {
                case 0:
                    Publisher.Publish<SaveEvent>(
                        new SaveEvent
                        {
                            Module = Domain.ModuleType.Planning,
                            SubModule = Domain.SubModuleType.Analytics,
                            Section = Domain.SectionType.PlanningAnalyticsIdentity

                        });
                    StepListBox.SelectedItem = StepListBox.Items[1];
                    break;
                case 1:
                    Publisher.Publish<SaveEvent>(
                        new SaveEvent
                        {
                            Module = Domain.ModuleType.Planning,
                            SubModule = Domain.SubModuleType.Analytics,
                            Section = Domain.SectionType.PlanningAnalyticsFilters

                        });
                    StepListBox.SelectedItem = StepListBox.Items[2];
                    break;
                default:
                    break;
	        }
            

        }
	}
}