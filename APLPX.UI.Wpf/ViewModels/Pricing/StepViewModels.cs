using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using APLPX.UI.WPF.ViewModels.Reactive;
using Domain = APLPX.Client.Entity;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class IdentityViewModel : ViewModelBase
    {
        public IdentityViewModel(object entity)
        {
            SelectedPriceRoutine = (Domain.PriceRoutine)entity;

            Tags = new ReactiveList<Domain.Tag>();

            this.WhenAny(x => x.SelectedPriceRoutine, x => x).Subscribe(a =>
            {
                TagsToSuggest = ((HomeSearchViewModel)MainViewModel.SubModuleCache[Domain.SubModuleType.Search]).Tags.Select(t => new Domain.Tag { Value = t.ToString() }).ToList();
                
                SelectedTags.Clear();
                SelectedTags.AddRange(SelectedPriceRoutine.Tags.Select(t => new Domain.Tag { Value = t.ToString() }));
            });

        }

        private ReactiveList<Domain.Tag> _SelectedTags = new ReactiveList<Domain.Tag>();
        public ReactiveList<Domain.Tag> SelectedTags { get { return _SelectedTags; } set { this.RaiseAndSetIfChanged(ref _SelectedTags, value); } }
        private ReactiveList<Domain.Tag> _Tags;
        public ReactiveList<Domain.Tag> Tags { get { return _Tags; } set { this.RaiseAndSetIfChanged(ref _Tags, value); } }
        private List<Domain.Tag> _TagsToSuggest;
        public List<Domain.Tag> TagsToSuggest { get { return _TagsToSuggest; } set { this.RaiseAndSetIfChanged(ref _TagsToSuggest, value); } }

    }


    public class FilterViewModel : ViewModelBase
    {
        EventAggregator EventManager = ((EventAggregator)App.Current.Resources["EventManager"]);

        public FilterViewModel(object entity)
        {
            SelectedPriceRoutine = (Domain.PriceRoutine)entity;

            //List of filter types
            FilterTypes = Enum.GetValues(typeof(Domain.FilterType)).Cast<Domain.FilterType>().ToList();

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



                    var type = Enum.GetName(typeof(Domain.FilterType), ftype);
                    var filterItems = SelectedPriceRoutine.Filters.Where(fs => fs.Type == type)
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

                    this.WhenAnyValue(vm => vm.SelectedFilterItems).Subscribe(selected =>
                    {
                        Console.WriteLine("test");
                    });

                    this.WhenAnyObservable(vm => vm.SelectedFilterItems.ItemChanged).Subscribe(selected =>
                    {
                        Console.WriteLine("test");
                    });

                    FilterItems.ItemChanged.Subscribe(x =>
                    {
                        Console.WriteLine("t");
                    });

                });


        }


        private ReactiveList<Domain.Filter> _FilterItems = new ReactiveList<Domain.Filter>() { ChangeTrackingEnabled = true };
        public ReactiveList<Domain.Filter> FilterItems { get { return _FilterItems; } set { this.RaiseAndSetIfChanged(ref _FilterItems, value); } }

        public IReactiveDerivedList<Domain.Filter> SelectedFilterItems { get; set; }

        private Domain.FilterType _SelectedType;
        public Domain.FilterType SelectedType { get { return _SelectedType; } set { this.RaiseAndSetIfChanged(ref _SelectedType, value); } }

        private List<Domain.FilterType> _FilterTypes;
        public List<Domain.FilterType> FilterTypes { get { return _FilterTypes; } set { this.RaiseAndSetIfChanged(ref _FilterTypes, value); } }
    }
}
