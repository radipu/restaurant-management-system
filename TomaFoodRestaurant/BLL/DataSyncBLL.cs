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
    public class DataSyncBLL
    {

        public void syncTableStatus(int tableId, string status)
        {
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            var aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

            string url = GlobalVars.hostUrl + "restaurantcontrol/request/crud/update_table_status/"+ aRestaurantInformation.Id + "/" + tableId;
            var result = syncServer(url, "POST", "{\"status\": \"" + status + "\"}");
            Console.WriteLine(result);
        }
  


        public static string syncServer(string url, string method = "GET", string data = null)
        {
            try
            {
                Console.WriteLine("{0} seconds with one transaction.", DateTime.Now);
              
                string result = "";
                using (var wb = new WebClient())
                {
                    if(method == "POST")
                    {
                        var post_data = new NameValueCollection();
                        post_data["data"] = data;
                        var response = wb.UploadValues(url, "POST", post_data);
                        result = Encoding.UTF8.GetString(response);
                    }

                    if(method == "GET")
                    {
                        var response = wb.OpenRead(url);
                        result = response.ToString();
                    }
                    
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
             
        }
        
    }
}





