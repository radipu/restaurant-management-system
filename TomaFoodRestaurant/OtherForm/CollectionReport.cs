using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;
using TomaFoodRestaurant.DAL.CommonMethod;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class CollectionReport : UserControl
    {
        public CollectionReport()
        {
            InitializeComponent();
        }

        private void CollectionReport_Load(object sender, EventArgs e)
        {
            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            }
            LoadCollectionOrder();
        }

        private void LoadCollectionOrder()
        {
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetAllOrder("CLT");
            List<CltOrder> cltOrders = new List<CltOrder>();
            int cnt = 0;
            foreach (RestaurantOrder order in restaurantOrder)
            {
                cnt++;
                CltOrder show = new CltOrder();
                show.Sl = cnt;
                show.OrderTime = order.OrderTime;
                if (order.FormatedAddtress != null)
                {
                    ////string[] address = order.FormatedAddtress.Split('@#@');
                    ////show.Customer = address[0].Substring(0, address[0].Length);

                    if (order.FormatedAddtress.Contains("@#@") && order.FormatedAddtress != "")
                    { 
                        if (order.DeliveryAddress != "")
                        {
                            show.Customer = order.DeliveryAddress;
                        }
                        else
                        {
                            String[] spearator = { "@#@" };
                            string[] address = order.FormatedAddtress.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                            show.Customer = address[0] + "\n" + address[1];
                        }
                    }
                    else
                    {
                        if (order.DeliveryAddress != "")
                        {
                            show.Customer = order.DeliveryAddress;
                        }
                        else
                        {
                            show.Customer = order.FormatedAddtress;
                        } 
                    }
                }
                else {
                    show.Customer = order.CustomerName;
                }
                // show.Customer = order.DeliveryAddress.Length > 0 ? order.CustomerName.Split(',').FirstOrDefault() + order.DeliveryAddress : order.CustomerName;
                show.Total = order.TotalCost;
                show.OrderId = order.Id;
                cltOrders.Add(show);
               }
            cltOrdersDataGridView.DataSource = cltOrders;
        }

        private void cltOrdersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (cltOrdersDataGridView.RowCount>0)
                {
                    int row = e.RowIndex;
                    if (e.RowIndex > -1 && cltOrdersDataGridView.Columns["OrderId"] != null)
                    {
                        int orderId = Convert.ToInt32("0" + cltOrdersDataGridView.Rows[e.RowIndex].Cells["OrderId"].Value);

                        if (cltOrdersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] ==
                            cltOrdersDataGridView.Rows[e.RowIndex].Cells["DONE"])
                        {
                            RestaurantOrder tempOrder = new RestaurantOrderBLL().GetRestaurantOrderByOrderId(orderId);

                            //ShowOrderDetails details = new ShowOrderDetails(tempOrder);
                            //details.ShowDialog();
                            string totalValue = Convert.ToString(cltOrdersDataGridView.Rows[e.RowIndex].Cells["Total"].Value);
                            ConfirmOrderForm.PaymentDetails = new PaymentDetails();
                            ConfirmOrderForm aConformOrderForm = new ConfirmOrderForm(tempOrder);
                            aConformOrderForm.ShowDialog();

                            PaymentDetails aPaymentDetails = ConfirmOrderForm.PaymentDetails;

                            if (aPaymentDetails.Status == "Ok")
                            {
                                tempOrder.Id = orderId;
                                tempOrder.Status = "Paid";
                                tempOrder.PaymentMethod = aPaymentDetails.PaymentMethod;
                                tempOrder.CardAmount = aPaymentDetails.CardAmount;
                                tempOrder.CashAmount = aPaymentDetails.CashAmount;
                                RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();
                                bool res = aVariousMethod.UpdateRestaurantOrder(tempOrder);

                               OrderSyncroniseBLL aOrderSyncroniseBll = new OrderSyncroniseBLL();
                               int updateId = aOrderSyncroniseBll.SingleOrderSyncronise(tempOrder);

                                try
                                {
                                    List<PrinterSetup> PrinterSetups = new List<PrinterSetup>();
                                    PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                                    PrinterSetups = aPrinterSetupBll.GetTotalPrinterList();
                                    CommonOrderDetials orderDetials = new CommonOrderDetials(tempOrder);

                                    PrinterSetups = PrinterSetups.Where(a => a.PrintStyle == "Receipt").ToList();
                                    foreach (PrinterSetup kprinter in PrinterSetups)
                                    {                                       
                                        string printStr = orderDetials.GenerateDetails(orderId);
                                        bool isBillPrint = kprinter.PrintStyle == "Bill" ? true : false;
                                        PrintMethods tempPrintMethods_ = new PrintMethods(false, false, kprinter.printerMargin, isBillPrint);
                                        tempPrintMethods_.USBPrint(printStr, kprinter.PrinterName, kprinter.PrintCopy);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Printer not found.Please add printer.");
                                }
                                LoadCollectionOrder();
                            }
                        }
                        else if (cltOrdersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] ==cltOrdersDataGridView.Rows[e.RowIndex].Cells["VIEW"])
                        {
                            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                            RestaurantOrder tempOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(orderId);

                            ShowOrderDetails details = new ShowOrderDetails(tempOrder);
                            details.ShowDialog();
                            LoadCollectionOrder();
                            //string totalValue = Convert.ToString(cltOrdersDataGridView.Rows[e.RowIndex].Cells["Total"].Value);
                            //ConfirmOrderForm.PaymentDetails = new PaymentDetails();
                            //ConfirmOrderForm aConformOrderForm = new ConfirmOrderForm("£" + totalValue);
                            //aConformOrderForm.ShowDialog();

                            //PaymentDetails aPaymentDetails = ConfirmOrderForm.PaymentDetails;

                            //if (aPaymentDetails.Status == "Ok")
                            //{
                            //    tempOrder.Id = orderId;
                            //    tempOrder.Status = "Paid";
                            //    RestaurantOrderBLL aVariousMethod = new RestaurantOrderBLL();

                            //    bool res = aVariousMethod.UpdateRestaurantOrder(tempOrder);

                            //    LoadCollectionOrder();
                            //}
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;

                return Params;
            }
        }
    }

    public class CltOrder
    {
        public int OrderId { set; get; }
        public int Sl { set; get; }
        public DateTime OrderTime { set; get; }
        public string Customer { set; get; }
        public double Total { set; get; }
    }
}
