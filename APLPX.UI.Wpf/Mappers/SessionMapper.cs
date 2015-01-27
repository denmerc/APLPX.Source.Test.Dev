using System;
using System.Collections.Generic;

using DTO = APLPX.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Session display and client entities.
    /// </summary>
    public static class SessionMapper
    {
        public static Display.Session<T> ToDisplayEntity<T>(this DTO.Session<T> dto) where T : class
        {
            var displayEntity = new Display.Session<T>();

            displayEntity.User = dto.User.ToDisplayEntity();
            displayEntity.Data = dto.Data as T;
            displayEntity.AppOnline = dto.AppOnline;
            displayEntity.Authenticated = dto.Authenticated;
            displayEntity.SqlAuthorization = dto.SqlAuthorization;
            displayEntity.WinAuthorization = dto.WinAuthorization;
            displayEntity.SessionOk = dto.SessionOk;
            displayEntity.ClientCommand = dto.ClientCommand;
            displayEntity.ClientMessage = dto.ClientMessage;
            displayEntity.ServerMessage = dto.ServerMessage;
            displayEntity.Modules = dto.Modules.ToDisplayEntities();

            return displayEntity;
        }

        public static DTO.Session<T> ToDto<T>(this Display.Session<T> displayEntity) where T : class
        {
            var dto = new DTO.Session<T>();

            dto.User = displayEntity.User.ToDto();
            dto.Data = displayEntity.Data as T;
            dto.AppOnline = displayEntity.AppOnline;
            dto.Authenticated = displayEntity.Authenticated;
            dto.SqlAuthorization = displayEntity.SqlAuthorization;
            dto.WinAuthorization = displayEntity.WinAuthorization;
            dto.SessionOk = displayEntity.SessionOk;
            dto.ClientMessage = displayEntity.ClientMessage;
            dto.ServerMessage = displayEntity.ServerMessage;
            dto.Modules = displayEntity.Modules.ToDTOs();

            return dto;
        }

        /// <summary>
        /// Maps a Session DTO to a Session display entity of type NullT with minimal set of display properties.
        /// </summary>
        public static Display.Session<DTO.NullT> ToDisplaySession<T>(this DTO.Session<T> dto) where T : class
        {
            Display.Session<DTO.NullT> displayEntity = new Display.Session<DTO.NullT>();
            displayEntity.SessionOk = dto.SessionOk;
            displayEntity.ClientCommand = dto.ClientCommand;
            displayEntity.ClientMessage = dto.ClientMessage;
            displayEntity.ServerMessage = dto.ServerMessage;

            return displayEntity;
        }


    }
}
