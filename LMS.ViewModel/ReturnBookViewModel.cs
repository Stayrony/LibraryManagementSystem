// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturnBookViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The return book view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Windows;

namespace LMS.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using LMS.Service.BLL;
    using LMS.Service.Domain;
    using LMS.UI.Contract;
    using LMS.UI.Utility;

    /// <summary>
    /// The return book view model.
    /// </summary>
    public class ReturnBookViewModel : ViewModelBase
    {
        /// <summary>
        /// The view.
        /// </summary>
        private IView View;

        /// <summary>
        /// The return book command.
        /// </summary>
        private RelayCommand returnBookCommand;

        /// <summary>
        /// The book manager.
        /// </summary>
        private BookManager bookManager;

        /// <summary>
        /// The books issued by user.
        /// </summary>
        private ObservableCollection<Book> booksIssuedByUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnBookViewModel"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public ReturnBookViewModel(IView view)
        {
            try
            {
                this.View = view;
                this.bookManager = new BookManager();
                this.booksIssuedByUser = new ObservableCollection<Book>(this.bookManager.GetBooksIssuedByUserID());

                //searh box criteria
                searchCriteriaCollection = new List<string>();
                this.searchCriteriaCollection.Add("all");
                this.searchCriteriaCollection.Add("title");
                this.searchCriteriaCollection.Add("author");
                this.searchCriteriaCollection.Add("category");
                //end searh box criteria

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Gets the books issued by user.
        /// </summary>
        public ObservableCollection<Book> BooksIssuedByUser
        {
            get
            {
                return this.booksIssuedByUser;
            }
        }

        /// <summary>
        /// Gets the return book command.
        /// </summary>
        public ICommand ReturnBookCommand
        {
            get
            {
                if (this.returnBookCommand == null)
                {
                    this.returnBookCommand = new RelayCommand(param => this.ReturnBook(), param => CanReturn);
                }

                return this.returnBookCommand;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can return.
        /// </summary>
        public bool CanReturn
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
        /// The return book.
        /// </summary>
        private void ReturnBook()
        {
            try
            {
                // TODO Selected Book
                this.bookManager.ReturnBook(this.SelectedBook, 1);
                this.BooksIssuedByUser.Remove(this.SelectedBook);
            }
            catch (Exception exception)
            {
                this.View.ShowError(exception.Message);
            }
        }

        /// <summary>
        /// Gets or sets the selected book.
        /// </summary>
        public Book SelectedBook
        {
            get
            {
                return (Book)GetValue(SelectedBookProperty);
            }

            set
            {
                SetValue(SelectedBookProperty, value);
            }
        }

        /// <summary>
        /// The selected book property.
        /// </summary>
        public static readonly DependencyProperty SelectedBookProperty = DependencyProperty.Register(
            "SelectedBook", 
            typeof(Book), 
            typeof(ReturnBookViewModel), 
            new UIPropertyMetadata(null));

        #region Search Box


        /// <summary>
        /// The search criteria collection.
        /// </summary>
        private List<string> searchCriteriaCollection;

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

        #endregion Search Box
    }
}