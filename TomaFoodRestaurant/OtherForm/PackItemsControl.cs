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
    public partial class PackItemsControl : UserControl
    {
        public int PackageId { set; get; }
        public int OptionIndex { set; get; }
        public int ItemId { set; get; }
        public int PackageItemOptionIndex { set; get; }
        static int index;
        GlobalUrl urls = new GlobalUrl();
        OthersMethod aOthersMethod = new OthersMethod();
        public PackItemsControl()
        {
            InitializeComponent();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (OptionIndex > 0)
            {
                PackageItem aPackageItem = mainForm.aPackageItemMdList.FirstOrDefault(a => a.ItemId == ItemId && a.OptionsIndex == OptionIndex);
                aPackageItem.ItemName = nameTextBox.Text;
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
            this.BackColor = Color.Red;
            int cnt = 0;
            PackItemsControl package = new PackItemsControl();
            foreach (PackageDetails cc in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
                {

                    if (c.BackColor == Color.Red) {
                        package = c;
                        cnt++;
                    }

                }


            }



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
                if (cc.OptionIndex == index) {
                    cc.BackColor = Color.Red;
                }
                foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
                {
                    if (index == c.OptionIndex && c.ItemId == package.ItemId)
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

        private void totalPriceLabel_TextChanged(object sender, EventArgs e)
        {
            if (OptionIndex > 0)
            {
              //  PackageItem aPackageItem = mainForm.aPackageItemMdList.SingleOrDefault(a => a.ItemId == ItemId);
              //  aPackageItem.Price += Convert.ToDouble("0"+totalPriceLabel.Text);
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

        private void PackItemsControl_DoubleClick(object sender, EventArgs e)
        {
            ClearAllSelect();
        }

        private void PackItemsControl_Click(object sender, EventArgs e)
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
