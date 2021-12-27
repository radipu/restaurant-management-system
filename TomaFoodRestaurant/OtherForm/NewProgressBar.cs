using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class NewProgressBar : Form
    {
        public NewProgressBar(int maxvalue)
        {
            InitializeComponent();

            labelpercent.Visible = true;
            label2.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = maxvalue;
            progressBar(1);
        }

        public void progressBar(int currentvalue)
        {
            progressBar1.Value = currentvalue;

            if (progressBar1.Value == progressBar1.Maximum) {
                button1.Visible = true;
                label1.Visible = true;
                labelpercent.Text = progressBar1.Value + "%";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
