using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using APLPX.Client.Entity;
using APLPX.Client.Contracts;

namespace APLPX.Client
{
    [Export]
    public class PricingEverydayClient : ClientBase<IPricingEverydayService>, IPricingEverydayService
    {
        public Session<List<Client.Entity.PricingEveryday>> LoadList(Session<Client.Entity.NullT> session) {

            return Channel.LoadList(session);
        }

        public Session<Client.Entity.PricingEveryday> SaveIdentity(Session<Client.Entity.PricingEveryday> session) {

            return Channel.SaveIdentity(session);
        }

        public Session<Client.Entity.PricingEveryday> LoadFilters(Session<Client.Entity.PricingEveryday> session) {

            return Channel.LoadFilters(session);
        }

        public Session<Client.Entity.PricingEveryday> SaveFilters(Session<Client.Entity.PricingEveryday> session) {

            return Channel.SaveFilters(session);
        }

        public Session<Client.Entity.PricingEveryday> SaveDrivers(Session<Client.Entity.PricingEveryday> session) {

            return Channel.SaveDrivers(session);
        }

        public Session<Client.Entity.PricingEveryday> LoadDrivers(Session<Client.Entity.PricingEveryday> session) {

            return Channel.LoadDrivers(session);
        }

        public Session<Client.Entity.PricingEveryday> LoadPriceLists(Session<Client.Entity.PricingEveryday> session) {

            return Channel.LoadPriceLists(session);
        }

        public Session<Client.Entity.PricingEveryday> SavePriceLists(Session<Client.Entity.PricingEveryday> session) {

            return Channel.SavePriceLists(session);
        }

        public Session<Client.Entity.PricingEveryday> LoadResults(Session<Client.Entity.PricingEveryday> session) {

            return Channel.LoadResults(session);
        }

        public Session<Client.Entity.PricingEveryday> LoadPricingEveryday(Session<Client.Entity.PricingEveryday> session)
        {

            return Channel.LoadPricingEveryday(session);
        }
    }
}
