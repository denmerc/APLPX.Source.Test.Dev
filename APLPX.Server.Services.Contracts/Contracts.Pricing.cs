using System.Collections.Generic;
using System.ServiceModel;
using APLPX.Server.Entity;

namespace APLPX.Server.Services.Contracts
{
    [ServiceContract]
    public interface IPricingService
    {
        [OperationContract]
        Session<List<Server.Entity.Pricing>> LoadList(Session<Server.Entity.NullT> session);
        [OperationContract]
        Session<Server.Entity.Pricing> SaveIdentity(Session<Server.Entity.Pricing> session);
        [OperationContract]
        Session<Server.Entity.Pricing> LoadFilters(Session<Server.Entity.Pricing> session);
        [OperationContract]
        Session<Server.Entity.Pricing> SaveFilters(Session<Server.Entity.Pricing> session);
        [OperationContract]
        Session<Server.Entity.Pricing> LoadDrivers(Session<Server.Entity.Pricing> session);
        [OperationContract]
        Session<Server.Entity.Pricing> SaveDrivers(Session<Server.Entity.Pricing> session);
        [OperationContract]
        Session<Server.Entity.Pricing> LoadPriceLists(Session<Server.Entity.Pricing> session);
        [OperationContract]
        Session<Server.Entity.Pricing> SavePriceLists(Session<Server.Entity.Pricing> session);
    }
}
