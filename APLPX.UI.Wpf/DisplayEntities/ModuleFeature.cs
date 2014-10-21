using System;
using System.Collections.Generic;

using APLPX.Client.Entity;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class ModuleFeature : DisplayEntityBase
    {
        #region Private Fields

        private ModuleFeatureType _typeId;
        private string _name;
        private string _title;
        private bool _isVisibile;
        private List<Folder> _folders;
        private List<ModuleFeatureStep> _steps;

        #endregion

        #region Constructors

        public ModuleFeature()
        {
            Folders = new List<Folder>();
            Steps = new List<ModuleFeatureStep>();
        }

        #endregion

        #region Properties

        public ModuleFeatureType TypeId
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

        public List<Folder> Folders
        {
            get { return _folders; }
            set { this.RaiseAndSetIfChanged(ref _folders, value); }
        }

        public List<ModuleFeatureStep> Steps
        {
            get { return _steps; }
            set { this.RaiseAndSetIfChanged(ref _steps, value); }
        }

        #endregion

    }
}
