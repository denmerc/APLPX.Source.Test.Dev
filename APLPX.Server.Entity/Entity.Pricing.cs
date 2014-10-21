using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
{
    [DataContract]
    public class Pricing
    {
        #region Initialize...
        public Pricing() {}
        public Pricing(
            Int32 Id
            ) {
            this.Id = Id;
        }
        public Pricing(
            Int32 Id,
            PricingIdentity Identity
            ) {
            this.Id = Id;
            this.Identity=Identity;
        }
        public Pricing(
            Int32 Id,
            List<PricingDriver> Drivers
            ) {
            this.Id = Id;
            this.Drivers = Drivers;
        }
        public Pricing(
            Int32 Id,
            List<PriceListGroup> PriceListGroups
            ) {
            this.Id = Id;
            this.PriceListGroups=PriceListGroups;
        }
        public Pricing(
            Int32 Id,
            List<FilterGroup> FilterGroups
            ) {
            this.Id = Id;
            this.FilterGroups=FilterGroups;
        }
        public Pricing(
            Int32 Id,
            List<PricingResult> Results
            ) {
            this.Id = Id;
            this.Results=Results;
        }
        public Pricing(
            Int32 Id,
            List<ModuleFeature> Features
            ) {
            this.Id = Id;
            this.Features=Features;
        }
        public Pricing(
            Int32 Id,
            PricingIdentity Identity,
            List<PricingDriver> Drivers,
            List<PriceListGroup> PriceListGroups,
            List<FilterGroup> FilterGroups,
            List<PricingResult> Results,
            List<ModuleFeature> Features
            ) {
            this.Id = Id;
            this.Identity=Identity;
            this.Drivers=Drivers;
            this.PriceListGroups=PriceListGroups;
            this.FilterGroups=FilterGroups;
            this.Results=Results;
            this.Features=Features;
        }
        #endregion


        [DataMember]
        public Int32 Id; //CLIENT { get; private set; }
        [DataMember]
        public PricingIdentity Identity; //CLIENT { get; private set; }
        [DataMember]
        public List<PricingDriver> Drivers; //CLIENT { get; private set; }
        [DataMember]
        public List<PriceListGroup> PriceListGroups; //CLIENT { get; private set; }
        [DataMember]
        public List<FilterGroup> FilterGroups; //CLIENT { get; private set; }
        [DataMember]
        public List<PricingResult> Results; //CLIENT { get; private set; }
        [DataMember]
        public List<ModuleFeature> Features; //CLIENT { get; private set; }
    }

    [DataContract]
    public class PricingIdentity
    {

        #region Initialize...
        public PricingIdentity() { }
        public PricingIdentity(
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
        public PricingIdentity(
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
        public Boolean Active; //CLIENT { get; private set; }
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




