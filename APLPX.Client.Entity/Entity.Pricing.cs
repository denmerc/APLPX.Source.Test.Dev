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
            Int32 Id
            ) {
            this.Id = Id;
        }
        public Pricing(
            Int32 Id,
            PricingIdentity Identity
            ) {
            this.Id = Id;
            this.Identity = Identity;
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
            this.PriceListGroups = PriceListGroups;
        }
        public Pricing(
            Int32 Id,
            List<FilterGroup> FilterGroups
            ) {
            this.Id = Id;
            this.FilterGroups = FilterGroups;
        }
        public Pricing(
            Int32 Id,
            List<PricingResult> Results
            ) {
            this.Id = Id;
            this.Results = Results;
        }
        public Pricing(
            Int32 Id,
            List<ModuleFeature> Features
            ) {
            this.Id = Id;
            this.Features = Features;
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
            this.Identity = Identity;
            this.Drivers = Drivers;
            this.PriceListGroups = PriceListGroups;
            this.FilterGroups = FilterGroups;
            this.Results = Results;
            this.Features = Features;
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
        [DataMember]
        public List<ModuleFeature> Features { get; private set; }
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
        public String Name { get; set; }
        [DataMember]
        public String Description { get; set; }
        [DataMember]
        public String Notes { get; set; }
        [DataMember]
        public DateTime Refreshed { get; private set; }
        [DataMember]
        public String RefreshedText { get; private set; }
        [DataMember]
        public DateTime Created { get; private set; }
        [DataMember]
        public String CreatedText { get; private set; }
        [DataMember]
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
        public Boolean Active { get; private set; }
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




