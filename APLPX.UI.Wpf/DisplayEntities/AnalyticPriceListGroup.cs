using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using APLPX.UI.WPF.Helpers;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class AnalyticPriceListGroup : PriceListGroup
    {

        #region Private Fields

        private ReactiveList<PriceList> _priceLists;

        private ISubject<PriceList> _priceListChangeSubject;
        private IDisposable _itemChangedSubscription;

        #endregion

        #region Constructors

        public AnalyticPriceListGroup()
        {
            PriceLists = new ReactiveList<PriceList>();

            //Initialize change notification related items.
            _priceListChangeSubject = new Subject<PriceList>();
            PriceListChanges = _priceListChangeSubject.AsObservable();
        }

        #endregion

        #region Properties

        public ReactiveList<PriceList> PriceLists
        {
            get { return _priceLists; }
            set
            {
                if (_priceLists != value)
                {
                    if (_priceLists != null && _itemChangedSubscription != null)
                    {
                        _itemChangedSubscription.Dispose();
                    }

                    _priceLists = value;
                    this.RaisePropertyChanged("PriceLists");

                    if (_priceLists != null)
                    {
                        //Subscribe to change notifications for any FIlter in this group. 
                        //This enables us to detect when a Filter is selected or unselected.
                        _priceLists.ChangeTrackingEnabled = true;
                        _itemChangedSubscription = _priceLists.ItemChanged.Subscribe(pl => OnPriceListChanged(pl));
                    }
                }
            }
        }

        public bool? AreAllItemsSelected
        {
            get
            {
                bool? result = _priceLists.AreAllItemsIncluded(p => p.IsSelected);
                return result;
            }
        }

        /// <summary>
        /// Gets the number of price lists in this group where IsSelected is true.
        /// </summary>
        public int SelectedCount
        {
            get
            {
                int result = _priceLists.Where(priceList => priceList.IsSelected).Count();

                return result;
            }
        }

        /// <summary>
        /// Exposes an IObservable sequence of Filters within this group whose IsSelected property has changed.        
        /// </summary>
        public IObservable<PriceList> PriceListChanges
        {
            private set;
            get;
        }


        #endregion


        private void OnPriceListChanged(IReactivePropertyChangedEventArgs<PriceList> args)
        {
            var priceList = args.Sender as PriceList;

            if (priceList != null)
            {
                _priceListChangeSubject.OnNext(priceList);

                //Update dependent properties.
                OnPropertyChanged("AreAllItemsSelected");
                OnPropertyChanged("SelectedCount");
            }
        }

    }
}
