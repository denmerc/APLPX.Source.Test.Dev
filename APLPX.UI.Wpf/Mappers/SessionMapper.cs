using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Methods for mapping Session display and client entities.
    /// </summary>
    public class SessionMapper<T> where T : class
    {
        public static Display.Session<T> ToDisplayEntity(DTO.Session<T> dto)
        {
            var displayEntity = new Display.Session<T>();

            displayEntity.UserIdentity = dto.UserIdentity.ToDisplayEntity();
            displayEntity.Data = dto.Data as T;
            displayEntity.AppOnline = dto.AppOnline;
            displayEntity.Authenticated = dto.Authenticated;
            displayEntity.SqlAuthorization = dto.SqlAuthorization;
            displayEntity.WinAuthorization = dto.WinAuthorization;
            displayEntity.SessionOk = dto.SessionOk;
            displayEntity.ClientMessage = dto.ClientMessage;
            displayEntity.ServerMessage = dto.ServerMessage;
            displayEntity.Feature = dto.Feature.ToDisplayEntity();           

            return displayEntity;
        }

        public static DTO.Session<T> ToDto(Display.Session<T> displayEntity)
        {
            var dto = new DTO.Session<T>();

            dto.UserIdentity = displayEntity.UserIdentity.ToDto();
            dto.Data = displayEntity.Data as T;
            dto.AppOnline = displayEntity.AppOnline;
            dto.Authenticated = displayEntity.Authenticated;
            dto.SqlAuthorization = displayEntity.SqlAuthorization;
            dto.WinAuthorization = displayEntity.WinAuthorization;
            dto.SessionOk = displayEntity.SessionOk;
            dto.ClientMessage = displayEntity.ClientMessage;
            dto.ServerMessage = displayEntity.ServerMessage;
            dto.Feature = displayEntity.Feature.ToDto();
            
            return dto;
        }
    }
}
