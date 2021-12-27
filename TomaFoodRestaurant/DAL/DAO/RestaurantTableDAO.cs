using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.CombineReader;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
    public class RestaurantTableDAO : GatewayConnection
    {

        RestaurantTableReader _restaurantTable = new RestaurantTableReader();
        public List<RestaurantTable> GetRestaurantTable()
        {

            List<RestaurantTable> aAreaCoverages = new List<RestaurantTable>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT id,restaurant_id,name,person,table_shape,sort_order,current_status,strftime(update_time)update_time,MergeStatus FROM rcs_restaurant_table;");

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            // dataRow = command.ExecuteReader();
            DT.Load(Reader);
            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    RestaurantTable coverage = _restaurantTable.ReaderToReadRestaurantTable(DT, rowCount);
                    aAreaCoverages.Add(coverage);
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }

                rowCount++;
            }


            return aAreaCoverages;
        }

        internal RestaurantTable GetRestaurantTableByTableId(int tableId)
        {
            RestaurantTable aRestaurantTable = new RestaurantTable();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            Query = String.Format("SELECT id,restaurant_id,name,person,table_shape,sort_order,current_status,strftime(update_time)update_time,MergeStatus FROM rcs_restaurant_table where id={0};", tableId);

            command = CommandMethod(command);

            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    aRestaurantTable = _restaurantTable.ReaderToReadRestaurantTable(DT, rowCount);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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
            Query = String.Format("SELECT id,restaurant_id,name,person,table_shape,sort_order,current_status,strftime(update_time)update_time,MergeStatus FROM rcs_restaurant_table where MergeStatus={0};", mergeId);

            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            // dataRow = command.ExecuteReader();
            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {

                try
                {
                    RestaurantTable aRestaurantTable = _restaurantTable.ReaderToReadRestaurantTable(DT,rowCount);
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

            Query = String.Format("UPDATE [rcs_restaurant_table] SET [person] = {0}, [current_status] = '{1}', [update_time] = '{2}',[MergeStatus]={4} where id={3} ;",
                aRestaurantTable.Person, aRestaurantTable.CurrentStatus, aRestaurantTable.UpdateTime, aRestaurantTable.Id, aRestaurantTable.MergeStatus);



            try
            {
                command = CommandMethod(command);

                lastId = command.ExecuteNonQuery();

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
            Query = String.Format("SELECT id,restaurant_id,name,person,table_shape,sort_order,current_status,strftime(update_time)update_time,MergeStatus FROM rcs_restaurant_table where name='{0}';", tableNumber);
            // dataRow = command.ExecuteReader();
            command = CommandMethod(command);
            Reader = ReaderMethod(Reader, command);
            DT.Load(Reader);
            int rowCount = 0;
            while (DT.Rows.Count > rowCount)
            {


                try
                {
                    aRestaurantTable = _restaurantTable.ReaderToReadRestaurantTable(DT,rowCount);

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

                Query = String.Format("UPDATE [rcs_restaurant_table] SET MergeStatus={1} WHERE MergeStatus={0}", aTable.MergeStatus, 0);


                try
                {
                    command = CommandMethod(command);
                    lastId = command.ExecuteNonQuery();

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
