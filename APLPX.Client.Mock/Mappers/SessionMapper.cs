using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
using Display = APLPX.Common.Mock.Entity;


namespace APLPX.Client.Mock.Mappers
{
    /// <summary>
    /// Extension methods for mapping Session display and client entities.
    /// </summary>
    public static class SessionMapper
    {
        public static Display.Session<T> ToDisplayEntity<T>(this DTO.Session<T> dto) where T : class
        {
            var displayEntity = new Display.Session<T>();

            //displayEntity.User = dto.User.ToDisplayEntity();
            //displayEntity.Data = dto.Data as T;
            //displayEntity.AppOnline = dto.AppOnline;
            //displayEntity.Authenticated = dto.Authenticated;
            //displayEntity.SqlAuthorization = dto.SqlAuthorization;
            //displayEntity.WinAuthorization = dto.WinAuthorization;
            //displayEntity.SessionOk = dto.SessionOk;
            //displayEntity.ClientMessage = dto.ClientMessage;
            //displayEntity.ServerMessage = dto.ServerMessage;
            //displayEntity.Modules = dto.Modules.ToDisplayEntities();	//BMB - Change based on changeset #289 (Change Feature to List of Modules (Entity.Session))           

            return displayEntity;
        }

        public static DTO.Session<T> ToDto<T>(this Display.Session<T> displayEntity) where T : class
        {
            var dto = new DTO.Session<T>();

            //dto.User = displayEntity.User.ToDto();
            //dto.Data = displayEntity.Data as T;
            //dto.AppOnline = displayEntity.AppOnline;
            //dto.Authenticated = displayEntity.Authenticated;
            //dto.SqlAuthorization = displayEntity.SqlAuthorization;
            //dto.WinAuthorization = displayEntity.WinAuthorization;
            //dto.SessionOk = displayEntity.SessionOk;
            //dto.ClientMessage = displayEntity.ClientMessage;
            //dto.ServerMessage = displayEntity.ServerMessage;
            //dto.Modules = displayEntity.Modules.ToDTOs();		    //BMB - Change based on changeset #289 (Change Feature to List of Features (Entity.Session)) 
            
            return dto;
        }
    }
}
