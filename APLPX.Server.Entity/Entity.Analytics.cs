﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
{
    [DataContract]
    public class Analytic
    {
        #region Initialize...
        public Analytic() {}
        public Analytic(
            Int32 Id
            ) {
            this.Id=Id;
        }
        public Analytic(
            Int32 Id,
            AnalyticIdentity Identity
            ) {
            this.Id=Id;
            this.Identity=Identity;
        }
        public Analytic(
            Int32 Id,
            List<AnalyticDriver> Drivers
            ) {
            this.Id=Id;
            this.Drivers=Drivers;
        }
        public Analytic(
            Int32 Id,
            List<PriceListGroup> PriceListGroups
            ) {
            this.Id=Id;
            this.PriceListGroups=PriceListGroups;
        }
        public Analytic(
            Int32 Id,
            List<FilterGroup> FilterGroups
            ) {
            this.Id=Id;
            this.FilterGroups=FilterGroups;
        }
        public Analytic(
            Int32 Id,
            List<AnalyticResult> Results
            ) {
            this.Id=Id;
            this.Results=Results;
        }
        public Analytic(
            Int32 Id,
            AnalyticIdentity Identity,
            List<AnalyticDriver> Drivers,
            List<PriceListGroup> PriceListGroups,
            List<FilterGroup> FilterGroups,
            List<AnalyticResult> Results
            ) {
            this.Id=Id;
            this.Identity=Identity;
            this.Drivers=Drivers;
            this.PriceListGroups=PriceListGroups;
            this.FilterGroups=FilterGroups;
            this.Results=Results;
        }
        #endregion

        [DataMember]
        public Int32 Id; //CLIENT { get; private set; }
        [DataMember]
        public AnalyticIdentity Identity; //CLIENT { get; private set; }
        [DataMember]
        public List<AnalyticDriver> Drivers; //CLIENT { get; private set; }
        [DataMember]
        public List<PriceListGroup> PriceListGroups; //CLIENT { get; private set; }
        [DataMember]
        public List<FilterGroup> FilterGroups; //CLIENT { get; private set; }
        [DataMember]
        public List<AnalyticResult> Results; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeature> Features; //CLIENT { get; private set; }
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
        public String Name; //CLIENT { get; set; }
        [DataMember]
        public String Description; //CLIENT { get; set; }
        [DataMember]
        public String Notes; //CLIENT { get; set; }
        [DataMember]
        public DateTime Refreshed; //CLIENT { get; private set; }
        [DataMember]
        public String RefreshedText; //CLIENT { get; private set; }
        [DataMember]
        public DateTime Created; //CLIENT { get; private set; }
        [DataMember]
        public String CreatedText; //CLIENT { get; private set; }
        [DataMember]
        public DateTime Edited; //CLIENT { get; private set; }
        [DataMember]
        public String EditedText; //CLIENT { get; private set; }
        [DataMember]
        public String Author; //CLIENT { get; private set; }
        [DataMember]
        public String Editor; //CLIENT { get; private set; }
        [DataMember]
        public String Owner; //CLIENT { get; private set; }
        [DataMember]
        public Boolean Active; //CLIENT { get; set; }
    }

    [DataContract]
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
            this.Id=Id;
            this.Key = Key;
            this.Name = Name;
            this.Title = Title;
            this.SortOrder=SortOrder;
            this.IsSelected = IsSelected;
            this.Modes = Modes;
        }
        #endregion

        [DataMember]
        public Int32 Id; //CLIENT { get; private set; }
        [DataMember]
        public Int32 Key; //CLIENT { get; private set; }
        [DataMember]
        public String Name; //CLIENT { get; private set; }
        [DataMember]
        public String Title; //CLIENT { get; private set; }
        [DataMember]
        public Int16 SortOrder; //CLIENT { get; private set; }
        [DataMember]
        public Boolean IsSelected; //CLIENT { get; set; }
        [DataMember]
        public List<AnalyticDriverMode> Modes; //CLIENT { get; private set; }

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
            this.SortOrder=SortOrder;
            this.IsSelected = IsSelected;
            this.Groups = Groups;
        }
        #endregion

        [DataMember]
        public Int32 Key; //CLIENT { get; private set; }
        [DataMember]
        public String Name; //CLIENT { get; private set; }
        [DataMember]
        public String Title; //CLIENT { get; private set; }
        [DataMember]
        public Int16 SortOrder; //CLIENT { get; private set; }
        [DataMember]
        public Boolean IsSelected; //CLIENT { get; set; }
        [DataMember]
        public List<AnalyticDriverGroup> Groups; //CLIENT { get; private set; }
    }

    [DataContract]
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
            this.SortOrder=SortOrder;
        }
        #endregion

        [DataMember]
        public Int32 Id; //CLIENT { get; private set; }
        [DataMember]
        public Int16 Value; //CLIENT { get; set; }
        [DataMember]
        public Decimal MinOutlier; //CLIENT { get; set; }
        [DataMember]
        public Decimal MaxOutlier; //CLIENT { get; set; }
        [DataMember]
        public Int16 SortOrder; //CLIENT { get; private set; }
    }

    [DataContract]
    public class AnalyticResult 
    {
        #region Initialize...
        public AnalyticResult() {}
        public AnalyticResult(
            Int16 Group,
            Decimal MinValue,
            Decimal MaxValue,
            Decimal SalesValue,
            Int16 SortOrder
            ) {
            this.Group=Group;
            this.MinValue=MinValue;
            this.MaxValue=MaxValue;
            this.SalesValue=SalesValue;
            this.SortOrder=SortOrder;
        }
        #endregion

        [DataMember]
        public Int16 Group; //CLIENT { get; private set; }
        [DataMember]
        public Decimal MinValue; //CLIENT { get; private set; }
        [DataMember]
        public Decimal MaxValue; //CLIENT { get; private set; }
        [DataMember]
        public Decimal SalesValue; //CLIENT { get; private set; }
        [DataMember]
        public Int16 SortOrder; //CLIENT { get; private set; }
    }
}




