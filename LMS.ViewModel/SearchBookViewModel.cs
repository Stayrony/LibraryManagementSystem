// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchBookViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The search book view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LMS.ViewModel
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;

    using LMS.Service.BLL;
    using LMS.Service.Domain;
    using LMS.UI.Utility;

    /// <summary>
    /// The search book view model.
    /// </summary>
    public abstract class SearchBookViewModel : DependencyObject
    {
        /// <summary>
        /// The search criteria collection.
        /// </summary>
        private readonly List<string> searchCriteriaCollection;

        /// <summary>
        /// The book manager.
        /// </summary>
        protected BookManager BookManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBookViewModel"/> class.
        /// </summary>
        public SearchBookViewModel()
        {
            this.BookManager = new BookManager();

            searchCriteriaCollection = new List<string>();
            this.searchCriteriaCollection.Add("all");
            this.searchCriteriaCollection.Add("title");
            this.searchCriteriaCollection.Add("author");
            this.searchCriteriaCollection.Add("category");
        }

        /// <summary>
        /// Gets the search book command.
        /// </summary>
        public abstract ICommand SearchBookCommand { get; }

        /// <summary>
        /// Gets or sets the searching criteria.
        /// </summary>
        public string SearchString
        {
            get
            {
                return (string)this.GetValue(SearchingCriteriaProperty);
            }

            set
            {
                this.SetValue(SearchingCriteriaProperty, value);
            }
        }

        /// <summary>
        /// The searching criteria property.
        /// </summary>
        public static readonly DependencyProperty SearchingCriteriaProperty = DependencyProperty.Register(
            "SearchString", 
            typeof(string), 
            typeof(IssueBookViewModel), 
            new UIPropertyMetadata(null));

        /// <summary>
        /// Gets the search criteria collection.
        /// </summary>
        public List<string> SearchCriteriaCollection
        {
            get
            {
                return this.searchCriteriaCollection;
            }
        }

        /// <summary>
        /// Gets or sets the selected search criteria.
        /// </summary>
        public string SelectedSearchCriteria
        {
            get
            {
                return (string)this.GetValue(SelectedSearchCriteriaProperty);
            }

            set
            {
                this.SetValue(SelectedSearchCriteriaProperty, value);
            }
        }

        /// <summary>
        /// The selected search criteria property.
        /// </summary>
        public static readonly DependencyProperty SelectedSearchCriteriaProperty =
            DependencyProperty.Register(
                "SelectedSearchCriteria", 
                typeof(string), 
                typeof(IssueBookViewModel), 
                new UIPropertyMetadata(null));
    }
}