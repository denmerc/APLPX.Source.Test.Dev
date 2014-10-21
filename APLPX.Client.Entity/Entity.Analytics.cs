using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Client.Entity
{
    [DataContract]
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class Analytic
    {
        #region Initialize...
        public Analytic() { }
        public Analytic(
            Int32 Id
            ) {
            this.Id = Id;
        }
        public Analytic(
            Int32 Id,
            AnalyticIdentity Identity
            ) {
            this.Id = Id;
            this.Identity = Identity;
        }
        public Analytic(
            Int32 Id,
            List<AnalyticDriver> Drivers
            ) {
            this.Id = Id;
            this.Drivers = Drivers;
        }
        public Analytic(
            Int32 Id,
            List<PriceListGroup> PriceListGroups
            ) {
            this.Id = Id;
            this.PriceListGroups = PriceListGroups;
        }
        public Analytic(
            Int32 Id,
            List<FilterGroup> FilterGroups
            ) {
            this.Id = Id;
            this.FilterGroups = FilterGroups;
        }
        public Analytic(
            Int32 Id,
            List<AnalyticResult> Results
            ) {
            this.Id = Id;
            this.Results = Results;
        }
        public Analytic(
            Int32 Id,
            AnalyticIdentity Identity,
            List<AnalyticDriver> Drivers,
            List<PriceListGroup> PriceListGroups,
            List<FilterGroup> FilterGroups,
            List<AnalyticResult> Results
            ) {
            this.Id = Id;
            this.Identity = Identity;
            this.Drivers = Drivers;
            this.PriceListGroups = PriceListGroups;
            this.FilterGroups = FilterGroups;
            this.Results = Results;
        }
        #endregion

        [DataMember]
        public Int32 Id { get; private set; }
        [DataMember]
        public AnalyticIdentity Identity { get; private set; }
        [DataMember]
        public List<AnalyticDriver> Drivers { get; private set; }
        [DataMember]
        public List<PriceListGroup> PriceListGroups { get; private set; }
        [DataMember]
        public List<FilterGroup> FilterGroups { get; private set; }
        [DataMember]
        public List<AnalyticResult> Results { get; private set; }
        [DataMember]
        public List<ModuleFeature> Features { get; private set; }
    }

        [DataContract]
    public class AnalyticIdentity
        {
            #region Initialize...
        public AnalyticIdentity() { }
        public AnalyticIdentity(
                String Name,
                String Description,
            String Notes,
            Boolean Active
            ) {
            this.Name = Name;
            this.Description = Description;
            this.Notes = Notes;
            this.Active = Active;
        }
        public AnalyticIdentity(
            String Name,
            String Description,
            String Notes,
                String RefreshedText,
                String CreatedText,
                String EditedText,
                DateTime Refreshed,
                DateTime Created,
                DateTime Edited,
                String Author,
                String Editor,
                String Owner,
                Boolean Active
                ) {
                    this.Name = Name;
                    this.Description = Description;
            this.Notes = Notes;
                    this.Refreshed = Refreshed;
                    this.RefreshedText = RefreshedText;
                    this.Created = Created;
                    this.CreatedText = CreatedText;
                    this.Edited = Edited;
                    this.EditedText = EditedText;
                    this.Author = Author;
                    this.Editor = Editor;
                    this.Owner = Owner;
                    this.Active = Active;
            }
            #endregion

            [DataMember]
        public String Name { get; set; }
            [DataMember]
            public String Description { get; set; }
            [DataMember]
        public String Notes { get; set; }
            [DataMember]
            [BsonIgnore]
        public DateTime Refreshed { get; private set; }
            [DataMember]
        public String RefreshedText { get; private set; }
            [DataMember]
            [BsonIgnore]
        public DateTime Created { get; private set; }
            [DataMember]
        public String CreatedText { get; private set; }
            [DataMember]
            [BsonIgnore]
        public DateTime Edited { get; private set; }
            [DataMember]
        public String EditedText { get; private set; }
            [DataMember]
        public String Author { get; private set; }
            [DataMember]
        public String Editor { get; private set; }
            [DataMember]
        public String Owner { get; private set; }
        [DataMember]
        public Boolean Active { get; set; }
        }

    [DataContract]
    [BsonIgnoreExtraElements]
    public class AnalyticDriver
        {
            #region Initialize...
        public AnalyticDriver() { }
        public AnalyticDriver(
                Int32 Id,
                Int32 Key,
            List<AnalyticDriverMode> Modes
            ) {
            this.Id = Id;
            this.Key = Key;
            this.IsSelected = true;
            this.Modes = Modes;
        }
        public AnalyticDriver(
            Int32 Id,
            Int32 Key,
                String Name,
                String Tooltip,
            Int16 SortOrder,
            Boolean IsSelected,
            List<AnalyticDriverMode> Modes
                ) {
            this.Id = Id;
                    this.Key = Key;
                    this.Name = Name;
            this.Title = Title;
            this.SortOrder = SortOrder;
            this.IsSelected = IsSelected;
                    this.Modes = Modes;
            }
            #endregion

            [DataMember]
            public Int32 Id { get; private set; }
            [DataMember]
            public Int32 Key { get; private set; }
            [DataMember]
            public String Name { get; private set; }
            [DataMember]
        public String Title { get; private set; }
        [DataMember]
        public Int16 SortOrder { get; private set; }
            [DataMember]
        public Boolean IsSelected { get; set; }
            [DataMember]
        public List<AnalyticDriverMode> Modes { get; private set; }

        #region Driver mode name indexer...
        public AnalyticDriverMode this[String index] {
            get {
                AnalyticDriverMode mode = new AnalyticDriverMode();
                foreach (AnalyticDriverMode item in this.Modes) {
                    if (item.Name == index) {
                        mode = item;
                        break;
                    }
                }
                return mode;
            }
        }
        #endregion
    }

    [DataContract]
    [BsonNoId]
    public class AnalyticDriverMode
    {
                #region Initialize...
        public AnalyticDriverMode() { }
        public AnalyticDriverMode(
            Int32 Key,
            List<AnalyticDriverGroup> Groups
            ) {
            this.Key = Key;
            this.IsSelected = true;
            this.Groups = Groups;
        }
        public AnalyticDriverMode(
                    Int32 Key,
                    String Name,
            String Title,
            Int16 SortOrder,
            Boolean IsSelected,
            List<AnalyticDriverGroup> Groups
                    ) {
                    this.Key = Key;
                    this.Name = Name;
            this.Title = Title;
            this.SortOrder = SortOrder;
            this.IsSelected = IsSelected;
                    this.Groups = Groups;
                }
                #endregion

                [DataMember]
                public Int32 Key { get; private set; }
                [DataMember]
                public String Name { get; private set; }
                [DataMember]
        public String Title { get; private set; }
        [DataMember]
        public Int16 SortOrder { get; private set; }
                [DataMember]
        public Boolean IsSelected { get; set; }
                [DataMember]
        public List<AnalyticDriverGroup> Groups { get; private set; }
    }

    [DataContract]    
    [BsonNoId]
    public class AnalyticDriverGroup
    {
                    #region Initialize...
        public AnalyticDriverGroup() { }
        public AnalyticDriverGroup(
            Int32 Id
            ) {
            this.Id = Id;
            this.Value = 0;
            this.MinOutlier = 0;
            this.MaxOutlier = 0;
        }
        public AnalyticDriverGroup(
                        Int32 Id,
            Int16 Value,
                        Decimal MinOutlier,
            Decimal MaxOutlier,
            Int16 SortOrder
                        ) {
                        this.Id = Id;
                        this.Value = Value;
                        this.MinOutlier = MinOutlier;
                        this.MaxOutlier = MaxOutlier;
            this.SortOrder = SortOrder;
                    }
                    #endregion

                    [DataMember]
                    public Int32 Id { get; private set; }
                    [DataMember]
        public Int16 Value { get; set; }
        [DataMember]
        public Decimal MinOutlier { get; set; }
                    [DataMember]
        public Decimal MaxOutlier { get; set; }
                    [DataMember]
        public Int16 SortOrder { get; private set; }
            }

    [DataContract]
    public class AnalyticResult
    {
        #region Initialize...
        public AnalyticResult() { }
        public AnalyticResult(
            Int16 Group,
            Decimal MinValue,
            Decimal MaxValue,
            Decimal SalesValue,
            Int16 SortOrder
            ) {
            this.Group = Group;
            this.MinValue = MinValue;
            this.MaxValue = MaxValue;
            this.SalesValue = SalesValue;
            this.SortOrder = SortOrder;
        }
        #endregion

        [DataMember]
        public Int16 Group { get; private set; }
        [DataMember]
        public Decimal MinValue { get; private set; }
        [DataMember]
        public Decimal MaxValue { get; private set; }
        [DataMember]
        public Decimal SalesValue { get; private set; }
        [DataMember]
        public Int16 SortOrder { get; private set; }
    }
}




