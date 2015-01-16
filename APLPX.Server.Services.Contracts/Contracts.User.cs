using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Entity;

namespace APLPX.Server.Services.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Session<Entity.NullT> Initialize(Session<Entity.NullT> session);
        [OperationContract]
        Session<Entity.NullT> Authenticate(Session<Entity.NullT> session);
        [OperationContract]
        Session<Entity.NullT> SavePassword(Session<Entity.NullT> session);
        
        //Administration service methods...
        [OperationContract]
        Session<List<Entity.User>> LoadList(Session<Entity.NullT> session);
        [OperationContract]
        Session<Entity.User> LoadUser(Session<Entity.User> session);
        [OperationContract]
        Session<Entity.User> SaveUser(Session<Entity.User> session);
    }
}
