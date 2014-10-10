using System;
using System.Collections.Generic;
using ReactiveUI;
using APLPX.UI.WPF.ViewModels.Events;
using APLPX.Server.Data;
using Domain = APLPX.Client.Display;
using System.Windows;


namespace APLPX.UI.WPF.ViewModels
{

    public class AdminModuleViewModel : ViewModelBase
    {
        APLPX.UI.WPF.ViewModels.Reactive.EventAggregator EventManager = ((APLPX.UI.WPF.ViewModels.Reactive.EventAggregator)App.Current.Resources["EventManager"]);

        private Dictionary<Domain.SectionType, ViewModelBase> SectionCache = new Dictionary<Domain.SectionType, ViewModelBase>();
        //private static Domain.Analytic SelectedAnalytic { get; set; }
        private MainViewModel _parent;
        private ISearchRepository _repo;
        public AdminModuleViewModel()
        {

            _repo = MainViewModel.SearchRepo;

            EventManager.GetEvent<SectionSelectionEvent>()
                .Subscribe(evt =>
                {
                    this.Navigate(evt.Section);

                });
            //EventManager.GetEvent<FoldersSelectedVM>()
            //    .Subscribe(vm =>
            //    { 

            //    });

            EventManager.GetEvent<FolderSettingsUpdatedEvent>()
                .Subscribe(evt =>
                {
                    var vm = ((FolderSettingsViewModel)(SelectedSectionViewModel));
                    try
                    {
                        var folderSet = new Domain.FolderSet
                        {
                            SelectedAnalyticFolders = vm.AnalyticFolderSet,
                            SelectedEverydayFolders = vm.EverydayFolderSet,
                            SelectedPromotionFolders = vm.PromoFolderSet,
                            SelectedKitFolders = vm.KitFolderSet
                        };
                        _repo.SaveFolders(folderSet);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    //if saved update in memory so search screen is updated
                    var mainFolderSet = ((HomeSearchViewModel)MainViewModel.SubModuleCache[Domain.SubModuleType.Search]);

                    mainFolderSet.FolderSet.SelectedAnalyticFolders = vm.AnalyticFolderSet;
                    mainFolderSet.FolderSet.SelectedEverydayFolders = vm.EverydayFolderSet;
                    mainFolderSet.FolderSet.SelectedPromotionFolders = vm.PromoFolderSet;
                    mainFolderSet.FolderSet.SelectedKitFolders = vm.KitFolderSet;

                    EventManager.Publish<NavigateEvent>(
                        new NavigateEvent
                        {
                            Module = Domain.ModuleType.Planning,
                            SubModule = Domain.SubModuleType.Search,
                            Section = Domain.SectionType.PlanningHomeMyHomePage

                        });

                });

        }


        private ViewModelBase _SelectedSectionViewModel;
        public ViewModelBase SelectedSectionViewModel
        {
            get { return _SelectedSectionViewModel; }
            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedSectionViewModel, value);
            }
        }
        
        
        private int _SelectedSectionIndex=100;
        public int SelectedSectionIndex
        {
            get { return _SelectedSectionIndex; }
            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedSectionIndex, value);
            }
        }


        public void Navigate(Domain.SectionType section)
        {
            if (!SectionCache.ContainsKey(section))
            {
                switch (section)
                {

                    case Domain.SectionType.AdministrationFolders:
                        SelectedSectionViewModel = new FolderSettingsViewModel();
                        SelectedSectionIndex = 7;
                        break;
                    case Domain.SectionType.AdministrationUserMaintenance:

                        break;
                    case Domain.SectionType.AdministrationPricelists:
                        break;
                    case Domain.SectionType.AdministrationOptimization:
                        break;
                    case Domain.SectionType.AdministrationMarkuprules:
                        break;
                    case Domain.SectionType.AdministrationRoundingrules:
                        break;
                    case Domain.SectionType.AdministrationRollback:
                        break;
                    case Domain.SectionType.AdministrationProcesses:
                        break;
                }
                SectionCache.Add(section, SelectedSectionViewModel);
            }
            else //in cache
            {
                switch (section)
                {

                    case Domain.SectionType.AdministrationFolders:
                        SelectedSectionViewModel = ((FolderSettingsViewModel)SectionCache[section]);

                        break;
                    case Domain.SectionType.AdministrationUserMaintenance:

                        break;
                    case Domain.SectionType.AdministrationPricelists:
                        break;
                    case Domain.SectionType.AdministrationOptimization:
                        break;
                    case Domain.SectionType.AdministrationMarkuprules:
                        break;
                    case Domain.SectionType.AdministrationRoundingrules:
                        break;
                    case Domain.SectionType.AdministrationRollback:
                        break;
                    case Domain.SectionType.AdministrationProcesses:
                        break;
                }
            }
        }

    }
}
