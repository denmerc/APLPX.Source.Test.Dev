using System;
using System.Collections.Generic;

using APLPX.Client.Entity;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class User : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private string _sqlKey;
        private UserIdentity _identity;
        private UserRole _role;
        private List<Module> _modules;
        //private UserPassword _password;
        private string _oldPassword;
        private string _newPassword;
        private List<SQLEnumeration> _roleTypes;

        #endregion

        #region Constructors

        public User()
        {
            Identity = new UserIdentity();
            Role = new UserRole();
            Modules = new List<Module>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public string SqlKey
        {
            get { return _sqlKey; }
            set { this.RaiseAndSetIfChanged(ref _sqlKey, value); }
        }

        public UserIdentity Identity
        {
            get { return _identity; }
            set { this.RaiseAndSetIfChanged(ref _identity, value); }
        }

        public UserRole Role
        {
            get { return _role; }
            set { this.RaiseAndSetIfChanged(ref _role, value); }
        }

        public List<Module> Modules
        {
            get { return _modules; }
            set { this.RaiseAndSetIfChanged(ref _modules, value); }
        }

        public string OldPassword
        {
            get { return _oldPassword; }
            set { this.RaiseAndSetIfChanged(ref _oldPassword, value); }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set { this.RaiseAndSetIfChanged(ref _newPassword, value); }
        }

        //public UserPassword Password
        //{
        //    get { return _password; }
        //    set { this.RaiseAndSetIfChanged(ref _password, value); }
        //}

        public List<SQLEnumeration> RoleTypes
        {
            get { return _roleTypes; }
            set { this.RaiseAndSetIfChanged(ref _roleTypes, value); }
        }

        #endregion

    }
}
