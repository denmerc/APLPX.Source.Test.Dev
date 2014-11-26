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
        private short _sort;
        private List<ModuleFeature> _features;
        private ModuleFeature _selectedFeature;

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

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        public List<ModuleFeature> Features
        {
            get { return _features; }
            set { this.RaiseAndSetIfChanged(ref _features, value); }
        }

        public ModuleFeature SelectedFeature
        {
            get { return _selectedFeature; }
            set { this.RaiseAndSetIfChanged(ref _selectedFeature, value); }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            string result = String.Format("{0}:Name={1};Type={2}",
                                          GetType().Name, Name, TypeId);
            return result;
        }

        #endregion
    }
}
