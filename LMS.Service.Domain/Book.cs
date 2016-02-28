// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Book.cs" company="">
//   
// </copyright>
// <summary>
//   The book.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.Service.Domain
{
    /// <summary>
    ///     The book.
    /// </summary>
    public class Book
    {
        /// <summary>
        ///     Gets or sets the book id.
        /// </summary>
        public int BookID { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the author name.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the quantity of books issued.
        /// </summary>
        public int QuantityOfBooksIssued { get; set; }
    }
}