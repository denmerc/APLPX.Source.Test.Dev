using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ENT = APLPX.Entity;

namespace APLPX.Client.Mock.Entity
{
    [DataContract]
    [BsonNoId]    
    [BsonIgnoreExtraElements]
    public class FilterGroup
    {
        #region Initialize...
        public FilterGroup() { }
        public FilterGroup(
            List<Filter> filters
            ) {
            Filters = filters;
        }
        public FilterGroup(
            short sort,
            string name,
            List<Filter> filters
            ) {
            Sort = sort;
            Name = name;
            Filters = filters;
        }
        #endregion
        [BsonId]
        public MongoDB.Bson.ObjectId _id { get; set; }
        [DataMember]
        [BsonElement("TypeName")]
        public string Name { get;  set; }
        [DataMember]
        public List<Filter> Filters { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
    }

    [DataContract]
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class Filter
    {
        #region Initialize...
        public Filter() { }
        public Filter(
            int id,
            int key,
            bool isSelected
            ) {
            Id = id;
            Key = key;
            IsSelected = isSelected;
        }
        public Filter(
            int id,
            int key,
            string code,
            string name,
            bool isSelected,
            short sort
            ) {
            Id = id;
            Key = key;
            Code = code;
            Name = name;
            IsSelected = isSelected;
            Sort = sort;
        }
        #endregion

        [DataMember]
        public int Id { get;  set; }
        [DataMember]
        public int Key { get;  set; }
        [DataMember]
        public string Code { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public bool IsSelected { get; set; }
    }

    [DataContract]
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class ValueDriver
    {
        #region Initialize...
        public ValueDriver() { }
        public ValueDriver(
            int id,
            int key,
            bool isSelected
            ) {
            Id = id;
            Key = key;
            IsSelected = isSelected;
        }
        public ValueDriver(
            int id,
            int key,
            bool isSelected,
            string name,
            string title,
            short sort
            ) {
            Id = id;
            Key = key;
            Name = name;
            Title = title;
            Sort = sort;
            IsSelected = isSelected;
        }
        #endregion

        [DataMember]
        public int Id { get;  set; }
        [DataMember]
        public int Key { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public bool IsSelected { get; set; }
    }

    [BsonNoId]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class ValueDriverMode
    {
        #region Initialize...
        public ValueDriverMode() { }
        public ValueDriverMode(
            int key,
            bool isSelected
            ) {
            Key = key;
            IsSelected = isSelected;
        }
        public ValueDriverMode(
            int key,
            bool isSelected,
            string name,
            string title,
            short sort
        ) {
            Key = key;
            Name = name;
            Title = title;
            Sort = sort;
            IsSelected = isSelected;
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
        [DataMember]
        public bool IsSelected { get; set; }
    }
    [DataContract]
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class ValueDriverGroup
    {
        #region Initialize...
        public ValueDriverGroup() { }
        public ValueDriverGroup(
            short value,
            int minOutlier,
            int maxOutlier
            ) {
            Id = 0;
            Value = value;
            MinOutlier = minOutlier;
            MaxOutlier = maxOutlier;
        }
        public ValueDriverGroup(
            int id,
            short value,
            int minOutlier,
            int maxOutlier
            ) {
            Id = id;
            Value = value;
            MinOutlier = minOutlier;
            MaxOutlier = maxOutlier;
        }
        public ValueDriverGroup(
            int id,
            short value,
            decimal minOutlier,
            decimal maxOutlier,
            short sort
            ) {
            Id = id;
            Value = value;
            MinOutlier = minOutlier;
            MaxOutlier = maxOutlier;
            Sort = sort;
        }
        #endregion
        [DataMember]
        [BsonElement("Group")]
        public int Id { get;  set; }
        [DataMember]
        public short Value { get; set; }
        [DataMember]
        public decimal MinOutlier { get; set; }
        [DataMember]
        public decimal MaxOutlier { get; set; }
        [DataMember]
        public short Sort { get; set; }
    }


    [BsonIgnoreExtraElements]
    [BsonNoId]
    [DataContract]
    public class PriceListGroup
    {
        #region Initialize...
        public PriceListGroup() { }
        public PriceListGroup(
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
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class PriceList
    {
        #region Initialize...
        public PriceList() { }
        public PriceList(
            int id,
            int key,
            bool isSelected
            ) {
            Id = id;
            Key = key;
            IsSelected = isSelected;
        }
        public PriceList(
            int id,
            int key,
            string code,
            string name,
            short sort,
            bool isSelected
            ) {
            Id = id;
            Key = key;
            Code = code;
            Name = name;
            Sort = sort;
            IsSelected = isSelected;
        }
        #endregion

        [DataMember]
        public int Id { get;  set; }
        [DataMember]
        public int Key { get;  set; }
        [DataMember]
        public string Code { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Title { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
        [DataMember]
        public bool IsSelected { get; set; }
    }

    [DataContract]
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class PriceMarkupRule
    {
        #region initialize...
        public PriceMarkupRule() { }
        public PriceMarkupRule(
            decimal dollarRangeLower,
            decimal dollarRangeUpper,
            int percentLimitLower,
            int percentLimitUpper
            ) {
            Id = 0;
            DollarRangeLower = dollarRangeLower;
            DollarRangeUpper = dollarRangeUpper;
            PercentLimitLower = percentLimitLower;
            PercentLimitUpper = percentLimitUpper;
        }
        public PriceMarkupRule(
            int id,
            decimal dollarRangeLower,
            decimal dollarRangeUpper,
            int percentLimitLower,
            int percentLimitUpper
            ) {
            Id = id;
            DollarRangeLower = dollarRangeLower;
            DollarRangeUpper = dollarRangeUpper;
            PercentLimitLower = percentLimitLower;
            PercentLimitUpper = percentLimitUpper;
        }
        #endregion

        [DataMember]
        public int Id { get;  set; }
        [DataMember]
        public decimal DollarRangeLower { get; set; }
        [DataMember]
        public decimal DollarRangeUpper { get; set; }
        [DataMember]
        public int PercentLimitLower { get; set; }
        [DataMember]
        public int PercentLimitUpper { get; set; }
    }

    [DataContract]
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class PriceOptimizationRule
    {
        #region initialize...
        public PriceOptimizationRule() { }
        public PriceOptimizationRule(
            decimal dollarRangeLower,
            decimal dollarRangeUpper,
            int percentChange
            ) {
            Id = 0;
            DollarRangeLower = dollarRangeLower;
            DollarRangeUpper = dollarRangeUpper;
            PercentChange = percentChange;
        }
        public PriceOptimizationRule(
            int id,
            decimal dollarRangeLower,
            decimal dollarRangeUpper,
            int percentChange
            ) {
            Id = id;
            DollarRangeLower = dollarRangeLower;
            DollarRangeUpper = dollarRangeUpper;
            PercentChange = percentChange;
        }
        #endregion

        [DataMember]
        public int Id { get;  set; }
        [DataMember]
        public decimal DollarRangeLower { get; set; }
        [DataMember]
        public decimal DollarRangeUpper { get; set; }
        [DataMember]
        public int PercentChange { get; set; }
    }

    [DataContract]
    [BsonNoId]
    [BsonIgnoreExtraElements]
    public class PriceRoundingRule
    {
        #region initialize...
        public PriceRoundingRule() { }
        public PriceRoundingRule(
            int type,
            decimal dollarRangeLower,
            decimal dollarRangeUpper,
            decimal valueChange
            ) {
            Id = 0;
            Type = type;
            DollarRangeLower = dollarRangeLower;
            DollarRangeUpper = dollarRangeUpper;
            ValueChange = valueChange;
        }
        public PriceRoundingRule(
            int id,
            int type,
            decimal dollarRangeLower,
            decimal dollarRangeUpper,
            decimal valueChange
            ) {
            Id = id;
            Type = type;
            DollarRangeLower = dollarRangeLower;
            DollarRangeUpper = dollarRangeUpper;
            ValueChange = valueChange;
        }
        #endregion

        [DataMember]
        public int Id { get;  set; }
        [DataMember]
        public int Type { get; set; }
        [DataMember]
        public decimal DollarRangeLower { get; set; }
        [DataMember]
        public decimal DollarRangeUpper { get; set; }
        [DataMember]
        public decimal ValueChange { get; set; }        
    }

    [DataContract]
    public class SQLEnumeration
    {
        #region Initialize...
        public SQLEnumeration() { }
        public SQLEnumeration(
            int value,
            string name,
            string description,
            short sort
            ) {
            Value = value;
            Name = name;
            Description = description;
            Sort = sort;
        }
        #endregion

        [DataMember]
        public int Value { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Description { get;  set; }
        [DataMember]
        public short Sort { get;  set; }
    }
}




