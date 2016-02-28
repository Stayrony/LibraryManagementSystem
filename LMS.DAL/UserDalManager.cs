// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserDalManager.cs" company="">
//   
// </copyright>
// <summary>
//   The user dal manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using LMS.DAL.Utility;
    using LMS.Service.Domain;

    /// <summary>
    ///     The user dal manager.
    /// </summary>
    public class UserDalManager
    {
        /// <summary>
        ///     The sql dal manager.
        /// </summary>
        private readonly SqlDalManager sqlDalManager;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserDalManager" /> class.
        /// </summary>
        public UserDalManager()
        {
            this.sqlDalManager = new SqlDalManager();
        }

        /// <summary>
        /// The get user on login.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        public User GetUserOnLogin(string login)
        {
            var user = new User();

            // TODO return user from DB
            var sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Login", SqlDbType.VarChar) { Value = login };
            var dataTable = this.sqlDalManager.SelectProcedure("GetUserOnLogin", sqlParameters);

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    DateTime date;
                    user.UserID = int.Parse(dataRow["UserId"].ToString());
                    user.Login = dataRow["Login"].ToString();
                    user.Password = dataRow["Password"].ToString();
                    DateTime.TryParse(dataRow["TimeCreated"].ToString(), out date);
                    user.TimeCreated = date;
                }
            }
            else
            {
                return null;
            }

            return user;
        }

        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="User"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public User CreateUser(User user)
        {
            var sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Login", SqlDbType.VarChar) { Value = user.Login };
            sqlParameters[1] = new SqlParameter("@Password", SqlDbType.VarChar) { Value = user.Password };
            sqlParameters[2] = new SqlParameter("@TimeCreated", SqlDbType.DateTime) { Value = user.TimeCreated };
            try
            {
                user.UserID = this.sqlDalManager.InsertProcedureWithOutputInsertedId("CreateUser", sqlParameters);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return user;
        }

        /// <summary>
        /// The delete user.
        /// </summary>
        /// <param name="login">
        /// The login.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void DeleteUser(string login)
        {
            var sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@Login", SqlDbType.VarChar) { Value = login };
            this.sqlDalManager.DeleteProcedure("DeleteUser", sqlParameter);
        }
    }
}