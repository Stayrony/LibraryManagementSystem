// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookDalManagerTester.cs" company="">
//   
// </copyright>
// <summary>
//   The book dal manager tester.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LMS.DAL.Test
{
    using System;
    using System.Collections.Generic;

    using LMS.Service.Domain;

    using NUnit.Framework;

    /// <summary>
    /// The book dal manager tester.
    /// </summary>
    [TestFixture]
    public class BookDalManagerTester
    {
        /// <summary>
        /// The book dal manager.
        /// </summary>
        private BookDalManager bookDalManager;

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            bookDalManager = new BookDalManager();
        }

        /// <summary>
        /// The create book basic test.
        /// </summary>
        [Test]
        public void CreateBookBasicTest()
        {
            // Book book = new Book();
            // book.Title = "Dune";
            // book.Author = "Frank Herbert";
            // book.Category = "Fiction";
            // book.QuantityOfBooksIssued = 7;
            Book book = new Book();
            book.Title = "Neuromancer";
            book.Author = "William Gibson";
            book.Category = "Fiction";
            book.QuantityOfBooksIssued = 10;

            Book newBook = bookDalManager.CreateBook(book);

            Assert.That(newBook.BookID, Is.Not.EqualTo(0));

            Console.WriteLine("BookID = " + newBook.BookID);
        }

        /// <summary>
        /// The get category name by book id basic test.
        /// </summary>
        [Test]
        public void GetCategoryNameByBookIDBasicTest()
        {
            Book book = new Book();
            book.Title = "The Lean Startup";
            book.Author = "Eric Ries";
            book.Category = "Business";
            book.QuantityOfBooksIssued = 3;

            Book newBook = bookDalManager.CreateBook(book);

            string categoryName = bookDalManager.GetCategoryNameByBookID(newBook.BookID);
            Assert.That(book.Category, Is.EqualTo(categoryName));
        }

        // [Test]
        // public void GetBooksByTitleOrAuthorBasicTest()
        // {
        // List<Book> books = new List<Book>();
        // string title = "Lean";
        // string author = "Eric";
        // books = bookDalManager.GetBooksByTitleOrAuthor(title, author);
        // foreach (Book book in books)
        // {
        // Console.WriteLine("Title = " + book.Title);
        // Console.WriteLine("Author = " + book.Author);
        // Console.WriteLine("Category = " + book.Category);
        // Console.WriteLine();
        // }
        // }

        /// <summary>
        /// The get book by category name basic test.
        /// </summary>
        [Test]
        public void GetBookByCategoryNameBasicTest()
        {
            List<Book> books = new List<Book>();
            string categoryName = "Fiction";
            books = bookDalManager.GetBookByCategoryName(categoryName);
            foreach (Book book in books)
            {
                Console.WriteLine("Title = " + book.Title);
                Console.WriteLine("Author = " + book.Author);
                Console.WriteLine("Category = " + book.Category);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// The get books by title basic test.
        /// </summary>
        [Test]
        public void GetBooksByTitleBasicTest()
        {
            List<Book> books = new List<Book>();
            string title = "Startup";
            books = bookDalManager.GetBooksByTitle(title);
            foreach (Book book in books)
            {
                Console.WriteLine("Title = " + book.Title);
                Console.WriteLine("Author = " + book.Author);
                Console.WriteLine("Category = " + book.Category);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// The get books by author basic test.
        /// </summary>
        [Test]
        public void GetBooksByAuthorBasicTest()
        {
            List<Book> books = new List<Book>();
            string author = "b";
            books = bookDalManager.GetBooksByAuthor(author);
            foreach (Book book in books)
            {
                Console.WriteLine("Title = " + book.Title);
                Console.WriteLine("Author = " + book.Author);
                Console.WriteLine("Category = " + book.Category);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// The get all books basic test.
        /// </summary>
        [Test]
        public void GetAllBooksBasicTest()
        {
            List<Book> books = new List<Book>();
            books = bookDalManager.GetAllBooks();
            foreach (Book book in books)
            {
                Console.WriteLine("Title = " + book.Title);
                Console.WriteLine("Author = " + book.Author);
                Console.WriteLine("Category = " + book.Category);
                Console.WriteLine();
            }
        }
    }
}