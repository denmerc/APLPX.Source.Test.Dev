using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Client.Entity
{
    [DataContract]
    [BsonNoId]
    public class PriceListGroup
    {
        #region Initialize...
        public PriceListGroup() { }
        public PriceListGroup(
            List<PriceList> priceLists
            ) {
            this.PriceLists = priceLists;
        }
        public PriceListGroup(
            short sort,
            string typeName,
            List<PriceList> priceLists
            ) {
            Sort = sort;
            TypeName = typeName;
            PriceLists = priceLists;
        }
        #endregion

        [DataMember]
        [BsonElement("Name")]
        public string TypeName; //CLIENT { get; private set; }
        [DataMember]
        public List<PriceList> PriceLists; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }

    [DataContract]
    [BsonNoId]
    public class PriceList
    {
        #region Initialize...
        public PriceList() { }
        public PriceList(
            int id,
            int key,
            bool isSelected
            ) {
            Id = id;
            Key = key;
            IsSelected = isSelected;
        }
        public PriceList(
            int id,
            int key,
            string code,
            string name,
            bool isSelected,
            short sort
            ) {
            Id = id;
            Key = key;
            Code = code;
            Name = name;
            IsSelected = isSelected;
            Sort = sort;
        }
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Code; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public bool IsSelected; //CLIENT { get; set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }

    [DataContract]
    public class FilterGroup
    {
        #region Initialize...
        public FilterGroup() { }
        public FilterGroup(
            List<Filter> filters
            ) {
            Filters = filters;
        }
        public FilterGroup(
            short sort,
            string typeName,
            List<Filter> filters
            ) {
            Sort = sort;
            TypeName = typeName;
            Filters = filters;
        }
        #endregion

        [DataMember]
        [BsonElement("Name")]
        public string TypeName; //CLIENT { get; private set; }
        [DataMember]
        public List<Filter> Filters; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }

    [DataContract]
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class Filter
    {
        #region Initialize...
        public Filter() { }
        public Filter(
            int id,
            int key,
            bool isSelected
            ) {
            Id = id;
            Key = key;
            IsSelected = isSelected;
        }
        public Filter(
            int id,
            int key,
            string code,
            string name,
            bool isSelected,
            short sort
            ) {
            Id = id;
            Key = key;
            Code = code;
            Name = name;
            IsSelected = isSelected;
            Sort = sort;
        }
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Code; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public bool IsSelected; //CLIENT { get; set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }

    [DataContract]
    [BsonNoId]
    [BsonIgnoreExtraElements]
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
        public ModuleFeatureStepType Type; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeatureStepError> Errors; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeatureStepAdvisor> Advisors; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeatureStepAction> Actions; //CLIENT { get; private set; }
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
            bool canNameChange,
            short sort
            ) {
            Name = name;
            ItemCount = itemCount;
            SearchKey = searchKey;
            ParentName = parentName;
            IsNameChanged = isNameChanged;
            CanNameChange = canNameChange;
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
        public bool CanNameChange; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }

    [DataContract]
    public class SQLEnumeration
    {
        #region Initialize...
        public SQLEnumeration() { }
        public SQLEnumeration(
            short sort,
            short value,
            string name,
            string description
            ) {
            Sort = sort;
            Value = value;
            Name = name;
            Description = description;
        }
        #endregion

        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public short Value; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Description; //CLIENT { get; private set; }
    }
}




