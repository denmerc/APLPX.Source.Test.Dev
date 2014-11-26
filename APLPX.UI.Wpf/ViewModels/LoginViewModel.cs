using APLPX.Client.Contracts;
using APLPX.Client.Entity;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APLPX.UI.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IUserService userService)
        {
            UserService = userService;
            //var sharedKey = UserService.Initialize();


            InitializeCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                {
                    StatusMessage = "Initializing PriceExpert...";
                    
                    Session = await Initialize();   
                });
            
            InitializeCommand.ExecuteAsync().Subscribe( x =>
                    {
                        //enable login button
                        StatusMessage = "";
                    }
                );
            LoginCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                {
                    StatusMessage = "Authenticating...";

                    Session = await Authenticate(UserName, Password);
                    
                    if(Session.SessionOk)
                    {
                        StatusMessage = "Loading modules..."; 
                        //todo: pass IAnalyticService via constructor in the case that user succesfully auths
                        var mvm = new MainViewModel(Session, new APLPX.UI.WPF.Data.MockAnalyticService(), userService);
                        var mainWindow = new MainWindow();
                        mainWindow.DataContext = mvm;
                        App.Current.Windows[0].Close();
                        mainWindow.Show();
                        
                    } 
                    else //failed
                    {
                        StatusMessage = "Failed to authenticate. Please try again.";
                    }
                }
            );
        }

        public IUserService UserService { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        private string _statusMessage;
        public string StatusMessage { get { return _statusMessage; } set { this.RaiseAndSetIfChanged(ref _statusMessage, value); } }
        public ReactiveCommand<Unit> LoginCommand { get; set; }
        public ReactiveCommand<Unit> InitializeCommand { get; set; }
        public Session<NullT> Session { get; set; }

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
                var session = new Session<NullT>
                {
                    User = new User(
                                            2,
                                            "UserKey",
                                            new UserRole(3, "Administrator", "Role description"),
                                            new UserIdentity("dave.jinkerson@advancedpricinglogic.com", "Analyst", "User", true),
                                            new UserCredential(loginName, password, null),
                                            new List<SQLEnumeration>()

                                       )
                };
                return UserService.Authenticate(session); //TODO:Using and Dispose of proxy.

            });
        }

        

    }
}
