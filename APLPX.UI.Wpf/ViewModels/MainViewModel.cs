using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APLPX.UI.WPF;
using APLPX.UI.WPF.ViewModels;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using APLPX.UI.WPF.ViewModels.Events;
using APLPX.Server.Data;
using Domain = APLPX.Client.Entity;
using System.Windows;


namespace APLPX.UI.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public static Dictionary<Domain.SubModuleType, ViewModelBase> SubModuleCache = new Dictionary<Domain.SubModuleType, ViewModelBase>();
        //private static Dictionary<Domain.SubModuleType, List<ViewModelBase>> SubModuleCacheHistory = new Dictionary<Domain.SubModuleType, List<ViewModelBase>>();

        public MainViewModel()
        {
            
            //Seed();
            EventManager = ((Reactive.EventAggregator)App.Current.Resources["EventManager"]);
            SearchRepo = new MockSearchRepository();
            AnalyticRepo = new MockAnalyticRepository();
            PricingRepo = new MockPricingRepository();

            //initialize session --> login mock User.IsAuthenticated = true
            //load session module list and tags
            Session = new Domain.Session
            {
                User = new Domain.User { Login = "dennism", FirstName = "Dennis", LastName = "Mercado", IsAuthenticated  = true }

            };


            //var loadTags = ReactiveCommand.Create();
            //loadTags.Subscribe(_ => {

            //    Session.Tags = SearchRepo.AllTags();
            //    Console.WriteLine(Session.Tags);
            //});

            //loadTags.Execute(null);

            //EventManager.GetEvent<Domain.ModuleType>()
            //    .Subscribe(module =>
            //    {
            //        var moduleType = (Domain.ModuleType)module;
            //        if(SubModuleCache.ContainsKey(moduleType))
            //        {
            //             SelectedSubModuleViewModel = SubModuleCache[moduleType];
            //        }
            //        else
            //        {
            //             SelectedSubModuleViewModel = new HomeSearchViewModel(SearchRepo, Session, EventManager);
            //        }
            //    });

            EventManager.GetEvent<NavigateEvent>()
                .Subscribe(navigator =>
                {
                    switch (navigator.Module)
                    {
                        case Domain.ModuleType.Planning:
                            switch (navigator.SubModule)
                            {
                                case Domain.SubModuleType.Analytics: //static-singleton SubModuleVM with proxies with sections reloaded
                                    //add if it doesnt exist otherwise navigate 
                                    if(!SubModuleCache.ContainsKey(navigator.SubModule))
                                    {
                                        //populate name for header
                                        var analytic = (Domain.Analytic)navigator.Entity;

                                        SelectedSubModuleViewModel = new AnalyticModuleViewModel(AnalyticRepo, Session, analytic.Name);
                                        SubModuleCache.Add(navigator.SubModule, SelectedSubModuleViewModel);
                                    }
                                    else
                                    {
                                        SelectedSubModuleViewModel = SubModuleCache[Domain.SubModuleType.Analytics];
                                
                                    }
                                    //if(navigator.Entity != null)
                                       ((AnalyticModuleViewModel)SubModuleCache[navigator.SubModule]).Navigate(navigator);
                                       //load tags from home search here todo:
                                
                                    break;
                                case Domain.SubModuleType.Everyday:
                                case Domain.SubModuleType.Promotions:
                                case Domain.SubModuleType.Kits:
                                    if (!SubModuleCache.ContainsKey(navigator.SubModule))
                                    {
                                        var priceRoutine = (Domain.PriceRoutine)navigator.Entity;
                                        SelectedSubModuleViewModel = new PricingModuleViewModel(PricingRepo, Session, priceRoutine.Name);
                                        SubModuleCache.Add(navigator.SubModule, SelectedSubModuleViewModel);
                                    }
                                    else
                                    {
                                        SelectedSubModuleViewModel = SubModuleCache[Domain.SubModuleType.Everyday];
                                    }
                                    ((PricingModuleViewModel)SubModuleCache[navigator.SubModule]).Navigate(navigator);
                                    break;
                                case Domain.SubModuleType.Search:
                                    if(!SubModuleCache.ContainsKey(navigator.SubModule))
                                    {
                                        SelectedSubModuleViewModel = new HomeSearchViewModel(SearchRepo, Session, EventManager); 
                                    }
                                    else
                                    {
                               
                                        SelectedSubModuleViewModel = SubModuleCache[Domain.SubModuleType.Search];
                                        this.RaisePropertyChanged("SelectedSubModuleViewModel");
                                        ((HomeSearchViewModel)SelectedSubModuleViewModel).ToggleSearchPane(navigator.SubModule);

                                    }
                                    break;
                                case Domain.SubModuleType.MySettings:
                                    //if(!SubModuleCache.ContainsKey(navigator.SubModule))
                                    //{
                                    //    SelectedSubModuleViewModel = new FolderSettingsViewModel(); 
                                    //}
                                    //else
                                    //{
                               
                                    //    SelectedSubModuleViewModel = SubModuleCache[Domain.SubModuleType.Search];
                               

                                    //}
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case Domain.ModuleType.Tracking:
                            break;
                        case Domain.ModuleType.Reporting:
                            break;
                        case Domain.ModuleType.Administration:


                            if (!SubModuleCache.ContainsKey(navigator.SubModule))
                            {
                                SelectedSubModuleViewModel = new AdminModuleViewModel();
                                SubModuleCache.Add(navigator.SubModule, SelectedSubModuleViewModel);
                            }
                            else
                            {
                                SelectedSubModuleViewModel = ((AdminModuleViewModel)SubModuleCache[navigator.SubModule]);
                            }


                            //((AdminViewModel)SelectedSubModuleViewModel).Navigate(navigator.Section);

                            //switch (navigator.SubModule)
                            //{

                            //    case Domain.SubModuleType.AdminDefault: //change SelectedSectionViewModel to FolderSettingsViewModel
                            //        ((AdminViewModel)SelectedSubModuleViewModel).Navigate(navigator.Section);
                            //        break;
                                
                            //    default:
                            //        break;
                                
                            //}
                            break;
                        default:
                            break;
                    }
                    
                    //if (!SubModuleCache.ContainsKey(navigator.SubModule))
                    //{
                    //    SubModuleCache.Add(navigator.SubModule, SelectedSubModuleViewModel);
                    //}
                    //SelectedSubModuleViewModel = Navigator.NavigateSectionBySectionType((Domain.SubModuleType)section);
                    
                });
            //SubModuleKeys = new[] { Domain.SubModuleType.Analytics, Domain.SubModuleType.Everyday, Domain.SubModuleType.Promotions, Domain.SubModuleType.Kits, Domain.SubModuleType.MySettings }.ToList();


            
           //default
           SelectedSubModuleViewModel = new HomeSearchViewModel(SearchRepo, Session, EventManager); 
           SubModuleCache.Add(Domain.SubModuleType.Search, SelectedSubModuleViewModel);


            //Commands
            //search by tags -> session.analytics
            //ReadAnalytic(id) find in session.analytics and show detail in panel inline
            //EditAnalytic(id) SelectedModuleViewModel == EditAnalyticVM(analytic)
            //navigate module -- change SelectedModuleVM
            //navigate by submodule -- change SelectedSubModuleVM
            //navigate by section -- change SelectedSectionVM
            

       
            //load lookups - filters  Filters_GetDistinctFilterTypeList()
            
            
       }
        private Domain.FolderSet _FolderSet;
        public Domain.FolderSet FolderSet{ get { return _FolderSet; } set { this.RaiseAndSetIfChanged(ref _FolderSet, value); } }
        //public ViewModels.Navigator Navigator { get; set; }
        public Reactive.EventAggregator EventManager { get; set; }

        public static ISearchRepository SearchRepo { get; set; }
        public IAnalyticRepository AnalyticRepo { get; set; }
        public IPricingRepository PricingRepo { get; set; }

        public Domain.Session Session { get; set; } //contains user history
        
        private ViewModelBase _SelectedSubViewModel;
        public ViewModelBase SelectedSubModuleViewModel { 
            get
            {
                return _SelectedSubViewModel;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedSubViewModel, value);

            }
        
        } //eg. 1) Home/SearchVM or 2) AnalyticEditVM 3) PricingEditVM  4) MySettings VM
                                                                  //Admin - 1) UsersVM 2) RulesVM etc..

        //public ViewModelBase SelectedPlanningViewModel { get; set; }
        //public ViewModelBase SelectedTrackingViewModel { get; set; }
        //public ViewModelBase SelectedAdminViewModel { get; set; } //preload


        //Screen sections
        public string MessageCenterViewModel { get; set; }
        public string StatusCenterViewModel { get; set; }


        //service references

        //Lookup lists?
        public List<Domain.Filter> SampleFilters { get; set; }
        //public List<Domain.Folder> SampleFolders { get; set; }

        //public List<Domain.SubModuleType> SubModuleKeys { get; set; }

        public void Seed()
        {
            Random r = new Random();

            //Mock lookups
            SampleFilters = new List<Domain.Filter>();
            //SampleFolders = new List<Domain.Folder>();

            for (int i = 0; i < 100; i++)
            {
                SampleFilters.Add(new Domain.Filter());
                //SampleFolders.Add(new Domain.Folder());
            }
            int j = 0;
            //SampleFilters.ForEach(x => x.Id = ++j);
            SampleFilters.ForEach(x => x.Code +=  ++j);

            //SampleFilters.ForEach(x => x.IsSelected = r.NextDouble() > 0.5);


            Session = new Domain.Session
            {
                User = new Domain.User { Login="dennism", FirstName = "Dennis", LastName="Mercado"},
                Analytics = new List<Domain.Analytic>
                {
                    new Domain.Analytic()
                    {
                        Id="1",
                        Name="Air & Fuel Delivery",
                        Description = "All Air & Fuel Delivery parts",
                        Status = "Active",
                        //Group1 = "Group!",
                        //Group2 = "Group2",
                        //Folder = "Folder1",
                        //Filters = SampleFilters.Where(x => x.IsSelected == true).ToList()
                    },
                    new Domain.Analytic()
                    {
                        Id="2",
                        Name="Bed Mats & Tonneau Covers",
                        Description = "All Mats and Covers",
                        Status = "Active"
                        //,Group1 = "Group1",
                        //Group2 = "Group2",
                        //Folder = "Folder1",
                        //,Filters = SampleFilters.Where(x => x.IsSelected == false).ToList()
                    },
                    new Domain.Analytic()
                    {
                        Id="3",
                        Name="Books",
                        Description = "All Books",
                        Status = "Active"
                        //,Group1 = "Group@",
                        //Group2 = "Group2"
                        //,Folder = "Folder1",
                        //,Filters = SampleFilters.Skip(r.Next()).Take(r.Next()).ToList()
                    },
                    new Domain.Analytic()
                    {
                        Id="4"
                        ,Name="Brake Systems"
                        ,Description = "All Brake Systems"
                        ,Status = "Active"
                        //,Group1 = "Group$"
                        //,Group2 = "Group2"
                        //,Folder = "Folder1"
                        //,Filters = SampleFilters.Skip(r.Next()).Take(r.Next()).ToList()
                    },
                    new Domain.Analytic()
                    {
                        Id="5",
                        Name="Bumpers & Hardware",
                        Description = "All Bumpers & Hardware",
                        Status = "Active"
                        //,Group1 = "Group$"
                        //,Group2 = "Group2"
                        //,Folder = "Folder1"
                        //,Filters = SampleFilters.Skip(r.Next()).Take(r.Next()).ToList()

                    },
                    new Domain.Analytic()
                    {
                        Id="6"
                        ,Name="Car Care & Paint"
                        ,Description = "All Car Care parts"
                        ,Status = "Active"
                        //,Group1 = "Group$"
                        //,Group2 = "Group2"
                        //,Folder = "Folder1"
                    },
                    new Domain.Analytic()
                    {
                        Id="7",
                        Name="Video & Software",
                        Description = "All Video & Software parts",
                        Status = "Active"
                        //,Group1 = "Group$"
                        //,Group2 = "Group2"
                        //,Folder = "Folder1"
                    },
                    new Domain.Analytic()
                    {
                        Id="8"
                        ,Name="Loyalty Points"                      
                        ,Description = "All Loyalty Points"
                        ,Status = "Active"
                        //,Group1 = "Group$"
                        //,Group2 = "Group2"
                        //,Folder = "Folder1"
                        //,Filters = SampleFilters.Skip(r.Next()).Take(r.Next()).ToList()
                    }
                }
                
            };


            
        }//original seed
    }
}





//TODO: Navigate using an action - must map to a viewmodel & populate recent activity?
//namespace Layout.Domain
//{

//    public class Folder
//    {
//        public string Name { get; set; }
//    }

//    public enum Module
//    {
//        Planning,
//        Tracking,
//        Reporting
//    }

//    public enum Sections
//    {
//        Analytics,
//        Everyday,
//        Promotions,
//        Kits,
//        MySettings,
//        Settings,
//        Rules,
//        PriceLists,

//    }

//    public enum AnalyticStepType
//    {
//        Identity,
//        Filters,
//        PriceLists,
//        ValueDrivers
//    }



//    public enum PricingStepType
//    {
//        Identity,
//        Filters,
//        PriceLists,
//        Rounding,
//        Strategy,
//        Results,
//        Forecasts,
//        Approval
//    }

//    public enum PricingType
//    {
//        Everyday,
//        Promotion,
//        Kits
//    }
//    public class Session
//    {
//        public User User { get; set; }        
//        public List<Analytic> Analytics { get; set; }
//        public List<PriceRoutine> PriceRoutines { get; set; }
//        public List<Tag> Tags { get; set; }


//    }

//    public class User
//    {
//        public string Login { get; set; }
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public Boolean IsAuthenticated { get; set; }
//        public Dictionary<Action, ViewModelBase> RecentHistory { get; set; }
//        //public List<Folder> Folders { get; set; } // Deprecated for tags defined on analytic
//        public class Role
//        {
//            List<Action> Permissions { get; set; } //which actions can they execute
//        }

//    }

//    public class Analytic
//    {
//        public string Id { get; set; }
//        public string Name { get; set; }
//        public string Status { get; set; }
//        public string Description { get; set; }
//        public string Comments { get; set; }
//        public string Group1 { get; set; }
//        public string Group2 { get; set; }
//        public string Folder { get; set; }
//        public bool Shared { get; set; }

//        public List<Filter> Filters { get; set; }
//        public List<ValueDriver> ValueDrivers { get; set; }
//        public List<PriceRoutine> RelatedPriceRoutines { get; set; }
//        public List<Action> Actions { get; set; } // TODO: seperate into own logic // usually navigates to step in module eg - for analytic->filters, price lists, rounding
//    }

//    public class PriceRoutine
//    {
//        public PriceRoutineType @Type { get; set; } //everyday, promo, kits
//        public List<string> AssignedAnalytics { get; set; } // list of ids
//        public List<Action> Actions { get; set; }
//        public List<RoundingScheme> RoundingSchemes { get; set; }
//        public List<PriceList> PriceLists { get; set; }
//    }


//    public class PriceList
//    {
        
//    }

//    public enum PricingMode
//    {
//        Single,
//        Cascade,
//        GlobalKey,
//        GlobalKeyPlus
//    }


//    public class RoundingScheme
//    {
//        public string Lower { get; set; }
//        public string Upper { get; set; }
//        public int RoundTo { get; set; }
//    }


//    public enum PriceRoutineType
//    {
//        EveryDay,
//        Promo,
//        Kit
//    }

//    public enum ModuleType
//    {
//        Planning,
//        Tracking,
//        Reporting,
//        Administration
//    }


//    public class ValueDriver
//    {
//        public int Group { get; set; }
//        public ValueDriverType @Type { get; set; }
//    }

//    public enum ValueDriverType
//    {
        
//    }

//    public class Filter
//    {
//        public int Id { get; set; }
//        public Boolean IsSelected { get; set; }
//        public string Code { get; set; }
//        public string Value { get; set; }
//    }

//    public class EntityBase
//    {

//    }
//}



                