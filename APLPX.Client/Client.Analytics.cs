using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using APLPX.Client.Entity;
using APLPX.Client.Contracts;

namespace APLPX.Client
{
    [Export]
    public class AnalyticClient : ClientBase<IAnalyticService>, IAnalyticService
    {
        public Session<List<Client.Entity.Analytic>> LoadList(Session<Client.Entity.NullT> session)
        {
            return Channel.LoadList(session);
        }

        public Session<Client.Entity.Analytic> SaveIdentity(Session<Analytic> session)
        {
            return Channel.SaveIdentity(session);
        }

        public Session<Client.Entity.Analytic> LoadFilters(Session<Client.Entity.Analytic> session)
        {
            return Channel.LoadFilters(session);
        }

        public Session<Client.Entity.Analytic> SaveFilters(Session<Client.Entity.Analytic> session)
        {
            return Channel.SaveFilters(session);
        }

        public Session<Client.Entity.Analytic> LoadDriver(Session<Client.Entity.Analytic> session) {
            return Channel.LoadDriver(session);
        }

        public Session<Client.Entity.Analytic> LoadDrivers(Session<Client.Entity.Analytic> session)
        {
            return Channel.LoadDrivers(session);
        }

        public Session<Client.Entity.Analytic> SaveDriver(Session<Client.Entity.Analytic> session) {
            return Channel.SaveDriver(session);
        }

        public Session<Client.Entity.Analytic> SaveDrivers(Session<Client.Entity.Analytic> session)
        {
            return Channel.SaveDrivers(session);
        }

        public Session<Client.Entity.Analytic> LoadPriceLists(Session<Client.Entity.Analytic> session) 
        {
            return Channel.LoadPriceLists(session);
        }

        public Session<Client.Entity.Analytic> SavePriceLists(Session<Client.Entity.Analytic> session) 
        {
            return Channel.SavePriceLists(session);
        }

        public Session<Client.Entity.Analytic> LoadAnalytic(Session<Client.Entity.Analytic> session)
        {
            return Channel.LoadAnalytic(session);
        }


        public Session<Analytic> LoadResults(Session<Analytic> session)
        {
            throw new System.NotImplementedException();
        }
    }
}
