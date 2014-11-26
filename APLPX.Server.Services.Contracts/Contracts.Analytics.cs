using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Server.Entity;

namespace APLPX.Server.Services.Contracts
{
    [ServiceContract]
    public interface IAnalyticService
    {
        [OperationContract]
        Session<List<Server.Entity.Analytic>> LoadList(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.Analytic> SaveIdentity(Session<Server.Entity.Analytic> session);
        [OperationContract]
        Session<Server.Entity.Analytic> LoadFilters(Session<Server.Entity.Analytic> session);
        [OperationContract]
        Session<Server.Entity.Analytic> SaveFilters(Session<Server.Entity.Analytic> session);
        [OperationContract]
        Session<Server.Entity.Analytic> LoadDriver(Session<Server.Entity.Analytic> session);
        [OperationContract]
        Session<Server.Entity.Analytic> LoadDrivers(Session<Server.Entity.Analytic> session);
        [OperationContract]
        Session<Server.Entity.Analytic> SaveDriver(Session<Server.Entity.Analytic> session);
        [OperationContract]
        Session<Server.Entity.Analytic> SaveDrivers(Session<Server.Entity.Analytic> session);
        [OperationContract]
        Session<Server.Entity.Analytic> LoadPriceLists(Session<Server.Entity.Analytic> session);
        [OperationContract]
        Session<Server.Entity.Analytic> SavePriceLists(Session<Server.Entity.Analytic> session);
    }
}
