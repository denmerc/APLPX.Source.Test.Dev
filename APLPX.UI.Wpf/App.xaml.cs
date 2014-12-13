using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using APLPX.UI.WPF.Data;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.ViewModels;
using DTO = APLPX.Client.Entity;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using APLPX.Client.Entity;

namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {
            var userService = new MockUserService();
            var analyticService = new MockAnalyticService();

            var eventManager = new EventAggregator();
            App.Current.Resources.Add("EventManager", eventManager);

            if (ConfigurationManager.AppSettings["Environment"] != "DEV")
            {
                var loginWindow = new LoginWindow();
                loginWindow.DataContext = new LoginViewModel(userService);
                loginWindow.ShowMaxRestoreButton = false;
                loginWindow.ShowMinButton = false;
                loginWindow.ShowDialog();
            }
            else // DEV environment mode
            {
                
                var session = new DTO.Session<DTO.NullT>
                {
                    User = new DTO.User(
                                            2,
                                            "UserKey",
                                            new DTO.UserRole(3, "Administrator", "Role description"),
                                            new DTO.UserIdentity("dave.jinkerson@advancedpricinglogic.com","Analyst", "User", true),
                                            new DTO.UserCredential("admin", "password", "passwordnew"),
                                            new List<DTO.SQLEnumeration>()
                                 
                                       )
                };
                //TODO: UNCOMMENT WHEN UserService is updated to work with new entity model:
                //var userService = new MockUserSevice();
                DTO.Session<DTO.NullT> response = userService.Authenticate(session);
                if (response.SessionOk)
                {
                    session.Modules = response.Modules;
                    session.Analytics = response.Analytics;
                    session.Pricing = response.Pricing;
                    //session.FilterGroups = FilterGroups;
                    var mvm = new MainViewModel(session, analyticService, userService);
                    var mainWindow = new MainWindow();
                    mainWindow.DataContext = mvm;
                    mainWindow.Show();
                //TODO: UNCOMMENT WHEN UserService is updated to work with new entity model:
                }
                else
                {
                    MessageBox.Show(response.ClientMessage);
                }
            }
            

            base.OnStartup(e);

        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        protected ReactiveCommand<Unit> LoadFiltersCommand { get; private set; }
        public List<FilterGroup> FilterGroups { get; set; }
    }
}
