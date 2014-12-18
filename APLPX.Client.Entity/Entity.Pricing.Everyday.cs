using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Client.Entity
{
    [DataContract]
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class PricingEveryday
    {
        #region Initialize...
        public PricingEveryday() { }
        public PricingEveryday(int id) {
            Id = id;
        }
        public PricingEveryday(
            int id,
            PricingIdentity identity
            ) {
            Id = id;
            Identity = identity;
        }
        public PricingEveryday(
            int id,
            List<FilterGroup> filterGroups
            ) {
            Id = id;
            FilterGroups = filterGroups;
        }
        public PricingEveryday(
            int id,
            PricingEverydayKeyValueDriver keyValueDriver,
            List<PricingEverydayLinkedValueDriver> linkedValueDrivers
            ) {
            Id = id;
            KeyValueDriver = keyValueDriver;
            LinkedValueDrivers = linkedValueDrivers;
        }
        public PricingEveryday(
            int id,
            List<PricingMode> pricingModes,
            PricingKeyPriceListRule keyPriceListRule,
            List<PricingLinkedPriceListRule> linkedPriceListRules
            ) {
            Id = id;
            PricingModes = pricingModes;
            KeyPriceListRule = keyPriceListRule;
            LinkedPriceListRules = linkedPriceListRules;
        }
        public PricingEveryday(
            int id,
            string searchGroupKey,
            PricingIdentity identity,
            List<FilterGroup> filterGroups,
            List<PricingEverydayValueDriver> valueDrivers,
            PricingEverydayKeyValueDriver keyValueDriver,
            List<PricingEverydayLinkedValueDriver> linkedValueDrivers,
            List<PricingMode> pricingModes,
            List<PricingEverydayPriceListGroup> priceListGroups,
            PricingKeyPriceListRule keyPriceListRule,
            List<PricingLinkedPriceListRule> linkedPriceListRules,
            List<PricingEverydayResult> results
            ) {
            Id = id;
            SearchGroupKey = searchGroupKey;
            Identity = identity;
            FilterGroups = filterGroups;
            ValueDrivers = valueDrivers;
            KeyValueDriver = keyValueDriver;
            LinkedValueDrivers = linkedValueDrivers;
            PricingModes = pricingModes;
            PriceListGroups = priceListGroups;
            KeyPriceListRule = keyPriceListRule;
            LinkedPriceListRules = linkedPriceListRules;
            Results = results;
        }
        #endregion

        [DataMember]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public int Id { get; private set; }
        [DataMember]
        public string SearchGroupKey { get; private set; }
        [DataMember]
        public PricingIdentity Identity { get; private set; }
        [DataMember]
        public List<FilterGroup> FilterGroups { get; set; }
        [DataMember]
        [BsonElement("ValueDriver")]
        public List<PricingEverydayValueDriver> ValueDrivers { get; private set; }
        [DataMember]
        public PricingEverydayKeyValueDriver KeyValueDriver { get; set; }
        [DataMember]
        [BsonElement("LinkedValueDriver")]
        public List<PricingEverydayLinkedValueDriver> LinkedValueDrivers { get; set; }
        [BsonElement("PricingMode")]
        [DataMember]
        public List<PricingMode> PricingModes { get; private set; }
        [BsonElement("PriceListGroup")]
        [DataMember]
        public List<PricingEverydayPriceListGroup> PriceListGroups { get; private set; }
        [DataMember]
        public PricingKeyPriceListRule KeyPriceListRule { get; set; }
        [DataMember]
        [BsonElement("LinkedPriceListRule")]
        public List<PricingLinkedPriceListRule> LinkedPriceListRules { get; set; }
        [DataMember]
        public List<PricingEverydayResult> Results { get; private set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class PricingEverydayValueDriver : ValueDriver
    {
        #region initialize...
        public PricingEverydayValueDriver() { }
        public PricingEverydayValueDriver(
            int valueDriverId,
            int key,
            bool isSelected,
            string name,
            string title,
            short sort,
            bool isKey,
            List<PricingValueDriverGroup> groups
            ) : base(valueDriverId, key, isSelected, name, title, sort) {
            IsKey = isKey;
            Groups = groups;
        }
        #endregion

        [DataMember]
        public bool IsKey { get; set; }
        [DataMember]
        [BsonElement("Group")]
        public List<PricingValueDriverGroup> Groups { get; private set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class PricingEverydayKeyValueDriver
    {
        #region initialize...
        public PricingEverydayKeyValueDriver() { }
        public PricingEverydayKeyValueDriver(
            int valueDriverId,
            List<PricingEverydayKeyValueDriverGroup> groups
            ) {
            ValueDriverId = valueDriverId;
            Groups = groups;
        }
        #endregion

        [DataMember]
        public int ValueDriverId { get; set; }
        [DataMember]
        [BsonElement("Group")]
        public List<PricingEverydayKeyValueDriverGroup> Groups { get; set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class PricingEverydayLinkedValueDriver
    {
        #region initialize...
        public PricingEverydayLinkedValueDriver() { }
        public PricingEverydayLinkedValueDriver(
            int valueDriverId,
            List<PricingEverydayLinkedValueDriverGroup> groups
            ) {
            ValueDriverId = valueDriverId;
            Groups = groups;
        }
        #endregion

        [DataMember]
        public int ValueDriverId { get; set; }
        [DataMember]
        [BsonElement("Group")]
        public List<PricingEverydayLinkedValueDriverGroup> Groups { get; set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class PricingEverydayKeyValueDriverGroup
    {
        #region initialize...
        public PricingEverydayKeyValueDriverGroup() { }
        public PricingEverydayKeyValueDriverGroup(
            int valueDriverGroupId,
            List<PriceMarkupRule> markupRules,
            List<PriceOptimizationRule> optimizationRules
            ) {
            ValueDriverGroupId = valueDriverGroupId;
            MarkupRules = markupRules;
            OptimizationRules = optimizationRules;
        }
        #endregion

        [DataMember]
        public int ValueDriverGroupId { get; set; }
        [DataMember]
        public List<PriceMarkupRule> MarkupRules { get; set; }
        [DataMember]
        public List<PriceOptimizationRule> OptimizationRules { get; set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class PricingEverydayLinkedValueDriverGroup
    {
        #region initialize...
        public PricingEverydayLinkedValueDriverGroup() { }
        public PricingEverydayLinkedValueDriverGroup(
            int valueDriverGroupId,
            decimal percentChange
            ) {
            ValueDriverGroupId = valueDriverGroupId;
            PercentChange = percentChange;
        }
        #endregion

        [DataMember]
        public int ValueDriverGroupId { get; set; }
        [DataMember]
        public decimal PercentChange { get; set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class PricingEverydayPriceListGroup : PriceListGroup
    {
        #region initialize...
        public PricingEverydayPriceListGroup() { }
        public PricingEverydayPriceListGroup(
            int key,
            string name,
            string title,
            short sort,
            List<PricingEverydayPriceList> priceLists
            ) : base(key, name, title, sort) {
            PriceLists = priceLists;
        }
        #endregion

        [DataMember]
        [BsonElement("PriceList")]
        public List<PricingEverydayPriceList> PriceLists { get; private set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class PricingEverydayPriceList : PriceList
    {
        #region initialize...
        public PricingEverydayPriceList() { }
        public PricingEverydayPriceList(
            int priceListId,
            int key,
            bool isSelected,
            bool isKey
            ) : base(priceListId, key, isSelected) {
            IsKey = isKey;
        }
        public PricingEverydayPriceList(
            int priceListId,
            int key,
            string code,
            string name,
            short sort,
            bool isSelected,
            bool isKey
            ) : base(priceListId, key, code, name, sort, isSelected) {
            IsKey = isKey;
        }
        #endregion

        [DataMember]
        public bool IsKey { get; set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class PricingEverydayResult
    {
        #region initialize...
        public PricingEverydayResult() { }
        public PricingEverydayResult(
            int skuId,
            string skuName,
            string skuTitle,
            PricingResultDriverGroup groups,
            List<PricingEverydayResultPriceList> priceLists
            ) {
            SkuId = skuId;
            SkuName = skuName;
            SkuTitle = skuTitle;
            Groups = groups;
            PriceLists = priceLists;
        }
        #endregion
 
        [DataMember]
        public int SkuId { get; private set; }
        [DataMember]
        public string SkuName { get; private set; }
        [DataMember]
        public string SkuTitle { get; private set; }
        [DataMember]
        [BsonElement("Group")]
        public PricingResultDriverGroup Groups { get; private set; }
        [DataMember]
        [BsonElement("PriceList")]
        public List<PricingEverydayResultPriceList> PriceLists { get; private set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class PricingResults
    {
        [DataMember]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public int PricingId { get; set; }

        [DataMember]
        public List<PricingEverydayResult> Results { get; set; }

    }


    [BsonNoId]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class PricingEverydayResultPriceList : PricingEverydayPriceList
    {
        #region initialize...
        public PricingEverydayResultPriceList() { }
        public PricingEverydayResultPriceList(
            int resultId
            ) {
            ResultId = resultId;    
        }
        public PricingEverydayResultPriceList(
            int resultId,
            decimal newPrice
            ) {
            ResultId = resultId;    
            NewPrice = newPrice;
        }
        public PricingEverydayResultPriceList(
            int resultId,
            int newMarkupPercent
            ) {
            ResultId = resultId;    
            NewMarkupPercent = newMarkupPercent;
        }
        public PricingEverydayResultPriceList(
            int resultId,
            int priceListId,
            int key,
            string code,
            string name,
            short sort,
            bool isSelected,
            bool isKey,
            decimal currentPrice,
            decimal newPrice,
            decimal currentMarkupPercent,
            decimal newMarkupPercent,
            decimal keyValueChange,
            decimal influenceValueChange,
            decimal priceChange,
            PricingResultsEditType priceEdit,
            PricingResultsWarningType priceWarning
            ) : base(priceListId, key, code, name, sort, isSelected, isKey) {
            ResultId = resultId;    
            CurrentPrice = currentPrice;
            NewPrice = newPrice;
            CurrentMarkupPercent = currentMarkupPercent;
            NewMarkupPercent = newMarkupPercent;
            KeyValueChange = keyValueChange;
            InfluenceValueChange = influenceValueChange;
            PriceChange = priceChange;
            PriceEdit = priceEdit;
            PriceWarning = priceWarning;
        }
        #endregion

        [DataMember]
        public int ResultId { get; private set; }
        [DataMember]
        public decimal CurrentPrice { get; private set; }
        [DataMember]
        public decimal NewPrice { get; set; }
        [DataMember]
        public decimal CurrentMarkupPercent { get; private set; }
        [DataMember]
        public decimal NewMarkupPercent { get; set; }
        [DataMember]
        public decimal KeyValueChange { get; private set; }
        [DataMember]
        public decimal InfluenceValueChange { get; private set; }
        [DataMember]
        public decimal PriceChange { get; private set; }
        [DataMember]
        public PricingResultsEditType PriceEdit { get; private set; }
        [DataMember]
        public PricingResultsWarningType PriceWarning { get; private set; }
    }
}
