// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveBookControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for  RemoveBookControl.xaml
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
    /// Interaction logic for  RemoveBookControl.xaml
    /// </summary>
    public partial class RemoveBookControl : UserControl, IView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveBookControl"/> class.
        /// </summary>
        public RemoveBookControl()
        {
            this.RemoveBookViewModel = new RemoveBookViewModel(this, Utility.EventAggregator.GetInstance());
            this.DataContext = this.RemoveBookViewModel;
            this.InitializeComponent();
            this.SetFocus();
        }

        /// <summary>
        /// Gets or sets the remove book view model.
        /// </summary>
        public RemoveBookViewModel RemoveBookViewModel
        {
            get
            {
                return (RemoveBookViewModel)this.GetValue(RemoveBookViewModelProperty);
            }

            set
            {
                this.SetValue(RemoveBookViewModelProperty, value);
            }
        }

        /// <summary>
        /// The remove book view model property.
        /// </summary>
        public static readonly DependencyProperty RemoveBookViewModelProperty =
            DependencyProperty.Register(
                "RemoveBookViewModel", 
                typeof(RemoveBookViewModel), 
                typeof(RemoveBookControl), 
                new UIPropertyMetadata(null));

        /// <summary>
        /// The set focus.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void SetFocus()
        {
           
        }

        /// <summary>
        /// The show error.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void ShowError(string error)
        {
            MessageBox.Show(error);
        }
    }
}