using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Entity;

namespace APLPX.Server.Services.Contracts
{
    [ServiceContract]
    public interface IAnalyticService
    {
        [OperationContract]
        Session<Entity.Analytic> Load(Session<Entity.Analytic> session);
        [OperationContract]
        Session<List<Entity.Analytic>> LoadList(Session<Entity.NullT> session);
        [OperationContract]
        Session<Entity.Analytic> LoadIdentity(Session<Entity.Analytic> session);
        [OperationContract]
        Session<Entity.Analytic> SaveIdentity(Session<Entity.Analytic> session);
        [OperationContract]
        Session<Entity.Analytic> LoadFilters(Session<Entity.Analytic> session);
        [OperationContract]
        Session<Entity.Analytic> SaveFilters(Session<Entity.Analytic> session);
        [OperationContract]
        Session<Entity.Analytic> LoadDrivers(Session<Entity.Analytic> session);
        [OperationContract]
        Session<Entity.Analytic> SaveDrivers(Session<Entity.Analytic> session);
        [OperationContract]
        Session<Entity.Analytic> LoadPriceLists(Session<Entity.Analytic> session);
        [OperationContract]
        Session<Entity.Analytic> SavePriceLists(Session<Entity.Analytic> session);
    }
}
