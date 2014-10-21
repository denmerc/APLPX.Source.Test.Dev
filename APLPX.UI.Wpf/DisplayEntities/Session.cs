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

        private UserIdentity _userIdentity;
        private object _data;
        private bool _appOnline;
        private bool _authenticated;
        private bool _sqlAuthorization;
        private bool _winAuthorization;
        private bool _sessionOk;
        private string _clientMessage;
        private string _serverMessage;
        private ModuleFeature _feature;

        #endregion

        #region Constructors

        public Session()
        {
            UserIdentity = new UserIdentity();
            Feature = new ModuleFeature();
        }

        #endregion

        #region Properties

        public UserIdentity UserIdentity
        {
            get { return _userIdentity; }
            set { this.RaiseAndSetIfChanged(ref _userIdentity, value); }
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

        public ModuleFeature Feature
        {
            get { return _feature; }
            set { this.RaiseAndSetIfChanged(ref _feature, value); }
        }

        #endregion

    }
}
