using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.Sequrity
{
    public partial class AutorizeForm : Form
    {
        public RestaurantUsers user=new RestaurantUsers();

        public AutorizeForm(RestaurantUsers form,string type=null)
        {
            InitializeComponent();

            lblHeading.Text = "Enter password";

            if (type == "discount")
            {
                lblHeading.Text = "Enter the admin password";
            }
            if (type == "till")
            {
                lblHeading.Text = "Admin password required.";
            }

            if (type == "resSetup")
            {
                lblHeading.Text = "Admin password required.";
            }

            lblHeading.Text = lblHeading.Text.ToUpper();
            user = form;
        }

        private void AutorizeForm_Load(object sender, EventArgs e)
        {

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            user.Autorize = false;
            this.Close();
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
           // user.Usertype = "Admin";
          Submit();
        }

        public void Submit()
        {
            var pass = GlobalSetting.RestaurantUsers.Password.ToUpper();
            var userType = GlobalSetting.RestaurantUsers.Usertype;
            string conformPass = new GeneralInformation().GetSha1(txtPassword.Text);

            //  if (txtPassword.Text == "123456")
                
            if (pass == conformPass)
             {
                user.Autorize = true;
                this.Close();}
            else
            {
                lblMessage.Text = "invaild password !".ToUpper();
                lblMessage.Visible = true;
            }
        }

        private void AutorizeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode  == Keys.Enter)
            {
                Submit();
            }
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                GlobalUrl urls = new GlobalUrl();
                urls = aGlobalUrlBll.GetUrls(); 
                OthersMethod aOthersMethod = new OthersMethod();
                aOthersMethod.KeyBoardClose();
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(10, 400);
                    NumberPad aNumberPad = new NumberPad(aPoint);
                    aNumberPad.Show();
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;

                return Params;
            }
        }
    }
}
