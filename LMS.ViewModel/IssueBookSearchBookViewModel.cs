// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueBookSearchBookViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The issue book search book view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Practices.Prism.PubSubEvents;

namespace LMS.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using LMS.Service.BLL;
    using LMS.Service.Domain;
    using LMS.UI.Utility;

    /// <summary>
    /// The issue book search book view model.
    /// </summary>
    public class IssueBookSearchBookViewModel : SearchBookViewModel
    {
        /// <summary>
        /// The search issue book command.
        /// </summary>
        private RelayCommand searchIssueBookCommand;

        /// <summary>
        /// The books.
        /// </summary>
        private List<Book> books;

        /// <summary>
        /// The _event aggregator.
        /// </summary>
        private IEventAggregator _eventAggregator;

        // public IssueBookSearchBookViewModel(IEventAggregator eventAggregator)
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueBookSearchBookViewModel"/> class.
        /// </summary>
        public IssueBookSearchBookViewModel()
        {
            this.books = base.BookManager.GetAllBooks();
            this._eventAggregator = LMS.UI.Utility.EventAggregator.GetInstance();
        }

        /// <summary>
        /// Gets the search book command.
        /// </summary>
        public override ICommand SearchBookCommand
        {
            get
            {
                if (this.searchIssueBookCommand == null)
                {
                    this.searchIssueBookCommand = new RelayCommand(param => this.SearchIssueBook());
                }

                return this.searchIssueBookCommand;
            }
        }

        /// <summary>
        /// The search issue book.
        /// </summary>
        public void SearchIssueBook()
        {
            if (string.IsNullOrEmpty(base.SearchString))
            {
                this.books = base.BookManager.GetAllBooks();
            }
            else
            {
                string title = string.Empty;
                string author = string.Empty;
                string category = string.Empty;

                switch (this.SelectedSearchCriteria)
                {
                    case "all":
                        {
                            title = base.SearchString;
                            author = base.SearchString;
                            category = base.SearchString;
                            break;
                        }

                    case "title":
                        title = base.SearchString;
                        break;
                    case "author":
                        author = base.SearchString;
                        break;
                    case "category":
                        category = base.SearchString;
                        break;
                }

                this.books = base.BookManager.GetAvailableBooksBySearchCriteria(title, author, category);
            }

            _eventAggregator.GetEvent<AvailableBooksEvent>().Publish(books);

            // BooksAvailableForIssue.Clear();
            // this.books = base.BookManager.GetAllBooks();
        }

        ///// <summary>
        ///// Gets the books available for issue.
        ///// </summary>
        // public ObservableCollection<Book> BooksAvailableForIssue
        // {
        // get
        // {
        // return new ObservableCollection<Book>(this.books);
        // }
        // }
    }
}