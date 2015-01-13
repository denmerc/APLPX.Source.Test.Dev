using System;
using ReactiveUI;
using APLPX.UI.WPF.Interfaces;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Display entity for an Analytic Identity.
    /// </summary>
    public class AnalyticIdentity : DisplayEntityBase
    {
        #region Private Fields

        private string _name;
        private string _description;
        private string _notes;
        private string _refreshedText;
        private string _createdText;
        private string _editedText;
        private DateTime _refreshed;
        private DateTime _created;
        private DateTime _edited;
        private string _author;
        private string _editor;
        private string _owner;
        private bool _isActive;
        private bool _isShared;

        #endregion

        public AnalyticIdentity()
        {
        }

        #region Properties

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string Description
        {
            get { return _description; }
            set { this.RaiseAndSetIfChanged(ref _description, value); }
        }

        public string Notes
        {
            get { return _notes; }
            set { this.RaiseAndSetIfChanged(ref _notes, value); }
        }

        public string RefreshedText
        {
            get { return _refreshedText; }
            set { this.RaiseAndSetIfChanged(ref _refreshedText, value); }
        }

        public string CreatedText
        {
            get { return _createdText; }
            set { this.RaiseAndSetIfChanged(ref _createdText, value); }
        }

        public string EditedText
        {
            get { return _editedText; }
            set { this.RaiseAndSetIfChanged(ref _editedText, value); }
        }

        public DateTime Refreshed
        {
            get { return _refreshed; }
            set { this.RaiseAndSetIfChanged(ref _refreshed, value); }
        }

        public DateTime Created
        {
            get { return _created; }
            set { this.RaiseAndSetIfChanged(ref _created, value); }
        }

        public DateTime Edited
        {
            get { return _edited; }
            set { this.RaiseAndSetIfChanged(ref _edited, value); }
        }

        public string Author
        {
            get { return _author; }
            set { this.RaiseAndSetIfChanged(ref _author, value); }
        }

        public string Editor
        {
            get { return _editor; }
            set { this.RaiseAndSetIfChanged(ref _editor, value); }
        }

        public string Owner
        {
            get { return _owner; }
            set { this.RaiseAndSetIfChanged(ref _owner, value); }
        }

        public bool IsActive
        {
            get { return _isActive; }

            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    this.RaisePropertyChanged("IsActive");

                    //Update dependent value.
                    this.RaisePropertyChanged("StatusDescription");
                }
            }
        }

        public bool Shared
        {
            get { return _isShared; }
            set
            {
                if (_isShared != value)
                {
                    _isShared = value;
                    this.RaisePropertyChanged("Shared");

                    //Update dependent value.
                    this.RaisePropertyChanged("SharedDescription");
                }
            }
        }

        /// <summary>
        /// Gets a string that describes this identity's status.
        /// </summary>
        public string StatusDescription
        {
            get
            {
                string result = "Inactive";

                if (IsActive)
                {
                    result = "Active";
                }

                return result;
            }
        }

        /// <summary>
        /// Gets a string that describes whether this identity represents a shared entity.
        /// </summary>
        public string SharedDescription
        {
            get
            {
                string result = "No";

                if (Shared)
                {
                    result = "Yes";
                }

                return result;
            }
        }

        #endregion

    }
}
