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

        public MongoCollection<Domain.Analytic> Analytics2
        {
            get
            {
                return database.GetCollection<Domain.Analytic>("analytics");
            }
        }


        public MongoCollection<Analytic> Analytics
        {
            get
            {
                return database.GetCollection<Analytic>("Analytics");
            }
        }

        //public List<Domain.Analytic> FindAnalyticsByTag(List<string> tags)
        //{

        //    //var list = Analytics.AsQueryable().Where(a => a.Tags.ContainsAny(tags)).Cast<T>().ToList(); //not supported

        //    return Analytics.AsQueryable().Where(a => a.Tags.ContainsAll(tags)).ToList();


        //}




        public Session<List<Analytic.Identity>> LoadList(Session<NullT> session)
        {

            var analytics = Analytics.AsQueryable().Select( x => x.Self).ToList();
            return new Session<List<Analytic.Identity>> { Data = analytics};


            //var identities = Analytics.AsQueryable().Select(x => new Analytic.Identity { Name = x. });
            //return new Session<List<Analytic.Identity>> { Data = identities.ToList() };
        }

        public Session<Analytic.Identity> SaveIdentity(Session<Analytic.Identity> session)
        {
            //Analytics.Save(session.Data);
            //return new Session<Analytic.Identity>();
            throw new NotImplementedException();

        }

        public Session<List<Filter>> LoadFilters(Session<Analytic.Identity> session)
        {
            //var identity = session.Data as Analytic.Identity;
            //var filters = Analytics.AsQueryable().Where(x => x.AnalyticId == identity.Id).SingleOrDefault().Filters
            //    .Select(f => new Filter
            //    {
            //        Name = f.Type,
            //        Values = f.Items.Select(x => new Filter.Value { Id = x._id, Code = x.Code, Name = x.Description }).ToList()
            //    }).ToList();

            var identity = session.Data as Analytic.Identity;
            var filters = Analytics.AsQueryable()
//.ToList();
                .Where(x => x.Self.Id == identity.Id).SingleOrDefault().Filters;

            return new Session<List<Filter>> { Data = filters };
            //return null;
            //throw new NotImplementedException();
        }

        public Session<List<Analytic.Driver>> LoadDrivers(Session<Analytic.Identity> session)
        {
            var identity = session.Data as Analytic.Identity;
            var drivers = Analytics.AsQueryable().Where(x => x.Self.Id == identity.Id).SingleOrDefault().Drivers.ToList();

            //var drivers = analytic.Drivers
            //    .Select((d,index)=>
                    
            //            new Analytic.Driver(index, index, Enum.GetName(typeof(Domain.ValueDriverType), d.Type), "Tooltip for Driver Type", false, 
            //                new List<Analytic.Driver.Mode>(){

            //                        new Analytic.Driver.Mode(1, Enum.GetName(typeof(Domain.Mode), Domain.Mode.Auto), "tooltip for auto group mode", false,
            //                                new List<Analytic.Driver.Mode.Group>{
            //                                    new Analytic.Driver.Mode.Group(
            //                                        d.Groups[0].LineItemId, 
            //                                        Convert.ToInt32(d.Groups[0].SalesValue),
            //                                        Convert.ToDecimal(d.Groups[0].Min),
            //                                        Convert.ToDecimal(d.Groups[0].Max)),   
            //                                }
            //                        ),
            //                        new Analytic.Driver.Mode(2, Enum.GetName(typeof(Domain.Mode), Domain.Mode.Manual), "tooltip for manual group mode", false,
            //                                new List<Analytic.Driver.Mode.Group>{
            //                                    new Analytic.Driver.Mode.Group(
            //                                        d.Groups[0].LineItemId, 
            //                                        Convert.ToInt32(d.Groups[0].SalesValue),
            //                                        Convert.ToDecimal(d.Groups[0].Min),
            //                                        Convert.ToDecimal(d.Groups[0].Max))
                                            
                                            
            //                                }
            //                        )
            //                }
            //           )

                    
            ////.Select(d => 
                    
            ////            new Analytic.Driver{ Name = Enum.GetName(typeof(Domain.ValueDriverType), d.Type)}
                    
            //    ).ToList();

            

            return new Session<List<Analytic.Driver>>(){ Data = drivers};

            //throw new NotImplementedException();


        }

        public Session<List<Filter>> SaveFilters(Session<Analytic> session)
        {
            //Analytics.Save(session.Data);
            //return new Session<List<Filter>>();

            throw new NotImplementedException();
        }


        public Session<List<Analytic.Driver>> SaveValueDrivers(Session<Analytic> session)
        {
           



            //var query = Query.And(
            //    Query.EQ("Analytic.Identity.Id", session.Data.Self.Id)
            //);

            //var sortBy = SortBy.Descending("Id");
            //var update = Update
            //    .Set("Drivers", session.Data.Drivers.ToBsonDocument<List<Analytic.Driver>>());

            //var args = new FindAndModifyArgs
            //{
            //    Query = query,
            //    SortBy = sortBy,
            //    Update = update
            //};


            //var r = Analytics.FindOneAs<Analytic>(query);

            var r = Analytics.AsQueryable().First(x =>);


            r.Drivers = session.Data.Drivers;

            Analytics.Save(r);

            //var result = Analytics.FindAndModify(args);

            //var chosenJob = result.ModifiedDocument;



            //Analytics.Save(session.Data);
            return new Session<List<Analytic.Driver>>();

        }

        //public void SaveDrivers(Session<Analytic.Identity> session)
        //{
        //    Analytics.Save(session.Data);
        //}
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
        //Session<List<Analytic.Driver>> SaveDrivers(Session<Analytic> session);        
        Session<List<Analytic.Driver>> SaveValueDrivers(Session<Analytic> session);


        //List<Domain.Analytic> FindAnalyticsByTag(List<string> tags);


    }

}
