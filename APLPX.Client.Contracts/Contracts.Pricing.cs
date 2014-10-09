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
        Session<List<Client.Entity.Pricing.Identity>> LoadList(Session<Client.Entity.NullT> session);
        [OperationContract]
        Session<Client.Entity.Pricing.Identity> SaveIdentity(Session<Client.Entity.Pricing.Identity> session);
    }
}
