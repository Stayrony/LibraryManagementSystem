// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="">
//   
// </copyright>
// <summary>
//   The user manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LMS.Service.BLL
{
    using System;

    using LMS.DAL;
    using LMS.Service.Domain;

    /// <summary>
    ///     The user manager.
    /// </summary>
    public class UserManager
    {
        /// <summary>
        ///     The user dal manager.
        /// </summary>
        private readonly UserDalManager userDalManager = new UserDalManager();

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
            User user = this.userDalManager.GetUserOnLogin(login);
            return user;
        }

        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="loginInfo">
        /// The login Info.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public User EnterTheSystem(LoginInfo loginInfo)
        {
            User user = this.GetUserOnLogin(loginInfo.Login);

            if (user != null)
            {
                if (loginInfo.Password == user.Password)
                {
                    return user;
                }
                else
                {
                    throw new Exception("User : " + loginInfo.Login + " enter incorrect password.");
                }
            }
            else
            {
                throw new Exception("User " + loginInfo.Login + " not found.");
            }
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
        /// <exception cref="Exception">
        /// </exception>
        public User CreateUser(RegisterInfo registerInfo)
        {
            User newUser = new User();

            if (!this.IsAlreadyUserExist(registerInfo.Login))
            {
                if (this.Validate(registerInfo))
                {
                    newUser.Login = registerInfo.Login;
                    newUser.Password = registerInfo.Password;
                    newUser.TimeCreated = DateTime.Now;

                    newUser = this.userDalManager.CreateUser(newUser);
                }
            }
            else
            {
                throw new Exception("User " + registerInfo.Login + "already exist");
            }

            return newUser;
        }

        /// <summary>
        /// The delete user.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        public void DeleteUser(string login)
        {
            this.userDalManager.DeleteUser(login);
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="registerInfo">
        /// The register info.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool Validate(RegisterInfo registerInfo)
        {
            if (string.IsNullOrEmpty(registerInfo.Login) || string.IsNullOrEmpty(registerInfo.Password)
                || registerInfo.Password.Contains(" ") || registerInfo.Login.Contains(" "))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The is already user exist.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsAlreadyUserExist(string login)
        {
            User user = this.GetUserOnLogin(login);
            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}