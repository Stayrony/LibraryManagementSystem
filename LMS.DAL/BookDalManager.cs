// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookDalManager.cs" company="">
//   
// </copyright>
// <summary>
//   The book dal manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using LMS.DAL.Utility;
    using LMS.Service.Domain;

    /// <summary>
    /// The book dal manager.
    /// </summary>
    public class BookDalManager
    {
        /// <summary>
        /// The sql dal manager.
        /// </summary>
        private SqlDalManager sqlDalManager;

        /// <summary>
        /// The category dal manager.
        /// </summary>
        private CategoryDalManager categoryDalManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookDalManager"/> class.
        /// </summary>
        public BookDalManager()
        {
            sqlDalManager = new SqlDalManager();
            categoryDalManager = new CategoryDalManager();
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
        /// <exception cref="Exception">
        /// </exception>
        public Book CreateBook(Book book)
        {
            int categoryID = this.categoryDalManager.GetCategoryIDByCategoryName(book.Category);

            SqlParameter[] sqlCategoryParameters = new SqlParameter[2];
            sqlCategoryParameters[0] = new SqlParameter("@CategoryID", SqlDbType.Int) { Value = categoryID };

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Title", SqlDbType.VarChar) { Value = book.Title };
            sqlParameters[1] = new SqlParameter("@Author", SqlDbType.VarChar) { Value = book.Author };

            sqlParameters[2] = new SqlParameter("@QuantityOfBooksIssued", SqlDbType.Int)
                                   {
                                       Value =
                                           book
                                           .QuantityOfBooksIssued
                                   };

            try
            {
                book.BookID = this.sqlDalManager.InsertProcedureWithOutputInsertedId("CreateBook", sqlParameters);
                sqlCategoryParameters[1] = new SqlParameter("@BookID", SqlDbType.Int) { Value = book.BookID };
                this.sqlDalManager.InsertProcedure("BindingBookWithCategory", sqlCategoryParameters);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return book;
        }

        /// <summary>
        /// The delete book.
        /// </summary>
        /// <param name="bookID">
        /// The book id.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void DeleteBook(int bookID, int countRemove)
        {
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@BookID", SqlDbType.Int) { Value = bookID };
            sqlParameters[1] = new SqlParameter("@CountRemove", SqlDbType.Int) { Value = countRemove };
            try
            {
                this.sqlDalManager.DeleteProcedure("DeleteBook", sqlParameters);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// The get book by book id.
        /// </summary>
        /// <param name="bookID">
        /// The book id.
        /// </param>
        /// <returns>
        /// The <see cref="Book"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public Book GetBookByBookID(int bookID)
        {
            Book book = new Book();
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@BookID", SqlDbType.Int) { Value = bookID };
            try
            {
                // TODO create procedure GetBookByBookID
                DataTable dataTable = this.sqlDalManager.SelectProcedure("GetBookByBookID", sqlParameters);

                // int categoryID = new int();
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        book.BookID = int.Parse(dataRow["BookID"].ToString());
                        book.Author = dataRow["Author"].ToString();
                        book.Title = dataRow["Title"].ToString();
                        book.QuantityOfBooksIssued = int.Parse(dataRow["QuantityOfBooksIssued"].ToString());
                    }
                }

                // book.Category = this.categoryDalManager.GetCategoryNameByCategoryID(categoryID);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return book;
        }

        /// <summary>
        /// The get category name by book id.
        /// </summary>
        /// <param name="bookID">
        /// The book id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public string GetCategoryNameByBookID(int bookID)
        {
            string categoryName = null;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@BookID", SqlDbType.Int) { Value = bookID };
            try
            {
                DataTable dataTable = sqlDalManager.SelectProcedure("GetCategoryNameByBookID", sqlParameters);
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        categoryName = dataRow["CategoryName"].ToString();
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return categoryName;
        }

        /// <summary>
        /// The get books by title.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Book> GetBooksByTitle(string title)
        {
            List<Book> books = new List<Book>();
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@TitleSearch", SqlDbType.VarChar) { Value = title };

            try
            {
                DataTable dataTable = this.sqlDalManager.SelectProcedure("SearchBookByTitle", sqlParameters);
                if (dataTable.Rows.Count > 0)
                {
                    books = this.ParseBookFromDataTable(dataTable);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return books;
        }

        /// <summary>
        /// The get books by  author.
        /// </summary>
        /// <param name="author">
        /// The author.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Book> GetBooksByAuthor(string author)
        {
            List<Book> books = new List<Book>();
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@AuthorSearch", SqlDbType.VarChar) { Value = author };

            try
            {
                DataTable dataTable = this.sqlDalManager.SelectProcedure("SearchBookByAuthor", sqlParameters);
                if (dataTable.Rows.Count > 0)
                {
                    books = this.ParseBookFromDataTable(dataTable);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return books;
        }

        /// <summary>
        /// The get book by category name.
        /// </summary>
        /// <param name="catagoryName">
        /// The catagory name.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Book> GetBookByCategoryName(string catagoryName)
        {
            List<Book> books = new List<Book>();
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@CategoryName", SqlDbType.VarChar) { Value = catagoryName };

            try
            {
                DataTable dataTable = this.sqlDalManager.SelectProcedure("SearchBookByCategoryName", sqlParameters);
                if (dataTable.Rows.Count > 0)
                {
                    books = this.ParseBookFromDataTable(dataTable);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return books;
        }

        /// <summary>
        /// The parse book from data table and binding with associated category.
        /// </summary>
        /// <param name="dataTable">
        /// The data table.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Book> ParseBookFromDataTable(DataTable dataTable)
        {
            List<Book> books = new List<Book>();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Book book = new Book();
                book.BookID = int.Parse(dataRow["BookID"].ToString());
                book.Author = dataRow["Author"].ToString();
                book.Title = dataRow["Title"].ToString();
                book.QuantityOfBooksIssued = int.Parse(dataRow["QuantityOfBooksIssued"].ToString());
                book.Category = this.GetCategoryNameByBookID(book.BookID);
                books.Add(book);
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
            List<Book> books = new List<Book>();
            try
            {
                DataTable dataTable = this.sqlDalManager.SelectAllProcedure("GetAllBooks");
                if (dataTable.Rows.Count > 0)
                {
                    books = this.ParseBookFromDataTable(dataTable);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return books;
        }

        /// <summary>
        /// The create issue book.
        /// </summary>
        /// <param name="bookID">
        /// The book ID.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void CreateIssueBook(int bookID, int userID)
        {
            BookIssueDetail bookIssueDetail = new BookIssueDetail();
            bookIssueDetail.BookID = bookID;
            bookIssueDetail.BookIssuedOn = DateTime.Now;
            bookIssueDetail.UserID = userID;

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@BookID", SqlDbType.Int) { Value = bookID };
            sqlParameters[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userID };
            sqlParameters[2] = new SqlParameter("@BookIssuedOn", SqlDbType.DateTime)
                                   {
                                       Value =
                                           bookIssueDetail
                                           .BookIssuedOn
                                   };

            try
            {
                bookIssueDetail.IssueID = this.sqlDalManager.InsertProcedureWithOutputInsertedId(
                    "CreateBookIssueDetail", 
                    sqlParameters);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            // ? return  bookIssueDetail ?
        }

        /// <summary>
        /// The update quantity of books issued.
        /// </summary>
        /// <param name="bookID">
        /// The book id.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public bool UpdateQuantityOfBooksIssued(int bookID, int count)
        {
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@BookID", SqlDbType.Int) { Value = bookID };
            sqlParameters[1] = new SqlParameter("@Count", SqlDbType.Int) { Value = count };

            try
            {
                return this.sqlDalManager.UpdateProcedure("UpdateQuantityOfBooksIssued", sqlParameters);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// The get books issued by user id.
        /// </summary>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<Book> GetBooksIssuedByUserID(int userID)
        {
            List<Book> books = new List<Book>();
            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userID };
            try
            {
                DataTable dataTable = this.sqlDalManager.SelectProcedure("GetBooksIssuedByUserID", sqlParameter);
                if (dataTable.Rows.Count > 0)
                {
                    books = this.ParseBookFromDataTable(dataTable);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return books;
        }

        /// <summary>
        /// The return book.
        /// </summary>
        /// <param name="bookID">
        /// The book ID.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        public void ReturnBook(int bookID, int userID)
        {
            // delete bookIssue
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@BookID", SqlDbType.Int) { Value = bookID };
            sqlParameters[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userID };
            //TODO delete or bit bookIssue
            sqlParameters[2] = new SqlParameter("@BookReturnedOn", SqlDbType.DateTime) { Value = DateTime.Now };
            try
            {
                this.sqlDalManager.UpdateProcedure("ReturnBook", sqlParameters);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// The get borrowed book by author.
        /// </summary>
        /// <param name="author">
        /// The author.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Book> GetBorrowedBookByAuthor(string author, int userID)
        {
            List<Book> books = new List<Book>();
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@AuthorSearch", SqlDbType.VarChar) { Value = author };
            sqlParameters[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userID };

            try
            {
                DataTable dataTable = this.sqlDalManager.SelectProcedure("SearchBorrowedBookByAuthor", sqlParameters);
                if (dataTable.Rows.Count > 0)
                {
                    books = this.ParseBookFromDataTable(dataTable);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return books;
        }

        /// <summary>
        /// The get borrowed book by title.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Book> GetBorrowedBookByTitle(string title, int userID)
        {
            List<Book> books = new List<Book>();
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@TitleSearch", SqlDbType.VarChar) { Value = title };
            sqlParameters[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userID };

            try
            {
                DataTable dataTable = this.sqlDalManager.SelectProcedure("SearchBorrowedBookByTitle", sqlParameters);
                if (dataTable.Rows.Count > 0)
                {
                    books = this.ParseBookFromDataTable(dataTable);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return books;
        }

        /// <summary>
        /// The get borrowed book by category.
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <param name="userID">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public List<Book> GetBorrowedBookByCategory(string category, int userID)
        {
            List<Book> books = new List<Book>();
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@CategorySearch", SqlDbType.VarChar) { Value = category };
            sqlParameters[1] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userID };

            try
            {
                DataTable dataTable = this.sqlDalManager.SelectProcedure(
                    "SearchBorrowedBookByCategoryName", 
                    sqlParameters);
                if (dataTable.Rows.Count > 0)
                {
                    books = this.ParseBookFromDataTable(dataTable);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return books;
        }
    }
}