using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Server.Entity;

namespace APLPX.Server.Services.Contracts
{
    [ServiceContract]
    public interface IPricingService
    {

        [OperationContract]
        Session<List<Server.Entity.Pricing.Identity>> LoadList(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.Pricing.Identity> SaveIdentity(Session<Server.Entity.Pricing.Identity> session);
        [OperationContract]
        Session<List<Server.Entity.Filter>> LoadFilters(Session<Server.Entity.Pricing.Identity> session);
        [OperationContract]
        Session<List<Server.Entity.Filter>> SaveFilters(Session<Server.Entity.Pricing> session);
        [OperationContract]
        Session<List<Server.Entity.Pricing.Driver>> LoadDrivers(Session<Server.Entity.Pricing.Identity> session);
        [OperationContract]
        Session<List<Server.Entity.Pricing.Driver>> SaveDrivers(Session<Server.Entity.Pricing> session);
        [OperationContract]
        Session<List<Server.Entity.PriceList>> LoadPriceLists(Session<Server.Entity.Pricing.Identity> session);
        [OperationContract]
        Session<List<Server.Entity.PriceList>> SavePriceLists(Session<Server.Entity.Pricing> session);

    }
}
