// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDalManager.cs" company="">
//   
// </copyright>
// <summary>
//   The user dal manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.DAL
{
    using System;

    using LMS.Service.Domain;

    /// <summary>
    ///     The user dal manager.
    /// </summary>
    public class UserDalManager
    {
        /// <summary>
        /// The get user on login.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public User GetUserOnLogin(string login)
        {
            var _user = new User();

            // TODO return user from DB
            return _user;
        }

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="registerInfo">
        /// The register info.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public User CreateUser(RegisterInfo registerInfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The delete user.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void DeleteUser(string login)
        {
            throw new NotImplementedException();
        }
    }
}