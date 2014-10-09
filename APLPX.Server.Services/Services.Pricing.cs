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

        public Session<List<Server.Entity.Pricing.Identity>> LoadList(Session<Server.Entity.NullT> sessionIn) {
            Session<List<Server.Entity.Pricing.Identity>> sessionOut = _pricingData.LoadList(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Pricing.Identity> SaveIdentity(Session<Server.Entity.Pricing.Identity> sessionIn) {
            Session<Server.Entity.Pricing.Identity> sessionOut = _pricingData.SaveIdentity(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.Filter>> LoadFilters(Session<Server.Entity.Pricing.Identity> sessionIn) {
            Session<List<Server.Entity.Filter>> sessionOut = _pricingData.LoadFilters(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.Filter>> SaveFilters(Session<Server.Entity.Pricing> sessionIn) {
            Session<List<Server.Entity.Filter>> sessionOut = _pricingData.SaveFilters(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.Pricing.Driver>> SaveDrivers(Session<Server.Entity.Pricing> sessionIn) {
            Session<List<Server.Entity.Pricing.Driver>> sessionOut = _pricingData.SaveDrivers(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.Pricing.Driver>> LoadDrivers(Session<Server.Entity.Pricing.Identity> sessionIn) {
            Session<List<Server.Entity.Pricing.Driver>> sessionOut = _pricingData.LoadDrivers(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.PriceList>> LoadPriceLists(Session<Server.Entity.Pricing.Identity> sessionIn) {
            Session<List<Server.Entity.PriceList>> sessionOut = _pricingData.LoadPriceLists(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.PriceList>> SavePriceLists(Session<Server.Entity.Pricing> sessionIn) {
            Session<List<Server.Entity.PriceList>> sessionOut = _pricingData.SavePriceLists(sessionIn);
            _pricingData.Dispose();

            return sessionOut;
        }

    }
}
