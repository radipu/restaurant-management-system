using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.DAL
{

    public class MySqlGatewayConnection
    {

        public MySqlCommand command { get; set; }
        public MySqlConnection Connection { get; set; }
        public MySqlDataAdapter Adapter { get; set; }
        public MySqlDataReader Reader { get; set; }
        public string Query { get; set; }
        static string MainConnectionString = Properties.Settings.Default.connString + "password=" + Properties.Settings.Default.password + ";" + "Pooling=false; Max Pool Size = 50000; Min Pool Size = 5;Charset=utf8;convert zero datetime=True";

        public MySqlTransaction Transaction { get; set; }
        public static bool IsServerConnected { get; set; }
        public MySqlGatewayConnection()
        {
            // string MainConnectionString = "server=127.0.0.1;user id=root;persistsecurityinfo=True;database=spiceresdb";

            try
            {
                //   var IsConnectedServer = OthersMethod.CheckServerConneciton();
                //    string ipAddress = LoadServer();
                //  string MainConnectionString = "server=" + ipAddress + ";user id=root; password=" + Properties.Settings.Default.password + ";database=" + Properties.Settings.Default.database + "; " + "Pooling=false; Max Pool Size = 50000; Min Pool Size = 5";

                Connection = new MySqlConnection(MainConnectionString);

                if (Connection.State == ConnectionState.Closed || Connection.State == ConnectionState.Broken || Connection.State == ConnectionState.Executing || Connection.State == ConnectionState.Fetching)
                {
                    Connection.Open();

                }
                else
                {
                    Connection.Close();
                    Connection.Open();
                }
            }
            catch (MySqlException ex)
            {


                //if (ex.Message == "Authentication to host 'localhost' for user 'root' using method 'mysql_native_password' failed with message: Unknown database '" + Properties.Settings.Default.database + "'")
                //{
                //    DBsetup dBsetup = new DBsetup(Form.ActiveForm);
                //    dBsetup.ShowDialog();
                //}

            }
            //AllocConsole();
            // Console.WriteLine();

        }

        public string LoadServer()
        {
            string serverIpAddress = Properties.Settings.Default.serverIp;
            string PCTYPE = Properties.Settings.Default.deviceType;

            if (PCTYPE == "CLIENT")
            {
                return serverIpAddress;

            }
            return Properties.Settings.Default.ipaddress;

        }
        public int IsCheckTable()
        {
            MainConnectionString = Properties.Settings.Default.connString + "Pooling=false; Max Pool Size = 50000; Min Pool Size = 5;Charset=utf8";
            Connection = new MySqlConnection(MainConnectionString);
            Connection.Close();
            Connection.Open();
            Query = String.Format("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'  AND TABLE_SCHEMA='{0}';", Properties.Settings.Default.database);
            command = new MySqlCommand(Query, Connection);
            Reader = command.ExecuteReader();

            if (Reader.HasRows)
            {
                return 1;
            }
            Reader.Close();
            Connection.Close();

            return 0;

        }

        public MySqlCommand CommandMethod(MySqlCommand Command)
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                Command = new MySqlCommand(Query, Connection);
                Command.CommandText = Query;

                return Command;

                // Connection.Close();
            }
            catch (Exception exception)
            {
                return Command;
            }
        }

        public MySqlDataReader ReaderMethod(MySqlDataReader reader, MySqlCommand Command)
        {
            try
            {
                if (Command.Connection.State == ConnectionState.Closed)
                {
                    Command.Connection.Open();
                }
                MySqlDataReader read = Command.ExecuteReader();
                return read;
            }
            catch (Exception exception)
            {
                return reader;
            }
        }
        public MySqlTransaction TransactionMethod(MySqlTransaction transaction)
        {
            transaction = Connection.BeginTransaction();

            return transaction;
        }

        public MySqlDataAdapter GetAdapter(MySqlDataAdapter adapter)
        {
            adapter = new MySqlDataAdapter(Query, Connection);

            return adapter;
        }

        public static bool IsExistDatatabase(ConnectionModelSave modelSave)
        {
            try
            {
                string con = "SERVER=" + modelSave.ipadderss + ";UID=" + modelSave.username + ";PASSWORD=" + modelSave.password + ";Charset=utf8";
                MySqlConnection mySqlConnection = new MySqlConnection(con);
                string Query = String.Format("SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME ='{0}';", modelSave.database);
                mySqlConnection.Open();
                MySqlCommand command = new MySqlCommand(Query, mySqlConnection);
                MySqlDataReader Reader = command.ExecuteReader();

                if (Reader.HasRows)
                {
                    return true;
                }
                Reader.Close();
                mySqlConnection.Close();
                return false;
            }
            catch (Exception exception)
            {
                return false;

            }

        }

        public static int CreateDataBase(ConnectionModelSave modelSave)
        {
            string con = "SERVER=" + modelSave.ipadderss + ";UID=" + modelSave.username + ";PASSWORD=" + modelSave.password + ";Charset=utf8";
            MySqlConnection connection = new MySqlConnection(con);
            connection.Open();
            string Query = String.Format("create database {0};", modelSave.database);
            MySqlCommand command = new MySqlCommand(Query, connection);
            int count = command.ExecuteNonQuery();
            connection.Close();


            return count;
        }

        public static bool closeDBConnection()
        {
            MySqlConnection conn = new MySqlConnection(MainConnectionString);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return true;
        }


    }
}
