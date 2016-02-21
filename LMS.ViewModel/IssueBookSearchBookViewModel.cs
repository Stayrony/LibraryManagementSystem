// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueBookSearchBookViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The issue book search book view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
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


        public IssueBookSearchBookViewModel()
        {
            this.books = base.BookManager.GetAllBooks();
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
            string title = string.Empty;
            string author = string.Empty;
            string category = string.Empty;

            switch (this.SelectedSearchCriteria)
            {
                case "all":
                    break;
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
           // this.books = base.BookManager.GetAllBooks();
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
    }
}