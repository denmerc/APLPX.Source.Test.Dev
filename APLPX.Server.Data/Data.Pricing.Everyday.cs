using System;
using System.Collections.Generic;
using APLPX.Server.Data;
using APLPX.Entity;

namespace APLPX.Server.Data
{

    public interface IPricingEverydayData
    {
        void Dispose();
        Session<List<Entity.PricingEveryday>> LoadList(Session<Entity.NullT> session);
        Session<Entity.PricingEveryday> SaveIdentity(Session<Entity.PricingEveryday> session);
        Session<Entity.PricingEveryday> LoadFilters(Session<Entity.PricingEveryday> session);
        Session<Entity.PricingEveryday> SaveFilters(Session<Entity.PricingEveryday> session);
        Session<Entity.PricingEveryday> LoadDrivers(Session<Entity.PricingEveryday> session);
        Session<Entity.PricingEveryday> SaveDrivers(Session<Entity.PricingEveryday> session);
        Session<Entity.PricingEveryday> LoadPriceLists(Session<Entity.PricingEveryday> session);
        Session<Entity.PricingEveryday> SavePriceLists(Session<Entity.PricingEveryday> session);
        Session<Entity.PricingEveryday> LoadResults(Session<Entity.PricingEveryday> session);
    }

    public class PricingEverydayData : IPricingEverydayData
    {

        #region Constants...
        const String INVALID = "Invalid:";
        const String CONNECTIONNAME = "defaultConnectionString";
        const String APLSERVICEEVENTLOG = "APLServiceEventLog";
        #endregion

        #region Variables...
        private System.Diagnostics.EventLog localServiceLog;
        private APLPX.Server.Data.AnalyticMap sqlMapper;
        private APLPX.Server.Data.SqlService sqlService;
        #endregion

        private String sqlConnection {
            get {
                return System.Configuration.ConfigurationManager.AppSettings[CONNECTIONNAME];
            }
        }

        public PricingEverydayData() {

            sqlMapper = new AnalyticMap();
            sqlService = new SqlService(this.sqlConnection);
            localServiceLog = new System.Diagnostics.EventLog();
            //if (!System.Diagnostics.EventLog.SourceExists(APLServiceEventLog)) EventLog.CreateEventSource(APLServiceEventLog, "Application");
            //Setup <APLServiceEventLog> event source manually through registry key: HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application
            //To resolve message IDs create a RG_EXPAND_SZ attribute, named "EventMessageFile" to: "C:\WINDOWS\Microsoft.NET\Framework\<current version>\EventLogMessages.dll"
            localServiceLog.Source = APLSERVICEEVENTLOG;

        }

        ~PricingEverydayData() {
            if (sqlService != null) sqlService.ExecuteCloseConnection();
        }

        public Session<List<Entity.PricingEveryday>> LoadList(Session<Entity.NullT> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Entity.PricingEveryday>> sessionOut = Session<NullT>.Clone<List<PricingEveryday>>(sessionIn);

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> SaveIdentity(Session<Entity.PricingEveryday> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.PricingEveryday> sessionOut = Session<PricingEveryday>.Clone<PricingEveryday>(sessionIn);

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> LoadFilters(Session<Entity.PricingEveryday> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.PricingEveryday> sessionOut = Session<PricingEveryday>.Clone<PricingEveryday>(sessionIn);

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> SaveFilters(Session<Entity.PricingEveryday> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.PricingEveryday> sessionOut = Session<PricingEveryday>.Clone<PricingEveryday>(sessionIn);

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> LoadDrivers(Session<Entity.PricingEveryday> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.PricingEveryday> sessionOut = Session<PricingEveryday>.Clone<PricingEveryday>(sessionIn);

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> SaveDrivers(Session<Entity.PricingEveryday> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.PricingEveryday> sessionOut = Session<PricingEveryday>.Clone<PricingEveryday>(sessionIn);

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> LoadPriceLists(Session<Entity.PricingEveryday> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.PricingEveryday> sessionOut = Session<PricingEveryday>.Clone<PricingEveryday>(sessionIn);

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> SavePriceLists(Session<Entity.PricingEveryday> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.PricingEveryday> sessionOut = Session<PricingEveryday>.Clone<PricingEveryday>(sessionIn);

            return sessionOut;
        }

        public Session<Entity.PricingEveryday> LoadResults(Session<Entity.PricingEveryday> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.PricingEveryday> sessionOut = Session<PricingEveryday>.Clone<PricingEveryday>(sessionIn);

            return sessionOut;
        }

        public void Dispose() {
            if (sqlService != null)
                if (!sqlService.ExecuteCloseConnection())
                    this.localServiceLog.WriteEntry(sqlService.SqlStatusMessage);
        }

    }
}
