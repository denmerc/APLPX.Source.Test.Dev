using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel;

using APLPX.Core;
namespace APLPX.Client.Entity
{
    [DataContract]
    public class User
    {
        #region Initialize...
        public User() { }
        public User(
            Int32 Id
            ) {
            this.Id = Id;
        }
        public User(
            Int32 Id,
            UserRole Role
            ) {
            this.Id = Id;
            this.Role = Role;
        }
        public User(
            Int32 Id,
            UserIdentity Identity
            ) {
            this.Id = Id;
            this.Identity = Identity;
        }
        public User(
            Int32 Id,
            UserIdentity Identity,
            List<Module> Modules
            ) {
            this.Id = Id;
            this.Identity = Identity;
            this.Modules = Modules;
        }
        public User(
            Int32 Id,
            List<Entity.SQLEnumeration> RoleTypes
            ) {
            this.Id = Id;
            this.RoleTypes = RoleTypes;
        }
        public User(
            Int32 Id,
            String SqlKey,
            UserRole Role,
            UserIdentity Identity
            ) {
            this.Id = Id;
            this.SqlKey = SqlKey;
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
            this.Id = Id;
            this.SqlKey = SqlKey;
            this.Role = Role;
            this.Identity = Identity;
            this.Modules = Modules;
            this.RoleTypes = RoleTypes;
        }
        #endregion

        [DataMember]
        public Int32 Id { get; private set; }
        [DataMember]
        public String SqlKey { get; private set; }
        [DataMember]
        public UserIdentity Identity { get; private set; }
        [DataMember]
        public UserRole Role { get; private set; }
        [DataMember]
        public List<Module> Modules { get; private set; } 
        [DataMember]
        public UserPassword Password { get; set; }
        [DataMember]
        public List<Entity.SQLEnumeration> RoleTypes { get; private set; }
    }

    [DataContract]
    public class UserIdentity
    {

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
        public String Login { get; set; }
        [DataMember]
        public String Email { get; set; }
        [DataMember]
        public String Name { get; private set; }
        [DataMember]
        public String FirstName { get; set; }
        [DataMember]
        public String LastName { get; set; }
        [DataMember]
        public String Greeting { get; private set; }
        [DataMember]
        public DateTime LastLogin { get; private set; }
        [DataMember]
        public String LastLoginText { get; private set; }
        [DataMember]
        public DateTime Created { get; private set; }
        [DataMember]
        public String CreatedText { get; private set; }
        [DataMember]
        public DateTime Edited { get; private set; }
        [DataMember]
        public String EditedText { get; private set; }
        [DataMember]
        public String Editor { get; private set; }
        [DataMember]
        public Boolean Active { get; set; }
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
        public Int32 Id { get; private set; }
        [DataMember]
        public String Name { get; private set; }
        [DataMember]
        public String Description { get; private set; }
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
        public String Old { get; set; }
        [DataMember]
        public String New { get; set; }
    }
}
