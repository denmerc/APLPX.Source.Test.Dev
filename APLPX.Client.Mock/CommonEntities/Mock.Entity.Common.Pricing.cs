﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DTO = APLPX.Entity;

namespace APLPX.Common.Mock.Entity
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
        public int AnalyticsId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Notes { get; set; }
        [DataMember]
        public DateTime Refreshed { get;  set; }
        [DataMember]
        public string RefreshedText { get;  set; }
        [DataMember]
        public DateTime Created { get;  set; }
        [DataMember]
        public string CreatedText { get;  set; }
        [DataMember]
        public DateTime Edited { get;  set; }
        [DataMember]
        public string EditedText { get;  set; }
        [DataMember]
        public string Author { get;  set; }
        [DataMember]
        public string Editor { get;  set; }
        [DataMember]
        public string Owner { get;  set; }
        [DataMember]
        public bool Shared { get; set; }
        [DataMember]
        public bool Active { get;  set; }
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
        public int Key { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public bool IsSelected { get; set; }
        [DataMember]
        public bool HasKeyPriceListRule { get;  set; }
        [DataMember]
        public bool HasLinkedPriceListRule { get;  set; }
        [DataMember]
        public int KeyPriceListGroupKey { get;  set; }
        [DataMember]
        public int LinkedPriceListGroupKey { get;  set; }
        [DataMember]
        [BsonElement("sort")]
        public short Sort { get;  set; }
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
            List<PriceRoundingRule> roundingRules,
            List<SQLEnumeration> roundingTypes
            ) {
            PriceListId = priceListId;
            DollarRangeLower = dollarRangeLower;
            DollarRangeUpper = dollarRangeUpper;
            RoundingRules = roundingRules;
            RoundingTypes = roundingTypes;
        }
        #endregion

        [DataMember]
        public int PriceListId { get; set; }
        [DataMember]
        [BsonElement("RangeLower")]
        public decimal DollarRangeLower { get; set; }
        [DataMember]
        [BsonElement("RangeUpper")]
        public decimal DollarRangeUpper { get; set; }
        [DataMember]
        [BsonElement("RoundingRule")]
        public List<PriceRoundingRule> RoundingRules { get; set; }
        [DataMember]
        public List<SQLEnumeration> RoundingTypes { get;  set; }
    }

    [DataContract]
    [BsonIgnoreExtraElements]
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
            List<PriceRoundingRule> roundingRules,
            List<SQLEnumeration> roundingTypes
            ) {
            PriceListId = priceListId;
            PercentChange = percentChange;
            RoundingRules = roundingRules;
            RoundingTypes = roundingTypes;
        }
        #endregion

        [DataMember]
        public int PriceListId { get; set; }
        [DataMember]
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        [BsonIgnore]
        public int PercentChange { get; set; }     
        [DataMember]
        [BsonElement("RoundingRule")]
        public List<PriceRoundingRule> RoundingRules { get; set; }
        [DataMember]
        public List<SQLEnumeration> RoundingTypes { get;  set; }
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
        public int Id { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Description { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public List<PriceMarkupRule> Rules{ get;  set; }
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
        public int Id { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Description { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public List<PriceOptimizationRule> Rules { get;  set; }
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
        public int Id { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Description { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public List<PriceRoundingRule> Rules { get;  set; }
    }

    [DataContract]
    [BsonNoId]
    [BsonIgnoreExtraElements]
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
        public int SkuCount { get;  set; }
        [DataMember]
        public string SalesValue { get;  set; }
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
        public int Key { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
    }

    [DataContract]
    public class PricingResultEdit
    {
        #region initialize...
        public PricingResultEdit() { }
        public PricingResultEdit(
            string name,
            string title,
            DTO.PricingResultsEditType type
            ) {
            Name = name;
            Title = title;
            Type = type;
        }
        #endregion

        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public DTO.PricingResultsEditType Type { get;  set; }
    }

    [DataContract]
    public class PricingResultWarning
    {
        #region initialize...
        public PricingResultWarning() { }
        public PricingResultWarning(
            string name,
            string title,
            DTO.PricingResultsWarningType type
            ) {
            Name = name;
            Title = title;
            Type = type;
        }
        #endregion

        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public DTO.PricingResultsWarningType Type { get;  set; }
    }

    [DataContract]
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class PricingResultDriverGroup : ValueDriverGroup
    {
        #region initialize...
        public PricingResultDriverGroup() { }
        public PricingResultDriverGroup(
            int id,
            short value,
            decimal minOutlier,
            decimal maxOutlier,
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
        public string Name { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public string Actual { get;  set; }
        [DataMember]
        public int SkuCount { get;  set; }
        [DataMember]
        public string SalesValue { get;  set; }
    }
}
