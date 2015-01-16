using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
//using APLPX.Client.Entity;
using APLPX.Entity;
using APLPX.Client.Contracts;


namespace APLPX.Client
{
    [Export]
    public class UserClient : ClientBase<IUserService>, IUserService
    {
        public Session<NullT> Initialize(Session<NullT> session)
        {
            return Channel.Initialize(session);
        }

        //TODO: Future implementation
        //public Task<Session<NullT>> InitializeAsync(Session<Client.Entity.NullT> session)
        //{
        //    return Channel.InitializeAsync(session);
        //}

        public Session<NullT> Authenticate(Session<NullT> session)
        {
            return Channel.Authenticate(session);
        }

        public Session<List<User>> LoadList(Session<NullT> session)
        {
            return Channel.LoadList(session);
        }

        public Session<User> LoadUser(Session<User> session)
        {
            return Channel.LoadUser(session);
        }

        public Session<User> SaveUser(Session<User> session)
        {
            return Channel.SaveUser(session);
        }

        public Session<NullT> SavePassword(Session<NullT> session)
        {
            return Channel.SavePassword(session);
        }
    }
}
