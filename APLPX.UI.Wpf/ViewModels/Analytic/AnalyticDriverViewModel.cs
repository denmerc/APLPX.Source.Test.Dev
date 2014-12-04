using System;
using System.Collections.Generic;
using System.Linq;
using DisplayEntities = APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;
using APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.ViewModels.Analytic
{
    /// <summary>
    /// View model for analytic driver-related views.
    /// </summary>
    public class AnalyticDriverViewModel : ViewModelBase
    {
        private DisplayEntities.Analytic _entity;
        private bool _isValueDriverSelected;
 
        #region Constructor and Initialization

        public AnalyticDriverViewModel(DisplayEntities.Analytic entity)
        {
            Entity = entity;
            InitializeCommands();

            var selectedValueDriverChanged = this.WhenAnyValue(vm => vm.Entity.SelectedValueDriver);
            selectedValueDriverChanged.Subscribe(driver => OnSelectedValueDriverChanged(driver));
        }

        private void InitializeCommands()
        {            
            AutoCalculateCommand = ReactiveCommand.Create();
            AutoCalculateCommand.Subscribe(_ => AutoCalculateCommandExecuted(_));
        }

        private object OnSelectedValueDriverChanged(AnalyticValueDriver driver)
        {
            IsValueDriverSelected = (driver != null);
            if (driver != null)
            {
                driver.Mode.RecalculateEditableGroups();
            }
            return null;
        }

        #endregion

        #region Properties

        public DisplayEntities.Analytic Entity
        {
            get { return _entity; }
            private set { this.RaiseAndSetIfChanged(ref _entity, value); }
        }

        public bool IsValueDriverSelected
        {
            get { return _isValueDriverSelected; }
            private set { this.RaiseAndSetIfChanged(ref _isValueDriverSelected, value); }
        }

        #endregion
 
        #region Commands

        /// <summary>
        /// Command for triggering an auto-calculation of the value driver groups.
        /// </summary>
        public ReactiveCommand<object> AutoCalculateCommand { get; private set; }

        private object AutoCalculateCommandExecuted(object parameter)
        {
            if (Entity != null && Entity.SelectedValueDriver != null)
            {
                AnalyticValueDriverMode mode = Entity.SelectedValueDriver.Mode;                
                if (mode != null && mode.Groups.Count > 0)
                {
                    mode.MockAutoCalculateDriverGroups();
                }
            }
            return parameter;
        }

        #endregion

    }
}
