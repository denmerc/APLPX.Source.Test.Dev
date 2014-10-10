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
using APLPX.UI.WPF.ViewModels.Events;
using Domain = APLPX.Client.Display;


namespace APLPX.UI.WPF
{


	/// <summary>
	/// Interaction logic for HomeControl.xaml
	/// </summary>
	public partial class PricingStepsControl : UserControl
	{
        APLPX.UI.WPF.ViewModels.Reactive.EventAggregator Publisher = ((APLPX.UI.WPF.ViewModels.Reactive.EventAggregator)App.Current.Resources["EventManager"]);
        public PricingStepsControl()
		{
			this.InitializeComponent();
		}

        private void AddNewPricingButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Content = new PricingStepsControl();
        }

        private void PlanningModuleButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Content = new HomeVerticalControl();
        }

        private void RelatedAnalyticButton_Click(object sender, RoutedEventArgs e)
        {
            //AnalyticStepsControl view = new AnalyticStepsControl();
            //view.NameTextBox.Text = "XXXX";
            //view.DescriptionTextBox.Text = "XXXX";
            //view.StatusTextBox.Text = "XXXX";

            //this.Content = view;
        }

        private void FilterStepListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            //FilterStepControl view = new FilterStepControl();
            //view.FilterGrid.Visibility = Visibility.Collapsed;
            //StepContentControl.Content = view;
        }

        private void IdentityListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            //if (StepContentControl != null)
            //{
            //    PricingIdentityStepControl c = new PricingIdentityStepControl();
            //    this.StepContentControl.Content = c;
            //}
        }

        private void PriceListItem_Selected(object sender, RoutedEventArgs e)
        {
            //PriceListStepControl content = new PriceListStepControl();
            //this.StepContentControl.Content = content;
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            //this.StepContentControl.Content = null;
        }

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            //var flyout = this.Flyouts.Items[1] as Flyout;
            //if (flyout == null)
            //{
            //    return;
            //}

            //flyout.IsOpen = !flyout.IsOpen;

            //DetailContentScrollViewer.HorizontalAlignment = HorizontalAlignment.Stretch;
            //CommandBar.Width = 400;
        }

        private void StepListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //((ListBoxItem) StepListBox.SelectedItem)
            var step = ((ListBoxItem)StepListBox.SelectedItem).Tag.ToString();
            switch (step)
            {
                case "0": //identity
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Everyday,
                        Section = Domain.SectionType.PlanningPricingIdentity
                        
                    });
                    break;
                case "1" : //filters
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Everyday,
                        Section = Domain.SectionType.PlanningPricingFilters

                    });
                    break;
                case "2" : //price lists
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Everyday,
                        Section = Domain.SectionType.PlanningPricingPriceLists

                    });
                    break;
                case "3": //rounding
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Everyday,
                        Section = Domain.SectionType.PlanningPricingRounding

                    });
                    break; 
                case "4" : //value driver - strategy?
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Everyday,
                        Section = Domain.SectionType.PlanningPricingStrategy

                    });
                    break;
                case "5": //results
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Everyday,
                        Section = Domain.SectionType.PlanningPricingResults

                    });
                    break;
                case "6": //forecast
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Everyday,
                        Section = Domain.SectionType.PlanningPricingForecast

                    });
                    break;
                case "7": //results
                    Publisher.Publish<NavigateEvent>(
                    new NavigateEvent
                    {
                        Module = Domain.ModuleType.Planning,
                        SubModule = Domain.SubModuleType.Everyday,
                        Section = Domain.SectionType.PlanningPricingApproval

                    });
                    break;
                default:
                    break;
            }

        }

	}
}