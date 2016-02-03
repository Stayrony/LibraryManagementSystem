// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="">
//   
// </copyright>
// <summary>
//   The user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LMS.Service.Domain
{
    using System;

    /// <summary>
    /// The user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        private int UserID { get; set; }

        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        private string Login { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        private string Password { get; set; }

        /// <summary>
        /// Gets or sets the time created.
        /// </summary>
        private DateTime TimeCreated { get; set; }

        /// <summary>
        /// Gets or sets the number of books issued.
        /// </summary>
        private int NumberOfBooksIssued { get; set; }
    }
}