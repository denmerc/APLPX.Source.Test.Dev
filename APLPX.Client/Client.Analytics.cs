using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using APLPX.Client.Contracts;

//using APLPX.Client.Entity;
using APLPX.Entity;

namespace APLPX.Client
{
    [Export]
    public class AnalyticClient : ClientBase<IAnalyticService>, IAnalyticService
    {
        public Session<List<Analytic>> LoadList(Session<NullT> session)
        {
            return Channel.LoadList(session);
        }

        public Session<Analytic> SaveIdentity(Session<Analytic> session)
        {
            return Channel.SaveIdentity(session);
        }

        public Session<Analytic> LoadFilters(Session<Analytic> session)
        {
            return Channel.LoadFilters(session);
        }

        public Session<Analytic> SaveFilters(Session<Analytic> session)
        {
            return Channel.SaveFilters(session);
        }

        //public Session<Analytic> LoadDriver(Session<Analytic> session) {
        //    return Channel.LoadDriver(session);
        //}

        public Session<Analytic> LoadDrivers(Session<Analytic> session)
        {
            return Channel.LoadDrivers(session);
        }

        //public Session<Analytic> SaveDriver(Session<Analytic> session) {
        //    return Channel.SaveDriver(session);
        //}

        public Session<Analytic> SaveDrivers(Session<Analytic> session)
        {
            return Channel.SaveDrivers(session);
        }

        public Session<Analytic> LoadPriceLists(Session<Analytic> session) 
        {
            return Channel.LoadPriceLists(session);
        }

        public Session<Analytic> SavePriceLists(Session<Analytic> session) 
        {
            return Channel.SavePriceLists(session);
        }

        public Session<Analytic> LoadAnalytic(Session<Analytic> session)
        {
            return Channel.Load(session);
        }


        public Session<Analytic> LoadResults(Session<Analytic> session)
        {
            throw new System.NotImplementedException();
        }

        public Session<Analytic> Load(Session<Analytic> session)
        {
            return Channel.Load(session);
        }

        public Session<Analytic> LoadIdentity(Session<Analytic> session)
        {
            throw new System.NotImplementedException();
        }

        public Session<Analytic> RunDrivers(Session<Analytic> session)
        {
            throw new System.NotImplementedException();
        }
    }
}
