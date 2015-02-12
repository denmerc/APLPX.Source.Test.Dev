using System;
using System.Collections.Generic;

using APLPX.Client.Contracts;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Helpers;
using APLPX.UI.WPF.Mappers;
using DTO = APLPX.Entity;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayServices
{
    /// <summary>
    /// Wrapper class for calling IAnalyticService methods from the display layer.
    /// </summary>
    public class AnalyticDisplayServices
    {
        private readonly IAnalyticService _analyticService;
        private DTO.Session<DTO.NullT> _session;

        #region Constructors

        public AnalyticDisplayServices(IAnalyticService analyticService, DTO.Session<DTO.NullT> session)
        {
            if (analyticService == null)
            {
                throw new ArgumentNullException("analyticService", "Value cannot be null.");
            }
            if (session == null)
            {
                throw new ArgumentNullException("session", "Value cannot be null.");
            }


            _analyticService = analyticService;
            _session = session;
        }

        #endregion

        #region Public Methods

        public Session<List<DTO.Analytic>> LoadAnalyticList()
        {
            var response = _analyticService.LoadList(new DTO.Session<DTO.NullT> { SqlKey = _session.SqlKey });
            List<DTO.Analytic> analyticDtos = response.Data;
            var displayAnalytics = analyticDtos.ToDisplayEntities();
            return CreateDisplayResponse<List<DTO.Analytic>>(response);
        }


        public Session<DTO.Analytic> LoadAnalytic(DisplayEntities.Analytic analytic, int entityId, int searchGroupId)
        {
            var payload = new DTO.Analytic(entityId);
            payload.SearchGroupId = searchGroupId;
            var response = _analyticService.Load(new DTO.Session<DTO.Analytic>() { Data = payload, SqlKey = _session.SqlKey, ClientCommand = _session.ClientCommand });
            return CreateDisplayResponse<DTO.Analytic>(response);
        }

        public Session<DTO.Analytic> SaveFilters(DisplayEntities.Analytic analytic)
        {
            var payload = analytic.ToPayload();
            payload.FilterGroups = analytic.FilterGroups;
            var session = new DTO.Session<DTO.Analytic>() { Data = payload.ToDto(), SqlKey = _session.SqlKey, ClientCommand = _session.ClientCommand };
            var response = _analyticService.SaveFilters(session);
            return CreateDisplayResponse<DTO.Analytic>(response);
        }

        public Session<DTO.Analytic> SaveAnalyticIdentity(Analytic analytic)
        {
            var payload = analytic.ToPayload();
            payload.Identity = analytic.Identity;
            var session = new DTO.Session<DTO.Analytic>() { Data = payload.ToDto(), SqlKey = _session.SqlKey, ClientCommand = _session.ClientCommand };
            var response = _analyticService.SaveIdentity(session);
            return CreateDisplayResponse<DTO.Analytic>(response);
        }

        public DTO.Session<DTO.Analytic> RunResults(DisplayEntities.Analytic analytic)
        {
            DisplayEntities.Analytic payload = analytic.ToPayload();
            payload.ValueDrivers = analytic.ValueDrivers;

            var session = CreateRequest(payload);
            var response = _analyticService.SaveDrivers(session);
            return response;
        }

        public Session<DTO.Analytic> SavePriceLists(Analytic analytic)
        {
            var payload = analytic.ToPayload();
            payload.PriceListGroups = analytic.PriceListGroups;
            var session = new DTO.Session<DTO.Analytic>() { Data = payload.ToDto(), SqlKey = _session.SqlKey, ClientCommand = _session.ClientCommand };


            var response = _analyticService.SavePriceLists(session);
            return CreateDisplayResponse<DTO.Analytic>(response);
        }

        public Session<DTO.Analytic> LoadPriceLists(Analytic analytic)
        {
            var payload = analytic.ToPayload();
            payload.PriceListGroups = analytic.PriceListGroups;
            var session = new DTO.Session<DTO.Analytic>() { Data = payload.ToDto(), SqlKey = _session.SqlKey, ClientCommand = _session.ClientCommand };
            var response =  _analyticService.SavePriceLists(session);
            return CreateDisplayResponse<DTO.Analytic>(response);
        }

        //private Session<Analytic> CreateResponseForUI(DTO.Session<DTO.Analytic> response )
        //{
        //    Analytic a = null;
        //    if(response.Data != null)
        //    {
        //        var d = (response.Data as DTO.Analytic);
        //        a = d.ToDisplayEntity();
        //    }
        //    return new Session<Analytic>
        //    {
        //        Authenticated = response.Authenticated,
        //        SqlAuthorization = response.SqlAuthorization,
        //        ClientMessage = response.ClientMessage ?? null,
        //        Data = a ,
        //        ServerMessage = response.ServerMessage ?? null,
        //        SessionOk = response.SessionOk,
        //        User = response.User != null ? response.User.ToDisplayEntity() : null,

        //    };
        //}

        private Session<T> CreateDisplayResponse<T>(DTO.Session<T> response) where T : class
        {

            //if (response.Data != null)
            //{
            //    var d = (response.Data as Tin);
            //    //a = d.ToDisplayEntity();
            //}
            return new Session<T>
            {
                Authenticated = response.Authenticated,
                SqlAuthorization = response.SqlAuthorization,
                ClientMessage = response.ClientMessage ?? null,
                Data = response.Data,
                ServerMessage = response.ServerMessage ?? null,
                SessionOk = response.SessionOk,
                User = response.User != null ? response.User.ToDisplayEntity() : null,

            };
        }

        private DTO.Session<DTO.Analytic> CreateRequest(Analytic payload)
        {

            var p = payload.ToDto();
            var session = new DTO.Session<DTO.Analytic>()
            {
                Data = p,
                SqlKey = _session.SqlKey,
                ClientCommand = _session.ClientCommand
            };
            return session;
        }


        public List<Analytic> LoadAnalytics(DTO.Session<DTO.NullT> session)
        {
            DTO.Session<List<DTO.Analytic>> response = null; ;//TODO: set to null until AnalyticService contract is updated.// _analyticService.LoadList(session);

            List<Analytic> displayList = new List<Analytic>();
            foreach (DTO.Analytic analytic in response.Data)
            {
                displayList.Add(analytic.ToDisplayEntity());
            }

            return displayList;
        }

        public List<FilterGroup> LoadFilterGroups(Analytic displayAnalytic)
        {
            DTO.Analytic payload = displayAnalytic.ToDto();
            var sessionDto = new DTO.Session<DTO.Analytic> { Data = payload };

            DTO.Session<DTO.Analytic> response = null;//TODO: set to null until AnalyticService contract is updated. _analyticService.LoadFilters(sessionDto);

            var displayList = new List<FilterGroup>();
            foreach (DTO.FilterGroup filterGroup in response.Data.FilterGroups)
            {
                displayList.Add(filterGroup.ToDisplayEntity());
            }

            return displayList;
        }

        public List<AnalyticValueDriver> LoadDrivers(Analytic displayAnalytic)
        {
            DTO.Analytic payload = displayAnalytic.ToDto();
            var sessionDto = new DTO.Session<DTO.Analytic> { Data = payload };

            DTO.Session<DTO.Analytic> response = null;//TODO: set to null until AnalyticService contract is updated. _analyticService.LoadDrivers(sessionDto);

            var displayList = new List<AnalyticValueDriver>();
            foreach (var driver in response.Data.ValueDrivers)
            {
                displayList.Add(driver.ToDisplayEntity());
            }

            return displayList;
        }

        public List<AnalyticPriceListGroup> LoadPriceListGroupss(Analytic displayAnalytic)
        {
            DTO.Analytic payload = displayAnalytic.ToDto();
            var sessionDto = new DTO.Session<DTO.Analytic> { Data = payload };

            DTO.Session<DTO.Analytic> returnedSession = null; ;//TODO: set to null until AnalyticService contract is updated. _analyticService.LoadDrivers(sessionDto);

            var displayList = new List<AnalyticPriceListGroup>();
            foreach (var group in returnedSession.Data.PriceListGroups)
            {
                displayList.Add(group.ToDisplayEntity());
            }

            return displayList;
        }

        #endregion

    }
}
