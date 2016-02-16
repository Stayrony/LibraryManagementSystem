// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisterControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for RegisterControl.xaml
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
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl, IView
    {
        /// <summary>
        /// Gets or sets the register view model.
        /// </summary>
        public RegisterViewModel RegisterViewModel
        {
            get
            {
                return (RegisterViewModel)this.GetValue(RegisterViewModelProperty);
            }

            set
            {
                this.SetValue(RegisterViewModelProperty, value);
            }
        }

        /// <summary>
        /// The register view model property.
        /// </summary>
        public static readonly DependencyProperty RegisterViewModelProperty =
            DependencyProperty.Register(
                "RegisterViewModel",
                typeof(RegisterViewModel),
                typeof(RegisterControl),
                new UIPropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterControl"/> class.
        /// </summary>
        public RegisterControl()
        {
            this.RegisterViewModel = new RegisterViewModel(this);
            this.DataContext = this.RegisterViewModel;
            this.InitializeComponent();
            this.SetFocus();
        }

        /// <summary>
        /// The set focus.
        /// </summary>
        public void SetFocus()
        {
            FocusManager.SetFocusedElement(this, this.LoginNameTxt);
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