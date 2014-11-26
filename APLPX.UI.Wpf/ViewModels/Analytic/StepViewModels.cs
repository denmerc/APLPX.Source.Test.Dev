using System;
using System.Collections.Generic;
using System.Linq;
using APLPX.UI.WPF.Interfaces;
using APLPX.UI.WPF.ViewModels.Reactive;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;

namespace APLPX.UI.WPF.ViewModels
{
    public class IdentityViewModel : ViewModelBase
    {
        private Display.Analytic _analytic;

        public Display.Analytic Analytic
        {
            get { return _analytic; }
            private set
            {
                _analytic = value;
            }

        }
        public IdentityViewModel(ISearchableEntity entity)
        {
            Analytic = entity as Display.Analytic;

            //SelectedAnalytic = (Domain.Analytic)entity;
            ////Tags2 = SelectedAnalytic.Tags;
            //Tags = new ReactiveList<Domain.Tag>(){
            //    new Domain.Tag{Value="tag-ut"}
            //};

            //this.WhenAny(x => x.SelectedAnalytic, x => x).Subscribe( a => {
            //    TagsToSuggest = ((HomeSearchViewModel)MainViewModel.SubModuleCache[Domain.SubModuleType.Search]).Tags.Select(t => new Domain.Tag { Value = t.ToString() }).ToList();
            //        //TagsToSuggest = SelectedAnalytic.Tags.Select(t => new Domain.Tag { Value = t.ToString() }).ToList();
            //        SelectedTags.Clear();
            //        SelectedTags.AddRange( SelectedAnalytic.Tags.Select(t => new Domain.Tag { Value = t.ToString() })); 
            //    });

        }

        //private ReactiveList<Domain.Tag> _SelectedTags = new ReactiveList<Domain.Tag>();
        //public ReactiveList<Domain.Tag> SelectedTags { get { return _SelectedTags; } set { this.RaiseAndSetIfChanged(ref _SelectedTags, value); } }
        //private ReactiveList<Domain.Tag> _Tags;
        //public ReactiveList<Domain.Tag> Tags { get { return _Tags; } set { this.RaiseAndSetIfChanged(ref _Tags, value); } }
        //private List<Domain.Tag> _TagsToSuggest;
        //public List<Domain.Tag> TagsToSuggest { get { return _TagsToSuggest; } set { this.RaiseAndSetIfChanged(ref _TagsToSuggest, value); } }

        //Selected Domain.Analytic
        //private Domain.Analytic _SelectedAnalytic;
        //public Domain.Analytic SelectedAnalytic
        //{
        //    get { return _SelectedAnalytic; }
        //    set { this.RaiseAndSetIfChanged(ref _SelectedAnalytic, value); }
        //}

        //public void Load(object entity)
        //{
        //    SelectedAnalytic = (Domain.Analytic)entity;
        //}
    }

    public class FilterViewModel : ViewModelBase
    {
        EventAggregator EventManager = ((EventAggregator)App.Current.Resources["EventManager"]);

        private ISearchableEntity _entity;
        public FilterViewModel(ISearchableEntity entity)
        {
            _entity = entity;
            //SelectedAnalytic = (Domain.Analytic)entity;

            ////List of filter types
            //FilterTypes = Enum.GetValues(typeof(Domain.FilterType)).Cast<Domain.FilterType>().ToList();
            ////FilterTypes = SelectedAnalytic.Filters.Select(x => x.Type).Distinct().ToList();

            ////selected filter type default it on first load
            //SelectedType = Domain.FilterType.VendorCode;

            ////filtered list of filter items based on type

            //EventManager.GetEvent<Domain.FilterType>()
            //    .Subscribe(ftype =>
            //    {
            //        var ttype = string.Empty;
            //        switch (ftype)
            //        {
            //            case Domain.FilterType.VendorCode:
            //                ttype = "VendorCode";
            //                break;
            //            case Domain.FilterType.IsKit:
            //                ttype = "IsKit";
            //                break;
            //            case Domain.FilterType.OnSale:
            //                ttype = "OnSale";
            //                break;
            //            case Domain.FilterType.Category:
            //                ttype = "Category";
            //                break;
            //            case Domain.FilterType.DiscountType:
            //                ttype = "DiscountType";
            //                break;
            //            case Domain.FilterType.StatusType:
            //                ttype = "StatusType";
            //                break;
            //            case Domain.FilterType.ProductType:
            //                ttype = "ProductType";
            //                break;
            //            case Domain.FilterType.StockClass:
            //                ttype = "StockClass";
            //                break;
            //            default:
            //                break;
            //        }
            //        //update selected analytic with changes for that type
            //        //var filterItems = SelectedAnalytic.Filters
            //        //    .Where(x => x.Type == ttype)
            //        //    .SelectMany(y => y.Items).ToList();
            //        //filterItems = FilterItems;



            //        var type = Enum.GetName(typeof(Domain.FilterType), ftype);
            //        var filterItems = SelectedAnalytic.Filters.Where(fs => fs.Type == type)
            //            .SelectMany(x => x.Items.Where(t => t.Type == SelectedType)).ToList();
            //        //FilterItems.SuppressChangeNotifications();
            //        FilterItems.Clear();
            //        for (int i = 0; i < filterItems.Count; i++)
            //        {
            //            FilterItems.Add(filterItems[i]);

            //        }

            //        var selectedObservable = this.WhenAnyObservable(x => x.FilterItems.ItemChanged)
            //             .Where(x => x.PropertyName == "IsSelected");
            //             //.Select(_ => FilterItems.Any(x => x.IsSelected));

            //        selectedObservable.Subscribe(x =>
            //        {
            //            Console.WriteLine("test");
            //        });
            //            //.ToProperty(this, x => x.SelectedFilterItems)


            //        SelectedFilterItems = FilterItems.CreateDerivedCollection(i => i, x => x.IsSelected);
            //            //.Select(f => new FilterItemViewModel { Code = f.Code, Description = f.Description, IsSelected = f.IsSelected}).ToList();

            //        //selectedItems.Changed.Subscribe(x => {
            //        //    Console.WriteLine("test");
            //        //});

            //        this.WhenAnyValue(vm => vm.SelectedFilterItems).Subscribe(selected => {
            //            Console.WriteLine("test");    
            //        });

            //        this.WhenAnyObservable(vm => vm.SelectedFilterItems.ItemChanged).Subscribe(selected =>
            //        {
            //            Console.WriteLine("test");
            //        });

            //        FilterItems.ItemChanged.Subscribe(x => {
            //            Console.WriteLine("t");
            //        });

            //    });


        }


        //public void Load(object entity)
        //{
        //    SelectedAnalytic = (Domain.Analytic)entity;
        //}

        //private Domain.Analytic _SelectedAnalytic;
        public ISearchableEntity Entity
        {
            get { return _entity; }
            set { this.RaiseAndSetIfChanged(ref _entity, value); }
        }

        //private ReactiveList<Domain.Filter> _FilterItems = new ReactiveList<Domain.Filter>() { ChangeTrackingEnabled = true };
        //public ReactiveList<Domain.Filter> FilterItems { get { return _FilterItems; } set { this.RaiseAndSetIfChanged(ref _FilterItems, value); } }

        //public IReactiveDerivedList<Domain.Filter> SelectedFilterItems { get; set; }

        //private Domain.FilterType _SelectedType;
        //public Domain.FilterType SelectedType { get { return _SelectedType; } set { this.RaiseAndSetIfChanged(ref _SelectedType, value); } }

        //private List<Domain.FilterType> _FilterTypes;
        //public List<Domain.FilterType> FilterTypes { get { return _FilterTypes; } set { this.RaiseAndSetIfChanged(ref _FilterTypes, value); } }
    }

    class FilterItemViewModel : ViewModelBase
    {
        private Boolean _IsSelected = false;
        public Boolean IsSelected { get { return _IsSelected; } set { this.RaiseAndSetIfChanged(ref _IsSelected, value); } }

        private string _Code;
        public string Code { get { return _Code; } set { this.RaiseAndSetIfChanged(ref _Code, value); } }

        private string _Description;
        public string Description { get { return _Description; } set { this.RaiseAndSetIfChanged(ref _Description, value); } }
    }

    public class DriverViewModel : ViewModelBase
    {
        private List<Display.AnalyticDriver> _drivers;

        //APLPX.UI.WPF.ViewModels.Reactive.EventAggregator EventManager = ((APLPX.UI.WPF.ViewModels.Reactive.EventAggregator)App.Current.Resources["EventManager"]);
        public DriverViewModel(List<Display.AnalyticDriver> drivers)
        {
            Drivers = drivers;
        }

        public List<Display.AnalyticDriver> Drivers
        {
            get { return _drivers; }
            private set { this.RaiseAndSetIfChanged(ref _drivers, value); }
        }

    }

}
