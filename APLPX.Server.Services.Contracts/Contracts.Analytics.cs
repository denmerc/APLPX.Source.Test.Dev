using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Server.Entity;

namespace APLPX.Server.Services.Contracts
{
    [ServiceContract]
    public interface IAnalyticService
    {

        [OperationContract]
        Session<List<Server.Entity.Analytic.Identity>> LoadList(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.Analytic.Identity> SaveIdentity(Session<Server.Entity.Analytic.Identity> session);
        [OperationContract]
        Session<List<Server.Entity.Filter>> LoadFilters(Session<Server.Entity.Analytic.Identity> session);
        [OperationContract]
        Session<List<Server.Entity.Filter>> SaveFilters(Session<Server.Entity.Analytic> session);
        [OperationContract]
        Session<List<Server.Entity.Analytic.Driver>> LoadDrivers(Session<Server.Entity.Analytic.Identity> session);
        [OperationContract]
        Session<List<Server.Entity.Analytic.Driver>> SaveDrivers(Session<Server.Entity.Analytic> session);
        [OperationContract]
        Session<List<Server.Entity.PriceList>> LoadPriceLists(Session<Server.Entity.Analytic.Identity> session);
        [OperationContract]
        Session<List<Server.Entity.PriceList>> SavePriceLists(Session<Server.Entity.Analytic> session);

    }
}
