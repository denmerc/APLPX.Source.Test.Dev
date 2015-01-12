using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using APLPX.Client.Contracts;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.DisplayServices;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.Helpers;
using APLPX.UI.WPF.Interfaces;
using APLPX.UI.WPF.Mappers;
using APLPX.UI.WPF.ViewModels.Analytic;
using APLPX.UI.WPF.ViewModels.Pricing;
using ReactiveUI;
using DTO = APLPX.Client.Entity;

namespace APLPX.UI.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly IUserService _userService;
        private readonly IAnalyticService _analyticService;
        private readonly IPricingEverydayService _pricingEverydayService;


        private List<Module> _modules;
        private ViewModelBase _selectedFeatureViewModel;
        private SearchViewModel _searchViewModel;
        private bool _isFeatureSelectorAvailable;
        private string _currentStatusText;
        private bool _isActionInProgress;

        private ISearchableEntity _originalEntity;
        private EventAggregator _eventManager;
        private Dictionary<DTO.ModuleFeatureType, ModuleFeature> _featureCache;
        private ReactiveCommand<Object> _logoutCommand;

        #endregion

        #region Constructors and Initialization

        private MainViewModel()
        {
            _featureCache = new Dictionary<DTO.ModuleFeatureType, ModuleFeature>();
            InitializeEventHandlers();
            InitializeCommands();
            CurrentStatusText = "Ready";
        }

        /// <summary>
        /// Creates an instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="session">An autheticated session for the current user.</param>
        /// <param name="analyticService"An IAnalyticService provider.></param>
        /// <param name="userService">An IUserService provider.</param>
        public MainViewModel(DTO.Session<DTO.NullT> session, IAnalyticService analyticService, IUserService userService, IPricingEverydayService pricingService)
            : this()
        {

            //var loadFiltersCommand = ReactiveCommand.CreateAsyncTask<List<DTO.FilterGroup>>(async _ =>
            //await Task.Run(() =>
            //{
            //    return new Data.MockUserService().FilterGroups;
            //})).ExecuteAsync().Subscribe(x =>
            //{
            //    session.FilterGroups = x;
            //});
            if (session == null)
            {
                throw new ArgumentNullException("session", "session cannot be null.");
            }

            if (analyticService == null)
            {
                throw new ArgumentNullException("analyticService", "Value cannot be null.");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("userService", "Value cannot be null.");
            }

            base.Session = session;

            base.UserServices = new UserDisplayServices(userService);
            _analyticService = analyticService;
            _userService = userService;

            CurrentUser = session.User.ToDisplayEntity();

            //TODO: COMMENT THIS OUT WHEN UserService is updated to work with new entity model:
            //Modules = DisplayModuleGenerator.CreateSampleModules();

            //TODO: UNCOMMENT THIS WHEN UserService is updated to work with new entity model:
            Modules = session.Modules.ToDisplayEntities();
            //Modules = userService.Authenticate(Session).Modules.ToDisplayEntities();
            SelectedModule = Modules.Where(x => x.TypeId == DTO.ModuleType.Planning).FirstOrDefault();

            //Pre-select the Home feature.
            if (SelectedModule.Features.Count > 0)
            {
                SelectedFeature = SelectedModule.Features[0];
                SelectedFeatureViewModel = new HomeViewModel();
            }
        }

        private void InitializeEventHandlers()
        {
            _eventManager = ((EventAggregator)App.Current.Resources["EventManager"]);
            _eventManager.GetEvent<SearchGroupsUpdatedEvent>().Subscribe(data => OnSearchGroupReassigned(data));

            var selectedModuleChanged = this.WhenAnyValue(vm => vm.SelectedModule);
            selectedModuleChanged.Subscribe(module => OnSelectedModuleChanged(module));

            //Bubble up contained object selections within the current module so we can handle them in this view model.
            var moduleSelectedFeatureChanged = this.WhenAnyValue(vm => vm.SelectedModule.SelectedFeature);
            moduleSelectedFeatureChanged.Subscribe(feature => OnSelectedFeatureChanged(feature));

            var selectedFeatureStepChanged = this.WhenAnyValue(vm => vm.SelectedModule.SelectedFeature.SelectedStep);
            selectedFeatureStepChanged.Subscribe(step => OnSelectedStepChanged(step));

            var selectedEntityChanged = this.WhenAnyValue(vm => vm.SelectedFeature.SelectedEntity);
            selectedEntityChanged.Subscribe(entity => OnSelectedEntityChanged(entity));
        }

        private void InitializeCommands()
        {

            ActionCommand = ReactiveCommand.Create();
            ActionCommand.Subscribe(x => ActionCommandExecuted(x));

            LogoutCommand = ReactiveCommand.Create();
            LogoutCommand.Subscribe(x =>
            {
                var loginWindow = new LoginWindow();
                loginWindow.DataContext = new LoginViewModel(_userService, _analyticService, _pricingEverydayService);
                loginWindow.ShowMaxRestoreButton = false;
                loginWindow.ShowMinButton = false;
                loginWindow.Show();
                App.Current.Windows[0].Close();
            });
            LoadAnalyticCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                await Task.Run(() =>
                {
                    var id = new DTO.Analytic(SelectedEntity.Id);
                    var a = _analyticService.LoadAnalytic(new DTO.Session<DTO.Analytic>() { Data = id });
                    a.Data.FilterGroups = Session.FilterGroups;
                    SelectedAnalytic = a.Data.ToDisplayEntity();
                }));

            LoadPricingEverydayCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                await Task.Run(() =>
                {
                    var id = new DTO.PricingEveryday(SelectedEntity.Id);
                    var a = _pricingEverydayService.LoadPricingEveryday(new DTO.Session<DTO.PricingEveryday>() { Data = id });
                    a.Data.FilterGroups = Session.FilterGroups;
                    SelectedPricingEveryday = a.Data.ToDisplayEntity();
                }));


            SaveAnalyticIdentityCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                await Task.Run(() =>
                {
                    var payload = SelectedAnalytic.ToPayload();
                    payload.Identity = SelectedAnalytic.Identity;
                    var session = new DTO.Session<DTO.Analytic>() { Data = payload.ToDto() };
                    var status = _analyticService.SaveIdentity(session);
                }));

            SaveFiltersCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                await Task.Run(() =>
                {
                    var payload = SelectedAnalytic.ToPayload();
                    payload.FilterGroups = SelectedAnalytic.FilterGroups;
                    var session = new DTO.Session<DTO.Analytic>() { Data = payload.ToDto() };
                    var status = _analyticService.SaveFilters(session);

                }));

            SavePriceListsCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                await Task.Run(() =>
                {
                    var payload = SelectedAnalytic.ToPayload();
                    payload.PriceListGroups = SelectedAnalytic.PriceListGroups;
                    var session = new DTO.Session<DTO.Analytic>() { Data = payload.ToDto() };
                    var status = _analyticService.SavePriceLists(session);

                }));

            SaveValueDriversCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                await Task.Run(() =>
                {
                    var payload = SelectedAnalytic.ToPayload();
                    payload.ValueDrivers = SelectedAnalytic.ValueDrivers;
                    var session = new DTO.Session<DTO.Analytic>() { Data = payload.ToDto() };
                    var status = _analyticService.SaveDrivers(session);

                }));
        }

        #endregion

        #region Properties

        public ReactiveCommand<object> LogoutCommand
        {
            get { return _logoutCommand; }
            private set { this.RaiseAndSetIfChanged(ref _logoutCommand, value); }
        }

        /// <summary>
        /// Gets the list of all modules for the current user.
        /// </summary>
        public List<Module> Modules
        {
            get { return _modules; }
            private set { this.RaiseAndSetIfChanged(ref _modules, value); }
        }

        /// <summary>
        /// Gets the list of all modules available for selection by the current user. 
        /// Note: Excludes items not intended to be explicitly selected, such as Startup.
        /// </summary>
        public List<Module> SelectableModules
        {
            get
            {
                List<Module> result = Modules.Where(item => item.TypeId != DTO.ModuleType.Startup && item.TypeId != DTO.ModuleType.Null).ToList();
                return result;
            }
        }

        /// <summary>
        /// Gets/sets the currently selected module feature.
        /// </summary>
        public new ModuleFeature SelectedFeature
        {
            get
            {
                ModuleFeature result = null;

                if (SelectedModule != null)
                {
                    result = SelectedModule.SelectedFeature;
                }
                return result;
            }
            set
            {
                if (SelectedModule != null)
                {
                    SelectedModule.SelectedFeature = value;
                    this.RaisePropertyChanged("SelectedFeature");

                    Navigate();
                }
            }
        }

        /// <summary>
        /// Gets the currently selected search entity.
        /// </summary>
        public ISearchableEntity SelectedEntity
        {
            get
            {
                ISearchableEntity result = null;
                if (SelectedFeature != null)
                {
                    result = SelectedFeature.SelectedEntity;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets/sets the currently selected module feature.
        /// </summary>
        public ModuleFeatureStep SelectedStep
        {
            get
            {
                ModuleFeatureStep result = null;

                if (SelectedFeature != null)
                {
                    result = SelectedFeature.SelectedStep;
                }

                return result;
            }
            set
            {
                if (SelectedFeature != null && SelectedFeature.SelectedStep != value)
                {
                    SelectedFeature.SelectedStep = value;
                    this.RaisePropertyChanged("SelectedStep");

                    //TODO any required logic related to the new SelectedFeature.
                    Navigate();
                }
            }
        }

        /// <summary>
        /// Gets/sets the currently selected sub view model.
        /// </summary>
        public ViewModelBase SelectedFeatureViewModel
        {
            get
            {
                return _selectedFeatureViewModel;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedFeatureViewModel, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether feature selection should be available.
        /// </summary>
        public bool IsFeatureSelectorAvailable
        {
            get { return _isFeatureSelectorAvailable; }
            private set { this.RaiseAndSetIfChanged(ref _isFeatureSelectorAvailable, value); }
        }

        /// <summary>
        /// Gets a value indicating whether a module is currently selected. This is a convenience property for data binding.
        /// </summary>
        public bool IsModuleSelected
        {
            get
            {
                return (SelectedModule != null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether step selection should be available.
        /// </summary>
        public bool IsStepSelectorAvailable
        {
            get
            {
                //TODO: finalize logic. This is for demo and prototyping only.
                bool isAvailable = false;

                if (SelectedFeature != null)
                {
                    isAvailable = (SelectedFeature.TypeId == DTO.ModuleFeatureType.PlanningAnalytics ||
                                   SelectedFeature.TypeId == DTO.ModuleFeatureType.PlanningEverydayPricing ||
                                   SelectedFeature.TypeId == DTO.ModuleFeatureType.PlanningPromotionPricing ||
                                   SelectedFeature.TypeId == DTO.ModuleFeatureType.PlanningKitPricing);
                }

                return isAvailable;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a specifc entity (e.g., Analytic, User, Price Routine, etc.) is currently selected.
        /// </summary>
        public bool IsEntitySelected
        {
            get
            {
                bool result = false;

                if (SelectedFeature != null)
                {
                    result = (SelectedFeature.SelectedEntity != null);
                }

                return result;
            }
        }

        /// <summary>
        /// Gets/sets the current status text.
        /// </summary>
        public string CurrentStatusText
        {
            get { return _currentStatusText; }
            private set { this.RaiseAndSetIfChanged(ref _currentStatusText, value); }
        }

        /// <summary>
        /// Gets/sets a value indicating whether an action, e.g. async save/retrieval is in progress.
        /// </summary>
        public bool IsActionInProgress
        {
            get { return _isActionInProgress; }
            private set { this.RaiseAndSetIfChanged(ref _isActionInProgress, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command for loading an analytic.
        /// </summary>
        protected ReactiveCommand<Unit> LoadAnalyticCommand { get; private set; }

        /// <summary>
        /// Command for loading an analytic.
        /// </summary>
        protected ReactiveCommand<Unit> LoadPricingEverydayCommand { get; private set; }

        /// <summary>
        /// Command for saving an analytic identity.
        /// </summary>
        protected ReactiveCommand<Unit> SaveAnalyticIdentityCommand { get; private set; }

        /// <summary>
        /// Command to save filters.
        /// </summary>
        protected ReactiveCommand<Unit> SaveFiltersCommand { get; private set; }

        /// <summary>
        /// Command to save price lists
        /// </summary>
        protected ReactiveCommand<Unit> SavePriceListsCommand { get; private set; }

        /// <summary>
        /// Command to save Value Drivers
        /// </summary>
        protected ReactiveCommand<Unit> SaveValueDriversCommand { get; private set; }

        /// <summary>
        /// Command that is invoked when any action is selected in the bound view.
        /// </summary>
        public ReactiveCommand<object> ActionCommand { get; private set; }

        private object ActionCommandExecuted(object sender)
        {
            var action = sender as DisplayEntities.Action;
            if (action != null && SelectedEntity != null)
            {
                //HandleSelectedAction(action);
            }

            return null;
        }

        /// <summary>
        /// Handles the action requested by the ActionCommand.
        /// </summary>
        /// <param name="action"></param>

        private void HandleSelectedAction(DisplayEntities.Action action)
        {
            switch (action.TypeId)
            {
                //Create a new current entity.
                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsNew:
                    //Create a new (blank) entity. TODO: refactor.
                    _originalEntity = SelectedAnalytic;
                    var newAnalytic = new DisplayEntities.Analytic();
                    newAnalytic.Identity.Name = "Analytic name (new)";
                    newAnalytic.Identity.Description = "Description (new)";
                    newAnalytic.IsDirty = true;

                    SelectedAnalytic = newAnalytic;
                    SelectedFeature.SelectedStep = SelectedFeature.DefaultActionStep;
                    SelectedFeature.DisableRemainingSteps();
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningEverydayPricingSearchEverydayNew:
                    _originalEntity = SelectedPricingEveryday;
                    //Create a new (blank) entity. TODO: refactor.
                    var newPriceRoutine = new DisplayEntities.PricingEveryday();
                    newPriceRoutine.Identity.Name = "Pricing Everyday name (new)";
                    newPriceRoutine.Identity.Description = "Description (new)";
                    newPriceRoutine.IsDirty = true;

                    SelectedPricingEveryday = newPriceRoutine;
                    SelectedFeature.SelectedStep = SelectedFeature.DefaultActionStep;
                    SelectedFeature.DisableRemainingSteps();
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningPromotionPricingSearchPromotionsNew:
                case DTO.ModuleFeatureStepActionType.PlanningKitPricingSearchKitsNew:
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsEdit:
                    SelectedFeature.SelectedStep = SelectedFeature.DefaultActionStep;
                    ExecuteAsyncCommand(LoadAnalyticCommand, x => SelectedFeatureViewModel = GetViewModel(SelectedStep), "Retrieving analytic...", "Analytic was successfully retrieved.");
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningEverydayPricingSearchEverydayEdit:
                    //TODO: get the full entity from the server and load edit screen.
                    //This is a simulation only.
                    SelectedPricingEveryday = SelectedEntity as DisplayEntities.PricingEveryday;
                    SelectedFeature.SelectedStep = SelectedFeature.DefaultActionStep;
                    ExecuteAsyncCommand(LoadPricingEverydayCommand, x => SelectedFeatureViewModel = GetViewModel(SelectedStep), "Retrieving pricing...", "PriceRoutine was successfully retrieved.");
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningPromotionPricingSearchPromotionsEdit:
                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningKitPricingSearchKitsEdit:
                    break;

                //Copy current entity.
                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsCopy:
                    //Create a copy of the existing entity and load edit screen.                   
                    SelectedAnalytic = SelectedAnalytic.Copy();
                    SelectedFeature.SelectedStep = SelectedFeature.DefaultActionStep;
                    SelectedFeature.DisableRemainingSteps();
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningEverydayPricingSearchEverydayCopy:
                    //Create a copy of the existing entity and load edit screen.                   
                    SelectedPricingEveryday = SelectedPricingEveryday.Copy();
                    SelectedFeature.SelectedStep = SelectedFeature.DefaultActionStep;
                    SelectedFeature.DisableRemainingSteps();
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningPromotionPricingSearchPromotionsCopy:
                case DTO.ModuleFeatureStepActionType.PlanningKitPricingSearchKitsCopy:
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningAnalyticsIdentityCancel:
                    SelectedFeature.SelectedStep = SelectedFeature.DefaultLandingStep;
                    SelectedFeature.DisableRemainingSteps();
                    break;

                //Save the current entity.
                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningAnalyticsIdentitySave:
                    //TODO: call analytic save method on service.
                    ExecuteAsyncCommand(SaveAnalyticIdentityCommand, x => SelectedFeatureViewModel = GetViewModel(SelectedStep), "Identity saving...", "Identity saved.");
                    SelectedAnalytic.IsDirty = false;
                    SelectedFeature.EnableRemainingSteps();
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningEverydayPricingIdentitySave:
                    //TODO: call price routine save method on service.
                    SelectedPricingEveryday.IsDirty = false;
                    SelectedFeature.EnableRemainingSteps();
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningPromotionPricingIdentitySave:
                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningKitPricingIdentitySave:
                    break;
                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningAnalyticsFiltersSave:
                    ExecuteAsyncCommand(SaveFiltersCommand, x => SelectedFeatureViewModel = GetViewModel(SelectedStep), "Filters saving...", "Filters saved.");
                    break;

                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningAnalyticsPriceListsSave:
                    ExecuteAsyncCommand(SavePriceListsCommand, x => SelectedFeatureViewModel = GetViewModel(SelectedStep), "Price Lists saving...", "Price Lists saved.");
                    break;
                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversSave:
                    ExecuteAsyncCommand(SaveValueDriversCommand, x => SelectedFeatureViewModel = GetViewModel(SelectedStep), "Value Drivers saving...", "Value Drivers saved.");
                    break;
                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsResultsRun:
                    if (SelectedAnalytic != null && SelectedAnalytic.SelectedValueDriver != null)
                    {
                        //TODO: for testing only. In production, will call server method.
                        AnalyticValueDriverMode mode = SelectedAnalytic.SelectedValueDriver.SelectedMode;
                        if (mode != null && mode.Groups.Count > 0)
                        {
                            mode.MockAutoCalculateDriverGroups();
                            mode.AreResultsAvailable = true;
                        }
                    }
                    break;
                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningEverydayPricingPriceListsSave:
                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningPromotionPricingPriceListsSave:
                case APLPX.Client.Entity.ModuleFeatureStepActionType.PlanningKitPricingPriceListsSave:
                    break;
            }
        }

        #endregion

        #region Methods


        public void Navigate()
        {
            if (SelectedFeature != null)
            {
                switch (SelectedFeature.TypeId)
                {
                    case APLPX.Client.Entity.ModuleFeatureType.PlanningHome:
                        SelectedFeature.SelectedStep = SelectedFeature.DefaultLandingStep; SelectedFeatureViewModel = new HomeViewModel();
                        break;
                    case APLPX.Client.Entity.ModuleFeatureType.PlanningAnalytics:

                        if (!_featureCache.ContainsKey(SelectedFeature.TypeId))
                        {

                            
                            ////TODO: UNCOMMENT THIS WHEN UserService is updated to work with new entity model:
                            var displayAnalytics = base.Session.Analytics.ToDisplayEntities();
                            var iSearchables = displayAnalytics.Cast<ISearchableEntity>().ToList();
                            SelectedFeature.SearchableEntities = iSearchables;

                            //var analyticDtos = _analyticService.LoadList(new DTO.Session<DTO.NullT>()).Data;
                            //var displayAnalytics = analyticDtos.ToDisplayEntities();
                            
                            _featureCache.Add(SelectedFeature.TypeId, SelectedFeature);
                            

                        }
                        else if (SelectedFeatureViewModel != null)
                        {
                            SelectedFeatureViewModel.SelectedFeature = _featureCache[SelectedFeature.TypeId];
                        }
                        SelectedFeature.SelectedStep = SelectedFeature.DefaultLandingStep;

                        break;

                    case APLPX.Client.Entity.ModuleFeatureType.PlanningPromotionPricing:
                    case APLPX.Client.Entity.ModuleFeatureType.PlanningEverydayPricing:
                    case APLPX.Client.Entity.ModuleFeatureType.PlanningKitPricing:
                        if (!_featureCache.ContainsKey(SelectedFeature.TypeId))
                        {
                            var pricing = base.Session.Pricing.ToDisplayEntities();
                            SelectedFeature.SearchableEntities = pricing.Cast<ISearchableEntity>().ToList();
                            _featureCache.Add(SelectedFeature.TypeId, SelectedFeature);
                        }
                        else if (SelectedFeatureViewModel != null)
                        {
                            SelectedFeatureViewModel.SelectedFeature = _featureCache[SelectedFeature.TypeId];
                        }
                        SelectedFeature.SelectedStep = SelectedFeature.DefaultLandingStep;
                        break;


                    case APLPX.Client.Entity.ModuleFeatureType.Null:
                    case APLPX.Client.Entity.ModuleFeatureType.StartupLogin:

                    case APLPX.Client.Entity.ModuleFeatureType.TrackingHome:
                    case APLPX.Client.Entity.ModuleFeatureType.ReportingHome:
                    case APLPX.Client.Entity.ModuleFeatureType.AdministrationHome:
                    case APLPX.Client.Entity.ModuleFeatureType.AdministrationUserMaintenance:
                    default:
                        SelectedFeatureViewModel = null;
                        break;
                }
            }
            else
            {
                SelectedFeatureViewModel = null;
            }
        }

        /// <summary>
        /// Gets the view model corresponding to a <see cref="ModuleFeatureStep"/>.
        /// </summary>
        /// <param name="step"></param>       
        /// <returns>The view model, as a <see cref="ViewModelBase"/> object.</returns>
        private ViewModelBase GetViewModel(ModuleFeatureStep step)
        {
            ViewModelBase result = null;

            switch (step.TypeId)
            {
                case DTO.ModuleFeatureStepType.StartupLoginInitialization:
                    break;
                case DTO.ModuleFeatureStepType.StartupLoginAuthentication:
                    break;
                case DTO.ModuleFeatureStepType.StartupLoginChangepassword:
                    break;
                case DTO.ModuleFeatureStepType.PlanningHomeDashboard:
                    result = new HomeViewModel();
                    break;

                //Search
                case DTO.ModuleFeatureStepType.PlanningAnalyticsSearchAnalytics:
                case DTO.ModuleFeatureStepType.PlanningEverydayPricingSearchEveryday:
                case DTO.ModuleFeatureStepType.PlanningPromotionPricingSearchPromotions:
                case DTO.ModuleFeatureStepType.PlanningKitPricingSearchKits:
                    if (_searchViewModel == null)
                    {
                        _searchViewModel = new SearchViewModel(SelectedFeature);
                    }
                    else if (_searchViewModel.SelectedFeature != SelectedFeature)
                    {
                        _searchViewModel.SelectedFeature = SelectedFeature;
                    }
                    result = _searchViewModel;
                    break;

                //Identity
                case DTO.ModuleFeatureStepType.PlanningAnalyticsIdentity:
                    result = new AnalyticIdentityViewModel(SelectedAnalytic);
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingIdentity:
                    result = new PricingIdentityViewModel(SelectedPricingEveryday);
                    break;
                case DTO.ModuleFeatureStepType.PlanningPromotionPricingIdentity:
                case DTO.ModuleFeatureStepType.PlanningKitPricingIdentity:
                    break;

                //Filters
                case DTO.ModuleFeatureStepType.PlanningAnalyticsFilters:
                    result = new FilterViewModel(SelectedAnalytic);
                    break;
                case DTO.ModuleFeatureStepType.PlanningEverydayPricingFilters:
                    result = new FilterViewModel(SelectedPricingEveryday);
                    break;

                case DTO.ModuleFeatureStepType.PlanningPromotionPricingFilters:
                case DTO.ModuleFeatureStepType.PlanningKitPricingFilters:
                    break;

                //Price Lists
                case DTO.ModuleFeatureStepType.PlanningAnalyticsPriceLists:
                    result = new AnalyticPriceListViewModel(SelectedAnalytic, SelectedAnalytic.PriceListGroups);
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingPriceLists:
                    result = new PricingEverydayPriceListListViewModel(SelectedPricingEveryday);
                    break;

                case DTO.ModuleFeatureStepType.PlanningPromotionPricingPriceLists:
                case DTO.ModuleFeatureStepType.PlanningKitPricingPriceLists:
                    break;
                //Value Drivers
                case DTO.ModuleFeatureStepType.PlanningAnalyticsValueDrivers:
                    result = new AnalyticDriverViewModel(SelectedAnalytic);
                    break;

                //Results
                case DTO.ModuleFeatureStepType.PlanningAnalyticsResults:
                    result = new AnalyticResultsViewModel(SelectedAnalytic);
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingResults:
                    result = new PricingEverydayResultsViewModel(SelectedPricingEveryday);
                    break;

                case DTO.ModuleFeatureStepType.PlanningPromotionPricingResults:
                case DTO.ModuleFeatureStepType.PlanningKitPricingResults:
                    break;

                //Rounding
                case DTO.ModuleFeatureStepType.PlanningEverydayPricingRounding:
                    PricingEverydayRoundingViewModel vm = new PricingEverydayRoundingViewModel(SelectedPricingEveryday);
                    vm.RoundingTemplates = DisplayModuleGenerator.GetSampleRoundingTemplates();
                    result = vm;

                    break;

                case DTO.ModuleFeatureStepType.PlanningPromotionPricingRounding:
                case DTO.ModuleFeatureStepType.PlanningKitPricingRounding:
                    break;

                //Strategy
                case DTO.ModuleFeatureStepType.PlanningEverydayPricingStrategy:
                    result = new PricingEverydayStrategyViewModel(SelectedPricingEveryday);
                    break;

                case DTO.ModuleFeatureStepType.PlanningPromotionPricingStrategy:
                case DTO.ModuleFeatureStepType.PlanningKitPricingStrategy:
                    break;

                //Forecast
                case DTO.ModuleFeatureStepType.PlanningEverydayPricingForecast:
                    break;

                case DTO.ModuleFeatureStepType.PlanningPromotionPricingForecast:
                case DTO.ModuleFeatureStepType.PlanningKitPricingForecast:
                    break;

                //Approval
                case DTO.ModuleFeatureStepType.PlanningEverydayPricingApproval:
                    break;

                case DTO.ModuleFeatureStepType.PlanningPromotionPricingApproval:
                case DTO.ModuleFeatureStepType.PlanningKitPricingApproval:
                    break;

                case DTO.ModuleFeatureStepType.TrackingHomeDashboard:
                    break;

                case DTO.ModuleFeatureStepType.ReportingHomeDashboard:
                    break;

                case DTO.ModuleFeatureStepType.AdministrationHomeDashboard:
                    break;

                case DTO.ModuleFeatureStepType.AdministrationUserMaintenanceSearchUsers:
                    break;

                //case DTO.ModuleFeatureStepType.AdministrationUserMaintenanceUserLogin:
                //    break;

                case DTO.ModuleFeatureStepType.AdministrationUserMaintenanceUserIdentity:
                    break;

                case DTO.ModuleFeatureStepType.AdministrationUserMaintenanceUserRole:
                    break;

                case DTO.ModuleFeatureStepType.Null:
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Entry point for executing a command asynchronously and managing the display during the operation.
        /// </summary>        
        /// <param name="command">The ReactiveCommand to execute asynchronously.</param>
        /// <param name="callbackAction">The action to perform when the command completes.</param>
        /// <param name="workingMessage">The message to display while the command is executing.</param>
        /// <param name="completedMessge">(Optional) The message to display when the command completes.</param>
        private void ExecuteAsyncCommand<T>(ReactiveCommand<T> command, Action<T> callbackAction, string workingMessage, string completedMessge = null)
        {
            //Update the UI to indicate an operation is in progress.
            Mouse.OverrideCursor = Cursors.Wait;
            IsActionInProgress = true;
            SelectedFeatureViewModel = new WaitViewModel(IsActionInProgress, workingMessage);
            CurrentStatusText = workingMessage;

            //Execute the async command with the specifed callback delegate.
            CancellationToken token = new CancellationToken(false);
            command.ExecuteAsync().Subscribe(callbackAction, ex => HandleException(ex), () => OnCommandCompleted(completedMessge), token);
        }

        private void OnCommandCompleted(string completedMessge = null)
        {
            try
            {
                if (!String.IsNullOrEmpty(completedMessge))
                {
                    _eventManager.Publish(new OperationCompletedEvent(completedMessge));
                }
            }
            finally
            {
                ReenableUserInterface();
            }
        }

        private void ReenableUserInterface()
        {
            Mouse.OverrideCursor = null;
            CurrentStatusText = "Ready";
            IsActionInProgress = false;
        }

        private void HandleException(Exception error)
        {
            ShowMessageBox(error.Message, MessageBoxImage.Error);
            ReenableUserInterface();
        }

        /// <summary>
        /// Updates view model properties with dependencies on the selected module.
        /// </summary>        
        private void OnSelectedModuleChanged(Module module)
        {
            this.RaisePropertyChanged("IsModuleSelected");

            //TODO: this property will depend on whether the view is in detail mode (e.g., viewing a specific Analytic, Price Routine, etc.)
            IsFeatureSelectorAvailable = (module != null);
        }

        /// <summary>
        /// Updates view model properties with dependencies on the selected feature.
        /// </summary>        
        private void OnSelectedFeatureChanged(ModuleFeature feature)
        {
            SelectedFeature = feature;

            if (SelectedFeature != null)
            {
                //Pre-select the first search group if one is not already selected.
                var searchGroups = SelectedFeature.SearchGroupDisplayList;
                if (SelectedFeature.SelectedSearchGroup == null && searchGroups.Count > 0)
                {
                    SelectedFeature.SelectedSearchGroup = searchGroups[0];
                }
                OnSelectedEntityChanged(SelectedFeature.SelectedEntity);
            }
        }

        /// <summary>
        /// Updates view model properties with dependencies on the selected step
        /// </summary>     
        private void OnSelectedStepChanged(ModuleFeatureStep step)
        {
            if (step != null)
            {
                SelectedFeatureViewModel = GetViewModel(step);
            }

            this.RaisePropertyChanged("IsStepSelectorAvailable");
        }

        /// <summary>
        /// Updates view model properties with dependencies on the selected search entiy.
        /// </summary>        
        private void OnSelectedEntityChanged(ISearchableEntity entity)
        {
            if (entity == null)
            {
                SelectedFeature.DisableRemainingSteps();
            }
            else
            {
                SelectedFeature.EnableRemainingSteps();
            }

            SelectedAnalytic = entity as DisplayEntities.Analytic;
            SelectedPricingEveryday = entity as DisplayEntities.PricingEveryday;

            this.RaisePropertyChanged("IsEntitySelected");
            this.RaisePropertyChanged("SelectedEntity");
        }

        private void OnSearchGroupReassigned(SearchGroupsUpdatedEvent data)
        {
            if (data.SourceEntity.SearchKey != data.DestinationSearchGroup.SearchKey)
            {
                data.SourceEntity.SearchKey = data.DestinationSearchGroup.SearchKey;
                SelectedFeature.RecalculateSearchItemCounts();

                //Re-select the reassigned entity within its new search group.
                SelectedFeature.SelectedSearchGroup = data.DestinationSearchGroup;
                SelectedFeature.SelectedEntity = data.SourceEntity;
            }
        }

        #endregion

    }
}
