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
    public partial class PrintCopySetupForm : UserControl
    {
        PrintCopySetup aPrintCopySetup = new PrintCopySetup();
        public PrintCopySetupForm()
        {
            InitializeComponent();
        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidForm())
                {
                    PrintCopySetupBLL aPrintCopySetupBll=new PrintCopySetupBLL();
                   
                    aPrintCopySetup.TableCopy = Convert.ToInt32(tableCopyTextBox.Text.Trim());
                    aPrintCopySetup.TakeawayCopy = Convert.ToInt32(takeawayTextBox.Text.Trim());
                    aPrintCopySetup.CollectionCopy = Convert.ToInt32(collectionTextBox.Text.Trim());
                    if (aPrintCopySetup.Id > 0)
                    {
                        int sr = aPrintCopySetupBll.UpdatePrintCopySetup(aPrintCopySetup);
                        MessageBox.Show("Print Copy has been updated successfully", "Print Copy Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       // AllSettingsForm.Status = "reload";

                    }
                    else 
                    {
                        int sr = aPrintCopySetupBll.InsertPrintCopySetup(aPrintCopySetup);
                        MessageBox.Show("Print Copy has been inserted successfully", "Print Copy Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AllSettingsForm.Status = "reload";
                    
                    }
                    LoadPrintCopyInformation();
                }
                else
                {
                    MessageBox.Show("Please Check Input Field", "Printer Setup Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
         
        }

        private bool ValidForm()
        {
            int copy;
            if (!int.TryParse(takeawayTextBox.Text.Trim(),out copy))
            {
                return false;
            }
            if (!int.TryParse(collectionTextBox.Text.Trim(), out copy))
            {
                return false;
            }
            if (!int.TryParse(tableCopyTextBox.Text.Trim(), out copy))
            {
                return false;
            }

            return true;
        }

        private void PrintCopySetupForm_Load(object sender, EventArgs e)
        {
            LoadPrintCopyInformation();
        }

        private void LoadPrintCopyInformation()
        {
            PrintCopySetupBLL aPrintCopySetupBll = new PrintCopySetupBLL();
            aPrintCopySetup = aPrintCopySetupBll.GetPrintCopy();
            takeawayTextBox.Text = aPrintCopySetup.TakeawayCopy.ToString();
            tableCopyTextBox.Text = aPrintCopySetup.TableCopy.ToString();
            collectionTextBox.Text = aPrintCopySetup.CollectionCopy.ToString();
        }


    }
}
