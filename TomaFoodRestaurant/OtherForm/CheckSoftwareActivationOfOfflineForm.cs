using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class CheckSoftwareActivationOfOfflineForm : Form
    {
        public CheckSoftwareActivationOfOfflineForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void activeNowButton_Click(object sender, EventArgs e)
        {
              if (ValidForm())
                {
                    try
                    {
                        
                        DBsetup setup = new DBsetup(this);
                        setup.ShowDialog();
                        RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                        RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                        LicenceKeyBLL aLicenceKeyBll = new LicenceKeyBLL();
                        SoftwareActivationBLL aSoftwareActivationBll = new SoftwareActivationBLL();
                        
                        string COMPUTERNAME = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
                        HardwareInforamtion aHardwareInforamtion = aSoftwareActivationBll.GetHardwareInforamtion();
                        string key = "ID#" + aHardwareInforamtion.ProcessorSerial.Trim() + "_HDD#" +
                                     aHardwareInforamtion.HardDriveSerialNo.Trim() + "_NAME#"+COMPUTERNAME.Trim();
                        key = Regex.Replace(key, "[^0-9a-zA-Z#_-]+", "");

                        LicenceKey restaurantLicence = aLicenceKeyBll.GetRestaurantLicenceBykey(restaurantInformation.Id, licenseCodeTextBox.Text);
                        //LicenceKey restaurantLicence = aLicenceKeyBll.GetRestaurantLicence(restaurantInformation.Id, key);
                        if (restaurantLicence != null && restaurantLicence.restaurant_id > 0){
                            restaurantLicence.restaurant_id = restaurantInformation.Id;
                            restaurantLicence.license_code = licenseCodeTextBox.Text;
                            restaurantLicence.hardware_info = key;
                            restaurantLicence.date_installed = DateTime.Now.Date.ToShortTimeString();
                            restaurantLicence.is_installed = 1;
                            if(aLicenceKeyBll.UpdateLicenceKey(restaurantLicence, licenseCodeTextBox.Text)=="Yes")
                            {
                                MessageBox.Show("Licence key successfully updated. Please restart your application", "Licencekey Confimation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                Application.Exit();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please check sever pc.", "Licencekey Confimation Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }

                    }
                    catch (Exception exception)
                    {
                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                        aErrorReportBll.SendErrorReport(exception.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please check input.", "Licencekey Confimation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
          

        }

        private bool ValidForm()
        {
            if (licenseCodeTextBox.Text.Trim().Length <= 0) return false;
            return true;
        }

  

        private void licenseCodeTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = licenseCodeTextBox;
        }

        private void CheckSoftwareActivationOfOfflineForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
