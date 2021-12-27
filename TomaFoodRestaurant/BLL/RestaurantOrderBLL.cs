using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
    public class RestaurantOrderBLL
    {
        internal int InsertRestaurantOrder(RestaurantOrder aRestaurantOrder)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.InsertRestaurantOrder(aRestaurantOrder);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.InsertRestaurantOrder(aRestaurantOrder);
            }

        }

        internal int InsertRestaurantOrderItem(List<OrderItem> aOrderItems)
        {

            //if (GlobalSetting.DbType == "SQLITE")
            //{
            //    RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
            //    return aRestaurantOrderDao.InsertRestaurantOrderItem(aOrderItems);
            //}
            //else
            //{
            MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            return aRestaurantOrderDao.InsertRestaurantOrderItem(aOrderItems);
            //  }
        }

        internal List<KeyValuePair<int, int>> InsertRestaurantOrderPackage(List<OrderPackage> aOrderPackage)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.InsertRestaurantOrderPackage(aOrderPackage);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.InsertRestaurantOrderPackage(aOrderPackage);
            }
        }

        internal RestaurantOrder GetRestaurantOrder(int tableId, string status)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrder(tableId, status);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrder(tableId, status);
            }
        }

        internal List<OrderItem> GetRestaurantOrderRecipeItems(int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderRecipeItems(orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderRecipeItems(orderId);
            }

        }

        internal List<OrderPackage> GetRestaurantOrderPackage(int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderPackage(orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderPackage(orderId);
            }
        }

        internal bool UpdateRestaurantOrderItem(List<OrderItem> aOrderItems, int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateRestaurantOrderItem(aOrderItems, orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateRestaurantOrderItem(aOrderItems, orderId);
            }

        }

        internal List<KeyValuePair<int, int>> UpdateRestaurantOrderPackage(List<OrderPackage> aOrderPackage, int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateRestaurantOrderPackage(aOrderPackage, orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateRestaurantOrderPackage(aOrderPackage, orderId);
            }
        }

        internal List<OrderItem> GetRestaurantOrderRecipeItemByItemId(int orderId, int recipeId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderRecipeItemByItemId(orderId, recipeId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderRecipeItemByItemId(orderId, recipeId);
            }
        }

        internal bool UpdateRestaurantOrderItem(int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateRestaurantOrderItem(orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateRestaurantOrderItem(orderId);
            }
        }


        internal bool UpdateRestaurantOrder(RestaurantOrder aRestaurantOrder)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateRestaurantOrder(aRestaurantOrder);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateRestaurantOrder(aRestaurantOrder);
            }
        }

        internal DataTable GetRestaurantOrderByDate(DateTime startDate, DateTime endDate, RestaurantOrder order = null)
        {
            //if (GlobalSetting.DbType == "SQLITE")
            //{
            //    RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
            //    return aRestaurantOrderDao.GetRestaurantOrderByDate(startDate, endDate);
            //}
            //else
            //{
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByDate(startDate, endDate, order);
            //}
        }

        internal RestaurantOrder GetRestaurantOrderByCustomerId(int customerId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByCustomerId(customerId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByCustomerId(customerId);
            }
        }

        internal RestaurantOrder GetRestaurantOrderByOrderId(int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByOrderId(orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByOrderId(orderId);
            }
        }

        internal bool DeleteItemsAndPackage(List<int> canceItem, List<int> cancelPackage)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteItemsAndPackage(canceItem, cancelPackage);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteItemsAndPackage(canceItem, cancelPackage);
            }
        }
        internal bool DeleteItemsAndPackage(int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteItemsAndPackage(orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteItemsAndPackage(orderId);
            }
        }

        internal RestaurantOrder GetRestaurantOrderByOnlineOrder(long onlineOrderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByOnlineOrder(onlineOrderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByOnlineOrder(onlineOrderId);
            }
        }

        internal List<RestaurantOrder> GetRestaurantOrderForOnline(int onlineOrder, string status)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderForOnline(onlineOrder, status);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderForOnline(onlineOrder, status);
            }
        }

        internal string DeleteOrderByOrderId(int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteOrderByOrderId(orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteOrderByOrderId(orderId);
            }
        }

        internal string UpdateOnlineOrder(int orderId, string status)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateOnlineOrder(orderId, status);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateOnlineOrder(orderId, status);
            }
        }

        internal RestaurantOrder GetRestaurantOrderByCustomerAndDate(DateTime toDate, DateTime fromDate, int customerId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByCustomerAndDate(toDate, fromDate, customerId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByCustomerAndDate(toDate, fromDate, customerId);
            }
        }

        public int GetMaxOrderNumber(DateTime toDate, DateTime fromDate)
        {
            MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            return aRestaurantOrderDao.GetMaxOrderNumber(toDate, fromDate);
        }

        public List<RestaurantOrder> GetRestaurantOrderByDateForOrderNo(DateTime toDate, DateTime fromDate)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {

                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByDateForOrderNo(toDate, fromDate);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetRestaurantOrderByDateForOrderNo(toDate, fromDate);
            }
        }

        public List<RestaurantOrder> GetFinishedOrder(string status, int onlineStatus)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetFinishedOrder(status, onlineStatus);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetFinishedOrder(status, onlineStatus);
            }
        }



        public List<RestaurantOrder> GetAllAutoPrintOrder()
        {
             MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            return aRestaurantOrderDao.GetAllAutoPrintOrder();
      
        }
        public List<RestaurantOrder> GetAllOrder(string type = "DEL", string status = "pending")
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetAllOrder(type);
            }
            else
            {

                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetAllOrder(type, status);
            }
        }

        public string DeleteOnlineOrder()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteOnlineOrder();
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteOnlineOrder();
            }
        }

        internal string DeleteAllOrder()
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteAllOrder();
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteAllOrder();
            }
        }

        internal string GetOrderHistory(int papersize, int orderId, GeneralInformation aGeneralInformation, string timebtn = "", bool kitchenPrint = false)
        {
            RestaurantInformationBLL restaurantInformationBLL = new RestaurantInformationBLL();
            RestaurantInformation restaurantInformation = restaurantInformationBLL.GetRestaurantInformation();
            PrintFormat printFormat = new PrintFormat(22);
            OthersMethod aOthersMethod = new OthersMethod();
            RestaurantOrder tempOrder = GetRestaurantOrderByOrderId(orderId);

           // if (Properties.Settings.Default.appLanguage != "Default")
          //  {
                string type = "";

                if (tempOrder.OnlineOrder > 0)
                {
                    type = "WEB ";
                }

                if (aGeneralInformation.OrderType == "DEL")
                {
                    type += "DELIVERY";
                }
                else if (aGeneralInformation.OrderType.ToLower() == "collect" || aGeneralInformation.OrderType.ToUpper() == "CLT")
                {
                    if (aGeneralInformation.PrintOrderType != null && aGeneralInformation.PrintOrderType.ToLower() == "wait")
                    {
                        type += "WAITING";
                    }
                    else
                    {
                        type += "COLLECTION";
                    }

                }
                else if (aGeneralInformation.OrderType.ToLower() == "in")
                {
                    type += "TABLE:" + aGeneralInformation.TableNumber + "-PEOPLE:" + aGeneralInformation.Person;

                }



                //RestaurantInformationBLL aRestaurantInformationBll=new RestaurantInformationBLL();
                //RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

                string result = "";

                string defaultTime = aGeneralInformation.OrderType == "DEL" ? restaurantInformation.DeliveryTime : restaurantInformation.CollectionTime;


                if (aGeneralInformation.OrderType != "IN")
                {

                    if (timebtn.ToLower() == "time" || timebtn.ToLower() == "wait")
                    {
                        result += printFormat.CenterTextWithWhiteSpace(type) + "\n" + printFormat.CenterTextWithWhiteSpace("ASAP (" + defaultTime + ")");
                    }
                    else
                    {
                        if (aOthersMethod.IsTimeFormatValid(aGeneralInformation.DeliveryTime) != "00:00")
                        {
                            result += type + "|" + Convert.ToDateTime(aGeneralInformation.DeliveryTime).ToString("hh:mmtt"); // aOthersMethod.IsTimeFormatValid(aGeneralInformation.DeliveryTime);
                        }
                        else
                        {
                            result += printFormat.CenterTextWithWhiteSpace(type) + "\n" + printFormat.CenterTextWithWhiteSpace("ASAP (" + defaultTime + ")");
                        }

                    }
                }
                else
                {
                    result += type;
                }


          //  }




            return result;

        }


        internal int GetSortOrderByCategory(int catId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetSortOrderByCategory(catId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetSortOrderByCategory(catId);
            }

        }

        internal int GetRecipeTypeIdByCategory(int catId)
        {          
            MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            return aRestaurantOrderDao.GetRecipeTypeIdByCategoryId(catId);
            
        }






        internal string DeleteAllOrderByDate(DateTime toDateTime)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteAllOrderByDate(toDateTime);
            }
            else
            {
                if (!OthersMethod.CheckServerConneciton())
                {
                    return "Network Disconnected";
                }

                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteAllOrderByDate(toDateTime);
            }
        }

        internal List<KeyValuePair<int, int>> InsertOrderPackage(List<OrderPackage> aOrderPackage)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.InsertOrderPackage(aOrderPackage);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.InsertOrderPackage(aOrderPackage);
            }
        }

        internal bool UpdateOrderItemKitchenStatus(int id, int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateOrderItemKitchenStatus(id, orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.UpdateOrderItemKitchenStatus(id, orderId);
            }
        }

        internal bool UpdateKitchenStatus(int orderId)
        {
            //if (GlobalSetting.DbType != "SQLITE")
            //{
            //    //RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
            //    //return aRestaurantOrderDao.UpdateOrderItemKitchenStatus(id, orderId);
            //}
            //else
            //{
            MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            return aRestaurantOrderDao.UpdateKitchenStatus(orderId);
            //}
        }

        internal bool UpdateKitchenStatusForAutoprint(int orderId, List<int> itemIds)
        {
           
            MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            return aRestaurantOrderDao.UpdateKitchenStatusForAutoprint(orderId, itemIds);
            
        }

        internal int GetOrderItemKitchenStatus(int p, int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.GetOrderItemKitchenStatus(p, orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.GetOrderItemKitchenStatus(p, orderId);
            }
        }

        internal bool DeleteItemsAndPackageByOrderId(int orderId)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                RestaurantOrderDAO aRestaurantOrderDao = new RestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteItemsAndPackageByOrderId(orderId);
            }
            else
            {
                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                return aRestaurantOrderDao.DeleteItemsAndPackageByOrderId(orderId);
            }
        }

        internal bool UpdateOrderStatus(RestaurantOrder tempOrder)
        {

            MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            return aRestaurantOrderDao.UpdateOrderStatus(tempOrder);
        }
        internal bool UpdateOrderDelTime(RestaurantOrder tempOrder)
        {

            MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
            return aRestaurantOrderDao.UpdateOrderDelTime(tempOrder);
        }
    }
}
