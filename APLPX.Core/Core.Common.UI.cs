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
using APLPX.Core.Reactive;

namespace APLPX.Core.UI
{




    [DataContract]
    public class ReactiveValidatedEntity : ReactiveObject, IDataErrorInfo, IDirty
    {

        public ReactiveValidatedEntity()
        {
            _Validator = GetValidator();

        }

        public void Dispose()
        {

        }

        #region Validation Members
        
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
        public void Validate()
        {
            if (_Validator != null)
            {
                ValidationResult results = _Validator.Validate(this);
                //_ValidationErrors = results.Errors.CreateDerivedCollection();
                _ValidationErrors = results.Errors;
            }
        }

        protected IValidator _Validator;
        protected IValidator Validator
        {
            get { return _Validator; }
            set { _Validator = value; }
        }

        [DataMember]
        protected IEnumerable<ValidationFailure> _ValidationErrors = null;
        [DataMember]
        public IEnumerable<ValidationFailure> ValidationErrors
        {
            get { return _ValidationErrors; }
            set { this.RaiseAndSetIfChanged(ref _ValidationErrors, value); }
        }



        protected virtual IValidator GetValidator()
        {
            return null;
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

        #endregion

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
                    }
                    , c => { });
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
                    },
                    c => { }
                );
            }

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
        #endregion
            
        #region Property change notification

        protected void OnPropertyChanged(string propertyName)
        {
            //OnPropertyChanged(propertyName, true);

            Validate();
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression, bool makeDirty)
        {
            //string propertyName = Core.Utils.ExtractPropertyName(propertyExpression);
            //OnPropertyChanged(propertyName, makeDirty);

            if (makeDirty)
                IsDirty = true;

            Validate();
        }

        protected void OnPropertyChanged(string propertyName, bool makeDirty)
        {
            

            if (makeDirty)
                IsDirty = true;

            Validate();
        }

        #endregion


    }
}
