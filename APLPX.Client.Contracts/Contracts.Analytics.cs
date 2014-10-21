using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using APLPX.Client.Entity;

namespace APLPX.Client.Contracts
{
    [ServiceContract]
    public interface IAnalyticService
    {
        [OperationContract]
        Session<List<Client.Entity.Analytic>> LoadList(Session<Client.Entity.NullT> session);
        [OperationContract]
        Session<Client.Entity.Analytic> SaveIdentity(Session<Client.Entity.Analytic> session);
        [OperationContract]
        Session<Client.Entity.Analytic> LoadFilters(Session<Client.Entity.Analytic> session);
        [OperationContract]
        Session<Client.Entity.Analytic> SaveFilters(Session<Client.Entity.Analytic> session);
        [OperationContract]
        Session<Client.Entity.Analytic> LoadDrivers(Session<Client.Entity.Analytic> session);
        [OperationContract]
        Session<Client.Entity.Analytic> SaveDrivers(Session<Client.Entity.Analytic> session);
        [OperationContract]
        Session<Client.Entity.Analytic> LoadPriceLists(Session<Client.Entity.Analytic> session);
        [OperationContract]
        Session<Client.Entity.Analytic> SavePriceLists(Session<Client.Entity.Analytic> session);
    }
}
