using APLPX.UI.WPF.DisplayEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO = APLPX.Entity;

namespace APLPX.UI.WPF.Helpers
{
    public static class MockAdministrationGenerator
    {
        public static List<ModuleFeature> GetAdministrationFeatures()
        {
            List<ModuleFeature> features = new List<ModuleFeature>();

            features.Add(new ModuleFeature()
            {
                Name = "Home",
                Title = "Home",
                Sort = 1,
                Steps = new List<ModuleFeatureStep>() {
                        new ModuleFeatureStep(){Name="Default", Sort=1, SelectedAction=null, TypeId = DTO.ModuleFeatureStepType.AdminHome}
                    },
                TypeId = DTO.ModuleFeatureType.AdminHome
            });

            features.Add(new ModuleFeature()
            {
                Name = "Users",
                Title = "Users",
                Sort = 2,
                ActionStepType = DTO.ModuleFeatureStepType.AdminUserIdentity,
                LandingStepType = DTO.ModuleFeatureStepType.AdminUserSearch,
                
                Steps = new List<ModuleFeatureStep>() {
                        new ModuleFeatureStep(){Name = "Search", Sort=1, SelectedAction = null, 
                            TypeId = DTO.ModuleFeatureStepType.AdminUserSearch,
                            Actions = new List<DisplayEntities.Action>() {
                                new DisplayEntities.Action(){ Name="New", ParentName="New", Title="New User", TypeId = DTO.ModuleFeatureStepActionType.AdminUserSearchNew },
                                new DisplayEntities.Action(){ Name="Edit", ParentName="Edit", Title="Edit User", TypeId = DTO.ModuleFeatureStepActionType.AdminUserSearchEdit },
                                new DisplayEntities.Action(){ Name="Remove", ParentName="Remove", Title="Remove User", TypeId = DTO.ModuleFeatureStepActionType.AdminUserSearchRemove }
                            }
                        },
                        new ModuleFeatureStep(){Name = "1) Identity", Sort=2, SelectedAction = null, TypeId = DTO.ModuleFeatureStepType.AdminUserIdentity},
                        new ModuleFeatureStep(){Name = "2) Credentials", Sort=3, SelectedAction = null, TypeId = DTO.ModuleFeatureStepType.AdminUserCredentials},
                        new ModuleFeatureStep(){Name = "3) Role", Sort=4, SelectedAction = null, TypeId = DTO.ModuleFeatureStepType.AdminUserRole}
                    },
                TypeId = DTO.ModuleFeatureType.AdminUsers
            });

            features.Add(new ModuleFeature()
            {
                Name = "Mark-Up Templates",
                Title = "Mark-Up Templates",
                Sort = 3,
                Steps = new List<ModuleFeatureStep>() {
                        new ModuleFeatureStep(){Name = "Identity", Sort=1}
                    },
                TypeId = DTO.ModuleFeatureType.AdminMarkUp
            });

            features.Add(new ModuleFeature()
            {
                Name = "Optimization Templates",
                Title = "Optimization Templates",
                Sort = 4,
                Steps = new List<ModuleFeatureStep>() {
                        new ModuleFeatureStep(){Name = "Identity", Sort=1}
                    },
                TypeId = DTO.ModuleFeatureType.AdminOptimization
            });

            features.Add(new ModuleFeature()
            {
                Name = "Rounding Templates",
                Title = "Rounding Templates",
                Sort = 5,
                Steps = new List<ModuleFeatureStep>() {
                        new ModuleFeatureStep(){Name = "Identity", Sort=1}
                    },
                TypeId = DTO.ModuleFeatureType.AdminTemplates
            });

            return features;
        }
    }
}
