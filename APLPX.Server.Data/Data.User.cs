using System;
using System.Collections.Generic;
using APLPX.Server.Entity;

namespace APLPX.Server.Data {

    public interface IUserData {
        void Dispose();
        Session<Server.Entity.NullT> Initialize(Session<Server.Entity.NullT> session);
        Session<Server.Entity.NullT> Authenticate(Session<Server.Entity.NullT> session);
        Session<List<Server.Entity.User>> LoadList(Session<Server.Entity.NullT> session);
        Session<Server.Entity.User> LoadUser(Session<Server.Entity.User> session);
        Session<Server.Entity.User> SaveUser(Session<Server.Entity.User> session);
        Session<Server.Entity.NullT> SavePassword(Session<Server.Entity.NullT> session);
    }

    public class UserData : IUserData, System.IDisposable  {

        #region Constants...
        const String INVALID = "Invalid:";
        const String CONNECTIONNAME = "defaultConnectionString";
        const String APLSERVICEEVENTNLOG = "APLServiceEventLog";
        #endregion

        #region Variables...
        private System.Diagnostics.EventLog localServiceLog;
        private APLPX.Server.Data.UserMap sqlMapper;
        private APLPX.Server.Data.SqlService sqlService;
        #endregion

        private String sqlConnection {
            get {
                return System.Configuration.ConfigurationManager.AppSettings[CONNECTIONNAME];
            }
        }

        public UserData() {
            sqlMapper = new UserMap();            
            sqlService = new SqlService(this.sqlConnection);
            localServiceLog = new System.Diagnostics.EventLog();
            //if (!System.Diagnostics.EventLog.SourceExists(APLServiceEventLog)) EventLog.CreateEventSource(APLServiceEventLog, "Application");
            //Setup <APLServiceEventLog> event source manually through registry key: HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application
            //To resolve message IDs create a RG_EXPAND_SZ attribute, named "EventMessageFile" to: "C:\WINDOWS\Microsoft.NET\Framework\<current version>\EventLogMessages.dll"
            localServiceLog.Source = APLSERVICEEVENTNLOG;
        }

        ~UserData() {
            if (sqlService != null) sqlService.ExecuteCloseConnection();
        }

        public Session<Server.Entity.NullT> Initialize(Session<Server.Entity.NullT> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.NullT> sessionOut = new Session<Server.Entity.NullT> {
                SqlKey = Server.Data.UserMap.Names.sharedKey
            };

            try {
                sqlMapper.InitializeMapParameters(sessionOut, ref sqlService);
                System.Data.DataSet dataSet = sqlService.ExecuteReaders();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut = sqlMapper.InitializeMapData(dataSet.Tables[(Int32)UserMap.DataSets.entitydata]);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionIn.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.NullT> Authenticate(Session<Server.Entity.NullT> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.NullT> sessionOut = sessionIn.Clone<NullT>(new NullT());

            try {
                sqlMapper.AuthenticateMapParameters(sessionOut, ref sqlService);
                System.Data.DataSet dataSet = sqlService.ExecuteReaders();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.User = sqlMapper.AuthenticateMapData(dataSet.Tables[(Int32)UserMap.DataSets.entitydata]);
                        sessionOut.SqlKey = sessionOut.User.Key;
                        sessionOut.User.RoleTypes = sqlMapper.EnumerationMapData(dataSet.Tables[(Int32)UserMap.DataSets.enumeration]);
                        //TODO: Move to Season...
                        //sessionOut.User.Modules = sqlMapper.AuthenticateMapWorkflowModules(dataSet.Tables[(Int32)UserMap.DataSets.workflowModules]);
                        sessionOut.SessionOk = true;
                        sessionOut.Authenticated = sessionOut.SessionOk;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionIn.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.User> LoadUser(Session<Server.Entity.User> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.User> sessionOut = sessionIn.Clone<User>(sessionIn.Data);

            try {
                sqlMapper.LoadUserMapParameters(sessionOut, ref sqlService);
                System.Data.DataSet dataSet = sqlService.ExecuteReaders();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data = sqlMapper.LoadUserMapData(dataSet.Tables[(Int32)UserMap.DataSets.entitydata]);
                        sessionOut.User.RoleTypes = sqlMapper.EnumerationMapData(dataSet.Tables[(Int32)UserMap.DataSets.enumeration]);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionIn.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }
            return sessionOut;
        }

        public Session<List<Server.Entity.User>> LoadList(Session<Server.Entity.NullT> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Server.Entity.User>> sessionOut = Session<NullT>.Clone<List<Server.Entity.User>>(sessionIn);

            try {
                sqlMapper.LoadListMapParameters(sessionOut, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data = sqlMapper.LoadListMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionIn.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }

            return sessionOut;
        }

        public Session<Server.Entity.User> SaveUser(Session<Server.Entity.User> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session out...
            Session<Server.Entity.User> sessionOut = sessionIn.Clone<User>(sessionIn.Data);

            try {
                sqlMapper.SaveUserMapParameters(sessionIn, ref sqlService);
                System.Data.DataTable dataTable = sqlService.ExecuteReader();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data = sqlMapper.SaveUserMapData(dataTable);
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionIn.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                }
            }
            return sessionOut;
        }

        public Session<Server.Entity.NullT> SavePassword(Session<Server.Entity.NullT> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Server.Entity.NullT> sessionOut = sessionIn.Clone<NullT>(new NullT());

            try {
                sqlMapper.SavePasswordMapParameters(sessionOut, ref sqlService);
                if (sqlService.ExecuteNonQuery()) {
                    sqlRequest = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.SessionOk = true;
                    }
                }
            }
            catch (Exception ex) {
                sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3}, {4} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, ex.Source, ex.Message);
                localServiceLog.WriteEntry(sessionIn.ServerMessage, System.Diagnostics.EventLogEntryType.FailureAudit);
            }
            finally {
                //SQL Service error...
                if (!sqlService.SqlStatusOk) {
                    sessionOut.SessionOk = sqlService.SqlStatusOk;
                    sessionOut.ClientMessage = sqlService.SqlStatusMessage;
                    sessionOut.ServerMessage = String.Format("{0}: {1}, {2}, {3} ", APLSERVICEEVENTNLOG, sqlService.SqlProcedure, sqlRequest, sqlService.SqlStatusMessage);
                }
                //SQL Validation warning...
                else if (sqlRequest != sqlResponse) {
                    sessionOut.ClientMessage = sqlResponse;
                    sessionOut.User.Credential.NewPassword = sessionOut.User.Credential.OldPassword;
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
