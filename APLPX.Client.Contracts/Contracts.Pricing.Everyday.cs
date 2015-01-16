using System.Collections.Generic;
using System.ServiceModel;
//using APLPX.Client.Entity;
using APLPX.Entity;

namespace APLPX.Client.Contracts
{
    [ServiceContract]
    public interface IPricingEverydayService
    {
        [OperationContract]
        Session<List<PricingEveryday>> LoadList(Session<NullT> session);
        [OperationContract]
        Session<PricingEveryday> SaveIdentity(Session<PricingEveryday> session);
        [OperationContract]
        Session<PricingEveryday> LoadFilters(Session<PricingEveryday> session);
        [OperationContract]
        Session<PricingEveryday> SaveFilters(Session<PricingEveryday> session);
        [OperationContract]
        Session<PricingEveryday> LoadDrivers(Session<PricingEveryday> session);
        [OperationContract]
        Session<PricingEveryday> SaveDrivers(Session<PricingEveryday> session);
        [OperationContract]
        Session<PricingEveryday> LoadPriceLists(Session<PricingEveryday> session);
        [OperationContract]
        Session<PricingEveryday> SavePriceLists(Session<PricingEveryday> session);
        [OperationContract]
        Session<PricingEveryday> LoadResults(Session<PricingEveryday> session);
        [OperationContract]
        Session<PricingEveryday> LoadPricingEveryday(Session<PricingEveryday> session);
    }
}
