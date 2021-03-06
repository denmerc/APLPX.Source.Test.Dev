﻿using System;
using System.Collections.Generic;

using APLPX.Entity;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class User : DisplayEntityBase, ISearchableEntity
    {
        #region Private Fields

        private int _id;
        
        private string _sqlKey;
        private UserIdentity _identity;
        private UserRole _role;
        private List<Module> _modules;
        private string _login;
        private string _oldPassword;
        private string _newPassword;
        private List<SQLEnumeration> _roleTypes;
        private FeatureSearchGroup _searchGroup;
        private int _searchGroupId;
        private int _owningSearchGroupId;
        private string _searchKey;        
        private bool _canNameChange;
        private bool _canSearchKeyChange;        
        
        #endregion

        #region Constructors

        public User()
        {
            Identity = new UserIdentity();
            Modules = new List<Module>();
            Role = new UserRole();
            RoleTypes = new List<SQLEnumeration>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public int SearchGroupId
        {
            get { return _searchGroupId; }
            set { this.RaiseAndSetIfChanged(ref _searchGroupId, value); }
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

        public string Login
        {
            get { return _login; }
            set { this.RaiseAndSetIfChanged(ref _login, value); }
        }

        public List<SQLEnumeration> RoleTypes
        {
            get { return _roleTypes; }
            set { this.RaiseAndSetIfChanged(ref _roleTypes, value); }
        }

        #endregion

        #region ISearchableEntity

        public FeatureSearchGroup SearchGroup
        {
            get { return _searchGroup; }
            set { this.RaiseAndSetIfChanged(ref _searchGroup, value); }
        }

        public int OwningSearchGroupId
        {
            get { return _owningSearchGroupId; }
            set { this.RaiseAndSetIfChanged(ref _owningSearchGroupId, value); }
        }

        public string SearchGroupKey
        {
            get { return _searchKey; }
            set { this.RaiseAndSetIfChanged(ref _searchKey, value); }
        }

        public string EntityTypeName
        {
            get { return GetType().Name; }
        }

        public bool CanNameChange
        {
            get { return _canNameChange; }
            set { _canNameChange = value; }
        }

        public bool CanSearchKeyChange
        {
            get { return _canSearchKeyChange; }
            set { _canSearchKeyChange = value; }
        }  

        #endregion
    }
}
