using APLPX.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APLPX.Server.Services
{
    public static class LogServiceExtensions
    {

        public static void LogServiceResponse<T, T2>(this T serviceType, Session<T2> session, string source)
            where T : class
            where T2 : class
        {

            var sessionCopy = new Session<T2>
            {
                AppOnline = session.AppOnline,
                Authenticated = session.Authenticated,
                ClientCommand = session.ClientCommand,
                ClientMessage = session.ClientMessage,
                Data = session.Data,
                Modules = null,
                ServerMessage = session.ServerMessage,
                SessionOk = session.SessionOk,
                SqlAuthorization = session.SqlAuthorization,
                SqlKey = session.SqlKey.Substring(0, 8),

                User = session.User,
                WinAuthorization = session.WinAuthorization,
            };
            if (sessionCopy.User != null && sessionCopy.User.Credential != null)
            {
                sessionCopy.User.Credential.NewPassword = string.Empty;
                sessionCopy.User.Credential.OldPassword = string.Empty;
            }



            string json = JsonConvert.SerializeObject(sessionCopy);
            //NLog.LogManager.GetCurrentClassLogger().Log<string>(NLog.LogLevel.Debug,
            //    methodName, json);
            NLog.LogManager.GetLogger(source).Log(NLog.LogLevel.Debug, json);
        }

    }
}
