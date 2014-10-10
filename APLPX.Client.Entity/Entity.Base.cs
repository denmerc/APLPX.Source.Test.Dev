using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using ReactiveUI;
using APLPX.Client.Entity;
using APLPX.Core;
using APLPX.Core.Reactive;


namespace APLPX.Client.Entity
{
    //[DataContract]
    public class ObjectBase : NotificationObject, IDataErrorInfo
    {
        protected IValidator _Validator;
        protected IEnumerable<ValidationFailure> _ValidationErrors = null;
        protected bool _IsDirty;


        public ObjectBase(){
            _Validator = GetValidator();
            Validate();
        }

        //[DataMember]
        public IEnumerable<ValidationFailure> ValidationErrors{
            get{ return _ValidationErrors;}
            set {}
        }
        
        public void Validate()
        {
            if(_Validator != null){
                ValidationResult results = _Validator.Validate(this);
                _ValidationErrors = results.Errors;
            }
        }
        //[DataMember]
        public virtual bool IsValid
        {
            get
            {
                if (_ValidationErrors != null && _ValidationErrors.Count() > 0)
                    return false;
                else
                    return true;
            }
        }
        
        protected virtual IValidator GetValidator()
        {
 	        return null;
        }


        #region Property Change Notification
        protected override void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName, true);
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression, bool makeDirty)
        {
            string propertyName = Core.Utils.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName, makeDirty);

        }

        protected void OnPropertyChanged(string propertyName, bool makeDirty)
        {
            base.OnPropertyChanged(propertyName);

            if (makeDirty)
                _IsDirty = true;

            Validate();
        }
        #endregion

        #region IDirty Members
        public virtual bool IsDirty{
            get { return _IsDirty; }
            
            set{ //TODO: removed protected here to access from tests to initialize 
                _IsDirty = value;
                OnPropertyChanged("IsDirty", false);
            }
        }
        #endregion

        #region IDataErrorInfo members
        [DataMember]
        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                StringBuilder errors = new StringBuilder();

                if (_ValidationErrors != null && _ValidationErrors.Count() > 0)
                {
                    foreach (ValidationFailure validationError in _ValidationErrors)
                    {
                        if (validationError.PropertyName == columnName)
                            errors.AppendLine(validationError.ErrorMessage);
                    }
                }

                return errors.ToString();
            }
        }

        #endregion
    }


    public class NotificationObject : INotifyPropertyChanged
    {
        private event PropertyChangedEventHandler _PropertyChangedEvent;
        protected List<PropertyChangedEventHandler> _PropertyChangedSubscribers = new List<PropertyChangedEventHandler>();

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (!_PropertyChangedSubscribers.Contains(value))
                {
                    _PropertyChangedEvent += value;
                    _PropertyChangedSubscribers.Add(value);
                }
            }
            remove
            {
                _PropertyChangedEvent -= value;
                _PropertyChangedSubscribers.Remove(value);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (_PropertyChangedEvent != null)
                _PropertyChangedEvent(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string propertyName = Core.Utils.ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }
    }


    [DataContract]
    public class ReactiveValidatedEntity : ReactiveObject, IDataErrorInfo, IDirty
    {
        protected IValidator _Validator;
        protected IValidator Validator
        {
            get { return _Validator; }
            set { _Validator = value; }
        }

        public ReactiveValidatedEntity()
        {
            _Validator = GetValidator();

        }

        public void Dispose()
        {

        }

        public void Validate()
        {
            if (_Validator != null)
            {
                ValidationResult results = _Validator.Validate(this);
                //_ValidationErrors = results.Errors.CreateDerivedCollection();
                _ValidationErrors = results.Errors;
            }
        }
        [DataMember]
        protected Workflow _Workflow = null;
        public Workflow Workflow
        {
            get { return _Workflow; }
            set { this.RaiseAndSetIfChanged(ref _Workflow, value); }
        }


        [IgnoreDataMember]
        private string _headerTitle;
        public string HeaderTitle
        {
            get
            {
                return _headerTitle;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _headerTitle, value);
            }
        }

        [IgnoreDataMember]
        private string _StepHeader;
        public string StepHeader
        {
            get
            {
                return _StepHeader;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _StepHeader, value);
            }
        }

        private int _ActiveStep = 1;
        public int ActiveStep
        {
            get
            {
                return _ActiveStep;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _ActiveStep, value);

            }
        }


        private ReactiveValidatedEntity _SelectedStepViewModel;
        public ReactiveValidatedEntity SelectedStepViewModel
        {
            get
            {
                return _SelectedStepViewModel;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _SelectedStepViewModel, value);
            }
        }

        [DataMember]
        public Int32 EntityId { get; set; }
        [DataMember]
        protected Boolean IsEditing { get; set; }

        [IgnoreDataMember]
        public ReactiveCommand<System.Reactive.Unit> PreviousCommand { get; set; }
        [IgnoreDataMember]
        public ReactiveCommand<System.Reactive.Unit> SaveCommand { get; set; }
        [IgnoreDataMember]
        public ReactiveCommand<System.Reactive.Unit> CancelCommand { get; set; }
        [IgnoreDataMember]
        public ReactiveCommand<System.Reactive.Unit> ClearCommand { get; set; }
        [IgnoreDataMember]
        public ReactiveCommand<System.Reactive.Unit> NextCommand { get; set; }
        [IgnoreDataMember]
        public ReactiveCommand<System.Reactive.Unit> ChangeStepCommand { get; set; }



        Session<NullT> _session;
        public Session<NullT> Session
        {
            get
            { return _session; }
            set { this.RaiseAndSetIfChanged(ref _session, value); }
        }

        [DataMember]
        protected IEnumerable<ValidationFailure> _ValidationErrors = null;
        [DataMember]
        public IEnumerable<ValidationFailure> ValidationErrors
        {
            get { return _ValidationErrors; }
            set { this.RaiseAndSetIfChanged(ref _ValidationErrors, value); }
        }

        [DataMember]
        protected ReactiveList<Client.Entity.Workflow.Error> _ServerErrors = null;
        public ReactiveList<Client.Entity.Workflow.Error> ServerErrors
        {
            get { return _ServerErrors; }
            set { this.RaiseAndSetIfChanged(ref _ServerErrors, value); }
        }

        protected virtual IValidator GetValidator()
        {
            return null;
        }

        [DataMember]
        public virtual bool IsValid
        {
            get
            {
                if (_ValidationErrors != null && _ValidationErrors.Count() > 0)
                    return false;
                else
                    return true;
            }
            set { }
        }


        [IgnoreDataMember]
        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
            
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                StringBuilder errors = new StringBuilder();

                if (_ValidationErrors != null && _ValidationErrors.Count() > 0)
                {
                    foreach (ValidationFailure validationError in _ValidationErrors)
                    {
                        if (validationError.PropertyName == columnName)
                            errors.AppendLine(validationError.ErrorMessage);
                    }
                }

                return errors.ToString();
            }
        }

        public IEventAggregator Publisher { get; set; }

        #region IDirty Members
        

            protected Boolean _IsDirty = false;
            [DataMember]
            public virtual Boolean IsDirty
            {
                get { return _IsDirty; }
                set { this.RaiseAndSetIfChanged(ref _IsDirty, value); }
            }

            public virtual bool IsAnythingDirty()
            {
                bool isDirty = false;
                WalkObjectGraph
                    (o =>
                        {
                            if (o.IsDirty)
                            {
                                isDirty = true;
                                return true; // short circuit
                            }
                            else
                                return false;
                        }, coll => { }
                    );
                return isDirty;
            }

            public List<IDirty> GetDirtyObjects()
            {
                List<IDirty> dirtyObjects = new List<IDirty>();

                WalkObjectGraph(
                o =>
                {
                    if (o.IsDirty)
                        dirtyObjects.Add(o);

                    return false;
                }, coll => { });

                return dirtyObjects;
            }

            public void CleanAll()
            {
                WalkObjectGraph(
                o =>
                {
                    if (o.IsDirty)
                        o.IsDirty = false;
                    return false;
                }, coll => { });
                            }

        #endregion
            
        
        
        protected void WalkObjectGraph(Func<ReactiveValidatedEntity, bool> snippetForObject,
                                   Action<IList> snippetForCollection,
                                   params string[] exemptProperties)
            {
                List<ReactiveValidatedEntity> visited = new List<ReactiveValidatedEntity>();
                Action<ReactiveValidatedEntity> walk = null;

                List<string> exemptions = new List<string>();
                if (exemptProperties != null)
                    exemptions = exemptProperties.ToList();

                walk = (o) =>
                {
                    if (o != null && !visited.Contains(o))
                    {
                        visited.Add(o);

                        bool exitWalk = snippetForObject.Invoke(o);

                        if (!exitWalk)
                        {
                            PropertyInfo[] properties = o.GetBrowsableProperties();
                            foreach (PropertyInfo property in properties)
                            {
                                if (!exemptions.Contains(property.Name))
                                {
                                    if (property.PropertyType.IsSubclassOf(typeof(ReactiveValidatedEntity)))
                                    {
                                        ReactiveValidatedEntity obj = (ReactiveValidatedEntity)(property.GetValue(o, null));
                                        walk(obj);
                                    }
                                    else
                                    {
                                        IList coll = property.GetValue(o, null) as IList;
                                        if (coll != null)
                                        {
                                            snippetForCollection.Invoke(coll);

                                            foreach (object item in coll)
                                            {
                                                if (item is ReactiveValidatedEntity)
                                                    walk((ReactiveValidatedEntity)item);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };

                walk(this);
            }

    }

    
}
