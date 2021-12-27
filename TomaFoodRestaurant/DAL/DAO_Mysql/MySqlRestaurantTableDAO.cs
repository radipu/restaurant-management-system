using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.CombineReader;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class MySqlRestaurantTableDAO : MySqlGatewayServerConnection
    {
        RestaurantTableReader _restaurantTable = new RestaurantTableReader();
        public List<RestaurantTable> GetRestaurantTable()
        {


            List<RestaurantTable> restaurantTables = new List<RestaurantTable>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT id,restaurant_id,name,person,table_shape,sort_order,current_status,date_format(update_time,update_time) AS update_time,MergeStatus FROM rcs_restaurant_table;");

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
           
            // dataRow = command.ExecuteReader();
            DT.Load(Reader);


            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);


            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    RestaurantTable coverage = _restaurantTable.ReaderToReadRestaurantTable(DT, rowCount);
                    restaurantTables.Add(coverage);
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

                rowCount++;
            }


            return restaurantTables;
        }

        internal RestaurantTable GetRestaurantTableByTableId(int tableId)
        {
            RestaurantTable aRestaurantTable = new RestaurantTable();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT id,restaurant_id,name,person,table_shape,sort_order,current_status,DATE_FORMAT(update_time, '%d-%m-%Y %H:%i') as update_time,MergeStatus FROM rcs_restaurant_table where id={0};", tableId);

            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    aRestaurantTable = _restaurantTable.ReaderToReadRestaurantTable(DT, rowCount);

                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }
                rowCount++;

            }
            
            return aRestaurantTable;
        
        }


        internal List<RestaurantTable> GetRestaurantTableByMergeId(int mergeId)
        {
            List<RestaurantTable> aRestaurantTables = new List<RestaurantTable>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT id,restaurant_id,name,person,table_shape,sort_order,current_status,update_time,MergeStatus FROM rcs_restaurant_table where MergeStatus={0};", mergeId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            // dataRow = command.ExecuteReader();
            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    RestaurantTable aRestaurantTable = _restaurantTable.ReaderToReadRestaurantTable(DT, rowCount);
                    aRestaurantTables.Add(aRestaurantTable);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
                rowCount++;

            }


            return aRestaurantTables;
        }

        internal int UpdateRestaurantTable(RestaurantTable aRestaurantTable)
        {
            long lastId = 0;

            Query =
                String.Format(
                    "UPDATE rcs_restaurant_table SET person = @person, update_time=@update_time, current_status = @current_status,MergeStatus=@mergerStatus where id=@id");

               


            try
            {
                command = CommandMethod(command);
                command.Parameters.AddWithValue("@person", aRestaurantTable.Person);
                command.Parameters.AddWithValue("@current_status", aRestaurantTable.CurrentStatus);
                command.Parameters.AddWithValue("@update_time", aRestaurantTable.UpdateTime);
                command.Parameters.AddWithValue("@mergerStatus", aRestaurantTable.MergeStatus);
                command.Parameters.AddWithValue("@id", aRestaurantTable.Id);

                lastId = (long)Convert.ToUInt64(command.ExecuteNonQuery());

                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }





            return (int)lastId;
        }

        internal RestaurantTable GetRestaurantTableByTableNumber(string tableNumber)
        {

            RestaurantTable aRestaurantTable = new RestaurantTable();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT id,restaurant_id,name,person,table_shape,sort_order,current_status,update_time,MergeStatus FROM rcs_restaurant_table where name='{0}';", tableNumber);
            // dataRow = command.ExecuteReader();
            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);

            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {


                try
                {
                    aRestaurantTable = _restaurantTable.ReaderToReadRestaurantTable(DT, rowCount);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
                rowCount++;
            }



            return aRestaurantTable;
        }

        internal string ToAvailableMergeTable(RestaurantTable aTable)
        {
            int lastId = 0;
            try
            {

                Query = String.Format("UPDATE rcs_restaurant_table SET MergeStatus={1} WHERE MergeStatus={0}", aTable.MergeStatus, 0);


                try
                {
                    command = CommandMethod(command);
                    lastId = command.ExecuteNonQuery();

                    bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }
            catch (Exception ex)
            {

            }

            return (int)lastId > 0 ? "Yes" : "No";
        }
    }
}
