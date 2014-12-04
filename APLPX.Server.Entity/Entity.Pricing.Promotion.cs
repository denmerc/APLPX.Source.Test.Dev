using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
{
    [DataContract]
    public class PricingPromotion
    {
        #region Initialize...
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public string SearchGroupKey; //CLIENT { get; private set; }
        [DataMember]
        public PricingIdentity Identity; //CLIENT { get; private set; }
        [DataMember]
        public List<FilterGroup> FilterGroups; //CLIENT { get; private set; }
        //[DataMember]
        // Drivers - Key, Linked
        //[DataMember]
        //Price rules, Key, Linked
        //[DataMember]
        //Results, SKUs, Price Lists, Drivers, Driver group summary

    }
}
