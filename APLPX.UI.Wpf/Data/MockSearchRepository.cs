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
using Domain = APLPX.Client.Entity;

namespace APLPX.Server.Data
{
    public class MockSearchRepository : ISearchRepository
    {
        public List<string> AllTagsBySubModule(string subModule)
        {
            List<string> list = new List<string>();
           //var query = from a in Analytics.AsQueryable<Domain.Analytic>()
           //        select a.Tags.Distinct();

           //return query.ToList();

            switch (subModule)
            {
                case "Analytics":
                        var query = Analytics.Distinct(

                        "Tags"

                        );

                       foreach (var item in query.ToList())
                       {
                           list.Add(item.ToString());
                       }
                       return list;

                case "Everyday":
                case "Promotions":
                case "Kits":
                       var pquery = PriceRoutines.Distinct(

                       "Tags"

                       );

                       foreach (var item in pquery.ToList())
                       {
                           list.Add(item.ToString());
                       }
                       return list;
                default: return null;
            }
        }


        public void SaveFolders(Domain.FolderSet folderSet)
        {
            var all = Folders.AsQueryable().First();

            all.SelectedAnalyticFolders = folderSet.SelectedAnalyticFolders;
            all.SelectedEverydayFolders = folderSet.SelectedEverydayFolders;
            all.SelectedKitFolders = all.SelectedKitFolders;
            all.SelectedPromotionFolders = all.SelectedPromotionFolders;
            Folders.Save(all);
        }

        public Domain.FolderSet AllFolderSets()
        {
            try
            {
              
              return Folders.AsQueryable().First();

            }
            catch (Exception)
            {
                
                //throw;
            }
            return null;
            
        }

        public List<string> AllFoldersBySubModule(string subModule)
        {
            List<string> list = new List<string>();
            //var query = from a in Analytics.AsQueryable<Domain.Analytic>()
            //        select a.Tags.Distinct();

            //return query.ToList();

            switch (subModule)
            {
                case "Analytics":
                    return Folders.AsQueryable().First().SelectedAnalyticFolders.ToList();
                case "Everyday":
                    return Folders.AsQueryable().First().SelectedEverydayFolders.ToList();

                case "Promotions":
                    return Folders.AsQueryable().First().SelectedPromotionFolders.ToList();

                case "Kits":
                    return Folders.AsQueryable().First().SelectedKitFolders.ToList();

                default: return null;
            }
        }
        public List<Domain.Analytic> FindAnalyticsByTag(List<string> tags)
        {

            //var list = Analytics.AsQueryable().Where(a => a.Tags.ContainsAny(tags)).Cast<T>().ToList(); //not supported

            return Analytics.AsQueryable().Where(a => a.Tags.ContainsAll(tags)).ToList();
  
            
        }

        public List<Domain.PriceRoutine> FindPricingByTag(List<string> tags) 
        {

            //var list = Analytics.AsQueryable().Where(a => a.Tags.ContainsAny(tags)).Cast<T>().ToList(); //not supported

            return PriceRoutines.AsQueryable().Where(a => a.Tags.ContainsAll(tags)).ToList();


        }
        public List<T> FindByTag<T>(List<string> tags) where T : class, new()
        {
            var name = typeof(T).Name;
            //Type typeParameterType = typeof(T);
            switch (name)
            {
                case "Analytic":
                    var list = Analytics.AsQueryable().Where(a => a.Tags.ContainsAll(tags)).Cast<T>().ToList();
                    return list;
                default:
                    return null;
            }
        }

        public List<string> FindFavoriteTagsByUserAndSubModule(string login, Domain.SubModuleType type)
        {
            try
            {
                switch (type)
                {
                    case Domain.SubModuleType.Analytics:
                        return Users.AsQueryable().First(u => u.Login == login).FavoriteATags.ToList();
                    case Domain.SubModuleType.Everyday:
                    case Domain.SubModuleType.Promotions:
                    case Domain.SubModuleType.Kits:
                        return Users.AsQueryable().First(u => u.Login == login).FavoritePTags.ToList();
                    
                    //case Domain.SubModuleType.MySettings:
                    //    break;
                    //case Domain.SubModuleType.Search:
                    //    break;
                    default:
                        return null;
                }

            }
            catch (Exception) //user does not exist - usually b/c cant make connection
            {
                
                throw;
            }
        }


               public void Dispose()
        {
            throw new NotImplementedException();
        }

        private readonly string connectionString = ConfigurationManager.AppSettings["connectionString"].ToString();
        private const string databaseName = "promo";

        private MongoClient client { get; set; }
        protected MongoServer server { get; set; }
        protected MongoDatabase database { get; set; }

        public MockSearchRepository()
        {
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(databaseName);
        }

        public MongoCollection<Domain.Filter> Filters
        {
            get
            {
                return database.GetCollection <Domain.Filter> ("filters");
            }
        }

        public MongoCollection<Domain.Analytic> Analytics
        {
            get
            {
                return database.GetCollection<Domain.Analytic>("analytics");
            }
        }

        public MongoCollection<Domain.PriceRoutine> PriceRoutines
        {
            get
            {
                return database.GetCollection<Domain.PriceRoutine>("PriceRoutines");
            }
        }

        public MongoCollection<Domain.User> Users
        {
            get
            {
                return database.GetCollection<Domain.User>("Users");
            }
        }


        public MongoCollection<Domain.FolderSet> Folders
        {
            get
            {
                return database.GetCollection<Domain.FolderSet>("Folders");
            }
        }

        public MongoCollection<Domain.Actions> Actions
        {
            get
            {
                return database.GetCollection<Domain.Actions>("options");
            }
        }

        public MongoCollection<BsonDocument> ActionsAsDocuments
        {
            get
            {
                return database.GetCollection("options");
            }
        }
        public MongoCollection<Domain.Action> Commands
        {
            get
            {
                return database.GetCollection<Domain.Action>("commands");
            }
        }

        //public List<string> AllTags()
        //{
        //    List<string> list = new List<string>();
        //   //var query = from a in Analytics.AsQueryable<Domain.Analytic>()
        //   //        select a.Tags.Distinct();

        //   //return query.ToList();


        //    var query = Analytics.Distinct(

        //    "Tags"

        //    );

        //   foreach (var item in query.ToList())
        //   {
        //       list.Add(item.ToString());
        //   }
        //   return list;
        //}
        //private const string TagsCollectionName = "tags";
        //public MongoDatabase Database;
        //public List<Domain.Analytic> FindByTag(List<string> tags)
        //{ 
        //    //var found = All<Domain.Analytic>().Where
        //   return Analytics.AsQueryable().Where(a => a.Tags.ContainsAll(tags)).ToList();

        //}

    }

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


    }

    public class MockPricingRepository : IPricingRepository
    {


        public MockPricingRepository()
        {
            client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(databaseName);
        }



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
        //public List<Domain.Analytic> FindByTag(List<string> tags)
        //{
        //    //var found = All<Domain.Analytic>().Where
        //    return Analytics.AsQueryable().Where(a => a.Tags.ContainsAll(tags)).ToList();

        //}
        //public List<Domain.PriceRoutine> FindPricingByTag(List<string> tags)
        //{

        //    //var list = Analytics.AsQueryable().Where(a => a.Tags.ContainsAny(tags)).Cast<T>().ToList(); //not supported

        //    return PriceRoutines.AsQueryable().Where(a => a.Tags.ContainsAll(tags)).ToList();


        //}

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

        public MongoCollection<Domain.PriceRoutine> PriceRoutines
        {
            get
            {
                return database.GetCollection<Domain.PriceRoutine>("PriceRoutines");
            }
        }

        public MongoCollection<Domain.Actions> Actions
        {
            get
            {
                return database.GetCollection<Domain.Actions>("options");
            }
        }

        public MongoCollection<BsonDocument> ActionsAsDocuments
        {
            get
            {
                return database.GetCollection("options");
            }
        }
        public MongoCollection<Domain.Action> Commands
        {
            get
            {
                return database.GetCollection<Domain.Action>("commands");
            }
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

    }


    public interface ISearchRepository : IDisposable
    {
        void SaveFolders(Domain.FolderSet folderSet);
        Domain.FolderSet AllFolderSets();

        List<string> AllFoldersBySubModule(string subModule);
        List<T> FindByTag<T>(List<string> tags) where T : class, new();
        List<Domain.Analytic> FindAnalyticsByTag(List<string> tags);

        List<Domain.PriceRoutine> FindPricingByTag(List<string> tags);
        //List<string> AllTags();

        List<string> AllTagsBySubModule(string subModule);

        List<string> FindFavoriteTagsByUserAndSubModule(string login, Domain.SubModuleType type);
        
        //void Save<T>(T item) where T : class, new();
        //void Add<T>(T item) where T : class, new();
        //void Delete<T>(T item) where T : class, new();
        //T Single<T>() where T : class, new();
        //System.Linq.IQueryable<T> All<T>() where T : class, new();
        //System.Linq.IQueryable<T> All<T>(int page, int pageSize) where T : class, new();
    }

    public interface IUserRepository : IDisposable
    {
        List<string> FindFavoriteTagsByUser(string userName);
        Domain.Session Login(string userName);
        Boolean Logoff(string userName);

    }


    public interface IAnalyticRepository : IDisposable
    {
        void Save<T>(T item) where T : class, new();
        void Add<T>(T item) where T : class, new();
        void Delete<T>(T item) where T : class, new();
        T Single<T>() where T : class, new();
        System.Linq.IQueryable<T> All<T>() where T : class, new();
        System.Linq.IQueryable<T> All<T>(int page, int pageSize) where T : class, new();

        //List<Domain.Analytic> FindAnalyticsByTag(List<string> tags);


    }

    public interface IPricingRepository : IDisposable
    {
        void Save<T>(T item) where T : class, new();
        void Add<T>(T item) where T : class, new();
        void Delete<T>(T item) where T : class, new();
        T Single<T>() where T : class, new();
        System.Linq.IQueryable<T> All<T>() where T : class, new();
        System.Linq.IQueryable<T> All<T>(int page, int pageSize) where T : class, new();

        //List<Domain.PriceRoutine> FindPricingByTag(List<string> tags);

    }
}
