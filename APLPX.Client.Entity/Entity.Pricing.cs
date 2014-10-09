using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Client.Entity
{
    [DataContract]
    public class Pricing
    {
        [DataMember]
        public List<Filter> Filters { get; private set; }
        [DataMember]
        public List<PriceList> PriceLists { get; private set; }
        [DataMember]
        public List<Workflow> Workflow { get; private set; }
        [DataMember]
        public Identity Self { get;  set; }

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
            public Int32 Id { get;  set; }
            [DataMember]
            public String Name { get;  set; }
            [DataMember]
            public String Description { get; set; }
            [DataMember]
            public DateTime Refreshed;
            [DataMember]
            public String RefreshedText;
            [DataMember]
            public DateTime Created;
            [DataMember]
            public String CreatedText;
            [DataMember]
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


    }
}




