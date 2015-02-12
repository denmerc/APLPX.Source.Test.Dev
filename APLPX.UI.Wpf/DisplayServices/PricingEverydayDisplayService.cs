using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APLPX.Client.Contracts;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Helpers;
using APLPX.UI.WPF.Mappers;
using DTO = APLPX.Entity;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayServices
{
    public class PricingEverydayDisplayService
    {
        public PricingEverydayDisplayService(IPricingEverydayService pricingService, DTO.Session<DTO.NullT> session)
        {
            if (pricingService == null)
            {
                throw new ArgumentNullException("pricingService", "Value cannot be null.");
            }
            if (session == null)
            {
                throw new ArgumentNullException("session", "Value cannot be null.");
            }


            _pricingEverydayService = pricingService;
            _session = session;
        }
        private readonly IPricingEverydayService _pricingEverydayService;
        private readonly DTO.Session<DTO.NullT> _session;

        #region Public Methods


        public Session<DTO.PricingEveryday> LoadPricingEveryDay(PricingIdentity identity, int selectedEntityId)
        {
            DTO.PricingIdentity id = identity.ToDto();
            var ids = new DTO.PricingEveryday(selectedEntityId, id);
            var response = _pricingEverydayService.LoadPricingEveryday(new DTO.Session<DTO.PricingEveryday>() { Data = ids });
            return CreateDisplayResponse<DTO.PricingEveryday>(response);
        }



        private Session<T> CreateDisplayResponse<T>(DTO.Session<T> response) 
        {
            //var d = (response.Data as DTO.PricingEveryday);
            //var p = d.ToDisplayEntity();
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
        #endregion
    }
}
