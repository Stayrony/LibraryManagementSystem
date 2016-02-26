// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateCategoryControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for CreateCategoryControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Practices.Prism.PubSubEvents;

namespace LMS.UI.Control
{
    using System.Windows;
    using System.Windows.Controls;

    using LMS.UI.Contract;
    using LMS.ViewModel;

    /// <summary>
    /// Interaction logic for CreateCategoryControl.xaml
    /// </summary>
    public partial class CreateCategoryControl : UserControl, IView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCategoryControl"/> class.
        /// </summary>
        public CreateCategoryControl()
        {
            this.CreateCategoryViewModel = new CreateCategoryViewModel(this, LMS.UI.Utility.EventAggregator.GetInstance());
            this.DataContext = this.CreateCategoryViewModel;
            this.InitializeComponent();
            this.SetFocus();
        }

        /// <summary>
        /// Gets or sets the create category view model.
        /// </summary>
        public CreateCategoryViewModel CreateCategoryViewModel
        {
            get
            {
                return (CreateCategoryViewModel)this.GetValue(CreateCategoryViewModelProperty);
            }

            set
            {
                this.SetValue(CreateCategoryViewModelProperty, value);
            }
        }

        /// <summary>
        /// The create category view model property.
        /// </summary>
        public static readonly DependencyProperty CreateCategoryViewModelProperty =
            DependencyProperty.Register(
                "CreateCategoryViewModel", 
                typeof(CreateCategoryViewModel), 
                typeof(CreateCategoryControl), 
                new UIPropertyMetadata(null));

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