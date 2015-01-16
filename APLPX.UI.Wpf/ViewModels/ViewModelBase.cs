﻿using System;
using System.Collections.Generic;

using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.DisplayServices;
using ReactiveUI;

using DTO = APLPX.Entity;
using Display = APLPX.UI.WPF.DisplayEntities;
using System.Windows;


namespace APLPX.UI.WPF.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
        #region Private Fields

        private Module _selectedModule;
        private ModuleFeature _selectedFeature;
        private User _currentUser;
        private Display.Analytic _selectedAnalytic;
        private Display.PricingEveryday _selectedPricingEveryday;

        private DTO.Session<DTO.NullT> _session;
        //private UserDisplayServices _userDisplayServices;

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

        ///// <summary>
        ///// Gets/sets the UserDisplayServices provider.
        ///// </summary>
        //public UserDisplayServices UserServices
        //{
        //    get { return _userDisplayServices; }
        //    set { this.RaiseAndSetIfChanged(ref _userDisplayServices, value); }
        //}

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
    }
}
