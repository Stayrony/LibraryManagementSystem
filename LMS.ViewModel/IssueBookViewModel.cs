// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueBookViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The issue book view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    using LMS.Service.BLL;
    using LMS.Service.Domain;
    using LMS.UI.Contract;
    using LMS.UI.Utility;

    /// <summary>
    /// The issue book view model.
    /// </summary>
    public class IssueBookViewModel : ViewModelBase
    {
        /// <summary>
        /// The view.
        /// </summary>
        private IView View;

        /// <summary>
        /// The issue book command.
        /// </summary>
        private RelayCommand issueBookCommand;

        /// <summary>
        /// The search book command.
        /// </summary>
        private RelayCommand searchBookCommand;

        /// <summary>
        /// The book manager.
        /// </summary>
        private BookManager bookManager;

        /// <summary>
        /// The books.
        /// </summary>
        private List<Book> books;


        /// <summary>
        /// The search criteria collection.
        /// </summary>
        private List<string> searchCriteriaCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueBookViewModel"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public IssueBookViewModel(IView view)
        {

            try
            {
                this.View = view;
                this.bookManager = new BookManager();
                this.books = new List<Book>();
                this.books = this.bookManager.GetAllBooks();
                searchCriteriaCollection = new List<string>();
                this.searchCriteriaCollection.Add("all");
                this.searchCriteriaCollection.Add("title");
                this.searchCriteriaCollection.Add("author");
                this.searchCriteriaCollection.Add("category");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Gets the books available for issue.
        /// </summary>
        public ObservableCollection<Book> BooksAvailableForIssue
        {
            get
            {
                return new ObservableCollection<Book>(this.books);
            }
        }

        /// <summary>
        /// Gets the issue book command.
        /// </summary>
        public ICommand IssueBookCommand
        {
            get
            {
                if (this.issueBookCommand == null)
                {
                    this.issueBookCommand = new RelayCommand(param => this.IssueBook(), param => this.CanIssueBook);
                }

                return this.issueBookCommand;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can issue book.
        /// </summary>
        public bool CanIssueBook
        {
            get
            {
                // check is Selected book
                if (this.SelectedBook != null)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// The issue book.
        /// </summary>
        private void IssueBook()
        {
            this.bookManager.IssueBook(this.SelectedBook, 1);
            //TODO update quantity of issued book
        }

        /// <summary>
        /// Gets the search book command.
        /// </summary>
        public ICommand SearchBookCommand
        {
            get
            {
                if (this.searchBookCommand == null)
                {
                    this.searchBookCommand = new RelayCommand(param => this.SearchBook());
                }

                return this.searchBookCommand;
            }
        }

        /// <summary>
        /// The search book.
        /// </summary>
        private void SearchBook()
        {
            // TODO search book

            // SelectedSearchCriteria
            //TODO update observable collection book
            string title = string.Empty;
            string author = string.Empty;
            string category = string.Empty;

            switch (this.SelectedSearchCriteria)
            {
                case "all":
                    break;
                case "title":
                    title = this.SearchString;
                    break;
                case "author":
                    author = this.SearchString;
                    break;
                case "category":
                    category = this.SearchString;
                    break;
            }

            
            this.books = this.bookManager.GetBooksBySearchCriteria(title, author, category);
            //   CollectionViewSource.GetDefaultView()
            // BooksAvailableForIssue.

        }

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
        public static readonly DependencyProperty SearchingCriteriaProperty =
            DependencyProperty.Register(
                "SearchString",
                typeof(string),
                typeof(IssueBookViewModel),
                new UIPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the selected book.
        /// </summary>
        public Book SelectedBook
        {
            get
            {
                return (Book)this.GetValue(SelectedBookProperty);
            }

            set
            {
                this.SetValue(SelectedBookProperty, value);
            }
        }

        /// <summary>
        /// The selected book property.
        /// </summary>
        public static readonly DependencyProperty SelectedBookProperty = DependencyProperty.Register(
            "SelectedBook",
            typeof(Book),
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
            DependencyProperty.Register("SelectedSearchCriteria",
            typeof(string), typeof(IssueBookViewModel),
            new UIPropertyMetadata(null));



    }
}