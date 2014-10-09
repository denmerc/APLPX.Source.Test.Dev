using System;
using System.Collections.Generic;
using APLPX.Server.Data;
using APLPX.Server.Entity;

namespace APLPX.Server.Data
{

    public interface IPricingData
    {
        void Dispose();
        Session<List<Server.Entity.Pricing.Identity>> LoadList(Session<Server.Entity.NullT> session);
        Session<Server.Entity.Pricing.Identity> SaveIdentity(Session<Server.Entity.Pricing.Identity> session);
        Session<List<Server.Entity.Filter>> LoadFilters(Session<Server.Entity.Pricing.Identity> session);
        Session<List<Server.Entity.Filter>> SaveFilters(Session<Server.Entity.Pricing> session);
        Session<List<Server.Entity.Pricing.Driver>> LoadDrivers(Session<Server.Entity.Pricing.Identity> session);
        Session<List<Server.Entity.Pricing.Driver>> SaveDrivers(Session<Server.Entity.Pricing> session);
        Session<List<Server.Entity.PriceList>> LoadPriceLists(Session<Server.Entity.Pricing.Identity> session);
        Session<List<Server.Entity.PriceList>> SavePriceLists(Session<Server.Entity.Pricing> session);
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

        public Session<List<Server.Entity.Pricing.Identity>> LoadList(Session<Server.Entity.NullT> sessionIn) {
            
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Server.Entity.Pricing.Identity>> sessionOut = Session<NullT>.Clone<List<Pricing.Identity>>(sessionIn);

            return sessionOut;
        }

        public Session<Server.Entity.Pricing.Identity> SaveIdentity(Session<Server.Entity.Pricing.Identity> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Pricing.Identity> sessionOut = sessionIn.Clone<Pricing.Identity>(new Pricing.Identity());

            return sessionOut;
        }

        public Session<List<Server.Entity.Filter>> LoadFilters(Session<Server.Entity.Pricing.Identity> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Server.Entity.Filter>> sessionOut = Session<Pricing.Identity>.Clone<List<Filter>>(sessionIn);

            return sessionOut;
        }

        public Session<List<Server.Entity.Filter>> SaveFilters(Session<Server.Entity.Pricing> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Server.Entity.Filter>> sessionOut = Session<Pricing>.Clone<List<Filter>>(sessionIn);

            return sessionOut;
        }

        public Session<List<Server.Entity.Pricing.Driver>> LoadDrivers(Session<Server.Entity.Pricing.Identity> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Server.Entity.Pricing.Driver>> sessionOut = Session<Pricing.Identity>.Clone<List<Pricing.Driver>>(sessionIn);

            return sessionOut;
        }

        public Session<List<Server.Entity.Pricing.Driver>> SaveDrivers(Session<Server.Entity.Pricing> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Server.Entity.Pricing.Driver>> sessionOut = Session<Pricing>.Clone<List<Pricing.Driver>>(sessionIn);

            return sessionOut;
        }

        public Session<List<Server.Entity.PriceList>> LoadPriceLists(Session<Server.Entity.Pricing.Identity> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Server.Entity.PriceList>> sessionOut = Session<Pricing.Identity>.Clone<List<PriceList>>(sessionIn);

            return sessionOut;
        }

        public Session<List<Server.Entity.PriceList>> SavePriceLists(Session<Server.Entity.Pricing> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Server.Entity.PriceList>> sessionOut = Session<Pricing>.Clone<List<PriceList>>(sessionIn);

            return sessionOut;
        }

        public void Dispose() {
            if (sqlService != null)
                if (!sqlService.ExecuteCloseConnection())
                    this.localServiceLog.WriteEntry(sqlService.SqlStatusMessage);
        }
    
    }
}
