using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Views.Common
{
    /// <summary>
    /// Interaction logic for RoundingRuleControl.xaml
    /// </summary>
    public partial class RoundingRuleControl : UserControl
    {

        /// <summary>
        /// Gets/sets whether this control is editable.
        /// </summary>
        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register("IsEditable", typeof(bool), typeof(RoundingRuleControl), new PropertyMetadata(false));


        public RoundingRuleControl()
        {
            InitializeComponent();
        }

        private void dgRoundingRules_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (IsEditable)
                {
                    //Get confirmation before deleting the rule.
                    var source = e.OriginalSource as DataGridCell;
                    if (source != null && source.DataContext is PriceRoundingRule)
                    {
                        PriceRoundingRule rule = source.DataContext as PriceRoundingRule;
                        object[] values = { rule.DollarRangeLower, rule.DollarRangeUpper, rule.ValueChange };
                        string prompt = String.Format("Delete this rounding rule (Min={0}, Max={1}, Rounding Value={2})?", values);

                        MessageBoxResult result = MessageBox.Show(prompt, "PRICEXPERT", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                        //Cancel unless the user confirms.
                        e.Handled = (result != MessageBoxResult.Yes);
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}
