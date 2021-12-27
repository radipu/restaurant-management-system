using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class DeliveryChargeForm : Form
    {

        public static double  DeliveryChargeAmount {set;get;}
        public DeliveryChargeForm()
        {
            InitializeComponent();
        }

    

        private void fixedDisocuntTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && deliveryChargeTextBox.Text.Contains("."))
            {
                e.Handled = true;
            }
        }



        private void deliveryChargeButton_click(object sen24, EventArgs e)
        {
            Button aButton = sen24 as Button;
            string totalAmount = aButton.Text;
            string[] amounts = totalAmount.Split('£');
            string amount = amounts[1];
            DeliveryChargeAmount = Convert.ToDouble(amount); ;
            this.Close();
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            double amount;
            if (double.TryParse(deliveryChargeTextBox.Text.Trim(),out amount)) {
                DeliveryChargeAmount=amount;
                this.Close();
                
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DeliveryChargeAmount = -1;
            this.Close();
        }

        private void deliveryChargeTextBox_Click(object sender, EventArgs e)
        {
            numberFormUS1.ControlToInputText = deliveryChargeTextBox;
        }

        private void FreeDelivery_Click(object sen23, EventArgs e)
        {
            Button aButton = sen23 as Button;
            string totalAmount = aButton.Text;
            deliveryChargeTextBox.Text = "0";
            this.Close();
        }
    }
}
