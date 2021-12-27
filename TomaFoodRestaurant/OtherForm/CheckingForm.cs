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
    public partial class CheckingForm : Form
    {

        public CheckingForm(Control control)
        {
            InitializeComponent();
            this.Controls.Add(control);
        }


    }
}
