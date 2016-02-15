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

namespace LMS.ViewModel
{
    public class RegisterViewModel : ViewModelBase, IDataErrorInfo
    {
        private IView View;
        private RelayCommand signUpCommandReg;
        private RelayCommand loginLinkCommandReg;
        private RegisterInfo registerInfo;

        public RegisterViewModel(IView view)
        {
            try
            {
                View = view;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }



        public string LoginNameReg
        {
            get { return (string)GetValue(LoginNameRegProperty); }
            set { SetValue(LoginNameRegProperty, value); }
        }

        public static readonly DependencyProperty LoginNameRegProperty =
           DependencyProperty.Register("LoginNameReg", typeof(string), typeof(RegisterViewModel), new UIPropertyMetadata(null));



        public string PasswordReg
        {
            get { return (string)GetValue(PasswordRegProperty); }
            set { SetValue(PasswordRegProperty, value); }
        }

        public static readonly DependencyProperty PasswordRegProperty =
        DependencyProperty.Register("PasswordReg", typeof(string), typeof(RegisterViewModel), new UIPropertyMetadata(null));



        public string ConfirmPasswordReg
        {
            get { return (string)GetValue(ConfirmPasswordRegProperty); }
            set { SetValue(ConfirmPasswordRegProperty, value); }
        }

        public static readonly DependencyProperty ConfirmPasswordRegProperty =
            DependencyProperty.Register("ConfirmPasswordReg", typeof(string), typeof(RegisterViewModel), new UIPropertyMetadata(null));

        public ICommand SignUpCommandReg
        {
            get
            {
                if (this.signUpCommandReg == null)
                {
                    this.signUpCommandReg = new RelayCommand(param => SignUp(), param => CanSignUp);
                }
                return signUpCommandReg;
            }
        }

        public bool CanSignUp
        {
            get { return this.IsValig; }
        }

        private void SignUp()
        {
            try
            {
                this.registerInfo = new RegisterInfo();
                registerInfo.Login = this.LoginNameReg;
                registerInfo.Password = this.PasswordReg;
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

        public ICommand LoginLinkCommandReg
        {
            get
            {
                if (this.loginLinkCommandReg == null)
                {
                    this.loginLinkCommandReg = new RelayCommand(param => LoginLink());
                }
                return loginLinkCommandReg;
            }
        }

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
        public bool IsValig
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    if (this.GetValidationError(property) != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

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

        private readonly List<string> ValidatedProperties = new List<string>
        {
            "LoginNameReg",
            "PasswordReg",
            "ConfirmPasswordReg"

        };

        #endregion Validate

        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        public string Error { get; }

        #endregion IDataErrorInfo Members
    }
}