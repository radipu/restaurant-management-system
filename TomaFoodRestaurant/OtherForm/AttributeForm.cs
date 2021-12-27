using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class AttributeForm : Form
    {

        public static string AttributeName="";
        public static double Price=0;
        public static string Result="";

        public static double Discount = 0;
        public AttributeForm()
        {
            InitializeComponent();
        }

        private void AttributeForm_Load(object sender, EventArgs e)
        {
            try
            {

                    RestaurantMenuBLL aRestaurantMenuBll=new RestaurantMenuBLL();
                    List<AttributeButton> aAttributeButtons = aRestaurantMenuBll.GetAllAttributeButton();
                    int count = aAttributeButtons.Count;
                    int height = (count / 8);
                    if (count % 8 != 0) height += 1;
                    attributeFlowLayoutPanel.Height = (height * 100);
                    foreach (AttributeButton aAttributeButton in aAttributeButtons)
                    {
                        aAttributeButton.Click += new EventHandler(AttributeButton_Click);
                        attributeFlowLayoutPanel.Controls.Add(aAttributeButton);
                    }
                    buttonPanel.Location = new Point(buttonPanel.Location.X, attributeFlowLayoutPanel.Size.Height + attributeFlowLayoutPanel.Location.Y);

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

        }

        private void AttributeButton_Click(object sen10, EventArgs e)
        {
            AttributeButton aButton = sen10 as AttributeButton;
            AttributeName = aButton.AttributeName;
            Price = aButton.Price;
            Discount = aButton.Discount;
            Result = "ok";
            this.Close();
        }


       

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Result = "cancel";
            this.Close();
        }

    }
}
