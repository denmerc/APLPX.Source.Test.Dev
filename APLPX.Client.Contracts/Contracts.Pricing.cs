using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using APLPX.Client.Entity;

namespace APLPX.Client.Contracts
{
    [ServiceContract]
    public interface IPricingService
    {
        [OperationContract]
        Session<List<Client.Entity.Pricing>> LoadList(Session<Client.Entity.NullT> session);
        [OperationContract]
        Session<Client.Entity.Pricing> SaveIdentity(Session<Client.Entity.Pricing> session);
        [OperationContract]
        Session<Client.Entity.Pricing> LoadFilters(Session<Client.Entity.Pricing> session);
        [OperationContract]
        Session<Client.Entity.Pricing> SaveFilters(Session<Client.Entity.Pricing> session);
        [OperationContract]
        Session<Client.Entity.Pricing> LoadDrivers(Session<Client.Entity.Pricing> session);
        [OperationContract]
        Session<Client.Entity.Pricing> SaveDrivers(Session<Client.Entity.Pricing> session);
        [OperationContract]
        Session<Client.Entity.Pricing> LoadPriceLists(Session<Client.Entity.Pricing> session);
        [OperationContract]
        Session<Client.Entity.Pricing> SavePriceLists(Session<Client.Entity.Pricing> session);
    }
}
