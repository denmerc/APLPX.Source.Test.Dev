﻿using System;
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
        [TestInitialize]
        public void Setup()
        {

        }

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


        [TestMethod]
        public void SaveFilters()
        {
            var repo = new MockAnalyticRepository();
            var userRepo = new MockUserRepository();

            var user = new User
            {

                Identity = new UserIdentity() { Login = "dmercado" }
                //, Role = Role.Administrator
                //, Password = ""

            };

            var init = userRepo.Initialize(new Session<NullT>());
            var auth = userRepo.Authenticate(init);

            if (auth.SessionOk == false)
            {
                Assert.Fail("Login failed"); return;
            }

            //TODO: insert test analytic 
            //arrange - get from list
            var list = repo.LoadList(new Client.Entity.Session<Client.Entity.NullT>());

            var i = list.Data.FirstOrDefault();

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

            Session<Analytic> packageIn = auth.Clone<Analytic>(a);
            var response = repo.SaveFilters(packageIn);
        }

        [TestMethod]
        public void SaveIdentity()
        {
            //Setup -Initialize and auth
            var repo = new MockAnalyticRepository();
            var userRepo = new MockUserRepository();

            var user = new User
            {

                Identity = new UserIdentity() { Login = "dmercado" }
                //, Role = Role.Administrator
                //, Password = ""

            };



            var init = userRepo.Initialize(new Session<NullT>());
            var auth = userRepo.Authenticate(init);

            if (auth.SessionOk == false)
            {
                Assert.Fail("Login failed"); return;
            }

            //Arrange - get an analytic identity from list
            var list = repo.LoadList(new Client.Entity.Session<Client.Entity.NullT>());
            var ident = list.Data.FirstOrDefault();

            //Act
            ident.Name = "Sheet Metal & Body Panels Sale 2";
            ident.Description = "Sheet Metal & Body Panels Sale description 2";

            Session<Analytic.Identity> packageIn = auth.Clone<Analytic.Identity>(ident);
            var response = repo.SaveIdentity(packageIn);
        
        }
    }
}
