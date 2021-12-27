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
    public partial class NumberForm : Form
    {
        const int WS_EX_NOACTIVATE = 0x08000000;
        //this line of code fixed the issue
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();
        Point location = new Point();
        public NumberForm(Point aPoint)
        {
            InitializeComponent();
            location = aPoint;
        }

        protected override void OnLoad(EventArgs e)
        {
            var screen = Screen.FromPoint(this.Location);
            this.Location = location;
          
            base.OnLoad(e);
            SendKeys.Send("{END}");
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
        private void button14_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");
        }
        private void numberButton_click(object sen24, EventArgs e)
        {
            Button aButton = sen24 as Button;
            SendKeys.Send(aButton.Text);
           
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{ENTER}");
            this.Close();
        }
    }
}
