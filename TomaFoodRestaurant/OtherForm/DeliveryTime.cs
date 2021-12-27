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
    public partial class DeliveryTime : Form
    {
        public static string time;
        public static string status;
        public DeliveryTime()
        {
            InitializeComponent();
        }

        private void DeliveryTime_Load(object sender, EventArgs e)
        {
            LoadTimeCombo();
        }

        private void LoadTimeCombo()
        {
            List<string> times = new List<string>() { "20 Mins", "45 Mins", "50 Mins", "1.15 Hours", "1.30 Hours", "1.45 Hours", "2.00 Hours", };
            timeComboBox.DataSource =times;
        }
        private void acceptButton_Click(object sender, EventArgs e)
        {
            time = timeComboBox.Text;
            status = "ok";
            this.Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            
            status = "cancel";
            this.Close();
        }
    }
}
