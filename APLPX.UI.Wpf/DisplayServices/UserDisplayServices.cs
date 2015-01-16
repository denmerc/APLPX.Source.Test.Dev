using System;
using System.Collections.Generic;

using APLPX.Client.Contracts;
using APLPX.Entity;

namespace APLPX.UI.WPF.DisplayServices
{
    /// <summary>
    /// Wrapper class for calling IUserService methods from the display layer.
    /// </summary>
    public class UserDisplayServices
    {
        private readonly IUserService _userService;

        #region Constructors

        public UserDisplayServices(IUserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService", "userService cannot be null.");
            }

            _userService = userService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the application.
        /// </summary>
        /// <returns>A Session containing the resultsssss of the initialization.</returns>
        public Session<NullT> Initialize()
        {
            var payload = new NullT();
            var session = new Session<NullT> { Data = payload };

            //Session<NullT> response = _userService.Initialize(session);

            return null;// response;
        }

        /// <summary>
        /// Saves a <see cref="User"/> display entity.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Session<NullT> Login(Session<NullT> session, string login, string password)
        {
            Session<NullT> response = null;

            if (!session.SessionOk)
            {
                throw new ArgumentException("Session is not OK.");
            }
            if (session.AppOnline)
            {
                throw new ArgumentException("Application is not online.");
            }
            if (!session.SqlAuthorization)
            {
                throw new ArgumentException("Sql Authorization is not valid.");
            }

            //Build the User to pass into the Session.
            var credential = new UserCredential(login, password);            
            User userDto = new User(0, credential); //TODO: how handle user Id? CTOR requires it.            

            Session<NullT> sessionIn = new Session<NullT>();
            sessionIn.User = userDto;

            //response = _userService.Authenticate(sessionIn);

            return response;
        }


        public Session<NullT> SavePassword(Session<NullT> sessionDto)
        {
            Session<NullT> response = null;// _userService.SavePassword(sessionDto);

            return response;
        }

        #endregion


    }
}
