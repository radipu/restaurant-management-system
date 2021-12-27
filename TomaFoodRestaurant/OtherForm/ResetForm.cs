using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{

    public partial class ResetForm : Form
    {

        public static string resetStatus = "";
        public ResetForm()
        {
            InitializeComponent();
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            if (paawordTextBox.Text.Trim() == "t-pospass")
            {RestaurantOrderBLL aRestaurantOrderBll=new RestaurantOrderBLL();
                string result= aRestaurantOrderBll.DeleteAllOrder();
                paawordTextBox.Clear();

                if (result == "Yes")
                {
                    MessageBox.Show("Successfully reset all orders.");
                    resetStatus = "reset";
                    this.Close();
                }
                else
                {
                    MessageBox.Show("System is another process.Try again after sometime.");
                }

            }
            else
            {
                MessageBox.Show("Password Incorrect ! Please enter correct password.");
            }
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void numberButton_click(object sen24, EventArgs e)
        {
            Button aButton = sen24 as Button;
            string aButtonPretext = paawordTextBox.Text;
            aButtonPretext += aButton.Text;

            if (aButton.Text == "CLEAR")
            {
                paawordTextBox.Text = "";
            }
            else
            {
                paawordTextBox.Text = aButtonPretext;
            }
        }

    }
}
