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
//using Domain = APLPX.Client.Display;
using APLPX.Client.Entity;
using APLPX.Client.Contracts;

namespace APLPX.UI.WPF.Data
{
    public class MockUserSevice : IUserService
    {

        public MockUserSevice()
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

        public MongoCollection<User> Users
        {
            get
            {
                return database.GetCollection<User>("User");
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

            var user = Users.AsQueryable().Where(x => x.Credential.Login == session.User.Credential.Login && x.Credential.OldPassword == session.User.Credential.OldPassword).ToList();
            if (user.Count > 0)
            {
                return new Session<NullT>()
                    { 
                        User = session.User,            
                        Modules = modules,
                        SessionOk = true
                    };
            }
            else
            {
                return new Session<NullT>()
                {
                    SessionOk = false
                };
            }
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



}
