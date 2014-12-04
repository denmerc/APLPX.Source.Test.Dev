using System;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    /// <summary>
    /// Display entity for a Pricing Identity.
    /// </summary>
    public class PricingIdentity : DisplayEntityBase
    {
        #region Private Fields

        private int _analyticsId;
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
        private bool _active;
        private bool _shared;

        #endregion

        #region Constructor

        public PricingIdentity()
        {
        }

        #endregion

        #region Properties

        public int AnalyticsId
        {
            get { return _analyticsId; }
            set { this.RaiseAndSetIfChanged(ref _analyticsId, value); }
        }

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

        public bool Active
        {
            get { return _active; }
            set { this.RaiseAndSetIfChanged(ref _active, value); }
        }

        public bool Shared
        {
            get { return _shared; }
            set { this.RaiseAndSetIfChanged(ref _shared, value); }
        }

        #endregion
    }
}
