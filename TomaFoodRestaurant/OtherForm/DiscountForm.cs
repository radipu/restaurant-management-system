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
    public partial class DiscountForm : Form
    {
        public static OrderDiscount OrderDiscount = new OrderDiscount();

        public DiscountForm()
        {
            InitializeComponent();

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

        private void orderDisocuntButton_Click(object sender, EventArgs e)
        {
            double amount;
            OrderDiscount.Status = "ok";
            String singleLine = fixedDisocuntTextBox.Text.Replace("\r\n", "");
            if (double.TryParse(fixedDisocuntTextBox.Text.Replace("\r\n", ""), out amount) && amount > 0)
            {
                OrderDiscount.DiscountType = "Fixed";
            }
            else if (double.TryParse(persentDiscountTextBox.Text.Replace("\r\n", ""), out amount) && amount > 0)
            {
                OrderDiscount.DiscountType = "Persent";
            }
            else
            {
                OrderDiscount.DiscountType = "Fixed";
                amount = 0;
            }
            OrderDiscount.DiscountArea = "Order";
            OrderDiscount.Amount = amount;
            this.Close();
        }

        private void lineDiscountButton_Click(object sender, EventArgs e)
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
            OrderDiscount.DiscountArea = "Line";
            OrderDiscount.Amount = amount;
            this.Close();
        }



        private void Freebutton_Click(object sen23, EventArgs e)
        {
            Button aButton = sen23 as Button;
            string totalAmount = aButton.Text;

            persentDiscountTextBox.Text = "100";
            fixedDisocuntTextBox.Text = "0";

        }

        private void persentDiscountTextBox_Click(object sender, EventArgs e)
        {
            numberFormUS1.ControlToInputText = persentDiscountTextBox;
        }

        private void fixedDisocuntTextBox_Click(object sender, EventArgs e)
        {
            numberFormUS1.ControlToInputText = fixedDisocuntTextBox;
        }
    }

    public class OrderDiscount
    {
        public string DiscountType { set; get; }
        public double Amount { set; get; }
        public string DiscountArea { set; get; }
        public string Status { set; get; }
    }
}
