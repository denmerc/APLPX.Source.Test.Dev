using System.ServiceModel;
using System.Collections.Generic;
using APLPX.Entity;
using APLPX.Server.Data;
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

        public Session<Entity.Analytic> Load(Session<Entity.Analytic> sessionIn) {
            Session<Entity.Analytic> sessionOut = _analyticData.Load(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<List<Entity.Analytic>> LoadList(Session<Entity.NullT> sessionIn) {
            Session<List<Entity.Analytic>> sessionOut = _analyticData.LoadList(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Entity.Analytic> LoadIdentity(Session<Entity.Analytic> sessionIn) {
            Session<Entity.Analytic> sessionOut = _analyticData.LoadIdentity(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Entity.Analytic> SaveIdentity(Session<Entity.Analytic> sessionIn)
        {
            Session<Entity.Analytic> sessionOut = _analyticData.SaveIdentity(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Entity.Analytic> LoadFilters(Session<Entity.Analytic> sessionIn)
        {
            Session<Entity.Analytic> sessionOut = _analyticData.LoadFilters(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Entity.Analytic> SaveFilters(Session<Entity.Analytic> sessionIn)
        {
            Session<Entity.Analytic> sessionOut = _analyticData.SaveFilters(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Entity.Analytic> LoadDrivers(Session<Entity.Analytic> sessionIn) {

            Session<Entity.Analytic> sessionOut = _analyticData.LoadDrivers(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Entity.Analytic> SaveDrivers(Session<Entity.Analytic> sessionIn)
        {
            Session<Entity.Analytic> sessionOut = _analyticData.SaveDrivers(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Entity.Analytic> RunDrivers(Session<Entity.Analytic> sessionIn) {
            Session<Entity.Analytic> sessionOut = _analyticData.SaveDrivers(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Entity.Analytic> LoadPriceLists(Session<Entity.Analytic> sessionIn) 
        {
            Session<Entity.Analytic> sessionOut = _analyticData.LoadPriceLists(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }

        public Session<Entity.Analytic> SavePriceLists(Session<Entity.Analytic> sessionIn) 
        {
            Session<Entity.Analytic> sessionOut = _analyticData.SavePriceLists(sessionIn);
            _analyticData.Dispose();

            return sessionOut;
        }
    }
}
