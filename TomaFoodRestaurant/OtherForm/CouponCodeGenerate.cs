using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.About;
using DevExpress.Utils.Drawing.Helpers;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using Microsoft.Win32;

using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;
using TextBox = System.Windows.Forms.TextBox;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class CouponCodeGenerate : UserControl
    {
        public CouponCodeGenerate()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clean();
            xtraTabControl1.SelectedTabPage = xtraTabPageCreate;
            btnCreate.Text = "CREATE";
            CreateRandomStringGenarete();
            CreatePriviewView();

          }
        public static void PrintDocument(object sender,WebBrowserDocumentCompletedEventArgs e)
        {
            var browser = sender as WebBrowser;
            // Print the document now that it is fully loaded.
            browser.Print();
            // Dispose the WebBrowser now that the task is complete. 
           // browser.Dispose();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPageList;
        }

        private void CouponCodeGenerate_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = new CustomerBLL().GetCustomerCoupon().ToList();
                //CreatePriviewView();
            //webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(Print);
              
           
        }
        public void Clean()
        {
            txtCode.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtCouponTile.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtMessage.Text = string.Empty;
            cmbType.SelectedText = null;
            cmbServiceType.SelectedText = null;
            cmbType.SelectedText = null;
            cmbUsage.SelectedText = null;
        }
        public void CreatePriviewView()
        {
            try
            {
                string header = "<div class='logo'>";
                header += "<div class='header'>";
                RestaurantInformation information = GlobalSetting.RestaurantInformation;

                string resInformation = "<div style='padding-top:10px;'><h1 style='font-family:play;font-size: 1.2em;'><b>" + information.RestaurantName.ToUpper() + "</b>" +
                    "<p style='font-family:play;font-size: 15px'>" + information.City + "<br>" + information.House + " " + information.Address + " " + information.Postcode + "</p>" +
                    "</h1></div>";
               

                string title = "<h1 style='padding-top:15px ;font-family:Comfortaa;font-size: 1.5em;'>" + txtCouponTile.Text + "</h1>";
                string minimumAamount = "<p  style='padding-top: 2px;font-family:Play;font-size: 0.9em;font-weight:  bolder;'>For minimum order : £ " + txtAmount.Text + "</p>";

                title += minimumAamount;
                string Code = String.Format("<h3 style='font-family:play;font-size: 1.5em;PADDING: 5px; border-top: 1px dashed'>COUPON CODE <br/><span class='barCodeFont'>" + txtCode.Text + "</span></h3>");

                string footer =
                    "<div style='border-top: 1px dashed;padding-bottom: 10px'>" +
                    "<p style='padding-top: 2px;font-family:Play;font-size: 0.9em;font-weight:  bolder;'>" +
                    txtMessage.Text +"</p><p style='padding-top: 5px;font-family:Play;font-size: 14px;font-weight:  bold'>Valid until : " +
                    txtExpire.Value.ToString("dd-MMM-yyyy") + "</p></div>";


                header += resInformation + title + Code + footer + "</div>";
                string body = header + "</div>";
                
                var imageString = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(@"Image/frame1.png")));
                string htmlPage =
                    "<!DOCTYPE html><html><head><meta http-equiv='X-UA-Compatible' content='IE=edge'><title>Title of the document</title><style>body{margin:0;}.barCodeFont{font-family: 'Libre Barcode 128 Text';font-size: 2em;font-weight: bolder}.header{margin: 0;padding: 10px;width: 346px;height:465px;background: url('" + imageString + "');background-repeat: no-repeat;background-size: 100% 100%;text-align: center}h3,h1,p{margin: 0px;padding: 0px;}</style></head>";
                htmlPage += "<body>" + body + "</body></html>";
                webBrowser1.DocumentText = htmlPage;
                webBrowser1.DocumentCompleted -= PrintDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!VaildForm())
                {
                    return;
                }
                CouponCode couponCode = new CouponCode();
                couponCode.Code = txtCode.Text;

                couponCode.MinAmount = Convert.ToDouble(txtAmount.Text);
                couponCode.Title = txtCouponTile.Text;
                couponCode.Discount = Convert.ToInt16(txtDiscount.Text);
                couponCode.Message = Convert.ToString(txtMessage.Text);
                couponCode.Expiring = txtExpire.Value;
                couponCode.ServiceType = cmbServiceType.Text;
                couponCode.Type = cmbType.Text;
                couponCode.Usage = cmbUsage.Text;

                if (btnCreate.Text == "CREATE")
                {
                    string message = Create(couponCode);
                    GeneratePrint(couponCode);
                }
                else if (btnCreate.Text == "EDIT")
                {
                    couponCode.Id = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.GetFocusedDataSourceRowIndex(), "Edit"));

                    string message = Edit(couponCode);
                }
                gridControl1.DataSource = new CustomerBLL().GetCustomerCoupon().ToList();
            }
            catch (Exception ex)
            {

                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
            }

            xtraTabControl1.SelectedTabPage = xtraTabPageList;
        }

        private void repEdit_Click(object sender, EventArgs e)
        {
            HyperLinkEdit repository = sender as HyperLinkEdit;

            CouponCode code = new CouponCode();
            code.Id = Convert.ToInt16(repository.Text);
            CouponCode couponCode = new CustomerBLL().GetCustomerCouponById(code);
            txtCode.Text = couponCode.Code;
            txtAmount.Text = Convert.ToString(couponCode.MinAmount);
            txtCouponTile.Text = couponCode.Title;
            txtDiscount.Text = Convert.ToString(couponCode.Discount);
            txtMessage.Text = couponCode.Message;
            txtExpire.Value = couponCode.Expiring;
            cmbServiceType.Text = couponCode.ServiceType;
            cmbType.Text = couponCode.Type;
            cmbUsage.Text = couponCode.Usage;
            xtraTabControl1.SelectedTabPage = xtraTabPageCreate;
            btnCreate.Text = "EDIT";
           
            CreatePriviewView();

        }

        public void GeneratePrint(CouponCode couponCode)
        {
            try
            {
                List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
                PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                PrinterSetups = aPrinterSetupBll.GetTotalPrinterList();
                var printer = PrinterSetups.FirstOrDefault(a => a.PrintStyle == "Receipt");
                if (printer != null)
                {
                    PrinterInformation.SetDefault(printer.PrinterName);

                }
                string header = "<div class='logo'>";
                header += "<div class='header'>";
                RestaurantInformation information = GlobalSetting.RestaurantInformation;
                string resInformation = "<div style='padding-top:10px;'><h1 style='font-family:play;font-size: 1.2em;'><b>" + information.RestaurantName.ToUpper() + "</b>" +
                   "<p style='font-family:play;font-size: 15px'>" + information.City + "<br>" + information.House + " " + information.Address + " " + information.Postcode + "</p>" +
                   "</h1></div>";
                
                string title = "<h1 style='padding-top:15px ;font-family:Comfortaa;font-size: 1.5em;'>" + couponCode.Title + "</h1>";
                string minimumAamount = "<p  style='padding-top: 2px;font-family:Play;font-size: 0.9em;font-weight:  bolder;'>For minimum order : £ " + txtAmount.Text + "</p>";
                title += minimumAamount;
                string Code = String.Format("<h3 style='font-family:play;font-size: 1.5em;PADDING: 5px; border-top: 1px dashed'>COUPON CODE <br/><span class='barCodeFont'>" + couponCode.Code + "</span></h3>");
                string footer =
                    "<div style='border-top: 1px dashed;padding-bottom: 10px'>" +
                    "<p style='padding-top: 2px;font-family:Play;font-size: 0.9em;font-weight:  bolder;'>" +
                    couponCode.Message +
                    "</p><p style='padding-top: 5px;font-family:Play;font-size: 14px;font-weight:  bold'>Valid until : " +
                    couponCode.Expiring.Date.ToString("dd-MMM-yyyy") + "</p></div>";


                header += resInformation + title + Code + footer + "</div>";
                string body = header + "</div>";
                
                var imageString = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(@"Image/frame1.png")));
                string htmlPage =
                    "<!DOCTYPE html><html><head><meta http-equiv='X-UA-Compatible' content='IE=edge'><title>Title of the document</title><style>body{margin-left:2px;}.barCodeFont{font-family: 'Libre Barcode 128 Text';font-size: 2em;font-weight: bolder}.header{margin: 0;padding: 10px;width: 250px;height: auto;background: url('" + imageString + "');background-repeat: no-repeat;background-size: 100% 100%;text-align: center}h3,h1,p{margin: 0px;padding: 0px;}</style></head>";
                htmlPage += "<body>" + body + "</body></html>";
                
                Print(htmlPage);}
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
         
          
        }

        private void Print(string html)
        {

           // webBrowser1  = new WebBrowser() { DocumentText = string.Empty };
            webBrowser1.DocumentText=html;

            webBrowser1.DocumentCompleted -= PrintDocument;
            webBrowser1.DocumentCompleted += PrintDocument;
            string keyName = @"Software\\Microsoft\\Internet Explorer\\PageSetup";
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true);
            if (key != null)
            {
                key.SetValue("footer", "");
                key.SetValue("header", "");
                key.SetValue("margin_bottom", "0");
                key.SetValue("margin_left", "0");
                key.SetValue("margin_right", "0");
                key.SetValue("margin_top", "0");
                key.SetValue("Print_Background", "yes");
                //wbPrintString.Print();//webBrowser1.ShowPrintPreviewDialog();
                
            }

        }

        public string Edit(CouponCode couponCode)
        {
            if (!VaildForm())
            {return "";
            }

            return new CustomerBLL().CouponCRUID(couponCode, "Edit");
        }

        public string Delete(CouponCode couponCode)
        {
            DialogResult result = MessageBox.Show("Do you wanted to remove this Coupon ?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                return new CustomerBLL().CouponCRUID(couponCode, "Remove");
            }
            else
            {
                return "NO";
            }
           

        }

        public string Create(CouponCode couponCode)
        {

            return new CustomerBLL().CouponCRUID(couponCode, "Create");
        }

        public bool VaildForm()
        {
            if (txtCode.Text == string.Empty)
            {
                MessageBox.Show("Requird Code");

                return false;
            }
            if (txtCouponTile.Text == string.Empty)
            {
                MessageBox.Show("Requird Coupon Title");
                return false;



            }
            if (cmbType.Text == string.Empty)
            {
                MessageBox.Show("Requird select Type");
                return false;



            }
            if (cmbServiceType.Text == string.Empty)
            {
                MessageBox.Show("Requird select service Type");
                return false;



            }
            if (cmbUsage.Text == string.Empty)
            {
                MessageBox.Show("Requird select usages");
                return false;



            }
            if (txtCouponTile.Text == string.Empty)
            {
                MessageBox.Show("Requird Coupon Title");
                return false;



            }
            bool Discount = Regex.IsMatch(txtDiscount.Text, @"^\d{1,9}(\.\d{0,3})?$");

            if (txtDiscount.Text == string.Empty || !Discount)
            {
               string message="Requird Discount Amount";
              
                MessageBox.Show(message);

                return false;

            }

            bool minAmount = Regex.IsMatch(txtAmount.Text, @"^\d{1,9}(\.\d{0,3})?$");

            if (txtAmount.Text == string.Empty || !minAmount)
            {
                string message = "Requird Minimum Amount";
               
                MessageBox.Show(message);

                return false;

            }
           
            return true;
        }

        private void repRemove_Click(object sender, EventArgs e)
        {
            HyperLinkEdit repository = sender as HyperLinkEdit;

            CouponCode code = new CouponCode();
            code.Id = Convert.ToInt16(repository.Text);
            string status = Delete(code);
            if (status != "NO")
            {
                int index = gridView1.GetFocusedDataSourceRowIndex();
                gridView1.DeleteRow(index);
            }

        }

        private void repPrint_Click(object sender, EventArgs e)
        {
            HyperLinkEdit repository = sender as HyperLinkEdit;

            CouponCode code = new CouponCode();

            code.Id = Convert.ToInt16(gridView1.GetRowCellValue(gridView1.GetFocusedDataSourceRowIndex(), "Edit"));
            CouponCode couponCode = new CustomerBLL().GetCustomerCouponById(code);
         
          
            GeneratePrint(couponCode);
        }
        private void xtraTabPageCreate_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCode_TextChanged(object sender, EventArgs e){
            CreatePriviewView();
        }

        private void txtCouponTile_TextChanged(object sender, EventArgs e)
        {
            CreatePriviewView();
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            CreatePriviewView();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

          CreateRandomStringGenarete();
        }

        public void CreateRandomStringGenarete()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ01234567890";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            txtCode.Text = finalString;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

           // CreatePriviewView();
        }

        private void txtDiscount_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {

           CreatePriviewView();
        }
    }
}
