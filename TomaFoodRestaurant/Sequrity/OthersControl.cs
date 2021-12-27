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
    public partial class OthersControl : Form
    {
        public OthersControl()
        {
            InitializeComponent();
            txtResTime.Text = Convert.ToString(Properties.Settings.Default.countOpenTime);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {

                Properties.Settings.Default.countOpenTime = Convert.ToInt16(txtResTime.Text);
                Properties.Settings.Default.Save();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Properties.Settings.Default.countOpenTime = 10;
                Properties.Settings.Default.Save();

                txtResTime.Text = "10";
            }
         
        }
    }
}
