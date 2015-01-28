using APLPX.Client.Contracts;
using APLPX.Entity;
using NLog;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace APLPX.UI.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IUserService userService)
        {
            UserService = userService;
            StatusMessage = "";
            
            InitializeCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                {
                    Session = await Initialize();   
                });
            InitializeCommand.ExecuteAsync().Subscribe( x =>
                    {
                        //TODO: enable login button
                    }
                );

            

            LoginCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                {
                    try
                    {
                        
                        StatusMessage = "Initializing";
                        Session = await Initialize();
                        StatusMessage = "Authenticating...";

                        Session = await Authenticate(UserName, Password);

                        if(Session.SessionOk)
                        {

                            foreach (Window item in App.Current.Windows)
                            {
                                if ( item.GetType().Name == "LoginWindow")
                                    item.DialogResult = true;
                            }
                        
                        } 
                        else //failed
                        {
                            LogManager.GetCurrentClassLogger().Log(LogLevel.Warn, "Login attempt failed");
                            ClearWhenLoginFails();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Log(LogLevel.Error, ex);
                        ClearWhenLoginFails();
                    }
                }
            );
        }

        public IUserService UserService { get; set; }
        public IPricingEverydayService PricingService { get; set; }
        public IAnalyticService AnalyticService { get; set; }
        public string UserName { get; set; }
        private string _password;
        public string Password { get { return _password;}   set { this.RaiseAndSetIfChanged(ref _password, value); } }
        private string _statusMessage;
        public string StatusMessage { get { return _statusMessage; } set { this.RaiseAndSetIfChanged(ref _statusMessage, value); } }
        public ReactiveCommand<Unit> LoginCommand { get; set; }
        public ReactiveCommand<Unit> InitializeCommand { get; set; }
        private Session<NullT> _session;
        public Session<NullT> Session { get { return _session; } set { this.RaiseAndSetIfChanged(ref _session, value); } }

        public async Task<Session<NullT>> Initialize()
        {
            return await Task.Run(() =>
            {
                Session<NullT> key = new Session<NullT>()
                {
                    SqlKey = ConfigurationManager.AppSettings["sharedKey"].ToString()
                };
                //throw new Exception();
                //Thread.Sleep(3000); //simulate initialize delay
                return UserService.Initialize(key);
            });
        }

        public async Task<Session<NullT>> Authenticate(string loginName, string password)
        {
            return await Task.Run(() =>
            {
                
                //Thread.Sleep(3000); //simulate initialize delay
                //throw new Exception();
                //var session = new Session<NullT>
                //{
                //    User = new User(
                //                            //2,
                //                            //"UserKey",
                //                            //new UserRole(3, "Administrator", "Role description"),
                //                            //new UserIdentity("dave.jinkerson@advancedpricinglogic.com", "Analyst", "User", true),
                //                            new UserCredential(loginName, password, null)
                //                            //,
                //                            //new List<SQLEnumeration>()

                //                       )
                //};

                    Session.User = new User(new UserCredential(loginName, password));
                    var response = UserService.Authenticate(Session); //TODO:Using and Dispose of proxy.
                
                return response;
            });
        }

        private void ClearWhenLoginFails()
        {
            Password = string.Empty;
            StatusMessage = "Failed to authenticate. Please try again.";
            
        }

        

    }
}
