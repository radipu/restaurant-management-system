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
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant
{
    internal partial class deatilsControls : UserControl
    {
        public int ItemId { set; get; }
        public int OptionIndex { set; get; }
        public int CategoryId { set; get; }
        GlobalUrl urls = new GlobalUrl();
        GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
        OthersMethod aOthersMethod = new OthersMethod();
        public deatilsControls()
        {
            InitializeComponent();

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
                    OrderItemDetailsMD aOrderItemDetailsMD = mainForm.aOrderItemDetailsMDList.FirstOrDefault(a => a.OptionsIndex == OptionIndex);
                    aOrderItemDetailsMD.Qty = (int)qty;
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
                    OrderItemDetailsMD aOrderItemDetailsMD = mainForm.aOrderItemDetailsMDList.SingleOrDefault(a => a.OptionsIndex == OptionIndex);
                    aOrderItemDetailsMD.Price = price;
                }
                double totalprice = qty * price;
                totalPriceLabel.Text = totalprice.ToString("F02");
            }
        }

        private void nameTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            ClearAllSelect();
            this.BackColor = Color.Red;

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

        static void ClearAllSelect()
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
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (OptionIndex > 0)
                {
                    OrderItemDetailsMD aOrderItemDetailsMD = mainForm.aOrderItemDetailsMDList.FirstOrDefault(a => a.OptionsIndex == OptionIndex);
                    aOrderItemDetailsMD.ItemName = nameTextBox.Text;
                }
            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private void qtyTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            ClearAllSelect();
            this.BackColor = Color.Red;

            try
            {
                aOthersMethod.KeyBoardClose();
                if (!Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
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
