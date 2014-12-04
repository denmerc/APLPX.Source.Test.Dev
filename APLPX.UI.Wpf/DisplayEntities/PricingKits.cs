﻿using System;
using System.Collections.Generic;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingKits : DisplayEntityBase, ISearchableEntity
    {
        #region Private Fields

        private int _id;        
        private PricingIdentity _identity;
        private List<FilterGroup> _filterGroups;
        private string _searchGroupKey;
        private string _parentKey;
        private bool _canNameChange;

        #endregion


        #region Constructors

        public PricingKits()
        {
            Identity = new PricingIdentity();
            FilterGroups = new List<FilterGroup>();
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return _id; }
            set { this.RaiseAndSetIfChanged(ref _id, value); }
        }

        public string SearchKey
        {
            get { return _searchGroupKey; }
            set { this.RaiseAndSetIfChanged(ref _searchGroupKey, value); }
        }

        public PricingIdentity Identity
        {
            get { return _identity; }
            set { this.RaiseAndSetIfChanged(ref _identity, value); }
        }

        public List<FilterGroup> FilterGroups
        {
            get { return _filterGroups; }
            set { this.RaiseAndSetIfChanged(ref _filterGroups, value); }
        }

        #endregion

        #region ISearchableEntity

        public string ParentKey
        {
            get { return _parentKey; }
            set { _parentKey = value; }
        }   

        public string EntityTypeName
        {
            get { return GetType().Name; }
        }

        public bool CanNameChange
        {
            get { return _canNameChange; }
            set { _canNameChange = value; }
        }

        #endregion
    }
}
