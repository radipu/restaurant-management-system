using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;
using TomaFoodRestaurant.Sequrity; 
using System.Reflection;
using System.Resources;
using System.Globalization;

namespace TomaFoodRestaurant.NewLoginForm
{    
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            panel7.Hide();

            //Screen[] screens = Screen.AllScreens;
            //Rectangle bounds = screens[1].Bounds;
            //this.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            //this.StartPosition = FormStartPosition.Manual;          
        }

        private void windowsUIButtonPanel1_Click(object sender, EventArgs e)
        {
            //panel7.Show();
            if (GlobalSetting.IsCustomerAdd)
            {
                MessageBox.Show("Customer updating process has been working. Please wait some moments.");
                return;
            }
            OthersMethod aOthersMethod = new OthersMethod();
            aOthersMethod.CallerIDClose();
            Application.Exit();
        }

        public void LoadAllProgram()
        {
            //set version
            labelVersion.Text = "Version " + GlobalVars.sVersion;
            int valOpen = Properties.Settings.Default.countOpenTime;
            string COMPUTERNAME = System.Environment.GetEnvironmentVariable("COMPUTERNAME"); 

            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            UserLoginBLL aUserLoginBLL = new UserLoginBLL();
            this.AcceptButton = logainButton;

            try
            {
                List<RestaurantUsers> aRestaurantUserses = new List<RestaurantUsers>();
                aRestaurantUserses = aUserLoginBLL.GetRestaurantUserByRestaurantId(restaurantInformation.Id);

                int wt = 0;
                int ht = 0;
                int inc = 1;
                foreach (RestaurantUsers user in aRestaurantUserses)
                {
                    TileItem userButton = new TileItem();
                    userButton.ItemPress += new TileItemClickEventHandler(loginUserButton_click);
                    userButton.ItemSize = TileItemSize.Wide;
                    userButton.Image = Properties.Resources.user; userButton.ImageScaleMode = TileItemImageScaleMode.NoScale;
                    userButton.ImageAlignment = TileItemContentAlignment.TopCenter;
                    userButton.TextAlignment = TileItemContentAlignment.BottomCenter;
                    userButton.Appearance.BackColor = Color.FromArgb(4, 73, 83);
                    userButton.Appearance.BorderColor = Color.White;
                    userButton.Text = user.Username.ToUpper();
                    userButton.AppearanceItem.Normal.Font = new System.Drawing.Font("Segoe UI", 12);
                    tileGroup2.Items.Add(userButton);
                }         
                userNameTextBox.Text = Properties.Settings.Default.userPreName;
                passwordTextBox.Text = Properties.Settings.Default.userPrePassword;

                string executableName = Application.ExecutablePath;
                FileInfo executableFileInfo = new FileInfo(executableName);
                string executableDirectoryName = executableFileInfo.DirectoryName;
                string curFile = @"" + executableDirectoryName + "\\TomaFoodRestaurant_pre.exe";
                if(File.Exists(curFile))
                {
                    System.IO.File.Delete(@"" + executableDirectoryName + "\\TomaFoodRestaurant_pre.exe");
                }
            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void loginUserButton_click(object sen24, TileItemEventArgs e)
        {
            TileItem aButton = sen24 as TileItem;
            userNameTextBox.Text = aButton.Text;
            passwordTextBox.Text = "";
        }

        private void FullScreenLogin_Load(object sender, EventArgs e)
        {
            LoadAllProgram();
            try
            {
                //ResourceManager rm = new ResourceManager("TomaFoodRestaurant.Language.ar", Assembly.GetExecutingAssembly());
                //String strName = rm.GetString("Currency");
                //String strWebsite = rm.GetString("AppTitle.Text", CultureInfo.CurrentCulture);
            }
            catch(Exception ex)
            {

            }
        }

        private bool VaildForm()
        {
            if (userNameTextBox.Text.Trim().Length == 0) return false;
            if (passwordTextBox.Text.Trim().Length == 0) return false;
            return true;
        }

        private void loginNumberButton_click(object sen24, EventArgs e)
        {

        }

        private void logainButton_Click(object sender, EventArgs e)
        {
            try
            {                 
                splashScreenManager1.ShowWaitForm();
                int limit = Properties.Settings.Default.openLimit;
                bool internetConnection = true;
                string str = File.ReadAllText("Config/call.txt");
                string predate = DateTime.Today.AddDays(-1).ToShortDateString();
                if (str != "" && str.Contains(predate))
                 {
                    File.WriteAllText("Config/call.txt", "");
                 }
                string COMPUTERNAME = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
              
                UserLoginBLL aUserLoginBll = new UserLoginBLL();
                LicenceKeyBLL aLicenceKeyBll = new LicenceKeyBLL();
                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                SoftwareActivationBLL aSoftwareActivationBll = new SoftwareActivationBLL();
                
                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                GlobalUrl gUrl = new GlobalUrl();
                gUrl = aGlobalUrlBll.GetUrls();
                gUrl.AcceptUrl = Properties.Settings.Default.backend;

                if (Properties.Settings.Default.countOpenTime > (limit + 7))
                {
                    internetConnection = OthersMethod.CheckForInternetConnection();
                 }
                HardwareInforamtion aHardwareInforamtion = aSoftwareActivationBll.GetHardwareInforamtion();              
                string key = "ID#" + aHardwareInforamtion.ProcessorSerial.Trim() + "_HDD#" + aHardwareInforamtion.HardDriveSerialNo.Trim();
                //+ COMPUTERNAME.Trim();
                key = Regex.Replace(key, "[^0-9a-zA-Z#_-]+", "");
                //  MessageBox.Show(key);
                string encriptLicenceKey = aSoftwareActivationBll.EncryptData(key);
                string myPCAddress1 = "ID#BFEBFBFF000306A9_HDD#WD-WXA1AA61SK6212_NAME#TUHIN";
                string myPCAddress = "ID#BFEBFBFF000306C3_HDD#WD-WCC4J2948136_NAME#DESK";
                LicenceKey restaurantLicence = aLicenceKeyBll.GetRestaurantLicence(restaurantInformation.Id, key);
                if ((restaurantLicence == null || restaurantLicence.restaurant_id < 1) && myPCAddress != key && myPCAddress1 != key)
                {
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("Unknown device.Please contact Ginilab to register this device.", "Security Problem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if(OthersMethod.CheckForInternetConnection())
                    {
                        CheckSoftwareActivation frmSoftwareActivation = new CheckSoftwareActivation();
                        frmSoftwareActivation.Show();
                        Properties.Settings.Default.countOpenTime = 0;
                        Properties.Settings.Default.Save();
                        this.Hide();
                        return;
                    }
                    else
                    {
                        CheckSoftwareActivationOfOfflineForm offFrm = new CheckSoftwareActivationOfOfflineForm();
                        offFrm.Show();
                        Properties.Settings.Default.countOpenTime = 0;
                        Properties.Settings.Default.Save();
                        this.Hide();
                        return;
                    }
                }
               
                if (VaildForm())
                {
                    //PrintToPrinterBLL printToPrinterBLL = new PrintToPrinterBLL();
                    //bool isPrinterError = printToPrinterBLL.CheckPrinterStatus();
                    //if (isPrinterError)
                    //{
                    //    MessageBox.Show("Printer Error. Please go to Printer Settings and check.");
                    //} 

                    string sha = GetSha1(passwordTextBox.Text).ToLower();

                    if (OthersMethod.CheckForInternetConnection())
                    {
                        try
                        {
                            string result_ = aSoftwareActivationBll.onlineLogin(userNameTextBox.Text, sha,
                                restaurantInformation.Id, gUrl.AcceptUrl);
                            if (Convert.ToInt32(result_) > 0)
                            {
                                aSoftwareActivationBll.updateUser(result_, sha);
                            }
                        }
                        catch (Exception exeException)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exeException.ToString());
                        }
                    }

                    RestaurantUsers user = aUserLoginBll.GetRestaurantUserByUsernameAndPassword(userNameTextBox.Text, sha);

                    if (user != null && user.Id > 0)
                    {
                        GlobalSetting.UserId = user.Id;
                        GlobalSetting.UserName = user.Username;
                        GlobalSetting.RestaurantUsers = user;
                        GlobalSetting.SettingInformation = restaurantLicence; 

                        if (keepCheckBox.Checked)
                        {
                            Properties.Settings.Default.userPreName = userNameTextBox.Text;
                            Properties.Settings.Default.userPrePassword = passwordTextBox.Text;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            userNameTextBox.Text = Properties.Settings.Default.userPreName;
                            passwordTextBox.Text = Properties.Settings.Default.userPrePassword;
                        }

                        Console.WriteLine("internetConnection " + internetConnection.ToString());
                        if (internetConnection)  
                        {
                            try
                            {
                                Properties.Settings.Default.countOpenTime = 0;
                                Properties.Settings.Default.Save();
                                var verstion = restaurantLicence.viewpage;
                                if (verstion == "Responsive")
                                {
                                    MainFormView aMainForm = new MainFormView();
                                    aMainForm.Show();                           
                                }
                                else
                                {
                                    mainForm aMainForm = new mainForm(); 
                                    aMainForm.Show();
                                }
                                splashScreenManager1.CloseWaitForm();
                                FormManage.aFormManage.Push(this);
                                this.Hide();
                            }
                            catch (Exception ex)
                            {
                                splashScreenManager1.CloseWaitForm();
                                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                aErrorReportBll.SendErrorReport(ex.ToString());
                                MessageBox.Show("Activating Software!", "Software Activation", MessageBoxButtons.OK,MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            Properties.Settings.Default.countOpenTime = Properties.Settings.Default.countOpenTime + 1;
                            Properties.Settings.Default.Save();
                            var version = restaurantLicence.viewpage;
                            if (version == "Responsive")
                            {
                                MainFormView aMainForm = new MainFormView();
                                aMainForm.Show();
                            }
                            else
                            {
                                mainForm aMainForm = new mainForm();
                                aMainForm.Show();
                            }

                            splashScreenManager1.CloseWaitForm();
                            FormManage.aFormManage.Push(this);
                            this.Hide();
                        }
                    }
                    else
                    {
                        splashScreenManager1.CloseWaitForm();
                        MessageBox.Show("Please enter  correct username & password.", "Login Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show("PLease enter username & password.", "Login Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                if (ex.Message == "Column 'IsonlineOrderCheck' does not belong to table rcs_restaurant_license.")
                {
                    MessageBox.Show("Need to restart your computer to start this application.Please restart and start TPOS again."," SYSTEM WARNING ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }
            }
        }

        public string GetSha1(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var hashData = new SHA1Managed().ComputeHash(data);
            var hash = string.Empty;

            foreach (var b in hashData)
                hash += b.ToString("X2");
            return hash;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (GlobalSetting.IsCustomerAdd)
            {
                MessageBox.Show("Customer updating process has been working. Please wait some moments.");
                return;
            }
            OthersMethod aOthersMethod = new OthersMethod();
            aOthersMethod.CallerIDClose();
            Application.Exit();
        }

        private void keepCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }     

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.Control && e.KeyCode == Keys.Q && e.Alt)
            {
                QueryExecuteForm queryExecuteForm = new QueryExecuteForm();
                queryExecuteForm.ShowDialog();
            }
            if (e.Control && e.KeyCode == Keys.D && e.Alt)
            {
                DBsetup queryExecuteForm = new DBsetup(this);
                queryExecuteForm.ShowDialog();
            }}

        private void userNameTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = userNameTextBox;
        }

        private void passwordTextBox_Click(object sender, EventArgs e)
        {
            numberPadUs1.ControlToInputText = passwordTextBox;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            panel7.Hide();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            try
            {
                UserLoginBLL aUserLoginBll = new UserLoginBLL();
                string sha = GetSha1(passwordTextBoxExit.Text).ToLower();
                RestaurantUsers user = aUserLoginBll.GetRestaurantUserByPassword(sha);
                if (user != null && user.Id > 0)
                {
                    Application.Exit();
                }

                else
                {
                    MessageBox.Show("Please enter  correct password.", "Login Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }
        }

        private void passwordTextBoxExit_Click(object sender, EventArgs e)
        {
            this.AcceptButton = confirmButton;
            numberPadUs1.ControlToInputText =passwordTextBoxExit;
        }
    }
}