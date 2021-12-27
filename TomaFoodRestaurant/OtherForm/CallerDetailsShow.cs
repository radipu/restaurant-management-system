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
    public partial class CallerDetailsShow : Form
    {
        public static string OrderType = "";
        RestaurantUsers aRestaurantUsers=new RestaurantUsers();
        public CallerDetailsShow(RestaurantUsers aRestaurantUser)
        {
            InitializeComponent();

            try
            {
                aRestaurantUsers = aRestaurantUser;
                string cell = aRestaurantUser.Mobilephone != "" ? aRestaurantUser.Mobilephone : aRestaurantUser.Homephone;
                string address = aRestaurantUser.Firstname;
                address += "," + cell;
                if (!string.IsNullOrEmpty(aRestaurantUser.FullAddress))
                {

                    address += "\n" + aRestaurantUser.House + "," + aRestaurantUser.FullAddress;

                }
                else
                {

                    address += " ," + aRestaurantUser.House + "," + aRestaurantUser.Address;
                    address += "\n" + aRestaurantUser.City + "," + aRestaurantUser.Postcode;
                }

                fullAddressTextBox.Text = address;
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

        }

        private void deliveryButton_Click(object sender, EventArgs e)
        {
            OrderType = "del";
            this.Close();
        }

        private void collectionButton_Click(object sender, EventArgs e)
        {
            OrderType = "clt";
            this.Close();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            OrderType = "edit";
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderType = "close";
            this.Close();
        }
    }
}
