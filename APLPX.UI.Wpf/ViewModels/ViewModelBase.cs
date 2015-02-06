using System;
using System.Collections.Generic;
using System.Windows;

using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.DisplayServices;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;
using DTO = APLPX.Entity;


namespace APLPX.UI.WPF.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IDisposable
    {
        #region Private Fields

        private Module _selectedModule;
        private ModuleFeature _selectedFeature;
        private User _currentUser;
        private Display.Analytic _selectedAnalytic;
        private Display.PricingEveryday _selectedPricingEveryday;

        private DTO.Session<DTO.NullT> _session;
        private Display.Session<DTO.NullT> _sessionDiagnostics;
        //private UserDisplayServices _userDisplayServices;
        private bool _isDebugMode;
        private bool _areDiagnosticsVisible;
        private bool _isDisposed;

        #endregion

        #region Constructors

        public ViewModelBase()
        {
            SessionDiagnostics = new Display.Session<DTO.NullT>();
#if DEBUG
            IsDebugMode = true;
#endif
        }

        #endregion

        #region Properties

        public ModuleFeature SelectedFeature
        {
            get { return _selectedFeature; }
            set { this.RaiseAndSetIfChanged(ref _selectedFeature, value); }
        }

        /// <summary>
        /// Gets/sets the current user.
        /// </summary>
        public User CurrentUser
        {
            get { return _currentUser; }
            set { this.RaiseAndSetIfChanged(ref _currentUser, value); }
        }

        /// <summary>
        /// Gets/sets the currently selected module.
        /// </summary>
        public Module SelectedModule
        {
            get { return _selectedModule; }
            set
            {
                if (_selectedModule != value)
                {
                    _selectedModule = value;
                    this.RaisePropertyChanged("SelectedModule");
                    //TODO any required logic related to the new SelectedModule.
                }
            }
        }

        public Display.Analytic SelectedAnalytic
        {
            get { return _selectedAnalytic; }
            set { this.RaiseAndSetIfChanged(ref _selectedAnalytic, value); }
        }

        public Display.PricingEveryday SelectedPricingEveryday
        {
            get { return _selectedPricingEveryday; }
            set { this.RaiseAndSetIfChanged(ref _selectedPricingEveryday, value); }
        }

        /// <summary>
        /// Gets/sets the current session.
        /// </summary>
        public DTO.Session<DTO.NullT> Session
        {
            get { return _session; }
            set { this.RaiseAndSetIfChanged(ref _session, value); }
        }

        /// <summary>
        /// Gets/sets a Session containing diagnostic information for display.
        /// </summary>
        public Display.Session<DTO.NullT> SessionDiagnostics
        {
            get { return _sessionDiagnostics; }
            set { this.RaiseAndSetIfChanged(ref _sessionDiagnostics, value); }

        }

        ///// <summary>
        ///// Gets/sets the UserDisplayServices provider.
        ///// </summary>
        //public UserDisplayServices UserServices
        //{
        //    get { return _userDisplayServices; }
        //    set { this.RaiseAndSetIfChanged(ref _userDisplayServices, value); }
        //}

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


        protected void ShowMessageBox(string message, MessageBoxImage image)
        {
            MessageBox.Show(message, "PRICEXPERT", MessageBoxButton.OK, image);
        }

        protected MessageBoxResult ShowPrompt(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "PRICEXPERT", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            return result;
        }


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
