using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class RecipeTypeDetails : UserControl
    {
        public int RecipeTypeId { set; get; }
    
        public ReceipeTypeButton ReceipeTypeButton { set; get; }
       
        public RecipeTypeDetails()
        {
            InitializeComponent();
            
         
        }


        private void recipeTypelabel_Click(object sender, EventArgs e)
        {
            if (!typeflowLayoutPanel1.Visible)
            {
                typeflowLayoutPanel1.Visible = true;
            }
            else 
            {
                typeflowLayoutPanel1.Visible = false;
            }
        }

        public void recipeTypelabel_TextChanged(object sender, EventArgs e)
        {
            if (recipeTypelabel.Text != "")
            {
                typeflowLayoutPanel1.Visible = false;
            }
            else {

                typeflowLayoutPanel1.Visible = true;
            }
        }

       
    }
}
