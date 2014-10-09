using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {
            var eventManager = new APLPX.UI.WPF.ViewModels.Reactive.EventAggregator();
            App.Current.Resources.Add("EventManager", eventManager);
            base.OnStartup(e);
        }
    }
}
