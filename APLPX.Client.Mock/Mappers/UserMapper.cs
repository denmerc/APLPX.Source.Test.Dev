using System;
using System.Collections.Generic;
using System.Linq;

using DTO = APLPX.Entity;
using Display = APLPX.Common.Mock.Entity;


namespace APLPX.Client.Mock.Mappers
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
            displayEntity.Key = dto.Key;
            displayEntity.Identity = dto.Identity.ToDisplayEntity();
            displayEntity.Role = dto.Role.ToDisplayEntity();
            if (dto.Credential != null)
            {
                displayEntity.Credential.Login = dto.Credential.Login;
                displayEntity.Credential.OldPassword = dto.Credential.OldPassword;
                displayEntity.Credential.NewPassword = dto.Credential.NewPassword;
            }

            return displayEntity;
        }

        public static DTO.User ToDto(this Display.User displayEntity)
        {
            DTO.UserCredential credential = new DTO.UserCredential(displayEntity.Credential.Login, displayEntity.Credential.OldPassword, displayEntity.Credential.NewPassword);

            var dto = new DTO.User(
                                displayEntity.Id,
                                displayEntity.Key,
                                displayEntity.Role.ToDto(),
                                displayEntity.Identity.ToDto(),
                                credential,
                                displayEntity.RoleTypes.ToDTOList());

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
