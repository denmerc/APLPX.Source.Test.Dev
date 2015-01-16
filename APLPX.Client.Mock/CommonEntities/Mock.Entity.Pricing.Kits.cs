using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Common.Mock.Entity
{
    [DataContract]
    public class PricingKits
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
        // Drivers
        //[DataMember]
        //Price rules
        //[DataMember]
        //Results
    }
}
