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

namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for PricingEverydayResultsEditedControl.xaml
    /// </summary>
    public partial class PricingEverydayResultsEditedControl : UserControl
    {
        public PricingEverydayResultsEditedControl()
        {
            InitializeComponent();
        }

        private ScrollViewer sv;

        private void LayoutUpdated(object sender, EventArgs e)
        {
            double offset = 0;
            GetScrollViewer(dg);
            if (sv != null && sv.ComputedHorizontalScrollBarVisibility == Visibility.Visible)
            {
                offset = sv.ContentHorizontalOffset;
            }

            double w = skuColumn.ActualWidth + descriptionColumn.ActualWidth + priceListsColumn.ActualWidth + currentPriceColumn.ActualWidth + currentMarkupColumn.ActualWidth - offset;
            Label1.Width = w < 0 ? 0 : w;

            double w2 = totalValDriverChangeColumn.ActualWidth + finalPriceColumn.ActualWidth + newMarkupColumn.ActualWidth + editTypeColumn.ActualWidth + warningColumn.ActualWidth;
            Label2.Width = w2;
        }

        private void GetScrollViewer(DependencyObject obj)
        {
            if (sv != null)
            {
                return;
            }

            var tmp = obj as ScrollViewer;
            if (tmp != null)
            {
                if (tmp.Name.Equals("DG_ScrollViewer"))
                {
                    sv = tmp;
                }
                else
                {
                    // Recursive call for each visual child
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                    {
                        GetScrollViewer(VisualTreeHelper.GetChild(obj, i));
                    }
                }
            }
            else
            {
                // Recursive call for each visual child
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    GetScrollViewer(VisualTreeHelper.GetChild(obj, i));
                }
            }
        }
    }
}
