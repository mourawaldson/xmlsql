using System;
using System.Data;
using System.Data.Odbc;
using System.Collections;

namespace XMLSQL
{
    class DB
    {
        private string ConnectionString;
        private OdbcConnection Connection;

        public string SQLTablesSQLServer = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME <> 'sysdiagrams'";
        public string SQLTablesFirebird = "SELECT TRIM(RDB$RELATION_NAME) FROM RDB$RELATIONS WHERE RDB$VIEW_BLR IS NULL AND (RDB$SYSTEM_FLAG = 0 OR RDB$SYSTEM_FLAG IS NULL);";

        public void open(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.Connection = new OdbcConnection(this.ConnectionString);
            this.Connection.Open();
        }

        public void close()
        {
            if (this.connected() == ConnectionState.Open)
            {
                this.Connection.Close();
            }
        }

        public DataTable getResult(String SQLQuery)
        {
            OdbcDataReader DataReader;
            OdbcCommand Command = new OdbcCommand();
            DataTable DataTable = new DataTable();

            Command.CommandText = SQLQuery;
            Command.Connection = this.Connection;
            DataReader = Command.ExecuteReader();
            DataTable.Load(DataReader);
            
            DataReader.Close();
 
            return DataTable;
        }

        public System.Collections.ArrayList getResult2(String SQLQuery)
        {
            OdbcDataReader DataReader;
            OdbcCommand Command = new OdbcCommand();

            Command.CommandText = SQLQuery;
            Command.Connection = this.Connection;
            DataReader = Command.ExecuteReader();
            System.Collections.ArrayList array = new System.Collections.ArrayList();

            while (DataReader.Read())
            {
                array.Add(DataReader.GetString(0).Trim());
            }

            DataReader.Close();

            return array;
        }

        public int numRows(String SQLQuery)
        {
            OdbcCommand Command = new OdbcCommand();

            Command.CommandText = SQLQuery;
            Command.Connection = this.Connection;
            return Command.ExecuteNonQuery();
        }

        public ConnectionState connected()
        {
            return this.Connection.State;
        }
    }
}
