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
using APLPX.Client.Contracts;

namespace APLPX.UI.WPF.Data
{
    public class MockUserRepository : IUserService
    {

        public MockUserRepository()
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


        public MongoCollection<Module> Modules
        {
            get
            {
                return database.GetCollection<Module>("Modules");
            }
        }


        public Session<NullT> Initialize(Session<NullT> session)
        {
            return new Session<NullT>();
        }

        public Session<NullT> Authenticate(Session<NullT> session)
        {
            var modules = Modules.AsQueryable().ToList();
            return new Session<NullT>()
                { 
                    User = session.User,            
                    Modules = modules
                };
        }

        public Session<UserIdentity> LoadIdentity(Session<UserIdentity> session)
        {
            throw new NotImplementedException();
        }

        public Session<UserIdentity> SaveIdentity(Session<UserIdentity> session)
        {
            throw new NotImplementedException();
        }

        public Session<NullT> SavePassword(Session<NullT> session)
        {
            throw new NotImplementedException();
        }


        public Session<List<User>> LoadList(Session<NullT> session)
        {
            throw new NotImplementedException();
        }

        public Session<User> LoadUser(Session<User> session)
        {
            throw new NotImplementedException();
        }

        public Session<User> SaveUser(Session<User> session)
        {
            throw new NotImplementedException();
        }
    }



    public interface IUserRepository : IDisposable
    {
        
        Session<NullT> Initialize(Session<NullT> session);
        Session<NullT> Authenticate(Session<NullT> session);
        Session<NullT> LoadExplorerPlanning(Session<NullT> session);
        Session<NullT> LoadExplorerTracking(Session<NullT> session);
        Session<NullT> LoadExplorerReporting(Session<NullT> session);
        Session<UserIdentity> LoadList(Session<NullT> session);
        Session<UserIdentity> LoadIdentity(Session<UserIdentity> session);
        Session<UserIdentity> SaveIdentity(Session<UserIdentity> session);
        Session<NullT> SavePassword(Session<NullT> session);


        //List<Domain.Analytic> FindAnalyticsByTag(List<string> tags);


    }

}
