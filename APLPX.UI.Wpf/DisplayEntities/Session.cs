using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Session<T> : DisplayEntityBase where T : class
    {
        #region Private Fields

        private User _user;
        private object _data;
        private bool _appOnline;
        private bool _authenticated;
        private bool _sqlAuthorization;
        private bool _winAuthorization;
        private bool _sessionOk;
        private string _clientMessage;
        private string _serverMessage;
        private List<Module> _modules;

        #endregion

        #region Constructors

        public Session()
        {
            User = new User();
            Modules = new List<Module>();
        }

        #endregion

        #region Properties

        public User User
        {
            get { return _user; }
            set { this.RaiseAndSetIfChanged(ref _user, value); }
        }

        public object Data
        {
            get { return _data; }
            set { this.RaiseAndSetIfChanged(ref _data, value); }
        }

        public bool AppOnline
        {
            get { return _appOnline; }
            set { this.RaiseAndSetIfChanged(ref _appOnline, value); }
        }

        public bool Authenticated
        {
            get { return _authenticated; }
            set { this.RaiseAndSetIfChanged(ref _authenticated, value); }
        }

        public bool SqlAuthorization
        {
            get { return _sqlAuthorization; }
            set { this.RaiseAndSetIfChanged(ref _sqlAuthorization, value); }
        }

        public bool WinAuthorization
        {
            get { return _winAuthorization; }
            set { this.RaiseAndSetIfChanged(ref _winAuthorization, value); }
        }

        public bool SessionOk
        {
            get { return _sessionOk; }
            set { this.RaiseAndSetIfChanged(ref _sessionOk, value); }
        }

        public string ClientMessage
        {
            get { return _clientMessage; }
            set { this.RaiseAndSetIfChanged(ref _clientMessage, value); }
        }

        public string ServerMessage
        {
            get { return _serverMessage; }
            set { this.RaiseAndSetIfChanged(ref _serverMessage, value); }
        }

        public List<Module> Modules
        {
            get { return _modules; }
            set { this.RaiseAndSetIfChanged(ref _modules, value); }
        }

        #endregion

    }
}
