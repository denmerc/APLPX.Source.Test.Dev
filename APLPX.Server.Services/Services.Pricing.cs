using System.Collections.Generic;
using APLPX.Server.Data;
using APLPX.Server.Entity;
using APLPX.Server.Services.Contracts;

namespace APLPX.Server.Services
{
    public class PricingService : IPricingService
    {
        private IPricingData _pricingData;

        public PricingService() : this(new PricingData()) {}
        public PricingService(IPricingData pricingRepository)
        { 
            this._pricingData = pricingRepository;
        }

        public Session<List<Server.Entity.Pricing>> LoadList(Session<Server.Entity.NullT> sessionIn) {

            Session<List<Server.Entity.Pricing>> sessionOut = _pricingData.LoadList(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> SaveIdentity(Session<Server.Entity.Pricing> sessionIn) {
            Session<Server.Entity.Pricing> sessionOut = _pricingData.SaveIdentity(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> LoadFilters(Session<Server.Entity.Pricing> sessionIn) {
            Session<Server.Entity.Pricing> sessionOut = _pricingData.LoadFilters(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> SaveFilters(Session<Server.Entity.Pricing> sessionIn) {
            Session <Server.Entity.Pricing> sessionOut = _pricingData.SaveFilters(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> SaveDrivers(Session<Server.Entity.Pricing> sessionIn) {
            Session<Server.Entity.Pricing> sessionOut = _pricingData.SaveDrivers(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> LoadDrivers(Session<Server.Entity.Pricing> sessionIn) {
            Session<Server.Entity.Pricing> sessionOut = _pricingData.LoadDrivers(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> LoadPriceLists(Session<Server.Entity.Pricing> sessionIn) {
            Session<Server.Entity.Pricing> sessionOut = _pricingData.LoadPriceLists(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> SavePriceLists(Session<Server.Entity.Pricing> sessionIn) {
            Session<Server.Entity.Pricing> sessionOut = _pricingData.SavePriceLists(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

    }
}
