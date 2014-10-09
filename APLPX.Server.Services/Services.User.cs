using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using APLPX.Server.Data;
using APLPX.Server.Entity;
using APLPX.Server.Services.Contracts;

namespace APLPX.Server.Services
{
    [ServiceBehavior(UseSynchronizationContext = true, InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    [CallbackBehavior(UseSynchronizationContext = false)]
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
            APLPX.Server.Entity.Session<NullT> sessionOut = _userData.Initialize(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<NullT> Authenticate(Session<NullT> sessionIn)
        {
            APLPX.Server.Entity.Session<NullT> sessionOut = _userData.Authenticate(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<NullT> LoadExplorerPlanning(Session<NullT> sessionIn)
        {
            APLPX.Server.Entity.Session<NullT> sessionOut = _userData.LoadExplorerPlanning(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<NullT> LoadExplorerTracking(Session<NullT> sessionIn)
        {
            APLPX.Server.Entity.Session<NullT> sessionOut = _userData.LoadExplorerTracking(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<NullT> LoadExplorerReporting(Session<NullT> sessionIn)
        {
            APLPX.Server.Entity.Session<NullT> sessionOut = _userData.LoadExplorerReporting(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<List<User.Identity>> LoadList(Session<NullT> sessionIn)
        {
            APLPX.Server.Entity.Session<List<User.Identity>> sessionOut = _userData.LoadList(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<User.Identity> LoadIdentity(Session<User.Identity> sessionIn)
        {
            APLPX.Server.Entity.Session<User.Identity> sessionOut = _userData.LoadIdentity(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<User.Identity> SaveIdentity(Session<User.Identity> sessionIn)
        {
            APLPX.Server.Entity.Session<User.Identity> sessionOut = _userData.SaveIdentity(sessionIn);
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
}
