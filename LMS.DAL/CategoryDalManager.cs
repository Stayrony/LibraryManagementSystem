// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CategoryDalManager.cs" company="">
//   
// </copyright>
// <summary>
//   The category dal manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;

using LMS.DAL.Utility;
using LMS.Service.Domain;

namespace LMS.DAL
{
    /// <summary>
    /// The category dal manager.
    /// </summary>
    public class CategoryDalManager
    {
        /// <summary>
        /// The sql dal manager.
        /// </summary>
        private SqlDalManager sqlDalManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryDalManager"/> class.
        /// </summary>
        public CategoryDalManager()
        {
            sqlDalManager = new SqlDalManager();
        }

        /// <summary>
        /// The create category.
        /// </summary>
        /// <param name="categoryName">
        /// The category name.
        /// </param>
        /// <returns>
        /// The <see cref="Category"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public Category CreateCategory(string categoryName)
        {
            Category category = new Category();
            category.CategoryName = categoryName;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@CategoryName", SqlDbType.VarChar) { Value = categoryName };

            try
            {
                category.CategoryID = sqlDalManager.InsertProcedureWithOutputInsertedId("CreateCategory", sqlParameters);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return category;
        }

        /// <summary>
        /// The get category id by category name.
        /// </summary>
        /// <param name="categoryName">
        /// The category name.
        /// </param>
        /// <returns>
        /// The <see cref="int?"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int GetCategoryIDByCategoryName(string categoryName)
        {
            int categoryID = new int();
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@CategoryName", SqlDbType.VarChar) { Value = categoryName };

            try
            {
                var dataTable = sqlDalManager.SelectProcedure("GetCategoryIDByCategoryName", sqlParameters);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    categoryID = int.Parse(dataRow["CategoryID"].ToString());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return categoryID;
        }

        /// <summary>
        /// The get category name by category id.
        /// </summary>
        /// <param name="categoryID">
        /// The category id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public string GetCategoryNameByCategoryID(int categoryID)
        {
            string categoryName = null;
            SqlParameter[] sqlParameters = new SqlParameter[0];
            sqlParameters[0] = new SqlParameter("CategoryID", SqlDbType.Int) { Value = categoryID };
            try
            {
                //TODO create procedure GetCategoryNameByCategoryID
                DataTable dataTable = this.sqlDalManager.SelectProcedure("GetCategoryNameByCategoryID", sqlParameters);
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
    }
}