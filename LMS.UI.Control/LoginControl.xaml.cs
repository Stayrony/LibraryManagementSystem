// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for LoginControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LMS.UI.Contract;
using LMS.ViewModel;

namespace LMS.UI.Control
{
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
                return (LoginViewModel)GetValue(LoginViewModelProperty);
            }

            set
            {
                SetValue(LoginViewModelProperty, value);
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
            InitializeComponent();
            this.LoginViewModel = new LoginViewModel(this);
            this.DataContext = LoginViewModel;
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