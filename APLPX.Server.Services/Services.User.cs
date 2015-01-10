using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using APLPX.Server.Data;
using APLPX.Server.Entity;
using APLPX.Server.Services.Contracts;

namespace APLPX.Server.Services
{
    //[ServiceBehavior(UseSynchronizationContext = true, InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    //[CallbackBehavior(UseSynchronizationContext = false)]
    public class UserService : IUserService
    {
        private IUserData _userData;

        public UserService() : this(new UserData()) { }
        [ImportingConstructor]
        public UserService(IUserData userData)
        {
            this._userData = userData;
        }

        public Session<NullT> Initialize(Session<NullT> sessionIn)
        {
            //APLPX.Server.Entity.Session<NullT> sessionOut = _userData.Initialize(sessionIn);
            //_userData.Dispose();

            //return sessionOut;

            return new Session<NullT> { SessionOk = true}; 
        }

        public Session<NullT> Authenticate(Session<NullT> sessionIn)
        {
            //APLPX.Server.Entity.Session<NullT> sessionOut = _userData.Authenticate(sessionIn);
            //_userData.Dispose();

            //return sessionOut;
            return new Session<NullT>
            {
                SessionOk = true,
                Authenticated = true,
                SqlKey = "sql123",
                User = new User
                            {
                                Id = 1,
                                Credential = new UserCredential { Login = "admin", OldPassword = "Password" },
                                Role = new UserRole { Name = "Admin" }
                            }
            };
        }

        public Session<List<User>> LoadList(Session<NullT> sessionIn)
        {
            APLPX.Server.Entity.Session<List<User>> sessionOut = _userData.LoadList(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<User> LoadUser(Session<User> sessionIn)
        {
            APLPX.Server.Entity.Session<User> sessionOut = _userData.LoadUser(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<User> SaveUser(Session<User> sessionIn)
        {
            APLPX.Server.Entity.Session<User> sessionOut = _userData.SaveUser(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<NullT> SavePassword(Session<NullT> sessionIn)
        {
            APLPX.Server.Entity.Session<NullT> sessionOut = _userData.SavePassword(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }
    }
    #region OBSOLETE...
        //public Session<NullT> LoadExplorerPlanning(Session<NullT> sessionIn)
        //{
        //    APLPX.Server.Entity.Session<NullT> sessionOut = _userData.LoadExplorerPlanning(sessionIn);
        //    _userData.Dispose();

        //    return sessionOut;
        //}

        //public Session<NullT> LoadExplorerTracking(Session<NullT> sessionIn)
        //{
        //    APLPX.Server.Entity.Session<NullT> sessionOut = _userData.LoadExplorerTracking(sessionIn);
        //    _userData.Dispose();

        //    return sessionOut;
        //}

        //public Session<NullT> LoadExplorerReporting(Session<NullT> sessionIn)
        //{
        //    APLPX.Server.Entity.Session<NullT> sessionOut = _userData.LoadExplorerReporting(sessionIn);
        //    _userData.Dispose();

        //    return sessionOut;
        //}
    #endregion
}
