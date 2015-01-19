using System;
using System.Collections.Generic;
using System.Linq;

using APLPX.UI.WPF.DisplayEntities;
using ReactiveUI;
using Display = APLPX.UI.WPF.DisplayEntities;


namespace APLPX.UI.WPF.ViewModels.Analytic
{
    /// <summary>
    /// View model for analytic identity-related views.
    /// </summary>
    public class AnalyticIdentityViewModel : ViewModelBase, IDisposable
    {
        #region Private Fields

        private Display.Analytic _analytic;
        private Display.ModuleFeature _feature;
        private IDisposable _searchKeyChangedSubscription;
        private bool _isDisposed;

        #endregion

        #region Constructor

        public AnalyticIdentityViewModel(Display.Analytic analytic, Display.ModuleFeature feature)
        {
            if (analytic == null)
            {
                throw new ArgumentNullException("analytic");
            }

            _analytic = analytic;
            _feature = feature;

            var searchKeyChanged = _analytic.WhenAnyValue(item => item.SearchGroupKey);
            _searchKeyChangedSubscription = searchKeyChanged.Subscribe(key => OnSearchKeyChanged(key));
        }

        #endregion

        public Display.Analytic Analytic
        {
            get { return _analytic; }
            private set { _analytic = value; }
        }

        public List<FeatureSearchGroup> AssignableFolders
        {
            get
            {
                var searchGroups = _feature.SearchGroups.Where(sg => sg.CanSearchKeyChange);
                var result = searchGroups.ToList();

                return result;
            }
        }

        private void OnSearchKeyChanged(string searchKey)
        {
            if (_feature != null)
            {
                _feature.AssignSearchProperties();
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
                if (_searchKeyChangedSubscription != null)
                {
                    _searchKeyChangedSubscription.Dispose();
                }
                _isDisposed = true;
            }
        }

        #endregion
    }
}
