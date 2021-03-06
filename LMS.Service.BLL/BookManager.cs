﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookManager.cs" company="">
//   
// </copyright>
// <summary>
//   The book manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net.Configuration;
using System.Net.Mime;

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
        public void DeleteBook(Book book, int countRemove)
        {
            if (countRemove > book.QuantityOfBooksIssued)
            {
                //TODO Exception base
                throw new Exception("You try to remove more books than available");
            }
            else
            {
                this.bookDalManager.DeleteBook(book.BookID, countRemove);
            }
          
        }

        /// <summary>
        /// The get all books.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Book> GetAllBooks()
        {
            return this.bookDalManager.GetAllBooks();
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
            countOfBorrowedBook = -countOfBorrowedBook;

            // TODO GetCurrentUser session
            int currentUserID = 1;
         //   currentUserID = int.TryParse(System.Windows.Application.Current.Properties["UserID"]);
           
            if (book.QuantityOfBooksIssued == 0)
            {
                throw new Exception("Current book are not available.");
            }

            this.bookDalManager.CreateIssueBook(book.BookID, currentUserID);

            // UPDATE QuantityOfBooksIssued in Book
            bool isUpdate = this.bookDalManager.UpdateQuantityOfBooksIssued(book.BookID, countOfBorrowedBook);
            if (!isUpdate)
            {
                throw new Exception("Quantity Of Books didn't update");
            }
        }

        /// <summary>
        /// The get books issued by user id.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Book> GetBooksIssuedByUserID()
        {
            int currentUserID = 1;
            return this.bookDalManager.GetBooksIssuedByUserID(currentUserID);
        }

        /// <summary>
        /// The return book.
        /// </summary>
        /// <param name="book">
        /// The book.
        /// </param>
        /// <param name="countOfReturnedBooks">
        /// The count Of Returned Books.
        /// </param>
        public void ReturnBook(Book book, int countOfReturnedBooks)
        {
            // TODO GetCurrentUser session
            int currentUserID = 1;

            // we return that count of books, so we add count to total number of Books Issued
            // UPDATE QuantityOfBooksIssued in Book
            this.bookDalManager.UpdateQuantityOfBooksIssued(book.BookID, countOfReturnedBooks);

            // delete row from IssueBook or update BookReturnedOn
            this.bookDalManager.ReturnBook(book.BookID, currentUserID);
        }

        /// <summary>
        /// The get available books by search criteria.
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
        public List<Book> GetAvailableBooksBySearchCriteria(string title, string author, string category)
        {
            List<Book> books = new List<Book>();

            if (!string.IsNullOrEmpty(title))
            {
                books.AddRange(this.bookDalManager.GetBooksByTitle(title));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books.AddRange(this.bookDalManager.GetBooksByAuthor(author));
            }

            if (!string.IsNullOrEmpty(category))
            {
                books.AddRange(this.bookDalManager.GetBookByCategoryName(category));
            }

            return books;
        }

        /// <summary>
        /// The get books borrowed by user by search criteria.
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
        public List<Book> GetBooksBorrowedByUserBySearchCriteria(string title, string author, string category)
        {
            // TODO GetCurrentUser session
            int currentUserID = 1;

            List<Book> books = new List<Book>();

            if (!string.IsNullOrEmpty(title))
            {
                books.AddRange(this.bookDalManager.GetBorrowedBookByTitle(title, currentUserID));
            }

            if (!string.IsNullOrEmpty(author))
            {
                books.AddRange(this.bookDalManager.GetBorrowedBookByAuthor(author, currentUserID));
            }

            if (!string.IsNullOrEmpty(category))
            {
                books.AddRange(this.bookDalManager.GetBorrowedBookByCategory(category, currentUserID));
            }

            return books;
        }
    }
}