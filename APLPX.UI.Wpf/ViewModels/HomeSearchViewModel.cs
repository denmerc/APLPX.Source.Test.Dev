using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive.Linq;
using APLPX.UI.WPF.ViewModels.Events;
using Domain = APLPX.Client.Display;
using System.Windows;
using APLPX.Server.Data;



namespace APLPX.UI.WPF.ViewModels
{
    //Default SubModule for plannning
    public class HomeSearchViewModel : ViewModelBase
    {
        /*
         * Bindings List submodules, List of tags, List of results, selected entityVM -- set of actions eg. copy, edit, add
         * 
         * command that switches object type to search (analytics or price routines) & gets tags by submodule type
         * command that set detail to specfic object in searched list
         * */
        public HomeSearchViewModel(ISearchRepository repo, Domain.Session session, Reactive.EventAggregator eventManager)
        {

            Session = session;
            EventManager = eventManager;



            //SubModuleKeys = new[] { Domain.SubModuleTypke.Analytics, Domain.SubModuleType.Everyday, Domain.SubModuleType.Promotions, Domain.SubModuleType.Kits, Domain.SubModuleType.MySettings }.ToList();
            SubModuleKeys = Enum.GetValues(typeof(Domain.SubModuleType)).Cast<Domain.SubModuleType>().ToList();

            LoadFolderSetCommand = ReactiveCommand.CreateAsyncTask(async _ =>
                await Task.Run(() =>
                {

                    FolderSet = repo.AllFolderSets();


                }));

            


            LoadTagsBySubModuleCommand = ReactiveCommand.Create();
            LoadTagsBySubModuleCommand.Subscribe(x =>
            {
                switch((Domain.SubModuleType) x)
                {
                    case Domain.SubModuleType.Analytics:
                        AnalyticTags = FolderSet.SelectedAnalyticFolders;
                        //AnalyticTags = repo.AllTagsBySubModule(x.ToString());
                        break;
                    case Domain.SubModuleType.Everyday:
                    case Domain.SubModuleType.Promotions:
                    case Domain.SubModuleType.Kits:
                        PricingTags = repo.AllTagsBySubModule(x.ToString());
                        break;
                }
            });

            LoadFavoritesBySubModuleCommand = ReactiveCommand.Create();
            LoadFavoritesBySubModuleCommand.Subscribe(x =>
            {
                switch((Domain.SubModuleType) x)
                {
                    case Domain.SubModuleType.Analytics:
                        FavoriteTags = repo.AllFoldersBySubModule(x.ToString());

                        //FavoriteTags = repo.FindFavoriteTagsByUserAndSubModule(Session.User.Login, (Domain.SubModuleType)x);
                        break;
                    case Domain.SubModuleType.Everyday:
                    case Domain.SubModuleType.Promotions:
                    case Domain.SubModuleType.Kits:
                        FavoritePricingTags = repo.AllFoldersBySubModule(x.ToString());
                        //FavoritePricingTags = repo.FindFavoriteTagsByUserAndSubModule(Session.User.Login, (Domain.SubModuleType)x);
                        break;
                }
            });


            LoadFolderSetCommand.ExecuteAsync().Subscribe(x =>
            {
                Console.WriteLine("test");

            });
            ;
            //LoadFavoritesBySubModuleCommand.Execute(Domain.SubModuleType.Analytics);
            //LoadFavoritesBySubModuleCommand.Execute(Domain.SubModuleType.Everyday);
            //LoadTagsBySubModuleCommand.Execute(Domain.SubModuleType.Analytics);
            //LoadTagsBySubModuleCommand.Execute(Domain.SubModuleType.Everyday);

            EventManager.GetEvent<Domain.SubModuleType>()
                .Subscribe(submodule =>
                {
                    SelectedSubModule = submodule;
                    
                    switch (submodule)
                    {
                        case Domain.SubModuleType.Analytics:
                            SelectedSubModuleIndex = 0;
                            if (FolderSet != null)
                            {
                                SelectedFavTags = FolderSet.SelectedAnalyticFolders; //TODO: move to main
                                Tags = FolderSet.MasterAnalyticFolderList;
                            }
                            if (SelectedAnalytic != null) { ToggleResults("Analytics"); }
                            else
                            {
                                ToggleResults("All");
                            }
                            break;
                        case Domain.SubModuleType.Everyday:
                            if(FolderSet != null)
                            {
                                SelectedFavTags = FolderSet.SelectedEverydayFolders;
                                Tags = FolderSet.MasterEverydayFolderList;
                            }
                            SelectedSubModuleIndex = 1;
                            if (SelectedPriceRoutine != null) { ToggleResults("Pricing"); }
                            else
                            {
                                ToggleResults("All");
                            }
                            break;
                        case Domain.SubModuleType.Promotions:
                            if (FolderSet != null)
                            {
                                SelectedFavTags = FolderSet.SelectedPromotionFolders;
                                Tags = FolderSet.MasterPromotionFolderList;
                            }
                            SelectedSubModuleIndex = 2;
                            if(SelectedPriceRoutine != null){ToggleResults("Pricing");}
                            else
                            {
                                ToggleResults("All");
                            }
                            break;

                        case Domain.SubModuleType.Kits:
                            if (FolderSet != null)
                            {
                                SelectedFavTags = FolderSet.SelectedKitFolders;
                                Tags = FolderSet.MasterKitFolderList;
                            }
                            SelectedSubModuleIndex = 3;
                            if(SelectedPriceRoutine != null){ToggleResults("Pricing");}
                            else
                            {
                                ToggleResults("All");
                            }
                            break;
                        case Domain.SubModuleType.MySettings:
                            break;
                        case Domain.SubModuleType.Search:
                            break;
                        default:
                            break;
                    }
                });


            

            LoadAnalyticsByTagCommand = ReactiveCommand.CreateAsyncTask( async evt =>
                await Task.Run( () =>
                {
                    var list = ((Events.TagSearchEvent) evt).Tags.Select(y => y.Value).ToList();
                     Analytics = repo.FindAnalyticsByTag(list);


                }));
                //Analytics = repo.FindByTag<Domain.Analytic>(new List<string> { ((Domain.Tag)x).Value });

            //loadAnalyticsByTagCommand.ToProperty(this, x => x.Analytics);

            //LoadAnalyticsByTagCommand = ReactiveCommand.Create();
            //LoadAnalyticsByTagCommand.Subscribe(x => 
            //{
            //    var list = ((Events.TagSearchEvent)x).Tags.Select(y => y.Value).ToList();
            //    //Analytics = repo.FindByTag<Domain.Analytic>(new List<string> { ((Domain.Tag)x).Value });
            //    Analytics = repo.FindAnalyticsByTag(list);
            //});


            LoadPricingByTagCommand = ReactiveCommand.CreateAsyncTask( async evt => 
                await Task.Run( () => {
                    var list = ((Events.TagSearchEvent) evt).Tags.Select(y => y.Value).ToList();
                    PriceRoutines = repo.FindPricingByTag(list);
            }));
            //LoadPricingByTagCommand.Subscribe(x =>
            //{
            //    var list = ((Events.TagSearchEvent)x).Tags.Select(y => y.Value).ToList();
            //    PriceRoutines = repo.FindPricingByTag(list);
            //});

            EventManager.GetEvent<ViewModels.Events.TagSearchEvent>().Subscribe(async evt =>
            {

                switch (evt.SubModule)
                {
                    case Domain.SubModuleType.Analytics:
                        ToggleResults("Analytics");
                        await LoadAnalyticsByTagCommand.ExecuteAsync(evt);
                        break;
                    case Domain.SubModuleType.Everyday:
                    case Domain.SubModuleType.Promotions:
                    case Domain.SubModuleType.Kits:
                        ToggleResults("Pricing");
                        await LoadPricingByTagCommand.ExecuteAsync(evt);
                        break;
                    case Domain.SubModuleType.MySettings:
                        break;
                    case Domain.SubModuleType.Search:
                        break;
                    default:
                        break;
                }
            });

            //watch for changed selected analytic & populate selectedAnalytic prop on this vm to capture state
            EventManager.GetEvent<SelectionEvent>().Subscribe(selection =>
            {
                switch (selection.EntityType)
                {
                    case Domain.SubModuleType.Analytics:
                        SelectedAnalytic = selection.Entity as Domain.Analytic;
                        //IsDetailDisplayed = Visibility.Hidden;this.RaisePropertyChanged("IsDetailDisplayed");
                        //IsDetailDisplayed = Visibility.Visible; this.RaisePropertyChanged("IsDetailDisplayed");
                        IsActionBarOn = Visibility.Visible;
                        break;
                    case Domain.SubModuleType.Everyday:
                    case Domain.SubModuleType.Promotions:
                    case Domain.SubModuleType.Kits:
                        //IsPDetailDisplayed = Visibility.Hidden;this.RaisePropertyChanged("IsPDetailDisplayed");
                        //IsPDetailDisplayed = Visibility.Visible; this.RaisePropertyChanged("IsPDetailDisplayed");

                        SelectedPriceRoutine = selection.Entity as Domain.PriceRoutine;
                        IsActionBarOn = Visibility.Visible;
                        break;
                    case Domain.SubModuleType.MySettings:
                        break;
                    case Domain.SubModuleType.Search:
                        break;
                    default:
                        break;
                }
            });
        }


        public void LoadFolders(Domain.SubModuleType submodule)
        {
            switch (submodule)
            {
                case Domain.SubModuleType.Analytics:

                    SelectedFavTags = FolderSet.SelectedAnalyticFolders;
                    Tags = FolderSet.MasterAnalyticFolderList;
                    //Tags = AnalyticTags.Union(SelectedFavTags).ToList();
                    //if(FavoriteTags == null)
                    //{
                    //LoadFavoritesBySubModuleCommand.ExecuteAsync(submodule).Subscribe( x => {
                    //    SelectedFavTags = FavoriteTags;
                    //    Tags = AnalyticTags.Union(SelectedFavTags).ToList();
                    //});

                    //}
                    if (Analytics == null)
                    { LoadTagsBySubModuleCommand.Execute(submodule); }

                    ToggleResults("All");
                    break;
                case Domain.SubModuleType.Everyday:
                case Domain.SubModuleType.Promotions:
                case Domain.SubModuleType.Kits:
                    SelectedFavTags = FolderSet.SelectedEverydayFolders;
                    Tags = FolderSet.MasterEverydayFolderList;
                    //Tags = AnalyticTags.Union(SelectedFavTags).ToList();
                    //LoadFavoritesBySubModuleCommand.ExecuteAsync(submodule).Subscribe( x => {
                    //    SelectedFavTags = FavoritePricingTags;
                    //    Tags = PricingTags.Union(SelectedFavTags).ToList();
                    //});
                    if (PriceRoutines == null)
                    { LoadTagsBySubModuleCommand.Execute(submodule); }


                    ToggleResults("All");
                    break;
                case Domain.SubModuleType.MySettings:
                    break;
                case Domain.SubModuleType.Search:
                    break;
                default:
                    break;
            }
        }
        private Domain.FolderSet _FolderSet;
        public Domain.FolderSet FolderSet { get { return _FolderSet; } set { this.RaiseAndSetIfChanged(ref _FolderSet, value); } }

        private List<string> _favoritePricingTags;
        public List<string> FavoritePricingTags
        {
            get
            {
                return _favoritePricingTags;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _favoritePricingTags, value.ToList());
            }
        }
        private List<string> _pricingTags;
        public List<string> PricingTags
        {
            get
            {
                return _pricingTags;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _pricingTags, value.ToList());
            }
        }

        private List<string> _analyticTags;
        public List<string> AnalyticTags
        {
            get
            {
                return _analyticTags;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _analyticTags, value.ToList());
            }
        }

        private List<string> _selectedFavTags;
        public List<string> SelectedFavTags
        {
            get
            {
                return _selectedFavTags;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedFavTags, value.ToList());
            }
        }

        private List<string> _favoriteTags;
        public List<string> FavoriteTags
        {
            get
            {
                return _favoriteTags;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _favoriteTags, value.ToList());
            }
        }
        private List<string> _tags;
        public List<string> Tags 
        { 
            get 
            {
                return _tags;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _tags, value.ToList()); 
            }            
        }
        private Domain.Analytic _SelectedAnalytic;
        public Domain.Analytic SelectedAnalytic
        {
            get
            {
                return _SelectedAnalytic;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedAnalytic, value);
            }
        }

        private Domain.PriceRoutine _SelectedPriceRoutine;
        public Domain.PriceRoutine SelectedPriceRoutine
        {
            get
            {
                return _SelectedPriceRoutine;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedPriceRoutine, value);
            }
        }

        private List<Domain.Analytic> _analytics;
        public List<Domain.Analytic> Analytics
        {
            get
            {
                return _analytics;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _analytics, value);
            }
        }


        private List<Domain.PriceRoutine> _PriceRoutines;
        public List<Domain.PriceRoutine> PriceRoutines
        {
            get
            {
                return _PriceRoutines;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _PriceRoutines, value);
            }
        }
        public Reactive.EventAggregator EventManager { get; set; }
        public List<Domain.SubModuleType> SubModuleKeys { get; set; }

        private Domain.SubModuleType _selectedSubModule = Domain.SubModuleType.Search;
        public Domain.SubModuleType SelectedSubModule { get { return _selectedSubModule; } set {this.RaiseAndSetIfChanged(ref _selectedSubModule,value); } }

        private int _selectedSubModuleIndex = 100;
        public int SelectedSubModuleIndex { get { return _selectedSubModuleIndex; } set { this.RaiseAndSetIfChanged(ref _selectedSubModuleIndex, value); } }



        public List<String> SelectedTagItems { get; set; }

        private System.Windows.Visibility _IsTagsDisplayed = System.Windows.Visibility.Hidden;
        public System.Windows.Visibility IsTagsDisplayed 
        {
            get { return _IsTagsDisplayed; }
            set { this.RaiseAndSetIfChanged(ref _IsTagsDisplayed, value); }
        }

        public void ToggleSearchPane(Domain.SubModuleType type) //TODO: only tags needed
        {
            switch (type)
            {
                case Domain.SubModuleType.Analytics:

                    break;
                case Domain.SubModuleType.Everyday:
                case Domain.SubModuleType.Promotions:
                case Domain.SubModuleType.Kits:
                    break;
                case Domain.SubModuleType.MySettings:
                    break;
                case Domain.SubModuleType.Search:
                    IsTagsDisplayed = System.Windows.Visibility.Visible;
                    switch (SelectedSubModule)
	                {
                        case Domain.SubModuleType.Analytics:
                            IsFiltersDisplayed = Visibility.Visible;
                            //IsFiltersPDisplayed = Visibility.Collapsed;
                            IsTagsDisplayed = Visibility.Visible;
                            //IsPDetailDisplayed = Visibility.Collapsed;this.RaisePropertyChanged("IsPDetailDisplayed");
                            //IsDetailDisplayed = Visibility.Visible;
                            //IsProgressBarAOn = Visibility.Hidden;
                            //IsProgressBarPOn = Visibility.Hidden;
                            break;
                        case Domain.SubModuleType.Everyday:
                        case Domain.SubModuleType.Promotions:
                        case Domain.SubModuleType.Kits:
                            //IsFiltersDisplayed = Visibility.Collapsed;
                            //IsFiltersPDisplayed = Visibility.Visible;
                            IsTagsDisplayed = Visibility.Visible;
                            //IsDetailDisplayed = Visibility.Collapsed;
                            //IsPDetailDisplayed = Visibility.Visible;
                            //IsProgressBarAOn = Visibility.Hidden;
                            //IsProgressBarPOn = Visibility.Hidden;
                            break;
                        
                        default:
                            //IsFiltersDisplayed = Visibility.Collapsed;
                            //IsFiltersPDisplayed = Visibility.Collapsed;   
                            IsTagsDisplayed = Visibility.Collapsed;
                            //IsDetailDisplayed = Visibility.Collapsed;
                            //IsPDetailDisplayed = Visibility.Collapsed;
                            //IsProgressBarAOn = Visibility.Collapsed;
                            //IsProgressBarPOn = Visibility.Collapsed;
                            break;
	                }
                    
                    break;
                default:
                    break;
            }
            //if (on)
            //{
            //    IsFiltersDisplayed = System.Windows.Visibility.Visible ;
            //    IsDetailDisplayed = System.Windows.Visibility.Visible;
            //    IsTagsDisplayed = System.Windows.Visibility.Visible;
            //}
            //else
            //{
            //    IsFiltersDisplayed = System.Windows.Visibility.Hidden;
            //    IsDetailDisplayed = System.Windows.Visibility.Collapsed;
            //    IsPDetailDisplayed = System.Windows.Visibility.Collapsed;
              
            //    IsTagsDisplayed = System.Windows.Visibility.Hidden;
            //}
        }

        public void ToggleResults(string type)
        {
            if (type == "Pricing") { IsFiltersDisplayed = Visibility.Collapsed; IsFiltersPDisplayed = Visibility.Visible; IsDetailDisplayed = Visibility.Collapsed; IsPDetailDisplayed = Visibility.Collapsed; IsActionBarOn = Visibility.Collapsed; }
            else if (type == "Analytics") { IsFiltersDisplayed = Visibility.Visible; IsFiltersPDisplayed = Visibility.Collapsed; IsDetailDisplayed = Visibility.Collapsed; IsPDetailDisplayed = Visibility.Collapsed; IsActionBarOn = Visibility.Collapsed; }
            else if (type == "All") { 
                IsFiltersDisplayed = Visibility.Collapsed; IsFiltersPDisplayed = Visibility.Collapsed; 
                IsDetailDisplayed = Visibility.Collapsed; IsPDetailDisplayed = Visibility.Collapsed; 
                IsActionBarOn = Visibility.Collapsed; 
                
            }
            
        }

        private System.Windows.Visibility _IsFiltersDisplayed = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility IsFiltersDisplayed
        {
            get { return _IsFiltersDisplayed; }
            set { this.RaiseAndSetIfChanged(ref _IsFiltersDisplayed, value); }
        }

        private System.Windows.Visibility _IsFiltersPDisplayed = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility IsFiltersPDisplayed
        {
            get { return _IsFiltersPDisplayed; }
            set { this.RaiseAndSetIfChanged(ref _IsFiltersPDisplayed, value); }
        }

        private System.Windows.Visibility _IsDetailDisplayed = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility IsDetailDisplayed
        {
            get { return _IsDetailDisplayed; }
            set { this.RaiseAndSetIfChanged(ref _IsDetailDisplayed, value); }
        }
        private System.Windows.Visibility _IsPDetailDisplayed = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility IsPDetailDisplayed
        {
            get { return _IsPDetailDisplayed; }
            set { this.RaiseAndSetIfChanged(ref _IsPDetailDisplayed, value); }
        }

        private System.Windows.Visibility _IsActionBarOn = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility IsActionBarOn
        {
            get { return _IsActionBarOn; }
            set { this.RaiseAndSetIfChanged(ref _IsActionBarOn, value); }
        }

        private System.Windows.Visibility _IsProgressBarAOn = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility IsProgressBarAOn
        {
            get { return _IsProgressBarAOn; }
            set { this.RaiseAndSetIfChanged(ref _IsProgressBarAOn, value); }
        }

        private System.Windows.Visibility _IsProgressBarPOn = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility IsProgressBarPOn
        {
            get { return _IsProgressBarAOn; }
            set { this.RaiseAndSetIfChanged(ref _IsProgressBarAOn, value); }
        }
        protected ReactiveCommand<object> LoadTagsBySubModuleCommand;
        protected ReactiveCommand<System.Reactive.Unit> LoadAnalyticsByTagCommand;
        //protected ReactiveCommand<object> LoadAnalyticsByTagCommand;
        protected ReactiveCommand<System.Reactive.Unit> LoadPricingByTagCommand;
        protected ReactiveCommand<object> LoadFavoritesBySubModuleCommand;
        protected ReactiveCommand<System.Reactive.Unit> LoadFolderSetCommand;





        public ISearchRepository SearchRepository { get; set; }
        public Domain.Session Session { get; set; }

        //public Navigator Navigator { get; set; } //TODO: needs to be handled globally on mainviewmodel but can be passed as ref

        //public AnalyticViewModel SelectedAnalyticViewModel { get; set; }
        //public PricingViewModel SelectedPricingViewModel { get; set; }




    }




  





}

