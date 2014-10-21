using System;
using System.ComponentModel;

using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Base class for Display Entities.
    /// </summary>
    public abstract class DisplayEntityBase : ReactiveObject
    {
        private bool _isDirty;

        protected DisplayEntityBase()
        {
        }
        #region Properties

        public bool IsDirty
        {
            get { return _isDirty; }
            set { this.RaiseAndSetIfChanged(ref _isDirty, value); }
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            IReactiveObject reactive = this as IReactiveObject;
            reactive.RaisePropertyChanged(propertyName);
        }

        #endregion
    }
}
