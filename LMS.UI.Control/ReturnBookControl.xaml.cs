// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturnBookControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for ReturnBookControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LMS.UI.Control
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using LMS.UI.Contract;
    using LMS.ViewModel;

    /// <summary>
    /// Interaction logic for ReturnBookControl.xaml
    /// </summary>
    public partial class ReturnBookControl : UserControl, IView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnBookControl"/> class.
        /// </summary>
        public ReturnBookControl()
        {
            this.ReturnBookViewModel = new ReturnBookViewModel(this);
            this.DataContext = this.ReturnBookViewModel;
            this.InitializeComponent();
            this.SetFocus();
        }

        /// <summary>
        /// Gets or sets the return book view model.
        /// </summary>
        public ReturnBookViewModel ReturnBookViewModel
        {
            get
            {
                return (ReturnBookViewModel)this.GetValue(ReturnBookViewModelProperty);
            }

            set
            {
                this.SetValue(ReturnBookViewModelProperty, value);
            }
        }

        /// <summary>
        /// The return book view model property.
        /// </summary>
        public static readonly DependencyProperty ReturnBookViewModelProperty =
            DependencyProperty.Register(
                "ReturnBookViewModel", 
                typeof(ReturnBookViewModel), 
                typeof(ReturnBookControl), 
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
        public void ShowError(string error)
        {
            MessageBox.Show(error);
        }
    }
}