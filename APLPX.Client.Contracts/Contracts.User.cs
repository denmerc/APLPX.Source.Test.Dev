using System.Collections.Generic;
using System.ServiceModel;
//using APLPX.Client.Entity;
using APLPX.Entity;

namespace APLPX.Client.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Session<NullT> Initialize(Session<NullT> session);
        [OperationContract]
        Session<NullT> Authenticate(Session<NullT> session);
        [OperationContract]
        Session<List<User>> LoadList(Session<NullT> session);
        [OperationContract]
        Session<User> LoadUser(Session<User> session);
        [OperationContract]
        Session<User> SaveUser(Session<User> session);
        [OperationContract]
        Session<NullT> SavePassword(Session<NullT> session);
    }
}
