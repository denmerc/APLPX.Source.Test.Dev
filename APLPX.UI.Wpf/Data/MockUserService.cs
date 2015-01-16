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
using APLPX.Entity;
using APLPX.Client.Contracts;

namespace APLPX.UI.WPF.Data
{
    public class MockUserService : IUserService
    {

        public MockUserService()
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
                return database.GetCollection<Module>("AllModules_Role");
            }
        }

        public MongoCollection<SessionList> Sessions
        {
            get
            {
                return database.GetCollection<SessionList>("Sessions");
            }
        }

        public List<FilterGroup> FilterGroups
        {
            get
            {
                return database.GetCollection<FilterGroup>("FilterGroups").AsQueryable().ToList();
            }
        }


        public Session<NullT> Initialize(Session<NullT> session)
        {
            return new Session<NullT>();
        }

        public Session<NullT> Authenticate(Session<NullT> session)
        {
            
            try
            {
                
                var user = Users.AsQueryable()
                    .Where(x => x.Credential.Login == session.User.Credential.Login 
                                && x.Credential.OldPassword == session.User.Credential.OldPassword)
                                .FirstOrDefault();
                if (user == null)
                {
                    return new Session<NullT>()
                    {
                        SessionOk = false
                    };
                }

                //var qLicMods = Query.ElemMatch("Roles", Query.EQ("Id", 3));
                //var qLicFeatures = Query.ElemMatch("Features.Roles" , Query.EQ( "Id" , 3));
                //var q = Query.And(new IMongoQuery[] {qLicFeatures, qLicMods });
                //var lModsandFeats = Modules.Find(qLicFeatures);
                
                //find  session based on 
                var owner = user.Identity.FirstName + " " + user.Identity.LastName;
                var s = Sessions.AsQueryable().Where(x => x.Owner == owner).FirstOrDefault();

                //var fg = FilterGroups.AsQueryable().ToList();
                

                //var modules = Modules.AsQueryable().ToList();
              
                //var licensedMods = from m in modules
                //                   where m.Roles.Any(r => r.Id == user.Role.Id)
                //                         select new Module
                //                         {
                //                            Type = m.Type,
                //                            Name =  m.Name,
                //                            Title = m.Title,
                //                            Sort = m.Sort,
                //                            Roles = m.Roles,
                //                            Features = m.Features.Where( fe => fe.Roles.Any( r => r.Id == user.Role.Id)).ToList()
                //                         };

                return new Session<NullT>()
                    { 
                        User = session.User,
                        Modules = s.Modules,
                        Analytics = s.Analytics,
                        Pricing = s.Pricing,
                        FilterGroups = null,
                        //Mo<pdules = licensedMods.ToList(),
                        //Modules = lModsandFeats.ToList(), 
                        SessionOk = true
                    };


            }
            catch (Exception)
            {

                return new Session<NullT>()
                {
                    SessionOk = false,
                    ClientMessage = "Failed to connect."
                };
            }
            
            //var mods = Modules.AsQueryable().SelectMany( m=> m.Features).SelectMany( f => f.Roles).Where( r => r.Id == user.Role.Id);
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
