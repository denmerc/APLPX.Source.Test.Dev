using System;
using System.Collections.Generic;

using DTO = APLPX.Client.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.Mappers
{
    /// <summary>
    /// Extension methods for mapping Module display and client entities.
    /// </summary>
    public static class ModuleMapper
    {
        #region Module Mapping

        public static Display.Module ToDisplayEntity(this DTO.Module dto)
        {
            var displayEntity = new Display.Module();

            displayEntity.TypeId = dto.Type;
            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.IsVisible = dto.IsVisible;

            foreach (var feature in dto.Features)
            {
                displayEntity.Features.Add(feature.ToDisplayEntity());
            }

            return displayEntity;
        }

        public static DTO.Module ToDto(this Display.Module displayEntity)
        {
            var featureDtos = new List<DTO.ModuleFeature>();
            displayEntity.Features.ForEach(item => featureDtos.Add(item.ToDto()));

            var dto = new DTO.Module(
                                    displayEntity.Name,
                                    displayEntity.Title,
                                    displayEntity.IsVisible,
                                    displayEntity.TypeId,
                                    featureDtos);

            return dto;
        }

        #endregion

        #region Module Feature Mapping

        public static Display.ModuleFeature ToDisplayEntity(this DTO.ModuleFeature dto)
        {
            var result = new Display.ModuleFeature();

            result.TypeId = dto.Type;
            result.Name = dto.Name;
            result.Title = dto.Title;
            result.IsVisible = dto.IsVisible;

            foreach (var folder in dto.Folders)
            {
                result.Folders.Add(folder.ToDisplayEntity());
            }

            foreach (var step in dto.Steps)
            {
                result.Steps.Add(step.ToDisplayEntity());
            }

            return result;
        }

        public static DTO.ModuleFeature ToDto(this Display.ModuleFeature displayEntity)
        {
            var stepDtos = new List<DTO.ModuleFeatureStep>();
            displayEntity.Steps.ForEach(step => stepDtos.Add(step.ToDto()));

            var folderDtos = new List<DTO.Folder>();
            displayEntity.Folders.ForEach(folder => folderDtos.Add(folder.ToDto()));

            var result = new DTO.ModuleFeature(
                                        displayEntity.Name,
                                        displayEntity.Title,
                                        displayEntity.IsVisible,
                                        displayEntity.TypeId,
                                        folderDtos,
                                        stepDtos);

            return result;
        }

        #endregion

        #region Module Feature Step Mapping

        public static Display.ModuleFeatureStep ToDisplayEntity(this DTO.ModuleFeatureStep dto)
        {
            var result = new Display.ModuleFeatureStep();

            result.TypeId = dto.Type;
            result.Index = dto.Index;
            result.Name = dto.Name;
            result.Title = dto.Title;
            result.IsVisible = dto.IsVisible;

            foreach (var advisor in dto.Advisors)
            {
                result.Advisors.Add(advisor.ToDisplayEntity());
            }

            foreach (var error in dto.Errors)
            {
                result.Errors.Add(error.ToDisplayEntity());
            }

            return result;
        }

        public static DTO.ModuleFeatureStep ToDto(this Display.ModuleFeatureStep displayEntity)
        {
            var advisors = new List<DTO.ModuleFeatureStepAdvisor>();
            foreach (var advisor in displayEntity.Advisors)
            {
                advisors.Add(advisor.ToDto());
            }

            var errors = new List<DTO.ModuleFeatureStepError>();
            foreach (var error in displayEntity.Errors)
            {
                errors.Add(error.ToDto());
            }

            var dto = new DTO.ModuleFeatureStep(
                                            displayEntity.Index,
                                            displayEntity.Name,
                                            displayEntity.Title,
                                            displayEntity.IsVisible,
                                            displayEntity.TypeId,
                                            errors, advisors);

            return dto;
        }

        #endregion

        #region Module Collection - top level mapping

        public static List<Display.Module> ToDisplayEntities(this List<DTO.Module> dtos)
        {
            var displayList = new List<Display.Module>();

            foreach (DTO.Module moduleDTO in dtos)
            {
                displayList.Add(moduleDTO.ToDisplayEntity());
            }
            return displayList;
        }

        public static List<DTO.Module> ToDTOs(this List<Display.Module> modules)
        {
            var dtoList = new List<DTO.Module>();

            foreach (var displayModule in modules)
            {
                dtoList.Add(displayModule.ToDto());
            }

            return dtoList;
        }
        #endregion
    }
}

