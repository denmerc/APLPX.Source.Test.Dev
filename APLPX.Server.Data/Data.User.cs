using System;
using System.Collections.Generic;
using APLPX.Entity;

namespace APLPX.Server.Data {

    public interface IUserData {
        void Dispose();
        Session<Entity.NullT> Initialize(Session<Entity.NullT> session);
        Session<Entity.NullT> Authenticate(Session<Entity.NullT> session);
        Session<Entity.NullT> SavePassword(Session<Entity.NullT> session);
        Session<List<Entity.User>> LoadList(Session<Entity.NullT> session);
        Session<Entity.User> LoadUser(Session<Entity.User> session);
        Session<Entity.User> SaveUser(Session<Entity.User> session);
    }

    public class UserData : IUserData, System.IDisposable  {

        #region Constants...
        const String INVALID = "Invalid:";
        const String CONNECTIONNAME = "defaultConnectionString";
        const String APLSERVICEEVENTLOG = "APLServiceEventLog";
        const Int32 identityData = (Int32)UserMap.DataSets.entitydata;
        const Int32 moduleData = (Int32)UserMap.DataSets.workflowModules;
        const Int32 searchData = (Int32)UserMap.DataSets.workflowSearchGroups;
        const Int32 enumerationData = (Int32)UserMap.DataSets.enumeration;
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
            localServiceLog.Source = APLSERVICEEVENTLOG;
        }

        ~UserData() {
            if (sqlService != null) sqlService.ExecuteCloseConnection();
        }

        public Session<Entity.NullT> Initialize(Session<Entity.NullT> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.NullT> sessionOut = new Session<Entity.NullT> {
                SqlKey = Server.Data.UserMap.Names.sharedKey
            };

            try {
                sqlMapper.InitializeMapParameters(sessionOut, ref sqlService);
                System.Data.DataSet dataSet = sqlService.ExecuteReaders();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut = sqlMapper.InitializeMapData(dataSet.Tables[identityData]);
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

        public Session<Entity.NullT> Authenticate(Session<Entity.NullT> sessionIn) {

            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.NullT> sessionOut = sessionIn.Clone<NullT>(new NullT());

            try {
                sqlMapper.AuthenticateMapParameters(sessionOut, ref sqlService);
                System.Data.DataSet dataSet = sqlService.ExecuteReaders();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.User = sqlMapper.AuthenticateMapData(dataSet.Tables[identityData]);
                        sessionOut.Modules = sqlMapper.AuthenticateMapWorkflowModules(dataSet.Tables[moduleData], dataSet.Tables[searchData]);
                        sessionOut.SessionOk = true;
                        sessionOut.SqlKey = sessionOut.User.Key;
                        sessionOut.Authenticated = sessionOut.SessionOk;
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

        public Session<Entity.NullT> SavePassword(Session<Entity.NullT> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.NullT> sessionOut = sessionIn.Clone<NullT>(new NullT());

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
                    sessionOut.User.Credential.NewPassword = sessionOut.User.Credential.OldPassword;
                }
            }
            return sessionOut;
        }

        public Session<Entity.User> LoadUser(Session<Entity.User> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<Entity.User> sessionOut = sessionIn.Clone<User>(sessionIn.Data);

            try {
                sqlMapper.LoadUserMapParameters(sessionOut, ref sqlService);
                System.Data.DataSet dataSet = sqlService.ExecuteReaders();
                if (sqlService.SqlStatusOk) {
                    sqlRequest = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbValue;
                    sqlResponse = sqlService.sqlParameters[Data.UserMap.Names.sqlMessage].dbOutput;
                    if (sqlRequest == sqlResponse) {
                        sessionOut.Data = sqlMapper.LoadUserMapData(dataSet);
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

        public Session<List<Entity.User>> LoadList(Session<Entity.NullT> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session...
            Session<List<Entity.User>> sessionOut = Session<NullT>.Clone<List<Entity.User>>(sessionIn);

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

        public Session<Entity.User> SaveUser(Session<Entity.User> sessionIn) {
            String sqlRequest = String.Empty;
            String sqlResponse = String.Empty;
            //Initialize session out...
            Session<Entity.User> sessionOut = sessionIn.Clone<User>(sessionIn.Data);

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

        public void Dispose() {
            if (sqlService != null)
                if (!sqlService.ExecuteCloseConnection())
                    this.localServiceLog.WriteEntry(sqlService.SqlStatusMessage);
        }
    }
}
