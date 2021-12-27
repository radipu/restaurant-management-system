using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.BLL
{

    public class OthersMethod
    {

        GlobalUrl urls = new GlobalUrl();
        GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
        public static bool DatabaseConnected = true;

      
        public static bool CheckForInternetConnection()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "tomafood.net";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }


            //try
            //{
            //    Uri uri = new Uri("https://tomafood.net");

            //    Ping ping = new Ping();
            //    PingReply pingReply = ping.Send(uri.Host);
            //    if (pingReply.Status != IPStatus.Success)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}
            //catch
            //{
            //    return false;
            //}


            //return CheckNet();
            //return connectionState();
            //  return true;
            //try
            //{
            //    using (var client = new WebClient())
            //    {
            //        using (var stream = client.OpenRead("https://tomafood.net"))
            //        {
            //            return true;
            //        }
            //    }
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public string IsTimeFormatValid(string time)
        {
            try
            {
                DateTime date = Convert.ToDateTime(time);
                return date.ToString("HH:mm");

            }
            catch (Exception ex)
            {
                return "00:00";
            }

        }

        public void NumberPadClose()
        {
            urls = aGlobalUrlBll.GetUrls();
            try
            {
                if (Application.OpenForms.OfType<NumberForm>().Count() == 1)
                    Application.OpenForms.OfType<NumberForm>().First().Close();
            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }
        }

        public static bool CheckServerConneciton(bool showMessage = true)
        {
            var networkConnected = true;

            if (Properties.Settings.Default.deviceType == "CLIENT")
            {
                
                string MainConnectionString = "server=" + Properties.Settings.Default.serverIp + ";user id=root; password=" + Properties.Settings.Default.password + ";database=" + Properties.Settings.Default.database + "; " + "Pooling=false; Max Pool Size = 50000; Min Pool Size = 5";
                networkConnected = false;
                MySqlConnection conn = new MySqlConnection(MainConnectionString);
                try
                { 
                    conn.Open();
                    networkConnected = true;
                }
                catch (MySqlException SQLex)
                {
                    networkConnected = false;
                }

            }
            if (showMessage && !networkConnected)
            {
                MessageBox.Show("Out of network coverage ! Please check the network connection.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            return networkConnected;

        }
        public void KeyBoardClose()
        {
            urls = aGlobalUrlBll.GetUrls();
            try
            {

                if (Application.OpenForms.OfType<NumberPad>().Count() == 1)
                    Application.OpenForms.OfType<NumberPad>().First().Close();

            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }
        }
        public void CallerIDClose()
        {
            urls = aGlobalUrlBll.GetUrls();
            try
            {

                if (Application.OpenForms.OfType<TomaFoodRestaurant.OtherForm.Form1>().Count() > 0)
                    Application.OpenForms.OfType<TomaFoodRestaurant.OtherForm.Form1>().First().Close();




            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }
        }

        private static bool CheckEmptyTable(RestaurantTable table)
        {
            try
            {
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                RestaurantOrder aRestaurantOrder = new RestaurantOrder();
                aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrder(table.Id, "pending");

                if (aRestaurantOrder != null && aRestaurantOrder.Id > 0)
                {
                    return false;
                }
                else return true;
            }
            catch (Exception)
            {
                return false;

            }

        }


        public static void RefreshAllEmptyTable(List<RestaurantTable> aRestaurantTable)
        {
            try
            {
                bool status = false;

                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
                foreach (RestaurantTable table in aRestaurantTable)
                {
                    bool emptyTable = CheckEmptyTable(table);
                    if (emptyTable)
                    {
                        table.CurrentStatus = "available";
                        aRestaurantTableBll.UpdateRestaurantTable(table);
                        status = true;
                    }
                    else if(!table.IsBill)                    
                    {
                        table.CurrentStatus = "busy";
                        aRestaurantTableBll.UpdateRestaurantTable(table);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //this.Activate();
                //throw;
            }

            //if (status)
            //{
            //    LoadAllTable();
            //}
        }




    }
}
