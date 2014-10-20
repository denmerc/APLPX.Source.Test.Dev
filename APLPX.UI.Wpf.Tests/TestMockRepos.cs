using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APLX.UI.WPF.Data;
using APLPX.Client.Entity;
using System.Collections.Generic;

namespace APLPX.UI.Wpf.Tests
{

    [TestClass]
    public class TestMockRepos
    {
        private MockAnalyticRepository RepoA = new MockAnalyticRepository();
        private MockUserRepository RepoU = new MockUserRepository();
        private User User;
        private Session<NullT> InitSession;
        private Session<NullT> AuthSession;

        [TestInitialize]
        public void Setup()
        {
            //Setup -Initialize and auth
            //TODO: insert test analytic 

            RepoA.InsertSampleAnalytic();


            User = new User
            {

                Identity = new UserIdentity() { Login = "dmercado" }
                //, Role = Role.Administrator
                //, Password = ""

            };

            InitSession = RepoU.Initialize(new Session<NullT>());
            AuthSession = RepoU.Authenticate(InitSession);

            if (AuthSession.SessionOk == false)
            {
                Assert.Fail("Login failed"); return;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            RepoA.RemoveSampleAnalytic();
        }


        [TestMethod]
        public void InsertMockAnalytic()
        {
            RepoA.InsertSampleAnalytic();
        }

        [TestMethod]
        public void RemoveMockAnalytic()
        {
            RepoA.RemoveSampleAnalytic();

        }

        [TestMethod]
        public void LoadList()
        {
            var list = RepoA.LoadList(new Client.Entity.Session<Client.Entity.NullT>());
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public void LoadFilters()
        {

            var session = new Session<Analytic.Identity>();
            var id = new Analytic.Identity {  Id = 3};
            session.Data = id;
            var list = RepoA.LoadFilters(session);
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public void LoadValueDrivers()
        {

            var session = new Session<Analytic.Identity>();
            var id = new Analytic.Identity { Id = 3 };
            session.Data = id;
            var list = RepoA.LoadDrivers(session);
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public void SaveValueDrivers()
        {

            //Arrange - get analytic 2 from list
            var list = RepoA.LoadList(new Client.Entity.Session<Client.Entity.NullT>());
            var i = list.Data.FirstOrDefault(ident => ident.Id == 3);

            var a = new Analytic();
            if( i != null)
            {
                a.Self = i;
            }


            var init = RepoU.Initialize(new Session<NullT>());
            var auth = RepoU.Authenticate(init);

            if(auth.SessionOk == false)
            {
                Assert.Fail("Login failed"); return;
            }

            var payload = new List<Analytic.Driver>()
            {
                new Analytic.Driver(1,1,"DriverName1", 
                    "DriverTooltip1", false, 
                        new List<Client.Entity.Analytic.Driver.Mode>()
                    )
                
            };

            a.Drivers = payload;
            Session<Analytic> packageIn = AuthSession.Clone<Analytic>(a);
            var response = RepoA.SaveValueDrivers(packageIn);
            
        }


        [TestMethod]
        public void SaveFilters()
        {
 
            //Arrange - get from list
            var list = RepoA.LoadList(new Client.Entity.Session<Client.Entity.NullT>());

            var i = list.Data.FirstOrDefault(ident => ident.Id == 3);

            var a = new Analytic();
            if (i != null)
            {
                a.Self = i;
            }

            var payload = new List<Filter>()
            {
                new Filter
                {
                     Name = "Discount Type",
                     Values = new List<Value>()
                     {
                         new Value(0,1000,"00", "Missing", true),
                         new Value(1,1111, "11", "Not Missing", true)
                     }
                }
                
            };

            a.Filters = payload;

            Session<Analytic> packageIn = AuthSession.Clone<Analytic>(a);
            var response = RepoA.SaveFilters(packageIn);

            //Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void SaveIdentity()
        {

            //Arrange - get an analytic identity from list
            var list = RepoA.LoadList(new Client.Entity.Session<Client.Entity.NullT>());
            var ident = list.Data.FirstOrDefault(id => id.Id == 3);

            //Act
            ident.Name = "Sheet Metal & Body Panels Sale 2";
            ident.Description = "Sheet Metal & Body Panels Sale description 2";

            Session<Analytic.Identity> packageIn = AuthSession.Clone<Analytic.Identity>(ident);
            var response = RepoA.SaveIdentity(packageIn);
        
            //Assert
            Assert.IsNotNull(response);
        }
    }
}
