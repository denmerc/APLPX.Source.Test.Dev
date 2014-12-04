using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Server.Entity;

namespace APLPX.Server.Services.Contracts
{
    [ServiceContract]
    public interface IPricingEverydayService
    {
        [OperationContract]
        Session<List<Server.Entity.PricingEveryday>> LoadList(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.PricingEveryday> SaveIdentity(Session<Server.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Server.Entity.PricingEveryday> LoadFilters(Session<Server.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Server.Entity.PricingEveryday> SaveFilters(Session<Server.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Server.Entity.PricingEveryday> LoadDrivers(Session<Server.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Server.Entity.PricingEveryday> SaveDrivers(Session<Server.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Server.Entity.PricingEveryday> LoadPriceLists(Session<Server.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Server.Entity.PricingEveryday> SavePriceLists(Session<Server.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Server.Entity.PricingEveryday> LoadResults(Session<Server.Entity.PricingEveryday> session);
    }
}
