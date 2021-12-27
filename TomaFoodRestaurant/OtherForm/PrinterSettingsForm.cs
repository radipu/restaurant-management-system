using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.OpenGL;
using DevExpress.Utils;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model;


namespace TomaFoodRestaurant.OtherForm
{
    public partial class PrinterSettingsForm : UserControl
    {
        //  private PrinterConfig printConfig;
        GlobalUrl urls = new GlobalUrl();
        public PrinterSettingsForm()
        {
            InitializeComponent();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();

        }

        private void PrinterSettingsForm_Load(object sender, EventArgs e)
        {
            List<string> printerlist = new List<string>();

            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                printerlist.Add(printer);
            }
            printerNamecomboBox.DataSource = printerlist;

            LoadAllPrinter();
            LoadAllRecipeType();
            LoadPrintStyle();

            comboBoxCashDrawer.DataSource = printerlist;
            LoadAllUrl();
            var type = GlobalSetting.RestaurantUsers.Usertype;
            settingInformationPanel.Visible = true;
            xtraTabPage1.Visible = false;
            xtraTabControl1.ShowTabHeader = DefaultBoolean.False;
            xtraTabControl1.SelectedTabPage = xtraTabPage2;

            if (type == "admin")
            {
                xtraTabPage2.Visible = false;
                xtraTabPage1.Visible = true;
                xtraTabControl1.SelectedTabPage = xtraTabPage1;
                btnSyncCustomer.Visible = true;
                pannelAdminSection.Enabled = true;
                pannelAdminSection.Height = 185;
                settingInformationPanel.Visible = false;
                btnSave.Enabled = true;
                buttonUpdateDatabase.Visible = true;
                pannelAdminSection.Visible = true;
                buttonUpdatePostcode.Visible = true;
               
            }



        }

        private void LoadPrintStyle()
        {
            List<string> aList = new List<string> { "Kitchen", "Receipt","Bill" };
            printStyleComboBox.DataSource = aList;
        }

        private void LoadAllRecipeType()
        {

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            List<ReceipeTypeButton> aRecipeTypeList = aRestaurantMenuBll.GetRecipeType();
            foreach (ReceipeTypeButton button in aRecipeTypeList)
            {
                CheckBox aCheckbox = new CheckBox();
                aCheckbox.Text = button.TypeName;
                aCheckbox.Name = button.TypeId.ToString();
                aCheckbox.Font = new System.Drawing.Font("Maiandra GD", 14, FontStyle.Regular); ;
                typeFlowLayoutPanel.Controls.Add(aCheckbox);
            }
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            if (ValidForm())
            {


                string machineName = System.Environment.MachineName;
                string printer = printerNamecomboBox.Text.Replace("\\", "\\\\");

                //if (printerNamecomboBox.Text.Contains(machineName))
                //{
                //    printer = printerNamecomboBox.Text.Replace("\\", "\\\\");
                //}
                //else
                //{
                //    printer = @"\\\\" + machineName + @"\\" + printerNamecomboBox.Text;
                //}

                PrinterSetup aPrinterSettings = new PrinterSetup();
                aPrinterSettings = GetRecipe();
                if (aPrinterSettings.RecipeTypeList.Length <= 0)
                {
                    MessageBox.Show("Please select atleast one recipe type!", "Recipe Type Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                aPrinterSettings.PrintStyle = printStyleComboBox.Text;
                aPrinterSettings.PrinterAddress = printerAddressTextBox.Text.Trim();
                aPrinterSettings.PrinterName = printer;
                aPrinterSettings.PrintCopy = Convert.ToInt16(cmbPrintCopy.Text);
               // aPrinterSettings.kitchenEmptyLine = Convert.ToInt16(comboBoxExtraLine.Text);
                aPrinterSettings.RestaurantId = GlobalSetting.RestaurantInformation.Id;
                aPrinterSettings.printerMargin = Convert.ToInt16(textBoxPrinterMargin.Text.Trim());
                aPrinterSettings.Status = "Active";

                PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                int id = aPrinterSetupBll.SavePrinter(aPrinterSettings);

                if (id > 0)
                {
                    ClearFiled();
                    MessageBox.Show("Printer has been save successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllPrinter();
                }

            }
            else MessageBox.Show("Please Check Input Field", "Printer Setup Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private PrinterSetup GetRecipe()
        {
            PrinterSetup aPrinter = new PrinterSetup();
            string recipesName = "";
            string recipeList = "";
            bool flag = false;
            foreach (CheckBox checkbox in typeFlowLayoutPanel.Controls.OfType<CheckBox>())
            {
                if (checkbox.Checked)
                {
                    if (flag)
                    {
                        recipeList += ",";
                        recipesName += ",";
                    }
                    recipeList += checkbox.Name;
                    recipesName += checkbox.Text;
                    flag = true;
                }
            }
            aPrinter.RecipeNames = recipesName;
            aPrinter.RecipeTypeList = recipeList;

            return aPrinter;

        }

        private void ClearFiled()
        {
            printerAddressTextBox.Text = "";

        }

        private void LoadAllPrinter()
        {
            PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();

            List<PrinterSetup> aPrinter = aPrinterSetupBll.GetTotalPrinterList();
            printerDataGridView.DataSource = aPrinter;
        }

        private bool ValidForm()
        {
            int n;
            if (printerAddressTextBox.Text.Trim().Length == 0)
            {
                return false;
            }
            else if (printerNamecomboBox.Text.Trim().Length == 0)
            {
                return false;
            }
            else if (cmbPrintCopy.Text.Trim().Length == 0)
            {
                return false;
            }
            else if (textBoxPrinterMargin.Text.Trim().Length == 0)
            {
                return false;
            }

            return true;
        }

        private void printCopyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void printerDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dataGridViewColumn = printerDataGridView.Columns["Id"];
            if (dataGridViewColumn != null)
                dataGridViewColumn.Visible = false;


            var dataGridViewColumn1 = printerDataGridView.Columns["RestaurantId"];
            if (dataGridViewColumn1 != null)
                dataGridViewColumn1.Visible = false;

            var dataGridViewColumn2 = printerDataGridView.Columns["RecipeTypeList"];
            if (dataGridViewColumn2 != null)
                dataGridViewColumn2.Visible = false;

        }

        private void printerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && printerDataGridView.Columns["Id"] != null)
            {
                int printerId = Convert.ToInt32("0" + printerDataGridView.Rows[e.RowIndex].Cells["Id"].Value); PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                //if (printerDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == printerDataGridView.Rows[e.RowIndex].Cells["update"])
                //{

                //    PrinterSetup printer = aPrinterSetupBll.GetPrinterByPrinterId(printerId);
                //    PrinterUpdateForm aPrinterUpdateForm = new PrinterUpdateForm(printer);
                //    aPrinterUpdateForm.ShowDialog();

                //    LoadAllPrinter();
                //}
                if (printerDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == printerDataGridView.Rows[e.RowIndex].Cells["delete"])
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to deleted it?", "Alert",
                                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        int id = aPrinterSetupBll.DeletePrinterByPrinterId(printerId);
                        LoadAllPrinter();

                    }
                    else if (dialogResult == DialogResult.No)
                    {


                    }
                }
            }
        }

        private void printStyleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (printStyleComboBox.Text == "Receipt")
            {

                textBoxPrinterMargin.Visible = false;
                label19.Visible = false;
                foreach (CheckBox checkbox in typeFlowLayoutPanel.Controls.OfType<CheckBox>())
                {
                    checkbox.Checked = true;


                }
                typeFlowLayoutPanel.Enabled = false;

            }
            else
            {
                textBoxPrinterMargin.Visible = true;
                label19.Visible = true;
              
                foreach (CheckBox checkbox in typeFlowLayoutPanel.Controls.OfType<CheckBox>())
                {
                    checkbox.Checked = false;

                }
                typeFlowLayoutPanel.Enabled = true;

            }
        }

        private void printerAddressTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 350);
                    NumberPad aNumberPad = new NumberPad(aPoint);
                    aNumberPad.ShowDialog();

                }

            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        public void fontInstall(string font)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(@"Config/font/");
                //Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.ttf");
                File.Copy(Files.FirstOrDefault(a => a.Name.Contains(font)).Name, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Fonts", Files.FirstOrDefault(a => a.Name.Contains(font)).Name));
            }
            catch (Exception)
            {


            }

        }
        public string DynamicUrlSetup()
        {
            //fontInstall(fontFamilyComboBox.Text);

            LicenceKey settings = GlobalSetting.SettingInformation;
            //if (cmbCardStatus.Text != "")
            //{
            //    settings.IsCardVisible = Convert.ToBoolean(cmbCardStatus.SelectedIndex);

            //}

            if (cmbTposVersion.Text != "")
            {
                settings.viewpage = cmbTposVersion.Text;
            }
            settings.onlineConnect = chkOnlineConnect.Checked ? "Active" : "Deactive";
            settings.till = chkTillButton.Checked ? "Enable" : "Disable";
            settings.IsReservationCheck = chkReservation.Checked;
            if (comboBoxCallerID.Text != "")
            {
                Properties.Settings.Default.callerID = comboBoxCallerID.Text;

                settings.IsCallerId = Convert.ToBoolean(comboBoxCallerID.SelectedIndex);
            }
            Properties.Settings.Default.showcouponsection = chkCouponSection.Checked ? true : false;
            Properties.Settings.Default.showkitichineSection = chkKitichine.Checked ? true : false;
            Properties.Settings.Default.requirdCustomTotal = chkCustomPrice.Checked ? true : false;
            Properties.Settings.Default.requirdDiscountPassword = chkDiscount.Checked ? true : false;
            Properties.Settings.Default.enableWebPrint = checkBoxWebPrint.Checked ? true : false;
            
            int IsUpdate = new GlobalUrlBLL().UpdateUrlsForSetting(settings);


            if (IsUpdate > 0)
            {
                return "Updated Setting";
            }
            return "Failed";
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {

                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                urls.AcceptUrl = onlineOrderAcceptTextBox.Text;
                urls.RejectUrl = onlineOrderRejectTextBox.Text;
                urls.OrderSyn = orderSyncronizeTextBox.Text;
                urls.fontSize = fontSizetexbox.Text; urls.fontStyle = fontStyleComboBox.Text;
                urls.fontFamily = fontFamilyComboBox.Text;
                urls.PrinterName = comboBoxCashDrawer.Text;
                urls.Cursur = cursurEnableCheckBox.Checked ? 1 : 0;
                urls.Keyboard = keyboardCheckBox.Checked ? 1 : 0;
                //Properties.Settings.Default.appLanguage = cmbCardStatus.Text;
                Properties.Settings.Default.enableDelcharge = checkDelCharge.Checked ? true : false;
                Properties.Settings.Default.callerID = comboBoxCallerID.Text;
                Properties.Settings.Default.autoPrint = textBoxAutoPrint.Text;
                Properties.Settings.Default.autoPrintLocation = txtTabLocation.Text;
                if (comboBoxKitchenExtraLine.Text != null)
                {
                    Properties.Settings.Default.kitchenEmptyLine = Convert.ToInt32(comboBoxKitchenExtraLine.Text);
                }
                Properties.Settings.Default.Save();

                 var type = GlobalSetting.RestaurantUsers.Usertype;
                if (type == "admin")
                {
                    DynamicUrlSetup();
                }


                if (urls.Id <= 0)
                {
                    int result = aGlobalUrlBll.InsertUrls(urls);
                    if (result > 0)
                    {
                        urls.Id = result;
                        MessageBox.Show("All data has been save succefully.", "Insertion Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        AllSettingsForm.Status = "reload";

                        var parentForm = this.ParentForm;


                        if (parentForm != null) parentForm.Close();
                    }
                    else
                        MessageBox.Show("Something wrong! Please try again  ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string result = aGlobalUrlBll.UpdateUrls(urls);
                    if (result == "Yes")
                    {
                        MessageBox.Show("All required url has been updated succefully", "Update Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AllSettingsForm.Status = "reload";
                        var parentForm = this.ParentForm;
                        if (parentForm != null) parentForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Something wrong! Please try again  ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }


        public void GetInstallFont()
        {

            DirectoryInfo d = new DirectoryInfo(@"Config/font/");
            //Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.ttf"); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {
                fontFamilyComboBox.Items.Add(file.Name);
            }
        }
        public Image stringToImage(string inputString)
        {
            byte[] imageBytes = Convert.FromBase64String(inputString);
            MemoryStream ms = new MemoryStream(imageBytes);

            Image image = Image.FromStream(ms, true, true);
            return image;
        }


        private void LoadAllUrl()
        {
            try
            {
                string image = "";
                string path = @"Image/" + GlobalSetting.RestaurantInformation.Id + "_website_logo.png";
                // GetInstallFont();
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    image = string.Format(Convert.ToBase64String(File.ReadAllBytes(path)));
                    pictureBox1.BackgroundImage = stringToImage(image);
                    pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          if (urls != null && urls.Id > 0)
            {
                onlineOrderAcceptTextBox.Text = urls.AcceptUrl;
                onlineOrderRejectTextBox.Text = urls.RejectUrl;
                orderSyncronizeTextBox.Text = urls.OrderSyn;
                fontSizetexbox.Text = urls.fontSize;
                lblFontSize.Text = urls.fontSize;
                fontStyleComboBox.Text = urls.fontStyle;
                fontFamilyComboBox.Text = urls.fontFamily;
                lblFontFamily.Text = urls.fontFamily;
                comboBoxCashDrawer.Text = urls.PrinterName;
                lblCashDrawer.Text = urls.PrinterName;
                cursurEnableCheckBox.Checked = urls.Cursur > 0;
                lblCursor.Text = urls.Cursur == 1 ? "Enable" : "Disable";
                keyboardCheckBox.Checked = urls.Keyboard > 0;
                lblKeyBoard.Text = urls.Keyboard == 1 ? "Enable" : "Disable";
                chkCouponSection.Checked = Properties.Settings.Default.showcouponsection;
                chkKitichine.Checked = Properties.Settings.Default.showkitichineSection;

            }
            LicenceKey settings = GlobalSetting.SettingInformation;
            chkCustomPrice.Checked = Properties.Settings.Default.requirdCustomTotal;
            lblCustomPrice.Text = Properties.Settings.Default.requirdCustomTotal ? "Enable" : "Disable";
            textBoxAutoPrint.Text = Properties.Settings.Default.autoPrint;
            lblAutoPrint.Text = Properties.Settings.Default.autoPrint;
            txtTabLocation.Text = Properties.Settings.Default.autoPrintLocation;
            chkTillButton.Checked = settings.till == "Enable" ? true : false;
            lblTillButton.Text = settings.till;

           // cmbCardStatus.Text = Properties.Settings.Default.appLanguage;

          //  lblOrderCart.Text = Properties.Settings.Default.appLanguage; //settings.IsCardVisible ? "Default" : "Arabic";

            chkReservation.Checked = settings.IsReservationCheck;
            lblReservation.Text = settings.IsReservationCheck ? "Enable" : "Disable";

          //  comboBoxCallerID.SelectedIndex = Convert.ToInt16(settings.IsCallerId);
            lblCallerId.Text = Properties.Settings.Default.callerID;
            comboBoxCallerID.Text = Properties.Settings.Default.callerID;

            chkOnlineConnect.Checked = settings.onlineConnect == "Active" ? true : false;
            lblOnlineOrder.Text = settings.onlineConnect == "Active" ? "Active" : "Deactive";

            cmbTposVersion.Text = settings.viewpage;
            lblViewPage.Text = settings.viewpage;

            checkBoxWebPrint.Checked = Properties.Settings.Default.enableWebPrint;
            checkDelCharge.Checked = Properties.Settings.Default.enableDelcharge;
            chkDiscount.Checked = Properties.Settings.Default.requirdDiscountPassword;
            comboBoxKitchenExtraLine.Text = Properties.Settings.Default.kitchenEmptyLine.ToString();

        }
        private void fontSizetexbox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(500, 150);
                    NumberForm aNumberPad = new NumberForm(aPoint);
                    aNumberPad.ShowDialog();

                }

            }
            catch
            {
            }
        }



        private void buttonUpdateDatabase_Click(object sender, EventArgs e)
        {


            DialogResult dialogResult = MessageBox.Show("Do you want to update database", "Update Database Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                AllSettingsForm.Status = "";
                processBar aProcessBar = new processBar("Please wait, database updating...", 1);
                aProcessBar.ShowDialog();

                if (AllSettingsForm.Status == "logout")
                {
                    try
                    {
                        StripeDetails stripeDetails = new StripeDetails();
                        MySqlRestaurantInformationDAO aRestaurantInformationDao = new MySqlRestaurantInformationDAO();
                        stripeDetails = aRestaurantInformationDao.GetStripeDetails();
                        if (stripeDetails.apiKey != "")
                        {
                            Properties.Settings.Default.StripeAPI = stripeDetails.apiKey;
                            Properties.Settings.Default.PublishAPI = stripeDetails.publishKey;
                            Properties.Settings.Default.accountId = stripeDetails.accNumber;
                            Properties.Settings.Default.feeType = stripeDetails.feeType;
                            Properties.Settings.Default.applicationFees = stripeDetails.accFee;
                            Properties.Settings.Default.Save();
                        }
                        bool distanceService = false;
                        distanceService = aRestaurantInformationDao.checkdistanceServiceStatus();
                        if (distanceService)
                        {
                            Properties.Settings.Default.enableDelcharge = true;
                            Properties.Settings.Default.Save();
                        }
                    }
                    catch (Exception ex) { }

                    var parentForm = this.ParentForm;
                    if (parentForm != null) parentForm.Close();
                }

            }
            //try
            //{
            //    DialogResult dialogResult = MessageBox.Show("Do you want to update local database", "Update Local  Database ", MessageBoxButtons.YesNo);
            //    if (dialogResult == DialogResult.Yes)
            //    {


            //        ProgressBar progressBar1 = new ProgressBar();
            //        DatabaseSyncBLL aDatabaseSyncBll = new DatabaseSyncBLL();
            //        string restaurantdata = aDatabaseSyncBll.GetAllRestaurantData();
            //        progressBar1.Value += 1000;
            //        if (restaurantdata != "Invaild Global Settings Web Address")
            //        {
            //            string tableSchema = aDatabaseSyncBll.GetSchema();
            //            progressBar1.Value += 500;
            //             progressBar1.Value += 500;
            //            label1.Text = "Database updating complete";
            //            progressBar1.Visible = false;
            //         //   MessageBox.Show(res);
            //            AllSettingsForm.Status = "logout";
            //            var parentForm = this.ParentForm;
            //            if (parentForm != null) parentForm.Close();
            //        }
            //        else
            //        {
            //            MessageBox.Show(restaurantdata);
            //        }

            //    }
            //    else if (dialogResult == DialogResult.No)
            //    {

            //    }
            //}

            //catch (Exception exception)
            //{
            //    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //    aErrorReportBll.SendErrorReport(exception.ToString());
            //}

        }

        private void syncOrderButton_Click(object sender, EventArgs e)
        {
            processBar1 processFrm = new processBar1();
            processFrm.ShowDialog();
        }

        private void buttonChangePrice_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("The feature are not available.");
            PriceChangeForm site = new PriceChangeForm();
            site.ShowDialog();
            //TomafoodSite site = new TomafoodSite();
            //site.ShowDialog();
        }

        private void settingInformationPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = @"Image/" + GlobalSetting.RestaurantInformation.Id + "_website_logo.png";


                FileInfo image = new FileInfo(path);

                if (image.Exists)
                {
                    image.Delete();

                }
                string url = GlobalVars.hostUrl + "images/restaurant/website/" + GlobalSetting.RestaurantInformation.Id + "_website_logo.png";
                using (WebClient webClient = new WebClient())
                {


                    byte[] data = webClient.DownloadData(url);
                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        using (var yourImage = Image.FromStream(mem))
                        {
                            yourImage.Save(path, ImageFormat.Png);
                            var imageFile = string.Format(Convert.ToBase64String(File.ReadAllBytes(path)));
                            pictureBox1.BackgroundImage = stringToImage(imageFile);
                            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

                            //pictureBox1.BackgroundImage = new Bitmap(@"Image/" + GlobalSetting.RestaurantInformation.Id + "_website_logo.png");
                            //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.GetBaseException().ToString());

            }
        }

        private void buttonUpdatePostcode_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to update database", "Update Database Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                AllSettingsForm.Status = "";
                processBar aProcessBar = new processBar("Please wait, Postcode updating...", 2);
                aProcessBar.ShowDialog();

                if (AllSettingsForm.Status == "logout")
                {
                    var parentForm = this.ParentForm;
                    if (parentForm != null) parentForm.Close();
                }
            }
        }
        private void buttonUpdatePostcode_Click_1(object sender, EventArgs e)
        {
             PostCodeBLL aPostCodeBll = new PostCodeBLL();
             aPostCodeBll.GetPostcodeByCovarageArea();
        }
        private void textBoxPrinterMargin_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 350);
                    NumberPad aNumberPad = new NumberPad(aPoint);
                    aNumberPad.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private async void btnSyncCustomer_Click(object sender, EventArgs e)
        {
            RestaurantUsers users = new RestaurantUsers();
            Sequrity.AutorizeForm form = new Sequrity.AutorizeForm(users);
            form.ShowDialog();

            if (form.user.Autorize)
            {
                await Task.Factory.StartNew(() =>
                {
                    DAL.CommonMethod.DataSync.SyncPendingCustomers();
                });
            } else
            {
                MessageBox.Show("Authentication Failed");
            }
                           
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }
    }
}
