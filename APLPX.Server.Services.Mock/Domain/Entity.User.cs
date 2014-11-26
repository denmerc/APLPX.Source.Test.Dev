using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Mock.Entity
{
    [DataContract]
    public class User
    {
        #region Initialize...
        public User() { }
        public User(
            int id
        ) {
            Id = id;
        }
        public User(
            UserCredential credential
        ) {
            Credential = credential;
        }
        public User(
            int id,
            UserCredential credential
        ) {
            Id = id;
            Credential = credential;
        }
        public User(
            int id,
            UserRole role
        ) {
            Id = id;
            Role = role;
        }
        public User(
            int id,
            UserIdentity identity
        ) {
            Id = id;
            Identity = identity;
        }
        public User(
            int id,
            List<Entity.SQLEnumeration> roleTypes
        ) {
            Id = id;
            RoleTypes = roleTypes;
        }
        public User(
            int id,
            string key,
            UserRole role,
            UserIdentity identity
        ) {
            Id = id;
            Key = key;
            Role = role;
            Identity = identity;
        }
        public User(
            int id,
            string key,
            UserRole role,
            UserIdentity identity,
            UserCredential credential,
            List<Entity.SQLEnumeration> roleTypes
        ) {
            Id = id;
            Key = key;
            Role = role;
            Identity = identity;
            Credential = credential;
            RoleTypes = roleTypes;
        }
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public string Key; //CLIENT { get; private set; }
        [DataMember]
        public UserCredential Credential; //CLIENT { get; private set; }
        [DataMember]
        public UserIdentity Identity; //CLIENT { get; private set; }
        [DataMember]
        public UserRole Role;  //CLIENT { get; private set; }
        [DataMember]
        public List<Entity.SQLEnumeration> RoleTypes; //CLIENT { get; private set; }
    }

    [DataContract]
    public class UserIdentity
    {
        #region Initialize...
        public UserIdentity() { }
        public UserIdentity(
            string email,
            string firstName,
            string lastName,
            bool active
        ) {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Active = active;
        }
        public UserIdentity(
            string email,
            string greeting,
            string name,
            string firstName,
            string lastName,
            string lastLoginText,
            string createdText,
            string editedText,
            DateTime lastLogin,
            DateTime created,
            DateTime edited,
            string editor,
            bool active
        ) {
            Email = email;
            Greeting = greeting;
            Name = name;
            FirstName = firstName;
            LastName = lastName;
            LastLoginText = lastLoginText;
            CreatedText = createdText;
            EditedText = editedText;
            LastLogin = lastLogin;
            Created = created;
            Edited = edited;
            Editor = editor;
            Active = active;
        }
        #endregion

        [DataMember]
        public string Email; //CLIENT { get; set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string FirstName; //CLIENT { get; set; }
        [DataMember]
        public string LastName; //CLIENT { get; set; }
        [DataMember]
        public string Greeting; //CLIENT { get; private set; }
        [DataMember]
        public DateTime LastLogin; //CLIENT { get; private set; }
        [DataMember]
        public string LastLoginText; //CLIENT { get; private set; }
        [DataMember]
        public DateTime Created; //CLIENT { get; private set; }
        [DataMember]
        public string CreatedText; //CLIENT { get; private set; }
        [DataMember]
        public DateTime Edited; //CLIENT { get; private set; }
        [DataMember]
        public string EditedText; //CLIENT { get; private set; }
        [DataMember]
        public string Editor; //CLIENT { get; private set; }
        [DataMember]
        public bool Active; //CLIENT { get; set; }
    }

    [DataContract]
    public class UserRole
    {
        #region Initialize...
        public UserRole() { }
        public UserRole(
            int id
        ) {
            Id = id;
        }
        public UserRole(
            int id,
            string name,
            string description
        ) {
            Id = id;
            Name = name;
            Description = description;
        }
        #endregion

        [DataMember]
        public int Id; //CLIENT { get; private set; }
        [DataMember]
        public string Name; //CLIENT { get; private set; }
        [DataMember]
        public string Description; //CLIENT { get; private set; }
    }

    [DataContract]
    public class UserCredential
    {
        #region Initialize...
        public UserCredential() { }
        public UserCredential(
            string login,
            string oldPassword
        ) {
            Login = login;
            OldPassword = oldPassword;
            NewPassword = string.Empty;
        }
        public UserCredential(
            string login,
            string oldPassword,
            string newPassword
        ) {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
        #endregion

        [DataMember]
        public string Login; //CLIENT { get; set; }
        [DataMember]
        public string OldPassword; //CLIENT { get; set; }
        [DataMember]
        public string NewPassword; //CLIENT { get; set; }
    }
}
