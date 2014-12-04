using System;
using System.Windows.Controls;


namespace APLPX.UI.WPF
{
    /// <summary>
    /// User control for viewing and editing analytic drivers.
    /// </summary>
    public partial class AnalyticValueDriversStepControl : UserControl
    {
        public AnalyticValueDriversStepControl()
        {
            InitializeComponent();
        }

        private void DriverGroupsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var group = e.Row.Item as APLPX.UI.WPF.DisplayEntities.ValueDriverGroup;
            
            if (e.Column == colLower)
            {
                e.Cancel = !group.IsMinValueEditable;
            }
            else if (e.Column == colUpper)
            {
                e.Cancel = !group.IsMaxValueEditable;
            }
        }

    }
}