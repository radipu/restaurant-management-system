using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TomaFoodRestaurant.Sequrity
{
    public partial class UrlChageForm : Form
    {
        public UrlChageForm(){
            InitializeComponent();
        }

        private void UrlChageForm_Load(object sender, EventArgs e)
        {
            txtBackend.Text = Properties.Settings.Default.backend;
            txtFontEnd.Visible = false;
            chkAutoDiscount.Checked = Properties.Settings.Default.isEnableAutoDiscount;
        }

        private void btnChangeUrl_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.backend = txtBackend.Text;
            Properties.Settings.Default.isEnableAutoDiscount = chkAutoDiscount.Checked ? true : false;
            Properties.Settings.Default.Save();
            MessageBox.Show("Successfully updated URL");

            Application.Exit();
        }
    }
}
