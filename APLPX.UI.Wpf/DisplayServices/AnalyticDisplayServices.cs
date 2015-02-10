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

        public DTO.Session<DTO.Analytic> RunResults(DisplayEntities.Analytic analytic)
        {
            DisplayEntities.Analytic payload = analytic.ToPayload();
            payload.ValueDrivers = analytic.ValueDrivers;

            var session = CreateNewRequest(payload);
            var response = _analyticService.SaveDrivers(session);
            return response;
        }




        private DTO.Session<DTO.Analytic> CreateNewRequest(Analytic payload)
        {

            var p = payload.ToPayload().ToDto();
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
