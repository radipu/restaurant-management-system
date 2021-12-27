using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class processBar : Form
    {
        private int type = 0;
        public processBar(string sr, int t)
        {
            InitializeComponent();

            label1.Text = sr;
            type = t;
            timer1.Enabled = true;

        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void processBar_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            bool ult = true;
            DatabaseSyncBLL aDatabaseSyncBll = new DatabaseSyncBLL();
            if (type == 1 && ult)
            {

                ProgressBar progressBar1 = new ProgressBar();
                progressBar1.Maximum = 4000;
                progressBar1.Value = 1000;

                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

                GlobalUrl gUrl = new GlobalUrl();
                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                gUrl = aGlobalUrlBll.GetUrls();
                gUrl.AcceptUrl = Properties.Settings.Default.backend; 

                string restaurantDataUrl = gUrl.AcceptUrl + "restaurantcontrol/home/download_recipes/" +restaurantInformation.Id;
                if (GlobalSetting.DbType == "SQLITE")
                {
                    restaurantDataUrl += "/sqlite/0/0";
                }
                else
                {
                    restaurantDataUrl += "/mysql/0/0";
                }

                string restaurantdata = aDatabaseSyncBll.GetAllRestaurantDataWithPassword(restaurantInformation.Id, restaurantDataUrl);

                if (restaurantdata != "Invaild Global Settings Web Address" || restaurantdata.Contains("Internal Server Error"))
                {



                    string tableSchema = "";
                    // aDatabaseSyncBll.GetSchema();
                    progressBar1.Value += 500;
                    string res = aDatabaseSyncBll.updateTable(restaurantdata, tableSchema);
                    if (restaurantInformation.RestaurantType == "restaurant")
                    {
                        RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
                        List<RestaurantTable> aRestaurantTable = aRestaurantTableBll.GetRestaurantTable();
                        OthersMethod.RefreshAllEmptyTable(aRestaurantTable);
                    }
                    
                    if (res == "Database has been Updated Successfully")
                    {

                        label1.Text = "Successfully updated.";
                        label1.ForeColor = Color.Green;
                        btnClose.Visible = true;

                        AllSettingsForm.Status = "logout";
                        this.Close();

                    }
                }
                else
                {

                    MessageBox.Show("Please try agin after sometime.Server is busy now.");
                    this.Close();
                }

            }
            else if (type == 2 && ult)
            {

                ProgressBar progressBar1 = new ProgressBar();
                progressBar1.Maximum = 4000;
                progressBar1.Value = 1000;

                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

                GlobalUrl gUrl = new GlobalUrl();
                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                gUrl = aGlobalUrlBll.GetUrls();
                gUrl.AcceptUrl = Properties.Settings.Default.backend;

                string restaurantDataUrl = GlobalVars.apiUrl + "postcode/get_postcode_by_area/";


                string restaurantdata = aDatabaseSyncBll.GetAllPostcodes(restaurantDataUrl);

                if (restaurantdata != "Invaild Global Settings Web Address" || restaurantdata.Contains("Internal Server Error"))
                {



                    string tableSchema = "";
                    // aDatabaseSyncBll.GetSchema();
                    progressBar1.Value += 500;
                    string res = "Database has been Updated Successfully";
                    // string res = aDatabaseSyncBll.updateTable(restaurantdata, tableSchema);
                    if (res == "Database has been Updated Successfully")
                    {

                        label1.Text = "Successfully updated.";
                        label1.ForeColor = Color.Green;
                        btnClose.Visible = true;

                        AllSettingsForm.Status = "logout";
                        this.Close();

                    }
                }
                else
                {
                    MessageBox.Show("Please try agin after sometime.Server is busy now.");
                    this.Close();
                }


            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
