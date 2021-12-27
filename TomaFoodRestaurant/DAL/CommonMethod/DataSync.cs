using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;
using TomaFoodRestaurant.DAL.DAO;

namespace TomaFoodRestaurant.DAL.CommonMethod
{
    public class DataSync
    {

        /// <summary>
        /// Sync table status while taking order from local machine
        /// </summary>
        /// <param name="tableId">Table id to update status</param>
        /// <param name="status">The status to update (available or busy)</param>
        public static async void syncTableStatus(int tableId, string status)
        {
            try
            {
                DataSyncBLL dataSyncBll = new DataSyncBLL();
                await Task.Factory.StartNew(() =>
                {
                    dataSyncBll.syncTableStatus(tableId, status);
                });

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

        }

        /// <summary>
        /// Sync a single customer
        /// </summary>
        /// <param name="aUser"></param>
        public static async void SyncCustomer(RestaurantUsers aUser)
        {
            
            try
            {
                CustomerBLL customerBll = new CustomerBLL();
                await Task.Factory.StartNew(() =>
                {
                    customerBll.CustomerSyncronise(aUser);
                });

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        /// <summary>
        /// Sync pending customer from local db to server
        /// 
        /// </summary>
        public static void SyncPendingCustomers()
        {
            //get all pending customer
            CustomerBLL customerBLL = new CustomerBLL();

            while (true)
            {
                var customers = new MySqlCustomerDAO().GetResturantCustomerDataTableByUpdateStatus(0, 20);

                if(customers.Rows.Count <= 0)
                {
                    break;
                }

                RestaurantInformationBLL restaurantInformationBLL = new RestaurantInformationBLL();
                RestaurantInformation restaurant = restaurantInformationBLL.GetRestaurantInformation();

                var postData = new
                {
                    RestaurantUsers = customers,
                    MemberShips = customerBLL.GetMembershipsByResId(restaurant.Id),
                    restaurantId = restaurant.Id,
                    bulkSync = true
                };


                try
                {
                    var jsonPostData = Newtonsoft.Json.JsonConvert.SerializeObject(postData);

                    var response = customerBLL.CustomerSyncToServer(jsonPostData, GlobalVars.hostUrl + "restaurantcontrol/request/json_add_customer");

                    var responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(response);


                    foreach (int customerId in responseData)
                    {
                        RestaurantUsers user = customerBLL.GetResturantCustomerByCustomerId(customerId);
                        user.IsUpdate = 1;
                        customerBLL.UpdateRestaurantCustomer(user);
                    }

                    
                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                    break;
                }
            }            
        }

    }
}
