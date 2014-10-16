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

        public Task<Session<NullT>> InitializeAsync(Session<Client.Entity.NullT> session)
        {
            return Channel.InitializeAsync(session);
        }

        public Session<NullT> Authenticate(Session<NullT> session)
        {
            return Channel.Authenticate(session);
        }

        public Session<NullT> LoadExplorerPlanning(Session<NullT> session)
        {
            return Channel.LoadExplorerPlanning(session);
        }

        public Session<NullT> LoadExplorerTracking(Session<NullT> session)
        {
            return Channel.LoadExplorerTracking(session);
        }

        public Session<NullT> LoadExplorerReporting(Session<NullT> session)
        {
            return Channel.LoadExplorerReporting(session);
        }

        public Session<List<UserIdentity>> LoadList(Session<NullT> session)
        {
            return Channel.LoadList(session);
        }

        public Session<UserIdentity> LoadIdentity(Session<UserIdentity> session)
        {
            return Channel.LoadIdentity(session);
        }

        public Session<UserIdentity> SaveIdentity(Session<UserIdentity> session)
        {
            return Channel.SaveIdentity(session);
        }

        public Session<NullT> SavePassword(Session<NullT> session)
        {
            return Channel.SavePassword(session);
        }
    }
}
