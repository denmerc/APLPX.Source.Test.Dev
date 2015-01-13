using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Client.Entity
{
    [DataContract]
    public class Analytic
    {

        #region Initialize...
        public Analytic() { }
        public Analytic(
            int id
            ) {
            Id = id;
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
            List<AnalyticPriceListGroup> priceListGroups
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
            List<AnalyticPriceListGroup> priceListGroups,
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
        public int Id { get; set; }
        [DataMember]
        public string SearchGroupKey { get;  set; }
        [DataMember]
        public AnalyticIdentity Identity { get;  set; }
        [DataMember]
        public List<AnalyticValueDriver> ValueDrivers { get;  set; }
        [DataMember]
        public List<AnalyticPriceListGroup> PriceListGroups { get; set; }
        [DataMember]
        public List<FilterGroup> FilterGroups { get; set; }
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
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public DateTime Refreshed { get;  set; }
        [DataMember]
        public string RefreshedText { get;  set; }
        [DataMember]
        public DateTime Created { get;  set; }
        [DataMember]
        public string CreatedText { get;  set; }
        [DataMember]
        public DateTime Edited { get;  set; }
        [DataMember]
        public string EditedText { get;  set; }
        [DataMember]
        public string Author { get;  set; }
        [DataMember]
        public string Editor { get;  set; }
        [DataMember]
        public string Owner { get;  set; }
        [DataMember]
        public bool Shared { get; set; }
        [DataMember]
        public bool Active { get; set; }
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
            )
            : base(id, key, isSelected) { }
        public AnalyticValueDriver(
            int id,
            int key,
            bool isSelected,
            List<AnalyticValueDriverMode> modes
            )
            : base(id, key, isSelected) {
            Modes = modes;
        }
        public AnalyticValueDriver(
            int id,
            int key,
            bool isSelected,
            List<AnalyticResultValueDriverGroup> results
            )
            : base(id, key, isSelected) {
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
           )
            : base(id, key, isSelected, name, title, sort) {
            Results = results;
            Modes = modes;
        }
        #endregion

        [DataMember]
        public List<AnalyticValueDriverMode> Modes { get; set; }
        [DataMember]
        public List<AnalyticResultValueDriverGroup> Results { get; set; }

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
            )
            : base(key, isSelected) {
            Groups = groups;
        }
        public AnalyticValueDriverMode(
            int key,
            bool isSelected,
            string name,
            string title,
            short sort,
            List<ValueDriverGroup> groups
            )
            : base(key, isSelected, name, title, sort) {
            Groups = groups;
        }
        #endregion

        [DataMember]
        public List<ValueDriverGroup> Groups { get; set; }
    }

    [DataContract]
    public class AnalyticResultValueDriverGroup : ValueDriverGroup
    {
        #region Initialize...
        public AnalyticResultValueDriverGroup() { }
        public AnalyticResultValueDriverGroup(
            int id,
            short value,
            decimal minOutlier,
            decimal maxOutlier,
            short sort,
            int skuCount,
            string salesValue
            )
            : base(id, value, minOutlier, maxOutlier, sort) {
            SkuCount = skuCount;
            SalesValue = salesValue;
        }
        #endregion

        [DataMember]
        public string MinValue { get; set; }
        [DataMember]
        public string MaxValue { get; set; }
        //[DataMember]
        //public int Group { get; set; }
        [DataMember]
        public int SkuCount { get;  set; }
        [DataMember]
        public string SalesValue { get;  set; }
    }

    [DataContract]
    public class AnalyticPriceListGroup : PriceListGroup
    {
        #region Initialize...
        public AnalyticPriceListGroup() { }
        public AnalyticPriceListGroup(
            int key,
            string name,
            string title,
            short sort,
            List<PriceList> priceLists
            )
            : base(key, name, title, sort) {
            PriceLists = priceLists;
        }
        #endregion

        [DataMember]
        public List<PriceList> PriceLists { get;  set; }
        [DataMember]
        public string TypeName { get;  set; }
    }
}




