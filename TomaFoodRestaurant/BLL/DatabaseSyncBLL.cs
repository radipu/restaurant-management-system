using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class DatabaseSyncBLL
    {
      
       internal string DeleteAllTable(string restaurantdata, string tableSchema)
       {



           //DatabaseSyncDAO aDatabaseSyncDao = new DatabaseSyncDAO();
           //return aDatabaseSyncDao.DeleteAllTable(restaurantdata, tableSchema);


           if (GlobalSetting.DbType == "SQLITE")
           {
               DatabaseSyncDAO aDatabaseSyncDao = new DatabaseSyncDAO();
               return aDatabaseSyncDao.DeleteAllTable(restaurantdata, tableSchema);
           }
           else
           {
               MySqlDatabaseSyncDAO aDatabaseSyncDao = new MySqlDatabaseSyncDAO();
               return aDatabaseSyncDao.DeleteAllTable(restaurantdata, tableSchema);
           }
          
       }

       internal string AddCustomerDataIntoDatabase(string customerData)
       {
           if (GlobalSetting.DbType == "SQLITE")
           {
               DatabaseSyncDAO aDatabaseSyncDao = new DatabaseSyncDAO();
               return aDatabaseSyncDao.AddCustomerDataIntoDatabase(customerData);
           }
           else
           {
               MySqlDatabaseSyncDAO aDatabaseSyncDao = new MySqlDatabaseSyncDAO();
               return aDatabaseSyncDao.AddCustomerDataIntoDatabase(customerData);
           }
          
       }

       public string GetSchema()
        {
            string text = "";
            try
            {
                GlobalUrl gUrl = new GlobalUrl();
                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                gUrl = aGlobalUrlBll.GetUrls();
                var request = (HttpWebRequest)WebRequest.Create(gUrl.AcceptUrl + "scripts/schema/tomafood_db_schema.sql");
                if (GlobalSetting.DbType == "MYSQL")
                {
                    request = (HttpWebRequest)WebRequest.Create(gUrl.AcceptUrl + "scripts/schema/tomafood_mysql_schema.sql");
                }
               
              
                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        text = reader.ReadToEnd();
                    }
                }

            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

            return text;
        }

        public string GetAllRestaurantData(int restaurantId = 0)
        {
            string text = "";
            try
            {
                GlobalUrl gUrl = new GlobalUrl();
                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                gUrl = aGlobalUrlBll.GetUrls();
                if (restaurantId <= 0)
                {
                    RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                    RestaurantInformation restaurantInfo = aRestaurantInformationBll.GetRestaurantInformation();
                    restaurantId = restaurantInfo.Id;
                }
                string version = "mysql";
                if (GlobalSetting.DbType=="SQLITE")
                {
                    version = "sqlite";
                }

                Console.WriteLine(gUrl.AcceptUrl + "restaurantcontrol/home/backup_tables/" + restaurantId +"/"+version+"/data/0");

                string link = gUrl.AcceptUrl + "restaurantcontrol/home/backup_tables/" + restaurantId + "/"+version+"/data/0";

                //  WebRequest.DefaultWebProxy = null;

                var request =
                    (HttpWebRequest)
                        WebRequest.Create(gUrl.AcceptUrl + "restaurantcontrol/home/backup_tables/" + restaurantId +
                                          "/"+version+"/data/0");

                request.Proxy = null;
                using (var response = request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {

                        using (var reader = new StreamReader(responseStream))
                        {
                            text = reader.ReadToEnd();

                        }
                        responseStream.Flush();
                        responseStream.Close();

                    }
                }

            }catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
                return "Invaild Global Settings Web Address";
            }


            return text;
        }



        public string GetAllRestaurantDataWithPassword(int restaurantId, string url)
        {
          



            string result = string.Empty;
            try
            {
                GlobalUrl gUrl = new GlobalUrl();
                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                gUrl = aGlobalUrlBll.GetUrls();


                string postData = "";
                if (GlobalSetting.RestaurantUsers != null && GlobalSetting.RestaurantUsers.Id > 0)
                {
                    postData = "username=" + GlobalSetting.RestaurantUsers.Username + "&password=" + GlobalSetting.RestaurantUsers.Password;
                }
               
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
                //result = "OK";
            }
            catch (Exception exception)
            {
                result = exception.ToString();
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            return result;
        }


        public string GetAllPostcodes(string urlL)
        {
            // Post the data to the right place.
            string url = GlobalVars.apiUrl + "postcode/get_postcode_by_area?area=['SR2']";
            Uri target = new Uri(url);
            WebRequest request = WebRequest.Create(target);

            request.Method = "GET";
            //  request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("x-api-key", "muzahid");
            request.ContentType = "application/json";

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

            var jo = JObject.Parse(result);
            var FULL_ADDRESS = jo["SQL_QUERY"].ToString();
        

            return FULL_ADDRESS;

        }
  
        public string GetAllPostcodes_(string url)
        { 
            var result = string.Empty;
             
                GlobalUrl gUrl = new GlobalUrl();
                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                gUrl = aGlobalUrlBll.GetUrls();


              //  string postData = @"{""object"":{""area"":""sr2""}}";// "area=['sr2']";
                
               // byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                // Post the data to the right place.
                Uri target = new Uri(url + "?area=[sr2]");
             HttpWebRequest request = (HttpWebRequest)WebRequest.Create(target);
            request.Method = "GET";
            request.ContentType = "application/json";
        //    request.ContentLength = postData.Length;
            //using (Stream webStream = request.GetRequestStream())
            //using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            //{
            //    requestWriter.Write(postData);
            //}

            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                    Console.Out.WriteLine(response);
                }
            } 
            catch (System.Net.WebException exception)
                {
                    result = exception.ToString();
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());

                }

 
            return result;
        }


        internal string updateTable(string restaurantdata, string tableSchema)
        {


            if (GlobalSetting.DbType == "SQLITE")
            {
                DatabaseSyncDAO aDatabaseSyncDao = new DatabaseSyncDAO();
                return aDatabaseSyncDao.updateTable(restaurantdata, tableSchema);
            }
            else
            {
                MySqlDatabaseSyncDAO aDatabaseSyncDao = new MySqlDatabaseSyncDAO();
                return aDatabaseSyncDao.updateTable(restaurantdata, tableSchema);
            }
            
        }
    }
}
