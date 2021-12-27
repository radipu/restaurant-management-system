using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.Sequrity
{
    public partial class AdminPermissionSection : Form
    {
        private RestaurantUsers users;
        private PrinterSettingsForm settings;

        public AdminPermissionSection(RestaurantUsers user,PrinterSettingsForm setting)
        {
            InitializeComponent();

            this.users = user;
            this.settings = setting;
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            users.Usertype = "Admin";
            var pass = GlobalSetting.RestaurantUsers.Password.ToUpper();
            string conformPass = new GeneralInformation().GetSha1(txtPassword.Text);


            if (pass == conformPass && users.Usertype=="Admin")
            {
                settings.pannelAdminSection.Visible = true;
                this.Close();
            }
            else
            {

                lblMessage.Text = "Please Conform Your Authoriztion !".ToUpper();
                lblMessage.Visible = true;
            }
        }
    }
}
