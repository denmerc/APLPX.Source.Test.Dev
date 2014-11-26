using System;
using APLPX.Client.Entity;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Display entity for an action, such as saving, copying, etc.
    /// </summary>
    public class Action : DisplayEntityBase
    {
        #region Private Fields
 
        private string _name;
        private string _parentName;
        private string _title;
        private short _sort;
        private ModuleFeatureStepActionType _typeId;    

        #endregion

        #region Constructors

        public Action()
        {
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string ParentName
        {
            get { return _parentName; }
            set { this.RaiseAndSetIfChanged(ref _parentName, value); }
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

        public ModuleFeatureStepActionType TypeId
        {
            get { return _typeId; }
            set { this.RaiseAndSetIfChanged(ref _typeId, value); }
        }


        #endregion

        #region Overrides

        public override string ToString()
        {
            string result = String.Format("{0}:Name={1};Type={2};Sort={3}", GetType().Name, Name, TypeId, Sort);
            return result;
        }

        #endregion
    }
}
