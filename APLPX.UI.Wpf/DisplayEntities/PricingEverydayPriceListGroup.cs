using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class PricingEverydayPriceListGroup : PriceListGroup
    {
        #region Private Fields

        private ReactiveList<PricingEverydayPriceList> _priceLists;
        private ObservableCollection<PricingEverydayPriceList> _filteredPriceLists;

        private IDisposable _itemChangedSubscription;
        private ISubject<PricingEverydayPriceList> _priceListChangeSubject;

        #endregion

        #region Constructor

        public PricingEverydayPriceListGroup()
        {
            PriceLists = new ReactiveList<PricingEverydayPriceList>();

            _priceListChangeSubject = new Subject<PricingEverydayPriceList>();
            PriceListSelectedChanges = _priceListChangeSubject.AsObservable();
        }

        #endregion

        #region Properties

        public ReactiveList<PricingEverydayPriceList> PriceLists
        {
            get { return _priceLists; }
            set
            {
                if (_priceLists != value)
                {
                    //Dispose of any existing change notification subscription.
                    if (_priceLists != null && _itemChangedSubscription != null)
                    {
                        _itemChangedSubscription.Dispose();
                    }

                    _priceLists = value;
                    this.RaisePropertyChanged("PriceLists");

                    if (_priceLists != null)
                    {
                        //Subscribe to change notifications for any Price List in this group. 
                        //This enables us to detect when a Price List is selected or unselected.
                        _priceLists.ChangeTrackingEnabled = true;
                        _itemChangedSubscription = _priceLists.ItemChanged.Subscribe(priceList => OnPriceListSelectedChanged(priceList));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the price lists in this PriceListGroup whose sort orders fall below that of the designated key price list.
        /// </summary>
        public ObservableCollection<PricingEverydayPriceList> FilteredPriceLists
        {
            get { return _filteredPriceLists; }
            private set { this.RaiseAndSetIfChanged(ref _filteredPriceLists, value); }
        }

        #endregion

        #region Public Methods

        public void RecalculateFilteredPriceLists(int keyPriceListId = 0)
        {
            List<PricingEverydayPriceList> includedLists = PriceLists.ToList();

            if (keyPriceListId > 0)
            {
                var keyPriceList = PriceLists.Where(p => p.Id == keyPriceListId).FirstOrDefault();
                if (keyPriceList != null)
                {
                    short keySort = keyPriceList.Sort;
                    includedLists = includedLists.Where(p => p.Sort > keySort).ToList();
                }
            }

            FilteredPriceLists = new ObservableCollection<PricingEverydayPriceList>(includedLists);
        }

        #endregion

        #region Price List change notification

        /// <summary>
        /// Updates the PriceListChangeObservable sequence whenever the IsSelected property changes for any price list in this PriceListGroup.
        /// </summary>    
        private void OnPriceListSelectedChanged(IReactivePropertyChangedEventArgs<PricingEverydayPriceList> args)
        {
            var priceList = args.Sender as PricingEverydayPriceList;

            if (priceList != null &&
                args.PropertyName == "IsSelected" &&
                !priceList.IsKey)
            {
                _priceListChangeSubject.OnNext(priceList);
            }
        }

        /// <summary>
        /// Exposes an IObservable sequence of PriceLists within this group whose IsSelected property has changed.        
        /// </summary>
        public IObservable<PricingEverydayPriceList> PriceListSelectedChanges
        {
            private set;
            get;
        }


        #endregion

    }
}
