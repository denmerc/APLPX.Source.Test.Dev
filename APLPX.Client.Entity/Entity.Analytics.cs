using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Client.Entity
{
    [DataContract]
    [BsonIgnoreExtraElements]
    public class Analytic
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [DataMember]
        public List<Filter> Filters { get; private set; }
        [DataMember]
        public List<Driver> Drivers { get; set; }
        [DataMember]
        public List<PriceList> PriceLists { get; private set; }
        [DataMember]
        public List<Workflow> Workflow { get; private set; }
        [DataMember]
        [BsonElement("Identity")]
        public Identity Self { get;  set; }

        [DataContract]
        [BsonNoId]
        [BsonIgnoreExtraElements]
        public class Identity
        {

            #region Initialize...
            public Identity() { }
            public Identity(
                Int32 Id,
                String Name,
                String Description,
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
                    this.Id = Id;
                    this.Name = Name;
                    this.Description = Description;
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

            //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            
            //public String id2 { get; set; }
            [DataMember]
            [BsonRepresentation(BsonType.String)]
            public Int32 Id { get;  set; }
            [DataMember]
            public String Name { get;  set; }
            [DataMember]
            public String Description { get; set; }
            [DataMember]
            [BsonIgnore]
            public DateTime Refreshed;
            [DataMember]
            public String RefreshedText;
            [DataMember]
            [BsonIgnore]
            public DateTime Created;
            [DataMember]
            public String CreatedText;
            [DataMember]
            [BsonIgnore]
            public DateTime Edited;
            [DataMember]
            public String EditedText;
            [DataMember]
            public String Author;
            [DataMember]
            public String Editor;
            [DataMember]
            public String Owner;
            [DataMember]
            public Boolean Active;
        }

        [DataContract]
        [BsonIgnoreExtraElements]
        public class Driver
        {
            #region Initialize...
            public Driver() { }
            public Driver(
                Int32 Id,
                Int32 Key,
                String Name,
                String Tooltip,
                Boolean Selected,
                List<Mode> Modes
                ) {
                    this.Id=Id;
                    this.Key = Key;
                    this.Name = Name;
                    this.Tooltip = Tooltip;
                    this.Selected = Selected;
                    this.Modes = Modes;
            }
            #endregion

            [DataMember]
            public Int32 Id { get; private set; }
            [DataMember]
            public Int32 Key { get; private set; }
            [DataMember]
            public String Name { get;  set; }
            [DataMember]
            public String Tooltip { get; private set; }
            [DataMember]
            public Boolean Selected { get; set; }
            [DataMember]
            [BsonElement("Mode")]
            public List<Mode> Modes { get; private set; }

            [DataContract]
            [BsonNoId]
            public class Mode {

                #region Initialize...
                public Mode() { }
                public Mode(
                    Int32 Key,
                    String Name,
                    String Tooltip,
                    Boolean Selected,
                    Group Groups
                    ) {
                    this.Key = Key;
                    this.Name = Name;
                    this.Tooltip = Tooltip;
                    this.IsSelected = Selected;
                    this.Groups = Groups;
                }
                #endregion

                [DataMember]
                public Int32 Key { get; private set; }
                [DataMember]
                public String Name { get; private set; }
                [DataMember]
                public String Tooltip { get; private set; }
                [DataMember]
                public Boolean IsSelected { get; private set; }
                [DataMember]
                public Int32 Sort { get; private set; }
                [DataMember]
                [BsonElement("Group")]
                public Group Groups { get; private set; }

                [DataContract]
                [BsonNoId]
                public class Group {

                    #region Initialize...
                    public Group() { }
                    public Group(
                        Int32 Id,
                        Int32 Value,
                        Decimal MinOutlier,
                        Decimal MaxOutlier
                        ) {
                        this.Id = Id;
                        this.Value = Value;
                        this.MinOutlier = MinOutlier;
                        this.MaxOutlier = MaxOutlier;
                    }
                    #endregion

                    [DataMember]
                    //[BsonRepresentation(BsonType.String)]
                    public Int32 Id { get; private set; }
                    [DataMember]
                    public Int32 Value { get; private set; }
                    [DataMember]
                    public Decimal MinOutlier { get; private set; }
                    [DataMember]
                    public Decimal MaxOutlier { get; private set; }
                }
            }

            public Mode this[String index] {
                get {
                    Mode mode = new Mode();
                    foreach (Mode item in this.Modes) {
                        if (item.Name == index) {
                            mode = item;
                            break;
                        }
                    }
                    return mode;
                }
            }
        }
    }
}




