// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookManager.cs" company="">
//   
// </copyright>
// <summary>
//   The book manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Configuration;

namespace LMS.Service.BLL
{
    using System;
    using System.Collections.Generic;

    using LMS.DAL;
    using LMS.Service.Domain;

    /// <summary>
    /// The book manager.
    /// </summary>
    public class BookManager
    {
        /// <summary>
        /// The book dal manager.
        /// </summary>
        private BookDalManager bookDalManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookManager"/> class.
        /// </summary>
        public BookManager()
        {
            this.bookDalManager = new BookDalManager();
        }

        /// <summary>
        /// The create book.
        /// </summary>
        /// <param name="book">
        /// The book.
        /// </param>
        /// <returns>
        /// The <see cref="Book"/>.
        /// </returns>
        public Book CreateBook(Book book)
        {
            // TODO Check exist Category
            // BUG new book added to dbo.Book even without category. so BookCategory is empty
            Book createdBook = this.bookDalManager.CreateBook(book);
            return createdBook;
        }

        /// <summary>
        /// The delete book.
        /// </summary>
        /// <param name="book">
        /// The book.
        /// </param>
        public void DeleteBook(Book book)
        {
            this.bookDalManager.DeleteBook(book.BookID);
        }

        /// <summary>
        /// The get books by search criteria.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="author">
        /// The author.
        /// </param>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Book> GetBooksBySearchCriteria(string title, string author, string category)
        {
            List<Book> books = new List<Book>();

            if (!string.IsNullOrEmpty(title))
            {
                books.AddRange(bookDalManager.GetBooksByTitle(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books.AddRange(bookDalManager.GetBooksByAuthor(author));
            }

            if (!string.IsNullOrEmpty(category))
            {
                books.AddRange(this.bookDalManager.GetBookByCategoryName(category));
            }

            return books;
        }

        /// <summary>
        /// The get all books.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Book> GetAllBooks()
        {
            return bookDalManager.GetAllBooks();
        }

        /// <summary>
        /// The issue book.
        /// </summary>
        /// <param name="book">
        /// The book.
        /// </param>
        /// <param name="countOfBorrowedBook">
        /// The count of issued books.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void IssueBook(Book book, int countOfBorrowedBook)
        {
            // we borrow that count of books, so we subtract from  total number of Books Issued
            countOfBorrowedBook = - countOfBorrowedBook;

            // TODO GetCurrentUser session
            int currentUserID = 1;
            if (book.QuantityOfBooksIssued == 0)
            {
                throw new Exception("Current book are not available.");
            }

            bookDalManager.CreateIssueBook(book.BookID, currentUserID);

            // UPDATE QuantityOfBooksIssued in Book
            bool isUpdate = bookDalManager.UpdateQuantityOfBooksIssued(book.BookID, countOfBorrowedBook);
            if (!isUpdate)
            {
                throw new Exception("Quantity Of Books didn't update");
            }

        }

        /// <summary>
        /// The return book.
        /// </summary>
        /// <param name="book">
        /// The book.
        /// </param>
        public void ReturnBook(Book book, int countOfReturnedBooks)
        {
            // TODO GetCurrentUser session
            int currentUserID = 1;

            // we return that count of books, so we add from  total number of Books Issued
            // UPDATE QuantityOfBooksIssued in Book
            bookDalManager.UpdateQuantityOfBooksIssued(book.BookID, countOfReturnedBooks);

            //delete row from IssueBook or update BookReturnedOn
            bookDalManager.ReturnBook(book.BookID, currentUserID);
        }
    }
}