// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueBookControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for IssueBookControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.UI.Control
{
    using System.Windows;
    using System.Windows.Controls;

    using LMS.UI.Contract;
    using LMS.ViewModel;

    /// <summary>
    /// Interaction logic for IssueBookControl.xaml
    /// </summary>
    public partial class IssueBookControl : UserControl, IView
    {
        /// <summary>
        /// Gets or sets the issue book view model.
        /// </summary>
        public IssueBookViewModel IssueBookViewModel
        {
            get
            {
                return (IssueBookViewModel)this.GetValue(IssueBookProperty);
            }

            set
            {
                this.SetValue(IssueBookProperty, value);
            }
        }

        /// <summary>
        /// The issue book property.
        /// </summary>
        public static readonly DependencyProperty IssueBookProperty = DependencyProperty.Register(
            "IssueBookViewModel", 
            typeof(IssueBookViewModel), 
            typeof(IssueBookControl), 
            new UIPropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueBookControl"/> class.
        /// </summary>
        public IssueBookControl()
        {
            this.IssueBookViewModel = new IssueBookViewModel(this, UI.Utility.EventAggregator.GetInstance());
            this.DataContext = this.IssueBookViewModel;
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