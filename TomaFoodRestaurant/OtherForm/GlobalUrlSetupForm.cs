using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class GlobalUrlSetupForm : UserControl
    {
        GlobalUrl url = new GlobalUrl();
        public GlobalUrlSetupForm()
        {
            InitializeComponent();
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            url.AcceptUrl = onlineOrderAcceptTextBox.Text;
            url.RejectUrl = onlineOrderRejectTextBox.Text;
            url.OrderSyn = orderSyncronizeTextBox.Text;
            url.fontSize = fontSizetexbox.Text;
            url.fontStyle = fontStyleComboBox.Text;
            url.fontFamily = fontFamilyComboBox.Text;
            url.PrinterName = printerNamecomboBox.Text;
            url.Cursur = cursurEnableCheckBox.Checked ? 1 : 0;
            url.Keyboard = keyboardCheckBox.Checked ? 1 : 0;

            if (url.Id <= 0)
            {
                int result = aGlobalUrlBll.InsertUrls(url);
                if (result > 0)
                {
                    url.Id=result;
                    MessageBox.Show("All required url has been save succefully","Insertion Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    
                    AllSettingsForm.Status = "reload";
                    var parentForm = this.ParentForm;
                    if (parentForm != null) parentForm.Close();
                }
                else MessageBox.Show("Something wrong! Please try again  ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                string result = aGlobalUrlBll.UpdateUrls(url);
                if (result == "Yes")
                {
                    MessageBox.Show("All required url has been updated succefully", "Update Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AllSettingsForm.Status = "reload";
                    var parentForm = this.ParentForm;
                    if (parentForm != null) parentForm.Close();
                }
                else 
                { 
                   MessageBox.Show("Something wrong! Please try again  ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GlobalUrlSetupForm_Load(object sender, EventArgs e)
        {

            List<string> printerlist = new List<string>();
            foreach (String printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                printerlist.Add(printer);
            }
            printerNamecomboBox.DataSource = printerlist;
            LoadAllUrl();
        }

        private void LoadAllUrl()
        {
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            url = aGlobalUrlBll.GetUrls();
            if (url != null && url.Id > 0) 
            {
                onlineOrderAcceptTextBox.Text=url.AcceptUrl;
                onlineOrderRejectTextBox.Text=url.RejectUrl;
                orderSyncronizeTextBox.Text = url.OrderSyn;
                fontSizetexbox.Text = url.fontSize;
                fontStyleComboBox.Text = url.fontStyle;
                fontFamilyComboBox.Text = url.fontFamily;
                printerNamecomboBox.Text = url.PrinterName;
                cursurEnableCheckBox.Checked = url.Cursur > 0;
                keyboardCheckBox.Checked = url.Keyboard > 0;
            }
        }

        private void updateDatabasetoolStripButton_Click(object sender, EventArgs e)
        {
           // AllSettingsForm all_sett = new AllSettingsForm();
            DialogResult dialogResult = MessageBox.Show("Do you want to update database", "Update Database Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                groupBox1.Visible = false;
                 
                Label label1 = new Label();
                label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label1.Location = new System.Drawing.Point(227, 98);
                label1.Name = "label1";
                label1.Size = new System.Drawing.Size(562, 23);
                label1.TabIndex = 1;
                label1.Text = "Please wait, database updating...";
                label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                label1.Visible = true;
                panel1.Controls.Add(label1);

                ProgressBar progressBar1 = new ProgressBar();
                progressBar1.Size = new Size(562, 23);
                progressBar1.Location = new Point(227, 59);
                panel1.Controls.Add(progressBar1);
                progressBar1.Visible = true;
                progressBar1.Maximum = 3000;
                progressBar1.Value = 1000;


              
                DatabaseSyncBLL aDatabaseSyncBll = new DatabaseSyncBLL();
                string restaurantdata = aDatabaseSyncBll.GetAllRestaurantData();
                progressBar1.Value += 1000;
               if (restaurantdata != "Invaild Global Settings Web Address")
                {
                    string tableSchema = aDatabaseSyncBll.GetSchema();
                    progressBar1.Value += 500;
                    string res = aDatabaseSyncBll.DeleteAllTable(restaurantdata, tableSchema);
                   progressBar1.Value += 500;
                   label1.Text = "Database updating complete";
                   progressBar1.Visible = false;
                     MessageBox.Show(res);
                     AllSettingsForm.Status = "logout";
                    var parentForm = this.ParentForm;
                    if (parentForm != null) parentForm.Close();
                }
                else
                {
                    MessageBox.Show(restaurantdata);
                }

            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void orderSystoolStripButton_Click(object sender, EventArgs e)
        {

            return;

            DialogResult dialogResult = MessageBox.Show("Do you want to order syncronize?", "Order Syncronize Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
               
                groupBox1.Visible = false;
                 OrderSyncroniseBLL aOrderSyncroniseBll=new OrderSyncroniseBLL();
                Label label1 = new Label();
                label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label1.Location = new System.Drawing.Point(227, 98);
                label1.Name = "label1";
                label1.Size = new System.Drawing.Size(562, 23);
                label1.TabIndex = 1;
                label1.Text = "Please wait, order syncronize...";
                label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                panel1.Controls.Add(label1);


                ProgressBar progressBar1 = new ProgressBar();
                progressBar1.Size = new Size(562, 23);
                progressBar1.Location = new Point(227, 59);
                panel1.Controls.Add(progressBar1);
                progressBar1.Visible = true;
                progressBar1.Maximum = 3000;
                progressBar1.Value = 1000;

                panel1.Controls.Add(progressBar1);
                progressBar1.Visible = true; 

                // progressBar1.

                string result = "";// aOrderSyncroniseBll.OrderSyncronise("all");
                progressBar1.Value = 3000;
                progressBar1.Visible = false;
                label1.Text = "Order syncronize complete";

              MessageBox.Show(result, "Order Synchronize Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (dialogResult == DialogResult.No)
            {

            }

        }

       
        private void buttonChangePrice_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start(Properties.Settings.Default.backend);
        }

        private void printerNamecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
