using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
//using APLPX.Client.Entity;
using APLPX.Entity;
using APLPX.Client.Contracts;
using System.Reflection;


namespace APLPX.Client
{
    [Export]
    public class UserClient : ClientBase<IUserService>, IUserService
    {
        public Session<NullT> Initialize(Session<NullT> session)
        {
            
            var response = Channel.Initialize(session);
            this.LogClientRequest<IUserService, NullT>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString())); 
            return response;
        }

        //TODO: Future implementation
        //public Task<Session<NullT>> InitializeAsync(Session<Client.Entity.NullT> session)
        //{
        //    return Channel.InitializeAsync(session);
        //}

        public Session<NullT> Authenticate(Session<NullT> session)
        {

            var response = Channel.Authenticate(session);
            //won't log for now because payload contains pwd
            //this.LogClientRequest<IUserService, NullT>(session,  string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString())); 
            return response;
        }

        public Session<List<User>> LoadList(Session<NullT> session)
        {
            var response = Channel.LoadList(session);
            this.LogClientRequest<IUserService, NullT>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<User> LoadUser(Session<User> session)
        {
            this.LogClientRequest<IUserService, User>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return Channel.LoadUser(session);
        }

        public Session<User> SaveUser(Session<User> session)
        {
            var response = Channel.SaveUser(session);
            this.LogClientRequest<IUserService, User>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<NullT> SavePassword(Session<NullT> session)
        {
            var response = Channel.SavePassword(session);
            //won't log for now because payload contains pwd
            //this.LogClientRequest<IUserService, NullT>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }
    }
}
