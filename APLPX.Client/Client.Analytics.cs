using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using APLPX.Client.Contracts;

//using APLPX.Client.Entity;
using APLPX.Entity;
using Newtonsoft.Json;
using System.Reflection;

namespace APLPX.Client
{



    [Export]
    public class AnalyticClient : ClientBase<IAnalyticService>, IAnalyticService
    {
        public Session<List<Analytic>> LoadList(Session<NullT> session)
        {
             var response = Channel.LoadList(session);
             this.LogClientRequest<IAnalyticService, NullT>(session, string.Format( "[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
             return response;
        }

        public Session<Analytic> SaveIdentity(Session<Analytic> session)
        {
            var response = Channel.SaveIdentity(session);
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format( "[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<Analytic> LoadFilters(Session<Analytic> session)
        {
            var response = Channel.LoadFilters(session);
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format( "[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<Analytic> SaveFilters(Session<Analytic> session)
        {
            var response =  Channel.SaveFilters(session);
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;

        }

        //public Session<Analytic> LoadDriver(Session<Analytic> session) {
        //    return Channel.LoadDriver(session);
        //}

        public Session<Analytic> LoadDrivers(Session<Analytic> session)
        {
            var response = Channel.LoadDrivers(session);
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        //public Session<Analytic> SaveDriver(Session<Analytic> session) {
        //    return Channel.SaveDriver(session);
        //}

        public Session<Analytic> SaveDrivers(Session<Analytic> session)
        {
            var response = Channel.SaveDrivers(session);
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<Analytic> LoadPriceLists(Session<Analytic> session) 
        {
            var response = Channel.LoadPriceLists(session);
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<Analytic> SavePriceLists(Session<Analytic> session) 
        {
            var response = Channel.SavePriceLists(session);
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }

        public Session<Analytic> LoadAnalytic(Session<Analytic> session)
        {
            var response = Channel.Load(session);
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return response;
        }


        public Session<Analytic> LoadResults(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            throw new System.NotImplementedException();
        }

        public Session<Analytic> Load(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            return Channel.Load(session);
        }

        public Session<Analytic> LoadIdentity(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            throw new System.NotImplementedException();
        }

        public Session<Analytic> RunDrivers(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, string.Format("[{0}].{1}", this.GetType().Name, MethodBase.GetCurrentMethod().ToString()));
            throw new System.NotImplementedException();
        }


    }


}
