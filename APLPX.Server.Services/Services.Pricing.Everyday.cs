using System.Collections.Generic;
using APLPX.Server.Data;
using APLPX.Server.Entity;
using APLPX.Server.Services.Contracts;

namespace APLPX.Server.Services
{
    public class PricingEverydayService : IPricingEverydayService
    {
        private IPricingEverydayData _pricingData;

        public PricingEverydayService() : this(new PricingEverydayData()) { }
        public PricingEverydayService(IPricingEverydayData pricingRepository) {
            this._pricingData = pricingRepository;
        }

        public Session<List<Server.Entity.PricingEveryday>> LoadList(Session<Server.Entity.NullT> sessionIn) {

            Session<List<Server.Entity.PricingEveryday>> sessionOut = _pricingData.LoadList(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.PricingEveryday> SaveIdentity(Session<Server.Entity.PricingEveryday> sessionIn) {
            Session<Server.Entity.PricingEveryday> sessionOut = _pricingData.SaveIdentity(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.PricingEveryday> LoadFilters(Session<Server.Entity.PricingEveryday> sessionIn) {
            Session<Server.Entity.PricingEveryday> sessionOut = _pricingData.LoadFilters(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.PricingEveryday> SaveFilters(Session<Server.Entity.PricingEveryday> sessionIn) {
            Session<Server.Entity.PricingEveryday> sessionOut = _pricingData.SaveFilters(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.PricingEveryday> SaveDrivers(Session<Server.Entity.PricingEveryday> sessionIn) {
            Session<Server.Entity.PricingEveryday> sessionOut = _pricingData.SaveDrivers(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.PricingEveryday> LoadDrivers(Session<Server.Entity.PricingEveryday> sessionIn) {
            Session<Server.Entity.PricingEveryday> sessionOut = _pricingData.LoadDrivers(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.PricingEveryday> LoadPriceLists(Session<Server.Entity.PricingEveryday> sessionIn) {
            Session<Server.Entity.PricingEveryday> sessionOut = _pricingData.LoadPriceLists(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.PricingEveryday> SavePriceLists(Session<Server.Entity.PricingEveryday> sessionIn) {
            Session<Server.Entity.PricingEveryday> sessionOut = _pricingData.SavePriceLists(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.PricingEveryday> LoadResults(Session<Server.Entity.PricingEveryday> sessionIn) {
            Session<Server.Entity.PricingEveryday> sessionOut = _pricingData.LoadResults(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

    }
}
