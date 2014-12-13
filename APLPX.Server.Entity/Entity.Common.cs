using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
{
    [DataContract]
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
            string typeName,
            List<Filter> filters
            ) {
            Sort = sort;
            TypeName = typeName;
            Filters = filters;
        }
        #endregion

        [DataMember]
        public string TypeName; //CLIENT { get; private set; }
        [DataMember]
        public List<Filter> Filters; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }

    [DataContract]
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
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Code; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public bool IsSelected; //CLIENT { get; set; }
    }

    [DataContract]
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
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public bool IsSelected; //CLIENT { get; set; }
    }

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
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public bool IsSelected; //CLIENT { get; set; }
    }

    [DataContract]
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
            int minOutlier,
            int maxOutlier,
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
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public short Value; //CLIENT { get; set; }
        [DataMember]
        public int MinOutlier; //CLIENT { get; set; }
        [DataMember]
        public int MaxOutlier; //CLIENT { get; set; }
        [DataMember]
        public short Sort; //CLIENT { get; set; }
    }

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
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }

    [DataContract]
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
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public int Key; //CLIENT { get; private set; }
        [DataMember]
        public string Code; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Title; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
        [DataMember]
        public bool IsSelected; //CLIENT { get; set; }
    }

    [DataContract]
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
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public decimal DollarRangeLower; //CLIENT { get; set; }
        [DataMember]
        public decimal DollarRangeUpper; //CLIENT { get; set; }
        [DataMember]
        public int PercentLimitLower; //CLIENT { get; set; }
        [DataMember]
        public int PercentLimitUpper; //CLIENT { get; set; }
    }

    [DataContract]
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
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public decimal DollarRangeLower; //CLIENT { get; set; }
        [DataMember]
        public decimal DollarRangeUpper; //CLIENT { get; set; }
        [DataMember]
        public int PercentChange; //CLIENT { get; set; }
    }

    [DataContract]
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
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public int Type; //CLIENT { get; set; }
        [DataMember]
        public decimal DollarRangeLower; //CLIENT { get; set; }
        [DataMember]
        public decimal DollarRangeUpper; //CLIENT { get; set; }
        [DataMember]
        public decimal ValueChange; //CLIENT { get; set; }        
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
        public int Value; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Description; //CLIENT { get; private set; }
        [DataMember]
        public short Sort; //CLIENT { get; private set; }
    }
}
