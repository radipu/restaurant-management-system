using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.CombineReader;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.DAL.DAO
{
   public class MySqlCustomerRecentItemDAO:MySqlGatewayServerConnection
    {
        RecentCustomerReader _customerReader = new RecentCustomerReader();
        public List<CustomerRecentItemMD> GetCustomerRecentItemMd(int customerId)
        {
            List<CustomerRecentItemMD> aCustomerRecentItemMds = new List<CustomerRecentItemMD>();

            try
            {

                Query = String.Format("SELECT * FROM rcs_customer_order_items where customer_id=@customerId ORDER BY `time_added` DESC LIMIT 15");
                

                command = CommandMethod(command);
                command.Parameters.AddWithValue("@customerId", customerId);

                Reader = ReaderMethod(Reader, command);
                DataTable dt = new DataTable();
                dt.Load(Reader);
                bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);
                int rowCount = 0;

                while (dt.Rows.Count > rowCount)
                {

                    try
                    {
                        CustomerRecentItemMD items = _customerReader.ReaderToReadCustomerRecentItem(dt, rowCount);
                        aCustomerRecentItemMds.Add(items);

                    }
                    catch (Exception exception)
                    {
                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                        aErrorReportBll.SendErrorReport(exception.ToString());
                    }
                    rowCount++;
                }

            }
            catch (Exception)
            {

            }


            return aCustomerRecentItemMds;

        }



        public string InsertCustomerRecentItems(List<CustomerRecentItemMD> aCustomerRecentItemMds)
        {
            long lastId = 0;


            foreach (CustomerRecentItemMD item in aCustomerRecentItemMds)
            {

                Query = String.Format("INSERT INTO rcs_customer_order_items (customer_id,recipe_id,package_id,time_added)" +
                                             "VALUES(@customerId,@recipe_id,@package_id,@time_added)");
                try
                {
                    command = CommandMethod(command);
                    command.Parameters.AddWithValue("@customerId", item.customer_id);
                    command.Parameters.AddWithValue("@recipe_id", item.recipe_id);
                    command.Parameters.AddWithValue("@package_id", item.package_id);
                    command.Parameters.AddWithValue("@time_added", item.time_added);

                    lastId = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    //      MessageBox.Show(ex.ToString());
                }




            }
            bool readConnection3 = CommonMethodConectionReaderClose.Connection_ReaderClose(Connection, Reader);

            return (int)lastId > 0 ? "Items has been added successfully" : "Something wrong please try again.";
        }
    }
}
