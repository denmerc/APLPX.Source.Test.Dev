using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APLPX.UI.WPF.ViewModels.Reactive;
using Domain = APLPX.Client.Display;

namespace APLPX.UI.WPF.ViewModels.Analytic
{
    public class IdentityViewModel : ViewModelBase
    {
        public IdentityViewModel(object entity)
        {
            SelectedAnalytic = (Domain.Analytic)entity;
            //Tags2 = SelectedAnalytic.Tags;
            Tags = new ReactiveList<Domain.Tag>(){
                new Domain.Tag{Value="tag-ut"}
            };

            this.WhenAny(x => x.SelectedAnalytic, x => x).Subscribe( a => {
                TagsToSuggest = ((HomeSearchViewModel)MainViewModel.SubModuleCache[Domain.SubModuleType.Search]).Tags.Select(t => new Domain.Tag { Value = t.ToString() }).ToList();
                    //TagsToSuggest = SelectedAnalytic.Tags.Select(t => new Domain.Tag { Value = t.ToString() }).ToList();
                    SelectedTags.Clear();
                    SelectedTags.AddRange( SelectedAnalytic.Tags.Select(t => new Domain.Tag { Value = t.ToString() })); 
                });
            //TagsToSuggest = SelectedAnalytic.Tags.Select(t => new Domain.Tag { Value = t.ToString() }).ToList();
            //SelectedTags.AddRange( SelectedAnalytic.Tags.Select(t => new Domain.Tag { Value = t.ToString() })); 
            //SelectedTags = SelectedAnalytic.Tags.Select(t => new Domain.Tag { Value = t.ToString() }).ToList();

        }

        private ReactiveList<Domain.Tag> _SelectedTags = new ReactiveList<Domain.Tag>();
        public ReactiveList<Domain.Tag> SelectedTags { get { return _SelectedTags; } set { this.RaiseAndSetIfChanged(ref _SelectedTags, value); } }
        private ReactiveList<Domain.Tag> _Tags;
        public ReactiveList<Domain.Tag> Tags { get { return _Tags; } set { this.RaiseAndSetIfChanged(ref _Tags, value); } }
        private List<Domain.Tag> _TagsToSuggest;
        public List<Domain.Tag> TagsToSuggest { get { return _TagsToSuggest; } set { this.RaiseAndSetIfChanged(ref _TagsToSuggest, value); } }

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

        public FilterViewModel(object entity)
        {
            SelectedAnalytic = (Domain.Analytic)entity;

            //List of filter types
            FilterTypes = Enum.GetValues(typeof(Domain.FilterType)).Cast<Domain.FilterType>().ToList();
            //FilterTypes = SelectedAnalytic.Filters.Select(x => x.Type).Distinct().ToList();

            //selected filter type default it on first load
            SelectedType = Domain.FilterType.VendorCode;

            //filtered list of filter items based on type

            EventManager.GetEvent<Domain.FilterType>()
                .Subscribe(ftype =>
                {
                    var ttype = string.Empty;
                    switch (ftype)
                    {
                        case Domain.FilterType.VendorCode:
                            ttype = "VendorCode";
                            break;
                        case Domain.FilterType.IsKit:
                            ttype = "IsKit";
                            break;
                        case Domain.FilterType.OnSale:
                            ttype = "OnSale";
                            break;
                        case Domain.FilterType.Category:
                            ttype = "Category";
                            break;
                        case Domain.FilterType.DiscountType:
                            ttype = "DiscountType";
                            break;
                        case Domain.FilterType.StatusType:
                            ttype = "StatusType";
                            break;
                        case Domain.FilterType.ProductType:
                            ttype = "ProductType";
                            break;
                        case Domain.FilterType.StockClass:
                            ttype = "StockClass";
                            break;
                        default:
                            break;
                    }
                    //update selected analytic with changes for that type
                    //var filterItems = SelectedAnalytic.Filters
                    //    .Where(x => x.Type == ttype)
                    //    .SelectMany(y => y.Items).ToList();
                    //filterItems = FilterItems;



                    var type = Enum.GetName(typeof(Domain.FilterType), ftype);
                    var filterItems = SelectedAnalytic.Filters.Where(fs => fs.Type == type)
                        .SelectMany(x => x.Items.Where(t => t.Type == SelectedType)).ToList();
                    //FilterItems.SuppressChangeNotifications();
                    FilterItems.Clear();
                    for (int i = 0; i < filterItems.Count; i++)
                    {
                        FilterItems.Add(filterItems[i]);
                        
                    }

                    var selectedObservable = this.WhenAnyObservable(x => x.FilterItems.ItemChanged)
                         .Where(x => x.PropertyName == "IsSelected");
                         //.Select(_ => FilterItems.Any(x => x.IsSelected));

                    selectedObservable.Subscribe(x =>
                    {
                        Console.WriteLine("test");
                    });
                        //.ToProperty(this, x => x.SelectedFilterItems)


                    SelectedFilterItems = FilterItems.CreateDerivedCollection(i => i, x => x.IsSelected);
                        //.Select(f => new FilterItemViewModel { Code = f.Code, Description = f.Description, IsSelected = f.IsSelected}).ToList();

                    //selectedItems.Changed.Subscribe(x => {
                    //    Console.WriteLine("test");
                    //});

                    this.WhenAnyValue(vm => vm.SelectedFilterItems).Subscribe(selected => {
                        Console.WriteLine("test");    
                    });

                    this.WhenAnyObservable(vm => vm.SelectedFilterItems.ItemChanged).Subscribe(selected =>
                    {
                        Console.WriteLine("test");
                    });

                    FilterItems.ItemChanged.Subscribe(x => {
                        Console.WriteLine("t");
                    });
                   
                });

            
        }

        //public void Load(object entity)
        //{
        //    SelectedAnalytic = (Domain.Analytic)entity;
        //}
        
        //private Domain.Analytic _SelectedAnalytic;
        //public Domain.Analytic SelectedAnalytic
        //{
        //    get { return _SelectedAnalytic; }
        //    set { this.RaiseAndSetIfChanged(ref _SelectedAnalytic, value); }
        //}

        private ReactiveList<Domain.Filter> _FilterItems = new ReactiveList<Domain.Filter>() { ChangeTrackingEnabled = true };
        public ReactiveList<Domain.Filter> FilterItems { get { return _FilterItems; } set { this.RaiseAndSetIfChanged(ref _FilterItems, value); } }

        public IReactiveDerivedList<Domain.Filter> SelectedFilterItems { get; set; }

        private Domain.FilterType _SelectedType;
        public Domain.FilterType SelectedType { get { return _SelectedType; } set { this.RaiseAndSetIfChanged(ref _SelectedType, value); } }

        private List<Domain.FilterType> _FilterTypes;
        public List<Domain.FilterType> FilterTypes { get { return _FilterTypes; } set { this.RaiseAndSetIfChanged(ref _FilterTypes, value); } }
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

    class DriverViewModel : ViewModelBase
    {

        APLPX.UI.WPF.ViewModels.Reactive.EventAggregator EventManager = ((APLPX.UI.WPF.ViewModels.Reactive.EventAggregator)App.Current.Resources["EventManager"]);
        public DriverViewModel(object entity)
        {
            SelectedAnalytic = (Domain.Analytic)entity;
            Groups = SelectedAnalytic.Drivers.SelectMany(d => d.Groups).ToList();
            ModeTypes = Enum.GetValues(typeof(Domain.Mode)).Cast<Domain.Mode>().ToList();
            DriverTypes = Enum.GetValues(typeof(Domain.ValueDriverType)).Cast<Domain.ValueDriverType>().ToList();
            SelectedDriverType = Domain.ValueDriverType.Markup; 
            SelectedMode = Domain.Mode.Auto;
            EventManager.GetEvent<ViewModels.Events.DriverSelectedEvent>()
                .Subscribe(evt =>
                {
                    SelectedDriverType = evt.DriverType;
                    SelectedMode = evt.Mode;
                    Groups = SelectedAnalytic.Drivers
                        .Where(d => d.Type == SelectedDriverType && d.Mode == SelectedMode)
                        .SelectMany(set => set.Groups).ToList();
                    this.RaisePropertyChanged("Groups");
                });

        }

        public List<Domain.Mode> ModeTypes { get; set; }
        public List<Domain.ValueDriverType> DriverTypes { get; set; }

        private List<Domain.Group> _Groups;
        public List<Domain.Group> Groups { get { return _Groups; } set { this.RaiseAndSetIfChanged(ref _Groups, value); } }
        public Domain.ValueDriverType SelectedDriverType { get; set; }
        private Domain.Mode _SelectedMode;
        public Domain.Mode SelectedMode { get { return _SelectedMode; } set { this.RaiseAndSetIfChanged(ref _SelectedMode, value); } }
    }

    public class ResultsViewModel : ViewModelBase
    {
        public ResultsViewModel(object entity)
        {
            SelectedAnalytic = (Domain.Analytic)entity;
        }


    }
}
