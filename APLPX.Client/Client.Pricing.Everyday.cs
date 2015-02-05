using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using APLPX.Client.Contracts;
//using APLPX.Client.Entity;
using APLPX.Entity;
using System.Reflection;

namespace APLPX.Client
{
    [Export]
    public class PricingEverydayClient : ClientBase<IPricingEverydayService>, IPricingEverydayService
    {
        public Session<List<PricingEveryday>> LoadList(Session<NullT> session) {

            var response = Channel.LoadList(session);
            this.LogClientRequest<IPricingEverydayService, NullT>(session,  string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString())); 
            return response;
        }

        public Session<PricingEveryday> SaveIdentity(Session<PricingEveryday> session) {

            var response = Channel.SaveIdentity(session);
            this.LogClientRequest<IPricingEverydayService, PricingEveryday>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<PricingEveryday> LoadFilters(Session<PricingEveryday> session) {

            var response = Channel.LoadFilters(session);
            this.LogClientRequest<IPricingEverydayService, PricingEveryday>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<PricingEveryday> SaveFilters(Session<PricingEveryday> session) {

            var response = Channel.SaveFilters(session);
            this.LogClientRequest<IPricingEverydayService, PricingEveryday>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<PricingEveryday> SaveDrivers(Session<PricingEveryday> session) {

            var response = Channel.SaveDrivers(session);
            this.LogClientRequest<IPricingEverydayService, PricingEveryday>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;            
        }

        public Session<PricingEveryday> LoadDrivers(Session<PricingEveryday> session) {
            var response = Channel.LoadDrivers(session);
            this.LogClientRequest<IPricingEverydayService, PricingEveryday>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<PricingEveryday> LoadPriceLists(Session<PricingEveryday> session) {

            var response = Channel.LoadPriceLists(session);
            this.LogClientRequest<IPricingEverydayService, PricingEveryday>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<PricingEveryday> SavePriceLists(Session<PricingEveryday> session) {
            var response  = Channel.SavePriceLists(session);
            this.LogClientRequest<IPricingEverydayService, PricingEveryday>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<PricingEveryday> LoadResults(Session<PricingEveryday> session) {


            var response = Channel.LoadResults(session);
            this.LogClientRequest<IPricingEverydayService, PricingEveryday>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<PricingEveryday> LoadPricingEveryday(Session<PricingEveryday> session)
        {
            var response = Channel.LoadPricingEveryday(session);
            this.LogClientRequest<IPricingEverydayService, PricingEveryday>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }
    }
}
