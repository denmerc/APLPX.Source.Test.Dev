using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;

using APLPX.Server.Data;
//using APLPX.Server.Entity;
using APLPX.Server.Services.Contracts;
using APLPX.Entity;
using System.Reflection;

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
            APLPX.Entity.Session<NullT> sessionOut = _userData.Initialize(sessionIn);
            _userData.Dispose();
            this.LogServiceResponse<IUserService,NullT>(sessionOut, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return sessionOut;

        }

        public Session<NullT> Authenticate(Session<NullT> sessionIn)
        {
            APLPX.Entity.Session<NullT> sessionOut = _userData.Authenticate(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<NullT> SavePassword(Session<NullT> sessionIn) {
            APLPX.Entity.Session<NullT> sessionOut = _userData.SavePassword(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        #region Administration service methods...
        public Session<List<User>> LoadList(Session<NullT> sessionIn) {
            APLPX.Entity.Session<List<User>> sessionOut = _userData.LoadList(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<User> LoadUser(Session<User> sessionIn)
        {
            APLPX.Entity.Session<User> sessionOut = _userData.LoadUser(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }

        public Session<User> SaveUser(Session<User> sessionIn)
        {
            APLPX.Entity.Session<User> sessionOut = _userData.SaveUser(sessionIn);
            _userData.Dispose();

            return sessionOut;
        }
        #endregion
    }
}
