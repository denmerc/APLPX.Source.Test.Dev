using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;


using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.ViewModels;
using DTO = APLPX.Entity;
using System.Reflection;
using APLPX.Client.Contracts;
using APLPX.Client;

namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {

            var eventManager = new EventAggregator();
            App.Current.Resources.Add("EventManager", eventManager);
            string[] pluginPaths = System.IO.Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "APLPX.Client.Mock.dll");
            if(pluginPaths.Count() > 0)
            {
                
                var plugins = ( from file in pluginPaths
                                    let asm = Assembly.LoadFile(file)
                                    from type in asm.GetExportedTypes()
                                    where typeof(IAnalyticService).IsAssignableFrom(type) || typeof(IPricingEverydayService).IsAssignableFrom(type) || typeof(IUserService).IsAssignableFrom(type)
                                    select Activator.CreateInstance(type)
                                ).ToArray();
                var analyticClient = (IAnalyticService) plugins[0];
                var pricingEverydayClient = (IPricingEverydayService) plugins[1];
                var userClient = (IUserService)plugins[2];


                
                var session = new DTO.Session<DTO.NullT>
                {
                    User = new DTO.User(
                                            2,
                                            "UserKey",
                                            new DTO.UserRole(3, "Administrator", "Role description"),
                                            new DTO.UserIdentity("dave.jinkerson@advancedpricinglogic.com", "Analyst", "User", true),
                                            new DTO.UserCredential("admin", "password", "passwordnew"),
                                            new List<DTO.SQLEnumeration>()

                                       )
                };

                DTO.Session<DTO.NullT> response = userClient.Authenticate(session);
                if (response.SessionOk)
                {
                    session.Modules = response.Modules;
                    //session.Analytics = response.Analytics;
                    //session.Pricing = response.Pricing;
                    //session.FilterGroups = response.FilterGroups;
                    var mvm = new MainViewModel(session, analyticClient, userClient, pricingEverydayClient);
                var mainWindow = new MainWindow();
                mainWindow.DataContext = mvm;
                mainWindow.Show();

                }
                else
                {
                    
                }




            }
            else
            {
                try
                {
                    var loginWindow = new LoginWindow();
                    loginWindow.DataContext = new LoginViewModel(new UserClient(), new AnalyticClient(), new PricingEverydayClient());
                    loginWindow.ShowMaxRestoreButton = false;
                    loginWindow.ShowMinButton = false;
                    loginWindow.ShowDialog();
                }
                catch (Exception)
                {
                    App.Current.Windows[0].Close();
                    MessageBox.Show("Application startup failure");
                }


            }            


            //var userService = new MockUserService();
            //var analyticService = new MockAnalyticService();

            //var eventManager = new EventAggregator();
            //App.Current.Resources.Add("EventManager", eventManager);

            //if (ConfigurationManager.AppSettings["Environment"] != "DEV")
                //{
            //    var loginWindow = new LoginWindow();
            //    loginWindow.DataContext = new LoginViewModel(userService);
            //    loginWindow.ShowMaxRestoreButton = false;
            //    loginWindow.ShowMinButton = false;
            //    loginWindow.ShowDialog();
            //}
            //else // DEV environment mode
            //{
            //    var session = new DTO.Session<DTO.NullT>
            //    {
            //        User = new DTO.User(
            //                                2,
            //                                "UserKey",
            //                                new DTO.UserRole(3, "Administrator", "Role description"),
            //                                new DTO.UserIdentity("dave.jinkerson@advancedpricinglogic.com", "Analyst", "User", true),
            //                                new DTO.UserCredential("admin", "password", "passwordnew"),
            //                                new List<DTO.SQLEnumeration>()

            //                           )
            //    };

                //    var mvm = new MainViewModel(session, analyticService, userService);
                //    var mainWindow = new MainWindow();
                //    mainWindow.DataContext = mvm;
                //    mainWindow.Show();

            //    //TODO: UNCOMMENT WHEN UserService is updated to work with new entity model:

            //    //DTO.Session<DTO.NullT> response = userService.Authenticate(session);
            //    //if (response.SessionOk)
            //    //{
            //    //    session.Modules = response.Modules;
            //    //    session.Analytics = response.Analytics;
            //    //    session.Pricing = response.Pricing;
            //    //    //session.FilterGroups = FilterGroups;
            //    //    var mvm = new MainViewModel(session, analyticService, userService);
            //    //    var mainWindow = new MainWindow();
            //    //    mainWindow.DataContext = mvm;
            //    //    mainWindow.Show();
            //    //}
            //    //else
            //    //{
            //    //    MessageBox.Show(response.ClientMessage);
            //    //}
                //}
                //TODO: UNCOMMENT WHEN UserService is updated to work with new entity model:
            

            base.OnStartup(e);

        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        //protected ReactiveCommand<Unit> LoadFiltersCommand { get; private set; }
        //public List<FilterGroup> FilterGroups { get; set; }
    }
}
