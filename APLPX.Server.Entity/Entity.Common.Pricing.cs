using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
{
    [DataContract]
    public class PricingIdentity
    {
        #region Initialize...
        public PricingIdentity() { }
        public PricingIdentity(
            int analyticsId,
            string name,
            string description,
            string notes,
            bool shared,
            bool active
            ) {
            AnalyticsId = analyticsId;
            Name = name;
            Description = description;
            Notes = notes;
            Shared = shared;
            Active = active;
        }
        public PricingIdentity(
            int analyticsId,
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
            AnalyticsId = analyticsId;
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
        public int AnalyticsId; //CLIENT { get; set; }
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
    public class PricingMode
    {
        #region initialize...
        public PricingMode() { }
        public PricingMode(
            int key,
            bool isSelected
            ) {
            Key = key;
            IsSelected = isSelected;
        }
        public PricingMode(
            int key,
            string name,
            string title,
            bool isSelected,
            bool hasKeyPriceListRule,
            bool hasLinkedPriceListRule,
            int keyPriceListGroupKey,
            int linkedPriceListGroupKey,
            short sort
            ) {
            Key = key;
            Name = name;
            Title = title;
            IsSelected = isSelected;
            HasKeyPriceListRule = hasKeyPriceListRule;
            HasLinkedPriceListRule = hasLinkedPriceListRule;
            KeyPriceListGroupKey = keyPriceListGroupKey;
            LinkedPriceListGroupKey = linkedPriceListGroupKey;
            Sort = sort;
        }
        #endregion

        [DataMember]
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public bool IsSelected; //CLIENT { get; set; }
        [DataMember]
        public bool HasKeyPriceListRule; //CLIENT { get; private set; }
        [DataMember]
        public bool HasLinkedPriceListRule; //CLIENT { get; private set; }
        [DataMember]
        public int KeyPriceListGroupKey; //CLIENT { get; private set; }
        [DataMember]
        public int LinkedPriceListGroupKey; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }

    [DataContract]
    public class PricingKeyPriceListRule
    {
        #region initialize...
        public PricingKeyPriceListRule() { }
        public PricingKeyPriceListRule(
            int priceListId,
            decimal dollarRangeLower,
            decimal dollarRangeUpper
            ) {
            PriceListId = priceListId;
            DollarRangeLower = dollarRangeLower;
            DollarRangeUpper = dollarRangeUpper;
        }
        public PricingKeyPriceListRule(
            int priceListId,
            List<PriceRoundingRule> roundingRules
            ) {
            PriceListId = priceListId;
            RoundingRules = roundingRules;
        }
        public PricingKeyPriceListRule(
            int priceListId,
            decimal dollarRangeLower,
            decimal dollarRangeUpper,
            List<PriceRoundingRule> roundingRules
            ) {
            PriceListId = priceListId;
            DollarRangeLower = dollarRangeLower;
            DollarRangeUpper = dollarRangeUpper;
            RoundingRules = roundingRules;
        }
        #endregion

        [DataMember]
        public int PriceListId; //CLIENT { get; set; }
        [DataMember]
        public decimal DollarRangeLower; //CLIENT { get; set; }
        [DataMember]
        public decimal DollarRangeUpper; //CLIENT { get; set; }
        [DataMember]
        public List<PriceRoundingRule> RoundingRules; //CLIENT { get; set; }
    }

    [DataContract]
    public class PricingLinkedPriceListRule
    {
        #region initialize...
        public PricingLinkedPriceListRule() { }
        public PricingLinkedPriceListRule(
            int priceListId,
            int percentChange
            ) {
            PriceListId = priceListId;
            PercentChange = percentChange;
        }
        public PricingLinkedPriceListRule(
            int priceListId,
            List<PriceRoundingRule> roundingRules
            ) {
            PriceListId = priceListId;
            RoundingRules = roundingRules;
        }
        public PricingLinkedPriceListRule(
            int priceListId,
            int percentChange,
            List<PriceRoundingRule> roundingRules
            ) {
            PriceListId = priceListId;
            PercentChange = percentChange;
            RoundingRules = roundingRules;
        }
        #endregion

        [DataMember]
        public int PriceListId; //CLIENT { get; set; }
        [DataMember]
        public int PercentChange; //CLIENT { get; set; }     
        [DataMember]
        public List<PriceRoundingRule> RoundingRules; //CLIENT { get; set; }
    }

    [DataContract]
    public class PricingMarkupTemplate
    {
        #region initialize...
        public PricingMarkupTemplate() { }
        public PricingMarkupTemplate(
            int id,
            string name,
            string description,
            short sort,
            List<PriceMarkupRule> rules
            ) {
            Id = id;
            Name = name;
            Description = description;
            Sort = sort;
            Rules = rules;
        }
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Description; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public List<PriceMarkupRule> Rules; //CLIENT{ get; private set; }
    }

    [DataContract]
    public class PricingOptimizationTemplate
    {
        #region initialize...
        public PricingOptimizationTemplate() { }
        public PricingOptimizationTemplate(
            int id,
            string name,
            string description,
            short sort,
            List<PriceOptimizationRule> rules
            ) {
            Id = id;
            Name = name;
            Description = description;
            Sort = sort;
            Rules = rules;
        }
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Description; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public List<PriceOptimizationRule> Rules; //CLIENT { get; private set; }
    }

    [DataContract]
    public class PricingRoundingTemplate
    {
        #region initialize...
        public PricingRoundingTemplate() { }
        public PricingRoundingTemplate(
            int id,
            string name,
            string description,
            short sort,
            List<PriceRoundingRule> rules
            ) {
            Id = id;
            Name = name;
            Description = description;
            Sort = sort;
            Rules = rules;
        }
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Description; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public List<PriceRoundingRule> Rules; //CLIENT { get; private set; }
    }

    [DataContract]
    public class PricingValueDriverGroup : ValueDriverGroup
    {
        #region Initialize...
        public PricingValueDriverGroup() { }
        public PricingValueDriverGroup(
            int valueDriverGroupId,
            short value,
            int minOutlier,
            int maxOutlier,
            short sort,
            int skuCount,
            string salesValue
            ) : base(valueDriverGroupId, value, minOutlier, maxOutlier, sort) {
            SkuCount = skuCount;
            SalesValue = salesValue;
        }
        #endregion

        [DataMember]
        public int SkuCount; //CLIENT { get; private set; }
        [DataMember]
        public string SalesValue; //CLIENT { get; private set; }
    }

    [DataContract]
    public class PricingResultFilter
    {
        #region initialize...
        public PricingResultFilter() { }
        public PricingResultFilter(
            int key,
            string name,
            string title,
            short sort
            ) {
            Key = key;
            Name = name;
            Title = title;
            Sort = sort;
        }
        #endregion

        [DataMember]
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }

    [DataContract]
    public class PricingResultEdit
    {
        #region initialize...
        public PricingResultEdit() { }
        public PricingResultEdit(
            string name,
            string title,
            PricingResultsEditType type
            ) {
            Name = name;
            Title = title;
            Type = type;
        }
        #endregion

        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public PricingResultsEditType Type; //CLIENT { get; private set; }
    }

    [DataContract]
    public class PricingResultWarning
    {
        #region initialize...
        public PricingResultWarning() { }
        public PricingResultWarning(
            string name,
            string title,
            PricingResultsWarningType type
            ) {
            Name = name;
            Title = title;
            Type = type;
        }
        #endregion

        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public PricingResultsWarningType Type; //CLIENT { get; private set; }
    }

    [DataContract]
    public class PricingResultDriverGroup : ValueDriverGroup
    {
        #region initialize...
        public PricingResultDriverGroup() { }
        public PricingResultDriverGroup(
            int id,
            short value,
            int minOutlier,
            int maxOutlier,
            short sort,
            string name,
            string title,
            string actual,
            int skuCount,
            string salesValue
            ) : base(id, value, minOutlier, maxOutlier, sort) {
            Name = name;
            Title = title;
            Actual = actual;
            SkuCount = skuCount;
            SalesValue = salesValue;
        }
        #endregion

        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public string Actual; //CLIENT { get; private set; }
        [DataMember]
        public int SkuCount; //CLIENT { get; private set; }
        [DataMember]
        public string SalesValue; //CLIENT { get; private set; }
    }
}
