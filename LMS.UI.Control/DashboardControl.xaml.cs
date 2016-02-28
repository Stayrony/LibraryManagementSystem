// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DashboardControl.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for DashboardControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LMS.UI.Control
{
    using System.Windows;
    using System.Windows.Controls;

    using LMS.UI.Contract;
    using LMS.ViewModel;

    /// <summary>
    /// Interaction logic for DashboardControl.xaml
    /// </summary>
    public partial class DashboardControl : UserControl, IView
    {
        /// <summary>
        /// Gets or sets the dashboard view model.
        /// </summary>
        public DashboardViewModel DashboardViewModel
        {
            get
            {
                return (DashboardViewModel)this.GetValue(DashboardViewModelyProperty);
            }

            set
            {
                this.SetValue(DashboardViewModelyProperty, value);
            }
        }

        /// <summary>
        /// The dashboard view modely property.
        /// </summary>
        public static readonly DependencyProperty DashboardViewModelyProperty =
            DependencyProperty.Register(
                "DashboardViewModel", 
                typeof(DashboardViewModel), 
                typeof(DashboardControl), 
                new UIPropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardControl"/> class.
        /// </summary>
        public DashboardControl()
        {
            this.DashboardViewModel = new DashboardViewModel(this);
            this.DataContext = this.DashboardViewModel;
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