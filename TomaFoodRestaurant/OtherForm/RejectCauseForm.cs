using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class RejectCauseForm : Form
    {
        public static string message;
        public static string status;
        public static bool refund;
        public RejectCauseForm()
        {
            InitializeComponent();
            labelfooter.Text = "Sorry for any inconvenience caused.\nFor more information please call us "+GlobalSetting.RestaurantInformation.Phone+"\nKind Regards\n"+GlobalSetting.RestaurantInformation.RestaurantName;
            refund = false;
        }


        private void acceptButton_Click(object sender, EventArgs e)
        {
            message = causesTextBox.Text;
            status = "ok";
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {

            status = "cancel";
            this.Close();
        }

        private void causesTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            numberPadUs1.ControlToInputText = causesTextBox;
        }

        private void RejectCauseForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonRefund_Click(object sender, EventArgs e)
        {
            message = causesTextBox.Text;
            status = "ok";
            refund = true;
            this.Close();
        }
    }
}
