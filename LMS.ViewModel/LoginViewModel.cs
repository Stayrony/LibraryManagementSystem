// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The login view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.Windows.Input;
using LMS.Service.BLL;
using LMS.Service.Domain;
using LMS.UI.Context;
using LMS.UI.Contract;
using LMS.UI.Utility;

namespace LMS.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    ///     The login view model.
    /// </summary>
    public class LoginViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// The view.
        /// </summary>
        private IView View;

        /// <summary>
        /// The login info user.
        /// </summary>
        private LoginInfo loginInfo;

        /// <summary>
        /// The login command.
        /// </summary>
        private RelayCommand loginCommand;

        /// <summary>
        /// The sign up command.
        /// </summary>
        private RelayCommand signUpCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public LoginViewModel(IView view)
        {
            try
            {
                this.View = view;
            }
            catch (Exception exception)
            {
                
                throw exception;
            }
        }

        /// <summary>
        ///     The password property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
            "Password",
            typeof(string),
            typeof(LoginViewModel),
            new UIPropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        public string Password
        {
            get
            {
                return (string)this.GetValue(PasswordProperty);
            }

            set
            {
                this.SetValue(PasswordProperty, value);
            }
        }



        public string LoginName
        {
            get { return (string)GetValue(LoginProperty); }
            set { SetValue(LoginProperty, value); }
        }

        public static readonly DependencyProperty LoginProperty =
           DependencyProperty.Register("LoginName", typeof(string), typeof(LoginViewModel), new UIPropertyMetadata(null));


        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    this.loginCommand = new RelayCommand(param => Login(), param => CanLogin);
                }
                return this.loginCommand;
            }
        }

        private bool CanLogin
        {
            get
            {
                return this.Validate;

            }
        }

        private void Login()
        {
            try
            {
                loginInfo = new LoginInfo();
                loginInfo.Login = this.LoginName;
                loginInfo.Password = this.Password;

                var userManager = new UserManager();
                User user = userManager.EnterTheSystem(loginInfo);

                ControlManager.GetInstance().Place("MainWindow", "mainRegion", "DashboardControl");

            }
            catch (Exception exception)
            {
                //TODO create Exception Base
               this.View.ShowError(exception.ToString());
            }
        }

        #region Validation
        /// <summary>
        /// The validated properties.
        /// </summary>
        private readonly List<string> ValidatedProperties = new List<string> { "LoginName", "Password" };

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="registerInfo">
        /// The register info.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool Validate
        {
            get
            {
                foreach (var property in this.ValidatedProperties)
                {
                    if (this.GetValidationError(property) != null)
                    {
                        return false;
                    }
                }

                return true;
            }

        }

        /// <summary>
        /// The get validation error.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetValidationError(string property)
        {
            string error = null;
            switch (property)
            {
                case "LoginName":
                    error = this.ValidateLogin();
                    break;
                case "Password":
                    error = this.ValidatePassword();
                    break;
                default:
                    break;
            }

            return error;
        }

        private string ValidatePassword()
        {
            string error = null;
            if (string.IsNullOrEmpty(this.Password))
            {
                error = "Please, enter password.";
            }

            //if (this.Password.Contains(" "))
            //{
            //    error = "Blank characters are not allowed in password.";
            //}
            return error;
        }

        private string ValidateLogin()
        {
            string error = null;
            if (string.IsNullOrEmpty(this.LoginName))
            {
                error = "Please, enter login.";
            }
            //if (this.LoginName.Contains(" "))
            //{
            //    error = "Blank characters are not allowed in login.";
            //}
            return error;
        }

        #endregion Validation

        #region IDataErrorInfo
        string IDataErrorInfo.this[string property]
        {
            get
            {
                string error = this.GetValidationError(property);
                return error;
            }
        }

        string IDataErrorInfo.Error
        {
            get { return null; }

        }

        #endregion IDataErrorInfo
    }
}