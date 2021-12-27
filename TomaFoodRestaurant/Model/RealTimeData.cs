﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
namespace TomaFoodRestaurant.Model
{
    class RealTimeData
    {
        private static RestaurantInformation aRestaurantInformation;
        public MainFormView ResMainFormView;
        public mainForm SinglePage;

        public static List<string> Queue = new List<string>(5);


        public async Task OpenSSEStream(string url, MainFormView ResponsivePage, mainForm SinglePage)
        {
            this.ResMainFormView = ResponsivePage;
            this.SinglePage = SinglePage;
            if (aRestaurantInformation == null)
            {
                var RestaurantInformationBll = new RestaurantInformationBLL().GetRestaurantInformation();
                aRestaurantInformation = RestaurantInformationBll;

            }
            if (OthersMethod.CheckForInternetConnection())
            {
                var request = WebRequest.Create(new Uri(url));
                request.Timeout = 15000000;
                ((HttpWebRequest)request).KeepAlive = false;
                ((HttpWebRequest)request).ProtocolVersion = HttpVersion.Version11;
                ((HttpWebRequest)request).AllowReadStreamBuffering = false;
                try
                {
                    var response = request.GetResponse();
                    var stream = response.GetResponseStream();
                    ReadStreamForever(stream);
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Execption on Open Stream " + " :" + ex.Message + " " + DateTime.Now.TimeOfDay);
                    //ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    //aErrorReportBll.SendErrorReport(ex.GetBaseException().ToString());
                    ReRunServer();
                    System.Threading.Thread.Sleep(1000000);

                }

            }


            //return stream;
        }
        public async void ReRunServer()
        {

            try
            {
                await
                       Task.Run(() => OpenSSEStream("https://" + aRestaurantInformation.Website + "/restaurants/check_online_order/" + aRestaurantInformation.Id, this.ResMainFormView, this.SinglePage));


            }
            catch (Exception exception)
            {

                Console.WriteLine("Execption on Re-run server " + " :" + exception.Message + " " + DateTime.Now.TimeOfDay);

                //ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                //aErrorReportBll.SendErrorReport(exception.ToString());

            }

        }
        public void ReadStreamForever(Stream stream)
        {
            var encoder = new UTF8Encoding();
            var buffer = new byte[2048];
            while (true)
            {
                //TODO: Better evented handling of the response stream

                if (stream.CanRead)
                {
                    int len = stream.Read(buffer, 0, 2048);
                    if (len > 0)
                    {
                        var text = encoder.GetString(buffer, 0, len);
                        //  File.AppendAllText("Config/log.txt", "SSE Log : " + text.ToString() + "\n\n");
                        Console.WriteLine(text + " " + DateTime.Now.TimeOfDay);
                        Push(text);
                    }
                    else
                    {
                        stream.Close();

                        break;
                    }
                } System.Threading.Thread.Sleep(500);
            }

            System.Threading.Thread.Sleep(500);
            try
            {

                Console.WriteLine("Reconnect to server" + " " + DateTime.Now.TimeOfDay);
                var response = OpenSSEStream("https://" + aRestaurantInformation.Website + "/restaurants/check_online_order/" + aRestaurantInformation.Id, this.ResMainFormView, this.SinglePage);

            }
            catch (Exception ex)
            {
                Console.WriteLine("On Reconnect :" + ex.Message + " " + DateTime.Now.TimeOfDay);
            }

        }

        public void Push(string text)
        {
            // Queue.Clear();

            if (String.IsNullOrWhiteSpace(text))
            {
                return;
            }
            var lines = text.Trim().Split('\n');
            Queue.AddRange(lines);

            if (text.Contains("data:"))
            {

                ProcessLines();
            }
        }

        public void ProcessLines()
        {
            try
            {
                var lines = Queue;
                int index = 0;
                int lastEventIdx = -1;

                for (int i = 0; i < lines.Count; i++)
                {
                    string line = lines[i];

                    if (String.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    line = line.Trim();



                    if (line.StartsWith("data:"))
                    {
                        string order = line.Replace("data:", String.Empty);

                        Console.WriteLine("Order count : " + order + " " + DateTime.Now.TimeOfDay);
                        if (order != "" && Convert.ToInt32(order) > 0)
                        {
                            //GetOnlineOrder();
                            new OnlineOrder().GetOnlineOrder();
                            //if (ResMainFormView != null)
                            //{
                            //    ResMainFormView.GetOnlineOrder();
                            //}
                            //else if (SinglePage != null)
                            //{
                            //    SinglePage.GetOnlineOrder();
                            //}
                        }

                        index++;
                        lastEventIdx = i;
                    }


                }
                //trim previously processed events
                if (lastEventIdx >= 0)
                {
                    lines.RemoveRange(0, lastEventIdx);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Get Process Line :" + ex.Message + " " + DateTime.Now.TimeOfDay);



            }


        }

        /*
            Optionally ignore certificate errors
        */
        public bool AcceptALlCertifications(object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate cert,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors errors)
        {
            return true;
        }

    }
}
