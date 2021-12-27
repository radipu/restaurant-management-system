using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.NewLoginForm;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class CheckSoftwareActivation : Form
    {
        private string message = "";
        public System.Timers.Timer statusUpdateTimer;
        public CheckSoftwareActivation()
        {
            InitializeComponent();
            this.AcceptButton = activeNowButton;
         
            string COMPUTERNAME = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
            pcNameTextBox.Text = COMPUTERNAME;
            versionComboBox.SelectedIndex = 0;
            if (GlobalSetting.DbType != "SQLITE")
            {
                versionComboBox.SelectedIndex = 1;
            }
           

        }
        public static string resturentId { get; set; } 
        private void activeNowButton_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                SoftwareActivationBLL aSoftwareActivationBll=new SoftwareActivationBLL();
                string restaurantId = restaurantIdTextBox.Text.Trim();
                string licenseCode = licenseCodeTextBox.Text.Trim();
                try
                {
                    DBsetup setup = new DBsetup(this);
                    setup.ShowDialog();
                    
                    activeNowButton.Visible = false;
                    statusLabel.Visible = true;

                    Properties.Settings.Default.deviceType = versionComboBox.Text.Trim();                    
                    message = "Connecting to the server.......";
                    statusLabel.Text = message;                    
                    string COMPUTERNAME = pcNameTextBox.Text;
                    HardwareInforamtion aHardwareInforamtion = aSoftwareActivationBll.GetHardwareInforamtion();
                    string key = "ID#" + aHardwareInforamtion.ProcessorSerial.Trim() + "_HDD#" + aHardwareInforamtion.HardDriveSerialNo.Trim() + "_NAME#" + COMPUTERNAME.Trim();
                    key = Regex.Replace(key, "[^0-9a-zA-Z#_-]+", "");
                    string postData = "hardware_info=" + key + "&restaurant_id=" + restaurantId + "&license_code=" + licenseCode;
                    GlobalUrl gUrl = new GlobalUrl();
                    GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                    DatabaseSyncBLL aDatabaseSyncBll=new DatabaseSyncBLL();
                    gUrl = aGlobalUrlBll.GetUrls();
                    gUrl.AcceptUrl = GlobalVars.hostUrl;
                     
                    string url = gUrl.AcceptUrl + "restaurantcontrol/request/check_activation";
                    restaurantId = restaurantIdTextBox.Text.Trim();
                    
                    //Console.WriteLine(statusLabel.Text);
                    string result = aSoftwareActivationBll.ToCheckSoftwareActivation(key, postData, url);
                    message = "Checking license code.........";
                    statusLabel.Text = message;
                    if (result == "OK")
                    {
                        message = "Authorizing.........";
                        statusLabel.Text = message;
                        Console.WriteLine(statusLabel.Text);

                        // parameter 0 used for no need username and password to post data

                        string restaurantDataUrl = gUrl.AcceptUrl + "restaurantcontrol/home/download_database/" + restaurantId + "/sqlite/nocustomer/0/0";
                        if (GlobalSetting.DbType == "MYSQL")
                        {
                           restaurantDataUrl  = gUrl.AcceptUrl + "restaurantcontrol/home/download_database/" + restaurantId + "/mysql/nocustomer/0/0";
                        }
                        string restaurantdata = aDatabaseSyncBll.GetAllRestaurantDataWithPassword(int.Parse(restaurantId), restaurantDataUrl);
                    
                        message = "Downloding data.........";
                        statusLabel.Text = message;
                        Console.WriteLine(statusLabel.Text);
                        string tableSchema = aDatabaseSyncBll.GetSchema();
                        message = "Creating local data .........";
                        statusLabel.Text = message;
                        DatabaseProcessbar processbar = new DatabaseProcessbar(tableSchema, restaurantdata,this);
                        //string res = aDatabaseSyncBll.DeleteAllTable(restaurantdata, tableSchema);
                       
                        processbar.TopLevel = false;
                        loadingPannel.Controls.Add(processbar);
                        processbar.Dock = DockStyle.Fill;
                        processbar.Show();
                        
                        //if (processbar.status == "Database has been Updated Successfully")
                        //{
                        //    //statusUpdateTimer.Enabled = true;//    //Console.WriteLine(statusLabel.Text);
                          
                        //  Application.Restart();
                        //}
                        //else
                        //{
                        //    statusLabel.Text = processbar.status;
                        //}
                    }
                    else
                    {
                        activeNowButton.Visible = true;statusLabel.Text = "Please contact to software vendor.";
                        //MessageBox.Show(url+"\n\n"+result, "Software Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                    activeNowButton.Visible = true;
                    MessageBox.Show(ex.ToString(), "Software Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            else {
                activeNowButton.Visible = true;
                MessageBox.Show("Please put Restaurant Id & License Code", "Wrong Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }


     

        private bool Valid()
        {
            if (restaurantIdTextBox.Text.Trim().Length > 0) return true;
            if (licenseCodeTextBox.Text.Trim().Length > 0) return true;
            if (pcNameTextBox.Text.Trim().Length > 0) return true;

            return false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CheckSoftwareActivation_Load(object sender, EventArgs e)
        {
            statusUpdateTimer = new System.Timers.Timer(1);
            statusUpdateTimer.Elapsed += statusUpdateTimer_Tick;
            statusUpdateTimer.Enabled = false;
           
        }
        private void statusUpdateTimer_Tick(object sender, EventArgs e)
        {
            statusUpdateTimer.Enabled = false;
            LoadCustomerDataIntoDatabase();

                //if (statusLabel.InvokeRequired)
                //{
     
                //    statusLabel.Invoke(new MethodInvoker(LoadCustomerDataIntoDatabase));
                //}
                //else
                //{

                  
                //}
        }

        private void LoadCustomerDataIntoDatabase()
        {
            try
            {
                GlobalSetting.IsCustomerAdd = true;
               
                RestaurantInformation information=new RestaurantInformationBLL().GetRestaurantInformation();
                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                GlobalUrl gUrl = aGlobalUrlBll.GetUrls();
                DatabaseSyncBLL aDatabaseSyncBll = new DatabaseSyncBLL();
                string restaurantDataUrl = gUrl.AcceptUrl + "restaurantcontrol/home/download_customer/" + information.Id + "/mysql/0/0";
                if (GlobalSetting.DbType=="SQLITE")
                {
                    restaurantDataUrl = gUrl.AcceptUrl + "restaurantcontrol/home/download_customer/" + information.Id + "/sqlite/0/0";
                }
                string restaurantdata = aDatabaseSyncBll.GetAllRestaurantDataWithPassword(int.Parse(information.Id.ToString()), restaurantDataUrl);
                string result = aDatabaseSyncBll.AddCustomerDataIntoDatabase(restaurantdata);
                GlobalSetting.IsCustomerAdd = false;
            }
            catch (Exception ex){
                MessageBox.Show(ex.GetBaseException().ToString());

            }
     
        }

        private void versionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
 
    }
}
