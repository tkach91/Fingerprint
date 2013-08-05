using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace DBInteraction
{
    public class DBNetBroker
    {
        public MySqlConnection Connection;
        public String connString;
        public String username;
        public String password;
        public String database;

        public DBNetBroker(String connection, String username, String password, String database)
        {
            connString = connection;
            this.username = username;
            this.password = password;
            this.database = database;

            Connection = new MySqlConnection("datasource=" + connection + ";username=" + username +
                                            ";password=" + password + ";database=" + database);

        }

        public void reconnect()
        {
            Connection.Close();
            Connection = new MySqlConnection("datasource=" + connString + ";username=" + username +
                                            ";password=" + password + ";database=" + database);
        }

        public void close()
        {
            Connection.Close();
        }
    }

    public class DBHandler
    {
        protected DBNetBroker DBB;
        public MySqlDataAdapter MyAdapter;
        public DataSet MyDataSet;

        public DBHandler(DBNetBroker dbnb)
        {
            DBB = dbnb;
        }

        public DataSet execute(String query)
        {
            MyAdapter = new MySqlDataAdapter(query, DBB.Connection);
            MyDataSet = new DataSet();
            MyAdapter.Fill(MyDataSet);
            return MyDataSet;
        }

        public void executeNQ(String query, byte[] Data)
        {
            DBB.Connection.Open();
            MySqlCommand msc = new MySqlCommand(query, DBB.Connection);
            MySql.Data.MySqlClient.MySqlParameter param = msc.Parameters.Add("?Data", MySql.Data.MySqlClient.MySqlDbType.Blob);
            param.Value = Data;
            msc.ExecuteNonQuery();
            DBB.Connection.Close();
        }

        public void executeNQDate(String query, DateTime Date)
        {
            DBB.Connection.Open();
            MySqlCommand msc = new MySqlCommand(query, DBB.Connection);
            MySql.Data.MySqlClient.MySqlParameter param = msc.Parameters.Add("?Date", MySql.Data.MySqlClient.MySqlDbType.Datetime);
            param.Value = Date;
            msc.ExecuteNonQuery();
            DBB.Connection.Close();
        }

        public void executeNQnoData(String query)
        {
            DBB.Connection.Open();
            MySqlCommand msc = new MySqlCommand(query, DBB.Connection);
            msc.ExecuteNonQuery();
            DBB.Connection.Close();
        }

        public void close()
        {
            DBB.close();
        }
    }
}
