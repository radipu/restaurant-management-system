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
    public partial class SoftActiveMsg : Form
    {
        public SoftActiveMsg()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {

         
            try
            {
                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                RestaurantSync aRestaurantSync = aRestaurantInformationBll.GetRestaurantSyncInformation();
                if (aRestaurantSync != null && aRestaurantSync.id > 0)
                {
                    aRestaurantInformationBll.UpdateRestaurantLicense(aRestaurantSync);
                }
                this.Close();
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
            Application.Exit();
           
        }
    }
}
