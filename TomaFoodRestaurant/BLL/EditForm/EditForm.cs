using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;

namespace TomaFoodRestaurant.EditForm
{
    public partial class EditForm : Form
    {


        private PackageItemFormLoadNew formviewForm;

        public EditForm(PackageItemFormLoadNew form)
        {
            InitializeComponent();



            formviewForm = form;

            form.aMainFormView.VirtualKeyBoard(0,Screen.PrimaryScreen.Bounds.Bottom-280);

        }
        public string GetReplacement(Match m)
        {
            return m.Groups[1].Success ? m.Groups[1].Value : txtEditName.Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {



                PackageItem item = (PackageItem)formviewForm.packagegridView.GetFocusedRowCellValue("Class");

                if (formviewForm.packagegridView.GetFocusedRowCellValue("PackageItemName").ToString().Contains("</br>"))
                {
                    string packageTitleName =
                        formviewForm.packagegridView.GetFocusedRowCellValue("PackageItemName").ToString();
                    string withoutOptionItem = item.Qty + " X " + txtEditName.Text +
                                               packageTitleName.Substring(packageTitleName.IndexOf("</br>"));
                    item.ItemName = txtEditName.Text;
                    formviewForm.packagegridView.SetFocusedRowCellValue("Class", item);
                    formviewForm.packagegridView.SetFocusedRowCellValue("EditName", txtEditName.Text);
                    formviewForm.packagegridView.SetFocusedRowCellValue("PackageItemName", withoutOptionItem);
                }
                else
                {
                   
                    string withoutOptionItem =item.Qty + " X "+ txtEditName.Text;
                    item.ItemName = txtEditName.Text;
                    formviewForm.packagegridView.SetFocusedRowCellValue("Class", item);
                    formviewForm.packagegridView.SetFocusedRowCellValue("EditName", txtEditName.Text);
                    formviewForm.packagegridView.SetFocusedRowCellValue("PackageItemName", withoutOptionItem);
                }
                
                formviewForm.pacakgegridControl.Update();

                this.Close();


            }));
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            txtEditName.Text = formviewForm.packagegridView.GetFocusedRowCellValue("EditName").ToString();
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
