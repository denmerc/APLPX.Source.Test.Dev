using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Entity
{
    [DataContract]
    public class PricingEveryday
    {
        #region Initialize...
        public PricingEveryday() {}
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
        public int Id { get; set; }
        [DataMember]
        public string SearchGroupKey { get;  set; }
        [DataMember]
        public PricingIdentity Identity { get;  set; }
        [DataMember]
        public List<FilterGroup> FilterGroups { get;  set; }
        [DataMember]
        public List<PricingEverydayValueDriver> ValueDrivers { get;  set; }
        [DataMember]
        public PricingEverydayKeyValueDriver KeyValueDriver { get; set; }
        [DataMember]
        public List<PricingEverydayLinkedValueDriver> LinkedValueDrivers { get; set; }
        [DataMember]
        public List<PricingMode> PricingModes { get;  set; }
        [DataMember]
        public List<PricingEverydayPriceListGroup> PriceListGroups { get;  set; }
        [DataMember]
        public PricingKeyPriceListRule KeyPriceListRule { get; set; }
        [DataMember]
        public List<PricingLinkedPriceListRule> LinkedPriceListRules { get; set; }
        [DataMember]
        public List<PricingEverydayResult> Results { get;  set; }
    }

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
        public List<PricingValueDriverGroup> Groups { get;  set; }
    }

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
        public List<PricingEverydayKeyValueDriverGroup> Groups { get; set; }
    }

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
        public List<PricingEverydayLinkedValueDriverGroup> Groups { get; set; }
    }

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

    [DataContract]
    public class PricingEverydayLinkedValueDriverGroup
    {
        #region initialize...
        public PricingEverydayLinkedValueDriverGroup() { }
        public PricingEverydayLinkedValueDriverGroup(
            int valueDriverGroupId,
            int percentChange
            ) {
            ValueDriverGroupId = valueDriverGroupId;
            PercentChange = percentChange;
        }
        #endregion

        [DataMember]
        public int ValueDriverGroupId { get; set; }
        [DataMember]
        public int PercentChange { get; set; }
    }

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
        public List<PricingEverydayPriceList> PriceLists { get;  set; }
    }

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

    [DataContract]
    public class PricingEverydayResult
    {
        #region initialize...
        public PricingEverydayResult() { }
        public PricingEverydayResult(
            int skuId,
            string skuName,
            string skuTitle,
            List<PricingResultDriverGroup> groups,
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
        public int SkuId { get;  set; }
        [DataMember]
        public string SkuName { get;  set; }
        [DataMember]
        public string SkuTitle { get;  set; }
        [DataMember]
        public List<PricingResultDriverGroup> Groups { get;  set; }
        [DataMember]
        public List<PricingEverydayResultPriceList> PriceLists { get;  set; }
    }

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
            int currentMarkupPercent,
            int newMarkupPercent,
            decimal keyValueChange,
            decimal influenceValueChange,
            decimal priceChange,
            PricingResultEdit priceEdit,
            PricingResultWarning priceWarning
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
        public int ResultId { get;  set; }
        [DataMember]
        public decimal CurrentPrice { get;  set; }
        [DataMember]
        public decimal NewPrice { get; set; }
        [DataMember]
        public int CurrentMarkupPercent { get;  set; }
        [DataMember]
        public int NewMarkupPercent { get; set; }
        [DataMember]
        public decimal KeyValueChange { get;  set; }
        [DataMember]
        public decimal InfluenceValueChange { get;  set; }
        [DataMember]
        public decimal PriceChange { get;  set; }
        [DataMember]
        public PricingResultEdit PriceEdit { get;  set; }
        [DataMember]
        public PricingResultWarning PriceWarning { get;  set; }
    }
}
