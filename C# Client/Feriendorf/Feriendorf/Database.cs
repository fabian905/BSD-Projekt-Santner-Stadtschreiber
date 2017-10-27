using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feriendorf
{
    public class Database
    {
        private static OleDbConnection myConnection = null;

        public string Connect()
        {
            try
            {
                string connectionString = "Provider=OraOLEDB.Oracle; Data Source = 192.168.128.152/ora11g; User Id = d5a17; Password = d5a; OLEDB.NET = True; ";

                myConnection = new OleDbConnection(connectionString);
                myConnection.Open();
                return "CONNECTED!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void Close()
        {
            myConnection.Close();
        }

        public OleDbConnection GetConnection()
        {
            return myConnection;
        }

        public OleDbDataReader ExecuteCommand(string sql)
        {
            OleDbCommand cmd = myConnection.CreateCommand();
            cmd.CommandText = sql;
            OleDbDataReader reader = cmd.ExecuteReader();
            return reader;
        }
    }
}
