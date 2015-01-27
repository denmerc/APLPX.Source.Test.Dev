using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using APLPX.UI.WPF.Helpers;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class FilterGroup : DisplayEntityBase, IDisposable
    {

        #region Private Fields

        private string _name;
        private short _sort;
        private ReactiveList<Filter> _filters;
 
        private IDisposable _itemChangedSubscription;
        private bool _isDisposed;

        #endregion

        #region Constructors

        public FilterGroup()
        {
            Filters = new ReactiveList<Filter>();

            Filters.ChangeTrackingEnabled = true;
            _itemChangedSubscription = Filters.ItemChanged.Subscribe(f => OnFilterChanged(f));  
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        public ReactiveList<Filter> Filters
        {
            get { return _filters; }
            set
            {
                if (_filters != value)
                {
                    if (_filters != null && _itemChangedSubscription != null)
                    {
                        //Dispose of any existing change notification subscription.
                        _itemChangedSubscription.Dispose();
                    }

                    _filters = value;
                    this.RaisePropertyChanged("Filters");

                    if (_filters != null)
                    {
                        //Subscribe to change notifications for any FIlter in this group. 
                        //This enables us to detect when a Filter is selected or unselected.
                        _filters.ChangeTrackingEnabled = true;
                        _itemChangedSubscription = _filters.ItemChanged.Subscribe(f => OnFilterChanged(f));

                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether all, none, or some of the filters in a collection are marked IsSelected.
        /// True: All; False: None; Null: at least one, but not all, are selected.
        /// The result is a nullable Boolean (suitable for binding to a three-state checkbox, etc.)
        /// </summary>           
        public bool? AreAllFiltersSelected
        {
            get
            {
                bool? result = _filters.AreAllItemsIncluded(filter => filter.IsSelected);

                return result;
            }
        }

        /// <summary>
        /// Gets the number of filters in this group where IsSelected is true.
        /// </summary>
        public int SelectedCount
        {
            get
            {
                int result = _filters.Where(filter => filter.IsSelected).Count();

                return result;
            }
        } 

        #endregion

        /// <summary>
        /// Updates the FilterChangess Observable sequence whenever a property changes for any filter in this FilterGroup.
        /// </summary>   
        private void OnFilterChanged(IReactivePropertyChangedEventArgs<Filter> args)
        {
            var filter = args.Sender as Filter;

            if (filter != null)
            {
                //Update dependent properties.
                OnPropertyChanged("AreAllFiltersSelected");
                OnPropertyChanged("SelectedCount");
            }
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
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
                    if (Filters!= null)
                    {
                        Filters.ChangeTrackingEnabled = false;
                    }
                }
                _isDisposed = true;
            }
        }

        #endregion
    }
}
