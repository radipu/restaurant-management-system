using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.BLL
{
   public class ErrorReportBLL
    {
       System.Timers.Timer reportSync;
       GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
      
       public ErrorReportBLL()
      {

         if (OthersMethod.CheckForInternetConnection()){
              //used for error report sync
             reportSync = new System.Timers.Timer(100);
             reportSync.Elapsed += reportSync_Tick;
             reportSync.Enabled = false;
         }

      }
      private void reportSync_Tick(object sender, EventArgs e)
      {

              reportSync.Stop();
              if (OthersMethod.CheckForInternetConnection())
              {
                  if (!string.IsNullOrEmpty(GlobalSetting.ReportMessage))
                  {
                        RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                        RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                        GlobalUrl urls = aGlobalUrlBll.GetUrls();
                      string url = urls.AcceptUrl + "restaurantcontrol/request/crud/send_errorlog/" + restaurantInformation.Id;
                     // MessageBox.Show(GlobalSetting.ReportMessage);
                      string error = Environment.NewLine + "****************************" + Environment.NewLine +DateTime.Now+" : " + GlobalSetting.ReportMessage;
                      File.WriteAllText("Config/errorLog.txt", error += error);
                      string data = "message=" + GlobalSetting.ReportMessage;

                      if (GlobalSetting.ReportMessage.Length > 20)
                         // AddLocalReservationToServer(data, url);
                          GlobalSetting.ReportMessage = "";
                      }
                  }
                  reportSync.Enabled = false;
              } 
       public void SendErrorReport(string reportMessage)
       {
           try
           {
               reportSync.Enabled = true;
               GlobalSetting.ReportMessage = reportMessage;
               string error = Environment.NewLine + "****************************" + Environment.NewLine + DateTime.Now + ": "  + GlobalSetting.ReportMessage;
               File.AppendAllText("Config/errorLog.txt", error += reportMessage);
           }
           catch (Exception)
           {
               File.AppendAllText("Config/errorLog.txt", GlobalSetting.ReportMessage += DateTime.Now + ": "+reportMessage);
           }
         //  MessageBox.Show(reportMessage);
        
       }

       public string AddLocalReservationToServer(string data, string url)
       {
           string result = "";
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
           }
           catch (Exception exception)
           {
             
               ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
               aErrorReportBll.SendErrorReport(exception.ToString());
           }

           return result;


       }
    }
}
