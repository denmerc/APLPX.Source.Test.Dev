//using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Client.Entity
{
    [DataContract]
    //[BsonNoId]
    //[BsonIgnoreExtraElements]
    public class Module //Workflow groups
    {
        #region Initialize...
        public Module() { }
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
        [DataMember]
        public List<UserRole> Roles;
    }
    //[BsonNoId]
    //[BsonIgnoreExtraElements]
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
        public ModuleFeatureType Type { get; set; }
        [DataMember]
        public ModuleFeatureStepType LandingStepType { get; set; }
        [DataMember]
        public ModuleFeatureStepType ActionStepType { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public short Sort { get; set; }
        [DataMember]
        public List<ModuleFeatureStep> Steps { get; set; }
        [DataMember]
        public List<FeatureSearchGroup> SearchGroups { get; set; }
        [DataMember]
        public List<UserRole> Roles;
    }

    [DataContract]
    //[BsonNoId]
    //[BsonIgnoreExtraElements]
    public class ModuleFeatureStep //Workflow View Steps
    {
        #region Initialize...
        public ModuleFeatureStep() { }
        public ModuleFeatureStep(
            string name,
            string title,
            short sort,
            ModuleFeatureStepType type,
            List<ModuleFeatureStepError> errors,
            List<ModuleFeatureStepAdvisor> advisors,
            List<ModuleFeatureStepAction> actions
            ) {
            Name = name;
            Title = title;
            Sort = sort;
            Type = type;
            Errors = errors;
            Advisors = advisors;
            Actions = actions;
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
        public List<ModuleFeatureStepError> Errors { get; set; }
        [DataMember]
        public List<ModuleFeatureStepAdvisor> Advisors { get; set; }
        [DataMember]
        public List<ModuleFeatureStepAction> Actions { get; set; }
    }

    [DataContract]
    //[BsonNoId]
    //[BsonIgnoreExtraElements]
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
        public short Sort { get; set; }
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
        public short Sort { get; private set; }
        [DataMember]
        public string Message { get; private set; }
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
        public short Sort { get; private set; }
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
        public string SearchKey { get;  set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public short ItemCount { get; set; }
        [DataMember]
        public string ParentName { get;  set; }
        [DataMember]
        public bool IsNameChanged { get; set; }
        [DataMember]
        public bool IsSearchKeyChanged { get; set; }
        [DataMember]
        public bool CanNameChange { get;  set; }
        [DataMember]
        public bool CanSearchKeyChange { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
    }
}
