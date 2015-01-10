using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Client.Entity;

namespace APLPX.Client.Contracts
{
    [ServiceContract]
    public interface IPricingEverydayService
    {
        [OperationContract]
        Session<List<Client.Entity.PricingEveryday>> LoadList(Session<Client.Entity.NullT> session);
        [OperationContract]
        Session<Client.Entity.PricingEveryday> SaveIdentity(Session<Client.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Client.Entity.PricingEveryday> LoadFilters(Session<Client.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Client.Entity.PricingEveryday> SaveFilters(Session<Client.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Client.Entity.PricingEveryday> LoadDrivers(Session<Client.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Client.Entity.PricingEveryday> SaveDrivers(Session<Client.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Client.Entity.PricingEveryday> LoadPriceLists(Session<Client.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Client.Entity.PricingEveryday> SavePriceLists(Session<Client.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Client.Entity.PricingEveryday> LoadResults(Session<Client.Entity.PricingEveryday> session);
        [OperationContract]
        Session<Client.Entity.PricingEveryday> LoadPricingEveryday(Session<Client.Entity.PricingEveryday> session);
    }
}
