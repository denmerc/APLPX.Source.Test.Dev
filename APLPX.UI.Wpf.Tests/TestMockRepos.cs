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
        [TestMethod]
        public void LoadList()
        {
            var repo = new MockAnalyticRepository();
            var list = repo.LoadList(new Client.Entity.Session<Client.Entity.NullT>());
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void LoadFilters()
        {
            var repo = new MockAnalyticRepository();

            var session = new Session<Analytic.Identity>();
            var id = new Analytic.Identity {  Id = 1};
            session.Data = id;
            var list = repo.LoadFilters(session);
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void LoadValueDrivers()
        {
            var repo = new MockAnalyticRepository();
            var session = new Session<Analytic.Identity>();
            var id = new Analytic.Identity { Id= 1 };
            session.Data = id;
            var list = repo.LoadDrivers(session);
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public void SaveValueDrivers()
        {
            var repo = new MockAnalyticRepository();
            var userRepo = new MockUserRepository();

            var user = new User
            {
                
                Identity = new UserIdentity(){ Login = "dmercado" }
                //, Role = Role.Administrator
                //, Password = ""
                
            };


            //get from list
            var list = repo.LoadList(new Client.Entity.Session<Client.Entity.NullT>());


            var i = list.Data.FirstOrDefault();

            var a = new Analytic();
            if( i != null)
            {
                a.Self = i;
            }


            var init = userRepo.Initialize(new Session<NullT>());
            var auth = userRepo.Authenticate(init);

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

            //var dataIn = auth.Clone(payload);
            a.Drivers = payload;

            Session<Analytic> packageIn = auth.Clone<Analytic>(a);



            //var list = repo.SaveDrivers(new Session<Analytic>() { Data = new Analytic { Drivers = drivers } });
            var response = repo.SaveValueDrivers(packageIn);
            
        }




    }
}
