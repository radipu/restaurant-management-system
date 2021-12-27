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
    public partial class PackageDetails : UserControl
    {
        public int PackageId { set; get; }
        public  int OptionIndex { set; get; }
        static int index;
        public int ItemLimit;
        GlobalUrl urls = new GlobalUrl();
        OthersMethod aOthersMethod = new OthersMethod();
        public PackageDetails()
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
            // MessageBox.Show(OptionIndex.ToString());
            if (double.TryParse(qtyTextBox.Text.Trim(), out qty) && double.TryParse(priceTextBox.Text.Trim(), out price))
            {
                if (OptionIndex > 0)
                {
                    RecipePackageMD aRecipePackageMD = mainForm.aRecipePackageMdList.FirstOrDefault(a => a.OptionsIndex == OptionIndex);
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
                    RecipePackageMD aRecipePackageMD = mainForm.aRecipePackageMdList.FirstOrDefault(a => a.OptionsIndex == OptionIndex);
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
           // static int option=OptionIndex;
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

            foreach (PackageDetails c in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {

                c.BackColor = PackageDetails.DefaultBackColor;

            }
            this.BackColor = Color.Red;
            PackageDetails package = aForm.orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>().SingleOrDefault(a => a.BackColor == Color.Red);
            index = package.OptionIndex;
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
                    if (index == c.OptionIndex)
                    {

                        c.BackColor = Color.Red;
                    }
                    else
                    {
                        c.BackColor = PackItemsControl.DefaultBackColor;
                    }
                }

                if (cc.OptionIndex != index)
                {
                    cc.BackColor = PackageDetails.DefaultBackColor;
                }
            }

        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (OptionIndex > 0)
            {
                RecipePackageMD aRecipePackageMD = mainForm.aRecipePackageMdList.SingleOrDefault(a => a.OptionsIndex == OptionIndex);
                aRecipePackageMD.PackageName = nameTextBox.Text;
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

        private void totalPriceLabel_DoubleClick(object sender, EventArgs e)
        {
            mainForm aForm = Application.OpenForms.OfType<mainForm>().FirstOrDefault();
            foreach (PackageDetails cc in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
                {

                    c.BackColor = PackItemsControl.DefaultBackColor;

                }

                cc.BackColor = PackageDetails.DefaultBackColor;

            }
            foreach (MultiplePartControl cc in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
            {

                cc.packageItemsFlowLayoutPanel.BackColor = MultiplePartControl.DefaultBackColor;
                foreach (Label c in cc.packageItemsFlowLayoutPanel.Controls.OfType<Label>())
                {

                    c.BackColor = MultiplePartControl.DefaultBackColor;

                }
                cc.BackColor = MultiplePartControl.DefaultBackColor;

            }
        }

        private void totalPriceLabel_Click(object sender, EventArgs e)
        {
            mainForm aForm = Application.OpenForms.OfType<mainForm>().FirstOrDefault();
            foreach (PackageDetails cc in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
                {

                    c.BackColor = PackItemsControl.DefaultBackColor;

                }

                cc.BackColor = PackageDetails.DefaultBackColor;

            }
            foreach (MultiplePartControl cc in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
            {

                cc.packageItemsFlowLayoutPanel.BackColor = MultiplePartControl.DefaultBackColor;
                foreach (Label c in cc.packageItemsFlowLayoutPanel.Controls.OfType<Label>())
                {

                    c.BackColor = MultiplePartControl.DefaultBackColor;

                }
                cc.BackColor = MultiplePartControl.DefaultBackColor;

            }
        }

   
    }
}
