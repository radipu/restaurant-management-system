using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class PrinterUpdateForm : Form
    {
        PrinterSetup aPrinter = new PrinterSetup();
        public PrinterUpdateForm(PrinterSetup printer)
        {
            InitializeComponent();
            aPrinter = printer;
        }

        private void PrinterUpdateForm_Load(object sender, EventArgs e)
        {
            LoadInstalledPrinter();
            LoadPrinter();
        }

        private void LoadInstalledPrinter()
        {
            List<string> printerlist = new List<string>();
            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                printerlist.Add(printer);
            }
            printerNamecomboBox.DataSource = printerlist;
        }

        private void LoadPrinter()
        {
            printerAddressTextBox.Text = aPrinter.PrinterAddress;
            printCopyTextBox.Text = aPrinter.PrintStyle.ToString();
            string[] items = aPrinter.PrinterName.Split('\\');
            string printer = items[3];
            printerNamecomboBox.SelectedIndex = printerNamecomboBox.FindStringExact(printer);
        }

        private void printCopyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private bool ValidForm()
        {
            int n;
            if (printerAddressTextBox.Text.Trim().Length == 0)
            {
                return false;
            }
            if (!int.TryParse(printCopyTextBox.Text.Trim(), out n))
            {
                return false;
            }
            if (printerNamecomboBox.Text.Trim().Length == 0)
            {
                return false;
            }

            return true;
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            if (ValidForm())
            {
                PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                string machineName = System.Environment.MachineName;
                string printer = "\\\\" + machineName + "\\" + printerNamecomboBox.Text;
                aPrinter.PrintStyle = printCopyTextBox.Text.Trim();
                aPrinter.PrinterAddress = printerAddressTextBox.Text.Trim();
                aPrinter.PrinterName = printer;
                aPrinter.RestaurantId = GlobalSetting.RestaurantInformation.Id;
                int id = aPrinterSetupBll.UpdatePrinter(aPrinter);
                if (id > 0)
                {
                    MessageBox.Show("Printer has been updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

            }
            else MessageBox.Show("Please Check Input Field", "Printer Setup Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
