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
            }
            catch (Exception exception)
            {
                throw exception;
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

            // TODO update quantity of issued book
        }

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
    }
}