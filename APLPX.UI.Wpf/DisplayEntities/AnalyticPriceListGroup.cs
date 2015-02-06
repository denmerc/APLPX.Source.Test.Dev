using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using APLPX.UI.WPF.Helpers;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class AnalyticPriceListGroup : PriceListGroup, IDisposable
    {

        #region Private Fields

        private ReactiveList<PriceList> _priceLists;

        private IDisposable _itemChangedSubscription;
        private bool _isDisposed;

        #endregion

        #region Constructors

        public AnalyticPriceListGroup()
        {
            PriceLists = new ReactiveList<PriceList>();

            PriceLists.ChangeTrackingEnabled = true;
            _itemChangedSubscription = PriceLists.ItemChanged.Subscribe(pl => OnPriceListChanged(pl));
        }

        #endregion

        #region Properties

        public ReactiveList<PriceList> PriceLists
        {
            get { return _priceLists; }
            set { this.RaiseAndSetIfChanged(ref _priceLists, value); }
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

        #endregion

        private void OnPriceListChanged(IReactivePropertyChangedEventArgs<PriceList> args)
        {
            var priceList = args.Sender as PriceList;

            if (priceList != null)
            {
                //Update dependent properties.
                OnPropertyChanged("AreAllItemsSelected");
                OnPropertyChanged("SelectedCount");
            }
        }

        public override List<Error> GetValidationErrors()
        {
            var result = new List<Error>();
            if (SelectedCount == 0)
            {
                string message = String.Format("{0} Price List: At least one item must be selected.", Name);
                result.Add(new Error { Message = message });
            }

            return result;
        }

        #region IDisposable

        protected override void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_itemChangedSubscription != null)
                    {
                        _itemChangedSubscription.Dispose();
                        _itemChangedSubscription = null;
                    }
                    if (PriceLists != null)
                    {
                        PriceLists.ChangeTrackingEnabled = false;
                    }
                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion

    }
}
