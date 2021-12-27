using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.CommonMethod;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class OnlineOrderDetailsForm : Form
    {

        RestaurantOrder aRestaurantOrder = new RestaurantOrder();
        RestaurantInformation aRestaurantInformation = new RestaurantInformation();
        private CommonOrderDetials orderDetials;
        OthersMethod aOthersMethod = new OthersMethod();
        RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
        public OnlineOrderDetailsForm(RestaurantOrder saveOrder, bool b)
        {
            InitializeComponent();
            try
            {

                aRestaurantOrder = saveOrder;
                aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                orderDetials = new CommonOrderDetials(aRestaurantOrder);

                if(aRestaurantOrder.OrderTable > 0)
                {
                    buttonAcceptWithDefaultTime.Text = "Accept Table Order";
                    btnReject.Visible = false;
                }
                else
                {
                    if (aOthersMethod.IsTimeFormatValid(aRestaurantOrder.DeliveryTime.ToString()) != "00:00")
                    {

                        buttonAcceptWithDefaultTime.Text = "ACCEPT AND PRINT (" + Convert.ToDateTime(aRestaurantOrder.DeliveryTime).ToString("hh:mmtt").ToLower() + ")";
                    }
                    else
                    {
                        buttonAcceptWithDefaultTime.Text = "ACCEPT AND PRINT  (ASAP)";

                    }
                }
                
                
                txtOtherTime.EditValue = DateTime.Now;
                txtOtherTime.Text = DateTime.Now.ToString();
                var Controls = pannelAccept.Controls.OfType<Button>().OrderByDescending(a => a.TabIndex).ToList();
                DateTime time = DateTime.Now; int inteval = 0;
                foreach (Button control in Controls)
                {

                    if (aRestaurantOrder.OrderTable > 0)
                    {
                        control.Visible = false;
                        continue;
                    }

                    int TotalAcceptMinute = DynamicTime(aRestaurantInformation, saveOrder, inteval);
                    DateTime afterTime = time.AddMinutes(TotalAcceptMinute);
                    string TotalTime = string.Format("{0:hh:mm tt}", afterTime);
                    string newTime = string.Format("{0:hh:mm tt}", afterTime);
                    var result = TimeSpan.FromMinutes(TotalAcceptMinute);

                    string times = "Ready in " + result.ToString(@"hh\:mm") + " minutes \n by " + TotalTime.ToLower();
                    control.Text = times;
                    control.FlatAppearance.BorderColor = Color.Black;
                    control.FlatAppearance.BorderSize = 1;
                    control.Click += async (sender, args) =>
                    {
                        splashScreenManager1.ShowWaitForm();
                        await orderDetials.AcceptOrderAsync(aRestaurantInformation, TotalTime);

                        Print(newTime);
                        splashScreenManager1.CloseWaitForm();
                        this.Close();
                    };
                    inteval = inteval + 10;
                }
                
                
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
          
        }

     
        public int DynamicTime(RestaurantInformation resInformation, RestaurantOrder order, int interval)
        {
            splashScreenManager1.ShowWaitForm();

            if (order.OrderType == "CLT")
            {
                string collectionTime = resInformation.CollectionTime.Replace("Mins", string.Empty);

                int ctlTime = (Convert.ToInt16(collectionTime) + interval);
                splashScreenManager1.CloseWaitForm();
                return ctlTime;
            }
            else
            {
                string deliveryTime = resInformation.DeliveryTime.ToString().Replace("Mins", string.Empty);
                //string deliveryTime = "45";

                int ctlTime = (Convert.ToInt16(deliveryTime) + interval);
                splashScreenManager1.CloseWaitForm();

                return ctlTime;
            }
           
           
        }
        
        private void OnlineOrderDetailsForm_Load(object sender, EventArgs e)
        {
            try
            {
                txtOtherTime.EditValue = DateTime.Now;
                txtOtherTime.Text = DateTime.Now.ToString();
                aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                string print = orderDetials.LoadAllOrder();

                //labelPrintText.Text = print;

                if (Properties.Settings.Default.enableWebPrint)
                {
                    webBrowser1.Visible = true;
                    labelPrintText.Visible = false;
                    webBrowser1.DocumentText = print; 
                    // webBrowser1.Navigating += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
                }
                else
                {
                    webBrowser1.Visible = false;
                    labelPrintText.Visible = true;
                    labelPrintText.Text = print;
                }

            }catch (Exception ex)
            {
                string message = ex.GetBaseException().ToString();
                MessageBox.Show(message);

            }


        }

        private void Print(string acceptTime="")
        {
            List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
            PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
            PrinterSetups = aPrinterSetupBll.GetTotalPrinterList();
            try
            {

                var printer = PrinterSetups.FirstOrDefault(a => a.PrintStyle == "Receipt");

                int printCopy = 1;

                if (printer.PrintStyle == "Receipt")
                {
                    if (aRestaurantOrder.OrderType.ToUpper() == "IN")
                    {
                        printCopy = aRestaurantInformation.DineInPrintCopy;
                    }
                    else if (aRestaurantOrder.OrderType.ToUpper() == "DEL")
                    {
                        printCopy = aRestaurantInformation.DelPrintCopy;
                    }
                    else
                    {
                        printCopy = aRestaurantInformation.PrintCopy;
                    }

                }

                if (acceptTime != "") {
                    if (acceptTime.Contains("AM"))
                    {
                        acceptTime = acceptTime.Replace(" AM", ":00 AM");
                    }

                    if (acceptTime.Contains("PM"))
                    {
                        acceptTime = acceptTime.Replace(" PM", ":00 PM");
                    }

                    aRestaurantOrder.DeliveryTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + acceptTime);
                    RestaurantOrderBLL bll = new RestaurantOrderBLL();
                    bll.UpdateOrderDelTime(aRestaurantOrder);

                }
                aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                string printStr = orderDetials.LoadAllOrder();

                if (Properties.Settings.Default.enableWebPrint)
                {
                    PrintToPrinter print = new PrintToPrinter();
                    print.PrintReceipt(printStr, printer, printCopy);
                }
                else
                {
                    PrintMethods tempPrintMethods = new PrintMethods(false);//, false, printer.printerMargin, isBillPrint);
                    tempPrintMethods.USBPrint(printStr, printer.PrinterName, printCopy);

                    var billPrinter = PrinterSetups.FirstOrDefault(a => a.PrintStyle == "Bill");
                    if(billPrinter != null)
                    {
                        bool isBillPrint = billPrinter.PrintStyle == "Bill" ? true : false;
                        PrintMethods tempPrintMethods_ = new PrintMethods(false, false, billPrinter.printerMargin, isBillPrint);
                        tempPrintMethods_.USBPrint(printStr, billPrinter.PrinterName, billPrinter.PrintCopy);

                    }

                }
            }
            catch(Exception ex){
                MessageBox.Show("Printer not found.Please add recipt printer.");
            }
            try
            {
                PrinterSetups = PrinterSetups.Where(a => a.PrintStyle == "Kitchen").ToList();
                foreach (PrinterSetup kprinter in PrinterSetups)
                {
                   // PrintMethods kitPrintMethods = new PrintMethods(true, false, kprinter.printerMargin);
                  orderDetials.GenerateKitchenCopyString(aRestaurantOrder.Id, kprinter);
                  //  kitPrintMethods.USBPrint(ppp, kprinter.PrinterName, kprinter.PrintCopy);

                }
            }
            catch(Exception ex){
                MessageBox.Show("Printer not found.Please add kitchen printer.");
            }
        }


        private async void btnAccept_ClickAsync(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            await orderDetials.AcceptOrderAsync(aRestaurantInformation, txtOtherTime.Text);
            Print(txtOtherTime.Text);
            splashScreenManager1.CloseWaitForm();
            this.Close();
           

        }

        private void txtOtherTime_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtOtherTime_EditValueChanged(object sender, EventArgs e)
        {
            btnAccept.Visible = false; 
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            orderDetials.RejectOrderAsync(aRestaurantInformation);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void buttonAcceptWithDefaultTime_ClickAsync(object sender, EventArgs e)
        {                                                                         
            splashScreenManager1.ShowWaitForm();
            string timeSlot = aRestaurantOrder.DeliveryTime.ToString("hh:mm tt");

            if (aRestaurantOrder.DeliveryTime.ToString() == "01/01/0001 12:00:00 AM" || aRestaurantOrder.DeliveryTime.ToString("hh:mm").Contains("12:00"))
            {
                  timeSlot = "";
            }

            await orderDetials.AcceptOrderAsync(aRestaurantInformation, timeSlot);

            //if IN order then show the order
            if(aRestaurantOrder.OrderTable > 0)
            {

            }
            //close the form
            Print(timeSlot);
            splashScreenManager1.CloseWaitForm();
            this.Close();
           
        }                                                                                
    }
}
