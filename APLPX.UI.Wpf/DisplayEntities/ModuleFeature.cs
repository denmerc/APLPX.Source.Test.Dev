using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using APLPX.Entity;
using APLPX.UI.WPF.Interfaces;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Display entity for a Module Feature .
    /// </summary>
    public class ModuleFeature : DisplayEntityBase
    {
        #region Private Fields

        private ModuleFeatureType _typeId;
        private string _name;
        private string _title;
        private short _sort;

        private ModuleFeatureStepType _landingStepType;
        private ModuleFeatureStepType _actionStepType;
        private ModuleFeatureStep _selectedStep;

        private List<ModuleFeatureStep> _steps;
        private List<FeatureSearchGroup> _searchGroups;
        private List<ISearchableEntity> _searchableEntities;

        private FeatureSearchGroup _selectedSearchGroup;
        private ISearchableEntity _selectedEntity;

        #endregion

        #region Constructors

        public ModuleFeature()
        {
            SearchGroups = new List<FeatureSearchGroup>();
            Steps = new List<ModuleFeatureStep>();
            SearchableEntities = new List<ISearchableEntity>();
        }

        #endregion

        #region Properties

        public ModuleFeatureType TypeId
        {
            get { return _typeId; }
            set { this.RaiseAndSetIfChanged(ref _typeId, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Title
        {
            get { return _title; }
            set { this.RaiseAndSetIfChanged(ref _title, value); }
        }

        public short Sort
        {
            get { return _sort; }
            set { this.RaiseAndSetIfChanged(ref _sort, value); }
        }

        public ModuleFeatureStepType LandingStepType
        {
            get { return _landingStepType; }
            set { this.RaiseAndSetIfChanged(ref _landingStepType, value); }
        }

        public ModuleFeatureStepType ActionStepType
        {
            get { return _actionStepType; }
            set { this.RaiseAndSetIfChanged(ref _actionStepType, value); }
        }

        public List<FeatureSearchGroup> SearchGroups
        {
            get { return _searchGroups; }
            set { this.RaiseAndSetIfChanged(ref _searchGroups, value); }
        }

        public List<ModuleFeatureStep> Steps
        {
            get { return _steps; }
            set { this.RaiseAndSetIfChanged(ref _steps, value); }
        }

        public ModuleFeatureStep SelectedStep
        {
            get { return _selectedStep; }
            set { this.RaiseAndSetIfChanged(ref _selectedStep, value); }
        }

        /// <summary>
        /// Gets the next step for this feature.
        /// </summary>
        public ModuleFeatureStep NextStep
        {
            get
            {
                ModuleFeatureStep result = null;

                if (SelectedStep != null)
                {
                    result = Steps.Where(step => step.Sort == SelectedStep.Sort + 1).FirstOrDefault();
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the previous step for this feature.
        /// </summary>
        public ModuleFeatureStep PreviousStep
        {
            get
            {
                ModuleFeatureStep result = null;

                if (SelectedStep != null)
                {
                    result = Steps.Where(step => step.Sort == SelectedStep.Sort - 1).FirstOrDefault();
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the <see cref="ModuleFeatureStep"/> designated as the default landing step for this feature.
        /// </summary>
        public ModuleFeatureStep DefaultLandingStep
        {
            get
            {
                ModuleFeatureStep result = Steps.Where(step => step.TypeId == LandingStepType).FirstOrDefault();
                return result;
            }
        }

        /// <summary>
        /// Gets the <see cref="ModuleFeatureStep"/> designated as the default action step for this feature.
        /// </summary>
        public ModuleFeatureStep DefaultActionStep
        {
            get
            {
                ModuleFeatureStep result = Steps.Where(step => step.TypeId == ActionStepType).FirstOrDefault();
                return result;
            }
        }

        /// <summary>
        /// Gets a representation of this feature's Search Groups suitable for display as a list.
        /// </summary>
        public ObservableCollection<FeatureSearchGroup> SearchGroupDisplayList
        {
            get
            {
                var list = new List<FeatureSearchGroup>(this.SearchGroups);

                AssignSearchProperties();

                //Identify items representing sub-groups ("Folder 1", "Folder 2", etc.) 
                var parentGroups = SearchGroups.GroupBy(sg => sg.ParentName)
                      .Where(group => group.First().ParentName != group.First().Name);

                //Mark each subgroup so the view can interpret accordingly.
                foreach (var parent in parentGroups)
                {
                    foreach (FeatureSearchGroup searchGroup in parent)
                    {
                        searchGroup.IsSubGroup = true;
                    }

                    //Create a new search group  to represent the "parent" of this subgroup.
                    //The view is responsible for displaying this item appropriately.
                    short sort = Convert.ToInt16(parent.First().Sort - 1);
                    short itemCount = Convert.ToInt16(parent.Sum(sub => sub.ItemCount));
                    var parentGroup = new FeatureSearchGroup
                    {
                        Name = parent.Key,
                        ParentName = parent.Key,
                        HasSubGroups = true,
                        Sort = sort,
                        ItemCount = itemCount
                    };

                    list.Add(parentGroup);
                }

                return new ObservableCollection<FeatureSearchGroup>(list);
            }
        }

        /// <summary>
        /// Gets/sets the currently selected SearchGroup within this feature.
        /// </summary>
        public FeatureSearchGroup SelectedSearchGroup
        {
            get { return _selectedSearchGroup; }
            set
            {
                if (_selectedSearchGroup != value)
                {
                    _selectedSearchGroup = value;
                    this.RaisePropertyChanged("SelectedSearchGroup");
                    if (_selectedSearchGroup != null)
                    {
                        //Trigger an update of related calculated property.
                        this.RaisePropertyChanged("FilteredSearchableEntities");
                    }
                }
            }
        }

        /// <summary>
        /// Gets/sets the selected entity (e.g., Analytic, Price Routine, User, etc.) within this feature.
        /// </summary>
        public ISearchableEntity SelectedEntity
        {
            get { return _selectedEntity; }
            set { this.RaiseAndSetIfChanged(ref _selectedEntity, value); }
        }

        /// <summary>
        /// Gets/sets the list of all searchable entities (e.g., Analytics, Price Routines, etc.) for this feature.
        /// </summary>
        public List<ISearchableEntity> SearchableEntities
        {
            get { return _searchableEntities; }
            set { this.RaiseAndSetIfChanged(ref _searchableEntities, value); }
        }

        /// <summary>
        /// Gets the list of searchable entites filtered by the currently selected SearchGroup, if any.
        /// Returns an empty list if no SearchGroup is selected.
        /// </summary>
        public List<ISearchableEntity> FilteredSearchableEntities
        {
            get
            {
                var matchingEntities = Enumerable.Empty<ISearchableEntity>();

                if (SelectedSearchGroup != null)
                {
                    matchingEntities = SearchableEntities.Where(item => item.SearchGroupKey == SelectedSearchGroup.SearchGroupKey);
                }

                return new List<ISearchableEntity>(matchingEntities);
            }
        }

        /// <summary>
        /// Gets a string describing this feature's classification.
        /// </summary>
        public string Classification
        {
            get
            {
                string result = String.Empty;

                switch (TypeId)
                {
                    case ModuleFeatureType.PlanningAnalytics:
                        result = "Analytic";
                        break;
                    case ModuleFeatureType.PlanningEverydayPricing:
                    case ModuleFeatureType.PlanningPromotionPricing:
                    case ModuleFeatureType.PlanningKitPricing:
                        result = "Price Routine";
                        break;
                    case ModuleFeatureType.AdministrationUserMaintenance:
                        result = "User";
                        break;
                    default:
                        result = TypeId.ToString();
                        break;
                }
                return result;
            }
        }

        #endregion

        #region Step Navigation Methods

        /// <summary>
        /// Moves to the specified step within this feature.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>true if the move was completed; otherwise, false.</returns>
        public bool MoveToStep(ModuleFeatureStepType type)
        {
            bool completed = false;

            var matchingStep = Steps.Where(item => item.TypeId == type).FirstOrDefault();

            if (matchingStep != null)
            {
                SelectedStep = matchingStep;
                completed = true;
            }

            return completed;
        }

        /// <summary>
        /// Moves to the next step within this feature.
        /// </summary>
        /// <returns>true if the move was completed; otherwise, false.</returns>
        public bool MoveToNextStep()
        {
            bool completed = false;
            if (NextStep != null)
            {
                SelectedStep = NextStep;
                completed = true;
            }

            return completed;
        }

        /// <summary>
        /// Moves to the previous step within this feature.
        /// </summary>
        /// <returns>true if the move was completed; otherwise, false.</returns>
        public bool MoveToPreviousStep()
        {
            bool completed = false;

            if (PreviousStep != null)
            {
                SelectedStep = PreviousStep;
                completed = true;
            }

            return completed;
        }

        public void DisableRemainingSteps()
        {
            SetRemainingStepsEnabled(false);
        }

        public void EnableRemainingSteps()
        {
            SetRemainingStepsEnabled(true);
        }

        public void SetAllStepsEnabled(bool isEnabled)
        {
            foreach (var step in Steps)
            {
                step.IsEnabled = isEnabled;
            }
        }

        private void SetRemainingStepsEnabled(bool isEnabled)
        {
            if (SelectedStep != null)
            {
                var steps = Steps.Where(step => step.TypeId != SelectedStep.TypeId).OrderBy(step => step.Sort);
                foreach (var step in steps)
                {
                    step.IsEnabled = isEnabled;
                }
            }
        }

        #endregion

        public void AssignSearchProperties()
        {
            foreach (FeatureSearchGroup searchGroup in SearchGroups)
            {
                var matchingEntities = SearchableEntities.Where(item => item.SearchGroupKey == searchGroup.SearchGroupKey);
                foreach (ISearchableEntity entity in matchingEntities)
                {
                    entity.SearchGroup = searchGroup;
                    entity.CanNameChange = searchGroup.CanNameChange;
                    entity.CanSearchKeyChange = searchGroup.CanSearchKeyChange;
                    entity.SearchGroupId = searchGroup.SearchGroupId;

                    AssignOwningSearchGroupId(entity);
                }
            }
        }

        private void AssignOwningSearchGroupId(ISearchableEntity entity)
        {
            if (entity.SearchGroupId > 0)
            {
                entity.OwningSearchGroupId = entity.SearchGroupId;
            }
            else
            {
                //Find the corresponding entity that is assigned to its owning search group.
                ISearchableEntity owningItem = SearchableEntities.FirstOrDefault(item => item.Id == entity.Id && item.SearchGroupId > 0);
                if (owningItem != null)
                {
                    entity.OwningSearchGroupId = owningItem.SearchGroupId;
                }
            }
        }

        public void RecalculateSearchItemCounts()
        {
            foreach (FeatureSearchGroup searchGroup in SearchGroups)
            {
                var matchingEntities = SearchableEntities.Where(item => item.SearchGroupKey == searchGroup.SearchGroupKey);
                searchGroup.ItemCount = Convert.ToInt16(matchingEntities.Count());
            }
            this.RaisePropertyChanged("SearchGroupDisplayList");
        }

        #region Overrides

        public override string ToString()
        {
            string result = String.Format("{0}:Name={1};Type={2};LandingStepType={3};ActionStepType={4}",
                                          GetType().Name, Name, TypeId, LandingStepType, ActionStepType);
            return result;
        }

        #endregion
    }
}
