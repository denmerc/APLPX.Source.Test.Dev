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

            displayEntity.Name = dto.Name;
            displayEntity.Title = dto.Title;
            displayEntity.Sort = dto.Sort;
            displayEntity.TypeId = dto.Type;

            if (dto.Features != null)
            {
                foreach (DTO.ModuleFeature feature in dto.Features)
                {
                    displayEntity.Features.Add(feature.ToDisplayEntity());
                }
            }

            return displayEntity;
        }

        public static DTO.Module ToDto(this Display.Module displayEntity)
        {
            var featureDtos = new List<DTO.ModuleFeature>();

            foreach (Display.ModuleFeature item in displayEntity.Features)
            {
                featureDtos.Add(item.ToDto());
            }            

            var dto = new DTO.Module(
                                displayEntity.Name,
                                displayEntity.Title,
                                displayEntity.Sort,
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
            result.Sort = dto.Sort;
            result.LandingStepType = dto.LandingStepType;
            result.ActionStepType = dto.ActionStepType;

            if (dto.SearchGroups != null)
            {
                foreach (DTO.FeatureSearchGroup searchGroup in dto.SearchGroups)
                {
                    result.SearchGroups.Add(searchGroup.ToDisplayEntity());
                }
            }

            if (dto.Steps != null)
            {
                foreach (DTO.ModuleFeatureStep step in dto.Steps)
                {
                    result.Steps.Add(step.ToDisplayEntity());
                }
            }

            return result;
        }

        public static DTO.ModuleFeature ToDto(this Display.ModuleFeature displayEntity)
        {
            var featureSteps = new List<DTO.ModuleFeatureStep>();
            foreach (var step in displayEntity.Steps)
            {
                featureSteps.Add(step.ToDto());
            }            

            var searchGroups = new List<DTO.FeatureSearchGroup>();
            foreach (var searchGroup in displayEntity.SearchGroups)
            {
                searchGroups.Add(searchGroup.ToDto());
            }            

            var result = new DTO.ModuleFeature(
                                        displayEntity.Name,
                                        displayEntity.Title,
                                        displayEntity.Sort,
                                        displayEntity.TypeId,
                                        displayEntity.LandingStepType,
                                        displayEntity.ActionStepType,
                                        featureSteps,
                                        searchGroups);

            return result;
        }

        #endregion

        #region Module Feature Step Mapping

        public static Display.ModuleFeatureStep ToDisplayEntity(this DTO.ModuleFeatureStep dto)
        {
            var result = new Display.ModuleFeatureStep();

            result.TypeId = dto.Type;
            result.Sort = dto.Sort;
            result.Name = dto.Name;
            result.Title = dto.Title;
            
            if (dto.Advisors != null)
            {
                foreach (var advisor in dto.Advisors)
                {
                    result.Advisors.Add(advisor.ToDisplayEntity());
                }
            }

            if (dto.Errors != null)
            {
                foreach (var error in dto.Errors)
                {
                    result.Errors.Add(error.ToDisplayEntity());
                }
            }

            if (dto.Actions != null)
            {
                foreach (var action in dto.Actions)
                {
                    result.Actions.Add(action.ToDisplayEntity());
                }
            }

            return result;
        }

        public static DTO.ModuleFeatureStep ToDto(this Display.ModuleFeatureStep displayEntity)
        {
            var advisors = new List<DTO.ModuleFeatureStepAdvisor>();

            if (displayEntity.Advisors != null)
            {
                foreach (Display.Advisor advisor in displayEntity.Advisors)
                {
                    advisors.Add(advisor.ToDto());
                }
            }

            var errors = new List<DTO.ModuleFeatureStepError>();
            if (displayEntity.Errors != null)
            {
                foreach (Display.Error error in displayEntity.Errors)
                {
                    errors.Add(error.ToDto());
                }
            }

            var actions = new List<DTO.ModuleFeatureStepAction>();
            if (displayEntity.Actions != null)
            {
                foreach (Display.Action item in displayEntity.Actions)
                {
                    actions.Add(item.ToDto());
                }
            }

            var dto = new DTO.ModuleFeatureStep(
                                            displayEntity.Name,
                                            displayEntity.Title,
                                            displayEntity.Sort,
                                            displayEntity.TypeId,
                                            errors,
                                            advisors,
                                            actions);

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

            foreach (Display.Module displayModule in modules)
            {
                dtoList.Add(displayModule.ToDto());
            }

            return dtoList;
        }

        #endregion
    }
}

