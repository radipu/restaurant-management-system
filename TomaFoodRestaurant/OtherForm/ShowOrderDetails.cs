using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.CommonMethod;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class ShowOrderDetails : Form
    {
        RestaurantOrder aRestaurantOrder = new RestaurantOrder();
        List<RecipeOptionMD> aRecipeOptionMdList = new List<RecipeOptionMD>();
        public static List<OrderItemDetailsMD> aOrderItemDetailsMDList = new List<OrderItemDetailsMD>();
        public static List<RecipePackageMD> aRecipePackageMdList = new List<RecipePackageMD>();
        public static List<PackageItem> aPackageItemMdList = new List<PackageItem>();
        public static List<RecipeMultipleMD> aRecipeMultipleMdList = new List<RecipeMultipleMD>();
        public static List<MultipleItemMD> aMultipleItemMdList = new List<MultipleItemMD>();

        GeneralInformation aGeneralInformation = new GeneralInformation();
        RestaurantInformation aRestaurantInformation = new RestaurantInformation();
        public static int OrderId = 0;
        List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
        PrintCopySetup aPrintCopySetup = new PrintCopySetup(); 
        public ShowOrderDetails(RestaurantOrder saveOrder)
        {
            InitializeComponent();
            aRestaurantOrder = saveOrder; 
            if (aRestaurantOrder.PaymentModule != "" && Properties.Settings.Default.stripeEnable && aRestaurantOrder.PaymentModule.ToLower() == GlobalVars.stripePaymentModule.ToLower())
            {
                buttonRefund.Visible = true;
            }
            else
            {
                buttonRefund.Visible = false;
            }
            if (aRestaurantOrder.Status.ToLower() == "pending" && aRestaurantOrder.OrderType.ToLower() =="clt")
            {
                buttonPayment.Visible = true;
            }
            else
            {
                buttonPayment.Visible = false;
            }
            if (aRestaurantOrder.OnlineOrder > 0 && aRestaurantOrder.Status.ToLower() != "pending")
            {
                buttonChangeOrder.Visible = false;
            }
            else
            {
                buttonChangeOrder.Visible = true;
            }
           // buttonRefund.Visible = false;
        }


        public ShowOrderDetails(RestaurantOrder saveOrder, bool status)
        {
            InitializeComponent();
            aRestaurantOrder = saveOrder;  
            if (!status)
            {
                buttonChangeOrder.Visible = false;
            }
            else
            {                                                                                   
                buttonChangeOrder.Visible = true;
            }
            if (aRestaurantOrder.PaymentModule != "" && aRestaurantOrder.PaymentModule.ToLower() == GlobalVars.stripePaymentModule.ToLower())
            {
                buttonRefund.Visible = true;
            }
            else {                
                buttonRefund.Visible = false;
            }

            if (aRestaurantOrder.Status.ToLower() == "pending" && aRestaurantOrder.OrderType.ToLower() == "clt")
            {
                buttonPayment.Visible = true;
            }
            else
            {
                buttonPayment.Visible = false;
            }
            if (aRestaurantOrder.OnlineOrder > 0 && aRestaurantOrder.Status.ToLower() != "pending")
            {
                buttonChangeOrder.Visible = false;
            }else
            {
                buttonChangeOrder.Visible = true;
            }

        }


        private void ShowOrderDetails_Load(object sender, EventArgs e)
        {
            try
            {
                RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
                aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
                CommonOrderDetials orderDetials = new CommonOrderDetials(aRestaurantOrder);
                string order = orderDetials.LoadAllOrder();
                if (Properties.Settings.Default.enableWebPrint)
                {
                    webBrowser1.Visible = true;
                    labelPrintText.Visible = false;
                    webBrowser1.DocumentText = order;
                   // webBrowser1.Navigating += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
                }
                else
                {
                    webBrowser1.Visible = false;
                    labelPrintText.Visible = true;
                    labelPrintText.Text = order;
                }
                LoadGeneralInformationIntoControl();

            }
            catch (Exception ex)
            {

                string exception = ex.GetBaseException().ToString();
            }

        }



        private void LoadGeneralInformationIntoControl()
        {

            orderTimeLabel.Text = aRestaurantOrder.OrderTime.ToString();
          
            if (aRestaurantOrder.OrderTable > 0)
            {
                panelDelivery.Visible = false;
            }

            if (aRestaurantOrder.DeliveryTime != null)
            {
                deliveryTimeLabel.Text = aRestaurantOrder.DeliveryTime.ToString("hh:mmtt");
                if (aRestaurantOrder.DeliveryTime.ToString() == "01-Jan-01 12:00:00 AM")
                {
                    deliveryTimeLabel.Text = "ASAP";
                }

            }
            if (aRestaurantOrder.OnlineOrder > 0)
            {
                onlineOrderTimeLabel.Text = "Yes";
            }
            else
            {
                onlineOrderTimeLabel.Text = "No";
            }

            if (aRestaurantOrder.OrderType == "Collect" || aRestaurantOrder.OrderType == "CLT" || aRestaurantOrder.OrderType == "WAIT")
            {
                collectionRadioButton.Checked = true;
                lblOrderType.Text = "Collection Time :";

            }
            else if (aRestaurantOrder.OrderType == "DEL")
            {
                deliveryRadioButton.Checked = true;
                lblOrderType.Text = "Delivery Time :";

            }
            else
            {
                dineInRadioButton.Checked = true;
            }

            if (aRestaurantOrder.CashAmount > 0 && aRestaurantOrder.CardAmount > 0)
            {
                splitRadioButton.Checked = true;
            }
            else if (aRestaurantOrder.CashAmount > 0)
            {
                cashRadioButton.Checked = true;
            }
            else if (aRestaurantOrder.CardAmount > 0)
            {
                cardrRadioButton.Checked = true;
            }


            if (aRestaurantOrder.Status.ToLower() == "paid")
            {
                paidRadioButton.Checked = true;
            }
            else if (aRestaurantOrder.Status.ToLower() == "pending")
            {
                pendingRadioButton.Checked = true;
            }
            else
            {
                cancelledRadioButton.Checked = true;
            }

            if (aRestaurantOrder.Receipt >0)
            {
                 pictureBox1.Image = global::TomaFoodRestaurant.Properties.Resources.r;
          
            }
            else{
                 pictureBox1.Image = global::TomaFoodRestaurant.Properties.Resources.nonrecord;
          
            }

        }
         
        private void printButton_Click(object sender, EventArgs e)
        {
            List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
            PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
            PrinterSetups = aPrinterSetupBll.GetTotalPrinterList();
            var printer = PrinterSetups.FirstOrDefault(a => a.PrintStyle == "Receipt");
            if (printer != null)
            {

                if (Properties.Settings.Default.enableWebPrint)
                {
                        string printStr = webBrowser1.DocumentText.ToString();
                        PrintToPrinter print = new PrintToPrinter();
                        print.PrintReceipt(printStr, printer, 1); 
                }else{
                        string printStr = labelPrintText.Text;
                        PrintMethods tempPrintMethods = new PrintMethods(false);
                        tempPrintMethods.USBPrint(printStr, printer.PrinterName, printer.PrintCopy);
                }
            }

 
            this.Close();
        }

        private void buttonChangeOrder_Click(object sender, EventArgs e)
        {
            if (aRestaurantOrder.OrderType == "IN" && aRestaurantOrder.OrderStatus.ToLower() != "finished")
            {
                MessageBox.Show("You are not eligible for update before finalize.");
            }
            else
            {
                if (dineInRadioButton.Checked)
                {
                    aRestaurantOrder.OrderType = "IN";
                }
                else if (collectionRadioButton.Checked)
                {
                    aRestaurantOrder.OrderType = "CLT";
                }
                else
                {
                    aRestaurantOrder.OrderType = "DEL";
                }

                if (pendingRadioButton.Checked)
                {
                    aRestaurantOrder.Status = "pending";
                }
                else if (paidRadioButton.Checked)
                {
                    aRestaurantOrder.Status = "paid";
                }
                else
                {
                    aRestaurantOrder.Status = "Cancelled";
                }
                if (cashRadioButton.Checked)
                {
                    aRestaurantOrder.PaymentMethod = "cash";
                    aRestaurantOrder.CardAmount = 0.0;
                    aRestaurantOrder.CashAmount = aRestaurantOrder.TotalCost;
                }
                else if (cardrRadioButton.Checked)
                {
                    aRestaurantOrder.PaymentMethod = "card";
                    aRestaurantOrder.CardAmount = aRestaurantOrder.TotalCost;
                    aRestaurantOrder.CashAmount = 0.0;
                }
                else if (splitRadioButton.Checked)
                {
                    aRestaurantOrder.PaymentMethod = "split";
                }

                RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                try
                {
                    if (OthersMethod.CheckForInternetConnection())
                    {
                        OrderSyncroniseBLL aOrderSyncroniseBll = new OrderSyncroniseBLL();
                        int updateId = aOrderSyncroniseBll.SingleOrderSyncronise(aRestaurantOrder);
                        aRestaurantOrder.IsSync = 1;
                    }
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
                aVariousMethod.UpdateRestaurantOrder(aRestaurantOrder);
                OrderId = 0;
            }
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
          
            //if (aRestaurantOrder.Receipt <= 0)
            //{
            //    aRestaurantOrder.Receipt = 1;
            //    pictureBox1.Image = global::TomaFoodRestaurant.Properties.Resources.r;
            //}
            //else
            //{
            //    aRestaurantOrder.Receipt = 0;
            //    pictureBox1.Image = global::TomaFoodRestaurant.Properties.Resources.NR;             
            //}  
            //try
            //{
            //    //if (OthersMethod.CheckForInternetConnection())
            //    //{
            //    //    OrderSyncroniseBLL aOrderSyncroniseBll = new OrderSyncroniseBLL();
            //    //    int updateId = aOrderSyncroniseBll.SingleOrderSyncronise(aRestaurantOrder);
            //    //    aRestaurantOrder.IsSync = 1;
            //    //    //if (updateId > 0)
            //    //    //{
            //    //    //    MessageBox.Show("Sucessfully Changed.");
            //    //    //}
            //    //}
            //}
            //catch (Exception exception)
            //{
            //    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
            //    aErrorReportBll.SendErrorReport(exception.ToString());
            //}
            //RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            //aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);
        }

        private void buttonRefund_Click(object sender, EventArgs e)
        {
            int orderid = aRestaurantOrder.Id;
            if (aRestaurantOrder.OnlineOrder >0)
            {
                orderid = (int) aRestaurantOrder.OnlineOrderId;
            }
            Refund refundFrom = new Refund(aRestaurantOrder.TotalCost,orderid);
            refundFrom.ShowDialog();
            if (refundFrom.paymentStatus)
            {
                RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                try
                {
                    aRestaurantOrder.Status = "refunded";
                    //if (OthersMethod.CheckForInternetConnection())
                    //{
                    //    OrderSyncroniseBLL aOrderSyncroniseBll = new OrderSyncroniseBLL();

                    //    int updateId = aOrderSyncroniseBll.SingleOrderSyncronise(aRestaurantOrder);
                    //    aRestaurantOrder.IsSync = 1;

                    //}
                    aRestaurantOrder.IsSync = 1;
                    aVariousMethod.UpdateRestaurantOrder(aRestaurantOrder);
                    OrderId = 0;
                }
                catch (Exception exception)
                {
                    ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                    aErrorReportBll.SendErrorReport(exception.ToString());
                }
              

            }
            this.Close();
        }

        private void buttonPayment_Click(object sender, EventArgs e)
        {
            RestaurantOrder tempOrder = new RestaurantOrderBLL().GetRestaurantOrderByOrderId(aRestaurantOrder.Id);

            //ShowOrderDetails details = new ShowOrderDetails(tempOrder);
            //details.ShowDialog();
            string totalValue = Convert.ToString(GlobalVars.numberRound(aRestaurantOrder.TotalCost));
            ConfirmOrderForm.PaymentDetails = new PaymentDetails();
            ConfirmOrderForm aConformOrderForm = new ConfirmOrderForm(tempOrder);
            aConformOrderForm.ShowDialog();

            PaymentDetails aPaymentDetails = ConfirmOrderForm.PaymentDetails;

            if (aPaymentDetails.Status == "Ok")
            {
                tempOrder.Id = aRestaurantOrder.Id;
                tempOrder.Status = "Paid";
                tempOrder.PaymentMethod = aPaymentDetails.PaymentMethod;
                tempOrder.CardAmount = aPaymentDetails.CardAmount;
                tempOrder.CashAmount = aPaymentDetails.CashAmount;
                RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                bool res = aVariousMethod.UpdateRestaurantOrder(tempOrder);

                OrderSyncroniseBLL aOrderSyncroniseBll = new OrderSyncroniseBLL();
                int updateId = aOrderSyncroniseBll.SingleOrderSyncronise(tempOrder);

                if (aPaymentDetails.IsPrint)
                {

                    List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
                    PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                    PrinterSetups = aPrinterSetupBll.GetTotalPrinterList();
                    var printer = PrinterSetups.FirstOrDefault(a => a.PrintStyle == "Receipt");
                    if (printer != null)
                    {

                        if (Properties.Settings.Default.enableWebPrint)
                        {
                            string printStr = webBrowser1.DocumentText.ToString();
                            PrintToPrinter print = new PrintToPrinter();
                            print.PrintReceipt(printStr, printer, 1);
                        }
                        else
                        {
                            string printStr = labelPrintText.Text;
                            PrintMethods tempPrintMethods = new PrintMethods(false);
                            tempPrintMethods.USBPrint(printStr, printer.PrinterName, printer.PrintCopy);
                        }


                    }
                }

                this.Close();
            }

        }

        private void btnDuplicateKitchenPrint_Click(object sender, EventArgs e)
        {

            try
            { 

                List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
                PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                PrinterSetups = aPrinterSetupBll.GetTotalPrinterList();
                PrinterSetup kPrint = PrinterSetups.Where(a => a.PrintStyle == "Kitchen").FirstOrDefault();

                CommonOrderDetials orderDetials = new CommonOrderDetials(aRestaurantOrder);
                string printStr = orderDetials.LoadAllOrder();
           
                orderDetials.GenerateKitchenCopyString(aRestaurantOrder.Id, kPrint, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Printer not found.Please add kitchen printer.");
            }

            this.Close();

        }
    }
}

