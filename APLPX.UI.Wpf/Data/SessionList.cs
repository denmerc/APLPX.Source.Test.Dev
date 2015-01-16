using APLPX.Entity;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APLPX.UI.WPF.Data
{
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class SessionList
    {
        public List<Module> Modules { get; set; }
        public List<Analytic> Analytics { get; set; }
        public List<PricingEveryday> Pricing { get; set; }
        public string Owner { get; set; }
    }

}
