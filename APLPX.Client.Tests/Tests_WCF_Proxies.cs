using Microsoft.VisualStudio.TestTools.UnitTesting;
using APLPX.Client.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APLPX.Client;

namespace APLPX.Server.Services.Tests
{
    [TestClass]
    public class ProxyTests
    {
        private String SQLKEYSHARED = "72B9ED08-5D12-48FD-9CF7-56A3CA30E660"; //Shared application key
        private String SQLKEYPRIVATE = "9C8B31D8-ACD5-446A-912E-3019BAF05E6C"; //Private customer key
        private String SQLKEYAPLADMIN = "45F2AE12-1428-481E-8A87-43566914B91A"; //APL Administrator

        
        
        [TestMethod]
        public void test_wcf_user_client_initialize()
        {
            UserClient proxy = new UserClient();
            proxy.Open();

            Session<NullT> response = proxy.Initialize(new Session<NullT> { SqlKey = SQLKEYSHARED });

            Assert.IsTrue(response.SessionOk);

        }
        
        
        
        [TestMethod]
        public void test_wcf_user_client_authenticate()
        {
            UserClient proxy = new UserClient();
            proxy.Open();


            var session = new Session<NullT>
            {
                User = new User(
                                        2,
                                        "UserKey",
                                        new UserRole(3, "Administrator", "Role description"),
                                        new UserIdentity("dave.jinkerson@advancedpricinglogic.com", "Analyst", "User", true),
                                        new UserCredential("admin", "password", null),
                                        new List<SQLEnumeration>()

                                   )
            };
            var s = proxy.Authenticate(session);




            //Session<NullT> response = proxy.Initialize(new Session<NullT> { SqlKey = SQLKEYSHARED });
            //response.User.Identity = new User.Identity();
            //response.User.Identity.Login = "Administrator";
            //response.User.Identity.Password = new APLPX.Client.Entity.User.Password { Old = "password" };

            //var authenticated = proxy.Authenticate(response);

            //var authenticated2 = proxy.Authenticate2(response);

            proxy.Close();

            //Assert.AreEqual(authenticated, authenticated2);
            Assert.IsTrue(s.SessionOk);
            //Assert.IsTrue(authenticated.SessionOk);
            //Assert.AreEqual(response.ServerMessage, String.Empty);
            //Assert.AreEqual(response.SqlKey, this.SQLKEYPRIVATE);

        }



        [TestMethod]
        public void test_analytic_client_loadlist()
        {
            AnalyticClient proxy = new AnalyticClient();
            proxy.Open();
            
            
            //TODO: catch faults to handle reconnect
            //ie. subscribe to proxy.InnerChannel.Faulted
            Session<List<Analytic>> response = proxy.LoadList(new Session<NullT> { SqlKey = SQLKEYAPLADMIN });

            proxy.Close();


            Assert.IsTrue(response.SessionOk);
            //Assert.AreEqual(response.ServerMessage, String.Empty);
            //Assert.AreEqual(response.SqlKey, this.SQLKEYPRIVATE);
        }


        [TestMethod]
        public void test_wcf_user_client_loaduser()
        {
            UserClient proxy = new UserClient();
            proxy.Open();

            Session<User> response = proxy.LoadUser(new Session<User> { SqlKey = SQLKEYSHARED, Data = new User { Id = 1 } });

            Assert.IsTrue(response.SessionOk);

        }

        [TestMethod, Ignore]
        public void test_wcf_user_client_saveuser()
        {
            UserClient proxy = new UserClient();
            proxy.Open();

            Session<User> response = proxy.SaveUser(new Session<User> { SqlKey = SQLKEYSHARED });

            Assert.IsTrue(response.SessionOk);

        }

        [TestMethod, Ignore]
        public void test_wcf_user_client_savepassword()
        {
            UserClient proxy = new UserClient();
            proxy.Open();

            Session<NullT> response = proxy.SavePassword(new Session<NullT> { SqlKey = SQLKEYSHARED });

            Assert.IsTrue(response.SessionOk);

        }

    }
}
