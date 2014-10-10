using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Configuration;
using MongoDB.Driver.Builders;
using Domain = APLPX.Client.Display;
using APLPX.Client.Entity;

namespace APLX.UI.WPF.Data
{
    public class MockAnalyticRepository : IAnalyticRepository
    {


        public void Save<T>(T item) where T : class, new()
        {
            Analytics.Save(item);
        }

        public void Add<T>(T item) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T item) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public T Single<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> All<T>(int page, int pageSize) where T : class, new()
        {
            throw new NotImplementedException();
        }


        public MockAnalyticRepository()
        {
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(databaseName);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private readonly string connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
        private const string databaseName = "promo";
        //private const string TagsCollectionName = "tags";
        //public MongoDatabase Database;

        private MongoClient client { get; set; }
        protected MongoServer server { get; set; }
        protected MongoDatabase database { get; set; }
        public MongoCollection<Domain.Filter> Filters
        {
            get
            {
                return database.GetCollection<Domain.Filter>("filters");
            }
        }

        public MongoCollection<Domain.Analytic> Analytics
        {
            get
            {
                return database.GetCollection<Domain.Analytic>("analytics");
            }
        }


        //public List<Domain.Analytic> FindAnalyticsByTag(List<string> tags)
        //{

        //    //var list = Analytics.AsQueryable().Where(a => a.Tags.ContainsAny(tags)).Cast<T>().ToList(); //not supported

        //    return Analytics.AsQueryable().Where(a => a.Tags.ContainsAll(tags)).ToList();


        //}




        public Session<List<Analytic.Identity>> LoadList(Session<NullT> session)
        {
            var identities = Analytics.AsQueryable().Select(x => new Analytic.Identity { Name = x.Name });
            return new Session<List<Analytic.Identity>> { Data = identities.ToList() };
        }

        public Session<Analytic.Identity> SaveIdentity(Session<Analytic.Identity> session)
        {
            throw new NotImplementedException();
        }

        public Session<List<Filter>> LoadFilters(Session<Analytic.Identity> session)
        {
            throw new NotImplementedException();
            //var identity = session.Data as Analytic.Identity;
            //var filters = Analytics.AsQueryable().Where(x => x.Id == identity.Id.ToString()).SingleOrDefault().Filters
            //    .Select( f => new Filter{Name = f.Type, Values = f.Items.Select( x => new Filter.Value{ Id = x.Id.To} ;
        }

        public Session<List<Filter>> SaveFilters(Session<Analytic> session)
        {
            throw new NotImplementedException();
        }

        public Session<List<Analytic.Driver>> LoadDrivers(Session<Analytic.Identity> session)
        {
            throw new NotImplementedException();
        }

        public Session<List<Analytic.Driver>> SaveDrivers(Session<Analytic> session)
        {
            throw new NotImplementedException();
        }
    }



    public interface IAnalyticRepository : IDisposable
    {
        void Save<T>(T item) where T : class, new();
        void Add<T>(T item) where T : class, new();
        void Delete<T>(T item) where T : class, new();
        T Single<T>() where T : class, new();
        System.Linq.IQueryable<T> All<T>() where T : class, new();
        System.Linq.IQueryable<T> All<T>(int page, int pageSize) where T : class, new();

        Session<List<Analytic.Identity>> LoadList(Session<NullT> session);
        Session<Analytic.Identity> SaveIdentity(Session<Analytic.Identity> session);
        Session<List<Filter>> LoadFilters(Session<Analytic.Identity> session);
        Session<List<Filter>> SaveFilters(Session<Analytic> session);
        Session<List<Analytic.Driver>> LoadDrivers(Session<Analytic.Identity> session);
        Session<List<Analytic.Driver>> SaveDrivers(Session<Analytic> session);


        //List<Domain.Analytic> FindAnalyticsByTag(List<string> tags);


    }

}
