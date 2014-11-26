using System;
using System.Collections.Generic;
using APLPX.Server.Data;
using APLPX.Server.Entity;

namespace APLPX.Server.Data {

    public interface IAnalyticData {
        void Dispose();
        Session<List<Server.Entity.Analytic>> LoadList(Session<Server.Entity.NullT> session);
        Session<Server.Entity.Analytic> SaveIdentity(Session<Server.Entity.Analytic> session);
        Session<Server.Entity.Analytic> LoadFilters(Session<Server.Entity.Analytic> session);
        Session<Server.Entity.Analytic> SaveFilters(Session<Server.Entity.Analytic> session);
        Session<Server.Entity.Analytic> LoadDriver(Session<Server.Entity.Analytic> session);
        Session<Server.Entity.Analytic> LoadDrivers(Session<Server.Entity.Analytic> session);
        Session<Server.Entity.Analytic> SaveDriver(Session<Server.Entity.Analytic> session);
        Session<Server.Entity.Analytic> SaveDrivers(Session<Server.Entity.Analytic> session);
        Session<Server.Entity.Analytic> LoadPriceLists(Session<Server.Entity.Analytic> session);
        Session<Server.Entity.Analytic> SavePriceLists(Session<Server.Entity.Analytic> session);
    }

    public class AnalyticData : IAnalyticData {

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

        public AnalyticData() {

            sqlMapper = new AnalyticMap();            
            sqlService = new SqlService(this.sqlConnection);
            localServiceLog = new System.Diagnostics.EventLog();
            //if (!System.Diagnostics.EventLog.SourceExists(APLServiceEventLog)) EventLog.CreateEventSource(APLServiceEventLog, "Application");
            //Setup <APLServiceEventLog> event source manually through registry key: HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application
            //To resolve message IDs create a RG_EXPAND_SZ attribute, named "EventMessageFile" to: "C:\WINDOWS\Microsoft.NET\Framework\<current version>\EventLogMessages.dll"
            localServiceLog.Source = APLSERVICEEVENTLOG;

        }

        ~AnalyticData() {
            if (sqlService != null) sqlService.ExecuteCloseConnection();
        }
        
        public Session<List<Server.Entity.Analytic>> LoadList(Session<Server.Entity.NullT> sessionIn) {
            
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Server.Entity.Analytic>> sessionOut = Session<NullT>.Clone<List<Analytic>>(sessionIn);

            try {
                sqlMapper.LoadListMapParameters(sessionIn, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data = sqlMapper.LoadListMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionIn.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> SaveIdentity(Session<Server.Entity.Analytic> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Analytic> sessionOut = Session<Analytic>.Clone<Analytic>(sessionIn);

            try {
                sqlMapper.SaveIdentityMapParameters(sessionOut, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data.Id = (sqlMapper.SaveIdentityMapData(dataTable)).Id;
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionOut.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> LoadFilters(Session<Server.Entity.Analytic> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Analytic> sessionOut = Session<Analytic>.Clone<Analytic>(sessionIn);

            try {
                sqlMapper.LoadFiltersMapParameters(sessionIn, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data.FilterGroups = sqlMapper.LoadFiltersMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionOut.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> SaveFilters(Session<Server.Entity.Analytic> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Analytic> sessionOut = Session<Analytic>.Clone<Analytic>(sessionIn);

            try {
                sqlMapper.SaveFiltersMapParameters(sessionIn, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data.FilterGroups = sqlMapper.LoadFiltersMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionOut.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> LoadDriver(Session<Server.Entity.Analytic> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Analytic> sessionOut = Session<Analytic>.Clone<Analytic>(sessionIn);

            try {
                sqlMapper.LoadDriversMapParameters(sessionIn, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data.Drivers = sqlMapper.LoadDriversMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionOut.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> LoadDrivers(Session<Server.Entity.Analytic> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Analytic> sessionOut = Session<Analytic>.Clone<Analytic>(sessionIn);

            try {
                sqlMapper.LoadDriversMapParameters(sessionIn, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data.Drivers = sqlMapper.LoadDriversMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionOut.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> SaveDriver(Session<Server.Entity.Analytic> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Analytic> sessionOut = Session<Analytic>.Clone<Analytic>(sessionIn);

            try {
                sqlMapper.SaveDriversMapParameters(sessionIn, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data.Drivers = sqlMapper.LoadDriversMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionOut.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> SaveDrivers(Session<Server.Entity.Analytic> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Analytic> sessionOut = Session<Analytic>.Clone<Analytic>(sessionIn);

            try {
                sqlMapper.SaveDriversMapParameters(sessionIn, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data.Drivers = sqlMapper.LoadDriversMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionOut.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> LoadPriceLists(Session<Server.Entity.Analytic> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Analytic> sessionOut = Session<Analytic>.Clone<Analytic>(sessionIn);

            try {
                sqlMapper.LoadPricelistsMapParameters(sessionIn, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data.PriceListGroups = sqlMapper.LoadPricelistsMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionOut.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.Analytic> SavePriceLists(Session<Server.Entity.Analytic> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.Analytic> sessionOut = Session<Analytic>.Clone<Analytic>(sessionIn);

            try {
                sqlMapper.SavePricelistsMapParameters(sessionIn, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Server.Data.AnalyticMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data.PriceListGroups = sqlMapper.LoadPricelistsMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionOut.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public void Dispose() {
            if (sqlService != null)
                if (!sqlService.ExecuteCloseConnection())
                    this.localServiceLog.WriteEntry(sqlService.SqlStatusMessage);
        }
    }
}
