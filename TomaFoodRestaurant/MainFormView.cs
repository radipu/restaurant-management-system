using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.VisualStyles;
using DevExpress.Data;
using DevExpress.Data.PLinq.Helpers;
using DevExpress.Data.WcfLinq.Helpers;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking.Helpers;
using DevExpress.XtraBars.Docking.Paint;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using FastFoodManagementSystem.BLL;
using Newtonsoft.Json;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.OtherForm;
using ContentAlignment = System.Drawing.ContentAlignment;
using TileGroup = DevExpress.XtraEditors.TileGroup;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraRichEdit;
using Microsoft.PointOfService;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using TomaFoodRestaurant.DAL;
using TomaFoodRestaurant.Report;
using TomaFoodRestaurant.Sequrity;
using AutoSizeMode = System.Windows.Forms.AutoSizeMode;
using Timer = System.Windows.Forms.Timer;
using System.Drawing.Printing;
using PriceCalculation = TomaFoodRestaurant.DAL.CommonMethod.PriceCalculation;

namespace TomaFoodRestaurant
{
    public partial class MainFormView : XtraForm
    {
        public MainFormView()
        {
            InitializeComponent();

        }

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

                MessageBox.Show("Please check till configuration.", "Till Open Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }

        }

        #endregion

        System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Ring/audio.wav");
        System.Media.SoundPlayer resPlayer = new System.Media.SoundPlayer(@"Ring/resAudio.wav");

       
        public GeneralInformation aGeneralInformation = new GeneralInformation();
        static List<ReceipeCategoryButton> GetAllCatergory = new List<ReceipeCategoryButton>();
        public static List<ReceipeMenuItemButton> AllRecipeButton = new List<ReceipeMenuItemButton>();
        static List<ReceipeMixType> ReceipeMixType = new List<ReceipeMixType>();
        public static List<OrderItemDetailsMD> aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
        public static List<RecipeMultipleMD> aRecipeMultipleMdList = new List<RecipeMultipleMD>();
        List<RecipeOptionMD> aRecipeOptionMdList = new List<RecipeOptionMD>();
        public static List<RecipePackageMD> aRecipePackageMdList = new List<RecipePackageMD>();
        List<RecipeTypeDetails> aRecipeTypes = new List<RecipeTypeDetails>();
        public static List<MultipleItemMD> aMultipleItemMdList = new List<MultipleItemMD>();
        public static List<PackageItem> aPackageItemMdList = new List<PackageItem>();
        List<RestaurantUsers> aRestaurantUserForSearchCustomer = new List<RestaurantUsers>();

        public List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
        //List<OptionItemView> viewOptionList = new List<OptionItemView>();
        List<SearchUserCustome> aSearchUserCustom = new List<SearchUserCustome>();
        List<PackageItem> packageItem = new List<PackageItem>();
        private bool rowPackageItemFocus = false;
        public Screen Screen;
        public DataTable GetAllRecipeForPackage = new DataTable();
        public RestaurantUsers restaurantUsers = new RestaurantUsers();

        private PackageItemFormLoadNew aItemFormLoadNew = null;

        private DataTable ReceipeTypeList = null;
        OthersMethod aOthersMethod = new OthersMethod();
        public static bool SetDefaultPrinter(string defaultPrinter)
        {
           return PrinterInformation.SetDefault(defaultPrinter);
        }

        public void callToTpos(string number)
        {
            // currentTimeLabel.Text = DateTime.Now.ToString("hh:mm:ss tt");
            // string number = //File.ReadAllText("Config/call.txt");

            try
            {
                if (number.Length > 0)
                {
                    number = number.Trim();
                }

                if (number != null && number != "")
                {
                    number = GetExactNumber(number);
                    if (number != "")
                    {

                        if (number == customerTextBox.Text.Trim()) return;
                        string phoneNumber = number;
                        RestaurantUsers aRestaurantUser = FindRestaurantUser(phoneNumber);
                        if ((customerTextBox.Text == "Search Customer" || customerTextBox.Text.Trim() == "") &&
                            IsCartEmpty())
                        {
                            isCustomerTextChanged = false;
                            customerTextBox.Text = number;
                            //   File.WriteAllText("Config/call.txt", "");
                            if ((aRestaurantUser == null || aRestaurantUser.Id <= 0) && number.Count() > 0)
                            {
                                if (Application.OpenForms.OfType<CustomerEntryForm>().Count() <= 0)
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
                            callingPannel.Visible = true;

                            if (aRestaurantUser != null && aRestaurantUser.Id > 0)
                            {
                                customerLoadLabel.Text = aRestaurantUser.Firstname + " " + aRestaurantUser.Lastname;

                                if (customerLoadLabel.Text.Length <= 0)
                                {
                                    customerLoadLabel.Text = phoneNumber;
                                }
                                customerPhnLabel.Text = phoneNumber;
                                customerLoadButtonNew.Text = customerLoadLabel.Text + "\r\nis calling...";
                            }
                            else if ((aRestaurantUser == null || aRestaurantUser.Id <= 0) && number.Count() > 0)
                            {
                                customerLoadLabel.Text = phoneNumber;
                                customerPhnLabel.Text = phoneNumber;
                                customerLoadButtonNew.Text = customerLoadLabel.Text + "\r\nis calling...";
                            }
                            // File.WriteAllText("Config/call.txt", "");
                        }
                    }
                }

                LoadAmountDetails();


                if (aGeneralInformation.CustomerId > 0)
                {
                    customerRecentItemsButton.Visible = true;
                }
                else
                {
                    customerRecentItemsButton.Visible = false;
                }
            }
            catch (Exception ex)
            {
                
                new ErrorReportBLL().SendErrorReport(DateTime.Now+" "+ex.GetBaseException());
                this.Activate();
            }
           
        }

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

        private string GetExactNumber(string szCallerID)
        {
            string mystr = Regex.Replace(szCallerID, @"[^0-9]", "");// Regex.Replace(szCallerID, @"[^\w\d\s]", "");
               
            string result = string.Concat(Enumerable.Reverse(mystr));
            string str = "";
            if (result.Count() >= 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    str += result[i];
                }
                if (str[str.Length - 1] != '0')
                    str += "0";
            }

            return string.Concat(Enumerable.Reverse(str));



        }

        private bool IsCartEmpty()
        {
            if (gridViewAddtocard.RowCount <= 0) return true;
            return false;
        }

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

        public double GetTotalAmountDetails()
        {
            //double amount1 = aOrderItemDetailsMDList.Sum(a => a.Qty * a.Price);
            //double amount2 = aRecipePackageMdList.Sum(a => a.Qty * a.UnitPrice);
            //double amount3 = aPackageItemMdList.Sum(a => a.Qty * a.Price);
            //double amount4 = aRecipeMultipleMdList.Sum(a => a.Qty * a.UnitPrice);
            //return (amount1 + amount2 + amount3 + amount4);

            return (double)ShopingCard();
        }

        public void CardButtonSetting(object sender, EventArgs e)
        {
            Button clickButton = (sender) as Button;
            if (clickButton != null && clickButton.Text == "CLT")
            {
                ChangeButtonLocation(false);
                collectionButton.BackColor = Color.Black;
                deliveryButton.BackColor = Color.WhiteSmoke;
                collectionButton.ForeColor = Color.WhiteSmoke;
                deliveryButton.ForeColor = Color.Black;

                aGeneralInformation.DeliveryCharge = 0;
                deliveryChargeButton.Text = "Delivery Charge\r\n0";
                aGeneralInformation.OrderType = "CLT";
                //LoadAmountDetails();
                CustomerBLL aCustomerBll = new CustomerBLL();
                if (aGeneralInformation.CustomerId > 0)
                {


                    RestaurantUsers aRestaurantUser = aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
                    string address = GetCustomerDetails(aRestaurantUser);
                    customerDetailsLabel.Text = address;
                }
            }
            else if (clickButton != null && clickButton.Text == "DEL")
            {
                aOthersMethod.KeyBoardClose();
                aOthersMethod.NumberPadClose();

                if (deliveryButton.Text != "RES")
                {
                    collectionButton.BackColor = Color.WhiteSmoke;
                    deliveryButton.BackColor = Color.Black;
                    deliveryButton.ForeColor = Color.WhiteSmoke;
                    collectionButton.ForeColor = Color.Black;// discountButton.Enabled = false;
                    double totalAmount = GetTotalAmountDetails();
                    aGeneralInformation.DeliveryCharge = totalAmount >= aRestaurantInformation.MinOrder
                        ? 0
                        : aRestaurantInformation.DeliveryCharge;
                    deliveryChargeButton.Text = aGeneralInformation.DeliveryCharge <= 0
                        ? "Delivery Charge\r\n0"
                        : "Delivery Charge\r\n" + aGeneralInformation.DeliveryCharge.ToString();
                    aGeneralInformation.OrderType = "DEL";
                    timeButton.Text = "Time";
                    ChangeButtonLocation(true);

                    if (aGeneralInformation.CustomerId > 0)
                    {
                        CustomerBLL aCustomerBll = new CustomerBLL();
                        RestaurantUsers aRestaurantUser = aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
                        LoadDeliveryCharge(aRestaurantUser);
                    }


                    //LoadAmountDetails();
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
                    RestaurantUsers aRestaurantUser = aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);


                    string customerAddress = GetCustomerDetails(aRestaurantUser);
                    customerDetailsLabelNew.Text = customerAddress;
                }
            }
            ShopingCard();
        }

        public DataTable dataTable = new DataTable();
        private DataRow dr;
        GridGroupSummaryItem item = new GridGroupSummaryItem();
        GlobalUrl urls = new GlobalUrl();
        bool orderLoadStatus = true;
        
        private TomaFoodRestaurant.OtherForm.Form1 aAdForm = null;
        RestaurantInformation restaurantInformation = new RestaurantInformationBLL().GetRestaurantInformation();
        public void InitLoadToCard()
        {
            try
            {
                GlobalSetting.RestaurantInformation = restaurantInformation;
                AllCardSytemClear();
                LoadExtraPrice();

            if (Properties.Settings.Default.callerID == "AD101")
            {
                try
                {
                    // used for AD101 caller id acivation

                    if (!Application.OpenForms.OfType<Form1>().Any())
                    {

                        aAdForm = new Form1(this, null);
                        aAdForm.WindowState = FormWindowState.Minimized;
                        aAdForm.Size = new Size(1024, 500);
                        aAdForm.Show();
                        aAdForm.Visible = false;
                    }

                }
                catch (Exception ex)
                {
                    File.AppendAllText("Config/log.txt", "CallerID LOg : " + ex.Message + "\n\n");
                }


            }

            tillOpenButton.Visible = false;
            if (Properties.Settings.Default.cashDrawer && GlobalSetting.SettingInformation.till=="Enable")
            {
                tillOpenButton.Visible = true;
            }
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();
            Screen = Screen.PrimaryScreen;
            gridViewAddtocard.OptionsView.AllowHtmlDrawGroups = true;
            gridViewAddtocard.OptionsView.ShowGroupExpandCollapseButtons = true;

            dataTable.Clear();
            dataTable.Columns.Add(new DataColumn("Index", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Cat"));
            dataTable.Columns.Add(new DataColumn("QTY", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Name"));
            dataTable.Columns.Add(new DataColumn("Price", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("Total", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("RecepiMenuId", typeof(int)));
            dataTable.Columns.Add(new DataColumn("ReceipTypeId", typeof(int)));
            dataTable.Columns.Add(new DataColumn("EditName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("OptionId", typeof(List<OptionJson>)));
            dataTable.Columns.Add(new DataColumn("Group", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Package", typeof(List<PackageItem>)));
            dataTable.Columns.Add(new DataColumn("GroupName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("OptionJson", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SortOrder", typeof(int)));
            dataTable.Columns.Add(new DataColumn("kitichineDone", typeof(int)));


            gridViewAddtocard.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(gridView1_CustomDrawRowIndicator_1);
            // gridViewOption.CustomDrawGroupRow -= GridViewCart_CustomDrawGroupRow;
            //gridViewAddtocard.CustomDrawGroupRow += GridViewCart_CustomDrawGroupRow;
            gridViewAddtocard.GroupCount = -1; gridControlAddTocard.DataSource = dataTable;
            gridViewAddtocard.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridViewAddtocard.Columns["Index"].SortOrder = ColumnSortOrder.Descending;
            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum);
            gridViewAddtocard.Columns["Total"].Summary.Add(item1);
            gridViewAddtocard.Columns["QTY"].Summary.Add(item1);
            aTimer = new System.Timers.Timer(10000);
            onlineOrderButtonNew.Visible = false;


            if (aRestaurantInformation.MultiplePart==0)
            {
                multiItembtn.Visible = false;
            }
            if (OthersMethod.CheckForInternetConnection())
            {
                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
              
                if (restaurantInformation.IsSyncOrder > 0)
                {
                    OrderSyncronize();
                }

                if (GlobalSetting.IsLicenseUpdate)
                {
                    UpdateRestaurantLicense();
                    GlobalSetting.IsLicenseUpdate = false;

                }
                onlineOrderButtonNew.Visible = false;
                if (GlobalSetting.SettingInformation.onlineConnect == "Active")
                {
                    onlineOrderButtonNew.Visible = true;
                    aTimer.Enabled = true;
                    aTimer.Elapsed += onlineOrderTimer_Tick;
                    onlineOrderSync();
                    
                    //OnlineReservation();
                }
                if (GlobalSetting.SettingInformation.IsReservationCheck)
                {
                    GetReservation(restaurantInformation);
                    if (restaurantInformation.RestaurantType.ToLower() == "restaurant")
                    {
                        resTimer = new System.Timers.Timer(10 * 60000);
                        //resTimer = new System.Timers.Timer(500);
                        resTimer.Enabled = true;
                        resTimer.Elapsed += timerReservation_Tick;
                    }
                }
                }

          
                
                //if (GlobalSetting.RestaurantInformation.IsSyncCustomer > 0 && Properties.Settings.Default.deviceType == "SERVER")
                //{

                //    DateTime currentTime = DateTime.Now;
                //    DateTime reportClosingTime = DateTime.Today;
                //    if (GlobalSetting.RestaurantInformation.ReportClosingHour > 12)
                //    {
                //        reportClosingTime = reportClosingTime.AddHours(-(24 - GlobalSetting.RestaurantInformation.ReportClosingHour));
                //        reportClosingTime = reportClosingTime.AddMinutes(GlobalSetting.RestaurantInformation.ReportClosingMin);
                //    }
                //    else{
                //        reportClosingTime = reportClosingTime.AddHours(GlobalSetting.RestaurantInformation.ReportClosingHour);
                //        reportClosingTime = reportClosingTime.AddMinutes(GlobalSetting.RestaurantInformation.ReportClosingMin);
                //    }


                //    if (currentTime > reportClosingTime)
                //    {

                //        RestaurantOrderBLL aRestaurantOrderBll = new RestaurantOrderBLL();
                //        string result = aRestaurantOrderBll.DeleteAllOrderByDate(reportClosingTime);

                //    }
                //    else
                //    {

                //        RestaurantOrderBLL aRestaurantOrderBll = new RestaurantOrderBLL();
                //        string result = aRestaurantOrderBll.DeleteAllOrderByDate(reportClosingTime.AddDays(-1));
                //    }
                //}
                    }
            catch (Exception ex) 
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                this.Activate();
                
            }
          

        }



        private void gridView1_CustomDrawRowIndicator_1(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (gridView1.IsGroupRow(e.RowHandle))
            {
                e.Info.ImageIndex = -1;
            }
        }

        Timer grapTimer = new Timer();

        // used for thread
        System.Timers.Timer aTimer;
        System.Timers.Timer resTimer;
     private async void onlineOrderTimer_Tick(object sender, EventArgs e)
        {

            if (aTimer.Enabled == false)
            {

                return;
            }

            try
            {
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();

                List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderForOnline(1, "pending");


                if (restaurantOrder.Count > 0)
                {
                    if (onlineOrderButtonNew.InvokeRequired)
                    {
                        onlineOrderButtonNew.Invoke(new MethodInvoker(delegate
                        {
                            onlineOrderButtonNew.Text = restaurantOrder.Count + " Order";
                        }));
                    }
                    PlayFile();
                    await onlineTimer();
                }
            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());

                this.Activate();

            }

        }

        public async Task<string> orderBtn_Tick()
        {
            for (int i = 0; i < 40; i++)
            {
                await Task.Delay(100);
                if (onlineOrderButtonNew.BackColor == Color.Red)
                {
                    onlineOrderButtonNew.BackColor = Color.White;
                    onlineOrderButtonNew.ForeColor = Color.Black;
                }
                else
                {
                    onlineOrderButtonNew.BackColor = Color.Red;
                    onlineOrderButtonNew.ForeColor = Color.White;
                }                                                     
            }
            return "0";
            //   Invalidate();
        }

        private void PlayFile()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Ring/audio.wav");
            player.Play();

        }

        private static RestaurantInformation RestaurantInformation;
        private async void onlineOrderSync()
        {
            try
            {
                if (RestaurantInformation == null)
                {
                    RestaurantInformation = new RestaurantInformationBLL().GetRestaurantInformation();
                }

                RealTimeData realTimeData = new RealTimeData();
                await
                    Task.Run(() =>
                            realTimeData.OpenSSEStream("https://" + RestaurantInformation.Website + "/restaurants/check_online_order/" +
                                                       RestaurantInformation.Id, this ,null));
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                this.Activate();
            }

        }

        private void SaveOnlineReservation(List<Reservation> reservations)
        {
            try
            {
                ReservationBLL aReservationBll = new ReservationBLL();
                foreach (Reservation reservation in reservations)
                {                 
                    Reservation tempReservation = aReservationBll.GetBookingByOnlineReservationId(reservation.online_reservation_id);
                    if (tempReservation.ReserveId <= 0 || reservation.online_reservation_id <= 0)
                    {                                                                               
                        int result = aReservationBll.InsertOnlineReservation(reservation, true);
                    }                   
                }
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.Message);
            }
        }

        private void GetReservation(RestaurantInformation aRestaurantInformation)
        {
            string text = "";
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(urls.AcceptUrl + "restaurantcontrol/request/crud/get_reservation/" + aRestaurantInformation.Id);
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
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                this.Activate();
            }
            string json = text;
            try
            {
                if (json != "")
                {
                    dynamic objects = JArray.Parse(json);
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
                this.Activate();

            }


        }
        public async Task<string> timerBtn_Tick()
        {

            for (int i = 0; i < 200000; i++)
            {
                await Task.Delay(100);
                if (btnReservationNew.BackColor == Color.Gold)
                {
                    btnReservationNew.BackColor = Color.White;
                }
                else
                {
                    btnReservationNew.BackColor = Color.Gold;
                }
            }

            return "0";
            //   Invalidate();
        }
        public async Task<string> onlineTimer()
        {

            for (int i = 0; i < 10000; i++)
            {
                await Task.Delay(1000);

            }

            return "0";
            //   Invalidate();
        }
        private async void timerReservation_Tick(object sender, EventArgs e)
        {

            resTimer.Stop();
            GetReservation(aRestaurantInformation);

            resTimer.Enabled = true;
            //    Application.DoEvents();
            try
            {
                ReservationBLL aReservationBll = new ReservationBLL();
                DataTable reservation = aReservationBll.GetNewReservation();
                IEnumerable<DataRow> query = (from myRow in reservation.AsEnumerable() select myRow);
                DataTable boundTable = new DataTable();

                if (query.Count() > 0)
                {

                    AppendTextBoxReservation(query.Count().ToString());
                    PlayResFile();
                    await timerBtn_Tick();
                }
                else
                {
                    AppendTextBoxReservation(query.Count().ToString());
                }
            }

            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
                //  MessageBox.Show(exception.Message);
                this.Activate();
            }

        }

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
        Task orderSync;
        private async void OrderSyncronize()
        {
            try
            {
                //  await orderSync.Run(() => aOrderSyncroniseBll.OrderSyncronise("all"));
                if (!(orderSync != null && (orderSync.Status == TaskStatus.Running || orderSync.Status == TaskStatus.WaitingToRun || orderSync.Status == TaskStatus.WaitingForActivation)))
                {
                    OrderSyncroniseBLL aOrderSyncroniseBll = new OrderSyncroniseBLL();
                    orderSync = Task.Factory.StartNew(() =>
                    {
                        aOrderSyncroniseBll.OrderSyncronise("all");
                    });
                }

            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
                this.Activate();

            }

        }


        //private void customerOrOrderSyncTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        customerOrOrderSyncTimer.Stop();

        //        if (GlobalSetting.IsLicenseUpdate)
        //        {
        //            UpdateRestaurantLicense();
        //            GlobalSetting.IsLicenseUpdate = false;

        //        }

        //        RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
        //        RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
        //        if (aRestaurantInformation.IsSyncOrder > 0)
        //        {
        //            OrderSyncronize();
        //        }

        //        aTimer = new System.Timers.Timer(10000);
        //        aTimer.Elapsed += onlineOrderTimer_Tick;
        //        aTimer.Enabled = true;

        //        if (aRestaurantInformation.IsSyncCustomer > 0)
        //        {
        //            CustomerSyncronize();
        //        }


        //        customerOrOrderSyncTimer.Enabled = true;
        //    }
        //    catch (Exception exception)
        //    {
        //        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
        //        aErrorReportBll.SendErrorReport(exception.ToString());
        //        this.Activate();

        //    }
        //}

         private void PlayResFile()
        {
            resPlayer.Play();
        }


        public void OtherInformation()
        {
            if (OthersMethod.CheckForInternetConnection())
            {
                //  // used for online order
                //  //    if (GlobalSetting.RestaurantUsers.CheckOnlineOrder > 0)
                //  //  {
                //  //aTimer = new System.Timers.Timer(10000);
                // // aTimer.Elapsed += onlineOrderTimer_Tick;
                //  //aTimer.Enabled = true;
                ////  onlineOrderSync();
                //  // }
                //  //
                //  RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                //  RestaurantInformation restaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                //  if (restaurantInformation.RestaurantType.ToLower() == "restaurant")
                //  {
                //      // used for reservation
                //      resTimer = new System.Timers.Timer(10 * 60000); //5 * 6
                //      resTimer.Elapsed += timerReservation_Tick;resTimer.Enabled = true;


                //  }

                //  // used for customer syncronize
                //  customerOrOrderSyncTimer = new System.Timers.Timer(10000);
                //  customerOrOrderSyncTimer.Elapsed += customerOrOrderSyncTimer_Tick;
                //  customerOrOrderSyncTimer.Enabled = true;

            }
        }

        public static Size WindowSize
        {
            get
            {

                var size = Screen.PrimaryScreen.Bounds.Size;

                return size;

            }

        }

        private FlowLayoutPanel dynamicTableLayoutPanel = new FlowLayoutPanel();

        private void cardPannel_Paint(object sender, PaintEventArgs e)
        {

        }
        public DataTable GetPackageCategory = new DataTable();

        public void PackageLoad()
        {
            PackageBLL packageBll = new PackageBLL();

            GetPackageCategory = packageBll.GetPackageCategory1();

            GetAllRecipeForPackage = packageBll.GetAllRecipe();


        }

        public void LoadAllData()
        {
            ReceipeMixType.Clear();
            AllRecipeButton.Clear();
            GetAllCatergory.Clear();
            ReceipeTypeList = new PrinterSetupBLL().GetAllReceipeTypeForPrint();

            // Package Data .....................
            PackageLoad();


            // All Type 
            List<ReceipeTypeButton> aReceipeTypeButton = new RestaurantMenuBLL().GetRecipeType().ToList();

            foreach (var typeButton in aReceipeTypeButton)
            {
                var TypeButton =
                    new RestaurantMenuBLL().GetAllCategory()
                        .FirstOrDefault(a => a.HasSubcategory == 1 & a.ReceipeTypeId == typeButton.TypeId);

                if (TypeButton != null)
                {
                    ReceipeMixType.Add(new ReceipeMixType
                    {
                        ReceipeTypeButton = typeButton,
                        SortOder = TypeButton.SortOrder,
                        Type = "Type"
                    });

                }

            }


            //// Has SubCatgory 0
            List<ReceipeCategoryButton> GetSubCatergorys = new RestaurantMenuBLL().GetAllCategory().Where(a => a.HasSubcategory == 0).ToList();

            foreach (var zeroCatory in GetSubCatergorys)
            {

                //  GetAllCatergory.Add(zeroCatory);
                ReceipeMixType.Add(new ReceipeMixType
                {
                    CategoryButton = zeroCatory,
                    Type = "SubCat",
                    SortOder = zeroCatory.SortOrder
                });


            }






            // All RecipeButton //
            List<ReceipeMenuItemButton> getallsuButtons = new RestaurantMenuBLL().AllRecipeButton().ToList();

            foreach (var recipButton in getallsuButtons)
            {
                AllRecipeButton.Add(recipButton);
            }
            // All SubCatogryAdd

            List<ReceipeCategoryButton> getAllCatgory = new RestaurantMenuBLL().GetAllCategory().ToList();

            foreach (var recipButton in getAllCatgory)
            {
                GetAllCatergory.Add(recipButton);
            }



        }


        public void ResponsiveItem(int column, bool cardVisibleInvisible, TileControl tileControl1)
        {

            var size = Screen.PrimaryScreen.Bounds;
            if (size.Width == 1024)
            {

                if (cardVisibleInvisible)
                {
                    tileControl1.ItemPadding = new Padding(1);

                    tileControl1.ColumnCount = 12;
                    if (column > 150)
                    {
                        tileControl1.ColumnCount = 10;
                    }

                    if (column > 5)
                    {
                        tileControl1.ItemSize = (size.Width / 18) + 1;
                        ////////////// Overflow on Item Add on Screen

                    }
                    else if (column > 4)
                    {
                        tileControl1.ItemSize = (size.Width / 15) + 2;
                    }
                    else if (column > 3)
                    {
                        tileControl1.ItemSize = (size.Width / 12) + 1;
                    }
                    else if (column > 2)
                    {
                        tileControl1.ItemSize = (size.Width / 9) + 1;
                    }
                }
                else
                {
                    tileControl1.ColumnCount = 16;
                    if (column > 150)
                    {
                        tileControl1.ColumnCount = 14;
                    }


                    if (column > 5)
                    {

                        tileControl1.ItemSize = (size.Width / 16) - 2;

                    }
                    else if (column > 4)
                    {

                        tileControl1.ItemSize = (size.Width / 10);
                    }
                    else if (column > 3)
                    {
                        tileControl1.ItemSize = (size.Width / 8) - 1;
                    }
                    else if (column > 2)
                    {
                        tileControl1.ItemSize = (size.Width / 6) - 1;
                    }
                }



            }
            else if (size.Width >= 1280)
            {
                if (cardVisibleInvisible)
                {
                    tileControl1.ColumnCount = 14;
                    if (column > 150)
                    {
                        tileControl1.ColumnCount = 12;
                    }


                    if (column > 5)
                    {
                        tileControl1.ItemSize = (size.Width / 18) - 2;

                    }
                    else if (column > 4)
                    {

                        tileControl1.ItemSize = (size.Width / 13) - 2;
                    }
                    else if (column > 3)
                    {
                        tileControl1.ItemSize = (size.Width / 10) - 8;
                    }
                    else if (column > 2)
                    {
                        tileControl1.ItemSize = (size.Width / 7) - 22;
                    }
                }
                else
                {
                    tileControl1.ColumnCount = 18;

                    if (column > 150)
                    {
                        tileControl1.ColumnCount = 16;
                    }

                    if (column > 5)
                    {
                        tileControl1.ItemSize = (size.Width / 18) - 0;

                    }
                    else if (column > 4)
                    {

                        tileControl1.ItemSize = (size.Width / 13) - 2;
                    }
                    else if (column > 3)
                    {
                        tileControl1.ItemSize = (size.Width / 10) - 2;
                    }
                    else if (column > 2)
                    {
                        tileControl1.ItemSize = (size.Width / 7) - 22;
                    }

                }



            }

        }
        public void ResponsiveItem1(int column, bool cardVisibleInvisible, TileControl tileControl1)
        {
            var size = tileControl1.Bounds;
            // var size = Screen.PrimaryScreen.Bounds;

            if (cardVisibleInvisible)
            {
                tileControl1.ColumnCount = (size.Width / column) / 2;
                if (column > 5)
                {
                    tileControl1.ItemSize = (size.Width / 18) - 2;

                }
                else if (column > 4)
                {

                    tileControl1.ItemSize = (size.Width / 13) - 2;
                }
                else if (column > 3)
                {
                    tileControl1.ItemSize = (size.Width / 10) - 8;
                }
                else if (column > 2)
                {
                    tileControl1.ItemSize = (size.Width / 7) - 22;
                }
            }
            else
            {
                tileControl1.ColumnCount = (size.Width / column) / 2;

                if (column > 5)
                {
                    tileControl1.ItemSize = (size.Width / 18) - 0;

                }
                else if (column > 4)
                {

                    tileControl1.ItemSize = (size.Width / 13) - 2;
                }
                else if (column > 3)
                {
                    tileControl1.ItemSize = (size.Width / 10) - 2;
                }
                else if (column > 2)
                {
                    tileControl1.ItemSize = (size.Width / 7) - 22;
                }

            }
        }
        public void ResponsiveItemOption(int column, bool cardVisibleInvisible, TileControl tileControl1)
        {
            var size = Screen.PrimaryScreen.Bounds;
            if (size.Width == 1024)
            {
                if (cardVisibleInvisible)
                {
                    tileControl1.ItemPadding = new Padding(1);
                    tileControl1.ColumnCount = 6;
                    if (column > 5)
                    {
                        tileControl1.ItemSize = (size.Width / 20) + 1;

                    }
                    else if (column > 4)
                    {

                        tileControl1.ItemSize = (size.Width / 18) + 2;
                    }
                    else
                    {
                        tileControl1.ItemSize = (size.Width / 19) + 2;
                    }
                    //else if (column > 3){
                    //    tileControl1.ItemSize = (size.Width / 15) + 1;
                    //}
                    //else if (column >= 2){
                    //    tileControl1.ItemSize = (size.Width / 15) + 1;
                    //}
                }
                else
                {
                    tileControl1.ColumnCount = 10;
                    if (column > 5)
                    {
                        tileControl1.ItemSize = (size.Width / 16) - 2;

                    }
                    else if (column > 4)
                    {

                        tileControl1.ItemSize = (size.Width / 10);
                    }
                    else if (column > 3)
                    {
                        tileControl1.ItemSize = (size.Width / 8) - 1;
                    }
                    else if (column > 2)
                    {
                        tileControl1.ItemSize = (size.Width / 6) - 1;
                    }
                }



            }
            else
            {
                if (cardVisibleInvisible)
                {
                    tileControl1.ColumnCount = 8;
                    if (column > 5)
                    {
                        tileControl1.ItemSize = (size.Width / 20) + 1;

                    }
                    else if (column > 4)
                    {

                        tileControl1.ItemSize = (size.Width / 18) + 2;
                    }
                    else
                    {
                        tileControl1.ItemSize = (size.Width / 19) + 2;
                    }}
                else
                {
                    tileControl1.ColumnCount = 8;


                    if (column > 5)
                    {
                        tileControl1.ItemSize = (size.Width / 18) - 0;

                    }
                    else if (column > 4)
                    {

                        tileControl1.ItemSize = (size.Width / 15) - 2;
                    }
                    else if (column > 3)
                    {
                        tileControl1.ItemSize = (size.Width / 18) - 2;
                    }
                    else if (column > 2)
                    {
                        tileControl1.ItemSize = (size.Width / 18) - 2;
                    }
                }



            }

        }

        public bool LoadMenuType()
        {
            try
            {
                pannelTopBar.Visible = false;
                tileGroup1.Items.Clear();
                tileControl1.Controls.Clear();
                optionPanel.Controls.Clear();
                optionPanel.Visible = false;
              var list = ReceipeMixType.OrderBy(a => a.SortOder).ToList();
                ResponsiveItem(list.Count, dockPanel1.Visible, tileControl1);

                foreach (ReceipeMixType receipeMixType in list)
                {
                    AddMixType(receipeMixType);

                }List<RecipePackageButton> aRecipePackageButtons = new RestaurantMenuBLL().GetPackageByMenuType(0);
                     if (aRecipePackageButtons.Count>0)
                {
                    LoadIntialPackage();}
               

                return true;
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                Application.Exit();
            }
            return false;

        }


        private void LoadIntialPackage()
        {


            CartItem aTileItem = new CartItem();

            aTileItem.Text = "Package";
            aTileItem.ItemSize = TileItemSize.Wide;
            aTileItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
            aTileItem.Appearance.BackColor = ColorTranslator.FromHtml("#1071ed");


            aTileItem.Appearance.Font = DefaultFont;
            aTileItem.Appearance.BorderColor = Color.Transparent;

            aTileItem.ItemClick += (sender, args) =>
            {

                ReceipeTypeButton topPackageButton = new ReceipeTypeButton();
                topPackageButton.Text = aTileItem.Text;
                topPackageButton.TypeId = aTileItem.TypeId;
                topPackageButton.Margin = new Padding(0);
                topPackageButton.Padding = new Padding(0);
                topPackageButton.AutoSize = true;
                topPackageButton.AutoSizeMode = AutoSizeMode.GrowOnly;

                topPackageButton.Height = pannelTopBar.Height - 2;
                topPackageButton.Font = aTileItem.Appearance.Font;
                topPackageButton.BackColor = aTileItem.Appearance.BackColor;
                topPackageButton.ForeColor = Color.White;
                topPackageButton.FlatStyle = FlatStyle.Flat;
                flowLayoutPanel1.Controls.Clear();

                topPackageButton.Click += (o, eventArgs) =>
                {
                    LoadMenuType();

                };
                flowLayoutPanel1.Controls.Add(topPackageButton);

                pannelTopBar.Visible = true;



                List<RecipePackageButton> aRecipePackageButtons = new RestaurantMenuBLL().GetPackageByMenuType(0);

                tileGroup1.Items.Clear();


                ResponsiveItem(aRecipePackageButtons.Count, dockPanel1.Visible, tileControl1);

                LoadPackage(aRecipePackageButtons);
            };

            tileGroup1.Items.Add(aTileItem);


            //ReceipeTypeButton receipeTypeButton = new ReceipeTypeButton();
            //receipeTypeButton.TypeId = aReceipeSubCategory.CategoryButton.ReceipeTypeId;
            //List<RecipePackageButton> aRecipePackageButtons = aPackageBll.GetPackageByRecipeType(receipeTypeButton);

            //if (aRecipePackageButtons.Count > 0)
            //{
            //    if (packageAdd)
            //    {
            //        LoadPackage(aRecipePackageButtons);
            //        packageAdd = false;
            //    }

            //}
        }
        List<PackageItem> definedPackageItem = new List<PackageItem>();

        public void LoadPackage(List<RecipePackageButton> package)
        {

            foreach (RecipePackageButton packageButton in package)
            {
                CartItem tempRecipePackageButton = new CartItem();
                tempRecipePackageButton.ItemSize = TileItemSize.Wide;
                tempRecipePackageButton.RecipePackageButton = packageButton;
                tempRecipePackageButton.Text = packageButton.PackageName;
                tempRecipePackageButton.TextAlignment = TileItemContentAlignment.MiddleCenter;
                tempRecipePackageButton.Appearance.BackColor = Color.CadetBlue;
                tempRecipePackageButton.Appearance.BorderColor = Color.Transparent;
                tempRecipePackageButton.Appearance.Font = packageButton.Font;
                tempRecipePackageButton.AppearanceItem.Selected.Font = packageButton.Font;

                var mainPackageButton = packageButton;
                tempRecipePackageButton.ItemClick += (sender, args) =>
                {
                    LoadPackageItemCategory(mainPackageButton, tempRecipePackageButton);

                };

                tileGroup1.Items.Add(tempRecipePackageButton);

            }

        }

        private void LoadPackageItemCategory(RecipePackageButton mainPackageButton, CartItem tempRecipePackageButton)
        {

            definedPackageItem = new List<PackageItem>();
            try
            {
                PackageItemForm.status = "";

                if (mainPackageButton != null && mainPackageButton.CustomPackage <= 0)
                {
                    LoadPackageCategory(mainPackageButton);
                    AddToCardFixedItem(definedPackageItem, mainPackageButton, null, mainPackageButton.PackageUpdateOrNot);
                    return;
                }

                mainPackageButton.Margin = new Padding(0);
                mainPackageButton.Padding = new Padding(0);
                mainPackageButton.AutoSize = true;

                mainPackageButton.AutoSizeMode = AutoSizeMode.GrowOnly;

                mainPackageButton.Height = pannelTopBar.Height - 2;
                mainPackageButton.Font = tempRecipePackageButton.Appearance.Font;
                mainPackageButton.BackColor = tempRecipePackageButton.AppearanceItem.Normal.BackColor;
                mainPackageButton.ForeColor = Color.White;
                mainPackageButton.FlatStyle = FlatStyle.Flat;
                mainPackageButton.Click += (o, eventArgs) =>
                {
                    List<RecipePackageButton> aRecipePackageButtons = new RestaurantMenuBLL().GetPackageByMenuType(0);
                    tileGroup1.Items.Clear();
                    ResponsiveItem(aRecipePackageButtons.Count, dockPanel1.Visible, tileControl1);
                    LoadPackage(aRecipePackageButtons);
                    tileControl1.Controls.Clear();
                    flowLayoutPanel1.Controls.Remove(mainPackageButton);
                };


                flowLayoutPanel1.Controls.Add(mainPackageButton);
                tileControl1.Controls.Clear();
                aItemFormLoadNew = new PackageItemFormLoadNew(this, mainPackageButton) { Dock = DockStyle.Fill };
                aItemFormLoadNew.aPackageItemList = new List<PackageItem>();
                aItemFormLoadNew.aRecipeOptionMdList = new List<RecipeOptionMD>();

                tileControl1.Controls.Add(aItemFormLoadNew);
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                this.Activate();
            }

            //new  PackageItemFormLoadNew().aPackageItemList = new List<PackageItem>();


        }

        //********************** Pacakge *********************

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
        private void LoadAllSubCategory(PackageCategoryButton category)
        {
            PackageBLL aPackageBll = new PackageBLL();
            List<PackageItemButton> aPackageItemButtonList = aPackageBll.GetAllSubCategory(category);
            List<int> subCategories = aPackageBll.GetCategory(category.SubCategory);

            subCategories = aPackageBll.MergeSubcategory(subCategories, aPackageItemButtonList);
            List<PackageItemButton> subCategoryList = aPackageBll.GerSubCategoryList(subCategories, category);
            int count = subCategoryList.Count; int height = (count / 10);
            if (count % 10 != 0) height += 1;


            foreach (PackageItemButton itemButton in subCategoryList)
            {
                PackageItem aItem = new PackageItem();
                aItem.ItemId = itemButton.RecipeId;
                aItem.ItemName = itemButton.ReciptName;
                aItem.Price = itemButton.AddPrice;
                aItem.Qty = 1;
                aItem.OptionName = itemButton.OptionName;
                aItem.PackageId = itemButton.PackageId;
                aItem.CategoryId = itemButton.CategoryId;
                aItem.SubcategoryId = itemButton.SubCategoryId;
                definedPackageItem.Add(aItem);
            }

        }

        //********************** Pacakge////////////////






       

        private void AddMixType(ReceipeMixType aReceipeSubCategory)
        {
            PackageBLL aPackageBll = new PackageBLL();

            if (aReceipeSubCategory.Type == "Type")
            {
                CartItem aTileItem = new CartItem();
                aTileItem.TypeId = aReceipeSubCategory.ReceipeTypeButton.TypeId;
                aTileItem.Text = aReceipeSubCategory.ReceipeTypeButton.Text;
                aTileItem.ItemSize = TileItemSize.Wide;
                aTileItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
                var color = aReceipeSubCategory.ReceipeTypeButton.BackColor;
                aTileItem.Appearance.BackColor = color;
                //aTileItem.Appearance.BackColor = ColorTranslator.FromHtml(new RestaurantMenuBLL().GetColorCode(color));


                aTileItem.AppearanceItem.Selected.Font = aReceipeSubCategory.ReceipeTypeButton.Font;

                aTileItem.Appearance.Font = aReceipeSubCategory.ReceipeTypeButton.Font;
                aTileItem.Appearance.BorderColor = Color.Transparent;

                aTileItem.ItemClick -= new TileItemClickEventHandler(TypeClick);
                aTileItem.ItemClick += new TileItemClickEventHandler(TypeClick);
                tileGroup1.Items.Add(aTileItem);

            }
            else
            {
                CartItem aTileItem = new CartItem();
                aTileItem.ItemSize = TileItemSize.Wide;


                aTileItem.CategoryId = aReceipeSubCategory.CategoryButton.CategoryId;
                aTileItem.Text = aReceipeSubCategory.CategoryButton.CategoryName;
                aTileItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
                aTileItem.Recipeid = aReceipeSubCategory.CategoryButton.ReceipeTypeId;


                var color = aReceipeSubCategory.CategoryButton.Color;
                aTileItem.Appearance.BackColor = ColorTranslator.FromHtml(new RestaurantMenuBLL().GetColorCode(color));
                aTileItem.Appearance.BorderColor = Color.Transparent;
                aTileItem.Appearance.Font = aReceipeSubCategory.CategoryButton.Font;
                aTileItem.AppearanceItem.Selected.Font = aReceipeSubCategory.CategoryButton.Font;

                tileGroup1.Items.Add(aTileItem);


                aTileItem.ItemClick -= new TileItemClickEventHandler(CategoryClick);

                aTileItem.ItemClick += new TileItemClickEventHandler(CategoryClick);

            }




        }


        private void RecipePackageButton_Click(object sen12, EventArgs e) // chnages for package
        {
            //aOthersMethod.KeyBoardClose();
            //aOthersMethod.NumberPadClose();

            RecipePackageButton tempRecipePackageButton = sen12 as RecipePackageButton;
            AndOnlyPackage(tempRecipePackageButton);
        }

        private void AndOnlyPackage(RecipePackageButton tempRecipePackageButton) // chnages for package
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
            //    int itemIndex = 0;

            //    PackageDetails aPackageDetails = new PackageDetails();
            //    aPackageDetails.qtyTextBox.Text = "1";
            //    aPackageDetails.nameTextBox.Text = tempRecipePackageButton.PackageName;

            //    if (deliveryButton.Text == "RES")
            //    {
            //        aPackageDetails.priceTextBox.Text = tempRecipePackageButton.InPrice.ToString();
            //        aPackageDetails.totalPriceLabel.Text = tempRecipePackageButton.InPrice.ToString();
            //    }
            //    else
            //    {
            //        aPackageDetails.priceTextBox.Text = tempRecipePackageButton.OutPrice.ToString();
            //        aPackageDetails.totalPriceLabel.Text = tempRecipePackageButton.OutPrice.ToString();
            //    }


            //    aPackageDetails.ItemLimit = tempRecipePackageButton.ItemLimit;
            //    aPackageDetails.BackColor = Color.Red;
            //    aPackageDetails.PackageId = tempRecipePackageButton.PackageId;
            //    int optionIndex = GetOptionIndex();

            //    RecipePackageMD aRecipePackage = new RecipePackageMD();
            //    aRecipePackage.Description = tempRecipePackageButton.Description;
            //    aRecipePackage.OptionsIndex = optionIndex + 1;
            //    aRecipePackage.PackageId = tempRecipePackageButton.PackageId;
            //    aRecipePackage.PackageName = tempRecipePackageButton.PackageName;
            //    aRecipePackage.Qty = 1;
            //    aRecipePackage.RecipeTypeId = tempRecipePackageButton.RecipeTypeId;
            //    aRecipePackage.RestaurantId = tempRecipePackageButton.RestaurantId;
            //    if (deliveryButton.Text == "RES")
            //    {
            //        aRecipePackage.UnitPrice = tempRecipePackageButton.InPrice;
            //    }
            //    else
            //    {
            //        aRecipePackage.UnitPrice = tempRecipePackageButton.OutPrice;
            //    }


            //    aRecipePackage.ItemLimit = tempRecipePackageButton.ItemLimit;
            //    aRecipePackageMdList.Add(aRecipePackage);
            //  //  ClearAllCartSelection();
            //    aPackageDetails.BackColor = Color.Red;
            //   // aPackageDetails.MouseClick += UsersGridForPackage_WasClicked;
            //    aPackageDetails.OptionIndex = optionIndex + 1;
            //    orderDetailsflowLayoutPanel1.Controls.Add(aPackageDetails);
            //}


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

                    AllCardSytemClear();
                    cc.BackColor = Color.Red;
                    LoadAmountDetails();
                }
            }
        }

        private void TypeClick(object sender, EventArgs e)
        {
            CartItem aReceipeTypeButton = (sender) as CartItem;
            aReceipeTypeButton.MenuType = "Type";
            ReceipeTypeButton topButton = new ReceipeTypeButton();
            topButton.Text = aReceipeTypeButton.Text;
            topButton.TypeId = aReceipeTypeButton.TypeId;
            topButton.Margin = new Padding(0);
            topButton.Padding = new Padding(0);
            topButton.AutoSize = true;
            topButton.AutoSizeMode = AutoSizeMode.GrowOnly;
            topButton.Height = pannelTopBar.Height - 2;
            topButton.Font = aReceipeTypeButton.Appearance.Font;
            topButton.BackColor = aReceipeTypeButton.Appearance.BackColor;
            topButton.ForeColor = Color.White;
            topButton.FlatStyle = FlatStyle.Flat;
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Controls.Add(topButton);
            pannelTopBar.Visible = true;

            topButton.Click -= new EventHandler(topFirstMenuClick);
            topButton.Click += new EventHandler(topFirstMenuClick);

            TypeCatLoad(aReceipeTypeButton, dynamicTableLayoutPanel);
        }

        private void topFirstMenuClick(object sender, EventArgs e)
        {
            LoadMenuType();
        }

        void topFirstMenuClick(object Object, EventHandler e)
        {

        }
        private void TypeCatLoad(CartItem aReceipeTypeButton, FlowLayoutPanel layoutPanel)
        {

            tileGroup1.Items.Clear();

            var receipeTypeList = GetAllCatergory.Where(a => a.ReceipeTypeId == Convert.ToInt16(aReceipeTypeButton.TypeId) && a.HasSubcategory == 1).ToList();

            ResponsiveItem(receipeTypeList.Count, dockPanel1.Visible, tileControl1);


            foreach (var receipeCategoryButton in receipeTypeList)
            {
                CartItem button = new CartItem();
                button.Text = receipeCategoryButton.Text;
                button.TypeId = receipeCategoryButton.ReceipeTypeId;
                button.CategoryId = receipeCategoryButton.CategoryId;
                button.ItemSize = TileItemSize.Wide;
                button.Appearance.Font = receipeCategoryButton.Font;
                button.TextAlignment = TileItemContentAlignment.MiddleCenter;
                button.Tag = receipeCategoryButton.Color;

                button.Appearance.BackColor = ColorTranslator.FromHtml(new RestaurantMenuBLL().GetColorCode(receipeCategoryButton.Color));
                button.Appearance.ForeColor = Color.White;
                button.Appearance.BorderColor = Color.Transparent;

                button.ItemClick -= new TileItemClickEventHandler(TypeCatClick);
                button.ItemClick += new TileItemClickEventHandler(TypeCatClick);

                tileGroup1.Items.Add(button);

            }

        }

        private void topcatClick(object sender, EventArgs e)
        {
            try
            {
                ReceipeTypeButton topButton = (sender) as ReceipeTypeButton;
                optionItemDataTable.Rows.Clear();
                tileControl1.Controls.Clear();
                CartItem TypeButton = new CartItem();
                if (topButton == null)
                {
                    ReceipeCategoryButton UpdateRetriveMenu = (sender) as ReceipeCategoryButton;

                    TypeButton.Text = UpdateRetriveMenu.Text;
                    TypeButton.TypeId = UpdateRetriveMenu.ReceipeTypeId;
                    TypeButton.CategoryId = UpdateRetriveMenu.CategoryId;

                }
                else
                {
                    TypeButton.Text = topButton.Text;
                    TypeButton.TypeId = topButton.TypeId;
                }





                int Removecount = 0;
                for (int i = 1; i < flowLayoutPanel1.Controls.Count; i++)
                {

                    flowLayoutPanel1.Controls.RemoveAt(i);
                    i--;
                    // Removecount++;


                }


                TypeCatLoad(TypeButton, dynamicTableLayoutPanel);


            }
            catch (Exception ex)
            {

            }

        }

        private void TypeCatLoad(CartItem receipeCategoryTypeButton)
        {

            tileGroup1.Items.Clear();
            tileControl1.Controls.Clear();
            var typeSubCatlist = AllRecipeButton.Where(a => a.CategoryId == Convert.ToInt16(receipeCategoryTypeButton.CategoryId)).ToList();

            List<ReceipeSubCategoryButton> GetAllSubCatgory = new RestaurantMenuBLL().GetAllSubcategory();
            ResponsiveItem(typeSubCatlist.Count, dockPanel1.Visible, tileControl1);
            foreach (ReceipeMenuItemButton menuItemButton in typeSubCatlist)
            {
                foreach (ReceipeSubCategoryButton subCategoryButton in GetAllSubCatgory.Where(a => a.SubCategoryId == menuItemButton.SubCategoryId))
                {
                    CartItem reNewButton = new CartItem();
                    reNewButton.MenuType = "Type";
                    reNewButton.Text = subCategoryButton.Title;
                    reNewButton.ItemSize = TileItemSize.Wide;
                    reNewButton.TypeId = receipeCategoryTypeButton.TypeId;
                    reNewButton.Appearance.Font = menuItemButton.Font;
                    reNewButton.RecipeMenuItemId = menuItemButton.RecipeMenuItemId;
                    reNewButton.CategoryId = menuItemButton.CategoryId;
                    reNewButton.RecipeMenuItemId = menuItemButton.RecipeMenuItemId;
                    reNewButton.ItemType = "SingleItem";
                    reNewButton.Appearance.BorderColor = Color.Transparent;
                    reNewButton.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    reNewButton.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    reNewButton.Appearance.BackColor = ColorTranslator.FromHtml(new RestaurantMenuBLL().GetColorCode(Convert.ToString(subCategoryButton.ButtonColor)));
                    reNewButton.AppearanceItem.Selected.Font = menuItemButton.Font;
                    reNewButton.Appearance.ForeColor = Color.White;
                    tileGroup1.Items.Add(reNewButton);

                    reNewButton.ItemPress -= new TileItemClickEventHandler(ReceipeMenuItemButton_Click);
                    reNewButton.ItemPress += new TileItemClickEventHandler(ReceipeMenuItemButton_Click);


                }

            }
        }

        private CartItem receipeCategoryTypeButton;

        private void receipeCategoryTypeButtonClick(object sender, EventArgs e)
        {

            flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1].Dispose();

            ReceipeTypeButton topButton = sender as ReceipeTypeButton;

            if (topButton == null)
            {
                LoadMenuType();
                return;
            }



            receipeCategoryTypeButton = new CartItem();
            receipeCategoryTypeButton.TypeId = topButton.TypeId;
            receipeCategoryTypeButton.CategoryId = Convert.ToInt32(topButton.Name);
            TypeSubCatLoad(receipeCategoryTypeButton);




        }
        private void TypeCatClick(object sender, EventArgs e)
        {
            CartItem receipeCategoryTypeButton = sender as CartItem;
            ReceipeTypeButton topButton = new ReceipeTypeButton();
            topButton.Text = receipeCategoryTypeButton.Text;
            topButton.Name = receipeCategoryTypeButton.CategoryId.ToString();
            topButton.TypeId = receipeCategoryTypeButton.TypeId;
            topButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            topButton.Font = receipeCategoryTypeButton.Appearance.Font;
            topButton.BackColor = Color.Black;
            topButton.ForeColor = Color.White;
            topButton.FlatStyle = FlatStyle.Flat;
            topButton.Margin = new Padding(0);
            topButton.Padding = new Padding(0);
            topButton.Height = pannelTopBar.Height - 2;

            topButton.Click -= new EventHandler(topcatClick);
            topButton.Click += new EventHandler(topcatClick);


            flowLayoutPanel1.Controls.Add(topButton);


            TypeSubCatLoad(receipeCategoryTypeButton);

        }

        private void TypeSubCatLoad(CartItem receipeCategoryTypeButton)
        {

            this.receipeCategoryTypeButton = receipeCategoryTypeButton;
            TypeCatLoad(receipeCategoryTypeButton);
        }
        private CartItem aCategoryButton;

        private void CategoryClick(object sender, EventArgs e)
        {

            CartItem aCategoryButton = (sender) as CartItem;
            CategoryLoad(aCategoryButton, dynamicTableLayoutPanel);


            this.aCategoryButton = aCategoryButton;

        }

        public  RestaurantInformation aRestaurantInformation = new RestaurantInformation();
        private void Form2_Load(object sender, EventArgs e)
        {
            loadwaitform.ShowWaitForm();
            tileControl1.BorderStyle = BorderStyles.NoBorder;
            InitLoadToCard();
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            GlobalSetting.RestaurantInformation = aRestaurantInformation;
            reataurantNameLabelNew.Text = aRestaurantInformation.RestaurantName;
            if (aRestaurantInformation.RestaurantType == "takeaway")
            {
                tableButtonNew.Visible = false;
            }
            if (aRestaurantInformation.IsServiceCharge > 0)
            {
                serviceChargeLabel.Visible = true;
            }
            else
            {
                serviceChargeLabel.Visible = false;
                discountButton.Width = 187;
            }
            //CustomerSyncronize();
            LoadAllData();
            LoadMenuType();
            CardVisibleInvisible(cardProperty);
            LoadAllPrinter();
            //OtherInformation();
            loadwaitform.CloseWaitForm();
        }

        private void dockManager1_Load(object sender, EventArgs e)
        {
            //hideContainerRight.Dock.
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //dockPanel1.ShowSliding();

        }



        ////////////////   Type wise Form Load///////////////////////////

        public void CategoryLoad(CartItem receipeCategory, FlowLayoutPanel layoutPanel)
        {
            tileControl1.Controls.Clear();
            ReceipeTypeButton topButton = new ReceipeTypeButton();
            topButton.Text = receipeCategory.Text;
            topButton.Name = receipeCategory.CategoryId.ToString();
            topButton.TypeId = receipeCategory.TypeId;
            topButton.Font = receipeCategory.Appearance.Font;
            topButton.AutoSize = true;
            topButton.MinimumSize = new Size(100, pannelTopBar.Height - 2);
            topButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            topButton.BackColor = Color.Black;
            topButton.ForeColor = Color.White;
            topButton.FlatStyle = FlatStyle.Flat;
            topButton.Margin = new Padding(0);
            topButton.Padding = new Padding(0);
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Controls.Add(topButton);
            pannelTopBar.Visible = true;
            topButton.Click -= new EventHandler(ZeroTopCatagory);
            topButton.Click += new EventHandler(ZeroTopCatagory);
            tileGroup1.Items.Clear();
            var getallsuButtons = AllRecipeButton.Where(a => a.CategoryId == Convert.ToInt16(receipeCategory.CategoryId)).ToList();
            ResponsiveItem(getallsuButtons.Count, dockPanel1.Visible, tileControl1);
            int count = 0;
            CategoryLoadChild(getallsuButtons, receipeCategory);
        }
        public void CategoryLoadChild(List<ReceipeMenuItemButton> getallsuButtons, CartItem receipeCategory)
        {
            int count = 0;
            foreach (ReceipeMenuItemButton menuItemButton in getallsuButtons)
            {
                CartItem menuItemTitle = new CartItem();
                menuItemTitle.MenuType = "Category";
                menuItemTitle.ItemType = "SingleItem";
                menuItemTitle.ItemSize = TileItemSize.Wide;
                var colorCode = GetAllCatergory.FirstOrDefault(a => a.CategoryId == Convert.ToInt16(receipeCategory.CategoryId));
                if (colorCode != null)
                {
                    menuItemTitle.Appearance.BackColor = ColorTranslator.FromHtml(new RestaurantMenuBLL().GetColorCode(colorCode.Color));
                }
                menuItemTitle.RecipeMenuItemId = Convert.ToInt32(menuItemButton.RecipeMenuItemId);
                menuItemTitle.CategoryId = Convert.ToInt16(receipeCategory.Id);
                menuItemTitle.Text = menuItemButton.ItemName;
                menuItemTitle.Appearance.Font = menuItemButton.Font;
                menuItemTitle.AppearanceItem.Selected.Font = menuItemButton.Font;
                menuItemTitle.Appearance.BorderColor = Color.Transparent;
                menuItemTitle.TextAlignment = TileItemContentAlignment.MiddleCenter;
                tileGroup1.Items.Add(menuItemTitle);
                menuItemTitle.ItemPress -= new TileItemClickEventHandler(ReceipeMenuItemButton_Click);
                menuItemTitle.ItemPress += new TileItemClickEventHandler(ReceipeMenuItemButton_Click);
            }
        }
        private void ZeroTopCatagory(object sender, EventArgs e)
        {
            LoadMenuType();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            LoadMenuType();
        }
        ///////////////// Add To Card /////////////////
        GridControl gridControlOption = new GridControl() { Dock = DockStyle.Right, Width = 250 };
        private GridView gridViewOption;
        XtraTabControl tabControl = new XtraTabControl
        {
            Dock = DockStyle.Fill
        };
        DataTable optionItemDataTable = new DataTable();
        private DataRow optionDataRow;

        FlowLayoutPanel FooterControl = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Padding = new Padding(0, 25, 0, 0),
            FlowDirection = FlowDirection.RightToLeft,
            WrapContents = false
        };
        private void optionRow_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Plus")
            {
                string price = gridViewOption.GetFocusedRowCellValue("Price").ToString();
                int Qty = Convert.ToInt32(gridViewOption.GetFocusedRowCellValue("Qty"));
                string Group = gridViewOption.GetFocusedRowCellValue("Group").ToString();
                CartItem CartItemButton = (CartItem)gridViewOption.GetFocusedRowCellValue("CartItem");

                string tabPageName = tabControl.SelectedTabPage.Text;
                if (tabPageName == Group)
                {

                    int TotalQty = optionItemDataTable.AsEnumerable().Where(a => a["Group"].ToString() == Group).Sum(a => Convert.ToInt32(a["Qty"]));
                    if (CartItemButton.RecipeOptionItemButton.RecipeOptionButton.ItemLImit > 100)
                    {

                        if (TotalQty >= CartItemButton.Index)
                        {
                            return;
                        }

                        if (price == string.Empty)
                        {
                            gridViewOption.SetFocusedRowCellValue("Qty", Qty + 1);
                            return;
                        }

                        //**************************************************************************************************************

                        decimal Orgprice = Convert.ToDecimal(gridViewOption.GetFocusedRowCellValue("OrgPrice"));
                        gridViewOption.SetFocusedRowCellValue("Qty", Qty + 1);
                        gridViewOption.SetFocusedRowCellValue("Price", ((Qty + 1) * Orgprice).ToString("n2"));

                    }
                    else
                    {
                        if (CartItemButton.RecipeOptionItemButton.RecipeOptionButton.ItemLImit <= TotalQty)
                        {
                            return;
                        }
                        if (price == string.Empty)
                        {
                            gridViewOption.SetFocusedRowCellValue("Qty", Qty + 1);
                            return;
                        }

                        decimal Orgprice = Convert.ToDecimal(gridViewOption.GetFocusedRowCellValue("OrgPrice"));
                        gridViewOption.SetFocusedRowCellValue("Qty", Qty + 1);
                        gridViewOption.SetFocusedRowCellValue("Price", ((Qty + 1) * Orgprice).ToString("n2"));
                    }

                }






                //PackageItem package = (PackageItem)packagegridView.GetFocusedRowCellValue("Class");

                //if (package.DeleteItem)
                //{
                //    MessageBox.Show("This Item is Fixed , You Can Not Remove this Item ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //    return;
                //}


                //for (int i = 0; i < package.PackageItemOptionList.Count; i++)
                //{
                //    aRecipeOptionMdList.Remove(package.PackageItemOptionList[i]);

                //}

                //double optionPrice = aRecipeOptionMdList.Sum(a => a.InPrice);

                //RecipePackageButton packageButton = (RecipePackageButton)aMainFormView.flowLayoutPanel1.Controls[1];
                //lblPrice.Text = "£" + (optionPrice + packageButton.InPrice).ToString("F");


                //aPackageItemList.Remove(package);

                //packagegridView.DeleteRow(e.RowHandle);

                //  lblPrice.Text = TotalPrice.ToString("C2");


            }
            else if (e.Column.FieldName == "Minus")
            {
                string price = gridViewOption.GetFocusedRowCellValue("Price").ToString();

                int Qty = Convert.ToInt32(gridViewOption.GetFocusedRowCellValue("Qty"));
                if (Qty > 1)
                {
                    if (price == string.Empty)
                    {
                        gridViewOption.SetFocusedRowCellValue("Qty", Qty - 1); return;

                    }
                    decimal Orgprice = Convert.ToDecimal(gridViewOption.GetFocusedRowCellValue("OrgPrice"));
                    gridViewOption.SetFocusedRowCellValue("Qty", Qty - 1);
                    gridViewOption.SetFocusedRowCellValue("Price", ((Qty - 1) * Orgprice).ToString("n2"));

                }


            }

        }
        private GridView OptionGridView()
        {

            gridViewOption = new GridView();


            gridViewOption.OptionsView.ShowGroupPanel = false;
            gridViewOption.OptionsView.ShowGroupExpandCollapseButtons = true;
            gridViewOption.OptionsView.ShowColumnHeaders = false;
            gridViewOption.OptionsView.ShowIndicator = false;
            Font font = new Font("Tahoma", 10);
            gridViewOption.OptionsBehavior.Editable = false;
            gridViewOption.Appearance.Row.Font = font;
            gridViewOption.RowHeight = 30;
            GridColumn Plus = new GridColumn() { FieldName = "Plus", ColumnEdit = repositoryItemHyperLinkEdit1, Caption = "Plus", Visible = true, Width = 5 };
            GridColumn Count = new GridColumn() { FieldName = "Qty", Caption = "Qty", Width = 3, Visible = true };
            GridColumn OptionName = new GridColumn() { FieldName = "OptionName", Caption = "OptionName", Visible = true };
            GridColumn Index = new GridColumn() { FieldName = "Index", Caption = "Name", Visible = false };
            GridColumn OptionId = new GridColumn() { FieldName = "OptionId", Caption = "OptionId", Visible = false };
            GridColumn Group = new GridColumn() { FieldName = "Group", Caption = "Group", Visible = true, GroupIndex = 0 };
            GridColumn Price = new GridColumn() { FieldName = "Price", Caption = "Price", Visible = true, Width = 25 };
            GridColumn Minus = new GridColumn() { FieldName = "Minus", ColumnEdit = repositoryMinus, Caption = "Minus", Visible = true, Width = 10 };
            GridColumn OrgPrice = new GridColumn() { FieldName = "OrgPrice", Caption = "Minus", Visible = false, Width = 10 };
            GridColumn CartItem = new GridColumn() { FieldName = "CartItem", Caption = "", Visible = false, Width = 10 };

            gridViewOption.Columns.Add(Minus);

            gridViewOption.Columns.Add(Count);
            gridViewOption.Columns.Add(Index);
            gridViewOption.Columns.Add(OptionName);
            gridViewOption.Columns.Add(Index);
            gridViewOption.Columns.Add(Group);
            gridViewOption.Columns.Add(Price);
            gridViewOption.Columns.Add(OptionId);
            gridViewOption.Columns.Add(Plus);
            gridViewOption.Columns.Add(OrgPrice);
            gridViewOption.Columns.Add(CartItem);

            gridViewOption.CustomDrawGroupRow -= GridView_CustomDrawGroupRow;
            gridViewOption.CustomDrawGroupRow += GridView_CustomDrawGroupRow;
            gridViewOption.RowCellClick -= optionRow_RowCellClick;
            gridViewOption.RowCellClick += optionRow_RowCellClick;
            gridViewOption.ExpandAllGroups();
            gridControlOption.MainView = gridViewOption;
            optionDataRow = null;
            gridControlOption.DataSource = optionDataRow;
            //gridViewOption.Columns["Index"].Visible = false;
            //gridViewOption.Columns["Price"].Width = 20;
            //gridViewOption.Columns["OptionId"].Visible = false;
            //gridViewOption.ExpandAllGroups();





            gridViewOption.GroupCount = 1;
            gridViewOption.OptionsView.ShowVerticalLines = DefaultBoolean.False;
            GridGroupSummaryItem item1 = new GridGroupSummaryItem();
            item1.FieldName = "Group";
            item1.SummaryType = DevExpress.Data.SummaryItemType.Count;
            item1.DisplayFormat = "{0}";
            gridViewOption.OptionsView.GroupDrawMode = GroupDrawMode.Office;
            gridViewOption.GroupSummary.Add(item1);
            return gridViewOption;
        }
        TileControl optionControl;
        TileGroup tileGroup;
        private void ItemOptionLoad(List<RecipeOptionButton> itemButtons, ReceipeMenuItemButton aMenuItemButton, string MenuType, bool IsUpdate)
        {


            ReceipeMenuItemButton topButton = new ReceipeMenuItemButton();
            topButton.Text = aMenuItemButton.ItemName;
            topButton.CategoryId = aMenuItemButton.CategoryId;
            topButton.RecipeTypeId = aMenuItemButton.RecipeMenuItemId;
            topButton.Font = aMenuItemButton.Font;
            topButton.AutoSize = true;
            topButton.MinimumSize = new Size(100, pannelTopBar.Height - 2);
            topButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            topButton.BackColor = aMenuItemButton.BackColor;
            topButton.ForeColor = Color.White;
            topButton.FlatStyle = FlatStyle.Flat;
            topButton.Margin = new Padding(0);
            topButton.Padding = new Padding(0);
            if (MenuType == "Type")
            {
                flowLayoutPanel1.Controls.Add(topButton);
                topButton.Click -= TypeButtonOnClick;
                topButton.Click += TypeButtonOnClick;
            }
            else
            {

                topButton.Click += (sender, args) =>
                {
                    Control exitsButton = (Control)flowLayoutPanel1.Controls[0];
                    CartItem item = new CartItem();
                    item.Recipeid = topButton.RecipeTypeId;
                    item.CategoryId = topButton.CategoryId;
                    item.Appearance.Font = exitsButton.Font;
                    item.Text = exitsButton.Text;
                    CategoryLoad(item, dynamicTableLayoutPanel);
                };
                flowLayoutPanel1.Controls.Add(topButton);
            }

            dynamicTableLayoutPanel = new CustomFlowLayoutPanel();
            tileGroup1.Items.Clear();
            FooterControl.Controls.Clear();
            tileControl1.Controls.Clear();
            tabControl.Controls.Clear();
            tabControl.TabPages.Clear();


            foreach (var group in itemButtons)
            {
                optionControl = new TileControl
                {
                    Dock = DockStyle.Fill,
                    AllowSelectedItem = false,
                    AllowDrag = false,
                    ItemCheckMode = TileItemCheckMode.None,
                    ItemAppearanceSelected = { ForeColor = Color.White },
                    HorizontalContentAlignment = HorzAlignment.Near,
                    Orientation = Orientation.Vertical,
                    VerticalContentAlignment = VertAlignment.Top,
                    ItemPadding = new Padding(0)
                };

                optionControl.IndentBetweenGroups = 8;

                tileGroup = new TileGroup();
                // To be Checked

                if (group.PlusMinus == 1)
                {
                    TileGroup ToptileGroup = new TileGroup();
                    optionControl.Groups.Add(ToptileGroup);
                    CartItem optionItem = new CartItem();
                    optionItem.ItemSize = TileItemSize.Wide;
                    optionItem.ReceipeMenuItemButton = aMenuItemButton;
                    optionItem.AppearanceItem.Selected.Font = group.Font;
                    optionItem.Appearance.BorderColor = Color.Transparent;
                    optionItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    optionItem.Appearance.BackColor = Color.CornflowerBlue;
                    optionItem.Appearance.Font = group.Font;
                    optionItem.Text = "No";
                    optionItem.IsNooption = true;
                    optionItem.MenuType = MenuType;

                    optionItem.ItemClick += (sender, args) =>
                    {

                        var tileGroupName = tabControl.TabPages[tabControl.SelectedTabPageIndex].Controls;
                        TileControl tileControl = (TileControl)tileGroupName[0];

                        if (optionItem.Checked == false)
                        {


                            if (tileControl.Groups.Count > 0)
                            {
                                var tileItem = tileControl.Groups[1].Items;
                                for (int i = 0; i < tileItem.Count; i++)
                                {
                                    CartItem OptionItem = (CartItem)tileItem[i];

                                    if (!OptionItem.Checked)
                                    {
                                        if (OptionItem.RecipeOptionItemButton.Title.Contains("No"))
                                        {
                                            OptionItem.RecipeOptionItemButton.Title = OptionItem.RecipeOptionItemButton.Title.ToString().Remove(0, 2);
                                            OptionItem.Text = OptionItem.Text.Remove(0, 2);
                                        }
                                        OptionItem.Text = optionItem.Text + " " + OptionItem.Text;
                                        OptionItem.RecipeOptionItemButton.Title = "No " + OptionItem.RecipeOptionItemButton.Title;
                                        OptionItem.RecipeOptionItemButton.Price = 0.0;
                                        OptionItem.RecipeOptionItemButton.InPrice = 0.0;
                                        OptionItem.IsNooption = true;
                                        OptionItem.Checked = false;
                                    }


                                    //  string html = "<span style='color:red;'>1/2 Bf Burg</span></br>&rarr;No ";

                                }
                            }


                            //   var  items = tabControl.SelectedTabPage.Controls[0].Controls;

                            optionItem.Appearance.BackColor = Color.Black;

                            optionItem.Checked = true;
                        }
                        else if (optionItem.Checked)
                        {
                            if (tileControl.Groups.Count > 0)
                            {
                                var tileItem = tileControl.Groups[1].Items;
                                for (int i = 0; i < tileItem.Count; i++)
                                {
                                    CartItem OptionItem = (CartItem)tileItem[i];
                                    if (!OptionItem.Checked)
                                    {
                                        if (OptionItem.Text.Contains("No"))
                                        {
                                            OptionItem.Text = OptionItem.Text.Remove(0, 2).Trim();
                                            OptionItem.RecipeOptionItemButton.Title = OptionItem.RecipeOptionItemButton.Title.Remove(0, 2).Trim();

                                            OptionItem.RecipeOptionItemButton.Price = OptionItem.Price;
                                            OptionItem.RecipeOptionItemButton.InPrice = OptionItem.InPrice;
                                            OptionItem.Checked = false;
                                            OptionItem.IsNooption = false;
                                        }

                                    }
                                }
                            }
                            optionItem.Appearance.BackColor = Color.CornflowerBlue;

                            optionItem.Checked = false;

                        }

                    };
                    ToptileGroup.Items.Add(optionItem);
                }

                optionControl.Groups.Add(tileGroup);

                var RecipeOptionItemButton_List = new RestaurantMenuBLL().GetRecipeOptionItemByOptionId(group);
                ResponsiveItemOption(RecipeOptionItemButton_List.Count, dockPanel1.Visible, optionControl);
                foreach (RecipeOptionItemButton recipeOptionItemButton in RecipeOptionItemButton_List)
                {

                    CartItem optionItem = new CartItem();
                    optionItem.Index = RecipeOptionItemButton_List.Count;
                    optionItem.ItemSize = TileItemSize.Wide;
                    optionItem.ReceipeMenuItemButton = aMenuItemButton;
                    optionItem.AppearanceItem.Selected.Font = recipeOptionItemButton.Font;
                    optionItem.Appearance.BorderColor = Color.Transparent;
                    optionItem.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    optionItem.Appearance.BackColor = recipeOptionItemButton.BackColor;
                    optionItem.Appearance.Font = recipeOptionItemButton.Font;
                    optionItem.Text = recipeOptionItemButton.Title;
                    optionItem.RecipeOptionItemButton = recipeOptionItemButton;

                    string mixText = "<span style='color:red;'>" + topButton.Text + "</span></br>&rarr;" + recipeOptionItemButton.Title;
                    optionItem.Title = mixText;
                    optionItem.RecipeOptionItemId = recipeOptionItemButton.RecipeOptionItemId;
                    optionItem.RecipeOptionId = recipeOptionItemButton.RecipeOptionId;
                    optionItem.MenuType = MenuType;
                    optionItem.InPrice = recipeOptionItemButton.InPrice;
                    optionItem.Price = recipeOptionItemButton.Price;

                    optionItem.ItemPress -= new TileItemClickEventHandler(OptionAdd);
                    optionItem.ItemPress += new TileItemClickEventHandler(OptionAdd);
                    tileGroup.Items.Add(optionItem);

                }
                //tabControl.Controls.Add(optionControl);


                var tabPage = tabControl.TabPages.Add(group.Title);

                tabPage.PageEnabled = false;
                tileGroup.Name = tabPage.Text;

                tabPage.Controls.Add(optionControl);


            }

            var windowSize = Screen.PrimaryScreen.Bounds.Height - 180;

            tabControl.Dock = DockStyle.Fill;
            tabControl.BringToFront();

            tabControl.TabPages[0].PageEnabled = true;

            tabControl.SelectedPageChanged += (sender, args) =>
            {
                if (tabControl.SelectedTabPageIndex == tabControl.TabPages.Count - 1)
                {
                    btnAddToCard.Visible = true;
                    btnNextPrevious.Visible = false;

                    btnNextPrevious.Text = "PREVIOUS";


                }
                else if (tabControl.SelectedTabPageIndex == 0)
                {
                    btnNextPrevious.Text = "NEXT";
                    btnNextPrevious.BackColor = Color.Coral;
                    btnAddToCard.Visible = false;
                    btnNextPrevious.Visible = true;

                }


            };

            PanelControl panelControl = new PanelControl { Dock = DockStyle.Top, Height = windowSize };
            optionItemDataTable = new DataTable();

            optionItemDataTable.Columns.Add(new DataColumn("Index"));
            optionItemDataTable.Columns.Add(new DataColumn("OptionName"));
            optionItemDataTable.Columns.Add(new DataColumn("Group"));
            optionItemDataTable.Columns.Add(new DataColumn("Price"));
            optionItemDataTable.Columns.Add(new DataColumn("OptionId"));
            optionItemDataTable.Columns.Add(new DataColumn("Qty"));
            optionItemDataTable.Columns.Add(new DataColumn("OrgPrice"));
            optionItemDataTable.Columns.Add(new DataColumn("CartItem", typeof(CartItem)));

            OptionGridView();

            panelControl.Controls.Add(gridControlOption);
            panelControl.Controls.Add(tabControl);
            tileGroup1.Control.AddControl(panelControl);
            tabControl.BringToFront();

         foreach (Button button in NextPreviousButtonAdd(tabControl.TabPages.Count, MenuType, IsUpdate))
            {


                FooterControl.Controls.Add(button);


            }
            if (tabControl.TabPages.Count == 1)
            {

                FooterControl.Controls[0].Visible = false;
                FooterControl.Controls[1].Visible = false;
                FooterControl.Controls[2].Visible = true;

            }

            Panel FPanel = new Panel();
            FPanel.Dock = DockStyle.Fill;

            FPanel.Controls.Add(FooterControl);

            tileGroup1.Control.AddControl(FPanel);
            FooterControl.BringToFront();
        }

        private void ClickSubMenuButton(object sender, EventArgs eventArgs)
        {
            ReceipeMenuItemButton itemButton = sender as ReceipeMenuItemButton;
            MessageBox.Show(itemButton.ItemName);
        }

       void GridView_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            GridView gridView = sender as GridView;

            var groupColumnCellValue = gridView.GetGroupRowValue(e.RowHandle, info.Column);
            string groupColumnCellValueStr = Convert.ToString(groupColumnCellValue);

            info.GroupText = gridView.GetGroupSummaryText(e.RowHandle) + ": " + groupColumnCellValueStr;
            //info.GroupText = info.Column.Caption + ": " + groupColumnCellValueStr + " " + gridView.GetGroupSummaryText(e.RowHandle);
        }
        void GridViewCart_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            GridView gridView = sender as GridView;

            var groupColumnCellValue = gridView.GetGroupRowValue(e.RowHandle, info.Column);
            string groupColumnCellValueStr = Convert.ToString(groupColumnCellValue);

            info.GroupText = gridView.GetGroupSummaryText(e.RowHandle) + "" + groupColumnCellValueStr;
            //info.GroupText = info.Column.Caption + ": " + groupColumnCellValueStr + " " + gridView.GetGroupSummaryText(e.RowHandle);
        }
        private void OptionAdd(object sender, TileItemEventArgs e)
        {
            try
            {
                CartItem OptionItem = sender as CartItem;

                if (OptionItem.Checked)
                {

                    for (int i = 0; i < gridViewOption.DataRowCount; i++)
                    {
                        var ExistCheckOptionId = optionDataRow.Table.Rows[i].Field<string>("OptionId");

                        if (ExistCheckOptionId == OptionItem.RecipeOptionItemButton.RecipeOptionItemId.ToString())
                        {

                            optionDataRow.Table.Rows[i].Delete();

                            OptionItem.Checked = false;

                            return;

                        }
                    }

                }
                else
                {
                    OptionItem.Checked = true;

                }



                string type = OptionItem.RecipeOptionItemButton.RecipeOptionButton.Type;
                int limit = OptionItem.RecipeOptionItemButton.RecipeOptionButton.ItemLImit;

                var selectTabpageControlCount = tabControl.SelectedTabPage.Controls;

                //  int limit = 3;

                if (type == "multiple")
                {

                    gridViewOption.Columns["Group"].GroupIndex = 0;
                    int rowCount = 0;
                    for (int i = 0; i < gridViewOption.DataRowCount; i++)
                    {
                        var groupRowCount = gridViewOption.GetGroupRowValue(i, gridViewOption.Columns["Group"]);
                        if (groupRowCount.ToString() == tabControl.SelectedTabPage.Text)
                        {
                            int rowQty = Convert.ToInt32(gridViewOption.GetGroupRowValue(i, gridViewOption.Columns["Qty"]));
                            rowCount = optionItemDataTable.AsEnumerable().Where(a => a["Group"].ToString() == tabControl.SelectedTabPage.Text)
                                   .Sum(a => Convert.ToInt32(a["Qty"]));

                            if (rowCount >= limit || rowCount >= OptionItem.Index)
                            {

                                OptionItem.Checked = false;
                                return;
                            }
                        }
                    }


                    optionDataRow = optionItemDataTable.NewRow();
                    optionDataRow[0] = optionItemDataTable.Rows.Count + 1;
                    optionDataRow[1] = OptionItem.RecipeOptionItemButton.Title;
                    optionDataRow[2] = tabControl.SelectedTabPage.Text;

                    if (aGeneralInformation.TableId > 0)
                    {
                        if (OptionItem.RecipeOptionItemButton.InPrice > 0)
                        {
                            optionDataRow[3] =
                                Convert.ToDecimal(OptionItem.RecipeOptionItemButton.InPrice).ToString("C2").Substring(1);
                            optionDataRow["OrgPrice"] =
                                Convert.ToDecimal(OptionItem.RecipeOptionItemButton.InPrice).ToString("C2").Substring(1);

                        }
                        else
                        {
                            optionDataRow[3] = null;
                            optionDataRow["OrgPrice"] = null;

                        }
                    }
                    else
                    {
                        if (OptionItem.RecipeOptionItemButton.Price > 0)
                        {
                            optionDataRow[3] = Convert.ToDecimal(OptionItem.RecipeOptionItemButton.Price).ToString("C2").Substring(1);
                            optionDataRow["OrgPrice"] = Convert.ToDecimal(OptionItem.RecipeOptionItemButton.Price).ToString("C2").Substring(1);

                        }
                        else
                        {
                            optionDataRow[3] = null;
                            optionDataRow["OrgPrice"] = null;

                        }
                    }





                    optionDataRow[4] = OptionItem.RecipeOptionItemId;
                    optionDataRow["Qty"] = 1;
                    optionDataRow["CartItem"] = OptionItem;
                    optionItemDataTable.Rows.Add(optionDataRow);

                    // rowCount = optionItemDataTable.AsEnumerable().Where(a => a["Group"].ToString() == tabControl.SelectedTabPage.Text).Sum(a => Convert.ToInt32(a["Qty"]));
                    if (rowCount == limit - 1 || rowCount == OptionItem.Index - 1)
                    {

                        if (tabControl.TabPages.Count - 1 != tabControl.SelectedTabPageIndex)
                        {
                            tabControl.TabPages[tabControl.SelectedTabPageIndex + 1].PageEnabled = true;
                            tabControl.SelectedTabPage = tabControl.TabPages[tabControl.SelectedTabPageIndex + 1];

                            if (tabControl.TabPages.Count - 1 == tabControl.SelectedTabPageIndex)
                            {
                                btnAddToCard.Visible = true;
                                btnWithoutOption.Visible = false;
                                btnNextPrevious.Text = "PREVIOUS";
                            }
                            else
                            {
                                btnAddToCard.Visible = false;
                                btnWithoutOption.Visible = true;
                                btnNextPrevious.Text = "NEXT";
                            }
                        }
                        else
                        {
                            TempOptionListAdd(OptionItem.MenuType, false);
                        }

                        return;
                    }

                }
                else if (type == "single")
                {


                    gridViewOption.Columns["Group"].GroupIndex = 0;
                    int rowCount = 0;
                    for (int i = 0; i < gridViewOption.DataRowCount; i++)
                    {
                        var groupRowCount = gridViewOption.GetGroupRowValue(i, gridViewOption.Columns["Group"]);
                        if (groupRowCount.ToString() == tabControl.SelectedTabPage.Text)
                        {

                            rowCount = optionItemDataTable.AsEnumerable().Where(a => a["Group"].ToString() == tabControl.SelectedTabPage.Text)
                                    .Sum(a => Convert.ToInt32(a["Qty"]));
                            if (rowCount == 1)
                            {

                                OptionItem.Checked = false;

                                return;
                            }
                        }
                    }

                    optionDataRow = optionItemDataTable.NewRow();
                    optionDataRow[0] = optionItemDataTable.Rows.Count + 1;
                    optionDataRow[1] = OptionItem.RecipeOptionItemButton.Title;
                    optionDataRow[2] = tabControl.SelectedTabPage.Text;

                    if (aGeneralInformation.TableId > 0)
                    {
                        if (OptionItem.RecipeOptionItemButton.InPrice > 0)
                        {
                            optionDataRow[3] = OptionItem.RecipeOptionItemButton.InPrice;
                            optionDataRow["OrgPrice"] = OptionItem.RecipeOptionItemButton.InPrice;

                        }
                        else
                        {
                            optionDataRow[3] = null;
                            optionDataRow["OrgPrice"] = null;
                        }

                    }
                    else
                    {
                        if (OptionItem.RecipeOptionItemButton.Price > 0)
                        {
                            optionDataRow[3] = OptionItem.RecipeOptionItemButton.Price;
                            optionDataRow["OrgPrice"] = OptionItem.RecipeOptionItemButton.Price;

                        }
                        else
                        {
                            optionDataRow[3] = null;
                            optionDataRow["OrgPrice"] = null;
                        }
                    }






                    optionDataRow[4] = OptionItem.RecipeOptionItemId;
                    optionDataRow["Qty"] = 1;
                    optionDataRow["CartItem"] = OptionItem;

                    optionItemDataTable.Rows.Add(optionDataRow);
                    try
                    {


                        if (tabControl.TabPages.Count - 1 != tabControl.SelectedTabPageIndex)
                        {
                            tabControl.TabPages[tabControl.SelectedTabPageIndex + 1].PageEnabled = true;
                            tabControl.SelectedTabPage = tabControl.TabPages[tabControl.SelectedTabPageIndex + 1];

                            if (tabControl.TabPages.Count - 1 == tabControl.SelectedTabPageIndex)
                            {
                                btnAddToCard.Visible = true;
                                btnWithoutOption.Visible = false;
                                btnNextPrevious.Text = "PREVIOUS";

                            }
                            else
                            {
                                btnAddToCard.Visible = false;
                                btnWithoutOption.Visible = true;
                                btnNextPrevious.Text = "NEXT";

                            }
                        }
                        else
                        {
                            btnAddToCard.Visible = true;
                            btnNextPrevious.Visible = false;

                            TempOptionListAdd(OptionItem.MenuType, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());

                    }


                }

                gridControlOption.DataSource = optionItemDataTable;
                gridViewOption.ExpandAllGroups();


            }
            catch (Exception ex)
            {

                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
            }



        }

        
        private Button btnAddToCard;
        private Button btnNextPrevious;
        private Button btnWithoutOption;

        public List<Button> NextPreviousButtonAdd(int tabPageCount, string MenuType, bool IsUpdate)
        {
            List<Button> ListOfButton = new List<Button>();

            btnWithoutOption = new Button
            {
                Text = "ON IT'S OWN",
                Dock = DockStyle.Top,
                Name = "Finish",
                FlatStyle = FlatStyle.Flat,
                Width = 290,
                Height = 60,
                BackColor = Color.CornflowerBlue,
                Visible = true,
                Font = new Font("Tahoma", 18),
                FlatAppearance = { BorderColor = Color.AliceBlue, BorderSize = 1 },
                ForeColor = Color.White
            };

            btnNextPrevious = new Button
            {
                Text = "NEXT",
                Dock = DockStyle.Top,
                Name = "btnPreviousNext",
                FlatStyle = FlatStyle.Flat,
                Width = 290,
                Height = 60,
                BackColor = Color.Coral,
                FlatAppearance = { BorderColor = Color.AliceBlue, BorderSize = 1 },
                Font = new Font("Tahoma", 18),

                ForeColor = Color.White
            };
            btnAddToCard = new Button
            {
                Text = "ADD TO ORDER",
                Dock = DockStyle.Top,
                Name = "Finish",
                FlatStyle = FlatStyle.Flat,
                Width = 290,
                Height = 60,
                BackColor = Color.LimeGreen,
                Visible = false,
                Font = new Font("Tahoma", 18),
                FlatAppearance = { BorderColor = Color.AliceBlue, BorderSize = 1 },
                ForeColor = Color.White
            };


            btnWithoutOption.Click += new EventHandler((sender, args) =>
            {

                try
                {
                    TempOptionListAdd(MenuType, IsUpdate);
                }
                catch (Exception ex)
                {

                    new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                }







            });


            btnNextPrevious.Click += new EventHandler((sender, args) =>
            {
                try
                {
                    int currentSelectedTab = tabControl.SelectedTabPageIndex;


                    if (btnNextPrevious.Text == "NEXT")
                    {
                        tabControl.TabPages[currentSelectedTab + 1].PageEnabled = true;
                        tabControl.SelectedTabPageIndex = currentSelectedTab + 1;

                        if (tabControl.TabPages.Count - 1 == tabControl.SelectedTabPageIndex)
                        {
                            btnAddToCard.Visible = true;
                            btnNextPrevious.Text = "PREVIOUS";
                            btnWithoutOption.Visible = false;

                        }
                        else
                        {
                            btnAddToCard.Visible = false;
                            btnWithoutOption.Visible = true;
                        }
                    }
                    else if (btnNextPrevious.Text == "PREVIOUS")
                    {
                        tabControl.SelectedTabPageIndex = currentSelectedTab - 1;

                        if (tabControl.SelectedTabPageIndex == 0)
                        {
                            btnAddToCard.Visible = false;
                            btnWithoutOption.Visible = true;
                            btnNextPrevious.Text = "NEXT";
                        }
                        else
                        {
                            btnWithoutOption.Visible = false;
                            btnAddToCard.Visible = false;

                        }
                    }





                }
                catch (Exception ex)
                {

                    new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                }




            });

            btnAddToCard.Click += new EventHandler((sender, args) =>
            {
                try
                {
                    TempOptionListAdd(MenuType, IsUpdate);
                }
                catch (Exception ex)
                {

                    new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                }


            });
            ListOfButton.Add(btnWithoutOption);
            ListOfButton.Add(btnNextPrevious);
            ListOfButton.Add(btnAddToCard);



            return ListOfButton;
        }







        /// Click Event ////
      
        MultipleItem multipleItem = new MultipleItem();

       private void ReceipeMenuItemButton_Click(object sen5, EventArgs e)
        {

            //  int itemIndex = GetAllItemWithOption(aReceipeMenuItemButton);

            aOthersMethod.NumberPadClose();
            aOthersMethod.KeyBoardClose();
            try
            {
                RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
                CartItem aReceipeSubCategoryButton = sen5 as CartItem;

                ReceipeMenuItemButton menuItemButton = sen5 as ReceipeMenuItemButton;

                if (menuItemButton != null)
                {
                    aReceipeSubCategoryButton = new CartItem();
                    aReceipeSubCategoryButton.CategoryId = menuItemButton.CategoryId;
                    aReceipeSubCategoryButton.RecipeMenuItemId = menuItemButton.RecipeMenuItemId;
                    aReceipeSubCategoryButton.Recipeid = menuItemButton.RecipeTypeId;
                    aReceipeSubCategoryButton.ItemType = "SingleItem";

                }

                List<RecipeOptionButton> OptionList = new RestaurantMenuBLL().GetRecipeOptionWhenItemClick(aReceipeSubCategoryButton.RecipeMenuItemId).ToList();
                if (OptionList.Count > 0)
                {

                    int recipId = Convert.ToInt32(aReceipeSubCategoryButton.RecipeMenuItemId);
                    menuItemButton = aRestaurantMenuBll.GetRecipeByItemId(recipId);
                    menuItemButton.BackColor = aReceipeSubCategoryButton.Appearance.BackColor;

                    ItemOptionLoad(OptionList, menuItemButton, aReceipeSubCategoryButton.MenuType, false);


                    return;
                }



                ReceipeMenuItemButton aReceipeMenuItemButton = new ReceipeMenuItemButton();
                if (aReceipeSubCategoryButton != null)
                {
                    int recipId = Convert.ToInt32(aReceipeSubCategoryButton.RecipeMenuItemId);
                    aReceipeMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(recipId);
                    aReceipeMenuItemButton.RecipeTypeId = aReceipeSubCategoryButton.TypeId;

                }
                if (multipleItem.IsCheckedQuater(pannelTopBar) > 0)
                {
                    aReceipeSubCategoryButton.ReceipeMenuItemButton = menuItemButton;
                    if (aGeneralInformation.TableId == 0)
                    {
                        aReceipeMenuItemButton.InPrice = aReceipeSubCategoryButton.ReceipeMenuItemButton.OutPrice;
                    }
                    int limit = multipleItem.AddToMultipleCart(aReceipeSubCategoryButton);
                    if (limit > 0)
                    {
                        AddToMainCartMultipleItem();
                        MultiInformationClear();
                    }
                    return;
                }
                if (aReceipeMenuItemButton == null || aReceipeMenuItemButton.RecipeMenuItemId <= 0)
                {
                    MessageBox.Show("Item not found");
                    return;
                }
                AddToCard(aReceipeMenuItemButton, Convert.ToString(aReceipeSubCategoryButton.Tag), aReceipeSubCategoryButton);

            }
            catch (Exception ex)
            {
                // new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());

            }


        }

        /// <summary>
        /// / New Add To card System///
       

        public void AddToCard(ReceipeMenuItemButton aMenuItemButton, string itemName, TileItem backColor)
        {


            var ItemNameChecked = Properties.Settings.Default.ItemNameChanged;

            if (ItemNameChecked == "ReceiptName" && aMenuItemButton.RecipePackageButton == null)
            {
                aMenuItemButton.ShortDescrip = aMenuItemButton.ReceiptName;
            }
            else
            {
                aMenuItemButton.ShortDescrip = aMenuItemButton.RecipePackageButton.PackageName;
                aMenuItemButton.InPrice = aMenuItemButton.RecipePackageButton.InPrice;
                aMenuItemButton.OutPrice = aMenuItemButton.RecipePackageButton.OutPrice;
                aMenuItemButton.CategoryId = aMenuItemButton.CategoryId;
                aMenuItemButton.RecipeTypeId = aMenuItemButton.RecipePackageButton.RecipeTypeId;
                aMenuItemButton.ItemType = "Package";

            }

            for (int i = 0; i < gridViewAddtocard.DataRowCount; i++)
            {
                var check = gridViewAddtocard.GetRowCellValue(i, gridViewAddtocard.Columns["Name"].FieldName).ToString();
                var Optionid = gridViewAddtocard.GetRowCellValue(i, gridViewAddtocard.Columns["OptionId"].FieldName).ToString();
                var CatId = gridViewAddtocard.GetRowCellValue(i, gridViewAddtocard.Columns["Cat"].FieldName).ToString();
                if (check == aMenuItemButton.ShortDescrip || CatId == aMenuItemButton.CategoryId.ToString() && (Optionid == aMenuItemButton.OptionList))
                {
                    var qty = Convert.ToInt16(gridViewAddtocard.GetRowCellValue(i, gridViewAddtocard.Columns["QTY"].FieldName)) + 1;

                    decimal cellValue = Convert.ToInt16(qty) * Convert.ToDecimal(gridViewAddtocard.GetRowCellValue(i, gridViewAddtocard.Columns["Price"]));
                    gridViewAddtocard.SetRowCellValue(i, gridViewAddtocard.Columns["Total"], cellValue);
                    gridViewAddtocard.SetRowCellValue(i, gridViewAddtocard.Columns["QTY"], qty);
                    ShopingCard();
                    return;

                }

            }


            dr = dataTable.NewRow();
            dr[0] = dataTable.Rows.Count + 1;
            dr[1] = aMenuItemButton.CategoryId;
            dr[2] = 1;

            dr[3] = aMenuItemButton.ShortDescrip;
            if (aGeneralInformation.TableId > 0)
            {
                dr[4] = aMenuItemButton.InPrice;
            }
            else
            {

                dr[4] = aMenuItemButton.OutPrice;
            }


            dr[5] = Convert.ToInt16(dr[2]) * Convert.ToDecimal(dr[4]);
            dr[6] = Convert.ToUInt64(aMenuItemButton.RecipeMenuItemId);
            dr[7] = Convert.ToUInt64(aMenuItemButton.RecipeTypeId);


            if (aMenuItemButton.OptionList == null)
            {
                dr[8] = aMenuItemButton.ShortDescrip;
            }
            else
            {
                dr[8] = aMenuItemButton.ItemName;
                dr["OptionId"] = aMenuItemButton.OptionJson;
                string OptionJson = JsonConvert.SerializeObject(aMenuItemButton.OptionJson);
                dr["OptionJson"] = OptionJson;
            }


            if (aMenuItemButton.ItemType == null)
            {
                dr["Group"] = "SingleItem";
            }
            else
            {
                dr["Group"] = aMenuItemButton.ItemType;
            }

            dr["GroupName"] = flowLayoutPanel1.Controls[0].Text;
            if (dataTable.AsEnumerable().Count() > 1)
            {

                dr["GroupName"] = "";

            }
            var SortOrder = GetAllCatergory.FirstOrDefault(a => a.CategoryId == Convert.ToInt32(dr["Cat"]));
            if (SortOrder != null)
            {
                dr["SortOrder"] = SortOrder.SortOrder;
            }

            var MultiplePackage = aMenuItemButton.GetMultiItemList;
            if (MultiplePackage != null)
            {
                dr["Package"] = MultiplePackage;
                dr["SortOrder"] = 0;
                dr["EditName"] = itemName;
            }

            dr["kitichineDone"] = 0;
            dataTable.Rows.Add(dr);

            gridViewAddtocard.FocusedRowHandle = 0;
            // gridViewAddtocard.MakeRowVisible(i);
            gridViewAddtocard.SelectRow(0);


            gridViewAddtocard.CollapseGroupRow(0);
            //  gridViewAddtocard.OptionsSelection.EnableAppearanceFocusedRow = false;

            //= ColumnSortOrder.Ascending;

            ShopingCard();




            //.Rows[value.Index].cells['IsActive'].BackColor= Color.Red; 

        }
        private string GetMultipleMenu(List<MultipleItemMD> itemDetails, int TypeId)
        {

            List<MultipleMenuDetailsJsonResponsivePage> aMultipleMenuDetailsJsons = new List<MultipleMenuDetailsJsonResponsivePage>();
            foreach (MultipleItemMD itemMd in itemDetails)
            {
                MultipleMenuDetailsJsonResponsivePage aJson = new MultipleMenuDetailsJsonResponsivePage();
                aJson.category_id = itemMd.CategoryId;
                aJson.id = itemMd.ItemId;
                aJson.kitchen_section = 0;

                string[] name = itemMd.ItemName.Replace("</br>", "#").Split('#');

                aJson.name = name[0];
                aJson.options = itemMd.OptionName;
                aJson.minus_options = null;
                aJson.price = itemMd.Price;
                aJson.quantity = itemMd.Qty;
                aJson.recipe_type_id = TypeId;
                aJson.sort_order = 0;

                aMultipleMenuDetailsJsons.Add(aJson);
            }

            var json = JsonConvert.SerializeObject(aMultipleMenuDetailsJsons);

            return json;

        }
        public void AddToCardFixedItemForRetrive(List<PackageItem> packageItems, RecipePackageButton packageButton, GridView packageGrid, string update)
        {
            PackageItem package = PackageAllComponent.FixedPackageItemBind(packageItems, packageButton, packageGrid);

            for (int i = 0; i < gridViewAddtocard.RowCount; i++)
            {
                int packageId = Convert.ToInt32(gridViewAddtocard.GetFocusedRowCellValue("Cat"));
                string name = Convert.ToString(gridViewAddtocard.GetFocusedRowCellValue("Name"));
                string Group = Convert.ToString(gridViewAddtocard.GetFocusedRowCellValue("Group"));
                if (packageId == packageButton.PackageId && package.ItemName == name)
                {
                    var qty = Convert.ToInt16(gridViewAddtocard.GetRowCellValue(i, gridViewAddtocard.Columns["QTY"].FieldName)) + 1;

                    decimal cellValue = Convert.ToInt16(qty) * Convert.ToDecimal(gridViewAddtocard.GetRowCellValue(i, gridViewAddtocard.Columns["Price"]));
                    gridViewAddtocard.SetRowCellValue(i, gridViewAddtocard.Columns["Total"], cellValue);
                    gridViewAddtocard.SetRowCellValue(i, gridViewAddtocard.Columns["QTY"], qty);
                    ShopingCard();
                    return;
                }

                //if (gridViewAddtocard)
                //{
                //}

            }


            if (update == "Update")
            {


                int focusRow = gridViewAddtocard.GetFocusedDataSourceRowIndex();
                dataTable.Rows.RemoveAt(focusRow);


            }



            dr = dataTable.NewRow();
            dr[0] = dataTable.Rows.Count + 1;
            dr[1] = packageButton.PackageId;
            if (packageButton.ItemQty > 0)
            {
                dr[2] = packageButton.ItemQty;
            }
            else
            {
                dr[2] = 1;
            }


            dr["EditName"] = packageButton.PackageName;dr["Group"] = "Package";
            dr["KitichineDone"] = packageButton.ItemQty;

            for (int i = 0; i < packageItems.Count; i++)
            {
                packageItems[i].RecipePackageButton = packageButton;
            }
            dr["Package"] = packageItems;
            dr[3] = package.ItemName;
            double packageItemOptionPriceAdd = 0.0;

            if (packageItems.Count > 0)
            {
                if (packageItems.Any(a => a.PackageItemOptionList != null))
                {
                    packageItemOptionPriceAdd = packageItems.Sum(a => a.PackageItemOptionList.Sum(b => b.Price));
                }
            }

            double packageItemPrice = packageItems.Sum(a => a.Price);
            packageItemOptionPriceAdd = packageItemPrice + packageItemOptionPriceAdd;

            if (aGeneralInformation.TableId > 0)
            {
                dr[4] = packageButton.ExtraItemPrice / packageButton.ItemQty;

                if (packageButton.ExtraItemPrice.Equals(packageButton.InPrice + packageItemOptionPriceAdd))
                {
                    dr[4] = packageButton.InPrice + packageItemOptionPriceAdd;

                }

            }
            else
            {

                dr[4] = packageButton.ExtraItemPrice / packageButton.ItemQty;
                if (packageButton.ExtraItemPrice.Equals(packageButton.OutPrice + packageItemOptionPriceAdd))
                {
                    dr[4] = packageButton.OutPrice + packageItemOptionPriceAdd;
                }


            }








            dr[5] = Convert.ToInt16(dr[2]) * Convert.ToDecimal(dr[4]);
            dr[6] = Convert.ToUInt64(0);
            dr[7] = Convert.ToUInt64(packageButton.RecipeTypeId);
            dr["GroupName"] = "Package";
            dr["SortOrder"] = 0;
           
            dataTable.Rows.Add(dr);

            gridViewAddtocard.FocusedRowHandle = 0; // gridViewAddtocard.MakeRowVisible(i);
            gridViewAddtocard.SelectRow(0);

            ShopingCard();

        }
        public void AddToCardFixedItem(List<PackageItem> packageItems, RecipePackageButton packageButton, GridView packageGrid, string update)
        {
            PackageItem package = PackageAllComponent.FixedPackageItemBind(packageItems, packageButton, packageGrid);

            //List<OptionJson> OptionListJson = new List<OptionJson>();
            //if (packageItems.Count > 0)
            //{
            //    foreach (PackageItem option in packageItems)
            //    {
            //        foreach (RecipeOptionMD recipeOptionMd in option.PackageItemOptionList)
            //        {
            //            OptionListJson.Add(new OptionJson
            //            {
            //                optionId = recipeOptionMd.RecipeOptionId,
            //                optionName = recipeOptionMd.OptionName,
            //                price = recipeOptionMd.Price,
            //                qty = recipeOptionMd.Qty
            //            });
            //        }

            //    }
            //}



            for (int i = 0; i < gridViewAddtocard.RowCount; i++)
            {
                int packageId = Convert.ToInt32(gridViewAddtocard.GetFocusedRowCellValue("Cat"));
                string name = Convert.ToString(gridViewAddtocard.GetFocusedRowCellValue("Name"));

                string Group = Convert.ToString(gridViewAddtocard.GetFocusedRowCellValue("Group"));


                if (packageId == packageButton.PackageId && package.ItemName == name)
                {
                    var qty = Convert.ToInt16(gridViewAddtocard.GetRowCellValue(i, gridViewAddtocard.Columns["QTY"].FieldName)) + 1;

                    decimal cellValue = Convert.ToInt16(qty) * Convert.ToDecimal(gridViewAddtocard.GetRowCellValue(i, gridViewAddtocard.Columns["Price"]));
                    gridViewAddtocard.SetRowCellValue(i, gridViewAddtocard.Columns["Total"], cellValue);
                    gridViewAddtocard.SetRowCellValue(i, gridViewAddtocard.Columns["QTY"], qty);
                    ShopingCard();
                    return;
                }




                //if (gridViewAddtocard)
                //{
                //}

            }


            if (update == "Update")
            {


                int focusRow = gridViewAddtocard.GetFocusedDataSourceRowIndex();
                dataTable.Rows.RemoveAt(focusRow);


            }



            dr = dataTable.NewRow();
            dr[0] = dataTable.Rows.Count + 1;
            dr[1] = packageButton.PackageId;
            if (packageButton.ItemQty > 0)
            {
                dr[2] = packageButton.ItemQty;
            }
            else
            {
                dr[2] = 1;
            }


            dr["EditName"] = packageButton.PackageName;
            dr["Group"] = "Package";
            for (int i = 0; i < packageItems.Count; i++)
            {
                packageItems[i].RecipePackageButton = packageButton;

            } dr["Package"] = packageItems;
            dr[3] = package.ItemName;
            double packageItemOptionPriceAdd = 0.0;
            if (packageItems.Count > 0)
            {
                if (packageItems.Any(a => a.PackageItemOptionList != null))
                {

                    packageItemOptionPriceAdd = packageItems.Sum(a => a.PackageItemOptionList.Sum(b => b.Price));

                }
            }
            double packageItemPrice = packageItems.Sum(a => a.Price);
            packageItemOptionPriceAdd = packageItemPrice + packageItemOptionPriceAdd;
            if (aGeneralInformation.TableId > 0)
            {

                dr[4] = packageButton.InPrice + packageItemOptionPriceAdd;
            }
            else
            {
                dr[4] = packageButton.OutPrice + packageItemOptionPriceAdd;
            }


            dr[5] = Convert.ToInt16(dr[2]) * Convert.ToDecimal(dr[4]);



            dr[6] = Convert.ToUInt64(0);
            //  dr[6] = Convert.ToUInt64(packageButton.RecipeMenuItemId);
            dr[7] = Convert.ToUInt64(packageButton.RecipeTypeId);
            dr["GroupName"] = "Package";

            dr["SortOrder"] = 0;
            //GetAllCatergory.FirstOrDefault(a => a.CategoryId == Convert.ToInt32(dr["Cat"]));

            //if (packageButton.OptionList == null)
            //{
            //    dr[8] = aMenuItemButton.ShortDescrip;
            //}
            //else
            //{
            //    dr[8] = aMenuItemButton.ItemName;
            //    dr["OptionId"] = aMenuItemButton.OptionList;
            //}


            //if (aMenuItemButton.ItemType == null)
            //{
            //    dr["Group"] = "SingleItem";
            //}
            //else
            //{
            //    dr["Group"] = aMenuItemButton.ItemType;
            //}
            dr["KitichineDone"] = package.Qty;
            dataTable.Rows.Add(dr);

            gridViewAddtocard.FocusedRowHandle = 0; // gridViewAddtocard.MakeRowVisible(i);
            gridViewAddtocard.SelectRow(0);

            //  gridViewAddtocard.OptionsSelection.EnableAppearanceFocusedRow = false;

            //= ColumnSortOrder.Ascending;

            ShopingCard();

        }




        DataTable table = new DataTable();

      
        private void gridView1_MasterRowEmpty(object sender, MasterRowEmptyEventArgs e)
        {
            GridView view = sender as GridView;
            DataRow dataRow = view.GetDataRow(e.RowHandle);

            if (dataRow != null)
            {

                var group = Convert.ToString(dataRow["Group"]);
                var Cat = Convert.ToString(dataRow["Cat"]);

                if (group != "Package" && Cat != "0")

                    e.IsEmpty = e.RelationIndex == 0;
                else
                // e.IsEmpty = !dataTable.AsEnumerable().Any(a => a["Group"].ToString() == pakage[e.RowHandle]["Group"].ToString()); ;
                {
                    var index = (e.RelationIndex == 1);
                    e.IsEmpty = index;

                }
            }
        }
        private object GetRowKey(ColumnView view, int rowHandle)
        {
            return view.GetRowCellValue(rowHandle, "Group");
        }
        Hashtable cache = new Hashtable();


        private void gridView1_MasterRowGetChildList(object sender, MasterRowGetChildListEventArgs e)
        {

            // e.ChildList = GetDetailData((ColumnView)sender, e.RowHandle);
        }

        private void gridView1_MasterRowGetRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;

        }

        private void gridView1_MasterRowGetRelationName(object sender, MasterRowGetRelationNameEventArgs e)
        {

            e.RelationName = "Package";

        }

        private void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            GridView dView = gridViewAddtocard.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;

            dView.OptionsView.ShowIndicator = false;
            dView.OptionsView.ShowViewCaption = false;
            dView.OptionsView.ShowColumnHeaders = false;
            dView.OptionsView.ShowVerticalLines = DefaultBoolean.False;
            dView.OptionsDetail.ShowDetailTabs = false;



            //   gridControlAddTocard.FocusedView = dView;

            gridView1 = dView;
            gridView1.OptionsBehavior.EditingMode = GridEditingMode.EditFormInplace;
            gridView1.OptionsEditForm.EditFormColumnCount = 1;
            gridView1.OptionsEditForm.PopupEditFormWidth = 300;
            //dView.Columns["Index"].Visible = false;
            //dView.Columns["Qty"].Width = 5;
            //dView.Columns["Name"].Width = 200;
            //dView.Columns["Price"].Width = 20;
            //dView.Columns["Qty"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;




        }
        /// </summary>
        /// <param name="sender12131231"></param>
        /// <param name="e"></param>



        public decimal ShopingCard()
        {
            double SubTotal = Convert.ToDouble(columnTotal.SummaryItem.SummaryValue) - aGeneralInformation.OrderDiscount + aGeneralInformation.CardFee + aGeneralInformation.ServiceCharge + aGeneralInformation.DeliveryCharge + aGeneralInformation.ItemDiscount;

            try
            {
                gridViewAddtocard.UpdateTotalSummary();
                //btnHideShowCard.Text = "$"+Convert.ToString(gridColumn5.SummaryItem.SummaryValue);

                btnShopingAdd.Appearance.TextOptions.WordWrap = WordWrap.Wrap;

                var itemQty = columnQty.SummaryItem.SummaryValue;

                if (customTotalTextBox.Text != "Custom Total")
                {
                    btnShopingAdd.Text = "<b>" + "<color= Red>" + itemQty + "</color>" + "</b>" + "<br>" + "£" +
                                         customTotalTextBox.Text;
                    btnShopingAdd.Font = new Font("Microsoft Sans Serif", 10);

                    SubTotal = Convert.ToDouble(customTotalTextBox.Text);
                    totalAmountLabel.Text = "£" + Convert.ToString(customTotalTextBox.Text);
                    customTotalTextBox.ForeColor = Color.Red;
                    return Convert.ToDecimal(SubTotal);
                }

                totalAmountLabel.Text = "£" + SubTotal.ToString("N2");

                btnShopingAdd.Text = "<b>" + "<color= Red>" + itemQty + "</color>" + "</b>" + "<br>" + totalAmountLabel.Text;
                btnShopingAdd.Font = new Font("Microsoft Sans Serif", 10);


                customTotalTextBox.ForeColor = Color.SlateGray;

                return Convert.ToDecimal(SubTotal);
            }
            catch (Exception e)
            {
                return Convert.ToDecimal(SubTotal);

            }
        }

        private void gridViewAddtocard_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gridViewAddtocard == null)
            {

                return;
            }

            if (e.Column.FieldName == "QTY")
            {
                decimal cellValue = Convert.ToDecimal(e.Value) *
                                    Convert.ToDecimal(gridViewAddtocard.GetRowCellValue(e.RowHandle,
                                        gridViewAddtocard.Columns["Price"]));

                gridViewAddtocard.SetRowCellValue(e.RowHandle, gridViewAddtocard.Columns["Total"], cellValue);

                return;
            }
            else if (e.Column.FieldName == "Price")
            {
                decimal cellValue =
                    Convert.ToInt16(gridViewAddtocard.GetRowCellValue(e.RowHandle, gridViewAddtocard.Columns["QTY"])) *
                    Convert.ToDecimal(gridViewAddtocard.GetRowCellValue(e.RowHandle, gridViewAddtocard.Columns["Price"]));

                gridViewAddtocard.SetRowCellValue(e.RowHandle, gridViewAddtocard.Columns["Total"], cellValue);


            }
            else if (e.Column.FieldName == "EditName")
            {
                string cellValue = e.Value.ToString();

                string name = gridViewAddtocard.GetRowCellValue(e.RowHandle, gridViewAddtocard.Columns["Name"]).ToString();

                //string name = gridViewAddtocard.GetRowCellValue(e.RowHandle, gridViewAddtocard.Columns["Name"]).ToString().Replace("&rarr;", ",");

                if (name.Contains("</h4>"))
                {

                    string removeString = name.Substring(0, name.IndexOf("</h4>"));
                    string list = name.Substring(removeString.Length).Replace("</h4>", "");

                    //    string trim2nd = removeString.Substring(0, removeString.IndexOf(','));
                    //    //orderItem.Name = trim2nd;
                    //    string Options = removeString.Substring(trim2nd.Length + 1);
                    string mixText = "<h4  style='margin:0px;'>" + cellValue + "</h4>" + list;
                    if (gridViewAddtocard.GetRowCellValue(e.RowHandle, "Group").ToString() == "MultipleItem")
                    {
                        mixText = "<div>" + "<h4  style='margin:0px;'>" + cellValue + "</h4>" + list;
                    }

                    gridViewAddtocard.SetRowCellValue(e.RowHandle, gridViewAddtocard.Columns["Name"], mixText);


                }
                else
                {
                    gridViewAddtocard.SetRowCellValue(e.RowHandle, gridViewAddtocard.Columns["Name"], cellValue);

                }



            }

            ShopingCard();

        }


        public void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            LoadMenuType();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadSettingsForm("settings");



        }

        bool cardProperty = GlobalSetting.SettingInformation.IsCardVisible;

        public void CardVisibleInvisible(bool cardStatus)
        {
            int itemCount = tileGroup1.Items.Count;
            if (cardStatus)
            {
                dockPanel1.Visibility = DockVisibility.Visible;

                ResponsiveItem(itemCount, true, tileControl1);
                btnShopingAdd.Visible = false;
            }
            else 
            {
                dockPanel1.Visibility = DockVisibility.Hidden;

                ResponsiveItem(itemCount, false, tileControl1);
                btnShopingAdd.Visible = true;
            }
        }




        private void LoadSettingsForm(string openpage)
        {

            AllSettingsForm.Status = "";
            RestaurantOrdersReport.OrderId = 0;
            AllSettingsForm aAllSettingsForm = new AllSettingsForm(openpage, null, this);
            aAllSettingsForm.ShowDialog();
            LoadAllPrinter();
            if (openpage == "LocalOrder")
            {

            }
            else if (RestaurantOrdersReport.OrderId > 0)
            {
                orderLoadStatus = true;

                loadwaitform.ShowWaitForm();


                LoadAllSaveOrder((int)RestaurantOrdersReport.OrderId, "reorder");

                loadwaitform.CloseWaitForm();
            }
            else if (AllSettingsForm.Status == "logout")
            {
                if (aAdForm != null)
                {
                    aAdForm.Close();

                }
                Form tempForm = FormManage.aFormManage.Pop();
                tempForm.Show();
                resPlayer.Stop();
                player.Stop();
                this.Close();
            }
            else if (AllSettingsForm.Status == "reload")
            {
                if (aAdForm != null)
                {
                    aAdForm.Close();

                } Form tempForm = FormManage.aFormManage.Pop();
                tempForm.Show();
                this.Close();

                //mainForm aMainForm = new mainForm(); ;//aMainForm.Show();
                //this.Hide();
            }
        }

        private void InstantCardShowHidden()
        {
            int itemCount = tileGroup1.Items.Count;

            if (dockPanel1.Visibility == DockVisibility.Hidden)
            {
                dockPanel1.Visibility = DockVisibility.Visible;

                ResponsiveItem(itemCount, true, tileControl1);
                for (int i = 0; i < tabControl.TabPages.Count; i++)
                {
                    var tileGroupName = tabControl.TabPages[i].Controls;
                    TileControl tileControl = (TileControl)tileGroupName[0];

                    ResponsiveItemOption(tileGroup.Items.Count, true, tileControl);

                }




                if (aItemFormLoadNew != null)
                {
                    aItemFormLoadNew.ResponsiveItem(aItemFormLoadNew.tileGroupContainer.Items.Count, true, aItemFormLoadNew.tillMainControl);

                }

                btnShopingAdd.Visible = false;

            }
            else if (dockPanel1.Visibility == DockVisibility.Visible)
            {
                dockPanel1.Visibility = DockVisibility.Hidden;

                ResponsiveItem(itemCount, false, tileControl1);

                for (int i = 0; i < tabControl.TabPages.Count; i++)
                {
                    var tileGroupName = tabControl.TabPages[i].Controls;
                    TileControl tileControl = (TileControl)tileGroupName[0];
                    ResponsiveItemOption(tileGroup.Items.Count, false, tileControl);

                }

                if (aItemFormLoadNew != null)
                {
                    aItemFormLoadNew.ResponsiveItem(aItemFormLoadNew.tileGroupContainer.Items.Count, false, aItemFormLoadNew.tillMainControl);
                }

                btnShopingAdd.Visible = true;


            }
        }

        private void btnShopingAdd_Click(object sender, EventArgs e)
        {

            InstantCardShowHidden();
        }

        private void totalAmountLabel_Click(object sender, EventArgs e)
        {
            //InstantCardShowHidden();
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
                    bool flag = true;

                    double focusqty = Convert.ToDouble(gridViewAddtocard.GetFocusedRowCellValue("QTY"));
                    double focusprice = Convert.ToDouble(gridViewAddtocard.GetFocusedRowCellValue("Price"));
                    string focusName = Convert.ToString(gridViewAddtocard.GetFocusedRowCellValue("EditName"));
                    double attPrice = AttributeForm.Price;
                    double attdiscount = AttributeForm.Discount;
                    if (attdiscount > 0)
                    {

                        focusprice = focusprice - (focusprice * (attdiscount / 100));

                    }

                    var totalPriceLabel = (focusqty * (focusprice + attPrice)).ToString("F02");
                    var priceTextBox = (focusprice + attPrice).ToString("F02");
                    var name = focusName + "+" + AttributeForm.AttributeName;


                    gridViewAddtocard.SetFocusedRowCellValue("Price", priceTextBox);
                    gridViewAddtocard.SetFocusedRowCellValue("EditName", name);
                    gridViewAddtocard.SetFocusedRowCellValue("Total", totalPriceLabel);
                    gridViewAddtocard.SetFocusedRowModified();


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

        private void price1Button_Click(object sender, EventArgs e)
        {
            ChangePriceButton(price1Button);
        }

        private void price2Button_Click(object sender, EventArgs e)
        {
            ChangePriceButton(price2Button);
        }


        private void ChangePriceButton(Button button)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            try
            {
                if (gridViewAddtocard.RowCount > 0)
                {
                    string gridPackage = gridViewAddtocard.GetFocusedRowCellValue("Group").ToString();

                    if (gridPackage == "Package")
                    {
                        return;

                    }


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

                    bool flag = true;



                    double focusqty = Convert.ToDouble(gridViewAddtocard.GetFocusedRowCellValue("QTY"));
                    double focusprice = Convert.ToDouble(gridViewAddtocard.GetFocusedRowCellValue("Price")) + exprice;

                    var totalPriceLabel = (focusqty * focusprice).ToString("F02");
                    var priceTextBox = focusprice.ToString("F02");

                    gridViewAddtocard.SetFocusedRowCellValue("Price", priceTextBox);
                    gridViewAddtocard.SetFocusedRowCellValue("Total", totalPriceLabel);
                    gridViewAddtocard.SetFocusedRowModified();

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
                                        cc.totalPriceLabel.Text = (packagePrice + exprice).ToString();


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

                        //if (flag)
                        //{
                        //    LoadOrderDetails(0);
                        //}
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }

        private void price3Button_Click(object sender, EventArgs e)
        {
            ChangePriceButton(price3Button);
        }

        private void price4Button_Click(object sender, EventArgs e)
        {
            ChangePriceButton(price4Button);
        }

        private void price5Button_Click(object sender, EventArgs e)
        {
            ChangePriceButton(price5Button);
        }

        private void DecrementPackageItemQty()
        {
            int selectFocuseQty = Convert.ToInt16(gridView1.GetFocusedRowCellValue("Qty"));
            if (selectFocuseQty > 1)
            {
                gridView1.SetFocusedRowCellValue("Qty", --selectFocuseQty);
            }
            else
            {
                var RemoveItemPakage = packageItem.FirstOrDefault(a => a.ItemId == (int)gridView1.GetFocusedRowCellValue("ItemId"));
                packageItem.Remove(RemoveItemPakage);
                gridView1.DeleteSelectedRows();




                if (packageItem.Count == 0)
                {
                    ClearAllData();
                }
            }

        }
        private void itemMinusButton_Click(object sender, EventArgs e)
        {
            if (rowPackageItemFocus)
            {
                DecrementPackageItemQty();
            }
            else
            {
                int selectFocuseQty = Convert.ToInt16(gridViewAddtocard.GetFocusedRowCellValue("QTY"));

                if (selectFocuseQty > 1)
                {
                    gridViewAddtocard.SetFocusedRowCellValue("QTY", --selectFocuseQty);
                    string checkPackage = gridViewAddtocard.GetFocusedRowCellValue("Group").ToString();

                    if (checkPackage == "Package")
                    {

                        List<PackageItem> packageItem = (List<PackageItem>)gridViewAddtocard.GetFocusedRowCellValue("Package");

                        List<PackageItem> temPackageItems = new List<PackageItem>();
                        foreach (PackageItem item in packageItem)
                        {
                            if (item.Qty > 1)
                            {
                              // item.Qty = item.Qty - 1;
                                item.Qty = (item.Qty / (selectFocuseQty-1)) * (selectFocuseQty - 1);
                             }
                            else
                            {
                                item.Qty = 1;
                            }

                            temPackageItems.Add(item);
                        }
                        PackageItem package = PackageAllComponent.FixedPackageItemBind(packageItem, packageItem[0].RecipePackageButton, null);
                        gridViewAddtocard.SetFocusedRowCellValue("Name", package.ItemName);
                        gridViewAddtocard.SetFocusedRowCellValue("Package", packageItem);

                    }
                }
                else
                {
                    gridViewAddtocard.DeleteSelectedRows();

                    if (dataTable.Rows.Count == 0)
                    {
                        ClearAllData();
                    }
                }


            }


            ShopingCard();

        }

        public void IncrementQtyPackageItem()
        {
            int selectFocuseQty = Convert.ToInt16(gridView1.GetFocusedRowCellValue("Qty"));

            if (selectFocuseQty >= 1)
            {

                gridView1.SetFocusedRowCellValue("Qty", ++selectFocuseQty);

            }


        }
        private void itemPlusButton_Click(object sender, EventArgs e)
        {



            if (rowPackageItemFocus)
            {
                IncrementQtyPackageItem();

            }
            else
            {
                int selectFocuseQty = Convert.ToInt16(gridViewAddtocard.GetFocusedRowCellValue("QTY"));
                if (selectFocuseQty >= 1)
                {
                    gridViewAddtocard.SetFocusedRowCellValue("QTY", ++selectFocuseQty);
                    string checkPackage = gridViewAddtocard.GetFocusedRowCellValue("Group").ToString();

                    if (checkPackage == "Package")
                    {
                        List<PackageItem> packageItem = (List<PackageItem>)gridViewAddtocard.GetFocusedRowCellValue("Package");

                        List<PackageItem> temPackageItems = new List<PackageItem>();
                        foreach (PackageItem item in packageItem)
                        {
                           // item.Qty = item.Qty+1;
                            item.Qty = (item.Qty * selectFocuseQty) / (selectFocuseQty-1);
                            temPackageItems.Add(item);
                        }PackageItem package = PackageAllComponent.FixedPackageItemBind(temPackageItems, packageItem[0].RecipePackageButton, null);

                        gridViewAddtocard.SetFocusedRowCellValue("Name", package.ItemName);
                        gridViewAddtocard.SetFocusedRowCellValue("Package", packageItem);
                    }
                }

            }



            ShopingCard();
        }

        public void MultiInformationClear()
        {

            multiItembtn.Visible = true;
            half.Visible = false;
            quater.Visible = false;
            dockPanel2.Visibility = DockVisibility.Hidden;
            LoadMenuType();

        }
        private void ClearAllData()
        {
            aGeneralInformation = new GeneralInformation();
            restaurantUsers = new RestaurantUsers(); multipleItem = new MultipleItem();
            customerTextBox.Text = "Search Customer";
            customerDetailsLabelNew.Visible = false;
            customerDetailsLabel.Text = "";
            commentTextBox.Text = "Comment";
            previousCustoTotalValue = 0;
            customTotalTextBox.Text = "Custom Total"; customerTextBox.Visible = true;
            discountButton.Text = "Disc\r\n0.00";
            aGeneralInformation.DeliveryCharge = 0.0;
            deliveryChargeButton.Text = "Delivery Charge\r\n0";
            billButton.Visible = false;
            finalizeButton.Visible = false;
            customerEditButtonNew.Visible = false;
            phoneNumberDeleteButton.Visible = false;
            collectionButton.Visible = true;
            timeButton.Visible = true;
            deliveryButton.Visible = true;
            deliveryButton.Text = "DEL";
            aGeneralInformation.OrderDiscount = 0.0;
            personButtonNew.Visible = false; personButtonNew.Text = "P";
            recentItemsFlowLayoutPanel.Visible = false;
            personButtonNew.Visible = false;
            personButtonNew.Text = "P";
            tableButtonNew.Text = "TABLE";
            aRestaurantInformation.DefaultOrderType = "CLT";
            collectionButton.PerformClick();
            searchCustomerpanel.Size = new Size(173, 43);
            OrderNo = 0;

            //.................................................................................

            MultiInformationClear();

            if (aRestaurantInformation.DefaultOrderType == "CLT" || aRestaurantInformation.DefaultOrderType == "WAIT")
            {
                collectionButton.BackColor = Color.Black;
                collectionButton.ForeColor = Color.WhiteSmoke;
                deliveryButton.BackColor = Color.WhiteSmoke;
                deliveryButton.ForeColor = Color.Black;
                if (aRestaurantInformation.DefaultOrderType == "WAIT")
                {
                    timeButton.Text = "CLT";


                    //    aGeneralInformation.OrderType = "CLT";
                }
                else
                {
                    timeButton.Text = "Time";
                }
                ChangeButtonLocation(false);
            }
            else if (aRestaurantInformation.DefaultOrderType == "DEL")
            {
                collectionButton.BackColor = Color.WhiteSmoke;
                deliveryButton.BackColor = Color.Black;
                deliveryButton.ForeColor = Color.WhiteSmoke;
                collectionButton.ForeColor = Color.Black;
                ChangeButtonLocation(true);
                aGeneralInformation.OrderType = "DEL";
                timeButton.Text = "Time";
            }





        }
        private void AllCardSytemClear()
        {
            ClearAllData();
            LoadMenuType();
            aRecipePackageMdList.Clear();
            dataTable.Rows.Clear();
            gridControlAddTocard.DataSource = dataTable;

            packageItem.Clear();
            ShopingCard();

        }

        private void orderAllClearButton_Click(object sender, EventArgs e)
        {

            optionItemDataTable.Rows.Clear();
            recentItemsFlowLayoutPanel.Controls.Clear();
            optionPanel.Controls.Clear();
            optionPanel.Visible = false;
            customTotalTextBox.Text = "Custom Total";
            AllCardSytemClear();

        }

        private void gridViewAddtocard_RowStyle(object sender, RowStyleEventArgs e)
        {

        }

        private void commentTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void customTotalTextBox_TextChanged(object sender, EventArgs e)
        {

            ShopingCard();

        }

        private void customTotalTextBox_Click(object sender, EventArgs e)
        {
            //try
            //{
            // //   aOthersMethod.KeyBoardClose();
            //    if (!Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
            //    {
            //        int x = rightPanel.Location.X - 195;
            //        Point aPoint = new Point(x, 280);
            //        NumberForm aNumberPad = new NumberForm(aPoint);
            //        aNumberPad.Show();

            //    }

            //}
            //catch (Exception exception)
            //{
            //    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //    aErrorReportBll.SendErrorReport(exception.ToString());
            //}
        }
        double previousCustoTotalValue = 0;
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
                        previousCustoTotalValue = customTotal;
                    }
                    else
                    {
                        if (previousCustoTotalValue > 0)
                        {
                            return;
                        }
                        customTotalTextBox.Text = "Custom Total";
                        customTotalTextBox.ForeColor = Color.SlateGray;


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
                        previousCustoTotalValue = price;
                        //do something
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        if (previousCustoTotalValue > 0)
                        {
                            return;
                        }
                        customTotalTextBox.Text = "Custom Total";
                        customTotalTextBox.ForeColor = Color.SlateGray;
                    }
                }


                //*********************************************


            }


        }

        private void customTotalTextBox_Enter(object sender, EventArgs e)
        {


            customTotalTextBox.Text = String.Empty;
            int x = dockPanel1.Location.X - 500;
            int y = Screen.Bounds.Bottom - 339;
            VirtualNumberPad(x, y);
        }

        public void TimerButtonAdd()
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();


            DeliveryTimeForm.TimeDetails = new TimeDetails();
            DeliveryTimeForm aDeliveryTimeForm =
                new DeliveryTimeForm(collectionButton.BackColor == Color.Black ? true : false);
            aDeliveryTimeForm.ShowDialog();
            TimeDetails aTimeDetails = DeliveryTimeForm.TimeDetails;
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
                    
                    aGeneralInformation.DeliveryTime = aTimeDetails.DeliveryTime;
                }

                else if (timeButton.Text.ToLower() == "wait")
                {
                    aGeneralInformation.OrderType = "Wait";
                    aGeneralInformation.DeliveryTime = aTimeDetails.DeliveryTime;
                }

            }

        }

        public List<OrderItem> GridPackageList(List<PackageItem> packageItem, int orderId, int packageIds, int packageValue)
        {

            List<OrderItem> packageItems = new List<OrderItem>();
            int optionIdkey = 0;
            foreach (PackageItem packageItemlist in packageItem)
            {

                OrderItem orderItem = new OrderItem();
               // orderItem.ExtraPrice = 0;
                orderItem.KitchenDone = 0;
                orderItem.KitchenProcessing = 1;
                orderItem.LastModifyTime = DateTime.Now;
                orderItem.Name = packageItemlist.ItemName;
                orderItem.OrderId = orderId;
                orderItem.Options = GetOptionsForpackGe(packageItemlist);
                orderItem.MinusOptions = GetMinusOptionForpackGes(packageItemlist);
                orderItem.PackageId = packageItemlist.PackageId;
                orderItem.Price = packageItemlist.Price;
                orderItem.ExtraPrice = packageItemlist.Price;
                orderItem.Quantity = packageItemlist.Qty;
                orderItem.RecipeId = packageItemlist.ItemId;


                if (packageIds != null)
                    orderItem.orderPackageId = packageIds;
                // optionIdkey++;

                //packageIds[itemDetails.OptionsIndex].Value; //ElementAt(itemDetails.OptionsIndex); //Select( [itemDetails.OptionsIndex];
                //orderItem.orderPackageId = packageIds.Where(i => i.Key == packageItemlist.OptionsIndex).Select(i => i.Value).First(); //packageIds[itemDetails.OptionsIndex].Value; //ElementAt(itemDetails.OptionsIndex); //Select( [itemDetails.OptionsIndex];

                orderItem.SentToKitchen = 1;
                if (packageItemlist.PackageItemOptionList != null)
                {
                    List<OptionJson> OptionJson = new List<OptionJson>();
                    List<OptionJson> NoOptionJson = new List<OptionJson>();

                    if (packageItemlist.PackageItemOptionList.Count > 0)
                    {
                        for (int i = 0; i < packageItemlist.PackageItemOptionList.Count; i++)
                        {
                            if (packageItemlist.PackageItemOptionList[i].Isoption)
                            {
                                NoOptionJson.Add(new OptionJson
                                {
                                    optionName = packageItemlist.PackageItemOptionList[i].Title,
                                    optionId = packageItemlist.PackageItemOptionList[i].RecipeOPtionItemId.ToString(),
                                    optionPrice = packageItemlist.PackageItemOptionList[i].Price,
                                    optionQty = (int)packageItemlist.PackageItemOptionList[i].Qty,
                                    NoOption = packageItemlist.PackageItemOptionList[i].Isoption

                                });
                            }
                            else
                            {
                                OptionJson.Add(new OptionJson
                                {
                                    optionName = packageItemlist.PackageItemOptionList[i].Title,
                                    optionId = packageItemlist.PackageItemOptionList[i].RecipeOPtionItemId.ToString(),
                                    optionPrice = packageItemlist.PackageItemOptionList[i].Price,
                                    optionQty = (int)packageItemlist.PackageItemOptionList[i].Qty,
                                    NoOption = packageItemlist.PackageItemOptionList[i].Isoption

                                });
                            }





                        }

                        orderItem.Options = new OptionJsonConverter().Serialize(OptionJson);
                        orderItem.MinusOptions = new OptionJsonConverter().Serialize(NoOptionJson);
                    }
                }

                packageItems.Add(orderItem);
            }



            return packageItems;
        }
        private string GetOptionsForpackGe(PackageItem itemDetails)
        {
            string options = "";
            List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex && a.Title != null).ToList();
            if (aOption.Count > 0)
            {

                foreach (RecipeOptionMD option in aOption)
                {
                    if (options != "") options += ", ";
                    options += option.Title;

                }
            }
            return options;
        }
        private string GetMinusOptionForpackGes(PackageItem itemDetails)
        {
            string options = "";
            List<RecipeOptionMD> aOption = aRecipeOptionMdList.Where(a => a.RecipeId == itemDetails.ItemId && a.OptionsIndex == itemDetails.OptionsIndex && a.MinusOption != null).ToList();
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

        OrderItem GetCheckNewItemAdd(int itemId)
        {
            var OldItem = tempDetailsMdsList.Where(a => a.RecipeTypeId == itemId);

            return new OrderItem();
        }
        private List<OrderItem> GetOrderItem(int orderId, List<KeyValuePair<int, int>> packageIds = null)
        {
            List<OrderItem> aOrderItem = new List<OrderItem>();

            for (int i = 0; i < gridViewAddtocard.RowCount; i++)
            {
                OrderItem orderItem = new OrderItem();
                int ReceiptMenuId = Convert.ToInt32(gridViewAddtocard.GetRowCellValue(i, "RecepiMenuId"));

                //OrderItem checkorderItem = GetCheckNewItemAdd(ReceiptMenuId);
              //orderItem.ExtraPrice = 0;
                orderItem.KitchenDone = 0;
                orderItem.KitchenProcessing = 1;
                orderItem.LastModifyTime = DateTime.Now;
                orderItem.OrderId = orderId;
                orderItem.SentToKitchen = 1;


                // orderItem.Options = GetOptions(itemDetails);
                // orderItem.MinusOptions = GetMinusOptions(itemDetails);
                orderItem.PackageId = 0;
                string name = gridViewAddtocard.GetRowCellValue(i, "Name").ToString().Replace("&rarr;", ",").Trim();
                string[] removeString = Regex.Replace(name, @"<[^>]+>|", "").Split(',');
                var IsPackage = gridViewAddtocard.GetRowCellValue(i, "Group").ToString();

                if (IsPackage != "MultipleItem" && IsPackage != "Package")
                {
                    var optionIsExist = gridViewAddtocard.GetRowCellValue(i, "OptionId");
                    if (optionIsExist.ToString() != string.Empty)
                    {
                        List<OptionJson> option = (List<OptionJson>)gridViewAddtocard.GetRowCellValue(i, "OptionId");
                        var listWithOption = option.Where(a => a.NoOption == false).ToList();
                        string optionId = new OptionJsonConverter().Serialize(listWithOption);
                        orderItem.Options = optionId;
                        orderItem.MinusOptions =
                             new OptionJsonConverter().Serialize(option.Where(a => a.NoOption).ToList());
                        orderItem.Options_ids = "";
                    }
                    orderItem.Name = removeString[0];
                }
                else if (IsPackage == "MultipleItem")
                {
                    string[] MultipleName = removeString[0].Split(':');
                    orderItem.Name = MultipleName[0];
                    List<PackageItem> package = (List<PackageItem>)gridViewAddtocard.GetRowCellValue(i, "Package");
                    List<MultipleItemMD> MultipleItemMD = package[0].MultipleItem;
                    orderItem.MultipleMenu = GetMultipleMenu(MultipleItemMD, package[0].ItemId);

                }

                orderItem.Price = Convert.ToInt16(gridViewAddtocard.GetRowCellValue(i, "QTY")) * Convert.ToDouble(gridViewAddtocard.GetRowCellValue(i, "Price"));
                orderItem.Quantity = Convert.ToInt16(gridViewAddtocard.GetRowCellValue(i, "QTY"));
                orderItem.RecipeId = Convert.ToInt32(gridViewAddtocard.GetRowCellValue(i, "RecepiMenuId"));



                if (IsPackage == "Package")
                {
                    List<PackageItem> package = (List<PackageItem>)gridViewAddtocard.GetRowCellValue(i, "Package");

                    if (packageIds == null)
                    {
                        foreach (PackageItem packageItem in package)
                        {



                            var isChcekDuplicateValue = aOrderItem.Count(a => a.RecipeId == packageItem.ItemId);

                            if (isChcekDuplicateValue == 0)
                            {
                                var listOrderPackageItem = GridPackageList(package, orderId, packageItem.Id, 0);
                                aOrderItem = aOrderItem.Concat(listOrderPackageItem).ToList();
                            }


                        }

                    }
                    else
                    {
                        int packageId = Convert.ToInt32(gridViewAddtocard.GetRowCellValue(i, "Cat"));
                        var PKeyId = packageIds.FirstOrDefault(k => k.Key == packageId).Value;


                        var listOrderPackageItem = GridPackageList(package, orderId, PKeyId, PKeyId);
                        aOrderItem = aOrderItem.Concat(listOrderPackageItem).ToList();

                        if (aOrderItem.Count(a => a.orderPackageId == PKeyId) > 0)
                        {
                            var removeKeyId = packageIds.FirstOrDefault(k => k.Key == packageId);
                            packageIds.Remove(removeKeyId);
                        }
                    }





                }
                else
                {
                    aOrderItem.Add(orderItem);

                }


            }


            return aOrderItem;
        }

        private void timeButton_Click(object sender, EventArgs e)
        {
            TimerButtonAdd();
        }

        public int OrderNo { get; set; }

        private void LoadAmountDetails()
        {
            ShopingCard();
            //double total;
            //if (!double.TryParse(customTotalTextBox.Text, out total))
            //{
            //    double amount1 = aOrderItemDetailsMDList.Sum(a => a.Qty * a.Price);

            //    totalAmountLabel.Text = "£" +
            //                            (amount1 + aGeneralInformation.CardFee + aGeneralInformation.ServiceCharge +
            //                             aGeneralInformation.DeliveryCharge - aGeneralInformation.OrderDiscount -
            //                             aGeneralInformation.ItemDiscount).ToString("F02");

            //}
            //else
            //{
            //    totalAmountLabel.Text = "£" + (total).ToString("F02");
            //}

        }

        private void deliveryChargeButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            aRecipePackageMdList.Clear();
            PackageSaveTemp();
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            if (deliveryChargeButton.Text != "SAVE")
            {
                DeliveryChargeForm.DeliveryChargeAmount = 0; DeliveryChargeForm aDeliveryChargeForm = new DeliveryChargeForm();
                aDeliveryChargeForm.ShowDialog();

                if (DeliveryChargeForm.DeliveryChargeAmount > 0)
                {
                    aGeneralInformation.DeliveryCharge = DeliveryChargeForm.DeliveryChargeAmount;
                    deliveryChargeButton.Text = aGeneralInformation.DeliveryCharge <= 0
                        ? "Delivery Charge\r\n0"
                        : "Delivery Charge\r\n" + aGeneralInformation.DeliveryCharge.ToString();
                    // LoadAmountDetails();
                    ShopingCard();


                }

            }
            else
            {
                if (!OthersMethod.CheckServerConneciton())
                {
                    return;
                }
                if (gridViewAddtocard.RowCount == 0)
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
                        aGeneralInformation.OrderType = aGeneralInformation.OrderType == null ||
                                                        aGeneralInformation.OrderType == ""
                            ? aRestaurantInformation.DefaultOrderType
                            : aGeneralInformation.OrderType;
                        //aGeneralInformation.DeliveryTime = aGeneralInformation.DeliveryTime == null ||
                        //                                   aGeneralInformation.DeliveryTime == ""
                        //    ? DateTime.Now.AddMinutes(aRestaurantInformation.DeliveryTime).ToString("HH:mm")
                        //    : aGeneralInformation.DeliveryTime;
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

                        List<OrderPackage> aOrderPackage = GetOrderPackage(result);
                        var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrderPackage);

                        List<OrderItem> aOrderItems = GetOrderItem(result, result2);

                        int result1 = aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrderItems);
                        aRestaurantOrder.isKitchenPrint = true;
                        aRestaurantOrder.OrderNo = OrderNo;
                        new PrintingHTML().PrintingOrder(aRestaurantOrder,false, false, this,null, true);
                    }
                    else
                    {
                        //List<OrderItem> aOrderItems = GetOrderItem(aGeneralInformation.OrderId);
                        //List<OrderPackage> aOrderPackage = GetOrderPackage(aGeneralInformation.OrderId);
                        //DeleteNewCancelItem(aOrderItems, aOrderPackage, aGeneralInformation.OrderId);
                        //bool result1 = aRestaurantOrderBLL.UpdateRestaurantOrderItem(aOrderItems, aGeneralInformation.OrderId);

                        RestaurantOrder aRestaurantOrder = GetRestaurantOrderForTableOrder();
                        aRestaurantOrder.Status = "pending";
                        aRestaurantOrder.Id = aGeneralInformation.OrderId;
                        bool result = aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);

                        List<OrderPackage> aOrderPackage = GetOrderPackage(aGeneralInformation.OrderId);
                        DeleteNewCancelItem(null, aOrderPackage, aGeneralInformation.OrderId);

                        //var result2 = aRestaurantOrderBLL.UpdateRestaurantOrderPackage(aOrderPackage, result);
                        var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrderPackage);

                        List<OrderItem> aOrderItems = GetOrderItem(aGeneralInformation.OrderId, result2);

                        int result1 = aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrderItems);





                        if (aGeneralInformation.TableId > 0)
                        {
                            RestaurantTable aRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableId(aGeneralInformation.TableId);
                            aRestaurantTable.CurrentStatus = "busy";
                            aRestaurantTableBll.UpdateRestaurantTable(aRestaurantTable);
                        }
                        aRestaurantOrder.isKitchenPrint = true;
                        aRestaurantOrder.OrderNo = aGeneralInformation.OrderNo;
                        new PrintingHTML().PrintingOrder(aRestaurantOrder, false, true, this, null, true);
                    }
                    ClearAllOrderInformation();
                    AllCardSytemClear();
                    //  ClearTableOrderDetails();
                }
                catch (Exception ex)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(ex.ToString());
                }
            }
        }



        private RestaurantOrder GetRestaurantOrderForTableOrder()
        {

            double totalCost = 0;
            RestaurantOrder aRestaurantOrder = new RestaurantOrder();
            aRestaurantOrder.Comment = commentTextBox.Text == "Comment" ? "" : commentTextBox.Text;
            aRestaurantOrder.DeliveryCost = aGeneralInformation.DeliveryCharge;
            aRestaurantOrder.DeliveryTime = Convert.ToDateTime(aGeneralInformation.DeliveryTime);
            //aRestaurantOrder.Discount = aGeneralInformation.ItemDiscount + aGeneralInformation.OrderDiscount;
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
            {aRestaurantOrder.TotalCost = GetTotalAmountDetails();}
            else
            {
                aRestaurantOrder.TotalCost = totalCost;
            }



            //  aRestaurantOrder.TotalCost = GetTotalAmountDetails() + aRestaurantOrder.DeliveryCost + aRestaurantOrder.CardFee - aRestaurantOrder.Discount;
            aRestaurantOrder.UserId = 0;
            aRestaurantOrder.CustomerId = aGeneralInformation.CustomerId;
            aRestaurantOrder.OrderTable = aGeneralInformation.TableId;
            aRestaurantOrder.Person = aGeneralInformation.Person;
            aRestaurantOrder.Status = "pending";
            aRestaurantOrder.RestaurantId = aRestaurantInformation.Id;
            aRestaurantOrder.UpdateTime = DateTime.Now.Date;
            if (aRestaurantOrder.CashAmount == 0 && aRestaurantOrder.CardAmount == 0)
            {
                aRestaurantOrder.CashAmount = aRestaurantOrder.TotalCost;
                aRestaurantOrder.PaymentMethod = "Cash";
            }
            // aRestaurantOrder.


            return aRestaurantOrder;

        }

        private void ClearAllOrderInformation()
        {
            commentTextBox.Text = "Comment";
            customerDetailsLabel.Text = "";
            customerEditButtonNew.Visible = false;
            phoneNumberDeleteButton.Visible = false;
            customerTextBox.Text = "Search Customer";
            customerTextBox.Visible = true;
            customTotalTextBox.Text = "Custom Total";
            aRecipeOptionMdList = new List<RecipeOptionMD>();
            aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
            aRecipePackageMdList = new List<RecipePackageMD>();
            aPackageItemMdList = new List<PackageItem>();
            aRecipeMultipleMdList = new List<RecipeMultipleMD>();
            aMultipleItemMdList = new List<MultipleItemMD>();

            //orderDetailsflowLayoutPanel1.Controls.Clear();
            //customPanel.Location = new Point(customPanel.Location.X, orderDetailsflowLayoutPanel1.Location.Y + orderDetailsflowLayoutPanel1.Size.Height);
            //paymentDetailsPanel.Location = new Point(paymentDetailsPanel.Location.X, customPanel.Location.Y + customPanel.Size.Height);
            aGeneralInformation = new GeneralInformation();
            aGeneralInformation.OrderType = aRestaurantInformation.DefaultOrderType;
            //  LoadOrderDetails(0);
            AddServiceChargeIntoLabel();
        }

        private void ChangeButtonLocation(bool deliveryChargeButtonShow)
        {
            int len = 3;
            if (!deliveryChargeButtonShow)
            {
                deliveryChargeButton.Visible = false;

                paidButton.Location = new Point(54, 2);
                discountButton.Location = new Point(paidButton.Location.X + paidButton.Width + len, 2);
            }
            else
            {
                deliveryChargeButton.Visible = true;
                paidButton.Location = new Point(3, 2);
                discountButton.Location = new Point(paidButton.Location.X + paidButton.Width + len, 2);
                deliveryChargeButton.Location = new Point(discountButton.Location.X + discountButton.Width + len, 2);
            }


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
        private void tableButtonNew_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            try
            {
                if (!OthersMethod.CheckServerConneciton())
                {
                    return;
                }

                AllCardSytemClear();
                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
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
                    ClearAllOrderInformation();

                    //  ClearTableOrderDetails();
                }
                else
                {
                    aGeneralInformation = new GeneralInformation();
                    RestaurantTable aRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableId(TableLoadResponsive.TableId);
                    LoadTableInformation(aRestaurantTable);
                    LoadAllSaveOrder(aGeneralInformation.TableId, "order");

                }
                AddServiceChargeIntoLabel();

                LoadMenuType();
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());

                
            }

            

        }

        public void LoadAllSaveOrder(int tableId, string type)
        {
            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            }
            try
            {

                OthersMethod aOthersMethod = new OthersMethod();
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
                //orderDetailsflowLayoutPanel1.Controls.Clear();
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
                            MessageBox.Show("Selected table is now busy", "Table not available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        LoadTableInformation(aRestaurantTable);
                    }
                    else
                    {
                        tableButton.Text = "TABLE";
                        //  personButtonNew.Text = "P " + aGeneralInformation.Person;
                        personButtonNew.Visible = false;
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

                        totalAmountLabel.Text = aRestaurantOrder.TotalCost.ToString();

                    }

                    LoadMenuType();

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
                    // aGeneralInformation.OrderDiscount = aRestaurantOrder.Discount;
                    aGeneralInformation.DeliveryCharge = aRestaurantOrder.DeliveryCost;
                    aGeneralInformation.CardFee = aRestaurantOrder.CardFee;
                    aGeneralInformation.DeliveryTime = aRestaurantOrder.DeliveryTime.ToString();
                    aGeneralInformation.Person = aRestaurantOrder.Person;
                    aGeneralInformation.TableId = aRestaurantOrder.OrderTable;
                    aGeneralInformation.OrderType = aRestaurantOrder.OrderType;
                    aGeneralInformation.ServiceCharge = aRestaurantOrder.ServiceCharge;
                    aGeneralInformation.OrderNo = aRestaurantOrder.OrderNo;
                }


                if (aRestaurantOrder != null && aRestaurantOrder.Id > 0)
                {
                    List<OrderItem> aOrderItems = aRestaurantOrderBLL.GetRestaurantOrderRecipeItems(aRestaurantOrder.Id);
                    List<OrderPackage> aPackageItems = aRestaurantOrderBLL.GetRestaurantOrderPackage(aRestaurantOrder.Id);
                    aOrderItemDetailsMDList.Clear();
                    int optionIndex = 0;

                    foreach (OrderItem item in aOrderItems)
                    {
                        try
                        {
                            if (item.PackageId <= 0 && String.IsNullOrEmpty(item.MultipleMenu))
                            {
                                optionIndex++;
                                ReceipeMenuItemButton aReceipeMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);
                                ReceipeCategoryButton aReceipeCategoryButton = aRestaurantMenuBll.GetCategoryByCategoryId(aReceipeMenuItemButton.CategoryId);
                                OrderItemDetailsMD aOrderItemDetails = new OrderItemDetailsMD();
                                aOrderItemDetails.CategoryId = aReceipeMenuItemButton.CategoryId;
                                aOrderItemDetails.ItemId = aReceipeMenuItemButton.RecipeMenuItemId;
                                aOrderItemDetails.ItemName = item.Name;
                                aOrderItemDetails.ItemFullName = aReceipeMenuItemButton.ShortDescrip;
                                aOrderItemDetails.OptionsIndex = optionIndex;
                                aOrderItemDetails.KitchenSection = aReceipeMenuItemButton.KitchenSection;
                                aOrderItemDetails.Price = item.Price / item.Quantity;
                                aOrderItemDetails.KitchenDone = item.SentToKitchen;
                                aOrderItemDetails.Qty = item.Quantity;
                                aOrderItemDetails.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                                aOrderItemDetails.SortOrder = aReceipeMenuItemButton.SortOrder;
                                aOrderItemDetails.CatSortOrder = aReceipeCategoryButton.SortOrder;
                                aOrderItemDetails.TableNumber = aGeneralInformation.TableId;
                                aOrderItemDetails.OptionName = item.Options;
                                aOrderItemDetails.OptionNoOption = item.MinusOptions;
                                aOrderItemDetails.Option_ids = "";

                                //LoadSaveOption(item, aOrderItemDetails);

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
                                    List<MultipleMenuDetailsJsonResponsivePage> aMultipleMenuDetailsJsons = new List<MultipleMenuDetailsJsonResponsivePage>();
                                    try
                                    {
                                        aMultipleMenuDetailsJsons = JsonConvert.DeserializeObject<List<MultipleMenuDetailsJsonResponsivePage>>(item.MultipleMenu);
                                    }
                                    catch (Exception exception)
                                    {
                                        ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                        aErrorReportBll.SendErrorReport(exception.ToString());
                                    }
                                    aMultipleItemMdList = new MultipleItem().LoadMultilpleItemsList(aOrderItemDetails, aMultipleMenuDetailsJsons);
                                    aOrderItemDetails.MultipleItem = aMultipleItemMdList;
                                    aOrderItemDetails.ItemLimit = aMultipleMenuDetailsJsons.Count;
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
                    List<PackageItem> packageList = null;
                    foreach (OrderPackage package in aPackageItems)
                    {

                        RecipePackageButton tempRecipePackageButton =
                            aRestaurantMenuBll.GetPackageByPackageId(package.PackageId);
                        RecipePackageMD aRecipePackage = new RecipePackageMD();
                        aRecipePackage.id = package.Id;
                        aRecipePackage.Description = tempRecipePackageButton.Description;
                        aRecipePackage.OptionsIndex = optionIndex;
                        aRecipePackage.PackageId = tempRecipePackageButton.PackageId;
                        aRecipePackage.PackageName = package.Name;
                        aRecipePackage.Qty = package.Quantity;
                        aRecipePackage.RecipeTypeId = tempRecipePackageButton.RecipeTypeId;
                        aRecipePackage.RestaurantId = tempRecipePackageButton.RestaurantId;
                        aRecipePackage.RecipePackageButton = tempRecipePackageButton;
                        aRecipePackage.RecipePackageButton.PackageName = package.Name;
                        aRecipePackage.RecipePackageButton.ItemQty = package.Quantity;

                        packageList = LoadPackageItem(aOrderItems, aRecipePackage, package.Id, aRestaurantOrder);


                        //  aRecipePackage.UnitPrice = package.Price;
                        if (package.Extra_price > 0){aRecipePackage.UnitPrice = package.Price + package.Extra_price;
                        }
                        else
                        {


                            aRecipePackage.UnitPrice =
                                (package.Price - GetPackageItemPriceForFetech(package.PackageId, optionIndex)) /
                                package.Quantity;
                            aRecipePackage.Extraprice = package.Extra_price;
                            if (aRestaurantOrder.OnlineOrderId > 0)
                            {
                                aRecipePackage.Extraprice =
                                    tempRecipePackageButton.InPrice + packageList.Sum(a => a.ExtraPrice);
                            }
                        }
                        //aRecipePackage.RecipePackageButton.InPrice = (package.Price - GetPackageItemPrice(package.PackageId, optionIndex)) / package.Quantity;
                        //aRecipePackage.RecipePackageButton.OutPrice = (package.Price - GetPackageItemPrice(package.PackageId, optionIndex)) / package.Quantity;


                        aRecipePackage.packageItemList = packageList;
                        aRecipePackageMdList.Add(aRecipePackage);
                        optionIndex++;
                    }

                    AddToCardLoadOrder(aOrderItemDetailsMDList, aRecipePackageMdList, packageList, aRecipeMultipleMdList);

                }


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
                }

                else
                {
                    customerDetailsLabel.Text = "";
                    customerEditButtonNew.Visible = false;
                    phoneNumberDeleteButton.Visible = false;
                    customerTextBox.Text = "Search Customer";
                }

                LoadGeneralInformation();
                if (aRestaurantOrder.Coupon != null)
                {

                    OrderDiscount aOrderDiscount = new OrderDiscount();
                    aOrderDiscount.Amount = aRestaurantOrder.Discount;
                    aOrderDiscount.DiscountArea = "Order";
                    aOrderDiscount.DiscountType = aRestaurantOrder.Coupon;
                    CalculateDiscount(aOrderDiscount);
                }
                //   double orderTotal = GetTotalAmountDetails();


                ShopingCard();
                //if ((orderPrice - aRestaurantOrder.TotalCost) < Math.Abs(0.2))
                //{//    customTotalTextBox.Text = "Custom Total";
                //    totalAmountLabel.Text = "£" + orderPrice.ToString("F02");
                //    customTotalTextBox.ForeColor = Color.Black;
                //}
                //else
                //{
                //    customTotalTextBox.Text = aRestaurantOrder.TotalCost.ToString("F02");
                //    totalAmountLabel.Text = "£" + aRestaurantOrder.TotalCost.ToString("F02");
                //    customTotalTextBox.ForeColor = Color.Red;

                //}
                // AddServiceChargeIntoLabel(); 

                //  ShowDiscount();
            }catch (Exception ex)
            {

                MessageBox.Show(ex.GetBaseException().ToString());
            }

        }

        public void LoadAllLocalOrder(RestaurantOrder restaurantOrder)
        {
            string type = "reorder";
            if (restaurantOrder.OrderNo > 0)
            {
                restaurantOrder.Id = restaurantOrder.OrderNo;
            }
            int tableId = restaurantOrder.Id;

            OthersMethod aOthersMethod = new OthersMethod();
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            //orderDetailsflowLayoutPanel1.Controls.Clear();
            RestaurantOrder aRestaurantOrder = new RestaurantOrder();
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();

            if (!orderLoadStatus) return;
            if (type == "reorder")
            {
                aRestaurantOrder = restaurantOrder;
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
                    //  personButtonNew.Text = "P " + aGeneralInformation.Person;
                    personButtonNew.Visible = false;
                    deliveryChargeButton.Text = "Delivery Charge\r\n" + aRestaurantOrder.DeliveryCost.ToString("F02");
                    discountButton.Text = "Disc\r\n" + aRestaurantOrder.Discount.ToString("F02");
                    finalizeButton.Visible = false;
                    deliveryButton.Text = "DEL";
                    deliveryButton.Visible = true;
                    collectionButton.Visible = true;
                    timeButton.Visible = true;
                    if (aOthersMethod.IsTimeFormatValid(aRestaurantOrder.DeliveryTime.ToString()) != "00:00")
                    {
                        timeButton.Text = aOthersMethod.IsTimeFormatValid(aRestaurantOrder.DeliveryTime.ToString());

                    }

                    aGeneralInformation = new GeneralInformation();
                    aGeneralInformation.OrderType = aRestaurantOrder.OrderType;

                    totalAmountLabel.Text = aRestaurantOrder.TotalCost.ToString();

                }

                LoadMenuType();
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
                aGeneralInformation.OrderDiscount = aRestaurantOrder.Discount;
                aGeneralInformation.DeliveryCharge = aRestaurantOrder.DeliveryCost;
                aGeneralInformation.CardFee = aRestaurantOrder.CardFee;
                aGeneralInformation.DeliveryTime = aRestaurantOrder.DeliveryTime.ToString();
                aGeneralInformation.Person = aRestaurantOrder.Person;
                aGeneralInformation.TableId = aRestaurantOrder.OrderTable;
                aGeneralInformation.OrderType = aRestaurantOrder.OrderType;
                aGeneralInformation.ServiceCharge = aRestaurantOrder.ServiceCharge;
            }


            if (aRestaurantOrder != null && aRestaurantOrder.Id > 0)
            {
                List<OrderItem> aOrderItems = restaurantOrder.OrderItem.Where(a => a.Group != "Package").ToList();

                List<OrderPackage> aPackageItems = new LocalDataSet().GetPackage(restaurantOrder.OrderItem);
                //.GetRestaurantOrderPackage(aRestaurantOrder.Id);
                aOrderItemDetailsMDList.Clear();
                int optionIndex = 0;
                foreach (OrderItem item in aOrderItems)
                {
                    try
                    {
                        if (item.PackageId <= 0 && String.IsNullOrEmpty(item.MultipleMenu))
                        {
                            optionIndex++;
                            ReceipeMenuItemButton aReceipeMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);
                            ReceipeCategoryButton aReceipeCategoryButton = aRestaurantMenuBll.GetCategoryByCategoryId(aReceipeMenuItemButton.CategoryId);
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
                            aOrderItemDetails.OptionName = item.Options ?? "";
                            aOrderItemDetails.OptionNoOption = item.MinusOptions ?? "";
                            aOrderItemDetails.Option_ids = "";

                            //LoadSaveOption(item, aOrderItemDetails);
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
                                List<MultipleMenuDetailsJsonResponsivePage> aMultipleMenuDetailsJsons = new List<MultipleMenuDetailsJsonResponsivePage>();
                                try
                                {
                                    aMultipleMenuDetailsJsons = JsonConvert.DeserializeObject<List<MultipleMenuDetailsJsonResponsivePage>>(item.MultipleMenu);
                                }
                                catch (Exception exception)
                                {
                                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                                    aErrorReportBll.SendErrorReport(exception.ToString());
                                }
                                aMultipleItemMdList = new MultipleItem().LoadMultilpleItemsList(aOrderItemDetails, aMultipleMenuDetailsJsons);
                                aOrderItemDetails.MultipleItem = aMultipleItemMdList;
                                aOrderItemDetails.ItemLimit = aMultipleMenuDetailsJsons.Count;
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
                List<PackageItem> packageList = new List<PackageItem>();
                int i = 0;
                foreach (OrderPackage package in aPackageItems)
                {
                    optionIndex++;
                    i++;
                    RecipePackageButton tempRecipePackageButton = aRestaurantMenuBll.GetPackageByPackageId(package.PackageId);
                    RecipePackageMD aRecipePackage = new RecipePackageMD();
                    aRecipePackage.id = i;
                    aRecipePackage.Description = tempRecipePackageButton.Description;
                    aRecipePackage.OptionsIndex = optionIndex;
                    aRecipePackage.PackageId = tempRecipePackageButton.PackageId;
                    aRecipePackage.PackageName = package.Name;
                    aRecipePackage.Qty = package.Quantity;
                    aRecipePackage.RecipeTypeId = tempRecipePackageButton.RecipeTypeId;
                    aRecipePackage.RestaurantId = tempRecipePackageButton.RestaurantId;
                    aRecipePackage.RecipePackageButton = tempRecipePackageButton;
                    aRecipePackage.RecipePackageButton.PackageName = package.Name;
                    aRecipePackage.RecipePackageButton.ItemQty = package.Quantity;
                    package.PackageItem.ForEach(item1 => item1.Id = i);//packageList = LoadPackageItem(aOrderItems, aRecipePackage, package.Id, aRestaurantOrder);
                    packageList = packageList.Concat(package.PackageItem).ToList();

                    //  aRecipePackage.UnitPrice = package.Price;

                    aRecipePackage.UnitPrice = (package.Price - GetPackageItemPriceForFetech(package.PackageId, optionIndex)) / package.Quantity;
                    aRecipePackage.Extraprice = package.Price;
                    if (aRestaurantOrder.OnlineOrderId > 0)
                    {
                        aRecipePackage.Extraprice = tempRecipePackageButton.InPrice + packageList.Sum(a => a.ExtraPrice);
                    }

                    aRecipePackage.packageItemList = packageList;
                    aRecipePackageMdList.Add(aRecipePackage);
                }

                AddToCardLoadOrder(aOrderItemDetailsMDList, aRecipePackageMdList, packageList, aRecipeMultipleMdList);

            }


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
            }

            else
            {
                customerDetailsLabel.Text = "";
                customerEditButtonNew.Visible = false;
                phoneNumberDeleteButton.Visible = false;
                customerTextBox.Text = "Search Customer";
            }

            LoadGeneralInformation();


            //   double orderTotal = GetTotalAmountDetails();
            double orderPrice = GetTotalAmountDetails();
            OrderDiscount aOrderDiscount = new OrderDiscount();
            aOrderDiscount.Amount = aRestaurantOrder.Discount;
            aOrderDiscount.DiscountArea = "Order";
            aOrderDiscount.DiscountType = aRestaurantOrder.Coupon;
            CalculateDiscount(aOrderDiscount); 
            if ((orderPrice - aRestaurantOrder.TotalCost) < Math.Abs(0.2))
            {

                totalAmountLabel.Text = "£" + orderPrice.ToString("F02");
                customTotalTextBox.ForeColor = Color.Black;
            }
            else
            {
                customTotalTextBox.Text = aRestaurantOrder.TotalCost.ToString("F02");
                totalAmountLabel.Text = "£" + aRestaurantOrder.TotalCost.ToString("F02");
                customTotalTextBox.ForeColor = Color.SlateGray;

            }

            AddServiceChargeIntoLabel();

          //  ShopingCard();
           // ShowDiscount();
        }
        private void ShowDiscount()
        {
            double discountAmount = 0;
            double discountPercent = 0;

            double amount1 = 0; double amount2 = 0;
            double amount3 = 0;

            amount1 = aOrderItemDetailsMDList.Sum(a => a.Qty * a.Price);

            if (!aRestaurantInformation.ExcludeDiscount.Contains("Package"))
            {
                amount2 = aRecipePackageMdList.Sum(a => a.Qty * a.UnitPrice);
                amount3 = aPackageItemMdList.Sum(a => a.Qty * a.Price);
            }
            double totalAmount = amount1 + amount2 + amount3;

            if (totalAmount > 0)
            {
                aGeneralInformation.DiscountPercent = (aGeneralInformation.OrderDiscount * 100) / totalAmount;
            }

            if (aGeneralInformation.DiscountPercent > 0)
            {
                discountButton.Text = "Disc " + aGeneralInformation.DiscountPercent.ToString("F02") + "%\r\n" + aGeneralInformation.OrderDiscount.ToString("F02");
            }
            else
            {

                discountButton.Text = "Disc\r\n" + discountAmount.ToString("F02");
            }

        }
        private List<PackageItem> LoadPackageItem(List<OrderItem> aOrderItems, RecipePackageMD aRecipePackage, int packageId, RestaurantOrder aRestaurantOrder)
        {

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            foreach (OrderItem item in aOrderItems)
            {

                if (item.PackageId > 0 && item.PackageId == aRecipePackage.PackageId && item.orderPackageId == packageId && item.orderPackageId > 0) //item.RecipeId==aRecipePackage.OptionsIndex
                {

                    PackageItemButton itemButton = aRestaurantMenuBll.GetRecipeByItemIdForPackage(item.RecipeId);
                    ReceipeMenuItemButton menuItem = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);

                    PackageItem aItem = new PackageItem();
                    aItem.Id = packageId;
                    aItem.ItemId = item.RecipeId;
                    aItem.ItemName = item.Name;
                    aItem.Price = item.Price;
                    aItem.ExtraPrice = item.ExtraPrice;
                    aItem.Qty = item.Quantity;
                    aItem.OptionName = item.Options ?? "";
                    aItem.MinusOption = item.MinusOptions ?? "";

                    aItem.PackageId = item.PackageId;
                    aItem.CategoryId = itemButton.CategoryId;
                    aItem.SubcategoryId = itemButton.SubCategoryId;
                    aItem.OptionsIndex = aRecipePackage.OptionsIndex;

                    aItem.PackageItemOptionList = LoadSaveOptionForPackage(item, aItem, aRestaurantOrder);


                    aPackageItemMdList.Add(aItem);
                }
                else if (item.PackageId > 0 && item.PackageId == aRecipePackage.PackageId && item.orderPackageId == 0) //item.RecipeId==aRecipePackage.OptionsIndex
                {

                    PackageItemButton itemButton = aRestaurantMenuBll.GetRecipeByItemIdForPackage(item.RecipeId);
                    ReceipeMenuItemButton menuItem = aRestaurantMenuBll.GetRecipeByItemId(item.RecipeId);

                    PackageItem aItem = new PackageItem();
                    aItem.Id = packageId;
                    aItem.ItemId = item.RecipeId;
                    aItem.ItemName = item.Name;
                    aItem.ExtraPrice = item.ExtraPrice;
                    aItem.Price = item.Price * item.Quantity;
                    aItem.Qty = item.Quantity;
                    aItem.OptionName = item.Options ?? "";
                    aItem.MinusOption = item.MinusOptions ?? "";

                    aItem.PackageId = item.PackageId;
                    aItem.CategoryId = itemButton.CategoryId;
                    aItem.SubcategoryId = itemButton.SubCategoryId;
                    aItem.OptionsIndex = aRecipePackage.OptionsIndex;
                    aItem.PackageItemOptionList = LoadSaveOptionForPackage(item, aItem, aRestaurantOrder);



                    aPackageItemMdList.Add(aItem);
                }

                aRecipePackage.OptionsIndex++;

            }
            return aPackageItemMdList;
        }

        private List<RecipeOptionMD> LoadSaveOptionForPackageForOnline(OrderItem item, PackageItem aOrderItemDetails, RestaurantOrder aRestaurantOrder)
        {
            //List<OptionJson> optionList = new OptionJsonConverter().DeSerialize(item.Options);

            string[] optionList = item.Options.Split(',');

            aRecipeOptionMdList = new List<RecipeOptionMD>();
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            if (optionList != null)
            {
                for (int i = 0; i < optionList.Length; i++)
                {
                    RecipeOptionItemButton recipe = aRestaurantMenuBll.GetRecipeOptionByOptionName(Convert.ToString(optionList[i].Trim()));

                    if (recipe.RecipeOptionId > 0)
                    {
                        var OptionTable_ForPrice = new PackageBLL().GetOptionAll(recipe.RecipeOptionId);
                        if (recipe.InPrice <= 0)
                        {
                            recipe.InPrice = (double)OptionTable_ForPrice.Rows[0]["in_price"];
                        }
                        if (recipe.Price <= 0)
                        {

                            recipe.Price = (double)OptionTable_ForPrice.Rows[0]["price"];
                        }

                    }

                    // RecipeOptionItemButton recipe = aRestaurantMenuBll.GetRecipeOptionByOptionName(optionList[i].Trim());
                    recipe.RecipeOptionButton = new RecipeOptionButton();
                    recipe.RecipeOptionButton.RecipeOptionId = recipe.RecipeOptionId;

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
                            aOptionMD.Price = 0.0;
                        }
                        else
                        {
                            aOptionMD.Price = 0.0;
                        }

                        aOptionMD.InPrice = 0.0;
                        aOptionMD.Qty = 1;
                        aOptionMD.OptionsIndex = aOrderItemDetails.OptionsIndex;
                        aOptionMD.RecipeOPtionItemId = recipe.RecipeOptionItemId;
                        aRecipeOptionMdList.Add(aOptionMD);
                    }
                }
            }

            string[] optionList1 = item.MinusOptions.Split(',');

            if (optionList1.Any())
            {
                for (int i = 0; i < optionList1.Count(); i++)
                {
                    if (optionList1[i] != null && optionList1[i].Length > 0)
                    {
                        string op = optionList1[i];
                        RecipeOptionMD tempOptionMd = aRecipeOptionMdList.FirstOrDefault(a => a.Title == optionList1[i] && a.OptionsIndex == aOrderItemDetails.OptionsIndex);
                        if (tempOptionMd != null && tempOptionMd.RecipeId > 0)
                        {
                            tempOptionMd.MinusOption = optionList1[i];
                        }
                        else
                        {
                            RecipeOptionItemButton recipe = aRestaurantMenuBll.GetRecipeOptionByOptionId(Convert.ToInt32(optionList1[i].Trim()));
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
            return aRecipeOptionMdList;
        }

        public List<OptionJson> AddTotalListForOptionItemPackage(string Option, string NoOpiton)
        {
            List<OptionJson> TotalList = new List<OptionJson>();
            try
            {

                if (Option != String.Empty)
                {
                    List<OptionJson> optionList = new OptionJsonConverter().DeSerialize(Option);
                    foreach (OptionJson optionJson in optionList)
                    {
                        TotalList.Add(optionJson);
                    }

                }
                if (NoOpiton != String.Empty)
                {
                    List<OptionJson> optionListMinus = new OptionJsonConverter().DeSerialize(NoOpiton);

                    foreach (OptionJson optionJson in optionListMinus)
                    {
                        TotalList.Add(optionJson);
                    }
                }

                return TotalList;
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.Message);

            }
            return TotalList;
        }

        private List<RecipeOptionMD> LoadSaveOptionForPackage(OrderItem item, PackageItem aOrderItemDetails, RestaurantOrder aRestaurantOrder)
        {



            List<OptionJson> optionList = AddTotalListForOptionItemPackage(item.Options, item.MinusOptions);


            aRecipeOptionMdList = new List<RecipeOptionMD>();
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            if (optionList != null)
            {
                for (int i = 0; i < optionList.Count; i++)
                {
                    RecipeOptionItemButton recipe = aRestaurantMenuBll.GetRecipeOptionByOptionId(Convert.ToInt32(optionList[i].optionId));

                    //var OptionTable_ForPrice = new PackageBLL().GetOptionAll(recipe.RecipeOptionId);
                    //if (recipe.InPrice <= 0)
                    //{
                    //    recipe.InPrice = (double)OptionTable_ForPrice.Rows[0]["in_price"];
                    //}
                    //if (recipe.Price <= 0)
                    //{

                    //    recipe.Price = (double)OptionTable_ForPrice.Rows[0]["price"];
                    //}
                    
                    // RecipeOptionItemButton recipe = aRestaurantMenuBll.GetRecipeOptionByOptionName(optionList[i].Trim());
                    recipe.RecipeOptionButton = new RecipeOptionButton();
                    recipe.RecipeOptionButton.RecipeOptionId = recipe.RecipeOptionId;

                    if (recipe.RecipeOptionItemId > 0)
                    {
                        RecipeOptionMD aOptionMD = new RecipeOptionMD();
                        aOptionMD.RecipeId = aOrderItemDetails.ItemId;
                        aOptionMD.TableNumber = 1;
                        aOptionMD.RecipeOptionId = recipe.RecipeOptionId;
                        aOptionMD.Title = optionList[i].optionName;
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
                        aOptionMD.Isoption = recipe.IsNooption;

                        aRecipeOptionMdList.Add(aOptionMD);
                    }
                }
            }




            return aRecipeOptionMdList;
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
                address += "," + cell;
                bool flag = false;
                if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                {
                    address += Environment.NewLine;
                    RestaurantOrder aORder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                    if (!string.IsNullOrEmpty(aORder.DeliveryAddress))
                    {
                        string[] ss = aORder.DeliveryAddress.Split(',');
                        flag = true;
                        // address +="\r\n"+ aORder.DeliveryAddress.Replace(",",",\r\n");

                        if (ss.Count() > 0)
                        {
                            address += "," + ss[0];
                        }

                        if (ss.Count() > 1)
                        {
                            address += "," + ss[1];
                        }
                        if (ss.Count() > 2)
                        {
                            address += "," + ss[2];
                        }
                        if (ss.Count() > 3)
                        {
                            address += ", " + ss[3];
                        }
                    }

                }

                if ((deliveryButton.BackColor == Color.Black || aGeneralInformation.OrderType == "DEL") && !flag)
                {

                    address = GetCustomerDetails(aRestaurantUser);
                }



                customerDetailsLabelNew.Text = address;
                customerDetailsLabelNew.Visible = true;

                customerEditButtonNew.Visible = true;
                phoneNumberDeleteButton.Visible = true;
                customerTextBox.Text = cell;
                customerTextBox.Visible = false;
                customerTextBox.SelectionStart = customerTextBox.Text.Length;
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

        private List<OrderItemDetailsMD> tempDetailsMdsList;
        private void AddToCardLoadOrder(List<OrderItemDetailsMD> aOrderItemDetailsMdList, List<RecipePackageMD> recipePackageMdList, List<PackageItem> packageItems, List<RecipeMultipleMD> aRecipeMultipleMdList)
        {
            dataTable.Rows.Clear();
            tempDetailsMdsList = new List<OrderItemDetailsMD>();
            try
            {
                if (aOrderItemDetailsMdList.Count > 0)
                {
                    OnlyOrderItemLoad(aOrderItemDetailsMdList);
                    tempDetailsMdsList = aOrderItemDetailsMdList;

                }
                if (aMultipleItemMdList.Count > 0)
                {

                    DataTable mainCart = new MultipleItem().OnlyMultipleOrder(aRecipeMultipleMdList, dataTable, GetAllCatergory);


                }
                OnlyOrderPackageLoad(recipePackageMdList, packageItems);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.GetBaseException().ToString());

            }



        }

        private void OnlyOrderItemLoad(List<OrderItemDetailsMD> aOrderItemDetailsMdList)
        {

            foreach (OrderItemDetailsMD itemDetailsMd in aOrderItemDetailsMdList)
            {
                dr = dataTable.NewRow();
                dr[0] = dataTable.Rows.Count + 1; 
                dr[1] = itemDetailsMd.CategoryId;
                dr[2] = itemDetailsMd.Qty;

                List<OptionJson> jsonOption = new List<OptionJson>();
                if (itemDetailsMd.OptionName != string.Empty)
                {

                    if (itemDetailsMd.OptionNoOption != string.Empty)
                    {var NojsonOption = new OptionJsonConverter().DeSerialize(itemDetailsMd.OptionNoOption);

                        jsonOption =
                            new OptionJsonConverter().DeSerialize(itemDetailsMd.OptionName)
                                .Concat(NojsonOption)
                                .ToList();
                    }
                    else
                    {

                        jsonOption = new OptionJsonConverter().DeSerialize(itemDetailsMd.OptionName).ToList();
                    }
                    string htmlText = "<h4 style='font-size:12px;margin:0px; text-align:left'>" + itemDetailsMd.ItemName + "</h4>";
                    string Option = itemDetailsMd.OptionName;
                    // List<string> optionName = new List<string>();
                    string removeString = Regex.Replace(Option, @"<[^>]+>|", "");

                    if (removeString.Contains(',') || itemDetailsMd.Option_ids != string.Empty || jsonOption.Count > 0)
                    {
                        htmlText += "<table style='width:100%;'>";

                        for (int i = 0; i < jsonOption.Count; i++)
                        {


                            if (jsonOption[i].optionPrice > 0)
                            {
                                htmlText += "<tr>" + "<td>" + "&rarr;" + jsonOption[i].optionQty + "</td>" +
                                            "<td  style='text-align:left'>" + jsonOption[i].optionName + "</td>" +
                                            " <td style='text-align:right'>£ " + jsonOption[i].optionPrice + "</td>" + "</tr>";


                            }
                            else
                            {
                                htmlText += "<tr>" + "<td>" + "&rarr;" + jsonOption[i].optionQty + "</td>" + "<td  style='text-align:left'>" + jsonOption[i].optionName + "</td>" + " <td style='text-align:right'></td>" + "</tr>";

                            }


                            itemDetailsMd.Option_ids += jsonOption[i].optionId + ",";
                            // htmlText += "&rarr;" + result[i] + "</br>";
                        }


                        htmlText += "</table>";
                        dr[3] = htmlText;
                    }
                    else
                    {
                        //htmlText += "&rarr;" + removeString + "</br>";
                        dr[3] = htmlText;
                    }


                }
                else
                {
                    dr[3] = itemDetailsMd.ItemName;//  dr["OptionId"] = itemDetailsMd.OptionList;

                }
                dr["OptionId"] = jsonOption;
                dr[4] = itemDetailsMd.Price;
                dr[5] = Convert.ToInt16(dr[2]) * Convert.ToDecimal(dr[4]);
                dr[6] = Convert.ToUInt64(itemDetailsMd.ItemId);
                dr[7] = Convert.ToUInt64(itemDetailsMd.ItemId);
                dr[8] = itemDetailsMd.ItemName;

                var GroupName = GetAllCatergory.FirstOrDefault(a => a.CategoryId == itemDetailsMd.CategoryId);
                dr["GroupName"] = GroupName.Text;
                dr["SortOrder"] = GroupName.SortOrder;
                dr["KitichineDone"] = itemDetailsMd.Qty;
                dataTable.Rows.Add(dr);


                dr = dataTable.NewRow();
            }
        }

        private void OnlyOrderPackageLoad(List<RecipePackageMD> recipePackageMdList, List<PackageItem> items)
        {


            foreach (RecipePackageMD recipePackageMd in recipePackageMdList)
            {
                var PackageWiseItems = items.Where(a => a.PackageId == recipePackageMd.PackageId && a.Id == recipePackageMd.id).ToList();
                //var PackageWiseItems = recipePackageMd.packageItemList;
                recipePackageMd.RecipePackageButton.InPrice = recipePackageMd.UnitPrice;
                recipePackageMd.RecipePackageButton.OutPrice = recipePackageMd.UnitPrice;
                recipePackageMd.RecipePackageButton.ItemQty = (int)recipePackageMd.Qty;
                recipePackageMd.RecipePackageButton.ExtraItemPrice = recipePackageMd.UnitPrice;

                AddToCardFixedItemForRetrive(PackageWiseItems, recipePackageMd.RecipePackageButton, null, "");

            }

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
                    tableButtonNew.Text = "T " + aGeneralInformation.TableNumber;
                    personButtonNew.Text = "P " + aGeneralInformation.Person;
                    personButtonNew.Visible = true;
                    deliveryChargeButton.Text = "SAVE";
                    finalizeButton.Visible = true;
                    billButton.Visible = true;
                    deliveryButton.Text = "RES";
                    deliveryButton.Visible = false;
                    collectionButton.Visible = false;
                    timeButton.Visible = false;
                    aGeneralInformation.OrderType = "IN";

                    ChangeButtonLocation(true);
                }
                else
                {
                    tableButton.Text = "TABLE";
                    //  personButtonNew.Text = "P " + aGeneralInformation.Person;
                    personButtonNew.Visible = false;
                    deliveryChargeButton.Text = "D/Ch0";
                    finalizeButton.Visible = false;
                    deliveryButton.Text = "DEL";
                    deliveryButton.Visible = true;
                    collectionButton.Visible = true;
                    timeButton.Visible = true;
                    aGeneralInformation.OrderType = "Collect";billButton.Visible = false;
                    collectionButton.BackColor = Color.Black;
                    deliveryButton.BackColor = Color.WhiteSmoke;
                    collectionButton.ForeColor = Color.WhiteSmoke;
                    deliveryButton.ForeColor = Color.Black;
                    ChangeButtonLocation(false);
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
            discountButton.Enabled = true;
            deliveryChargeButton.Text = "Delivery Charge\r\n0";
            aGeneralInformation.OrderType = "CLT";
            // LoadAmountDetails();
            CustomerBLL aCustomerBll = new CustomerBLL();
            if (aGeneralInformation.CustomerId > 0)
            {


                RestaurantUsers aRestaurantUser =
                    aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
                string address = GetCustomerDetails(aRestaurantUser);
                customerDetailsLabel.Text = address;
            }
            ShopingCard();
        }

        private string GetCustomerDetails(RestaurantUsers aRestaurantUser)
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

                address += "" + cell;
            }

            if (deliveryButton.BackColor == Color.Black)
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

                    address += "\n" + aRestaurantUser.House + " " + aRestaurantUser.FullAddress;

                }
            }
            return address;
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

        private void discountButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            DiscountForm.OrderDiscount = new OrderDiscount();
            if (Properties.Settings.Default.requirdDiscountPassword)
            {
                RestaurantUsers user = new RestaurantUsers();
                AutorizeForm authorization = new AutorizeForm(user, "discount");
                authorization.ShowDialog();
                if (authorization.user.Autorize)
                {
                    DiscountForm aDiscountForm = new DiscountForm();
                    aDiscountForm.ShowDialog();OrderDiscount aOrderDiscount = DiscountForm.OrderDiscount;
                    if (aOrderDiscount.Status == "cancel")
                    {
                        return;
                    }
                    CalculateDiscount(aOrderDiscount);
                    ShopingCard();
                }
            }
            else
            {
                DiscountForm aDiscountForm = new DiscountForm();
                aDiscountForm.ShowDialog();
                OrderDiscount aOrderDiscount = DiscountForm.OrderDiscount;
                if (aOrderDiscount.Status == "cancel")
                {
                    return;
                }
                CalculateDiscount(aOrderDiscount);
                ShopingCard();
            }
            
        }


        private GeneralInformation CalculateDiscount(OrderDiscount aOrderDiscount)
        {
            aGeneralInformation = new PriceCalculation(null, this).CalculateDiscount(aOrderDiscount, aGeneralInformation);
            return aGeneralInformation;
          
        }
        public void LineDiscount(OrderDiscount aOrderDiscount)
        {
            bool flag = false;
            double discountAmount = 0;
            double discountPercent = 0;

            flag = true;

            if (aOrderDiscount.DiscountType == "Fixed")
            {
                discountAmount = aOrderDiscount.Amount;
                // aOrderItemDetailsMDList.Where(a => a.OptionsIndex == details.OptionIndex).ToList().ForEach(b => b.Price = (b.Price - discountAmount));
                //   aGeneralInformation.ItemDiscount = discountAmount;

                double outPrice = Convert.ToDouble(gridViewAddtocard.GetFocusedRowCellValue("Price")) -
                                  Convert.ToDouble(discountAmount);
                double outTotalPrice = outPrice * Convert.ToDouble(gridViewAddtocard.GetFocusedRowCellValue("QTY"));

                gridViewAddtocard.SetFocusedRowCellValue("Price", outPrice.ToString("F2"));
                gridViewAddtocard.SetFocusedRowCellValue("Total", outTotalPrice.ToString("F2"));


            }
            else if (aOrderDiscount.DiscountType == "Persent")
            {
                discountAmount = (Convert.ToDouble(gridViewAddtocard.GetFocusedRowCellValue("Price")) *
                                  aOrderDiscount.Amount) / 100;
                // aOrderItemDetailsMDList.Where(a => a.OptionsIndex == details.OptionIndex).ToList().ForEach(b => b.Price = (b.Price - discountAmount));
                //  aGeneralInformation.ItemDiscount = discountAmount;

                double outPrice = (Convert.ToDouble(gridViewAddtocard.GetFocusedRowCellValue("Price"))) - discountAmount;
                double outTotalPrice = (outPrice * Convert.ToDouble(gridViewAddtocard.GetFocusedRowCellValue("QTY")));


                gridViewAddtocard.SetFocusedRowCellValue("Price", outPrice);
                gridViewAddtocard.SetFocusedRowCellValue("Total", outTotalPrice);

            }

            if (!flag)
            {

                foreach (PackageDetails cc in orderDetailsflowLayoutPanel1.Controls.OfType<PackageDetails>())
                {
                    if (cc.BackColor == Color.Red)
                    {

                        PackageDetails details = cc;
                        if (aOrderDiscount.DiscountType == "Fixed")
                        {
                            discountAmount = aOrderDiscount.Amount;
                            aRecipePackageMdList.Where(a => a.OptionsIndex == details.OptionIndex)
                                .ToList()
                                .ForEach(b => b.UnitPrice = (b.UnitPrice - discountAmount));
                            //    aGeneralInformation.ItemDiscount = discountAmount;
                            details.priceTextBox.Text =
                                (Convert.ToDouble(details.priceTextBox.Text) - discountAmount).ToString("F02");
                            details.totalPriceLabel.Text =
                                ((Convert.ToDouble(details.priceTextBox.Text)) * Convert.ToDouble(details.qtyTextBox.Text))
                                    .ToString("F02");


                        }
                        else if (aOrderDiscount.DiscountType == "Persent")
                        {
                            discountAmount = (Convert.ToDouble(details.priceTextBox.Text) * aOrderDiscount.Amount) / 100;
                            aRecipePackageMdList.Where(a => a.OptionsIndex == details.OptionIndex)
                                .ToList()
                                .ForEach(b => b.UnitPrice = (b.UnitPrice - discountAmount));
                            //   aGeneralInformation.ItemDiscount = discountAmount;
                            details.priceTextBox.Text =
                                (Convert.ToDouble(details.priceTextBox.Text) - discountAmount).ToString("F02");
                            details.totalPriceLabel.Text =
                                ((Convert.ToDouble(details.priceTextBox.Text)) * Convert.ToDouble(details.qtyTextBox.Text))
                                    .ToString("F02");

                        }

                    }
                }


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

        public void LocalOrderSave(RestaurantOrder order, GeneralInformation generalInformation)
        {

            //.......................................................

            try
            {
                if (LocalDataSet.CheckServer())
                {

                    order.OrderNo = generalInformation.OrderId;

                    DataTable table = gridControlAddTocard.DataSource as DataTable;
                    new LocalDataSet().GetOrder(table, order);


                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            //..............................................................

        }
        private void PaidAmountWithPrint(bool paymentStatus, bool isKitchen, bool isPrint = false, bool isFinalize = false)
        {

            try
            {
                if (!OthersMethod.CheckServerConneciton())
                {
                    return;
                }
                aRecipePackageMdList.Clear();
                PackageSaveTemp();

                if (gridViewAddtocard.RowCount == 0)
                {
                    MessageBox.Show("No items in the order!", "Save Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime stDate = DateTime.Now.Date;
                DateTime endDate = stDate.AddDays(1);
                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
                if (ShopingCard() >= 0)
                {
                    ConfirmOrderForm.PaymentDetails = new PaymentDetails();
                    PaymentDetails aPaymentDetails = ConfirmOrderForm.PaymentDetails;
                    RestaurantOrder aRestaurantOrder = GetRestaurantOrder(aPaymentDetails);
                    RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                    ConfirmOrderForm aConformOrderForm = new ConfirmOrderForm(aRestaurantOrder);
                    aRestaurantOrder.Discount = 0.0;
                    aRestaurantOrder.Coupon = "";
                    aRestaurantOrder.DiscountAmount = aGeneralInformation.DiscountFlat;
                    if (aGeneralInformation.DiscountType != null)
                    {
                        aRestaurantOrder.Coupon = aGeneralInformation.DiscountType;
                        aRestaurantOrder.DiscountAmount = aGeneralInformation.DiscountFlat;

                     if (aGeneralInformation.DiscountType == "percent")
                    {
                            aRestaurantOrder.Discount = aGeneralInformation.DiscountPercent;

                    }
                        else
                        {
                            aRestaurantOrder.Discount = aGeneralInformation.OrderDiscount;

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
                    if (paymentStatus)
                    {
                        aConformOrderForm.ShowDialog();
                    }



                    if ((aPaymentDetails.Status == "Ok" && paymentStatus) || !paymentStatus)
                    {
                        try
                        {
                            int result = 0;
                            aGeneralInformation.OrderType = aGeneralInformation.OrderType == null ||
                                                            aGeneralInformation.OrderType == ""
                                ? aRestaurantInformation.DefaultOrderType
                                : aGeneralInformation.OrderType;
                            //aGeneralInformation.DeliveryTime = aGeneralInformation.DeliveryTime == null ||
                            //                                   aGeneralInformation.DeliveryTime == ""
                            //    ? DateTime.Now.AddMinutes(aRestaurantInformation.DeliveryTime).ToString("HH:mm")
                            //    : aGeneralInformation.DeliveryTime;
                            aGeneralInformation.PaymentMethod = aPaymentDetails.PaymentMethod;

                            if (paymentStatus)
                            {

                                aRestaurantOrder.CardAmount = aPaymentDetails.CardAmount;
                                aRestaurantOrder.CardFee = aPaymentDetails.CardFee;
                                aRestaurantOrder.CashAmount = aPaymentDetails.CashAmount;
                            }
                            // LocalOrderSave(aRestaurantOrder, aGeneralInformation);



                            if (aGeneralInformation.OrderId <= 0)
                            {
                                int orderNo = aRestaurantOrderBLL.GetMaxOrderNumber(stDate, endDate);

                                OrderNo = orderNo;
                                aRestaurantOrder.OrderStatus = "finished";
                                //   if (paymentStatus || aGeneralInformation.OrderType != "IN")
                                if (paymentStatus)
                                {
                                    aRestaurantOrder.Status = "Paid";
                                    if (aRestaurantOrder.OnlineOrder > 0 || aRestaurantOrder.OnlineOrderId > 0)
                                    {
                                        aRestaurantOrder.OnlineOrderStatus = "accepted";
                                    }

                                }
                                else
                                {

                                    aRestaurantOrder.Status = "pending";
                                    if (aRestaurantOrder.OnlineOrder > 0 || aRestaurantOrder.OnlineOrderId > 0)
                                    {
                                        aRestaurantOrder.OnlineOrderStatus = "accepted";
                                    }
                                }



                                aRestaurantOrder.OrderNo = orderNo;
                                if (customerTextBox.Text != "" && customerTextBox.Text != "Search Customer" &&
                                    aRestaurantOrder.CustomerId == 0)
                                {
                                    aRestaurantOrder.CustomerName = customerTextBox.Text;
                                }




                                result = aRestaurantOrderBLL.InsertRestaurantOrder(aRestaurantOrder);


                                List<OrderPackage> aOrderPackage = GetOrderPackage(result);
                                var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrderPackage);

                                List<OrderItem> aOrderItems = GetOrderItem(result, result2);

                                int result1 = aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrderItems);

                                if (aGeneralInformation.CustomerId > 0)
                                {
                                    CustomerRecentItemBLL aCustomerRecentItemBll = new CustomerRecentItemBLL();
                                    List<CustomerRecentItemMD> aCustomerRecentItemMds =
                                        GetCustomerOrderItems(aOrderItems, aOrderPackage);
                                    string res = aCustomerRecentItemBll.InsertCustomerRecentItems(aCustomerRecentItemMds);

                                }

                                if ((ConfirmOrderForm.PaymentDetails.IsPrint && paymentStatus) || !paymentStatus)
                                {
                                    PrintingHTML GeneratePrint = new PrintingHTML();
                                     aRestaurantOrder.isFinalize = isFinalize;
                                    aRestaurantOrder.isKitchenPrint = isKitchen;
                                    if (paymentStatus)
                                    {
                                        //GenerateHtmlPrint(result, true, aPaymentDetails, isKitchen, false, isFinalize);
                                        GeneratePrint.PrintingOrder(aRestaurantOrder, isPrint, true, this,aPaymentDetails);
                                    }
                                    else
                                    {
                                        // GenerateHtmlPrint(result, false, aPaymentDetails, isKitchen, isPrint, isFinalize);

                                        GeneratePrint.PrintingOrder(aRestaurantOrder, isPrint, false, this,aPaymentDetails);

                                    }
                                }

                                //foreach (OrderItem aOrderItem in aOrderItems)
                                //{
                                //}



                            }
                            else
                            {
                                RestaurantOrder order =
                                    aRestaurantOrderBLL.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                                OrderNo = order.OrderNo;
                                result = aGeneralInformation.OrderId;
                                aRestaurantOrder.OnlineOrder = order.OnlineOrder;
                                aRestaurantOrder.OnlineOrderId = order.OnlineOrderId;
                                aRestaurantOrder.OnlineOrderStatus = order.OnlineOrderStatus;


                                if ((ConfirmOrderForm.PaymentDetails.IsPrint && paymentStatus) || !paymentStatus)
                                {
                                    if (paymentStatus)
                                    {
                                        GenerateHtmlPrint(aGeneralInformation.OrderId, true, aPaymentDetails, isKitchen,
                                            false, isFinalize);
                                    }
                                    else
                                    {
                                        GenerateHtmlPrint(aGeneralInformation.OrderId, false, aPaymentDetails, isKitchen,
                                            isPrint, isFinalize);
                                    }
                                }

                                List<OrderPackage> aOrderPackage = GetOrderPackage(result);
                                DeleteNewCancelItem(null, aOrderPackage, result);

                                //var result2 = aRestaurantOrderBLL.UpdateRestaurantOrderPackage(aOrderPackage, result);
                                var result2 = aRestaurantOrderBLL.InsertOrderPackage(aOrderPackage);

                                List<OrderItem> aOrderItems = GetOrderItem(result, result2);

                                int result1 = aRestaurantOrderBLL.InsertRestaurantOrderItem(aOrderItems);
                                // bool result1 = aRestaurantOrderBLL.UpdateRestaurantOrderItem(aOrderItems, result);

                                if (paymentStatus || aGeneralInformation.OrderType != "IN")
                                {
                                    aRestaurantOrder.OrderStatus = "finished";
                                    aRestaurantOrder.Status = "Paid";
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
                                aRestaurantOrder.Id = aGeneralInformation.OrderId;
                                aRestaurantOrder.OrderNo = order.OrderNo;

                                bool res = aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);
                            }

                            if (aGeneralInformation.TableId > 0)
                            {

                                if (paymentStatus || aGeneralInformation.OrderType != "IN")
                                {

                                    RestaurantTable aTable =
                                        aRestaurantTableBll.GetRestaurantTableByTableId(aGeneralInformation.TableId);
                                    aTable.CurrentStatus = "available";
                                    aRestaurantTableBll.UpdateRestaurantTable(aTable);
                                    aRestaurantOrder.OrderStatus = "finished";


                                    if (aTable.MergeStatus > 0)
                                    {
                                        aRestaurantTableBll.ToAvailableMergeTable(aTable);
                                    }

                                }
                                else
                                {
                                    aRestaurantOrder.OrderStatus = "pending";
                                }

                            }

                            try
                            {
                             
                                if (paymentStatus && collectionButton.BackColor == Color.Black && timeButton.Text.ToLower() == "wait" && GlobalSetting.SettingInformation.till == "Enable")
                                {
                                    OpenCashDrawer();
                                }
                            }
                            catch (Exception ex)
                            {
                                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
                                this.Activate();
                                orderAllClearButton.PerformClick();
                            }

                            if (GlobalSetting.RestaurantInformation.IsSyncOrder > 0 &&
                                aRestaurantOrder.OrderStatus == "finished" &&
                                Properties.Settings.Default.deviceType == "SERVER" &&
                                OthersMethod.CheckForInternetConnection())
                            {
                                OrderSyncronize();
                            }
                        }
                        catch (Exception exception)
                        {
                            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                            aErrorReportBll.SendErrorReport(exception.ToString());
                        }
                        orderAllClearButton.PerformClick();
                    }
                  
                }
               
            }catch (Exception)
            {

                MessageBox.Show("Server Not Found");
                this.Activate();

            }

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
            aRestaurantOrder.CardAmount = aPaymentDetails.CardAmount;
            aRestaurantOrder.CardFee = aGeneralInformation.CardFee;
            aRestaurantOrder.CashAmount = aPaymentDetails.CashAmount;
            aRestaurantOrder.Comment = commentTextBox.Text == "Comment" ? "" : commentTextBox.Text;
            aRestaurantOrder.DeliveryCost = aGeneralInformation.DeliveryCharge;

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
            aRestaurantOrder.Discount = aGeneralInformation.ItemDiscount + aGeneralInformation.OrderDiscount;
            aRestaurantOrder.Coupon = aGeneralInformation.DiscountType;
            aRestaurantOrder.OrderTime = DateTime.Now;
            aRestaurantOrder.PaymentMethod = aPaymentDetails.PaymentMethod;
            aRestaurantOrder.ServiceCharge = aGeneralInformation.ServiceCharge;
            if (!double.TryParse(customTotalTextBox.Text, out totalCost))
            {
                //aRestaurantOrder.TotalCost = GetTotalAmountDetails() + aRestaurantOrder.DeliveryCost +
                //                             aRestaurantOrder.ServiceCharge + aRestaurantOrder.CardFee -
                //                             aRestaurantOrder.Discount;      
                aRestaurantOrder.TotalCost = GetTotalAmountDetails();
            }
            else
            {
                aRestaurantOrder.TotalCost = totalCost;
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


        private List<OrderPackage> GetOrderPackage(int orderId)
        {
            List<OrderPackage> aOrderPackages = new List<OrderPackage>();
            int i = 0;
            foreach (RecipePackageMD package in aRecipePackageMdList)
            {


                OrderPackage aPackage = new OrderPackage();
                aPackage.Extra_price = 0;
                aPackage.Name = package.PackageName;
                aPackage.OrderId = orderId;
                aPackage.Id = package.id;
                aPackage.PackageId = package.PackageId;
                aPackage.Price = package.Qty * package.UnitPrice + GetPackageItemPrice(package.PackageId, package.OptionsIndex);
                aPackage.Quantity = (int)package.Qty;
                aPackage.optionIndex = package.OptionsIndex;
                aOrderPackages.Add(aPackage);
                i++;

            }

            return aOrderPackages;
        }

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

        private void GenerateHtmlPrint(int result, bool status, PaymentDetails aPaymentDetails, bool isKitchenPrint, bool isPrint = false, bool isFinalize = false)
        {


        //    PrintDirect print = new PrintDirect();




            string printHeaderStr = "<div style='width:260px; font-family:tahoma, sans-serif'>";


            int papersize = 25;
            string reciept_font = "";

            aGeneralInformation.OrderId = result;
            orderDetailsflowLayoutPanel1 = new CustomFlowLayoutPanel();
            RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
            RestaurantMenuBLL aRestaurantMenuBLL = new RestaurantMenuBLL();
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            UserLoginBLL aCustomerBll = new UserLoginBLL();
            RestaurantInformation aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
            List<ReceipeTypeButton> aReceipeTypeButton = new RestaurantMenuBLL().GetRecipeType().ToList();
            RestaurantOrder aRestaurantOrder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
            int blankLine = 0;
            int starterId = aRestaurantMenuBLL.GetCategoryByName("Starter");

            List<RecipeTypeDetails> aListTypeDetails =
                orderDetailsflowLayoutPanel1.Controls.OfType<RecipeTypeDetails>().ToList();

            List<ReportData> listOfReport = new List<ReportData>();

         
            reciept_font = aRestaurantInformation.RecieptFont;
            reciept_font = (Convert.ToDouble(reciept_font) * 1.5).ToString();
            string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 3).ToString();
            string reciept_font_exlgr = "26";
            string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();
            if (aRestaurantInformation.RecieptOption != "none")
            {
                printHeaderStr += "<div align='center' style='width: 250px; margin-bottom:5px;'><b>" +
                                  aRestaurantInformation.RestaurantName.ToUpper() + "</b><br>" +
                                  aRestaurantInformation.House + ", " + aRestaurantInformation.Address + "<br>" +
                                  aRestaurantInformation.Postcode + "<br>TEL:" + aRestaurantInformation.Phone + "<br>" +
                                  aRestaurantInformation.VatRegNo + "</div>";
            }
            string orderHistory = aVariousMethod.GetOrderHistory(papersize, result, aGeneralInformation, timeButton.Text.ToString());
            printHeaderStr +=
                "<div  style='width: 250px;border-bottom:1px dashed;'><div style='text-align:center;font-size:" +
                reciept_font_lgr + "px'><b>" +
                orderHistory + "</b></div></div>";
            if (isPrint)
            {
                printHeaderStr +=
                    "<div   style='width: 250px;border-bottom:1px dashed;'><div style='text-align:center;'>" +
                    DateTime.Now.ToString("dddd,dd/MM/yyyy") + "</div></div>";
            }



            if (aGeneralInformation.CustomerId > 0)
            {
                printHeaderStr +=
                    "<div style='float:left; max-width:250px;border-bottom:1px dashed; margin-bottom:5px; font-weight:bold; font-size:" +
                    reciept_font + "px'>";
                RestaurantUsers aUser =
                    aRestaurantUserForSearchCustomer.SingleOrDefault(a => a.Id == aGeneralInformation.CustomerId);
                if (aUser == null)
                {
                    aUser = aCustomerBll.GetResturantUserByUserId(aGeneralInformation.CustomerId);
                }
                printHeaderStr += aUser.Firstname + " " + aUser.Lastname + "<br>";


                string cell = aUser.Mobilephone != "" ? aUser.Mobilephone : aUser.Homephone;


                string address = "";
                bool flag = false;
                if (aGeneralInformation != null && aGeneralInformation.OrderId > 0)
                {
                    //RestaurantOrder aORder = aVariousMethod.GetRestaurantOrderByOrderId(aGeneralInformation.OrderId);
                    if (!string.IsNullOrEmpty(aRestaurantOrder.DeliveryAddress))
                    {
                        string[] ss = aRestaurantOrder.DeliveryAddress.Split(',');
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

                if (aGeneralInformation.DeliveryCharge > 0 && deliveryButton.Text == "DEL" &&
                    deliveryButton.BackColor == Color.Black)
                {
                    if (address == "")
                    {
                        if (string.IsNullOrEmpty(aUser.FullAddress))
                        {

                            address += "" + aUser.House + " " + aUser.Address;
                            address += "," + aUser.City + "<br>" + aUser.Postcode;
                        }
                        else
                        {

                            address += "" + aUser.House + "," + aUser.FullAddress + "<br>" + aUser.Postcode;

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
                    "<div  style='width: 250px;border-bottom:1px dashed;margin-bottom:10px; '><div style='text-align:left;font-size:" +
                    reciept_font + "px'><b>" + customerTextBox.Text + "</b></div></div>";
            }


            foreach (PrinterSetup printer in PrinterSetups)
            {
                string printStr = "";
                string printRecipeStr =
                    "<div style='width:250px; font-family:tahoma, sans-serif; font-weight:bold;margin:0;padding:0;'>";
                string printFooterStr =
                    "<div style='width:250px; font-family:tahoma, sans-serif; font-weight:bold;padding:0;margin:0;'>";

                if (printer.PrintStyle != "Receipt") continue;

                List<OrderItemDetailsMD> OrderMdList = new List<OrderItemDetailsMD>();
                List<RecipePackageMD> packageMds = new List<RecipePackageMD>();
                List<PackageItem> mPackageItems = new List<PackageItem>();
                List<RecipeMultipleMD> aOrderItemDetailsMDList = new List<RecipeMultipleMD>();
                List<RecipeOptionMD> recipeOptionMds = new List<RecipeOptionMD>();

              
                List<PrintContent> aPrintContentsHead = new List<PrintContent>();
                List<PrintContent> aPrintContentsMid = new List<PrintContent>();

                PrintContent aPrintContent = new PrintContent();

                PrintFormat aPrintFormat = new PrintFormat(papersize);
                PrintFormat aPrintFormat1 = new PrintFormat(papersize - 10);
                PrintFormat aPrintFormat2 = new PrintFormat(papersize - 2);
                aPrintContent = new PrintContent();
                aPrintContent.StringLine = "\r\n" + aPrintFormat.CreateDashedLine();
                aPrintContentsHead.Add(aPrintContent);


                aPrintContent = new PrintContent();
                aPrintContent.StringLine = "\r\n" + orderHistory;
                aPrintContentsHead.Add(aPrintContent);
                aPrintContent = new PrintContent(); 
                aPrintContent.StringLine = "\r\n" + aPrintFormat.CreateDashedLine();
                aPrintContentsHead.Add(aPrintContent);
                bool startdas = false;
                dataTable = dataTable.AsEnumerable().OrderBy(a => a["SortOrder"]).CopyToDataTable();


                var maxStater = dataTable.AsEnumerable().Count(a => a["Cat"].ToString() == starterId.ToString());
                if (maxStater > 0)
                {
                    dataTable = dataTable.AsEnumerable().OrderByDescending(a => a["SortOrder"]).CopyToDataTable();
                    //   var test = dataTable.AsEnumerable().GroupBy(a => Convert.ToInt32(a["Cat"]) == starterId).ToList();
                }
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Label recipeTypeAmountlabel = new Label();
                    Label receipTypeLabel = new Label();
                    int kitichineDone = Convert.ToInt16(dataTable.Rows[i]["KitichineDone"]);
                    recipeTypeAmountlabel.Text = Convert.ToDouble(dataTable.Rows[i]["Total"]).ToString("N");

                    receipTypeLabel.Text = dataTable.Rows[i]["EditName"].ToString();
                    var CatId = dataTable.Rows[i]["Cat"];
                    var GroupTitle = new RestaurantMenuBLL().GetAllCategory().FirstOrDefault(a => a.CategoryId == Convert.ToInt32(CatId));
                   

                    string IsPackgage = dataTable.Rows[i]["Group"].ToString();
                    if (IsPackgage == "Package")
                    {
                        List<PackageItem> packageItem = (List<PackageItem>)dataTable.Rows[i]["Package"];

                        string packageItemString = "";
                        foreach (PackageItem item in packageItem)
                        {
                            string optionName = "";
                            string kitchineOption = "";

                            if (item.PackageItemOptionList != null)
                            {
                                if (item.PackageItemOptionList.Count > 0)
                                {
                                    for (int j = 0; j < item.PackageItemOptionList.Count; j++)
                                    {
                                        optionName += "→" + "&nbsp;" + item.PackageItemOptionList[j].Title + (item.PackageItemOptionList[j].Price > 0 ? "+" + item.PackageItemOptionList[j].Price.ToString("F02") : "") + "<br/>";
                                        kitchineOption += "→" + "&nbsp;" + item.PackageItemOptionList[j].Title + "<br/>";


                                    }
                                    optionName = optionName.Remove(optionName.Length - 5);
                                    kitchineOption = kitchineOption.Remove(kitchineOption.Length - 5);


                                }

                            }
                            packageItemString += "<h3 style='font-weight:bold;margin: 0px 10px; font-size:" + reciept_font_small +
                                                  "px'><span style='text-align:left;'>" +
                                                  item.Qty +
                                                  "</span><span style='text-align:left;'>&nbsp;&nbsp;" +
                                                 item.ItemName + "</br>" + optionName + "</span></h3>";
                            blankLine++;

                            listOfReport.Add(new ReportData
                            {

                                // ItemName = gridViewAddtocard.GetRowCellValue(i, "Name").ToString(),
                                Name = receipTypeLabel.Text,
                                Index = i,
                                Qty = item.Qty,
                                ItemName = item.ItemName,
                                ItemQty = Convert.ToInt32(dataTable.Rows[i]["QTY"]),
                                Price = Convert.ToDouble(dataTable.Rows[i]["Total"]),
                                OptionName = optionName,
                                GroupTitle = dataTable.Rows[i]["GroupName"].ToString(),
                                Id = i.ToString(),
                                CatId = item.CategoryId,
                                KitchineOption = kitchineOption,
                                SortOrder = item.CategorySortOrder,

                                ReceipeTypeId = 0
                            });
                        }
                        packageMds.Add(new RecipePackageMD()
                        {
                            RecipeTypeId = Convert.ToInt32(dataTable.Rows[i][7]),
                            PackageName = receipTypeLabel.Text,
                            Qty = Convert.ToInt32(dataTable.Rows[i]["QTY"]),
                            PackageId = Convert.ToInt32(dataTable.Rows[i][1]),
                            packageItemList = packageItem,
                            KitichineDone = Convert.ToInt32(dataTable.Rows[i]["KitichineDone"])
                            

                        });
                        printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                             "px'><span style='text-align:left;float:left;width:70%;'>" +
                                            Convert.ToInt32(dataTable.Rows[i]["QTY"]) + "" + receipTypeLabel.Text +
                                             "</span><span style='text-align:right;float:right;width:22%;'>" +
                                             recipeTypeAmountlabel.Text +
                                             "</span></h3>";
                        blankLine++;
                        printRecipeStr += packageItemString;
                    }
                    else
                    {
                        if (IsPackgage == "MultipleItem")
                        {
                            string multipleItem = Convert.ToString(dataTable.Rows[i]["Name"]);
                            receipTypeLabel.Text = multipleItem;
                            receipTypeLabel.Text = Regex.Replace(receipTypeLabel.Text, "<hr/>", "", RegexOptions.Singleline);
                            printRecipeStr += "<p style='font-weight:bold;'>" + "<span style='text-align:right;float:left;'>" +
                                              Convert.ToInt32(dataTable.Rows[i]["QTY"]) + "</span>" + "<span style='text-align:left;margin-left:5px;float:left;width:72%;'>" +
                                              receipTypeLabel.Text + "</span><span style='text-align:right;float:right;width:22%;'>" +
                                              recipeTypeAmountlabel.Text + "</span></p>";

                        }
                        string name = Convert.ToString(dataTable.Rows[i]["Name"]).Replace("&rarr;", ",");

                        string removeString = Regex.Replace(name, @"<[^>]+>|", ""); string optionName = "";
                        string kitchineOptionName = "";
                        var option = dataTable.Rows[i]["OptionId"];
                        List<OptionJson> listOption = new List<OptionJson>();
                        if (option != System.DBNull.Value)
                        {
                            
                            int j = 0; 
                          
                            listOption = (List<OptionJson>)option;
                            foreach (OptionJson optionJson in listOption)
                            {
                                j++;

                                string optionMenu = "&rarr;" + "&nbsp;" + optionJson.optionQty + " " + optionJson.optionName + (optionJson.optionPrice > 0 ? "+" + optionJson.optionPrice.ToString("F02") : "");
                                string optionMenukitchine = "&rarr;" + "&nbsp;" + optionJson.optionQty + " " + optionJson.optionName;

                                if (optionJson.optionPrice > 0)
                                {
                                    optionMenu = "&rarr;" + "&nbsp;" + optionJson.optionQty + " " + optionJson.optionName + (optionJson.optionPrice > 0 ? "+" + optionJson.optionPrice.ToString("F02") : "");
                                }

                                if (listOption.Count == j)
                                {
                                    optionName += optionMenu;
                                    kitchineOptionName += optionMenukitchine;
                                }
                                else
                                {
                                    optionName += optionMenu + "<br/>";

                                    kitchineOptionName += optionMenukitchine + "<br/>";
                                }
                            }

                        }
                        bool recipeTypelabel = false;
                        if (recipeTypelabel)
                        {
                            if (recipeTypelabel == false)
                            {

                                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font_lgr +
                                                  "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                  receipTypeLabel.Text +
                                                  "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                recipeTypeAmountlabel.Text + "</span></h3>";

                            }
                            else
                            {
                                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font_lgr +
                                                  "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                  receipTypeLabel.Text +
                                                  "</span><span style='text-align:right;float:right;width:8%;'></span></h3>";

                            }
                            printRecipeStr +=
                                "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:3px;margin-top:1px;padding:0;width:75%;'>&nbsp;</h3>";
                        } int catId = 0;



                        if (GroupTitle != null && IsPackgage != "MultipleItem")
                        {


                            if (isPrint)
                            {

                                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                  "px'><span style='text-align:left;float:left;width:70%;'>" +
                                                  Convert.ToInt16(dataTable.Rows[i]["QTY"]) + " " + receipTypeLabel.Text +
                                                  "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                  recipeTypeAmountlabel.Text +
                                                  "</span></h3>";
                                blankLine++;
                            }
                            else
                            {
                                printRecipeStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                                  "px'><span style='text-align:left;float:left;width:75%;'>" +
                                                    Convert.ToInt16(dataTable.Rows[i]["QTY"]) + " " + receipTypeLabel.Text +
                                                  "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                 recipeTypeAmountlabel.Text +
                                                  "</span></h3>";
                                blankLine++;
                            }

                            if (optionName != string.Empty)
                            {
                                printRecipeStr += "<h3 style='font-weight:bold; font-size: " +
                                                            reciept_font_small +
                                                            "px'><span style='text-align:left;float:left;width:75%;padding-left:10px'>" +
                                                            optionName +
                                                            "</span><span style='text-align:right;float:right;width:22%;'>" +
                                                            "" + "</span></h3>"; blankLine++;
                            }
                            if (aRestaurantInformation.MenuSeparation == 2)
                            {
                                printRecipeStr +=
                                       "<h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:3px;padding:0;'>&nbsp;</h3>";
                            }
                            if (starterId == GroupTitle.CategoryId)
                            {
                                maxStater--;
                            }
                            if (aRestaurantInformation.MenuSeparation == 3 && starterId == GroupTitle.CategoryId && maxStater == 0)
                            {

                                printRecipeStr += "<h3  style='border-bottom:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";

                            }

                            //if (starterId == GroupTitle.CategoryId)
                            //{
                            //    startdas = true;
                            //}
                            listOfReport.Add(new ReportData
                            {

                                Name = receipTypeLabel.Text,
                                Index = i,
                                Qty = 0,
                                ItemName = receipTypeLabel.Text,
                                ItemQty = Convert.ToInt16(gridViewAddtocard.GetRowCellValue(i, "QTY")),
                                Price = Convert.ToDouble(gridViewAddtocard.GetRowCellValue(i, "Total")),
                                OptionName = optionName,
                                GroupTitle = gridViewAddtocard.GetRowCellValue(i, "GroupName").ToString(),
                                Id = "-1",
                                CatId = GroupTitle.CategoryId,
                                SortOrder = GroupTitle.SortOrder,
                                ReceipeTypeId = GroupTitle.ReceipeTypeId
                            });
                            OrderMdList.Add(new OrderItemDetailsMD
                            {
                                CategoryId = GroupTitle.CategoryId,
                                CatSortOrder = GroupTitle.SortOrder,
                                RecipeTypeId = GroupTitle.ReceipeTypeId,
                                Qty = Convert.ToInt16(dataTable.Rows[i]["QTY"]),
                                ItemId = GroupTitle.ReceipeTypeId,
                                ItemName = receipTypeLabel.Text,
                                KitchenDone = kitichineDone,
                                ItemOption = listOption.ToList()
                               
                            });
                            ReceipeTypeButton receipeTypeButton = new ReceipeTypeButton();
                            receipeTypeButton.SortOrder = GroupTitle.SortOrder;
                            receipeTypeButton.Text = receipeTypeButton.Text = aReceipeTypeButton.FirstOrDefault(a => a.TypeId == GroupTitle.ReceipeTypeId).Text;

                            if (aListTypeDetails.Count(a => a.RecipeTypeId == GroupTitle.ReceipeTypeId) == 0)
                            {
                                aListTypeDetails.Add(new RecipeTypeDetails
                                {

                                    ReceipeTypeButton = receipeTypeButton,
                                    recipeTypeAmountlabel = recipeTypeAmountlabel,
                                    recipeTypelabel = receipTypeLabel,
                                    RecipeTypeId = GroupTitle.ReceipeTypeId});
                            }
                        }


                    }





                }

                if (isKitchenPrint && !(isPrint || isFinalize))
                {
                    GenerateKitchenCopy(OrderMdList, packageMds, aListTypeDetails, listOfReport, aRestaurantOrder.Id);

                    // KitchinePrint(listOfReport, mainFormView.PrinterSetups, aRestaurantOrder, aGeneralInformation, mainFormView.timeButton.Text);

                }

                double amount = GetTotalAmountDetails();

                if (blankLine < aRestaurantInformation.RecieptMinHeight)
                {
                    for (int kk = blankLine; kk < aRestaurantInformation.RecieptMinHeight; kk++)
                    {
                        printRecipeStr +=
                            "<h3  style='text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                    }


                }

                if (aGeneralInformation.OrderDiscount > 0)
                {

                    printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                      "px'><span style='text-align:left;float:left;width:35%;'>Discount</span><span style='text-align:right;float:right;width:62%;'>(" +
                                      aGeneralInformation.DiscountPercent.ToString("F02") + "%) £" +
                                      aGeneralInformation.OrderDiscount.ToString("F02") + "</span></h3>";


                }



                if (aGeneralInformation.CardFee > 0)
                {

                    printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                      "px'><span style='text-align:left;float:left;width:35%;'>S/C </span><span style='text-align:right;float:right;width:62%;'> £" +
                                      aGeneralInformation.CardFee.ToString("F02") + "</span></h3>";


                }

                if (aGeneralInformation.DeliveryCharge > 0 && deliveryButton.Text == "DEL" &&
                    deliveryButton.BackColor == Color.Black && collectionButton.BackColor != Color.Black)
                {

                    printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                      "px'><span style='text-align:left;float:left;width:35%;'>D/C</span><span style='text-align:right;float:right;width:62%;'> £" +
                                      aGeneralInformation.DeliveryCharge.ToString("F02") + "</span></h3>";
                }

                if (customTotalTextBox.Text != "Custom Total" && Convert.ToDouble(customTotalTextBox.Text) > 0)
                {
                    amount = Convert.ToDouble(customTotalTextBox.Text);
                }


                printFooterStr +=
                    "<h3 style='font-weight:bold;margin:30px auto;padding-top:10px;border-top:1px dashed; font-size:" +
                    reciept_font + "px'><span style='text-align:left;float:left;width:30%;'>" +
                    DateTime.Now.ToString("h:mm tt") + "</span><span style='text-align:right;float:right;width:70%;'>" +
                    "TOTAL&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; £" + amount.ToString("F02") + "</span></h3>";


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
                                          "px'><span style='text-align:left;float:left;width:65%;'>CASH ORDER</span><span style='text-align:right;float:right;width:30%;'>" +
                                          "£" + aRestaurantOrder.CashAmount.ToString("F02") + "</span></h3>";

                    }


                }
                else
                {

                    if (aPaymentDetails.CashAmount > 0 && aPaymentDetails.CardAmount > 0 && status)
                    {

                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:65%;'>CASH</span><span style='text-align:right;float:right;width:30%;'>" +
                                          "£" + aPaymentDetails.CashAmount.ToString("F02") + "</span></h3>";

                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:65%;'>PAID BY CARD </span><span style='text-align:right;float:right;width:30%;'>" +
                                          "£" + aPaymentDetails.CardAmount.ToString("F02") + "</span></h3>";

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

                    }
                    if (status)
                    {
                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font +
                                          "px'><span style='text-align:left;float:left;width:65%;'>CHANGE</span><span style='text-align:right;float:right;width:30%;'>" +
                                          "£" + aPaymentDetails.ChangeAmount.ToString("F02") + "</span></h3>";
                        printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font_exlgr +
                                          "px;border-top:1px dashed;'><span style='text-align:center;float:left;width:95%;'>PAID ORDER </span></h3>";
                    }


                }

                if (commentTextBox.Text != "Comment" && commentTextBox.Text.Trim().Length > 0)
                {
                    printFooterStr += "<h3  style='border-bottom:1px dashed;text-align:center;'>" + commentTextBox.Text +
                                      "</h3>";

                }
                if (aRestaurantInformation.ThankYouMsg.Length > 5)
                {
                    printFooterStr += "<h3  style='text-align:center;'>" + aRestaurantInformation.ThankYouMsg + "</h3>";

                }

                if (isPrint && aGeneralInformation.Person > 1)
                {
                    printFooterStr += "<h3 style='font-weight:bold; font-size:" + reciept_font + "px'>" + "Total split " +
                                      aGeneralInformation.Person + " ways £" +
                                      (amount / aGeneralInformation.Person).ToString("F02") + "</h3>";


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



                printHeaderStr += "</div>";
                printRecipeStr += "</div>";
                printFooterStr += "<br/><br/></div>";
                printStr = printHeaderStr + printRecipeStr + printFooterStr;

                SetDefaultPrinter(printer.PrinterName);


                for (int i = 0; i < printCopy; i++)
                {
                    try
                    {

                        string str =
                            "<html><head><style>html, body { padding: 0; margin: 0 }</style></head><body style='font-family:tahoma, sans-serif;margin:0;padding:0;'>" +
                            printStr + "</body></html>";
                        WebBrowser wbPrintString = new WebBrowser() { DocumentText = string.Empty };
                        wbPrintString.Document.Write(str);
                        wbPrintString.Document.Title = "";



                        string keyName = @"Software\\Microsoft\\Internet Explorer\\PageSetup";
                        Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true);

                        if (key != null)
                        {
                            //string old_footer = key.GetValue("footer");//string old_header = key.GetValue("header");
                            key.SetValue("footer", "");
                            key.SetValue("header", "");
                            key.SetValue("margin_bottom", "0");
                            key.SetValue("margin_left", "0.15");
                            key.SetValue("margin_right", "0");
                            key.SetValue("margin_top", "0");

                            //wbPrintString.ShowPrintDialog();

                            wbPrintString.Print();
                            wbPrintString.Dispose();

                        }

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Selected printer not exist!", "Printer Setup Warning", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }



        }
        private void DeleteNewCancelItem(List<OrderItem> newOrderItems, List<OrderPackage> newPackage, int orderId)
        {
            try
            {
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                bool res = aRestaurantOrderBLL.DeleteItemsAndPackage(orderId);

                //List<OrderItem> oldOrderItems = aRestaurantOrderBLL.GetRestaurantOrderRecipeItems(orderId);
                //List<OrderPackage> oldPackages = aRestaurantOrderBLL.GetRestaurantOrderPackage(orderId);
                //List<int> canceItem = new List<int>();
                //List<int> cancelPackage = new List<int>();
                //foreach (OrderItem oldItem in oldOrderItems)
                //{
                //    if (oldItem.PackageId <= 0)
                //    {
                //        try
                //        {
                //            var items = newOrderItems.Where(a => a.RecipeId == oldItem.RecipeId && a.Options == oldItem.Options);
                //            if (items == null || items.Count() <= 0)
                //            {
                //                canceItem.Add(oldItem.Id);

                //            }
                //        }
                //        catch (Exception exception)
                //        {
                //            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                //            aErrorReportBll.SendErrorReport(exception.ToString());
                //        }
                //    }
                //    else if (oldItem.PackageId > 0)
                //    {
                //        try
                //        {
                //            var items =
                //                newOrderItems.Where(
                //                    a => a.PackageId == oldItem.PackageId && a.RecipeId == oldItem.RecipeId);
                //            if (items == null || items.Count() <= 0)
                //            {
                //                canceItem.Add(oldItem.Id);

                //            }
                //        }
                //        catch (Exception exception)
                //        {
                //            ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                //            aErrorReportBll.SendErrorReport(exception.ToString());
                //        }

                //    }

                //}

                //foreach (OrderPackage oldPackage in oldPackages)
                //{


                //    var items = newPackage.Where(a => a.PackageId == oldPackage.PackageId);
                //    if (items == null || items.Count() <= 0)
                //    {
                //        cancelPackage.Add(oldPackage.Id);

                //    }

                //}


            }
            catch (Exception exception)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(exception.ToString());
            }



        }

        private double GetPackageItemPrice(int packageId, int optionIndex)
        {
            double sum = aPackageItemMdList.Where(a => a.PackageId == packageId && a.OptionsIndex == optionIndex).Sum(b => b.Price * b.Qty);
            return sum;
        }
        private double GetPackageItemPriceForFetech(int packageId, int optionIndex)
        {
            double sum = aPackageItemMdList.Where(a => a.PackageId == packageId && a.OptionsIndex == optionIndex).Sum(b => b.Price * b.Qty);
            return sum;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aOrderItemDetailsMDList"></param>
        /// <param name="aListTypeDetails"></param>
        /// <param name="cartOrderList"></param>
        /// <param name="orderId"></param>
        public void GenerateKitchenCopy(List<OrderItemDetailsMD> aOrderItemDetailsMDList, List<RecipePackageMD> aRecipePackageMdList, List<RecipeTypeDetails> aListTypeDetails, List<ReportData> cartOrderList, int orderId)
        {

           //0 = "None";
            //1 = "Category Wise";
            //2 = "Menu Wise";
            //3 = "Category With Title";
            //4 = "Starter";
            //5 = "Type With Title";

            string printHeaderStr =
                "<div style='width:260px; font-family:Arial, Helvetica, sans-serif;padding:0px 0px 0px 0px;border-bottom:double;border-top:double; margin-top: 5px'>";

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
            

            reciept_font = "22";
            string reciept_font_lgr = (Convert.ToDouble(reciept_font) + 1).ToString();
            string reciept_font_small = (Convert.ToDouble(reciept_font) - 1).ToString();

            string orderHistory = aVariousMethod.GetOrderHistory(papersize, orderId, aGeneralInformation, timeButton.Text.ToString(), true);
            printHeaderStr += "<div   style='width: 250px;'>" +
                              "<div style='text-align:center;font-size:15px'>" + DateTime.Now.ToString("ddd,dd/MM/yyyy HH:mm") + "</div>" +
                              "</div>";
            printHeaderStr += "<div  style='width: 250px;'><div style='text-align:center;'><b>" + orderHistory + "</b></div></div>";
            string customer = "";
            if (aGeneralInformation.CustomerId > 0)
            {
                customer +=
                    "<div style='font-family:monospace;border-top:1px dashed; margin-top:5px; font-weight:bold; font-size:" +
                    20 + "px'>";
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



                customer += "<div style='font-family:monospace; font-weight:bold; font-size:" + 18 +
                                  "px'>" + address + "</div>";
                if (!flag)
                {
                    customer += "<div style='font-family:monospace; text-align:right; font-weight:bold'>" +
                                      cell + "</div>";

                }

                customer += "</div>";
            }
            else if (customerTextBox.Text != "" && customerTextBox.Text != "Search Customer")
            {
                customer +=
                    "<div style='clear:both'></div><div  style='font-family:monospace; width: 268px;margin-bottom:10px; '>" +
                    "<div style='text-align:left;font-size:" +
                    18 + "px'><b>" + customerTextBox.Text + "</b></div></div>";
            }
            printHeaderStr += customer;
            foreach (PrinterSetup printer in PrinterSetups)
            {
               
             
                if (printer.PrintStyle == "Receipt")
                {
                    continue;
                }

                bool flag = false;
                string printStr = "";
                string printRecipeStr = "<div style='width: 260px; font-family:Cantarell; font-weight:lighter;line-height:26px;margin-top: 10px;padding:0px 0px 0px 0px;'>";
                string printFooterStr =
                    "<div style='width: 260px; font-family:Cantarell; font-weight:lighter;line-height:26px;padding:0px 0px 0px 0px;margin:0;'>";
                Dictionary<int, string> recipeTypes = GetRecipeTypes(printer);
                aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.SortOrder).ToList();
                aOrderItemDetailsMDList = aOrderItemDetailsMDList.OrderBy(a => a.CatSortOrder).ToList();

                int catId = 0;
                bool startdas = false;
                try
                {
                    aListTypeDetails = aListTypeDetails.OrderBy(a => a.ReceipeTypeButton.SortOrder).ToList();
                }
                catch (Exception es){

                }
                int FoundItemcount = 0;
                foreach (RecipeTypeDetails typeDetails in aListTypeDetails)
                {

                    var IsSelectedTypes = recipeTypes.Count(a => a.Key == typeDetails.RecipeTypeId);
                    if (IsSelectedTypes == 0)
                    {
                        continue;
                    }

                    if (menuSeperation == 5)
                    {
                        printRecipeStr += "<h3 class='type'><span>" + typeDetails.ReceipeTypeButton.Text.ToUpper() + "</span></h3>";
                    }
                    int CategoryId = 0;

                    int stater = 0;
                    foreach (OrderItemDetailsMD itemDetails in aOrderItemDetailsMDList)
                    {
                        if (itemDetails.KitchenDone >= itemDetails.Qty && aGeneralInformation.TableId > 0)
                        {
                            continue;
                        }if (itemDetails.RecipeTypeId == typeDetails.RecipeTypeId)
                        {
                            FoundItemcount++;
                            string CateName = catList.FirstOrDefault(a => a.CategoryId == itemDetails.CategoryId).CategoryName;
                            if (CategoryId != itemDetails.CategoryId && menuSeperation == 4)
                            {

                                //Cate+Title
                                printRecipeStr += "<h3 class='dashed'><span>" + CateName + "</span></h3>";

                                CategoryId = itemDetails.CategoryId;
                                
                            }
                            else if (CategoryId != itemDetails.CategoryId && menuSeperation == 1)
                            {
                                printRecipeStr += "<h3 class='dashed'></h3>";

                                CategoryId = itemDetails.CategoryId;
                            }
                            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                            //bool result1 = aRestaurantOrderBLL.UpdateOrderItemKitchenStatus(itemDetails.ItemId,
                            //   aGeneralInformation.OrderId);
                            printRecipeStr += "<h3 style='margin-top:5px;font-weight:lighter;line-height:26px; font-size:" +
                                              reciept_font +
                                              "px'><span style='text-align:left;width:95%;text-transform: capitalize;'>" +
                                              (aGeneralInformation.TableId > 0 ? (itemDetails.Qty - itemDetails.KitchenDone) : itemDetails.Qty) + " " + itemDetails.ItemName.ToLower() +
                                              "</span></h3>";
                            blankLine++;

                            List<OptionJson> aOption = itemDetails.ItemOption.ToList();
                            if (aOption.Count > 0)
                            {

                                foreach (OptionJson option in aOption)
                                {
                                    if (!option.NoOption)
                                    {
                                        printRecipeStr +=
                                            "<h3 style='font-weight:lighter;line-height:26px; font-size: " +
                                            reciept_font_small +
                                            "px'><span style='text-align:left;width:95%;padding-left:10px;text-transform: capitalize;'>" +
                                            "→" + option.optionName.ToLower() + "</span></h3>";
                                        blankLine++;
                                    }
                                    else
                                    {
                                        printRecipeStr +=
                                            "<h3 style='font-weight:lighter;line-height:26px; font-size: " +
                                            reciept_font_small +
                                            "px'><span style='text-align:left;width:95%;padding-left:10px;text-transform: capitalize;'>" +
                                            "→No" + option.optionName.ToLower() + "</span></h3>";
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
                                    //  Stater Category  Set Bottom border
                                    printRecipeStr += "<h3 class='dashed'></h3>";


                                }
                            }
                            if (menuSeperation == 2)
                            {
                                //Menu Wise
                                printRecipeStr += "<h3 style='border-top:1px dashed;margin-top:5px'></h3>";

                            }

                        }

                    }


                }
                foreach (RecipePackageMD package in aRecipePackageMdList)
                {
                    var type = recipeTypes.FirstOrDefault(a => a.Key == package.RecipeTypeId).Value;
                    if (type != null)
                    {

                        //List<PackageItem> aPaItem =aPackageItemMdList.Where(a => a.PackageId == package.PackageId && a.Id == package.id).ToList();
                        List<PackageItem> aPaItem = package.packageItemList;
                        if (aPaItem.Count == 0 || package.KitichineDone == package.Qty)
                        {
                            continue;
                        }
                        
                        FoundItemcount++;
                        printRecipeStr += "<h3 class='dashed'><span>" + type.ToUpper() + "</span></h3>";
                        printRecipeStr += "<h3 style='line-height:26px; font-size:" +
                                              reciept_font + "px;'><span style='text-align:left;width:95%;text-transform: capitalize;'>" +
                                         (package.Qty- package.KitichineDone).ToString() + " " + package.PackageName.ToLower() +
                                          "</span></h3>";
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
                            string packageItemPrice = itemDetails.Qty * itemDetails.Price > 0
                                ? (itemDetails.Qty * itemDetails.Price).ToString()
                                : "";

                            printRecipeStr += "<h3 style='font-weight:lighter;line-height:26px; font-size: " +
                                              reciept_font_small +
                                              "px'><span style='text-align:left;width:95%;padding-left:10px;text-transform: capitalize;'>" +
                                              itemDetails.Qty.ToString() + "  " + itemDetails.ItemName.ToLower() +
                                              "</span></h3>";
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
                                        printRecipeStr +=
                                            "<h3 style='font-weight:lighter;line-height:26px; font-size: " +
                                            reciept_font_small +
                                            "px'><span style='text-align:left;width:95%;padding-left:10px;text-transform: capitalize;'>" +
                                            "→" + option.Title.ToLower() + "</span> </h3>";
                                        blankLine++;
                                    }
                                    if (!string.IsNullOrEmpty(option.MinusOption))
                                    {
                                        printRecipeStr +=
                                            "<h3 style='font-weight:lighter;line-height:26px; font-size: " +
                                            reciept_font_small +
                                            "px'><span style='text-align:left;width:95%;padding-left:10px;text-transform: capitalize;'>" +
                                            "→No" + option.MinusOption.ToLower() + "</span></h3>";
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
                        printRecipeStr += "<h3 style='font-weight:lighter;line-height:26px; font-size:" +
                                          reciept_font +
                                          "px'><span style='text-align:left;width:95%;text-transform: capitalize;'>" +
                                          package.Qty.ToString() + " " + package.MultiplePartName.ToLower() +
                                          "</span></h3>";
                        blankLine++;
                        List<MultipleItemMD> aPaItem =
                            aMultipleItemMdList.Where(
                                a => a.CategoryId == package.CategoryId && a.OptionsIndex == package.OptionsIndex)
                                .ToList();
                        int cnt = 0;
                        foreach (MultipleItemMD itemDetails in aPaItem)
                        {
                            cnt++;
                            string packageItemPrice = itemDetails.Qty * itemDetails.Price > 0
                                ? (itemDetails.Qty * itemDetails.Price).ToString()
                                : "";

                            printRecipeStr += "<h3 style='font-weight:lighter;line-height:26px; font-size: " +
                                              reciept_font_small +
                                              "px'><span style='text-align:left;width:95%;padding-left:10px;text-transform: capitalize;'>" +
                                              (cnt != 2 ? GetOrdinalSuffix(cnt) : " " + GetOrdinalSuffix(cnt)) +
                                              ": " + itemDetails.ItemName.ToLower() + "</span></h3>";
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
                                        printRecipeStr +=
                                            "<h3 style='font-weight:lighter;line-height:26px; font-size: " +
                                            reciept_font_small +
                                            "px'><span style='text-align:left;width:95%;padding-left:10px;text-transform: capitalize;'>" +
                                            "→No" + option.Title.ToLower() + "</span></h3>";
                                        blankLine++;
                                    }
                                    if (!string.IsNullOrEmpty(option.MinusOption))
                                    {
                                        printRecipeStr +=
                                            "<h3 style='font-weight:lighter;line-height:26px; font-size: " +
                                            reciept_font_small +
                                            "px'><span style='text-align:left;width:95%;padding-left:10px;text-transform: capitalize;'>" +
                                            "No" + option.MinusOption.ToLower() + "</span></h3>";
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
                        printRecipeStr +=
                            "<h3  style='text-align:left;font-size:20px;line-height:22px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                    }
                    printRecipeStr +=
                        "<div style='clear:both'></div><h3  style='border-top:1px dashed;text-align:left;font-size:2px;line-height:3px;margin-bottom:1px;padding:0;'>&nbsp;</h3>";
                }

                if (commentTextBox.Text != "Comment" && commentTextBox.Text.Trim().Length > 0)
                {
                    printFooterStr +=
                        "<div style='clear:both'></div><h3  style='border-bottom:1px dashed;text-align:center;text-transform: capitalize;'>" +
                        commentTextBox.Text.ToLower() + "</h3>";

                }


                int printCopy = printer.PrintCopy;
                printHeaderStr += "</div>";
                printRecipeStr += "</div>";
                printFooterStr += "<br/><br/></div>";
                printStr = printHeaderStr + printRecipeStr + printFooterStr;

                PrinterSettings printerName = new PrinterSettings();
                string defPrinter = printerName.PrinterName;

                if (FoundItemcount <= 0)
                {
                    continue;
                }
                SetDefaultPrinter(printer.PrinterName);

                for (int i = 0; i < printCopy; i++)
                {
                    try
                    {
                        string str =
                            "<html><head><style>html, body ,h3" +
                            " { padding: 0; margin: 0;}" +
                            ".dashed { border-top: 1px dashed #000;text-align: center;margin: 20px 0 10px;line-height: 0;}" +
                            ".type {border-bottom: double;text-align:left;margin: 20px 0 10px;}" +
                            ".type span {background: #fff;padding: 0 5px;}" + ".dashed span {background: white;padding: 0 5px;}</style></head><body style='font-family:Arial, Helvetica, sans-serif;margin:0;padding:0;'>" +
                            printStr + "</body></html>";
                        WebBrowser wbPrintString = new WebBrowser() { DocumentText = string.Empty };
                        wbPrintString.Document.Write(str);
                        wbPrintString.Document.Title = "";
                        string keyName = @"Software\\Microsoft\\Internet Explorer\\PageSetup";
                        Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true);


                        if (key != null)
                        {
                            //string old_footer = key.GetValue("footer");
                            //string old_header = key.GetValue("header");
                            key.SetValue("footer", "");
                            key.SetValue("header", "");
                            key.SetValue("margin_bottom", "0");
                            key.SetValue("margin_left", "0.09");
                            key.SetValue("margin_right", "0.09");
                            key.SetValue("margin_top", "0");
                            key.SetValue("Print_Background", "yes");
                            wbPrintString.Print();
                            wbPrintString.Dispose();
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Selected printer not exist!", "Printer Setup Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
                //RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                //aRestaurantOrderBLL.UpdateKitchenStatus(aGeneralInformation.OrderId);
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

       private void gridViewAddtocard_RowLoaded(object sender, DevExpress.XtraGrid.Views.Base.RowEventArgs e)
        {
            //    ColumnView view = sender as ColumnView;
            //    if (needMoveLastRow)
            //    {
            //        needMoveLastRow = false;
            //        view.MoveLast();
            //    }
        }

        private void gridViewAddtocard_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //e.Appearance.BackColor=Color.Black;
        }

        private void gridViewAddtocard_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle == 0)
            {
                gridViewAddtocard.FocusedRowHandle = e.FocusedRowHandle;

            }
            rowPackageItemFocus = false;

        }

        private void personButtonNew_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            try
            {
                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();

                RestaurantTable aRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableNumber(aGeneralInformation.TableNumber);
                CoversForm.Status = "";
                CoversForm.Covers = "";
                CoversForm aForm = new CoversForm();
                aForm.ShowDialog();
                if (CoversForm.Status == "" || CoversForm.Status == "cancel") return;
                aRestaurantTable.Person = Convert.ToInt32("0" + CoversForm.Covers);
                aRestaurantTableBll.UpdateRestaurantTable(aRestaurantTable);
                aGeneralInformation.Person = aRestaurantTable.Person;
                personButtonNew.Text = "P " + aGeneralInformation.Person;
            }
            catch (Exception ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());
               
            }

           
        }

        private void PackageSaveTemp()
        {
            for (int i = 0; i < gridViewAddtocard.DataRowCount; i++)
            {
                var IsPackage = gridViewAddtocard.GetRowCellValue(i, "Group").ToString();
                if (IsPackage == "Package")
                {
                    //  List<PackageItem> packageItemMD =
                    //    (List<PackageItem>) gridViewAddtocard.GetRowCellValue(i, "Package");
                    //var packageItemOptionList = packageItemMD[0].RecipePackageButton;

                    RecipePackageMD packageMd = new RecipePackageMD();
                    List<PackageItem> packageId = (List<PackageItem>)gridViewAddtocard.GetRowCellValue(i, "Package");

                    packageMd.id = packageId[0].Id;

                    packageMd.PackageId = Convert.ToInt32(gridViewAddtocard.GetRowCellValue(i, "Cat"));
                    packageMd.PackageName = Convert.ToString(gridViewAddtocard.GetRowCellValue(i, "EditName"));
                    packageMd.Qty = Convert.ToInt32(gridViewAddtocard.GetRowCellValue(i, "QTY"));
                    packageMd.UnitPrice = (double)Convert.ToDecimal(gridViewAddtocard.GetRowCellValue(i, "Price"));

                    aRecipePackageMdList.Add(packageMd);
                }
            }
        }
        private void paidButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            }

            PaidAmountWithPrint(true, true);
            LoadMenuType();
        }

        private void finalizeButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            if (gridViewAddtocard.RowCount == 0)
            {
                MessageBox.Show("No items in this order!", "Save Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PaidAmountWithPrint(true, true, false, true);
           
        }

        bool isCustomerTextChanged = true;

        private void SelectDeliveryButton()
        {
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

        private void newCustomerButton_Click(object sender, EventArgs e)
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

        private void CustomerLoadWhenAddNewCustomer(RestaurantUsers restaurantUsers)
        {

            RestaurantUsers aRestaurantUser = restaurantUsers;
            if (aRestaurantUser != null && aRestaurantUser.Id > 0)
            {

                string cell = aRestaurantUser.Mobilephone != ""
                    ? aRestaurantUser.Mobilephone
                    : aRestaurantUser.Homephone;
                string customerAddress = GetCustomerDetails(aRestaurantUser);

                customerDetailsLabelNew.Text = customerAddress;
                customerDetailsLabelNew.Visible = true;
                customerEditButtonNew.Visible = true;
                phoneNumberDeleteButton.Visible = true;
                customerTextBox.Text = cell;

                customerTextBox.Visible = false;
                customerTextBox.SelectionStart = customerTextBox.Text.Length;
                aGeneralInformation.CustomerId = aRestaurantUser.Id;
                if (aRestaurantUser.Postcode.Length > 0)
                {
                    LoadDeliveryCharge(aRestaurantUser);
                }

                searchCustomerpanel.Width = customerDetailsLabelNew.Width;
            }
            else
            {

                customerDetailsLabelNew.Text = "";
                customerEditButtonNew.Visible = false;
                customerTextBox.Visible = true;
                if (customerTextBox.Text == "Search Customer")
                {
                    phoneNumberDeleteButton.Visible = false;
                }
                else
                {
                    phoneNumberDeleteButton.Visible = true;
                }
                aGeneralInformation.CustomerId = 0;
            }



        }

        private void LoadDeliveryCharge(RestaurantUsers aRestaurantUser)
        {
            try
            {
                double deliveryCharge = GetDeliveryCharge(aRestaurantUser);

                if (deliveryButton.Text != "RES" && deliveryButton.BackColor == Color.Black)
                {

                    double totalAmount = GetTotalAmountDetails();
                    aGeneralInformation.DeliveryCharge = totalAmount >= aRestaurantInformation.MinOrder ? 0 : deliveryCharge;
                    deliveryChargeButton.Text = aGeneralInformation.DeliveryCharge <= 0
                        ? "Delivery Charge\r\n0"
                        : "Delivery Charge\r\n" + aGeneralInformation.DeliveryCharge.ToString();
                    LoadAmountDetails();
                }
            }
            catch (Exception e)
            {
                new ErrorReportBLL().SendErrorReport(e.GetBaseException().ToString());


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


        private double GetDeliveryCharge(RestaurantUsers aRestaurantUser)
        {

            //if (string.IsNullOrEmpty(aRestaurantUser.Postcode))
            //{
            //    return GlobalSetting.RestaurantInformation.DeliveryCharge;
            //}

            //try
            //{
            //    CoverageAreaBLL aCoverageAreaBll = new CoverageAreaBLL();
            //    DistanceBLL aDistanceBll = new DistanceBLL();
            //    DeliveryChargeBLL aDeliveryChargeBll = new DeliveryChargeBLL();
            //    string postCode = aRestaurantUser.Postcode.Replace(" ", "").ToUpper();
            //    string _shortpostCode = postCode.Substring(0, postCode.Length - 3).ToUpper();
            //    AreaCoverage areaCoverage = aCoverageAreaBll.GetCoverageAreaByPostcode(postCode, aRestaurantInformation.Id);
            //    if (!string.IsNullOrEmpty(areaCoverage.Postcode))
            //    {
            //        return areaCoverage.DeliveryCharge;
            //    }
            //    string restaurantPostCode = aRestaurantInformation.Postcode.Replace(" ", "").ToUpper();
            //    bool haveDeliveryCharge = aDeliveryChargeBll.CheckDeliveryCharge(aRestaurantInformation.Id);
            //    if (haveDeliveryCharge)
            //    {
            //        double distnace = 0;
            //        Distance aDistance = aDistanceBll.GetDistanceByPostcode(restaurantPostCode, postCode);
            //        if (aDistance != null && aDistance.Id > 0)
            //        {
            //            distnace = aDistance.distance;
            //        }
            //        else
            //        {
            //            string link = "http://maps.googleapis.com/maps/api/distancematrix/json?origins=" + restaurantPostCode + "&destinations=" + postCode + "&sensor=false";
            //            string text = "";
            //            try
            //            {
            //                var request = (HttpWebRequest)WebRequest.Create(link);
            //                using (var response = request.GetResponse())
            //                {
            //                    using (var reader = new StreamReader(response.GetResponseStream()))
            //                    {
            //                        text = reader.ReadToEnd();
            //                    }
            //                }
            //                string json = text;
            //                var aDistanceLookUp = JsonConvert.DeserializeObject<DistanceLookUpJson>(text);




            //                if (aDistanceLookUp.status == "OK")
            //                {
            //                    //  List<element> elements=

            //                    if (aDistanceLookUp.rows[0].elements[0].status == "OK")
            //                    {
            //                        distnace = Convert.ToDouble(aDistanceLookUp.rows[0].elements[0].distance.value) / 1609.344;

            //                        aDistance = new Distance();
            //                        aDistance.source = restaurantPostCode;
            //                        aDistance.destination = postCode;
            //                        aDistance.distance = distnace;
            //                        aDistanceBll.InsertDistance(aDistance);
            //                    }

            //                }


            //            }
            //            catch (Exception ex) {
            //                return GlobalSetting.RestaurantInformation.DeliveryCharge;
            //            }

            //        }

            //        if (distnace > 0)
            //        {
            //            DelvaryCharge aDelivaryCharge = aDeliveryChargeBll.GetDeliveryChargeByDistance(distnace, aRestaurantInformation.Id);
            //            if (aDelivaryCharge != null && aDelivaryCharge.Id > 0)
            //            {
            //                return aDelivaryCharge.amount;
            //            }
            //        }
            //    }

            //    AreaCoverage anotherCoverage = aCoverageAreaBll.GetCoverageAreaByPostcode(_shortpostCode, aRestaurantInformation.Id);

            //    if (!string.IsNullOrEmpty(anotherCoverage.Postcode))
            //    {
            //        if (anotherCoverage.DeliveryCharge > 0)
            //        {
            //            return anotherCoverage.DeliveryCharge;
            //        }
            //        return aRestaurantInformation.DeliveryCharge;
            //    }
            //}
            //catch (Exception exception) 
            //{
            //    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //    aErrorReportBll.SendErrorReport(exception.ToString());
            //    return aRestaurantInformation.DeliveryCharge;

            //}

            return GlobalSetting.RestaurantInformation.DeliveryCharge;

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

        private void CustomerTextBoxChanged()
        {
            if (customerTextBox.Text.Trim() != "Search Customer" && customerTextBox.Text.Trim() != "" &&
                customerTextBox.Text.Trim().Length > 2 && isCustomerTextChanged)
            {
                phoneNumberDeleteButton.Visible = true;
                string query = customerTextBox.Text.Trim();
                customerShowFlowLayoutPanel.Controls.Clear();
                CustomerBLL aCustomerBll = new CustomerBLL();
                List<RestaurantUsers> showRestaurantUsers = aCustomerBll.GetRestaurantAllCustomerForShow(query);
                if (showRestaurantUsers.Any())
                {
                    RemoveCustomerControl(dynamicTableLayoutPanel);
                    if (showRestaurantUsers.Count() == 1 && query.Length >= 11)
                    {
                        CustomerLoadWhenAddNewCustomer(showRestaurantUsers.FirstOrDefault());
                        customerShowFlowLayoutPanel.Visible = false;
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
                        aCustomerShowButton.Height = 100;
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


                // customerShowFlowLayoutPanel.Visible = false;
            }

        }

        private void customerTextBox_TextChanged(object sender, EventArgs e)
        {
            CustomerTextBoxChanged();

            //if (!aGeneralInformation.CustomerId > 0)
            //{

            //    recentItemsFlowLayoutPanel.Controls.Clear();
            //}

        }

        private void ShowCustomerButton_Click(object sender1212, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            CustomerShowButton aCustom = sender1212 as CustomerShowButton;
            restaurantUsers = new RestaurantUsers();
            restaurantUsers = aCustom.RestaurantUsers;
            if (restaurantUsers != null && restaurantUsers.Id > 0)
            {

                string cell = restaurantUsers.Mobilephone != ""
                    ? restaurantUsers.Mobilephone
                    : restaurantUsers.Homephone;
                string customerAddress = GetCustomerDetails(restaurantUsers);
                customerDetailsLabelNew.Text = customerAddress;
                customerDetailsLabelNew.Visible = true;
                customerEditButtonNew.Visible = true;
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
                customerEditButtonNew.Visible = false;
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


        private void customerTextBox_Leave(object sender, EventArgs e)
        {
            if (customerTextBox.Text == "")
            {
                customerTextBox.Text = "Search Customer";

            }


        }

        private void customerTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                customerTextBox.Text = "";
                // aOthersMethod.KeyBoardClose();
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

        private void customerTextBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == (char)13)
            {
                if (customerTextBox.Text != "")
                {
                    SearchUserCustome aCustom = new SearchUserCustome();
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
                        customerEditButtonNew.Visible = true;
                        phoneNumberDeleteButton.Visible = true;
                        customerTextBox.Text = cell;
                        customerTextBox.SelectionStart = customerTextBox.Text.Length;
                        customerTextBox.Visible = false;
                    }
                    else
                    {
                        customerDetailsLabel.Text = "";
                        customerEditButtonNew.Visible = false;
                        phoneNumberDeleteButton.Visible = true;
                        customerTextBox.Visible = true;
                    }
                }
            }
        }

        public void UpdateCartOtionItem(int recipe_id, string MenuType, List<OptionJson> OptionListName)
        {

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();


            ReceipeMenuItemButton aMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(recipe_id);
            ReceipeCategoryButton aReceipeCategoryButton = aRestaurantMenuBll.GetCategoryByCategoryId(aMenuItemButton.CategoryId);



            CartItem aReceipeSubCategoryButton = new CartItem();
            aReceipeSubCategoryButton.CategoryId = aMenuItemButton.CategoryId;
            aReceipeSubCategoryButton.RecipeMenuItemId = aMenuItemButton.RecipeMenuItemId;

            aReceipeSubCategoryButton.Appearance.BackColor = aMenuItemButton.BackColor;


            ReceipeMenuItemButton menuItemButton = null;

            if (menuItemButton != null)
            {
                aReceipeSubCategoryButton = new CartItem();
                aReceipeSubCategoryButton.CategoryId = menuItemButton.CategoryId;
                aReceipeSubCategoryButton.RecipeMenuItemId = menuItemButton.RecipeMenuItemId;
                aReceipeSubCategoryButton.Recipeid = menuItemButton.RecipeTypeId;

            }

            List<RecipeOptionButton> OptionList = new RestaurantMenuBLL().GetRecipeOptionWhenItemClick(aReceipeSubCategoryButton.RecipeMenuItemId).ToList();
            if (OptionList.Count > 0)
            {

                int recipId = Convert.ToInt32(aReceipeSubCategoryButton.RecipeMenuItemId);
                menuItemButton = aRestaurantMenuBll.GetRecipeByItemId(recipId);
                menuItemButton.RecipeTypeId = recipId;

                pannelTopBar.Visible = true;




                aReceipeCategoryButton.Margin = new Padding(0);
                aReceipeCategoryButton.Padding = new Padding(0);
                aReceipeCategoryButton.AutoSizeMode = AutoSizeMode.GrowOnly;
                aReceipeCategoryButton.Height = pannelTopBar.Height - 2;
                aReceipeCategoryButton.BackColor = Color.Black;
                aReceipeCategoryButton.FlatStyle = FlatStyle.Flat;
                aReceipeCategoryButton.ForeColor = Color.White;


                menuItemButton.Text = menuItemButton.ReceiptName;
                menuItemButton.Margin = new Padding(0);
                menuItemButton.Padding = new Padding(0);
                menuItemButton.AutoSize = true;
                menuItemButton.AutoSizeMode = AutoSizeMode.GrowOnly;
                menuItemButton.Height = pannelTopBar.Height - 2;
                menuItemButton.ForeColor = Color.White;
                menuItemButton.FlatStyle = FlatStyle.Flat;

                var subCatColorCode = aRestaurantMenuBll.GetAllSubcategory().FirstOrDefault(a => a.SubCategoryId == menuItemButton.SubCategoryId);
                if (subCatColorCode != null)
                {
                    menuItemButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode(subCatColorCode.ButtonColor));

                }
                else
                {
                    menuItemButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("primary"));

                }





                if (aReceipeCategoryButton.HasSubcategory == 0)
                {
                    MenuType = "Category";
                    ItemOptionLoad(OptionList, menuItemButton, MenuType, true);
                    RetriveUpdateOptionList(OptionListName, gridViewOption);
                }
                else
                {
                    MenuType = "Type";
                    ItemOptionLoad(OptionList, menuItemButton, MenuType, true);
                    RetriveUpdateOptionList(OptionListName, gridViewOption);
                }

                flowLayoutPanel1.Controls.Clear();
                if (MenuType == "Type")
                {
                    var FirstType = ReceipeMixType.FirstOrDefault(a => a.ReceipeTypeButton.TypeId == aReceipeCategoryButton.ReceipeTypeId).ReceipeTypeButton;
                    if (FirstType != null)
                    {

                        ReceipeTypeButton topButton = new ReceipeTypeButton();
                        topButton.Text = FirstType.Text;
                        topButton.TypeId = FirstType.TypeId;
                        topButton.Margin = new Padding(0);
                        topButton.Padding = new Padding(0);
                        topButton.TypeId = FirstType.TypeId;
                        topButton.Font = FirstType.Font;
                        topButton.AutoSize = true;
                        topButton.AutoSizeMode = AutoSizeMode.GrowOnly;
                        topButton.Height = pannelTopBar.Height - 2;
                        topButton.BackColor = Color.Blue;
                        topButton.ForeColor = Color.White;
                        topButton.FlatStyle = FlatStyle.Flat;
                        topButton.Click -= new EventHandler(topFirstMenuClick);
                        topButton.Click += new EventHandler(topFirstMenuClick);

                        flowLayoutPanel1.Controls.Add(topButton);

                    }
                    aReceipeCategoryButton.Click -= topcatClick;
                    aReceipeCategoryButton.Click += topcatClick;



                    flowLayoutPanel1.Controls.Add(aReceipeCategoryButton);

                    flowLayoutPanel1.Controls.Add(menuItemButton);

                    menuItemButton.Click += TypeButtonOnClick;


                }
                else
                {
                    aReceipeCategoryButton.Click -= new EventHandler(receipeCategoryTypeButtonClick);
                    aReceipeCategoryButton.Click += new EventHandler(receipeCategoryTypeButtonClick);

                    flowLayoutPanel1.Controls.Add(aReceipeCategoryButton);
                    flowLayoutPanel1.Controls.Add(menuItemButton);

                    menuItemButton.Click += (sender, args) =>
                    {
                        // Control exitsButton = (Control)flowLayoutPanel1.Controls[0];
                        CartItem item = new CartItem();

                        item.Recipeid = menuItemButton.RecipeTypeId;
                        item.CategoryId = menuItemButton.CategoryId;
                        item.Appearance.Font = aReceipeCategoryButton.Font;
                        item.Text = aReceipeCategoryButton.Text;
                        CategoryLoad(item, dynamicTableLayoutPanel);
                    };

                }


            }


        }

        private void TypeButtonOnClick(object sender, EventArgs eventArgs)
        {
            flowLayoutPanel1.Controls.RemoveAt(flowLayoutPanel1.Controls.Count - 1);

            ReceipeMenuItemButton receipeCategoryTypeButton = sender as ReceipeMenuItemButton;


            CartItem cartItem = new CartItem();
            cartItem.CategoryId = receipeCategoryTypeButton.CategoryId;
            cartItem.Recipeid = receipeCategoryTypeButton.RecipeTypeId;
            cartItem.Text = receipeCategoryTypeButton.Text;

            TypeSubCatLoad(cartItem);
        }


        private void RetriveUpdateOptionList(List<OptionJson> optionList, GridView optionGridView)
        {
            // var RecipeOptionItemButton_List = new RestaurantMenuBLL().GetRecipeOptionItemByOptionId(group);
            string selectName = gridViewAddtocard.GetFocusedRowCellValue("Name").ToString();

            string removeString = Regex.Replace(selectName, @"<[^>]+>|", "");
            //Dictionary<Int64, TempOptionDictionary> OptionName = new Dictionary<Int64, TempOptionDictionary>();
            //for (int i = 0; i < result.Length; i++)
            //{
            //    string qty = Regex.Match(result[i], @"\d+").Value;
            //    if (result[i].Contains('£') && qty != string.Empty)
            //    {
            //        string name = result[i].Substring(1, result[i].IndexOf('£') - qty.Length).Trim();
            //        string price = result[i].Substring(name.Length, result[i].Length - name.Length).Replace("£", String.Empty);

            //        OptionName.Add(Convert.ToInt64(optionList[i]), new TempOptionDictionary { OptionName = name, Qty = Convert.ToInt32(qty), Price = Convert.ToDecimal(0.0) });
            //    }
            //    else
            //    {
            //        OptionName.Add(Convert.ToInt64(optionList[i]), new TempOptionDictionary { OptionName = result[i] });
            //    }

            //    // OptionName.Add(Convert.ToInt64(optionList[i]), new TempOptionDictionary { OptionName = result[i] });


            //}

            foreach (var OptionId in optionList)
            {

                //RecipeOptionButton tempOption = new RecipeOptionButton();
                //tempOption.RecipeOptionId = Convert.ToInt32(OptionId);
                //var list = new RestaurantMenuBLL().GetRecipeOptionItemByOnlyId(tempOption);


                for (int i = 0; i < tabControl.TabPages.Count; i++)
                {
                    var tileGroupName = tabControl.TabPages[i].Controls;
                    TileControl tileControl = (TileControl)tileGroupName[0];

                    var checkItem = tileControl.Groups[0].Items;

                    CartItem TopBar = (CartItem)checkItem[0];
                    if (TopBar.Title == null)
                    {
                        checkItem = tileControl.Groups[1].Items;
                    }
                    //if (tabControl.TabPages.Count>1)
                    //{
                    //    checkItem = tileControl.Groups[i].Items;
                    //}


                    for (int k = 0; k < checkItem.Count; k++)
                    {
                        CartItem item = (CartItem)checkItem[k];

                        if (item.RecipeOptionItemId == Convert.ToInt32(OptionId.optionId))
                        {
                            optionDataRow = optionItemDataTable.NewRow();
                            optionDataRow[0] = optionItemDataTable.Rows.Count + 1;

                            var IsNoOption = optionList.FirstOrDefault(a => Convert.ToUInt32(a.optionId) == Convert.ToInt64(OptionId.optionId));
                            if (IsNoOption != null)
                            {

                                if (OptionId.NoOption)
                                {
                                    item.Text = IsNoOption.optionName;
                                    item.RecipeOptionItemButton.Title = "No " + item.RecipeOptionItemButton.Title;
                                    item.RecipeOptionItemButton.InPrice = 0;
                                    item.RecipeOptionItemButton.Price = 0;
                                    item.IsNooption = true;
                                    optionDataRow[1] = IsNoOption.optionName;
                                    optionDataRow["Qty"] = 1;
                                }
                                else
                                {
                                    optionDataRow[1] = IsNoOption.optionName;
                                    optionDataRow[3] = Convert.ToDouble(IsNoOption.optionPrice).ToString("N2");

                                    if (IsNoOption.optionPrice > 0.0)
                                    {
                                        optionDataRow["Qty"] = IsNoOption.optionQty;
                                    }
                                    else
                                    {
                                        optionDataRow["Qty"] = 1;
                                    }

                                }


                            }
                            else
                            {
                                optionDataRow[1] = item.RecipeOptionItemButton.Title;
                                optionDataRow[3] = null;

                                optionDataRow["Qty"] = 1;
                            }


                            if (aGeneralInformation.TableId > 0)
                            {


                                if (item.RecipeOptionItemButton.InPrice > 0)
                                {
                                    //optionDataRow[3] = item.RecipeOptionItemButton.InPrice;
                                    optionDataRow["OrgPrice"] = item.RecipeOptionItemButton.InPrice;
                                }
                                else
                                {
                                    optionDataRow[3] = null;
                                }
                            }
                            else
                            {
                                if (item.RecipeOptionItemButton.Price > 0)
                                {
                                    //  optionDataRow[3] = item.RecipeOptionItemButton.Price;
                                    optionDataRow["OrgPrice"] = item.RecipeOptionItemButton.Price;
                                }
                                else
                                {
                                    optionDataRow[3] = null;
                                }


                            }




                            optionDataRow["OptionId"] = item.RecipeOptionItemId;

                            optionDataRow[2] = tabControl.TabPages[i].Text;
                            optionDataRow["CartItem"] = item;






                            optionItemDataTable.Rows.Add(optionDataRow);
                            item.Checked = true;
                        }
                    }
                }

            }

            gridControlOption.DataSource = optionItemDataTable;
            optionGridView.ExpandAllGroups();

            // optionGridView
            //  

        }
        private void LoadRecetAddedItems()
        {
            recentItemsFlowLayoutPanel.Visible = true;

            // recentItemsFlowLayoutPanel.Location = new Point(recentItemsFlowLayoutPanel.Location.X, customPanel.Location.Y - recentItemsFlowLayoutPanel.Height);

            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            CustomerRecentItemBLL aCustomerRecentItemBll = new CustomerRecentItemBLL();
            List<CustomerRecentItemMD> aCustomerRecentItemMds =
                aCustomerRecentItemBll.GetCustomerRecentItemMd(aGeneralInformation.CustomerId);
            if (aCustomerRecentItemMds.Any())
            {
                recentItemsFlowLayoutPanel.Controls.Clear();
                ReceipeMenuItemButton aMenuItemButton1 = new ReceipeMenuItemButton();
                aMenuItemButton1.Text = "Hide";
                aMenuItemButton1.Width = (recentItemsFlowLayoutPanel.Width / 3) - 4;
                aMenuItemButton1.Dock = DockStyle.Right;

                aMenuItemButton1.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("danger"));
                aMenuItemButton1.ForeColor = Color.White;
                aMenuItemButton1.Height = 30;
                aMenuItemButton1.FlatStyle = FlatStyle.Flat;
                aMenuItemButton1.FlatAppearance.BorderSize = 0;
                aMenuItemButton1.Click += new EventHandler(HideButton_Click);

                Panel topPanel = new Panel
                {
                    Dock = DockStyle.Top,
                    Width = recentItemsFlowLayoutPanel.Width - 7,
                    Height = 30
                };
                topPanel.Controls.Add(aMenuItemButton1);

                recentItemsFlowLayoutPanel.Controls.Add(topPanel);

                foreach (CustomerRecentItemMD itemMd in aCustomerRecentItemMds)
                {
                    if (itemMd.recipe_id > 0)
                    {

                        ReceipeMenuItemButton aMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(itemMd.recipe_id);
                        ReceipeCategoryButton aReceipeCategoryButton = aRestaurantMenuBll.GetCategoryByCategoryId(aMenuItemButton.CategoryId);
                        aMenuItemButton.Text = aMenuItemButton.ItemName;
                        aMenuItemButton.ButtonWidth = recentItemsFlowLayoutPanel.Width;
                        aMenuItemButton.Padding = new Padding(0);

                        //  aMenuItemButton.AutoSize = true;


                        aMenuItemButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("info"));
                        aMenuItemButton.ForeColor = Color.White;
                        aMenuItemButton.Height = 30;
                        aMenuItemButton.Width = (recentItemsFlowLayoutPanel.Width / 3) - 6;
                        aMenuItemButton.FlatStyle = FlatStyle.Flat;
                        aMenuItemButton.FlatAppearance.BorderSize = 0;
                        aMenuItemButton.Status = "RecentItem";
                        aMenuItemButton.Click -= new EventHandler(ReceipeMenuItemButton_Click);
                        aMenuItemButton.Click += new EventHandler(ReceipeMenuItemButton_Click);
                        //aMenuItemButton.Click += (sender, args) =>
                        //{
                        // //  OptionItemMainAddtoCart();

                        //    AddToCard(aMenuItemButton,"XYZ",null);
                        //};

                        aMenuItemButton.RecipeTypeId = aReceipeCategoryButton.ReceipeTypeId;
                        recentItemsFlowLayoutPanel.Controls.Add(aMenuItemButton);
                    }
                    else if (false)
                    {
                        //RecipePackageButton aRecipePackageButton = aRestaurantMenuBll.GetPackageByPackageId(itemMd.package_id);
                        //aRecipePackageButton.Text = aRecipePackageButton.PackageName;
                        //aRecipePackageButton.BackColor = ColorTranslator.FromHtml(aRestaurantMenuBll.GetColorCode("info"));
                        //aRecipePackageButton.ForeColor = Color.White;
                        //aRecipePackageButton.Height = 50;
                        //aRecipePackageButton.Width = 120;
                        //aRecipePackageButton.FlatStyle = FlatStyle.Flat;
                        //aRecipePackageButton.FlatAppearance.BorderSize = 0;
                        //aRecipePackageButton.Click += new EventHandler(aRecipePackageButton_Click);
                        //itemsflowLayoutPanel.Controls.Add(aRecipePackageButton);
                    }
                }
                recentItemsFlowLayoutPanel.MinimumSize = new System.Drawing.Size(recentItemsFlowLayoutPanel.Width,
                    recentItemsFlowLayoutPanel.Height);

                // no larger than screen size
                // recentItemsFlowLayoutPanel.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, (int)System.Windows.SystemParameters.PrimaryScreenHeight);

                recentItemsFlowLayoutPanel.AutoSize = true;
                recentItemsFlowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;



            }
        }

        private void HideButton_Click(object sen5, EventArgs e)
        {
            recentItemsFlowLayoutPanel.Visible = false;
        }

        public class SearchUserCustome
        {
            public int UserId { set; get; }
            public string CustomerDetails { set; get; }

        }

        private void phoneNumberDeleteButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            customerTextBox.Text = "Search Customer";
            customerDetailsLabel.Text = "";
            customerEditButtonNew.Visible = false;
            phoneNumberDeleteButton.Visible = false;
            customerDetailsLabelNew.Visible = false;
            customerTextBox.Visible = true;
            searchCustomerpanel.Size = new Size(173, 43); aGeneralInformation.CustomerId = 0;
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

        private void customerRecentItemsButton_Click(object sender, EventArgs e)
        {
            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            }
            LoadRecetAddedItems();
        }




        public void TempOptionListAdd(string MenuType, bool IsUpdate)
        {
            try
            {
                ReceipeMenuItemButton aReceipeMenuItemButton = OptionItemMainAddtoCart(IsUpdate);

                if (aReceipeMenuItemButton.Status == "Finish")
                {
                    MultiInformationClear();
                    return;
                }
                if (MenuType == "Type")
                {
                    //flowLayoutPanel1.Controls.Clear();
                    tileControl1.Controls.Clear();

                    var topButton = flowLayoutPanel1.Controls.OfType<ReceipeTypeButton>().FirstOrDefault();
                    //IEnumerable<ReceipeCategoryButton> updateCat = flowLayoutPanel1.Controls.OfType<ReceipeCategoryButton>();

                    CartItem typCartItem = new CartItem();



                    typCartItem.TypeId = topButton.TypeId;



                    TypeCatLoad(typCartItem, dynamicTableLayoutPanel);

                    if (multipleItem.itemLimit > 0)
                    {

                        TypeSubCatLoad(receipeCategoryTypeButton);
                        flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1].Dispose(); return;
                    }

                    int Removecount = 0;
                    for (int i = 1; i < flowLayoutPanel1.Controls.Count; i++)
                    {

                        flowLayoutPanel1.Controls.RemoveAt(i);
                        i--;
                        // Removecount++;


                    }

                }
                else if (MenuType == "Category")
                {
                    flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1].Dispose();
                    CategoryLoad(aCategoryButton, dynamicTableLayoutPanel);
                }
                if (optionDataRow != null)
                {
                    gridControlOption.DataSource = null;
                    optionDataRow.Table.Clear();
                }


                //*************************Go to Home Menu*******************************************

                LoadMenuType();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());

            }
        }

        public ReceipeMenuItemButton OptionItemMainAddtoCart(bool IsUpdate)
        {
            RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();
            ReceipeMenuItemButton aReceipeSubCategoryButton = (ReceipeMenuItemButton)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1];

            ReceipeMenuItemButton aReceipeMenuItemButton = new ReceipeMenuItemButton();
            if (aReceipeSubCategoryButton != null)
            {

                int recipId = Convert.ToInt32(aReceipeSubCategoryButton.RecipeTypeId);
                aReceipeMenuItemButton = aRestaurantMenuBll.GetRecipeByItemId(recipId);
                aReceipeMenuItemButton.RecipeTypeId = aReceipeSubCategoryButton.RecipeTypeId;
                aReceipeMenuItemButton.OptionJson = new List<OptionJson>();

                string mixText = aReceipeSubCategoryButton.Text;
                string optionList = "";
                string OptionId = "";
                decimal optionPrice = 0;
                if (optionItemDataTable.Rows.Count > 0)
                {
                    mixText = "<h4 style='font-size:12px;margin:0px; text-align:left'>" + aReceipeSubCategoryButton.Text + "</h4>";
                    mixText += "<table style='width:100%;'>";
                    for (int i = 0; i < optionItemDataTable.Rows.Count; i++)
                    {

                        optionList += "&rarr;" + optionItemDataTable.Rows[i]["OptionName"] + "</br>";
                        OptionId += "," + optionItemDataTable.Rows[i]["OptionId"];
                        string price = optionItemDataTable.Rows[i]["Price"].ToString();
                        string qty = optionItemDataTable.Rows[i]["Qty"].ToString();

                        CartItem item = (CartItem)optionItemDataTable.Rows[i]["CartItem"];

                        if (price == string.Empty)
                        {
                            mixText += "<tr>" + "<td>" + "&rarr;" + qty + "</td><td> " + optionItemDataTable.Rows[i]["OptionName"] + "</td>" + " <td style='text-align:right'>" + "</td>" + "</tr>";

                            optionPrice = Convert.ToDecimal(0.0) + optionPrice;
                            aReceipeMenuItemButton.OptionJson.Add(new OptionJson()
                            {
                                optionId = optionItemDataTable.Rows[i]["OptionId"].ToString(),
                                optionName = optionItemDataTable.Rows[i]["OptionName"].ToString(),
                                optionPrice = Convert.ToDouble(0.0),
                                optionQty = Convert.ToInt32(qty),
                                NoOption = Convert.ToBoolean(item.IsNooption)
                            });
                        }
                        else
                        {
                            mixText += "<tr>" + "<td>" + "&rarr;" + qty + " </td><td>" + optionItemDataTable.Rows[i]["OptionName"] + "</td>" + " <td style='text-align:right'>£ " + price + "</td>" + "</tr>";

                            aReceipeMenuItemButton.OptionJson.Add(new OptionJson()
                            {
                                optionId = optionItemDataTable.Rows[i]["OptionId"].ToString(),
                                optionName = optionItemDataTable.Rows[i]["OptionName"].ToString(),
                                optionPrice = Convert.ToDouble(price),
                                optionQty = Convert.ToInt32(qty),
                                NoOption = Convert.ToBoolean(item.IsNooption)
                            });
                        }



                    }

                    mixText += "</table>";
                }


                //  mixText = "<table style='width:100%;'>" + "<tr>" + "<td>Item1</td><td style='text-align:right'>$0.05</td>" + "</tr>" + "</table>";
                for (int i = 0; i < gridViewAddtocard.DataRowCount; i++)
                {
                    var ReceipMenuId = gridViewAddtocard.GetRowCellValue(i, "ReceipTypeId");
                    var OptionItemId = gridViewAddtocard.GetRowCellValue(i, "OptionId");

                    if (IsUpdate)
                    {
                        if (ReceipMenuId.ToString() == recipId.ToString())
                        {
                            gridViewAddtocard.DeleteSelectedRows();
                            break;
                        }
                    }
                    else
                    {
                        if (ReceipMenuId.ToString() == recipId.ToString() && OptionItemId.ToString() == OptionId)
                        {
                            gridViewAddtocard.DeleteSelectedRows();
                            break;
                        }
                    }


                }


                optionPrice = (decimal)aReceipeMenuItemButton.OptionJson.Sum(a => a.optionPrice * a.optionQty);

                if (aGeneralInformation.TableId > 0)
                {
                    aReceipeMenuItemButton.InPrice = Convert.ToDouble(optionPrice + Convert.ToDecimal(aReceipeMenuItemButton.InPrice));

                }
                else
                {

                    aReceipeMenuItemButton.OutPrice = Convert.ToDouble(optionPrice + Convert.ToDecimal(aReceipeMenuItemButton.OutPrice));

                }

                if (Properties.Settings.Default.ItemNameChanged == "ReceiptName")
                {
                    aReceipeMenuItemButton.ReceiptName = mixText;
                }
                else
                {
                    aReceipeMenuItemButton.ShortDescrip = mixText;
                }


                aReceipeMenuItemButton.OptionList = OptionId;


            }
            if (aReceipeMenuItemButton == null || aReceipeMenuItemButton.RecipeMenuItemId <= 0)
            {
                MessageBox.Show("Item not found");

                return aReceipeMenuItemButton;
            }

            if (multipleItem.IsCheckedQuater(pannelTopBar) > 0)
            {
                CartItem cartItem = new CartItem();

                aReceipeMenuItemButton.ItemName = aReceipeMenuItemButton.ReceiptName;
                if (aGeneralInformation.TableId == 0)
                {
                    aReceipeMenuItemButton.InPrice = aReceipeMenuItemButton.OutPrice;
                }
                cartItem.ReceipeMenuItemButton = aReceipeMenuItemButton;

                int limit = multipleItem.AddToMultipleCart(cartItem);
                if (limit > 0)
                {
                    AddToMainCartMultipleItem();
                    aReceipeMenuItemButton.Status = "Finish";

                }

                return aReceipeMenuItemButton;
            }
            AddToCard(aReceipeMenuItemButton, Convert.ToString(aReceipeSubCategoryButton.Tag), null);

            return aReceipeMenuItemButton;
        }

        private void btnOptionAdd_Click(object sender, EventArgs e)
        {
            if (GlobalSetting.DbType == "SQLITE")
            {
                GatewayConnection connection = new GatewayConnection();
                connection.Connection.Open();

                using (SQLiteCommand command = connection.Connection.CreateCommand())
                {
                    command.CommandText = "vacuum;";
                    int count = command.ExecuteNonQuery();
                }
                connection.Connection.Close();
            }
            else
            {
                MySqlGatewayConnection connection = new MySqlGatewayConnection();
                connection.Connection.Open();

                using (MySqlCommand command = connection.Connection.CreateCommand())
                {
                    command.CommandText = "FLUSH QUERY CACHE;";
                    int count1 = command.ExecuteNonQuery();


                    command.CommandText = "RESET QUERY CACHE;";
                    int count2 = command.ExecuteNonQuery();

                }
                connection.Connection.Close();
            }





        }

        private void gridControlAddTocard_Click(object sender, EventArgs e)
        {

                }



        private void onlineOrderButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
           new OnlineOrder().GetOnlineOrder();
            orderLoadStatus = true;

            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            player.Stop();
            OnlineOrderForm.Id = 0;
            OnlineOrderForm aOnlineOrderForm = new OnlineOrderForm();
            aOnlineOrderForm.ShowDialog();

            if (OnlineOrderForm.Id > 0)
            {
                LoadAllSaveOrder((int)OnlineOrderForm.Id, "reorder");
            }


            List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderForOnline(1, "pending");

            if (restaurantOrder.Count > 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    onlineOrderButtonNew.Text = restaurantOrder.Count + " " + "Order";

                });
                PlayFile();
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    onlineOrderButtonNew.Text = "Online Order";
                });
                player.Stop();
            }
        }

        private RestaurantUsers FindRestaurantUser(string phoneNumber)
        {

            CustomerBLL aCustomerBll = new CustomerBLL();
            Int64 n;
            string phnTrack;
            if (!Int64.TryParse(phoneNumber, out n))
            {
                phnTrack = "postcode";
            }
            else if (phoneNumber.Count() >= 2 && phoneNumber[1] == '7')
            {
                phnTrack = "mobilephone";
            }
            else
            {
                phnTrack = "homephone";
            }


            return aCustomerBll.GetRestaurantCustomerByHomePhone(phoneNumber, phnTrack);

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


        private void customerLoadButtonNew_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();



            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            isCustomerTextChanged = false;
            customerTextBox.Text = customerPhnLabel.Text;
            callingPannel.Visible = false;

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

        private void btnReservationNew_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();

            resPlayer.Stop();

            if (!this.IsHandleCreated)
                this.CreateControl();

            this.Invoke((MethodInvoker)delegate
            {
                btnReservation.Visible = false;
                btnReservation.Width = 0;
                customerDetailsLabelNew.Location = new Point(btnReservation.Location.X, customerDetailsLabel.Location.Y);
            });
            LoadSettingsForm("reservation");
        }

        private void customerEditButtonNew_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();



            isCustomerTextChanged = false;

            string phoneNumber = customerTextBox.Text.Trim();
            if (phoneNumber.Length <= 0)
            {
                phoneNumber = customerDetailsLabelNew.Text;
            }

            CustomerBLL aCustomerBll = new CustomerBLL();
            RestaurantUsers aRestaurantUser = FindRestaurantUser(phoneNumber);
            if (aRestaurantUser != null && aRestaurantUser.Id <= 0 && aGeneralInformation.CustomerId > 0)
            {
                aRestaurantUser = aCustomerBll.GetResturantCustomerByCustomerId(aGeneralInformation.CustomerId);
            }
            EditCustomer(aRestaurantUser);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentTimeLabelNew.Text = DateTime.Now.ToString("hh:mm:ss tt");
           
        }
        public void AppendTextBoxReservation(string value)
        {
            if (this.IsDisposed)
            {
                return;
            }
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBoxReservation), new object[] { value + "\n Res" });
                this.btnReservationNew.Text = value + "\n Res";
                return;
            }

            if (value.Contains("0"))
            {
                this.btnReservationNew.Visible = false;

            }
            else
            {
                this.btnReservationNew.Visible = true;
            }
            this.btnReservationNew.Text = value + "\n Res";
            this.btnReservationNew.BringToFront();
            this.btnReservationNew.Width = 53;
            customerDetailsLabelNew.Location = new Point(btnReservationNew.Location.X,
                customerDetailsLabelNew.Location.Y);
            this.btnReservationNew.Text = value;
        }

        public void Virtualkeyboard()
        {
            try
            {
                aOthersMethod.NumberPadClose();
                aOthersMethod.KeyBoardClose();

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

        public void VirtualNumberPad(int x, int y)
        {
            try
            {
                aOthersMethod.KeyBoardClose();
                if (!Application.OpenForms.OfType<NumberForm>().Any() && urls.Keyboard > 0)
                {
                    //  int x = rightPanel.Location.X - 195;
                    Point aPoint = new Point(x, y);
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
        private void gridControlAddTocard_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                BeginInvoke(new Action(delegate
                {
                    VirtualKeyBoard(0, Screen.Bounds.Bottom - 280);
                    gridViewAddtocard.SelectRow(gridViewAddtocard.FocusedRowHandle);

                    var IsGroupRow = gridViewAddtocard.GetSelectedRows();
                    if (gridViewAddtocard.IsGroupRow(IsGroupRow[0]))
                    {
                        return;
                    }
                    var Type = gridViewAddtocard.GetFocusedRowCellValue("Group").ToString();
                    if (Type == "Package")
                    {
                        //PackageItemFormLoadNew.aPackageItemList.Clear();
                        //PackageItemFormLoadNew.aRecipeOptionMdList.Clear();
                        int packageId = Convert.ToInt32(gridViewAddtocard.GetFocusedRowCellValue("Cat"));
                        RecipePackageButton aRecipePackageButtons = new RestaurantMenuBLL().GetPackageByMenuType(0).FirstOrDefault(a => a.PackageId == packageId);
                        CartItem tempRecipePackageButton = new CartItem();
                        tempRecipePackageButton.ItemSize = TileItemSize.Wide;
                        tempRecipePackageButton.RecipePackageButton = aRecipePackageButtons;
                        tempRecipePackageButton.Text = aRecipePackageButtons.PackageName;
                        tempRecipePackageButton.TextAlignment = TileItemContentAlignment.MiddleCenter;
                        tempRecipePackageButton.Appearance.BackColor = Color.CadetBlue;
                        tempRecipePackageButton.Appearance.BorderColor = Color.Transparent;
                        tempRecipePackageButton.Appearance.Font = aRecipePackageButtons.Font;
                        tempRecipePackageButton.AppearanceItem.Selected.Font = aRecipePackageButtons.Font;
                        aRecipePackageButtons.PackageUpdateOrNot = "Update";
                        var mainPackageButton = aRecipePackageButtons;

                        if (aRecipePackageButtons.CustomPackage == 0)
                        {
                            return;

                        }


                        List<PackageItem> packageitem = (List<PackageItem>)gridViewAddtocard.GetFocusedRowCellValue("Package");

                        mainPackageButton.UpdatePackageList = packageitem;
                        //flowLayoutPanel1.Visible = true;

                        ReceipeMenuItemButton packageButton = new ReceipeMenuItemButton();

                        packageButton.Margin = new Padding(0);
                        packageButton.Padding = new Padding(0);
                        packageButton.AutoSize = true;
                        packageButton.AutoSizeMode = AutoSizeMode.GrowOnly;
                        packageButton.Height = pannelTopBar.Height - 2;
                        packageButton.Font = tempRecipePackageButton.Appearance.Font;
                        packageButton.BackColor = Color.Blue;
                        packageButton.ForeColor = Color.White;
                        packageButton.FlatStyle = FlatStyle.Flat;
                        packageButton.Text = "Package";
                        pannelTopBar.Visible = true;

                        flowLayoutPanel1.Controls.Clear();
                        packageButton.Click += (o, args) =>
                        {
                            LoadMenuType();

                        };
                        flowLayoutPanel1.Controls.Add(packageButton);

                        LoadPackageItemCategory(mainPackageButton, tempRecipePackageButton);

                        return;
                    }


                    int ReceipTypeId = (int)gridViewAddtocard.GetFocusedRowCellValue("RecepiMenuId");
                    var name = gridViewAddtocard.GetFocusedRowCellValue("OptionId");
                    if (name == DBNull.Value)
                    {

                        // For Single Item Update Condition
                        return;
                    }
                    name = (List<OptionJson>)gridViewAddtocard.GetFocusedRowCellValue("OptionId");

                    UpdateCartOtionItem(ReceipTypeId, "Type", (List<OptionJson>)name);




                }));
            }
            catch (Exception)
            {


            }

        }

        private void serviceChargeLabel_Click(object sender, EventArgs e)
        {
            ServiceChargeForm.OrderDiscount = new OrderDiscount();
            ServiceChargeForm aServiceChargeForm = new ServiceChargeForm();
            aServiceChargeForm.ShowDialog();
            OrderDiscount aOrderDiscount = ServiceChargeForm.OrderDiscount;
            if (aOrderDiscount.Status == "cancel")
                return;
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
                } aGeneralInformation.ServiceCharge = aOrderDiscount.Amount;
            }
            else if (aOrderDiscount.DiscountType == "Persent")
            {

                aGeneralInformation.ServiceCharge = (totalAmount * aOrderDiscount.Amount) / 100;
                aGeneralInformation.ServiceChargePercent = aOrderDiscount.Amount;

            }
            AddServiceChargeIntoLabel();
        }

        private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
        }
        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            rowPackageItemFocus = true;
        }

        private void gridView1_GotFocus(object sender, EventArgs e)
        {
            rowPackageItemFocus = true;
        }

        private void gridViewAddtocard_GotFocus(object sender, EventArgs e)
        {
            rowPackageItemFocus = false;
        }

        private void gridControlAddTocard_DockChanged(object sender, EventArgs e)
        {

        }

        private void gridViewAddtocard_RowUpdated(object sender, RowObjectEventArgs e)
        {

        }

        private void tillOpenButton_Click(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            if (GlobalSetting.SettingInformation.till == "Enable")
            {
                OpenCashDrawer();

            }

           
        }

        private void commentTextBox_Click(object sender, EventArgs e)
        {
            commentTextBox.Text = string.Empty;
        }

        public void VirtualKeyBoard(int x, int y)
        {
            try
            {
                aOthersMethod.KeyBoardClose();
                if (!Application.OpenForms.OfType<NumberPad>().Any() && urls.Keyboard > 0)
                {

                    Point aPoint = new Point(x, y);
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
        private void commentTextBox_Enter(object sender, EventArgs e)
        {
            int x = 0;
            int y = Screen.Bounds.Bottom - 280;
            VirtualKeyBoard(x, y);
        }

        private void commentTextBox_Leave(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
            if (commentTextBox.Text == string.Empty)
            {
                commentTextBox.Text = "Comment";
            }
        }

        private void customerTextBox_Enter(object sender, EventArgs e)
        {
            VirtualKeyBoard(0, Screen.Bounds.Bottom - 280);
        }



        private void gridControlAddTocard_Leave(object sender, EventArgs e)
        {
            aOthersMethod.KeyBoardClose();
        }

        private void dockManager1_Sizing(object sender, SizingEventArgs e)
        {
            e.Panel.MinimumSize = new Size(312, 500);
            e.Cancel = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(10000);

            //var Time = DateTime.Now.TimeOfDay.Minutes;
            //MessageBox.Show(Time.ToString());

            //onlineOrderSync();

            //OnlineOrderProcess();
            //OnlineReservation();

            aOthersMethod.KeyBoardClose();
            aOthersMethod.NumberPadClose();
            new OnlineOrder().GetOnlineOrder();
            orderLoadStatus = true;

            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            //player.Stop();
            //OnlineOrderForm.Id = 0;
            //OnlineOrderForm aOnlineOrderForm = new OnlineOrderForm();
            //aOnlineOrderForm.ShowDialog();

            //if (OnlineOrderForm.Id > 0)
            //{
            //    LoadAllSaveOrder((int)OnlineOrderForm.Id, "reorder");
            //}


            List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderForOnline(1, "pending");

            if (restaurantOrder.Count > 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    onlineOrderButtonNew.Text = restaurantOrder.Count + " " + "Order";
                }); PlayFile();
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    onlineOrderButtonNew.Text = "Online Order";
                });
                player.Stop();
            }
        }



        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void btnCartShowHidden_Click(object sender, EventArgs e)
        {
            InstantCardShowHidden();
        }

        private void customTotalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
      (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tileControl1_Click(object sender, EventArgs e)
        {
        }

        private void windowsUIButtonPanel1_Click(object sender, EventArgs e)
        {

        }

        private void MainFormView_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GlobalSetting.SettingInformation.onlineConnect == "Active")
            {
                if (resTimer != null)
                {
                    resTimer.Elapsed -= timerReservation_Tick;
                    resPlayer.Stop();
                }


            }


        }

        private void MainFormView_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void MainFormView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GlobalSetting.SettingInformation.onlineConnect == "Active")
            {
                if (resTimer != null)
                {
                    resTimer.Elapsed -= timerReservation_Tick;
                }

            }

        }

        public void MultipleButton()
        {

            dockPanel2.Visibility = DockVisibility.Visible;
            multipleItem.LoadMulipleItemLoad(gridControlMultiple);

            if (half.Visible)
            {
                quater.Visible = true;
                half.Visible = false;

            }
            else if (quater.Visible)
            {
                quater.Visible = false;
                half.Visible = true;
            }
        }
        private void btnMultiple_Click(object sender, EventArgs e)
        {

            MultipleButton();

        }

        private void half_Click(object sender, EventArgs e)
        {
            MultipleButton();
        }

        private void multiItembtn_Click(object sender, EventArgs e)
        {

            multiItembtn.Visible = false;
            quater.Visible = true;
            MultipleButton();
        }

        private void AddToMainCartMultipleItem()
        {
            int i = 0;
            try
            {


                ReceipeTypeButton aReceipeSubCategoryButton = (ReceipeTypeButton)flowLayoutPanel1.Controls[1];
                string name = "<h4 style='margin:0px; text-align:left'>" + aReceipeSubCategoryButton.Text + "  Split" + "</h4>";
                foreach (DataRow row in multipleItem.multipleItemLoad.Rows)
                {
                    i++;
                    name += ":" + row["MultipleItemName"] + "<hr/>";
                }
                ReceipeMenuItemButton menuItemButton = new ReceipeMenuItemButton();
                menuItemButton.ReceiptName = "<div>" + name + "</div>";
                DataRow[] dr = multipleItem.multipleItemLoad.Select("[Price] = MAX([Price])");
                menuItemButton.RecipeMenuItemId = Convert.ToInt32(dr[0]["RecepiMenuId"]);
                menuItemButton.CategoryId = Convert.ToInt32(dr[0]["RecepiMenuId"]);
                menuItemButton.InPrice = Convert.ToDouble(dr[0]["Price"]);
                menuItemButton.OutPrice = Convert.ToDouble(dr[0]["Price"]);
                menuItemButton.ItemType = "MultipleItem";
                menuItemButton.GetMultiItemList = multipleItem.GetAllMultipleItem(multipleItem.multipleItemLoad);
                menuItemButton.SortOrder = 0;
                AddToCard(menuItemButton, aReceipeSubCategoryButton.Text + "  Split", null);
                gridControlMultiple.DataSource = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); this.Activate();
            }

        }
        private void btnAddToMultiple_Click(object sender, EventArgs e)
        {

            AddToMainCartMultipleItem();
            MultiInformationClear();



        }



        internal void ShowOnlineOrderBtn()
        {
            throw new NotImplementedException();
        }

        private void currentTimeLabelNew_Click(object sender, EventArgs e)
        {

        }
    }
}
