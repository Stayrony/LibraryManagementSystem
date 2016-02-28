// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveBookViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   The remove book view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LMS.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    using LMS.Service.BLL;
    using LMS.Service.Domain;
    using LMS.UI.Contract;
    using LMS.UI.Utility;

    using Microsoft.Practices.Prism.PubSubEvents;

    /// <summary>
    /// The remove book view model.
    /// </summary>
    public class RemoveBookViewModel : ViewModelBase
    {
        /// <summary>
        /// The event aggregator.
        /// </summary>
        private IEventAggregator _eventAggregator;

        /// <summary>
        /// The remove book command.
        /// </summary>
        private RelayCommand removeBookCommand;

        /// <summary>
        /// The view.
        /// </summary>
        private IView View;

        /// <summary>
        /// The books.
        /// </summary>
        private ObservableCollection<Book> books;

        /// <summary>
        /// The book manager.
        /// </summary>
        private BookManager bookManager;

        /// <summary>
        /// The subscription token for available books.
        /// </summary>
        private SubscriptionToken subscriptionTokenForAvailableBooks;

        /// <summary>
        /// The subscription token for add book.
        /// </summary>
        private SubscriptionToken subscriptionTokenForAddBook;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveBookViewModel"/> class.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <param name="eventAggregator">
        /// The event aggregator.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public RemoveBookViewModel(IView view, IEventAggregator eventAggregator)
        {
            try
            {
                this._eventAggregator = eventAggregator;
                this.View = view;
                this.bookManager = new BookManager();
                this.books = new ObservableCollection<Book>(this.bookManager.GetAllBooks());

                AvailableBooksEvent updatedAvailableBooksEvent = _eventAggregator.GetEvent<AvailableBooksEvent>();
                subscriptionTokenForAvailableBooks =
                    updatedAvailableBooksEvent.Subscribe(
                        UpdateAvailableBooksEventHandler,
                        ThreadOption.UIThread,
                        false,
                        (updatedBooks) => true);

                CreateBookEvent bookAddedEvent = _eventAggregator.GetEvent<CreateBookEvent>();
                subscriptionTokenForAddBook = bookAddedEvent.Subscribe(
                    AddNewBookEventHandler,
                    ThreadOption.UIThread,
                    false,
                    (book) => true);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// The add new book event handler.
        /// </summary>
        /// <param name="book">
        /// The book.
        /// </param>
        private void AddNewBookEventHandler(Book book)
        {
            Books.Add(book);
        }

        /// <summary>
        /// The update available books event handler.
        /// </summary>
        /// <param name="updatedBooks">
        /// The updated books.
        /// </param>
        private void UpdateAvailableBooksEventHandler(List<Book> updatedBooks)
        {
            this.Books.Clear();
            foreach (Book book in updatedBooks)
            {
                Books.Add(book);
            }
        }

        /// <summary>
        /// Gets the remove book command.
        /// </summary>
        public ICommand RemoveBookCommand
        {
            get
            {
                if (this.removeBookCommand == null)
                {
                    this.removeBookCommand = new RelayCommand(param => this.RemoveBook(), param => CanRemoveBook);
                }

                return this.removeBookCommand;
            }
        }

        /// <summary>
        /// The remove book.
        /// </summary>
        private void RemoveBook()
        {
            try
            {
                if (this.RemoveQuantity > this.SelectedBook.QuantityOfBooksIssued)
                {
                    throw new Exception("You try to remove more books than available");
                }
                else if (this.RemoveQuantity == this.SelectedBook.QuantityOfBooksIssued)
                {
                    this.Books.Remove(this.SelectedBook);
                    this.bookManager.DeleteBook(this.SelectedBook, this.RemoveQuantity);
                }
                else
                {
                    //TODO Update QuantityOfBooksIssued
                    this.bookManager.DeleteBook(this.SelectedBook, this.RemoveQuantity);
                }
            }
            catch (Exception exception)
            {
                // TODO create Exception Base
                this.View.ShowError(exception.ToString());
            }
          
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

        /// <summary>
        /// Gets the list quantity.
        /// </summary>
        public IList<int> ListQuantity
        {
            get
            {
                return new List<int>(Enumerable.Range(1, 500));
            }
        }

        /// <summary>
        /// Gets or sets the remove quantity.
        /// </summary>
        public int RemoveQuantity
        {
            get
            {
                return (int)this.GetValue(RemoveQuantityProperty);
            }

            set
            {
                this.SetValue(RemoveQuantityProperty, value);
            }
        }

        /// <summary>
        /// The remove quantity property.
        /// </summary>
        public static readonly DependencyProperty RemoveQuantityProperty = DependencyProperty.Register(
            "RemoveQuantity", 
            typeof(int), 
            typeof(AddBookViewModel), 
            new PropertyMetadata(0));

        /// <summary>
        /// Gets the books.
        /// </summary>
        public ObservableCollection<Book> Books
        {
            get
            {
                return this.books;
            }
        }

        /// <summary>
        /// Gets a value indicating whether can remove book.
        /// </summary>
        public bool CanRemoveBook {
            get
            {
                if (SelectedBook == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}