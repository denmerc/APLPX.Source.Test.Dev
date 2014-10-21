using System;
using System.Collections.Generic;

using APLPX.Client.Display;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PriceRoutine : DisplayEntityBase
    {
        #region Private Fields

        private int _id;
        private PriceRoutineType _typeCode;
        private string _name;
        private string _description;
        private bool _shared;
        private List<string> _tags;
        private DateTime _lastUpdated;
        private string _lastUserUpdated;
        private string _owner;
        private Analytic _analytic;
        private List<FilterGroup> _filters;
        private List<PriceScheme> _priceSchemes;
        private List<AnalyticDriver> _valueDrivers;

        #endregion

        #region Constructors

        public PriceRoutine()
        {
            Tags = new List<string>();
            Filters = new List<FilterGroup>();
            PriceSchemes = new List<PriceScheme>();
            ValueDrivers = new List<AnalyticDriver>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public PriceRoutineType TypeCode
        {
            get { return _typeCode; }
            set { this.RaiseAndSetIfChanged(ref _typeCode, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Description
        {
            get { return _description; }
            set { this.RaiseAndSetIfChanged(ref _description, value); }
        }

        public bool Shared
        {
            get { return _shared; }
            set { this.RaiseAndSetIfChanged(ref _shared, value); }
        }

        public List<string> Tags
        {
            get { return _tags; }
            private set { this.RaiseAndSetIfChanged(ref _tags, value); }
        }

        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            set { this.RaiseAndSetIfChanged(ref _lastUpdated, value); }
        }

        public string LastUserUpdated
        {
            get { return _lastUserUpdated; }
            set { this.RaiseAndSetIfChanged(ref _lastUserUpdated, value); }
        }

        public string Owner
        {
            get { return _owner; }
            set { this.RaiseAndSetIfChanged(ref _owner, value); }
        }

        public Analytic Analytic
        {
            get { return _analytic; }
            set { this.RaiseAndSetIfChanged(ref _analytic, value); }
        }

        public List<FilterGroup> Filters
        {
            get { return _filters; }
            private set { this.RaiseAndSetIfChanged(ref _filters, value); }
        }

        public List<PriceScheme> PriceSchemes
        {
            get { return _priceSchemes; }
            private set { this.RaiseAndSetIfChanged(ref _priceSchemes, value); }
        }

        public List<AnalyticDriver> ValueDrivers
        {
            get { return _valueDrivers; }
            private set { this.RaiseAndSetIfChanged(ref _valueDrivers, value); }
        }

        #endregion

    }
}
