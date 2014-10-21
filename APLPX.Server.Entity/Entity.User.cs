using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Server.Entity
{
    [DataContract]
    public class User
    {
        #region Initialize...
        public User() { }
        public User(
            Int32 Id
            ) {
            this.Id=Id;
        }
        public User(
            Int32 Id,
            UserRole Role
            ) {
            this.Id=Id;
            this.Role = Role;
        }
        public User(
            Int32 Id,
            UserIdentity Identity
            ) {
            this.Id=Id;
            this.Identity = Identity;
        }
        public User(
            Int32 Id,
            UserIdentity Identity,
            List<Module> Modules
            ) {
            this.Id=Id;
            this.Identity = Identity;
            this.Modules=Modules;
        }
        public User(
            Int32 Id,
            List<Entity.SQLEnumeration> RoleTypes
            ) {
            this.Id=Id;
            this.RoleTypes=RoleTypes;
        }
        public User(
            Int32 Id,
            String SqlKey,
            UserRole Role,
            UserIdentity Identity
            ) {
            this.Id=Id;
            this.SqlKey=SqlKey;
            this.Role = Role;
            this.Identity = Identity;
        }
        public User(
            Int32 Id,
            String SqlKey,
            UserRole Role,
            UserIdentity Identity,
            List<Module> Modules,
            List<Entity.SQLEnumeration> RoleTypes
            ) {
            this.Id=Id;
            this.SqlKey=SqlKey;
            this.Role = Role;
            this.Identity = Identity;
            this.Modules=Modules;
            this.RoleTypes=RoleTypes;
        }
        #endregion

        [DataMember]
        public Int32 Id; //CLIENT { get; private set; }
        [DataMember]
        public String SqlKey; //CLIENT { get; private set; }
        [DataMember]
        public UserIdentity Identity; //CLIENT { get; private set; }
        [DataMember]
        public UserRole Role;  //CLIENT { get; private set; }
        [DataMember]
        public List<Module> Modules; //CLIENT { get; private set; } 
        [DataMember]
        public UserPassword Password; //CLIENT { get; set; }
        [DataMember]
        public List<Entity.SQLEnumeration> RoleTypes; //CLIENT { get; private set; }
    }

    [DataContract]
    public class UserIdentity {

        #region Initialize...
        public UserIdentity() { }
        public UserIdentity(
            String Login,
            String Email,
            String FirstName,
            String LastName,
            Boolean Active
            ) {
            this.Login = Login;
            this.Email = Email;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Active = Active;
        }
        public UserIdentity(
            String Login,
            String Email,
            String Greeting,
            String Name,
            String FirstName, 
            String LastName,
            String LastLoginText,
            String CreatedText, 
            String EditedText, 
            DateTime LastLogin,
            DateTime Created,
            DateTime Edited,
            String Editor,
            Boolean Active
            ) {
            this.Login = Login;
            this.Email = Email;
            this.Greeting = Greeting;
            this.Name = Name;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.LastLoginText = LastLoginText;
            this.CreatedText = CreatedText;
            this.EditedText = EditedText;
            this.LastLogin = LastLogin;
            this.Created = Created;
            this.Edited = Edited;
            this.Editor = Editor;
            this.Active = Active;
        }
        #endregion

        [DataMember]
        public String Login; //CLIENT { get; set; }
        [DataMember]
        public String Email; //CLIENT { get; set; }
        [DataMember]
        public String Name; //CLIENT { get; private set; }
        [DataMember]
        public String FirstName; //CLIENT { get; set; }
        [DataMember]
        public String LastName; //CLIENT { get; set; }
        [DataMember]
        public String Greeting; //CLIENT { get; private set; }
        [DataMember]
        public DateTime LastLogin; //CLIENT { get; private set; }
        [DataMember]
        public String LastLoginText; //CLIENT { get; private set; }
        [DataMember]
        public DateTime Created; //CLIENT { get; private set; }
        [DataMember]
        public String CreatedText; //CLIENT { get; private set; }
        [DataMember]
        public DateTime Edited; //CLIENT { get; private set; }
        [DataMember]
        public String EditedText; //CLIENT { get; private set; }
        [DataMember]
        public String Editor; //CLIENT { get; private set; }
        [DataMember]
        public Boolean Active; //CLIENT { get; set; }
    }

    [DataContract]
    public class UserRole
    {

        #region Initialize...
        public UserRole() { }
        public UserRole(
            Int32 Id
        ) {
            this.Id = Id;
        }
        public UserRole(
            Int32 Id,
            String Name,
            String Description
        ) {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
        }
        #endregion

        [DataMember]
        public Int32 Id; //CLIENT { get; private set; }
        [DataMember]
        public String Name; //CLIENT { get; private set; }
        [DataMember]
        public String Description; //CLIENT { get; private set; }
    }

    [DataContract]
    public class UserPassword
    {

        #region Initialize...
        public UserPassword() { }
        public UserPassword(
            String Old
        ) {
            this.Old = Old;
            this.New = String.Empty;
        }
        public UserPassword(
            String Old,
            String New
        ) {
            this.Old = Old;
            this.New = New;
        }
        #endregion

        [DataMember]
        public String Old; //CLIENT { get; set; }
        [DataMember]
        public String New; //CLIENT { get; set; }
    }
}
