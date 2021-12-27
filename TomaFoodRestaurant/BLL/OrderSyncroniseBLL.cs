using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;
using System.Collections.Specialized;

namespace TomaFoodRestaurant.BLL
{
    public class OrderSyncroniseBLL
    {

        public void OrderSyncronise(string syncType)
        {
            //if (!OthersMethod.CheckServerConneciton())
            //{
            //    return;
            //}
            
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            CustomerBLL aCustomerBll = new CustomerBLL();
            List<RestaurantOrder> aListOfFinishedOrder = aRestaurantOrderBLL.GetFinishedOrder("finished", 0); 
            aListOfFinishedOrder = aListOfFinishedOrder.Where(a => a.Status.ToLower() != "cancelled").Where(b => b.Status != "").ToList();
            var createJson = new JavaScriptSerializer();
            bool flag = false;
            if (aListOfFinishedOrder.Count > 0)
            {
                foreach (RestaurantOrder order in aListOfFinishedOrder)
                {
                     
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
                
                    string JSONresult =  JsonConvert.SerializeObject(localOrderSyn);
                   
                    string result = "0";
                    if (syncType == "all" || syncType == "order" || syncType == "customer")
                    {
                        urls.AcceptUrl = GlobalVars.hostUrl;
                       string url = urls.AcceptUrl + "restaurantcontrol/request/json_tpos_order";
                         result = NewOrderToServer(JSONresult, url);
                       //  Console.WriteLine(result);
                    }
                    if (result != "" && Convert.ToInt32(result) > 0)
                    {
                        flag = true;
                        RestaurantOrder aRestaurantOrder = new RestaurantOrder();
                        aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(order.Id);
                        aRestaurantOrder.OnlineOrderId = Convert.ToInt32(result);
                        aRestaurantOrder.IsSync = 1;
                        aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);
                    }
                    else {
                        File.AppendAllText("Config/log.txt", "Order Sync issue : " + result.ToString() + "\n\n");
                        RestaurantOrder aRestaurantOrder = new RestaurantOrder();
                        aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(order.Id);
                        aRestaurantOrder.IsSync = -1;
                        aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
  
        internal int SingleOrderSyncronise(RestaurantOrder order)
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl urls = aGlobalUrlBll.GetUrls();
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            CustomerBLL aCustomerBll = new CustomerBLL();
            bool flag = false;
            int res = 0;
            if (order.OnlineOrderId > 0)
            {
                if (order.PaymentMethod != "")
                {
                    order.PaymentMethod = order.PaymentMethod.ToLower();
                }
              else
                {
                    order.PaymentMethod = "cash";
                    order.CashAmount = order.TotalCost;
                    order.CardAmount = 0;
                }

                order.OrderStatus = order.OrderStatus.ToLower();
                order.Status = order.Status.ToLower();
                order.OrderType = order.OrderType.ToUpper();

                if (order.OrderType.ToLower() == "wait")
                {
                    order.OrderType = "CLT";
                }
                
                aRestaurantOrderBLL.UpdateRestaurantOrder(order);
                LocalOrderSyn localOrderSyn = new LocalOrderSyn();
                 
                //if (GlobalSetting.DbType == "MYSQL")
                //{
                    localOrderSyn.RestaurantOrder = new MySqlRestaurantOrderDAO().GetRestaurantOrderByOrderOnlineSyncId(order.Id);
                //}
                //else
                //{
                //    localOrderSyn.RestaurantOrder = new RestaurantOrderDAO().GetRestaurantOrderByOrderOnlineSyncId(order.Id);
                 
                //}
              
                string JSONresult = "MAINORDER=" + JsonConvert.SerializeObject(localOrderSyn);
                urls.AcceptUrl = Properties.Settings.Default.backend;
                string url = urls.AcceptUrl + "restaurantcontrol/request/update_tpos_order";
                res = Convert.ToInt32(UpdateOrderToServer(JSONresult, url));

                //if (res > 0)
                //{
                //    RestaurantOrder aRestaurantOrder = new RestaurantOrder();
                //    aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(order.Id);
                //    aRestaurantOrder.OnlineOrderId = res;
                //    aRestaurantOrder.IsSync = 1;
                //    aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);
                //}

                RestaurantOrder aRestaurantOrder = new RestaurantOrder();
                aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(order.Id);
                if (res  > 0)
                {
                    flag = true;
                    aRestaurantOrder.OnlineOrderId = Convert.ToInt32(res);
                    aRestaurantOrder.IsSync = 1;
                    aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);
                }
                else
                {

                    aRestaurantOrder.IsSync = -1;
                    aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);
                }                                                                                     

            }
            return res;
        }

        public List<string> GetColumnNames(string tableName)
        {

            if (GlobalSetting.DbType == "SQLITE")
            {
                DatabaseSyncDAO orDatabaseSyncDao = new DatabaseSyncDAO();
                return orDatabaseSyncDao.GetColumnNames(tableName);
            }
            else
            {
                MySqlDatabaseSyncDAO orDatabaseSyncDao = new MySqlDatabaseSyncDAO();
                return orDatabaseSyncDao.GetColumnNames(tableName);
            }

        }



        public bool AddOrderToServer(string data, string url)
        {
            try
            {
                string postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Post the data to the right place.
                Uri target = new Uri(url);
                WebRequest request = WebRequest.Create(target);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                string result = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            result = readStream.ReadToEnd();
                        }
                    }
                }

                if (result == "DONE" || result == "EXIST") return true;
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;


        }
        public string NewUpdateMenu(string data, string url)
        {
            try
            {
                Console.WriteLine("{0} seconds with one transaction.", DateTime.Now);
                string postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                Uri target = new Uri(url);
                WebRequest request = WebRequest.Create(target);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }

                string result = "0";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            result = readStream.ReadToEnd();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return "0";
            }




        }

        public string NewOrderToServer(string data, string url)
        {
            try
            {
                Console.WriteLine("{0} seconds with one transaction.", DateTime.Now);
              
                string result = "";
                using (var wb = new WebClient())
                {
                    var post_data = new NameValueCollection();
                    post_data["MAINORDER"] = data; 

                    var response = wb.UploadValues(url, "POST", post_data);
                    result = Encoding.UTF8.GetString(response);
                }
                return result;
            }
            catch (Exception ex)
            {
                return '0'.ToString();
            }
             

        }

        public string UpdateOrderToServer(string data, string url)
        {
            string result = string.Empty;
            try
            {
                string postData = data;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Post the data to the right place.
                Uri target = new Uri(url);
                WebRequest request = WebRequest.Create(target);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }


                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            result = readStream.ReadToEnd();
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                return "0";
            }


        }
    }
}





