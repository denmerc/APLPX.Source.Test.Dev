using System.ServiceModel;
using System.Collections.Generic;
using APLPX.Server.Data;
using APLPX.Server.Entity;
using APLPX.Server.Services.Contracts;

namespace APLPX.Server.Services
{
    [ServiceBehavior(UseSynchronizationContext = true, InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class AnalyticService : IAnalyticService
    {
        private IAnalyticData _analyticData;

        public AnalyticService() : this(new AnalyticData()) { }
        public AnalyticService(IAnalyticData analyticRepository)
        { 
            this._analyticData = analyticRepository;
        }

        public Session<List<Server.Entity.Analytic>> LoadList(Session<Server.Entity.NullT> sessionIn) {
            Session<List<Server.Entity.Analytic>> sessionOut = _analyticData.LoadList(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> SaveIdentity(Session<Server.Entity.Analytic> sessionIn)
        {
            Session<Server.Entity.Analytic> sessionOut = _analyticData.SaveIdentity(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> LoadFilters(Session<Server.Entity.Analytic> sessionIn)
        {
            Session<Server.Entity.Analytic> sessionOut = _analyticData.LoadFilters(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> SaveFilters(Session<Server.Entity.Analytic> sessionIn)
        {
            Session<Server.Entity.Analytic> sessionOut = _analyticData.SaveFilters(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> LoadDriver(Session<Server.Entity.Analytic> sessionIn) {

            Session<Server.Entity.Analytic> sessionOut = _analyticData.LoadDrivers(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> LoadDrivers(Session<Server.Entity.Analytic> sessionIn) {

            Session<Server.Entity.Analytic> sessionOut = _analyticData.LoadDrivers(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> SaveDriver(Session<Server.Entity.Analytic> sessionIn) {
            Session<Server.Entity.Analytic> sessionOut = _analyticData.SaveDrivers(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> SaveDrivers(Session<Server.Entity.Analytic> sessionIn)
        {
            Session<Server.Entity.Analytic> sessionOut = _analyticData.SaveDrivers(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> LoadPriceLists(Session<Server.Entity.Analytic> sessionIn) 
        {
            Session<Server.Entity.Analytic> sessionOut = _analyticData.LoadPriceLists(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> SavePriceLists(Session<Server.Entity.Analytic> sessionIn) 
        {
            Session<Server.Entity.Analytic> sessionOut = _analyticData.SavePriceLists(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }
    }
}
