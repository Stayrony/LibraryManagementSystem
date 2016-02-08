// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlDalManager.cs" company="">
//   
// </copyright>
// <summary>
//   The sql dal manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LMS.DAL.Utility
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    ///     The sql dal manager.
    /// </summary>
    public class SqlDalManager
    {
        /// <summary>
        ///     The conn.
        /// </summary>
        private readonly SqlConnection conn;

        /// <summary>
        ///     The sql data adapter.
        /// </summary>
        private SqlDataAdapter sqlDataAdapter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SqlDalManager" /> class.
        /// </summary>
        public SqlDalManager()
        {
            this.sqlDataAdapter = new SqlDataAdapter();
            this.conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
        }

        /// <summary>
        ///     The open connection.
        /// </summary>
        /// <returns>
        ///     The <see cref="SqlConnection" />.
        /// </returns>
        private SqlConnection OpenConnection()
        {
            if (this.conn.State == ConnectionState.Closed || this.conn.State == ConnectionState.Broken)
            {
                this.conn.Open();
            }

            return this.conn;
        }

        /// <summary>
        /// The insert procedure.
        /// </summary>
        /// <param name="procedureName">
        /// The procedure name.
        /// </param>
        /// <param name="sqlParameters">
        /// The sql parameters.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void InsertProcedure(string procedureName, SqlParameter[] sqlParameters)
        {
            using (var cmd = new SqlCommand(procedureName, this.conn))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqlParameters);
                    cmd.Connection = this.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException exception)
                {
                    throw new Exception(
                        "Error - Connection.InsertProcedure - Procedure: " + procedureName + exception.StackTrace);
                }
                finally
                {
                    this.conn.Close();
                }
            }
        }

        /// <summary>
        /// The insert procedure with output inserted id.
        /// </summary>
        /// <param name="procedureName">
        /// The procedure name.
        /// </param>
        /// <param name="sqlParameters">
        /// The sql parameters.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public int InsertProcedureWithOutputInsertedId(string procedureName, SqlParameter[] sqlParameters)
        {
            int id = 0;
            using (var cmd = new SqlCommand(procedureName, this.conn))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqlParameters);
                    cmd.Connection = this.OpenConnection();
                    id = (int)cmd.ExecuteScalar();
                }
                catch (SqlException exception)
                {
                    throw new Exception(
                        "Error - Connection.InsertProcedureWithOutputInsertedId - Procedure : " + procedureName
                        + exception.StackTrace);
                }
                finally
                {
                    this.conn.Close();
                }
            }

            return id;
        }

        /// <summary>
        /// The delete procedure.
        /// </summary>
        /// <param name="procedureName">
        /// The procedure name.
        /// </param>
        /// <param name="sqlParameters">
        /// The sql parameters.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void DeleteProcedure(string procedureName, SqlParameter[] sqlParameters)
        {
            using (var cmd = new SqlCommand(procedureName, this.conn))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqlParameters);
                    cmd.Connection = this.OpenConnection();
                    var rowAffected = cmd.ExecuteNonQuery();
                    if (rowAffected == 0)
                    {
                        throw new Exception("Procedure : " + procedureName + " not has been deleted Item from DB");
                    }
                }
                catch (SqlException exception)
                {
                    throw new Exception(
                        "Error - Connection.DeleteProcedure - Procedure : " + procedureName + exception.StackTrace);
                }
                finally
                {
                    this.conn.Close();
                }
            }
        }

        /// <summary>
        /// The select procedure.
        /// </summary>
        /// <param name="procedureName">
        /// The procedure name.
        /// </param>
        /// <param name="sqlParameters">
        /// The sql parameters.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public DataTable SelectProcedure(string procedureName, SqlParameter[] sqlParameters)
        {
            using (var cmd = new SqlCommand(procedureName, this.conn))
            {
                var dataTable = new DataTable();
                var dataSet = new DataSet();
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqlParameters);
                    var dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);
                    dataTable = dataSet.Tables[0];
                }
                catch (SqlException exception)
                {
                    throw new Exception(
                        "Error - Connection.SelectProcedure - Procedure : " + procedureName + exception.StackTrace);
                }
                finally
                {
                    this.conn.Close();
                }

                return dataTable;
            }
        }

        /// <summary>
        /// The select all procedure.
        /// </summary>
        /// <param name="procedureName">
        /// The procedure name.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public DataTable SelectAllProcedure(string procedureName)
        {
            using (var cmd = new SqlCommand(procedureName, this.conn))
            {
                var dataTable = new DataTable();
                var dataSet = new DataSet();
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataSet);
                    dataTable = dataSet.Tables[0];
                }
                catch (SqlException exception)
                {
                    throw new Exception(
                        "Error - Connection.SelectAllProcedure - Procedure : " + procedureName + exception);
                }
                finally
                {
                    this.conn.Close();
                }

                return dataTable;
            }
        }
    }
}