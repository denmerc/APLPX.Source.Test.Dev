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
using APLPX.UI.WPF.ViewModels.Pricing;

namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for PricingEverydayResultsStepControl.xaml
    /// </summary>
    public partial class PricingEverydayResultsStepControl : UserControl
    {
        public PricingEverydayResultsStepControl()
        {
            InitializeComponent();
        }

        private void FilterGroupsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch ((FilterGroupsListBox.SelectedItem as PricingView).Name.ToString())
                {
                    case "Summary":
                        pricingEverydayCanvas.Children.Clear();
                        pricingEverydayCanvas.Children.Add(new PricingEverydayResultsSummaryControl());
                        break;
                    case "Warnings":
                        pricingEverydayCanvas.Children.Clear();
                        pricingEverydayCanvas.Children.Add(new PricingEverydayResultsWarningsControl());
                        break;
                    case "Price Delta":
                        pricingEverydayCanvas.Children.Clear();
                        pricingEverydayCanvas.Children.Add(new PricingEverydayResultsPriceChangeControl());
                        break;
                    case "Mark-Up Delta":
                        pricingEverydayCanvas.Children.Clear();
                        pricingEverydayCanvas.Children.Add(new PricingEverydayResultsMarkupChangeControl());
                        break;
                    case "Price List":
                        pricingEverydayCanvas.Children.Clear();
                        pricingEverydayCanvas.Children.Add(new PricingEverydayResultsPriceListControl());
                        break;
                    case "Competition":
                        PricingEverydayResultsCompetitionControl competitionControl = new PricingEverydayResultsCompetitionControl();
                        competitionControl.IsEnabled = false;
                        pricingEverydayCanvas.Children.Clear();
                        pricingEverydayCanvas.Children.Add(competitionControl);
                        break;
                    case "Value Driver Groups":
                        pricingEverydayCanvas.Children.Clear();
                        pricingEverydayCanvas.Children.Add(new PricingEverydayResultsValueDriverGroupsControl());
                        break;
                    case "Edited":
                        pricingEverydayCanvas.Children.Clear();
                        pricingEverydayCanvas.Children.Add(new PricingEverydayResultsEditedControl());
                        break;
                    case "Excluded":
                        pricingEverydayCanvas.Children.Clear();
                        pricingEverydayCanvas.Children.Add(new PricingEverydayResultsExcludedControl());
                        break;
                    default:
                        pricingEverydayCanvas.Children.Clear();
                        pricingEverydayCanvas.Children.Add(new PricingEverydayResultsSummaryControl());
                        break;

                }
            }
            catch
            {
                pricingEverydayCanvas.Children.Clear();
                pricingEverydayCanvas.Children.Add(new PricingEverydayResultsSummaryControl());
            }
            
        }
    }
}
