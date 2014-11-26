using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Client.Entity
{
    [DataContract]
    public class Pricing
    {
        #region Initialize...
        public Pricing() { }
        public Pricing(
            Int32 id
            ) {
            Id = id;
        }
        public Pricing(
            Int32 id,
            PricingIdentity identity
            ) {
            Id = id;
            Identity = identity;
        }
        public Pricing(
            Int32 id,
            List<PricingDriver> drivers
            ) {
            Id = id;
            Drivers = drivers;
        }
        public Pricing(
            Int32 id,
            List<PriceListGroup> priceListGroups
            ) {
            Id = id;
            PriceListGroups = priceListGroups;
        }
        public Pricing(
            Int32 id,
            List<FilterGroup> filterGroups
            ) {
            Id = id;
            FilterGroups = filterGroups;
        }
        public Pricing(
            Int32 id,
            List<PricingResult> results
            ) {
            Id = id;
            Results = results;
        }
        public Pricing(
            Int32 id,
            PricingIdentity identity,
            List<PricingDriver> drivers,
            List<PriceListGroup> priceListGroups,
            List<FilterGroup> filterGroups,
            List<PricingResult> results
            ) {
            Id = id;
            Identity = identity;
            Drivers = drivers;
            PriceListGroups = priceListGroups;
            FilterGroups = filterGroups;
            Results = results;
        }
        #endregion

        [DataMember]
        public Int32 Id { get; private set; }
        [DataMember]
        public PricingIdentity Identity { get; private set; }
        [DataMember]
        public List<PricingDriver> Drivers { get; private set; }
        [DataMember]
        public List<PriceListGroup> PriceListGroups { get; private set; }
        [DataMember]
        public List<FilterGroup> FilterGroups { get; private set; }
        [DataMember]
        public List<PricingResult> Results { get; private set; }
    }

    [DataContract]
    public class PricingIdentity
    {
        #region Initialize...
        public PricingIdentity() { }
        public PricingIdentity(
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
        public PricingIdentity(
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
        public bool Active; //CLIENT { get; private set; }
    }

    [DataContract]
    public class PricingDriver
    {
    }

    [DataContract]
    public class PricingResult
    {
    }
}




