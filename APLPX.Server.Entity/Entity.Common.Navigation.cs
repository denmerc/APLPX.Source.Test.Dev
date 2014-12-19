using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
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
        public ModuleType Type; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeature> Features; //CLIENT { get; private set; }
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
        public ModuleFeatureType Type; //CLIENT { get; private set; }
        [DataMember]
        public ModuleFeatureStepType LandingStepType; //CLIENT { get; private set; }
        [DataMember]
        public ModuleFeatureStepType ActionStepType; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeatureStep> Steps; //CLIENT { get; private set; }
        [DataMember]
        public List<FeatureSearchGroup> SearchGroups; //CLIENT { get; private set; }
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
        public ModuleFeatureStepType Type; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeatureStepAction> Actions; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeatureStepAdvisor> Advisors; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeatureStepError> Errors; //CLIENT { get; private set; }
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
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string ParentName; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public ModuleFeatureStepActionType Type; //CLIENT { get; private set; }
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
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public string Message; //CLIENT { get; private set; }
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
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public string Message; //CLIENT { get; set; }
    }

    [DataContract]
    public class FeatureSearchGroup
    {
        #region Initialize...
        public FeatureSearchGroup() { }
        public FeatureSearchGroup(
            string name,
            short itemCount,
            string searchKey,
            string parentName,
            bool isNameChanged,
            bool isSearchKeyChanged,
            bool canNameChange,
            bool canSearchKeyChange,
            short sort
            ) {
            Name = name;
            ItemCount = itemCount;
            SearchKey = searchKey;
            ParentName = parentName;
            IsNameChanged = isNameChanged;
            IsSearchKeyChanged = isSearchKeyChanged;
            CanNameChange = canNameChange;
            CanSearchKeyChange = canSearchKeyChange;
            Sort = sort;
        }
        #endregion

        [DataMember]
        public string SearchKey; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; set; }
        [DataMember]
        public short ItemCount; //CLIENT { get; set; }
        [DataMember]
        public string ParentName; //CLIENT { get; private set; }
        [DataMember]
        public bool IsNameChanged; //CLIENT { get; set; }
        [DataMember]
        public bool IsSearchKeyChanged; //CLIENT { get; set; }
        [DataMember]
        public bool CanNameChange; //CLIENT { get; private set; }
        [DataMember]
        public bool CanSearchKeyChange; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }
}
