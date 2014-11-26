using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace APLPX.UI.WPF.DisplayEntities
{
    public class UserIdentity : DisplayEntityBase
    {
        #region Private Fields
             
        private bool _active;
        private string _email;
        private string _name;
        private string _firstName;
        private string _lastName;
        private string _greeting;
        private DateTime _lastLogin;
        private string _lastLoginText;
        private DateTime _created;
        private string _createdText;
        private DateTime _edited;
        private string _editedText;
        private string _editor;

        #endregion

        #region Constructors

        public UserIdentity()
        {
        }

        #endregion

        #region Properties 

        public bool Active
        {
            get { return _active; }
            set { this.RaiseAndSetIfChanged(ref _active, value); }
        }

        public string Email
        {
            get { return _email; }
            set { this.RaiseAndSetIfChanged(ref _email, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.RaiseAndSetIfChanged(ref _name, value); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { this.RaiseAndSetIfChanged(ref _firstName, value); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { this.RaiseAndSetIfChanged(ref _lastName, value); }
        }

        public DateTime LastLogin
        {
            get { return _lastLogin; }
            set { this.RaiseAndSetIfChanged(ref _lastLogin, value); }
        }

        public string Greeting
        {
            get { return _greeting; }
            set { this.RaiseAndSetIfChanged(ref _greeting, value); }
        }

        public string LastLoginText
        {
            get { return _lastLoginText; }
            set { this.RaiseAndSetIfChanged(ref _lastLoginText, value); }
        }

        public DateTime Created
        {
            get { return _created; }
            set { this.RaiseAndSetIfChanged(ref _created, value); }
        }

        public string CreatedText
        {
            get { return _createdText; }
            set { this.RaiseAndSetIfChanged(ref _createdText, value); }
        }

        public DateTime Edited
        {
            get { return _edited; }
            set { this.RaiseAndSetIfChanged(ref _edited, value); }
        }

        public string EditedText
        {
            get { return _editedText; }
            set { this.RaiseAndSetIfChanged(ref _editedText, value); }
        }

        public string Editor
        {
            get { return _editor; }
            set { this.RaiseAndSetIfChanged(ref _editor, value); }
        }

        #endregion

    }
}
