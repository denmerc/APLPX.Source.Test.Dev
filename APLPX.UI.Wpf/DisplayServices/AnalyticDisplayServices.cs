using System;
using System.Collections.Generic;

using APLPX.Client.Contracts;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Mappers;
using DTO = APLPX.Client.Entity;

namespace APLPX.UI.WPF.DisplayServices
{
    /// <summary>
    /// Wrapper class for calling IAnalyticService methods from the display layer.
    /// </summary>
    public class AnalyticDisplayServices
    {
        private readonly IAnalyticService _analyticService;

        #region Constructors

        public AnalyticDisplayServices(IAnalyticService analyticService)
        {
            if (analyticService == null)
            {
                throw new ArgumentNullException("analyticService", "Value cannot be null.");
            }

            _analyticService = analyticService;
        }

        #endregion

        #region Public Methods

        public List<Analytic> LoadAnalytics(DTO.Session<DTO.NullT> session)
        {            
            DTO.Session<List<DTO.Analytic>> response = _analyticService.LoadList(session);

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

            DTO.Session<DTO.Analytic> response = _analyticService.LoadFilters(sessionDto);

            var displayList = new List<FilterGroup>();
            foreach (DTO.FilterGroup filterGroup in response.Data.FilterGroups)
            {
                displayList.Add(filterGroup.ToDisplayEntity());
            }

            return displayList;
        }

        public List<AnalyticDriver> LoadDrivers(Analytic displayAnalytic)
        {
            DTO.Analytic payload = displayAnalytic.ToDto();
            var sessionDto = new DTO.Session<DTO.Analytic> { Data = payload };

            DTO.Session<DTO.Analytic> response = _analyticService.LoadDrivers(sessionDto);

            var displayList = new List<AnalyticDriver>();
            foreach (DTO.AnalyticDriver driver in response.Data.Drivers)
            {
                displayList.Add(driver.ToDisplayEntity());
            }

            return displayList;
        }

        public List<PriceListGroup> LoadPriceListGroupss(Analytic displayAnalytic)
        {
            DTO.Analytic payload = displayAnalytic.ToDto();
            var sessionDto = new DTO.Session<DTO.Analytic> { Data = payload };

            DTO.Session<DTO.Analytic> returnedSession = _analyticService.LoadDrivers(sessionDto);

            var displayList = new List<PriceListGroup>();
            foreach (DTO.PriceListGroup group in returnedSession.Data.PriceListGroups)
            {
                displayList.Add(group.ToDisplayEntity());
            }

            return displayList;
        }

        #endregion
  
    }
}
