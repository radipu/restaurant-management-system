using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.NewLoginForm;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class Download : Form
    {
        public Download()
        {
            InitializeComponent();
        }

        WebClient webClient;               // Our WebClient that will be doing the downloading for us
        Stopwatch sw = new Stopwatch(); 

        private void btnDownload_Click(object sender, EventArgs e)
        {


            panelMsg.Controls.Remove(labelMsg);
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl gUrl = aGlobalUrlBll.GetUrls();
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

            string resId = restaurantInformation.Id.ToString();
            string urlAddress = gUrl.AcceptUrl + "restaurantcontrol/home/downloadtpos/"+resId;
            progressBar.Visible = true;
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                Uri URL = urlAddress.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("https://" + urlAddress);
                sw.Start();
                try
                {
                    webClient.DownloadFileAsync(URL, @"C:\TomaFoodRestaurant.exe");

                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                  //  MessageBox.Show(ex.Message);
                }
            } 
             
        }

        public void wc_DownloadProgressChanged(Object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;

        }
         
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            
            labelSpeed.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));
            progressBar.Value = e.ProgressPercentage;
            labelPerc.Text = e.ProgressPercentage.ToString() + "% Complete";
            labelDownloaded.Text = string.Format("{0} MB's / {1} MB's",
                (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
                (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();
            if(e.Cancelled == true)
            {
                MessageBox.Show("Download has been canceled.");             
            }
            else
            {


                MessageBox.Show("Download completed.Please restart TPOS again.");
                this.Hide();
                string executableName = Application.ExecutablePath;
                FileInfo executableFileInfo = new FileInfo(executableName);
                string executableDirectoryName = executableFileInfo.DirectoryName;


              


                System.IO.File.Move(@"" + executableDirectoryName + "\\TomaFoodRestaurant.exe", @"" + executableDirectoryName + "\\TomaFoodRestaurant_pre.exe");
                System.IO.File.Move(@"C:\TomaFoodRestaurant.exe", @"" + executableDirectoryName + "\\TomaFoodRestaurant.exe");
                System.IO.File.SetAttributes(@"" + executableDirectoryName + "\\TomaFoodRestaurant_pre.exe", FileAttributes.Normal);

                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                aRestaurantInformationBll.updateSoftVersion();


                Process ProcessObj = new Process();
                ProcessObj.StartInfo.FileName = executableDirectoryName + "\\TomaFoodRestaurant.exe";
                ProcessObj.StartInfo.Arguments = "";
                ProcessObj.Start();


                Process.GetCurrentProcess().Kill();
                string curFile = @"" + executableDirectoryName + "\\TomaFoodRestaurant_pre.exe";
                if (File.Exists(curFile))
                {
                    System.IO.File.Delete(@"" + executableDirectoryName + "\\TomaFoodRestaurant_pre.exe");
                }
            
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        { 

           this.Hide();
           LoginForm frm = new LoginForm();
           frm.Show();
     
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void buttonUpdateOnly_Click(object sender, EventArgs e)
        {
            panelMsg.Controls.Remove(labelMsg);
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            GlobalUrl gUrl = aGlobalUrlBll.GetUrls();
           string resID = "32";
            string urlAddress = gUrl.AcceptUrl + "restaurantcontrol/home/downloadtpos/"+resID;
            progressBar.Visible = true;
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                Uri URL = urlAddress.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ? new Uri(urlAddress) : new Uri("https://" + urlAddress);
                sw.Start();
                try
                {
                    webClient.DownloadFileAsync(URL, "tpos.msi");
                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL(); 
                    aErrorReportBll.SendErrorReport(ex.ToString());
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
