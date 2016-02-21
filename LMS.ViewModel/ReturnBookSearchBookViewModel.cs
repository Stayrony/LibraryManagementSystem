// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturnBookSearchBookViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The return book search book view model.
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
    /// The return book search book view model.
    /// </summary>
    public class ReturnBookSearchBookViewModel : SearchBookViewModel
    {
        /// <summary>
        /// The search return book command.
        /// </summary>
        private RelayCommand searchReturnBookCommand;

        /// <summary>
        /// The books.
        /// </summary>
        private List<Book> books;

        public ReturnBookSearchBookViewModel()
        {
            this.books = BookManager.GetBooksIssuedByUserID();
        }

        /// <summary>
        /// Gets the search book command.
        /// </summary>
        public override ICommand SearchBookCommand
        {
            get
            {
                if (this.searchReturnBookCommand == null)
                {
                    this.searchReturnBookCommand = new RelayCommand(param => this.SearchReturnBook());
                }

                return this.searchReturnBookCommand;
            }
        }

        /// <summary>
        /// The search return book.
        /// </summary>
        public void SearchReturnBook()
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

            this.books = base.BookManager.GetBooksBorrowedByUserBySearchCriteria(title, author, category);
        }

        /// <summary>
        /// Gets the books available for issue.
        /// </summary>
        public ObservableCollection<Book> BooksBorrowedByUser
        {
            get
            {
                return new ObservableCollection<Book>(this.books);
            }
        }
    }
}