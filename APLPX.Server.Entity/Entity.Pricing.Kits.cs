using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
{
    [DataContract]
    public class PricingKits
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
        // Drivers
        //[DataMember]
        //Price rules
        //[DataMember]
        //Results
    }
}
