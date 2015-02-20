using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using APLPX.Client.Contracts;
using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.DisplayServices;
using APLPX.UI.WPF.Events;
using APLPX.UI.WPF.Helpers;
using APLPX.UI.WPF.Interfaces;
using APLPX.UI.WPF.Mappers;
using APLPX.UI.WPF.ViewModels.Analytic;
using APLPX.UI.WPF.ViewModels.Pricing;
using MahApps.Metro;
using NLog;
using ReactiveUI;
using DTO = APLPX.Entity;
using Ninject;
using Ninject.Parameters;

namespace APLPX.UI.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly IUserService _userService;
        private readonly IAnalyticService _analyticService;
        private readonly IPricingEverydayService _pricingEverydayService;
        private readonly AnalyticDisplayServices _analyticDisplayServices;
        private readonly PricingEverydayDisplayService _pricingEverydayDisplayServices;

        private List<Module> _modules;
        private ViewModelBase _selectedFeatureViewModel;
        private SearchViewModel _searchViewModel;
        private bool _isFeatureSelectorAvailable;
        private string _currentStatusText;
        private bool _isActionInProgress;
        private bool _isFoldersPanelVisible;
        private bool _isMessageCenterVisible;
        private bool _isPropertiesPanelVisible;

        private ISearchableEntity _originalEntity;
        private EventAggregator _eventManager;

        private Dictionary<DTO.ModuleFeatureType, ModuleFeature> _featureCache;
        private Dictionary<DTO.ModuleType, ViewModelBase> _moduleViewModelCache;

        public List<AccentColorMenuData> AccentColors { get; private set; }
        public List<AppThemeMenuData> AppThemes { get; private set; }

        #endregion

        #region Constructors and Initialization

        private MainViewModel(EventAggregator eventManager)
        {

            _eventManager = eventManager;
            _featureCache = new Dictionary<DTO.ModuleFeatureType, ModuleFeature>();
            _moduleViewModelCache = new Dictionary<DTO.ModuleType, ViewModelBase>();

            InitializeEventHandlers();
            InitializeCommands();
            //InitializeCommandErrorHandlers();
            CurrentStatusText = "Ready";

            // create accent color menu items for the demo
            this.AccentColors = ThemeManager.Accents
                                            .Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                            .ToList();

            // create metro theme color menu items for the demo
            this.AppThemes = ThemeManager.AppThemes
                                           .Select(a => new AppThemeMenuData() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                           .ToList();

        }

        /// <summary>
        /// Creates an instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="session">An autheticated session for the current user.</param>
        /// <param name="analyticService">An IAnalyticService provider.></param>
        /// <param name="userService">An IUserService provider.</param>
        public MainViewModel(DTO.Session<DTO.NullT> session,
                                IAnalyticService analyticService,
                                IUserService userService,
                                IPricingEverydayService pricingService,
                                EventAggregator eventManager
                            )
            : this(eventManager)
        {
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

            //base.UserServices = new UserDisplayServices(userService);
            _analyticService = analyticService;
            _userService = userService;
            _pricingEverydayService = pricingService;
            _analyticDisplayServices = new AnalyticDisplayServices(_analyticService, Session);
            _pricingEverydayDisplayServices = new PricingEverydayDisplayService(_pricingEverydayService, Session);

            CurrentUser = session.User.ToDisplayEntity();

            //TODO: COMMENT THIS OUT WHEN UserService is updated to work with new entity model:
            //Modules = DisplayModuleGenerator.CreateSampleModules();

            //TODO: UNCOMMENT THIS WHEN UserService is updated to work with new entity model:
            Modules = session.Modules.ToDisplayEntities();
            if (Modules == null || Modules.Count <= 0)
            {
                //App.Current.Windows[0].Close();
                ShowMessageBox("No licensed modules.");
                Application.Current.Shutdown();
            }
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
            
            _eventManager.GetEvent<SearchGroupsUpdatedEvent>().Subscribe(data => OnSearchGroupReassigned(data));

            _eventManager.GetEvent<FeatureSearchGroup>().Subscribe(data => OnCreateNewEntityRequested(data));

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
            AboutCommand = ReactiveCommand.Create();
            AboutCommand.Subscribe(_ => AboutCommandExecuted());
            ActionCommand = ReactiveCommand.Create();
            ActionCommand.Subscribe(x => ActionCommandExecuted(x));

            LaunchWebPageCommand = ReactiveCommand.Create();
            LaunchWebPageCommand.Subscribe(x => LaunchWebPageExecuted(x));

            ShowMessageCenterCommand = ReactiveCommand.Create();
            ShowMessageCenterCommand.Subscribe(x => ShowMessageCenterExecuted(x));

            ShowPropertiesPanelCommand = ReactiveCommand.Create();
            ShowPropertiesPanelCommand.Subscribe(x => ShowPropertiesPanelExecuted(x));

            ValidateEntityCommand = ReactiveCommand.Create();
            ValidateEntityCommand.Subscribe(x => ValidateEntityExecuted(x));

            LogoutCommand = ReactiveCommand.Create();
            LogoutCommand.Subscribe(x =>
            {
                var loginWindow = new LoginWindow();
                var vm = ViewModelBase.Kernel.Get<LoginViewModel>();
                vm.Session = null;
                vm.StatusMessage = string.Empty;
                vm.Password = string.Empty;
                vm.UserName = string.Empty;
                loginWindow.DataContext = vm;
                loginWindow.ShowMaxRestoreButton = false;
                loginWindow.ShowMinButton = false;
                loginWindow.ShowCloseButton = true;
                loginWindow.ShowInTaskbar = false;
                loginWindow.ShowActivated = true;
                loginWindow.Owner = App.Current.MainWindow;



                if (loginWindow.ShowDialog() == true)
                {
                    //TODO: trigger this at inactive timeout interval
                    if (vm.Session.Authenticated)
                    {
                        var main = ViewModelBase.Kernel.Get<MainViewModel>(
                            new ConstructorArgument("session", vm.Session)
                            );
                        //var main = ViewModelBase.Kernel.Get<MainViewModel>();

                        //main.Session = vm.Session;
                        //var mvm = new MainViewModel(vm.Session, _analyticService, _userService, _pricingEverydayService, _eventManager);
                        var mainW = new MainWindow();
                        mainW.DataContext = main;
                        mainW.Show();
                        App.Current.Windows[0].DataContext = null;
                        App.Current.Windows[0].Close();
                        App.Current.MainWindow = mainW;
                    }
                    else { loginWindow.ShowDialog(); }
                }
                else { App.Current.Shutdown(); }
            });


            LoadAnalyticListCommand = ReactiveCommand.CreateAsyncTask<Session<List<DisplayEntities.Analytic>>>(async _ =>
                    await Task.Run(() =>
                    {
                        return _analyticDisplayServices.LoadAnalyticList();
                    }


                ));

            LoadAnalyticCommand = ReactiveCommand.CreateAsyncTask<DisplayEntities.Analytic>(async _ =>
                await Task.Run<DisplayEntities.Analytic>( () =>
            {
                    SelectedFeature.RestoreSelectedSearchGroup();

                    DisplayEntities.Analytic sourceAnalytic = SelectedAnalytic;

                    int searchGroupId = GetSearchGroupId(Session.ClientCommand);
                    int entityId = 0;
                    if (SelectedEntity != null)
                    {
                        entityId = SelectedEntity.Id;                      
                    }
                    else
                    {
                        //This applies when creating a new Analytic.
                        sourceAnalytic = new DisplayEntities.Analytic();
                    }

                    return _analyticDisplayServices.LoadAnalytic(sourceAnalytic, entityId, searchGroupId);


                }));

            LoadPricingEverydayCommand = ReactiveCommand.CreateAsyncTask<Session<DisplayEntities.PricingEveryday>>(async _ =>
                await Task.Run(() =>
                {
                    return _pricingEverydayDisplayServices.LoadPricingEveryDay(SelectedPricingEveryday.Identity, SelectedEntity.Id);
                }));

            SaveAnalyticIdentityCommand = ReactiveCommand.CreateAsyncTask<DisplayEntities.Analytic>(async _ =>
                await Task.Run(() =>
                {
                    return _analyticDisplayServices.SaveAnalyticIdentity(SelectedAnalytic);
                }));

            SaveFiltersCommand = ReactiveCommand.CreateAsyncTask<DisplayEntities.Analytic>(async _ =>
                await Task.Run(() =>
                {
                    return _analyticDisplayServices.SaveFilters(SelectedAnalytic);
                }));

            SavePriceListsCommand = ReactiveCommand.CreateAsyncTask<DisplayEntities.Analytic>(async _ =>
                await Task.Run(() =>
                {
                    var response = _analyticDisplayServices.SavePriceLists(SelectedAnalytic);
                    return response;
                }));


            SaveOrRunValueDriversCommand = ReactiveCommand.CreateAsyncTask<DisplayEntities.Analytic>(async _ =>
                await Task.Run(() =>
                {
                    //SelectedAnalytic.SaveStateAreDriverResultsCurrent();
                    //var driverToRun = SelectedAnalytic.ValueDrivers.FirstOrDefault(d => d.RunResults);
                    //var driverKey = driverToRun.Key;

                    var response = _analyticDisplayServices.RunResults(SelectedAnalytic);
                    return response;

                }));

            RunResultsCommand = ReactiveCommand.CreateAsyncTask<DisplayEntities.Analytic>(async _ =>
            await Task.Run(() =>
            {
                return _analyticDisplayServices.RunResults(SelectedAnalytic);

            }));

                    }


        #endregion

        #region Properties

        public ReactiveCommand<DisplayEntities.Analytic> RunResultsCommand { get; private set; }

        public ReactiveCommand<object> LogoutCommand { get; private set; }

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
        /// Gets the currently selected feature step.
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
        /// The bound view is responsible for implementing related display behavior.
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
        /// The bound view is responsible for implementing related display behavior.
        /// </summary>
        public bool IsStepSelectorAvailable
        {
            get
            {
                bool isAvailable = false;

                if (SelectedFeature != null)
                {
                    isAvailable = (SelectedStep != SelectedFeature.DefaultLandingStep);
                }

                return isAvailable;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a specifc entity (e.g., Analytic, User, Price Routine, etc.) is currently selected.
        /// </summary>
        public bool IsSearchEntitySelected
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

        public bool IsActionSelectorAvailable
        {
            get
            {
                bool result = IsSearchEntitySelected || SelectedAnalytic != null || SelectedPricingEveryday != null;
                if (!result && SelectedStep != null)
                {
                    switch (SelectedStep.TypeId)
                    {
                        case APLPX.Entity.ModuleFeatureStepType.PlanningAnalyticsSearchAnalytics:
                        case APLPX.Entity.ModuleFeatureStepType.PlanningEverydayPricingSearchEveryday:
                        case APLPX.Entity.ModuleFeatureStepType.PlanningPromotionPricingSearchPromotions:
                        case APLPX.Entity.ModuleFeatureStepType.PlanningKitPricingSearchKits:
                        case APLPX.Entity.ModuleFeatureStepType.AdministrationUserMaintenanceSearchUsers:
                            //TODO: implement related CanExecute before enabling this code.
                            //Actions need to be visible, but only Create New should be enabled; Edit and Copy need to be disabled.
                            //result = true;
                            break;

                        default:
                            break;
                    }
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

        /// <summary>
        /// Gets a value indicating whether the folders panel should be displayed.
        /// </summary>
        public bool IsFoldersPanelVisible
        {
            get { return _isFoldersPanelVisible; }
            set { this.RaiseAndSetIfChanged(ref _isFoldersPanelVisible, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the message center should be displayed.
        /// </summary>
        public bool IsMessageCenterVisible
        {
            get { return _isMessageCenterVisible; }
            set { this.RaiseAndSetIfChanged(ref _isMessageCenterVisible, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the message center should be displayed.
        /// </summary>
        public bool IsPropertiesPanelVisible
        {
            get { return _isPropertiesPanelVisible; }
            set { this.RaiseAndSetIfChanged(ref _isPropertiesPanelVisible, value); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command for loading an analytic.
        /// </summary>
        protected ReactiveCommand<DisplayEntities.Analytic> LoadAnalyticCommand { get; private set; }

        /// <summary>
        /// Command for loading analytic list.
        /// </summary>
        protected ReactiveCommand<Session<List<DisplayEntities.Analytic>>> LoadAnalyticListCommand { get; private set; }

        /// <summary>
        /// Command for loading an analytic.
        /// </summary>
        protected ReactiveCommand<Session<PricingEveryday>> LoadPricingEverydayCommand { get; private set; }

        /// <summary>
        /// Command for saving an analytic identity.
        /// </summary>
        protected ReactiveCommand<DisplayEntities.Analytic> SaveAnalyticIdentityCommand { get; private set; }

        /// <summary>
        /// Command to save filters.
        /// </summary>
        protected ReactiveCommand<DisplayEntities.Analytic> SaveFiltersCommand { get; private set; }

        /// <summary>
        /// Command to save price lists
        /// </summary>
        protected ReactiveCommand<DisplayEntities.Analytic> SavePriceListsCommand { get; private set; }

        /// <summary>
        /// Command to save or run Value Drivers.
        /// </summary>
        protected ReactiveCommand<DisplayEntities.Analytic> SaveOrRunValueDriversCommand { get; private set; }

        /// <summary>
        /// Command that is invoked when any action is selected in the bound view.
        /// </summary>
        public ReactiveCommand<object> ActionCommand { get; private set; }

        /// <summary>
        /// Command to show the About box.
        /// </summary>
        public ReactiveCommand<object> AboutCommand { get; private set; }

        /// <summary>
        /// Command to display the Message Center.
        /// </summary>
        public ReactiveCommand<object> ShowMessageCenterCommand { get; private set; }

        /// <summary>
        /// Command to display the Properties panel.
        /// </summary>
        public ReactiveCommand<object> ShowPropertiesPanelCommand { get; private set; }

        public ReactiveCommand<object> ValidateEntityCommand { get; private set; }

        /// <summary>
        /// Command to launch a web page.
        /// </summary>
        public ReactiveCommand<object> LaunchWebPageCommand { get; private set; }

        private void ActionCommandExecuted(object sender)
        {
            var action = sender as DisplayEntities.Action;
            if (action != null &&
                (SelectedEntity != null ||
                 SelectedAnalytic != null ||
                 SelectedPricingEveryday != null))
            {
                HandleSelectedAction(action);
            }
        }

        /// <summary>
        /// Handles the action requested by the ActionCommand.
        /// </summary>
        /// <param name="action"></param>

        private void HandleSelectedAction(DisplayEntities.Action action)
        {
            string message = String.Empty;
            Session.ClientCommand = (int)action.TypeId;

            switch (action.TypeId)
            {
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

                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsEdit:

                    SelectedFeature.RestoreSelectedSearchGroup();
                    int searchGroupId = GetSearchGroupId(Session.ClientCommand);

                    int entityId = 0;
                    if (SelectedEntity != null)
                    {
                        entityId = SelectedEntity.Id;
                    }
                    ExecuteAsyncCommand(LoadAnalyticCommand, r => OnLoadAnalyticCommandCompleted(r), "Retrieving analytic...", "Load Analytic", "Analytic successfully loaded.");
                    break;

                case APLPX.Entity.ModuleFeatureStepActionType.PlanningEverydayPricingSearchEverydayEdit:
                    //TODO: get the full entity from the server and load edit screen.
                    //This is a simulation only.
                    SelectedPricingEveryday = SelectedEntity as DisplayEntities.PricingEveryday;
                    ExecuteAsyncCommand(LoadPricingEverydayCommand, r => OnLoadPricingEverydayCommandCompleted(r), "Retrieving pricing...", "Load Everyday Pricing", "Pricing Everyday successfully loaded.");
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningPromotionPricingSearchPromotionsEdit:
                case DTO.ModuleFeatureStepActionType.PlanningKitPricingSearchKitsEdit:
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsCopy:

                    ExecuteAsyncCommand(LoadAnalyticCommand,  r => OnLoadAnalyticCommandCompleted(r), "Copying analytic...", "Copy Analytic", "Analytic successfully copied.");
                    SelectedFeature.DisableRemainingSteps();
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsNew:
                    SelectedFeature.RestoreSelectedSearchGroup();

                    ExecuteAsyncCommand(LoadAnalyticCommand,  r => OnLoadAnalyticCommandCompleted(r), "Creating new analytic...", "New Analytic", "New Analytic successfully created.");
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

                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsIdentityCancel:
                    SelectedAnalytic = null;
                    SelectedFeature.SelectedStep.IsCompleted = false;
                    SelectedFeature.SelectedStep = SelectedFeature.DefaultLandingStep;
                    SelectedFeature.SelectedStep.IsCompleted = false;
                    SelectedFeature.DisableRemainingSteps();
                    break;

                //Save the current entity.
                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsIdentitySave:
                    //TODO: call analytic save method on service.
                    ExecuteAsyncCommand(SaveAnalyticIdentityCommand, r => OnSaveAnalyticIdentityCommandCompleted(r), "Saving Identity...", "Analytic - Save Identity", "Identity successfully saved");
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningEverydayPricingIdentitySave:
                    //TODO: call price routine save method on service.
                    SelectedPricingEveryday.IsDirty = false;
                    SelectedFeature.EnableRemainingSteps();
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningPromotionPricingIdentitySave:
                case DTO.ModuleFeatureStepActionType.PlanningKitPricingIdentitySave:
                    break;
                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsFiltersSave:
                    ExecuteAsyncCommand(SaveFiltersCommand, _ => OnFiltersCommandCompleted(), "Saving Filters...", "Analytic - Save Filters", "Filters successfully saved.");
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsPriceListsSave:
                    ExecuteAsyncCommand(SavePriceListsCommand,  r => OnSavePriceListsCommandCompleted(r),"Price Lists saving...", "Analytic - Save Price Lists", "Price Lists successfully saved." );
                    break;
                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversSave:
                    //Note: The Save action saves all value drivers only; it does not re-run any driver.

                    SelectedAnalytic.SaveStateAreDriverResultsCurrent();
                    var driverToSave = SelectedAnalytic.ValueDrivers.FirstOrDefault(d => d.RunResults);
                    var driverKey = driverToSave.Key;
                    ExecuteAsyncCommand(SaveOrRunValueDriversCommand,  r => OnSaveOrRunValueDriversCommandCompleted(r, driverKey), "Saving Value Drivers...", "Analytic - Save Value Drivers", "Value Drivers successfully saved.");
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsValueDriversRun:
                    //Note: The Run action saves all value drivers and re-runs the driver with RunResults set to true.     
                    var driverToRun = SelectedAnalytic.ValueDrivers.FirstOrDefault(d => d.RunResults);
                    message = String.Format("Saving all Value Drivers.\nRecalculating \"{0}\" Value Driver...", driverToRun.Name);
                    ExecuteAsyncCommand(SaveOrRunValueDriversCommand, r => OnSaveOrRunValueDriversCommandCompleted(r, driverToRun.Key), message,  "Analytic - Run Value Drivers", "Successfully ran value drivers.");
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningAnalyticsResultsRun:
                    var driversToRun = SelectedAnalytic.ValueDrivers.Where(driver => driver.RunResults)
                                                                    .Select((driver, index) => String.Format("{0}. {1}", index + 1, driver.Name));
                    if (driversToRun.Count() > 0)
                    {
                        string runList = String.Join("\n", driversToRun);
                        message = String.Format("Recalculating Value Drivers:\n{0}", runList);
                        ExecuteAsyncCommand(RunResultsCommand, r => OnRunResultsCommandCompleted(r), message, "Analytic - Run Results", "Successfully ran results.");
                    }
                    else
                    {
                        ShowMessageBox("Please select at least one Value Driver to run.");
                    }
                    break;

                case DTO.ModuleFeatureStepActionType.PlanningEverydayPricingPriceListsSave:
                case DTO.ModuleFeatureStepActionType.PlanningPromotionPricingPriceListsSave:
                case DTO.ModuleFeatureStepActionType.PlanningKitPricingPriceListsSave:
                    break;
            }
        }



        #endregion

        #region Methods



        #region Command Callbacks

        private void OnFiltersCommandCompleted()
        {
            SelectedAnalytic.FilterGroups.ClearIsDirty();
            SelectedFeature.SelectedStep.IsCompleted = true;
            SelectedFeature.EnableRemainingSteps();
        }
        private void OnSavePriceListsCommandCompleted(DisplayEntities.Analytic response)
        {
            SelectedAnalytic.PriceListGroups = new ReactiveList<AnalyticPriceListGroup>(response.PriceListGroups);
            SelectedAnalytic.FilterGroups.ClearIsDirty();
            SelectedFeature.SelectedStep.IsCompleted = true;
            SelectedFeature.EnableRemainingSteps();
        }
        private void OnLoadPricingEverydayCommandCompleted(Session<DisplayEntities.PricingEveryday> response)
        {
            var p = ((DisplayEntities.PricingEveryday)response.Data);
            SelectedPricingEveryday = p;
            SelectedFeature.SelectedStep.IsCompleted = true;
            SelectedFeature.SelectedStep = SelectedFeature.DefaultActionStep;
        }
        private void OnLoadAnalyticListCommandCompleted(Session<List<DisplayEntities.Analytic>> response)
        {
            var iSearchables = ((List<DisplayEntities.Analytic>)response.Data).Cast<ISearchableEntity>().ToList();
            SelectedFeature.SearchableEntities = iSearchables;
        }
        private void OnSaveAnalyticIdentityCommandCompleted(DisplayEntities.Analytic response)
        {
            SelectedAnalytic.Identity = response.Identity;
            SelectedAnalytic.IsDirty = false;


            ExecuteAsyncCommand(LoadAnalyticListCommand, r => OnLoadAnalyticListCommandCompleted(r));
            SelectedFeature.SelectedStep.IsCompleted = true;

            //TODO: determine why current step is sometimes disabled here.
            if (!SelectedFeature.SelectedStep.IsEnabled)
            {
                SelectedFeature.SelectedStep.IsEnabled = true;
            }

            SelectedFeature.EnableRemainingSteps();
        }
        private void OnLoadAnalyticCommandCompleted(DisplayEntities.Analytic response)
        {
            SelectedFeature.RestoreSelectedSearchGroup();
            var searchGroupId = GetSearchGroupId(Session.ClientCommand);
            //var a = ((DisplayEntities.Analytic)response.Item1.Data);
            if (Session.ClientCommand == (int)DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsNew &&
                   response.SearchGroupId != searchGroupId)
            {
                //Currentlt the service is sending back a value of 1 instead of the originating SearchGroupId,
                // so we must resolve it here for display purposes.
                response.SearchGroupId = searchGroupId;
            }

            SelectedAnalytic = response;

            foreach (AnalyticValueDriver driver in SelectedAnalytic.ValueDrivers)
            {
                driver.AssignResultsToDriverGroups();
                driver.AreResultsCurrent = true;
            }

            SelectedFeature.SelectedStep = SelectedFeature.DefaultActionStep;

            if (Session.ClientCommand == (int)DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsEdit)
            {
                //Treat the edit as completed when initially loaded. 
                SelectedFeature.SelectedStep.IsCompleted = true;
                SelectedFeature.EnableRemainingSteps();
            }
            else
            {
                SelectedAnalytic.IsDirty = true;
                SelectedFeature.SelectedStep.IsCompleted = false;
                SelectedFeature.DisableRemainingSteps();
            }
        }
        private void OnSaveOrRunValueDriversCommandCompleted(DisplayEntities.Analytic response, int driverKey)
        {

           
            SelectedAnalytic.ValueDrivers = new ReactiveList<AnalyticValueDriver>(response.ValueDrivers);

            SelectedAnalytic.RestoreStateAreDriverResultsCurrent();

            //Restore the selected value driver (the one that the service just recalculated) and mark its results current.
            SelectedAnalytic.SelectedValueDriver = SelectedAnalytic.ValueDrivers.FirstOrDefault(d => d.Key == driverKey);
            if (SelectedAnalytic.SelectedValueDriver != null)
            {
                SelectedAnalytic.SelectedValueDriver.AssignResultsToDriverGroups();
                SelectedAnalytic.SelectedValueDriver.AreResultsCurrent = true;
            }

            SelectedAnalytic.ValueDrivers.ClearIsDirty();
            SelectedFeature.SelectedStep.IsCompleted = true;
            SelectedFeature.EnableRemainingSteps();

        }
        private void OnRunResultsCommandCompleted(DisplayEntities.Analytic response)
        {
            SelectedFeatureViewModel = GetViewModel(SelectedStep);
            SelectedAnalytic.ValueDrivers = new ReactiveList<AnalyticValueDriver>(response.ValueDrivers);

            foreach (AnalyticValueDriver driver in SelectedAnalytic.ValueDrivers)
            {
                driver.AssignResultsToDriverGroups();
                driver.AreResultsCurrent = true;
            }
            SelectedFeature.SelectedStep.IsCompleted = true;
        }
        private string FormatDisplayErrorMessage(string title, string clientMessage, string serverMessage)
        {
            return string.Format("{0}" + Environment.NewLine + "{1}",
                              clientMessage, serverMessage);
        }
        private string FormatLogErrorMessage(string title, string clientMessage, string serverMessage)
        {
            return string.Format("{0}/n{1}/n{2}", title, clientMessage, serverMessage);
        }


        #endregion



        private bool SetIsFeatureSelectorAvailable()
        {
            bool isAvailable = (SelectedModule != null);
            if (isAvailable)
            {
                //Only show the feature selector if we're past the default landing step (which is typically, but not necessarily, Search).
                isAvailable = (SelectedFeature == null ||
                               SelectedStep == null ||
                               SelectedStep == SelectedFeature.DefaultLandingStep);
            }

            return isAvailable;
        }

        /// <summary>
        /// Determines the SearchGroupId to send to the service when loading an Analytic. This depends on:
        /// 1. The service action being called (e.g., New, Copy, Edit), and 
        /// 2. What is currently selected in the bound view.
        /// </summary>
        private int GetSearchGroupId(int commandId)
        {
            int result = 0;

            if (commandId == (int)DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsNew)
            {
                //For new analytics, the search group depends only on the selected search group;
                // (it is independent of the SelectedEntity, if any.)
                result = SelectedFeature.SelectedSearchGroup.SearchGroupId;
            }
            else if (SelectedEntity != null)
            {
                //If the SelectedEntity is in a "fictitious" folder, e.g., Recent, its SearchGroupId is 0. 
                //In this case, the OwningSearchGroupId points to the actual folder to which it is assigned.
                result = SelectedEntity.OwningSearchGroupId;
            }

            return result;
        }

        private void Navigate()
        {
            if (SelectedFeature != null)
            {
                switch (SelectedFeature.TypeId)
                {
                    case DTO.ModuleFeatureType.PlanningHome:
                        SelectedFeature.SelectedStep = SelectedFeature.DefaultLandingStep; SelectedFeatureViewModel = new HomeViewModel();
                        break;

                    case DTO.ModuleFeatureType.PlanningAnalytics:
                        if (!_featureCache.ContainsKey(SelectedFeature.TypeId))
                        {
                            ExecuteAsyncCommand(
                                LoadAnalyticListCommand, r => OnLoadAnalyticListCommandCompleted(r), "Loading Analytics...", "Analytic - Load List");
                        }
                        else if (SelectedFeatureViewModel != null)
                        {
                            SelectedFeatureViewModel.SelectedFeature = _featureCache[SelectedFeature.TypeId];
                        }
                        SelectedFeature.SelectedStep = SelectedFeature.DefaultLandingStep;

                        break;

                    case DTO.ModuleFeatureType.PlanningPromotionPricing:
                    case DTO.ModuleFeatureType.PlanningEverydayPricing:
                    case DTO.ModuleFeatureType.PlanningKitPricing:
                        //if (!_featureCache.ContainsKey(SelectedFeature.TypeId))
                        //{
                        //var pricing = _pricingEverydayService.LoadList(new DTO.Session<DTO.NullT> { SqlKey = Session.SqlKey }).Data;
                        //    SelectedFeature.SearchableEntities = pricing.Cast<ISearchableEntity>().ToList();
                        //    _featureCache.Add(SelectedFeature.TypeId, SelectedFeature);
                        //}
                        //else if (SelectedFeatureViewModel != null)
                        //{
                        //    SelectedFeatureViewModel.SelectedFeature = _featureCache[SelectedFeature.TypeId];
                        //}
                        SelectedFeature.SelectedStep = SelectedFeature.DefaultLandingStep;
                        break;


                    case DTO.ModuleFeatureType.Null:
                    case DTO.ModuleFeatureType.StartupLogin:

                    case DTO.ModuleFeatureType.TrackingHome:
                    case DTO.ModuleFeatureType.ReportingHome:
                    case DTO.ModuleFeatureType.AdministrationHome:
                    case DTO.ModuleFeatureType.AdministrationUserMaintenance:
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

                    SelectedFeature.RestoreSelectedSearchGroup();

                    result = _searchViewModel;
                    break;

                //Identity
                case DTO.ModuleFeatureStepType.PlanningAnalyticsIdentity:
                    result = new AnalyticIdentityViewModel(SelectedAnalytic, SelectedFeature);
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingIdentity:
                    result = new PricingIdentityViewModel(SelectedPricingEveryday);
                    break;
                case DTO.ModuleFeatureStepType.PlanningPromotionPricingIdentity:
                case DTO.ModuleFeatureStepType.PlanningKitPricingIdentity:
                    break;

                //Filters
                case DTO.ModuleFeatureStepType.PlanningAnalyticsFilters:
                    result = new FilterViewModel(SelectedAnalytic, SelectedFeature);
                    break;
                case DTO.ModuleFeatureStepType.PlanningEverydayPricingFilters:
                    result = new FilterViewModel(SelectedPricingEveryday, SelectedFeature);
                    break;

                case DTO.ModuleFeatureStepType.PlanningPromotionPricingFilters:
                case DTO.ModuleFeatureStepType.PlanningKitPricingFilters:
                    break;

                //Price Lists
                case DTO.ModuleFeatureStepType.PlanningAnalyticsPriceLists:
                    result = new AnalyticPriceListViewModel(SelectedAnalytic, SelectedFeature);
                    break;

                case DTO.ModuleFeatureStepType.PlanningEverydayPricingPriceLists:
                    result = new PricingEverydayPriceListListViewModel(SelectedPricingEveryday);
                    break;

                case DTO.ModuleFeatureStepType.PlanningPromotionPricingPriceLists:
                case DTO.ModuleFeatureStepType.PlanningKitPricingPriceLists:
                    break;
                //Value Drivers
                case DTO.ModuleFeatureStepType.PlanningAnalyticsValueDrivers:
                    result = new AnalyticDriverViewModel(SelectedAnalytic, SelectedFeature);
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
                    vm.RoundingTemplates = MockPricingEverydayGenerator.GetRoundingTemplates();
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

            _moduleViewModelCache[SelectedModule.TypeId] = result;

            return result;
        }

        /// <summary>
        /// Entry point for executing a command asynchronously and managing the display during the operation.
        /// </summary>        
        /// <param name="command">The ReactiveCommand to execute asynchronously.</param>
        /// <param name="callbackAction">The action to perform when the command completes.</param>
        /// <param name="workingMessage">The message to display while the command is executing.</param>
        /// <param name="completedMessge">(Optional) The message to display when the command completes.</param>
        private void ExecuteAsyncCommand<T>(ReactiveCommand<T> command, Action<T> callback = null,
            string workingMessage = "", string apiName = "", string successMessage = "", Tuple<int,int> parameters = null) where T : DisplayEntityBase
        {
            //Update the UI to indicate an operation is in progress.
            Mouse.OverrideCursor = Cursors.Wait;
            IsActionInProgress = true;
            SelectedFeatureViewModel = new WaitViewModel(IsActionInProgress, workingMessage);
            CurrentStatusText = workingMessage;

            command.ExecuteAsync(parameters).Subscribe(r =>
                {
                    if (!r.SessionOk)
                    {
                        HandleInvalidRequest(string.Format("Invalid Api Request : {0}", apiName), r.ClientMessage, r.ServerMessage);
                    }
                    else
            {
                        if (callback != null) { callback.Invoke(r); }
                        OnCommandCompleted(successMessage);
            }

                }, ex => HandleException(string.Format("Api Exception : {0}", apiName), ex), () => 
                    {
                        SelectedFeatureViewModel = GetViewModel(SelectedStep);
                        ReenableUserInterface();
                    });

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

        private void HandleException(string title, Exception error)
        {

            LogManager.GetCurrentClassLogger().Log(LogLevel.Error, String.Format("Api Error"), error);
            _eventManager.Publish<ErrorEvent>(new ErrorEvent { Title = title, Message = error.Message });
            //ShowMessageBox(error.Message, MessageBoxImage.Error);
            ReenableUserInterface();
            Navigate();

        }

        private void HandleInvalidRequest(string title, string clientMessage, string serverMessage)
        {
            LogManager.GetCurrentClassLogger().Log(LogLevel.Warn, FormatLogErrorMessage(title, clientMessage, serverMessage));
            ReenableUserInterface();
            _eventManager.Publish<ErrorEvent>(new ErrorEvent { Title = title, Message = FormatDisplayErrorMessage(title, clientMessage, serverMessage) });
            //Navigate();

        }

        /// <summary>
        /// Updates view model properties with dependencies on the selected module.
        /// </summary>        
        private void OnSelectedModuleChanged(Module module)
        {
            this.RaisePropertyChanged("IsModuleSelected");

            //Restore the view model associated with the module.
            if (module != null && _moduleViewModelCache.ContainsKey(module.TypeId))
            {
                SelectedFeatureViewModel = _moduleViewModelCache[module.TypeId];
            }

            IsFeatureSelectorAvailable = SetIsFeatureSelectorAvailable();
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
                //BMB: Initial selection taken out per request (Terri)
                /*
                var searchGroups = SelectedFeature.SearchGroupDisplayList;
                if (SelectedFeature.SelectedSearchGroup == null && searchGroups.Count > 0)
                {
                    SelectedFeature.SelectedSearchGroup = searchGroups[0];
                }
                */
                OnSelectedEntityChanged(SelectedFeature.SelectedEntity);
            }
            IsFeatureSelectorAvailable = SetIsFeatureSelectorAvailable();
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

            IsFeatureSelectorAvailable = SetIsFeatureSelectorAvailable();
            this.RaisePropertyChanged("IsStepSelectorAvailable");
            this.RaisePropertyChanged("IsActionSelectorAvailable");
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
                SelectedFeature.SetAllStepsCompleted(false);
            }

            SelectedAnalytic = entity as DisplayEntities.Analytic;
            SelectedPricingEveryday = entity as DisplayEntities.PricingEveryday;

            this.RaisePropertyChanged("IsEntitySelected");
            this.RaisePropertyChanged("SelectedEntity");
            this.RaisePropertyChanged("IsActionSelectorAvailable");
        }

        private void OnSearchGroupReassigned(SearchGroupsUpdatedEvent data)
        {
            if (data.SourceEntity.SearchGroupKey != data.DestinationSearchGroup.SearchGroupKey)
            {
                data.SourceEntity.SearchGroup = data.DestinationSearchGroup;
                data.SourceEntity.SearchGroupKey = data.DestinationSearchGroup.SearchGroupKey;
                data.SourceEntity.SearchGroupId = data.DestinationSearchGroup.SearchGroupId;

                SelectedFeature.RecalculateSearchItemCounts();

                //Re-select the reassigned entity within its new search group.
                SelectedFeature.SelectedSearchGroup = data.DestinationSearchGroup;
                SelectedFeature.SelectedEntity = data.SourceEntity;

                SelectedFeature.SelectedSearchGroup.IsSearchKeyChanged = true;
            }
        }

        private void OnCreateNewEntityRequested(FeatureSearchGroup data)
        {
            int searchGroupId = data.SearchGroupId;

            //TODO: make the service call aware of the search group ID.
            DisplayEntities.Action action = new DisplayEntities.Action { TypeId = DTO.ModuleFeatureStepActionType.PlanningAnalyticsSearchAnalyticsNew };
            HandleSelectedAction(action);
        }

        private void AboutCommandExecuted()
        {
            var viewModel = new AboutViewModel();
            _eventManager.Publish(viewModel);
        }

        private void LaunchWebPageExecuted(object parameter)
        {
            string url = Convert.ToString(parameter);

            if (!String.IsNullOrWhiteSpace(url))
            {
                System.Diagnostics.Process.Start(url);
            }
        }

        private void ShowMessageCenterExecuted(object parameter)
        {
            IsMessageCenterVisible = true;
        }

        private void ShowPropertiesPanelExecuted(object parameter)
        {
            IsPropertiesPanelVisible = true;
        }


        private void ValidateEntityExecuted(object parameter)
        {
            if (SelectedAnalytic != null)
            {
                SelectedAnalytic.ValidationResults.Clear();
                var errors = SelectedAnalytic.GetValidationErrors();
                foreach (Error error in errors)
                {
                    SelectedAnalytic.ValidationResults.Add(error);
                }
            }
        }

        #endregion


    }
}
