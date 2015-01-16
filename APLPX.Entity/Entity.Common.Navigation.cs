using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Entity
{
    [DataContract]
    public class Module //Workflow groups
    {
        #region Initialize...
        public Module() { }
        public Module(
            string name,
            string title,
            short sort,
            ModuleType type
        ) {
            Name = name;
            Title = title;
            Sort = sort;
            Type = type;
        }
        public Module(
            string name,
            string title,
            short sort,
            ModuleType type,
            List<ModuleFeature> features
        ) {
            Name = name;
            Title = title;
            Sort = sort;
            Type = type;
            Features = features;
        }
        #endregion

        [DataMember]
        public ModuleType Type { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public List<ModuleFeature> Features { get;  set; }
    }

    [DataContract]
    public class ModuleFeature  //Workflow Views
    {
        #region Initialize...
        public ModuleFeature() { }
        public ModuleFeature(
            string name,
            string title,
            short sort,
            ModuleFeatureType type,
            ModuleFeatureStepType landingStepType,
            ModuleFeatureStepType actionStepType
            ) {
            Name = name;
            Title = title;
            Sort = sort;
            Type = type;
            LandingStepType = landingStepType;
            ActionStepType = actionStepType;
        }
        public ModuleFeature(
            string name,
            string title,
            short sort,
            ModuleFeatureType type,
            ModuleFeatureStepType landingStepType,
            ModuleFeatureStepType actionStepType,
            List<ModuleFeatureStep> steps,
            List<FeatureSearchGroup> searchGroups
            ) {
            Name = name;
            Title = title;
            Sort = sort;
            Type = type;
            LandingStepType = landingStepType;
            ActionStepType = actionStepType;
            Steps = steps;
            SearchGroups = searchGroups;
        }
        #endregion

        [DataMember]
        public ModuleFeatureType Type { get;  set; }
        [DataMember]
        public ModuleFeatureStepType LandingStepType { get;  set; }
        [DataMember]
        public ModuleFeatureStepType ActionStepType { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public List<ModuleFeatureStep> Steps { get;  set; }
        [DataMember]
        public List<FeatureSearchGroup> SearchGroups { get;  set; }
    }

    [DataContract]
    public class ModuleFeatureStep //Workflow View Steps
    {
        #region Initialize...
        public ModuleFeatureStep() { }
        public ModuleFeatureStep(
            string name,
            string title,
            short sort,
            ModuleFeatureStepType type
            ) {
            Name = name;
            Title = title;
            Sort = sort;
            Type = type;
        }
        public ModuleFeatureStep(
            string name,
            string title,
            short sort,
            ModuleFeatureStepType type,
            List<ModuleFeatureStepAction> actions,
            List<ModuleFeatureStepAdvisor> advisors
            ) {
            Name = name;
            Title = title;
            Sort = sort;
            Type = type;
            Actions = actions;
            Advisors = advisors;
        }
        public ModuleFeatureStep(
            string name,
            string title,
            short sort,
            ModuleFeatureStepType type,
            List<ModuleFeatureStepError> errors
            ) {
            Name = name;
            Title = title;
            Sort = sort;
            Type = type;
            Errors = errors;
        }
        public ModuleFeatureStep(
            string name,
            string title,
            short sort,
            ModuleFeatureStepType type,
            List<ModuleFeatureStepAction> actions,
            List<ModuleFeatureStepAdvisor> advisors,
            List<ModuleFeatureStepError> errors
            ) {
            Name = name;
            Title = title;
            Sort = sort;
            Type = type;
            Actions = actions;
            Advisors = advisors;
            Errors = errors;
        }
        #endregion

        [DataMember]
        public ModuleFeatureStepType Type { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public List<ModuleFeatureStepAction> Actions { get;  set; }
        [DataMember]
        public List<ModuleFeatureStepAdvisor> Advisors { get;  set; }
        [DataMember]
        public List<ModuleFeatureStepError> Errors { get;  set; }
    }

    [DataContract]
    public class ModuleFeatureStepAction
    {
        #region Initialize...
        public ModuleFeatureStepAction() { }
        public ModuleFeatureStepAction(
            string name,
            string parentName,
            string title,
            short sort,
            ModuleFeatureStepActionType type
            ) {
            Name = name;
            ParentName = parentName;
            Title = title;
            Sort = sort;
            Type = type;
        }
        #endregion

        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string ParentName { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public ModuleFeatureStepActionType Type { get;  set; }
    }

    [DataContract]
    public class ModuleFeatureStepAdvisor
    {
        #region Initialize...
        public ModuleFeatureStepAdvisor() { }
        public ModuleFeatureStepAdvisor(
            short sort,
            string message
            ) {
            Sort = sort;
            Message = message;
        }
        #endregion

        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public string Message { get;  set; }
    }

    [DataContract]
    public class ModuleFeatureStepError
    {
        #region Initialize...
        public ModuleFeatureStepError() { }
        public ModuleFeatureStepError(
            short sort,
            string message
            ) {
            Sort = sort;
            Message = message;
        }
        #endregion

        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public string Message { get; set; }
    }

    [DataContract]
    public class FeatureSearchGroup
    {
        #region Initialize...
        public FeatureSearchGroup() { }
        public FeatureSearchGroup(
            string name,
            short itemCount,
            int searchId,
            string searchGroup,
            string parentName,
            bool isNameChanged,
            bool isSearchGroupChanged,
            bool canNameChange,
            bool canSearchGroupChange,
            short sort
            ) {
            Name = name;
            ItemCount = itemCount;
            SearchId = searchId;
            SearchGroup = searchGroup;
            ParentName = parentName;
            IsNameChanged = isNameChanged;
            IsSearchGroupChanged = isSearchGroupChanged;
            CanNameChange = canNameChange;
            CanSearchGroupChange = canSearchGroupChange;
            Sort = sort;
        }
        #endregion

        [DataMember]
        public int SearchId { get;  set; }
        [DataMember]
        public string SearchGroup { get;  set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public short ItemCount { get; set; }
        [DataMember]
        public string ParentName { get;  set; }
        [DataMember]
        public bool IsNameChanged { get; set; }
        [DataMember]
        public bool IsSearchGroupChanged { get; set; }
        [DataMember]
        public bool CanNameChange { get;  set; }
        [DataMember]
        public bool CanSearchGroupChange { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
    }
}
