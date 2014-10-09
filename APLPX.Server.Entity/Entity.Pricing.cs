using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
{
    [DataContract]
    public class Pricing
    {
        [DataMember]
        public List<Filter> Filters; //CLIENT { get; private set; }
        [DataMember]
        public List<Driver> Drivers; //CLIENT { get; private set; }
        [DataMember]
        public List<PriceList> PriceLists; //CLIENT { get; private set; }
        [DataMember]
        public List<Workflow> Workflow; //CLIENT { get; private set; }
        [DataMember]
        public Identity Self; //CLIENT { get; private set; }

        [DataContract]
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

            [DataMember]
            public Int32 Id; //CLIENT { get; set; }
            [DataMember]
            public String Name; //CLIENT { get; set; }
            [DataMember]
            public String Description; //CLIENT { get; set; }
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
        public class Driver
        {


        }


    }
}




