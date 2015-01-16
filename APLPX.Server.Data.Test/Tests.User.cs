using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using APLPX.Entity;
using APLPX.Server.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APLPX.Server.Data.Test
{
    [TestClass]
    public class User
    {
        private UserData _UserData;
        private System.Diagnostics.TraceListener listener;
        private static String lineBreak = new String('.', 200);
        private const String traceFile = @"\APLPX.Tests.Server.Data.log";
        private String SQLKEYSHARED = "72B9ED08-5D12-48FD-9CF7-56A3CA30E660"; //Shared application key
        private String SQLKEYPRIVATE = "9C8B31D8-ACD5-446A-912E-3019BAF05E6C"; //Private customer key
        private String dateStamp = System.DateTime.Now.ToLongDateString();
        private static String debugPath = System.IO.Directory.GetCurrentDirectory();
        private static System.IO.FileStream log = System.IO.File.Open(debugPath + traceFile, FileMode.Append, FileAccess.Write, FileShare.Write);

        [TestInitialize]
        public void Setup() {
            _UserData = new APLPX.Server.Data.UserData();
            this.listener = new TextWriterTraceListener(log);
            System.Diagnostics.Trace.Listeners.Add(listener);

            this.listener.WriteLine(String.Format("{0} {1} ms {2} - {3}", this.dateStamp, System.DateTime.Now.ToLongTimeString(), System.DateTime.Now.Millisecond.ToString(), this.GetType().Name));
            this.listener.WriteLine(lineBreak);
        }
        [TestCleanup]
        public void Cleanup() {
            this.listener.WriteLine(String.Format("{0} {1} ms {2} - {3}", this.dateStamp, System.DateTime.Now.ToLongTimeString(), System.DateTime.Now.Millisecond.ToString(), this.GetType().Name));
            this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak); this.listener.WriteLine(String.Empty); this.listener.WriteLine(String.Empty); this.listener.WriteLine(String.Empty);
            this.listener.Flush();
        }

        //Application initialization (test-pass)...
        [TestMethod, TestCategory("Application pass")]
        public void TEST01_GivenUserInitializesApplication_WhenRequestReceived_ThenSuccessStatusRecdAndSQLPrivateKeyRecd() {
            Session<NullT> response = _UserData.Initialize(new Session<NullT> { SqlKey = SQLKEYSHARED });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("SQL Private key: {0}", response.SqlKey)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ServerMessage, String.Empty);
                Assert.AreEqual(response.SqlKey, this.SQLKEYPRIVATE);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                Assert.IsTrue(false);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //SQL Server Authentication (test-pass)...
        [TestMethod, TestCategory("Authentication pass")]
        public void TEST02_GivenUserRequestsAuthentication_WhenApplicationInitialized_ThenSuccessStatusRecdAndSQLSessionKeyRecd() {
            Session<NullT> response = null;

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                response = _UserData.Initialize(new Session<NullT> { SqlKey = SQLKEYSHARED });
                this.listener.WriteLine(String.Format("Init Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Init Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Init Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Init SQL Private key: {0}", response.SqlKey)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ServerMessage, String.Empty);
                Assert.AreEqual(response.SqlKey, this.SQLKEYPRIVATE);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}, {1}", ex.Message, ex.Source)); this.listener.WriteLine(lineBreak);
                Assert.IsTrue(false);
            }

            try {
                //response.User = new APLPX.Entity.User(new APLPX.Entity.UserCredential("Administrator", "password")); //this user does not own anything
                response.User = new APLPX.Entity.User(new APLPX.Entity.UserCredential("admin", "password")); //admin user owns analytics and pricing objects
                //response.User = new APLPX.Entity.User(new APLPX.Entity.UserCredential("analyst", "password")); //analyst user owns analytics and pricing objects
                //response.User = new APLPX.Entity.User(new APLPX.Entity.UserCredential("approver", "password")); //approver user owns analytics and pricing objects
                response = _UserData.Authenticate(response);
                this.listener.WriteLine(String.Format("Authenticate User Greeting: {0}", response.User.Identity.Greeting)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate User Authenticated: {0}", (response.Authenticated) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate SQL Session key: {0}", response.SqlKey)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate User Role: {0}", response.User.Role.Name)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ServerMessage, String.Empty);
                Assert.IsNotNull(response.User.Role);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}, {1}", ex.Message, ex.Source)); this.listener.WriteLine(lineBreak);
                Assert.IsTrue(false);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //User change password...
        [TestMethod, TestCategory("Authentication pass")]
        public void TEST03_GivenUserRequestsChangePassword_WhenRequestValid_ThenSuccessStatusRecdAndPasswordChanged() {
            Session<NullT> request = null;
            Entity.UserCredential credential = null;

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            #region Initialization...
            try {
                request = _UserData.Initialize(new Session<NullT> { SqlKey = SQLKEYSHARED });
                this.listener.WriteLine(String.Format("Init Session valid: {0}", (request.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Init Client message: {0}", request.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Init Server message: {0}", request.ServerMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Init SQL Private key: {0}", request.SqlKey)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(request.SessionOk);
                Assert.AreEqual(request.ClientMessage, String.Empty);
                Assert.AreEqual(request.ServerMessage, String.Empty);
                Assert.AreEqual(request.SqlKey, this.SQLKEYPRIVATE);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}, {1}", ex.Message, ex.Source)); this.listener.WriteLine(lineBreak);
                Assert.IsTrue(false);
            }
            #endregion
            #region Authentication...
            try {
                request.User = new APLPX.Entity.User(new APLPX.Entity.UserCredential("admin", "password")); //admin user owns analytics and pricing objects
                request = _UserData.Authenticate(request);
                this.listener.WriteLine(String.Format("Authenticate User Greeting: {0}", request.User.Identity.Greeting)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate Session valid: {0}", (request.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate User Authenticated: {0}", (request.Authenticated) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate Client message: {0}", request.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate Server message: {0}", request.ServerMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate SQL Session key: {0}", request.SqlKey)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate User Role: {0}", request.User.Role.Name)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(request.SessionOk);
                Assert.AreEqual(request.ServerMessage, String.Empty);
                Assert.AreEqual(request.ClientMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}, {1}", ex.Message, ex.Source)); this.listener.WriteLine(lineBreak);
                Assert.IsTrue(false);
            }
            #endregion
            #region Change password...
            try {
                credential = new APLPX.Entity.UserCredential("admin", "password", "newPassword");
                request.User = new APLPX.Entity.User(request.User.Key, credential);
                request = _UserData.SavePassword(request);
                this.listener.WriteLine(String.Format("Authenticate Session valid: {0}", (request.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate Client message: {0}", request.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Authenticate Server message: {0}", request.ServerMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Change password: {0}", credential.NewPassword)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(request.SessionOk);
                Assert.AreEqual(request.ClientMessage, String.Empty);
                Assert.AreEqual(request.ServerMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}, {1}", ex.Message, ex.Source)); this.listener.WriteLine(lineBreak);
                Assert.IsTrue(false);
            }
            finally {
                if (request.SessionOk) {
                    credential = new APLPX.Entity.UserCredential("admin", "newPassword", "password");
                    request.User = new APLPX.Entity.User(request.User.Key, credential);
                    request = _UserData.SavePassword(request);
                    this.listener.WriteLine(String.Format("Authenticate Session valid: {0}", (request.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Authenticate Client message: {0}", request.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Authenticate Server message: {0}", request.ServerMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Restore password: {0}", credential.NewPassword)); this.listener.WriteLine(lineBreak);

                    Assert.IsTrue(request.SessionOk);
                    Assert.AreEqual(request.ClientMessage, String.Empty);
                    Assert.AreEqual(request.ServerMessage, String.Empty);
                }
            }
            #endregion
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(request.SessionOk);
        }

    }
}
