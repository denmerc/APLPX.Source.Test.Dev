using System;
using System.Collections.Generic;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Base class for Display Entities.
    /// </summary>
    public abstract class DisplayEntityBase : ReactiveObject
    {
        private bool _isDirty;
        private ReactiveList<Error> _errors;
        private bool _isDisposed;

        protected DisplayEntityBase()
        {
            ValidationResults = new ReactiveList<Error>();
        }

        #region Properties

        public bool IsDirty
        {
            get { return _isDirty; }
            set { this.RaiseAndSetIfChanged(ref _isDirty, value); }
        }

        /// <summary>
        /// Gets the list of validation results/errors for this object.
        /// </summary>
        public ReactiveList<Error> ValidationResults
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
        /// <returns>A list containing a populated <see cref="Error"/> object for each validation error.</returns>
        public virtual List<Error> GetValidationErrors()
        {
            var result = new List<Error>();

            return result;
        }

        public virtual bool Validate()
        {
            ValidationResults.Clear();

            var errors = GetValidationErrors();
            foreach (Error error in errors)
            {
                ValidationResults.Add(error);
            }

            return (errors.Count == 0);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                }
                _isDisposed = true;
            }
        }

        #endregion
    }
}
