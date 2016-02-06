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


        public LoginViewModel LoginViewModel
        {
            get { return (LoginViewModel)GetValue(LoginViewModelProperty); }
            set { SetValue(LoginViewModelProperty, value); }
        }

        public static readonly DependencyProperty LoginViewModelProperty =
           DependencyProperty.Register("LoginViewModel", typeof(LoginViewModel), typeof(LoginControl), new UIPropertyMetadata(null));



        public LoginControl()
        {
            InitializeComponent();
            this.LoginViewModel = new LoginViewModel(this);
            this.DataContext = LoginViewModel;
            this.SetFocus();
        }

        public void SetFocus()
        {

        }

        public void ShowError(string error)
        {
            MessageBox.Show(error);
        }
    }
}
