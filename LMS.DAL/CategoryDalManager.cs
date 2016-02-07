using System;
using System.Data;
using System.Data.SqlClient;
using LMS.DAL.Utility;
using LMS.Service.Domain;

namespace LMS.DAL
{
    public class CategoryDalManager
    {
        private SqlDalManager sqlDalManager;

        public CategoryDalManager()
        {
            sqlDalManager = new SqlDalManager();
        }

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

        public int? GetCategoryIDByCategoryName(string categoryName)
        {
            int? categoryID;
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@CategoryName", SqlDbType.VarChar) { Value = categoryName };

            try
            {
                var dataTable = sqlDalManager.SelectProcedure("GetCategoryIDByCategoryName", sqlParameters);
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        return categoryID = int.Parse(dataRow["CategoryID"].ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return null;
        }
    }
}