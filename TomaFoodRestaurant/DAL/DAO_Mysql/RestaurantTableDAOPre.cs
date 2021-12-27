using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL
{
   public class RestaurantTableDAO
   {
       string MainConnectionString = GlobalSetting.serverConnectionString;
        private RestaurantTable ReaderToReadRestaurantTable(IDataReader oReader)
        {
            RestaurantTable arcs_restaurant_table = new RestaurantTable();
            if (oReader["id"] != DBNull.Value)
            {
                arcs_restaurant_table.Id = Convert.ToInt32(oReader["id"]);
            }
            if (oReader["restaurant_id"] != DBNull.Value)
            {
                arcs_restaurant_table.RestaurantId = Convert.ToInt32(oReader["restaurant_id"]);
            }
            if (oReader["name"] != DBNull.Value)
            {
                arcs_restaurant_table.Name = Convert.ToString(oReader["name"]);
            }
            if (oReader["person"] != DBNull.Value)
            {
                arcs_restaurant_table.Person = Convert.ToInt32(oReader["person"]);
            }
            if (oReader["table_shape"] != DBNull.Value)
            {
                arcs_restaurant_table.TableShape = Convert.ToString(oReader["table_shape"]);
            }
            if (oReader["sort_order"] != DBNull.Value)
            {
                arcs_restaurant_table.SortOrder = Convert.ToInt32(oReader["sort_order"]);
            }
            if (oReader["current_status"] != DBNull.Value)
            {
                arcs_restaurant_table.CurrentStatus = Convert.ToString(oReader["current_status"]);
            }
            try
            {
                if (oReader["update_time"] != DBNull.Value && oReader["update_time"].ToString() != string.Empty)
                {
                    //  string sr = Convert.ToString(oReader["update_time"]);arcs_restaurant_table.UpdateTime = Convert.ToDateTime(oReader["update_time"]);
                }
            }
            catch (Exception exception)
            {
                arcs_restaurant_table.UpdateTime = DateTime.Now;

                //ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                //aErrorReportBll.SendErrorReport(exception.ToString());
            }

            //MergeStatus
            try
            {
                if (oReader["MergeStatus"] != DBNull.Value)
                {
                    arcs_restaurant_table.MergeStatus = Convert.ToInt32(oReader["MergeStatus"]);
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            if (arcs_restaurant_table.CurrentStatus == "bill")
            {
                arcs_restaurant_table.CurrentStatus = "busy";
                arcs_restaurant_table.IsBill = true;
            }


            return arcs_restaurant_table;
        }

        public List<RestaurantTable> GetRestaurantTable()
        {

            List<RestaurantTable> aAreaCoverages = new List<RestaurantTable>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            string query = String.Format("SELECT * FROM rcs_restaurant_table;");

            using (SQLiteConnection c = new SQLiteConnection(MainConnectionString, true))
            {

                using (SQLiteCommand command = new SQLiteCommand(query, c))
                {

                    c.Open();
                    using (SQLiteDataReader dataRow = command.ExecuteReader())
                    {
                        // dataRow = command.ExecuteReader();
                        while (dataRow.Read())
                        {

                            try
                            {
                                RestaurantTable coverage = ReaderToReadRestaurantTable(dataRow);
                                aAreaCoverages.Add(coverage);
                            }
                            catch (Exception exception)
                            {
                                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                aErrorReportBll.SendErrorReport(exception.ToString());
                            }


                        }

                    }
                    c.Close();
                }
            }

            return aAreaCoverages;
        }

        internal RestaurantTable GetRestaurantTableByTableId(int tableId)
        {
            RestaurantTable aRestaurantTable = new RestaurantTable();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            string query = String.Format("SELECT * FROM rcs_restaurant_table where id={0};", tableId);

            using (SQLiteConnection c = new SQLiteConnection(MainConnectionString, true))
            {

                using (SQLiteCommand command = new SQLiteCommand(query, c))
                {

                    c.Open();
                    using (SQLiteDataReader dataRow = command.ExecuteReader())
                    {
                        // dataRow = command.ExecuteReader();
                        while (dataRow.Read())
                        {

                            try
                            {
                                aRestaurantTable = ReaderToReadRestaurantTable(dataRow);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }


                        }

                    }
                    c.Close();
                }
            }

            return aRestaurantTable;
        }


        internal List<RestaurantTable> GetRestaurantTableByMergeId(int mergeId)
        {
            List<RestaurantTable> aRestaurantTables = new List<RestaurantTable>();
            //  SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            string query = String.Format("SELECT * FROM rcs_restaurant_table where MergeStatus={0};", mergeId);

            using (SQLiteConnection c = new SQLiteConnection(MainConnectionString, true))
            {

                using (SQLiteCommand command = new SQLiteCommand(query, c))
                {

                    c.Open();
                    using (SQLiteDataReader dataRow = command.ExecuteReader())
                    {
                        // dataRow = command.ExecuteReader();
                        while (dataRow.Read())
                        {

                            try
                            {
                                RestaurantTable aRestaurantTable = ReaderToReadRestaurantTable(dataRow);
                                aRestaurantTables.Add(aRestaurantTable);

                            }
                            catch (Exception exception)
                            {
                                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                aErrorReportBll.SendErrorReport(exception.ToString());
                            }


                        }

                    }
                    c.Close();
                }
            }

            return aRestaurantTables;
        }

        internal int UpdateRestaurantTable(RestaurantTable aRestaurantTable)
        {
            long lastId = 0;
            using (SQLiteConnection c = new SQLiteConnection(MainConnectionString, true))
            {
                c.Open();
                //  using (SQLiteTransaction mytransaction = c.BeginTransaction())
                {
                    string query = String.Format("UPDATE [rcs_restaurant_table] SET [person] = {0}, [current_status] = '{1}', [update_time] = '{2}',[MergeStatus]={4} where id={3} ;",
                        aRestaurantTable.Person, aRestaurantTable.CurrentStatus, aRestaurantTable.UpdateTime, aRestaurantTable.Id, aRestaurantTable.MergeStatus);

                    using (SQLiteCommand command = new SQLiteCommand(query, c))
                    {

                        try
                        {

                            lastId = command.ExecuteNonQuery();

                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }


                    }
                    //  mytransaction.Commit();
                }

            }
            return (int)lastId;
        }

        internal RestaurantTable GetRestaurantTableByTableNumber(string tableNumber)
        {

            RestaurantTable aRestaurantTable = new RestaurantTable();
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            string query = String.Format("SELECT * FROM rcs_restaurant_table where name='{0}';", tableNumber);

            using (SQLiteConnection c = new SQLiteConnection(MainConnectionString, true))
            {

                using (SQLiteCommand command = new SQLiteCommand(query, c))
                {

                    c.Open();
                    using (SQLiteDataReader dataRow = command.ExecuteReader())
                    {
                        // dataRow = command.ExecuteReader();
                        while (dataRow.Read())
                        {

                            try
                            {
                                aRestaurantTable = ReaderToReadRestaurantTable(dataRow);

                            }
                            catch (Exception exception)
                            {
                                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                aErrorReportBll.SendErrorReport(exception.ToString());
                            }

                        }

                    }
                    c.Close();
                }
            }

            return aRestaurantTable;
        }

        internal string ToAvailableMergeTable(RestaurantTable aTable)
        {
            int lastId = 0;
            try
            {


                using (SQLiteConnection c = new SQLiteConnection(MainConnectionString, true))
                {


                    string query = String.Format("UPDATE [rcs_restaurant_table] SET MergeStatus={1} WHERE MergeStatus={0}", aTable.MergeStatus, 0);

                    using (SQLiteCommand command = new SQLiteCommand(query, c))
                    {
                        c.Open();
                        try
                        {
                            lastId = command.ExecuteNonQuery();

                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }
                        c.Close();
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return (int)lastId > 0 ? "Yes" : "No";
        }
       
    }
}
