using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PROMPT
{
    public class Database
    {

        private readonly string connectionName;

        /// <summary>
        /// Get user Id from Session Variable If exist
        /// </summary>
        

        /// <summary>
        /// Creates DataBase object for default Name
        /// </summary>
        public Database()
        {
            Hashtable dataConfig = (Hashtable)ConfigurationManager.GetSection("dataConfiuration");
            connectionName = (string)dataConfig["defaultDatabase"];
            dataConfig = null;

        }

        /// <summary>
        /// Creates DataBase object for default Name
        /// </summary>
        public Database(string name)
        {
            connectionName = name;
        }

        public DbCommand GetStoredPocCommand(string storedProcedureName)
        {
            DbCommand databaseCommand = new SqlCommand()
            {
                CommandText = storedProcedureName,
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 36000
            };
            return databaseCommand;
        }

        private void WriteToLog(DbCommand Command)
        {
        }

        public DbCommand GetSqlStringCommand(string query)
        {
            DbCommand databaseCommand = new SqlCommand()
            {
                CommandText = query,
                CommandType = CommandType.Text,
                CommandTimeout = 36000
            };
            return databaseCommand;
        }

        public void AddInParameter(DbCommand command, string name, DbType dbType, object value)
        {
            if (value == null)
                value = DBNull.Value;
            DbParameter parameter = new SqlParameter(name, (SqlDbType)dbType, 0, ParameterDirection.Input, true, 0, 0, string.Empty, DataRowVersion.Default, value) { DbType = dbType, Value = value };
            command.Parameters.Add(parameter);
        }

        public void AddOutParameter(DbCommand command, string name, DbType dbType, int Size)
        {
            DbParameter parameter = new SqlParameter(name, (SqlDbType)dbType, 0, ParameterDirection.Output, true, 0, 0, string.Empty, DataRowVersion.Default, DBNull.Value) { DbType = dbType, Value = DBNull.Value };
            command.Parameters.Add(parameter);
        }

        public object GetParameterValue(DbCommand command, string name)
        {
            return command.Parameters[name].Value;
        }

        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection() { ConnectionString = ConnectionString };
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        public string ConnectionString
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings[connectionName] != null)
                    return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                else
                    return string.Empty;
            }
        }

        public DataSet ExecuteDataSet(DbCommand command)
        {
            WriteToLog(command);
            DataSet data = new DataSet();
            using (SqlConnection connection = OpenConnection())
            {
                command.Connection = connection;
                using (SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = command as SqlCommand })
                {
                    adapter.Fill(data);
                }
                connection.Close();
            }
            return data;
        }

        public DataTable ExecuteDataTable(DbCommand command)
        {
            WriteToLog(command);
            DataTable data = new DataTable();
            using (SqlConnection connection = OpenConnection())
            {
                command.Connection = connection;
                using (DbDataReader reader = command.ExecuteReader())
                {
                    data.Load(reader);
                    reader.Close();
                }
                connection.Close();
            }
            return data;
        }

        public int ExecuteNonQuery(DbCommand command)
        {
            WriteToLog(command);
            int returnValue = 0;
            using (SqlConnection connection = OpenConnection())
            {
                command.Connection = connection;
                returnValue = command.ExecuteNonQuery();
                connection.Close();
            }
            return returnValue;
        }

        public object ExecuteScalar(DbCommand command)
        {
            WriteToLog(command);

            object returnValue = string.Empty;
            using (SqlConnection connection = OpenConnection())
            {
                command.Connection = connection;
                returnValue = command.ExecuteScalar();
                connection.Close();
            }
            return returnValue;
        }

        public IDataReader ExecuteReader(DbCommand command)
        {
            WriteToLog(command);
            SqlConnection connection = new SqlConnection();
            try
            {
                connection = OpenConnection();
                command.Connection = connection;
                return command.ExecuteReader();
            }
            catch
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection = null;
                }
                throw;    
            }
        }

        public void ReaderClose(DbCommand command,IDataReader reader)
        {
            reader.Close();
            if (command.Connection.State == ConnectionState.Open)
            {
                command.Connection.Close();
                command.Connection = null;
            }
        }

    }
}