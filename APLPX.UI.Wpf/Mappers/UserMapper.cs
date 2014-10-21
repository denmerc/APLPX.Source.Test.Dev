using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping User display and client entities.
    /// </summary>
    public static class UserMapper
    {
        #region User

        public static Display.User ToDisplayEntity(this DTO.User dto)
        {
            var displayEntity = new Display.User();

            displayEntity.Id = dto.Id;
            displayEntity.SqlKey = dto.SqlKey;
            displayEntity.Identity = dto.Identity.ToDisplayEntity();
            displayEntity.Role = dto.Role.ToDisplayEntity();
            displayEntity.Modules = dto.Modules.ToDisplayEntities();
            displayEntity.OldPassword = dto.Password.Old;
            displayEntity.NewPassword = dto.Password.New;

            return displayEntity;
        }

        public static DTO.User ToDto(this Display.User displayEntity)
        {
            var dto = new DTO.User(
                                displayEntity.Id,
                                displayEntity.SqlKey,
                                displayEntity.Role.ToDto(),
                                displayEntity.Identity.ToDto(),
                                displayEntity.Modules.ToDTOs(),
                                displayEntity.RoleTypes.ToDTOs());

            return dto;
        }

        #endregion

        #region User Role mapping

        public static Display.UserRole ToDisplayEntity(this DTO.UserRole dto)
        {
            var displayEntity = new Display.UserRole();

            displayEntity.Id = dto.Id;
            displayEntity.Name = dto.Name;
            displayEntity.Description = dto.Description;

            return displayEntity;
        }

        public static DTO.UserRole ToDto(this Display.UserRole displayEntity)
        {
            var dto = new DTO.UserRole(
                                    displayEntity.Id,
                                    displayEntity.Name,
                                    displayEntity.Description);

            return dto;
        }

        #endregion
    }
}
