using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using APLPX.Client.Entity;
using APLPX.Client.Contracts;


namespace APLPX.Client
{
    [Export]
    public class UserClient : ClientBase<IUserService>, IUserService
    {
        public Session<Client.Entity.NullT> Initialize(Session<Client.Entity.NullT> session)
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

        public Session<List<Client.Entity.User>> LoadList(Session<NullT> session)
        {
            return Channel.LoadList(session);
        }

        public Session<Client.Entity.User> LoadUser(Session<User> session)
        {
            return Channel.LoadUser(session);
        }

        public Session<Client.Entity.User> SaveUser(Session<User> session)
        {
            return Channel.SaveUser(session);
        }

        public Session<NullT> SavePassword(Session<NullT> session)
        {
            return Channel.SavePassword(session);
        }
    }
}
