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
using NLog;
using System.Windows.Threading;
using Ninject.Parameters;
using APLPX.Entity;
using Ninject;


namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {

            //var eventManager = new EventAggregator(); App.Current.Resources.Add("EventManager", eventManager);

            //string[] pluginPaths = System.IO.Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "APLPX.Client.Mock.dll");
            //if(pluginPaths.Count() <= 0) // normal startup
            //{

            try
            {
                IKernel kernel = new StandardKernel();


                //mock

                var accentColors = new List<AccentColorMenuData>();
                var appThemes = new List<AppThemeMenuData>();

                kernel.Bind<IAnalyticService>().To<AnalyticClient>();
                kernel.Bind<IPricingEverydayService>().To<PricingEverydayClient>();
                kernel.Bind<IUserService>().To<UserClient>();
                kernel.Bind<EventAggregator>().ToSelf().InSingletonScope();


                var analyticClient = kernel.Get<IAnalyticService>();
                var pricingEverydayClient = kernel.Get<IPricingEverydayService>();
                var userClient = kernel.Get<IUserService>();
                var eventAggregator = kernel.Get<EventAggregator>();

                kernel.Bind<MainViewModel>().ToSelf()
                    .WithConstructorArgument("analyticService", analyticClient)
                    .WithConstructorArgument("userService", userClient)
                    .WithConstructorArgument("pricingService", pricingEverydayClient);

                kernel.Bind<LoginViewModel>().ToSelf().InSingletonScope();


                //var main = kernel.Get<MainViewModel>(
                //    new ConstructorArgument("session", session),
                //    new ConstructorArgument("analyticService", analyticClient),
                //    new ConstructorArgument("pricingService", pricingEverydayClient),
                //    new ConstructorArgument("userService", userClient),
                //    new ConstructorArgument("eventManager", eventAggregator)
                //    );

                //var mainWindow = new MainWindow();
                //mainWindow.DataContext = main;
                //mainWindow.Show();
                var eventManager = eventAggregator; App.Current.Resources.Add("EventManager", eventManager);

                var loginViewModel = kernel.Get<LoginViewModel>();
                ViewModelBase.Kernel = kernel;

                var loginWindow = new LoginWindow();
                //var vm = new LoginViewModel();
                loginWindow.DataContext = loginViewModel;
                loginWindow.ShowMaxRestoreButton = false;
                loginWindow.ShowMinButton = false;
                var mainWindow = new MainWindow();

                if (loginWindow.ShowDialog() == true)
                {
                    //var mvm = new MainViewModel(loginViewModel.Session, analyticClient, userClient, pricingClient, eventManager);
                    var main = kernel.Get<MainViewModel>(
                        new ConstructorArgument("session", loginViewModel.Session)
                        );
                        //,
                        //new ConstructorArgument("analyticService", analyticClient),
                        //new ConstructorArgument("pricingService", pricingEverydayClient),
                        //new ConstructorArgument("userService", userClient),
                        //new ConstructorArgument("eventManager", eventAggregator)
                    mainWindow.DataContext = main;
                    mainWindow.Show();
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Log(LogLevel.Error, "Unhandled Application Startup Exception", ex);
                App.Current.Shutdown();
            }

            //}
            //else // mock
            //{

            //    var plugins = ( from file in pluginPaths
            //                        let asm = Assembly.LoadFile(file)
            //                        from type in asm.GetExportedTypes()
            //                        where typeof(IAnalyticService).IsAssignableFrom(type) || typeof(IPricingEverydayService).IsAssignableFrom(type) || typeof(IUserService).IsAssignableFrom(type)
            //                        select Activator.CreateInstance(type)
            //                    ).ToArray();
            //    var analyticClient = (IAnalyticService) plugins[0];
            //    var pricingEverydayClient = (IPricingEverydayService) plugins[1];
            //    var userClient = (IUserService)plugins[2];

            //var session = new DTO.Session<DTO.NullT>
            //{
            //    User = new DTO.User(
            //                            2,
            //                            "UserKey",
            //                            new DTO.UserRole(3, "Administrator", "Role description"),
            //                            new DTO.UserIdentity("dave.jinkerson@advancedpricinglogic.com", "Analyst", "User", true),
            //                            new DTO.UserCredential("admin", "password", "passwordnew"),
            //                            new List<DTO.SQLEnumeration>()

            //                       )
            //};

            //    DTO.Session<DTO.NullT> response = userClient.Authenticate(session);
            //    if (response.SessionOk)
            //    {
            //        session.Modules = response.Modules;
            //        //session.Analytics = response.Analytics;
            //        //session.Pricing = response.Pricing;
            //        //session.FilterGroups = response.FilterGroups;
            //        var mvm = new MainViewModel(session, analyticClient, userClient, pricingEverydayClient, eventManager);
            //        var mainWindow = new MainWindow();
            //        mainWindow.DataContext = mvm;
            //        mainWindow.Show();
            //    }
            //}            
            //IKernel kernel = new StandardKernel();


            ////mock

            //var accentColors = new List<AccentColorMenuData>();
            //var appThemes = new List<AppThemeMenuData>();

            //kernel.Bind<IAnalyticService>().To<AnalyticClient>();
            //kernel.Bind<IPricingEverydayService>().To<PricingEverydayClient>();
            //kernel.Bind<IUserService>().To<UserClient>();
            //kernel.Bind<EventAggregator>().ToSelf().InSingletonScope();


            //var analyticClient = kernel.Get<IAnalyticService>();
            //var pricingEverydayClient = kernel.Get<IPricingEverydayService>();
            //var userClient = kernel.Get<IUserService>();
            //var eventAggregator = kernel.Get<EventAggregator>();

            //kernel.Bind<MainViewModel>().ToSelf();
            //kernel.Bind<LoginViewModel>().ToSelf().InSingletonScope();
           

            ////var main = kernel.Get<MainViewModel>(
            ////    new ConstructorArgument("session", session),
            ////    new ConstructorArgument("analyticService", analyticClient),
            ////    new ConstructorArgument("pricingService", pricingEverydayClient),
            ////    new ConstructorArgument("userService", userClient),
            ////    new ConstructorArgument("eventManager", eventAggregator)
            ////    );

            ////var mainWindow = new MainWindow();
            ////mainWindow.DataContext = main;
            ////mainWindow.Show();

            //var loginViewModel = kernel.Get<LoginViewModel>();
            //ViewModelBase.Kernel = kernel;
            //var loginView = new LoginWindow();
            //loginView.DataContext = loginViewModel;
            //loginView.Show();

            base.OnStartup(e);

        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogManager.GetCurrentClassLogger().Log(LogLevel.Error, "Unhandled Application Exception", e.Exception);

            // Prevent default unhandled exception processing
            e.Handled = false;
        }
    }
}
