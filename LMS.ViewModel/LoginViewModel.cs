// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The login view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LMS.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    using LMS.Service.BLL;
    using LMS.Service.Domain;
    using LMS.UI.Context;
    using LMS.UI.Contract;
    using LMS.UI.Utility;

    /// <summary>
    ///     The login view model.
    /// </summary>
    public class LoginViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        ///     The password property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
            "Password", 
            typeof(string), 
            typeof(LoginViewModel), 
            new UIPropertyMetadata(null));

        /// <summary>
        /// The login property.
        /// </summary>
        public static readonly DependencyProperty LoginProperty = DependencyProperty.Register(
            "LoginName", 
            typeof(string), 
            typeof(LoginViewModel), 
            new UIPropertyMetadata(null));

        /// <summary>
        ///     The view.
        /// </summary>
        private readonly IView View;

        /// <summary>
        ///     The login command.
        /// </summary>
        private RelayCommand loginCommand;

        /// <summary>
        ///     The login info user.
        /// </summary>
        private LoginInfo loginInfo;

        /// <summary>
        ///     The sign up command.
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

        /// <summary>
        /// Gets or sets the login name.
        /// </summary>
        public string LoginName
        {
            get
            {
                return (string)this.GetValue(LoginProperty);
            }

            set
            {
                this.SetValue(LoginProperty, value);
            }
        }

        /// <summary>
        /// Gets the login command.
        /// </summary>
        public ICommand LoginCommand
        {
            get
            {
                if (this.loginCommand == null)
                {
                    this.loginCommand = new RelayCommand(param => this.Login(), param => this.CanLogin);
                }

                return this.loginCommand;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can login.
        /// </summary>
        private bool CanLogin
        {
            get
            {
                return this.Validate;
            }
        }

        /// <summary>
        /// The login.
        /// </summary>
        private void Login()
        {
            try
            {
                this.loginInfo = new LoginInfo();
                this.loginInfo.Login = this.LoginName;
                this.loginInfo.Password = this.Password;

                UserManager userManager = new UserManager();
                User user = userManager.EnterTheSystem(this.loginInfo);

                ControlManager.GetInstance().Place("MainWindow", "mainRegion", "DashboardControl");
            }
            catch (Exception exception)
            {
                // TODO create Exception Base
                this.View.ShowError(exception.ToString());
            }
        }

        public ICommand SignUpLinkCommand
        {
            get
            {
                if (this.signUpCommand == null)
                {
                    this.signUpCommand = new RelayCommand(param => SignUpLink());
                }
                return signUpCommand;
            }
        }

        private void SignUpLink()
        {
           ControlManager.GetInstance().Place("MainWindow", "mainRegion", "RegisterControl");
        }

        #region Validation

        /// <summary>
        ///     The validated properties.
        /// </summary>
        private readonly List<string> ValidatedProperties = new List<string> { "LoginName", "Password" };

        /// <summary>
        ///     The validate.
        /// </summary>
        /// <param name="registerInfo">
        ///     The register info.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        private bool Validate
        {
            get
            {
                foreach (string property in this.ValidatedProperties)
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

        /// <summary>
        /// The validate password.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string ValidatePassword()
        {
            string error = null;
            if (string.IsNullOrEmpty(this.Password))
            {
                error = "Please, enter password.";
            }

            // if (this.Password.Contains(" "))
            // {
            // error = "Blank characters are not allowed in password.";
            // }
            return error;
        }

        /// <summary>
        /// The validate login.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string ValidateLogin()
        {
            string error = null;
            if (string.IsNullOrEmpty(this.LoginName))
            {
                error = "Please, enter login.";
            }

            // if (this.LoginName.Contains(" "))
            // {
            // error = "Blank characters are not allowed in login.";
            // }
            return error;
        }

        #endregion Validation

        #region IDataErrorInfo

        /// <summary>
        /// The i data error info.this.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string IDataErrorInfo.this[string property]
        {
            get
            {
                string error = this.GetValidationError(property);
                return error;
            }
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        string IDataErrorInfo.Error
        {
            get
            {
                return null;
            }
        }

        #endregion IDataErrorInfo
    }
}