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
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl, IView
    {
        public RegisterViewModel RegisterViewModel
        {
            get { return (RegisterViewModel)GetValue(RegisterViewModelProperty); }
            set { SetValue(RegisterViewModelProperty, value); }
        }

        public static readonly DependencyProperty RegisterViewModelProperty =
           DependencyProperty.Register("RegisterViewModel", typeof(RegisterViewModel), typeof(RegisterControl), new UIPropertyMetadata(null));


        public RegisterControl()
        {
            this.RegisterViewModel = new RegisterViewModel(this);
            this.DataContext = RegisterViewModel;
            InitializeComponent();
            this.SetFocus();
        }

        public void SetFocus()
        {
            FocusManager.SetFocusedElement(this, LoginNameTxt);
        }

        public void ShowError(string error)
        {
            MessageBox.Show(error);
        }
    }
}
