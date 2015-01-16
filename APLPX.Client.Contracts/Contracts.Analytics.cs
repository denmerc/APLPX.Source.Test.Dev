using System.Collections.Generic;
using System.ServiceModel;
//using APLPX.Client.Entity;
using APLPX.Entity;
namespace APLPX.Client.Contracts
{
    [ServiceContract]
    public interface IAnalyticService
    {
        [OperationContract]
        Session<Entity.Analytic> Load(Session<Entity.Analytic> session);
        [OperationContract]
        Session<List<Analytic>> LoadList(Session<NullT> session);
        [OperationContract]
        Session<Entity.Analytic> LoadIdentity(Session<Entity.Analytic> session);
        [OperationContract]
        Session<Analytic> SaveIdentity(Session<Analytic> session);
        [OperationContract]
        Session<Analytic> LoadFilters(Session<Analytic> session);
        [OperationContract]
        Session<Analytic> SaveFilters(Session<Analytic> session);
        //[OperationContract]
        //Session<Analytic> LoadDriver(Session<Analytic> session);
        [OperationContract]
        Session<Analytic> LoadDrivers(Session<Analytic> session);
        //[OperationContract]
        //Session<Analytic> SaveDriver(Session<Analytic> session);
        [OperationContract]
        Session<Analytic> SaveDrivers(Session<Analytic> session);
        [OperationContract]
        Session<Entity.Analytic> RunDrivers(Session<Entity.Analytic> session);
        [OperationContract]
        Session<Analytic> LoadPriceLists(Session<Analytic> session);
        [OperationContract]
        Session<Analytic> SavePriceLists(Session<Analytic> session);

        //[OperationContract]
        //Session<Analytic> LoadAnalytic(Session<Analytic> session);
        //[OperationContract]
        //Session<Analytic> LoadResults(Session<Analytic> session);

    }
}
