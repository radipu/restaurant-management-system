using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using DevExpress.DataAccess.Native.DB;using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;
using DataTable = System.Data.DataTable;

namespace TomaFoodRestaurant.BLL
{
    public class ConnectToServerBLL
    {
        public void OrderSyncronise()
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            CustomerBLL aCustomerBll = new CustomerBLL();
            List<RestaurantOrder> aListOfFinishedOrder = aRestaurantOrderBLL.GetFinishedOrder("finished", 0);
            aListOfFinishedOrder = aListOfFinishedOrder.Where(a => a.Status.ToLower() != "cancelled").Where(b => b.Status != "").ToList();
           
            bool flag = false;
            if (aListOfFinishedOrder.Count > 0)
            {
                foreach (RestaurantOrder order in aListOfFinishedOrder)
                {

                    if (order.PaymentMethod.ToLower() == "cash")
                    {
                        order.PaymentMethod = "cash";
                    }

                    else if (order.PaymentMethod.ToLower() == "card")
                    {
                        order.PaymentMethod = "card";
                    }
                    else if (order.PaymentMethod.ToLower() == "split")
                    {
                        order.PaymentMethod = "split";

                    }
                    else
                    {
                        order.PaymentMethod = "cash";
                        order.CashAmount = order.TotalCost;
                    }

                    if (order.OrderStatus.ToLower() == "paid")
                    {
                        order.OrderStatus = "paid";
                    }
                    else if (order.OrderStatus.ToLower() == "pending")
                    {
                        order.OrderStatus = "pending";
                    }

                    if (order.Status.ToLower() == "paid")
                    {
                        order.Status = "paid";
                    }
                    else if (order.Status.ToLower() == "pending")
                    {
                        order.Status = "pending";
                    }

                    if (order.OrderType.ToLower() == "IN")
                    {
                        order.OrderType = "IN";
                    }
                    else if (order.OrderType.ToLower() == "wait")
                    {
                        order.OrderType = "CLT";
                    }

                    LocalOrderSyn localOrderSyn = new LocalOrderSyn();

                    List<CustomerOrderItems> customerItems = aCustomerBll.GetCustomerItems(order.CustomerId);
                    if (GlobalSetting.DbType == "MYSQL")
                    {
                        localOrderSyn.RestaurantOrder = new MySqlRestaurantOrderDAO().GetRestaurantOrderByOrderOnlineSyncId(order.Id);
                        localOrderSyn.RestaurantUsers = new MySqlCustomerDAO().GetResturantCustomerByCustomerIdDataTableSync(order.CustomerId);
                        localOrderSyn.OrderPackages = new MySqlRestaurantOrderDAO().GetRestaurantOrderPackageDataTableSync(order.Id);
                        localOrderSyn.OrderItems = new MySqlRestaurantOrderDAO().GetRestaurantOrderRecipeItemsTest(order.Id);
                        localOrderSyn.MemberShips = new MySqlCustomerDAO().GetMemberShipByUserIdDataTableByMemberShips(order.UserId, order.RestaurantId);
                    }
                    else
                    {
                        localOrderSyn.RestaurantOrder = new RestaurantOrderDAO().GetRestaurantOrderByOrderOnlineSyncId(order.Id);
                        localOrderSyn.RestaurantUsers = new CustomerDAO().GetResturantCustomerByCustomerIdDataTableSync(order.CustomerId);
                        localOrderSyn.OrderPackages = new RestaurantOrderDAO().GetRestaurantOrderPackageDataTableSync(order.Id);
                        localOrderSyn.OrderItems = new RestaurantOrderDAO().GetRestaurantOrderRecipeItemsTest(order.Id);
                        localOrderSyn.MemberShips = new CustomerDAO().GetMemberShipByUserIdDataTableByMemberShips(order.UserId, order.RestaurantId);

                    }
                    InsertData(localOrderSyn.RestaurantOrder);
                    //if (Convert.ToInt32(result) > 0)
                    //{
                    //    flag = true;
                    //    RestaurantOrder aRestaurantOrder = new RestaurantOrder();
                    //    aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(order.Id);
                    //    aRestaurantOrder.OnlineOrderId = Convert.ToInt32(result);
                    //    aRestaurantOrder.IsSync = 1;
                    //    aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);
                    //}

                    System.Threading.Thread.Sleep(1000);
                }
            }
            //   return flag == true ? "Operation Successfull!" : "Orders are not moved successfully! Try later";
        }
       
        public void InsertData(DataTable rcsOrder)
        { 
             string MainConnectionString = Properties.Settings.Default.connString + "password=" + Properties.Settings.Default.password + ";" + "Pooling=false; Max Pool Size = 50000; Min Pool Size = 5";
       
            MySqlConnection conn=new MySqlConnection(MainConnectionString);
            using (MySqlTransaction tran = conn.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    cmd.CommandText = "SELECT * FROM rcs_order";
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.UpdateBatchSize = 1000;
                        using (MySqlCommandBuilder cb = new MySqlCommandBuilder(da))
                        {
                            da.Update(rcsOrder);
                            tran.Commit();
                        }
                    }
                }
            }
        }
    }
}
