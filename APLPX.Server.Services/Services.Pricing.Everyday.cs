using System.Collections.Generic;
using APLPX.Entity;
using APLPX.Server.Data;
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

        public Session<List<Entity.PricingEveryday>> LoadList(Session<Entity.NullT> sessionIn) {

            Session<List<Entity.PricingEveryday>> sessionOut = _pricingData.LoadList(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> SaveIdentity(Session<Entity.PricingEveryday> sessionIn) {
            Session<Entity.PricingEveryday> sessionOut = _pricingData.SaveIdentity(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> LoadFilters(Session<Entity.PricingEveryday> sessionIn) {
            Session<Entity.PricingEveryday> sessionOut = _pricingData.LoadFilters(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> SaveFilters(Session<Entity.PricingEveryday> sessionIn) {
            Session<Entity.PricingEveryday> sessionOut = _pricingData.SaveFilters(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> SaveDrivers(Session<Entity.PricingEveryday> sessionIn) {
            Session<Entity.PricingEveryday> sessionOut = _pricingData.SaveDrivers(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> LoadDrivers(Session<Entity.PricingEveryday> sessionIn) {
            Session<Entity.PricingEveryday> sessionOut = _pricingData.LoadDrivers(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> LoadPriceLists(Session<Entity.PricingEveryday> sessionIn) {
            Session<Entity.PricingEveryday> sessionOut = _pricingData.LoadPriceLists(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> SavePriceLists(Session<Entity.PricingEveryday> sessionIn) {
            Session<Entity.PricingEveryday> sessionOut = _pricingData.SavePriceLists(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> LoadResults(Session<Entity.PricingEveryday> sessionIn) {
            Session<Entity.PricingEveryday> sessionOut = _pricingData.LoadResults(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

    }
}
