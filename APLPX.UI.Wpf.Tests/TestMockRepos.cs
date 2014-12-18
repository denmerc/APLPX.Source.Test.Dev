using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ENTITY = APLPX.Client.Entity;
using SERVICES = APLPX.UI.WPF.Data; //APLPX.Mock.Services;

using System.Collections.Generic;


namespace APLPX.UI.Wpf.Tests
{

    [TestClass]
    public class MockTests
    {
        private SERVICES.MockAnalyticService AnalyticService = new SERVICES.MockAnalyticService();

        private SERVICES.MockUserService UserService = new SERVICES.MockUserService();
        //private User User;
        //private Session<NullT> InitSession;
        //private Session<NullT> AuthSession;


        //[TestInitialize]
        //public void Setup()
        //{
        //    //Setup -Initialize and auth
        //    //TODO: insert test analytic 

        //    RepoA.InsertSampleAnalytic();


        //    User = new User
        //    (1,

        //        new UserIdentity("dmercado", "dmercado@apl.com", "Den", "Mercado", true)


        //    );

        //    InitSession = RepoU.Initialize(new Session<NullT>());
        //    AuthSession = RepoU.Authenticate(InitSession);

        //    if (AuthSession.SessionOk == false)
        //    {
        //        Assert.Fail("Login failed"); return;
        //    }
        //}

        //[TestCleanup]
        //public void Cleanup()
        //{
        //    RepoA.RemoveSampleAnalytic();
        //}

        //[TestMethod]
        //public void SerializeModulesIntoTestData()
        //{
        //}



        //[TestMethod, Ignore]
        //public void InsertMockAnalytic()
        //{
        //    RepoA.InsertSampleAnalytic();
        //}

        //[TestMethod, Ignore]
        //public void RemoveMockAnalytic()
        //{
        //    RepoA.RemoveSampleAnalytic();

        //}

        [TestMethod]
        public void LoadAnalyticList()
        {
            var list = AnalyticService.LoadList(new ENTITY.Session<ENTITY.NullT>());
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void RunPriceRoutine()
        {
            var list = AnalyticService.RunPricing(1);
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void Load_Analytic()
        {
            var id = new ENTITY.Analytic(3);
            var response = AnalyticService.LoadAnalytic(new ENTITY.Session<ENTITY.Analytic>() { Data = id });
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);
        }


        [TestMethod]
        public void LoadAnalyticFilters()
        {
            var id = new ENTITY.Analytic(3);

            var response = AnalyticService.LoadFilters(new ENTITY.Session<ENTITY.Analytic>() { Data = id});
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        public void Load_Analytic_Drivers()
        {
            var id = new ENTITY.Analytic(3);

            var response = AnalyticService.LoadDrivers(new ENTITY.Session<ENTITY.Analytic>() { Data = id });
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        public void Load_Analytic_PriceLists()
        {
            var id = new ENTITY.Analytic(3);

            var response = AnalyticService.LoadPriceLists(new ENTITY.Session<ENTITY.Analytic>() { Data = id });
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Data);
        }

        [TestMethod]
        public void LoadModulesByAuthenticating()
        {
            //var list = AnalyticService.LoadModules(new Client.Entity.Session<Client.Entity.NullT>());
            //Assert.IsNotNull(list);

            var session = new ENTITY.Session<ENTITY.NullT>
            {
                User = new ENTITY.User(2, new ENTITY.UserIdentity
                                                                    ("dave.jinkerson@advancedpricinglogic.com", 
                                                                    "Analyst", "User", true))};
            var modules =  UserService.Authenticate(session);

            Assert.IsNotNull(modules);

        }

        //[TestMethod]
        //public void LoadFilters()
        //{

        //    var session = new Session<Analytic>();
        //    var id = new Analytic(3);
        //    session.Data = id;
        //    var list = RepoA.LoadFilters(session);
        //    Assert.IsNotNull(list);

        //}

        //[TestMethod]
        //public void LoadValueDrivers()
        //{

        //    var session = new Session<Analytic>();
        //    var id = new Analytic(3);
        //    session.Data = id;
        //    var list = RepoA.LoadDrivers(session);
        //    Assert.IsNotNull(list);

        //}

        //[TestMethod]
        //public void SaveValueDrivers()
        //{

        //    //Arrange - get analytic 2 from list
        //    var list = RepoA.LoadList(new Client.Entity.Session<Client.Entity.NullT>());
        //    var i = list.Data.FirstOrDefault(an => an.Id == 3);




        //    var init = RepoU.Initialize(new Session<NullT>());
        //    var auth = RepoU.Authenticate(init);

        //    if(auth.SessionOk == false)
        //    {
        //        Assert.Fail("Login failed"); return;
        //    }

        //    var payload = new List<AnalyticDriver>()
        //    {
        //        new AnalyticDriver(1,1,"DriverName1", 
        //            "DriverTooltip1", 1,false, 
        //                new List<Client.Entity.AnalyticDriverMode>()
        //            )
                
        //    };

        //    var a = new Analytic(i.Id,payload);
            
        //    Session<Analytic> packageIn = AuthSession.Clone<Analytic>(a);
        //    var response = RepoA.SaveValueDrivers(packageIn);
            
        //}


        //[TestMethod]
        //public void SaveFilters()
        //{
 
        //    //Arrange - get from list
        //    var list = RepoA.LoadList(new Client.Entity.Session<Client.Entity.NullT>());

        //    var i = list.Data.FirstOrDefault(ident => ident.Id == 3);


        //    var payload = new List<FilterGroup>()
        //    {
        //        new FilterGroup("Discount Type", new List<Filter> 
        //        {
        //            new Filter(0, 0 , "Code 0", "Name 0", true),
        //            new Filter(0, 1, "Code 1", "Name 1",true)
        //        })
                
        //    };

        //    var a = new Analytic(i.Id,payload);
        //    Session<Analytic> packageIn = AuthSession.Clone<Analytic>(a);
        //    var response = RepoA.SaveFilters(packageIn);

        //    //Assert
        //    Assert.IsNotNull(response);
        //}

        //[TestMethod]
        //public void SaveIdentity()
        //{

        //    //Arrange
        //    var list = RepoA.LoadList(new Client.Entity.Session<Client.Entity.NullT>());
        //    var a = list.Data.FirstOrDefault(id => id.Id == 3);

        //    //Act
        //    var updatedA = new Analytic(a.Id, new AnalyticIdentity(Name: "Sheet Metal & Body Panels Sale 2", 
        //                                    Description: "Sheet Metal & Body Panels Sale description 2", Notes: "blah,blah", Active: true));

        //    Session<Analytic> packageIn = AuthSession.Clone<Analytic>(updatedA);
        //    var response = RepoA.SaveIdentity(packageIn);
        
        //    //Assert
        //    Assert.IsNotNull(response);
        //}
    }
}
