using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Entity;

namespace APLPX.Server.Services.Contracts
{
    [ServiceContract]
    public interface IPricingEverydayService
    {
        [OperationContract]
        Session<List<Entity.PricingEveryday>> LoadList(Session<Entity.NullT> session);
        [OperationContract]
        Session<Entity.PricingEveryday> SaveIdentity(Session<Entity.PricingEveryday> session);
        [OperationContract]
        Session<Entity.PricingEveryday> LoadFilters(Session<Entity.PricingEveryday> session);
        [OperationContract]
        Session<Entity.PricingEveryday> SaveFilters(Session<Entity.PricingEveryday> session);
        [OperationContract]
        Session<Entity.PricingEveryday> LoadDrivers(Session<Entity.PricingEveryday> session);
        [OperationContract]
        Session<Entity.PricingEveryday> SaveDrivers(Session<Entity.PricingEveryday> session);
        [OperationContract]
        Session<Entity.PricingEveryday> LoadPriceLists(Session<Entity.PricingEveryday> session);
        [OperationContract]
        Session<Entity.PricingEveryday> SavePriceLists(Session<Entity.PricingEveryday> session);
        [OperationContract]
        Session<Entity.PricingEveryday> LoadResults(Session<Entity.PricingEveryday> session);
    }
}
