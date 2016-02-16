// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The register view model.
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
    /// The register view model.
    /// </summary>
    public class RegisterViewModel : ViewModelBase, IDataErrorInfo
    {
        /// <summary>
        /// The view.
        /// </summary>
        private IView View;

        /// <summary>
        /// The sign up command reg.
        /// </summary>
        private RelayCommand signUpCommandReg;

        /// <summary>
        /// The login link command reg.
        /// </summary>
        private RelayCommand loginLinkCommandReg;

        /// <summary>
        /// The register info.
        /// </summary>
        private RegisterInfo registerInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterViewModel"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public RegisterViewModel(IView view)
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
        /// Gets or sets the login name reg.
        /// </summary>
        public string LoginNameReg
        {
            get
            {
                return (string)this.GetValue(LoginNameRegProperty);
            }

            set
            {
                this.SetValue(LoginNameRegProperty, value);
            }
        }

        /// <summary>
        /// The login name reg property.
        /// </summary>
        public static readonly DependencyProperty LoginNameRegProperty = DependencyProperty.Register(
            "LoginNameReg",
            typeof(string),
            typeof(RegisterViewModel),
            new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the password reg.
        /// </summary>
        public string PasswordReg
        {
            get
            {
                return (string)this.GetValue(PasswordRegProperty);
            }

            set
            {
                this.SetValue(PasswordRegProperty, value);
            }
        }

        /// <summary>
        /// The password reg property.
        /// </summary>
        public static readonly DependencyProperty PasswordRegProperty = DependencyProperty.Register(
            "PasswordReg",
            typeof(string),
            typeof(RegisterViewModel),
            new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the confirm password reg.
        /// </summary>
        public string ConfirmPasswordReg
        {
            get
            {
                return (string)this.GetValue(ConfirmPasswordRegProperty);
            }

            set
            {
                this.SetValue(ConfirmPasswordRegProperty, value);
            }
        }

        /// <summary>
        /// The confirm password reg property.
        /// </summary>
        public static readonly DependencyProperty ConfirmPasswordRegProperty =
            DependencyProperty.Register(
                "ConfirmPasswordReg",
                typeof(string),
                typeof(RegisterViewModel),
                new UIPropertyMetadata(null));

        /// <summary>
        /// Gets the sign up command reg.
        /// </summary>
        public ICommand SignUpCommandReg
        {
            get
            {
                if (this.signUpCommandReg == null)
                {
                    this.signUpCommandReg = new RelayCommand(param => this.SignUp(), param => this.CanSignUp);
                }

                return this.signUpCommandReg;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can sign up.
        /// </summary>
        public bool CanSignUp
        {
            get
            {
                return this.IsValig;
            }
        }

        /// <summary>
        /// The sign up.
        /// </summary>
        private void SignUp()
        {
            try
            {
                this.registerInfo = new RegisterInfo();
                this.registerInfo.Login = this.LoginNameReg;
                this.registerInfo.Password = this.PasswordReg;
                UserManager userManager = new UserManager();
                userManager.CreateUser(this.registerInfo);
                ControlManager.GetInstance().Place("MainWindow", "mainRegion", "DashboardControl");
            }
            catch (Exception exception)
            {
                var message = exception.Message;
                this.View.ShowError(message);
            }
        }

        /// <summary>
        /// Gets the login link command reg.
        /// </summary>
        public ICommand LoginLinkCommandReg
        {
            get
            {
                if (this.loginLinkCommandReg == null)
                {
                    this.loginLinkCommandReg = new RelayCommand(param => this.LoginLink());
                }

                return this.loginLinkCommandReg;
            }
        }

        /// <summary>
        /// The login link.
        /// </summary>
        /// <exception cref="Exception">
        /// </exception>
        private void LoginLink()
        {
            try
            {
                ControlManager.GetInstance().Place("MainWindow", "mainRegion", "LoginControl");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #region Validate

        /// <summary>
        /// Gets a value indicating whether is valig.
        /// </summary>
        public bool IsValig
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
                case "LoginNameReg":
                    error = this.ValidateLoginName();
                    break;
                case "PasswordReg":
                    error = this.ValidatePassword();
                    break;
                case "ConfirmPasswordReg":
                    error = this.ValidateConfirmPassword();
                    break;
            }

            return error;
        }

        /// <summary>
        /// The validate confirm password.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string ValidateConfirmPassword()
        {
            string error = null;
            if (string.IsNullOrEmpty(this.ConfirmPasswordReg))
            {
                error = "Please, enter confirm password.";
            }
            else if (this.ConfirmPasswordReg.Contains(" "))
            {
                error = "Blank characters are not allowed in password.";
            }
            else if (this.PasswordReg != this.ConfirmPasswordReg)
            {
                error = "Password does not match the confirm password.";
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
            if (string.IsNullOrEmpty(this.PasswordReg))
            {
                error = "Please, enter password.";
            }
            else if (this.PasswordReg.Contains(" "))
            {
                error = "Blank characters are not allowed in password.";
            }

            return error;
        }

        /// <summary>
        /// The validate login name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string ValidateLoginName()
        {
            string error = null;
            if (string.IsNullOrEmpty(this.LoginNameReg))
            {
                error = "Please, enter login.";
            }
            else if (this.LoginNameReg.Contains(" "))
            {
                error = "Blank characters are not allowed in login.";
            }

            return error;
        }

        /// <summary>
        /// The validated properties.
        /// </summary>
        private readonly List<string> ValidatedProperties = new List<string>
                                                                {
                                                                    "LoginNameReg",
                                                                    "PasswordReg",
                                                                    "ConfirmPasswordReg"
                                                                };

        #endregion Validate

        #region IDataErrorInfo Members

        /// <summary>
        /// The i data error info.this.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return this.GetValidationError(propertyName);
            }
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        public string Error { get; }

        #endregion IDataErrorInfo Members
    }
}