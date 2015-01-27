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
        public void TEST11_GivenUserInputsAnalyticIdentity_WhenValidAnalyticIdentityAdded_ThenSuccessStatusRecdAndNoValidationMessageRecd() {
            int newId = 0;
            int oldId = 4; //analyst owns 4,5,7,10
            Boolean sessionLoaded = false;
            Boolean analyticsActive = true;
            Boolean analyticsShared = false;
            String uniqueName = DateTime.Now.ToString("MMdyyyyhhmmss");
            String analyticName = "Add Analytics from Tests.Server.Data - " + uniqueName;
            String analyticDescription = "New analytic description, use this to test adding unique Analytics routine names - " + this.uniqueStamp;
            String analyticNotes = String.Format("New analytic notes, {0}, {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, this.uniqueStamp);

            Analytic existingAnalytic = new Analytic(oldId);
            Session<APLPX.Entity.Analytic> responseLoad = _AnalyticData.LoadIdentity(new Session<Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

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
                int searchGroupId = responseLoad.Data.SearchGroupId;
                Analytic newAnalytic = new Analytic(newId, searchGroupId, new AnalyticIdentity(analyticName, analyticDescription, analyticNotes, analyticsShared, analyticsActive));
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

            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
        }

        //Analytic routine identity duplicate name fails validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST12_GivenUserInputsAnalyticIdentity_WhenAnalyticIdentityIsDuplicateName_ThenFailedStatusRecdAndValidationMessageRecd() {
            Boolean analyticsActive = true;
            Boolean analyticsShared = false;
            String analyticName = "Analytics from Tests.Server.Data";
            String analyticDescription = "New analytic description, use this to test duplicate Analytics routine names";
            String analyticNotes = String.Format("New analytic notes, {0}, {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, dateStamp);
            Analytic newAnalytic = new Analytic(new AnalyticIdentity(analyticName, analyticDescription, analyticNotes, analyticsShared, analyticsActive));
            Session<Analytic> response = _AnalyticData.SaveIdentity(new Session<Analytic> { SqlKey = SQLKEYAPLADMIN, Data = newAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsFalse(response.SessionOk);
                Assert.AreNotEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            Assert.IsFalse(response.SessionOk);
        }

        //Analytic routine identity name > 100 characters fails validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST13_GivenUserInputsAnalyticIdentity_WhenAnalyticIdentityNameGreaterThan100Char_ThenFailedStatusRecdAndValidationMessageRecd() {
            Boolean analyticsActive = true;
            Boolean analyticsShared = false;
            String dateStamp = String.Format("{0} {1}", System.DateTime.Now.ToLongDateString(), System.DateTime.Now.ToLongTimeString());
            String analyticName = "Analytics from Tests.Server.Data, this name is too long and will fail validation " + dateStamp;
            String analyticDescription = "New analytic description, use this to test routine identity name > 100 characters fails validation " + dateStamp;
            String analyticNotes = String.Format("New analytic notes, {0}, {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, dateStamp);
            Analytic newAnalytic = new Analytic(new AnalyticIdentity(analyticName, analyticDescription, analyticNotes, analyticsShared, analyticsActive));
            //Analytic newAnalytic = new Analytic { Identity = new AnalyticIdentity { Name = analyticName, Description = analyticDescription } };

            Assert.IsTrue(newAnalytic.Identity.Name.Length > 100);
            Session<Analytic> response = _AnalyticData.SaveIdentity(new Session<Analytic> { SqlKey = SQLKEYAPLADMIN, Data = newAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsFalse(response.SessionOk);
                Assert.AreNotEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsFalse(response.SessionOk);
        }

        //Analytic routine identity name < 5 characters fails validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST14_GivenUserInputsAnalyticIdentity_WhenAnalyticIdentityNameLessThan5Char_ThenFailedStatusRecdAndValidationMessageRecd() {
            Boolean analyticsActive = true;
            Boolean analyticsShared = false;
            String analyticName = new String('0', 4);
            String analyticDescription = "New analytic description, use this to test routine identity name < 5 characters fails validation";
            String analyticNotes = String.Format("New analytic notes, {0}, {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, dateStamp);
            Analytic newAnalytic = new Analytic(new AnalyticIdentity(analyticName, analyticDescription, analyticNotes, analyticsShared, analyticsActive));
            //Analytic newAnalytic = new Analytic { Identity = new AnalyticIdentity { Name = analyticName, Description = analyticDescription } };

            Assert.IsTrue(newAnalytic.Identity.Name.Length < 5);
            Session<Analytic> response = _AnalyticData.SaveIdentity(new Session<Analytic> { SqlKey = SQLKEYAPLADMIN, Data = newAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsFalse(response.SessionOk);
                Assert.AreNotEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsFalse(response.SessionOk);
        }

        //Analytic routine identity invalid session fails validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST15_GivenUserInputsAnalyticIdentity_WhenAnalyticIdentitySessionInvalid_ThenFailedStatusRecdAndValidationMessageRecd() {
            Boolean analyticsActive = true;
            Boolean analyticsShared = false;
            String analyticName = "Add Analytics from Tests.Server.Data";
            String analyticDescription = "New analytic description, routine identity invalid session fails validation";
            String analyticNotes = String.Format("New analytic notes, {0}, {1}", System.Reflection.MethodInfo.GetCurrentMethod().Name, dateStamp);
            Analytic newAnalytic = new Analytic(new AnalyticIdentity(analyticName, analyticDescription, analyticNotes, analyticsShared, analyticsActive));
            //Analytic newAnalytic = new Analytic { Identity = new AnalyticIdentity { Name = analyticName, Description = analyticDescription } };
            Session<Analytic> response = _AnalyticData.SaveIdentity(new Session<Analytic> { SqlKey = SQLKEYAPLADMIN, Data = newAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsFalse(response.SessionOk);
                Assert.AreNotEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsFalse(response.SessionOk);
        }

        //Analytic routine select identity list...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST16_GivenUserRequestsAnalyticIdentityList_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsIdentityListRecd() {
            Session<List<Analytic>> response = _AnalyticData.LoadList(new Session<NullT> { SqlKey = SQLKEYANALYST });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine select meta data...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST17_GivenUserRequestsAnalyticMetaData_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsMetaDataRecd() {
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
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine filters select list...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST18_GivenUserRequestsAnalyticFilterList_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsFilterListRecd() {
            var existingAnalytic = new Analytic(29); //analyst 4,5,7,10,28,29
            Session<Analytic> response = _AnalyticData.LoadFilters(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine drivers select list...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST19_GivenUserRequestsAnalyticTypeList_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsTypesListRecd() {
            var existingAnalytic = new Analytic(1);
            Session<APLPX.Entity.Analytic> response = _AnalyticData.LoadFilters(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);

                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine pricelists select list...
        [TestMethod, TestCategory("Analytics select")]
        public void TEST20_GivenUserRequestsAnalyticPriceLists_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsPriceListsRecd() {
            var existingAnalytic = new Analytic(1);
            Session<APLPX.Entity.Analytic> response = _AnalyticData.LoadFilters(new Session<APLPX.Entity.Analytic> { SqlKey = SQLKEYANALYST, Data = existingAnalytic });

            this.listener.WriteLine("Begin - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
            try {
                this.listener.WriteLine(String.Format("Session valid: {0}", (response.SessionOk) ? "True" : "False")); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Client message: {0}", response.ClientMessage)); this.listener.WriteLine(lineBreak);
                this.listener.WriteLine(String.Format("Server message: {0}", response.ServerMessage)); this.listener.WriteLine(lineBreak);
                foreach (APLPX.Entity.AnalyticPriceListGroup priceListGroup in response.Data.PriceListGroups)
                    foreach (APLPX.Entity.PriceList list in priceListGroup.PriceLists)
                        this.listener.WriteLine(String.Format("List item: {0}, {1}, {2}, {3}, Selected: {4}", priceListGroup.Name, list.Key.ToString(), list.Code, list.Name, list.IsSelected.ToString()));

                this.listener.WriteLine(lineBreak);
                Assert.IsTrue(response.SessionOk);
                Assert.AreEqual(response.ClientMessage, String.Empty);
                Assert.AreEqual(response.ServerMessage, String.Empty);
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

        //Analytic routine save filters with validation...
        [TestMethod, TestCategory("Analytics update")]
        public void TEST21_GivenUserSelectsAnalyticFilterValues_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndValidFiltersSaved() {
            Boolean sessionLoaded = false;
            var existingAnalytic = new Analytic(29); //analyst 4,5,7,10,28,29
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
                foreach (APLPX.Entity.FilterGroup filterGroup in responseLoad.Data.FilterGroups) {
                    foreach (APLPX.Entity.Filter filter in filterGroup.Filters) {
                        if (filter.Key.CompareTo(1073) == 0) { filter.IsSelected = (filter.IsSelected) ? false : true; }
                        if (filter.Key.CompareTo(1093) == 0) { filter.IsSelected = (filter.IsSelected) ? false : true; }
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
        public void TEST22_GivenUserInputsAnalyticDriverValues_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndValidDriversSaved() {
            Boolean sessionLoaded = false;
            var existingAnalytic = new Analytic(5);
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
                foreach (APLPX.Entity.AnalyticValueDriver driver in responseLoad.Data.ValueDrivers) {
                    if (driver.IsSelected) {
                        foreach (APLPX.Entity.AnalyticValueDriverMode mode in driver.Modes) {
                            if (mode.IsSelected) {
                                foreach (APLPX.Entity.ValueDriverGroup group in mode.Groups) {
                                    if (group.Value == 1) {
                                        group.MinOutlier = (group.MinOutlier == 0) ? 100 : 0;
                                        group.MaxOutlier = (group.MaxOutlier > 2000) ? 2000 : 3500;
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
        public void TEST23_GivenUserSelectsAnalyticPriceListValues_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndValidPriceListsSaved() {
            Boolean sessionLoaded = false;
            var existingAnalytic = new Analytic(5); //analyst 4,5,7,10,28,29
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
                        if (list.Key >= 2 && list.Key <= 4) { list.IsSelected = (list.IsSelected) ? false : true; }
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
                            this.listener.WriteLine(String.Format("List item: {0}, {1}, {2}, {3}, Selected: {4}", priceListGroup.Name, list.Key.ToString(), list.Code, list.Name, list.IsSelected.ToString()));

                    Assert.IsTrue(responseSave.SessionOk);
                    Assert.AreEqual(responseSave.ClientMessage, String.Empty);
                    Assert.AreEqual(responseSave.ServerMessage, String.Empty);
                    this.listener.WriteLine("End price list save - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak);
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
        public void TEST24_GivenUserSaveAnalyticIdentity_WhenAnalyticSessionValid_ThenSuccessStatusRecdAndValidIdentitySaved() {
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
                var newAnalytic = new APLPX.Entity.Analytic(existingAnalytic.Id, responseLoad.Data.Identity);
                newAnalytic.Identity.Description = String.Format("{0}, Test: {1}", newAnalytic.Identity.Name, this.uniqueStamp);
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
        public void TEST25_GivenUserRequestsNewAnalyticMetaData_WhenSessionValid_ThenSuccessStatusRecdAndAnalyticsMetaDataRecd() {
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
            }
            catch (System.Exception ex) {
                this.listener.WriteLine(String.Format("Exception: {0}", ex.Message)); this.listener.WriteLine(lineBreak);
            }
            this.listener.WriteLine("End - " + System.Reflection.MethodInfo.GetCurrentMethod().Name); this.listener.WriteLine(lineBreak); this.listener.WriteLine(lineBreak);
            Assert.IsTrue(response.SessionOk);
        }

    }
}
