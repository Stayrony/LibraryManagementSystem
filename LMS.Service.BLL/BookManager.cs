// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookManager.cs" company="">
//   
// </copyright>
// <summary>
//   The book manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.Service.BLL
{
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
            //TODO Check exist Category
            //BUG new book added to dbo.Book even without category. so BookCategory is empty
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
    }
}