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
        Session<List<Server.Entity.User>> LoadList(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.User> LoadUser(Session<Server.Entity.User> session);
        [OperationContract]
        Session<Server.Entity.User> SaveUser(Session<Server.Entity.User> session);
        [OperationContract]
        Session<Server.Entity.NullT> SavePassword(Session<Server.Entity.NullT> session);
    }
}
