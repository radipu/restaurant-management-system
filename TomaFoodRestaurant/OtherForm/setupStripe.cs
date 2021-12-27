using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class setupStripe : Form
    {
        public setupStripe()
        {
            InitializeComponent();
        }

        private void logainButton_Click(object sender, EventArgs e)
        {
            if (paawordTextBox.Text.Trim() == "t-pospass")
            {
                reportGroupBox.Enabled = true;

                paawordTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please try again.");
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.StripeAPI = apiTextbox.Text;
            Properties.Settings.Default.PublishAPI = publishtextBox.Text;
            Properties.Settings.Default.accountId = accIdTextBox.Text;
            Properties.Settings.Default.applicationFees = Convert.ToDouble(appFeeTextBox.Text);
            Properties.Settings.Default.feeType = feeTypeComboBox.Text;
            Properties.Settings.Default.stripeEnable = checkBoxStripe.Checked;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void setupStripe_Load(object sender, EventArgs e)
        {
           apiTextbox.Text=Properties.Settings.Default.StripeAPI ;
           publishtextBox.Text=Properties.Settings.Default.PublishAPI ;

            accIdTextBox.Text = Properties.Settings.Default.accountId;
            appFeeTextBox.Text = Properties.Settings.Default.applicationFees.ToString();
            feeTypeComboBox.Text = Properties.Settings.Default.feeType;
            checkBoxStripe.Checked = Properties.Settings.Default.stripeEnable;


        }
    }
}
