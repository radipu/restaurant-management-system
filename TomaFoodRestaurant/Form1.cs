using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using FastFoodManagementSystem.BLL;
using Microsoft.PointOfService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;
using AutoSizeMode = System.Windows.Forms.AutoSizeMode;
using Timer = System.Windows.Forms.Timer;
using DevExpress.Data.PLinq.Helpers;
using TomaFoodRestaurant.Sequrity;
using ContentAlignment = System.Drawing.ContentAlignment;
using PriceCalculation = TomaFoodRestaurant.DAL.CommonMethod.PriceCalculation;
using TomaFoodRestaurant.DAL.CommonMethod;
using TomaFoodRestaurant.DAL.DAO;
using System.Timers;
using System.Reflection;
using System.Resources;
using System.ComponentModel;
using System.Threading;

namespace TomaFoodRestaurant
{
    public partial class mainForm : Form
    {
        int screenHeight;
        int screenWidth;
        int recipeTypeLine = 1;

        List<CustomFlowLayoutPanel> aFlowLayoutlist = new List<CustomFlowLayoutPanel>();
        List<CustomFlowLayoutPanel> rightFlowLayoutlist = new List<CustomFlowLayoutPanel>();
        public List<RecipeOptionMD> aRecipeOptionMdList = new List<RecipeOptionMD>();
        public static List<OrderItemDetailsMD> aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
        public static List<RecipePackageMD> aRecipePackageMdList = new List<RecipePackageMD>();
        public static List<PackageItem> aPackageItemMdList = new List<PackageItem>();
        public GeneralInformation aGeneralInformation = new GeneralInformation();
        object previousControl = null;
        int hasSubcategoryId = 0;
        int leftPanelSize = 0;
        public List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
        PrintCopySetup aPrintCopySetup = new PrintCopySetup();
        public RestaurantInformation aRestaurantInformation = new RestaurantInformation();
        List<RestaurantUsers> aRestaurantUserForSearchCustomer = new List<RestaurantUsers>();
        List<SearchUserCustome> aSearchUserCustom = new List<SearchUserCustome>();
        List<ReceipeCategoryButton> allCategoryButton = new List<ReceipeCategoryButton>();
        GlobalUrl urls = new GlobalUrl();

        CashDrawer myCashDrawer;
        PosExplorer explorer;
        PosPrinter _oposPrinter;

        System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Ring/audio.wav");
        System.Media.SoundPlayer resPlayer = new System.Media.SoundPlayer(@"Ring/resAudio.wav");
        List<RecipeTypeDetails> aRecipeTypes = new List<RecipeTypeDetails>();
        List<ReceipeTypeButton> allRecipeType = new List<ReceipeTypeButton>();
        DeviceInfo _device;
        string LDN=null;
        bool orderLoadStatus = true;
        List<ReceipeSubCategoryButton> allSubcategoryButton = new List<ReceipeSubCategoryButton>();
        List<ReceipeMenuItemButton> allRecipeButton = new List<ReceipeMenuItemButton>();
        List<CustomiseTabPage> aCustomizeTabPageList = new List<CustomiseTabPage>();
        List<PackageItem> definedPackageItem = new List<PackageItem>();
        OthersMethod aOthersMethod = new OthersMethod();

        public static List<RecipeMultipleMD> aRecipeMultipleMdList = new List<RecipeMultipleMD>();
        public static List<MultipleItemMD> aMultipleItemMdList = new List<MultipleItemMD>();
        public RestaurantUsers restaurantUsers = new RestaurantUsers();

        bool isCustomerTextChanged = true;
        int OrderNo = 0;
             
        private int menuTabControlHeight = 0;
        bool lastCategory = false;
      
        bool isMultiplePart = false;
        string font_size = "16";

        Timer grapTimer = new Timer();
        public System.Timers.Timer aTimer;
        System.Timers.Timer resTimer;
        System.Timers.Timer blinkBtnTimer;
        System.Timers.Timer autoPrintTimer;
        Task orderSync;
        Task taskTableStatus;
        public static ResourceManager rm = new ResourceManager("TomaFoodRestaurant.lang_ar", Assembly.GetExecutingAssembly());

        public mainForm()
        {
            InitializeComponent();
            IntialStartElement();
        }

        private void IntialStartElement()
        {
            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;
            orderDetailsflowLayoutPanel1.VerticalScroll.Visible = false;
            int rightPanelWidth = rightPanel.Width;
            leftPanelSize = screenWidth - rightPanelWidth - 70;

            var recentItemLoacation = receipeTypeFlowLayoutPanel.Location;
            orderDetailsflowLayoutPanel1.Size = new Size(orderDetailsflowLayoutPanel1.Size.Width, (int)(screenHeight * 0.60));

            customPanel.Location = new Point(customPanel.Location.X,
               orderDetailsflowLayoutPanel1.Location.Y + orderDetailsflowLayoutPanel1.Size.Height);
            paymentDetailsPanel.Location = new Point(paymentDetailsPanel.Location.X,
                customPanel.Location.Y + customPanel.Size.Height);

            this.Height = screenHeight;
            menuTabControl.Size = new Size(screenWidth - rightPanelWidth - 5, screenHeight - headerPanel.Height); 

           receipeTypeFlowLayoutPanel.AutoSize = false;
  
            orderDetailsflowLayoutPanel1.VerticalScroll.Enabled = false;
        }

        /// <summary>
        ///  Incoming Order from Online Servert
        /// </summary>
        /// <param name="No Parameter"></param> <returns>No Reurns</returns>

        private async void onlineOrderSync()
        {
          
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            //  RealTimeData realTimeData = new RealTimeData();
              await Task.Run(() => OnlineOrder.fireBaseOrderAsync(aRestaurantInformation.Url));
            // realTimeData.OpenSSEStream("https://" + aRestaurantInformation.Website + "/restaurants/check_online_order/" + aRestaurantInformation.Id, null, this));
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            //    aOthersMethod.NumberPadClose();
            //    aOthersMethod.KeyBoardClose(); 
        }

        private void MainForm_KeyPress(object sender, EventArgs e)
        {
            //aOthersMethod.NumberPadClose();
            //aOthersMethod.KeyBoardClose();
        }

        /// <summary>
        ///  Update RestaurantLicense
        /// </summary>
        /// <param name="No Parameter"></param>
        /// <returns>No Reurns</returns>
        
        private void UpdateRestaurantLicense()
        {
            try
            {
                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                RestaurantSync aRestaurantSync = aRestaurantInformationBll.GetRestaurantSyncInformation();
                if (aRestaurantSync != null && aRestaurantSync.id > 0)
                {
                    aRestaurantInformationBll.UpdateRestaurantLicense(aRestaurantSync);
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        /// <summary>
        ///  OrderSyncronize Client To  Server
        /// </summary>
        /// <param name="No Parameter"></param>
        /// <returns>No Reurns</returns>

        private async void OrderSyncronize()
        {
            try
            {
                //await orderSync.Run(() => aOrderSyncroniseBll.OrderSyncronise("all"));
                OrderSyncroniseBLL aOrderSyncroniseBll = new OrderSyncroniseBLL();
                await Task.Factory.StartNew(() =>
                {
                    aOrderSyncroniseBll.OrderSyncronise("all");
                });
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

        }

        #region Tab Design

        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();

        private void SetTabHeader(TabPage page, Color color)
        {
            TabColors[page] = color;
            menuTabControl.Invalidate();
        }

        #endregion

        #region comment text and custom total placeholder

        private void commentTextBox_Enter(object sender, EventArgs e)
        {
            if (commentTextBox.Text == "Comment")
            {
                commentTextBox.Text = "";
                commentTextBox.ForeColor = Color.Black;
            }
        }

        private void commentTextBox_Leave(object sender, EventArgs e)
        {
            if (commentTextBox.Text == "")
            {
                commentTextBox.Text = "Comment";
                commentTextBox.ForeColor = Color.SlateGray;
            }
        }

        private void customTotalTextBox_Enter(object sender, EventArgs e)
        {
            if (customTotalTextBox.Text == "Custom Total")
            {
                customTotalTextBox.Text = "";
                customTotalTextBox.ForeColor = Color.Red;
            }
        }

        private void customTotalTextBox_Leave(object sender, EventArgs e)
        {
            double customTotal = 0;
            if (customTotalTextBox.Text == "")
            {
                customTotalTextBox.Text = "Custom Total";
                customTotalTextBox.ForeColor = Color.SlateGray;
            }
            if (customTotalTextBox.Text != "Custom Total" && Double.TryParse(customTotalTextBox.Text.Trim(), out customTotal))
            {
                if (Properties.Settings.Default.requirdCustomTotal)
                {
                    RestaurantUsers users = new RestaurantUsers();
                    AutorizeForm form = new AutorizeForm(users);
                    form.ShowDialog();

                    if (form.user.Autorize)
                    {
                        if (customTotalTextBox.Text == string.Empty)
                        {
                            customTotalTextBox.Text = "Custom Total";
                            customTotalTextBox.ForeColor = Color.SlateGray;
                        }
                        else
                        {
                            double price = Convert.ToDouble(customTotalTextBox.Text);
                            customTotalTextBox.Text = price.ToString("F02");
                            customTotalTextBox.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        customTotalTextBox.Text = "Custom Total";
                        customTotalTextBox.ForeColor = Color.SlateGray;
                        panel1.Focus();
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to overwrite the total cost?", "Total Cost",
                        MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        double price = Convert.ToDouble(customTotalTextBox.Text);
                        customTotalTextBox.Text = price.ToString("F02");
                        customTotalTextBox.ForeColor = Color.Red;
                        //do something
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        customTotalTextBox.Text = "Custom Total";
                        customTotalTextBox.ForeColor = Color.SlateGray;
                    }
                }
                //*********************************************
            }
        }

        #endregion

        #region Cash Drawer Open

        public void OpenCashDrawer()
        {
            try
            {
                if (GlobalSetting.SettingInformation.till == "Enable")
                {
                    RawPrinterHelper aRawPrinterHelper = new RawPrinterHelper();
                    aRawPrinterHelper.openCashDrawer();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Please check till configuration.", "Till Open Error", MessageBoxButtons.OK,MessageBoxIcon.Warning);
             }
        }

        #endregion
        
        /// <summary>
        ///  Load Form Intial when Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
     
        private TomaFoodRestaurant.OtherForm.Form1 aAdForm = null;
        private TomaFoodRestaurant.OtherForm.frmMain aAdFrmMain = null;
        private FrmCidv2 afrmCidv2 = null;

        private void mainForm_Load(object sender, EventArgs e)
        {
            StartScreen aStartScreen = new StartScreen("Loading");
            try
            {
                aStartScreen.Show();

                foreach (System.Windows.Forms.Control cont1 in this.Controls)
                {
                    foreach (System.Windows.Forms.Control cont in cont1.Controls)
                    {
                        cont.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyPress);
                    }
                }
                foreach (System.Windows.Forms.Control cont1 in this.Controls)
                {
                    foreach (System.Windows.Forms.Control cont in cont1.Controls)
                    {
                        cont.Click += this.MainForm_Click;
                    }
                }
                RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
              
                if (Properties.Settings.Default.callerID == "CIDEASY")
                {
                    try
                    {
                        // used for CIDEASY caller id acivation                                              
                        if (!Application.OpenForms.OfType<OtherForm.FrmCidv2>().Any())
                        {
                            File.AppendAllText("Config/log.txt", "CallerID CIDEASY : Activated. \n\n");
                            afrmCidv2 = new OtherForm.FrmCidv2(this);
                            afrmCidv2.Show();
                            afrmCidv2.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText("Config/log.txt", "CallerID CIDEASY v2 : " + ex.Message.ToString() + "\n\n");
                    }
                }

                if (Properties.Settings.Default.callerID == "AD101")
                {
                    try
                    {    
                      if(!Application.OpenForms.OfType<TomaFoodRestaurant.OtherForm.Form1>().Any())
                        {
                            aAdForm = new TomaFoodRestaurant.OtherForm.Form1(null, this);
                            aAdForm.WindowState = FormWindowState.Minimized;
                            aAdForm.Size = new System.Drawing.Size(1024, 700);
                            aAdForm.Show();
                            aAdForm.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText("Config/log.txt", "CallerID AD101 : " + ex.Message.ToString() + "\n\n");
                    }
                }

                try
                {
                     ClearAllFlowLayout();
                     explorer = new PosExplorer(this);
                     DeviceInfo ObjDevicesInfo = explorer.GetDevice("CashDrawer");
                     myCashDrawer = (CashDrawer)explorer.CreateInstance(ObjDevicesInfo);
                     explorer = new PosExplorer();
                     _device = explorer.GetDevice(DeviceType.PosPrinter, LDN);
                     _oposPrinter = (PosPrinter)explorer.CreateInstance(_device);

                 }
                 catch (Exception ex)
                 {
                      //new ErrorReportBLL().SendErrorReport(ex.GetBaseException().Message);        
                 }

                if(GlobalSetting.SettingInformation.till == "Enable")
                {
                    tillOpenButton.Visible = true;
                }

                GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
                urls = aGlobalUrlBll.GetUrls();
               
                if (OthersMethod.CheckForInternetConnection())
                {
                    RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                    RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                    if (restaurantInformation.IsSyncOrder > 0)
                    {
                        OrderSyncronize();
                    }
                    if (GlobalSetting.IsLicenseUpdate)
                    {
                        UpdateRestaurantLicense();
                        GlobalSetting.IsLicenseUpdate = false;
                    }
                    onlineOrderButton.Visible = false;
                    if (GlobalSetting.SettingInformation.onlineConnect == "Active")
                    {
                        onlineOrderButton.Visible = true;
                        aTimer = new System.Timers.Timer(10000);
                        aTimer.Elapsed += OnlineOrderTimer_Tick;
                        aTimer.Enabled = true;

                        // blink the button                        
                        blinkBtnTimer = new System.Timers.Timer(100);                        
                        blinkBtnTimer.Elapsed += new ElapsedEventHandler(blinkButtonEvent);
                        blinkBtnTimer.Enabled = false;
                        //set manual online order checking to true
                        GlobalVars.isOnlineOrderTimer = true;
                        //realtime online order checking
                        onlineOrderSync();
                    }
                    if (GlobalSetting.SettingInformation.IsReservationCheck && restaurantInformation.RestaurantType.ToLower() == "restaurant")
                    {
                        GetAllReservation(restaurantInformation.Id);
                        resTimer = new System.Timers.Timer(10 * 60000);
                        //resTimer = new System.Timers.Timer(1000);
                        resTimer.Enabled = true;
                        resTimer.Elapsed += timerReservation_Tick;
                    }
                }

                aRestaurantInformation = new RestaurantInformationBLL().GetRestaurantInformation();
                GlobalSetting.RestaurantInformation = aRestaurantInformation;
                reataurantNameLabel.Text = aRestaurantInformation.RestaurantName;
                allCategoryButton = aRestaurantMenuBll.GetAllCategory();
                GetAllItemData();
                allRecipeButton = aRestaurantMenuBll.AllRecipeButton();                
                LoadAllPrinter();
                LoadExtraPrice();
                LoadMenuType(); 
                LoadDefaultInformation();             
                                                                      
                aStartScreen.Visible = false;
                aStartScreen.Dispose();

                if (urls.Cursur == 0)
                {
                    Cursor.Hide();
                }
                //menuTabControl.Location = new Point(0, headerPanel.Height + receipeTypeFlowLayoutPanel.Height - (22 * recipeTypeLine));
                receipeTypeFlowLayoutPanel.MinimumSize = new Size(200, receipeTypeFlowLayoutPanel.Height + 6);

                if (menuTabControl.Width  < (receipeTypeFlowLayoutPanel.Width + packageTopFlowLayoutPanel.Width))
                {
                   receipeTypeFlowLayoutPanel.Height = packageTopFlowLayoutPanel.Height = receipeTypeFlowLayoutPanel.Height ;
                }
               
                receipeTypeFlowLayoutPanel.Width = menuTabControl.Width - packageTopFlowLayoutPanel.Width;
                menuTabControl.Location = new Point(0, receipeTypeFlowLayoutPanel.Height);
                receipeTypeFlowLayoutPanel.BringToFront();
                
                if (packageTopFlowLayoutPanel.Width > 100)
                {
                    packageTopFlowLayoutPanel.BringToFront();
                    packageTopFlowLayoutPanel.Visible = true;
                }
                
                if (aRestaurantInformation.MultiplePart > 0)
                {
                    multiplePartPanel.Visible = true;
                    LoadMultiplePart();
                }
                else
                {
                    multiplePartPanel.Visible = false;
                } 

                if(GlobalSetting.RestaurantInformation.IsSyncCustomer > 0)
                {
                    DateTime currentTime = DateTime.Now;
                    DateTime reportClosingTime = DateTime.Today;
                 
                    if (GlobalSetting.RestaurantInformation.ReportClosingHour > 12)
                    {
                        reportClosingTime = reportClosingTime.AddHours(-(24 - GlobalSetting.RestaurantInformation.ReportClosingHour));
                        reportClosingTime = reportClosingTime.AddMinutes(GlobalSetting.RestaurantInformation.ReportClosingMin);
                    }
                    else
                    {
                        reportClosingTime = reportClosingTime.AddHours(GlobalSetting.RestaurantInformation.ReportClosingHour);
                        reportClosingTime = reportClosingTime.AddMinutes(GlobalSetting.RestaurantInformation.ReportClosingMin);
                    }

                    if (currentTime > reportClosingTime)
                    {
                        RestaurantOrderBLL aRestaurantOrderBll = new RestaurantOrderBLL();
                        string result = aRestaurantOrderBll.DeleteAllOrderByDate(reportClosingTime);
                    }
                    else
                    {
                        RestaurantOrderBLL aRestaurantOrderBll = new RestaurantOrderBLL();
                        string result = aRestaurantOrderBll.DeleteAllOrderByDate(reportClosingTime.AddDays(-1));
                    }
                }
                if(Properties.Settings.Default.autoPrint.Trim() != "")
                {
                    //backgroundWorkerForAutoPrint.RunWorkerAsync(2000);
                    autoPrintTimer = new System.Timers.Timer(2000);
                    autoPrintTimer.Elapsed += new ElapsedEventHandler(AutoPrintOrders);
                    autoPrintTimer.Enabled = true;
                }

                //reset cart on form load
                resetCart();
                
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                aStartScreen.Visible = false;
                MessageBox.Show("Please check db configure");
                this.Activate();
            }

            if (GlobalSetting.RestaurantInformation.IsServiceCharge <= 0)
            {
                serviceChargeLabel.Visible = false;
            }
            //menuTabControl.AutoScroll = true;
            //menuTabControl.VerticalScroll.Enabled = false;
            //menuTabControl.VerticalScroll.Visible = false;
        }

        private void LoadMultiplePart()
        {
            int wi = receipeTypeFlowLayoutPanel.Width; 
            multiplePartPanel.Location = new Point(wi - 42, ((headerPanel.Height * recipeTypeLine) + 5));
            multiplePartPanel.BringToFront();
            multiplePartPanel.Visible = true;
        }

        #region Load All Required Data

        /// <summary>
        /// Get All ReceipeType Data List
        /// 
        /// </summary>
        /// <para>
        /// no parameter
        /// </para>
        /// <returns>
        /// no values
        /// </returns>
        
        private void GetAllItemData()
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            allSubcategoryButton = aRestaurantMenuBll.GetAllSubcategory();
            allRecipeType = aRestaurantMenuBll.GetRecipeType();
        }

        /// <summary>
        ///    - Get Print Copy  (How many printcopy print) 
        /// </summary>
        
        private void LoadPrintCopy()
        {
            try
            {
                PrintCopySetupBLL aPrintCopySetupBll = new PrintCopySetupBLL();
                aPrintCopySetup = aPrintCopySetupBll.GetPrintCopy();
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        /// <summary>
        /// Load All Printer From DB 
        /// </summary>
        
        private void LoadAllPrinter()
        {
            try
            {
                PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                PrinterSetups = aPrinterSetupBll.GetTotalPrinterList();
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        #endregion

        /// <summary>
        /// Load All Display Top Package
        /// </summary>
        /// <param name="ReceipeTypeButton"></param>
        
        private void LoadPackage(ReceipeTypeButton aReceipeTypeButton)
        {

            PackageBLL aPackageBll = new PackageBLL();
            List<RecipePackageButton> aRecipePackageButtons = aPackageBll.GetPackageByRecipeType(aReceipeTypeButton);
            
            bool topPac = false;
            packageTopFlowLayoutPanel.Width = 0;
            foreach (RecipePackageButton aRecipePackageButton in aRecipePackageButtons)
            {
                aRecipePackageButton.Click += new EventHandler(RecipePackageButton_Click);
                if (aRecipePackageButton.DisplayTop == 1)
                {
                    packageTopFlowLayoutPanel.Controls.Add(aRecipePackageButton);
                    topPac = true;
                    //packageTopFlowLayoutPanel.BackColor = Color.BlueViolet;                    
                }
            }
            int width = aRecipePackageButtons.Where(a=>a.DisplayTop==1).Sum(a => a.Width);
            packageTopFlowLayoutPanel.Width = width;
            //packageTopFlowLayoutPanel.BackColor = Color.Blue;
            packageTopFlowLayoutPanel.Location = new Point(menuTabControl.Width-packageTopFlowLayoutPanel.Width, 50);
            //packageTopFlowLayoutPanel.Dock = DockStyle.Right;
            //receipeTypeFlowLayoutPanel.Controls.Add(packageTopFlowLayoutPanel);
            subCategoryMenuItemFlowLayoutPanel.Location = new Point(hasSubCategoryFlowLayoutPanel.Location.X + hasSubCategoryFlowLayoutPanel.Size.Width,
                    subCategoryMenuItemFlowLayoutPanel.Location.Y);
        }

        #region   Predefined Package Check

        /// <summary>
        /// Load Package without SubCategory
        /// 
        /// </summary>
        /// <param name="PackageCategoryButton"></param>
        
        private void LoadPackageItemWithoytSubCategory(PackageCategoryButton category)
        {
            PackageBLL aPackageBll = new PackageBLL();
            List<PackageItemButton> aPackageItemButtonList = aPackageBll.GetPackageItemWithoytSubCategory(category);

            foreach (PackageItemButton itemButton in aPackageItemButtonList)
            {
                PackageItem aItem = new PackageItem();
                aItem.ItemId = itemButton.RecipeId;
                aItem.ItemName = itemButton.ReciptName;
                aItem.Price = itemButton.AddPrice;
                aItem.Qty = itemButton.CountPackageItem;
                aItem.OptionName = itemButton.OptionName;
                aItem.PackageId = itemButton.PackageId;
                aItem.CategoryId = itemButton.CategoryId;
                aItem.SubcategoryId = itemButton.SubCategoryId;
                definedPackageItem.Add(aItem);
            }
        }

        /// <summary>
        /// Load PackageCategory </summary>
        /// <param name="aRecipePackageButton"></param>
        
        private void LoadPackageCategory(RecipePackageButton aRecipePackageButton)
        {
            PackageBLL aPackageBll = new PackageBLL();
            List<PackageCategoryButton> tempList = aPackageBll.GetPackageCategoryWhereNoOption(aRecipePackageButton);
            foreach (PackageCategoryButton package in tempList)
            {
                LoadDetails(package);
            }
            if (definedPackageItem.Any())
            {
                PackageItemForm.status = "ok";
                PackageItemForm.aPackageItemList = definedPackageItem;
            }
        }

        /// <summary>
        /// LoadPackageItemWithoytSubCategory information and LoadAllSubCategory
        /// 
        /// </summary>
        /// <param name="category"></param>
        
        private void LoadDetails(PackageCategoryButton category)
        {
            if (category.SubCategory.Trim().Length == 0)
            {
                LoadPackageItemWithoytSubCategory(category);
            }
            else if (category.SubCategory.Trim().Length > 0)
            {
                LoadAllSubCategory(category);
            }
        }

        /// <summary>
        /// Load All SubCategory
        /// </summary>
        /// <param name="category"></param>
        
        private void LoadAllSubCategory(PackageCategoryButton category)
        {
            PackageBLL aPackageBll = new PackageBLL();
            List<PackageItemButton> aPackageItemButtonList = aPackageBll.GetAllSubCategory(category);
            List<int> subCategories = aPackageBll.GetCategory(category.SubCategory);

            subCategories = aPackageBll.MergeSubcategory(subCategories, aPackageItemButtonList);
            List<PackageItemButton> subCategoryList = aPackageBll.GerSubCategoryList(subCategories, category);
            int count = subCategoryList.Count;
            int height = (count / 10);
            if (count % 10 != 0) height += 1;

            foreach (PackageItemButton itemButton in subCategoryList)
            {
                PackageItem aItem = new PackageItem();
                aItem.ItemId = itemButton.RecipeId;
                aItem.ItemName = itemButton.ReciptName;
                aItem.Price = itemButton.AddPrice;
                aItem.Qty =  itemButton.CountPackageItem > 0 ? itemButton.CountPackageItem : 1;
                aItem.OptionName = itemButton.OptionName;
                aItem.PackageId = itemButton.PackageId;
                aItem.CategoryId = itemButton.CategoryId;
                aItem.SubcategoryId = itemButton.SubCategoryId;
                definedPackageItem.Add(aItem);

            }
        }

        #endregion

        #region AllClickEvent

        private void RecipePackageButtonForNonTop_Click(object sen12, EventArgs e) // chnages for package
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            RecipePackageButton tempRecipePackageButton = sen12 as RecipePackageButton;
            definedPackageItem = new List<PackageItem>();
            PackageItemForm.aPackageItemList = new List<PackageItem>();
            PackageItemForm.aRecipeOptionMdList = new List<RecipeOptionMD>();
            PackageItemForm.status = "";

            if (tempRecipePackageButton != null && tempRecipePackageButton.CustomPackage <= 0)
            {
                LoadPackageCategory(tempRecipePackageButton);
            }
            if (!PackageItemForm.aPackageItemList.Any() && PackageItemForm.status != "ok")
            {
                PackageItemForm aPackageItemForm = new PackageItemForm(tempRecipePackageButton);
                aPackageItemForm.ShowDialog();
            }

            int itemIndex = 0;
            if (PackageItemForm.status == "ok" && PackageItemForm.aPackageItemList.Count > 0)
            {

                List<PackageItem> aPackageItemList = PackageItemForm.aPackageItemList;
                List<RecipeOptionMD> aPackageOptionList = PackageItemForm.aRecipeOptionMdList;
                PackageDetails aPackageDetails = new PackageDetails();
                aPackageDetails.qtyTextBox.Text = PackageItemForm.packageQty.ToString();
                aPackageDetails.nameTextBox.Text = tempRecipePackageButton.PackageName;

                if (deliveryButton.Text == "RES")
                {
                    aPackageDetails.priceTextBox.Text = tempRecipePackageButton.InPrice.ToString();
                    aPackageDetails.totalPriceLabel.Text = tempRecipePackageButton.InPrice.ToString();
                }
                else
                {
                    aPackageDetails.priceTextBox.Text = tempRecipePackageButton.OutPrice.ToString();
                    aPackageDetails.totalPriceLabel.Text = tempRecipePackageButton.OutPrice.ToString();
                }

                int optionIndex = GetOptionIndex();

                RecipePackageMD aRecipePackage = new RecipePackageMD();
                aRecipePackage.Description = tempRecipePackageButton.Description;
                aRecipePackage.OptionsIndex = optionIndex + 1;
                aRecipePackage.PackageId = tempRecipePackageButton.PackageId;
                aRecipePackage.PackageName = tempRecipePackageButton.PackageName;
                aRecipePackage.ItemLimit = tempRecipePackageButton.ItemLimit;
                aRecipePackage.Qty = Convert.ToInt32(aPackageDetails.qtyTextBox.Text);
                aRecipePackage.RecipeTypeId = tempRecipePackageButton.RecipeTypeId;
                aRecipePackage.RestaurantId = tempRecipePackageButton.RestaurantId;

                if (deliveryButton.Text == "RES")
                {
                    aRecipePackage.UnitPrice = tempRecipePackageButton.InPrice;
                }
                else
                {
                    aRecipePackage.UnitPrice = tempRecipePackageButton.OutPrice;
                }

                int index = CheckDuplicateForPackage(tempRecipePackageButton, aPackageItemList, aPackageOptionList);

                if (index > 0)
                {
                    itemIndex = index;
                    RecipePackageMD tempOrderItemDetails =aRecipePackageMdList.SingleOrDefault(a => a.OptionsIndex == index);
                    tempOrderItemDetails.Qty += 1;
                    List<PackageItem> aRList = aPackageItemMdList.Where(a => a.OptionsIndex == index).ToList();
                    foreach (PackageItem list in aRList)
                    {
                        list.Qty += 1;
                      //  list.Price = list.Price * list.Qty;
                    }
                }
                else
                {
                    itemIndex = optionIndex + 1;
                    int PackageItemOptionsIndex = 1;
                    aRecipePackageMdList.Add(aRecipePackage);

                    foreach (PackageItem item in aPackageItemList)
                    {
                        //aRecipePackage.ItemLimit -= item.Qty;
                        item.OptionsIndex = itemIndex;
                        item.PackageItemOptionsIndex = PackageItemOptionsIndex;
                        aPackageItemMdList.Add(item);
                       // List<RecipeOptionMD> _optionList = aPackageOptionList.Where(a => a.RecipeId == item.ItemId).ToList();
                        //foreach (RecipeOptionMD option in _optionList)
                        //{
                        //    option.OptionsIndex = itemIndex;
                        //    option.PackageItemOptionsIndex = PackageItemOptionsIndex;
                        //    aRecipeOptionMdList.Add(option);
                        //}
                        PackageItemOptionsIndex++;
                    }

                    foreach (RecipeOptionMD option in aPackageOptionList)
                    {
                        option.OptionsIndex = itemIndex; 
                        aRecipeOptionMdList.Add(option);
                    }
                }

                LoadOrderDetails(itemIndex);
            }
        }

        private void RecipePackageButton_Click(object sen12, EventArgs e) // chnages for package
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            RecipePackageButton tempRecipePackageButton = sen12 as RecipePackageButton;
            int index = AndOnlyPackage(tempRecipePackageButton);

            PackageDetails aDetails = orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>().FirstOrDefault(a => a.PackageId == tempRecipePackageButton.PackageId && a.OptionIndex == index);
            var packagetemp = aRecipePackageMdList.FirstOrDefault(a => a.OptionsIndex == index && a.PackageId == tempRecipePackageButton.PackageId);

            List<PackageItem> packageItems = new PackageItemBLL().GetPackageItem(tempRecipePackageButton, index);
            if (packageItems.Count > 0)
            {
                aPackageItemMdList = aPackageItemMdList.Concat(packageItems).ToList();
                NewItemAdd(index);
            }
            LoadAmountDetails(); 
        }
        private void UsersGridForPackageItem_WasClicked(object sen21, MouseEventArgs e)
        {
            PackItemsControl details = sen21 as PackItemsControl; PackageDetails package =
                    orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>()
                        .FirstOrDefault(a => a.OptionIndex == details.OptionIndex);
            package.BackColor = Color.Red;
            foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {

                foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
                {
                    if (details.nameTextBox == c.nameTextBox)
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                    {
                        c.BackColor = PackItemsControl.DefaultBackColor;
                    }
                }
                if (cc.OptionIndex != package.OptionIndex)
                {
                    cc.BackColor = PackageDetails.DefaultBackColor;
                }
            }

            foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
            {
                foreach (deatilsControls c in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                {
                    c.BackColor = deatilsControls.DefaultBackColor;
                }
            }
            foreach (MultiplePartControl cc in orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
            {
                cc.packageItemsFlowLayoutPanel.BackColor = MultiplePartControl.DefaultBackColor;
                foreach (Label c in cc.packageItemsFlowLayoutPanel.Controls.OfType<Label>())
                {
                    c.BackColor = MultiplePartControl.DefaultBackColor;
                }
                cc.BackColor = MultiplePartControl.DefaultBackColor;
            }
        }

        private void ReceipeSubCategoryButton_Click(object sen4, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            ReceipeSubCategoryButton aReceipeSubCategoryButton = sen4 as ReceipeSubCategoryButton;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            ReceipeMenuItemButton aReceipeMenuItemButton =
                aRestaurantMenuBll.GetRecipeByCategoryAndSubcategory(hasSubcategoryId,
                    aReceipeSubCategoryButton.SubCategoryId);
            aReceipeMenuItemButton.RecipeTypeId = aReceipeSubCategoryButton.RecipeTypeId;
            if (aReceipeMenuItemButton == null || aReceipeMenuItemButton.RecipeMenuItemId <= 0)
            {
                MessageBox.Show("Item not found");
                return;
            }

            RecipePackageMD package = IsCheckPackage(aReceipeMenuItemButton);

            if (package != null && package.PackageId > 0)
            {
                int itemIndex = GetPackageAllItemWithOption(aReceipeMenuItemButton, package);
                if (itemIndex > 0)
                {
                    LoadOrderDetails(itemIndex);
                }
            }
            else if (IsPackageItemAdd())
            {
                int itemIndex = AddItemIntoPackage(aReceipeMenuItemButton);
                if (itemIndex > 0)
                {
                    AddPackageitems(itemIndex);
                }
            }
            else
            {
                int itemIndex = GetAllItemWithOption(aReceipeMenuItemButton);
                if (itemIndex > 0)
                {
                    LoadOrderDetails(itemIndex);
                }
            }
        }

        private void ReceipeMenuItemButton_Click(object sen5, EventArgs e)
        {
            aOthersMethod.NumberPadClose();
            aOthersMethod.KeyBoardClose();

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            ReceipeMenuItemButton aReceipeSubCategoryButton = sen5 as ReceipeMenuItemButton;

            ReceipeMenuItemButton aReceipeMenuItemButton = new ReceipeMenuItemButton();
            if (aReceipeSubCategoryButton != null)
            {
                aReceipeMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(aReceipeSubCategoryButton.RecipeMenuItemId);
                aReceipeMenuItemButton.RecipeTypeId = aReceipeSubCategoryButton.RecipeTypeId;
            }
            if (aReceipeMenuItemButton == null || aReceipeMenuItemButton.RecipeMenuItemId <= 0)
            {
                MessageBox.Show("Item not found");
                return;
            }

            RecipePackageMD package = IsCheckPackage(aReceipeMenuItemButton);

            if (package != null && package.PackageId > 0)
            {
                int itemIndex = GetPackageAllItemWithOption(aReceipeMenuItemButton, package);
                if (itemIndex > 0)
                {
                    LoadOrderDetails(itemIndex, "", aReceipeMenuItemButton.RecipeMenuItemId);
                }
                //}
                // else if (IsPackageItemAdd())
                // {
                //     int itemIndex = AddItemIntoPackage(aReceipeMenuItemButton);
                //     if (itemIndex > 0)
                //     {
                //         AddPackageitems(itemIndex);
                //     }
            }
            else
            {
                int itemIndex = GetAllItemWithOption(aReceipeMenuItemButton);
                if (itemIndex > 0)
                {
                    LoadOrderDetails(itemIndex, aReceipeMenuItemButton.ReceiptName, aReceipeMenuItemButton.RecipeMenuItemId);
                }
            }
        }

        #endregion

        /// <summary>
        /// All Package Information
        /// </summary>
        /// <param name="tempRecipePackageButton"></param>

        #region   All  Package Method
     
        private int AndOnlyPackage(RecipePackageButton tempRecipePackageButton) // chnages for package
        {
             
            //PackageDetails aDetails = orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>().FirstOrDefault(a => a.PackageId == tempRecipePackageButton.PackageId);
            //if (aDetails != null && aDetails.OptionIndex > 0)
            //{
            //    aDetails.ItemLimit += tempRecipePackageButton.ItemLimit;
            //    aRecipePackageMdList.Where(a => a.PackageId == tempRecipePackageButton.PackageId).ToList().ForEach(a => a.Qty += 1);
            //    aRecipePackageMdList.Where(a => a.PackageId == tempRecipePackageButton.PackageId).ToList().ForEach(a => a.ItemLimit += tempRecipePackageButton.ItemLimit);
            //    UpdateTopPackage(aDetails.OptionIndex);

            //}
            //else
            //{
            int itemIndex = 0;

            PackageDetails aPackageDetails = new PackageDetails();
            aPackageDetails.qtyTextBox.Text = "1";
            aPackageDetails.nameTextBox.Text = tempRecipePackageButton.PackageName;

            if (deliveryButton.Text == "RES")
            {
                aPackageDetails.priceTextBox.Text = tempRecipePackageButton.InPrice.ToString();
                aPackageDetails.totalPriceLabel.Text = tempRecipePackageButton.InPrice.ToString();
            }
            else
            {
                aPackageDetails.priceTextBox.Text = tempRecipePackageButton.OutPrice.ToString();
                aPackageDetails.totalPriceLabel.Text = tempRecipePackageButton.OutPrice.ToString();
            }

            aPackageDetails.ItemLimit = tempRecipePackageButton.ItemLimit;
            aPackageDetails.BackColor = Color.Red;
            aPackageDetails.PackageId = tempRecipePackageButton.PackageId;
            int optionIndex = GetOptionIndex();

            RecipePackageMD aRecipePackage = new RecipePackageMD();
            aRecipePackage.Description = tempRecipePackageButton.Description;
            aRecipePackage.OptionsIndex = optionIndex + 1;
            aRecipePackage.PackageId = tempRecipePackageButton.PackageId;
            aRecipePackage.PackageName = tempRecipePackageButton.PackageName;
            aRecipePackage.Qty = 1;
            aRecipePackage.RecipeTypeId = tempRecipePackageButton.RecipeTypeId;
            aRecipePackage.RestaurantId = tempRecipePackageButton.RestaurantId;
            if (deliveryButton.Text == "RES")
            {
                aRecipePackage.UnitPrice = tempRecipePackageButton.InPrice;
            }
            else
            {
                aRecipePackage.UnitPrice = tempRecipePackageButton.OutPrice;
            }

            aRecipePackage.ItemLimit = tempRecipePackageButton.ItemLimit;
            aRecipePackageMdList.Add(aRecipePackage);
            ClearAllCartSelection();
            aPackageDetails.BackColor = Color.Red;
            aPackageDetails.MouseClick += UsersGridForPackage_WasClicked;
            aPackageDetails.OptionIndex = optionIndex + 1;
            orderDetailsflowLayoutPanel1.Controls.Add(aPackageDetails);
            orderDetailsflowLayoutPanel1.Controls.SetChildIndex(aPackageDetails, 0);

            return optionIndex + 1;
        }

        private void UpdateTopPackage(int itemIndex)
        {
            foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                if (cc.OptionIndex == itemIndex)
                {
                    var items = cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>();
                    double qty = Convert.ToDouble(cc.qtyTextBox.Text);
                    double price = Convert.ToDouble(cc.priceTextBox.Text);
                    cc.qtyTextBox.Text = (qty + 1).ToString();
                    cc.totalPriceLabel.Text = ((qty + 1) * price).ToString();

                    ClearAllCartSelection();
                    cc.BackColor = Color.Red;
                    LoadAmountDetails();
                }
            }
        }

        public int CheckDuplicateForPackage(RecipePackageButton tempRecipePackageButton, List<PackageItem> aRecipeList,
            List<RecipeOptionMD> aPackageOptionList)
        {
            return 0;

            List<RecipePackageMD> packageDetails =
                aRecipePackageMdList.Where(a => a.PackageId == tempRecipePackageButton.PackageId).ToList();
            if (packageDetails.Count == 0) return 0;

            int result = 0;
            foreach (RecipePackageMD item in packageDetails)
            {
                List<PackageItem> aRList = aPackageItemMdList.Where(a => a.OptionsIndex == item.OptionsIndex).ToList();

                int cnt = 0;
                if (aRList.Count == aRecipeList.Count)
                {
                    foreach (PackageItem list in aRList)
                    {
                        bool flag = false;
                        foreach (PackageItem recipe in aRecipeList)
                        {
                            List<RecipeOptionMD> saveOptions =
                                aRecipeOptionMdList.Where(
                                    a => a.RecipeId == list.ItemId && a.OptionsIndex == list.OptionsIndex).ToList();
                            List<RecipeOptionMD> currentOptions =
                                aPackageOptionList.Where(
                                    a => a.RecipeId == list.ItemId && a.OptionsIndex == recipe.OptionsIndex).ToList();
                            bool checkOption = CheckOption(saveOptions, currentOptions);
                            if (list.ItemId == recipe.ItemId && checkOption)
                            {
                                cnt++;
                            }
                        }
                    }

                    if (cnt != 0 && cnt == aRecipeList.Count)
                    {
                        result = item.OptionsIndex;
                    }
                }
            }

            return result;
        }

        private bool CheckWithoutMinusItemIsExitsForPackage(RecipeOptionMD saveItem, RecipeOptionMD currentItem)
        {
            if (saveItem.Title != null)
            {
                if (saveItem.Title != currentItem.Title) return false;
            }
            if (currentItem.MinusOption != null)
            {
                if (saveItem.MinusOption != currentItem.MinusOption) return false;
            }
            return true;
        }

        private void AddPackageitems(int itemIndex)
        {
            var packagetemp = aRecipePackageMdList.FirstOrDefault(a => a.OptionsIndex == itemIndex);
            PackageDetails tempPackageDetails =
                orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>()
                    .ToList()
                    .FirstOrDefault(
                        a =>
                            packagetemp != null && a.PackageId == packagetemp.PackageId &&
                            a.OptionIndex == packagetemp.OptionsIndex);
            if (packagetemp != null && (tempPackageDetails != null && packagetemp.PackageId > 0))
            {
                List<PackageItem> itemList =
                    aPackageItemMdList.Where(
                        a => a.PackageId == tempPackageDetails.PackageId && a.OptionsIndex == itemIndex).ToList();
                foreach (PackageItem item in itemList)
                {
                    var packageItem =
                        tempPackageDetails.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>()
                            .FirstOrDefault(a => a.ItemId == item.ItemId);

                    if (packageItem != null && packageItem.ItemId > 0)
                    {
                        packageItem.qtyTextBox.Text = (item.Qty).ToString();
                    }
                    else
                    {
                        PackItemsControl aControl = new PackItemsControl();
                        aControl.qtyTextBox.Text = item.Qty.ToString();
                        aControl.nameTextBox.Text = item.ItemName;
                        aControl.MouseClick += UsersGridForPackageItem_WasClicked;
                        aControl.OptionIndex = tempPackageDetails.OptionIndex;
                        aControl.PackageId = tempPackageDetails.PackageId;
                        aControl.ItemId = item.ItemId;
                        if (item.Price > 0)
                        {
                            aControl.totalPriceLabel.Text = item.Price.ToString();
                        }
                        else
                        {
                            aControl.totalPriceLabel.Text = "";
                        }

                        List<RecipeOptionMD> recipeOptions =
                            aRecipeOptionMdList.Where(
                                a => a.OptionsIndex == tempPackageDetails.OptionIndex && a.RecipeId == item.ItemId)
                                .ToList();
                        if (recipeOptions.Count > 0)
                        {
                            aControl.packageOptionsLabel.Text = "  ";
                            bool flag = false;
                            foreach (RecipeOptionMD list in recipeOptions)
                            {
                                if (flag)
                                {
                                    aControl.packageOptionsLabel.Text += "\r\n  ";
                                }
                                if (!string.IsNullOrEmpty(list.MinusOption))
                                {
                                    if (list.InPrice > 0)
                                    {
                                        aControl.packageOptionsLabel.Text += ("→No " + list.MinusOption);
                                    }
                                    else aControl.packageOptionsLabel.Text += ("→No " + list.MinusOption);

                                }
                                if (!string.IsNullOrEmpty(list.Title))
                                {
                                    if (list.InPrice > 0)
                                    {
                                        aControl.packageOptionsLabel.Text += "→" + (list.Title) + "+" + list.InPrice;
                                    }
                                    else aControl.packageOptionsLabel.Text += "→" + (list.Title);
                                }
                                flag = true;
                            }
                        }
                        else
                        {
                            aControl.packageOptionsLabel.Visible = false;
                        }

                        tempPackageDetails.packageItemsFlowLayoutPanel.Controls.Add(aControl);
                    }
                }

                if (tempPackageDetails.ItemLimit > 0 || packagetemp.ItemLimit > 0)
                {
                    ClearAllCartSelection();
                    tempPackageDetails.BackColor = Color.Red;
                }
            }
        }

        private int AddItemIntoPackage(ReceipeMenuItemButton aReceipeMenuItemButton)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                if (cc.BackColor == Color.Red)
                {
                    RecipePackageButton aResItemButton = aRestaurantMenuBll.GetPackageByPackageId(cc.PackageId);
                    List<PackItemsControl> packItems =
                        cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>().ToList();
                    int qty = Convert.ToInt32(packItems.Sum(a => Convert.ToInt32(a.qtyTextBox.Text)));
                    if (qty < aResItemButton.ItemLimit)
                    {

                        bool flag = aRestaurantMenuBll.GetReceipeOptionsByItemId(aReceipeMenuItemButton.RecipeMenuItemId);
                        int itemIndex = 0;

                        ItemOptionForm.Status = "";
                        ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();

                        if (flag)
                        {
                            ItemOptionForm.Status = "";
                            ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();
                            ItemOptionForm aItemOptionForm = new ItemOptionForm(aReceipeMenuItemButton.RecipeMenuItemId);
                            aItemOptionForm.ShowDialog();

                        }
                        if ((ItemOptionForm.Status == "cancel" || ItemOptionForm.Status == "") && flag) return 0;

                        List<RecipeOptionItemButton> aRecipeList = ItemOptionForm.aRecipeList;
                        int optionIndex = cc.OptionIndex;

                        PackageItemExtraPrice aPackageItemExtraPrice =
                            aRestaurantMenuBll.GetPackageItemPrice(aReceipeMenuItemButton.RecipeMenuItemId, cc.PackageId);

                        PackageItem aOrderItemDetails = new PackageItem();
                        aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
                        aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
                        aOrderItemDetails.ItemName = aReceipeMenuItemButton.ReceiptName;
                        aOrderItemDetails.ItemFullName = aReceipeMenuItemButton.ShortDescrip;
                        aOrderItemDetails.OptionsIndex = optionIndex;
                        aOrderItemDetails.Price = aPackageItemExtraPrice.AddPrice;
                        aOrderItemDetails.Qty = 1;
                        aOrderItemDetails.SubcategoryId = aReceipeMenuItemButton.SubCategoryId;
                        aOrderItemDetails.PackageId = cc.PackageId;

                        int index = CheckPackageItemDulicate(aOrderItemDetails, aRecipeList, cc.PackageId, optionIndex);

                        if(index > 0)
                        {
                            itemIndex = aOrderItemDetails.OptionsIndex;
                            List<PackageItem> aRList =
                                aPackageItemMdList.Where(a => a.OptionsIndex == optionIndex && a.ItemId == index)
                                    .ToList();
                            foreach (PackageItem list in aRList)
                            {
                                list.Qty += 1;
                            }
                        }
                        else
                        {
                            itemIndex = aOrderItemDetails.OptionsIndex;
                            aPackageItemMdList.Add(aOrderItemDetails);
                            foreach (RecipeOptionItemButton recipe in aRecipeList)
                            {
                                RecipeOptionMD aOptionMD = new RecipeOptionMD();
                                aOptionMD.RecipeId = aReceipeMenuItemButton.RecipeMenuItemId;
                                aOptionMD.TableNumber = 1;
                                aOptionMD.RecipeOptionId = recipe.RecipeOptionId;

                                if (!string.IsNullOrEmpty(recipe.MinusTitle))
                                {
                                    aOptionMD.MinusOption = recipe.MinusTitle;
                                }
                                else if (!string.IsNullOrEmpty(recipe.Title))
                                {
                                    aOptionMD.Title = recipe.Title;
                                }

                                aOptionMD.Type = recipe.RecipeOptionButton.Type;
                                aOptionMD.Price = recipe.Price;
                                aOptionMD.InPrice = recipe.InPrice;
                                aOptionMD.Qty = 1;
                                aOptionMD.OptionsIndex = optionIndex;
                                aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                                aRecipeOptionMdList.Add(aOptionMD);
                            }
                        }
                        return itemIndex;
                    }
                }
            }
            return 0;
        }

        private bool IsPackageItemAdd()
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                if (cc.BackColor == Color.Red)
                {
                    RecipePackageButton aResItemButton = aRestaurantMenuBll.GetPackageByPackageId(cc.PackageId);
                    List<PackItemsControl> packItems =
                        cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>().ToList();
                    int qty = Convert.ToInt32(packItems.Sum(a => Convert.ToInt32(a.qtyTextBox.Text)));
                    if (qty < aResItemButton.ItemLimit)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private RecipePackageMD IsCheckPackage(ReceipeMenuItemButton aReceipeMenuItemButton) //changes for package
        {
            foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                int limit = Convert.ToInt16(aPackageItemMdList.Where(a => a.OptionsIndex == cc.OptionIndex && a.PackageId == cc.PackageId).Sum(a => a.Qty));
                if (cc.BackColor == Color.Red && Convert.ToInt16(cc.qtyTextBox.Text) * cc.ItemLimit > limit)
                {
                    return aRecipePackageMdList.FirstOrDefault(a => a.PackageId == cc.PackageId && a.OptionsIndex == cc.OptionIndex);
                }
            }
            return new RecipePackageMD();
        }

        private int GetPackageAllItemWithOption(ReceipeMenuItemButton aReceipeMenuItemButton, RecipePackageMD package)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            bool flag = aRestaurantMenuBll.GetReceipeOptionsByItemId(aReceipeMenuItemButton.RecipeMenuItemId);
            int itemIndex = 0;

            ItemOptionForm.Status = "";
            ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();

            if (flag)
            {
                ItemOptionForm.Status = "";
                ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();
                ItemOptionForm aItemOptionForm = new ItemOptionForm(aReceipeMenuItemButton.RecipeMenuItemId);
                aItemOptionForm.ShowDialog();

            }
            if ((ItemOptionForm.Status == "cancel" || ItemOptionForm.Status == "") && flag) return 0;

            List<RecipeOptionItemButton> aRecipeList = ItemOptionForm.aRecipeList;
            int optionIndex = package.OptionsIndex;

            PackageItemExtraPrice aPackageItemExtraPrice =
                aRestaurantMenuBll.GetPackageItemPrice(aReceipeMenuItemButton.RecipeMenuItemId, package.PackageId);

            PackageItem aOrderItemDetails = new PackageItem();
            aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
            aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
            aOrderItemDetails.ItemName = aReceipeMenuItemButton.ReceiptName;
            aOrderItemDetails.ItemFullName = aReceipeMenuItemButton.ShortDescrip;
            aOrderItemDetails.PackageItemOptionsIndex = GetPackageItemOptionIndex(package);
            aOrderItemDetails.OptionsIndex = optionIndex;
            aOrderItemDetails.Price = aPackageItemExtraPrice.AddPrice;
            aOrderItemDetails.Qty = 1;
            aOrderItemDetails.SubcategoryId = aReceipeMenuItemButton.SubCategoryId;
            aOrderItemDetails.PackageId = package.PackageId;
            int index = CheckPackageItemDulicate(aOrderItemDetails, aRecipeList, package.PackageId, optionIndex);
            if (index > 0)
            {
                itemIndex = optionIndex;
                List<PackageItem> aRList = aPackageItemMdList.Where(a => a.OptionsIndex == optionIndex && a.ItemId == index).ToList();
                foreach (PackageItem list in aRList)
                {
                    list.Qty += 1;
                }
            }
            else
            {
                itemIndex = optionIndex;
                aPackageItemMdList.Add(aOrderItemDetails);
                int incOPtion = 0;
                foreach (RecipeOptionItemButton recipe in aRecipeList)
                {
                    RecipeOptionMD aOptionMD = new RecipeOptionMD();
                    aOptionMD.RecipeId = aReceipeMenuItemButton.RecipeMenuItemId;
                    aOptionMD.TableNumber = 1;
                    aOptionMD.RecipeOptionId = recipe.RecipeOptionId;

                    if (!string.IsNullOrEmpty(recipe.MinusTitle))
                    {
                        aOptionMD.MinusOption = recipe.MinusTitle;
                    }
                    else if (!string.IsNullOrEmpty(recipe.Title))
                    {
                        aOptionMD.Title = recipe.Title;
                    }

                    aOptionMD.Type = recipe.RecipeOptionButton.Type;
                    aOptionMD.Price = recipe.Price;
                    aOptionMD.InPrice = recipe.InPrice;
                    aOptionMD.PackageItemOptionsIndex = aOrderItemDetails.PackageItemOptionsIndex;
                    aOptionMD.Qty = 1;
                    aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex; //Convert.ToInt32(optionIndex+"000"+(incOPtion++).ToString());
                    aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                    aRecipeOptionMdList.Add(aOptionMD);
                }
            }
            return itemIndex;
        }

        private int CheckPackageItemDulicate(PackageItem aOrderItemDetails, List<RecipeOptionItemButton> aRecipeList, int PackageId, int index)
        {
            PackageBLL aPackageBll = new PackageBLL();
            return aPackageBll.CheckPackageItemDulicate(aOrderItemDetails, aRecipeList, PackageId, aPackageItemMdList, aRecipeOptionMdList, index);
        }

        #endregion

        /// <summary>
        /// Check Option Item List
        /// </summary>
        /// <param name="saveOptions"></param>
        /// <param name="currentOptions"></param>
        /// <returns></returns>
        
        private bool CheckOption(List<RecipeOptionMD> saveOptions, List<RecipeOptionMD> currentOptions)
        {
            if (!saveOptions.Any() && !currentOptions.Any()) return true;
            int cnt = 0;
            bool res = true;
            if (saveOptions.Count == currentOptions.Count)
            {
                foreach (RecipeOptionMD list in saveOptions)
                {
                    bool flag = false;
                    foreach (RecipeOptionMD recipe in currentOptions)
                    {
                        if (CheckWithoutMinusItemIsExitsForPackage(list, recipe))
                        {
                            res = false;
                            break;
                        }
                        else cnt++;
                    }
                    if (res == false)
                    {
                        break;
                    }
                }
            }
            return res;
        }

        #region All Load Ammount Detials

        private void LoadAmountDetails()
        {
            double total;
            if (!double.TryParse(customTotalTextBox.Text, out total))
            {
                double amount1 = aOrderItemDetailsMDList.Sum(a => a.Qty * a.Price);
                double amount2 = aRecipePackageMdList.Sum(a => a.Qty * a.UnitPrice);
               // double amount3 = aPackageItemMdList.Sum(a => a.Qty * a.Price);
                double amount3 = aPackageItemMdList.Sum(a => a.Price);
                double packageOptionPrice = aRecipeOptionMdList.Sum(a => a.Qty * a.Price);

               double amount4 = aRecipeMultipleMdList.Sum(a => a.Qty * a.UnitPrice);
                totalAmountLabel.Text = "£" + (amount1 + amount2 + amount3+ amount4 + aGeneralInformation.CardFee +
                                         aGeneralInformation.ServiceCharge + aGeneralInformation.DeliveryCharge -
                                         aGeneralInformation.DiscountFlat - aGeneralInformation.ItemDiscount).ToString("F02");
            }
            else
            {
                totalAmountLabel.Text = "£" + (total).ToString("F02");
            }

            foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
            {
                double amount =
                    aOrderItemDetailsMDList.Where(b => b.RecipeTypeId == control1.RecipeTypeId).Sum(a => a.Price * a.Qty);
                control1.recipeTypeAmountlabel.Text = amount.ToString("F02");
            }
        }

        /// <summary>
        /// LoadExtraPrice
        /// </summary>
        /// <param name="aReceipeTypeButton"></param>
        /// <param name="tp"></param>
        
        private void LoadExtraPrice()
        {
            ExtraPriceBLL aExtraPriceBll = new ExtraPriceBLL();
            ExtraPriceModel aExtraPriceModel = aExtraPriceBll.GetExtraPrice();
            if (aExtraPriceModel != null)
            {
                price1Button.Text = GetExtraPrice(aExtraPriceModel.Price_1, price1Button);
                price2Button.Text = GetExtraPrice(aExtraPriceModel.Price_2, price2Button);
                price3Button.Text = GetExtraPrice(aExtraPriceModel.Price_3, price3Button);
                price4Button.Text = GetExtraPrice(aExtraPriceModel.Price_4, price4Button);
                price5Button.Text = GetExtraPrice(aExtraPriceModel.Price_5, price5Button);
            }
        }

        private string GetExtraPrice(double poisa, System.Windows.Forms.Button pricebutton)
        {
            if (poisa < 1)
            {
                if (poisa <= 0) pricebutton.Visible = false;
                return (poisa * 100) + "p";
            }
            else return ("£" + poisa);
        }

        #endregion

        #region LoadMenuType

        private void LoadMenuType()
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            List<ReceipeTypeButton> aReceipeTypeButtons = aRestaurantMenuBll.GetRecipeType();
            int cnt = 0;
            foreach (ReceipeTypeButton aReceipeTypeButton in aReceipeTypeButtons)
            {
                AddIntoTabPage(aReceipeTypeButton);
                LoadPackage(aReceipeTypeButton);
            }
        }

        #endregion

        #region Tab Event

        private void AddIntoTabPage(ReceipeTypeButton aReceipeTypeButton)
        {
            CustomiseTabPage tp = new CustomiseTabPage();
            tp.TypeId = aReceipeTypeButton.TypeId;
            tp.ParentTypeId = aReceipeTypeButton.ParentTypeId;
            tp.TypeName = aReceipeTypeButton.TypeName;
            tp.RestaurantId = aReceipeTypeButton.RestaurantId;
            tp.SortOrder = aReceipeTypeButton.SortOrder;
            tp.MergeItems = aReceipeTypeButton.MergeItems;
            tp.CategoryWidth = aReceipeTypeButton.CategoryWidth;
            tp.PackageWidth = aReceipeTypeButton.PackageWidth;
            tp.SubcategoryWidth = aReceipeTypeButton.SubcategoryWidth;
            tp.Font = aReceipeTypeButton.Font;
            tp.Text = aReceipeTypeButton.Text;
            tp.Height = aReceipeTypeButton.Height = 36;

            tp.MinimumSize = new Size(120, 50);
            tp.Margin = new Padding(0, 0, 10, 0);
            tp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tp.Name = aReceipeTypeButton.TypeId.ToString();
            tp.Click += tp_click;
            tp.AutoScroll = true;
            LoadCategoryWhenFormLoad(aReceipeTypeButton, tp);
        }

        #endregion

        #region  Load All Menu Into Tab Page

        /// <summary>
        /// Load Category When Form Load
        /// </summary>
        /// <param name="aReceipeTypeButton"></param>
        /// <param name="tp"></param>
        
        private void LoadCategoryWhenFormLoad(ReceipeTypeButton aReceipeTypeButton, CustomiseTabPage tp)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            int categoryHeight = 20;
            leftCategoryPanel.Location = new Point(leftCategoryPanel.Location.X, 0);
            subCategoryPanel.Location = new Point(subCategoryPanel.Location.X, 0);
            rightCategoryPanel.Location = new Point(rightCategoryPanel.Location.X, 0);

            leftCategoryPanel.Visible = false;
            rightCategoryPanel.Visible = false;
            subCategoryPanel.Visible = false;

            aFlowLayoutlist = new List<CustomFlowLayoutPanel>();
            rightFlowLayoutlist = new List<CustomFlowLayoutPanel>();
            ClearAllItemsIntoFlowlayoutPanel();

            List<ReceipeCategoryButton> categoryLeftList = new List<ReceipeCategoryButton>();
            List<ReceipeCategoryButton> categoryRightList = new List<ReceipeCategoryButton>();
            List<ReceipeCategoryButton> hasSubCategoryList = new List<ReceipeCategoryButton>();

            List<ReceipeCategoryButton> tempCategoryList =
                allCategoryButton.Where(a => a.ReceipeTypeId == aReceipeTypeButton.TypeId).ToList();
            bool rightList = false;
            int buttonWidth = 0;
            int lineHeight = 40;
            hasSubCategoryFlowLayoutPanel.SuspendLayout();

            Panel subCatPanel = new Panel();
            subCatPanel.Location = new Point(0, 0);
            subCatPanel.Size = new Size(0, 0);
            subCatPanel.AutoSize = true;
            subCatPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            subCatPanel.Padding = new Padding(0, 0, 0, 0);
            subCatPanel.Margin = new Padding(0, 0, 0, 0);

            FlowLayoutPanel subcategoryFlowLayoutPanel = new FlowLayoutPanel();
            subcategoryFlowLayoutPanel.Location = new Point(0, 0);
            subcategoryFlowLayoutPanel.Size = new Size(0, 0);
            subcategoryFlowLayoutPanel.AutoSize = true;
            subcategoryFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            subcategoryFlowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            subcategoryFlowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
            subcategoryFlowLayoutPanel.Margin = new Padding(1, 1, 1, 1);

            FlowLayoutPanel subItemflowLayoutPanel1 = new FlowLayoutPanel();
            subItemflowLayoutPanel1.Location = subCategoryMenuItemFlowLayoutPanel.Location;
            subItemflowLayoutPanel1.Size = new Size(0, 0);
            subItemflowLayoutPanel1.AutoSize = false;
            subItemflowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            subItemflowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            subItemflowLayoutPanel1.Padding = new Padding(0, 0, 0, 0);
            subItemflowLayoutPanel1.Margin = new Padding(0, 0, 0, 0);

            int totalCategoryTab = tempCategoryList.Any(a => a.HasSubcategory <= 0)
                ? tempCategoryList.Sum(a => a.MaxRow)
                : 0;

            totalCategoryTab = 0;
            foreach (ReceipeCategoryButton categoryButton in tempCategoryList)
            {
                bool showCategory = ShowCategory(categoryButton);
                if (showCategory && categoryButton.HasSubcategory <= 0)
                {
                    totalCategoryTab += categoryButton.MaxRow;
                }
            }

            int totalSubCategoryTab = tempCategoryList.Any(a => a.HasSubcategory > 0)
                ? aReceipeTypeButton.CategoryWidth
                : 0;
            int totalSubItemTab = tempCategoryList.Any(a => a.HasSubcategory > 0)
                ? aReceipeTypeButton.SubcategoryWidth
                : 0;
            int totoltab = totalCategoryTab + totalSubCategoryTab + totalSubItemTab;

            bool isLasttab = false;
            if (totoltab == 12)
            {
                isLasttab = true;
            }
            int lineHeight1 = 0;
            foreach (ReceipeCategoryButton aReceipeCategoryButton in tempCategoryList)
            {
                if (aReceipeCategoryButton.HasSubcategory == 0)
                {
                    if (!rightList)
                    {
                        categoryLeftList.Add(aReceipeCategoryButton);
                    }
                    else
                    {
                        categoryRightList.Add(aReceipeCategoryButton);
                    }
                }
                else
                {
                    hasSubCategoryList.Add(aReceipeCategoryButton);
                    aReceipeCategoryButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(aReceipeCategoryButton.Color));
                    aReceipeCategoryButton.FlatStyle = FlatStyle.Flat;
                    aReceipeCategoryButton.FlatAppearance.BorderSize = 0;

                    aReceipeCategoryButton.Font = new System.Drawing.Font(urls.fontFamily,
                        Convert.ToInt32(urls.fontSize), urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                    aReceipeCategoryButton.ForeColor = Color.White;
                    aReceipeCategoryButton.MouseClick += btn_Read_MouseClick;
                    aReceipeCategoryButton.Padding = new Padding(1, 1, 1, 1);
                    aReceipeCategoryButton.Margin = new Padding(1, 1, 1, 1);

                    aReceipeCategoryButton.Height = aReceipeCategoryButton.CategoryHeight;
                    if (aReceipeCategoryButton.Height < 40)
                    {
                        if (aReceipeCategoryButton.BackColor != Color.BurlyWood)
                        {
                            aReceipeCategoryButton.Height = 42;
                        }
                    }
                    if (!rightList && aReceipeCategoryButton.CategoryWidth != 12)
                    {
                        lineHeight1 = aReceipeCategoryButton.Height;
                    }

                    int categoryWidth = (int)Math.Ceiling(aReceipeTypeButton.CategoryWidth * 1.0 * (leftPanelSize / 12.0));

                    aReceipeCategoryButton.Width =
                        (int)Math.Ceiling(aReceipeCategoryButton.CategoryWidth * 1.0 * (categoryWidth / 12.0));
                    buttonWidth += aReceipeCategoryButton.Width + aReceipeCategoryButton.CategoryWidth;
                    // changed for button width
                    if (buttonWidth >= categoryWidth + aReceipeTypeButton.CategoryWidth &&
                        tempCategoryList[tempCategoryList.Count() - 1] != aReceipeCategoryButton)
                    {
                        lineHeight1 += (aReceipeCategoryButton.Height + 4);
                        subcategoryFlowLayoutPanel.Size = new Size(categoryWidth + aReceipeTypeButton.CategoryWidth,
                            lineHeight1);
                        buttonWidth = 0;
                    }
                    subcategoryFlowLayoutPanel.Controls.Add(aReceipeCategoryButton);
                    rightList = true;
                }
            }

            hasSubCategoryFlowLayoutPanel.ResumeLayout();
            Panel leftCatPanel = new Panel();
            leftCatPanel.Location = new Point(0, 0);
            leftCatPanel.Size = new Size(0, 0);
            leftCatPanel.AutoSize = true;
            leftCatPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            leftCatPanel.Padding = new Padding(0, 0, 0, 0);
            leftCatPanel.Margin = new Padding(0, 0, 0, 0);

            FlowLayoutPanel leftRecipeCatFlowLayoutPanel = new FlowLayoutPanel();

            leftRecipeCatFlowLayoutPanel.Location = new Point(0, 0);
            leftRecipeCatFlowLayoutPanel.Size = new Size(0, 0);
            leftRecipeCatFlowLayoutPanel.AutoSize = true;
            leftRecipeCatFlowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            leftRecipeCatFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            leftRecipeCatFlowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
            leftRecipeCatFlowLayoutPanel.Margin = new Padding(0, 0, 0, 0);

            FlowLayoutPanel leftItemflowLayoutPanel1 = new FlowLayoutPanel();
            leftItemflowLayoutPanel1.Location = leftCatItemflowLayoutPanel1.Location;
            leftItemflowLayoutPanel1.Size = new Size(0, 0);
            leftItemflowLayoutPanel1.AutoSize = true;
            leftItemflowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            leftItemflowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            leftItemflowLayoutPanel1.Padding = new Padding(0, 0, 0, 0);
            leftItemflowLayoutPanel1.Margin = new Padding(0, 0, 0, 0);

            int cnt = 0;
            int count1 = categoryLeftList.Count() - 1;
            foreach (ReceipeCategoryButton aaReceipeCategoryButton in categoryLeftList)
            {
                if (!ShowCategory(aaReceipeCategoryButton))
                {
                    // count1--;
                    continue;
                }
                int categoryWidth = 0;
                if (aaReceipeCategoryButton.HasSubcategory == 0)
                {
                    categoryWidth = (int)Math.Ceiling(aaReceipeCategoryButton.MaxRow * 1.0 * (leftPanelSize / 12.0));
                    aaReceipeCategoryButton.Width = categoryWidth + aaReceipeCategoryButton.MaxRow;
                }
                aaReceipeCategoryButton.BackColor = Color.BurlyWood;
                aaReceipeCategoryButton.FlatStyle = FlatStyle.Flat;
                string font_size = "8"; // urls.fontSize;
                aaReceipeCategoryButton.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                    urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                aaReceipeCategoryButton.FlatAppearance.BorderSize = 0;
                aaReceipeCategoryButton.Height = categoryHeight;
                //  aaReceipeCategoryButton.Margin = new Padding(2, 2, 2, 2);
                aaReceipeCategoryButton.Padding = new Padding(1, 0, 1, 0);
                aaReceipeCategoryButton.Margin = new Padding(2, 1, 2, 1);
                aaReceipeCategoryButton.Width = (aaReceipeCategoryButton.Width - 2);
                string sr = aaReceipeCategoryButton.Width.ToString();
                aaReceipeCategoryButton.FlatAppearance.MouseOverBackColor = Color.BurlyWood;
                leftRecipeCatFlowLayoutPanel.Controls.Add(aaReceipeCategoryButton);
                cnt += (aaReceipeCategoryButton.Width + 1);
                try
                {
                    if (categoryRightList.Count() <= 0 && categoryLeftList[count1] == aaReceipeCategoryButton &&
                        isLasttab)
                    {
                        lastCategory = true;
                    }
                    else lastCategory = false;
                }
                catch
                {

                }

                LoadMenuItemWhenFormLoad(aaReceipeCategoryButton, cnt, categoryWidth, aaReceipeCategoryButton.MaxRow, leftRecipeCatFlowLayoutPanel);
            }

            if (!isLasttab && categoryRightList.Count() <= 0)
            {
                List<RecipePackageButton> aRecipePackage =
                    aRestaurantMenuBll.GetPackageByMenuType(aReceipeTypeButton.TypeId);
                ReceipeTypeButton aReceipeType = aReceipeTypeButton;
                if (aRecipePackage.Count() > 0)
                {
                    ReceipeCategoryButton button = new ReceipeCategoryButton();
                    button.Text = "Package";
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    string font_size = "8"; // urls.fontSize;
                    button.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                        urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                    button.Height = categoryHeight;
                    if (aReceipeType.PackageWidth == 0)
                    {
                        aReceipeType.PackageWidth = 1;
                    }

                    button.Width = (int)((int)aReceipeTypeButton.PackageWidth * 1.0 * (leftPanelSize / 12));
                    int categoryWidth = (int)((int)aReceipeTypeButton.PackageWidth * 1.0 * (leftPanelSize / 12));

                    button.BackColor = Color.BurlyWood;
                    button.ForeColor = Color.Black;
                    button.Padding = new Padding(0, 0, 0, 0);
                    button.Margin = new Padding(2, 3, 3, 1);
                    leftRecipeCatFlowLayoutPanel.Controls.Add(button);
                    lineHeight += button.Height;

                    LoadLeftPackageForPackage(aRecipePackage, cnt, categoryWidth, aReceipeTypeButton.PackageWidth, leftRecipeCatFlowLayoutPanel, aReceipeType);
                }
            }

            leftCatItemflowLayoutPanel1.SuspendLayout();

            for (int i = 0; i < aFlowLayoutlist.Count; i++)
            {
                leftItemflowLayoutPanel1.Controls.Add(aFlowLayoutlist[i]);
            }
            leftCatItemflowLayoutPanel1.ResumeLayout();

            Panel rightCatPanel = new Panel();
            rightCatPanel.Location = new Point(rightCategoryPanel.Location.X, 0);

            rightCatPanel.Size = new Size(0, 0);
            rightCatPanel.AutoSize = true;

            rightCatPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rightCatPanel.Padding = new Padding(0, 0, 0, 0);
            rightCatPanel.Margin = new Padding(0, 0, 0, 0);

            FlowLayoutPanel rightRecipeCatFlowLayoutPanel = new FlowLayoutPanel();

            rightRecipeCatFlowLayoutPanel.Location = new Point(0, 0);
            rightRecipeCatFlowLayoutPanel.Size = new Size(0, 0);
            rightRecipeCatFlowLayoutPanel.AutoSize = true;
            rightRecipeCatFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            rightRecipeCatFlowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rightRecipeCatFlowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
            rightRecipeCatFlowLayoutPanel.Margin = new Padding(0, 0, 0, 0);

            FlowLayoutPanel rightItemflowLayoutPanel1 = new FlowLayoutPanel();
            rightItemflowLayoutPanel1.Location = rightCatItemflowLayoutPanel1.Location;
            rightItemflowLayoutPanel1.Size = new Size(0, 0);
            rightItemflowLayoutPanel1.AutoSize = true;
            rightItemflowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            rightItemflowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rightItemflowLayoutPanel1.Padding = new Padding(0, 0, 0, 0);
            rightItemflowLayoutPanel1.Margin = new Padding(0, 0, 0, 0);

            cnt = 0;
            aFlowLayoutlist = new List<CustomFlowLayoutPanel>();
            int count = categoryRightList.Count() - 1;
            foreach (ReceipeCategoryButton aaaReceipeCategoryButton in categoryRightList)
            {
                if (!ShowCategory(aaaReceipeCategoryButton))
                {
                    continue;
                }

                aaaReceipeCategoryButton.BackColor = Color.BurlyWood;
                aaaReceipeCategoryButton.FlatStyle = FlatStyle.Flat;
                string font_size = "8"; // urls.fontSize;
                aaaReceipeCategoryButton.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                    urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                aaaReceipeCategoryButton.FlatAppearance.BorderSize = 0;
                aaaReceipeCategoryButton.FlatAppearance.MouseOverBackColor = Color.BurlyWood;
                aaaReceipeCategoryButton.Height = categoryHeight;
                int categoryWidth = 0;
                if (aaaReceipeCategoryButton.HasSubcategory == 0)
                {
                    categoryWidth = (int)Math.Ceiling(aaaReceipeCategoryButton.MaxRow * 1.0 * (leftPanelSize / 12.0));
                    aaaReceipeCategoryButton.Width = categoryWidth + aaaReceipeCategoryButton.MaxRow;
                }
                aaaReceipeCategoryButton.Padding = new Padding(1, 0, 1, 0);
                aaaReceipeCategoryButton.Margin = new Padding(1, 1, 1, 1);
                rightRecipeCatFlowLayoutPanel.Controls.Add(aaaReceipeCategoryButton);
                cnt += (aaaReceipeCategoryButton.Width + 1);
                if (categoryRightList[count] == aaaReceipeCategoryButton && isLasttab)
                {
                    lastCategory = true;
                }
                else lastCategory = false;
                LoadMenuItemForRightWhenFormLoad(aaaReceipeCategoryButton, cnt, categoryWidth,
                    aaaReceipeCategoryButton.MaxRow, rightRecipeCatFlowLayoutPanel);
            }

            if (!isLasttab && categoryRightList.Count() > 0)
            {

                List<RecipePackageButton> aRecipePackage =
                    aRestaurantMenuBll.GetPackageByMenuType(aReceipeTypeButton.TypeId);
                ReceipeTypeButton aReceipeType = aReceipeTypeButton;

                if (aRecipePackage.Count() > 0)
                {
                    ReceipeCategoryButton button = new ReceipeCategoryButton();
                    button.Text = "Package";
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.Height = categoryHeight;
                    string font_size = "8"; // urls.fontSize;
                    button.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                        urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                    if (aReceipeType.PackageWidth == 0)
                    {
                        aReceipeType.PackageWidth = 1;
                    }
                    button.Width = (int)((int)aReceipeTypeButton.PackageWidth * 1.0 * (leftPanelSize / 12));
                    int categoryWidth = (int)((int)aReceipeTypeButton.PackageWidth * 1.0 * (leftPanelSize / 12));

                    button.BackColor = Color.BurlyWood;
                    button.ForeColor = Color.Black;
                    button.Padding = new Padding(0, 0, 0, 0);
                    button.Margin = new Padding(2, 3, 3, 1);
                    rightRecipeCatFlowLayoutPanel.Controls.Add(button);
                    lineHeight += button.Height;
                    LoadRightPackageForPackage(aRecipePackage, cnt, categoryWidth, aReceipeTypeButton.PackageWidth,
                        rightRecipeCatFlowLayoutPanel, aReceipeType);
                }
            }

            rightCatItemflowLayoutPanel1.SuspendLayout();
            for (int i = 0; i < rightFlowLayoutlist.Count; i++)
            {

                rightItemflowLayoutPanel1.Controls.Add(rightFlowLayoutlist[i]);
            }
            rightCatItemflowLayoutPanel1.ResumeLayout();

            tp.Controls.Clear();
            leftCatPanel.Controls.Add(leftRecipeCatFlowLayoutPanel);
            leftCatPanel.Controls.Add(leftItemflowLayoutPanel1);

            tp.Controls.Add(leftCatPanel);

            subCatPanel.Controls.Add(subcategoryFlowLayoutPanel);
            subItemflowLayoutPanel1 = GetSubCategory(aReceipeTypeButton);
            subItemflowLayoutPanel1.Location =
                new Point(subcategoryFlowLayoutPanel.Location.X + subcategoryFlowLayoutPanel.Size.Width,
                    subcategoryFlowLayoutPanel.Location.Y);
            subCatPanel.Controls.Add(subItemflowLayoutPanel1);
            subCatPanel.Location = new Point(leftCatPanel.Location.X + leftCatPanel.Size.Width, subCatPanel.Location.Y);

            if (hasSubCategoryList.Count() <= 0)
            {
                subCatPanel.Visible = false;
            }

            CheckingForm aForm = new CheckingForm(subCatPanel);

            rightCatPanel.Controls.Add(rightRecipeCatFlowLayoutPanel);
            rightCatPanel.Controls.Add(rightItemflowLayoutPanel1);
            rightCatPanel.Location = new Point(subCatPanel.Location.X + subCatPanel.Size.Width, rightCatPanel.Location.Y);

            //  ColorTranslator.FromHtml
            //try//{
            //    SetTabHeader(tp, aReceipeTypeButton.BackColor);
            //}
            //catch (Exception)
            //{
            //    SetTabHeader(tp, Color.FromArgb(59, 175, 218));
            //}

            tp.Controls.Add(subCatPanel);
            tp.Controls.Add(rightCatPanel);

            menuTabControl.TabPages.Add(tp);
            int recipeTypeWidth = menuTabControl.Width;
            // screenWidth - (330 + packageTopFlowLayoutPanel.Width);

            Button aButton = new Button();

            aButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            aButton.FlatStyle = FlatStyle.Flat;
            aButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));

            aButton.Cursor = System.Windows.Forms.Cursors.Hand;
            aButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            aButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            aButton.ForeColor = System.Drawing.Color.White;
            aButton.MinimumSize = new Size(50, 40);
            aButton.Text = tp.Text;
            aButton.TabIndex = menuTabControl.TabPages.Count - 1;
            if (aButton.TabIndex == 0)
            {
                aButton.BackColor = Color.Black;
            }
            aButton.AutoSize = true;
            aButton.Font = menuTabControl.Font;
            aButton.Click += tb_new_click;

            receipeTypeFlowLayoutPanel.Width += aButton.Width + 20;
            if ((receipeTypeFlowLayoutPanel.Width - (recipeTypeWidth * recipeTypeLine)) >200)
            {
                receipeTypeFlowLayoutPanel.Width -= aButton.Width;
                receipeTypeFlowLayoutPanel.Height += 55;
                recipeTypeLine = 1;
            }

            receipeTypeFlowLayoutPanel.Controls.Add(aButton);
            receipeTypeFlowLayoutPanel.Visible = true;

            menuTabControl.SendToBack();

            if (leftCatPanel.Height > menuTabControlHeight)
            {
                menuTabControlHeight = leftCatPanel.Height;
            }
            if (subCatPanel.Height > menuTabControlHeight)
            {
                menuTabControlHeight = subCatPanel.Height;
            }
            if (rightCatPanel.Height > menuTabControlHeight)
            {
                menuTabControlHeight = rightCatPanel.Height;
            }

            tp.Click += tp_click;
        }

        private void tb_new_click(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Control cont in receipeTypeFlowLayoutPanel.Controls)
            {
                cont.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(120)))),
                    ((int)(((byte)(215)))));

            }

            Button tabProduct = sender as Button;
            tabProduct.BackColor = Color.Black;
            menuTabControl.SelectTab(tabProduct.TabIndex);
        }

        //private void tb_new_click(TabPage page)
        //{

        //    menuTabControl.SelectTab(page);

        //}

        private void tp_click(object sender, EventArgs e)
        {
            aOthersMethod.NumberPadClose();
            aOthersMethod.KeyBoardClose();
        }

        /// <summary>
        /// LoadLeftPackageForPackage
        /// </summary>
        /// <param name="aRecipePackage"></param>
        /// <param name="x"></param>
        /// <param name="categoryLength"></param>
        /// <param name="categoryTab"></param>
        /// <param name="leftCatPanel"></param>
        /// <param name="aReceipeTypeButton"></param>
        
        private void LoadLeftPackageForPackage(List<RecipePackageButton> aRecipePackage, int x, int categoryLength, int categoryTab, FlowLayoutPanel leftCatPanel, ReceipeTypeButton aReceipeTypeButton)
        {
            var aFlowLayoutPanel = new CustomFlowLayoutPanel();

            int lineHeight = 40;
            int cnt = 0;
            double buttonWidth = 0;
            bool flag = false;

            ReceipeTypeButton aReceipeType = aReceipeTypeButton;

            foreach (RecipePackageButton aRecipePackageButton in aRecipePackage)
            {
                try
                {
                    RecipePackageButton aReceipeMenuItemButton = aRecipePackageButton;
                    aReceipeMenuItemButton.FlatStyle = FlatStyle.Flat;
                    aReceipeMenuItemButton.FlatAppearance.BorderSize = 0;
                    aRecipePackageButton.Click += new EventHandler(RecipePackageButtonForNonTop_Click);
                    aRecipePackageButton.Margin = new Padding(0, 0, 0, 0);
                    aReceipeMenuItemButton.ForeColor = Color.White;
                    string font_size = urls.fontSize;
                    aReceipeMenuItemButton.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                        urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                    if (aReceipeType.PackageWidth == 1)
                    {
                        aReceipeType.PackageWidth = 1;
                    }
                    aReceipeMenuItemButton.Width = categoryLength;
                    {
                        aReceipeMenuItemButton.BackColor = Color.Teal;
                    }
                    if (aReceipeMenuItemButton.Height < 40)
                    {
                        aReceipeMenuItemButton.Height = 42;
                    }
                    buttonWidth += (aReceipeMenuItemButton.Width) + 2; // changed for button width
                    if (buttonWidth >= categoryLength)
                    {
                        lineHeight += aReceipeMenuItemButton.Height;
                        aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight);
                        buttonWidth = 0;
                        cnt++;
                    }
                    aReceipeMenuItemButton.Padding = new Padding(1, 1, 1, 1);
                    aReceipeMenuItemButton.Margin = new Padding(1, 1, 1, 1);
                    aFlowLayoutPanel.Controls.Add(aReceipeMenuItemButton);
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }

            aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight + 50);
            aFlowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
            aFlowLayoutPanel.Margin = new Padding(1, 1, 1, 1);
            aFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            aFlowLayoutPanel.Location = new Point(x, leftCatPanel.Location.Y + leftCatPanel.Size.Height);
            aFlowLayoutlist.Add(aFlowLayoutPanel);
        }

        /// <summary>
        /// LoadRightPackageForPackage
        /// </summary>
        /// <param name="aRecipePackage"></param>
        /// <param name="x"></param>
        /// <param name="categoryLength"></param>
        /// <param name="categoryTab"></param>
        /// <param name="rightCatPanel"></param>
        /// <param name="aReceipeTypeButton"></param>
        
        private void LoadRightPackageForPackage(List<RecipePackageButton> aRecipePackage, int x, int categoryLength, int categoryTab, FlowLayoutPanel rightCatPanel, ReceipeTypeButton aReceipeTypeButton)
        {
            var aFlowLayoutPanel = new CustomFlowLayoutPanel();

            int lineHeight = 40;
            int cnt = 0;
            double buttonWidth = 0;
            bool flag = false;

            ReceipeTypeButton aReceipeType = aReceipeTypeButton;

            foreach (RecipePackageButton aRecipePackageButton in aRecipePackage)
            {
                try
                {
                    RecipePackageButton aReceipeMenuItemButton = aRecipePackageButton;
                    aReceipeMenuItemButton.FlatStyle = FlatStyle.Flat;
                    aReceipeMenuItemButton.FlatAppearance.BorderSize = 0;
                    aRecipePackageButton.Click += new EventHandler(RecipePackageButtonForNonTop_Click);
                    aRecipePackageButton.Margin = new Padding(0, 0, 0, 0);
                    aReceipeMenuItemButton.ForeColor = Color.White;
                    string font_size = urls.fontSize;
                    aReceipeMenuItemButton.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                        urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);


                    if (aReceipeType.PackageWidth == 1)
                    {

                    }
                    aReceipeMenuItemButton.Width = categoryLength;

                    {
                        aReceipeMenuItemButton.BackColor = Color.Teal;
                    }
                    if (aReceipeMenuItemButton.Height < 40)
                    {
                        aReceipeMenuItemButton.Height = 42;
                    }
                    buttonWidth += (aReceipeMenuItemButton.Width) + 2; // changed for button width
                    if (buttonWidth >= categoryLength)
                    {
                        lineHeight += aReceipeMenuItemButton.Height;
                        aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight + 50);
                        buttonWidth = 0;
                        cnt++;
                    }
                    aReceipeMenuItemButton.Padding = new Padding(1, 1, 1, 1);
                    aReceipeMenuItemButton.Margin = new Padding(1, 1, 1, 1);
                    aFlowLayoutPanel.Controls.Add(aReceipeMenuItemButton);
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
            }

            aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight + 50);
            aFlowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
            aFlowLayoutPanel.Margin = new Padding(1, 1, 1, 1);
            aFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            aFlowLayoutPanel.Location = new Point(x, rightCatPanel.Location.Y + rightCatPanel.Size.Height);
            rightFlowLayoutlist.Add(aFlowLayoutPanel);
        }

        /// <summary>
        /// Load  MenuItem Load in Form Right Side Pannel
        /// 
       
        /// </summary>
        /// <param name="aReceipeCategoryButton"></param>
        /// <param name="x"></param>
        /// <param name="categoryLength"></param>
        /// <param name="categoryTab"></param>
        /// <param name="rightCatPanel"></param>

        private void LoadMenuItemForRightWhenFormLoad(ReceipeCategoryButton aReceipeCategoryButton, int x, int categoryLength, int categoryTab, Control rightCatPanel)
        {
            var aFlowLayoutPanel = new CustomFlowLayoutPanel();
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            int lineHeight = 15;
            int cnt = 0;
            double buttonWidth = 0;
            bool flag = false;

            List<ReceipeMenuItemButton> tempRecipeButton = new List<ReceipeMenuItemButton>();
            tempRecipeButton = allRecipeButton.Where(a => a.ShowCategory == aReceipeCategoryButton.CategoryId).ToList();
            tempRecipeButton = tempRecipeButton.OrderBy(a => a.ReceiptName).ToList();
            tempRecipeButton = tempRecipeButton.OrderBy(a => a.SortOrder).ToList();
            int maxRow = tempRecipeButton.Min(a => a.ButtonWidth);


            foreach (ReceipeMenuItemButton aReceipeMenuItemBut in tempRecipeButton)
            {
                ReceipeMenuItemButton aReceipeMenuItemButton = aReceipeMenuItemBut;

                aReceipeMenuItemButton.Click -= new EventHandler(ReceipeMenuItemButton_Click);
                aReceipeMenuItemButton.Click += new EventHandler(ReceipeMenuItemButton_Click);

                aReceipeMenuItemButton.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize),
                    urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);
                aReceipeMenuItemButton.FlatStyle = FlatStyle.Flat;
                aReceipeMenuItemButton.FlatAppearance.BorderSize = 0;
                aReceipeMenuItemButton.ForeColor = Color.White;
                ReceipeCategoryButton category =
                    allCategoryButton.FirstOrDefault(a => a.CategoryId == aReceipeMenuItemButton.CategoryId);
                aReceipeMenuItemButton.BackColor =
                    ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(category.Color));
                aReceipeMenuItemButton.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                aReceipeMenuItemButton.Padding = new Padding(1, 1, 1, 1);
                aReceipeMenuItemButton.Margin = new Padding(1, 1, 1, 1);

                aReceipeMenuItemButton.Width = (int)(aReceipeMenuItemButton.ButtonWidth * 1.0 * (categoryLength / 12.0)) - 1;
                aReceipeMenuItemButton.Height = aReceipeMenuItemButton.ButtonHeight;


                if (aReceipeMenuItemButton.Height == 200)
                {

                }

                if (aReceipeMenuItemButton.Height < 40)
                {
                    aReceipeMenuItemButton.Height = 42;
                }

                if (!flag)
                {
                    lineHeight = aReceipeMenuItemButton.Height;
                    flag = true;
                }

                buttonWidth += (aReceipeMenuItemButton.Width) + 2; // changed for button width
                if (buttonWidth >= categoryLength &&
                    tempRecipeButton[tempRecipeButton.Count() - 1] != aReceipeMenuItemButton)
                {
                    lineHeight += aReceipeMenuItemButton.Height;
                    aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight + 50);
                    buttonWidth = 0;
                    cnt++;
                }
                else if (buttonWidth >= categoryLength &&
                         tempRecipeButton[tempRecipeButton.Count() - 1] == aReceipeMenuItemButton)
                {
                    lineHeight += aReceipeMenuItemButton.Height;
                    aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight + 50);
                    buttonWidth = 0;
                    cnt++;
                }

                aFlowLayoutPanel.Controls.Add(aReceipeMenuItemButton);
            }

            if (lastCategory)
            {
                List<RecipePackageButton> aRecipePackage = aRestaurantMenuBll.GetPackageByMenuType(aReceipeCategoryButton.ReceipeTypeId);
                ReceipeCategoryButton subCategory = allCategoryButton.FirstOrDefault(a => a.ReceipeTypeId == aReceipeCategoryButton.ReceipeTypeId && a.HasSubcategory == 1);

                ReceipeTypeButton aReceipeType = allRecipeType.FirstOrDefault(a => a.TypeId == aReceipeCategoryButton.ReceipeTypeId);

                if (aRecipePackage.Count() > 0)
                {
                    ReceipeCategoryButton button = new ReceipeCategoryButton();
                    button.Text = "Package";
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    string font_size = "8"; // urls.fontSize;
                    button.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                        urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                    button.Height = 30;
                    if (aReceipeType.PackageWidth == 0)
                    {
                        aReceipeType.PackageWidth = 1;
                    }
                    button.Width = categoryLength;
                    button.BackColor = Color.BurlyWood;
                    button.ForeColor = Color.Black;
                    button.Padding = new Padding(1, 1, 1, 1);
                    button.Margin = new Padding(2, 3, 3, 1);

                    button.Font = new System.Drawing.Font(urls.fontFamily, Convert.ToInt32(urls.fontSize),
                        urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                    aFlowLayoutPanel.Controls.Add(button);
                    lineHeight += button.Height;
                    string an = button.Font.Size.ToString();
                    aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight + 50);
                }

                foreach (RecipePackageButton aRecipePackageButton in aRecipePackage)
                {
                    try
                    {
                        RecipePackageButton aReceipeMenuItemButton = aRecipePackageButton;
                        aReceipeMenuItemButton.FlatStyle = FlatStyle.Flat;
                        aReceipeMenuItemButton.FlatAppearance.BorderSize = 0;
                        aRecipePackageButton.Click += new EventHandler(RecipePackageButtonForNonTop_Click);
                        aRecipePackageButton.Margin = new Padding(0, 0, 0, 0);
                        aReceipeMenuItemButton.ForeColor = Color.White;
                        string font_size = urls.fontSize;
                        aReceipeMenuItemButton.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                            urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);


                        if (aReceipeType.PackageWidth == 0)
                        {
                            aReceipeType.PackageWidth = 1;
                        }
                        aReceipeMenuItemButton.Width = categoryLength;
                        if (subCategory != null)
                        {
                            aReceipeMenuItemButton.BackColor =
                                ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(subCategory.Color));
                        }
                        else
                        {
                            aReceipeMenuItemButton.BackColor = Color.Teal;
                        }
                        if (aReceipeMenuItemButton.Height < 40)
                        {
                            aReceipeMenuItemButton.Height = 42;
                        }
                        buttonWidth += (aReceipeMenuItemButton.Width) + 2; // changed for button width
                        if (buttonWidth >= categoryLength)
                        {
                            lineHeight += aReceipeMenuItemButton.Height;
                            aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight);
                            buttonWidth = 0;
                            cnt++;
                        }
                        aReceipeMenuItemButton.Padding = new Padding(1, 1, 1, 1);
                        aReceipeMenuItemButton.Margin = new Padding(1, 1, 1, 1);
                        aFlowLayoutPanel.Controls.Add(aReceipeMenuItemButton);

                    }
                    catch
                    {

                    }
                }
            }
            aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight + 50);
            aFlowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
            aFlowLayoutPanel.Margin = new Padding(1, 1, 1, 1);
            aFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            aFlowLayoutPanel.Location = new Point(x, rightCatPanel.Location.Y + rightCatPanel.Size.Height);
            rightFlowLayoutlist.Add(aFlowLayoutPanel);
        }

        /// <summary>
        /// Load All Menu Item when Form Load
        /// </summary>
        /// <param name="aReceipeCategoryButton"></param>
        /// <param name="x"></param>
        /// <param name="categoryLength"></param>
        /// <param name="categoryTab"></param>
        /// <param name="leftCatPanel"></param>
        
        private void LoadMenuItemWhenFormLoad(ReceipeCategoryButton aReceipeCategoryButton, int x, int categoryLength, int categoryTab, Control leftCatPanel)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            var aFlowLayoutPanel = new CustomFlowLayoutPanel();
            //aFlowLayoutPanel.BackColor = Color.Honeydew;

            int lineHeight = 15;
            int cnt = 0;
            double buttonWidth = 0;
            bool flag = false;
            int singleHeight = 0;
            var sfiestlist = new List<ReceipeMenuItemButton>(allRecipeButton).Where(a => a.SubCategoryId <= 0);
            List<ReceipeMenuItemButton> tempRecipeButton1 = new List<ReceipeMenuItemButton>();
            var tempRecipeButton2 = new List<ReceipeMenuItemButton>();
            foreach (ReceipeMenuItemButton item in sfiestlist)
            {
                if (item.ShowCategory == aReceipeCategoryButton.CategoryId)
                {
                    tempRecipeButton1.Add(item);
                }
            }

            tempRecipeButton1 = tempRecipeButton1.OrderBy(a => a.ReceiptName).ToList();
            tempRecipeButton1 = tempRecipeButton1.OrderBy(a => a.SortOrder).ToList();
            foreach (ReceipeMenuItemButton rcItem in tempRecipeButton1)
            {
                ReceipeMenuItemButton aReceipeMenuItemButton = rcItem;
                aReceipeMenuItemButton.Click -= new EventHandler(ReceipeMenuItemButton_Click);
                aReceipeMenuItemButton.Click += new EventHandler(ReceipeMenuItemButton_Click);
                aReceipeMenuItemButton.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                ReceipeCategoryButton category = allCategoryButton.FirstOrDefault(a => a.CategoryId == aReceipeMenuItemButton.CategoryId);
                aReceipeMenuItemButton.Width = (int)(aReceipeMenuItemButton.ButtonWidth * 1.0 * (categoryLength / 12.0)) - 1;
                aReceipeMenuItemButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(category.Color));
                if (aReceipeMenuItemButton.Height < 40)
                {
                    aReceipeMenuItemButton.Height = 42;
                }
                if (!flag)
                {
                    lineHeight = aReceipeMenuItemButton.Height;
                    singleHeight = aReceipeMenuItemButton.Height;
                    flag = true;
                }
                aReceipeMenuItemButton.Padding = new Padding(1, 1, 1, 1);
                aReceipeMenuItemButton.Margin = new Padding(1, 1, 1, 1);
                buttonWidth += (aReceipeMenuItemButton.Width) + 2; // changed for button width
                if (buttonWidth >= categoryLength && tempRecipeButton1[tempRecipeButton1.Count() - 1] != rcItem)
                {
                    lineHeight += aReceipeMenuItemButton.Height;
                    aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight);
                    buttonWidth = 0;
                    cnt++;
                }
                aFlowLayoutPanel.Controls.Add(aReceipeMenuItemButton);
            }

            if (lastCategory)
            {
                List<RecipePackageButton> aRecipePackage =
                    aRestaurantMenuBll.GetPackageByMenuType(aReceipeCategoryButton.ReceipeTypeId);
                ReceipeCategoryButton subCategory =
                    allCategoryButton.FirstOrDefault(
                        a => a.ReceipeTypeId == aReceipeCategoryButton.ReceipeTypeId && a.HasSubcategory == 1);

                ReceipeTypeButton aReceipeType =
                    allRecipeType.FirstOrDefault(a => a.TypeId == aReceipeCategoryButton.ReceipeTypeId);

                if (aRecipePackage.Count() > 0)
                {
                    ReceipeCategoryButton button = new ReceipeCategoryButton();
                    button.Text = "Package";
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.Height = aReceipeCategoryButton.Height;
                    string font_size = "8"; // urls.fontSize;
                    button.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                        urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                    if (aReceipeType.PackageWidth == 0)
                    {
                        aReceipeType.PackageWidth = 1;
                    }
                    button.Width = categoryLength;
                    button.BackColor = Color.BurlyWood;
                    button.ForeColor = Color.Black;
                    button.Padding = new Padding(1, 1, 1, 1);
                    button.Margin = new Padding(2, 3, 3, 1);
                    aFlowLayoutPanel.Controls.Add(button);
                    lineHeight += button.Height;
                    aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight + 50);
                }

                foreach (RecipePackageButton aRecipePackageButton in aRecipePackage)
                {
                    try
                    {
                        RecipePackageButton aReceipeMenuItemButton = aRecipePackageButton;

                        aReceipeMenuItemButton.FlatStyle = FlatStyle.Flat;
                        aReceipeMenuItemButton.FlatAppearance.BorderSize = 0;
                        aRecipePackageButton.Click += new EventHandler(RecipePackageButtonForNonTop_Click);
                        aRecipePackageButton.Margin = new Padding(0, 0, 0, 0);
                        aReceipeMenuItemButton.ForeColor = Color.White;
                        string font_size = urls.fontSize;
                        aReceipeMenuItemButton.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                            urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                        if (aReceipeType.PackageWidth == 1)
                        {
                            aReceipeType.PackageWidth = 12;
                        }
                        aReceipeMenuItemButton.Width = categoryLength;
                        if (subCategory != null)
                        {
                            aReceipeMenuItemButton.BackColor =
                                ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(subCategory.Color));
                        }
                        else
                        {
                            aReceipeMenuItemButton.BackColor = Color.Teal;
                        }

                        if (aReceipeMenuItemButton.Height < 40)
                        {
                            aReceipeMenuItemButton.Height = 42;
                        }
                        buttonWidth += (aReceipeMenuItemButton.Width) + 2; // changed for button width
                        if (buttonWidth >= categoryLength)
                        {
                            lineHeight += aReceipeMenuItemButton.Height;
                            aFlowLayoutPanel.Size = new Size(categoryLength + categoryTab, lineHeight);
                            buttonWidth = 0;
                            cnt++;
                        }
                        aReceipeMenuItemButton.Padding = new Padding(1, 1, 1, 1);
                        aReceipeMenuItemButton.Margin = new Padding(1, 1, 1, 1);
                        aFlowLayoutPanel.Controls.Add(aReceipeMenuItemButton);
                    }
                    catch
                    {

                    }
                }
            }

            aFlowLayoutPanel.Size = new Size(categoryLength + (categoryTab), lineHeight + 50);
            aFlowLayoutPanel.Padding = new Padding(0, 0, 0, 0);
            aFlowLayoutPanel.Margin = new Padding(1, 1, 1, 1);
            aFlowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            aFlowLayoutPanel.Location = new Point(x, leftCatPanel.Location.Y + leftCatPanel.Size.Height);
            aFlowLayoutlist.Add(aFlowLayoutPanel);
        }

        private FlowLayoutPanel GetSubCategory(ReceipeTypeButton aReceipeTypeButton)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            FlowLayoutPanel rightItemflowLayoutPanel1 = new FlowLayoutPanel();
            rightItemflowLayoutPanel1.Location = subCategoryMenuItemFlowLayoutPanel.Location;
            rightItemflowLayoutPanel1.Size = new Size(0, 0);
            rightItemflowLayoutPanel1.AutoSize = false;
            rightItemflowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            rightItemflowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rightItemflowLayoutPanel1.Padding = new Padding(0, 0, 0, 0);
            int buttonWidth = 0;
            int lineHeight = 40;
            int cnt = 0;
            bool flag = false;
            int hi = 0;

            List<ReceipeSubCategoryButton> tempSubcategory =
                allSubcategoryButton.Where(a => a.RecipeTypeId == aReceipeTypeButton.TypeId).ToList();

            foreach (ReceipeSubCategoryButton aReceipeSubCategoryButton in tempSubcategory)
            {
                string font_size = urls.fontSize;
                aReceipeSubCategoryButton.Font = new System.Drawing.Font(urls.fontFamily, int.Parse(font_size),
                    urls.fontStyle == "Normal" ? FontStyle.Regular : FontStyle.Bold);

                aReceipeSubCategoryButton.Click -= new EventHandler(ReceipeSubCategoryButton_Click);
                aReceipeSubCategoryButton.Click += new EventHandler(ReceipeSubCategoryButton_Click);
                aReceipeSubCategoryButton.BackColor =
                    ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(aReceipeSubCategoryButton.ButtonColor));
                int categoryWidth = (int)Math.Floor(aReceipeTypeButton.SubcategoryWidth * 1.0 * (leftPanelSize / 12.0));
                //     aReceipeSubCategoryButton.Padding = new Padding(1, 0, 1, 2);

                aReceipeSubCategoryButton.Margin = new Padding(1, 1, 1, 2);

                if (aReceipeSubCategoryButton.Height < 40)
                {
                    aReceipeSubCategoryButton.Height = 42;
                }
                if (!flag)
                {
                    lineHeight = aReceipeSubCategoryButton.Height;
                    flag = true;
                }

                aReceipeSubCategoryButton.Width =
                    (int)Math.Floor(aReceipeSubCategoryButton.ButtonWidth * 1.0 * (categoryWidth / 12)) - 1;
                rightItemflowLayoutPanel1.Controls.Add(aReceipeSubCategoryButton);
                buttonWidth += (int)Math.Floor(aReceipeSubCategoryButton.ButtonWidth * 1.0 * (categoryWidth / 12.0)) +
                               aReceipeTypeButton.SubcategoryWidth;
                if (hi < Math.Floor(12.0 / aReceipeTypeButton.SubcategoryWidth))
                {
                    hi = (int)Math.Floor(12.0 / aReceipeTypeButton.SubcategoryWidth);
                }
                cnt++;
                if (buttonWidth >= categoryWidth &&
                    tempSubcategory[tempSubcategory.Count() - 1] != aReceipeSubCategoryButton)
                {
                    lineHeight += aReceipeSubCategoryButton.Height;
                    rightItemflowLayoutPanel1.Size = new Size(categoryWidth + aReceipeTypeButton.SubcategoryWidth,
                        lineHeight);
                    cnt = 0;
                    buttonWidth = 0;
                }
            }

            rightItemflowLayoutPanel1.Size = new Size(rightItemflowLayoutPanel1.Width, lineHeight + 50);

            return rightItemflowLayoutPanel1;
        }

        #endregion

        #region Clear All Data Into Control

        private void ClearAllItemsIntoFlowlayoutPanel()
        {
            leftRecipeCategoryFlowLayoutPanel.Controls.Clear();
            leftCatItemflowLayoutPanel1.Controls.Clear();
            hasSubCategoryFlowLayoutPanel.Controls.Clear();
            rightRecipeCategoryFlowLayoutPanel.Controls.Clear();
            rightCatItemflowLayoutPanel1.Controls.Clear();

            RemoveControls(leftRecipeCategoryFlowLayoutPanel);
            RemoveControls(leftCatItemflowLayoutPanel1);
            RemoveControls(hasSubCategoryFlowLayoutPanel);
            RemoveControls(rightRecipeCategoryFlowLayoutPanel);
            RemoveControls(rightCatItemflowLayoutPanel1);
        }

        private void ClearAllFlowLayout()
        {
            leftRecipeCategoryFlowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            leftCatItemflowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            leftCategoryPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            leftRecipeCategoryFlowLayoutPanel.Size = new Size(0, 0);
            leftCatItemflowLayoutPanel1.Size = new Size(0, 0);
            leftCategoryPanel.Size = new Size(0, 0);

            hasSubCategoryFlowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            subCategoryMenuItemFlowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            subCategoryPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            hasSubCategoryFlowLayoutPanel.Size = new Size(0, 0);
            subCategoryMenuItemFlowLayoutPanel.Size = new Size(0, 0);
            subCategoryPanel.Size = new Size(0, 0);

            rightRecipeCategoryFlowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rightCatItemflowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rightCategoryPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rightRecipeCategoryFlowLayoutPanel.Size = new Size(0, 0);
            rightCatItemflowLayoutPanel1.Size = new Size(0, 0);
            rightCategoryPanel.Size = new Size(0, 0);
        }

        private void RemoveControls(CustomFlowLayoutPanel rightCatItemflowLayoutPanel11)
        {
            List<Control> listControls = new List<Control>();

            foreach (Control control in rightCatItemflowLayoutPanel11.Controls)
            {
                listControls.Add(control);
            }

            foreach (Control control in listControls)
            {
                rightCatItemflowLayoutPanel11.Controls.Remove(control);
                control.Dispose();
            }
        }

        private void ClearAllCartSelection()
        {
            foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
                {
                    c.BackColor = PackItemsControl.DefaultBackColor;
                }

                cc.BackColor = PackageDetails.DefaultBackColor;
            }
            foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
            {
                foreach (deatilsControls c in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                {
                    c.BackColor = deatilsControls.DefaultBackColor;
                }
            }

            foreach (MultiplePartControl cc in orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
            {
                cc.packageItemsFlowLayoutPanel.BackColor = MultiplePartControl.DefaultBackColor;
                foreach (Label c in cc.packageItemsFlowLayoutPanel.Controls.OfType<Label>())
                {
                    c.BackColor = MultiplePartControl.DefaultBackColor;
                }
                cc.BackColor = MultiplePartControl.DefaultBackColor;
            }
        }

        #endregion

        private bool ShowCategory(ReceipeCategoryButton aReceipeCategoryButton)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            return aRestaurantMenuBll.IsShowCategory(aReceipeCategoryButton, allRecipeButton);
        }

        #region AllOptionMethod Here
        private int GetAllItemWithOption(ReceipeMenuItemButton aReceipeMenuItemButton)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            int itemIndex = 0;
            bool flag = aRestaurantMenuBll.GetReceipeOptionsByItemId(aReceipeMenuItemButton.RecipeMenuItemId);
            ItemOptionForm.Status = "";
            ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();
            if (flag)
            {
                ItemOptionForm.Status = "";
                ItemOptionForm.aRecipeList = new List<RecipeOptionItemButton>();
                ItemOptionForm aItemOptionForm = new ItemOptionForm(aReceipeMenuItemButton.RecipeMenuItemId);
                aItemOptionForm.ShowDialog();
            }
            if ((ItemOptionForm.Status == "cancel" || ItemOptionForm.Status == "") && flag) return 0;
            List<RecipeOptionItemButton> aRecipeList = ItemOptionForm.aRecipeList;
            int optionIndex = GetOptionIndex();
            OrderItemDetailsMD aOrderItemDetails = new OrderItemDetailsMD();
            aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
            aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
            aOrderItemDetails.ItemName = aReceipeMenuItemButton.ReceiptName;
            aOrderItemDetails.ItemFullName = aReceipeMenuItemButton.ShortDescrip;
            aOrderItemDetails.OptionsIndex = optionIndex + 1;
            aOrderItemDetails.KitchenSection = aReceipeMenuItemButton.KitchenSection;

            if (deliveryButton.Text == "RES")
            {
                aOrderItemDetails.Price = aReceipeMenuItemButton.InPrice;
            }
            else
            {
                aOrderItemDetails.Price = aReceipeMenuItemButton.OutPrice;
            }

            aOrderItemDetails.Qty = 1;
            aOrderItemDetails.RecipeTypeId = aReceipeMenuItemButton.RecipeTypeId;
            aOrderItemDetails.SortOrder = aReceipeMenuItemButton.SortOrder;

            ReceipeCategoryButton aReceipeCategoryButton = aRestaurantMenuBll.GetCategoryByCategoryId(aReceipeMenuItemButton.CategoryId);
            aOrderItemDetails.CatSortOrder = aReceipeCategoryButton.SortOrder;
            aOrderItemDetails.TableNumber = 1;
            int index = CheckDulicate(aOrderItemDetails, aRecipeList);

            if (index > 0)
            {
                itemIndex = index;
                List<RecipeOptionMD> aRList = aRecipeOptionMdList.Where(a => a.OptionsIndex == index).ToList();
                foreach (RecipeOptionMD list in aRList)
                {
                    list.Qty += 1;
                }

                OrderItemDetailsMD tempOrderItemDetails =  aOrderItemDetailsMDList.SingleOrDefault(a => a.OptionsIndex == index);
                tempOrderItemDetails.Qty += 1;
            }
            else if (index <= 0)
            {
                itemIndex = optionIndex + 1;
                aOrderItemDetailsMDList.Add(aOrderItemDetails);
                foreach (RecipeOptionItemButton recipe in aRecipeList)
                {
                    RecipeOptionMD aOptionMD = new RecipeOptionMD();
                    aOptionMD.RecipeId = aReceipeMenuItemButton.RecipeMenuItemId;
                    aOptionMD.TableNumber = 1;
                    aOptionMD.RecipeOptionId = recipe.RecipeOptionId;
                    if (!string.IsNullOrEmpty(recipe.MinusTitle))
                    {
                        aOptionMD.MinusOption = recipe.MinusTitle;
                    }
                    else if (!string.IsNullOrEmpty(recipe.Title))
                    {
                        aOptionMD.Title = recipe.Title;
                    }

                    aOptionMD.Type = recipe.RecipeOptionButton.Type;
                    if (deliveryButton.Text == "RES")
                    {
                        aOptionMD.Price = recipe.InPrice;
                        aOptionMD.InPrice = recipe.InPrice;
                    }
                    else
                    {
                        aOptionMD.Price = recipe.Price;
                        aOptionMD.InPrice = recipe.Price;
                    }

                    aOptionMD.Qty = 1;
                    aOptionMD.OptionsIndex = optionIndex + 1;
                    aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                    aRecipeOptionMdList.Add(aOptionMD);
                }
            }
            return itemIndex;
        }

        public int GetOptionIndex()
        {
            int res = 0;
            int index = 0;
            index = aOrderItemDetailsMDList.Count == 0 ? 0 : aOrderItemDetailsMDList.Max(a => a.OptionsIndex);
            int index2 = 0;
            index2 = aRecipePackageMdList.Count == 0 ? 0 : aRecipePackageMdList.Max(a => a.OptionsIndex);
            int index3 = aRecipeMultipleMdList.Count == 0 ? 0 : aRecipeMultipleMdList.Max(a => a.OptionsIndex);
            res = index > index2 ? index : index2;
            return res > index3 ? res : index3;
        }

        public int GetPackageItemOptionIndex(RecipePackageMD package)
        {
            int index = 1;
            int index2 = aPackageItemMdList.Count == 0 ? 0 : aPackageItemMdList.Where(a=>a.OptionsIndex <= package.OptionsIndex).Max(a => a.PackageItemOptionsIndex);
            index2++;
            return index2 >index? index2: index;
        }

        #endregion

        public void LoadOrderDetails(int itemIndex, string itemName = "", int menuId = 0)
        {
            if (itemIndex > 0)
            {
                LoadItemDetails(itemIndex, itemName, menuId);
                //customPanel.Location = new Point(customPanel.Location.X,
                //orderDetailsflowLayoutPanel1.Location.Y + orderDetailsflowLayoutPanel1.Size.Height);
                //paymentDetailsPanel.Location = new Point(paymentDetailsPanel.Location.X,
                //customPanel.Location.Y + customPanel.Size.Height);
            }
            LoadAmountDetails();
        }

        /// <summary>
        /// LoadItem Detials information
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <param name="itemName"></param>
        /// <param name="menuId"></param>
        ///

        private void LoadItemDetails(int itemIndex, string itemName = "", int menuId = 0) // chnages for package
        {
            bool flag = false;
            bool sameFlag = false;
            int isCartSelection = 0;

            if (!sameFlag)
            {
                foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
                {
                    foreach (deatilsControls control in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                    {
                        if (control.OptionIndex == itemIndex && !flag)
                        {
                            flag = true;
                            deatilsControls details = control;
                            double qty = Convert.ToDouble(details.qtyTextBox.Text);
                            double price = Convert.ToDouble(details.priceTextBox.Text);
                            control.qtyTextBox.Text = (qty + 1).ToString();
                            control.totalPriceLabel.Text = ((qty + 1) * price).ToString();
                            control1.typeflowLayoutPanel1.Controls.SetChildIndex(control, 0);
                            LoadAmountDetails();
                            orderDetailsflowLayoutPanel1.Controls.SetChildIndex(control1, 0);
                        }
                    } 
                    double amount = aOrderItemDetailsMDList.Where(b => b.RecipeTypeId == control1.RecipeTypeId)
                            .Sum(a => a.Price * a.Qty);
                    control1.recipeTypeAmountlabel.Text = amount.ToString("F02");
                }
                if (!flag)
                {
                    foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
                    {
                        if (cc.OptionIndex == itemIndex && cc.ItemLimit <= 0)
                        {
                            flag = true;
                            var items = cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>();
                            double qty = Convert.ToDouble(cc.qtyTextBox.Text);
                            double price = Convert.ToDouble(cc.priceTextBox.Text);

                            cc.qtyTextBox.Text = (qty + 1).ToString();
                            cc.totalPriceLabel.Text = ((qty + 1) * price).ToString();

                            foreach (var item in items)
                            {
                                item.qtyTextBox.Text = (Convert.ToInt32(item.qtyTextBox.Text) + 1).ToString();
                            }
                            LoadAmountDetails();
                        }
                    }
                }

                if (!flag)
                {
                    foreach (MultiplePartControl cc in orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
                    {
                        if (cc.OptionIndex == itemIndex)
                        {
                            flag = true;
                            double qty = Convert.ToDouble(cc.qtyTextBox.Text);
                            double price = Convert.ToDouble(cc.priceTextBox.Text);
                            cc.qtyTextBox.Text = (qty + 1).ToString();
                            cc.totalPriceLabel.Text = ((qty + 1) * price).ToString();
                            LoadAmountDetails();
                        }
                    }
                }
                
                if (!flag)
                {
                    isCartSelection = NewItemAdd(itemIndex, itemName, menuId);
                }
            }
            if (isCartSelection <= 0)
            {
                foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
                {
                    foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
                    {                                                                                                 
                        c.BackColor = PackItemsControl.DefaultBackColor;
                    }
                    cc.BackColor = PackageDetails.DefaultBackColor;
                }
            }
            foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
            {
                foreach (deatilsControls c in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                {
                    c.BackColor = deatilsControls.DefaultBackColor;
                }
            }

            foreach (MultiplePartControl cc in orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
            {
                cc.packageItemsFlowLayoutPanel.BackColor = MultiplePartControl.DefaultBackColor;
                foreach (Label c in cc.packageItemsFlowLayoutPanel.Controls.OfType<Label>())
                {
                    c.BackColor = MultiplePartControl.DefaultBackColor;
                }
                cc.BackColor = MultiplePartControl.DefaultBackColor;
            }
        }

        private static TextBox GetNameTextBox(deatilsControls control)
        {
            return control.nameTextBox;
        }

        /// <summary>
        /// Add New Item into the Cart
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <param name="itemName"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        
        private int NewItemAdd(int itemIndex, string itemName = "", int menuId = 0)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            var itemDetails = aOrderItemDetailsMDList.SingleOrDefault(a => a.OptionsIndex == itemIndex);
            var multiplePartDetails = aRecipeMultipleMdList.SingleOrDefault(a => a.OptionsIndex == itemIndex);
            if (itemDetails != null && itemDetails.ItemId > 0)
            {
                RecipeTypeDetails type =
                    orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>()
                        .FirstOrDefault(a => a.RecipeTypeId == itemDetails.RecipeTypeId);
                List<RecipeOptionMD> aRList =
                    aRecipeOptionMdList.Where(a => a.OptionsIndex == itemDetails.OptionsIndex).ToList();

                double optionPrice = GetOptionPrice(aRList);
                double price = 0;
                deatilsControls aDetailsCon = new deatilsControls();
                //if (itemDetails.ItemName == itemName)
                //{ 
                    aDetailsCon.ItemId = itemDetails.ItemId;
                    aDetailsCon.OptionIndex = itemDetails.OptionsIndex;
                    aDetailsCon.qtyTextBox.Text = itemDetails.Qty.ToString();
                    aDetailsCon.nameTextBox.Text = itemDetails.ItemName;
                    aDetailsCon.optionItemLabel.Text = "";
                    aDetailsCon.CategoryId = itemDetails.CategoryId;
                    if (aRList.Count == 0) aDetailsCon.optionItemLabel.Visible = false;
                    foreach (RecipeOptionMD list in aRList)
                    {
                        if (!string.IsNullOrEmpty(list.MinusOption))
                        {

                            if (list.InPrice > 0)
                            {
                                aDetailsCon.optionItemLabel.Text += ("→No " + list.MinusOption) + "\r\n";
                            }
                            else aDetailsCon.optionItemLabel.Text += ("→No " + list.MinusOption) + "\r\n";
                        }

                        if (!string.IsNullOrEmpty(list.Title))
                        {
                            price += (list.Qty * list.Price);
                            if (list.InPrice > 0)
                            {
                                aDetailsCon.optionItemLabel.Text += "→" + (list.Title) + "\r\n";
                            }
                            else aDetailsCon.optionItemLabel.Text += "→" + (list.Title) + "\r\n";
                        }
                    }
                    aDetailsCon.priceTextBox.Text = (itemDetails.Price + price).ToString("F02");
                    aDetailsCon.totalPriceLabel.Text = ((itemDetails.Price) * itemDetails.Qty).ToString("F02");
              
                aDetailsCon.MouseClick += UsersGrid_WasClicked;

                if (orderDetailsflowLayoutPanel1.Controls.Count > 0)
                {
                    System.Windows.Forms.Control aControl = orderDetailsflowLayoutPanel1.Controls[0];
                    previousControl = aControl;
                }
                else
                {
                    previousControl = null;
                }
                RecipeTypeDetails typeDetails = new RecipeTypeDetails();
                if (previousControl != null)
                {
                    if (type == null || type.RecipeTypeId <= 0)
                    {
                        ReceipeTypeButton typeButton = allRecipeType.SingleOrDefault(a => a.TypeId == itemDetails.RecipeTypeId);
                        if (typeButton != null)
                        {
                            if (typeButton.MergeItems != 0)
                            {

                                typeDetails.recipeTypelabel.Text = typeButton.TypeName;

                                double amount = aOrderItemDetailsMDList.Where(b => b.RecipeTypeId == itemDetails.RecipeTypeId).Sum(a => a.Price * a.Qty);
                                typeDetails.recipeTypeAmountlabel.Text = amount.ToString("F02");
                                typeDetails.recipeTypeAmountlabel.Visible = false;
                                typeDetails.ReceipeTypeButton = typeButton;
                            }
                            else
                            {

                                typeDetails.typeflowLayoutPanel1.Location = new Point(2, 0);
                                typeDetails.recipeTypelabel.Size = new System.Drawing.Size(0, 0);
                                typeDetails.recipeTypelabel.Visible = false;
                                typeDetails.recipeTypeAmountlabel.Size = new System.Drawing.Size(0, 0);
                                typeDetails.recipeTypeAmountlabel.Visible = false;
                                typeDetails.ReceipeTypeButton = typeButton;
                            }
                        }
                        typeDetails.RecipeTypeId = itemDetails.RecipeTypeId;
                        aRecipeTypes.Add(typeDetails);
                        typeDetails.typeflowLayoutPanel1.Controls.Add(aDetailsCon);
                        var prevIndex = orderDetailsflowLayoutPanel1.Controls.IndexOf(previousControl as System.Windows.Forms.Control);
                        orderDetailsflowLayoutPanel1.Controls.Add(typeDetails);
                        orderDetailsflowLayoutPanel1.Controls.SetChildIndex(typeDetails, prevIndex);
                        previousControl = typeDetails;
                    }

                    else
                    {
                        foreach (RecipeTypeDetails control in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
                        {
                            if (control.RecipeTypeId == itemDetails.RecipeTypeId)// && itemDetails.ItemName == itemName)
                            {
                                control.typeflowLayoutPanel1.Controls.Add(aDetailsCon);
                                control.typeflowLayoutPanel1.Controls.SetChildIndex(aDetailsCon, 0);
                                double amount = aOrderItemDetailsMDList.Where(b => b.RecipeTypeId == itemDetails.RecipeTypeId).Sum(a => a.Price * a.Qty);
                                control.recipeTypeAmountlabel.Text = amount.ToString("F02");
                            }
                        }
                    }
                }

                else
                {
                    ReceipeTypeButton typeButton =
                        allRecipeType.SingleOrDefault(a => a.TypeId == itemDetails.RecipeTypeId);
                    if (typeButton != null)
                    {
                        if (typeButton.MergeItems != 0)
                        {
                            typeDetails.ReceipeTypeButton = typeButton;
                            typeDetails.recipeTypelabel.Text = typeButton.TypeName;
                            double amount =
                                aOrderItemDetailsMDList.Where(b => b.RecipeTypeId == itemDetails.RecipeTypeId)
                                    .Sum(a => a.Price * a.Qty);
                            typeDetails.recipeTypeAmountlabel.Text = amount.ToString("F02");
                        }
                        else
                        {
                            typeDetails.typeflowLayoutPanel1.Location = new Point(2, 0);
                            typeDetails.recipeTypelabel.Size = new System.Drawing.Size(0, 0);
                            typeDetails.recipeTypelabel.Visible = false;
                            typeDetails.recipeTypeAmountlabel.Size = new System.Drawing.Size(0, 0);
                            typeDetails.recipeTypeAmountlabel.Visible = false;
                            typeDetails.ReceipeTypeButton = typeButton;
                        }
                    }

                    typeDetails.RecipeTypeId = itemDetails.RecipeTypeId;
                    aRecipeTypes.Add(typeDetails);
                    typeDetails.typeflowLayoutPanel1.Controls.Add(aDetailsCon);
                    var prevIndex =
                        orderDetailsflowLayoutPanel1.Controls.IndexOf(previousControl as System.Windows.Forms.Control);

                    orderDetailsflowLayoutPanel1.Controls.Add(typeDetails);
                    orderDetailsflowLayoutPanel1.Controls.SetChildIndex(typeDetails, prevIndex);
                    previousControl = typeDetails;

                }
                return 0;
            }
            else if (multiplePartDetails != null && multiplePartDetails.CategoryId > 0)
            {
                RecipeMultipleMD aRecipeMultipleMd =
                    aRecipeMultipleMdList.FirstOrDefault(a => a.OptionsIndex == itemIndex);
                List<MultipleItemMD> aMultipleItemList =
                    aMultipleItemMdList.Where(a => a.OptionsIndex == aRecipeMultipleMd.OptionsIndex).ToList();
                List<RecipeOptionMD> aRList =
                    aRecipeOptionMdList.Where(a => a.OptionsIndex == aRecipeMultipleMd.OptionsIndex).ToList();

                double optionPrice1 = GetOptionPrice(aRList);
                MultiplePartControl aMultiplePartControl = new MultiplePartControl();
                aMultiplePartControl.OptionIndex = aRecipeMultipleMd.OptionsIndex;
                aMultiplePartControl.qtyTextBox.Text = aRecipeMultipleMd.Qty.ToString();
                aMultiplePartControl.nameTextBox.Text = aRecipeMultipleMd.MultiplePartName;
                aMultiplePartControl.priceTextBox.Text = (aRecipeMultipleMd.UnitPrice).ToString("F02");

                aMultiplePartControl.CategoryId = aRecipeMultipleMd.CategoryId;

                int cnt = 0;
                double price = 0;
                foreach (MultipleItemMD itemlist in aMultipleItemList)
                {
                    cnt++;
                    Label aLabel = new Label();
                    aLabel.Name = aMultiplePartControl.OptionIndex.ToString();
                    aLabel.AutoSize = true;
                    aLabel.Text = GetOrdinalSuffix(cnt) + ": " + itemlist.ItemName + "\r\n";
                    List<RecipeOptionMD> aMultipleList =
                        aRecipeOptionMdList.Where(
                            a => a.OptionsIndex == aRecipeMultipleMd.OptionsIndex && a.RecipeId == itemlist.ItemId)
                            .ToList();
                    foreach (RecipeOptionMD list in aMultipleList)
                    {
                        if (!string.IsNullOrEmpty(list.MinusOption))
                        {
                            if (list.InPrice > 0)
                            {
                                aLabel.Text += ("   →No " + list.MinusOption) + "\r\n";
                            }
                            else aLabel.Text += ("   →No " + list.MinusOption) + "\r\n";
                        }
                        if (!string.IsNullOrEmpty(list.Title))
                        {
                            price += (list.Qty * list.Price);
                            if (list.InPrice > 0)
                            {
                                aLabel.Text += "   →" + (list.Title) + "\r\n";
                            }
                            else aLabel.Text += "   →" + (list.Title) + "\r\n";
                        }
                    }

                    aMultiplePartControl.priceTextBox.Text = (aRecipeMultipleMd.UnitPrice + price).ToString("F02");

                    aMultiplePartControl.totalPriceLabel.Text =
                        ((aRecipeMultipleMd.UnitPrice) * aRecipeMultipleMd.Qty).ToString("F02");
                    aLabel.MouseClick += aLabel_MouseClick;
                    aMultiplePartControl.packageItemsFlowLayoutPanel.Controls.Add(aLabel);
                }
                orderDetailsflowLayoutPanel1.Controls.Add(aMultiplePartControl);

                return 0;
            }
            else
            {
                var packagetemp = aRecipePackageMdList.FirstOrDefault(a => a.OptionsIndex == itemIndex);
                //PackageDetails tempPackageDetails = orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>().ToList().FirstOrDefault(a => packagetemp != null && a.PackageId == packagetemp.PackageId && a.OptionIndex == packagetemp.OptionsIndex);
                PackageDetails tempPackageDetails = orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>().FirstOrDefault(a => a.PackageId == packagetemp.PackageId && a.OptionIndex == packagetemp.OptionsIndex);
                if (packagetemp != null && (tempPackageDetails != null && packagetemp.PackageId > 0))
                {
                    //int limit = Convert.ToInt16(tempPackageDetails.qtyTextBox.Text) * tempPackageDetails.ItemLimit;
                    List<PackageItem> itemList = aPackageItemMdList.Where(a => a.PackageId == tempPackageDetails.PackageId && a.OptionsIndex == itemIndex).ToList(); // && a.OptionsIndex == itemIndex

                    foreach (PackageItem item in itemList)
                    {

                        item.DeleteItem = true;
                       var packageItem = tempPackageDetails.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>().FirstOrDefault(a => a.ItemId == item.ItemId && a.PackageItemOptionIndex == item.PackageItemOptionsIndex);
                      //  var packageItem = tempPackageDetails.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>().FirstOrDefault(a => a.ItemId == item.ItemId);
                        if (packageItem != null && packageItem.ItemId > 0)
                        {
                            packageItem.qtyTextBox.Text = (item.Qty).ToString();
                        }
                        else
                        {
                            PackItemsControl aControl = new PackItemsControl();
                            aControl.PackageItemOptionIndex = item.PackageItemOptionsIndex;
                            aControl.qtyTextBox.Text = item.Qty.ToString();
                            aControl.nameTextBox.Text = item.ItemName;
                            aControl.MouseClick += UsersGridForPackageItem_WasClicked;
                            aControl.OptionIndex = item.OptionsIndex;//tempPackageDetails.OptionIndex;
                            aControl.PackageId = tempPackageDetails.PackageId;
                            aControl.ItemId = item.ItemId;
                            
                            item.Price = aRestaurantMenuBll.GetPrice(item.CategoryId, item.SubcategoryId, item.PackageId, item.ItemId);
                            List<RecipeOptionMD> recipeOptions =
                            aRecipeOptionMdList.Where(a => a.PackageItemOptionsIndex == item.PackageItemOptionsIndex && a.RecipeId == item.ItemId).ToList();
                            if (item.Price > 0)
                            {
                                aControl.totalPriceLabel.Text = item.Price.ToString("F02");
                            }
                            else{
                                aControl.totalPriceLabel.Text = "";
                            }
                            if (recipeOptions.Count > 0)
                            {
                                aControl.packageOptionsLabel.Text = "  ";
                                bool flag = false;
                                foreach (RecipeOptionMD list in recipeOptions)
                                {
                                    if (flag)
                                    {
                                        aControl.packageOptionsLabel.Text += "\r\n  ";
                                    }
                                    if (!string.IsNullOrEmpty(list.MinusOption))
                                    {
                                        if (list.InPrice > 0)
                                        {
                                            aControl.packageOptionsLabel.Text += ("→No " + list.MinusOption);
                                           
                                        }
                                        else aControl.packageOptionsLabel.Text += ("→No " + list.MinusOption);
                                    }
                                    if (!string.IsNullOrEmpty(list.Title))
                                    {
                                        if (list.InPrice > 0)
                                        {
                                            aControl.packageOptionsLabel.Text += "→" + (list.Title) + "+" + list.Price;
                                            tempPackageDetails.priceTextBox.Text =
                                              (Convert.ToDouble(tempPackageDetails.priceTextBox.Text) + list.Price).ToString("F02");
                                        }
                                        else aControl.packageOptionsLabel.Text += "→" + (list.Title);
                                    }
                                    flag = true;
                                }
                            }
                            else
                            {
                                aControl.packageOptionsLabel.Visible = false;
                            }

                            tempPackageDetails.packageItemsFlowLayoutPanel.Controls.Add(aControl);
                        }
                    }

                    if (tempPackageDetails.ItemLimit > 0 || packagetemp.ItemLimit > 0)
                    {
                        ClearAllCartSelection();

                        foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>().Where(a => a.OptionIndex == tempPackageDetails.OptionIndex))
                        {

                            foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
                            {
                                c.BackColor = Color.Red;
                            }
                            cc.BackColor = Color.Red;
                        }
                        return 1;
                    }
                }
                else
                {
                    List<RecipeOptionMD> recipeOptions1 = aRecipeOptionMdList.Where(a => a.OptionsIndex == packagetemp.OptionsIndex).ToList();
                    double optionPrice = GetOptionPrice(recipeOptions1);
                    var package = aRecipePackageMdList.FirstOrDefault(a => a.OptionsIndex == itemIndex);
                    PackageDetails aPackageDetails = new PackageDetails();
                    aPackageDetails.qtyTextBox.Text = package.Qty.ToString();
                    aPackageDetails.nameTextBox.Text = package.PackageName;
                    aPackageDetails.PackageId = package.PackageId;
                    aPackageDetails.ItemLimit = (int)package.ItemLimit;
                    aPackageDetails.OptionIndex = package.OptionsIndex;
                    List<PackageItem> itemList = aPackageItemMdList.Where(a => a.OptionsIndex == package.OptionsIndex).ToList();
                    double Extraprice = 0;
                    foreach (PackageItem item in itemList)
                    {
                        PackItemsControl aControl = new PackItemsControl();
                        aControl.qtyTextBox.Text = item.Qty.ToString();
                        aControl.nameTextBox.Text = item.ItemName;
                        aControl.MouseClick += UsersGridForPackageItem_WasClicked;
                        aControl.OptionIndex = package.OptionsIndex;
                        aControl.PackageItemOptionIndex = item.PackageItemOptionsIndex;
                        aControl.PackageId = package.PackageId;
                        aControl.ItemId = item.ItemId;
                        item.DeleteItem = true;
                        double price = 0;
                        if (item.Price > 0)
                        {
                            aControl.totalPriceLabel.Text = item.Price.ToString("F02");
                        }
                        else
                        {
                            aControl.totalPriceLabel.Text = "";
                        }

                        List<RecipeOptionMD> recipeOptions = aRecipeOptionMdList.Where(a => a.OptionsIndex == item.OptionsIndex && a.PackageItemOptionsIndex == item.PackageItemOptionsIndex && a.RecipeId == item.ItemId).ToList();
                        if (recipeOptions.Count > 0)
                        {
                            aControl.packageOptionsLabel.Text = "  ";
                            bool flag = false;
                            foreach (RecipeOptionMD list in recipeOptions)
                            {
                                if (flag)
                                {
                                    aControl.packageOptionsLabel.Text += "\r\n  ";
                                }
                                if (!string.IsNullOrEmpty(list.MinusOption))
                                {
                                    aControl.packageOptionsLabel.Text += ("→No " + list.MinusOption);

                                }
                                if (!string.IsNullOrEmpty(list.Title))
                                {
                                    price = (list.Qty * list.Price);
                                    Extraprice += price;
                                    if(price > 0)
                                    {
                                        aControl.packageOptionsLabel.Text += "→" + (list.Title) + "+ " +price.ToString("F2");
                                    }
                                    else
                                    {
                                        aControl.packageOptionsLabel.Text += "→" + (list.Title);
                                    }
                                }
                                flag = true;
                            }
                        }
                        else
                        {
                            aControl.packageOptionsLabel.Visible = false;
                        }
                        aPackageDetails.packageItemsFlowLayoutPanel.Controls.Add(aControl);
                    }
                    aPackageDetails.priceTextBox.Text = (package.UnitPrice + Extraprice).ToString("F02");
                    aPackageDetails.totalPriceLabel.Text = (package.UnitPrice * package.Qty).ToString("F02");

                    if (itemList.Count == 0)
                    {
                        aPackageDetails.packageItemsFlowLayoutPanel.Size =
                            new Size(aPackageDetails.packageItemsFlowLayoutPanel.Size.Width, 0);
                    }
                    aPackageDetails.MouseClick += UsersGridForPackage_WasClicked;

                    if (orderDetailsflowLayoutPanel1.Controls.Count > 0)
                    {
                        System.Windows.Forms.Control aaControl = orderDetailsflowLayoutPanel1.Controls[0];
                        previousControl = aaControl;
                    }
                    else
                    {
                        previousControl = null;
                    }
                    if (previousControl != null)
                    {
                        var prevIndex = orderDetailsflowLayoutPanel1.Controls.IndexOf(
                                previousControl as System.Windows.Forms.Control);
                        orderDetailsflowLayoutPanel1.Controls.Add(aPackageDetails);
                        orderDetailsflowLayoutPanel1.Controls.SetChildIndex(aPackageDetails, prevIndex);
                        previousControl = aPackageDetails;
                    }
                    else
                    {
                        orderDetailsflowLayoutPanel1.Controls.Add(aPackageDetails);
                        previousControl = aPackageDetails;
                    }
                    return 0;
                }
            }
            return 0;
        }

        private double GetOptionPrice(List<RecipeOptionMD> aRList)
        {
            double price = 0;
            foreach (RecipeOptionMD list in aRList)
            {
                price += (list.Qty * list.Price);
            }

            return price;
        }

        /// <summary>
        /// Check Duplication Only Item From Cart
        /// </summary>
        /// <param name="aOrderItemDetails"></param>
        /// <param name="aRecipeList"></param>
        /// <returns></returns>
        
        private int CheckDulicate(OrderItemDetailsMD aOrderItemDetails, List<RecipeOptionItemButton> aRecipeList)
        {
            List<OrderItemDetailsMD> itemDetails = aOrderItemDetailsMDList.Where(a => a.ItemId == aOrderItemDetails.ItemId && a.ItemName == aOrderItemDetails.ItemName).ToList();
            if (itemDetails.Count == 0)
            {
                return 0;
            }

            int result = 0;

            foreach (OrderItemDetailsMD item in itemDetails)
            {
                List<RecipeOptionMD> aRList =
                    aRecipeOptionMdList.Where(a => a.OptionsIndex == item.OptionsIndex).ToList();
                if (aRecipeList.Count == 0 && aRList.Count == 0)
                {
                    return item.OptionsIndex;
                }

                int cnt = 0;
                bool res = true;
                if (aRList.Count == aRecipeList.Count)
                {
                    foreach (RecipeOptionMD list in aRList)
                    {
                        bool flag = false;
                        foreach (RecipeOptionItemButton recipe in aRecipeList)
                        {
                            if (!CheckWithoutMinusItemIsExits(list, recipe)) res = false;
                            else cnt++;
                        }
                    }

                    if (cnt != 0 && cnt == aRecipeList.Count && res)
                    {
                        result = item.OptionsIndex;
                    }
                }

            }
            return result;
        }

        private bool CheckWithoutMinusItemIsExits(RecipeOptionMD saveItem, RecipeOptionItemButton currentItem)
        {

            if (saveItem.Title != null)
            {
                if (saveItem.Title != currentItem.Title) return false;
            }
            if (currentItem.MinusTitle != null)
            {
                if (saveItem.MinusOption == currentItem.MinusTitle) return false;
            }

            if (currentItem.Title != null && currentItem.Title != null && saveItem.MinusOption != currentItem.MinusTitle)
            {
                return false;
            }

            return true;
        }

        private void btn_Read_MouseClick(object sender, EventArgs e)
        {
            try
            {
                aOthersMethod.KeyBoardClose();
                aOthersMethod.NumberPadClose();
                RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
                ReceipeCategoryButton aButton = sender as ReceipeCategoryButton;
                aButton.BackColor = Color.Black;
                hasSubcategoryId = aButton.CategoryId;

                foreach (Control c in menuTabControl.SelectedTab.Controls)
                {
                    foreach (Control childc in c.Controls.OfType<FlowLayoutPanel>())
                    {
                        foreach (FlowLayoutPanel button in c.Controls.OfType<FlowLayoutPanel>())
                        {
                            foreach (ReceipeCategoryButton tempButton in button.Controls.OfType<ReceipeCategoryButton>()
                                )
                            {
                                if (tempButton != aButton && tempButton.HasSubcategory > 0)
                                {
                                    tempButton.BackColor =
                                        ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(tempButton.Color));
                                }
                            }
                        }
                    }
                }

                if (isMultiplePart)
                {
                    LoadMultipleMenuForm.aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
                    LoadMultipleMenuForm.aRecipeOptionMdList = new List<RecipeOptionMD>();
                    LoadMultipleMenuForm aLoadMultipleMenuForm = new LoadMultipleMenuForm(aButton.CategoryId);
                    aLoadMultipleMenuForm.ShowDialog();
                    SelectMultiplePartButton();
                    if (LoadMultipleMenuForm.aOrderItemDetailsMDList.Any() && LoadMultipleMenuForm.status == "ok")
                    {
                        LoadMultiplePartMenu(LoadMultipleMenuForm.aOrderItemDetailsMDList,
                            LoadMultipleMenuForm.aRecipeOptionMdList, aButton);
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        /// <summary>
        /// Load MultipleItem Method
        /// </summary>
        /// <param name="items"></param>
        /// <param name="options"></param>
        /// <param name="aReceipeCategoryButton"></param>
        
        private void LoadMultiplePartMenu(List<OrderItemDetailsMD> items, List<RecipeOptionMD> options, ReceipeCategoryButton aReceipeCategoryButton)
        {
            int itemIndex = 0;

            List<OrderItemDetailsMD> aPackageItemList = items;
            List<RecipeOptionMD> aPackageOptionList = options;

            List<MultipleItemMD> aMultipleItemMds = GetMultipleItems(aPackageItemList);
            double maxPrice = aMultipleItemMds.Max(a => a.Price);
            MultipleItemMD maxItemMd = aMultipleItemMds.FirstOrDefault(a => a.Price >= maxPrice);
            int optionIndex = GetOptionIndex();

            RecipeMultipleMD aRecipePackage = new RecipeMultipleMD();
            aRecipePackage.Description = maxItemMd.ItemName;
            aRecipePackage.OptionsIndex = optionIndex + 1;
            aRecipePackage.CategoryId = maxItemMd.CategoryId;

            if (aPackageItemList.Count == 2)
            {
                aRecipePackage.MultiplePartName = aReceipeCategoryButton.CategoryName + " " + "Half & Half";
            }
            else
            {
                aRecipePackage.MultiplePartName = aReceipeCategoryButton.CategoryName + " " + "Quater";
            }

            aRecipePackage.Qty = 1;
            aRecipePackage.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
            aRecipePackage.RestaurantId = aRestaurantInformation.Id;
            aRecipePackage.ItemId = maxItemMd.ItemId;

            aRecipePackage.UnitPrice = maxItemMd.Price;

            int index = 0;

            if (index > 0)
            {
                itemIndex = index;
                RecipeMultipleMD tempOrderItemDetails =
                    aRecipeMultipleMdList.SingleOrDefault(a => a.OptionsIndex == index);
                tempOrderItemDetails.Qty += 1;
                List<MultipleItemMD> aRList = aMultipleItemMdList.Where(a => a.OptionsIndex == index).ToList();
                foreach (MultipleItemMD list in aRList)
                {
                    list.Qty += 1;
                }
            }
            else
            {
                itemIndex = optionIndex + 1;
                aRecipeMultipleMdList.Add(aRecipePackage);

                foreach (MultipleItemMD item in aMultipleItemMds)
                {
                    item.OptionsIndex = optionIndex + 1;
                    aMultipleItemMdList.Add(item);
                }

                foreach (RecipeOptionMD option in aPackageOptionList)
                {
                    option.OptionsIndex = itemIndex;
                    aRecipeOptionMdList.Add(option);
                }
            }

            LoadOrderDetails(itemIndex);
        }

        private List<MultipleItemMD> GetMultipleItems(List<OrderItemDetailsMD> aItemList)
        {
            List<MultipleItemMD> aMultipleItemMds = new List<MultipleItemMD>();
            foreach (OrderItemDetailsMD itemDetails in aItemList)
            {
                MultipleItemMD item = new MultipleItemMD();
                item.CategoryId = itemDetails.CategoryId;
                item.ItemId = itemDetails.ItemId;
                item.ItemName = itemDetails.ItemName;
                item.Price = itemDetails.Price;
                item.Qty = itemDetails.Qty;
                item.SubcategoryId = item.SubcategoryId;
                aMultipleItemMds.Add(item);
            }
            return aMultipleItemMds;
        }

        /// <summary>
        /// Selected on cart item event
        /// </summary>
        /// <param name="sen7"></param>
        /// <param name="e"></param>
        
        private void UsersGrid_WasClicked(object sen7, MouseEventArgs e)
        {
            deatilsControls details = sen7 as deatilsControls;
            foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
            {
                foreach (deatilsControls c in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                {
                    if (details.nameTextBox == c.nameTextBox)
                    {
                        c.BackColor = Color.Red;
                    }
                    else
                    {
                        c.BackColor = deatilsControls.DefaultBackColor;
                    }
                }
            }

            foreach (PackageDetails c in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                c.BackColor = PackageDetails.DefaultBackColor;
            }

            foreach (MultiplePartControl cc in orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
            {
                cc.packageItemsFlowLayoutPanel.BackColor = MultiplePartControl.DefaultBackColor;
                foreach (Label c in cc.packageItemsFlowLayoutPanel.Controls.OfType<Label>())
                {
                    c.BackColor = MultiplePartControl.DefaultBackColor;
                }
                cc.BackColor = MultiplePartControl.DefaultBackColor;
            }
        }

        /// <summary>
        /// Existing Cart Package Click event
        /// </summary>
        /// <param name="sen20"></param>
        /// <param name="e"></param>
        
        private void UsersGridForPackage_WasClicked(object sen20, MouseEventArgs e)
        {
            PackageDetails details = sen20 as PackageDetails;
            PackageDetails package =
                orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>()
                    .SingleOrDefault(a => a.OptionIndex == details.OptionIndex);
            package.BackColor = Color.Red;
            foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
            {
                foreach (PackItemsControl c in cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>())
                {
                    if (details.OptionIndex == c.OptionIndex)
                    {

                        c.BackColor = Color.Red;
                    }
                    else
                    {
                        c.BackColor = PackItemsControl.DefaultBackColor;
                    }
                }

                if (cc.OptionIndex != package.OptionIndex)
                {
                    cc.BackColor = PackageDetails.DefaultBackColor;
                }
            }
            foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
            {
                foreach (deatilsControls c in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                {
                    c.BackColor = deatilsControls.DefaultBackColor;
                }
            }

            foreach (MultiplePartControl cc in orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>())
            {
                cc.packageItemsFlowLayoutPanel.BackColor = MultiplePartControl.DefaultBackColor;
                foreach (Label c in cc.packageItemsFlowLayoutPanel.Controls.OfType<Label>())
                {
                    c.BackColor = MultiplePartControl.DefaultBackColor;
                }
                cc.BackColor = MultiplePartControl.DefaultBackColor;
            }
        }

        /// <summary>
        /// Item  Decrement  Qty click event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void itemMinusButton_Click(object sender, EventArgs e)
        {            
            try
            {
                aOthersMethod.KeyBoardClose();
                aOthersMethod.NumberPadClose();
               
                bool flag = false;
                int itemIndex = 0;
                //check if the regular recipe is selected for decreasing qty
                foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
                {
                    foreach (deatilsControls control in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                    {
                        if (control.BackColor == Color.Red)
                        {
                            flag = true;
                            deatilsControls details = control;
                            double qty = Convert.ToDouble(details.qtyTextBox.Text);
                            double price = GlobalVars.numberRound(Convert.ToDouble(details.priceTextBox.Text));

                            if (qty > 1)
                            {
                                UpdateItemAndOption(details.OptionIndex, (int)qty - 1);
                                control.qtyTextBox.Text = (qty - 1).ToString();
                                control.totalPriceLabel.Text = ((qty - 1) * price).ToString();
                                LoadAmountDetails();

                            }
                            else
                            {
                                aRecipeOptionMdList.RemoveAll(a => a.OptionsIndex == details.OptionIndex);
                                aOrderItemDetailsMDList.RemoveAll(a => a.OptionsIndex == details.OptionIndex);
                                control.Dispose();
                                LoadAmountDetails();
                            }
                        }

                        double amount =   aOrderItemDetailsMDList.Where(b => b.RecipeTypeId == control1.RecipeTypeId)
                                .Sum(a => a.Price * a.Qty);

                        control1.recipeTypeAmountlabel.Text = amount.ToString("F02");
                    }
                    if (!control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>().Any())
                    {
                        control1.Dispose();
                    }
                }

                //no regular menu is selected, now check for packages
                if (!flag)
                {
                    foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
                    {
                        //if the package is selected (when backcolor is red)
                        if (cc.BackColor == Color.Red)
                        {
                            flag = true;
                            int items = cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>().Count();
                            double qty = Convert.ToDouble(cc.qtyTextBox.Text);
                            double price = Convert.ToDouble(cc.priceTextBox.Text);
                            List<PackItemsControl> packItems =
                                cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>()
                                    .Where(a => a.BackColor == Color.Red)
                                    .ToList();

                            //decrease package qty
                            if (items == packItems.Count)
                            {
                                var totalSelectedItems = aPackageItemMdList.Sum(t => t.Qty);

                                if (qty - 1 == 0)
                                {
                                    aRecipePackageMdList.RemoveAll(a => a.OptionsIndex == cc.OptionIndex);
                                    aPackageItemMdList.RemoveAll(a => a.OptionsIndex == cc.OptionIndex);
                                    aRecipeOptionMdList.RemoveAll(a => a.OptionsIndex == cc.OptionIndex);
                                    LoadAmountDetails();
                                    cc.Dispose();
                                    break;
                                }

                                //need to remove package items first
                                if (totalSelectedItems > cc.ItemLimit * (qty - 1))
                                {
                                    MessageBox.Show("Please remove items first.", "Selected items are more than the limit.",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    aRecipePackageMdList.Where(a => a.OptionsIndex == cc.OptionIndex).ToList().ForEach(b => b.Qty = ((Convert.ToInt32(b.Qty) / qty) * (qty - 1)));
                                    //aPackageItemMdList.Where(a => a.OptionsIndex == cc.OptionIndex).ToList().ForEach(b => b.Qty = (b.Qty - 1));
                                    cc.qtyTextBox.Text = (qty - 1).ToString();
                                    cc.totalPriceLabel.Text = ((qty - 1) * price).ToString();
                                    LoadAmountDetails();
                                    break;
                                }
                            }
                            //decrease package item qty
                            else
                            {
                                if (packItems.Count > 0)
                                {
                                    PackItemsControl pack = packItems[0];
                                    
                                    //if the item qty is 0 after decreasing then remove it
                                    if (Convert.ToInt32(pack.qtyTextBox.Text) <= 1)
                                    {
                                        aPackageItemMdList.RemoveAll(
                                            a => a.OptionsIndex == pack.OptionIndex && a.ItemId == pack.ItemId);
                                        aRecipeOptionMdList.RemoveAll(
                                            a => a.OptionsIndex == pack.OptionIndex && a.RecipeId == pack.ItemId);
                                        pack.Dispose();
                                        cc.Refresh();
                                        break;
                                    }
                                    else
                                    {
                                        pack.qtyTextBox.Text = (Convert.ToInt32(pack.qtyTextBox.Text) - 1).ToString();
                                        if (pack.totalPriceLabel.Text != "")
                                        {
                                            pack.totalPriceLabel.Text = (Convert.ToDouble(Convert.ToInt32(pack.qtyTextBox.Text)) * (Convert.ToDouble(pack.totalPriceLabel.Text) / (Convert.ToInt32(pack.qtyTextBox.Text) + 1))).ToString("F02");
                                        }
                                        
                                        aPackageItemMdList.Where(
                                            a => a.OptionsIndex == pack.OptionIndex && a.ItemId == pack.ItemId)
                                            .ToList()
                                            .ForEach(b => b.Qty = (b.Qty - 1));
                                        LoadAmountDetails();
                                        break;
                                    }
                                }
                            }
                        }                        
                    }
                }
                if (flag)
                {
                    LoadOrderDetails(0);

                }
            }
            catch (Exception)
            {
                Activate();
            }
        }

        /// <summary>
        /// Item Quantity Increment Qty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void itemPlusButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            try
            {
                bool flag = false;
                int itemIndex = 0;
                foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
                {
                    foreach (deatilsControls control in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                    {
                        if (control.BackColor == Color.Red)
                        {
                            flag = true;
                            deatilsControls details = control;
                            double qty = Convert.ToDouble(details.qtyTextBox.Text);
                            double price = Convert.ToDouble(details.priceTextBox.Text);
                            UpdateItemAndOption(details.OptionIndex, (int)qty + 1);
                            control.qtyTextBox.Text = (qty + 1).ToString();
                            control.totalPriceLabel.Text = ((qty + 1) * price).ToString();
                            LoadAmountDetails();
                        }

                        double amount =
                            aOrderItemDetailsMDList.Where(b => b.RecipeTypeId == control1.RecipeTypeId)
                                .Sum(a => a.Price * a.Qty);
                        control1.recipeTypeAmountlabel.Text = amount.ToString("F02");
                    }
                    if (!control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>().Any())
                    {
                        control1.Dispose();
                    }
                }

                if (!flag)
                {
                    //foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
                    //{
                    //    if (cc.BackColor == Color.Red)
                    //    {
                    //        RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
                    //        flag = true;
                    //        int items = cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>().Count();
                    //        double qty = Convert.ToDouble(cc.qtyTextBox.Text);
                    //        double price = Convert.ToDouble(cc.priceTextBox.Text);
                    //        List<PackItemsControl> packItems =
                    //            cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>()
                    //                .Where(a => a.BackColor == Color.Red)
                    //                .ToList();
                    //        int itemqty =
                    //            Convert.ToInt32(
                    //                cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>()
                    //                    .Sum(a => Convert.ToInt32(a.qtyTextBox.Text)));
                    //        RecipePackageButton aResItemButton = aRestaurantMenuBll.GetPackageByPackageId(cc.PackageId);


                    //        if (items == packItems.Count)
                    //        {
                    //            aRecipePackageMdList.Where(a => a.OptionsIndex == cc.OptionIndex).ToList().ForEach(b => b.Qty = ((Convert.ToInt32(b.Qty) * (qty + 1)) / qty));
                    //            aPackageItemMdList.Where(a => a.OptionsIndex == cc.OptionIndex).ToList().ForEach(b => b.Qty = Convert.ToInt32((Convert.ToInt32(b.Qty) * (qty + 1)) / qty));
                    //            cc.qtyTextBox.Text = (qty + 1).ToString();
                    //            cc.totalPriceLabel.Text = ((qty + 1) * price).ToString();
                    //            packItems.ForEach(a => a.qtyTextBox.Text = ((Convert.ToInt32(a.qtyTextBox.Text) * (qty + 1)) / qty).ToString());
                    //            LoadAmountDetails();

                    //            break;
                    //        }
                    //        else if (packItems.Count > 0)
                    //        {
                    //            PackItemsControl pack = packItems[0];
                    //            RecipePackageMD loadPack = aRecipePackageMdList.FirstOrDefault(
                    //                    a => a.PackageId == pack.PackageId && a.OptionsIndex == pack.OptionIndex);
                    //            int itemSum = cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>().Sum(a => Convert.ToInt16(a.qtyTextBox.Text));

                    //            if (loadPack.ItemLimit * qty > itemSum)
                    //            {

                    //                pack.qtyTextBox.Text = (Convert.ToInt32(pack.qtyTextBox.Text) + 1).ToString();
                    //                aPackageItemMdList.Where(a => a.OptionsIndex == pack.OptionIndex && a.ItemId == pack.ItemId)
                    //                    .ToList()
                    //                    .ForEach(b => b.Qty = (b.Qty + 1));
                    //                //loadPack.ItemLimit--;
                    //                //cc.ItemLimit--;
                    //            }

                    //            break;
                    //        }
                    //        else
                    //        {

                    //            cc.qtyTextBox.Text = (qty + 1).ToString();
                    //            cc.totalPriceLabel.Text = ((qty + 1) * price).ToString();
                    //        }

                    //    }
                    //}
                    foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
                    {
                        if (cc.BackColor == Color.Red)
                        {
                            flag = true;
                            int items = cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>().Count();
                            double qty = Convert.ToDouble(cc.qtyTextBox.Text);
                            double price = Convert.ToDouble(cc.priceTextBox.Text);
                            List<PackItemsControl> packItems =
                                cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>()
                                    .Where(a => a.BackColor == Color.Red)
                                    .ToList();
                            if (items == packItems.Count)
                            {                                                              
                                    aRecipePackageMdList.Where(a => a.OptionsIndex == cc.OptionIndex).ToList().ForEach(b => b.Qty = ((Convert.ToInt32(b.Qty) / qty) * (qty + 1)));
                                    cc.qtyTextBox.Text = (qty + 1).ToString();
                                    cc.totalPriceLabel.Text = ((qty + 1) * price).ToString();

                                //aPackageItemMdList.Where(a => a.OptionsIndex == cc.OptionIndex && a.PackageId == cc.PackageId)
                                //                     .ToList().ForEach(a => a.Price = (a.Price / a.Qty) * Convert.ToInt32(cc.qtyTextBox.Text));


                                //aPackageItemMdList.Where(a => a.OptionsIndex == cc.OptionIndex && a.PackageId == cc.PackageId).ToList().ForEach(a => a.Qty = Convert.ToInt32(a.Qty * Convert.ToInt32(cc.qtyTextBox.Text)));
                              
                              
                                //packItems.ForEach(a => a.qtyTextBox.Text = ((Math.Ceiling(Convert.ToInt32(a.qtyTextBox.Text) / qty) * (qty + 1))).ToString());
                             //   packItems.ForEach(a => a.totalPriceLabel.Text = Convert.ToDouble(Convert.ToDouble(Convert.ToInt32(a.qtyTextBox.Text) * Convert.ToDouble(a.totalPriceLabel.Text))).ToString("F02"));
                                LoadAmountDetails();
                                break;
                            }
                            else
                            {
                                if (packItems.Count > 0)
                                {
                                    PackItemsControl pack = packItems[0];
                                    
                                    pack.qtyTextBox.Text = (Convert.ToInt32(pack.qtyTextBox.Text) + 1).ToString();
                                    if (pack.totalPriceLabel.Text != "")
                                    {
                                        pack.totalPriceLabel.Text = (Convert.ToDouble(Convert.ToInt32(pack.qtyTextBox.Text)) * (Convert.ToDouble(pack.totalPriceLabel.Text) / (Convert.ToInt32(pack.qtyTextBox.Text) - 1))).ToString("F02");
                                    }

                                    //aPackageItemMdList.Where(
                                    //a => a.OptionsIndex == pack.OptionIndex && a.ItemId == pack.ItemId)
                                    //.ToList()
                                    //.ForEach(b => b.ExtraPrice = Convert.ToDouble(pack.totalPriceLabel.Text));
                                    aPackageItemMdList.Where(a => a.OptionsIndex == pack.OptionIndex && a.ItemId == pack.ItemId)
                                                     .ToList().ForEach(b => b.Price = (b.Price / b.Qty) * (b.Qty + 1));

                                    aPackageItemMdList.Where(
                                    a => a.OptionsIndex == pack.OptionIndex && a.ItemId == pack.ItemId)
                                    .ToList()
                                    .ForEach(b => b.Qty = (b.Qty + 1));
                                    LoadAmountDetails();
                                    break;
                                }
                            }
                        }
                    }
                }
                if (flag)
                {
                    LoadOrderDetails(0);
                }
            }
            catch (Exception)
            {
                this.Activate();
            }
        }

        private void UpdateItemAndOption(int optionIndex, int qty)
        {
            List<RecipeOptionMD> aRList = aRecipeOptionMdList.Where(a => a.OptionsIndex == optionIndex).ToList();
            foreach (RecipeOptionMD list in aRList)
            {
                list.Qty = qty;
            }

            OrderItemDetailsMD tempOrderItemDetails =
                aOrderItemDetailsMDList.SingleOrDefault(a => a.OptionsIndex == optionIndex);
            tempOrderItemDetails.Qty = qty;
        }

        private void orderAllClearButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            resetCart();
        }

        /// <summary>
        /// Reset Cart Information when Cart item not clear
        /// 
        /// </summary>
        
        private void resetCart()
        {
            LoadDefaultInformation();
            aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
            aRecipeOptionMdList = new List<RecipeOptionMD>();
            aRecipePackageMdList = new List<RecipePackageMD>();
            aPackageItemMdList = new List<PackageItem>();
            aRecipeMultipleMdList = new List<RecipeMultipleMD>();
            aMultipleItemMdList = new List<MultipleItemMD>();
           // aGeneralInformation = new GeneralInformation();
            //deliveryChargeButton.Text = "Delivery Charge\r\n"+aRestaurantInformation.DeliveryCharge;
            customerDetailsLabel.Text = "";
            commentTextBox.Text = "Comment";
            customerTextBox.Visible = true;
            discountButton.Text = "Disc\r\n0.00";
            recentItemsFlowLayoutPanel.Visible = false;
            customerTextBox.Text = "Search Customer";
            customerEditButton.Visible = false;
            phoneNumberDeleteButton.Visible = false;
            customerRecentItemsButton.Visible = false;

            orderDetailsflowLayoutPanel1.Controls.Clear();
            customPanel.Location = new Point(customPanel.Location.X,
                orderDetailsflowLayoutPanel1.Location.Y + orderDetailsflowLayoutPanel1.Size.Height);
            paymentDetailsPanel.Location = new Point(paymentDetailsPanel.Location.X,
                customPanel.Location.Y + customPanel.Size.Height);
            AddServiceChargeIntoLabel();
            LoadAmountDetails();
        }

        private void price1Button_Click(object sen9, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            System.Windows.Forms.Button button = sen9 as System.Windows.Forms.Button;
            string pricestring = button.Text;
            double exprice = 0;
            if (pricestring.Contains("p"))
            {
                string[] sr = pricestring.Split('p');
                pricestring = sr[0];
                exprice = Double.Parse("0" + pricestring);
                exprice /= 100;
            }
            else if (pricestring.Contains("£"))
            {
                string[] sr = pricestring.Split('£');
                pricestring = sr[1];
                exprice = Double.Parse("0" + pricestring);
            }

            bool flag = false;
            foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
            {
                foreach (deatilsControls control in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                {
                    if (control.BackColor == Color.Red)
                    {
                        flag = true;
                        deatilsControls details = control;
                        double qty = Convert.ToDouble(details.qtyTextBox.Text);
                        double price = Convert.ToDouble(details.priceTextBox.Text) + exprice;
                        control.totalPriceLabel.Text = (qty * price).ToString();
                        control.priceTextBox.Text = price.ToString();
                    }
                }
            }

            if (!flag)
            {
                foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
                {
                    if (cc.BackColor == Color.Red)
                    {
                        flag = true;
                        int items = cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>().Count();

                        List<PackItemsControl> packItems =
                            cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>()
                                .Where(a => a.BackColor == Color.Red)
                                .ToList();
                        if (items != packItems.Count)
                        {
                            PackItemsControl pack = packItems[0];
                            {
                                double packagePrice = Convert.ToDouble("0" + cc.totalPriceLabel.Text);
                                cc.totalPriceLabel.Text = (packagePrice).ToString();


                                double qty = Convert.ToDouble(pack.qtyTextBox.Text);
                                double price = Convert.ToDouble("0" + pack.totalPriceLabel.Text);

                                aPackageItemMdList.Where(
                                    a => a.OptionsIndex == pack.OptionIndex && a.ItemId == pack.ItemId).ToList().
                                    ForEach(b => b.Price = (exprice + price));

                                if ((exprice + price) > 0)
                                {
                                    pack.totalPriceLabel.Text = (exprice + price).ToString();
                                }
                                break;
                            }
                        }
                    }
                }
            }
            LoadOrderDetails(0); 

        }

        private void attributeButton_Click(object sender, EventArgs e)
        { 
            try
            {
                aOthersMethod.KeyBoardClose();
                aOthersMethod.NumberPadClose();
                
                AttributeForm.AttributeName = "";
                AttributeForm.Price = 0;
                AttributeForm.Result = "";
                AttributeForm aAttributeForm = new AttributeForm();
                aAttributeForm.ShowDialog();

                if (AttributeForm.Result == "ok")
                {
                    bool flag = false;
                    foreach (
                        RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
                    {
                        foreach (
                            deatilsControls control in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                        {
                            if (control.BackColor == Color.Red)
                            {
                                flag = true;
                                deatilsControls details = control;
                                double qty = Convert.ToDouble(details.qtyTextBox.Text);
                                double price = Convert.ToDouble(details.priceTextBox.Text);
                                double attrPrice = AttributeForm.Price;
                                double discount = AttributeForm.Discount;
                                if (discount != 0)
                                {
                                    price = price - (price * (discount / 100));
                                }
                                control.totalPriceLabel.Text = (qty * (price + attrPrice)).ToString("F02");
                                control.priceTextBox.Text = (price + attrPrice).ToString("F02");
                                control.nameTextBox.Text = control.nameTextBox.Text + "+" + AttributeForm.AttributeName;
                            }
                        }
                    }
                    if (!flag)
                    {
                        //  AttributeForm.Price+price
                        foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
                        {
                            if (cc.BackColor == Color.Red)
                            {
                                flag = true;
                                int items = cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>().Count();

                                List<PackItemsControl> packItems =
                                    cc.packageItemsFlowLayoutPanel.Controls.OfType<PackItemsControl>()
                                        .Where(a => a.BackColor == Color.Red)
                                        .ToList();
                                if (items != packItems.Count || items == 1)
                                {
                                    PackItemsControl pack = packItems[0];
                                    {
                                        double qty = Convert.ToDouble(pack.qtyTextBox.Text);
                                        double price = Convert.ToDouble("0" + pack.totalPriceLabel.Text);
                                        double attrPrice = AttributeForm.Price;
                                        double discount = AttributeForm.Discount;
                                        if (discount != 0)
                                        {

                                            price = price - (price * (discount / 100));

                                        }
                                        aPackageItemMdList.Where(
                                            a => a.OptionsIndex == pack.OptionIndex && a.ItemId == pack.ItemId)
                                            .ToList()
                                            . ForEach(
                                                b =>
                                                    b.ItemName =
                                                        (pack.nameTextBox.Text + "+" + AttributeForm.AttributeName));
                                        aPackageItemMdList.Where(
                                            a => a.OptionsIndex == pack.OptionIndex && a.ItemId == pack.ItemId)
                                            .ToList()
                                            .
                                            ForEach(b => b.Price = (AttributeForm.Price + price));

                                        pack.nameTextBox.Text = pack.nameTextBox.Text + "+" +
                                                                AttributeForm.AttributeName;

                                        if ((AttributeForm.Price + price) > 0)
                                        {
                                            pack.totalPriceLabel.Text = (AttributeForm.Price + price).ToString("F02");
                                        }
                                        break;
                                    }
                                }
                            }
                        }                        
                    }
                    if (flag)
                    {
                        LoadOrderDetails(0);
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
                MessageBox.Show("Something Error!. Please try again.", "Database Unknown Field Error.",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void customTotalTextBox_TextChanged(object sender, EventArgs e)
        {
            LoadAmountDetails();
        }

        private void discountButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            DiscountForm.OrderDiscount = new OrderDiscount();
            if (Properties.Settings.Default.requirdDiscountPassword)
            {
                RestaurantUsers user = new RestaurantUsers();
                AutorizeForm authorization = new AutorizeForm(user,"discount");
                authorization.ShowDialog();
                if (authorization.user.Autorize)
                {
                    DiscountForm aDiscountForm = new DiscountForm();
                    aDiscountForm.ShowDialog();
                    OrderDiscount aOrderDiscount = DiscountForm.OrderDiscount;
                    if (aOrderDiscount.Status == "cancel") return;
                    CalculateDiscount(aOrderDiscount);
                }
            }
            else
            {
                DiscountForm aDiscountForm = new DiscountForm();
                aDiscountForm.ShowDialog();
                OrderDiscount aOrderDiscount = DiscountForm.OrderDiscount;
                if (aOrderDiscount.Status == "cancel") return;
                CalculateDiscount(aOrderDiscount);
            }            
        }

        /// <summary>
        /// PriceCalculation Amount information 
        /// </summary>
        /// <param name="aOrderDiscount"></param>
        
        private void CalculateDiscount(OrderDiscount aOrderDiscount)
        {
            if (aOrderDiscount.DiscountType != null)
            {
                aGeneralInformation = new PriceCalculation(this, null).CalculateDiscount(aOrderDiscount, aGeneralInformation);
            }
        }

        /// <summary>
        /// Take line Discount when Discount Button Click
        /// Line Discount : Indivisual Order
        /// </summary>
        /// <param name="aOrderDiscount"></param>
        
        public void LineDiscount(OrderDiscount aOrderDiscount)
        {
            bool flag = false;
            double discountAmount = 0;
            double discountPercent = 0;
            string[] categoryStringList = aRestaurantInformation.ExcludeDiscount.Split(',');
            foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
            {
                foreach (deatilsControls control in control1.typeflowLayoutPanel1.Controls.OfType<deatilsControls>())
                {
                    if (control.BackColor == Color.Red)
                    {
                        flag = true;
                        deatilsControls details = control;
                        if (!categoryStringList.Contains(control.CategoryId.ToString()))
                        {
                            if (aOrderDiscount.DiscountType == "Fixed")
                            {
                                discountAmount = aOrderDiscount.Amount;
                                aOrderItemDetailsMDList.Where(a => a.OptionsIndex == details.OptionIndex)
                                    .ToList()
                                    .ForEach(b => b.Price = (b.Price - discountAmount));
                                details.priceTextBox.Text =
                                    (Convert.ToDouble(details.priceTextBox.Text) - discountAmount).ToString("F02");
                                details.totalPriceLabel.Text =
                                    ((Convert.ToDouble(details.priceTextBox.Text)) *
                                     Convert.ToDouble(details.qtyTextBox.Text)).ToString("F02");
                            }
                            else if (aOrderDiscount.DiscountType == "Persent")
                            {
                                discountAmount = (Convert.ToDouble(details.priceTextBox.Text) * aOrderDiscount.Amount) / 100;
                                aOrderItemDetailsMDList.Where(a => a.OptionsIndex == details.OptionIndex)
                                    .ToList()
                                    .ForEach(b => b.Price = (b.Price - discountAmount));
                                //  aGeneralInformation.ItemDiscount = discountAmount;
                                details.priceTextBox.Text =
                                    (Convert.ToDouble(details.priceTextBox.Text) - discountAmount).ToString("F02");
                                details.totalPriceLabel.Text =
                                    ((Convert.ToDouble(details.priceTextBox.Text)) *
                                     Convert.ToDouble(details.qtyTextBox.Text)).ToString("F02");
                            }
                        }
                    }
                }
            }

            if (!flag)
            {
                foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
                {
                    if (cc.BackColor == Color.Red)
                    {
                        PackageDetails details = cc;
                        if (!categoryStringList.Contains("Package"))
                        {
                            if (aOrderDiscount.DiscountType == "Fixed")
                            {
                                discountAmount = aOrderDiscount.Amount;
                                aRecipePackageMdList.Where(a => a.OptionsIndex == details.OptionIndex)
                                    .ToList().ForEach(b => b.UnitPrice = (b.UnitPrice - discountAmount));

                                details.priceTextBox.Text = (Convert.ToDouble(details.priceTextBox.Text) - discountAmount).ToString("F02");
                                    details.totalPriceLabel.Text = ((Convert.ToDouble(details.priceTextBox.Text)) * Convert.ToDouble(details.qtyTextBox.Text)).ToString("F02");
                            }
                            else if (aOrderDiscount.DiscountType == "Persent")
                            {
                                discountAmount = (Convert.ToDouble(details.priceTextBox.Text) * aOrderDiscount.Amount) / 100;
                                aRecipePackageMdList.Where(a => a.OptionsIndex == details.OptionIndex)
                                    .ToList().ForEach(b => b.UnitPrice = (b.UnitPrice - discountAmount));
                           
                                details.priceTextBox.Text = (Convert.ToDouble(details.priceTextBox.Text) - discountAmount).ToString("F02");
                                details.totalPriceLabel.Text =
                                    ((Convert.ToDouble(details.priceTextBox.Text)) *
                                     Convert.ToDouble(details.qtyTextBox.Text)).ToString("F02");
                            }
                        }
                    }
                }
            }
            LoadOrderDetails(0);

        }

        private void paidButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            PaidAmountWithPrint(true, true);
        }

        /// <summary>
        /// Save,Update Item with Restaurent Information and Order Information
        /// </summary>
        /// <param name="paymentStatus"></param>
        /// <param name="isKitchen"></param>
        /// <param name="isPrint"></param>
        /// <param name="isFinalize"></param>
        
        private void PaidAmountWithPrint(bool paymentStatus, bool isKitchen, bool isPrint = false,
            bool isFinalize = false,bool kitchenPrintOnly =false,bool OrderSaveonly=false)
        {
            string paymentTransferId = "";

            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            }

            if (orderDetailsflowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("No items int the order!", "Save Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime stDate = DateTime.Now.Date;
            DateTime endDate = stDate.AddDays(1);
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            if (GetTotalAmountDetails() > 0)
            {
                ConfirmOrderForm.PaymentDetails = new PaymentDetails();
                PaymentDetails aPaymentDetails = ConfirmOrderForm.PaymentDetails;
                RestaurantOrder aRestaurantOrder = GetRestaurantOrder(aPaymentDetails);
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                aRestaurantOrder.Discount = 0.0;
                aRestaurantOrder.Coupon = "";
                aRestaurantOrder.DiscountAmount = aGeneralInformation.DiscountFlat;
              
                if (aGeneralInformation.DiscountType != null)
                {
                    aRestaurantOrder.Coupon = aGeneralInformation.DiscountType;
                    if (aGeneralInformation.DiscountType == "percent")
                    {
                        aRestaurantOrder.Discount = aGeneralInformation.DiscountPercent;
                    }
                    else
                    {
                        aRestaurantOrder.Discount = aGeneralInformation.DiscountFlat;
                    }
                }
                else
                {
                    if (aGeneralInformation.OrderType != "IN" && Properties.Settings.Default.isEnableAutoDiscount)
                    {            
                        DiscountForm.OrderDiscount = new OrderDiscount();
                        DiscountForm aDiscountForm = new DiscountForm();
                        aDiscountForm.ShowDialog();
                        OrderDiscount aOrderDiscount = DiscountForm.OrderDiscount;                        
                        if (aOrderDiscount.Status == "cancel") return;
                            CalculateDiscount(aOrderDiscount);
                    }
                }

                ConfirmOrderForm aConformOrderForm = new ConfirmOrderForm(aRestaurantOrder);
                if (paymentStatus)
                {
                    aConformOrderForm.ShowDialog();
                }
                if ((aPaymentDetails.Status == "Ok" && paymentStatus) || !paymentStatus)
                {
                    try
                    {
                        paymentTransferId = aPaymentDetails.PaymentTransferId; 
                        int result = 0;
                        aGeneralInformation.OrderType = String.IsNullOrEmpty(aGeneralInformation.OrderType)
                            ? aRestaurantInformation.DefaultOrderType
                            : aGeneralInformation.OrderType;
                        string[] time;
                        if (aGeneralInformation.OrderType == "DEL")
                        {
                            time = aRestaurantInformation.DeliveryTime.Split(' ');
                        } else //if (aGeneralInformation.OrderType == "CLT")
                        {
                            time = aRestaurantInformation.CollectionTime.Split(' ');
                        }
                        aGeneralInformation.DeliveryTime = String.IsNullOrEmpty(aGeneralInformation.DeliveryTime)
                            ? DateTime.Now.AddMinutes(Convert.ToDouble(time[0])).ToString("HH:mm")
                            : aGeneralInformation.DeliveryTime;

                        aGeneralInformation.PaymentMethod = String.IsNullOrEmpty(aPaymentDetails.PaymentMethod)
                            ? aRestaurantOrder.PaymentMethod
                            : aPaymentDetails.PaymentMethod;
                        aRestaurantOrder.PaymentMethod = String.IsNullOrEmpty(aPaymentDetails.PaymentMethod)
                            ? aGeneralInformation.PaymentMethod
                            : aPaymentDetails.PaymentMethod;
                        if (paymentStatus)
                        {
                            aRestaurantOrder.CardAmount = aPaymentDetails.CardAmount;
                            aRestaurantOrder.CardFee = aPaymentDetails.CardFee;
                            aRestaurantOrder.CashAmount = aPaymentDetails.CashAmount;
                        }
                        if (aGeneralInformation.OrderId <= 0)
                        {
                            //List<RestaurantOrder> aOrders = aRestaurantOrderBLL.GetRestaurantOrderByDateForOrderNo(stDate, endDate);
                            //int orderNo = 0;

                            //if (aOrders != null && aOrders.Count > 0)
                            //{
                            //    orderNo = aOrders.Max(a => a.OrderNo);

                            //}
                            //orderNo += 1;

                            int orderNo = aRestaurantOrderBLL.GetMaxOrderNumber(stDate, endDate);

                            OrderNo = orderNo;

                            aRestaurantOrder.OrderStatus = "pending";
                            aRestaurantOrder.Status = "pending";
                            if (paymentStatus)
                            {
                                aRestaurantOrder.Status = "Paid";
                            } 

                            if (aRestaurantOrder.OnlineOrder > 0 || aRestaurantOrder.OnlineOrderId > 0)
                            {
                                aRestaurantOrder.OnlineOrderStatus = "accepted";
                            }

                            aRestaurantOrder.OrderNo = orderNo;
                            if (customerTextBox.Text != "" && customerTextBox.Text != "Search Customer" &&
                                aRestaurantOrder.CustomerId == 0)
                            {
                                aRestaurantOrder.CustomerName = customerTextBox.Text; 
                            }

                            aGeneralInformation.CardFee = aRestaurantOrder.CardFee;
                            if (aGeneralInformation.TableId <= 0)
                            {
                                aRestaurantOrder.OrderStatus = "finished";
                            }
                            if(paymentTransferId != null)
                            {
                               aRestaurantOrder.PaymentModule = GlobalVars.stripePaymentModule;
                            }

                            if (aGeneralInformation.CustomerId > 0)
                            {
                                aRestaurantOrder.DeliveryAddress = customerDetailsLabel.Text;
                            }
                            result = aRestaurantOrderBLL.InsertRestaurantOrder(aRestaurantOrder);
                            if(paymentTransferId != null) {
                                //var stripeData = new { charge = paymentTransferId, payment_intent = "", refund_id = "" };
                                OrderPayments insertArray = new OrderPayments();
                                insertArray.order_id = result;
                                insertArray.payment_reference = paymentTransferId; //JsonConvert.SerializeObject(stripeData);
                                MySqlRestaurantOrderDAO aRestaurantOrderDao = new MySqlRestaurantOrderDAO();
                                aRestaurantOrderDao.InsertOrderPayment(insertArray); 
                            }
                            List<OrderPackage> aOrderPackage = GetOrderPackage(result);
                            var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrderPackage);
                            List<OrderItem> aOrderItems = GetOrderItem(result, result2);
                            int result1 = aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrderItems);

                            if (aGeneralInformation.CustomerId > 0)
                            {
                                CustomerRecentItemBLL aCustomerRecentItemBll = new CustomerRecentItemBLL();
                                List<CustomerRecentItemMD> aCustomerRecentItemMds = GetCustomerOrderItems(aOrderItems, aOrderPackage);
                                string res = aCustomerRecentItemBll.InsertCustomerRecentItems(aCustomerRecentItemMds);

                                if(aGeneralInformation.OrderType == "DEL")
                                {
                                    PostCodeBLL postcodeinfo = new PostCodeBLL();                                                                        
                                    var resAddress = postcodeinfo.insertCustomerAddress(aGeneralInformation.CustomerId, aRestaurantOrder.DeliveryAddress);
                                }
                            }

                            if ((ConfirmOrderForm.PaymentDetails.IsPrint && paymentStatus) || !paymentStatus)
                            {
                                aRestaurantOrder.isFinalize = isFinalize;
                                aRestaurantOrder.isKitchenPrint = isKitchen;
                                aRestaurantOrder.OrderNo = result;
                                if (!kitchenPrintOnly && !OrderSaveonly)
                                {
                                    if (paymentStatus)
                                    {
                                        GenerateHtmlPrint(result, true, aPaymentDetails, isKitchen, false, isFinalize);
                                    }
                                    else
                                    {
                                        GenerateHtmlPrint(result, false, aPaymentDetails, isKitchen, isPrint, isFinalize);
                                    }
                                }
                                else {
                                    if (kitchenPrintOnly) {

                                        GenerateKitchenCopy(result, paymentStatus, aPaymentDetails);
                                    }  
                                }

                                if(OrderSaveonly)
                                {
                                    aRestaurantOrderBLL.UpdateKitchenStatus(result);
                                }
                            }

                            //sync table status when order is created in a table
                            if(aGeneralInformation.TableId > 0)
                            {
                                DataSync.syncTableStatus(aGeneralInformation.TableId, "busy");
                            }
                        }
                        else
                        {
                            RestaurantOrder order = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                            OrderNo = order.OrderNo;
                            result = aGeneralInformation.OrderId;
                            aRestaurantOrder.OnlineOrder = order.OnlineOrder;
                            aRestaurantOrder.OnlineOrderId = order.OnlineOrderId;
                            aRestaurantOrder.OnlineOrderStatus = order.OnlineOrderStatus;

                            aGeneralInformation.CardFee = aRestaurantOrder.CardFee;                         
                            List<OrderPackage> aOrderPackage = GetOrderPackage(result);
                            DeleteNewCancelItem(null, null, result);
                            var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrderPackage);
                            List<OrderItem> aOrderItems = GetOrderItem(result, result2);
                            int result1 = aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrderItems);
                            if ((ConfirmOrderForm.PaymentDetails.IsPrint && paymentStatus) || !paymentStatus)
                            {
                                if (!kitchenPrintOnly && !OrderSaveonly)
                                {
                                    if (paymentStatus)
                                    {
                                        GenerateHtmlPrint(aGeneralInformation.OrderId, true, aPaymentDetails, isKitchen, false, isFinalize);
                                    }
                                    else
                                    {
                                        GenerateHtmlPrint(aGeneralInformation.OrderId, false, aPaymentDetails, isKitchen, isPrint, isFinalize);
                                    }
                                }
                                else
                                {
                                    if (kitchenPrintOnly)
                                    {
                                        GenerateKitchenCopy(aGeneralInformation.OrderId, paymentStatus, aPaymentDetails);
                                    }
                                }
                            }

                            if (paymentStatus || aGeneralInformation.OrderType != "IN")
                            {
                                aRestaurantOrder.OrderStatus = "finished";
                                if (paymentStatus)
                                {
                                    aRestaurantOrder.Status = "Paid";
                                }
                                else
                                {
                                    aRestaurantOrder.Status = "pending";
                                }
                                if (aRestaurantOrder.OnlineOrder > 0 || aRestaurantOrder.OnlineOrderId > 0)
                                {
                                    aRestaurantOrder.OnlineOrderStatus = "accepted";
                                }
                            }
                            else
                            {
                                aRestaurantOrder.OrderStatus = "pending";
                                aRestaurantOrder.Status = "pending";
                                if (aRestaurantOrder.OnlineOrder > 0 || aRestaurantOrder.OnlineOrderId > 0)
                                {
                                    aRestaurantOrder.OnlineOrderStatus = "accepted";
                                }
                            }
                            if (order.OnlineOrder > 0 && order.CardAmount > 0)
                            {
                                aRestaurantOrder = order;
                                aRestaurantOrder.OrderStatus = "finished";
                                aRestaurantOrder.Status = "Paid";
                                if (aRestaurantOrder.OnlineOrder > 0 || aRestaurantOrder.OnlineOrderId > 0)
                                {
                                    aRestaurantOrder.OnlineOrderStatus = "accepted";
                                }
                            }
                            if (order.OnlineOrderId > 0 && aRestaurantOrder.OnlineOrderId <= 0)
                            {
                                aRestaurantOrder.OnlineOrderId = order.OnlineOrderId;
                            }

                            if (aGeneralInformation.CustomerId > 0)
                            {
                                aRestaurantOrder.DeliveryAddress = customerDetailsLabel.Text;
                            }
                            aRestaurantOrder.Id = aGeneralInformation.OrderId;
                            aRestaurantOrder.OrderNo = order.OrderNo;
                            bool res = aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);

                            if(OrderSaveonly)
                            {
                                aRestaurantOrderBLL.UpdateKitchenStatus(aRestaurantOrder.Id);
                            }
                        }
                        if (aGeneralInformation.TableId > 0)
                        {
                            if (paymentStatus)
                            {
                                RestaurantTable aTable = aRestaurantTableBll.GetRestaurantTableByTableId(aGeneralInformation.TableId);
                                aTable.CurrentStatus = "available";
                                aRestaurantTableBll.UpdateRestaurantTable(aTable);
                                aRestaurantOrder.OrderStatus = "finished";
                                if (aTable.MergeStatus > 0)
                                {
                                    aRestaurantTableBll.ToAvailableMergeTable(aTable);
                                }

                                //sync status
                                DataSync.syncTableStatus(aTable.Id, aTable.CurrentStatus);
                            }
                            else
                            {
                                aRestaurantOrder.OrderStatus = "pending";
                            }
                            aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);
                        }
                        
                        ClearAllOrderInformation();
                        ClearTableOrderDetails();
                    }
                    catch (Exception exception)
                    {
                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                        aErrorReportBll.SendErrorReport(exception.ToString());
                    }
                }
                
                if (GlobalSetting.RestaurantInformation.IsSyncOrder > 0 && aRestaurantOrder.OrderStatus == "finished")
                {
                    OrderSyncronize();
                }
                else if(aGeneralInformation.CustomerId > 0)
                {
                    //get customer
                    RestaurantUsers aUser = aRestaurantUserForSearchCustomer.SingleOrDefault(a => a.Id == aGeneralInformation.CustomerId);
                    UserLoginBLL aUserLoginBll = new UserLoginBLL();
                    if (aUser == null)
                    {
                        aUser = aUserLoginBll.GetUserByUserId(aGeneralInformation.CustomerId);
                    }
                    
                    if(aUser.IsUpdate <= 0)
                    {
                        //sync customer
                        DataSync.SyncCustomer(aUser);
                    }
                }                
            }
        }

        /// <summary>
        /// Get CustomerOrder Items
        /// </summary>
        /// <param name="aOrderItems"></param>
        /// <param name="aOrderPackage"></param>
        /// <returns></returns>

        private List<CustomerRecentItemMD> GetCustomerOrderItems(List<OrderItem> aOrderItems, List<OrderPackage> aOrderPackage)
        {
            List<CustomerRecentItemMD> aCustomerRecentItemMds = new List<CustomerRecentItemMD>();
            foreach (OrderItem aOrderItem in aOrderItems)
            {
                if (aCustomerRecentItemMds.All(a => a.recipe_id != aOrderItem.RecipeId))
                {
                    CustomerRecentItemMD aCustomerRecentItemMd = new CustomerRecentItemMD();
                    aCustomerRecentItemMd.customer_id = aGeneralInformation.CustomerId;
                    aCustomerRecentItemMd.recipe_id = aOrderItem.RecipeId;
                    aCustomerRecentItemMd.time_added = DateTime.Now.ToLongDateString();
                    aCustomerRecentItemMd.package_id = 0;
                    aCustomerRecentItemMds.Add(aCustomerRecentItemMd);
                }
            }
            foreach (OrderPackage aOrderItem in aOrderPackage)
            {
                if (aCustomerRecentItemMds.All(a => a.package_id != aOrderItem.PackageId))
                {
                    CustomerRecentItemMD aCustomerRecentItemMd = new CustomerRecentItemMD();
                    aCustomerRecentItemMd.customer_id = aGeneralInformation.CustomerId;
                    aCustomerRecentItemMd.recipe_id = 0;
                    aCustomerRecentItemMd.time_added = DateTime.Now.ToLongDateString();
                    aCustomerRecentItemMd.package_id = aOrderItem.PackageId;
                    aCustomerRecentItemMds.Add(aCustomerRecentItemMd);
                }
            }

            return aCustomerRecentItemMds;
        }

        /// <summary>
        /// Delete Cancel New Item
        /// </summary>
        /// <param name="newOrderItems"></param>
        /// <param name="newPackage"></param>
        /// <param name="orderId"></param>
        
        private void DeleteNewCancelItem(List<OrderItem> newOrderItems, List<OrderPackage> newPackage, int orderId)
        {
            try
            {
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                aRestaurantOrderBLL.DeleteItemsAndPackageByOrderId(orderId);
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        /// <summary>
        /// Generate Html ReciptPrint and Kitichine Print
        /// </summary>
        /// <param name="result"></param>
        /// <param name="status"></param>
        /// <param name="aPaymentDetails"></param>
        /// <param name="isKitchenPrint"></param>
        /// <param name="isPrint"></param>
        /// <param name="isFinalize"></param>
        /// 

        public void GenerateHtmlPrint(int result, bool status, PaymentDetails aPaymentDetails, bool isKitchenPrint = false, bool isPrint = false, bool isFinalize = false)
        {
            if (Properties.Settings.Default.enableWebPrint)
            {
                GenerateHtmlPrintNEW(result, status, aPaymentDetails, isKitchenPrint, isPrint, isFinalize);
                //break;
            }
            else
            {
                try
                {
                    int papersize = 25;
                    string reciept_font = "";
                    RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                    RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
                    RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                    UserLoginBLL aCustomerBll = new UserLoginBLL();
                    RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                    RestaurantOrder aRestaurantOrder = aVariousMethod.GetRestaurantOrderByOrderId(result);
                    int blankLine = 0;
                    List<int> starterIdsForPackage = new List<int>(0);
                    List<int> starterIds = starterIdsForPackage = aRestaurantMenuBLL.GetCategoriesByName("Starter");
                    if (starterIds.Count == 0)
                    {
                        starterIds = starterIdsForPackage = aRestaurantMenuBLL.GetCategoriesByName("Starters");
                    }
                    List<RecipeTypeDetails> aListTypeDetails = orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>().ToList();

                    reciept_font = aRestaurantInformation.RecieptFont;
                    reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
                    string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 2).ToString();
                    string reciept_font_bill = (Convert.ToDouble(reciept_font) - 2).ToString();
                    string reciept_font_bill2 = (Convert.ToDouble(reciept_font_bill) - 2).ToString();
                    string reciept_font_exlgr = "26";

                    string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();
                    PrintContent aPrintContent = new PrintContent();
                    PrintFormat aPrintFormat = new PrintFormat(22);

                    LoadAllPrinter();
                    PrinterSetups = PrinterSetups.Where(a => a.PrintStyle.ToLower() != "kitchen").ToList();
                    List<PrintContent> aPrintContentsRecipe = new List<PrintContent>();

                    foreach (PrinterSetup printer in PrinterSetups)
                    {
                        List<PrintContent> aPrintContentsMid = new List<PrintContent>();
                        if (aRestaurantInformation.RecieptOption != "none")
                        {
                            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantInformation.RestaurantName.ToUpper()) + "\n";
                            aPrintContentsMid.Add(aPrintContent);

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantInformation.House) + "\n";
                            aPrintContentsMid.Add(aPrintContent);

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantInformation.Address) + "\n";
                            aPrintContentsMid.Add(aPrintContent);

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantInformation.Postcode) + "\n";
                            aPrintContentsMid.Add(aPrintContent);

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("TEL:" + aRestaurantInformation.Phone) + "\n";
                            aPrintContentsMid.Add(aPrintContent);

                            if (aRestaurantInformation.VatRegNo != "")
                            {
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantInformation.VatRegNo.ToUpper()) + "\n";
                                aPrintContentsMid.Add(aPrintContent);
                            }

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }

                        string orderHistory = aVariousMethod.GetOrderHistory(papersize, result, aGeneralInformation, timeButton.Text.ToString());

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(orderHistory) + "\n";
                        aPrintContentsMid.Add(aPrintContent);

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\n";
                        aPrintContentsMid.Add(aPrintContent);

                        if (isPrint)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(DateTime.Now.ToString("dddd,dd/MM/yyyy")) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }

                        if (aGeneralInformation.CustomerId > 0)
                        {
                            RestaurantUsers aUser = aRestaurantUserForSearchCustomer.SingleOrDefault(a => a.Id == aGeneralInformation.CustomerId);
                            if (aUser == null)
                            {
                                aUser = aCustomerBll.GetUserByUserId(aGeneralInformation.CustomerId);
                            }

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aUser.Firstname + " " + aUser.Lastname + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                            string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;
                            string address = "";
                            bool flag = false;
                            if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                            {
                                RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                                if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                                {
                                    string[] ss = aORder.DeliveryAddress.Split(',');
                                    flag = true;
                                    if (ss.Count() > 0)
                                    {
                                        address += "," + ss[0];
                                    }
                                    if (ss.Count() > 1)
                                    {
                                        address += ", " + ss[1];
                                    }
                                    if (ss.Count() > 2)
                                    {
                                        address += " " + ss[2];
                                    }
                                    if (ss.Count() > 3)
                                    {
                                        address += " " + ss[3];
                                    }
                                }
                            }

                            if (deliveryButton.Text == "DEL" && deliveryButton.BackColor == Color.Black)
                            {
                                if (address == "")
                                {
                                    if (string.IsNullOrEmpty(aUser.FullAddress))
                                    {
                                        address += "" + aUser.House + " " + aUser.Address;
                                        address += ", " + aUser.City + " " + aUser.Postcode;
                                    }
                                    else
                                    {
                                        address += (aUser.House != "" ? aUser.House + ", " : "");
                                        address += aUser.FullAddress + " " + aUser.Postcode;
                                    }
                                }
                            }

                            if (!flag)
                            {
                                address += " " + cell;
                            }

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(address) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }
                        else if (customerTextBox.Text != "" && customerTextBox.Text != "Search Customer")
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = customerTextBox.Text + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                        }
                        string printStr = "";
                        string printRecipeStr = "";
                        string printFooterStr = "";
                        Dictionary<int, string> recipeTypes = GetRecipeTypes(printer);

                        List<OrderItemMerged> newOrderItemList = new List<OrderItemMerged>();
                        //List<OrderItemDetailsMD> newOrderItemList = new List<OrderItemDetailsMD>();


                        //aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
                        //aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();

                        int countKichneDone = aOrderItemDetailsMDList.Sum(a => a.Qty - a.KitchenDone);

                        int catId = 0;

                        List<MenuType> MenuTypes = new List<MenuType>();

                        //show packages if merged print set to true
                        if (aRestaurantInformation.UseJava > 0)
                        {
                            var pkgCount = 0;
                            foreach (RecipePackageMD package in aRecipePackageMdList)
                            {
                                if (recipeTypes.ContainsKey(package.RecipeTypeId))
                                {
                                    pkgCount++;
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_alignmentString(package.Qty.ToString() + " " + package.PackageName + " " + (package.Qty * package.UnitPrice).ToString("F02"), (package.Qty * package.UnitPrice).ToString("F02").Length) + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);
                                }
                            }
                            if(pkgCount > 0)
                            {
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);                                
                            }

                            //if merge print true, then merge package items together

                            foreach (RecipePackageMD package in aRecipePackageMdList)
                            {
                                if (recipeTypes.ContainsKey(package.RecipeTypeId))
                                {
                                    List<PackageItem> aPaItem =
                                        aPackageItemMdList.Where(
                                        a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex)
                                        .ToList();
                                    List<PackageItem> aPaItemNew = new List<PackageItem>();
                                    foreach (PackageItem item in aPaItem)
                                    {
                                        //item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                                        //aPaItemNew.Add(item);

                                        //check if the same item is already stored
                                        var exitingItem = getExistingOrderItem(newOrderItemList, null, item);
                                        if (exitingItem != null)
                                        {
                                            exitingItem.Qty += item.Qty;
                                            exitingItem.Price += item.Price * item.Qty;
                                        }
                                        else
                                        {
                                            OrderItemMerged orderItemMerged = new OrderItemMerged();
                                            orderItemMerged.CategoryId = item.CategoryId;
                                            orderItemMerged.CatSortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                                            orderItemMerged.ItemFullName = item.ItemFullName != null ? item.ItemFullName : item.ItemName;
                                            orderItemMerged.ItemName = item.ItemName;
                                            orderItemMerged.ItemId = item.ItemId;
                                            orderItemMerged.KitchenProcessing = item.kitchenProcessing;
                                            orderItemMerged.OptionsIndex = item.PackageItemOptionsIndex;
                                            orderItemMerged.OptionName = null;
                                            orderItemMerged.OptionNoOption = item.MinusOption;
                                            orderItemMerged.Price = item.Price * item.Qty;
                                            orderItemMerged.Qty = item.Qty;
                                            newOrderItemList.Add(orderItemMerged);
                                        }
                                    }
                                }
                            }
                        }

                        foreach (RecipeTypeDetails typeDetails in aListTypeDetails)
                        {
                            if (typeDetails.recipeTypelabel.Visible)
                            {
                                if (typeDetails.typeflowLayoutPanel1.Visible == false)
                                {
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_alignmentString(typeDetails.recipeTypelabel.Text + " " + " " + typeDetails.recipeTypeAmountlabel.Text, typeDetails.recipeTypeAmountlabel.Text.Length) + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);

                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);
                                }
                            }
                            if (typeDetails.typeflowLayoutPanel1.Visible)
                            {
                                foreach (OrderItemDetailsMD item_details in aOrderItemDetailsMDList)
                                {
                                    if (item_details.RecipeTypeId == typeDetails.RecipeTypeId && recipeTypes.ContainsKey(item_details.RecipeTypeId))
                                    {
                                        //check if the same item is already stored
                                        var exitingItem = getExistingOrderItem(newOrderItemList, item_details);

                                        if (exitingItem != null)
                                        {
                                            exitingItem.Qty += item_details.Qty;
                                            exitingItem.Price += item_details.Price * item_details.Qty;
                                        }
                                        else
                                        {
                                            OrderItemMerged orderItemMerged = new OrderItemMerged();
                                            orderItemMerged.CategoryId = item_details.CategoryId;
                                            orderItemMerged.CatSortOrder = item_details.CatSortOrder;
                                            orderItemMerged.ItemFullName = item_details.ItemFullName != null ? item_details.ItemFullName : item_details.ItemName;
                                            orderItemMerged.ItemName = item_details.ItemName;
                                            orderItemMerged.ItemId = item_details.ItemId;
                                            orderItemMerged.ItemOption = item_details.ItemOption;
                                            orderItemMerged.KitchenDone = item_details.KitchenDone;
                                            orderItemMerged.KitchenProcessing = item_details.KitchenProcessing;
                                            orderItemMerged.KitchenSection = item_details.KitchenSection;
                                            orderItemMerged.OptionsIndex = item_details.OptionsIndex;
                                            orderItemMerged.OptionName = item_details.OptionName;
                                            orderItemMerged.OptionNoOption = item_details.OptionNoOption;
                                            orderItemMerged.Option_ids = item_details.Option_ids;
                                            orderItemMerged.Price = item_details.Price * item_details.Qty;
                                            orderItemMerged.Qty = item_details.Qty;
                                            orderItemMerged.RecipeTypeId = item_details.RecipeTypeId;
                                            orderItemMerged.sendToKitchen = item_details.sendToKitchen;
                                            orderItemMerged.SortOrder = item_details.SortOrder;
                                            newOrderItemList.Add(orderItemMerged);
                                        }
                                    }
                                }                                
                            }
                        }

                        bool startdas = false;
                        //sort by category
                        newOrderItemList = newOrderItemList.OrderBy(a => a.CatSortOrder).ToList();
                        MenuType type = new MenuType();
                        foreach (OrderItemMerged itemDetails in newOrderItemList)
                        {
                            if (aRestaurantInformation.MenuSeparation == 3 && startdas && !starterIds.Contains(itemDetails.CategoryId))
                            {
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                startdas = false;
                            }

                            double itemPrice = itemDetails.Price;
                            aPrintContent = new PrintContent();

                            aPrintContent.StringLine = aPrintFormat.get_alignmentString(itemDetails.Qty + " " + (isPrint ? itemDetails.ItemFullName : itemDetails.ItemName) + " " + (itemPrice > 0 ? itemPrice.ToString("F02") : ""), itemPrice > 0 ? (itemPrice).ToString("F02").Length : 0) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                            string options = "";
                            List<RecipeOptionMD> aOption =
                                aRecipeOptionMdList.Where(
                                    a =>
                                        a.RecipeId == itemDetails.ItemId &&
                                        a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                            if (aOption.Count > 0)
                            {
                                foreach (RecipeOptionMD option in aOption)
                                {
                                    if (!string.IsNullOrEmpty(option.Title))
                                    {
                                        aPrintContent = new PrintContent();
                                        aPrintContent.StringLine = aPrintFormat.get_fullString("  → " + option.Title) + "\r\n";
                                        aPrintContentsMid.Add(aPrintContent);
                                        blankLine++;
                                    }
                                    if (!string.IsNullOrEmpty(option.MinusOption))
                                    {
                                        aPrintContent = new PrintContent();
                                        aPrintContent.StringLine = aPrintFormat.get_fullString("  →No " + option.MinusOption) + "\r\n";
                                        aPrintContentsMid.Add(aPrintContent);
                                        blankLine++;
                                    }
                                }
                            }
                            blankLine++;

                            if (aRestaurantInformation.MenuSeparation == 2 && !isPrint)
                            {
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                            }

                            if (starterIds.Contains(itemDetails.CategoryId))
                            {
                                startdas = true;
                            }
                        }

                        if (aRestaurantInformation.UseJava <= 0)
                        {                            
                            foreach (RecipePackageMD package in aRecipePackageMdList)
                            {
                                if (recipeTypes.ContainsKey(package.RecipeTypeId))
                                {
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_alignmentString(package.Qty.ToString() + " " + package.PackageName + " " + (package.Qty * package.UnitPrice).ToString("F02"), (package.Qty * package.UnitPrice).ToString("F02").Length) + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);

                                    blankLine++;
                                    List<PackageItem> aPaItem =
                                        aPackageItemMdList.Where(
                                            a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex)
                                            .ToList();
                                    List<PackageItem> aPaItemNew = new List<PackageItem>();
                                    foreach (PackageItem item in aPaItem)
                                    {
                                        item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                                        aPaItemNew.Add(item);
                                    }
                                    aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();
                                    bool startdasForPackage = false;
                                    foreach (PackageItem itemDetails in aPaItem)
                                    {
                                        string packageItemPrice = itemDetails.Price > 0
                                            ? (itemDetails.Price).ToString("F02")
                                            : "";

                                        if (aRestaurantInformation.MenuSeparation == 3 && startdasForPackage && !starterIdsForPackage.Contains(itemDetails.CategoryId))
                                        {
                                            aPrintContent = new PrintContent();
                                            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                                            aPrintContentsMid.Add(aPrintContent);
                                            startdasForPackage = false;
                                        }

                                        aPrintContent = new PrintContent();
                                        if (packageItemPrice.Length > 0)
                                        {
                                            aPrintContent.StringLine = aPrintFormat.get_alignmentString(" " + " " + itemDetails.Qty.ToString() + " " + itemDetails.ItemName + " " + packageItemPrice, packageItemPrice.Length) + "\r\n";
                                        }
                                        else
                                        {
                                            aPrintContent.StringLine = aPrintFormat.get_alignmentString(" " + " " + itemDetails.Qty.ToString() + " " + itemDetails.ItemName + " " + "") + "\r\n";
                                        }
                                        aPrintContentsMid.Add(aPrintContent);


                                        if (starterIdsForPackage.Contains(itemDetails.CategoryId))
                                        {
                                            startdasForPackage = true;
                                        }
                                        blankLine++;
                                        string options = "";
                                        List<RecipeOptionMD> aOption =
                                            aRecipeOptionMdList.Where(
                                                a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex && a.PackageItemOptionsIndex == itemDetails.PackageItemOptionsIndex)
                                                .ToList();
                                        if (aOption.Count > 0)
                                        {

                                            foreach (RecipeOptionMD option in aOption)
                                            {
                                                if (!string.IsNullOrEmpty(option.Title))
                                                {

                                                    aPrintContent = new PrintContent();
                                                    aPrintContent.StringLine = aPrintFormat.get_fullString(" " + " " + " \r→" + option.Title) + "\r\n";
                                                    aPrintContentsMid.Add(aPrintContent);
                                                    blankLine++;
                                                }
                                                if (!string.IsNullOrEmpty(option.MinusOption))
                                                {

                                                    aPrintContent = new PrintContent();
                                                    aPrintContent.StringLine = aPrintFormat.get_fullString(" " + " " + " \r→No" + option.MinusOption) + "\r\n";
                                                    aPrintContentsMid.Add(aPrintContent);

                                                    blankLine++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        foreach (RecipeMultipleMD package in aRecipeMultipleMdList)
                        {
                            if (recipeTypes.ContainsKey(package.RecipeTypeId))
                            {
                                //printRecipeStr += "<tr><td style='font-weight:bold; font-size:" + reciept_font +
                                //                     "px'>" +
                                //                     package.Qty.ToString() + " " + getStringForPrint(package.MultiplePartName) +
                                //                     "</td><td align='right'>" +
                                //                     (package.Qty * package.UnitPrice).ToString("F02") + "</td></tr>";
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_alignmentString(package.Qty.ToString() + " " + package.MultiplePartName + " " + (package.Qty * package.UnitPrice).ToString("F02"), (package.Qty * package.UnitPrice).ToString("F02").Length) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);

                                blankLine++;
                                List<MultipleItemMD> aPaItem =
                                    aMultipleItemMdList.Where(
                                        a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                                        .ToList();
                                int cnt = 0;
                                foreach (MultipleItemMD itemDetails in aPaItem)
                                {
                                    cnt++;
                                    string packageItemPrice = itemDetails.Price > 0
                                        ? (itemDetails.Price).ToString()
                                        : "";

                                    //printRecipeStr += "<tr><td style='font-weight:bold; font-size: " + reciept_font_small +
                                    //                  "px'>" +
                                    //                  (cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) + ": " +
                                    //                  getStringForPrint(itemDetails.ItemName) +
                                    //                  "</td><td></td></tr>";
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_alignmentString(" " + " " + (cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) + ": " + itemDetails.ItemName + " " + " " + packageItemPrice) + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);

                                    blankLine++;
                                    string options = "";
                                    List<RecipeOptionMD> aOption =
                                        aRecipeOptionMdList.Where(
                                            a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                                            .ToList();
                                    if (aOption.Count > 0)
                                    {

                                        foreach (RecipeOptionMD option in aOption)
                                        {
                                            if (!string.IsNullOrEmpty(option.Title))
                                            {
                                                //printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                                //                  reciept_font_small +
                                                //                  "px'><span style='text-align:left;float:left;padding-left:10px'>" +
                                                //                  getStringForPrint(option.Title) +
                                                //                  "</span></td><td></td></tr>";
                                                aPrintContent = new PrintContent();
                                                aPrintContent.StringLine = aPrintFormat.get_fullString(" " + " " + " \r→" + option.Title) + "\r\n";
                                                aPrintContentsMid.Add(aPrintContent);

                                                blankLine++;
                                            }
                                            if (!string.IsNullOrEmpty(option.MinusOption))
                                            {
                                                //printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                                //                  reciept_font_small +
                                                //                  "px'><span style='text-align:left;float:left;padding-left:10px'>" +
                                                //                  getStringForPrint("→No" + option.MinusOption) +
                                                //                  "</span></td><td></td></tr>";

                                                aPrintContent = new PrintContent();
                                                aPrintContent.StringLine = aPrintFormat.get_fullString(" " + " " + " \r→No" + option.MinusOption) + "\r\n";
                                                aPrintContentsMid.Add(aPrintContent);
                                                blankLine++;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        int numOfLine = blankLine;

                        double subamount = GetTotalAmountDetails();
                        subamount = GlobalVars.numberRound(subamount, 2);

                        if (blankLine < aRestaurantInformation.RecieptMinHeight)
                        {
                            for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                            {
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = " " + " " + "  " + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                numOfLine++;
                            }
                        }

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_alignmentString(" SUBTOTAL  £" + subamount.ToString("F02"), ("£" + subamount.ToString("F02")).Length) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);

                        double amount = GetTotalAmountDetails() + aGeneralInformation.CardFee -
                                       GlobalVars.numberRound(aGeneralInformation.DiscountFlat) - GlobalVars.numberRound(aGeneralInformation.ItemDiscount);

                        amount = GlobalVars.numberRound(amount, 2);                        

                        string extraPanel = "";

                        if (aGeneralInformation.DiscountFlat > 0)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_alignmentString("Discount (" + aGeneralInformation.DiscountPercent.ToString("F02") + "%) " + aGeneralInformation.DiscountFlat.ToString("F02"), aGeneralInformation.DiscountFlat.ToString("F02").Length) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                            numOfLine++;
                        }

                        if (aGeneralInformation.CardFee > 0)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_alignmentString("S/C " + aGeneralInformation.CardFee.ToString("F02"), aGeneralInformation.CardFee.ToString("F02").Length) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                            numOfLine++;
                        }

                        if (aGeneralInformation.DeliveryCharge > 0 && deliveryButton.Text == "DEL" &&
                            deliveryButton.BackColor == Color.Black && collectionButton.BackColor != Color.Black)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_alignmentString("D/C  " + aGeneralInformation.DeliveryCharge.ToString("F02"), aGeneralInformation.DeliveryCharge.ToString("F02").Length) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                            amount += aGeneralInformation.DeliveryCharge;
                            numOfLine++;
                        }

                        if (customTotalTextBox.Text != "Custom Total" && Convert.ToDouble(customTotalTextBox.Text) > 0)
                        {
                            amount = GlobalVars.numberRound(Convert.ToDouble(customTotalTextBox.Text), 2);
                        }
                        if (extraPanel != "")
                        {
                            printFooterStr += extraPanel;

                        }

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_alignmentString(DateTime.Now.ToString("hh:mmtt") + "  TOTAL  £" + amount.ToString("F02"), ("£" + amount.ToString("F02")).Length) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);

                        numOfLine++;
                        if (aRestaurantOrder.OnlineOrder > 0)
                        {
                            if (aRestaurantOrder.CardAmount > 0)
                            {
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = "PAID BY CARD   " + "£" + aRestaurantOrder.CardAmount.ToString("F02") + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                            }
                            else
                            {
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = "ORDER NOT PAID   " + "£" + aRestaurantOrder.CashAmount.ToString("F02") + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                            }
                            numOfLine++;
                        }
                        else
                        {
                            if (aPaymentDetails.CashAmount > 0 && aPaymentDetails.CardAmount > 0 && status)
                            {
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_alignmentString("CASH  £" + aRestaurantOrder.CashAmount.ToString("F02"), ("£" + aRestaurantOrder.CashAmount.ToString("F02")).Length) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                numOfLine++;
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_alignmentString("PAID BY CARD  £" + aRestaurantOrder.CardAmount.ToString("F02"), ("£" + aRestaurantOrder.CardAmount.ToString("F02")).Length) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                numOfLine++;
                            }
                            else if (status)
                            {
                                if (aPaymentDetails.CardAmount > 0)
                                {
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_alignmentString("PAID BY CARD  £" + aRestaurantOrder.CardAmount.ToString("F02"), ("£" + aRestaurantOrder.CardAmount.ToString("F02")).Length) + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);
                                }
                                else
                                {
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_alignmentString("CASH  £" + aRestaurantOrder.CashAmount.ToString("F02"), ("£" + aRestaurantOrder.CashAmount.ToString("F02")).Length) + "\r\n";
                                    aPrintContentsMid.Add(aPrintContent);
                                }
                                numOfLine++;
                            }
                            if (status)
                            {
                                numOfLine++;
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_alignmentString("CHANGE     £" + aPaymentDetails.ChangeAmount.ToString("F02"), ("£" + aPaymentDetails.ChangeAmount.ToString("F02")).Length) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("  PAID ORDER  ") + "\n";
                                aPrintContentsMid.Add(aPrintContent);
                                numOfLine++;
                            }
                        }

                        if (commentTextBox.Text != "Comment" && commentTextBox.Text.Trim().Length > 0)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(commentTextBox.Text) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                            numOfLine++;
                        }
                        if (aRestaurantInformation.ThankYouMsg.Length > 5)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(aRestaurantInformation.ThankYouMsg) + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }

                        int printCopy = 1;
                        if (printer.PrintStyle == "Receipt")
                        {
                            if (aGeneralInformation.OrderType == "IN")
                            {
                                printCopy = aRestaurantInformation.DineInPrintCopy;
                            }
                            else if (aGeneralInformation.OrderType == "Takeaway" || aGeneralInformation.OrderType == "CLT")
                            {
                                printCopy = aRestaurantInformation.PrintCopy;
                            }
                            else
                            {
                                printCopy = aRestaurantInformation.DelPrintCopy;
                            }
                        }

                        if (aRestaurantInformation.ShowOrderNumber > 0)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(aRestaurantOrder.OrderNo.ToString("D2")) + "\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }

                        string str = "";
                        string allpage = "";
                        for (int i = 0; i < aPrintContentsMid.Count; i++)
                        {
                            allpage += aPrintContentsMid[i].StringLine;
                        }
                        //  aPrintContentsMid.Clear();

                        if (isPrint)
                        {
                            PrintMethods tempPrintMethods = new PrintMethods(true, true);
                            tempPrintMethods.USBPrint(allpage, printer.PrinterName, 1);
                        }
                        else
                        {
                            bool isBillPrint = printer.PrintStyle == "Bill" ? true : false;
                            PrintMethods tempPrintMethods = new PrintMethods(false, false, printer.printerMargin, isBillPrint);
                            tempPrintMethods.USBPrint(allpage, printer.PrinterName, printCopy);
                        }
                    }

                    if (!(isPrint || isFinalize))
                    {
                        GenerateKitchenCopy(result, status, aPaymentDetails);
                    }
                }
                catch (Exception ex)
                {
                    new ErrorReportBLL().SendErrorReport(ex.GetBaseException().Message);
                }
            }
        }

        public void GenerateHtmlPrintNEW(int result, bool status, PaymentDetails aPaymentDetails, bool isKitchenPrint = false, bool isPrint = false, bool isFinalize = false)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToString("HH : mm : ss"));
                string tableWidth = "250";
                string printHeaderStr = "<table width='" + tableWidth + "' style='text-align:center;font-size:16px'>";
                int papersize = 25;
                string reciept_font = "";
                RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                UserLoginBLL aCustomerBll = new UserLoginBLL();
                RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

                RestaurantOrder aRestaurantOrder = aVariousMethod.GetRestaurantOrderByOrderId(result);
                int blankLine = 0;
                int starterId = aRestaurantMenuBLL.GetCategoryByName("Starter");

                List<RecipeTypeDetails> aListTypeDetails = orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>().ToList();

                reciept_font = aRestaurantInformation.RecieptFont;
                reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
                string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 2).ToString();
                string reciept_font_bill = (Convert.ToDouble(reciept_font) - 2).ToString();
                string reciept_font_bill2 = (Convert.ToDouble(reciept_font_bill) - 2).ToString();
                string reciept_font_exlgr = "26";

                string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();

                if (aRestaurantInformation.RecieptOption == "logo_title")
                {
                    string path = @"Image/" + aRestaurantInformation.Id + "_website_logo.png";
                    if (File.Exists(path))
                    {
                        var imageString = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(path)));
                        printHeaderStr += "<tr><td colspan='2' style='text-align:center'><img style='width:120px; height: 60px' src='" + imageString + "'></td></tr>";
                    }
                }
                if (aRestaurantInformation.RecieptOption != "none")
                {
                    printHeaderStr += "<tr><td colspan='2' style='text-align:center;font-size:16px;font-weight: bold;'><b>" +
                                         aRestaurantInformation.RestaurantName.ToUpper() + "</b><br>" +
                                         aRestaurantInformation.House + ", " + aRestaurantInformation.Address + " " +
                                         aRestaurantInformation.Postcode + "<br>TEL:" + aRestaurantInformation.Phone + "<br>" +
                                         aRestaurantInformation.VatRegNo + "</td></tr>";
                }

                // string dashLine = "-------------------------------";
                string dashLine = "-------------------------------------";
                if (isPrint)
                {
                    dashLine = "-------------------------------------";

                }

                printHeaderStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                string orderHistory = aVariousMethod.GetOrderHistory(papersize, result, aGeneralInformation, timeButton.Text.ToString());
                if (isPrint)
                {
                    printHeaderStr += "<tr><td colspan='2' style='text-align:center;font-size:" + reciept_font_lgr + "px'>" + orderHistory + "</td></tr>";
                }
                else
                {
                    printHeaderStr += "<tr><td colspan='2' style='text-align:center;font-size:" + reciept_font_lgr + "px'><b>" + orderHistory + "</b></td></tr>";
                }
                printHeaderStr += "<tr><td colspan='2' style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";

                if (isPrint)
                {
                    printHeaderStr += "<tr><td colspan='2'><span style='text-align:center;'>" + DateTime.Now.ToString("dddd,dd/MM/yyyy") + "</span></td></tr>";
                    printHeaderStr += "<tr><td colspan='2' style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                }


                if (aGeneralInformation.CustomerId > 0)
                {
                    printHeaderStr += "<tr><td colspan='2' style='text-align:left; font-size:" + reciept_font + "px;'>";

                    RestaurantUsers aUser = aRestaurantUserForSearchCustomer.SingleOrDefault(a => a.Id == aGeneralInformation.CustomerId);
                    if (aUser == null)
                    {
                        aUser = aCustomerBll.GetUserByUserId(aGeneralInformation.CustomerId);
                    }

                    printHeaderStr += aUser.Firstname + " " + aUser.Lastname + "<br>";
                    string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;

                    string address = "";
                    bool flag = false;
                    if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                    {
                        RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                        if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                        {
                            string[] ss = aORder.DeliveryAddress.Split(',');
                            flag = true;
                            if (ss.Count() > 0)
                            {
                                address += "," + ss[0];
                            }
                            if (ss.Count() > 1)
                            {
                                address += ", " + ss[1];
                            }
                            if (ss.Count() > 2)
                            {
                                address += "<br>" + ss[2];
                            } if (ss.Count() > 3)
                            {
                                address += ", " + ss[3];
                            } 
                        }
                    }

                    if (deliveryButton.Text == "DEL" && deliveryButton.BackColor == Color.Black)
                    {
                        if (address == "")
                        {
                            if (string.IsNullOrEmpty(aUser.FullAddress))
                            {
                                address += "" + aUser.House + " " + aUser.Address;
                                address += ", " + aUser.City + "<br>" + aUser.Postcode;
                            }
                            else
                            {
                                address += (aUser.House != "" ? aUser.House + ", " : "");
                                address += aUser.FullAddress + "<br>" + aUser.Postcode;
                            }
                        }
                    }

                    printHeaderStr += "</td></tr>";

                    // address = Regex.Split(address, "<br>","");   
                    if (!flag)
                    {
                        printHeaderStr += "<tr><td colspan='2' style='text-align:left;font-size:" + reciept_font + "px'>" + address + " " + cell + "</td></tr>";
                    }
                    else
                    {
                        printHeaderStr += "<tr><td colspan='2' style='text-align:left;font-size:" + reciept_font + "px'>" + address + "</td></tr>";
                    }

                    printHeaderStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                }
                else if (customerTextBox.Text != "" && customerTextBox.Text != "Search Customer")
                {
                    printHeaderStr +=
                        "<tr><td colspan='2' style='text-align:left;font-size:" + reciept_font + "px'><b>" + customerTextBox.Text + "</b></td></tr>";
                    printHeaderStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";

                }

                printHeaderStr += "</table>";
                LoadAllPrinter();
                PrinterSetups = PrinterSetups.Where(a => a.PrintStyle != "Kitchen").ToList();

                foreach (PrinterSetup printer in PrinterSetups)
                {
                    string printStr = "";
                    string printRecipeStr = "";
                    string printFooterStr = "";

                    Dictionary<int, string> recipeTypes = GetRecipeTypes(printer);

                    List<OrderItemDetailsMD> newOrderItemList = new List<OrderItemDetailsMD>();

                    aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
                    aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();

                    int countKichneDone = aOrderItemDetailsMDList.Sum(a => a.Qty - a.KitchenDone);

                    int catId = 0;
                    bool startdas = false;
                    printRecipeStr = "<table width='" + tableWidth + "'   align='left'>";

                    List<MenuType> MenuTypes = new List<MenuType>();

                    foreach (RecipeTypeDetails typeDetails in aListTypeDetails)
                    {
                        if (typeDetails.recipeTypelabel.Visible)
                        {
                            if (typeDetails.typeflowLayoutPanel1.Visible == false)
                            {
                                printRecipeStr += "<tr><td style='font-weight:bold; font-size:" + reciept_font_lgr + "px'>" + typeDetails.recipeTypelabel.Text + "</td><td align='right'>" + typeDetails.recipeTypeAmountlabel.Text + "</td></tr>";
                            }
                        }
                        if (typeDetails.typeflowLayoutPanel1.Visible)
                        {
                            foreach (OrderItemDetailsMD item_details in aOrderItemDetailsMDList)
                            {
                                if (item_details.RecipeTypeId == typeDetails.RecipeTypeId && recipeTypes.ContainsKey(item_details.RecipeTypeId))
                                {
                                    newOrderItemList.Add(item_details);
                                }
                            }
                        }
                        // menu wise change
                        //int recipeTypeLine = aRestaurantMenuBLL.GetTypeStatusByID(typeDetails.RecipeTypeId);
                        //if (aRestaurantInformation.MenuSeparation == 6 && recipeTypeLine ==1)
                        //{
                        //    MenuType empty_type = new MenuType();
                        //    empty_type.Id = typeDetails.RecipeTypeId;
                        //    empty_type.has_underline = true;
                        //    empty_type.status = true;
                        //    MenuTypes.Add(empty_type);
                        //}

                    }

                    newOrderItemList = newOrderItemList.OrderBy(a => a.CatSortOrder).ToList();
                    MenuType type = new MenuType();
                    foreach (OrderItemDetailsMD itemDetails in newOrderItemList)
                    {
                        if (aRestaurantInformation.MenuSeparation == 3 && startdas && starterId != itemDetails.CategoryId)
                        {
                            printRecipeStr += "<tr><td colspan='2' style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                            startdas = false;
                        }
                        // menu wise change
                        //MenuType mtype = MenuTypes.FirstOrDefault(a => a.Id == itemDetails.RecipeTypeId);
                        //if (mtype != null)
                        //{  
                        //    type = mtype;
                        //}
                        //if (aRestaurantInformation.MenuSeparation == 6 &&  type.status &&  type.Id != itemDetails.RecipeTypeId)
                        //{
                        //    printRecipeStr += "<tr><td colspan='2' style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                        //    type.status = false;
                        //}
                        if (isPrint)
                        {
                            // string ItemName
                            printRecipeStr += "<tr><td style='font-size:" + reciept_font_bill +
                                              "px'>" +
                                              itemDetails.Qty + " " + getStringForPrint(itemDetails.ItemFullName, 25) +
                                              "</td><td align='right' style='font-size:" + reciept_font_bill +
                                              "px'>" +
                                              (itemDetails.Qty * itemDetails.Price).ToString("F02") +
                                              "</td></tr>";
                            string options = "";
                            List<RecipeOptionMD> aOption =
                                aRecipeOptionMdList.Where(
                                    a =>
                                        a.RecipeId == itemDetails.ItemId &&
                                        a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                            if (aOption.Count > 0)
                            {
                                foreach (RecipeOptionMD option in aOption)
                                {
                                    if (!string.IsNullOrEmpty(option.Title))
                                    {
                                        printRecipeStr += "<tr><td style='font-size: " +
                                                          reciept_font_bill2 +
                                                          "px'><span style='padding-left:10px'>" +
                                                          "→" + getStringForPrint(option.Title, 25) + "</span></td><td align='right'><span style='text-style:italic'>&nbsp;</span>" +
                                                          "</td></tr>";
                                        blankLine++;
                                    }
                                    if(!string.IsNullOrEmpty(option.MinusOption))
                                    {
                                        printRecipeStr += "<tr><td style='font-size: " +
                                                          reciept_font_bill2 + "px'><span style='padding-left:10px'>" +
                                                          getStringForPrint("→No" + option.MinusOption, 25) +
                                                         "</span></td><td>&nbsp;</td></tr>";
                                        blankLine++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            printRecipeStr += "<tr><td style='font-weight:bold;font-size:" + reciept_font +
                                              "px'>" +
                                              itemDetails.Qty + " " + getStringForPrint(itemDetails.ItemName) +
                                              "</td><td align='right'>" +
                                              (itemDetails.Qty * itemDetails.Price).ToString("F02") +
                                              "</td></tr>";

                            string options = "";
                            List<RecipeOptionMD> aOption =
                                aRecipeOptionMdList.Where(
                                    a =>
                                        a.RecipeId == itemDetails.ItemId &&
                                        a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                            if (aOption.Count > 0)
                            {
                                foreach (RecipeOptionMD option in aOption)
                                {
                                    if (!string.IsNullOrEmpty(option.Title))
                                    {
                                        printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                                          reciept_font_small +
                                                          "px'><span style='padding-left:10px'>" +
                                                          "→" + getStringForPrint(option.Title) + "</span></td><td align='right'><span style='text-style:italic'>" + "&nbsp;" + "</span>" +
                                                          "</td></tr>";
                                        blankLine++;
                                    } if (!string.IsNullOrEmpty(option.MinusOption))
                                    {
                                        printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                                          reciept_font_small + "px'><span style='padding-left:10px'>" +
                                                          getStringForPrint("→No" + option.MinusOption) +
                                                         "</span></td><td>&nbsp;</td></tr>";
                                        blankLine++;
                                    }
                                }
                            }
                        }
                        blankLine++;
                        if (aRestaurantInformation.MenuSeparation == 2 && !isPrint)
                        {
                            printRecipeStr += "<tr><td colspan='2' style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                        }

                        if (starterId == itemDetails.CategoryId)
                        {
                            startdas = true;
                        }
                    }

                    if (!isPrint)
                    {
                        foreach (RecipePackageMD package in aRecipePackageMdList)
                        {
                            if (recipeTypes.ContainsKey(package.RecipeTypeId))
                            {

                                printRecipeStr += "<tr><td style='font-weight:bold; font-size:" + reciept_font + "px'>" +
                                                  package.Qty.ToString() + " " + getStringForPrint(package.PackageName) +
                                                  "</td><td align='right'>" +
                                                  (package.Qty * package.UnitPrice).ToString("F02") + "</td></tr>";
                                blankLine++;
                                List<PackageItem> aPaItem =
                                    aPackageItemMdList.Where(
                                        a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex)
                                        .ToList();
                                List<PackageItem> aPaItemNew = new List<PackageItem>();
                                foreach (PackageItem item in aPaItem)
                                {
                                    item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                                    aPaItemNew.Add(item);
                                }
                                aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();

                                foreach (PackageItem itemDetails in aPaItem)
                                {
                                    string packageItemPrice = itemDetails.Price > 0
                                        ? (itemDetails.Price).ToString()
                                        : "";

                                    printRecipeStr += "<tr><td style='font-weight:bold; font-size: " + reciept_font_small +
                                                      "px'>" +
                                                      itemDetails.Qty.ToString() + "  " + getStringForPrint(itemDetails.ItemName) +
                                                      "</td><td align='right'>" +
                                                      packageItemPrice + "</td></tr>";
                                    blankLine++;
                                    string options = "";
                                    List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex && a.PackageItemOptionsIndex == itemDetails.PackageItemOptionsIndex).ToList();
                                    if (aOption.Count > 0)
                                    {

                                        foreach (RecipeOptionMD option in aOption)
                                        {
                                            if (!string.IsNullOrEmpty(option.Title))
                                            {
                                                printRecipeStr += "<tr><td style='font-weight:bold; font-size: " + reciept_font_small +
                                                                  "px'><span style='text-align:left;float:left;padding-left:10px'>" +
                                                                  "→" + getStringForPrint(option.Title) +
                                                                  "</span></td><td align='right'>" + option.Price + "</td></tr>";
                                                blankLine++;
                                            }
                                            if (!string.IsNullOrEmpty(option.MinusOption))
                                            {
                                                printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                                                  reciept_font_small +
                                                                  "px'><span style='text-align:left;padding-left:10px'>" +
                                                                  getStringForPrint("→No" + option.MinusOption) +
                                                                  "</span></td><td></td></tr>";
                                                blankLine++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (RecipePackageMD package in aRecipePackageMdList)
                        {
                            if (recipeTypes.ContainsKey(package.RecipeTypeId))
                            {

                                printRecipeStr += "<tr><td style='font-size:" + reciept_font_bill + "px'>" +
                                                  package.Qty.ToString() + " " + getStringForPrint(package.PackageName, 25) +
                                                  "</td><td align='right' style='font-size: " + reciept_font_bill +
                                                      "px'>" +
                                                  (package.Qty * package.UnitPrice).ToString("F02") + "</td></tr>";
                                blankLine++;
                                List<PackageItem> aPaItem =
                                    aPackageItemMdList.Where(
                                        a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex)
                                        .ToList();
                                List<PackageItem> aPaItemNew = new List<PackageItem>();
                                foreach (PackageItem item in aPaItem)
                                {
                                    item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                                    aPaItemNew.Add(item);
                                }
                                aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();

                                foreach (PackageItem itemDetails in aPaItem)
                                {
                                    string packageItemPrice = itemDetails.Price > 0
                                        ? (itemDetails.Price).ToString("F02")
                                        : "";

                                    printRecipeStr += "<tr><td style='font-size: " + reciept_font_bill +
                                                      "px'>" +
                                                      itemDetails.Qty.ToString() + "  " + getStringForPrint(itemDetails.ItemName, 25) +
                                                      "</td><td align='right' style='font-size: " + reciept_font_bill +
                                                      "px'>" +
                                                      packageItemPrice + "</td></tr>";
                                    blankLine++;
                                    string options = "";
                                    List<RecipeOptionMD> aOption =
                                        aRecipeOptionMdList.Where(
                                            a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                                            .ToList();
                                    if (aOption.Count > 0)
                                    {

                                        foreach (RecipeOptionMD option in aOption)
                                        {
                                            if (!string.IsNullOrEmpty(option.Title))
                                            {
                                                printRecipeStr += "<tr><td style='font-size: " + reciept_font_bill2 +
                                                                  "px'><span style='text-align:left;float:left;padding-left:10px'>" +
                                                                  "→" + getStringForPrint(option.Title, 25) +
                                                                  "</span></td><td align='right'>&nbsp;</td></tr>";
                                                blankLine++;
                                            }
                                            if (!string.IsNullOrEmpty(option.MinusOption))
                                            {
                                                printRecipeStr += "<tr><td style='font-size: " +
                                                                  reciept_font_bill2 +
                                                                  "px'><span style='text-align:left;padding-left:10px'>" +
                                                                  getStringForPrint("→No" + option.MinusOption, 25) +
                                                                  "</span></td><td></td></tr>";
                                                blankLine++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (RecipeMultipleMD package in aRecipeMultipleMdList)
                    {
                        if (recipeTypes.ContainsKey(package.RecipeTypeId))
                        {
                            printRecipeStr += "<tr><td style='font-weight:bold; font-size:" + reciept_font +
                                                 "px'>" +
                                                 package.Qty.ToString() + " " + getStringForPrint(package.MultiplePartName) +
                                                 "</td><td align='right'>" +
                                                 (package.Qty * package.UnitPrice).ToString("F02") + "</td></tr>";
                            blankLine++;
                            List<MultipleItemMD> aPaItem =
                                aMultipleItemMdList.Where(
                                    a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                                    .ToList();
                            int cnt = 0;
                            foreach (MultipleItemMD itemDetails in aPaItem)
                            {
                                cnt++;
                                string packageItemPrice = itemDetails.Price > 0
                                    ? (itemDetails.Price).ToString()
                                    : "";

                                printRecipeStr += "<tr><td style='font-weight:bold; font-size: " + reciept_font_small +
                                                  "px'>" +
                                                  (cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) + ": " +
                                                  getStringForPrint(itemDetails.ItemName) +
                                                  "</td><td></td></tr>";
                                blankLine++;
                                string options = "";
                                List<RecipeOptionMD> aOption =
                                    aRecipeOptionMdList.Where(
                                        a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                                        .ToList();
                                if (aOption.Count > 0)
                                {
                                    foreach (RecipeOptionMD option in aOption)
                                    {
                                        if (!string.IsNullOrEmpty(option.Title))
                                        {
                                            printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;padding-left:10px'>" +
                                                              getStringForPrint(option.Title) +
                                                              "</span></td><td></td></tr>";
                                            blankLine++;
                                        }
                                        if (!string.IsNullOrEmpty(option.MinusOption))
                                        {
                                            printRecipeStr += "<tr><td style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;padding-left:10px'>" +
                                                              getStringForPrint("→No" + option.MinusOption) +
                                                              "</span></td><td></td></tr>";
                                            blankLine++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    int numOfLine = blankLine;

                    double amount = GetTotalAmountDetails() + aGeneralInformation.CardFee -
                                    aGeneralInformation.DiscountFlat - aGeneralInformation.ItemDiscount;

                    amount = GlobalVars.numberRound(amount, 2);
                    if (blankLine < aRestaurantInformation.RecieptMinHeight)
                    {
                        for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                        {
                            printRecipeStr += "<tr><td>&nbsp;</td><td>&nbsp;</td></tr>";
                            numOfLine++;
                        }
                    }

                    string extraPanel = "";

                    if (aGeneralInformation.DiscountFlat > 0)
                    {
                        extraPanel += "<tr><td style='font-weight:bold; font-size:" + reciept_font +
                                          "px'> Discount</td><td  align='right'>(" +
                                          aGeneralInformation.DiscountPercent.ToString("F02") + "%) £" +
                                          aGeneralInformation.DiscountFlat.ToString("F02") + "</td></tr>";
                        numOfLine++;
                    }

                    if (aGeneralInformation.CardFee > 0)
                    {
                        extraPanel += "<tr><td style='font-weight:bold; font-size:" + reciept_font +
                                          "px'>S/C </td><td  align='right'> £" +
                                          aGeneralInformation.CardFee.ToString("F02") + "</td></tr>";
                        numOfLine++;
                    }

                    if (aGeneralInformation.DeliveryCharge > 0 && deliveryButton.Text == "DEL" &&
                        deliveryButton.BackColor == Color.Black && collectionButton.BackColor != Color.Black)
                    {
                        extraPanel += "<tr><td style='font-weight:bold; font-size:" + reciept_font +
                                           "px'>D/C</td><td  align='right'> £" +
                                          aGeneralInformation.DeliveryCharge.ToString("F02") + "</td></tr>";
                        amount += aGeneralInformation.DeliveryCharge;
                        numOfLine++;
                    }

                    if (customTotalTextBox.Text != "Custom Total" && Convert.ToDouble(customTotalTextBox.Text) > 0)
                    {
                        amount = Convert.ToDouble(customTotalTextBox.Text);
                    }
                    if (extraPanel != "")
                    {
                        printFooterStr += extraPanel;

                    }

                    printFooterStr += "<tr><td colspan='2' style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                    printFooterStr += "<tr><td colspan='2' style='text-align:right;font-weight:bold;font-size:" + reciept_font + "px'>" + DateTime.Now.ToString("hh:mmtt") + "&nbsp;&nbsp;&nbsp;&nbsp;  TOTAL&nbsp;£" + amount.ToString("F02") + "&nbsp;&nbsp;</td></tr>";
                    printFooterStr += "<tr><td colspan='2' style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";

                    numOfLine++;
                    if (aRestaurantOrder.OnlineOrder > 0)
                    {
                        if (aRestaurantOrder.CardAmount > 0)
                        {
                            printFooterStr += "<tr><td  colspan='2' style='text-align:right;font-weight:bold;font-size:" + reciept_font +
                                              "px'>PAID BY CARD &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='float: right;'>" + "£" + aRestaurantOrder.CardAmount.ToString("F02") + "</span></td></tr>";
                        }
                        else
                        {
                            printFooterStr += "<tr><td  colspan='2' style='text-align:right;font-weight:bold;font-size:" + reciept_font +
                                          "px'>ORDER NOT PAID &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='float: right;'>" + "£" + aRestaurantOrder.CashAmount.ToString("F02") + "</span></td></tr>";
                        }
                        numOfLine++;
                    }
                    else
                    {
                        if (aPaymentDetails.CashAmount > 0 && aPaymentDetails.CardAmount > 0 && status)
                        {
                            printFooterStr += "<tr><td  colspan='2' style='text-align:right;font-weight:bold;font-size:" + reciept_font +
                                        "px'>CASH &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='float: right;'>" + "£" + aRestaurantOrder.CashAmount.ToString("F02") + "</span></td></tr>";
                            numOfLine++;
                            printFooterStr += "<tr><td  colspan='2' style='text-align:right;font-weight:bold;font-size:" + reciept_font +
                                     "px'>PAID BY CARD &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='float: right;'>" + "£" + aRestaurantOrder.CardAmount.ToString("F02") + "</span></td></tr>";

                            numOfLine++;
                        }
                        else if (status)
                        {
                            if (aPaymentDetails.CardAmount > 0)
                            {
                                printFooterStr += "<tr><td  colspan='2' style='text-align:right;font-weight:bold;font-size:" + reciept_font +
                                   "px'>PAID BY CARD &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='float: right;'>" + "£" + aRestaurantOrder.CardAmount.ToString("F02") + "</span></td></tr>";
                            }
                            else
                            {
                                printFooterStr += "<tr><td colspan='2' style='text-align:right;font-weight:bold;font-size:" + reciept_font +
                                       "px'>ORDER NOT PAID  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='float: right;'>" + "£" + aRestaurantOrder.CashAmount.ToString("F02") + "</span></td></tr>";
                            }
                            numOfLine++;
                        }
                        if (status)
                        {
                            printFooterStr += "<tr><td colspan='2' style='text-align:right;font-weight:bold;font-size:" + reciept_font +
                                      "px'>CHANGE &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='float: right;'>" + "£" + aPaymentDetails.ChangeAmount.ToString("F02") + "</td></tr>";
                            numOfLine++;

                            printFooterStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                            printFooterStr += "<tr><td colspan='2' style='text-align:center;'><span style='font-weight:bold;font-size:" + reciept_font_exlgr + "px;text-align:center'> PAID ORDER </span></td></tr>";

                            numOfLine++;
                        }
                    }

                    if (commentTextBox.Text != "Comment" && commentTextBox.Text.Trim().Length > 0)
                    {
                        printFooterStr += "<tr><td style='text-align:center;' colspan='2'>" + commentTextBox.Text +
                                          "</td></tr>";
                        numOfLine++;

                    }
                    if (aRestaurantInformation.ThankYouMsg.Length > 5)
                    {
                        printFooterStr += "<tr><td style='font-weight:bold;text-align:center;font-size:" + reciept_font + "px;' colspan='2'>" + aRestaurantInformation.ThankYouMsg +
                                           "</td></tr>";
                        numOfLine++;
                        printFooterStr += "<tr><td colspan='2'  style='line-height:5px;font-size:" +font_size+ "px'><span style='line-height:0px;'>" + dashLine + "</span></td></tr>";
                    }

                    int printCopy = 1;
                    if (printer.PrintStyle == "Receipt")
                    {
                        if (aGeneralInformation.OrderType == "IN")
                        {
                            printCopy = aRestaurantInformation.DineInPrintCopy;
                        }
                        else if (aGeneralInformation.OrderType == "Takeaway" || aGeneralInformation.OrderType == "CLT")
                        {
                            printCopy = aRestaurantInformation.PrintCopy;
                        }
                        else
                        {
                            printCopy = aRestaurantInformation.DelPrintCopy;
                        }
                    }

                    if (aRestaurantInformation.ShowOrderNumber > 0)
                    {
                        printFooterStr += "<tr><td style='font-weight:bold;text-align:center;font-size:30px' colspan='2'>" + aRestaurantOrder.OrderNo.ToString("D2") + "</td></tr>";
                    }

                    printStr = printHeaderStr + printRecipeStr + printFooterStr + "</table>";
                    string str = "";
                    if (!isPrint)
                    {
                        str = "<html><head><meta charset='UTF-8'><style>body,table { max-width:" + tableWidth + "; } td{min-width:100px;font-weight: bold;line-height:15px;font-size:" + reciept_font + "px;}  td:nth-child(1){max-width:140px;overflow:hidden;} td:nth-child(2){text-align: right;float: left;max-width:100px;min-width:100px;font-weight: bold;}</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;'>" + printStr + "</body></html>";
                        if (printer.PrintStyle == "Bill")
                        {
                             str = "<html><head><meta charset='UTF-8'><style>body,table { max-width:" + tableWidth + "; } td{min-width:100px;font-style: italic;line-height:15px;font-size:" + reciept_font + "px;}  td:nth-child(1){max-width:140px;overflow:hidden;} td:nth-child(2){text-align: right;float: left;max-width:100px;min-width:100px;}</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;'>" + printStr + "</body></html>";
                        }
                    }
                    else
                    {
                        str = "<html><head><meta charset='UTF-8'><style>body,table { max-width:" + tableWidth + "; } td{min-width:100px;line-height:15px;font-size:" + reciept_font + "px;}  td:nth-child(1){max-width:140px;overflow:hidden;} td:nth-child(2){text-align: right;float: left;max-width:100px;min-width:100px;}</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;'>" + printStr + "</body></html>";                       
                    }

                    Console.WriteLine("Last ");
                    Console.WriteLine(DateTime.Now.ToString("HH : mm : ss"));

                    PrintToPrinter print = new PrintToPrinter();
                    print.PrintReceipt(str, printer, printCopy);

                    Console.WriteLine("Last print ");
                    Console.WriteLine(DateTime.Now.ToString("HH : mm : ss"));

                    // NewPrint.printRecipt(str, printer.PrinterName, printCopy, numOfLine);
                    //SetDefaultPrinter(printer.PrinterName);
                    //for (int i = 0; i < printCopy; i++)
                    //{
                    //    try
                    //    {
                    //        WebBrowser wbPrintString = new WebBrowser() { DocumentText = string.Empty };
                    //        wbPrintString.Document.Write(str);
                    //        wbPrintString.Document.Title = "";
                    //        string keyName = @"Software\\Microsoft\\Internet Explorer\\PageSetup";
                    //        Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true);
                    //        if (key != null)
                    //        {
                    //            key.SetValue("footer", "");
                    //            key.SetValue("header", "");
                    //            key.SetValue("margin_bottom", "0");
                    //            key.SetValue("margin_left", "0.15");
                    //            key.SetValue("margin_right", "0");
                    //            key.SetValue("margin_top", "0");
                    //            key.SetValue("Print_Background", "false");
                    //            wbPrintString.Print();
                    //            wbPrintString.Dispose();
                    //        }
                    //        Console.WriteLine("END " + DateTime.Now);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessageBox.Show("Selected printer not exist!", "Printer Setup Warning", MessageBoxButtons.OK,
                    //            MessageBoxIcon.Warning);
                    //    }
                    //}

                }
                //if (GlobalSetting.RestaurantInformation.RestaurantType == "restaurant")
                //{
                //    if (isKitchenPrint && !(isPrint || isFinalize))
                if (!(isPrint || isFinalize))
                    {
                    GenerateKitchenCopy(result, status, aPaymentDetails);
                    }
               // }
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().Message);
            }
        }

        public void GenerateHtmlPrint_pre(int result, bool status, PaymentDetails aPaymentDetails, bool isKitchenPrint = false ,bool isPrint = false, bool isFinalize = false)
        {
            try
            {
                string printHeaderStr = "<div style='width:250px; font-family:tahoma, sans-serif'>";
                int papersize = 25;
                string reciept_font = "";               

                RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                UserLoginBLL aCustomerBll = new UserLoginBLL();
                RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

                RestaurantOrder aRestaurantOrder = aVariousMethod.GetRestaurantOrderByOrderId(result);
                int blankLine = 0;
                int starterId = aRestaurantMenuBLL.GetCategoryByName("Starter");

                List<RecipeTypeDetails> aListTypeDetails = orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>().ToList();

                reciept_font = aRestaurantInformation.RecieptFont;
                reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
                string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 2).ToString();
                string reciept_font_exlgr = "26";

                string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();

                if (aRestaurantInformation.RecieptOption == "logo_title")
                {
                    string path = @"Image/" + aRestaurantInformation.Id + "_website_logo.png";
                    if (File.Exists(path))
                    {
                        var imageString = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(File.ReadAllBytes(path)));
                        printHeaderStr += "<div align='center' style='width: 250px;'><img style='width:120px; height: 60px' src='" + imageString + "'></div>";
                    }                  
                }

                if (aRestaurantInformation.RecieptOption != "none")
                {
                    printHeaderStr += "<div align='center' style='width: 250px; margin-bottom:5px;'><b>" +
                                         aRestaurantInformation.RestaurantName.ToUpper() + "</b><br>" +
                                         aRestaurantInformation.House + ", " + aRestaurantInformation.Address + "<br>" +
                                         aRestaurantInformation.Postcode + "<br>TEL:" + aRestaurantInformation.Phone + "<br>" +
                                         aRestaurantInformation.VatRegNo + "</div>";
                }

                string orderHistory = aVariousMethod.GetOrderHistory(papersize, result, aGeneralInformation,
                    timeButton.Text.ToString());
                printHeaderStr += "<div  style='line-height:1.5;width: 250px;border-bottom:1px dashed;border-top:double;'><div style='text-align:center;font-size:" + reciept_font_lgr + "px'><b>" +
                    orderHistory + "</b></div></div>";
                if (isPrint)
                {
                    printHeaderStr += "<div   style='width: 250px;border-bottom:1px dashed;'><div style='text-align:center;'>" +
                        DateTime.Now.ToString("dddd,dd/MM/yyyy") + "</div></div>";
                }

                if (aGeneralInformation.CustomerId > 0)
                {
                    printHeaderStr +=
                        "<div style='float:left;width: 250px;border-bottom:1px dashed; margin-bottom:5px; font-weight:bold; font-size:" +
                        reciept_font + "px'>";
                    RestaurantUsers aUser = aRestaurantUserForSearchCustomer.SingleOrDefault(a => a.Id == aGeneralInformation.CustomerId);
                    if (aUser == null)
                    {
                        aUser = aCustomerBll.GetUserByUserId(aGeneralInformation.CustomerId);
                    }

                    printHeaderStr += aUser.Firstname + " " + aUser.Lastname + "<br>";
                    string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;

                    string address = "";
                    bool flag = false;
                    if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                    {
                        RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                        if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                        {
                            string[] ss = aORder.DeliveryAddress.Split(',');
                            flag = true;
                            if (ss.Count() > 0)
                            {
                                address += "," + ss[0];
                            }
                            if (ss.Count() > 1)
                            {
                                address += ", " + ss[1];
                            }
                            if (ss.Count() > 2)
                            {
                                address += "<br>" + ss[2];
                            }
                            if (ss.Count() > 3)
                            {
                                address += " " + ss[3];
                            } 
                        }
                    }

                    if (aGeneralInformation.DeliveryCharge > 0 && deliveryButton.Text == "DEL" &&
                        deliveryButton.BackColor == Color.Black)
                    {
                        if (address == "")
                        {
                            if (string.IsNullOrEmpty(aUser.FullAddress))
                            {
                                address += "" + aUser.House + " " + aUser.Address;
                                address += ", " + aUser.City + "<br>" + aUser.Postcode;
                            }
                            else
                            {
                                address += (aUser.House != "" ? aUser.House + ", " : "");
                                address += aUser.FullAddress + "<br>" + aUser.Postcode;
                            }
                        }
                    }

                    printHeaderStr += "<div style='float:left; max-width:185px; font-weight:bold; font-size:" + reciept_font +
                                      "px'>" + address + "</div>";
                    if (!flag)
                    {
                        printHeaderStr += "<div style='float:right; text-align:left; min-width:125px; font-weight:bold'>" +
                                          cell + "</div>";
                    }
                    printHeaderStr += "</div>";
                }
                else if (customerTextBox.Text != "" && customerTextBox.Text != "Search Customer")
                {
                    printHeaderStr +=
                        "<div style='clear:both'></div><div  style='width: 250px;border-bottom:1px dashed;margin-bottom:10px; '><div style='text-align:left;font-size:" +
                        reciept_font + "px'><b>" + customerTextBox.Text + "</b></div></div>";
                }
                LoadAllPrinter();
                PrinterSetups = PrinterSetups.Where(a => a.PrintStyle.ToLower() == "receipt").ToList();
                foreach (PrinterSetup printer in PrinterSetups)
                {
                    string printStr = "";
                    string printRecipeStr =
                        "<div style='width: 250px;margin-top:1px; font-family:tahoma, sans-serif; font-weight:bold;margin:0;padding:0;'>";
                    string printFooterStr =
                        "<div style='width: 250px; font-family:tahoma, sans-serif; font-weight:bold;padding:0;margin:0;'>";

                    //if (printer.PrintStyle.ToLower() != "receipt")
                    //{ 
                    //    continue; 
                    //} 

                    Dictionary<int, string> recipeTypes = GetRecipeTypes(printer);
                    
                    List<OrderItemDetailsMD> newOrderItemList = new List<OrderItemDetailsMD>();

                    aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
                    aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();

                    // aOrderItemDetailsMDList = aOrderItemDetailsMDList.GroupBy(a => a.CategoryId).OrderBy(b => b.Key).SelectMany(c => c.OrderBy(d => d.CatSortOrder)).ToList();
                    // aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();

                    int countKichneDone = aOrderItemDetailsMDList.Sum(a => a.Qty - a.KitchenDone);

                    int catId = 0;
                    bool startdas = false;
                    //try
                    //{
                    //    aListTypeDetails = aListTypeDetails.OrderBy(a => a.ReceipeTypeButton.SortOrder).ToList();

                    //}
                    //catch (Exception es)
                    //{
                    //}                   

                    foreach (RecipeTypeDetails typeDetails in aListTypeDetails)
                    {
                        if (typeDetails.recipeTypelabel.Visible)
                        {
                            if (typeDetails.typeflowLayoutPanel1.Visible == false)
                            {

                                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font_lgr +
                                                  "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                  typeDetails.recipeTypelabel.Text +
                                                  "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                  typeDetails.recipeTypeAmountlabel.Text + "</span></h3>";
                            }
                            else
                            {
                                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font_lgr +
                                                  "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                  typeDetails.recipeTypelabel.Text +
                                                  "</span><span style='text-align:right;float:right;width:8%;'></span></h3>";
                            }
                            printRecipeStr +=
                                "<div style='clear:both'></div><h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:3px;padding:0;width:75%;'>&nbsp;</h3>";
                        }
                        if (typeDetails.typeflowLayoutPanel1.Visible)
                        {
                            foreach (OrderItemDetailsMD item_details in aOrderItemDetailsMDList)
                            {
                                //if (!isFinalize && itemDetails.KitchenDone == itemDetails.Qty && aGeneralInformation.TableId > 0 && countKichneDone != 0)
                                //{
                                //    continue;
                                //}
                                if (item_details.RecipeTypeId == typeDetails.RecipeTypeId && recipeTypes.ContainsKey(item_details.RecipeTypeId))
                                {
                                    newOrderItemList.Add(item_details);
                                }
                            }
                        }
                    }
                    
                    newOrderItemList =  newOrderItemList.OrderBy(a => a.CatSortOrder).ToList();
                    foreach (OrderItemDetailsMD itemDetails in newOrderItemList)
                    {
                        if (aRestaurantInformation.MenuSeparation == 3 && startdas && starterId != itemDetails.CategoryId)
                        {
                            printRecipeStr +=
                                "<div style='clear:both'></div><h3  style='border-bottom:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                            startdas = false;
                        }
                        
                        //if (aRestaurantInformation.MenuSeparation == 6 && startdas && starterId != itemDetails.RecipeTypeId)
                        //{
                        //    printRecipeStr +=
                        //    /        "<div style='clear:both'></div><h3  style='border-bottom:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                        //    startdas = false;
                        //}
                        
                        if (isPrint)
                        {
                            printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + 
                                "px'><span style='text-align:left;float:left;width:70%;'>" + 
                                itemDetails.Qty + " " + itemDetails.ItemFullName + 
                                "</span><span style='text-align:right;float:right;width:22%;'>" + 
                                (itemDetails.Qty * itemDetails.Price).ToString("F02") + 
                                "</span></h3>";
                            blankLine++;
                        }
                        else
                        {
                            int qty = itemDetails.Qty;
                            
                            //if (itemDetails.Qty > 1 && !isFinalize && aGeneralInformation.TableId > 0 && countKichneDone > 1)
                            //{
                            //    qty = itemDetails.Qty - itemDetails.KitchenDone;
                            //}
                            printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + 
                                "px'><span style='text-align:left;float:left;width:75%;'>" + 
                                qty + " " + itemDetails.ItemName + 
                                "</span><span style='text-align:right;float:right;width:22%;'>" + 
                                (qty * itemDetails.Price).ToString("F02") + 
                                "</span></h3>"; blankLine++;
                        }
                        string options = "";
                        List<RecipeOptionMD> aOption =
                            aRecipeOptionMdList.Where(
                                a =>
                                a.RecipeId == itemDetails.ItemId && 
                                a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                        if (aOption.Count > 0)
                        {
                            foreach (RecipeOptionMD option in aOption)
                            {
                                if (!string.IsNullOrEmpty(option.Title))
                                {
                                    printRecipeStr += "<h3 style='font-weight:bold; font-size: " + 
                                        reciept_font_small + 
                                        "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + 
                                        "→" + option.Title + "<span style='text-style:italic'>" + (option.Price > 0 ? "+" + option.Price.ToString("F02") : "") + "</span>" + 
                                        "</span><span style='text-align:right;float:right;width:22%;'>" + 
                                        "" + "</span></h3>";
                                    blankLine++;
                                }
                                if (!string.IsNullOrEmpty(option.MinusOption))
                                {
                                    printRecipeStr += "<h3 style='font-weight:bold; font-size: " + 
                                        reciept_font_small + "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" + 
                                        "→No" + option.MinusOption + 
                                        "</span><span style='text-align:right;float:right;width:22%;'>" + 
                                        "" + "</span></h3>";
                                    blankLine++;
                                }
                            }
                        }
                        if (aRestaurantInformation.MenuSeparation == 2)
                        {
                            printRecipeStr += 
                                "<div style='clear:both'></div><h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:3px;padding:0;'>&nbsp;</h3>";
                        }
                        
                        if (starterId == itemDetails.CategoryId)
                        {
                            startdas = true;
                        }
                    }

                    foreach (RecipePackageMD package in aRecipePackageMdList)
                    {
                        if (recipeTypes.ContainsKey(package.RecipeTypeId))
                        {
                            printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:75%;'>" +
                                              package.Qty.ToString() + " " + package.PackageName +
                                              "</span><span style='text-align:right;float:right;width:22%;'>" +
                                              (package.Qty * package.UnitPrice).ToString("F02") + "</span></h3>";
                            blankLine++;
                            List<PackageItem> aPaItem =
                                aPackageItemMdList.Where(
                                    a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex)
                                    .ToList();
                            List<PackageItem> aPaItemNew = new List<PackageItem>();
                            foreach (PackageItem item in aPaItem)
                            {
                                item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                                aPaItemNew.Add(item);
                            }
                            aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();

                            foreach (PackageItem itemDetails in aPaItem)
                            {
                                string packageItemPrice = itemDetails.Price > 0
                                    ? (itemDetails.Price).ToString()
                                    : "";

                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small +
                                                  "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                  itemDetails.Qty.ToString() + "  " + itemDetails.ItemName +
                                                  "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                  packageItemPrice + "</span></h3>";
                                blankLine++;
                                string options = "";
                                List<RecipeOptionMD> aOption =
                                    aRecipeOptionMdList.Where(
                                        a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                                        .ToList();
                                if (aOption.Count > 0)
                                {
                                    foreach (RecipeOptionMD option in aOption)
                                    {
                                        if(!string.IsNullOrEmpty(option.Title))
                                        {
                                            printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                              "→" + option.Title +
                                                              "</span><span style='text-align:right;float:right;width:22%;'>"+option.Price+"</span></h3>";
                                            blankLine++;
                                        }
                                        if (!string.IsNullOrEmpty(option.MinusOption))
                                        {                    
                                            printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                              "→No" + option.MinusOption +
                                                              "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                            blankLine++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (RecipeMultipleMD package in aRecipeMultipleMdList)
                    {
                        if (recipeTypes.ContainsKey(package.RecipeTypeId))
                        {
                            printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                 "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                 package.Qty.ToString() + " " + package.MultiplePartName +
                                                 "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                 (package.Qty * package.UnitPrice).ToString("F02") + "</span></h3>";
                            blankLine++;
                            List<MultipleItemMD> aPaItem =
                                aMultipleItemMdList.Where(
                                    a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                                    .ToList();
                            int cnt = 0;
                            foreach (MultipleItemMD itemDetails in aPaItem)
                            {
                                cnt++;
                                string packageItemPrice = itemDetails.Price > 0
                                    ? (itemDetails.Price).ToString()
                                    : "";

                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " + reciept_font_small +
                                                  "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                  (cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) + ": " +
                                                  itemDetails.ItemName +
                                                  "</span></h3>";
                                blankLine++;
                                string options = "";
                                List<RecipeOptionMD> aOption =
                                    aRecipeOptionMdList.Where(
                                        a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                                        .ToList();
                                if (aOption.Count > 0)
                                {
                                    foreach (RecipeOptionMD option in aOption)
                                    {
                                        if (!string.IsNullOrEmpty(option.Title))
                                        {
                                            printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                              "→No" + option.Title +
                                                              "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                            blankLine++;
                                        }
                                        if (!string.IsNullOrEmpty(option.MinusOption))
                                        {
                                            printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                              "→No" + option.MinusOption +
                                                              "</span><span style='text-align:right;float:right;width:22%;'></span></h3>";
                                            blankLine++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    int numOfLine = blankLine;

                    double amount = GetTotalAmountDetails() + aGeneralInformation.CardFee -
                                    aGeneralInformation.DiscountFlat - aGeneralInformation.ItemDiscount;

                    amount = GlobalVars.numberRound(amount, 2);

                    if (blankLine < aRestaurantInformation.RecieptMinHeight)
                    {
                        for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                        {
                            printRecipeStr +=
                                "<h3  style='text-align:left;font-size:2px;line-height:20px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                            numOfLine++;
                        }
                    }
                    string extraPanel = "";

                    if (aGeneralInformation.DiscountFlat > 0)
                    {
                        extraPanel += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:35%;'>Discount</span><span style='text-align:right;float:right;width:62%;'>(" +
                                          aGeneralInformation.DiscountPercent.ToString("F02") + "%) £" +
                                          aGeneralInformation.DiscountFlat.ToString("F02") + "</span></h3>";
                        numOfLine++;
                    }

                    if (aGeneralInformation.CardFee > 0)
                    {
                        extraPanel += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:35%;'>S/C </span><span style='text-align:right;float:right;width:62%;'> £" +
                                          aGeneralInformation.CardFee.ToString("F02") + "</span></h3>";
                        numOfLine++;
                    }

                    if (aGeneralInformation.DeliveryCharge > 0 && deliveryButton.Text == "DEL" &&
                        deliveryButton.BackColor == Color.Black && collectionButton.BackColor != Color.Black)
                    {
                        extraPanel += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:35%;'>D/C</span><span style='text-align:right;float:right;width:62%;'> £" +
                                          aGeneralInformation.DeliveryCharge.ToString("F02") + "</span></h3>";
                        amount += aGeneralInformation.DeliveryCharge;
                        numOfLine++;
                    }

                    if (customTotalTextBox.Text != "Custom Total" && Convert.ToDouble(customTotalTextBox.Text) > 0)
                    {
                        amount = Convert.ToDouble(customTotalTextBox.Text);
                    }
                    if (extraPanel != "")
                    {
                        string panel = "<div style='border-top:double'></div>";
                        printFooterStr += panel + extraPanel;
                    }

                    //double subTotal = GetTotalAmountDetails() - GetSubAmountDetails();

                    //if (subTotal==0 || aGeneralInformation.TableId==0)
                    //{

                    printFooterStr +=
                     "<div style='clear:both'></div><br/><h3 style='font-weight:bold;margin:30px auto;padding-top:10px;border-top:1px dashed; font-size:" +
                     reciept_font + "px'><span style='text-align:left;float:left;width:25%;'>" +
                     DateTime.Now.ToString("hh:mmtt") + "</span>" +
                      "<span style='text-align:right;float:right;width:72%;'>" +
                     "TOTAL&nbsp;£" + amount.ToString("F02") + "</span></h3>";
                    printFooterStr += "<div style='border-top:double'></div>";
                    //}else
                    //{


                    //var total = subTotal > 0 ? subTotal.ToString("F02") : GetTotalAmountDetails().ToString("F02");

                    //printFooterStr +=//    "<div style='clear:both'></div><br/><h3 style='font-weight:bold;margin:30px auto;padding-top:10px;border-top:1px dashed; font-size:" +
                    //    reciept_font + "px'><span style='text-align:left;float:left;width:25%;'>" +
                    //    DateTime.Now.ToString("HH:mm") + "</span>" +
                    //    "<span style='text-align:right;float:right;width:72%;'>" + "SUB-TOTAL &nbsp; £" + total +
                    //    "</span></h3>";

                    //printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                    //                  "px'><span style='text-align:left;float:left;width:25%;'>&nbsp;</span>" +
                    //                  "<span style='text-align:right;float:right;width:72%;'>" + "TOTAL &nbsp; £" +
                    //                  amount.ToString("F02") + "</span></h3>";
                    //}

                    numOfLine++;
                    if (aRestaurantOrder.OnlineOrder > 0)
                    {
                        if (aRestaurantOrder.CardAmount > 0)
                        {
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>PAID BY CARD</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aRestaurantOrder.CardAmount.ToString("F02") + "</span></h3>";
                        }
                        else
                        {
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>ORDER NOT PAID</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aRestaurantOrder.CashAmount.ToString("F02") + "</span></h3>";
                        }
                        
                        numOfLine++;
                    }
                    else
                    {
                        if (aPaymentDetails.CashAmount > 0 && aPaymentDetails.CardAmount > 0 && status)
                        {
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>CASH</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aPaymentDetails.CashAmount.ToString("F02") + "</span></h3>";
                            numOfLine++;
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>PAID BY CARD </span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aPaymentDetails.CardAmount.ToString("F02") + "</span></h3>";
                            numOfLine++;
                        }
                        else if (status)
                        {
                            if (aPaymentDetails.CardAmount > 0)
                            {

                                printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                  "px'><span style='text-align:left;float:left;width:65%;'>PAID BY CARD </span><span style='text-align:right;float:right;width:30%;'>" +
                                                  "£" + aPaymentDetails.CardAmount.ToString("F02") + "</span></h3>";
                            }

                            else
                            {
                                printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                  "px'><span style='text-align:left;float:left;width:65%;'>CASH</span><span style='text-align:right;float:right;width:30%;'>" +
                                                  "£" + aPaymentDetails.CashAmount.ToString("F02") + "</span></h3>";
                            }
                            numOfLine++;
                        }
                        if (status)
                        {
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                              "px'><span style='text-align:left;float:left;width:65%;'>CHANGE</span><span style='text-align:right;float:right;width:30%;'>" +
                                              "£" + aPaymentDetails.ChangeAmount.ToString("F02") + "</span></h3>";
                            numOfLine++;
                            printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font_exlgr +
                                              "px;border-top:1px dashed;'><span style='text-align:center;float:left;width:95%;'>PAID ORDER </span></h3>";
                            numOfLine++;
                        }
                    }

                    if (commentTextBox.Text != "Comment" && commentTextBox.Text.Trim().Length > 0)
                    {
                        printFooterStr += "<h3  style='border-bottom:1px dashed;text-align:center;'>" + commentTextBox.Text +
                                          "</h3>";
                        numOfLine++;
                    }
                    if (aRestaurantInformation.ThankYouMsg.Length > 5)
                    {
                        printFooterStr += "<h3  style='text-align:center;'>" + aRestaurantInformation.ThankYouMsg + "</h3>";
                        numOfLine++;
                    }

                    //if (isPrint && aGeneralInformation.Person > 1)
                    //{
                    //    printFooterStr += "<h3 style='text-align:center;font-weight:bold; font-size:" + reciept_font + "px'>" + "Total split " +
                    //                      aGeneralInformation.Person + " ways £" +
                    //                      (amount / aGeneralInformation.Person).ToString("F02") + "</h3>";
                    //    numOfLine++;
                    //}

                    int printCopy = 1;
                    if (printer.PrintStyle == "Receipt")
                    {
                        if (aGeneralInformation.OrderType == "IN")
                        {
                            printCopy = aRestaurantInformation.DineInPrintCopy;
                        }
                        else if (aGeneralInformation.OrderType == "Takeaway" || aGeneralInformation.OrderType == "CLT")
                        {
                            printCopy = aRestaurantInformation.PrintCopy;
                        }
                        else
                        {
                            printCopy = aRestaurantInformation.DelPrintCopy;
                        }
                    }

                    printHeaderStr += "</div>";
                    printRecipeStr += "</div>";
                    printFooterStr += "<br/><br/></div>";    
                    printStr = printHeaderStr + printRecipeStr + printFooterStr;
                    string str = "<html><head><meta charset='UTF-8'><style>html, body ,h3, h2, h4 , span, b { padding: 0; margin: 0; }</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;width:250px;'>" +
                                printStr + "</body></html>";

                    PrintToPrinter print = new PrintToPrinter();
                    print.PrintReceipt(printStr, printer, printCopy);                  
                }
                if (GlobalSetting.RestaurantInformation.RestaurantType == "restaurant")
                {
                    if (isKitchenPrint && !(isPrint || isFinalize))
                    {
                        GenerateKitchenCopy(result, status, aPaymentDetails);
                    }
                }              
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().Message);               
            }
        }

        public static bool SetDefaultPrinter(string defaultPrinter)
        {
            return PrinterInformation.SetDefault(defaultPrinter);
        }

        private string getMeg(string ThankYouMsg = "")
        {
            string newString = "";
            if (ThankYouMsg.Length > 1)
            {
                string[] separators = { " " };
                string[] words = ThankYouMsg.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                int mainLength = 0;
                foreach (var word in words)
                {
                    mainLength += word.Length;
                    if (mainLength < 24)
                    {
                        newString += " " + word;
                    }
                    else
                    {
                        newString += "\r\n" + word;
                        mainLength = word.Length;
                    }
                }
            }

            return newString;
        }   

        private double GetServiceChargePercent()
        {
            double serviceChargePecent = 0;
            double amount1 = 0;
            double amount2 = 0;
            double amount3 = 0;
            amount1 = aOrderItemDetailsMDList.Sum(a => a.Qty * a.Price);
            double totalAmount = amount1 + amount2 + amount3;

            if (totalAmount > 0)
            {
                serviceChargePecent = (aGeneralInformation.ServiceCharge * 100) / totalAmount;
            }

            return serviceChargePecent;
        }

        public string getStringForPrint(string str,int isPrint = 18)
        {
            string newStr = "";

            string[] strArray = str.Split(' ');
            int arrayLeng = strArray.Length;
            int newLen = 0;
            for (int i = 0; i < arrayLeng; i++)
            {
                newLen += strArray[i].Length;
                if (newLen < isPrint)
                {
                    newStr += strArray[i] + " ";
                }
                else
                {
                    newLen = 0;
                    newStr += "<br>&nbsp;&nbsp;&nbsp;" + strArray[i] + " ";
                }
            }
            //int leng = str.Length;
            //if (leng > 20)
            //{
            //    newStr = str.Substring(0, 20).ToString() + "<br>" + str.Substring(20, leng - 20).ToString();
            //}
            return newStr;
        }
       
        public void GenerateKitchenCopyNEW(int orderId)
        { 
            try
            {
                string printHeaderStr = "<table width='250' align='left'>";
       
                int papersize = 25;
                string reciept_font = "";
                RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                UserLoginBLL aCustomerBll = new UserLoginBLL();
                RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                //  RestaurantOrder aRestaurantOrder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                int blankLine = 0;
                int menuSeperation = aRestaurantInformation.MenuSeparation;

                var catList = aRestaurantMenuBLL.GetAllCategory();
                List<RecipeTypeDetails> aListTypeDetails = orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>().ToList();

                reciept_font = "20";
                string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 1).ToString();
                string reciept_font_small = "15";

                string orderHistory = aVariousMethod.GetOrderHistory(papersize, orderId, aGeneralInformation, timeButton.Text.ToString(), true);

                if (Properties.Settings.Default.kitchenEmptyLine > 0)
                {
                    for (int kk = 0; kk < Properties.Settings.Default.kitchenEmptyLine; kk++)
                    {
                        printHeaderStr += "<tr><td colspan='2' style='text-align:center'  style='line-height:5px;'><span style='line-height:16px;'>&nbsp;&nbsp;&nbsp;</span></td></tr>";
                    }
                }

                printHeaderStr += "<tr><td colspan='2' style='text-align:center'  style='line-height:5px;'><span style='line-height:0px;'>-----------------------------</span></td></tr>";
                printHeaderStr += "<tr><td colspan='2' style='text-align:center;font-size:20px'><b style='text-align:center;'>" + orderHistory + "</b></td></tr>";
                printHeaderStr += "<tr><td colspan='2' style='text-align:center'  style='line-height:5px;'><span style='line-height:0px;'>-----------------------------</span></td></tr>";
                string printTopStr = "";
               
                string customer = "";
                if (aGeneralInformation.CustomerId > 0)
                {
                    printHeaderStr += "<tr><td colspan='2' style='text-align:left'>";
                    printHeaderStr += "<span style='text-align:left;font-weight:bold; font-size:20px'>";
                    RestaurantUsers aUser = aRestaurantUserForSearchCustomer.SingleOrDefault(a => a.Id == aGeneralInformation.CustomerId);
                    if (aUser == null)
                    {
                        aUser = aCustomerBll.GetUserByUserId(aGeneralInformation.CustomerId);
                    }

                    customer += aUser.Firstname + " " + aUser.Lastname + "<br>";
                    string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;

                    string address = "";
                    bool flag = false;
                    if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                    {
                        RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                        if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                        {
                            string[] ss = aORder.DeliveryAddress.Split(',');
                            flag = true;
                            if (ss.Count() > 0)
                            {
                                address += "," + ss[0];
                            }
                            if (ss.Count() > 1)
                            {
                                address += ", " + ss[1];
                            }
                            if (ss.Count() > 2)
                            {
                                address += "<br>" + ss[2];
                            }
                            if (ss.Count() > 3)
                            {
                                address += ", " + ss[3];
                            } 
                        }
                    }
                    if (deliveryButton.Text == "DEL" && deliveryButton.BackColor == Color.Black)
                    {
                        if (address == "")
                        {
                            if (string.IsNullOrEmpty(aUser.FullAddress))
                            {
                                address += "" + aUser.House + " " + aUser.Address;
                                address += ", " + aUser.City + "<br>" + aUser.Postcode;
                            }
                            else
                            {
                                address += (aUser.House != "" ? aUser.House + ", " : "");
                                address += aUser.FullAddress + "<br>" + aUser.Postcode;
                            }
                        }
                    }

                    printHeaderStr += "</span></td></tr>";

                    printHeaderStr += "<tr><td colspan='2' style='text-align:left'><span style='text-align:left;font-weight:bold; font-size:18px'>" + address + "</span></td></tr>";

                    if (!flag)
                    {
                        printHeaderStr += "<tr><td colspan='2' align='right'><span style='text-align:right;font-weight:bold'>" +  cell + "</span></td></tr>";
                    }
                    printHeaderStr += "<tr><td colspan='2'  style='line-height:5px;'><span style='line-height:0px;'>-----------------------------</span></td></tr>";
                }
                else if (customerTextBox.Text != "" && customerTextBox.Text != "Search Customer")
                {
                    printHeaderStr += "<tr><td colspan='2' style='text-align:left'><span style='text-align:left;font-weight:bold; font-size:20px'>" + customerTextBox.Text + "</span></td></tr>";
                }

                printHeaderStr += "</table>";
                LoadAllPrinter();
                PrinterSetups = PrinterSetups.Where(a => a.PrintStyle != "Receipt").ToList();
                foreach (PrinterSetup printer in PrinterSetups)
                {
                    bool flag = false;
                    string printStr = "";
                    string printRecipeStr = "<table width='250' align='left'>";
                    string printFooterStr = "";
                    Dictionary<int, string> recipeTypes = GetRecipeTypes(printer);

                    //aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
                    aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();

                    int catId = 0;
                    bool startdas = false;
                    try
                    {
                        aListTypeDetails = aListTypeDetails.OrderBy(a => a.ReceipeTypeButton.SortOrder).ToList();
                    }
                    catch (Exception es)
                    {

                    }

                    List<OrderItemDetailsMD> newOrderItemList = new List<OrderItemDetailsMD>();
                    int FoundItemcount = 0;

                    List<MenuType> MenuTypes = new List<MenuType>();
                    foreach (RecipeTypeDetails typeDetails in aListTypeDetails)
                    {
                        var IsSelectedTypes = recipeTypes.Count(a => a.Key == typeDetails.RecipeTypeId);
                        if (IsSelectedTypes == 0)
                        {
                            continue;
                        }

                        if (menuSeperation == 5)
                        {
                            printRecipeStr += "<tr><td colspan='2' style='text-align:left'><span style='text-align:left;font-weight:bold; font-size:" + reciept_font +
                                              "px'>" + typeDetails.ReceipeTypeButton.Text.ToUpper() + "</span></td></tr>";

                            printRecipeStr += "<tr><td colspan='2'  style='line-height:5px;'><span style='line-height:0px;'>-----------------------------</span></td></tr>";                           
                        }

                        foreach (OrderItemDetailsMD item_details in aOrderItemDetailsMDList)
                        {
                            if (item_details.KitchenDone >= item_details.Qty)
                            {
                                continue;
                            }

                            if (item_details.RecipeTypeId == typeDetails.RecipeTypeId)
                            {
                                newOrderItemList.Add(item_details);
                            }

                            //if (aRestaurantInformation.MenuSeparation == 6 && recipeTypeLine == 1)
                            //{
                            //    MenuType empty_type = new MenuType();
                            //    empty_type.Id = typeDetails.RecipeTypeId;
                            //    empty_type.has_underline = true;
                            //    empty_type.status = true;
                            //    MenuTypes.Add(empty_type);
                            //}
                        }
                    }

                    int CategoryId = 0;
                    int stater = 0;
                    newOrderItemList = newOrderItemList.OrderBy(a => a.CatSortOrder).ToList();
                    MenuType rtype = new MenuType();
                    foreach (OrderItemDetailsMD itemDetails in newOrderItemList)
                    {
                        FoundItemcount++;

                        //MenuType mtype = MenuTypes.FirstOrDefault(a => a.Id == itemDetails.RecipeTypeId);
                        //if (mtype != null)
                        //{
                        //    rtype = mtype;
                        //}

                        //if (aRestaurantInformation.MenuSeparation == 6 && rtype.status && rtype.Id != itemDetails.RecipeTypeId)
                        //{
                        //    printRecipeStr += "<tr><td colspan='2'  style='line-height:5px;'><span style='line-height:0px;'>-----------------------------</span></td></tr>";
                        //    rtype.status = false;
                        //}

                        string CateName = catList.FirstOrDefault(a => a.CategoryId == itemDetails.CategoryId).CategoryName;
                        if (CategoryId != itemDetails.CategoryId && menuSeperation == 4)
                        {
                            printRecipeStr += "<tr><td colspan='2' style='text-align:center;font-weight:bold; font-size:" + reciept_font +
                                            "px'>" + CateName.ToUpper() + "</span></td></tr>";

                            printRecipeStr += "<tr><td colspan='2'  style='line-height:5px;'><span style='line-height:0px;'>-----------------------------</span></td></tr>";
                            //Cate+Title
                          
                            CategoryId = itemDetails.CategoryId;


                        }
                        else if (CategoryId != itemDetails.CategoryId && menuSeperation == 1)
                        {
                            printRecipeStr += "<tr><td colspan='2'  style='line-height:5px;'><span style='line-height:0px;'>-----------------------------</span></td></tr>";

                            CategoryId = itemDetails.CategoryId;
                        }

                        printRecipeStr += "<tr><td colspan='2' style='font-size:" + reciept_font + "px;padding-left:10px'>&nbsp;&nbsp;" + (aGeneralInformation.TableId > 0 ? (itemDetails.Qty - itemDetails.KitchenDone) : itemDetails.Qty) + " " + itemDetails.ItemName + "</td></tr>";
           
                        blankLine++;

                        List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId &&
                                       a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                        if (aOption.Count > 0)
                        {
                            foreach (RecipeOptionMD option in aOption)
                            {
                                if (!string.IsNullOrEmpty(option.Title))
                                {
                                    printRecipeStr += "<tr><td style='font-size: " +reciept_font_small +
                                                      "px;line-height: " +reciept_font_small +"px'><span style='padding-left:20px'>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                                      "→" + option.Title + "</span></td><td align='right'  style='font-size: " + reciept_font_small +
                                                      "px;line-height: " + reciept_font_small + "px'><span style='text-style:italic'>&nbsp;</span>" +
                                                      "</td></tr>";
                                    blankLine++;
                                }
                                if (!string.IsNullOrEmpty(option.MinusOption))
                                {
                                    printRecipeStr += "<tr><td style='font-size: " +
                                                      reciept_font_small + "px;line-height: " + reciept_font_small + "px'><span style='padding-left:20px'>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                                      "→No" + option.MinusOption +
                                                     "</span></td><td style='font-size: " + reciept_font_small +
                                                      "px;line-height: " + reciept_font_small + "px'>&nbsp;</td></tr>";
                                    blankLine++;
                                }                               
                            }
                        }

                        if (menuSeperation == 3 && CateName.Contains("Starter"))
                        {
                            stater++;
                            //Stater wise
                            int count = aOrderItemDetailsMDList.Count(a => a.CategoryId == itemDetails.CategoryId);
                            if (stater == count)
                            {
                                printRecipeStr += "<tr><td colspan='2'  style='line-height:5px;'><span style='line-height:0px;'>-----------------------------</span></td></tr>";
                            }
                        }
                        if (menuSeperation == 2)
                        {
                            //Menu Wise
                            printRecipeStr += "<tr><td colspan='2'  style='line-height:5px;'><span style='line-height:0px;'>-----------------------------</span></td></tr>";
                        }
                    }

                    foreach (RecipePackageMD package in aRecipePackageMdList)
                    {
                        var type = recipeTypes.FirstOrDefault(a => a.Key == package.RecipeTypeId).Value;
                        if (type != null)
                        {
                            List<PackageItem> aPaItem =
                                  aPackageItemMdList.Where(a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex && a.DeleteItem).ToList();
                            if (aPaItem.Count == 0)
                            {
                                continue;
                            }
                            FoundItemcount++;
                            printRecipeStr += "<tr><td colspan='2' style='font-weight:bold; font-size:" + reciept_font + "px;padding-left:10px'>&nbsp;&nbsp;" + package.Qty.ToString() + " " + package.PackageName.ToLower() + "</td></tr>";
            
                            blankLine++;

                            List<PackageItem> aPaItemNew = new List<PackageItem>();
                            foreach (PackageItem item in aPaItem)
                            {
                                item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                                aPaItemNew.Add(item);
                            }
                            aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();

                            foreach (PackageItem itemDetails in aPaItem)
                            {
                                string packageItemPrice = itemDetails.Price > 0
                                    ? (itemDetails.Price).ToString()
                                    : "";

                                printRecipeStr += "<tr><td  colspan='2' style='font-weight:bold;padding-left:10px;font-size: " + reciept_font_small +
                                                  "px'>&nbsp;&nbsp;" +
                                                  itemDetails.Qty.ToString() + "  " + itemDetails.ItemName +
                                                  "</td></tr>";
                                blankLine++;
                                string options = "";
                                List<RecipeOptionMD> aOption =
                                    aRecipeOptionMdList.Where(
                                        a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex)
                                        .ToList();
                                if (aOption.Count > 0)
                                {

                                    foreach (RecipeOptionMD option in aOption)
                                    {
                                        if (!string.IsNullOrEmpty(option.Title))
                                        {
                                            printRecipeStr += "<tr><td  colspan='2' style='font-weight:bold; font-size: " + reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;padding-left:20px'>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                                              "→" + option.Title +
                                                              "</span></td></tr>";
                                            blankLine++;
                                        }
                                        if (!string.IsNullOrEmpty(option.MinusOption))
                                        {
                                            printRecipeStr += "<tr><td  colspan='2' style='font-weight:bold; font-size: " +
                                                              reciept_font_small +
                                                              "px'><span style='text-align:left;padding-left:20px'>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                                              "→No" + option.MinusOption +
                                                              "</span></td></tr>";
                                            blankLine++;
                                        }
                                    }
                                }
                            }                          
                        }
                    }
                    foreach (RecipeMultipleMD package in aRecipeMultipleMdList)
                    {
                        if (recipeTypes.ContainsKey(package.RecipeTypeId))
                        {
                            printRecipeStr += "<tr><td colspan='2' style='font-weight:bold;padding-left:10px;font-size:" + reciept_font + "px'>&nbsp;&nbsp;" + package.Qty.ToString() + " " + package.MultiplePartName.ToLower() + "</td></tr>";

                            blankLine++;
                            List<MultipleItemMD> aPaItem =
                                aMultipleItemMdList.Where(
                                    a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                                    .ToList();
                            int cnt = 0;
                            foreach (MultipleItemMD itemDetails in aPaItem)
                            {
                                cnt++;
                                string packageItemPrice = itemDetails.Price > 0
                                    ? (itemDetails.Price).ToString()
                                    : "";

                                printRecipeStr += "<tr><td  colspan='2' style='font-weight:bold;padding-left:10px;font-size: " + reciept_font_small +
                                                  "px'>&nbsp;&nbsp;" + (cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) +
                                                  ": " + itemDetails.ItemName.ToLower() + "</td></tr>";
                                blankLine++;
                                string options = "";
                                List<RecipeOptionMD> aOption =
                                    aRecipeOptionMdList.Where(
                                        a =>
                                            a.RecipeId == itemDetails.ItemId &&
                                            a.OptionsIndex == itemDetails.OptionsIndex)
                                        .ToList();
                                if (aOption.Count > 0)
                                {
                                    foreach (RecipeOptionMD option in aOption)
                                    {
                                        if (!string.IsNullOrEmpty(option.Title))
                                        { 
                                            printRecipeStr += "<tr><td  colspan='2' style='font-weight:bold; font-size: " + reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;padding-left:20px'>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                                              "→" + option.Title +
                                                              "</span></td></tr>";
                                            blankLine++;
                                        }
                                        if (!string.IsNullOrEmpty(option.MinusOption))
                                        { 

                                                printRecipeStr += "<tr><td  colspan='2' style='font-weight:bold; font-size: " + reciept_font_small +
                                                              "px'><span style='text-align:left;float:left;padding-left:20px'>&nbsp;&nbsp;&nbsp;&nbsp;" +
                                                               "No" + option.MinusOption.ToLower() +"</span></td></tr>";
                                            blankLine++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (blankLine < aRestaurantInformation.RecieptMinHeight)
                    {
                        for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                        {
                            printRecipeStr += "<tr><td>&nbsp;</td><td>&nbsp;</td></tr>";
                        }
                    }

                    blankLine = 0;

                    if (commentTextBox.Text != "Comment" && commentTextBox.Text.Trim().Length > 0)
                    { 
                        printFooterStr += "<tr><td style='text-align:center;' colspan='2'>" + commentTextBox.Text.ToLower() +"</td></tr>";
                    }

                    if (FoundItemcount <= 0)
                    {   
                        continue;
                    }

                     int numOfLine = blankLine;
                     printTopStr = "<table width='250' align='left'><tr><td  colspan='2' style='text-align:center'><b  style='text-align:center'>" + printer.PrinterAddress.ToUpper() + "<b></td></tr></table>";
                     if (aRestaurantInformation.ShowOrderNumber > 0)
                     {
                         printFooterStr += "<tr><td colspan='2'  style='line-height:5px;'><span style='line-height:0px;'>-----------------------------</span></td></tr>";
                         printFooterStr += "<tr><td style='font-weight:bold;text-align:center;font-size:30px' colspan='2'>" + OrderNo.ToString("D2") + "</td></tr>";
                     }

                     printFooterStr += "<tr><td colspan='2' style='text-align:center;font-size:25px;line-height:28px;'><span style='text-align:center;'> <b> " + DateTime.Now.ToString(" hh:mmtt") + "</b></span></td></tr>";
                    
                    printStr =printTopStr+ printHeaderStr + printRecipeStr + printFooterStr + "</table>";
                    string str = "<html><head><meta charset='UTF-8'><style>html, body ,h3, h2, h4 , span, b { padding: 0; margin: 0; } td{line-height:" + reciept_font_lgr + "px;font-size:" + reciept_font + "px;}   td{min-width:80px;} td:nth-child(2){text-align: right;float: left;width:80px;}</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;'>" + printStr + "</body></html>";

                    PrintToPrinter print = new PrintToPrinter();
                    print.PrintReceipt(str, printer, printer.PrintCopy);                 

                    RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                    aRestaurantOrderBLL.UpdateKitchenStatus(orderId);
                }
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
            }
        }

        public OrderItemMerged getExistingOrderItem(List<OrderItemMerged> orderItems, OrderItemDetailsMD orderItem = null, PackageItem packageItem=null)
        {
            var existingItem = new OrderItemMerged();
            if(orderItem != null)
            {
                existingItem = orderItems.Where(a => a.ItemId == orderItem.ItemId && a.ItemName == orderItem.ItemName).SingleOrDefault();
                if(existingItem != null)
                {
                    //check if options is there
                    List<RecipeOptionMD> aOptions = aRecipeOptionMdList.Where(a => a.RecipeId == orderItem.ItemId &&
                                           a.OptionsIndex == orderItem.OptionsIndex).ToList();
                    //option is there we cannot increase the qty
                    if (aOptions.Count > 0)
                    {
                        return null;
                    }                    
                }                
            }
            else
            {
                existingItem = orderItems.Where(a => a.ItemId == packageItem.ItemId && a.ItemName == packageItem.ItemName).SingleOrDefault();
                //check if options is there
                List<RecipeOptionMD> aOption =
                                        aRecipeOptionMdList.Where(
                                            a => a.RecipeId == packageItem.ItemId && a.OptionsIndex == packageItem.OptionsIndex && a.PackageItemOptionsIndex == packageItem.PackageItemOptionsIndex)
                                            .ToList();
                if (aOption.Count > 0)
                {
                    return null;
                }
            }

            return existingItem;
        }

        public void GenerateKitchenCopy(int orderId, bool paymentStatus = false, PaymentDetails aPaymentDetails = null)
        {
            //if (Properties.Settings.Default.enableWebPrint)
            //{
            //    GenerateKitchenCopyNEW(orderId);
            //    //break;
            //}
            //else
            //{
            try
            {
                string printHeaderStr = "";
                int papersize = 25;
                string reciept_font = "";
                RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                UserLoginBLL aCustomerBll = new UserLoginBLL();
                RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();

                List<int> starterIds = aRestaurantMenuBLL.GetCategoriesByName("Starter");
                if (starterIds.Count == 0)
                {
                    starterIds = aRestaurantMenuBLL.GetCategoriesByName("Starters");
                }

                int blankLine = 0;
                int menuSeperation = aRestaurantInformation.MenuSeparation;

                var catList = aRestaurantMenuBLL.GetAllCategory();
                List<RecipeTypeDetails> aListTypeDetails = orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>().ToList();
                string orderHistory = aVariousMethod.GetOrderHistory(papersize, orderId, aGeneralInformation, timeButton.Text.ToString(), true);
                LoadAllPrinter();
                PrinterSetups = PrinterSetups.Where(a => a.PrintStyle == "Kitchen").ToList();
                foreach (PrinterSetup printer in PrinterSetups)
                {

                    List<PrintContent> aPrintContentsMid = new List<PrintContent>();
                    PrintContent aPrintContent = new PrintContent();
                    PrintFormat aPrintFormat = new PrintFormat(22);

                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(printer.PrinterAddress.ToUpper()) + "\n";
                    aPrintContentsMid.Add(aPrintContent);

                    if (Properties.Settings.Default.kitchenEmptyLine > 0)
                    {
                        for (int kk = 0; kk < Properties.Settings.Default.kitchenEmptyLine; kk++)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = " " + " " + "  " + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }
                    }

                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(orderHistory.ToUpper()) + "\n";
                    aPrintContentsMid.Add(aPrintContent);

                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                    string customer = "";
                    if (aGeneralInformation.CustomerId > 0)
                    {
                        RestaurantUsers aUser = aRestaurantUserForSearchCustomer.SingleOrDefault(a => a.Id == aGeneralInformation.CustomerId);
                        if (aUser == null)
                        {
                            aUser = aCustomerBll.GetUserByUserId(aGeneralInformation.CustomerId);
                        }

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aUser.Firstname + " " + aUser.Lastname + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                        string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;
                        string address = "";
                        bool flag = false;
                        if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                        {
                            RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                            if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                            {
                                string[] ss = aORder.DeliveryAddress.Split(',');
                                flag = true;
                                if (ss.Count() > 0)
                                {
                                    address += "," + ss[0];
                                }
                                if (ss.Count() > 1)
                                {
                                    address += ", " + ss[1];
                                }
                                if (ss.Count() > 2)
                                {
                                    address += "\r\n" + ss[2];
                                }
                                if (ss.Count() > 3)
                                {
                                    address += " " + ss[3];
                                }
                            }
                        }
                        if (deliveryButton.Text == "DEL" && deliveryButton.BackColor == Color.Black)
                        {
                            if (address == "")
                            {
                                if (string.IsNullOrEmpty(aUser.FullAddress))
                                {

                                    address += "" + aUser.House + " " + aUser.Address;
                                    address += ", " + aUser.City + "\r\n" + aUser.Postcode;
                                }
                                else
                                {
                                    address += (aUser.House != "" ? aUser.House + ", " : "");
                                    address += aUser.FullAddress + "\r\n" + aUser.Postcode;
                                }
                            }
                        }

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(address) + "\n";
                        aPrintContentsMid.Add(aPrintContent);

                        if (!flag)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = cell + "\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }
                    else if (customerTextBox.Text != "" && customerTextBox.Text != "Search Customer")
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = customerTextBox.Text + "\n";
                        aPrintContentsMid.Add(aPrintContent);

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }

                    // bool flag = false;
                    string printStr = "";
                    string printFooterStr = "";
                    Dictionary<int, string> recipeTypes = GetRecipeTypes(printer);
                    //aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();
                    int catId = 0;
                    bool startdas = false;
                    try
                    {
                        aListTypeDetails = aListTypeDetails.OrderBy(a => a.ReceipeTypeButton.SortOrder).ToList();
                    }
                    catch (Exception es)
                    {

                    }

                    List<OrderItemMerged> newOrderItemList = new List<OrderItemMerged>();
                    int FoundItemcount = 0;

                    //show packages if merged print set to true
                    if (aRestaurantInformation.UseJava > 0)
                    {
                        var pkgCount = 0;
                        foreach (RecipePackageMD package in aRecipePackageMdList)
                        {
                            if (recipeTypes.ContainsKey(package.RecipeTypeId))
                            {
                                pkgCount++;
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_alignmentString(package.Qty.ToString() + " " + package.PackageName, 0) + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                            }
                        }
                        if (pkgCount > 0)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }

                        //if merge print true, then merge package items together                        
                        List<PackageItem> aPaItem =
                            aPackageItemMdList.Where(
                                a => a.PackageId > 0)
                                .ToList();

                        foreach (PackageItem item in aPaItem)
                        {
                            if (!recipeTypes.ContainsKey(aVariousMethod.GetRecipeTypeIdByCategory(item.CategoryId)))
                            {
                                continue;
                            }                            

                            var exitingItem = getExistingOrderItem(newOrderItemList, null, item);

                            //&& a.OptionsIndex == item.PackageItemOptionsIndex
                            if (exitingItem != null)
                            {
                                exitingItem.Qty += item.Qty;
                                exitingItem.Price += item.Price * item.Qty;
                            }
                            else
                            {
                                OrderItemMerged orderItemMerged = new OrderItemMerged();
                                orderItemMerged.CategoryId = item.CategoryId;
                                orderItemMerged.CatSortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                                orderItemMerged.ItemFullName = item.ItemFullName;
                                orderItemMerged.ItemName = item.ItemName;
                                orderItemMerged.ItemId = item.ItemId;
                                orderItemMerged.KitchenProcessing = item.kitchenProcessing;
                                orderItemMerged.OptionsIndex = item.PackageItemOptionsIndex;
                                orderItemMerged.OptionName = null;
                                orderItemMerged.OptionNoOption = item.MinusOption;
                                orderItemMerged.Price = item.Price * item.Qty;
                                orderItemMerged.Qty = item.Qty;
                                newOrderItemList.Add(orderItemMerged);
                            }
                        }                        
                    }

                    List<MenuType> MenuTypes = new List<MenuType>();
                    int stater = 0;
                    foreach (RecipeTypeDetails typeDetails in aListTypeDetails)
                    {
                        var IsSelectedTypes = recipeTypes.Count(a => a.Key == typeDetails.RecipeTypeId);
                        if (IsSelectedTypes == 0)
                        {
                            continue;
                        }

                        if (menuSeperation == 5)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = typeDetails.ReceipeTypeButton.Text.ToUpper() + "\n";
                            aPrintContentsMid.Add(aPrintContent);

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                        }

                        foreach (OrderItemDetailsMD item_details in aOrderItemDetailsMDList)
                        {
                            if (item_details.RecipeTypeId == typeDetails.RecipeTypeId && recipeTypes.ContainsKey(item_details.RecipeTypeId))
                            {
                                if ((item_details.KitchenProcessing) < item_details.Qty)
                                {
                                    //check if the same item is already stored
                                    var exitingItem = getExistingOrderItem(newOrderItemList, item_details);
                                    if (exitingItem != null)
                                    {
                                        exitingItem.Qty += item_details.Qty;
                                        exitingItem.Price += item_details.Price * item_details.Qty;
                                    }
                                    else
                                    {
                                        OrderItemMerged orderItemMerged = new OrderItemMerged();
                                        orderItemMerged.CategoryId = item_details.CategoryId;
                                        orderItemMerged.CatSortOrder = item_details.CatSortOrder;
                                        orderItemMerged.ItemFullName = item_details.ItemFullName;
                                        orderItemMerged.ItemName = item_details.ItemName;
                                        orderItemMerged.ItemId = item_details.ItemId;
                                        orderItemMerged.ItemOption = item_details.ItemOption;
                                        orderItemMerged.KitchenDone = item_details.KitchenDone;
                                        orderItemMerged.KitchenProcessing = item_details.KitchenProcessing;
                                        orderItemMerged.KitchenSection = item_details.KitchenSection;
                                        orderItemMerged.OptionsIndex = item_details.OptionsIndex;
                                        orderItemMerged.OptionName = item_details.OptionName;
                                        orderItemMerged.OptionNoOption = item_details.OptionNoOption;
                                        orderItemMerged.Option_ids = item_details.Option_ids;
                                        orderItemMerged.Price = item_details.Price * item_details.Qty;
                                        orderItemMerged.Qty = item_details.Qty;
                                        orderItemMerged.RecipeTypeId = item_details.RecipeTypeId;
                                        orderItemMerged.sendToKitchen = item_details.sendToKitchen;
                                        orderItemMerged.SortOrder = item_details.SortOrder;
                                        newOrderItemList.Add(orderItemMerged);
                                    }
                                }
                            }
                        }
                    }

                    int CategoryId = 0;
                    
                    newOrderItemList = newOrderItemList.OrderBy(a => a.CatSortOrder).ToList();
                    int starterCount = newOrderItemList.Count(a => starterIds.Contains(a.CategoryId));
                    MenuType rtype = new MenuType();
                    foreach (OrderItemMerged itemDetails in newOrderItemList)
                    {
                        FoundItemcount++;
                        
                        if (CategoryId != itemDetails.CategoryId && menuSeperation == 4)
                        {
                            string CateName = catList.FirstOrDefault(a => a.CategoryId == itemDetails.CategoryId).CategoryName;
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = CateName.ToUpper() + "\n";
                            aPrintContentsMid.Add(aPrintContent);

                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);

                            CategoryId = itemDetails.CategoryId;
                        }
                        else if (CategoryId != itemDetails.CategoryId && menuSeperation == 1)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                            CategoryId = itemDetails.CategoryId;
                        }

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen((aGeneralInformation.TableId > 0 ? (itemDetails.Qty - (itemDetails.KitchenProcessing)) : itemDetails.Qty) + " " + itemDetails.ItemName) + "\n";
                        aPrintContentsMid.Add(aPrintContent);

                        blankLine++;

                        List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId &&
                                       a.OptionsIndex == itemDetails.OptionsIndex).ToList();
                        if (aOption.Count > 0)
                        {
                            foreach (RecipeOptionMD option in aOption)
                            {
                                if (!string.IsNullOrEmpty(option.Title))
                                {
                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = "  →" + option.Title + "\n";
                                    aPrintContentsMid.Add(aPrintContent);

                                    blankLine++;
                                }
                                if (!string.IsNullOrEmpty(option.MinusOption))
                                {

                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = "  →No" + option.MinusOption + "\n";
                                    aPrintContentsMid.Add(aPrintContent);

                                    blankLine++;
                                }
                            }
                        }

                        if (menuSeperation == 3 && starterIds.Contains(itemDetails.CategoryId))
                        {
                            stater++;
                            //Stater wise
                            
                            if (stater == starterCount)
                            {
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                            }
                        }
                        if (menuSeperation == 2)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }
                    }
                    List<int> starterIdsForPackage = aRestaurantMenuBLL.GetCategoriesByName("Starter");
                    if (starterIdsForPackage.Count == 0)
                    {
                        starterIdsForPackage = aRestaurantMenuBLL.GetCategoriesByName("Starters");
                    }

                    if (aRestaurantInformation.UseJava <= 0)
                    {
                        foreach (RecipePackageMD package in aRecipePackageMdList)
                        {
                            var type = recipeTypes.FirstOrDefault(a => a.Key == package.RecipeTypeId).Value;
                            if (type != null)
                            {
                                List<PackageItem> aPaItem =
                                aPackageItemMdList.Where(a => a.PackageId == package.PackageId && a.OptionsIndex == package.OptionsIndex && a.DeleteItem).ToList();
                                if (aPaItem.Count == 0)
                                {
                                    continue;
                                }
                                FoundItemcount++;
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(package.Qty.ToString() + " " + package.PackageName.ToLower()) + "\n";
                                aPrintContentsMid.Add(aPrintContent);

                                blankLine++;

                                List<PackageItem> aPaItemNew = new List<PackageItem>();
                                foreach (PackageItem item in aPaItem)
                                {
                                    item.CategorySortOrder = aVariousMethod.GetSortOrderByCategory(item.CategoryId);
                                    aPaItemNew.Add(item);
                                }
                                aPaItem = aPaItemNew.OrderBy(a => a.CategorySortOrder).ToList();
                                bool startdasForPackage = false;
                                foreach (PackageItem itemDetails in aPaItem)
                                {
                                    string packageItemPrice = itemDetails.Price > 0
                                        ? (itemDetails.Price).ToString()
                                        : "";

                                    if (menuSeperation == 3 && startdasForPackage && !starterIdsForPackage.Contains(itemDetails.CategoryId))
                                    {
                                        aPrintContent = new PrintContent();
                                        aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                                        aPrintContentsMid.Add(aPrintContent);
                                        startdasForPackage = false;
                                    }

                                    aPrintContent = new PrintContent();
                                    aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(" " + itemDetails.Qty.ToString() + "  " + itemDetails.ItemName) + "\n";
                                    aPrintContentsMid.Add(aPrintContent);

                                    if (starterIdsForPackage.Count == itemDetails.CategoryId)
                                    {
                                        startdasForPackage = true;
                                    }

                                    blankLine++;
                                    string options = "";
                                    List<RecipeOptionMD> aOption =
                                        aRecipeOptionMdList.Where(
                                            a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex && a.PackageItemOptionsIndex == itemDetails.PackageItemOptionsIndex)
                                            .ToList();
                                    if (aOption.Count > 0)
                                    {
                                        foreach (RecipeOptionMD option in aOption)
                                        {
                                            if (!string.IsNullOrEmpty(option.Title))
                                            {
                                                aPrintContent = new PrintContent();
                                                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(" " + " " + " →" + option.Title) + "\n";
                                                aPrintContentsMid.Add(aPrintContent);
                                                blankLine++;
                                            }
                                            if (!string.IsNullOrEmpty(option.MinusOption))
                                            {
                                                aPrintContent = new PrintContent();
                                                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(" " + " " + " →No" + option.Title) + "\n";
                                                aPrintContentsMid.Add(aPrintContent);

                                                blankLine++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    foreach (RecipeMultipleMD package in aRecipeMultipleMdList)
                    {
                        if (recipeTypes.ContainsKey(package.RecipeTypeId))
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(package.Qty.ToString() + " " + package.MultiplePartName.ToLower()) + "\n";
                            aPrintContentsMid.Add(aPrintContent);

                            blankLine++;
                            List<MultipleItemMD> aPaItem =
                                aMultipleItemMdList.Where(
                                    a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                                    .ToList();
                            int cnt = 0;
                            foreach (MultipleItemMD itemDetails in aPaItem)
                            {
                                cnt++;
                                string packageItemPrice = itemDetails.Price > 0
                                    ? (itemDetails.Price).ToString()
                                    : "";
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(" " + (cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) +
                                                  ": " + itemDetails.ItemName.ToLower()) + "\n";
                                aPrintContentsMid.Add(aPrintContent);

                                blankLine++;
                                string options = "";
                                List<RecipeOptionMD> aOption =
                                    aRecipeOptionMdList.Where(
                                        a =>
                                            a.RecipeId == itemDetails.ItemId &&
                                            a.OptionsIndex == itemDetails.OptionsIndex)
                                        .ToList();
                                if (aOption.Count > 0)
                                {

                                    foreach (RecipeOptionMD option in aOption)
                                    {
                                        if (!string.IsNullOrEmpty(option.Title))
                                        {
                                            aPrintContent = new PrintContent();
                                            aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(" " + " " + " →" + option.Title) + "\n";
                                            aPrintContentsMid.Add(aPrintContent);

                                            blankLine++;
                                        }
                                        if (!string.IsNullOrEmpty(option.MinusOption))
                                        {
                                            aPrintContent = new PrintContent();
                                            aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen(" " + " " + " →No" + option.MinusOption.ToLower()) + "\n";
                                            aPrintContentsMid.Add(aPrintContent);

                                            blankLine++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (blankLine < aRestaurantInformation.RecieptMinHeight)
                    {
                        for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = " " + " " + "\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }
                    }
                    blankLine = 0;

                    if (FoundItemcount <= 0)
                    {
                        continue;
                    }
                    int numOfLine = blankLine;
                    double subamount = GetTotalAmountDetails();
                    subamount = GlobalVars.numberRound(subamount, 2);

                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.get_alignmentString(" SUBTOTAL  £" + subamount.ToString("F02"), ("£" + subamount.ToString("F02")).Length) + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                    double amount = GetTotalAmountDetails() + aGeneralInformation.CardFee -
                                   GlobalVars.numberRound(aGeneralInformation.DiscountFlat) - GlobalVars.numberRound(aGeneralInformation.ItemDiscount);

                    amount = GlobalVars.numberRound(amount, 2);

                    if (aGeneralInformation.DiscountFlat > 0)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_alignmentString("Discount (" + aGeneralInformation.DiscountPercent.ToString("F02") + "%) " + aGeneralInformation.DiscountFlat.ToString("F02"), aGeneralInformation.DiscountFlat.ToString("F02").Length) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                        numOfLine++;
                    }

                    if (aGeneralInformation.CardFee > 0)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_alignmentString("S/C " + aGeneralInformation.CardFee.ToString("F02"), aGeneralInformation.CardFee.ToString("F02").Length) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                        numOfLine++;
                    }
                    if (aGeneralInformation.DeliveryCharge > 0 && deliveryButton.Text == "DEL" &&
                        deliveryButton.BackColor == Color.Black && collectionButton.BackColor != Color.Black)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_alignmentString("Delivery charge   £" + aGeneralInformation.DeliveryCharge.ToString("F02"), ("£" + aGeneralInformation.DeliveryCharge.ToString("F02")).Length) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                        amount += aGeneralInformation.DeliveryCharge;
                    }
                    //aGeneralInformation.PaymentMethod
                    if (customTotalTextBox.Text != "Custom Total" && Convert.ToDouble(customTotalTextBox.Text) > 0)
                    {
                        amount = Convert.ToDouble(customTotalTextBox.Text);
                    }
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.get_alignmentString(DateTime.Now.ToString("hh:mmtt") + "  TOTAL  £" + amount.ToString("F02"), ("£" + amount.ToString("F02")).Length) + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                    if (aPaymentDetails != null)
                    {
                        if (aPaymentDetails.CashAmount > 0)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("PAID BY CASH") + "\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }
                        else if (aPaymentDetails.CardAmount > 0)
                        {
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("PAID BY CARD") + "\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }
                    }
                    else
                    {                        
                        if (aGeneralInformation.PaymentMethod.ToLower() == "cash")
                        {
                            aPrintContent = new PrintContent();
                            if (paymentStatus)
                            {
                                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("PAID BY CARD") + "\n";
                            }
                            else
                            {
                                aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("ORDER IS NOT PAID") + "\n";
                            }
                            
                            aPrintContentsMid.Add(aPrintContent);
                        }
                        else
                        {                            
                            aPrintContent = new PrintContent();
                            aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace("PAID BY CARD") + "\n";
                            aPrintContentsMid.Add(aPrintContent);
                        }
                    }
                    
                    if (commentTextBox.Text != "Comment" && commentTextBox.Text.Trim().Length > 0)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_fullString(commentTextBox.Text.ToLower()) + "\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }
                    if (aRestaurantInformation.ShowOrderNumber > 0)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.CreateDashedLineForKitchen() + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);

                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.CenterTextWithWhiteSpace(OrderNo.ToString("D2")) + "\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }

                    string allpage = "";
                    for (int i = 0; i < aPrintContentsMid.Count; i++)
                    {
                        allpage += aPrintContentsMid[i].StringLine;
                    }

                    PrintMethods tempPrintMethods = new PrintMethods(true, false, printer.printerMargin);
                    tempPrintMethods.USBPrint(allpage, printer.PrinterName, printer.PrintCopy);
                }

                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                aRestaurantOrderBLL.UpdateKitchenStatus(orderId);
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
            }
        }

        public Dictionary<int, string> GetRecipeTypes(PrinterSetup recipeTypes)
        {
            Dictionary<int, string> types = new Dictionary<int, string>();
            string[] list = recipeTypes.RecipeTypeList.Split(',');
            string[] name = recipeTypes.RecipeNames.Split(',');

            for (int i = 0; i < list.Count(); i++)
            {
                int type = Convert.ToInt32("0" + list[i]);
                string typeName = name[i];
                types.Add(type, typeName);
            }
            return types;
        }

        private void ClearAllOrderInformation()
        {
            commentTextBox.Text = "Comment";
            customerDetailsLabel.Text = "";
            customerEditButton.Visible = false;
            phoneNumberDeleteButton.Visible = false;
            customerRecentItemsButton.Visible = false;
            recentItemsFlowLayoutPanel.Visible = false;
            customerTextBox.Text = "Search Customer";
            customerTextBox.Visible = true;
            customTotalTextBox.Text = "Custom Total";
            aRecipeOptionMdList = new List<RecipeOptionMD>();
            aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
            aRecipePackageMdList = new List<RecipePackageMD>();
            aPackageItemMdList = new List<PackageItem>();
            aRecipeMultipleMdList = new List<RecipeMultipleMD>();
            aMultipleItemMdList = new List<MultipleItemMD>();
            orderDetailsflowLayoutPanel1.Controls.Clear();
            customPanel.Location = new Point(customPanel.Location.X,
                orderDetailsflowLayoutPanel1.Location.Y + orderDetailsflowLayoutPanel1.Size.Height);
            paymentDetailsPanel.Location = new Point(paymentDetailsPanel.Location.X,
                customPanel.Location.Y + customPanel.Size.Height);
            aGeneralInformation = new GeneralInformation();
            aGeneralInformation.OrderType = aRestaurantInformation.DefaultOrderType;
            LoadOrderDetails(0);
            AddServiceChargeIntoLabel();
        }

        private List<OrderPackage> GetOrderPackage(int orderId)
        {
            List<OrderPackage> aOrderPackages = new List<OrderPackage>();
            foreach (RecipePackageMD package in aRecipePackageMdList)
            {
                OrderPackage aPackage = new OrderPackage();
                aPackage.Id = package.id;
                aPackage.Name = package.PackageName;
                aPackage.OrderId = orderId;

                aPackage.PackageId = package.PackageId;
                aPackage.Price = (package.Qty * package.UnitPrice);
                aPackage.Extra_price = (package.Qty * package.Extraprice);
                aPackage.Quantity = (int)package.Qty;
                aPackage.optionIndex = package.OptionsIndex;
                aOrderPackages.Add(aPackage);
            }
            return aOrderPackages;
        }

        private double GetPackageItemPrice(int packageId, int optionIndex)
        {
            double sum =
                aPackageItemMdList.Where(a => a.PackageId == packageId && a.OptionsIndex == optionIndex)
                    .Sum(b => b.Price);

            return sum;
        }

        private double GetPackageItemOptionPrice(int packageId, int optionIndex)
        {
            double sum =
                aPackageItemMdList.Where(a => a.PackageId == packageId && a.OptionsIndex == optionIndex)
                    .Sum(a => a.PackageItemOptionList.Sum(b => b.Price));

            return sum;
        }

        private List<OrderItem> GetOrderItem(int orderId, List<KeyValuePair<int, int>> packageIds = null)
        {
            List<OrderItem> aOrderItem = new List<OrderItem>();
            try
            {
                foreach (OrderItemDetailsMD itemDetails in aOrderItemDetailsMDList)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.ExtraPrice = 0;
                    orderItem.LastModifyTime = DateTime.Now;
                    orderItem.Name = itemDetails.ItemName;
                    orderItem.Id = itemDetails.OrderItemId;
                    orderItem.OrderId = orderId;
                    orderItem.Options = GetOptions(itemDetails).Title;
                    orderItem.Options_ids = GetOptions(itemDetails).optionId;
                    orderItem.MinusOptions = GetMinusOptions(itemDetails).Title;
                    orderItem.PackageId = 0;
                    orderItem.Price = itemDetails.Qty * itemDetails.Price;
                    orderItem.Quantity = itemDetails.Qty;
                    orderItem.RecipeId = itemDetails.ItemId;
                    orderItem.SentToKitchen = itemDetails.sendToKitchen;
                    orderItem.KitchenProcessing = itemDetails.KitchenProcessing;
                    orderItem.KitchenDone = itemDetails.KitchenDone;
                    orderItem.MultipleMenu = "";
                    orderItem.PreOrderId = itemDetails.OrderId;
                    aOrderItem.Add(orderItem);
                }
                foreach (PackageItem itemDetails in aPackageItemMdList)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.ExtraPrice = 0;
                    orderItem.KitchenDone = 0;
                    orderItem.KitchenProcessing = 0;
                    orderItem.LastModifyTime = DateTime.Now;
                    orderItem.Name = itemDetails.ItemName;
                    orderItem.OrderId = orderId;

                    orderItem.Options = GetOptionsForpackGe(itemDetails).Title;
                    orderItem.Options_ids = "";

                    orderItem.MinusOptions = GetOptionsForpackageForWithNoOption(itemDetails).Title;
                    orderItem.PackageId = itemDetails.PackageId;
                    orderItem.Price = itemDetails.Price;
                    orderItem.ExtraPrice = itemDetails.Price;
                    orderItem.Quantity = itemDetails.Qty;
                    orderItem.RecipeId = itemDetails.ItemId;
                    if (packageIds != null)
                        orderItem.orderPackageId = packageIds.Where(i => i.Key == itemDetails.OptionsIndex).Select(i => i.Value).First();

                    orderItem.SentToKitchen = 1;
                    aOrderItem.Add(orderItem);

                }
                foreach (RecipeMultipleMD itemDetails in aRecipeMultipleMdList)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.ExtraPrice = 0;
                    orderItem.KitchenDone = 0;
                    orderItem.KitchenProcessing = 1;
                    orderItem.LastModifyTime = DateTime.Now;
                    orderItem.Name = itemDetails.MultiplePartName;
                    orderItem.OrderId = orderId;
                    orderItem.Options = GetOptionsForMultiple(itemDetails);
                    orderItem.MinusOptions = GetMinusOptionsForMultiple(itemDetails);
                    orderItem.PackageId = 0;
                    orderItem.Price = itemDetails.Qty * itemDetails.UnitPrice;
                    orderItem.Quantity = (int)itemDetails.Qty;
                    orderItem.RecipeId = itemDetails.ItemId;
                    orderItem.SentToKitchen = 1;
                    orderItem.MultipleMenu = GetMultipleMenu(itemDetails);
                    aOrderItem.Add(orderItem);

                }
            }
            catch(Exception ex)
            {

            }
            return aOrderItem;
        }

        private string GetMultipleMenu(RecipeMultipleMD itemDetails)
        {
            List<MultipleItemMD> aMultipleItemMds =
                aMultipleItemMdList.Where(a => a.OptionsIndex == itemDetails.OptionsIndex).ToList();
            List<MultipleMenuDetailsJson> aMultipleMenuDetailsJsons = new List<MultipleMenuDetailsJson>();
            foreach (MultipleItemMD itemMd in aMultipleItemMds)
            {
                MultipleMenuDetailsJson aJson = new MultipleMenuDetailsJson();
                aJson.category_id = itemMd.CategoryId;
                aJson.id = itemMd.ItemId;
                aJson.kitchen_section = 0;
                aJson.name = itemMd.ItemName;
                aJson.options = GetOptionsStringList(itemMd, false);
                aJson.minus_options = GetOptionsStringList(itemMd, true);
                aJson.price = 0;
                aJson.quantity = itemMd.Qty;
                aJson.recipe_type_id = itemDetails.RecipeTypeId;
                aJson.sort_order = 0;

                aMultipleMenuDetailsJsons.Add(aJson);
            }

            var json = JsonConvert.SerializeObject(aMultipleMenuDetailsJsons);

            return json;
        }

        private List<string> GetOptionsStringList(MultipleItemMD itemMd, bool isMinus)
        {
            List<string> optionsList = new List<string>();
            List<RecipeOptionMD> aRecipeOptionMds =
                aRecipeOptionMdList.Where(a => a.OptionsIndex == itemMd.OptionsIndex && a.RecipeId == itemMd.ItemId)
                    .ToList();
            foreach (RecipeOptionMD options in aRecipeOptionMds)
            {
                if (!isMinus)
                {
                    optionsList.Add(options.Title);
                }
                else
                {
                    optionsList.Add(options.MinusOption);
                }
            }
            return optionsList;
        }

        private string GetOptionsForMultiple(RecipeMultipleMD itemDetails)
        {
            List<OptionJson> listOfOptionJsons = new List<OptionJson>();
            string options = "";
            List<RecipeOptionMD> aOption =
                aRecipeOptionMdList.Where(
                    a =>
                        a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex &&
                        a.Title != null).ToList();
            if (aOption.Count > 0)
            {
                foreach (RecipeOptionMD option in aOption)
                {
                    listOfOptionJsons.Add(new OptionJson
                    {
                        optionName = option.Title,
                        optionId = option.RecipeOPtionItemId.ToString(),
                        optionPrice = option.Price,
                        optionQty = (int)option.Qty
                    });
                }
            }
            return options;
        }

        private string GetMinusOptionsForMultiple(RecipeMultipleMD itemDetails)
        {
            string options = "";
            List<OptionJson> listOfOptionJsons = new List<OptionJson>();
            List<RecipeOptionMD> aOption =
                aRecipeOptionMdList.Where(
                    a =>
                        a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex &&
                        a.MinusOption != null).ToList();
            if (aOption.Count > 0)
            {
                foreach (RecipeOptionMD option in aOption)
                {
                    listOfOptionJsons.Add(new OptionJson
                    {
                        optionName = option.Title,
                        optionId = option.RecipeOPtionItemId.ToString(),
                        optionPrice = option.Price,
                        optionQty = (int)option.Qty
                    });
                }
            }
            return options;
        }

        private RecipeOptionButton GetOptionsForpackGe(PackageItem itemDetails)
        {
            string options = "";
            string optionId = "";

            List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex && a.PackageItemOptionsIndex == itemDetails.PackageItemOptionsIndex).ToList();
            List<OptionJson> listOfOptionJsons = new List<OptionJson>();

            RecipeOptionButton optionMd = new RecipeOptionButton();

            optionMd.optionId = "";

            if (aOption.Count > 0)
            {
                foreach (RecipeOptionMD option in aOption)
                {
                    if (option.MinusOption == null)
                    {
                        listOfOptionJsons.Add(new OptionJson
                        {
                            optionName = option.Title,optionId = option.RecipeOptionId.ToString(),
                            optionPrice = option.Price,
                            optionQty = (int)option.Qty,
                            NoOption = false
                        });
                    }
                }
            }

            RecipeOptionButton optionButton = new RecipeOptionButton();
            optionButton.optionId = optionId;
            optionButton.Title = new OptionJsonConverter().Serialize(listOfOptionJsons);

            return optionButton;
        }

        private RecipeOptionButton GetOptionsForpackageForWithNoOption(PackageItem itemDetails)
        {
            string options = "";
            string optionId = "";

            List<RecipeOptionMD> aOption =
                aRecipeOptionMdList.Where(
                    a =>
                        a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex).ToList();
            List<OptionJson> listOfOptionJsons = new List<OptionJson>();

            RecipeOptionButton optionMd = new RecipeOptionButton();

            optionMd.optionId = "";

            if (aOption.Count > 0)
            {
                foreach (RecipeOptionMD option in aOption)
                {
                    if (option.MinusOption != null)
                    {
                        listOfOptionJsons.Add(new OptionJson
                        {
                            optionName = "No" + option.MinusOption,
                            optionId = option.RecipeOPtionItemId.ToString(),
                            optionPrice = 0.0,
                            optionQty = (int)option.Qty,
                            NoOption = true
                        });
                    }
                }
            }

            RecipeOptionButton optionButton = new RecipeOptionButton();
            optionButton.optionId = optionId;
            optionButton.Title = new OptionJsonConverter().Serialize(listOfOptionJsons);

            return optionButton;
        }

        private string GetMinusOptionForpackGes(PackageItem itemDetails)
        {
            string options = "";
            List<RecipeOptionMD> aOption =
                aRecipeOptionMdList.Where(
                    a =>
                        a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex &&
                        a.MinusOption != null).ToList();
            if (aOption.Count > 0)
            {
                foreach (RecipeOptionMD option in aOption)
                {
                    if (options != "") options += ",";
                    options += option.MinusOption;
                }
            }
            return options;
        }

        private RecipeOptionButton GetOptions(OrderItemDetailsMD itemDetails)
        {
            List<OptionJson> listOfOptionJsons = new List<OptionJson>();
            List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex).ToList();
            if (aOption.Count > 0)
            {
                foreach (RecipeOptionMD option in aOption)
                {
                    if (option.MinusOption == null)
                    {
                        listOfOptionJsons.Add(new OptionJson
                        {
                            optionName = option.Title,
                            optionId = option.RecipeOPtionItemId.ToString(),
                            optionPrice = option.Price,
                            optionQty = (int)option.Qty,
                            NoOption = false
                        });
                    }
                }
            }

            RecipeOptionButton optionMd = new RecipeOptionButton();
            optionMd.Title = new OptionJsonConverter().Serialize(listOfOptionJsons);
            optionMd.optionId = "";

            return optionMd;
        }

        private RecipeOptionButton GetMinusOptions(OrderItemDetailsMD itemDetails)
        {
            List<OptionJson> listOfOptionJsons = new List<OptionJson>();
            List<RecipeOptionMD> aOption =
                aRecipeOptionMdList.Where(
                    a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex).ToList();
            if (aOption.Count > 0)
            {
                foreach (RecipeOptionMD option in aOption)
                {
                    if (option.MinusOption != null)
                    {
                        listOfOptionJsons.Add(new OptionJson
                        {
                            optionName = "NO " + option.MinusOption,
                            optionId = option.RecipeOPtionItemId.ToString(),
                            optionPrice = 0.0,
                            optionQty = (int)option.Qty,
                            NoOption = true
                        });
                    }
                }
            }

            RecipeOptionButton optionMd = new RecipeOptionButton();
            optionMd.Title = new OptionJsonConverter().Serialize(listOfOptionJsons);
            optionMd.optionId = "";

            return optionMd;
        }

        private RestaurantOrder GetRestaurantOrder(PaymentDetails aPaymentDetails)
        {
            if (aGeneralInformation.OrderType == null)
            {
                aGeneralInformation.OrderType = GlobalSetting.RestaurantInformation.DefaultOrderType;
            }
            //  GlobalSetting.RestaurantInformation.DefaultOrderType;
            if (aPaymentDetails.CardFee > 0)
            {
                aGeneralInformation.CardFee = aPaymentDetails.CardFee;
                LoadAmountDetails();
            }
            else
            {
                aGeneralInformation.CardFee = aGeneralInformation.CardFee;
            }
            double totalCost = 0;
            RestaurantOrder aRestaurantOrder = new RestaurantOrder();
            aRestaurantOrder.CardAmount = GlobalVars.numberRound(Convert.ToDouble(aPaymentDetails.CardAmount), 2); 
            aRestaurantOrder.CardFee = GlobalVars.numberRound(Convert.ToDouble(aGeneralInformation.CardFee), 2); 
            aRestaurantOrder.CashAmount = GlobalVars.numberRound(Convert.ToDouble(aPaymentDetails.CashAmount), 2); 
            aRestaurantOrder.Comment = commentTextBox.Text == "Comment" ? "" : commentTextBox.Text;
            aRestaurantOrder.DeliveryCost = GlobalVars.numberRound(Convert.ToDouble(aGeneralInformation.DeliveryCharge), 2); 
            aRestaurantOrder.OrderType = aGeneralInformation.OrderType;
            if (aRestaurantOrder.OrderType == "Wait" || aGeneralInformation.DeliveryTime == "")
            {
                aRestaurantOrder.DeliveryTime = DateTime.Now;
            }
            else
            {
                aRestaurantOrder.DeliveryTime = Convert.ToDateTime(aGeneralInformation.DeliveryTime);
            }

            if (customerTextBox.Text != "" && customerTextBox.Text != "Search Customer" && aRestaurantOrder.CustomerId == 0)
            {
                try
                {
                    RestaurantUsers aCustomerBll = new CustomerBLL().GetCustomerByPhone(customerTextBox.Text);
                    aRestaurantOrder.CustomerName = aCustomerBll.Name;
                    aRestaurantOrder.CustomerEmail = aCustomerBll.Email;
                    aRestaurantOrder.PostCode = aCustomerBll.Postcode;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //if (aRestaurantOrder.CashAmount > 0 && aRestaurantOrder.CardAmount > 0) {
            //    aRestaurantOrder.PaymentMethod = "SPLIT";
            //}
            //else if (aRestaurantOrder.CashAmount > 0 && aRestaurantOrder.CardAmount == 0) {
            //    aRestaurantOrder.PaymentMethod = "CASH";
            //}
            //else
            //{
            //    aRestaurantOrder.PaymentMethod = "CARD";
            //}
            aRestaurantOrder.Discount = GlobalVars.numberRound(Convert.ToDouble((aGeneralInformation.ItemDiscount + aGeneralInformation.DiscountFlat)), 2); 
            aRestaurantOrder.OrderTime = DateTime.Now;
            aRestaurantOrder.PaymentMethod = aPaymentDetails.PaymentMethod;
            aRestaurantOrder.ServiceCharge = aGeneralInformation.ServiceCharge;
            if (!double.TryParse(customTotalTextBox.Text, out totalCost))
            {
                aRestaurantOrder.TotalCost = GetTotalAmountDetails() + aRestaurantOrder.DeliveryCost +
                                             aRestaurantOrder.ServiceCharge + aRestaurantOrder.CardFee -
                                             aRestaurantOrder.Discount;
                aRestaurantOrder.TotalCost = GlobalVars.numberRound(Convert.ToDouble(aRestaurantOrder.TotalCost), 2);
            }
            else
            {
                aRestaurantOrder.TotalCost = GlobalVars.numberRound(Convert.ToDouble(totalCost), 2);                
            }

            if (aRestaurantOrder.CashAmount == 0 && aRestaurantOrder.CardAmount == 0)
            {
                aRestaurantOrder.CashAmount = aRestaurantOrder.TotalCost;
                aRestaurantOrder.PaymentMethod = "Cash";
            }
            aRestaurantOrder.UserId = GlobalSetting.UserId;
            aRestaurantOrder.CustomerId = aGeneralInformation.CustomerId;
            aRestaurantOrder.RestaurantId = aRestaurantInformation.Id;
            aRestaurantOrder.OrderTable = aGeneralInformation.TableId;
            aRestaurantOrder.ServedBy = "" + aPaymentDetails.ServedBy;
            aRestaurantOrder.SpecialOfferItem = "";
            aRestaurantOrder.UpdateTime = DateTime.Now.Date;
            aRestaurantOrder.Person = aGeneralInformation.Person;
            return aRestaurantOrder;
        }

        public double GetTotalAmountDetails()
        {
            double amount1 = aOrderItemDetailsMDList.Sum(a => a.Qty * a.Price);
            double amount2 = aRecipePackageMdList.Sum(a => (a.Qty * a.UnitPrice));
            double amount3 = aPackageItemMdList.Sum(a => a.Price);
            double amount4 = aRecipeMultipleMdList.Sum(a => a.Qty * a.UnitPrice);
            return GlobalVars.numberRound(Convert.ToDouble(amount1 + amount2 + amount3 + amount4), 2);
        }

        public double GetSubAmountDetails()
        {
            double amount1 = aOrderItemDetailsMDList.Sum(a => a.KitchenDone * a.Price);
            double amount2 = aRecipePackageMdList.Sum(a => a.Qty * a.UnitPrice);
            double amount3 = aPackageItemMdList.Sum(a => a.Price);
            double amount4 = aRecipeMultipleMdList.Sum(a => a.Qty * a.UnitPrice);
            return amount1 + amount2 + amount3 + amount4;
        }

        private void timeButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            DeliveryTimeForm.TimeDetails = new TimeDetails();
            DeliveryTimeForm aDeliveryTimeForm = new DeliveryTimeForm(collectionButton.BackColor == Color.Black ? true : false);
            aDeliveryTimeForm.ShowDialog();
            TimeDetails aTimeDetails = DeliveryTimeForm.TimeDetails;
            aGeneralInformation.PrintOrderType = "";
            if (aTimeDetails.Status == "Ok")
            {
                timeButton.Text = aTimeDetails.DeliveryTime == "" ? aTimeDetails.IsCollect : aTimeDetails.DeliveryTime;
                if (timeButton.Text.ToLower() == "time")
                {
                    //  aGeneralInformation.OrderType = aTimeDetails.IsCollect;
                    aGeneralInformation.DeliveryTime = "";
                }
                else if (aTimeDetails.DeliveryTime != "")
                {
                    //aGeneralInformation.OrderType = "CLT";
                    aGeneralInformation.DeliveryTime = aTimeDetails.DeliveryTime;
                }

                else if (timeButton.Text.ToLower() == "wait")
                {
                    aGeneralInformation.PrintOrderType = "Wait";
                    aGeneralInformation.DeliveryTime = aTimeDetails.DeliveryTime;
                }
            }
        }

        private void collectionButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            ChangeButtonLocation(false); 
            collectionButton.BackColor = Color.Black;
            deliveryButton.BackColor = Color.WhiteSmoke;
            collectionButton.ForeColor = Color.WhiteSmoke;
            deliveryButton.ForeColor = Color.Black;
            aGeneralInformation.DeliveryCharge = 0;
            deliveryChargeButton.Text = "Delivery Charge\r\n0";
            deliveryChargeButton.TextAlign = ContentAlignment.MiddleCenter;
            aGeneralInformation.OrderType = "CLT";
            
            LoadAmountDetails();
            
            CustomerBLL aCustomerBll = new CustomerBLL();
            if (aGeneralInformation.CustomerId > 0)
            {
                RestaurantUsers aRestaurantUser =
                    aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
                string address = GetCustomerDetails(aRestaurantUser);
                customerDetailsLabel.Text = address;
            }
        }

        private void ChangeButtonLocation(bool deliveryChargeButtonShow)
        {
            int len = 3;
            if (!deliveryChargeButtonShow)
            {
                deliveryChargeButton.Visible = false;
                paidButton.Location = new Point(54, 2);
                discountButton.Location = new Point(paidButton.Location.X + paidButton.Width + len, 2);
                OrderSaveOnlyBtn.Location = new Point(0, 0);
                KitchenPrintBtn.Location = new Point(96,0);
                OrderSaveOnlyBtn.Height = KitchenPrintBtn.Height = 93;
            }
            else
            {
                deliveryChargeButton.Visible = true;
                paidButton.Location = new Point(3, 2);
                discountButton.Location = new Point(paidButton.Location.X + paidButton.Width + len, 2);
                deliveryChargeButton.Location = new Point(discountButton.Location.X + discountButton.Width + len, 2);
                OrderSaveOnlyBtn.Location = new Point(0, 50);
                KitchenPrintBtn.Location = new Point(96, 50);
                OrderSaveOnlyBtn.Height = KitchenPrintBtn.Height = 43;                   
            }
        }

        private void deliveryButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            if (deliveryButton.Text != "RES")
            {
                collectionButton.BackColor = Color.WhiteSmoke;
                deliveryButton.BackColor = Color.Black;
                deliveryButton.ForeColor = Color.WhiteSmoke;
                collectionButton.ForeColor = Color.Black;
                double totalAmount = GetTotalAmountDetails();
                aGeneralInformation.DeliveryCharge = totalAmount >= aRestaurantInformation.MinOrder
                    ? 0
                    : aRestaurantInformation.DeliveryCharge;
                deliveryChargeButton.Text = aGeneralInformation.DeliveryCharge <= 0
                    ? "Delivery Charge\r\n0"
                    : "Delivery Charge\r\n" + aGeneralInformation.DeliveryCharge.ToString();
                aGeneralInformation.OrderType = "DEL";
                deliveryChargeButton.BackColor = Color.FromArgb(151, 106, 55);
                timeButton.Text = "Time";
                ChangeButtonLocation(true);

                if (aGeneralInformation.CustomerId > 0)
                {
                    CustomerBLL aCustomerBll = new CustomerBLL();
                    RestaurantUsers aRestaurantUser =
                        aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
                    LoadDeliveryCharge(aRestaurantUser);
                } 
                   
                LoadAmountDetails();
            }
            else
            {
                deliveryButton.BackColor = Color.Black;
                deliveryButton.ForeColor = Color.WhiteSmoke;
                ChangeButtonLocation(true);
            }

            if (aGeneralInformation.CustomerId > 0)
            {
                CustomerBLL aCustomerBll = new CustomerBLL();
                RestaurantUsers aRestaurantUser =
                    aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
                string customerAddress = GetCustomerDetails(aRestaurantUser);
                customerDetailsLabel.Text = customerAddress;
            }
        }

        public string GetCustomerDetails(RestaurantUsers aRestaurantUser)
        {
            string cell = aRestaurantUser.Mobilephone != "" ? aRestaurantUser.Mobilephone : aRestaurantUser.Homephone;
            string address = aRestaurantUser.Firstname;
            if (!string.IsNullOrEmpty(aRestaurantUser.Lastname))
            {
                address += " " + aRestaurantUser.Lastname;
            }

            if (address.Length > 0)
            {
                address += ", ";
            }
            if (cell.Length > 0)
            {
                address += " " + cell;
            }

            if (deliveryButton.BackColor == Color.Black || aGeneralInformation.OrderType == "DEL")
            {
                if (string.IsNullOrEmpty(aRestaurantUser.FullAddress))
                {
                    address += "\n" + aRestaurantUser.House;

                    if (!string.IsNullOrEmpty(aRestaurantUser.House))
                    {
                        address += ",";
                    }
                    address += aRestaurantUser.Address;

                    if (!string.IsNullOrEmpty(aRestaurantUser.Address))
                    {
                        address += ",";
                    }
                    address += aRestaurantUser.City;

                    if (!string.IsNullOrEmpty(aRestaurantUser.City))
                    {
                        address += ",";
                    }
                    address += aRestaurantUser.Postcode;
                }
                else
                {
                    address += "\n" + aRestaurantUser.House + " " + aRestaurantUser.FullAddress + aRestaurantUser.Postcode;
                }
            }
            return address;
        }

        private void deliveryChargeButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            if (deliveryChargeButton.Text != "SEND TO\n\nKITCHEN")
            {
                DeliveryChargeForm.DeliveryChargeAmount = 0;
                DeliveryChargeForm aDeliveryChargeForm = new DeliveryChargeForm();
                aDeliveryChargeForm.ShowDialog();

                if (DeliveryChargeForm.DeliveryChargeAmount > 0)
                {
                    aGeneralInformation.DeliveryCharge = DeliveryChargeForm.DeliveryChargeAmount;
                    deliveryChargeButton.Text = aGeneralInformation.DeliveryCharge <= 0
                        ? "Delivery Charge\r\n0"
                        : "Delivery Charge\r\n" + aGeneralInformation.DeliveryCharge.ToString();
                    LoadAmountDetails();
                }

                else if (DeliveryChargeForm.DeliveryChargeAmount == 0)
                {
                    aGeneralInformation.DeliveryCharge = DeliveryChargeForm.DeliveryChargeAmount;
                    deliveryChargeButton.Text = "£0";
                }
            }
            else
            {
                if (!OthersMethod.CheckServerConneciton())
                {
                    return;
                }

                if (orderDetailsflowLayoutPanel1.Controls.Count == 0)
                {
                    MessageBox.Show("No items in the order!", "Save Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    if (aGeneralInformation.OrderId <= 0)
                    {
                        DateTime stDate = DateTime.Now.Date;
                        DateTime endDate = stDate.AddDays(1);
                       
                        aGeneralInformation.OrderType = String.IsNullOrEmpty(aGeneralInformation.OrderType)
                            ? aRestaurantInformation.DefaultOrderType
                            : aGeneralInformation.OrderType;
                        string[] time;
                        if (aGeneralInformation.OrderType == "DEL")
                        {
                            time = aRestaurantInformation.DeliveryTime.Split(' ');
                        }
                        else //if (aGeneralInformation.OrderType == "CLT")
                        {
                            time = aRestaurantInformation.CollectionTime.Split(' ');
                        }

                        aGeneralInformation.DeliveryTime = String.IsNullOrEmpty(aGeneralInformation.DeliveryTime)
                           ? DateTime.Now.AddMinutes(Convert.ToDouble(time[0])).ToString("HH:mm")
                            //   ? DateTime.Now.AddMinutes(aRestaurantInformation.DeliveryTime).ToString("HH:mm")
                            : aGeneralInformation.DeliveryTime;  

                        RestaurantOrder aRestaurantOrder = GetRestaurantOrderForTableOrder();
                        int orderNo = aRestaurantOrderBLL.GetMaxOrderNumber(stDate, endDate);
                        aRestaurantOrder.OrderNo = orderNo;
                        OrderNo = orderNo;

                        aRestaurantOrder.Discount = 0.0;
                        aRestaurantOrder.Coupon = "";
                        aRestaurantOrder.DiscountAmount = aGeneralInformation.DiscountFlat;
                        if (aGeneralInformation.DiscountType != null)
                        {
                            aRestaurantOrder.Coupon = aGeneralInformation.DiscountType;
                            if (aGeneralInformation.DiscountType == "percent")
                            {
                                aRestaurantOrder.Discount = aGeneralInformation.DiscountPercent;
                            }
                            else
                            {
                                aRestaurantOrder.Discount = aGeneralInformation.DiscountFlat;
                            }
                        }

                        int result = aRestaurantOrderBLL.InsertRestaurantOrder(aRestaurantOrder);
                        List<OrderPackage> aOrder_Package = GetOrderPackage(result);
                        var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrder_Package);
                        List<OrderItem> aOrder_Items = GetOrderItem(result, result2);
                        aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrder_Items);

                       GenerateKitchenCopy(result);
                    }
                    else
                    {
                        DeleteNewCancelItem(null, null, aGeneralInformation.OrderId);
                        List<OrderPackage> aOrder_Package = GetOrderPackage(aGeneralInformation.OrderId);
                        var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrder_Package);
                        List<OrderItem> aOrder_Items = GetOrderItem(aGeneralInformation.OrderId, result2);
                        aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrder_Items);

                        RestaurantOrder aRestaurantOrder = GetRestaurantOrderForTableOrder();

                        aRestaurantOrder.Status = "pending";
                        aRestaurantOrder.Id = aGeneralInformation.OrderId;

                        bool result = aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);
                    
                        if (aGeneralInformation.TableId > 0)
                        {
                            RestaurantTable aRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableId(aGeneralInformation.TableId);
                            aRestaurantTable.CurrentStatus = "busy";
                            aRestaurantTableBll.UpdateRestaurantTable(aRestaurantTable);
                        }
                      GenerateKitchenCopy(aRestaurantOrder.Id);
                    }

                    ClearAllOrderInformation();
                    ClearTableOrderDetails();
                }
                catch (Exception ex){
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }
            }
        }

        private void ClearTableOrderDetails()
        {
            LoadDefaultInformation();
        }

        private void LoadDefaultInformation()
        {
            personButton.Text = "P";
            personButton.Visible = false;
            tableButton.Text = "TABLE";
            finalizeButton.Visible = false;
            billButton.Visible = false;
            //btnOrderSaveOnly.Visible = false;
            deliveryChargeButton.Text = "Delivery Charge\r\n0";
            discountButton.Text = "Disc\r\n0.00";
            aGeneralInformation = new GeneralInformation(); 
            if (!this.IsHandleCreated)
                this.CreateControl();

            this.Invoke((MethodInvoker)delegate
            {
                if (GlobalSetting.RestaurantInformation.RestaurantType == "restaurant")
                {
                    tableButton.Visible = true;
                }
                else
                {
                    tableButton.Visible = false;
                   onlineOrderButton.Location = new Point(panel4.Location.X+ panel4.Width+25,-1);
                }
                personButton.Visible = false;
                
            });             
                   
            if (aRestaurantInformation.DefaultOrderType.ToUpper() == "CLT" || aRestaurantInformation.DefaultOrderType == "WAIT")
            {
                collectionButton.BackColor = Color.Black;
                collectionButton.ForeColor = Color.WhiteSmoke;
                deliveryButton.BackColor = Color.WhiteSmoke;
                deliveryButton.ForeColor = Color.Black;
                timeButton.Text = "Time";
                ChangeButtonLocation(false);
            }
            else if (aRestaurantInformation.DefaultOrderType.ToUpper() == "DEL")
            {
                collectionButton.BackColor = Color.WhiteSmoke;
                deliveryButton.BackColor = Color.Black;
                deliveryButton.ForeColor = Color.WhiteSmoke;
                collectionButton.ForeColor = Color.Black;
                ChangeButtonLocation(true);
                aGeneralInformation.OrderType = "DEL";
                timeButton.Text = "Time";
            }
                                                                                  
            deliveryButton.Visible = true;
            collectionButton.Visible = true;
            timeButton.Visible = true;
            deliveryButton.Text = "DEL";
            deliveryButton.Visible = true;
            customTotalTextBox.Text = "Custom Total";
            commentTextBox.Text = "Comment";

            if (deliveryButton.BackColor == Color.Black)
            {
                aGeneralInformation.OrderType = "DEL";
                aGeneralInformation.DeliveryCharge = aRestaurantInformation.DeliveryCharge;
                deliveryChargeButton.Text = "Delivery Charge\r\n" + aRestaurantInformation.DeliveryCharge;
            }
            else
            {
                aGeneralInformation.OrderType = "CLT";
                aGeneralInformation.DeliveryCharge = 0;
                deliveryChargeButton.Text = "Delivery Charge\r\n0";
            }
            timeButton.Text = "Time";
            aGeneralInformation.PrintOrderType = "";           

            LoadAmountDetails();
        }

        private RestaurantOrder GetRestaurantOrderForTableOrder()
        {
            double totalCost = 0;
          
            RestaurantOrder aRestaurantOrder = new RestaurantOrder();
            aRestaurantOrder.Comment = commentTextBox.Text == "Comment" ? "" : commentTextBox.Text;
            aRestaurantOrder.DeliveryCost = aGeneralInformation.DeliveryCharge;
            aRestaurantOrder.DeliveryTime = Convert.ToDateTime(aGeneralInformation.DeliveryTime); 
            aRestaurantOrder.OrderTime = DateTime.Now;

            aRestaurantOrder.OrderType = "IN";
            aRestaurantOrder.PaymentMethod = aGeneralInformation.PaymentMethod;
            aRestaurantOrder.ServiceCharge = aGeneralInformation.ServiceCharge;

            if (aGeneralInformation.DiscountType != null)
            {
                aRestaurantOrder.Coupon = aGeneralInformation.DiscountType;
                if (aGeneralInformation.DiscountType == "percent")
                {
                    aRestaurantOrder.Discount = aGeneralInformation.DiscountPercent;
                }
                else
                {
                    aRestaurantOrder.Discount = aGeneralInformation.DiscountFlat;
                }
            }
            if (!double.TryParse(customTotalTextBox.Text, out totalCost))
            {
                aRestaurantOrder.TotalCost = GetTotalAmountDetails() + aRestaurantOrder.DeliveryCost +
                                             aRestaurantOrder.ServiceCharge + aRestaurantOrder.CardFee -
                                             aGeneralInformation.DiscountFlat;

                aRestaurantOrder.TotalCost = GlobalVars.numberRound(aRestaurantOrder.TotalCost, 2);
            }
            else
            {
                aRestaurantOrder.TotalCost = totalCost;
            }

            aRestaurantOrder.UserId = 0;
            aRestaurantOrder.CustomerId = aGeneralInformation.CustomerId;
            aRestaurantOrder.OrderTable = aGeneralInformation.TableId;
            aRestaurantOrder.Person = aGeneralInformation.Person;
                                                                                         
            aRestaurantOrder.Status = "pending";
            aRestaurantOrder.RestaurantId = aRestaurantInformation.Id;
            aRestaurantOrder.UpdateTime = DateTime.Now.Date;
            aRestaurantOrder.OrderNo = new RestaurantOrderBLL().GetRestaurantOrderByOrderId(aGeneralInformation.OrderId).OrderNo;

            if (aRestaurantOrder.CashAmount == 0 && aRestaurantOrder.CardAmount == 0)
            {
                aRestaurantOrder.CashAmount = aRestaurantOrder.TotalCost;
                aRestaurantOrder.PaymentMethod = "Cash";
            }
          return aRestaurantOrder;
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            LoadSettingsForm("settings");
        }

        private void LoadSettingsForm(string openpage)
        {
            AllSettingsForm.Status = "";
            RestaurantOrdersReport.OrderId = 0;
            AllSettingsForm aAllSettingsForm = new AllSettingsForm(openpage, this, null);
            aAllSettingsForm.ShowDialog();
            if (RestaurantOrdersReport.OrderId > 0)
            {
                orderLoadStatus = true;
                ClearTableOrderDetails();
                ClearAllOrderInformation();
                LoadAllSaveOrder((int)RestaurantOrdersReport.OrderId, "reorder");
            }
            else if (AllSettingsForm.Status == "logout")
            {
                if (aAdForm != null)
                {
                     aAdForm.Close();
                }
                Form tempForm = FormManage.aFormManage.Pop();
                tempForm.Show();
                if (aTimer!=null)
                {
                    aTimer.Elapsed -= OnlineOrderTimer_Tick;
                }
                this.Close();
            }
            else if (AllSettingsForm.Status == "reload")
            {
                Form tempForm = FormManage.aFormManage.Pop();
                tempForm.Show();
                this.Close();
            }
        }

        public void callToTpos(string number)
        {
            try
            {
                //  Refresh();
                if (number.Length > 0)
                {
                    number = number.Trim();
                }

                if (number != null && number != "")
                {
                    File.AppendAllText("Config/call.txt", DateTime.Now + " " + number + "\n\n");
                    if (customerTextBox != null && number == customerTextBox.Text) return;
                    //MessageBox.Show(number);
                    string phoneNumber = number.ToString();
                    RestaurantUsers aRestaurantUser = FindRestaurantUser(number);
                    if (aRestaurantUser != null && aRestaurantUser.Id > 0)
                    {
                        aGeneralInformation.CustomerId = aRestaurantUser.Id;
                        LoadRecetAddedItems();
                    }
                    if ((customerTextBox != null && (customerTextBox.Text == "Search Customer" || customerTextBox.Text == "")) && IsCartEmpty())
                    {
                        isCustomerTextChanged = false;
                        customerTextBox.Text = number;
                        if ((aRestaurantUser == null || aRestaurantUser.Id <= 0) && number.Count() > 0)
                        {
                            if (!Application.OpenForms.OfType<TomaFoodRestaurant.OtherForm.CustomerEntryForm>().Any())

                            {
                                CustomerEntryForm.OrderType = "";
                                RestaurantUsers aUser = new RestaurantUsers();
                                CustomerEntryForm aCustomerEntryForm = new CustomerEntryForm(phoneNumber, aUser);
                                aCustomerEntryForm.ShowDialog();
                                if (CustomerEntryForm.OrderType == "del")
                                {
                                    SelectDeliveryButton();
                                }
                                if (CustomerEntryForm.OrderType == "clt")
                                {
                                    SelectCollectionButton();
                                }
                                isCustomerTextChanged = true;
                                CustomerLoadWhenAddNewCustomer(CustomerEntryForm.aRestaurantUser);
                            }
                        }
                        else
                        {
                            isCustomerTextChanged = true;
                            CallerDetailsShow.OrderType = "";
                            CallerDetailsShow aCallerDetailsShow = new CallerDetailsShow(aRestaurantUser);
                            aCallerDetailsShow.ShowDialog();

                            if (CallerDetailsShow.OrderType == "close") return;
                            if (CallerDetailsShow.OrderType == "del")
                            {
                                SelectDeliveryButton();
                            }

                            if (CallerDetailsShow.OrderType == "clt")
                            {
                                SelectCollectionButton();
                            }
                            if (CallerDetailsShow.OrderType != "edit")
                                CustomerLoadWhenAddNewCustomer(aRestaurantUser);
                            if (CallerDetailsShow.OrderType == "edit")
                            {
                                EditCustomer(aRestaurantUser);
                            }
                        }
                    }
                    else
                    {
                        seconePhnPanel.Visible = true;
                        customerLoadButton.Text = phoneNumber + "\r\nis calling...";
                        if (aRestaurantUser != null && aRestaurantUser.Id > 0)
                        {
                            customerLoadLabel.Text = aRestaurantUser.Firstname + " " + aRestaurantUser.Lastname;
                            if (customerLoadLabel.Text.Length <= 0)
                            {
                                customerLoadLabel.Text = phoneNumber;
                            }
                            customerPhnLabel.Text = phoneNumber;
                            customerLoadButton.Text = customerLoadLabel.Text + "\r\nis calling...";
                        }
                        else if ((aRestaurantUser == null || aRestaurantUser.Id <= 0) && number.Count() > 0)
                        {
                            customerLoadLabel.Text = phoneNumber;
                            customerPhnLabel.Text = phoneNumber;
                            customerLoadButton.Text = customerLoadLabel.Text + "\r\nis calling...";
                        }
                    }                    
                }

                LoadAmountDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                new ErrorReportBLL().SendErrorReport(DateTime.Now + " " + ex.GetBaseException());
                this.Activate();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentTimeLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private bool IsCartEmpty()
        {
            if (orderDetailsflowLayoutPanel1.Controls.Count <= 0) return true;
            return false;
        }

        private string GetExactNumber(string szCallerID)
        { 
            string mystr = Regex.Replace(szCallerID, @"[^0-9]", "");            
            string result = string.Concat(Enumerable.Reverse(mystr));
            string str = "";
            if (result.Count() >= 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    str += result[i];
                }
                str += "0";
            }
            return string.Concat(Enumerable.Reverse(str));
        }

        private void newCustomerButton_Click(object sender, EventArgs e)
        {
            try
            {
                aOthersMethod.KeyBoardClose();
                aOthersMethod.NumberPadClose(); 
                isCustomerTextChanged = false;
                RestaurantUsers aUser = new RestaurantUsers();
                string phoneNumber = customerTextBox.Text.Trim();
                if (phoneNumber.Length <= 0)
                {
                    phoneNumber = customerDetailsLabel.Text;
                }

                CustomerEntryForm.OrderType = "";
                CustomerEntryForm.aRestaurantUser = new RestaurantUsers();
                CustomerEntryForm aCustomerEntryForm = new CustomerEntryForm(phoneNumber, aUser);
                aCustomerEntryForm.ShowDialog();

                if (CustomerEntryForm.OrderType == "del")
                {
                    SelectDeliveryButton();
                }

                if (CustomerEntryForm.OrderType == "clt")
                {
                    SelectCollectionButton();
                }
                if (CustomerEntryForm.aRestaurantUser.Id > 0)
                    { 
                        string Mobilephone = CustomerEntryForm.aRestaurantUser.Mobilephone != ""
                            ? CustomerEntryForm.aRestaurantUser.Mobilephone
                            : CustomerEntryForm.aRestaurantUser.Homephone;
                        customerTextBox.Text = Mobilephone;
                    }

                isCustomerTextChanged = true;
                CustomerLoadWhenAddNewCustomer(CustomerEntryForm.aRestaurantUser);
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                this.Activate();
            }
        }

        private void SelectCollectionButton()
        {
            ChangeButtonLocation(false);
            collectionButton.BackColor = Color.Black;
            deliveryButton.BackColor = Color.WhiteSmoke;
            collectionButton.ForeColor = Color.WhiteSmoke;
            deliveryButton.ForeColor = Color.Black;
            aGeneralInformation.DeliveryCharge = 0;
            deliveryChargeButton.Text = "Delivery Charge\r\n0";
            aGeneralInformation.OrderType = "CLT";
            LoadAmountDetails();
        }

        private void SelectDeliveryButton()
        {
            if (deliveryButton.Text != "RES")
            {
                collectionButton.BackColor = Color.WhiteSmoke;
                deliveryButton.BackColor = Color.Black;
                deliveryButton.ForeColor = Color.WhiteSmoke;
                collectionButton.ForeColor = Color.Black;
                double totalAmount = GetTotalAmountDetails();
                aGeneralInformation.DeliveryCharge = totalAmount >= aRestaurantInformation.MinOrder ? 0   : aRestaurantInformation.DeliveryCharge;
                deliveryChargeButton.Text = aGeneralInformation.DeliveryCharge <= 0
                    ? "Delivery Charge\r\n0"
                    : "Delivery Charge\r\n" + aGeneralInformation.DeliveryCharge.ToString();
                aGeneralInformation.OrderType = "DEL";
                timeButton.Text = "Time";
                ChangeButtonLocation(true);
                LoadAmountDetails();
            }
            else
            {
                deliveryButton.BackColor = Color.Black;
                deliveryButton.ForeColor = Color.WhiteSmoke;

                ChangeButtonLocation(true);
            }
        }

        private void CustomerLoadWhenAddNewCustomer(RestaurantUsers restaurantUsers)
        {
            RestaurantUsers aRestaurantUser = restaurantUsers;
            if (aRestaurantUser != null && aRestaurantUser.Id > 0)
            {
                string cell = aRestaurantUser.Mobilephone != ""
                    ? aRestaurantUser.Mobilephone
                    : aRestaurantUser.Homephone;
                string customerAddress = GetCustomerDetails(aRestaurantUser);

                customerDetailsLabel.Text = customerAddress;

                customerEditButton.Visible = true;
                phoneNumberDeleteButton.Visible = true;
                customerTextBox.Text = cell;
                customerTextBox.Visible = false;
                customerTextBox.SelectionStart = customerTextBox.Text.Length;
                aGeneralInformation.CustomerId = aRestaurantUser.Id;
                if (aRestaurantUser.Postcode.Length > 0)
                {
                    LoadDeliveryCharge(aRestaurantUser);
                }
            }
            else
            { 
               customerDetailsLabel.Text = "";
                customerEditButton.Visible = false;
                if (customerTextBox.Text == "Search Customer")
                {
                    phoneNumberDeleteButton.Visible = false;
                }
                else
                {
                    phoneNumberDeleteButton.Visible = true;
                }
                customerTextBox.Visible = true; 
                aGeneralInformation.CustomerId = 0;
            }
        }

        private void LoadDeliveryCharge(RestaurantUsers aRestaurantUser)
        {
            double deliveryCharge = GetDeliveryCharge(aRestaurantUser);

            if (deliveryButton.Text != "RES" && deliveryButton.BackColor == Color.Black)
            {
                double totalAmount = GetTotalAmountDetails();
                aGeneralInformation.DeliveryCharge = totalAmount >= aRestaurantInformation.MinOrder ? 0 : deliveryCharge;
                deliveryChargeButton.Text = aGeneralInformation.DeliveryCharge <= 0
                    ? "Delivery Charge\r\n0"
                    : "Delivery Charge\r\n" + aGeneralInformation.DeliveryCharge.ToString();             
            }
            LoadAmountDetails();
        }

        private void ShowCustomerButton_Click(object sender1212, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            CustomerShowButton aCustom = sender1212 as CustomerShowButton;
            restaurantUsers = aCustom.RestaurantUsers;
            if (restaurantUsers != null && restaurantUsers.Id > 0)
            {
                List<CustomerRecentItemMD> aCustomerRecentItemMds = new CustomerRecentItemBLL().GetCustomerRecentItemMd(restaurantUsers.Id);
                if (aCustomerRecentItemMds.Count > 0)
                {
                    customerRecentItemsButton.Visible = true;
                }
                string cell = restaurantUsers.Mobilephone != ""
                    ? restaurantUsers.Mobilephone
                    : restaurantUsers.Homephone;
                string customerAddress = GetCustomerDetails(restaurantUsers);
                customerDetailsLabel.Text = customerAddress;
                customerEditButton.Visible = true;
                phoneNumberDeleteButton.Visible = true;
                customerTextBox.Text = cell;
                customerTextBox.Visible = false;
                customerTextBox.SelectionStart = customerTextBox.Text.Length;
                aGeneralInformation.CustomerId = restaurantUsers.Id;
                LoadDeliveryCharge(restaurantUsers);
            }
            else
            {
                customerDetailsLabel.Text = "";
                customerEditButton.Visible = false;
                if (customerTextBox.Text == "Search Customer")
                {
                    phoneNumberDeleteButton.Visible = false;
                }
                else
                {
                    phoneNumberDeleteButton.Visible = true;
                }
                customerTextBox.Visible = true;
                aGeneralInformation.CustomerId = 0;
            }
            customerShowFlowLayoutPanel.Visible = false;
        }

        private double GetDeliveryCharge(RestaurantUsers aRestaurantUser)
        {
            double delCharge = GlobalSetting.RestaurantInformation.DeliveryCharge;
      
            if (Properties.Settings.Default.enableDelcharge)
            {
                string result = string.Empty;
                try
                {
                    string postData = "postcode=" + aRestaurantUser.Postcode;
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                     
                    Uri target = new Uri(urls.AcceptUrl + "restaurantcontrol/request/crud/get_delivery_charge/" + GlobalSetting.RestaurantInformation.Id);
                    WebRequest request = WebRequest.Create(target);

                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = byteArray.Length;

                    using (var dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                            {
                                result = readStream.ReadToEnd();
                            }
                        }
                    }
                    if (result != "" && !(result == "NOT_FOUND" || result == "ERROR"))
                        delCharge = Convert.ToDouble(result);
                }
                catch (Exception ex)
                {
                    return delCharge;
                }

            }
            else
            {
                if (string.IsNullOrEmpty(aRestaurantUser.Postcode))
                {
                    return GlobalSetting.RestaurantInformation.DeliveryCharge;
                }
                try
                {
                    CoverageAreaBLL aCoverageAreaBll = new CoverageAreaBLL();
                    DistanceBLL aDistanceBll = new DistanceBLL();
                    DeliveryChargeBLL aDeliveryChargeBll = new DeliveryChargeBLL();
                    string postCode = aRestaurantUser.Postcode.Replace(" ", "").ToUpper();
                    string _shortpostCode = postCode;
                    if (postCode.Length > 3)
                    {
                        _shortpostCode = postCode.Substring(0, postCode.Length - 3).ToUpper();
                    }

                    AreaCoverage areaCoverage = aCoverageAreaBll.GetCoverageAreaByPostcode(postCode, GlobalSetting.RestaurantInformation.Id);
                    if (!string.IsNullOrEmpty(areaCoverage.Postcode))
                    {
                        delCharge = areaCoverage.DeliveryCharge;
                    }
                    string restaurantPostCode = GlobalSetting.RestaurantInformation.Postcode.Replace(" ", "").ToUpper();
                    bool haveDeliveryCharge = aDeliveryChargeBll.CheckDeliveryCharge(GlobalSetting.RestaurantInformation.Id);

                    if (haveDeliveryCharge)
                    {
                        double distnace = 0;
                        Distance aDistance = aDistanceBll.GetDistanceByPostcode(restaurantPostCode, postCode);
                        if (aDistance != null && aDistance.Id > 0)
                        {
                            distnace = aDistance.distance;
                        }

                        if (distnace > 0)
                        {
                            DelvaryCharge aDelivaryCharge = aDeliveryChargeBll.GetDeliveryChargeByDistance(distnace,
                                aRestaurantInformation.Id);
                            if (aDelivaryCharge != null && aDelivaryCharge.Id > 0)
                            {
                                delCharge = aDelivaryCharge.amount;
                            }
                        }
                    }

                    AreaCoverage anotherCoverage = aCoverageAreaBll.GetCoverageAreaByPostcode(_shortpostCode,
                        aRestaurantInformation.Id);

                    if (!string.IsNullOrEmpty(anotherCoverage.Postcode))
                    {
                        if (anotherCoverage.DeliveryCharge > 0)
                        {
                            delCharge = anotherCoverage.DeliveryCharge;
                            return delCharge;
                        }
                        return aRestaurantInformation.DeliveryCharge;
                    }
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.GetBaseException().ToString());
                    delCharge = aRestaurantInformation.DeliveryCharge;
                }
            }

            return delCharge;
        }

        private void customerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (customerTextBox.Text.Trim() != "Search Customer" && customerTextBox.Text.Trim() != "" &&
                customerTextBox.Text.Trim().Length > 2 && isCustomerTextChanged)
            {
                phoneNumberDeleteButton.Visible = true;
                string query = customerTextBox.Text.Trim();

                CustomerBLL aCustomerBll = new CustomerBLL();
                List<RestaurantUsers> showRestaurantUsers = aCustomerBll.GetRestaurantAllCustomerForShow(query);
                if (showRestaurantUsers.Any())
                {
                    RemoveCustomerControl(customerShowFlowLayoutPanel);
                    if (showRestaurantUsers.Count() == 1 && query.Length >= 11)
                    {
                        CustomerLoadWhenAddNewCustomer(showRestaurantUsers.FirstOrDefault());
                        customerShowFlowLayoutPanel.Visible = false;
                        aGeneralInformation.CustomerId = showRestaurantUsers.FirstOrDefault().Id;
                        if (aGeneralInformation.CustomerId > 0)
                        {
                            LoadRecetAddedItems();
                        }
                        return;
                    }

                    foreach (RestaurantUsers user in showRestaurantUsers)
                    {
                        CustomerShowButton aCustomerShowButton = new CustomerShowButton();

                        if (!string.IsNullOrEmpty(user.Name.Trim()))
                        {
                            aCustomerShowButton.Text += user.Name + "";
                        }
                        if (!string.IsNullOrEmpty(user.Mobilephone))
                        {
                            aCustomerShowButton.Text += ", " + user.Mobilephone + "";
                        }
                        if (!string.IsNullOrEmpty(user.Homephone))
                        {
                            aCustomerShowButton.Text += ", " + user.Homephone + "";
                        }
                        aCustomerShowButton.Text += "\r\n";
                        if (!string.IsNullOrEmpty(user.Address))
                        {
                            aCustomerShowButton.Text += (user.House + " " + user.Address);
                        }
                        aCustomerShowButton.RestaurantUsers = user;
                        aCustomerShowButton.Width = 140;
                        aCustomerShowButton.Height = 65;
                        aCustomerShowButton.BackColor = Color.LightSeaGreen;
                        aCustomerShowButton.ForeColor = Color.White;
                        aCustomerShowButton.Click += new EventHandler(ShowCustomerButton_Click);
                        customerShowFlowLayoutPanel.Controls.Add(aCustomerShowButton);
                    }
                    customerShowFlowLayoutPanel.Visible = true;
                    customerShowFlowLayoutPanel.BringToFront();

                    if (aGeneralInformation.CustomerId > 0)
                    {
                        LoadRecetAddedItems();
                    }
                }
                else
                {
                    customerShowFlowLayoutPanel.Visible = false;
                }
            }
            else
            {
                if (customerTextBox.Text.Trim().Length > 0 && customerTextBox.Text.Trim() != "Search Customer")
                {
                    phoneNumberDeleteButton.Visible = true;
                }
                else
                {
                    phoneNumberDeleteButton.Visible = false;
                }
                customerShowFlowLayoutPanel.Visible = false;
            }

            //if (!aGeneralInformation.CustomerId > 0)
            //{

            //    recentItemsFlowLayoutPanel.Controls.Clear();
            //}
        }

        private void RemoveCustomerControl(FlowLayoutPanel rightCatItemflowLayoutPanel11)
        {
            List<Control> listControls = new List<Control>();

            foreach (Control control in rightCatItemflowLayoutPanel11.Controls)
            {
                listControls.Add(control);
            }
            foreach (Control control in listControls)
            {
                rightCatItemflowLayoutPanel11.Controls.Remove(control);
                control.Dispose();
            }
        }

        private RestaurantUsers FindRestaurantUser(string phoneNumber)
        {
            //File.AppendAllText("Config/log.txt", " \n\n number :: " + phoneNumber + "\n\n");

            CustomerBLL aCustomerBll = new CustomerBLL();

            return aCustomerBll.GetCustomerByPhone(phoneNumber);
        }

        private void customerTextBox_Leave(object sender, EventArgs e)
        {
            if (customerTextBox.Text == "")
            {
                customerTextBox.Text = "Search Customer";
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            if (GlobalSetting.RestaurantInformation.ConfirmPayment > 0 && collectionButton.BackColor == Color.Black && timeButton.Text.ToLower() == "wait")
            {              
                PaidAmountWithPrint(true, true);
            }
            else
            {
                PaidAmountWithPrint(false, true);
            }
        }

        private void phoneNumberDeleteButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            recentItemsFlowLayoutPanel.Visible = false;
            customerTextBox.Text = "Search Customer";
            customerDetailsLabel.Text = "";
            customerEditButton.Visible = false;
            phoneNumberDeleteButton.Visible = false;
            customerTextBox.Visible = true;
            customerRecentItemsButton.Visible = false;
            aGeneralInformation.CustomerId = 0;

            if (deliveryButton.Text != "RES" && deliveryButton.BackColor == Color.Black)
            {
                double totalAmount = GetTotalAmountDetails();
                aGeneralInformation.DeliveryCharge = totalAmount >= aRestaurantInformation.MinOrder
                    ? 0
                    : aRestaurantInformation.DeliveryCharge;
                deliveryChargeButton.Text = aGeneralInformation.DeliveryCharge <= 0
                    ? "Delivery Charge\r\n0"
                    : "Delivery Charge\r\n" + aGeneralInformation.DeliveryCharge.ToString();
            }
        }

        private void customerEditButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            isCustomerTextChanged = false;

            string phoneNumber = customerTextBox.Text.Trim();
            if (phoneNumber.Length <= 0)
            {
                phoneNumber = customerDetailsLabel.Text;
            }

            CustomerBLL aCustomerBll = new CustomerBLL();
            RestaurantUsers aRestaurantUser = FindRestaurantUser(phoneNumber);
            if (aRestaurantUser != null && aRestaurantUser.Id <= 0 && aGeneralInformation.CustomerId > 0)
            {
                aRestaurantUser = aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
            }
            EditCustomer(aRestaurantUser);
            aGeneralInformation.CustomerId = aRestaurantUser.Id;
        }

        private void EditCustomer(RestaurantUsers aRestaurantUser)
        {
            CustomerEntryForm.OrderType = "";
            CustomerEntryForm aForm = new CustomerEntryForm("", aRestaurantUser);
            aForm.ShowDialog();

            if (CustomerEntryForm.OrderType.ToLower() == "del")
            {
                SelectDeliveryButton();
            }

            if (CustomerEntryForm.OrderType.ToLower() == "clt")
            {
                SelectCollectionButton();
            }

            CustomerLoadWhenAddNewCustomer(CustomerEntryForm.aRestaurantUser);
            isCustomerTextChanged = true;
        }

        private void customerLoadButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            isCustomerTextChanged = false;
            customerTextBox.Text = customerPhnLabel.Text;
            seconePhnPanel.Visible = false;

            string phoneNumber = customerTextBox.Text.Trim();
            RestaurantUsers aRestaurantUser = FindRestaurantUser(phoneNumber);

            if ((aRestaurantUser == null || aRestaurantUser.Id <= 0) && phoneNumber.Count() > 0)
            {
                if (Application.OpenForms.OfType<CustomerEntryForm>().Count() <= 0)
                {
                    RestaurantUsers aUser = new RestaurantUsers();
                    CustomerEntryForm aCustomerEntryForm = new CustomerEntryForm(phoneNumber, aUser);
                    aCustomerEntryForm.ShowDialog();

                    isCustomerTextChanged = true;
                    CustomerLoadWhenAddNewCustomer(CustomerEntryForm.aRestaurantUser);
                }
            }
            else if (aRestaurantUser != null && aRestaurantUser.Id > 0)
            {

                CallerDetailsShow.OrderType = "";
                CallerDetailsShow aCallerDetailsShow = new CallerDetailsShow(aRestaurantUser);
                aCallerDetailsShow.ShowDialog();

                if (CallerDetailsShow.OrderType == "close") return;
                if (CallerDetailsShow.OrderType == "del")
                {
                    SelectDeliveryButton();
                }

                if (CallerDetailsShow.OrderType == "clt")
                {
                    SelectCollectionButton();
                }
                if (CallerDetailsShow.OrderType != "edit")
                    CustomerLoadWhenAddNewCustomer(aRestaurantUser);
                if (CallerDetailsShow.OrderType == "edit")
                {
                    EditCustomer(aRestaurantUser);
                }

                if (CallerDetailsShow.OrderType != "edit")
                {
                    DateTime toDate = DateTime.Now.Date;
                    DateTime fromDate = toDate.AddDays(1);
                    RestaurantOrder saveOrder = aRestaurantOrderBLL.GetRestaurantOrderByCustomerAndDate(toDate, fromDate,
                        aRestaurantUser.Id);
                    if (saveOrder != null && saveOrder.Id > 0)
                    {
                        ShowOrderDetails.OrderId = 0;
                        ShowOrderDetails aOrderDetails = new ShowOrderDetails(saveOrder);
                        aOrderDetails.ShowDialog();
                        if (ShowOrderDetails.OrderId > 0)
                        {
                            LoadAllSaveOrder((int)ShowOrderDetails.OrderId, "reorder");
                        }
                    }
                }
                isCustomerTextChanged = true;
            }
        }

        private void tableButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            }

            /*RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            List<RestaurantTable> aRestaurantTable = aRestaurantTableBll.GetRestaurantTable();
            OthersMethod.RefreshAllEmptyTable(aRestaurantTable);      */     

            orderLoadStatus = true;
            TableLoadResponsive.Status = "";
            TableLoadResponsive.TableNumber = "";
            TableLoadResponsive.Person = 0;
            TableLoadResponsive.TableId = 0;
            TableLoadResponsive aForm = new TableLoadResponsive();
            aForm.ShowDialog();
            if (TableLoadResponsive.Status != "ok")
            {
                aGeneralInformation = new GeneralInformation();
                ClearTableOrderDetails();
                ClearAllOrderInformation();
            }
            else
            {
                loadTableOrder(TableLoadResponsive.TableId);
            }
            ChangeButtonLocation(false);
            AddServiceChargeIntoLabel();
        }

        public void loadTableOrder(int tableId)
        {
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            aGeneralInformation = new GeneralInformation();
            resetCart();
            RestaurantTable aRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableId(tableId);
            LoadTableInformation(aRestaurantTable);
            LoadAllSaveOrder(aGeneralInformation.TableId, "order");
        }

        private void LoadTableInformation(RestaurantTable aRestaurantTable)
        {
            aGeneralInformation.TableNumber = aRestaurantTable.Name;
            aGeneralInformation.Person = aRestaurantTable.Person;
            aGeneralInformation.TableId = aRestaurantTable.Id;

            if (aGeneralInformation.TableNumber != null && aGeneralInformation.TableNumber.Length > 0)
            {
                if (aGeneralInformation.TableNumber != "0")
                {
                    tableButton.Text = "T " + aGeneralInformation.TableNumber;
                    personButton.Text = "P " + aGeneralInformation.Person;
                    personButton.Visible = true;
                    //   deliveryChargeButton.Text = "SEND TO\n\nKITCHEN";
                    //  deliveryChargeButton.BackColor = Color.LightSeaGreen;  
                    finalizeButton.Visible = true;
                    billButton.Visible = true;
                    deliveryButton.Text = "RES";
                    deliveryButton.Visible = false;
                    collectionButton.Visible = false;
                    timeButton.Visible = false;

                    //btnOrderSaveOnly.Visible = true;
                    aGeneralInformation.OrderType = "IN";
                    ChangeButtonLocation(false);
                }
                else
                {
                    tableButton.Text = "TABLE";

                    personButton.Visible = false;
                    deliveryChargeButton.Text = "D/Ch0";
                    deliveryChargeButton.BackColor = Color.FromArgb(151, 106, 55); 
                   
                    finalizeButton.Visible = false;
                    deliveryButton.Text = "DEL";
                    deliveryButton.Visible = true;
                    collectionButton.Visible = true;
                    timeButton.Visible = true;
                    //btnOrderSaveOnly.Visible = false;
                    aGeneralInformation.OrderType = "Collect";
                    billButton.Visible = false;
                    collectionButton.BackColor = Color.Black;
                    deliveryButton.BackColor = Color.WhiteSmoke;
                    collectionButton.ForeColor = Color.WhiteSmoke;
                    deliveryButton.ForeColor = Color.Black;
                    ChangeButtonLocation(false);
                }
            }
        }

        private void LoadAllLocalSaveOrder(int tableId, string type)
        {
            try
            {
                OthersMethod aOthersMethod = new OthersMethod();
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
                orderDetailsflowLayoutPanel1.Controls.Clear();
                RestaurantOrder aRestaurantOrder = new RestaurantOrder();
                RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();

                if (!orderLoadStatus) return;

                if (type == "order")
                {
                    aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrder(tableId, "pending");
                }
                else if (type == "reorder")
                {
                    aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(tableId);
                    ///tableId means order id
                    if (aRestaurantOrder.OrderTable > 0)
                    {

                        RestaurantTable aRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableId(aRestaurantOrder.OrderTable);
                        if (aRestaurantTable.CurrentStatus == "busy")
                        {
                            MessageBox.Show("Selected table is now busy", "Table not available", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                        LoadTableInformation(aRestaurantTable);
                    }
                    else
                    {
                        tableButton.Text = "TABLE";
                        //  personButton.Text = "P " + aGeneralInformation.Person;
                        personButton.Visible = false;
                        deliveryChargeButton.Text = "Delivery Charge\r\n" + aRestaurantOrder.DeliveryCost.ToString("F02");
                        discountButton.Text = "Disc\r\n" + aRestaurantOrder.Discount.ToString("F02");
                        finalizeButton.Visible = false;
                        deliveryButton.Text = "DEL";
                        deliveryButton.Visible = true;
                        collectionButton.Visible = true;
                        timeButton.Visible = true;
                        if (aRestaurantOrder.OnlineOrder > 0)
                            timeButton.Text = "ASAP";

                        if (Convert.ToDateTime(aRestaurantOrder.DeliveryTime.ToString()).ToLongTimeString() != "12:00 AM")
                        {
                            timeButton.Text = aOthersMethod.IsTimeFormatValid(aRestaurantOrder.DeliveryTime.ToString());
                        }

                        aGeneralInformation = new GeneralInformation();
                        aGeneralInformation.OrderType = aRestaurantOrder.OrderType;
                        totalAmountLabel.Text = aRestaurantOrder.TotalCost.ToString();
                    }

                    //List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderForOnline(1, "pending");
                    //if (restaurantOrder.Count == 0)
                    //{ 
                    //    if (!this.IsHandleCreated && !this.IsDisposed) return;

                    //    this.Invoke((MethodInvoker)delegate
                    //    {
                    //        onlineOrderButton.Visible = false;
                    //        grapTimer.Stop();
                    //    });
                    //}
                }

                aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
                aRecipePackageMdList = new List<RecipePackageMD>();
                aPackageItemMdList = new List<PackageItem>();
                aRecipeOptionMdList = new List<RecipeOptionMD>();
                aRecipeMultipleMdList = new List<RecipeMultipleMD>();
                aMultipleItemMdList = new List<MultipleItemMD>();
                if (aRestaurantOrder.Id > 0)
                {
                    aGeneralInformation.OrderId = aRestaurantOrder.Id;
                    aGeneralInformation.CustomerId = aRestaurantOrder.CustomerId;
                    aGeneralInformation.DiscountFlat = aRestaurantOrder.Discount;
                    aGeneralInformation.DeliveryCharge = aRestaurantOrder.DeliveryCost;
                    aGeneralInformation.CardFee = aRestaurantOrder.CardFee;
                    aGeneralInformation.DeliveryTime = aRestaurantOrder.DeliveryTime.ToString();
                    aGeneralInformation.Person = aRestaurantOrder.Person;
                    aGeneralInformation.TableId = aRestaurantOrder.OrderTable;
                    aGeneralInformation.OrderType = aRestaurantOrder.OrderType;
                    aGeneralInformation.ServiceCharge = aRestaurantOrder.ServiceCharge;

                    List<OrderItem> aOrderItems = aRestaurantOrderBLL.GetRestaurantOrderRecipeItems(aRestaurantOrder.Id);
                    List<OrderPackage> aPackageItems = aRestaurantOrderBLL.GetRestaurantOrderPackage(aRestaurantOrder.Id);

                    int optionIndex = 0;
                    foreach (OrderItem item in aOrderItems)
                    {
                        try
                        {
                            if (item.PackageId <= 0 && String.IsNullOrEmpty(item.MultipleMenu))
                            {
                                optionIndex++;
                                ReceipeMenuItemButton aReceipeMenuItemButton =
                                    aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);
                                ReceipeCategoryButton aReceipeCategoryButton =
                                    aRestaurantMenuBll.GetCategoryByCategoryId(aReceipeMenuItemButton.CategoryId);
                                OrderItemDetailsMD aOrderItemDetails = new OrderItemDetailsMD();
                                aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
                                aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
                                aOrderItemDetails.ItemName = item.Name;
                                aOrderItemDetails.ItemFullName = aReceipeMenuItemButton.ShortDescrip;
                                aOrderItemDetails.OptionsIndex = optionIndex;
                                aOrderItemDetails.KitchenSection = aReceipeMenuItemButton.KitchenSection;
                                aOrderItemDetails.Price = item.Price / item.Quantity;
                                aOrderItemDetails.Qty = item.Quantity;
                                aOrderItemDetails.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                                aOrderItemDetails.SortOrder = aReceipeMenuItemButton.SortOrder;
                                aOrderItemDetails.CatSortOrder = aReceipeCategoryButton.SortOrder;
                                aOrderItemDetails.TableNumber = aGeneralInformation.TableId;
                                LoadSaveOption(item, aOrderItemDetails); 

                                aOrderItemDetailsMDList.Add(aOrderItemDetails);
                            }
                            else if (item.PackageId <= 0 && !String.IsNullOrEmpty(item.MultipleMenu))
                            {
                                try
                                {
                                    optionIndex++;
                                    ReceipeMenuItemButton aReceipeMenuItemButton =
                                        aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);
                                    ReceipeCategoryButton aReceipeCategoryButton =
                                        aRestaurantMenuBll.GetCategoryByCategoryId(aReceipeMenuItemButton.CategoryId);
                                    RecipeMultipleMD aOrderItemDetails = new RecipeMultipleMD();
                                    aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
                                    aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
                                    aOrderItemDetails.MultiplePartName = item.Name;
                                    aOrderItemDetails.OptionsIndex = optionIndex;
                                    aOrderItemDetails.UnitPrice = item.Price / item.Quantity;
                                    aOrderItemDetails.Qty = item.Quantity;
                                    aOrderItemDetails.RestaurantId = aRestaurantInformation.Id;
                                    aOrderItemDetails.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                                    List<MultipleMenuDetailsJson> aMultipleMenuDetailsJsons =  new List<MultipleMenuDetailsJson>();
                                    try
                                    {
                                        aMultipleMenuDetailsJsons =
                                            JsonConvert.DeserializeObject<List<MultipleMenuDetailsJson>>(
                                                item.MultipleMenu);
                                    }
                                    catch (Exception exception)
                                    {
                                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                        aErrorReportBll.SendErrorReport(exception.ToString());
                                    }

                                    aOrderItemDetails.ItemLimit = aMultipleMenuDetailsJsons.Count;
                                    LoadMultilpleItemsList(aOrderItemDetails, aMultipleMenuDetailsJsons);
                                    aRecipeMultipleMdList.Add(aOrderItemDetails);
                                }
                                catch (Exception exception)
                                {
                                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                    aErrorReportBll.SendErrorReport(exception.ToString());
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }
                    }

                    foreach (OrderPackage package in aPackageItems)
                    {
                        optionIndex++;
                        RecipePackageButton tempRecipePackageButton =
                            aRestaurantMenuBll.GetPackageByPackageId(package.PackageId);
                        RecipePackageMD aRecipePackage = new RecipePackageMD();
                        aRecipePackage.Description = tempRecipePackageButton.Description;
                        aRecipePackage.OptionsIndex = optionIndex;
                        aRecipePackage.PackageId = tempRecipePackageButton.PackageId;
                        aRecipePackage.PackageName = package.Name;
                        aRecipePackage.Qty = package.Quantity;
                        aRecipePackage.RecipeTypeId = tempRecipePackageButton.RecipeTypeId;
                        aRecipePackage.RestaurantId = tempRecipePackageButton.RestaurantId;
                        aRecipePackage.id = package.Id;
                        aRecipePackage.ItemLimit = tempRecipePackageButton.ItemLimit * package.Quantity;
                        RecipePackageMD PackageItemLoad = LoadPackageItem(aOrderItems, aRecipePackage, package.Id);
                        //aRecipePackage.UnitPrice = (package.Price - GetPackageItemPrice(package.PackageId, optionIndex)) / package.Quantity;
                        if (package.Extra_price > 0)
                        {
                            aRecipePackage.UnitPrice = (package.Price + package.Extra_price) / package.Quantity;
                            aRecipePackage.Extraprice = package.Price;
                        }
                        else
                        {
                            aRecipePackage.UnitPrice = (package.Price + GetPackageItemPrice(package.PackageId, optionIndex)) /
                                                       package.Quantity;
                            aRecipePackage.Extraprice = package.Price;
                        }

                        aRecipePackage.ItemLimit = PackageItemLoad.ItemLimit;
                        // aRecipePackage.UnitPrice = (package.Price) / package.Quantity;
                        aRecipePackageMdList.Add(aRecipePackage);
                    }
                }
                LoadOrderDetails1();
                LoadMultiplePartIntoCart();
                LoadPackageDetails1();

                if (!string.IsNullOrEmpty(aRestaurantOrder.Comment))
                {
                    commentTextBox.Text = aRestaurantOrder.Comment;
                    commentTextBox.ForeColor = Color.Black;
                }
                else
                {
                    commentTextBox.Text = "Comment";
                    commentTextBox.ForeColor = Color.SlateGray;
                }

                if (aRestaurantOrder.CustomerId > 0)
                {
                    isCustomerTextChanged = false;
                    LoadCustomerForReorder(aRestaurantOrder.CustomerId);
                    isCustomerTextChanged = true;
                    //RestaurantUsers aRestaurantUser = new CustomerBLL().GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
                    //string address = GetCustomerDetails(aRestaurantUser);
                    //customerDetailsLabel.Text = address;
                }

                else
                {
                    customerDetailsLabel.Text = "";
                    customerEditButton.Visible = false;
                    phoneNumberDeleteButton.Visible = false;
                    customerTextBox.Text = "Search Customer";
                }
                LoadGeneralInformation();

                double orderTotal = GetTotalAmountDetails() + aRestaurantOrder.DeliveryCost + aRestaurantOrder.CardFee +
                                    aRestaurantOrder.ServiceCharge - aRestaurantOrder.Discount;

                orderTotal = GlobalVars.numberRound(orderTotal, 2);

                if ((orderTotal - aRestaurantOrder.TotalCost) < Math.Abs(0.2))
                {

                    totalAmountLabel.Text = "£" + orderTotal.ToString("F02");
                    customTotalTextBox.ForeColor = Color.Black;
                }
                else
                {
                    customTotalTextBox.Text = aRestaurantOrder.TotalCost.ToString("F02");
                    totalAmountLabel.Text = "£" + aRestaurantOrder.TotalCost.ToString("F02");
                    customTotalTextBox.ForeColor = Color.SlateGray;

                }

                AddServiceChargeIntoLabel(); 
                if (aRestaurantOrder.Coupon != null)
                {
                    OrderDiscount aOrderDiscount = new OrderDiscount();
                    aOrderDiscount.Amount = aRestaurantOrder.Discount;
                    aOrderDiscount.DiscountArea = "Order";
                    aOrderDiscount.DiscountType = aRestaurantOrder.Coupon;
                    CalculateDiscount(aOrderDiscount);
                }
            }
            catch (Exception ex)
            {
                ErrorReportBLL errorReport = new ErrorReportBLL();

                errorReport.SendErrorReport(ex.Message);
            }
        }

        private void LoadAllSaveOrder(int tableId, string type)
        {
            try
            {
                OthersMethod aOthersMethod = new OthersMethod();
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
                orderDetailsflowLayoutPanel1.Controls.Clear();
                RestaurantOrder aRestaurantOrder = new RestaurantOrder();
                RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();

                if (!orderLoadStatus) return;

                if (type == "order")
                { 
                    aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrder(tableId, "pending");
                }
                else if (type == "reorder")
                {
                    aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(tableId);
                    if (aRestaurantOrder.OrderTable > 0)
                    {

                        RestaurantTable aRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableId(aRestaurantOrder.OrderTable);
                        if (aRestaurantTable.CurrentStatus == "busy")
                        {
                            MessageBox.Show("Selected table is now busy", "Table not available", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                        resetCart();
                        LoadTableInformation(aRestaurantTable);
                    }
                    else
                    {
                        resetCart(); 
                        tableButton.Text = "TABLE";
                        personButton.Visible = false;
                        deliveryChargeButton.Text = "Delivery Charge\r\n" + aRestaurantOrder.DeliveryCost.ToString("F02");
                        discountButton.Text = "Disc\r\n" + aRestaurantOrder.Discount.ToString("F02");
                        finalizeButton.Visible = false;
                        deliveryButton.Text = "DEL";
                        deliveryButton.Visible = true;
                        collectionButton.Visible = true;
                        timeButton.Visible = true;

                        if (aRestaurantOrder.OnlineOrder > 0)
                            timeButton.Text = "ASAP";

                         if (Convert.ToDateTime(aRestaurantOrder.DeliveryTime.ToString()).ToShortTimeString() != "12:00 AM")
                        {
                            timeButton.Text = aOthersMethod.IsTimeFormatValid(aRestaurantOrder.DeliveryTime.ToString());
                        }

                        aGeneralInformation = new GeneralInformation();
                        aGeneralInformation.OrderType = aRestaurantOrder.OrderType;
                        aGeneralInformation.CardFee = aRestaurantOrder.CardFee;
                        totalAmountLabel.Text = aRestaurantOrder.TotalCost.ToString();
                    }

                    //List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderForOnline(1, "pending");
                    //if (restaurantOrder.Count == 0)
                    //{ 
                    //    if (!this.IsHandleCreated && !this.IsDisposed) return;

                    //    this.Invoke((MethodInvoker)delegate
                    //    {
                    //        onlineOrderButton.Visible = false;
                    //        grapTimer.Stop();
                    //    });


                    //}
                }

                aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
                aRecipePackageMdList = new List<RecipePackageMD>();
                aPackageItemMdList = new List<PackageItem>();
                aRecipeOptionMdList = new List<RecipeOptionMD>();
                aRecipeMultipleMdList = new List<RecipeMultipleMD>();
                aMultipleItemMdList = new List<MultipleItemMD>();
                if (aRestaurantOrder.Id > 0)
                {
                    aGeneralInformation.OrderId = aRestaurantOrder.Id;
                    aGeneralInformation.CustomerId = aRestaurantOrder.CustomerId;
                    aGeneralInformation.DiscountFlat = aRestaurantOrder.Discount;
                    aGeneralInformation.DeliveryCharge = aRestaurantOrder.DeliveryCost;
                    aGeneralInformation.CardFee = aRestaurantOrder.CardFee;
                    aGeneralInformation.DeliveryTime = aRestaurantOrder.DeliveryTime.ToString();
                    aGeneralInformation.Person = aRestaurantOrder.Person;
                    aGeneralInformation.TableId = aRestaurantOrder.OrderTable;
                    aGeneralInformation.OrderType = aRestaurantOrder.OrderType;
                    aGeneralInformation.ServiceCharge = aRestaurantOrder.ServiceCharge;

                    List<OrderItem> aOrderItems = aRestaurantOrderBLL.GetRestaurantOrderRecipeItems(aRestaurantOrder.Id);
                    List<OrderPackage> aPackageItems = aRestaurantOrderBLL.GetRestaurantOrderPackage(aRestaurantOrder.Id);

                    int optionIndex = 0;
                    List<OrderItem> _aOrderItems = aOrderItems.Where(p => p.PackageId == 0).ToList();
                    List<OrderItem> _aOrderPackageItems = aOrderItems.Where(p => p.PackageId > 0).ToList();

                    foreach (OrderItem item in _aOrderItems)
                    {
                        try
                        {
                            if (item.PackageId <= 0)
                            {
                                optionIndex++;
                                ReceipeMenuItemButton aReceipeMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);
                                ReceipeCategoryButton aReceipeCategoryButton = aRestaurantMenuBll.GetCategoryByCategoryId(aReceipeMenuItemButton.CategoryId);
                                OrderItemDetailsMD aOrderItemDetails = new OrderItemDetailsMD();
                                aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
                                aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
                                aOrderItemDetails.OrderItemId = item.Id;
                                aOrderItemDetails.OrderId = aRestaurantOrder.Id;
                                aOrderItemDetails.ItemName = item.Name;
                                aOrderItemDetails.ItemFullName = aReceipeMenuItemButton.ShortDescrip;
                                aOrderItemDetails.OptionsIndex = optionIndex;
                                aOrderItemDetails.KitchenSection = aReceipeMenuItemButton.KitchenSection;
                                aOrderItemDetails.Price = item.Price / item.Quantity;
                                aOrderItemDetails.Qty = item.Quantity;
                                aOrderItemDetails.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                                aOrderItemDetails.SortOrder = aReceipeMenuItemButton.SortOrder;
                                aOrderItemDetails.CatSortOrder = aReceipeCategoryButton.SortOrder;
                                aOrderItemDetails.TableNumber = aGeneralInformation.TableId;

                                if (type == "order")
                                {
                                    aOrderItemDetails.sendToKitchen = item.SentToKitchen;
                                    aOrderItemDetails.KitchenProcessing = item.KitchenProcessing;
                                    aOrderItemDetails.KitchenDone = item.KitchenDone;
                                }
                                else {
                                    aOrderItemDetails.sendToKitchen = item.Quantity;
                                    aOrderItemDetails.KitchenProcessing = 0;
                                    aOrderItemDetails.KitchenDone = 0;
                                }

                                try
                                {
                                    LoadSaveOption(item, aOrderItemDetails);
                                }
                                catch (Exception ex)
                                {
                                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                    aErrorReportBll.SendErrorReport(ex.ToString());
                                }

                                //if (aRestaurantOrder.OnlineOrder > 0)
                                //{
                                //    OnlineOrder LoadOptionOnline = new OnlineOrder();
                                //    LoadOptionOnline.LoadSaveOptionForOnline(item, aOrderItemDetails, this);
                                //}
                                //else{
                                //    LoadSaveOption(item, aOrderItemDetails);
                                //}
                             
                                aOrderItemDetailsMDList.Add(aOrderItemDetails);
                            }
                            //else if (item.PackageId <= 0 && !String.IsNullOrEmpty(item.MultipleMenu))
                            //{
                            //    try
                            //    {
                            //        optionIndex++;
                            //        ReceipeMenuItemButton aReceipeMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);
                            //        ReceipeCategoryButton aReceipeCategoryButton = aRestaurantMenuBll.GetCategoryByCategoryId(aReceipeMenuItemButton.CategoryId);
                            //        RecipeMultipleMD aOrderItemDetails = new RecipeMultipleMD();
                            //        aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
                            //        aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
                            //        aOrderItemDetails.MultiplePartName = item.Name;
                            //        aOrderItemDetails.OptionsIndex = optionIndex;
                            //        aOrderItemDetails.UnitPrice = item.Price / item.Quantity;
                            //        aOrderItemDetails.Qty = item.Quantity;
                            //        aOrderItemDetails.RestaurantId = aRestaurantInformation.Id;
                            //        aOrderItemDetails.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;

                            //        List<MultipleMenuDetailsJson> aMultipleMenuDetailsJsons = new List<MultipleMenuDetailsJson>();
                            //        try
                            //        {
                            //            aMultipleMenuDetailsJsons = JsonConvert.DeserializeObject<List<MultipleMenuDetailsJson>>(item.MultipleMenu);
                            //        }
                            //        catch (Exception exception)
                            //        {
                            //            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            //            aErrorReportBll.SendErrorReport(exception.ToString());
                            //        }
                            //        aOrderItemDetails.ItemLimit = aMultipleMenuDetailsJsons.Count;
                            //        LoadMultilpleItemsList(aOrderItemDetails, aMultipleMenuDetailsJsons);
                            //        aRecipeMultipleMdList.Add(aOrderItemDetails);
                            //    }
                            //    catch (Exception exception)
                            //    {
                            //        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            //        aErrorReportBll.SendErrorReport(exception.ToString());
                            //    }


                            //}
                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }
                    }

                    foreach (OrderPackage package in aPackageItems)
                    {
                        optionIndex++;
                        RecipePackageButton tempRecipePackageButton = aRestaurantMenuBll.GetPackageByPackageId(package.PackageId);
                        RecipePackageMD aRecipePackage = new RecipePackageMD();
                        aRecipePackage.Description = tempRecipePackageButton.Description;
                        aRecipePackage.OptionsIndex = optionIndex;

                        aRecipePackage.PackageId = tempRecipePackageButton.PackageId;
                        aRecipePackage.PackageName = package.Name;
                        aRecipePackage.Qty = package.Quantity;
                        aRecipePackage.RecipeTypeId = tempRecipePackageButton.RecipeTypeId;
                        aRecipePackage.RestaurantId = tempRecipePackageButton.RestaurantId;
                        aRecipePackage.id = package.Id;
                        aRecipePackage.ItemLimit = tempRecipePackageButton.ItemLimit * package.Quantity;
                        RecipePackageMD PackageItemLoad = LoadPackageItem(_aOrderPackageItems, aRecipePackage, package.Id);
                        aRecipePackage.UnitPrice = (package.Price) / package.Quantity;
                        if (package.Extra_price > 0)
                        {
                            aRecipePackage.UnitPrice = (package.Price + package.Extra_price) / package.Quantity;
                            aRecipePackage.Extraprice = 0; 
                        }
                        else
                        {
                            aRecipePackage.Extraprice = 0;
                        }

                        aRecipePackage.ItemLimit = tempRecipePackageButton.ItemLimit;
                        // aRecipePackage.UnitPrice = (package.Price) / package.Quantity;
                        aRecipePackageMdList.Add(aRecipePackage);
                    }
                }
                LoadOrderDetails1();
                LoadMultiplePartIntoCart();
                LoadPackageDetails1();

                if (!string.IsNullOrEmpty(aRestaurantOrder.Comment))
                {
                    commentTextBox.Text = aRestaurantOrder.Comment;
                    commentTextBox.ForeColor = Color.Black;
                }
                else
                {
                    commentTextBox.Text = "Comment";
                    commentTextBox.ForeColor = Color.SlateGray;
                }

                if (aRestaurantOrder.CustomerId > 0)
                {
                    isCustomerTextChanged = false;
                    LoadCustomerForReorder(aRestaurantOrder.CustomerId);
                    isCustomerTextChanged = true;
                    //RestaurantUsers aRestaurantUser = new CustomerBLL().GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
                    //string address = GetCustomerDetails(aRestaurantUser);
                    //customerDetailsLabel.Text = address;
                }
                else
                {
                    customerDetailsLabel.Text = "";
                    customerEditButton.Visible = false;
                    phoneNumberDeleteButton.Visible = false;
                    customerTextBox.Text = "Search Customer";
                    if (aRestaurantOrder.CustomerName != "")
                        customerTextBox.Text = aRestaurantOrder.CustomerName;

                }
                LoadGeneralInformation();

                OrderDiscount aOrderDiscount = new OrderDiscount();
                aOrderDiscount.Amount = aRestaurantOrder.Discount;
                aOrderDiscount.DiscountArea = "Order";
                aOrderDiscount.DiscountType = aRestaurantOrder.Coupon;
                CalculateDiscount(aOrderDiscount);

                double orderTotal = (GetTotalAmountDetails() + aRestaurantOrder.DeliveryCost + aRestaurantOrder.CardFee + aRestaurantOrder.ServiceCharge) - aGeneralInformation.DiscountFlat;
                orderTotal = GlobalVars.numberRound(orderTotal, 2);

                if ((orderTotal - aRestaurantOrder.TotalCost) < Math.Abs(0.2))
                {
                    totalAmountLabel.Text = "£" + orderTotal.ToString("F02");
                    customTotalTextBox.ForeColor = Color.Black;
                }
                else
                {
                    customTotalTextBox.Text = aRestaurantOrder.TotalCost.ToString("F02");
                    totalAmountLabel.Text = "£" + aRestaurantOrder.TotalCost.ToString("F02");
                    customTotalTextBox.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                ErrorReportBLL errorReport = new ErrorReportBLL();
                errorReport.SendErrorReport(ex.GetBaseException().ToString());
            }
        }

        private void LoadMultilpleItemsList(RecipeMultipleMD aOrderItemDetails,
            List<MultipleMenuDetailsJson> aMultipleMenuDetailsJsons)
        {
            try
            {
                foreach (MultipleMenuDetailsJson multipleItem in aMultipleMenuDetailsJsons)
                {
                    MultipleItemMD itemMd = new MultipleItemMD();
                    itemMd.CategoryId = multipleItem.category_id;
                    itemMd.ItemId = multipleItem.id;
                    itemMd.ItemName = multipleItem.name;
                    itemMd.OptionsIndex = aOrderItemDetails.OptionsIndex;
                    itemMd.Price = 0;
                    itemMd.Qty = (int)multipleItem.quantity;
                    itemMd.SubcategoryId = aOrderItemDetails.CategoryId;
                    aMultipleItemMdList.Add(itemMd);
                    LoadOptions(aOrderItemDetails, itemMd, multipleItem);
                }

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private void LoadOptions(RecipeMultipleMD recipeMultiple, MultipleItemMD aOrderItemDetails,
            MultipleMenuDetailsJson json)
        {
            List<string> optionList = json.options;

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            if (optionList.Any())
            {
                for (int i = 0; i < optionList.Count(); i++)
                {
                    if (optionList[i] != null && optionList[i].Length > 0)
                    {
                        RecipeOptionItemButton recipe =
                            aRestaurantMenuBll.GetRecipeOptionByOptionName(optionList[i].Trim());
                        if (recipe.RecipeOptionItemId > 0)
                        {
                            RecipeOptionMD aOptionMD = new RecipeOptionMD();
                            aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                            aOptionMD.TableNumber = 1;
                            aOptionMD.RecipeOptionId = recipe.RecipeOptionId;
                            aOptionMD.Title = recipe.Title;
                            aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                            if (deliveryButton.Text == "RES")
                            {
                                aOptionMD.Price = recipe.InPrice;
                            }
                            else
                            {
                                aOptionMD.Price = recipe.Price;
                            }

                            aOptionMD.InPrice = recipe.InPrice;
                            aOptionMD.Qty = 1;
                            aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                            aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                            aRecipeOptionMdList.Add(aOptionMD);
                        }
                    }
                }
            }

            List<string> optionList1 = json.minus_options;

            if (optionList1.Any())
            {
                for (int i = 0; i < optionList1.Count(); i++)
                {
                    if (optionList1[i] != null && optionList1[i].Length > 0)
                    {
                        string op = optionList1[i];
                        RecipeOptionMD tempOptionMd =
                            aRecipeOptionMdList.FirstOrDefault(
                                a => a.Title == optionList1[i] && a.OptionsIndex == aOrderItemDetails.OptionsIndex);
                        if (tempOptionMd != null && tempOptionMd.RecipeId > 0)
                        {
                            tempOptionMd.MinusOption = optionList1[i];
                        }
                        else
                        {
                            RecipeOptionItemButton recipe =
                                aRestaurantMenuBll.GetRecipeOptionByOptionName(optionList1[i].Trim());
                            if (recipe.RecipeOptionItemId > 0)
                            {
                                RecipeOptionMD aOptionMD = new RecipeOptionMD();
                                aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                                aOptionMD.TableNumber = 1;
                                aOptionMD.RecipeOptionId = recipe.RecipeOptionId;
                                aOptionMD.MinusOption = optionList1[i];
                                aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                                if (deliveryButton.Text == "RES")
                                {
                                    aOptionMD.Price = recipe.InPrice;
                                }
                                else
                                {
                                    aOptionMD.Price = recipe.Price;
                                }

                                aOptionMD.InPrice = recipe.InPrice;
                                aOptionMD.Qty = 1;
                                aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                                aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                                aRecipeOptionMdList.Add(aOptionMD);
                            }
                        }
                    }
                }
            }
        }

        private void LoadGeneralInformation()
        {
            if (aGeneralInformation.OrderType == "Collect" || aGeneralInformation.OrderType == "CLT" ||
                aGeneralInformation.OrderType == "WAIT")
            {
                collectionButton.BackColor = Color.Black;
                collectionButton.ForeColor = Color.WhiteSmoke;
                deliveryButton.BackColor = Color.WhiteSmoke;
                deliveryButton.ForeColor = Color.Black;
                ChangeButtonLocation(false);
            }
            else if (aGeneralInformation.OrderType == "DEL")
            {
                collectionButton.BackColor = Color.WhiteSmoke;
                deliveryButton.BackColor = Color.Black;
                deliveryButton.ForeColor = Color.WhiteSmoke;
                collectionButton.ForeColor = Color.Black;
                ChangeButtonLocation(true);
            }

            else
            {
                ChangeButtonLocation(true);
            }
        }

        private void LoadCustomerForReorder(int customerId)
        {
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();

            CustomerBLL aCustomerBll = new CustomerBLL();
            RestaurantUsers aRestaurantUser = aCustomerBll.GetResturantCustomerByCustomerId(customerId);
            if (aRestaurantUser != null && aRestaurantUser.Id > 0)
            {
                string cell = aRestaurantUser.Mobilephone != ""
                    ? aRestaurantUser.Mobilephone
                    : aRestaurantUser.Homephone;

                string address = aRestaurantUser.Firstname;
                address += address != ""? "," + cell: cell;
                bool flag = false;
                if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                {
                    RestaurantOrder aORder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                    if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                    {
                        if (aORder.DeliveryAddress.Contains(cell))
                        {
                            aORder.DeliveryAddress = aORder.DeliveryAddress.Replace(cell, "");
                        }
                        string[] ss = aORder.DeliveryAddress.Split(',');
                        flag = true;
                        // address +="\r\n"+ aORder.DeliveryAddress.Replace(",",",\r\n");

                        if (ss.Count() > 0)
                        {
                            address += "\r\n";

                            address += ss[0]== " \n" ? "" : "," + ss[0];
                        }

                        if (ss.Count() > 1)
                        {
                            address += ss[1] == "\n" ? "" : "," + ss[1];
                        }
                        if (ss.Count() > 2)
                        {
                            address += "," + ss[2];
                        }
                        if (ss.Count() > 3)
                        {
                            address += " " + ss[3];
                        } 
                    }
                }

                if ((deliveryButton.BackColor == Color.Black || aGeneralInformation.OrderType == "DEL") && !flag)
                {
                    address = GetCustomerDetails(aRestaurantUser);
                }

                customerDetailsLabel.Text = address;
                customerEditButton.Visible = true;
                phoneNumberDeleteButton.Visible = true;
                customerRecentItemsButton.Visible = true; 
                customerTextBox.Text = cell;
                customerTextBox.Visible = false;
                customerTextBox.SelectionStart = customerTextBox.Text.Length;
            }
        }

        private void LoadOrderDetails1()
        {
            //aOrderItemDetailsMDList  aRecipeOptionMdList
            try
            {
                orderDetailsflowLayoutPanel1.Controls.Clear();
                foreach (OrderItemDetailsMD itemDetails in aOrderItemDetailsMDList)
                {
                    List<RecipeOptionMD> aRList =
                        aRecipeOptionMdList.Where(a => a.OptionsIndex == itemDetails.OptionsIndex).ToList();

                    double optionPrice = GetOptionPrice(aRList);
                    deatilsControls aDetailsCon = new deatilsControls();
                    aDetailsCon.ItemId = itemDetails.ItemId;
                    aDetailsCon.CategoryId = itemDetails.CategoryId;
                    aDetailsCon.OptionIndex = itemDetails.OptionsIndex;
                    aDetailsCon.qtyTextBox.Text = itemDetails.Qty.ToString();
                    aDetailsCon.nameTextBox.Text = itemDetails.ItemName;
                    aDetailsCon.priceTextBox.Text = itemDetails.Price.ToString();
                    aDetailsCon.totalPriceLabel.Text = (itemDetails.Price * itemDetails.Qty).ToString();
                    aDetailsCon.optionItemLabel.Text = "";
                    if (aRList.Count == 0) 
                        aDetailsCon.optionItemLabel.Visible = false;
                    foreach (RecipeOptionMD list in aRList)
                    {
                        if (list.Isoption)
                        {
                            aDetailsCon.optionItemLabel.Text += ("→No " + list.MinusOption) + "\r\n";
                        }
                        else
                        {
                            aDetailsCon.optionItemLabel.Text += "→" + list.Qty + " " + (list.Title) + (list.Price > 0 ? "+" + list.Price.ToString("F02") : "") + "\r\n";
                        }
                        //  aDetailsCon.optionItemLabel.Text += (list.Title) + "\r\n";
                    } aDetailsCon.MouseClick += UsersGrid_WasClicked;

                    RecipeTypeDetails type =
                        orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>().FirstOrDefault(a => a.RecipeTypeId == itemDetails.RecipeTypeId);

                    if (type == null || type.RecipeTypeId <= 0)
                    {
                        RecipeTypeDetails typeDetails = new RecipeTypeDetails();
                        ReceipeTypeButton typeButton =
                            allRecipeType.SingleOrDefault(a => a.TypeId == itemDetails.RecipeTypeId);
                        if (typeButton != null)
                        {

                            if (typeButton.MergeItems != 0)
                            {
                                typeDetails.recipeTypelabel.Text = typeButton.TypeName;
                                typeDetails.ReceipeTypeButton = typeButton;
                                double amount =
                                    aOrderItemDetailsMDList.Where(b => b.RecipeTypeId == itemDetails.RecipeTypeId)
                                        .Sum(a => a.Price * a.Qty);
                                typeDetails.recipeTypeAmountlabel.Text = amount.ToString("F02");
                            }
                            else
                            {
                                // typeDetails.Auto
                                typeDetails.typeflowLayoutPanel1.Location = new Point(2, 0);
                                typeDetails.recipeTypelabel.Size = new System.Drawing.Size(0, 0);
                                typeDetails.recipeTypelabel.Visible = false;
                                typeDetails.recipeTypeAmountlabel.Size = new System.Drawing.Size(0, 0);
                                typeDetails.recipeTypeAmountlabel.Visible = false;
                                typeDetails.ReceipeTypeButton = typeButton;
                            }
                        }

                        typeDetails.RecipeTypeId = itemDetails.RecipeTypeId;
                        aRecipeTypes.Add(typeDetails);
                        typeDetails.typeflowLayoutPanel1.Controls.Add(aDetailsCon);
                        orderDetailsflowLayoutPanel1.Controls.Add(typeDetails);
                    }
                    else
                    {
                        foreach (RecipeTypeDetails control in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
                        {
                            if (control.RecipeTypeId == itemDetails.RecipeTypeId)
                            {
                                control.typeflowLayoutPanel1.Controls.Add(aDetailsCon);
                            }
                        }
                    }
                }

                customPanel.Location = new Point(customPanel.Location.X,
                    orderDetailsflowLayoutPanel1.Location.Y + orderDetailsflowLayoutPanel1.Size.Height);

                paymentDetailsPanel.Location = new Point(paymentDetailsPanel.Location.X,
                    customPanel.Location.Y + customPanel.Size.Height);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());
            }          
        }

        private void LoadMultiplePartIntoCart()
        {
            foreach (RecipeMultipleMD aRecipeMultipleMd in aRecipeMultipleMdList)
            {
                List<MultipleItemMD> aMultipleItemList =
                    aMultipleItemMdList.Where(a => a.OptionsIndex == aRecipeMultipleMd.OptionsIndex).ToList();
                List<RecipeOptionMD> aRList =
                    aRecipeOptionMdList.Where(a => a.OptionsIndex == aRecipeMultipleMd.OptionsIndex).ToList();

                double optionPrice1 = GetOptionPrice(aRList);
                MultiplePartControl aMultiplePartControl = new MultiplePartControl();
                aMultiplePartControl.OptionIndex = aRecipeMultipleMd.OptionsIndex;
                aMultiplePartControl.qtyTextBox.Text = aRecipeMultipleMd.Qty.ToString();
                aMultiplePartControl.nameTextBox.Text = aRecipeMultipleMd.MultiplePartName;
                aMultiplePartControl.priceTextBox.Text = (aRecipeMultipleMd.UnitPrice + optionPrice1).ToString();
                aMultiplePartControl.totalPriceLabel.Text =
                    ((aRecipeMultipleMd.UnitPrice) * aRecipeMultipleMd.Qty).ToString();
                aMultiplePartControl.CategoryId = aRecipeMultipleMd.CategoryId;

                int cnt = 0;
                foreach (MultipleItemMD itemlist in aMultipleItemList)
                {
                    cnt++;
                    Label aLabel = new Label();
                    aLabel.Name = aRecipeMultipleMd.OptionsIndex.ToString();
                    aLabel.AutoSize = true;
                    aLabel.Text = GetOrdinalSuffix(cnt) + ": " + itemlist.ItemName + "\r\n";
                    List<RecipeOptionMD> aMultipleList =
                        aRecipeOptionMdList.Where(
                            a => a.OptionsIndex == aRecipeMultipleMd.OptionsIndex && a.RecipeId == itemlist.ItemId)
                            .ToList();
                    foreach (RecipeOptionMD list in aMultipleList)
                    {
                        if (!string.IsNullOrEmpty(list.MinusOption))
                        {
                            if (list.InPrice > 0)
                            {
                                aLabel.Text += (" →No " + list.MinusOption) + "\r\n";
                            }
                            else aLabel.Text += (" →No " + list.MinusOption) + "\r\n";
                        }
                        if (!string.IsNullOrEmpty(list.Title))
                        {
                            if (list.InPrice > 0)
                            {
                                aLabel.Text += " →" + (list.Title) + "\r\n";
                            }
                            else aLabel.Text += " →" + (list.Title) + "\r\n";
                        }
                    }

                    aLabel.MouseClick += (aLabel_MouseClick);
                    aMultiplePartControl.packageItemsFlowLayoutPanel.Controls.Add(aLabel);
                }
                orderDetailsflowLayoutPanel1.Controls.Add(aMultiplePartControl);
            }
        }

        private void LoadPackageDetails1()
        {
            foreach (RecipePackageMD package in aRecipePackageMdList)
            {
                PackageDetails aPackageDetails = new PackageDetails();
                aPackageDetails.qtyTextBox.Text = package.Qty.ToString();
                aPackageDetails.nameTextBox.Text = package.PackageName;
                aPackageDetails.priceTextBox.Text = (package.UnitPrice).ToString("F02");

                aPackageDetails.PackageId = package.PackageId;
                aPackageDetails.OptionIndex = package.OptionsIndex;
                aPackageDetails.ItemLimit = Convert.ToInt16(package.ItemLimit);
                double itemPrice = 0;
                List<PackageItem> itemList = aPackageItemMdList.Where(a => a.OptionsIndex == package.OptionsIndex).ToList();
                foreach (PackageItem item in itemList)
                {
                    PackItemsControl aControl = new PackItemsControl();
                    aControl.qtyTextBox.Text = item.Qty.ToString();
                    aControl.nameTextBox.Text = item.ItemName;
                    aControl.MouseClick += UsersGridForPackageItem_WasClicked;
                    aControl.OptionIndex = package.OptionsIndex;
                    aControl.PackageItemOptionIndex = item.PackageItemOptionsIndex;
                    aControl.PackageId = package.PackageId;
                    aControl.ItemId = item.ItemId;

                    if (item.Price > 0)
                    {
                        itemPrice += item.Price;
                        aControl.totalPriceLabel.Text = item.Price.ToString("F02");
                    }
                    else
                    {
                        aControl.totalPriceLabel.Text = "";
                    }

                    List<RecipeOptionMD> recipeOptions = aRecipeOptionMdList.Where(a => a.OptionsIndex == item.OptionsIndex && a.RecipeId == item.ItemId && a.PackageItemRowId == item.Id ).ToList();
                    if (recipeOptions.Count > 0)
                    {
                        aControl.packageOptionsLabel.Text = "  ";
                        bool flag = false;
                        foreach (RecipeOptionMD list in recipeOptions)
                        {
                            if (flag)
                            {
                                aControl.packageOptionsLabel.Text += "\r\n  ";
                            } if (list.Isoption)
                            {
                                if (list.InPrice > 0)
                                {
                                    aControl.packageOptionsLabel.Text += ("→" + list.Qty + "No " + list.MinusOption);
                                }
                                else aControl.packageOptionsLabel.Text += ("→" + list.Qty + "No " + list.MinusOption);

                            }
                            else
                            {
                                if (list.InPrice > 0)
                                {
                                    aControl.packageOptionsLabel.Text += "→" + list.Qty + (list.Title) + "+ " +
                                                                         list.InPrice.ToString("F02");
                                }
                                else aControl.packageOptionsLabel.Text += "→" + list.Qty + (list.Title);
                            }

                            flag = true;
                        }
                    }
                    else
                    {
                        aControl.packageOptionsLabel.Visible = false;
                    }
                    aPackageDetails.packageItemsFlowLayoutPanel.Controls.Add(aControl);
                }
                if (itemList.Count == 0)
                {
                    aPackageDetails.packageItemsFlowLayoutPanel.Size =
                        new Size(aPackageDetails.packageItemsFlowLayoutPanel.Size.Width, 0);
                }
               
              aPackageDetails.totalPriceLabel.Text = (((package.UnitPrice) * package.Qty) + itemPrice).ToString();
                //   aPackageDetails.totalPriceLabel.Text = (package.Extraprice).ToString("F02");
                aPackageDetails.MouseClick += UsersGridForPackage_WasClicked;
                orderDetailsflowLayoutPanel1.Controls.Add(aPackageDetails);
            }
        }

        private RecipePackageMD LoadPackageItem(List<OrderItem> aOrderItems, RecipePackageMD aRecipePackage, int packageId)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            int packageOptionIndex = 1;
            foreach (OrderItem item in aOrderItems)
            {
                if (item.PackageId > 0 && item.PackageId == aRecipePackage.PackageId && item.orderPackageId == packageId && item.orderPackageId > 0) //item.RecipeId==aRecipePackage.OptionsIndex
                {
                    PackageItemButton itemButton = aRestaurantMenuBll.GetRecipeByItemIdForPackage(item.RecipeId);
                    ////PackageItemButton itemButton = aVariousMethod.GetItemForPackageOrder(item.RecipeId,item.PackageId);
                    //ReceipeMenuItemButton menuItem = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);

                    PackageItem aItem = new PackageItem();
                    aItem.ItemId = item.RecipeId;
                    aItem.ItemName = item.Name;
                    aItem.OrderId = item.OrderId;
                    aItem.OrderItemId = item.Id;
                    aItem.Price = item.ExtraPrice > 0 ? item.ExtraPrice : item.Price;
                    aItem.Id = item.Id;
                    aItem.ExtraPrice = item.ExtraPrice;
                    aItem.Qty = item.Quantity;
                    aItem.OptionName = string.IsNullOrEmpty(item.Options) ? itemButton.OptionName : item.Options;
                    aItem.PackageId = item.PackageId;
                    aItem.CategoryId = itemButton.CategoryId;
                    aItem.SubcategoryId = itemButton.SubCategoryId;
                    aItem.OptionsIndex = aRecipePackage.OptionsIndex;
                    aItem.PackageItemOptionsIndex = packageOptionIndex;
                    LoadSaveOptionForPackage(item, aItem);
                    aRecipePackage.ItemLimit = aRecipePackage.ItemLimit - item.Quantity;
                    aPackageItemMdList.Add(aItem);
                    packageOptionIndex++;
                }
                else if (item.PackageId > 0 && item.PackageId == aRecipePackage.PackageId && item.orderPackageId == 0)
                //item.RecipeId==aRecipePackage.OptionsIndex
                {
                    PackageItemButton itemButton = aRestaurantMenuBll.GetRecipeByItemIdForPackage(item.RecipeId);
                    //    PackageItemButton itemButton = aVariousMethod.GetItemForPackageOrder(item.RecipeId,item.PackageId);
                    //  ReceipeMenuItemButton menuItem = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);

                    PackageItem aItem = new PackageItem();
                    aItem.ItemId = item.RecipeId;
                    aItem.ItemName = item.Name;
                    aItem.Price = item.ExtraPrice;
                    aItem.ExtraPrice = item.ExtraPrice;
                    aItem.Qty = item.Quantity;
                    aItem.OptionName = string.IsNullOrEmpty(item.Options) ? itemButton.OptionName : item.Options;
                    aItem.PackageId = item.PackageId;
                    aItem.CategoryId = itemButton.CategoryId;
                    aItem.SubcategoryId = itemButton.SubCategoryId;
                    aItem.OptionsIndex = aRecipePackage.OptionsIndex;
                    aItem.PackageItemOptionsIndex = packageOptionIndex;

                    LoadSaveOptionForPackage(item, aItem);
                    aRecipePackage.ItemLimit = aRecipePackage.ItemLimit - item.Quantity;
                    aPackageItemMdList.Add(aItem);
                    packageOptionIndex++;
                }
            }
            return aRecipePackage;
        }

        private void LoadSaveOptionForPackage(OrderItem item, PackageItem aOrderItemDetails)
        {
            List<OptionJson> ListOfOption = new OptionJsonConverter().DeSerialize(item.Options);
            List<OptionJson> ListOfOption1 = new OptionJsonConverter().DeSerialize(item.MinusOptions);
            // string[] optionList = item.Options.Split(',');

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            if (ListOfOption.Count >0)
            {
                for (int i = 0; i < ListOfOption.Count(); i++)
                {
                    RecipeOptionMD aOptionMD = new RecipeOptionMD();
                    aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                    aOptionMD.TableNumber = 1;
                    aOptionMD.RecipeOptionId = Convert.ToInt32(ListOfOption[i].optionId);
                    aOptionMD.Title = ListOfOption[i].optionName;
                    aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                    aOptionMD.PackageItemOptionsIndex = aOrderItemDetails.PackageItemOptionsIndex;
                    aOptionMD.Price = ListOfOption[i].optionPrice;
                    aOptionMD.InPrice = ListOfOption[i].optionPrice;
                    aOptionMD.Isoption = ListOfOption[i].NoOption;
                    aOptionMD.PackageItemRowId = item.Id;
                    aOptionMD.Qty = ListOfOption[i].optionQty; 
                    aRecipeOptionMdList.Add(aOptionMD);
                }
            }
            if (ListOfOption1.Count > 0)
            {
                for (int i = 0; i < ListOfOption1.Count(); i++)
                {
                    RecipeOptionMD aOptionMD = new RecipeOptionMD();
                    aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                    aOptionMD.TableNumber = 1;
                    aOptionMD.RecipeOptionId = Convert.ToInt32(ListOfOption1[i].optionId);// ListOfOption1[i].optionId;
                    aOptionMD.Title = ListOfOption1[i].optionName;
                    aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                    aOptionMD.Price = 0;
                    aOptionMD.InPrice = 0;
                    aOptionMD.Isoption = ListOfOption1[i].NoOption;
                    aOptionMD.Qty = ListOfOption1[i].optionQty; 
                    aRecipeOptionMdList.Add(aOptionMD);
                }
            }
        }

        private void LoadSaveOption(OrderItem item, OrderItemDetailsMD aOrderItemDetails)
        {
            List<OptionJson> optionList = new OptionJsonConverter().DeSerialize(item.Options);

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            if (optionList != null && optionList.Count > 0)
            {
                for (int i = 0; i < optionList.Count; i++)
                {
                    RecipeOptionMD aOptionMD = new RecipeOptionMD();
                    aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                    aOptionMD.TableNumber = 1;
                    aOptionMD.RecipeOptionId = Convert.ToInt32(optionList[i].optionId);
                    aOptionMD.Title = optionList[i].optionName;
                    aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                    aOptionMD.Price = optionList[i].optionPrice;
                    aOptionMD.Isoption = optionList[i].NoOption;
                    aOptionMD.InPrice = optionList[i].optionPrice;
                    aOptionMD.Qty = optionList[i].optionQty;
                    aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                    aOptionMD.RecipeOPtionItemId = aOrderItemDetails.ItemId;
                    aRecipeOptionMdList.Add(aOptionMD);
                }
            }

            List<OptionJson> optionList1 = new OptionJsonConverter().DeSerialize(item.MinusOptions);

            if (optionList1 != null && optionList1.Count > 0)
            {
                for (int i = 0; i < optionList1.Count; i++)
                {
                    if (Convert.ToInt32(optionList1[i].optionId) > 0)
                    {
                        RecipeOptionMD aOptionMD = new RecipeOptionMD();
                        aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                        aOptionMD.TableNumber = 1;
                        aOptionMD.RecipeOptionId = Convert.ToInt32(optionList1[i].optionId);
                        aOptionMD.Title = optionList1[i].optionName;
                        aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                        aOptionMD.Price = 0;
                        aOptionMD.Isoption = optionList1[i].NoOption;
                        aOptionMD.InPrice = 0;
                        aOptionMD.Qty = optionList1[i].optionQty;
                        aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                        aOptionMD.RecipeOPtionItemId = aOrderItemDetails.ItemId;
                        aRecipeOptionMdList.Add(aOptionMD);
                    }
                }
            }

            //string[] optionList = item.Options_ids.Split(',');

            //string[] optionList1 = item.MinusOptions.Split(',');

            //if (optionList1.Any())
            //{
            //    for (int i = 0; i < optionList1.Count(); i++)
            //    {


            //        RecipeOptionMD tempOptionMd =
            //            aRecipeOptionMdList.FirstOrDefault(a => a.Title == optionList1[i].optionName && a.OptionsIndex == aOrderItemDetails.OptionsIndex);
            //        if (tempOptionMd != null && tempOptionMd.RecipeId > 0)
            //        {
            //            tempOptionMd.MinusOption = optionList1[i];
            //        }
            //        else
            //        {
            //            RecipeOptionItemButton recipe =
            //                aRestaurantMenuBll.GetRecipeOptionByOptionName(optionList1[i]);
            //            if (recipe.RecipeOptionItemId > 0)
            //            {
            //                RecipeOptionMD aOptionMD = new RecipeOptionMD();
            //                aOptionMD.RecipeId = aOrderItemDetails.ItemId;
            //                aOptionMD.TableNumber = 1;
            //                aOptionMD.RecipeOptionId = recipe.RecipeOptionId;
            //                aOptionMD.MinusOption = optionList1[i];
            //                aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
            //                if (deliveryButton.Text == "RES")
            //                {
            //                    aOptionMD.Price = recipe.InPrice;
            //                }
            //                else
            //                {
            //                    aOptionMD.Price = recipe.Price;
            //                }

            //                aOptionMD.InPrice = recipe.InPrice;
            //                aOptionMD.Qty = 1;
            //                aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
            //                aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
            //                aRecipeOptionMdList.Add(aOptionMD);
            //            }
            //        }
            //    }
        }

        private void personButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();

            RestaurantTable aRestaurantTable =
                aRestaurantTableBll.GetRestaurantTableByTableNumber(aGeneralInformation.TableNumber);

            CoversForm.Status = "";
            CoversForm.Covers = "";
            CoversForm aForm = new CoversForm();
            aForm.ShowDialog();
            if (CoversForm.Status == "" || CoversForm.Status == "cancel") return;
            aRestaurantTable.Person = Convert.ToInt32("0" + CoversForm.Covers);
            aRestaurantTableBll.UpdateRestaurantTable(aRestaurantTable);
            aGeneralInformation.Person = aRestaurantTable.Person;
            personButton.Text = "P " + aGeneralInformation.Person;
        }

        private void finalizeButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            if (orderDetailsflowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("No items in this order!", "Save Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PaidAmountWithPrint(true, true, false, true);
        }

        private async void onlineOrderButton_ClickAsync(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
             
            orderLoadStatus = true;

            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();

            try
            {
                List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderForOnline(1, "pending");
                if (restaurantOrder.Count() == 0)
                {
                    await Task.Run(() => OnlineOrder.manualGetOnlineOrder());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            OnlineOrderForm.Id = 0;
            OnlineOrderForm aOnlineOrderForm = new OnlineOrderForm();
            aOnlineOrderForm.ShowDialog();

            if (OnlineOrderForm.Id > 0)
            {
                LoadAllSaveOrder((int)OnlineOrderForm.Id, "reorder");
            }

            OnlineOrderMethod();
        }

        private void GetAllReservation(int resId)
        { 
            urls.AcceptUrl = GlobalVars.hostUrl;
            string text = "";
            try
            {
                var request = WebRequest.Create(urls.AcceptUrl + "restaurantcontrol/request/crud/get_all_reservation/" + resId);
                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        text = reader.ReadToEnd();
                    }
                }
            } 
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }

            string json = text;

            try
            {
                if (json != "[]")
                {
                    dynamic objects = JArray.Parse(json); // parse as array 
                    List<Reservation> reservations = new List<Reservation>();

                    ReservationBLL aReservationBll = new ReservationBLL();

                    foreach (JObject ject in objects)
                    {
                        Reservation reservation = aReservationBll.GetOnlineReservation(ject);
                        reservations.Add(reservation);
                    }

                    if (reservations.Count > 0)
                    {
                        SaveOnlineReservation(reservations);
                    }
                }
            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private void GetReservation()
        {
            string text = "";
            try
            {
                urls.AcceptUrl = GlobalVars.hostUrl;
                var request =  (HttpWebRequest) WebRequest.Create(urls.AcceptUrl + "restaurantcontrol/request/crud/get_reservation/" +
                                          aRestaurantInformation.Id);
                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        text = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.GetBaseException().ToString());
            }

            string json = text;

            try
            {
                if (json != "")
                {
                    dynamic objects = JArray.Parse(json); // parse as array 
                    List<Reservation> reservations = new List<Reservation>();

                    ReservationBLL aReservationBll = new ReservationBLL();

                    foreach (JObject ject in objects)
                    {
                        Reservation reservation = aReservationBll.GetOnlineReservation(ject);
                        reservations.Add(reservation);
                    }

                    if (reservations.Count > 0)
                    {
                        SaveOnlineReservation(reservations);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.GetBaseException().ToString());
            }
        }

        private void SaveOnlineReservation(List<Reservation> reservations)
        {
            ReservationBLL aReservationBll = new ReservationBLL();

            foreach (Reservation reservation in reservations)
            {
                Reservation tempReservation =  aReservationBll.GetBookingByOnlineReservationId(reservation.online_reservation_id);

                if (tempReservation.ReserveId == 0 || reservation.online_reservation_id == 0)
                {
                    int result = aReservationBll.InsertOnlineReservation(reservation, true);
                }
            }
        }

        //private  bool CheckOnlineOrder(RestaurantOrder order)
        //{
        //    RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
        //    RestaurantOrder rcsOrder = aRestaurantOrderBLL.GetRestaurantOrderByOnlineOrder(order.OnlineOrderId);
        //    if (rcsOrder != null && rcsOrder.OnlineOrderId > 0) return true;
        //    return false;
        //}

        public void OnlineOrderTimer_Tick(object sender, EventArgs e)
        {
            if (aTimer.Enabled == false)
            {
                return;
            }
            OnlineOrderMethod();
        }

        public async void OnlineOrderMethod()
        {
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();

            try
            {
                List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderForOnline(1, "pending");
                if (GlobalVars.isOnlineOrderTimer == true && GlobalVars.checkOnlineOrderCounter++ >= GlobalVars.intervalCheckOnlineOrder)
                {
                    await Task.Run(() => OnlineOrder.manualGetOnlineOrder());
                    GlobalVars.checkOnlineOrderCounter = 0;
                }
                MethodInvoker methodInvokerDelegate = delegate()
                {
                    string order = "Online Order";
                    if (restaurantOrder.Count() > 0)
                    {
                        order = restaurantOrder.Count + " Order";
                        PlayFile();
                        blinkBtnTimer.Enabled = true;
                    }
                    else
                    {
                        blinkBtnTimer.Enabled = false;
                        onlineOrderButton.BackColor = Color.Crimson;
                    }
                    
                    onlineOrderButton.Text = order;
                };

                if(this.InvokeRequired)
                    this.Invoke(methodInvokerDelegate);
                else
                    methodInvokerDelegate();
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.GetBaseException().ToString());
                
                MethodInvoker activateFormDelegate = delegate ()
                {
                    this.Activate();
                };

                if (this.InvokeRequired)
                {
                    this.Invoke(activateFormDelegate);
                }
                else
                {
                    activateFormDelegate();
                }                    
            }
        }

        private void PlayFile()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Ring/audio.wav");
            player.Play();
        }

        void blinkButtonEvent(object source, ElapsedEventArgs e)
        {
            this.onlineOrderButton.BackColor = this.onlineOrderButton.BackColor == Color.Crimson ? Color.Aqua : Color.Crimson;
        }

        public class SearchUserCustome
        {
            public int UserId { set; get; }
            public string CustomerDetails { set; get; }
        }

        private void customerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)13)
            {
                if (customerTextBox.Text != "")
                {SearchUserCustome aCustom = new SearchUserCustome();
                    aCustom = aSearchUserCustom.FirstOrDefault(a => a.CustomerDetails.Contains(customerTextBox.Text));
                    if (aCustom == null) return;
                    RestaurantUsers aRestaurantUser =
                        aRestaurantUserForSearchCustomer.SingleOrDefault(a => a.Id == aCustom.UserId);
                    if (aRestaurantUser != null && aRestaurantUser.Id > 0)
                    {
                        string cell = aRestaurantUser.Mobilephone != ""
                            ? aRestaurantUser.Mobilephone
                            : aRestaurantUser.Homephone;
                        string address = GetCustomerDetails(aRestaurantUser);
                        customerDetailsLabel.Text = address;
                        customerEditButton.Visible = true;
                        phoneNumberDeleteButton.Visible = true;
                        customerTextBox.Text = cell;
                        customerTextBox.SelectionStart = customerTextBox.Text.Length;
                        customerTextBox.Visible = false;
                    }
                    else
                    {
                        customerDetailsLabel.Text = "";
                        customerEditButton.Visible = false;
                        phoneNumberDeleteButton.Visible = true;
                        customerTextBox.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void menuTabControl_MouseEnter(object sender, EventArgs e)
        {
            // menuTabControl.SelectedTab.Focus();
        }

        private void tillOpenButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            if (GlobalSetting.RestaurantUsers.Usertype != "user")
            {
                OpenCashDrawer();
            }
            else if (GlobalSetting.SettingInformation.till == "Enable")
            {
                RestaurantUsers users = new RestaurantUsers();
                AutorizeForm form = new AutorizeForm(users,"till");
                form.ShowDialog();

                if (form.user.Autorize)
                {
                    OpenCashDrawer();
                }
                else
                { 
                    panel1.Focus();
                }
            }
        }

        private void multiplePartButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            SelectMultiplePartButton();
        }

        private void SelectMultiplePartButton()
        {
            if (!isMultiplePart)
            {
                multiplePartButton.BackgroundImage = Properties.Resources.half;
                isMultiplePart = true;
            }
            else
            {
                multiplePartButton.BackgroundImage = Properties.Resources.half_grey;
                isMultiplePart = false;
            }
        }

        private void aLabel_MouseClick(object sender12131231, EventArgs e)
        {
            int index = 0;
            Label aLabel = sender12131231 as Label;
            if (int.TryParse(aLabel.Name, out index))
            {
                ClearAllSelect(index);
            }
        }

        private void ClearAllSelect(int index)
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

            foreach
                (
                RecipeTypeDetails control1 in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
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

            foreach (MultiplePartControl cc in aForm.orderDetailsflowLayoutPanel1.Controls.OfType<MultiplePartControl>()
                )
            {
                if (cc.OptionIndex == index)
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

        private static string GetOrdinalSuffix(int num)
        {
            if (num.ToString().EndsWith("11")) return "th";
            if (num.ToString().EndsWith("12")) return "th";
            if (num.ToString().EndsWith("13")) return "th";
            if (num.ToString().EndsWith("1")) return (" " + num + "st");
            if (num.ToString().EndsWith("2")) return (" " + num + "nd");
            if (num.ToString().EndsWith("3")) return (" " + num + "rd");
            return (" " + num + "th");
        }

        private void commentTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                aOthersMethod.NumberPadClose();
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 350);
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

        private void commentTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void customerTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                customerTextBox.Text = "";
                aOthersMethod.KeyBoardClose();
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {
                    Point aPoint = new Point(0, 270);
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

        private void customTotalTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                aOthersMethod.KeyBoardClose();
                if (!Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
                {
                    int x = rightPanel.Location.X - 195;
                    Point aPoint = new Point(x, 280);
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

        private void billButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            }
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            if (aGeneralInformation.TableId > 0)
            {
                RestaurantTable aTable = aRestaurantTableBll.GetRestaurantTableByTableId(aGeneralInformation.TableId);
                aTable.CurrentStatus = "bill";
                aRestaurantTableBll.UpdateRestaurantTable(aTable);
                PaidAmountWithPrint(false, false, true);
            }
        }

        private void orderDetailsflowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            customerShowFlowLayoutPanel.Visible = false;

            /*double total;
            if (!double.TryParse(customTotalTextBox.Text, out total))
            {
                double amount1 = aOrderItemDetailsMDList.Sum(a => a.Qty * a.Price);
                double amount2 = aRecipePackageMdList.Sum(a => a.Qty * a.UnitPrice);
                // double amount3 = aPackageItemMdList.Sum(a => a.Qty * a.Price);
                double amount3 = aPackageItemMdList.Sum(a => a.Price);
                double packageOptionPrice = aRecipeOptionMdList.Sum(a => a.Qty * a.Price);

                double amount4 = aRecipeMultipleMdList.Sum(a => a.Qty * a.UnitPrice);
                totalAmountLabel.Text = "£" + (amount1 + amount2 + amount3 + amount4 + aGeneralInformation.CardFee +
                                         aGeneralInformation.ServiceCharge + aGeneralInformation.DeliveryCharge -
                                         aGeneralInformation.DiscountFlat - aGeneralInformation.ItemDiscount).ToString("F02");
            }
            else
            {
                totalAmountLabel.Text = "£" + (total).ToString("F02");
            }

            foreach (RecipeTypeDetails control1 in orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>())
            {
                double amount =
                    aOrderItemDetailsMDList.Where(b => b.RecipeTypeId == control1.RecipeTypeId).Sum(a => a.Price * a.Qty);
                control1.recipeTypeAmountlabel.Text = amount.ToString("F02");
            }*/
        }

        public void GetDataBaseReservation()
        {
            try
            {
                ReservationBLL aReservationBll = new ReservationBLL();
                DataTable reservation = aReservationBll.GetNewReservation();
                List<DataRow> query = reservation.AsEnumerable().ToList();
           //     IEnumerable<DataRow> query = (from myRow in reservation.AsEnumerable() select myRow).ToList();
               
                btnReservation.Visible = false;
                if (query.Count > 0)
                {
                    PlayResFile();
                    // Refresh();
                    MethodInvoker methodInvokerDelegate = delegate()
                    {
                        btnReservation.Visible = true;
                        btnReservation.BringToFront();
                        btnReservation.Width = 53;
                        btnReservation.Text = query.Count + "\n Res";
                        customerDetailsLabel.Location = new Point(btnReservation.Location.X,
                            customerDetailsLabel.Location.Y);
                    };
                    
                    if (this.InvokeRequired)
                        this.Invoke(methodInvokerDelegate);
                    else
                        methodInvokerDelegate();
                }
                else {
                    resPlayer.Stop();
                    MethodInvoker methodInvokerDelegate = delegate()
                    {
                        btnReservation.Visible = false;
                        btnReservation.BringToFront();
                        btnReservation.Width = 0;
                        btnReservation.Text =  "0\n Res";
                        customerDetailsLabel.Location = new Point(btnReservation.Location.X,
                            customerDetailsLabel.Location.Y);
                    };
                    if (this.InvokeRequired)
                        this.Invoke(methodInvokerDelegate);
                    else
                        methodInvokerDelegate();
                }               
            }
            catch (Exception ex)
            {
                this.Activate();
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
            }           
        }

        private async void timerReservation_Tick(object sender, EventArgs e)
        {          
            try
            {
                if (resTimer.Enabled == false)
                {
                    return;
                }
                resTimer.Stop();
                resTimer.Enabled = true;
                GetReservation();
                GetDataBaseReservation();
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.GetBaseException().ToString());
            }
        }

        private void PlayResFile()
        {
            resPlayer.Play();
        }

        private void customerRecentItemsButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            LoadRecetAddedItems();
        }

        //customPanel
        private void LoadRecetAddedItems()
        {
            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            }
            recentItemsFlowLayoutPanel.Visible = true;
            recentItemsFlowLayoutPanel.Location = new Point(recentItemsFlowLayoutPanel.Location.X,
                customPanel.Location.Y - recentItemsFlowLayoutPanel.Height);
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            CustomerRecentItemBLL aCustomerRecentItemBll = new CustomerRecentItemBLL();
            List<CustomerRecentItemMD> aCustomerRecentItemMds =
                aCustomerRecentItemBll.GetCustomerRecentItemMd(aGeneralInformation.CustomerId);
            if (aCustomerRecentItemMds.Any())
            {
                recentItemsFlowLayoutPanel.Controls.Clear();
                foreach (CustomerRecentItemMD itemMd in aCustomerRecentItemMds)
                {
                    if (itemMd.recipe_id > 0)
                    {
                        ReceipeMenuItemButton aMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(itemMd.recipe_id);
                        ReceipeCategoryButton aReceipeCategoryButton =
                            aRestaurantMenuBll.GetCategoryByCategoryId(aMenuItemButton.CategoryId);
                        aMenuItemButton.Text = aMenuItemButton.ItemName;

                        aMenuItemButton.Width = (recentItemsFlowLayoutPanel.Width / 2) - 15;
                        aMenuItemButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("info"));
                        aMenuItemButton.ForeColor = Color.White;
                        aMenuItemButton.Height = 30;
                        aMenuItemButton.FlatStyle = FlatStyle.Flat;
                        aMenuItemButton.FlatAppearance.BorderSize = 0;
                        aMenuItemButton.Click += new EventHandler(ReceipeMenuItemButton_Click);
                        aMenuItemButton.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                        recentItemsFlowLayoutPanel.Controls.Add(aMenuItemButton);
                    }
                }
            }
            else
            {
                recentItemsFlowLayoutPanel.Visible = false;
                // customPanel.Visible = true;
            }
        }

        private void HideButton_Click(object sen5, EventArgs e)
        {
            recentItemsFlowLayoutPanel.Visible = false;
        }

        private int SaveAllCustomerRecentItem(ReceipeMenuItemButton aReceipeMenuItemButton)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            int itemIndex = 0;

            List<RecipeOptionItemButton> aRecipeList = aReceipeMenuItemButton.aRecipeOptionItemButtons;
            int optionIndex = GetOptionIndex();

            OrderItemDetailsMD aOrderItemDetails = new OrderItemDetailsMD();
            aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
            aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
            aOrderItemDetails.ItemName = aReceipeMenuItemButton.ReceiptName;
            aOrderItemDetails.ItemFullName = aReceipeMenuItemButton.ShortDescrip;
            aOrderItemDetails.OptionsIndex = optionIndex + 1;
            aOrderItemDetails.KitchenSection = aReceipeMenuItemButton.KitchenSection;
            if (deliveryButton.Text == "RES")
            {
                aOrderItemDetails.Price = aReceipeMenuItemButton.InPrice;
            }
            else
            {
                aOrderItemDetails.Price = aReceipeMenuItemButton.OutPrice;
            }

            aOrderItemDetails.Qty = 1;
            aOrderItemDetails.RecipeTypeId = aReceipeMenuItemButton.RecipeTypeId;
            aOrderItemDetails.SortOrder = aReceipeMenuItemButton.SortOrder;
            aOrderItemDetails.TableNumber = 1;

            int index = CheckDulicate(aOrderItemDetails, aRecipeList);

            if (index > 0)
            {
                itemIndex = index;
                List<RecipeOptionMD> aRList = aRecipeOptionMdList.Where(a => a.OptionsIndex == index).ToList();
                foreach (RecipeOptionMD list in aRList)
                {
                    list.Qty += 1;
                }

                OrderItemDetailsMD tempOrderItemDetails =
                    aOrderItemDetailsMDList.SingleOrDefault(a => a.OptionsIndex == index);
                tempOrderItemDetails.Qty += 1;
            }
            else if (index <= 0)
            {
                itemIndex = optionIndex + 1;
                aOrderItemDetailsMDList.Add(aOrderItemDetails);
                foreach (RecipeOptionItemButton recipe in aRecipeList)
                {
                    RecipeOptionMD aOptionMD = new RecipeOptionMD();
                    aOptionMD.RecipeId = aReceipeMenuItemButton.RecipeMenuItemId;
                    aOptionMD.TableNumber = 1;
                    aOptionMD.RecipeOptionId = recipe.RecipeOptionId;

                    if (!string.IsNullOrEmpty(recipe.MinusTitle))
                    {
                        aOptionMD.MinusOption = recipe.MinusTitle;
                    }
                    else if (!string.IsNullOrEmpty(recipe.Title))
                    {
                        aOptionMD.Title = recipe.Title;
                    }

                    aOptionMD.Type = recipe.RecipeOptionButton.Type;

                    if (deliveryButton.Text == "RES")
                    {
                        aOptionMD.Price = recipe.InPrice;
                    }
                    else
                    {
                        aOptionMD.Price = recipe.Price;
                    }

                    aOptionMD.InPrice = recipe.InPrice;
                    aOptionMD.Qty = 1;
                    aOptionMD.OptionsIndex = optionIndex + 1;
                    aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                    aRecipeOptionMdList.Add(aOptionMD);
                }
            }
            return itemIndex;
        }

        private void serviceChargeLabel_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            ServiceChargeForm.OrderDiscount = new OrderDiscount();
            ServiceChargeForm aServiceChargeForm = new ServiceChargeForm();
            aServiceChargeForm.ShowDialog();
            OrderDiscount aOrderDiscount = ServiceChargeForm.OrderDiscount;
            if (aOrderDiscount.Status == "cancel") return;
            else if (aOrderDiscount.DiscountArea == "service_charge")
            {
                ApplingServiceCharge(aOrderDiscount);
            }
        }

        private void ApplingServiceCharge(OrderDiscount aOrderDiscount)
        {
            double serviceCharge = 0;

            double amount1 = 0;
            double amount2 = 0;
            double amount3 = 0;

            amount1 = aOrderItemDetailsMDList.Sum(a => a.Qty * a.Price);
            amount2 = aRecipePackageMdList.Sum(a => a.Qty * a.UnitPrice);
            amount3 = aPackageItemMdList.Sum(a => a.Qty * a.Price);

            double totalAmount = amount1 + amount2 + amount3;
            if (aOrderDiscount.DiscountType == "Fixed")
            {
                if (totalAmount > 0)
                {
                    aGeneralInformation.ServiceChargePercent = (aOrderDiscount.Amount * 100) / totalAmount;
                }
                aGeneralInformation.ServiceCharge = aOrderDiscount.Amount;
            }
            else if (aOrderDiscount.DiscountType == "Persent")
            {
                aGeneralInformation.ServiceCharge = (totalAmount * aOrderDiscount.Amount) / 100;
                aGeneralInformation.ServiceChargePercent = aOrderDiscount.Amount;
            }
            AddServiceChargeIntoLabel();
        }
                                                                  
        private void AddServiceChargeIntoLabel()
        {
            if (aGeneralInformation.ServiceCharge > 0)
            {
                serviceChargeLabel.Text = "Service Charge: " + aGeneralInformation.ServiceCharge.ToString("F02");
            }
            else
            {
                serviceChargeLabel.Text = "Service Charge";
            }
        }

        private void mainForm_MouseMove(object sender, MouseEventArgs e)
        {
            //  MessageBox.Show("Hello");
        }

        private void mainForm_Deactivate(object sender, EventArgs e)
        {
            //aOthersMethod.KeyBoardClose();
            //aOthersMethod.NumberPadClose();
        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            resPlayer.Stop();
            LoadSettingsForm("reservation");
        }

        private void paidButton_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void discountButton_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void deliveryChargeButton_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void printButton_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void serviceChargeLabel_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void customerRecentItemsButton_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void orderAllClearButton_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void itemMinusButton_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void itemPlusButton_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void finalizeButton_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void billButton_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void menuTabControl_MouseClick(object sender, MouseEventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
        }

        private void mainForm_Leave(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (timer1.Enabled == false)
                {
                    return;
                }
                var type = new RestaurantInformationBLL().GetRestaurantInformation().RestaurantType.ToLower();
                if (type == "restaurant")
                {
                    ReservationBLL aReservationBll = new ReservationBLL(); DataTable reservation = aReservationBll.GetNewReservation();
                    IEnumerable<DataRow> query = (from myRow in reservation.AsEnumerable() select myRow);
                    DataTable boundTable = new DataTable();

                    if (!this.IsHandleCreated) this.CreateControl();
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.btnReservation.Visible = false;
                        if (query.Count() > 0)
                        {
                            this.btnReservation.Visible = true;
                            this.btnReservation.BringToFront(); this.btnReservation.Width = 53;
                            this.btnReservation.Text = query.Count() + "\n Res";
                            this.customerDetailsLabel.Location = new Point(this.btnReservation.Location.X,
                                this.customerDetailsLabel.Location.Y);
                            PlayResFile();
                        }
                        else
                        { 
                        
                        }
                    });

                    //.....................................................................................................
                    RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();

                    try
                    {
                        List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderForOnline(1, "pending");
                        if (!this.IsHandleCreated) this.CreateControl();
                        this.Invoke((MethodInvoker)delegate
                        {
                            //this.onlineOrderButton.Visible = false;

                            if (restaurantOrder.Count > 0)
                            {
                                this.onlineOrderButton.Text = restaurantOrder.Count + " " + "Order";
                               
                                PlayFile();
                            } 
                        });
                    }
                    catch (Exception exception)
                    {
                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                        aErrorReportBll.SendErrorReport(exception.ToString());
                        Console.WriteLine("Background Process: " + exception.Message + " " + DateTime.Now.TimeOfDay);
                    }
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender,
            System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(e);
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                aTimer.Elapsed -= OnlineOrderTimer_Tick;
                aTimer.Enabled = false;

            }
            catch (Exception)
            {

            }
        }

        internal void ShowOnlineOrderBtn()
        {
            onlineOrderButton.Visible = true;
        }

        private void totalAmountLabel_Click(object sender, EventArgs e)
        {

        }

        private void btnCartShowHidden_Click(object sender, EventArgs e)
        {

        }

        private void rightPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void customPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOrderSaveOnly_Click(object sender, EventArgs e)
        {
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            }

            if (orderDetailsflowLayoutPanel1.Controls.Count == 0)
            {
                MessageBox.Show("No items in the order!", "Save Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (aGeneralInformation.OrderId <= 0)
                {
                    DateTime stDate = DateTime.Now.Date;
                    DateTime endDate = stDate.AddDays(1);

                    aGeneralInformation.OrderType = String.IsNullOrEmpty(aGeneralInformation.OrderType)
                        ? aRestaurantInformation.DefaultOrderType
                        : aGeneralInformation.OrderType;
                    string[] time;
                    if (aGeneralInformation.OrderType == "DEL")
                    {
                        time = aRestaurantInformation.DeliveryTime.Split(' ');
                    }
                    else //if (aGeneralInformation.OrderType == "CLT")
                    {
                        time = aRestaurantInformation.CollectionTime.Split(' ');
                    }
                    aGeneralInformation.DeliveryTime = String.IsNullOrEmpty(aGeneralInformation.DeliveryTime)
                        ? DateTime.Now.AddMinutes(Convert.ToDouble(time[0])).ToString("HH:mm")
                        : aGeneralInformation.DeliveryTime;

                    RestaurantOrder aRestaurantOrder = GetRestaurantOrderForTableOrder();                    
                    int orderNo = aRestaurantOrderBLL.GetMaxOrderNumber(stDate, endDate);
                    aRestaurantOrder.OrderNo = orderNo;
                    OrderNo = orderNo;

                    aRestaurantOrder.Discount = 0.0;
                    aRestaurantOrder.Coupon = "";
                    aRestaurantOrder.DiscountAmount = aGeneralInformation.DiscountFlat;
                    if (aGeneralInformation.DiscountType != null)
                    {
                        aRestaurantOrder.Coupon = aGeneralInformation.DiscountType;
                        if (aGeneralInformation.DiscountType == "percent")
                        {
                            aRestaurantOrder.Discount = aGeneralInformation.DiscountPercent;
                        }
                        else
                        {
                            aRestaurantOrder.Discount = aGeneralInformation.DiscountFlat;
                        }
                    }

                    int result = aRestaurantOrderBLL.InsertRestaurantOrder(aRestaurantOrder);
                    List<OrderPackage> aOrder_Package = GetOrderPackage(result);
                    var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrder_Package);
                    List<OrderItem> aOrder_Items = GetOrderItem(result, result2);
                    aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrder_Items);
                     
                    aRestaurantOrderBLL.UpdateKitchenStatus(result);
                }
                else
                {
                    DeleteNewCancelItem(null, null, aGeneralInformation.OrderId);
                    List<OrderPackage> aOrder_Package = GetOrderPackage(aGeneralInformation.OrderId);
                    var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrder_Package);
                    List<OrderItem> aOrder_Items = GetOrderItem(aGeneralInformation.OrderId, result2);
                    aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrder_Items);

                    RestaurantOrder aRestaurantOrder = GetRestaurantOrderForTableOrder();

                    aRestaurantOrder.Status = "pending";
                    aRestaurantOrder.Id = aGeneralInformation.OrderId;

                    bool result = aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);

                    if (aGeneralInformation.TableId > 0)
                    {
                        RestaurantTable aRestaurantTable =
                            aRestaurantTableBll.GetRestaurantTableByTableId(aGeneralInformation.TableId);
                        aRestaurantTable.CurrentStatus = "busy";
                        aRestaurantTableBll.UpdateRestaurantTable(aRestaurantTable);
                    }
                     
                    aRestaurantOrderBLL.UpdateKitchenStatus(aRestaurantOrder.Id);               
                }

                ClearAllOrderInformation();
                ClearTableOrderDetails();
            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
            }
        }

        private void KitchenPrintBtn_Click(object sender, EventArgs e)
        {
            if (GlobalSetting.RestaurantInformation.ConfirmPayment > 0 && collectionButton.BackColor == Color.Black && timeButton.Text.ToLower() == "wait")
            {
                PaidAmountWithPrint(true, true);
            }
            else
            {
                PaidAmountWithPrint(false, true,false,false,true);
            }
        }

        private void OrderSaveOnlyBtn_Click(object sender, EventArgs e)
        {
            PaidAmountWithPrint(false, true, false, false, false,true);
        }

        private void backgroundWorkerForAutoPrint_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker helperBW = sender as BackgroundWorker;
            int arg = 1;
            //e.Result = AutoPrintOrders(helperBW, arg);
            if (helperBW.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        //private int AutoPrintOrders(BackgroundWorker bw, int a)
        private async void AutoPrintOrders(object source, ElapsedEventArgs e)
        {            
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            /* RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetAllAutoPrintOrder();
            foreach (RestaurantOrder _order in restaurantOrder)
            {
                CommonOrderDetials orderDetials = new CommonOrderDetials(_order);
                if (_order.ServedBy.Contains("-kitchen"))
                {
                    orderDetials.autoPrint(_order,true);
                }
                else
                {
                    orderDetials.autoPrint(_order);
                }
                _order.ServedBy = "done";
                bool res = aRestaurantOrderBLL.UpdateRestaurantOrder(_order);
            }
            Thread.Sleep(2000);
            //Console.WriteLine("I was doing some work in the background.");
            return result;*/
            //read a text file on network
            /*string networkName = Properties.Settings.Default.serverIp;
            if (networkName == "127.0.0.1")
            {
                networkName = System.Environment.MachineName;
            }*/
            //string networkPath = @"\\" + networkName + "\\" + Properties.Settings.Default.autoPrintLocation + "\\" + Properties.Settings.Default.autoPrint + ".txt";

            string networkPath = @Properties.Settings.Default.autoPrintLocation + "\\" + Properties.Settings.Default.autoPrint + ".txt";
            try
            {
                if(File.Exists(networkPath))
                {
                    string[] lines = File.ReadAllLines(networkPath);

                    File.Delete(networkPath);

                    Thread.Sleep(1000);

                    if (lines.Length > 0)
                    {
                        foreach (string line in lines)
                        {
                            string[] orderTxt = line.Trim().Split('-');

                            if (orderTxt.Length > 0)
                            {
                                int orderId = Int32.Parse(orderTxt[0]);

                                RestaurantOrder restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(orderId);
                                CommonOrderDetials orderDetails = new CommonOrderDetials(restaurantOrder);
                                //normal or kitchen print
                                orderDetails.autoPrint(restaurantOrder, orderTxt[1] == "kitchen");
                            }
                            //Thread.Sleep(1000);
                        }
                    }                    
                }
            } catch (Exception ex)
            {
                //return 0;
            }
            //Thread.Sleep(1000);
            //return 0;
        }

        private void backgroundWorkerForAutoPrint_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            backgroundWorkerForAutoPrint.RunWorkerAsync(e);
        }

        private void backgroundWorkerForAutoPrint_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }

        private void buttonShowRecentItems_Click(object sender, EventArgs e)
        {
            if (aGeneralInformation.CustomerId > 0)
            {
                if (recentItemsFlowLayoutPanel.Visible)
                {
                    recentItemsFlowLayoutPanel.Visible = false;
                }
                else
                {
                    recentItemsFlowLayoutPanel.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Please select a customer.");
            }
        }

        //private void backgroundWorkerForAutoPrint_RunWorkerCompleted(object sender,
        //    System.ComponentModel.RunWorkerCompletedEventArgs e)
        //{
        //    backgroundWorker1.RunWorkerAsync(e);
        //}
    }
}