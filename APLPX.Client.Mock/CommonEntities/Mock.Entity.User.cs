﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace APLPX.Common.Mock.Entity
{
    [BsonIgnoreExtraElements]
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
            string key,
            UserCredential credential
        ) {
            Key = key;
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

        [BsonId]
        public MongoDB.Bson.ObjectId _id { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Key { get;  set; }
        [DataMember]
        public UserCredential Credential { get; set; }
        [DataMember]
        public UserIdentity Identity { get;  set; }
        [DataMember]
        public UserRole Role { get;  set; }
        [DataMember]
        public List<Entity.SQLEnumeration> RoleTypes { get;  set; }
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
        public string Email { get; set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Greeting { get;  set; }
        [DataMember]
        public DateTime LastLogin { get;  set; }
        [DataMember]
        public string LastLoginText { get;  set; }
        [DataMember]
        public DateTime Created { get;  set; }
        [DataMember]
        public string CreatedText { get;  set; }
        [DataMember]
        public DateTime Edited { get;  set; }
        [DataMember]
        public string EditedText { get;  set; }
        [DataMember]
        public string Editor { get;  set; }
        [DataMember]
        public bool Active { get; set; }
    }

    [BsonNoId]
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
        public int Id { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Description { get;  set; }
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
            Login = login;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
        #endregion

        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string OldPassword { get; set; }
        [DataMember]
        public string NewPassword { get; set; }
    }
}
