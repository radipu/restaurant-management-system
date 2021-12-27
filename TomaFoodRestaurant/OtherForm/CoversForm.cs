using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class CoversForm : Form
    {
        public static string Status="";
        public static string Covers = "";
        public CoversForm()
        {
            InitializeComponent();
        }

        private void CoversForm_Load(object sender, EventArgs e)
        {
            LoadPersons();
        }

        private void LoadPersons()
        {
          //  this.BackgroundImage = Properties.Resources.people;

            for (int i = 0; i < 30; i++) {

                Button aButton = new Button();
                aButton = new RectangleButton();
                aButton.Width = 150;
                aButton.Height = 60;
                aButton.Font = new System.Drawing.Font(aButton.Font.Name, 30, FontStyle.Bold);
                aButton.FlatStyle = FlatStyle.Flat;
                aButton.FlatAppearance.BorderSize = 0;
                aButton.BackColor = Color.DarkSeaGreen;
                aButton.ForeColor = Color.White;
                aButton.Name = (i + 1).ToString();
                aButton.Text = (i + 1).ToString();
                aButton.BackgroundImage = Properties.Resources.people3;
               aButton.BackgroundImageLayout=ImageLayout.None;
                aButton.TextImageRelation = TextImageRelation.ImageBeforeText;
                aButton.Click += new EventHandler(CoversButton_Click);
                coversFlowLayoutPanel.Controls.Add(aButton);
            }

            Button tempButton = new Button();
            tempButton = new RectangleButton();
            tempButton.Width = 150;
            tempButton.Height = 60;
            tempButton.Font = new System.Drawing.Font(tempButton.Font.Name, 16, FontStyle.Bold);
            tempButton.FlatStyle = FlatStyle.Flat;
            tempButton.FlatAppearance.BorderSize = 0;
            tempButton.BackColor = Color.Red;
            tempButton.ForeColor = Color.White;
            tempButton.Name = "Cancel";
            tempButton.Text = "Cancel";
            tempButton.Click += new EventHandler(CoversButton_Click);
            coversFlowLayoutPanel.Controls.Add(tempButton);
            

        }

        private void CoversButton_Click(object sen31, EventArgs e)
        {
            Button aButton = sen31 as Button;
            if (aButton != null && aButton.Text == "Cancel")
            {
                Status = "cancel";
                Covers = "no";
            }
            else {

                Status = "ok";
                Covers = aButton.Text;
                 }

            this.Close();
        }
    }
}
