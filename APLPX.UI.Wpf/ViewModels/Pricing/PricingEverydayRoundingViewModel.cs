using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using APLPX.UI.WPF.DisplayEntities;
using APLPX.UI.WPF.Helpers;
using ReactiveUI;

namespace APLPX.UI.WPF.ViewModels.Pricing
{
    public class PricingEverydayRoundingViewModel : ViewModelBase
    {
        #region Private Fields

        private PricingEveryday _priceRoutine;
        private PricingEverydayPriceList _selectedPriceList;
        private List<PricingRoundingTemplate> _roundingTemplates;
        private PricingRoundingTemplate _selectedTemplate;
        private bool _canApplyTemplate;
        private IDisposable _applyTemplateSubscription;
        private bool _isDisposed;

        #endregion

        #region Constructor and Initialization

        public PricingEverydayRoundingViewModel(PricingEveryday priceRoutine)
        {
            if (priceRoutine == null)
            {
                throw new ArgumentNullException("priceRoutine");
            }

            PriceRoutine = priceRoutine;
            PriceRoutine.UpdatePriceListGroups();

            InitializeObservables();

            //Select the first price list by default.
            if (PriceRoutine.RoundingRulePriceLists.Count > 0)
            {
                SelectedPriceList = PriceRoutine.RoundingRulePriceLists[0];
            }
        }

        private void InitializeObservables()
        {
            var canApplyTemplate = this.WhenAnyValue(vm => vm.SelectedTemplate, vm => vm.SelectedPriceList, (a, b) => ApplyTemplateCanExecute(a, b));
            ApplyTemplateCommand = ReactiveCommand.Create(canApplyTemplate);

            //Also expose current value via a property for data binding.
            canApplyTemplate.Subscribe(value => CanApplyTemplate = value);

            //Note: see http://stackoverflow.com/questions/22213444/what-are-the-distinctions-between-the-various-whenany-methods-in-reactive-ui
            //for subscribing to a command via WhenAnyObservable() vs. subscribing directly.
            _applyTemplateSubscription = this.WhenAnyObservable(vm => vm.ApplyTemplateCommand).Subscribe(item => ApplyTemplateExecuted(item));
        }

        #endregion

        #region Properties

        public ReactiveCommand<object> ApplyTemplateCommand { get; private set; }

        /// <summary>
        /// Gets the PricingEveryday price routine for this view model.
        /// </summary>
        public PricingEveryday PriceRoutine
        {
            get { return _priceRoutine; }
            private set { _priceRoutine = value; }
        }

        /// <summary>
        /// Gets/sets the currently selected price list in the bound view.
        /// </summary>
        public PricingEverydayPriceList SelectedPriceList
        {
            get { return _selectedPriceList; }
            set { this.RaiseAndSetIfChanged(ref _selectedPriceList, value); }
        }

        /// <summary>
        /// Gets/sets the available rounding templates.
        /// </summary>
        public List<PricingRoundingTemplate> RoundingTemplates
        {
            get { return _roundingTemplates; }
            set
            {
                if (_roundingTemplates != value)
                {
                    _roundingTemplates = value;
                    this.RaisePropertyChanged("RoundingTemplates");

                    //Select the first template by default.
                    if (_roundingTemplates != null &&
                        _roundingTemplates.Count > 0 &&
                        SelectedTemplate == null)
                    {
                        SelectedTemplate = _roundingTemplates[0];
                    }
                }
            }
        }

        public PricingRoundingTemplate SelectedTemplate
        {
            get { return _selectedTemplate; }
            set { this.RaiseAndSetIfChanged(ref _selectedTemplate, value); }
        }

        /// <summary>
        /// Gets/sets a value indicating whether a rounding template can be applied in the current context.
        /// </summary>
        public bool CanApplyTemplate
        {
            get { return _canApplyTemplate; }
            set { this.RaiseAndSetIfChanged(ref _canApplyTemplate, value); }
        }

        #endregion

        #region Command Handlers

        private bool ApplyTemplateCanExecute(PricingRoundingTemplate template, PricingEverydayPriceList priceList)
        {
            bool result = template != null && priceList != null;
            return result;
        }

        private object ApplyTemplateExecuted(object parameter)
        {
            var template = parameter as PricingRoundingTemplate;
            if (template != null && SelectedPriceList != null)
            {
                string prompt = String.Format("Copy the rounding rules\nfrom \"{0}\"\nto \"{1}\"?", SelectedTemplate.Name, SelectedPriceList.Name);
                bool okToProceed = base.ShowPrompt(prompt) ?? false;

                if (okToProceed)
                {
                    //Copy the rules from the template to the price list.
                    List<PriceRoundingRule> copiedRules = new List<PriceRoundingRule>();
                    foreach (PriceRoundingRule rule in template.Rules)
                    {
                        PriceRoundingRule copy = rule.Copy();
                        copiedRules.Add(copy);
                    }
                    SelectedPriceList.LinkedPriceListRule.RoundingRules = copiedRules;
                }
            }
            return null;
        }

        #endregion

        #region IDisposable

        protected override void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (_applyTemplateSubscription != null)
                    {
                        _applyTemplateSubscription.Dispose();
                        _applyTemplateSubscription = null;
                    }
                }
                _isDisposed = true;
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}
