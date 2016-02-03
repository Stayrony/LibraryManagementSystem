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
    /// The book.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the book id.
        /// </summary>
        private int BookID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        private string Title { get; set; }

        /// <summary>
        /// Gets or sets the author name.
        /// </summary>
        private string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        private string Category { get; set; }
    }
}