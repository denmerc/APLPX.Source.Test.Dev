using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using APLPX.UI.WPF.Data;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.ViewModels;
using DTO = APLPX.Client.Entity;

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

            if (ConfigurationManager.AppSettings["Environment"] != "DEV")
            {
                var loginWindow = new LoginWindow();
                loginWindow.DataContext = new LoginViewModel(new MockUserSevice());
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

                var userService = new MockUserSevice();
                DTO.Session<DTO.NullT> response = userService.Authenticate(session);
                if (response.SessionOk)
                {
                    session.Modules = response.Modules;
                    var mvm = new MainViewModel(session, new MockAnalyticService(), new MockUserSevice());
                    var mainWindow = new MainWindow();
                    mainWindow.DataContext = mvm;
                    mainWindow.Show();
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
    }
}
