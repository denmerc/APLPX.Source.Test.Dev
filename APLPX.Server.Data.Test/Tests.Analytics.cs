using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using APLPX.Entity;
using APLPX.Server.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APLPX.Tests.Server.Data {


    [TestClass]
    public class Analytics
    {
        #region Properties...
        private AnalyticData _AnalyticData;
        private System.Diagnostics.TraceListener listener;
        private String dateStamp = System.DateTime.Now.ToLongDateString();
        private String timeStamp = System.DateTime.Now.ToLongTimeString();
        private String uniqueStamp = String.Format(" {0}, {1}", System.DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
        #endregion
        #region Constants...
        private const String traceFile = @"\APLPX.Tests.Server.Data.log";
        private const String SQLKEYINVALID = "00000000-0000-0000-0000-000000000000";
        private const String SQLKEYADMIN = "9F8A3400-CF1B-4D0D-B157-DEF9C105BE35"; //Admin
        private const String SQLKEYANALYST = "28BC4950-6E0F-4F11-97D1-9669A995256F"; //Analyst
        private const String SQLKEYAPPROVER = "3C2FB8E8-0BBF-4B7A-966C-AFC95B3F28DD"; //Approver
        private const String SQLKEYAPLADMIN = "A118D8AE-7C79-4B53-84A5-302BD583B5D1"; //APL Administrator
        #endregion
        #region Static...
        private static String lineBreak = new String('.', 200);
        private static String debugPath = System.IO.Directory.GetCurrentDirectory();
        private static System.IO.FileStream log = System.IO.File.Open(debugPath + traceFile, FileMode.Append, FileAccess.Write, FileShare.Write);
        #endregion

        [TestInitialize]
        public void Setup() {
            _AnalyticData = new APLPX.Server.Data.AnalyticData();
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

        //Analytic routine unique identity added to database...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST01_GivenUserInputsAnalyticIdentity_WhenValidAnalyticIdentityAdded_ThenSuccessStatusRecdAndNoValidationMessageRecd() {
            int newId = 0;
            int oldId = 4; //analyst owns 4,5,7,10
            Boolean sessionLoaded = false;
            Boolean analyticsActive = true;
            Boolean analyticsShared = false;
            String uniqueName = DateTime.Now.ToString("MMdyyyyhhmmss");
            String analyticName = "Add Analytics from Tests.Server.Data - " + uniqueName;
            String analyticDescription = "New analytics description, use this to test adding unique Analytics routine names - " + this.uniqueStamp;
            String analyticNotes = String.Format("New analytics notes, {0}, {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, this.uniqueStamp);

            Analytic existingAnalytic = new Analytic(oldId);
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadIdentity(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine("Begin Identity load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsNotNull(responseLoad.Data);
                this.listener.WriteLine(String.Format("Load: {0}, {1}", responseLoad.Data.Id, responseLoad.Data.SearchGroupKey)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(responseLoad.SessionOk);
                sessionLoaded = responseLoad.SessionOk;
                this.listener.WriteLine("End Indentity load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            if (sessionLoaded) {
                int validSearchGroupId = responseLoad.Data.SearchGroupId;
                Analytic newAnalytic = new Analytic(newId, validSearchGroupId, new AnalyticIdentity(analyticName, analyticDescription, analyticNotes, analyticsShared, analyticsActive));
                Session<Analytic> responseSave = _AnalyticData.SaveIdentity(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = newAnalytic });

                try {
                    this.listener.WriteLine("Begin Add Analytic - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Session valid: {0}", (responseSave.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Client message: {0}", responseSave.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Server message: {0}", responseSave.ServerMessage)); this.listener.WriteLine(lineBreak);

                    Assert.IsTrue(responseSave.SessionOk);
                    Assert.IsNotNull(responseSave.Data.Identity);
                    this.listener.WriteLine(String.Format("New Analytic: {0}", responseSave.Data.Identity.Name)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine("End Add Analytic - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                catch (System.Exception ex) {
                    this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                }
            }
            Assert.IsTrue(sessionLoaded);
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
        }

        //Analytic routine identity duplicate name fails validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST02_GivenUserInputsAnalyticIdentity_WhenAnalyticIdentityIsDuplicateName_ThenFailedStatusRecdAndValidationMessageRecd() {
            int newId = 0;
            int oldId = 4; //analyst owns 4,5,7,10
            Boolean sessionLoaded = false;
            Boolean analyticsActive = true;
            Boolean analyticsShared = false;
            String analyticDescription = "New analytic description, use this to test duplicate Analytics routine names";
            String analyticNotes = String.Format("New analytic notes, {0}, {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, dateStamp);

            Analytic existingAnalytic = new Analytic(oldId);
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadIdentity(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine("Begin Identity load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Existing Analytics: {0}", responseLoad.Data.Identity.Name)); this.listener.WriteLine(lineBreak);

                Assert.IsNotNull(responseLoad.Data);
                this.listener.WriteLine(String.Format("Load: {0}, {1}", responseLoad.Data.Id, responseLoad.Data.SearchGroupKey)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(responseLoad.SessionOk);
                sessionLoaded = responseLoad.SessionOk;
                this.listener.WriteLine("End Indentity load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            if (sessionLoaded) {
                int validSearchGroupId = responseLoad.Data.SearchGroupId;
                String duplicateName = responseLoad.Data.Identity.Name;
                Analytic newAnalytic = new Analytic(newId, validSearchGroupId, new AnalyticIdentity(duplicateName, analyticDescription, analyticNotes, analyticsShared, analyticsActive));
                Session<Analytic> responseSave = _AnalyticData.SaveIdentity(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = newAnalytic });

                try {
                    this.listener.WriteLine("Begin Save duplicate - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Session valid: {0}", (responseSave.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Client message: {0}", responseSave.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Server message: {0}", responseSave.ServerMessage)); this.listener.WriteLine(lineBreak);

                    Assert.IsFalse(responseSave.SessionOk);
                    Assert.AreNotEqual(responseSave.ClientMessage, String.Empty);
                    Assert.AreEqual(responseSave.ServerMessage, String.Empty);
                    this.listener.WriteLine("End Save duplicate - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                catch (System.Exception ex) {
                    this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                }
            }
            Assert.IsTrue(sessionLoaded);
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
        }

        //Analytic routine identity name > 100 characters fails validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST03_GivenUserInputsAnalyticIdentity_WhenAnalyticIdentityNameGreaterThan100Char_ThenFailedStatusRecdAndValidationMessageRecd() {
            int newId = 0;
            int oldId = 4; //analyst owns 4,5,7,10
            Boolean sessionLoaded = false;
            Boolean analyticsActive = true;
            Boolean analyticsShared = false;
            String analyticName = "Analytics from Tests.Server.Data, this name is too long and will fail validation " + uniqueStamp;
            String analyticDescription = "New analytic description, use this to test routine identity name > 100 characters fails validation " + uniqueStamp;
            String analyticNotes = String.Format("New analytic notes, {0}, {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, uniqueStamp);

            Analytic existingAnalytic = new Analytic(oldId);
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadIdentity(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine("Begin Identity load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsNotNull(responseLoad.Data);
                this.listener.WriteLine(String.Format("Load: {0}, {1}", responseLoad.Data.Id, responseLoad.Data.SearchGroupKey)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(responseLoad.SessionOk);
                sessionLoaded = responseLoad.SessionOk;
                this.listener.WriteLine("End Indentity load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            if (sessionLoaded) {
                int validSearchGroupId = responseLoad.Data.SearchGroupId;
                Analytic newAnalytic = new Analytic(newId, validSearchGroupId, new AnalyticIdentity(analyticName, analyticDescription, analyticNotes, analyticsShared, analyticsActive));
                Session<Analytic> responseSave = _AnalyticData.SaveIdentity(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = newAnalytic });

                Assert.IsTrue(newAnalytic.Identity.Name.Length > 100);
                try {
                    this.listener.WriteLine("Begin save name > 100 - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Session valid: {0}", (responseSave.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Client message: {0}", responseSave.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Server message: {0}", responseSave.ServerMessage)); this.listener.WriteLine(lineBreak);

                    Assert.IsFalse(responseSave.SessionOk);
                    Assert.AreNotEqual(responseSave.ClientMessage, String.Empty);
                    Assert.AreEqual(responseSave.ServerMessage, String.Empty);
                    this.listener.WriteLine("End save name > 100 - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                catch (System.Exception ex) {
                    this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                }
            }
            Assert.IsTrue(sessionLoaded);
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
        }

        //Analytic routine identity name < 5 characters fails validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST04_GivenUserInputsAnalyticIdentity_WhenAnalyticIdentityNameLessThan5Char_ThenFailedStatusRecdAndValidationMessageRecd() {
            int newId = 0;
            int oldId = 4; //analyst owns 4,5,7,10
            Boolean sessionLoaded = false;
            Boolean analyticsActive = true;
            Boolean analyticsShared = false;
            String analyticName = new String('0', 4);
            String analyticDescription = "New analytic description, use this to test routine identity name < 5 characters fails validation";
            String analyticNotes = String.Format("New analytic notes, {0}, {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, uniqueStamp);

            Analytic existingAnalytic = new Analytic(oldId);
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadIdentity(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine("Begin Identity load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsNotNull(responseLoad.Data);
                this.listener.WriteLine(String.Format("Load: {0}, {1}", responseLoad.Data.Id, responseLoad.Data.SearchGroupKey)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(responseLoad.SessionOk);
                sessionLoaded = responseLoad.SessionOk;
                this.listener.WriteLine("End Indentity load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            if (sessionLoaded) {
                int validSearchGroupId = responseLoad.Data.SearchGroupId;
                Analytic newAnalytic = new Analytic(newId, validSearchGroupId, new AnalyticIdentity(analyticName, analyticDescription, analyticNotes, analyticsShared, analyticsActive));
                Session<Analytic> responseSave = _AnalyticData.SaveIdentity(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = newAnalytic });

                Assert.IsTrue(newAnalytic.Identity.Name.Length < 5);
                try {
                    this.listener.WriteLine("Begin save name < 5 - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Session valid: {0}", (responseSave.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Client message: {0}", responseSave.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Server message: {0}", responseSave.ServerMessage)); this.listener.WriteLine(lineBreak);

                    Assert.IsFalse(responseSave.SessionOk);
                    Assert.AreNotEqual(responseSave.ClientMessage, String.Empty);
                    Assert.AreEqual(responseSave.ServerMessage, String.Empty);
                    this.listener.WriteLine("End save name < 5 - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                catch (System.Exception ex) {
                    this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                }
            }
            Assert.IsTrue(sessionLoaded);
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
        }

        //Analytic routine identity invalid session fails validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST05_GivenUserInputsAnalyticIdentity_WhenAnalyticIdentitySessionInvalid_ThenFailedStatusRecdAndValidationMessageRecd() {
            int oldId = 4; //analyst owns 4,5,7,10
            String notOwnerKey = SQLKEYADMIN;
            Analytic existingAnalytic = new Analytic(oldId);
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadIdentity(new Session<Analytic> { SqlKey = notOwnerKey, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsFalse(responseLoad.SessionOk);
                Assert.AreNotEqual(responseLoad.ClientMessage, String.Empty);
                Assert.AreEqual(responseLoad.ServerMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
        }

        //Analytic routine select identity list...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST06_GivenUserRequestsAnalyticIdentityList_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsIdentityListRecd() {
            Session<List<Analytic>> response = _AnalyticData.LoadList(new Session<NullT> { SqlKey = SQLKEYANALYST });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);

                this.listener.WriteLine("Begin Analytics Identity list..."); this.listener.WriteLine(lineBreak);
                foreach (Entity.Analytic analytic in response.Data) {
                    this.listener.WriteLine(String.Format("    {0}, {1}, {2}", analytic.Id,analytic.SearchGroupKey,analytic.Identity.Name));

                }
                this.listener.WriteLine(lineBreak); this.listener.WriteLine("End Analytics Identity list..."); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine select meta data...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST07_GivenUserRequestsAnalyticMetaData_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsMetaDataRecd() {
            var existingAnalytic = new Analytic(1);
            Session<Analytic> response = _AnalyticData.Load(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYADMIN, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
                this.listener.WriteLine("Begin Analytics meta data..."); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("{0}, {1}, {2}", response.Data.Id, response.Data.SearchGroupKey, response.Data.Identity.Name));
                this.listener.WriteLine("    Filters excluded...");
                foreach (Entity.FilterGroup filterGroup in response.Data.FilterGroups) {
                    foreach (Entity.Filter filter in filterGroup.Filters) {
                        if (!filter.IsSelected) this.listener.WriteLine(String.Format("        {0}: {1}", filterGroup.Name, filter.Name));
                    }
                }
                this.listener.WriteLine("    Price lists included...");
                foreach (Entity.AnalyticPriceListGroup priceGroup in response.Data.PriceListGroups) {
                    foreach (Entity.PriceList priceList in priceGroup.PriceLists) {
                        if (priceList.IsSelected) this.listener.WriteLine(String.Format("        {0}: {1} - {2}", priceGroup.Name, priceList.Code, priceList.Name));
                    }
                }
                this.listener.WriteLine("    Drivers included...");
                foreach (Entity.AnalyticValueDriver driver in response.Data.ValueDrivers) {
                    foreach (Entity.AnalyticValueDriverMode mode in driver.Modes) {
                        foreach (Entity.ValueDriverGroup driverGroup in mode.Groups) {
                            if (driver.IsSelected && mode.IsSelected) 
                                this.listener.WriteLine(String.Format("        {0}: {1}: {2} - Min({3}), Max({4})", driver.Name, mode.Name, driverGroup.Value, driverGroup.MinOutlier, driverGroup.MaxOutlier));
                        }
                    }
                }
                this.listener.WriteLine(lineBreak); this.listener.WriteLine("End Analytics meta data..."); this.listener.WriteLine(lineBreak);

            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine filters select list...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST08_GivenUserRequestsAnalyticFilterList_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsFilterListRecd() {
            var existingAnalytic = new Analytic(4); //analyst 4,5,7,10
            Session<Analytic> response = _AnalyticData.LoadFilters(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
                this.listener.WriteLine("Begin Analytics load filters..."); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Analytic Id: {0}", response.Data.Id));
                this.listener.WriteLine("    Filters excluded...");
                foreach (Entity.FilterGroup filterGroup in response.Data.FilterGroups) {
                    foreach (Entity.Filter filter in filterGroup.Filters) {
                        if (!filter.IsSelected) this.listener.WriteLine(String.Format("        {0}: {1}", filterGroup.Name, filter.Name));
                    }
                }
                this.listener.WriteLine(lineBreak); this.listener.WriteLine("End Analytics load filters..."); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine drivers select list...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST09_GivenUserRequestsAnalyticTypeList_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsTypesListRecd() {
            var existingAnalytic = new Analytic(10); //analyst 4,5,7,10
            Session<APLPX.Entity.Analytic> response = _AnalyticData.LoadDrivers(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
                this.listener.WriteLine("Begin Analytics load drivers..."); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Analytic Id: {0}", response.Data.Id));
                this.listener.WriteLine("    Drivers included...");
                foreach (Entity.AnalyticValueDriver driver in response.Data.ValueDrivers) {
                    foreach (Entity.AnalyticValueDriverMode mode in driver.Modes) {
                        foreach (Entity.ValueDriverGroup driverGroup in mode.Groups) {
                            if (driver.IsSelected && mode.IsSelected)
                                this.listener.WriteLine(String.Format("        {0}: {1}: {2} - Min({3}), Max({4})", driver.Name, mode.Name, driverGroup.Value, driverGroup.MinOutlier, driverGroup.MaxOutlier));
                        }
                    }
                }
                this.listener.WriteLine(lineBreak); this.listener.WriteLine("End Analytics load drivers..."); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine pricelists select list...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST10_GivenUserRequestsAnalyticPriceLists_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsPriceListsRecd() {
            var existingAnalytic = new Analytic(10); //analyst 4,5,7,10
            Session<APLPX.Entity.Analytic> response = _AnalyticData.LoadPriceLists(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
                this.listener.WriteLine("Begin Analytics load price lists..."); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Analytic Id: {0}", response.Data.Id));
                this.listener.WriteLine("    Price lists...");
                foreach (APLPX.Entity.AnalyticPriceListGroup priceListGroup in response.Data.PriceListGroups)
                    foreach (APLPX.Entity.PriceList list in priceListGroup.PriceLists)
                        this.listener.WriteLine(String.Format("        {0}, {1}, {2}, {3}, Selected: {4}", priceListGroup.Name, list.Key.ToString(), list.Code, list.Name, list.IsSelected.ToString()));
                this.listener.WriteLine(lineBreak); this.listener.WriteLine("End Analytics load price lists..."); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine save filters with validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST11_GivenUserInputsAnalyticFilterValues_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndValidFiltersSaved() {
            Boolean sessionLoaded = false;
            var existingAnalytic = new Analytic(4); //analyst 4,5,7,10
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadFilters(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            try {
                this.listener.WriteLine("Begin filter load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);
                sessionLoaded = responseLoad.SessionOk;
                Assert.IsTrue(responseLoad.SessionOk);
                this.listener.WriteLine("End filter load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            if (sessionLoaded) {
                this.listener.WriteLine(String.Format("Loaded Analytic Id: {0}", responseLoad.Data.Id));
                this.listener.WriteLine("    Filters excluded...");
                foreach (APLPX.Entity.FilterGroup filterGroup in responseLoad.Data.FilterGroups) {
                    foreach (APLPX.Entity.Filter filter in filterGroup.Filters) { //1068, 1070, 1072, 1073
                        if (filter.Key.CompareTo(1068) == 0) { filter.IsSelected = (filter.IsSelected) ? false : true; }
                        if (filter.Key.CompareTo(1070) == 0) { filter.IsSelected = (filter.IsSelected) ? false : true; }
                        if (filter.Key.CompareTo(1072) == 0) { filter.IsSelected = (filter.IsSelected) ? false : true; }
                        if (filter.Key.CompareTo(1073) == 0) { filter.IsSelected = (filter.IsSelected) ? false : true; }
                        if (!filter.IsSelected) this.listener.WriteLine(String.Format("        {0}: {1}", filterGroup.Name, filter.Name));
                    }
                }
                var newAnalytic = new APLPX.Entity.Analytic(existingAnalytic.Id, responseLoad.Data.FilterGroups);
                Session<APLPX.Entity.Analytic> responseSave = _AnalyticData.SaveFilters(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = newAnalytic });

                try {
                    this.listener.WriteLine("Begin filter save - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Session valid: {0}", (responseSave.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Client message: {0}", responseSave.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Server message: {0}", responseSave.ServerMessage)); this.listener.WriteLine(lineBreak);

                    Assert.IsTrue(responseSave.SessionOk);
                    Assert.AreEqual(responseSave.ClientMessage, String.Empty);
                    Assert.AreEqual(responseSave.ServerMessage, String.Empty);
                    this.listener.WriteLine("End filter save - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                catch (System.Exception ex) {
                    this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                Assert.IsTrue(responseSave.SessionOk);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(responseLoad.SessionOk);
        }

        //Analytic routine save drivers with validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST12_GivenUserInputsAnalyticDriverValues_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndValidDriversSaved() {
            Boolean sessionLoaded = false;
            var existingAnalytic = new Analytic(10); //analyst 4,5,7,10
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadDrivers(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            try {
                this.listener.WriteLine("Begin drivers load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);
                sessionLoaded = responseLoad.SessionOk;
                Assert.IsTrue(responseLoad.SessionOk);
                this.listener.WriteLine("End drivers load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            if (sessionLoaded) {
                this.listener.WriteLine(String.Format("Loaded Analytic Id: {0}", responseLoad.Data.Id));
                this.listener.WriteLine("    Drivers selected...");
                foreach (APLPX.Entity.AnalyticValueDriver driver in responseLoad.Data.ValueDrivers) {
                    if (driver.IsSelected) {
                        foreach (APLPX.Entity.AnalyticValueDriverMode mode in driver.Modes) {
                            if (mode.IsSelected) {
                                foreach (APLPX.Entity.ValueDriverGroup group in mode.Groups) {
                                    if ( driver.Key==50 && group.Value == 1) { //markup
                                        group.MinOutlier = (group.MinOutlier > 0) ? 0 : 10;
                                        group.MaxOutlier = (group.MaxOutlier > 1000) ? 1000 : 1500;
                                        this.listener.WriteLine(String.Format("        {0}: {1}: {2} - Min({3}), Max({4})", driver.Name, mode.Name, group.Value, group.MinOutlier, group.MaxOutlier));
                                    }
                                }
                            }
                        }
                    }
                }
                var newAnalytic = new APLPX.Entity.Analytic(existingAnalytic.Id, responseLoad.Data.ValueDrivers);
                Session<APLPX.Entity.Analytic> responseSave = _AnalyticData.SaveDrivers(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = newAnalytic });

                this.listener.WriteLine("Begin drivers save - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                try {
                    this.listener.WriteLine("Begin driver save - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Session valid: {0}", (responseSave.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Client message: {0}", responseSave.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Server message: {0}", responseSave.ServerMessage)); this.listener.WriteLine(lineBreak);

                    Assert.IsTrue(responseSave.SessionOk);
                    Assert.AreEqual(responseSave.ClientMessage, String.Empty);
                    Assert.AreEqual(responseSave.ServerMessage, String.Empty);
                    this.listener.WriteLine("End drivers save - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                catch (System.Exception ex) {
                    this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                Assert.IsTrue(responseSave.SessionOk);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(responseLoad.SessionOk);
        }

        //Analytic routine save price lists with validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST13_GivenUserInputsAnalyticPriceListValues_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndValidPriceListsSaved() {
            Boolean sessionLoaded = false;
            var existingAnalytic = new Analytic(10); //analyst 4,5,7,10
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadPriceLists(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            try {
                this.listener.WriteLine("Begin price list load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);
                sessionLoaded = responseLoad.SessionOk;
                Assert.IsTrue(responseLoad.SessionOk);
                this.listener.WriteLine("End price list load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            if (sessionLoaded) {
                foreach (APLPX.Entity.AnalyticPriceListGroup priceListGroup in responseLoad.Data.PriceListGroups) {
                    foreach (APLPX.Entity.PriceList list in priceListGroup.PriceLists)
                        //if (list.Key > 2 && list.Key <= 4) { list.IsSelected = (list.IsSelected) ? false : true; }
                        list.IsSelected = (list.Key > 2 && list.Key <= 5) ? false : true;
                        //list.IsSelected = (list.IsSelected) ? false : true;
                }
                var newAnalytic = new APLPX.Entity.Analytic(existingAnalytic.Id, responseLoad.Data.PriceListGroups);
                Session<APLPX.Entity.Analytic> responseSave = _AnalyticData.SavePriceLists(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = newAnalytic });

                try {
                    this.listener.WriteLine("Begin price list save - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Session valid: {0}", (responseSave.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Client message: {0}", responseSave.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Server message: {0}", responseSave.ServerMessage)); this.listener.WriteLine(lineBreak);
                    foreach (APLPX.Entity.AnalyticPriceListGroup priceListGroup in responseSave.Data.PriceListGroups)
                        foreach (APLPX.Entity.PriceList list in priceListGroup.PriceLists)
                            this.listener.WriteLine(String.Format("List item: {0}, {1}, {2}, {3}, Selected: {4}", priceListGroup.Name, list.Key, list.Code, list.Name, list.IsSelected));

                    Assert.IsTrue(responseSave.SessionOk);
                    Assert.AreEqual(responseSave.ClientMessage, String.Empty);
                    Assert.AreEqual(responseSave.ServerMessage, String.Empty);
                    this.listener.WriteLine(lineBreak); this.listener.WriteLine("End price list save - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                catch (System.Exception ex) {
                    this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                Assert.IsTrue(responseSave.SessionOk);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(responseLoad.SessionOk);
        }

        //Analytic routine save identity with validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST14_GivenUserInputsAnalyticIdentity_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndValidIdentitySaved() {
            Boolean sessionLoaded = false;
            var existingAnalytic = new Analytic(7); //analyst owns 4,5,7
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadIdentity(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin Identity load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsNotNull(responseLoad.Data.Identity);
                this.listener.WriteLine(String.Format("Load: {0}", responseLoad.Data.Identity.Description)); this.listener.WriteLine(lineBreak);

                sessionLoaded = responseLoad.SessionOk;
                Assert.IsTrue(responseLoad.SessionOk);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            if (sessionLoaded) {
                int validSearchGroupId = responseLoad.Data.SearchGroupId;
                var newAnalytic = new APLPX.Entity.Analytic(existingAnalytic.Id, validSearchGroupId, responseLoad.Data.Identity);
                newAnalytic.Identity.Description = String.Format("{0}, Test update: {1}", newAnalytic.Identity.Name, this.uniqueStamp);
                Session<APLPX.Entity.Analytic> responseSave = _AnalyticData.SaveIdentity(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = newAnalytic });

                this.listener.WriteLine("Begin Identity save - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                try {                 
                    this.listener.WriteLine(String.Format("Session valid: {0}", (responseSave.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Client message: {0}", responseSave.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Server message: {0}", responseSave.ServerMessage)); this.listener.WriteLine(lineBreak);

                    Assert.IsNotNull(responseSave.Data.Identity);
                    this.listener.WriteLine(String.Format("Saved: {0}", responseSave.Data.Identity.Description)); this.listener.WriteLine(lineBreak);

                    Assert.IsTrue(responseSave.SessionOk);
                    Assert.AreEqual(responseSave.ClientMessage, String.Empty);
                    Assert.AreEqual(responseSave.ServerMessage, String.Empty);
                }
                catch (System.Exception ex) {
                    this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                Assert.IsTrue(responseSave.SessionOk);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(responseLoad.SessionOk);
        }

        //Analytic routine select meta data...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST15_GivenUserRequestsNewAnalyticMetaData_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsMetaDataRecd() {
            Boolean allFilters;
            var existingAnalytic = new Analytic(4); //this analytic id will be ignored because the action is new...
            const int actionType = (Int32)Entity.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsNew;
            Session<Analytic> response = _AnalyticData.Load(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic, ClientCommand = actionType });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
                this.listener.WriteLine("Begin Analytics master template..."); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("{0}, {1}, {2}", response.Data.Id, response.Data.SearchGroupKey, response.Data.Identity.Name));
                this.listener.WriteLine("    Filters...");
                foreach (Entity.FilterGroup filterGroup in response.Data.FilterGroups) {
                    allFilters = true;
                    foreach (Entity.Filter filter in filterGroup.Filters) {
                        if (!filter.IsSelected) {
                            this.listener.WriteLine(String.Format("        {0}: {1}", filterGroup.Name, filter.Name));
                            allFilters = false;
                        }
                    }
                    if (allFilters) this.listener.WriteLine(String.Format("        {0}: No exclusions", filterGroup.Name));
                }
                this.listener.WriteLine("    Price lists...");
                foreach (Entity.AnalyticPriceListGroup priceGroup in response.Data.PriceListGroups) {
                    foreach (Entity.PriceList priceList in priceGroup.PriceLists) {
                        this.listener.WriteLine(String.Format("        {0}: {1} - {2}, Selected: {3}", priceGroup.Name, priceList.Code, priceList.Name,priceList.IsSelected));
                    }
                }
                this.listener.WriteLine("    Drivers...");
                foreach (Entity.AnalyticValueDriver driver in response.Data.ValueDrivers) {
                    foreach (Entity.AnalyticValueDriverMode mode in driver.Modes) {
                        foreach (Entity.ValueDriverGroup driverGroup in mode.Groups) {
                            this.listener.WriteLine(String.Format("        {0}: {1}: {2} - Min({3}), Max({4}), Selected: {5}", driver.Name, mode.Name, driverGroup.Value, driverGroup.MinOutlier, driverGroup.MaxOutlier, mode.IsSelected));
                        }
                    }
                }
                this.listener.WriteLine(lineBreak); this.listener.WriteLine("End Analytics master template..."); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine save & run driver with validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST16_GivenUserInputsRunAnalyticDriverValues_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndValidDriverResults() {
            Boolean sessionLoaded = false;
            //var existingAnalytic = new Analytic(10); //analyst 4,5,7,10 -- SQLKEYANALYST
            var existingAnalytic = new Analytic(3); //admin 1,2,3,6,8,9 -- SQLKEYADMIN
            const int actionType = (Int32)Entity.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversRun;
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadDrivers(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYADMIN, Data = existingAnalytic });

            try {
                this.listener.WriteLine("Begin drivers load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Load Analytic Id: {0}", responseLoad.Data.Id)); this.listener.WriteLine(lineBreak);
                sessionLoaded = responseLoad.SessionOk;
                Assert.IsTrue(responseLoad.SessionOk);
                this.listener.WriteLine("End drivers load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            if (sessionLoaded) {
                foreach (APLPX.Entity.AnalyticValueDriver driver in responseLoad.Data.ValueDrivers) {
                    if (driver.IsSelected && driver.Key == 51) { //Markup=50, Movement=51, Days On Hand=52
                        driver.RunResults = true; break; //only run the first driver, usually markup
                    }
                }
                var newAnalytic = new APLPX.Entity.Analytic(existingAnalytic.Id, responseLoad.Data.ValueDrivers);
                Session<APLPX.Entity.Analytic> responseSave = _AnalyticData.SaveDrivers(new Session<Analytic> { SqlKey = SQLKEYADMIN, Data = newAnalytic, ClientCommand = actionType });

                try {
                    this.listener.WriteLine("Begin drivers run - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Session valid: {0}", (responseSave.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Client message: {0}", responseSave.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Server message: {0}", responseSave.ServerMessage)); this.listener.WriteLine(lineBreak);

                    Assert.IsTrue(responseSave.SessionOk);
                    Assert.AreEqual(responseSave.ClientMessage, String.Empty);
                    Assert.AreEqual(responseSave.ServerMessage, String.Empty);
                    this.listener.WriteLine("    Results...");
                    foreach (Entity.AnalyticValueDriver driver in responseSave.Data.ValueDrivers) {
                        if (driver.IsSelected) {
                            foreach (Entity.AnalyticResultValueDriverGroup group in driver.Results) {
                                this.listener.WriteLine(String.Format("        {0}: {1} - Min({2}), Max({3}), SKUs: {4}, Sales: {5}", driver.Name, group.Value, group.MinOutlier, group.MaxOutlier, group.SkuCount, group.SalesValue));
                            }
                        }
                    }
                    this.listener.WriteLine(lineBreak); this.listener.WriteLine("End drivers run - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                catch (System.Exception ex) {
                    this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                Assert.IsTrue(responseSave.SessionOk);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(responseLoad.SessionOk);
        }

        //Analytic routine save & run drivers with validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST17_GivenUserInputsRunAllAnalyticDriverValues_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndValidDriversResults() {
            Boolean sessionLoaded = false;
            var existingAnalytic = new Analytic(10); //analyst 4,5,7,10
            const int actionType = (Int32)Entity.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversRun;
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadDrivers(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            try {
                this.listener.WriteLine("Begin drivers load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseLoad.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseLoad.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseLoad.ServerMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Load Analytic Id: {0}", responseLoad.Data.Id)); this.listener.WriteLine(lineBreak);
                sessionLoaded = responseLoad.SessionOk;
                Assert.IsTrue(responseLoad.SessionOk);
                this.listener.WriteLine("End drivers load - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }

            if (sessionLoaded) {
                foreach (APLPX.Entity.AnalyticValueDriver driver in responseLoad.Data.ValueDrivers) {
                    if (driver.IsSelected) {
                        driver.RunResults = true;
                    }
                }
                var newAnalytic = new APLPX.Entity.Analytic(existingAnalytic.Id, responseLoad.Data.ValueDrivers);
                Session<APLPX.Entity.Analytic> responseSave = _AnalyticData.SaveDrivers(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = newAnalytic, ClientCommand = actionType });

                try {
                    this.listener.WriteLine("Begin drivers run - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Session valid: {0}", (responseSave.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Client message: {0}", responseSave.ClientMessage)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine(String.Format("Server message: {0}", responseSave.ServerMessage)); this.listener.WriteLine(lineBreak);

                    Assert.IsTrue(responseSave.SessionOk);
                    Assert.AreEqual(responseSave.ClientMessage, String.Empty);
                    Assert.AreEqual(responseSave.ServerMessage, String.Empty);
                    this.listener.WriteLine("    Results...");
                    foreach (Entity.AnalyticValueDriver driver in responseSave.Data.ValueDrivers) {
                        if (driver.IsSelected) {
                            foreach (Entity.AnalyticResultValueDriverGroup group in driver.Results) {
                                this.listener.WriteLine(String.Format("        {0}: {1} - Min({2}), Max({3}), SKUs: {4}, Sales: {5}", driver.Name, group.Value, group.MinOutlier, group.MaxOutlier, group.SkuCount, group.SalesValue));
                            }
                        }
                    }
                    this.listener.WriteLine(lineBreak); this.listener.WriteLine("End drivers run - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                catch (System.Exception ex) {
                    this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
                    this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
                }
                Assert.IsTrue(responseSave.SessionOk);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(responseLoad.SessionOk);
        }

        //Analytic routine copy with validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST18_GivenUserInputsValidAnalyticToCopy_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndAnalyticCopySaved() {
            int analyticId = 3; //owner admin 1,2,3,6,8,9
            var existingAnalytic = new Analytic(analyticId);
            const int cmdCopy = (Int32)Entity.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsCopy;
            const int cmdCopyIdentity = (Int32)Entity.ModuleFeatureStepActionType.PlanningAnalyticsIdentitySave;
            const int cmdCopyFilters = (Int32)Entity.ModuleFeatureStepActionType.PlanningAnalyticsFiltersSave;
            const int cmdCopyPriceLists = (Int32)Entity.ModuleFeatureStepActionType.PlanningAnalyticsPriceListsSave;
            const int cmdCopyValueDrivers = (Int32)Entity.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversSave;

            //Get copy of existing Analytic...
            Session<Analytic> responseCopy = _AnalyticData.Load(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYADMIN, Data = existingAnalytic, ClientCommand = cmdCopy });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (responseCopy.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", responseCopy.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", responseCopy.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(responseCopy.SessionOk);
                Assert.AreEqual(responseCopy.ClientMessage, String.Empty);
                Assert.AreEqual(responseCopy.ServerMessage, String.Empty);
                this.listener.WriteLine("Begin Analytics copy..."); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Existing {0}, {1}, {2}", responseCopy.Data.Id, responseCopy.Data.SearchGroupKey, responseCopy.Data.Identity.Name));
                
                #region Copy Identity...
                responseCopy.ClientCommand = cmdCopyIdentity;
                responseCopy.Data.Identity.Name = responseCopy.Data.Identity.Name + " - copy on " + this.uniqueStamp;
                responseCopy.Data.Identity.Description = responseCopy.Data.Identity.Description + " - Analytic copied on " + this.uniqueStamp;
                responseCopy.Data.Identity.Notes = String.Format("Analytic originally copied from Owner {0}, Original Analytic Id: {1}, Analytic name: {2}", responseCopy.Data.Identity.Owner, analyticId, responseCopy.Data.Identity.Name);
                Session<Analytic> responseCopyIdentity = _AnalyticData.SaveIdentity(responseCopy);
                Assert.IsTrue(responseCopyIdentity.SessionOk);
                this.listener.WriteLine(String.Format("Copy {0}, {1}, {2}", responseCopyIdentity.Data.Id, responseCopyIdentity.Data.SearchGroupKey, responseCopyIdentity.Data.Identity.Name));
                #endregion
                #region Copy Filters...
                responseCopyIdentity.ClientCommand = cmdCopyFilters;
                responseCopyIdentity.Data = new Analytic(responseCopyIdentity.Data.Id, responseCopy.Data.FilterGroups);
                Session<Analytic> responseCopyFilters = _AnalyticData.SaveFilters(responseCopyIdentity);
                Assert.IsTrue(responseCopyFilters.SessionOk);
                this.listener.WriteLine("    Filters excluded...");
                foreach (Entity.FilterGroup filterGroup in responseCopyFilters.Data.FilterGroups) {
                    foreach (Entity.Filter filter in filterGroup.Filters) {
                        if (!filter.IsSelected) this.listener.WriteLine(String.Format("        {0}: {1}", filterGroup.Name, filter.Name));
                    }
                }
                #endregion
                #region Copy Price Lists...
                responseCopyIdentity.ClientCommand = cmdCopyPriceLists;
                responseCopyIdentity.Data = new Analytic(responseCopyIdentity.Data.Id, responseCopy.Data.PriceListGroups);
                Session<Analytic> responseCopyPriceLists = _AnalyticData.SavePriceLists(responseCopyIdentity);
                Assert.IsTrue(responseCopyPriceLists.SessionOk);
                this.listener.WriteLine("    Price lists included...");
                foreach (Entity.AnalyticPriceListGroup priceGroup in responseCopyPriceLists.Data.PriceListGroups) {
                    foreach (Entity.PriceList priceList in priceGroup.PriceLists) {
                        if (priceList.IsSelected) this.listener.WriteLine(String.Format("        {0}: {1} - {2}", priceGroup.Name, priceList.Code, priceList.Name));
                    }
                }
                #endregion
                #region Copy Drivers...
                responseCopyIdentity.ClientCommand = cmdCopyValueDrivers;
                responseCopyIdentity.Data = new Analytic(responseCopyIdentity.Data.Id, responseCopy.Data.ValueDrivers);
                Session<Analytic> responseCopyDrivers = _AnalyticData.SaveDrivers(responseCopyIdentity);
                Assert.IsTrue(responseCopyDrivers.SessionOk);
                this.listener.WriteLine("    Drivers included...");
                foreach (Entity.AnalyticValueDriver driver in responseCopyDrivers.Data.ValueDrivers) {
                    foreach (Entity.AnalyticValueDriverMode mode in driver.Modes) {
                        foreach (Entity.ValueDriverGroup driverGroup in mode.Groups) {
                            if (driver.IsSelected && mode.IsSelected)
                                this.listener.WriteLine(String.Format("        {0}: {1}: {2} - Min({3}), Max({4})", driver.Name, mode.Name, driverGroup.Value, driverGroup.MinOutlier, driverGroup.MaxOutlier));
                        }
                    }
                }
                #endregion

                this.listener.WriteLine(lineBreak); this.listener.WriteLine("End Analytics copy..."); this.listener.WriteLine(lineBreak);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
        }
    }
}
