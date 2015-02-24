using System;
using System.Collections.ObjectModel;
using System.Linq;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.DisplayServices;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;
using DTO = APLPX.Entity;

namespace APLPX.UI.WPF.ViewModels.Analytic
{
    public class AnalyticPriceListViewModel : ViewModelBase
    {
        private Display.Analytic _entity;
        private AnalyticDisplayServices _analyticDisplayService;

        private IDisposable _selectAllPriceListsSubscription;
        private IDisposable _priceListChangedSubscription;
        private bool _isDisposed;

        #region Constructor and Initialization

        public AnalyticPriceListViewModel(Display.Analytic entity, Display.ModuleFeature feature, AnalyticDisplayServices analyticDisplayService)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (feature == null)
            {
                throw new ArgumentNullException("feature");
            }
            if (analyticDisplayService == null)
            {
                throw new ArgumentNullException("analyticDisplayService");
            }

            Entity = entity;
            SelectedFeature = feature;
            _analyticDisplayService = analyticDisplayService;

            if (Entity.PriceListGroups.Count > 0 && Entity.SelectedPriceListGroup == null)
            {
                Entity.SelectedPriceListGroup = Entity.PriceListGroups[0];
            }

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            IObservable<bool> canExecute = this.WhenAnyValue(vm => vm.Entity.SelectedPriceListGroup, (plGroup) => SelectAllPriceListsCanExecute(plGroup));
            SelectAllPriceListsCommand = ReactiveCommand.Create(canExecute);

            _selectAllPriceListsSubscription = this.WhenAnyObservable(vm => vm.SelectAllPriceListsCommand).Subscribe(item => SelectAllPriceListsExecuted(item));

            _priceListChangedSubscription = this.Entity.PriceListGroups.ItemChanged.Subscribe(plg => OnPriceListChanged(plg));

            canExecute = this.WhenAnyValue(vm => vm.IsAnyPriceListGroupDirty, vm => vm.ValidationResults.Count, (isDirty, errorCount) => SaveCanExecute(isDirty, errorCount));
            SaveCommand = ReactiveCommand.Create(canExecute);
            this.WhenAnyObservable(vm => vm.SaveCommand).Subscribe(val => SaveExecuted(val));

            Commands.Add(new DisplayEntities.Action { Command = SaveCommand, Name = "Save", TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsPriceListsSave });
        }

        #endregion

        #region Properties

        public ReactiveCommand<object> SaveCommand
        {
            get;
            private set;
        }

        public ReactiveCommand<object> SelectAllPriceListsCommand
        {
            get;
            private set;
        }

        public Display.Analytic Entity
        {
            get { return _entity; }
            private set { this.RaiseAndSetIfChanged(ref _entity, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the price list groups should be displayed.
        /// </summary>
        public bool ShowPriceListGroups
        {
            get
            {
                bool result = (Entity.PriceListGroups.Count > 1);

                return result;
            }
        }

        public ObservableCollection<Error> ValidationResults
        {
            get
            {
                var errors = Entity.PriceListGroups.SelectMany(g => g.ValidationResults);

                var result = new ObservableCollection<Error>(errors);
                return result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether any PriceListGroup has unsaved changes.
        /// </summary>
        public bool IsAnyPriceListGroupDirty
        {
            get
            {
                bool result = Entity.PriceListGroups.Any(grp => grp.IsDirty);

                return result;
            }
        }

        #endregion

        #region Command and Event Handlers

        private bool SaveCanExecute(bool isDirty, int errorCount)
        {
            return (isDirty && errorCount == 0);
        }

        private void SaveExecuted(object parameter)
        {
        }


        private bool SelectAllPriceListsCanExecute(Display.AnalyticPriceListGroup priceListGroup)
        {
            return (priceListGroup != null);
        }

        private void SelectAllPriceListsExecuted(object parameter)
        {
            bool isSelected = Convert.ToBoolean(parameter);

            foreach (Display.PriceList priceList in Entity.SelectedPriceListGroup.PriceLists)
            {
                priceList.IsSelected = isSelected;
            }
        }

        private void OnPriceListChanged(IReactivePropertyChangedEventArgs<AnalyticPriceListGroup> args)
        {
            var source = args.Sender as AnalyticPriceListGroup;
            if (source != null && source.IsDirty)
            {
                SelectedFeature.SelectedStep.IsCompleted = false;
                SelectedFeature.DisableRemainingSteps();
            }

            //Update dependent calculated properties.
            this.RaisePropertyChanged("ValidationResults");
            this.RaisePropertyChanged("IsAnyPriceListGroupDirty");
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_selectAllPriceListsSubscription != null)
                    {
                        _selectAllPriceListsSubscription.Dispose();
                    }
                    if (_priceListChangedSubscription != null)
                    {
                        _priceListChangedSubscription.Dispose();
                    }
                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
