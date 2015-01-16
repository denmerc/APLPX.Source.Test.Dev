using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Entity
{
    [DataContract]
    public class PricingPromotion
    {
        #region Initialize...
        #endregion

        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        public string SearchGroupKey { get; private set; }
        [DataMember]
        public PricingIdentity Identity { get; private set; }
        [DataMember]
        public List<FilterGroup> FilterGroups { get; private set; }
        //[DataMember]
        // Drivers - Key, Linked
        //[DataMember]
        //Price rules, Key, Linked
        //[DataMember]
        //Results, SKUs, Price Lists, Drivers, Driver group summary

    }
}
