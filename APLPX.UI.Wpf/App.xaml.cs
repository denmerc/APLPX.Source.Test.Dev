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
using APLPX.UI.WPF.AppllicationServices;
using APLPX.UI.WPF.ApplicationServices;


namespace APLPX.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {

            try
            {
                PriceExpertApplication.Current.Bootstrap();

                var loginViewModel = PriceExpertApplication.Current.Container.Get<LoginViewModel>();
                var loginWindow = new LoginWindow();
                loginWindow.DataContext = loginViewModel;
                loginWindow.ShowMaxRestoreButton = false;
                loginWindow.ShowMinButton = false;

                var mainWindow = new MainWindow();
                if (loginWindow.ShowDialog() == true)
                {
                    var main = PriceExpertApplication.Current.Container.Get< MainViewModel > (new ConstructorArgument("session", loginViewModel.Session)); 
                    mainWindow.DataContext = main;
                    mainWindow.Show(); 
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Log(LogLevel.Error, "Unhandled Application Startup Exception", ex);
                App.Current.Shutdown();
            }

            

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
