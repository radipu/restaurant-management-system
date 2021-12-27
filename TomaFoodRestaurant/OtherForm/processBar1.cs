using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class processBar1 : Form
    {
        public static string syncType = "";
        public processBar1()
        {
            InitializeComponent();
        }


        private void OrderSync(string syncType)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to order syncronize?", "Order Syncronize Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                OrderSyncroniseBLL aOrderSyncroniseBll = new OrderSyncroniseBLL();
                processBar1 processFrm = new processBar1();
                processFrm.Show();
                processFrm.Controls.Clear();
                Label label1 = new Label();
                label1.Font = new System.Drawing.Font("Verdana", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label1.ForeColor = Color.Black;
                label1.Location = new System.Drawing.Point(124, 129);
                label1.Name = "label1";
                label1.Size = new System.Drawing.Size(358, 18);
                label1.TabIndex = 5;
                label1.Text = "Please wait, order syncronize...";
                label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                processFrm.Controls.Add(label1);

                ProgressBar progressBar1 = new ProgressBar();
                progressBar1.Size = new Size(258, 23);
                progressBar1.Location = new Point(124, 100);
                progressBar1.Step = 10;

                processFrm.Controls.Add(progressBar1);
                progressBar1.Visible = true;
                progressBar1.Maximum = 3000;
                progressBar1.Value = 3000;
                label1.Text = "Please wait, order syncronize...";
                string result = "";// aOrderSyncroniseBll.OrderSyncronise(syncType);
                label1.Text = "Please wait, order syncronize...";
                progressBar1.Value = 3000;
                progressBar1.Visible = false;
                if (result == "Operation Successfull!")
                {
                   
                    MessageBox.Show("Successfully synced all order.", "Order Synchronize Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                  
                    MessageBox.Show("System running an another process.Please try again later.", "Order Synchronize Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void allSyncButton_Click(object sender, EventArgs e)
        {
            OrderSync("all");
        }

        private void custyomerSyncButton_Click(object sender, EventArgs e)
        {
            OrderSync("customer");
        }

        private void resetAllButton_Click(object sender, EventArgs e)
        {
            OrderSync("reset");
        }

        private void onlyOrderSyncButton_Click(object sender, EventArgs e)
        {
            OrderSync("order");
        }
    }
}
