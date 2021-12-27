using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL
{
  public  class GatewayConnection
  {
      public SQLiteCommand command { get; set; }
      public SQLiteConnection Connection { get; set; }
      public SQLiteDataAdapter Adapter { get; set; }
      public SQLiteDataReader Reader { get; set; }

     
      public string Query { get; set; }

      public SQLiteTransaction Transaction { get; set; }
      public GatewayConnection()
      {
          string MainConnectionString = GlobalSetting.serverConnectionString;

          Connection= new SQLiteConnection(MainConnectionString,true);

          
      }

      public SQLiteCommand CommandMethod(SQLiteCommand Command)
       {
          try
          {
              if (Connection.State == ConnectionState.Closed)
              {
                  Connection.Open();
              }
              else if (Connection.State == ConnectionState.Open)
              {
                  Connection.Close();
                  Connection.Open();
              }

              Command = new SQLiteCommand(Query, Connection);
              Command.CommandText = Query;

          }
          catch (Exception exception)
          {

              ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
              aErrorReportBll.SendErrorReport(exception.ToString());
              return Command;
          }
           
           return Command;
       }
      public SQLiteDataReader ReaderMethod(SQLiteDataReader reader,SQLiteCommand Command)
      {
          try
          {

              reader = Command.ExecuteReader();
             
          }
          catch (Exception exception)
          {
              ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
              aErrorReportBll.SendErrorReport(exception.ToString());
              return reader;
          }
          return reader;
      }

      public SQLiteTransaction TransactionMethod(SQLiteTransaction transaction)
      {
        
          transaction = Connection.BeginTransaction(IsolationLevel.Serializable);

          return transaction;}

      public SQLiteDataAdapter GetAdapter(SQLiteDataAdapter adapter)
      {
          adapter=new SQLiteDataAdapter(Query,Connection);
         
          return adapter;
      }


      public static bool IsExistDatatabase(ConnectionModelSave modelSave)
      {
          try
          {
              string path = File.ReadAllText("Config/ppp.txt");

              if (File.Exists(path))
              {
                  GlobalSetting.ConnectionString = "Data Source=" + path + ";Version=3; busy timeout=10000; Pooling=True; Max Pool Size=100;datetimeformat=CurrentCulture;";
                  GlobalSetting.serverConnectionString = "Data Source=" + path + ";Version=3; busy timeout=10000; Pooling=True; Max Pool Size=100;datetimeformat=CurrentCulture;";
                  return false;
              }

          
              return true;
          }
          catch (Exception exception)
          {
              MessageBox.Show(exception.InnerException.Message);
              return false;

          }
      }
  }
}
