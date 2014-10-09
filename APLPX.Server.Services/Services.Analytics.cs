using System.Collections.Generic;
using APLPX.Server.Data;
using APLPX.Server.Entity;
using APLPX.Server.Services.Contracts;

namespace APLPX.Server.Services
{
    public class AnalyticService : IAnalyticService
    {
        private IAnalyticData _analyticData;

        public AnalyticService() : this(new AnalyticData()) { }
        public AnalyticService(IAnalyticData analyticRepository)
        { 
            this._analyticData = analyticRepository;
        }

        public Session<List<Server.Entity.Analytic.Identity>> LoadList(Session<Server.Entity.NullT> sessionIn) {
            Session<List<Server.Entity.Analytic.Identity>> sessionOut = _analyticData.LoadList(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Analytic.Identity> SaveIdentity(Session<Server.Entity.Analytic.Identity> sessionIn)
        {
            Session<Server.Entity.Analytic.Identity> sessionOut = _analyticData.SaveIdentity(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.Filter>> LoadFilters(Session<Server.Entity.Analytic.Identity> sessionIn)
        {
            Session<List<Server.Entity.Filter>> sessionOut = _analyticData.LoadFilters(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.Filter>> SaveFilters(Session<Server.Entity.Analytic> sessionIn)
        {
            Session<List<Server.Entity.Filter>> sessionOut = _analyticData.SaveFilters(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.Analytic.Driver>> SaveDrivers(Session<Server.Entity.Analytic> sessionIn)
        {
            Session<List<Server.Entity.Analytic.Driver>> sessionOut = _analyticData.SaveDrivers(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.Analytic.Driver>> LoadDrivers(Session<Server.Entity.Analytic.Identity> sessionIn)
        {
            Session<List<Server.Entity.Analytic.Driver>> sessionOut = _analyticData.LoadDrivers(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.PriceList>> LoadPriceLists(Session<Server.Entity.Analytic.Identity> sessionIn) 
        {
            Session<List<Server.Entity.PriceList>> sessionOut = _analyticData.LoadPriceLists(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<List<Server.Entity.PriceList>> SavePriceLists(Session<Server.Entity.Analytic> sessionIn) 
        {
            Session<List<Server.Entity.PriceList>> sessionOut = _analyticData.SavePriceLists(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }


    }
}
