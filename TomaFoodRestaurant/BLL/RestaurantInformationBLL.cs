using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace TomaFoodRestaurant.BLL
{
   public class RestaurantInformationBLL
    {
       public RestaurantInformation GetRestaurantInformation()
       {
           //if (GlobalSetting.DbType == "SQLITE")
           //{
           //
           //    RestaurantInformationDAO aRestaurantInformationDao = new RestaurantInformationDAO();
           //    return aRestaurantInformationDao.GetRestaurantInformation();
           //}
           //else
           //{                                                                                       
                                                                                              
               MySqlRestaurantInformationDAO aRestaurantInformationDao = new MySqlRestaurantInformationDAO();
               return aRestaurantInformationDao.GetRestaurantInformation();
          
       }

       public RestaurantSync GetRestaurantSyncInformation()
       {
           RestaurantSync aRestaurantSync = new RestaurantSync();
           try
           {
               RestaurantInformation aRestaurantInformation = GetRestaurantInformation();
               GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
               GlobalUrl gUrl = aGlobalUrlBll.GetUrls();
               DatabaseSyncBLL aDatabaseSyncBll = new DatabaseSyncBLL();
               string restaurantDataUrl = gUrl.AcceptUrl + "restaurantcontrol/request/crud/get_restaurant_info/" + aRestaurantInformation.Id;
               string restaurantinfo = aDatabaseSyncBll.GetAllRestaurantDataWithPassword(aRestaurantInformation.Id, restaurantDataUrl);
               aRestaurantSync = JsonConvert.DeserializeObject<RestaurantSync>(restaurantinfo);
               
           }
           catch (Exception)
           {
                                                                                                                                                                                                            
           }

           return aRestaurantSync;
       }

        

       public string updateSoftVersion()
       {
           string postData = "version=2.0.0";
           byte[] byteArray = Encoding.UTF8.GetBytes(postData);
           RestaurantInformation aRestaurantInformation = GetRestaurantInformation();
           GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
           GlobalUrl gUrl = aGlobalUrlBll.GetUrls();

           Uri target = new Uri(gUrl.AcceptUrl + "restaurantcontrol/request/crud/finish_update/" + aRestaurantInformation.Id);
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


           int lastId = 0;
           try
           {

               using (SQLiteConnection c = new SQLiteConnection(GlobalSetting.ConnectionString, true))
               {
                   string query =
                   String.Format("UPDATE [rcs_restaurant] SET update_required='{0}'","2");
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

           return result; 

       }

        /// <summary>
        /// update restaurant data and sync with server
        /// </summary>
        /// <param name="restaurantInformation"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        internal bool updateRestaurantInformation(RestaurantInformation restaurantInformation, string postData)
        {
            try
            {
                string result = DataSyncBLL.syncServer(GlobalVars.hostUrl + "restaurantcontrol/request/crud/restaurant_update/" + restaurantInformation.Id, "POST", postData);
                
                //update local db
                MySqlRestaurantInformationDAO aRestaurantInformationDao = new MySqlRestaurantInformationDAO();
                return aRestaurantInformationDao.updateRestaurantInformation(restaurantInformation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }            
        }

        public bool UpdateRestaurantLicense(RestaurantSync aRestaurantSync)
       { 
               MySqlRestaurantInformationDAO aRestaurantInformationDao = new MySqlRestaurantInformationDAO();
               return aRestaurantInformationDao.UpdateRestaurantLicense(aRestaurantSync);
       }

        public EmailModule GetEmailModule()
        {
            MysqlEmailModuleDAO mysqlEmailModuleDAO = new MysqlEmailModuleDAO();
            return mysqlEmailModuleDAO.GetEmailModule();
        }
    }
}
