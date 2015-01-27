using System;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Error> _errors;

        protected DisplayEntityBase()
        {
            Errors = new ObservableCollection<Error>();        
        }

        #region Properties

        public bool IsDirty
        {
            get { return _isDirty; }
            set { this.RaiseAndSetIfChanged(ref _isDirty, value); }
        }

        /// <summary>
        /// Gets the list of validation errors for this object.
        /// </summary>
        public ObservableCollection<Error> Errors
        {
            get { return _errors; }
            protected set { this.RaiseAndSetIfChanged(ref _errors, value); }
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

        /// <summary>
        /// Validates this object against business rules (display-layer only).
        /// </summary>
        /// <returns></returns>
        public virtual bool Validate()
        {
            bool result = (Errors.Count == 0);

            return result;
        }

        #endregion
    }
}
