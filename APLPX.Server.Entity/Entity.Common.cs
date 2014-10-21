using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
{
    [DataContract]
    public class PriceListGroup
    {
        #region Initialize...
        public PriceListGroup() { }
        public PriceListGroup(
            List<PriceList> PriceLists
            ) {
                this.PriceLists = PriceLists;
        }
        public PriceListGroup(
            String TypeName,
            List<PriceList> PriceLists
            ) {
            this.TypeName = TypeName;
            this.PriceLists = PriceLists;
        }
        #endregion

        [DataMember]
        public String TypeName; //CLIENT { get; private set; }
        [DataMember]
        public List<PriceList> PriceLists; //CLIENT { get; private set; }
    }

    [DataContract]
    public class PriceList
    {
        #region Initialize...
        public PriceList() { }
        public PriceList(
            Int32 Id,
            Int32 Key,
            Boolean IsSelected
            ) {
            this.Id = Id;
            this.Key = Key;
            this.IsSelected = IsSelected;
        }
        public PriceList(
            Int32 Id,
            Int32 Key,
            String Code,
            String Name,
            Boolean IsSelected
            ) {
            this.Id = Id;
            this.Key = Key;
            this.Code = Code;
            this.Name = Name;
            this.IsSelected = IsSelected;
        }
        #endregion

        [DataMember]
        public Int32 Id; //CLIENT { get; private set; }
        [DataMember]
        public Int32 Key; //CLIENT { get; private set; }
        [DataMember]
        public String Code; //CLIENT { get; private set; }
        [DataMember]
        public String Name; //CLIENT { get; private set; }
        [DataMember]
        public Boolean IsSelected; //CLIENT { get; set; }
    }

    [DataContract]
    public class FilterGroup
    {
        #region Initialize...
        public FilterGroup() { }
        public FilterGroup(
            List<Filter> Filters
            ) {
                this.Filters = Filters;
        }
        public FilterGroup(
            String TypeName,
            List<Filter> Filters
            ) {
            this.TypeName = TypeName;
            this.Filters = Filters;
        }
        #endregion

        [DataMember]
        public String TypeName; //CLIENT { get; private set; }
        [DataMember]
        public List<Filter> Filters; //CLIENT { get; private set; }
    }

    [DataContract]
    public class Filter
    {
        #region Initialize...
        public Filter() { }
        public Filter(
            Int32 Id,
            Int32 Key,
            Boolean IsSelected
            ) {
            this.Id = Id;
            this.Key = Key;
            this.IsSelected = IsSelected;
        }
        public Filter(
            Int32 Id,
            Int32 Key,
            String Code,
            String Name,
            Boolean IsSelected
            ) {
            this.Id = Id;
            this.Key = Key;
            this.Code = Code;
            this.Name = Name;
            this.IsSelected = IsSelected;
        }
        #endregion

        [DataMember]
        public Int32 Id; //CLIENT { get; private set; }
        [DataMember]
        public Int32 Key; //CLIENT { get; private set; }
        [DataMember]
        public String Code; //CLIENT { get; private set; }
        [DataMember]
        public String Name; //CLIENT { get; private set; }
        [DataMember]
        public Boolean IsSelected; //CLIENT { get; set; }
    }

    [DataContract]
    public class Module
    {
        #region Initialize...
        public Module() { }
        public Module(
            String Name,
            String Title,
            Boolean IsVisible,
            ModuleType Type,
            List<ModuleFeature> Features
        ) {
            this.Name = Name;
            this.Title = Title;
            this.IsVisible = IsVisible;
            this.Features = Features;
            this.Type = Type;
        }
        #endregion

        [DataMember]
        public ModuleType Type; //CLIENT { get; private set; }
        [DataMember]
        public String Name; //CLIENT { get; private set; }
        [DataMember]
        public String Title; //CLIENT { get; private set; }
        [DataMember]
        public Boolean IsVisible; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeature> Features; //CLIENT { get; private set; }
    }

    [DataContract]
    public class ModuleFeature
    {

        #region Initialize...
        public ModuleFeature() { }
        public ModuleFeature(
            String Name,
            String Title,
            Boolean IsVisible,
            ModuleFeatureType Type,
            List<Folder> Folders,
            List<ModuleFeatureStep> Steps
            ) {
            this.Name = Name;
            this.Title = Title;
            this.IsVisible = IsVisible;
            this.Type = Type;
            this.Folders = Folders;
            this.Steps = Steps;
        }
        #endregion

        [DataMember]
        public List<Folder> Folders; //CLIENT { get; private set; }
        [DataMember]
        public ModuleFeatureType Type; //CLIENT { get; private set; }
        [DataMember]
        public String Name; //CLIENT { get; private set; }
        [DataMember]
        public String Title; //CLIENT { get; private set; }
        [DataMember]
        public Boolean IsVisible;  //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeatureStep> Steps; //CLIENT { get; private set; }
    }

    [DataContract]
    public class ModuleFeatureStep
    {
        #region Initialize...
        public ModuleFeatureStep() { }
        public ModuleFeatureStep(
            Int16 Index,
            String Name,
            String Title,
            Boolean IsVisible,
            ModuleFeatureStepType Type,
            List<ModuleFeatureStepError> Errors,
            List<ModuleFeatureStepAdvisor> Advisors
            ) {
            this.Index = Index;
            this.Name = Name;
            this.Title = Title;
            this.IsVisible = IsVisible;
            this.Errors = Errors;
            this.Advisors = Advisors;
            this.Type = Type;
        }
        #endregion

        [DataMember]
        public ModuleFeatureStepType Type; //CLIENT { get; private set; }
        [DataMember]
        public Int16 Index; //CLIENT { get; private set; }
        [DataMember]
        public String Name; //CLIENT { get; private set; }
        [DataMember]
        public String Title; //CLIENT { get; set; }
        [DataMember]
        public Boolean IsVisible; //CLIENT { get; set; }
        [DataMember]
        public List<ModuleFeatureStepError> Errors; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeatureStepAdvisor> Advisors; //CLIENT { get; private set; }
    }

    [DataContract]
    public class ModuleFeatureStepAdvisor
    {
        #region Initialize...
        public ModuleFeatureStepAdvisor() { }
        public ModuleFeatureStepAdvisor(
            Int32 SortOrder,
            String Message
            ) {
                this.SortOrder = SortOrder;
            this.Message = Message;
        }
        #endregion

        [DataMember]
        public Int32 SortOrder; //CLIENT { get; private set; }
        [DataMember]
        public String Message; //CLIENT { get; private set; }
    }

    [DataContract]
    public class ModuleFeatureStepError
    {
        #region Initialize...
        public ModuleFeatureStepError() { }
        public ModuleFeatureStepError(
            String Message
            ) {
            this.Message = Message;
        }
        #endregion

        [DataMember]
        public String Message; //CLIENT { get; set; }
    }

    [DataContract]
    public class Folder
    {
        #region Initialize...
        public Folder() { }
        public Folder(
            Int32 Id,
            Int32 Template,
            Int16 ItemCount,
            String Name,
            String ParentName,
            Boolean IsNameChanged,
            Boolean CanNameChange,
            Int16 Sort
            ) {
            this.Id = Id;
            this.Template = Template;
            this.ItemCount = ItemCount;
            this.Name = Name;
            this.ParentName = ParentName;
            this.IsNameChanged = IsNameChanged;
            this.CanNameChange = CanNameChange;
            this.Sort = Sort;
        }
        #endregion

        [DataMember]
        public Int32 Id; //CLIENT { get; private set; }
        [DataMember]
        public Int32 Template; //CLIENT { get; private set; }
        [DataMember]
        public Int16 ItemCount; //CLIENT { get; set; }
        [DataMember]
        public String Name; //CLIENT { get; set; }
        [DataMember]
        public String ParentName; //CLIENT { get; private set; }
        [DataMember]
        public Boolean IsNameChanged; //CLIENT { get; set; }
        [DataMember]
        public Boolean CanNameChange; //CLIENT { get; private set; }
        [DataMember]
        public Int16 Sort; //CLIENT { get; private set; }
    }

    [DataContract]
    public class SQLEnumeration
    {
        #region Initialize...
        public SQLEnumeration() { }
        public SQLEnumeration(
            Int16 Sort,
            Int16 Value,
            String Name,
            String Description
            ) {
            this.Sort = Sort;
            this.Value = Value;
            this.Name = Name;
            this.Description = Description;
        }
        #endregion

        [DataMember]
        public Int16 Sort; //CLIENT { get; private set; }
        [DataMember]
        public Int16 Value; //CLIENT { get; private set; }
        [DataMember]
        public String Name; //CLIENT { get; private set; }
        [DataMember]
        public String Description; //CLIENT { get; private set; }
    }
}
