using System;
using System.Collections.Generic;
using APLPX.Client.Entity;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class Module : DisplayEntityBase
    {
        #region Private Fields

        private ModuleType _typeId;
        private string _name;
        private string _title;
        private bool _isVisibile;
        private List<ModuleFeature> _features;

        #endregion

        #region Constructors

        public Module()
        {
            Features = new List<ModuleFeature>();
        }

        #endregion

        #region Properties

        public ModuleType TypeId
        {
            get { return _typeId; }
            set { this.RaiseAndSetIfChanged(ref _typeId, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Title
        {
            get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }

        public bool IsVisible
        {
            get { return _isVisibile; }
            set { this.RaiseAndSetIfChanged(ref _isVisibile, value); }
        }

        public List<ModuleFeature> Features
        {
            get { return _features; }
            set { this.RaiseAndSetIfChanged(ref _features, value); }
        }

        #endregion

    }
}
