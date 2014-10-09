using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Server.Entity;

namespace APLPX.Server.Services.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Session<Server.Entity.NullT> Initialize(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.NullT> Authenticate(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.NullT> LoadExplorerPlanning(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.NullT> LoadExplorerTracking(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.NullT> LoadExplorerReporting(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<List<Server.Entity.User.Identity>> LoadList(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.User.Identity> LoadIdentity(Session<Server.Entity.User.Identity> session);
        [OperationContract]
        Session<Server.Entity.User.Identity> SaveIdentity(Session<Server.Entity.User.Identity> session);
        [OperationContract]
        Session<Server.Entity.NullT> SavePassword(Session<Server.Entity.NullT> session);

    }
}
