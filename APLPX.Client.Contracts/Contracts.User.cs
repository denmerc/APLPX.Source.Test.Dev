using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Client.Entity;

namespace APLPX.Client.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Session<Client.Entity.NullT> Initialize(Session<Client.Entity.NullT> session);
        [OperationContract]
        Session<Client.Entity.NullT> Authenticate(Session<Client.Entity.NullT> session);
        [OperationContract]
        Session<List<Client.Entity.User>> LoadList(Session<Client.Entity.NullT> session);
        [OperationContract]
        Session<Client.Entity.User> LoadUser(Session<Client.Entity.User> session);
        [OperationContract]
        Session<Client.Entity.User> SaveUser(Session<Client.Entity.User> session);
        [OperationContract]
        Session<Client.Entity.NullT> SavePassword(Session<Client.Entity.NullT> session);
    }
}
