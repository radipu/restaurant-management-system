using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class MultiplePartControl : UserControl
    {
        public int CategoryId { set; get; }
        public int OptionIndex { set; get; }
        static int index;
        public int ItemLimit;
        GlobalUrl urls = new GlobalUrl();
        OthersMethod aOthersMethod = new OthersMethod();
        public MultiplePartControl()
        {
            InitializeComponent();
            index = OptionIndex;
            packageItemsFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();

        }


        private void qtyTextBox_TextChanged(object sender, EventArgs e)
        {
            double qty;
            double price;
            if (double.TryParse(qtyTextBox.Text.Trim(), out qty) && double.TryParse(priceTextBox.Text.Trim(), out price))
            {
                if (OptionIndex > 0)
                {
                    RecipeMultipleMD aRecipePackageMD = mainForm.aRecipeMultipleMdList.FirstOrDefault(a => a.OptionsIndex == OptionIndex);
                    aRecipePackageMD.Qty = (int)qty;
                }
                double totalprice = qty * price;
                totalPriceLabel.Text = totalprice.ToString("F02");
            }
        }

        private void priceTextBox_TextChanged(object sender, EventArgs e)
        {
            double qty;
            double price;
            if (double.TryParse(qtyTextBox.Text.Trim(), out qty) && double.TryParse(priceTextBox.Text.Trim(), out price))
            {
                if (OptionIndex > 0)
                {
                    RecipeMultipleMD aRecipePackageMD = mainForm.aRecipeMultipleMdList.FirstOrDefault(a => a.OptionsIndex == OptionIndex);
                    aRecipePackageMD.UnitPrice = price;
                }
                double totalprice = qty * price;
                totalPriceLabel.Text = totalprice.ToString("F02");
            }
        }

        private void nameTextBox_MouseClick(object sender, MouseEventArgs e)
        {


            ClearAllSelect();
            try
            {
                aOthersMethod.NumberPadClose();
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 480);
                    NumberPad aNumberPad = new NumberPad(aPoint);
                    aNumberPad.Show();

                }

            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

        }

        private void ClearAllSelect()
        {
            mainForm aForm = Application.OpenForms.OfType<mainForm>().FirstOrDefault();

            foreach (MultiplePartControl cc in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
            {
              
                    cc.packageItemsFlowLayoutPanel.BackColor = MultiplePartControl.DefaultBackColor;
                    foreach (Label c in cc.packageItemsFlowLayoutPanel.Controls.OfType<Label>())
                    {

                        c.BackColor = MultiplePartControl.DefaultBackColor;

                    }
                    cc.BackColor = MultiplePartControl.DefaultBackColor;
                
            }


            foreach (RecipeTypeDetails control1 in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
            {
                foreach (deatilsControls c in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                {

                    c.BackColor = deatilsControls.DefaultBackColor;

                }
            }

            foreach (PackageDetails cc in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
                {

                    c.BackColor = PackItemsControl.DefaultBackColor;

                }

                cc.BackColor = PackageDetails.DefaultBackColor;

            }

            foreach (MultiplePartControl c in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
            {

                c.BackColor = MultiplePartControl.DefaultBackColor;

            }

            this.BackColor = Color.Red;

            foreach (MultiplePartControl cc in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
            {
                
                if (cc.OptionIndex == OptionIndex)
                {
                    cc.packageItemsFlowLayoutPanel.BackColor = Color.Red;
                foreach (Label c in cc.packageItemsFlowLayoutPanel.Controls.OfType<Label>())
                {
                   
                        c.BackColor = Color.Red;
                    
                }
                    cc.BackColor = Color.Red;
                }
            }


        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (OptionIndex > 0)
            {
                RecipeMultipleMD aRecipePackageMD = mainForm.aRecipeMultipleMdList.SingleOrDefault(a => a.OptionsIndex == OptionIndex);
                aRecipePackageMD.MultiplePartName = nameTextBox.Text;
            }
        }

        private void qtyTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            ClearAllSelect();
            try
            {
                aOthersMethod.KeyBoardClose();
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(200, 200);
                    NumberForm aNumberPad = new NumberForm(aPoint);
                    aNumberPad.Show();

                }

            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }
    }


}
