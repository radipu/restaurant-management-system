using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class PathChangingForm : Form
    {
        public PathChangingForm()
        {
            InitializeComponent();
        }

        private void logainButton_Click(object sender, EventArgs e)
        {
            if (paawordTextBox.Text.Trim() == "t-pospass")
            {
                reportGroupBox.Enabled = true;
                
                paawordTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please try again.");
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
               // File.WriteAllText("Config/call.txt", path);
                databasePathTextbox.Text = path;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (databasePathTextbox.Text.Trim().Length <= 2) return;
            DialogResult dialogResult = MessageBox.Show("Do You Want to Save Database Path?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                File.WriteAllText("Config/ppp.txt", databasePathTextbox.Text);
                Application.Restart();
              //  this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void PathChangingForm_Load(object sender, EventArgs e)
        {
            string path = File.ReadAllText("Config/ppp.txt");
            databasePathTextbox.Text = path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //string con = "SERVER=" + textBoxIP.Text + ";DATABASE=" + textBoxdb.Text + ";UID=" + textBoxusername.Text + ";PASSWORD=" + textBoxpassword.Text + ";";

            //Properties.Settings.Default.connString = con;
            //Properties.Settings.Default.Save();
            //MessageBox.Show(Properties.Settings.Default.connString);
        }
    }
}
