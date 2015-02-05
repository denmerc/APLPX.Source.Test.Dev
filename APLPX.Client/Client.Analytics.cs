using System.ServiceModel;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using APLPX.Client.Contracts;

//using APLPX.Client.Entity;
using APLPX.Entity;
using Newtonsoft.Json;
using System.Reflection;

namespace APLPX.Client
{
    public static class LogExtensions
	{
		
        public static void LogClientRequest<T,T2>(this ClientBase<T> proxyType, Session<T2> session, string source) where T : class
                                                                                                                        where T2 : class
        {
            var sessionCopy = new Session<T2>
            {
                AppOnline = session.AppOnline,
                Authenticated = session.Authenticated,
                ClientCommand = session.ClientCommand,
                ClientMessage = session.ClientMessage += source,
                Data = session.Data,
                Modules = null,
                ServerMessage = session.ServerMessage,
                SessionOk = session.SessionOk,
                SqlAuthorization = session.SqlAuthorization,
                SqlKey = session.SqlKey.Substring(0,8), 
                    
                User = session.User,
                WinAuthorization = session.WinAuthorization,
            };
                
            //sessionCopy.ClientMessage += methodName;
            //sessionCopy.SqlKey = session.SqlKey.Substring(0, 8);
            //sessionCopy.Data.SqlKey = session.SqlKey.Substring(0, 8);

            string json = JsonConvert.SerializeObject(session);
            //NLog.LogManager.GetCurrentClassLogger().Log<string>(NLog.LogLevel.Debug,
            //    methodName, json);
            NLog.LogManager.GetLogger(source).Log(NLog.LogLevel.Debug, json);
        }
	}


    [Export]
    public class AnalyticClient : ClientBase<IAnalyticService>, IAnalyticService
    {
        public Session<List<Analytic>> LoadList(Session<NullT> session)
        {
            this.LogClientRequest<IAnalyticService, NullT>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString());

                return Channel.LoadList(session);

            //LogIncomingSession<NullT>(session);

        }

        public Session<Analytic> SaveIdentity(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString());
            return Channel.SaveIdentity(session);
        }

        public Session<Analytic> LoadFilters(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString()); 
            
            return Channel.LoadFilters(session);
        }

        public Session<Analytic> SaveFilters(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString()); 
            
            return Channel.SaveFilters(session);
        }

        //public Session<Analytic> LoadDriver(Session<Analytic> session) {
        //    return Channel.LoadDriver(session);
        //}

        public Session<Analytic> LoadDrivers(Session<Analytic> session)
        {

            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString()); 
            return Channel.LoadDrivers(session);
        }

        //public Session<Analytic> SaveDriver(Session<Analytic> session) {
        //    return Channel.SaveDriver(session);
        //}

        public Session<Analytic> SaveDrivers(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString());
            return Channel.SaveDrivers(session);
        }

        public Session<Analytic> LoadPriceLists(Session<Analytic> session) 
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                            MethodBase.GetCurrentMethod().ToString());
            return Channel.LoadPriceLists(session);
        }

        public Session<Analytic> SavePriceLists(Session<Analytic> session) 
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString());
            return Channel.SavePriceLists(session);
        }

        public Session<Analytic> LoadAnalytic(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString());
            return Channel.Load(session);
        }


        public Session<Analytic> LoadResults(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString());
            throw new System.NotImplementedException();
        }

        public Session<Analytic> Load(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString());
            return Channel.Load(session);
        }

        public Session<Analytic> LoadIdentity(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString());
            throw new System.NotImplementedException();
        }

        public Session<Analytic> RunDrivers(Session<Analytic> session)
        {
            this.LogClientRequest<IAnalyticService, Analytic>(session, "[" + this.GetType().Name + "]." +
                MethodBase.GetCurrentMethod().ToString());
            throw new System.NotImplementedException();
        }
    }
}
