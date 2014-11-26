using System;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels
{

    public class FilterViewModel : ViewModelBase
    {
        private ISearchableEntity _entity;

        public FilterViewModel(ISearchableEntity entity)
        {
            _entity = entity;
        }

        public ISearchableEntity Entity
        {
            get { return _entity; }
            set { this.RaiseAndSetIfChanged(ref _entity, value); }
        }

    }

}
