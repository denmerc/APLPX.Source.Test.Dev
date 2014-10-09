using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using APLPX.UI.WPF.ViewModels.Events;
using APLPX.Server.Data;
using Domain = APLPX.Client.Entity;
using System.Windows;
using APLPX.UI.WPF.ViewModels.Reactive;


namespace APLPX.UI.WPF.ViewModels
{
    public class AnalyticModuleViewModel : ViewModelBase
    {
        /**
         * SelectedAnalytic - contains selected filters, pricelists, valuedrivers
         * SelectedAnalyticStepType - enum
         * static Dictionary of Steps / vm / selected
         * -OR-
         * List<Steps> and navigated vm's
         * SelectedStepViewModel to bind to content control with analytic state , actions
         * Services needed for analytic 
         * */

        EventAggregator EventManager = ((EventAggregator)App.Current.Resources["EventManager"]);

        private Dictionary<Domain.SectionType, ViewModelBase> StepCache = new Dictionary<Domain.SectionType, ViewModelBase>();
        //private static Domain.Analytic SelectedAnalytic { get; set; }

        private ViewModelBase _SelectedStepViewModel;
        public ViewModelBase SelectedStepViewModel
        {
            get { return _SelectedStepViewModel; }
            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedStepViewModel, value);
            }
        }


        public void Navigate(NavigateEvent navigator)
        {
            if (navigator.Entity != null) SelectedAnalytic = (Domain.Analytic)navigator.Entity;
            Name = SelectedAnalytic.Name;

            if (!StepCache.ContainsKey(navigator.Section))
            {
                switch (navigator.Section)// switch action bar for each step
                {
                    case Domain.SectionType.PlanningAnalyticsMyAnalytics:
                        break;
                    case Domain.SectionType.PlanningAnalyticsIdentity:
                        SelectedStepViewModel = new ViewModels.Analytic.IdentityViewModel(SelectedAnalytic);

                        break;
                    case Domain.SectionType.PlanningAnalyticsFilters:
                        SelectedStepViewModel = new ViewModels.Analytic.FilterViewModel(SelectedAnalytic);
                        break;
                    //case SectionType.PlanningAnalyticsPriceLists:
                    //    break;
                    case Domain.SectionType.PlanningAnalyticsValueDrivers:
                        SelectedStepViewModel = new ViewModels.Analytic.DriverViewModel(SelectedAnalytic);
                        break;
                    case Domain.SectionType.PlanningAnalyticsResults:
                        SelectedStepViewModel = new ViewModels.Analytic.ResultsViewModel(SelectedAnalytic);
                        break;
                    default:
                        break;
                }
                StepCache.Add(navigator.Section, _SelectedStepViewModel);
            }
            else // in cache
            {
                switch (navigator.Section)
                {
                    case Domain.SectionType.PlanningAnalyticsIdentity:
                        SelectedStepViewModel = ((Analytic.IdentityViewModel)StepCache[navigator.Section]);
                        //SelectedStepViewModel.Load(navigator.Entity);
                        break;
                    case Domain.SectionType.PlanningAnalyticsFilters:
                        SelectedStepViewModel = ((Analytic.FilterViewModel)StepCache[navigator.Section]);
                        break;
                    case Domain.SectionType.PlanningAnalyticsValueDrivers:
                        SelectedStepViewModel = ((Analytic.DriverViewModel)StepCache[navigator.Section]);
                        break;
                    case Domain.SectionType.PlanningAnalyticsResults:
                        SelectedStepViewModel = ((Analytic.ResultsViewModel)StepCache[navigator.Section]);
                        break;
                    default:
                        break;
                }
                SelectedStepViewModel.LoadAnalytic(SelectedAnalytic);

            }
        }

        public AnalyticModuleViewModel(IAnalyticRepository repo, Domain.Session session, string name)
        {
            Name = name;

            EventManager.GetEvent<SaveEvent>()
                .Subscribe(evt =>
                {
                    Domain.Analytic analyticToSave = null;
                    //ViewModelBase vm = null;
                    switch (evt.Section)
                    {
                        case Domain.SectionType.PlanningHomeMyMarkuprules:
                            break;
                        case Domain.SectionType.PlanningHomeMyRoundingrules:
                            break;
                        case Domain.SectionType.PlanningAnalyticsMyAnalytics:
                            break;
                        case Domain.SectionType.PlanningAnalyticsIdentity:
                            var vmi = (ViewModels.Analytic.IdentityViewModel)(SelectedStepViewModel);
                            var tagsToSave = vmi.SelectedTags;
                            analyticToSave = vmi.SelectedAnalytic;
                            vmi.SelectedAnalytic.Tags = tagsToSave.Select(y => y.Value).ToList<string>();
                            repo.Save<Domain.Analytic>(analyticToSave);
                            this.Navigate(new NavigateEvent { Module = Domain.ModuleType.Planning, SubModule = Domain.SubModuleType.Analytics, Section = Domain.SectionType.PlanningAnalyticsFilters });
                            break;
                        case Domain.SectionType.PlanningAnalyticsFilters:
                            var vmf = (ViewModels.Analytic.FilterViewModel)(SelectedStepViewModel);
                            analyticToSave = vmf.SelectedAnalytic;



                            repo.Save<Domain.Analytic>(analyticToSave);
                            this.Navigate(new NavigateEvent { Module = Domain.ModuleType.Planning, SubModule = Domain.SubModuleType.Analytics, Section = Domain.SectionType.PlanningAnalyticsFilters });
                            break;
                        case Domain.SectionType.PlanningAnalyticsPriceLists:
                            break;
                        case Domain.SectionType.PlanningAnalyticsValueDrivers:
                            break;
                        case Domain.SectionType.PlanningAnalyticsResults:
                            break;
                        default:
                            break;
                    }
                });
        }

        public string Name { get; set; }
        void Save() { }
    }

}
