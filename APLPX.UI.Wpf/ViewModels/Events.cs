using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using Domain = APLPX.Client.Entity;

namespace APLPX.UI.WPF.ViewModels
{
    namespace Events
    {
        public class NavigateEvent
        {
            public Domain.ModuleType Module { get; set; }
            public Domain.SubModuleType SubModule { get; set; }
            public Domain.SectionType Section { get; set; }
            public Object Entity { get; set; }
        }

        public class SaveEvent
        {
            public Domain.ModuleType Module { get; set; }
            public Domain.SubModuleType SubModule { get; set; }
            public Domain.SectionType Section { get; set; }
            public Object Entity { get; set; }
        }

        public class TagSearchEvent
        {
            public Domain.ModuleType Module { get; set; }
            public Domain.SubModuleType SubModule { get; set; }
            public Domain.SectionType Section { get; set; }
            public List<Domain.Tag> Tags { get; set; }
        }

        public class SelectionEvent
        {
            public Domain.SubModuleType EntityType { get; set; }
            public Object Entity { get; set; }
        }

        public class SectionSelectionEvent
        {
            public Domain.SectionType Section { get; set; }
        }

        public class DriverSelectedEvent
        {
            public Domain.Mode Mode { get; set; }
            public Domain.ValueDriverType DriverType { get; set; }
        }
    }

    namespace Reactive
    {

        public class EventAggregator : IEventAggregator
        {
            readonly ISubject<object> subject = new Subject<object>();

            public IObservable<TEvent> GetEvent<TEvent>()
            {
                return subject.OfType<TEvent>().AsObservable();
            }

            public void Publish<TEvent>(TEvent sampleEvent)
            {
                subject.OnNext(sampleEvent);
            }
        }

        public interface IEventAggregator
        {
            IObservable<TEvent> GetEvent<TEvent>();
            void Publish<TEvent>(TEvent sampleEvent);
        }
    }

    public class Navigator
    {
        //TODO: make this is own class so we can pass it to submodules or use event aggregator

        private static Dictionary<Domain.ModuleType, ViewModelBase> ModuleSearchCache = new Dictionary<Domain.ModuleType, ViewModelBase>();
        private static Dictionary<Domain.SubModuleType, List<ViewModelBase>> SubModuleCache = new Dictionary<Domain.SubModuleType, List<ViewModelBase>>();
        //private string EventAggregatorReceiver { get; set; } //receive navigation events from aggregator
        public ViewModelBase NavigateHomeByModuleType(Domain.ModuleType module)
        {
            //find HomeSearch for Planning Landing page only if one exists and reload
            if (ModuleSearchCache.ContainsKey(module))
            {
                return ModuleSearchCache[module];
            }
            return null;
        }
        public ViewModelBase NavigateSectionBySectionType(Domain.SubModuleType section)
        {
            //multiple analytic and pricing viewmodels can exist here TODO: determine cap to hold in cache
            if (SubModuleCache.ContainsKey(section))
            {
                var list = SubModuleCache[section];
                while (list.Count > 10)
                {
                    list.RemoveAt(0);
                }
                return list.Last();
                //TODO: dispose of any eventagg related items


            }
            return null; //return approp SectionViewModel
        }
        public void AddModule(Domain.ModuleType moduleType, ViewModelBase viewModel)
        {

        }

        public void AddNewSection(Domain.SectionType sectionType, ViewModelBase viewModel)
        {
            //only on selection of analytic or price routine -> edit link clicked
        }


    }
}
