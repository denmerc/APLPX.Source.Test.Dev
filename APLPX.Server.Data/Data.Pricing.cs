using System;
using System.Collections.Generic;
using APLPX.Server.Data;
using APLPX.Server.Entity;

namespace APLPX.Server.Data
{

    public interface IPricingData
    {
        void Dispose();
        Session<List<Server.Entity.Pricing>> LoadList(Session<Server.Entity.NullT> session);
        Session<Server.Entity.Pricing> SaveIdentity(Session<Server.Entity.Pricing> session);
        Session<Server.Entity.Pricing> LoadFilters(Session<Server.Entity.Pricing> session);
        Session<Server.Entity.Pricing> SaveFilters(Session<Server.Entity.Pricing> session);
        Session<Server.Entity.Pricing> LoadDrivers(Session<Server.Entity.Pricing> session);
        Session<Server.Entity.Pricing> SaveDrivers(Session<Server.Entity.Pricing> session);
        Session<Server.Entity.Pricing> LoadPriceLists(Session<Server.Entity.Pricing> session);
        Session<Server.Entity.Pricing> SavePriceLists(Session<Server.Entity.Pricing> session);
    }

    public class PricingData : IPricingData
    {

        #region Constants...
        const String invalid = "Invalid:";
        const String connectionName = "defaultConnectionString";
        const String aplServiceEventLog = "APLServiceEventLog";
        #endregion

        #region Variables...
        private System.Diagnostics.EventLog localServiceLog;
        private APLPX.Server.Data.AnalyticMap sqlMapper;
        private APLPX.Server.Data.SqlService sqlService;
        #endregion

        private String sqlConnection {
            get {
                return System.Configuration.ConfigurationManager.AppSettings[connectionName];
            }
        }

        public PricingData() {

            sqlMapper = new AnalyticMap();
            sqlService = new SqlService(this.sqlConnection);
            localServiceLog = new System.Diagnostics.EventLog();
            //if (!System.Diagnostics.EventLog.SourceExists(APLServiceEventLog)) EventLog.CreateEventSource(APLServiceEventLog, "Application");
            //Setup <APLServiceEventLog> event source manually through registry key: HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application
            //To resolve message IDs create a RG_EXPAND_SZ attribute, named "EventMessageFile" to: "C:\WINDOWS\Microsoft.NET\Framework\<current version>\EventLogMessages.dll"
            localServiceLog.Source = aplServiceEventLog;

        }

        ~PricingData() {
            if (sqlService != null) sqlService.ExecuteCloseConnection();
        }

        public Session<List<Server.Entity.Pricing>> LoadList(Session<Server.Entity.NullT> sessionIn) {
            
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Server.Entity.Pricing>> sessionOut = Session<NullT>.Clone<List<Pricing>>(sessionIn);

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> SaveIdentity(Session<Server.Entity.Pricing> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Pricing> sessionOut = Session<Pricing>.Clone<Pricing>(sessionIn);

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> LoadFilters(Session<Server.Entity.Pricing> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Pricing> sessionOut = Session<Pricing>.Clone<Pricing>(sessionIn);

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> SaveFilters(Session<Server.Entity.Pricing> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Pricing> sessionOut = Session<Pricing>.Clone<Pricing>(sessionIn);

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> LoadDrivers(Session<Server.Entity.Pricing> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Pricing> sessionOut = Session<Pricing>.Clone<Pricing>(sessionIn);

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> SaveDrivers(Session<Server.Entity.Pricing> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Pricing> sessionOut = Session<Pricing>.Clone<Pricing>(sessionIn);

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> LoadPriceLists(Session<Server.Entity.Pricing> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Pricing> sessionOut = Session<Pricing>.Clone<Pricing>(sessionIn);

            return sessionOut;
        }

        public Session<Server.Entity.Pricing> SavePriceLists(Session<Server.Entity.Pricing> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Pricing> sessionOut = Session<Pricing>.Clone<Pricing>(sessionIn);

            return sessionOut;
        }

        public void Dispose() {
            if (sqlService != null)
                if (!sqlService.ExecuteCloseConnection())
                    this.localServiceLog.WriteEntry(sqlService.SqlStatusMessage);
        }
    
    }
}
