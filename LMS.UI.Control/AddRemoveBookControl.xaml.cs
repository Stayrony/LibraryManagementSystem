// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddRemoveBookControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Логика взаимодействия для AddRemoveBookControl.xaml
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

namespace LMS.UI.Control
{
    using LMS.UI.Contract;
    using LMS.ViewModel;

    /// <summary>
    /// Логика взаимодействия для AddRemoveBookControl.xaml
    /// </summary>
    public partial class AddRemoveBookControl : UserControl, IView
    {
        /// <summary>
        /// Gets or sets the add remove book view model.
        /// </summary>
        public AddRemoveBookViewModel AddRemoveBookViewModel
        {
            get
            {
                return (AddRemoveBookViewModel)this.GetValue(AddRemoveBookProperty);
            }

            set
            {
                this.SetValue(AddRemoveBookProperty, value);
            }
        }

        /// <summary>
        /// The add remove book property.
        /// </summary>
        public static readonly DependencyProperty AddRemoveBookProperty =
            DependencyProperty.Register(
                "AddRemoveBookViewModel", 
                typeof(AddRemoveBookViewModel), 
                typeof(AddRemoveBookControl), 
                new UIPropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRemoveBookControl"/> class.
        /// </summary>
        public AddRemoveBookControl()
        {
            this.AddRemoveBookViewModel = new AddRemoveBookViewModel(this, LMS.UI.Utility.EventAggregator.GetInstance());
            this.DataContext = this.AddRemoveBookViewModel;
            this.InitializeComponent();
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