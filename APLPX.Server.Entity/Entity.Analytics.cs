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
            AnalyticIdentity identity
            ) {
            Id = id;
            Identity = identity;
        }
        public Analytic(
            int id,
            List<AnalyticValueDriver> valueDrivers
            ) {
            Id = id;
            ValueDrivers = valueDrivers;
        }
        public Analytic(
            int id,
            List<AnalyitcPriceListGroup> priceListGroups
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
            List<AnalyticValueDriver> valueDrivers,
            List<AnalyitcPriceListGroup> priceListGroups,
            List<FilterGroup> filterGroups
            ) {
            Id = id;
            SearchGroupKey = searchGroupKey;
            Identity = identity;
            ValueDrivers = valueDrivers;
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
        public List<AnalyticValueDriver> ValueDrivers; //CLIENT { get; private set; }
        [DataMember]
        public List<AnalyitcPriceListGroup> PriceListGroups; //CLIENT { get; private set; }
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
    public class AnalyticValueDriver : ValueDriver
    {
        #region Initialize...
        public AnalyticValueDriver() { }
        public AnalyticValueDriver(
            int id,
            int key,
            bool isSelected
            ) : base(id, key, isSelected) { }
        public AnalyticValueDriver(
            int id,
            int key,
            bool isSelected,
            List<AnalyticValueDriverMode> modes
            ) : base(id, key, isSelected) {
            Modes = modes;
        }
        public AnalyticValueDriver(
            int id,
            int key,
            bool isSelected,
            List<AnalyticResultValueDriverGroup> results
            ) : base(id, key, isSelected) {
            Results = results;
        }
        public AnalyticValueDriver(
            int id,
            int key,
            bool isSelected,
            string name,
            string title,
            short sort,
            List<AnalyticResultValueDriverGroup> results,
            List<AnalyticValueDriverMode> modes
           ) : base(id, key, isSelected, name, title, sort) {
            Results = results;
            Modes = modes;
        }
        #endregion

        [DataMember]
        public List<AnalyticValueDriverMode> Modes; //CLIENT { get; private set; }
        [DataMember]
        public List<AnalyticResultValueDriverGroup> Results; //CLIENT { get; private set; }

        #region Driver mode name indexer...
        public AnalyticValueDriverMode this[string index] {
            get {
                AnalyticValueDriverMode mode = new AnalyticValueDriverMode();
                foreach (AnalyticValueDriverMode item in Modes) {
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
    public class AnalyticValueDriverMode : ValueDriverMode
    {
        #region Initialize...
        public AnalyticValueDriverMode() { }
        public AnalyticValueDriverMode(
            int key,
            bool isSelected,
            List<ValueDriverGroup> groups
            ) : base (key, isSelected) {
            Groups = groups;
        }
        public AnalyticValueDriverMode(
            int key,
            bool isSelected,
            string name,
            string title,
            short sort,
            List<ValueDriverGroup> groups
            ) : base(key, isSelected, name, title, sort) {
            Groups = groups;
        }
        #endregion

        [DataMember]
        public List<ValueDriverGroup> Groups; //CLIENT { get; private set; }
    }

    [DataContract]
    public class AnalyticResultValueDriverGroup : ValueDriverGroup
    {
        #region Initialize...
        public AnalyticResultValueDriverGroup() { }
        public AnalyticResultValueDriverGroup(
            int id,
            short value,
            int minOutlier,
            int maxOutlier,
            short sort,
            int skuCount,
            string salesValue
            ) : base(id, value, minOutlier, maxOutlier, sort) {
            SkuCount = skuCount;
            SalesValue = salesValue;
        }
        #endregion

        [DataMember]
        public int SkuCount; //CLIENT { get; private set; }
        [DataMember]
        public string SalesValue; //CLIENT { get; private set; }
    }

    [DataContract]
    public class AnalyitcPriceListGroup : PriceListGroup
    {
        #region Initialize...
        public AnalyitcPriceListGroup() { }
        public AnalyitcPriceListGroup(
            int key,
            string name,
            string title,
            short sort,
            List<PriceList> priceLists
            ) : base(key, name, title, sort) {
            PriceLists = priceLists;
        }
        #endregion

        [DataMember]
        public List<PriceList> PriceLists; //CLIENT { get; private set; }
    }
}




