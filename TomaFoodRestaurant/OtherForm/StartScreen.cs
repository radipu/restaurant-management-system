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
    public partial class StartScreen : Form
    {
        public StartScreen(string statusMessage)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(statusMessage))
            {
             // loadingMessageTextBox.SelectionStart = 0;
            }
            
        }
    }
}
