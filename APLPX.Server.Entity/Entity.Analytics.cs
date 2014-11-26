using System;
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
            int id
            ) {
            Id=id;
        }
        public Analytic(
            int id,
            string searchGroupKey
            ) {
            Id = id;
            SearchGroupKey = searchGroupKey;
        }
        public Analytic(
            int id,
            string searchGroupKey,
            AnalyticIdentity identity
            ) {
            Id = id;
            SearchGroupKey = searchGroupKey;
            Identity = identity;
        }
        public Analytic(
            int id,
            List<AnalyticDriver> drivers
            ) {
            Id = id;
            Drivers = drivers;
        }
        public Analytic(
            int id,
            List<PriceListGroup> priceListGroups
            ) {
            Id = id;
            PriceListGroups = priceListGroups;
        }
        public Analytic(
            int id,
            List<FilterGroup> filterGroups
            ) {
            Id = id;
            FilterGroups = filterGroups;
        }
        public Analytic(
            int id,
            string searchGroupKey,
            AnalyticIdentity identity,
            List<AnalyticDriver> drivers,
            List<PriceListGroup> priceListGroups,
            List<FilterGroup> filterGroups
            ) {
            Id = id;
            SearchGroupKey = searchGroupKey;
            Identity = identity;
            Drivers = drivers;
            PriceListGroups = priceListGroups;
            FilterGroups = filterGroups;
        }
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public string SearchGroupKey; //CLIENT { get; private set; }
        [DataMember]
        public AnalyticIdentity Identity; //CLIENT { get; private set; }
        [DataMember]
        public List<AnalyticDriver> Drivers; //CLIENT { get; private set; }
        [DataMember]
        public List<PriceListGroup> PriceListGroups; //CLIENT { get; private set; }
        [DataMember]
        public List<FilterGroup> FilterGroups; //CLIENT { get; private set; }
    }

    [DataContract]
    public class AnalyticIdentity
    {
        #region Initialize...
        public AnalyticIdentity() { }
        public AnalyticIdentity(
            string name,
            string description,
            string notes,
            bool shared,
            bool active
            ) {
            Name = name;
            Description = description;
            Notes = notes;
            Shared = shared;
            Active = active;
        }
        public AnalyticIdentity(
            string name,
            string description,
            string notes,
            string refreshedText,
            string createdText,
            string editedText,
            DateTime refreshed,
            DateTime created,
            DateTime edited,
            string author,
            string editor,
            string owner,
            bool shared,
            bool active
            ) {
            Name = name;
            Description = description;
            Notes = notes;
            Refreshed = refreshed;
            RefreshedText = refreshedText;
            Created = created;
            CreatedText = createdText;
            Edited = edited;
            EditedText = editedText;
            Author = author;
            Editor = editor;
            Owner = owner;
            Shared = shared;
            Active = active;
        }
        #endregion

        [DataMember]
        public string Name; //CLIENT { get; set; }
        [DataMember]
        public string Description; //CLIENT { get; set; }
        [DataMember]
        public string Notes; //CLIENT { get; set; }
        [DataMember]
        public DateTime Refreshed; //CLIENT { get; private set; }
        [DataMember]
        public string RefreshedText; //CLIENT { get; private set; }
        [DataMember]
        public DateTime Created; //CLIENT { get; private set; }
        [DataMember]
        public string CreatedText; //CLIENT { get; private set; }
        [DataMember]
        public DateTime Edited; //CLIENT { get; private set; }
        [DataMember]
        public string EditedText; //CLIENT { get; private set; }
        [DataMember]
        public string Author; //CLIENT { get; private set; }
        [DataMember]
        public string Editor; //CLIENT { get; private set; }
        [DataMember]
        public string Owner; //CLIENT { get; private set; }
        [DataMember]
        public bool Shared; //CLIENT { get; set; }
        [DataMember]
        public bool Active; //CLIENT { get; set; }
    }

    [DataContract]
    public class AnalyticDriver
    {
        #region Initialize...
        public AnalyticDriver() { }
        public AnalyticDriver(
            int id,
            int key,
            List<AnalyticDriverMode> modes
            ) {
            Id = id;
            Key = key;
            IsSelected = true;
            Modes = modes;
        }
        public AnalyticDriver(
            int id,
            int key,
            string name,
            string title,
            List<AnalyticResult> results
            ) {
            Id=id;
            Key = key;
            Name = name;
            Title = title;
            IsSelected = true;
            Results = results;
        }
        public AnalyticDriver(
            int id,
            int key,
            string name,
            string title,
            short sort,
            bool isSelected,
            List<AnalyticResult> results,
            List<AnalyticDriverMode> modes
            ) {
            Id=id;
            Key = key;
            Name = name;
            Title = title;
            Sort = sort;
            IsSelected = isSelected;
            Results = results;
            Modes = modes;
        }
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public bool IsSelected; //CLIENT { get; set; }
        [DataMember]
        public List<AnalyticDriverMode> Modes; //CLIENT { get; private set; }
        [DataMember]
        public List<AnalyticResult> Results; //CLIENT { get; private set; }

        #region Driver mode name indexer...
        public AnalyticDriverMode this[string index] {
            get {
                AnalyticDriverMode mode = new AnalyticDriverMode();
                foreach (AnalyticDriverMode item in Modes) {
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
            int key,
            List<AnalyticDriverGroup> groups
            ) {
            Key = key;
            IsSelected = true;
            Groups = groups;
        }
        public AnalyticDriverMode(
            int key,
            string name,
            string title,
            short sort,
            bool isSelected,
            List<AnalyticDriverGroup> groups
            ) {
            Key = key;
            Name = name;
            Title = title;
            Sort=sort;
            IsSelected = isSelected;
            Groups = groups;
        }
        #endregion

        [DataMember]
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public bool IsSelected; //CLIENT { get; set; }
        [DataMember]
        public List<AnalyticDriverGroup> Groups; //CLIENT { get; private set; }
    }

    [DataContract]
    public class AnalyticDriverGroup 
    {
        #region Initialize...
        public AnalyticDriverGroup() { }
        public AnalyticDriverGroup(
            int id
            ) {
            Id = id;
            Value = 0;
            MinOutlier = 0;
            MaxOutlier = 0;
        }
        public AnalyticDriverGroup(
            int id,
            short value,
            decimal minOutlier,
            decimal maxOutlier,
            short sort
            ) {
            Id = id;
            Value = value;
            MinOutlier = minOutlier;
            MaxOutlier = maxOutlier;
            Sort=sort;
        }
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public short Value; //CLIENT { get; set; }
        [DataMember]
        public decimal MinOutlier; //CLIENT { get; set; }
        [DataMember]
        public decimal MaxOutlier; //CLIENT { get; set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }

    [DataContract]
    public class AnalyticResult 
    {
        #region Initialize...
        public AnalyticResult() {}
        public AnalyticResult(
            short group,
            decimal minValue,
            decimal maxValue,
            string salesValue,
            short sort
            ) {
            Group=group;
            MinValue=minValue;
            MaxValue=maxValue;
            SalesValue=salesValue;
            Sort=sort;
        }
        #endregion

        [DataMember]
        public short Group; //CLIENT { get; private set; }
        [DataMember]
        public decimal MinValue; //CLIENT { get; private set; }
        [DataMember]
        public decimal MaxValue; //CLIENT { get; private set; }
        [DataMember]
        public string SalesValue; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }
}




