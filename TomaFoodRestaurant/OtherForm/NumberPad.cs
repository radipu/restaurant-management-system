using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class NumberPad : Form
    {
        const int WS_EX_NOACTIVATE = 0x08000000;
        //this line of code fixed the issue
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();
        Point location=new Point();
        public NumberPad(Point aPoint)
        {
            InitializeComponent();
            location = aPoint;

            //foreach (System.Windows.Forms.Control cont in this.Controls)
            //    cont.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyPress);

            //foreach (System.Windows.Forms.Control cont in this.Controls)
            //    cont.Click += this.MainForm_Click;
        }


        private void MainForm_Click(object sender, EventArgs e)
        {
            try
            {
              //  this.Close();

            }
            catch
            {
            }
        }


        private void MainForm_KeyPress(object sender, EventArgs e)
        {
            try
            {

             //  this.Close();
            }
            catch
            {
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            var screen = Screen.FromPoint(this.Location);
            this.Location = location;
            base.OnLoad(e);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams param = base.CreateParams;
                param.ExStyle |= WS_EX_NOACTIVATE;


                //This line of code fix the issues
                param.Style = 0x40000000 | 0x4000000;
                param.Parent = GetDesktopWindow();

                return param;
            }
        } 

        private void keyboardButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            
            if (radioButton1.Checked == true)
            {
                SendKeys.Send(btn.Text.ToUpper());
            }
            else {

                SendKeys.Send(btn.Text.ToLower());
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true)
            {
                radioButton1.BackColor = Color.Black;
                radioButton1.ForeColor = Color.White;
                radioButton1.Checked = false;
            }
            else
            {
                radioButton1.BackColor = Color.Silver;
                radioButton1.ForeColor = Color.Black;             
                radioButton1.Checked = true;
            }
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSpace_Click(object sender, EventArgs e)
        {
            SendKeys.Send(" ");
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{DELETE}");
        }

        private void specialButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;

              SendKeys.Send(btn.Text);
            
        }

        private void button49_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{RIGHT}");
        }

        private void button50_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{LEFT}");
        }
         

       
    }
}
