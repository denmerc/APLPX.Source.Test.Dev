using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using APLPX.Client.Contracts;
//using APLPX.Client.Entity;
using APLPX.Entity;

namespace APLPX.Client
{
    [Export]
    public class PricingEverydayClient : ClientBase<IPricingEverydayService>, IPricingEverydayService
    {
        public Session<List<PricingEveryday>> LoadList(Session<NullT> session) {

            return Channel.LoadList(session);
        }

        public Session<PricingEveryday> SaveIdentity(Session<PricingEveryday> session) {

            return Channel.SaveIdentity(session);
        }

        public Session<PricingEveryday> LoadFilters(Session<PricingEveryday> session) {

            return Channel.LoadFilters(session);
        }

        public Session<PricingEveryday> SaveFilters(Session<PricingEveryday> session) {

            return Channel.SaveFilters(session);
        }

        public Session<PricingEveryday> SaveDrivers(Session<PricingEveryday> session) {

            return Channel.SaveDrivers(session);
        }

        public Session<PricingEveryday> LoadDrivers(Session<PricingEveryday> session) {

            return Channel.LoadDrivers(session);
        }

        public Session<PricingEveryday> LoadPriceLists(Session<PricingEveryday> session) {

            return Channel.LoadPriceLists(session);
        }

        public Session<PricingEveryday> SavePriceLists(Session<PricingEveryday> session) {

            return Channel.SavePriceLists(session);
        }

        public Session<PricingEveryday> LoadResults(Session<PricingEveryday> session) {

            return Channel.LoadResults(session);
        }

        public Session<PricingEveryday> LoadPricingEveryday(Session<PricingEveryday> session)
        {

            return Channel.LoadPricingEveryday(session);
        }
    }
}
