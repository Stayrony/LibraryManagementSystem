// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for LoginControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.UI.Control
{
    using System.Windows;
    using System.Windows.Controls;

    using LMS.UI.Contract;
    using LMS.ViewModel;

    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl, IView
    {
        /// <summary>
        /// Gets or sets the login view model.
        /// </summary>
        public LoginViewModel LoginViewModel
        {
            get
            {
                return (LoginViewModel)this.GetValue(LoginViewModelProperty);
            }

            set
            {
                this.SetValue(LoginViewModelProperty, value);
            }
        }

        /// <summary>
        /// The login view model property.
        /// </summary>
        public static readonly DependencyProperty LoginViewModelProperty = DependencyProperty.Register(
            "LoginViewModel", 
            typeof(LoginViewModel), 
            typeof(LoginControl), 
            new UIPropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginControl"/> class.
        /// </summary>
        public LoginControl()
        {
            this.InitializeComponent();
            this.LoginViewModel = new LoginViewModel(this);
            this.DataContext = this.LoginViewModel;
            this.SetFocus();
        }

        /// <summary>
        /// The set focus.
        /// </summary>
        public void SetFocus()
        {
        }

        /// <summary>
        /// The show error.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        public void ShowError(string error)
        {
            MessageBox.Show(error);
        }
    }
}