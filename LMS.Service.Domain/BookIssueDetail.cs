// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookIssueDetail.cs" company="">
//   
// </copyright>
// <summary>
//   The book issue detail.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LMS.Service.Domain
{
    using System;

    /// <summary>
    /// The book issue detail.
    /// </summary>
    public class BookIssueDetail
    {
        /// <summary>
        /// Gets or sets the issue id.
        /// </summary>
        public int IssueID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the book id.
        /// </summary>
        public int BookID { get; set; }

        /// <summary>
        /// Gets or sets the book issued on.
        /// </summary>
        public DateTime BookIssuedOn { get; set; }

        /// <summary>
        /// Gets or sets the book returned on.
        /// </summary>
        public DateTime BookReturnedOn { get; set; }
    }
}