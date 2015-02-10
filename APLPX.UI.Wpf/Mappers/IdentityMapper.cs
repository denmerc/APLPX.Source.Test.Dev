using System;
using System.Collections.Generic;

using DTO = APLPX.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Identity display and client entities.
    /// </summary>
    public static class IdentityMapper
    {
        #region Analytic Identity

        public static Display.AnalyticIdentity ToDisplayEntity(this DTO.AnalyticIdentity dto)
        {
            var displayEntity = new Display.AnalyticIdentity();

            displayEntity.Name = dto.Name;
            displayEntity.Description = dto.Description;
            displayEntity.Notes = dto.Notes;
            displayEntity.RefreshedText = dto.RefreshedText;
            displayEntity.CreatedText = dto.CreatedText;
            displayEntity.EditedText = dto.EditedText;
            displayEntity.Refreshed = dto.Refreshed;
            displayEntity.Created = dto.Created;
            displayEntity.Edited = dto.Edited;
            displayEntity.Author = dto.Author;
            displayEntity.Editor = dto.Editor;
            displayEntity.Owner = dto.Owner;
            displayEntity.IsActive = dto.Active;
            displayEntity.Shared = dto.Shared;

            displayEntity.IsDirty = false;

            return displayEntity;
        }

        public static DTO.AnalyticIdentity ToDto(this Display.AnalyticIdentity displayEntity)
        {
            var dto = new DTO.AnalyticIdentity(
                                        displayEntity.Name,
                                        displayEntity.Description,
                                        displayEntity.Notes,
                                        displayEntity.RefreshedText,
                                        displayEntity.CreatedText,
                                        displayEntity.EditedText,
                                        displayEntity.Refreshed,
                                        displayEntity.Created,
                                        displayEntity.Edited,
                                        displayEntity.Author,
                                        displayEntity.Editor,
                                        displayEntity.Owner,
                                        displayEntity.Shared,
                                        displayEntity.IsActive);

            return dto;
        }

        #endregion

        #region Pricing Identity

        public static Display.PricingIdentity ToDisplayEntity(this DTO.PricingIdentity dto)
        {
            var displayEntity = new Display.PricingIdentity();

            displayEntity.AnalyticsId = dto.AnalyticsId;
            displayEntity.Name = dto.Name;
            displayEntity.Description = dto.Description;
            displayEntity.Notes = dto.Notes;
            displayEntity.RefreshedText = dto.RefreshedText;
            displayEntity.CreatedText = dto.CreatedText;
            displayEntity.EditedText = dto.EditedText;
            displayEntity.Refreshed = dto.Refreshed;
            displayEntity.Created = dto.Created;
            displayEntity.Edited = dto.Edited;
            displayEntity.Author = dto.Author;
            displayEntity.Editor = dto.Editor;
            displayEntity.Owner = dto.Owner;
            displayEntity.Shared = dto.Shared;
            displayEntity.Active = dto.Active;

            return displayEntity;
        }

        public static DTO.PricingIdentity ToDto(this Display.PricingIdentity displayEntity)
        {
            var dto = new DTO.PricingIdentity(
                                            displayEntity.AnalyticsId,
                                            displayEntity.Name,
                                            displayEntity.Description,
                                            displayEntity.Notes,
                                            displayEntity.RefreshedText,
                                            displayEntity.CreatedText,
                                            displayEntity.EditedText,
                                            displayEntity.Refreshed,
                                            displayEntity.Created,
                                            displayEntity.Edited,
                                            displayEntity.Author,
                                            displayEntity.Editor,
                                            displayEntity.Owner,
                                            displayEntity.Shared,
                                            displayEntity.Active);

            return dto;
        }

        #endregion

        #region User Identity

        public static Display.UserIdentity ToDisplayEntity(this DTO.UserIdentity dto)
        {
            var displayEntity = new Display.UserIdentity();

            displayEntity.Active = dto.Active;
            displayEntity.Email = dto.Email;
            displayEntity.Name = dto.Name;
            displayEntity.FirstName = dto.FirstName;
            displayEntity.LastName = dto.LastName;
            displayEntity.Greeting = dto.Greeting;
            displayEntity.LastLogin = dto.LastLogin;
            displayEntity.LastLoginText = dto.LastLoginText;
            displayEntity.Created = dto.Created;
            displayEntity.CreatedText = dto.CreatedText;
            displayEntity.Edited = dto.Edited;
            displayEntity.EditedText = dto.EditedText;
            displayEntity.Editor = dto.Editor;

            return displayEntity;
        }

        public static DTO.UserIdentity ToDto(this Display.UserIdentity displayEntity)
        {
            var dto = new DTO.UserIdentity(
                                        displayEntity.Email,
                                        displayEntity.FirstName,
                                        displayEntity.LastName,
                                        displayEntity.Active);

            return dto;
        }


        #endregion
    }
}
