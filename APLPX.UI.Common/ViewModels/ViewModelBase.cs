using APLPX.UI.Common.Interfaces;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using DTO = APLPX.Entity;


namespace APLPX.UI.Common.ViewModels
{
    /// <summary>
    /// Base class for view models.
    /// </summary>
    public abstract class ViewModelBase : ReactiveObject, IDisposable, IViewModel
    {
        #region Private Fields

        private DTO.Session<DTO.NullT> _session;

        private bool _isDebugMode;
        private bool _areDiagnosticsVisible;
        private bool _isDisposed;

        #endregion

        #region Constructors

        protected ViewModelBase()
        {
#if DEBUG
            IsDebugMode = true;
#endif
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets/sets the current session.
        /// </summary>
        public DTO.Session<DTO.NullT> Session
        {
            get { return _session; }
            set { this.RaiseAndSetIfChanged(ref _session, value); }
        }

        /// <summary>
        /// Gets a value indicating whether to the application is in debug mode.
        /// Views can use this value to enable the display of debug/diagnostic information.        
        /// </summary>
        public bool IsDebugMode
        {
            get { return _isDebugMode; }
            set { this.RaiseAndSetIfChanged(ref _isDebugMode, value); }
        }

        /// <summary>
        /// Gets/sets a  indicating whether to display diagnostic/debug information for the bound view.
        /// This property should be settable from the view only if IsDebugMode is true.
        /// </summary>
        public bool AreDiagnosticsVisible
        {
            get { return _areDiagnosticsVisible; }
            set { this.RaiseAndSetIfChanged(ref _areDiagnosticsVisible, value); }
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

        ~ViewModelBase()
        {
            Dispose(false);
        }

        #endregion
    }
}
