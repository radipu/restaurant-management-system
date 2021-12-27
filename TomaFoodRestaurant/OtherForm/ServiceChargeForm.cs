using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class ServiceChargeForm : Form
    {
        public static OrderDiscount OrderDiscount = new OrderDiscount();
        public ServiceChargeForm()
        {
            InitializeComponent();
        }
        private void persentDiscountTextBox_Click(object sender, EventArgs e)
        {
            numberFormUS1.ControlToInputText = persentDiscountTextBox;
        }

        private void fixedDisocuntTextBox_Click(object sender, EventArgs e)
        {
            numberFormUS1.ControlToInputText = fixedDisocuntTextBox;
        }

        private void serviceChargeBtn_Click(object sender, EventArgs e)
        {
            double amount;
            OrderDiscount.Status = "ok";
            if (double.TryParse(fixedDisocuntTextBox.Text.Trim(), out amount) && amount > 0)
            {
                OrderDiscount.DiscountType = "Fixed";
            }
            else if (double.TryParse(persentDiscountTextBox.Text.Trim(), out amount) && amount > 0)
            {
                OrderDiscount.DiscountType = "Persent";
            }
            else
            {
                OrderDiscount.DiscountType = "Fixed";
                amount = 0;
            }
            OrderDiscount.DiscountArea = "service_charge";
            OrderDiscount.Amount = amount;
            this.Close();


        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            OrderDiscount.Status = "cancel";
            this.Close();
        }

        private void Persentbutton_Click(object sen23, EventArgs e)
        {
            Button aButton = sen23 as Button;
            string totalAmount = aButton.Text;
            string[] amounts = totalAmount.Split('%');
            persentDiscountTextBox.Text = amounts[0];
            fixedDisocuntTextBox.Text = "0";

        }
        private void Fixedbutton_Click(object sen24, EventArgs e)
        {
            Button aButton = sen24 as Button;
            string totalAmount = aButton.Text;
            string[] amounts = totalAmount.Split('£');
            fixedDisocuntTextBox.Text = amounts[1];
            persentDiscountTextBox.Text = "0";
        }
    }
}
