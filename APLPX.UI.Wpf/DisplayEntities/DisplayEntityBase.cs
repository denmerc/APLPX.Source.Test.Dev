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
        private ReactiveList<Error> _validationResults;
        private string _clientMessage;
        private string _serverMessage;
        private bool _sessionOk;


        private bool _isDisposed;

        protected DisplayEntityBase()
        {
            ValidationResults = new ReactiveList<Error>();
        }

        #region Properties

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;
                    OnPropertyChanged("IsDirty");
                }
            }
        }

        public string ClientMessage
        {
            get { return _clientMessage; }
            set
            {
                if (_clientMessage != value)
                {
                    _clientMessage = value;
                    OnPropertyChanged("ClientMessage");
                }
            }
        }

        public string ServerMessage
        {
            get { return _serverMessage; }
            set
            {
                if (_serverMessage != value)
                {
                    _serverMessage = value;
                    OnPropertyChanged("ServerMessage");
                }
            }
        }

        public bool SessionOk
        {
            get { return _sessionOk; }
            set
            {
                if (_sessionOk != value)
                {
                    _sessionOk = value;
                    OnPropertyChanged("SessionOk");
                }
            }
        }



        /// <summary>
        /// Gets the list of validation results/errors for this object.
        /// </summary>
        public ReactiveList<Error> ValidationResults
        {
            get { return _validationResults; }
            set
            {
                if (_validationResults != value)
                {
                    _validationResults = value;
                    OnPropertyChanged("ValidationResults");
                }
            }
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
