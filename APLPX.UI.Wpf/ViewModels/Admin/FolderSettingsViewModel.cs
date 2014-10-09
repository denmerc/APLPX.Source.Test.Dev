using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive;
using Domain = APLPX.Client.Entity;

namespace APLPX.UI.WPF.ViewModels
{
    public class FolderSettingsViewModel : ViewModelBase
    {
        APLPX.UI.WPF.ViewModels.Reactive.EventAggregator Publisher = ((APLPX.UI.WPF.ViewModels.Reactive.EventAggregator)App.Current.Resources["EventManager"]);
        public FolderSettingsViewModel()
        {
            Reload(Domain.SubModuleType.Analytics);
            SubModuleKeys = Enum.GetValues(typeof(Domain.SubModuleType)).Cast<Domain.SubModuleType>().ToList();

            var selectedObservable = this.WhenAnyObservable(x => x.SelectedFolderList.ItemChanged)
                .Where(x => x.PropertyName == "IsSelected")
            //.Select(_ => FilterItems.Any(x => x.IsSelected));

            .Subscribe(x =>
            {
                    switch (SelectedModuleIndex) //TODO:
	                {
                        case 0:
                            if (x.Sender.IsSelected)
	                        {
                                AnalyticFolderSet.Add(x.Sender.Name);
                                //MasterFolderSet.SelectedAnalyticFolders.Add(x.Sender.Name);
	                        }
                            else
                            {
                                AnalyticFolderSet.Remove(x.Sender.Name);
                                //MasterFolderSet.SelectedAnalyticFolders.Remove(x.Sender.Name);
                            }
                            break;
                        case 1:
                            if (x.Sender.IsSelected)
                            {
                                EverydayFolderSet.Add(x.Sender.Name);
                            }
                            else
                            {
                                EverydayFolderSet.Remove(x.Sender.Name);
                            }
                            break;

                        case 2:
                            if (x.Sender.IsSelected)
                            {
                                PromoFolderSet.Add(x.Sender.Name);
                            }
                            else
                            {
                                PromoFolderSet.Remove(x.Sender.Name);
                            }
                            break;

                        case 3:
                            if (x.Sender.IsSelected)
                            {
                                KitFolderSet.Add(x.Sender.Name);
                            }
                            else
                            {
                                KitFolderSet.Remove(x.Sender.Name);
                            }
                            break;


                      
	                }
                
                //Publisher.Publish<FoldersSelectedVM>(x.Sender);
                
                //Console.WriteLine("test");

            });

            var selectedO = this.WhenAnyValue(x => x.SelectedFolderList.ItemChanged);
            //.Where(x => x.PropertyName == "IsSelected");
            //.Select(_ => FilterItems.Any(x => x.IsSelected));

            selectedO.Subscribe(x =>
            {
                Console.WriteLine("test");
            });


            SelectedFolderList.ItemChanged.Subscribe(x =>
            {
                Console.WriteLine("t");
                
            });


            //var master =((HomeSearchViewModel)MainViewModel.SubModuleCache[Domain.SubModuleType.Search]).FolderSet;


            //SelectedAnalyticFolders = master.SelectedAnalyticFolders.Select(x => new FoldersSelectedVM { Name = x, IsSelected = true }).ToList(); 
            //MasterAnalyticFoldersVM = master.MasterAnalyticFolderList.Select( x=> new FoldersSelectedVM {Name = x, IsSelected = false}).ToList();

            //var unique = SelectedAnalyticFolders
            //    .Concat(MasterAnalyticFoldersVM)
            //    .GroupBy(item => item.Name)
            //    .Select(group => group.First()).ToList();

            //MasterEveryDayFolders = master.MasterEverydayFolderList;
            //MasterKitFolders = master.MasterKitFolderList;
            //MasterPromotionFolders = master.MasterPromotionFolderList;

            Publisher.GetEvent<Views.FolderModuleSelectedEvent>()
                .Subscribe(evt =>
                {
                    switch (evt.SelectedIndex)
                    {
                        case 0:

                            Reload(Domain.SubModuleType.Analytics);
                            break;
                        case 1:
                            Reload(Domain.SubModuleType.Everyday);
                            break;
                        case 2:
                            Reload(Domain.SubModuleType.Promotions);
                            break;
                        case 3:
                            Reload(Domain.SubModuleType.Kits);
                            break;
                        default:
                            break;
                    }

                });
        }

        public void Reload(Domain.SubModuleType submodule)
        {
            try
            {

                //save to in memory if folderList != null until flushed to db when user clicks save link
                var master = ((HomeSearchViewModel)MainViewModel.SubModuleCache[Domain.SubModuleType.Search]);

                //MasterFolderSet = ((HomeSearchViewModel)MainViewModel.SubModuleCache[Domain.SubModuleType.Search]).FolderSet;
                AnalyticFolderSet = master.FolderSet.SelectedAnalyticFolders.ToList();
                MasterAnalyticFolderSet = master.FolderSet.MasterAnalyticFolderList.ToList();

                EverydayFolderSet = master.FolderSet.SelectedEverydayFolders.ToList();
                MasterEverydayFolderSet = master.FolderSet.MasterEverydayFolderList.ToList();

                PromoFolderSet = master.FolderSet.SelectedPromotionFolders.ToList();
                MasterPromoFolderSet = master.FolderSet.MasterPromotionFolderList.ToList();

                KitFolderSet = master.FolderSet.SelectedKitFolders.ToList();
                MasterKitFolderSet = master.FolderSet.MasterKitFolderList.ToList();

                List<FoldersSelectedVM> selectedVMs = null; List<FoldersSelectedVM> masterVMs = null; 
                switch (submodule)
                {
                    case Domain.SubModuleType.Analytics:

                        selectedVMs = AnalyticFolderSet.Select(x => new FoldersSelectedVM { Name = x, IsSelected = true }).ToList();
                        masterVMs = MasterAnalyticFolderSet.Select(x => new FoldersSelectedVM { Name = x, IsSelected = false }).ToList();
                        //masterVMs = MasterFolderSet.MasterAnalyticFolderList.Select(x => new FoldersSelectedVM { Name = x, IsSelected = false }).ToList();
                        Title = "Folders";
                        SelectedModuleIndex = 0;
                        break;
                    case Domain.SubModuleType.Everyday:
                        selectedVMs = EverydayFolderSet.Select(x => new FoldersSelectedVM { Name = x, IsSelected = true }).ToList();
                        masterVMs = MasterEverydayFolderSet.Select(x => new FoldersSelectedVM { Name = x, IsSelected = false }).ToList();
                        Title = "Folders";
                        SelectedModuleIndex = 1;
                        break;
                    case Domain.SubModuleType.Promotions:
                        selectedVMs = PromoFolderSet.Select(x => new FoldersSelectedVM { Name = x, IsSelected = true }).ToList();
                        masterVMs = MasterPromoFolderSet.Select(x => new FoldersSelectedVM { Name = x, IsSelected = false }).ToList();
                        Title = "Folders";
                        SelectedModuleIndex = 2;break;
                    case Domain.SubModuleType.Kits:
                        selectedVMs = KitFolderSet.Select(x => new FoldersSelectedVM { Name = x, IsSelected = true }).ToList();
                        masterVMs = MasterKitFolderSet.Select(x => new FoldersSelectedVM { Name = x, IsSelected = false }).ToList();
                        Title = "Folders";                  
                        SelectedModuleIndex = 3;
                        break;

                    default:
                        break;
                }

                var folderList = selectedVMs
                            .Concat(masterVMs)
                            .GroupBy(item => item.Name)
                            .Select(group => group.First()).ToList() ;

                SelectedFolderList.Clear();
                foreach (var item in folderList)
                {
                    SelectedFolderList.Add(item);
                }

                //SelectedFolderList.AddRange(folderList);
            }
            catch (Exception)
            {
                
                //throw;
            }
        }

        private List<Domain.SubModuleType> _subModuleKeys;
        public List<Domain.SubModuleType> SubModuleKeys { get { return _subModuleKeys; } set { this.RaiseAndSetIfChanged(ref _subModuleKeys, value); } }


        private List<string> _kitFolderSet;
        public List<string> KitFolderSet { get { return _kitFolderSet; } set { this.RaiseAndSetIfChanged(ref _kitFolderSet, value); } }
        private List<string> _masterKitFolderSet;
        public List<string> MasterKitFolderSet { get { return _masterKitFolderSet; } set { this.RaiseAndSetIfChanged(ref _masterKitFolderSet, value); } }

        private List<string> _promoFolderSet;
        public List<string> PromoFolderSet { get { return _promoFolderSet; } set { this.RaiseAndSetIfChanged(ref _promoFolderSet, value); } }
        private List<string> _masterPromoFolderSet;
        public List<string> MasterPromoFolderSet { get { return _masterPromoFolderSet; } set { this.RaiseAndSetIfChanged(ref _masterPromoFolderSet, value); } }

        private List<string> _everydayFolderSet;
        public List<string> EverydayFolderSet { get { return _everydayFolderSet; } set { this.RaiseAndSetIfChanged(ref _everydayFolderSet, value); } }
        private List<string> _masterEverydayFolderSet;
        public List<string> MasterEverydayFolderSet { get { return _masterEverydayFolderSet; } set { this.RaiseAndSetIfChanged(ref _masterEverydayFolderSet, value); } }

        private List<string> _analyticFolderSet;
        public List<string> AnalyticFolderSet { get { return _analyticFolderSet; } set { this.RaiseAndSetIfChanged(ref _analyticFolderSet, value); } }
        private List<string> _masterAnalyticFolderSet;
        public List<string> MasterAnalyticFolderSet { get { return _masterAnalyticFolderSet; } set { this.RaiseAndSetIfChanged(ref _masterAnalyticFolderSet, value); } }

        //private Domain.FolderSet _masterFolderSet;
        //public Domain.FolderSet MasterFolderSet { get { return _masterFolderSet; } set { this.RaiseAndSetIfChanged(ref _masterFolderSet, value); } }


        private int _SelectedModuleIndex;
        public int SelectedModuleIndex { get { return _SelectedModuleIndex; } set { this.RaiseAndSetIfChanged(ref _SelectedModuleIndex, value); } }

        private string _title;
        public string Title { get { return _title; } set { this.RaiseAndSetIfChanged(ref _title, value); } }

        private ReactiveList<FoldersSelectedVM> _FolderList = new ReactiveList<FoldersSelectedVM>(){ChangeTrackingEnabled = true};
        public ReactiveList<FoldersSelectedVM> SelectedFolderList { get { return _FolderList; } set { this.RaiseAndSetIfChanged(ref _FolderList, value); } }

    }   

    public class FoldersSelectedVM : ReactiveObject
    {

        public string Name { get; set; }
        private Boolean _isSelected;
        public Boolean IsSelected { get { return _isSelected; } set { this.RaiseAndSetIfChanged(ref _isSelected, value); } }
    }
}


