using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class RestaurantOrdersReport : UserControl
    {
        public static long OrderId = 0;
        public RestaurantOrdersReport()
        {
            InitializeComponent();
            tableLayoutPanel1.Hide();
            panel7.Hide();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            } 
           LoadAllOrder();
        }
       
        private IEnumerable<DataRow> CheckPayment(IEnumerable<DataRow> query)
        {
            if (paymentComboBox.Text != "All")
            {
                List<DataRow> aRows = new List<DataRow>();
                foreach (DataRow data in query)
                {
                    if (data["payment_method"].ToString().ToUpper() == paymentComboBox.Text.ToUpper())
                    {
                        aRows.Add(data);
                    }
                }
                query = aRows;
            }

            return query;
        }

        //{ "All", "Paid", "pending" };
        private IEnumerable<DataRow> CheckStatus(IEnumerable<DataRow> query)
        {
            if (statusComboBox.Text != "All")
            {
                List<DataRow> aRows = new List<DataRow>();
                string status = "";
                if (statusComboBox.Text == "Paid") status = "Paid";
                else if (statusComboBox.Text == "pending") status = "pending";
                // if (typeComboBox.Text == "Delivery") status = "WAIT";
                foreach (DataRow data in query)
                {
                    if (data["status"].ToString().ToUpper() == status.ToUpper())
                    {
                        aRows.Add(data);
                    }
                }
                query = aRows;          
            }
            return query;
        }     

        private IEnumerable<DataRow> CheckType(IEnumerable<DataRow> query)
        {
            List<DataRow> aRows = new List<DataRow>();
            if (typeComboBox.Text != "All")
            {
                string status = "";
                if (typeComboBox.Text == "Restaurant") status = "IN";
                else if (typeComboBox.Text == "Delivery") status = "DEL";
                else status = "CLT";
                // if (typeComboBox.Text == "Delivery") status = "WAIT";
                //  online_order

                foreach (DataRow data in query)
                {
                    if (typeComboBox.Text != "Online")
                    {
                        if (data["order_type"].ToString() == status)
                        {
                            aRows.Add(data);
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(data["online_order"]))
                        {
                            aRows.Add(data);
                        }
                    }
                }
                query = aRows;

                //query = (from myRow in query.AsEnumerable()
                //         where (myRow.Field<string>("order_type").ToString().ToUpper()==status.ToUpper())
                //         select myRow);
            }
            
            return query;
        }
     
        private void orderDetails(Int32 orderId)
        {
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            RestaurantOrder tempOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(orderId);
            ShowOrderDetails details = new ShowOrderDetails(tempOrder);
            details.ShowDialog(); 
        }

        private void RestaurantOrdersReport_Load(object sender, EventArgs e)
        {
                statusComboBox.Text = typeComboBox.Text = paymentComboBox.Text = "All";
                LoadAllOrder(false);           
        }

        public RestaurantOrder CheckOrder()
        {
            RestaurantOrder ordrOrder = new RestaurantOrder();
            string status = "";
            if (typeComboBox.Text == "All") status = "All";
            else if (typeComboBox.Text == "Restaurant") status = "IN";
            else if (typeComboBox.Text == "Delivery") status = "DEL";
            else if (typeComboBox.Text == "Collection") status = "CLT";
            else status = "Online";
            ordrOrder.OrderType = status;
          
            //*****************************************************************
            if (statusComboBox.Text == "All") status = "All";
            else  if (statusComboBox.Text == "Paid") status = "Paid";
            else if (statusComboBox.Text == "pending") status = "pending";
            ordrOrder.Status = status;
            //*****************************************************************
            
            ordrOrder.PaymentMethod =  paymentComboBox.Text;

            //*****************************************************************
            //if (userComboBox.Text!="All")
            //{
            //    ordrOrder.UserId = Convert.ToInt16(userComboBox.SelectedValue);
            //}
            
            return ordrOrder;
        }

        public void  LoadAllOrder(bool Isprint=false)
        {
            try
            {
                DateTime fromDateTime = fromDateTimePicker.Value.Date;
                DateTime toDateTime = toDateTimePicker.Value.Date.AddDays(1);

                if (GlobalSetting.RestaurantInformation.ReportClosingHour > 12)
                {
                    toDateTime = toDateTime.AddHours(-(24 - GlobalSetting.RestaurantInformation.ReportClosingHour));
                    toDateTime = toDateTime.AddMinutes(-GlobalSetting.RestaurantInformation.ReportClosingMin);
                }
                else
                {
                    toDateTime = toDateTime.AddHours(GlobalSetting.RestaurantInformation.ReportClosingHour);
                    toDateTime = toDateTime.AddMinutes(GlobalSetting.RestaurantInformation.ReportClosingMin);
                }
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                RestaurantOrder _checkOrder = CheckOrder();
                DataTable aDataTable = aRestaurantOrderBLL.GetRestaurantOrderByDate(fromDateTime, toDateTime, _checkOrder);
                //IEnumerable<DataRow> query = (from myRow in aDataTable.AsEnumerable() select myRow);
                 
                List<SaleOrder> aSaleOrders = new List<SaleOrder>();
                int cnt = 1;
                int cntOrder = 0;
                int cash_count = 0;
                int card_count = 0;
                int split_count = 0;

                double cashAmount = 0;
                double cardAmount = 0;
                double splitAmount = 0;
                double tableOrder = 0;
                int person = 0;
                int deliveryOrder = 0;
                int collectionOrder = 0;
                double totalSale = 0;

                foreach (DataRow row in aDataTable.Rows)
                {

                    if (!Convert.ToBoolean(row["online_order"]) || (Convert.ToBoolean(row["online_order"]) && Convert.ToString(row["online_order_status"]) != "pending"))
                    {
                        SaleOrder aSaleOrder = new SaleOrder();
                        aSaleOrder.SL = cnt;
                        aSaleOrder.OrderId = row.Field<int>("id");
                        aSaleOrder.Date = row.Field<DateTime>("OrderTime").ToString("g");
                        aSaleOrder.Status = row.Field<string>("status");
                        aSaleOrder.Receipt = Convert.ToBoolean(row["receipt"]) ? "R" : "NR";
                        aSaleOrder.Total = "£ " + row.Field<double>("total_cost").ToString("F02");
                        if (row.Field<string>("order_type") == "IN")
                        {
                            if (Convert.ToBoolean(row["receipt"]))
                            {
                                tableOrder += 1;
                            }
                            aSaleOrder.Type = "T " + row.Field<string>("name");

                            try
                            {
                                if (Convert.ToBoolean(row["receipt"]))
                                {
                                    person += Convert.ToInt32(row["person"]);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(row["receipt"]))
                            {
                                if (row.Field<string>("order_type") == "CLT")
                                {
                                    collectionOrder += 1;
                                }
                                else
                                {
                                    deliveryOrder += 1;
                                }
                            }
                            aSaleOrder.Type = row.Field<string>("order_type");
                        }
                        if (Convert.ToBoolean(row["receipt"]))
                        {
                            if ((row.Field<double>("cash_amount")) > 0 && (row.Field<double>("card_amount")) == 0)
                            {
                                cash_count++;
                            }
                            else if ((row.Field<double>("card_amount")) > 0 && (row.Field<double>("cash_amount")) == 0)
                            {
                                card_count++;
                            }
                            else
                            {
                                split_count++;
                                splitAmount += GlobalVars.numberRound((row.Field<double>("total_cost")), 2);
                            }
                        }

                        aSaleOrder.Date = row.Field<DateTime>("OrderTime").ToString("g");
                        aSaleOrder.Status = row.Field<string>("status");
                        aSaleOrder.Total = "£ " + row.Field<double>("total_cost").ToString("F02");
                        if (row.Field<string>("order_type") == "TABLE" || row.Field<string>("order_type") == "IN")
                        {
                            aSaleOrder.Type = "T " + row.Field<string>("name");
                        }
                        else
                        {
                            if (Convert.ToBoolean(row["online_order"]))
                            {
                                aSaleOrder.Type = "W-" + row.Field<string>("order_type");
                            }
                            else
                            {
                                aSaleOrder.Type = row.Field<string>("order_type");
                            }
                        }
                        if (Convert.ToBoolean(row["receipt"]))
                        {
                            if (aSaleOrder.Status.ToLower() != "cancelled")
                            {
                                totalSale += row.Field<double>("total_cost");
                                double cash = row.Field<double>("cash_amount");
                                double card_amount = row.Field<double>("card_amount");

                                cashAmount += GlobalVars.numberRound(cash, 2);
                                cardAmount += GlobalVars.numberRound(card_amount, 2);
                                cntOrder++;
                            }
                        }
                        aSaleOrder.Card = "£ " + row.Field<double>("card_amount").ToString("F02");
                        aSaleOrder.Cash = "£ " + row.Field<double>("cash_amount").ToString("F02");
                        var customer = row.Field<string>("firstname");
                        if (customer != null)
                        {
                            String[] spearator = { "@#@" };
                            string[] address = customer.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                            aSaleOrder.Customer = address[0].Substring(0, address[0].Length - 1);
                            // aSaleOrder.Customer = "<span style='margin:15px;' >" +customer.Replace("#", "</br>") +"</span>";
                            if (aSaleOrder.Type == "DEL" || aSaleOrder.Type == "W-DEL")
                            {
                                var delivery_address = row.Field<string>("delivery_address");
                                if (delivery_address != "")
                                {
                                    if (aSaleOrder.Type == "DEL")
                                    {
                                        aSaleOrder.Customer = delivery_address;
                                    }
                                    else
                                    {
                                        var fName = address[0].Split(',');
                                        aSaleOrder.Customer = fName[0] + "\n" + delivery_address;
                                    }
                                }
                                else {
                                    aSaleOrder.Customer = address[0] + "\n" + address[1];
                                }
                            }
                            // aSaleOrder.Customer = customer.Replace("#", "\n\r");
                        }
                        else {
                            var customerName = row.Field<string>("customer_name");
                            if (customerName != null)
                                aSaleOrder.Customer = customerName;
                        }
                        aSaleOrder.Total = row.Field<double>("total_cost").ToString("F02");

                        aSaleOrders.Add(aSaleOrder);

                        if (Convert.ToBoolean(row["receipt"]))
                        {
                            cnt++;
                        }
                    }
                }
                if (GlobalSetting.RestaurantUsers.Usertype.ToLower() == "restaurant_admin" || GlobalSetting.RestaurantUsers.Usertype.ToLower() == "admin")
                {
                    panel3.Visible = true;
                    panel4.Visible = true;
                    panel5.Visible = true;
                    panel6.Visible = true; totalOrderLabel.Text = +cntOrder + "\r\nTotal Order";
                    totalSaleLabel.Text = "£" + GlobalVars.numberRound(totalSale,2).ToString("f02") + "\r\nTotal Sale";
                    cashLabel.Text = "£" + GlobalVars.numberRound(cashAmount,2).ToString("F02") + "\r\nCash";
                    cardLabel.Text = "£" + GlobalVars.numberRound(cardAmount,2).ToString("F02") + "\r\nCard";
                    restaurantOrderLabel.Text = (tableOrder).ToString() + "\r\nRestaurant Order";
                    personLabel.Text = (person).ToString() + "\r\nPeople";
                    totalDeliveryLabel.Text = (deliveryOrder).ToString() + "\r\nDelivery";
                    totalCollecctionLabel.Text = (collectionOrder).ToString() + "\r\nCollection";

                }
                else
                {
                    panel3.Visible = false;
                    panel4.Visible = false;
                    panel5.Visible = false;
                    panel6.Visible = false;
                    tableLayoutPanel1.Visible = false;
                    tableLayoutPanel1.Height = 0;
                    // ordersDataGridViewControl.Location = new Point(ordersDataGridViewControl.Location.X, panel1.Location.Y + panel1.Size.Height + 10);
                }

                ordersDataGridViewControl.DataSource = aSaleOrders.ToList();
                if (Isprint)
                {
                    List<PrintContent> aPrintContentsHead = new List<PrintContent>();
                    List<PrintContent> aPrintContentsMid = new List<PrintContent>();
                    List<PrintContent> aPrintContentsLast = new List<PrintContent>();

                    PrintFormat aPrintFormat = new PrintFormat(24);
                    PrintContent aPrintContent = new PrintContent();

                    PrinterSetupBLL aPrinterSetupBll = new PrinterSetupBLL();
                    PrinterSetup printer = aPrinterSetupBll.GetTotalPrinterList().FirstOrDefault(a => a.PrintStyle == "Receipt");

                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);

                    if (tableOrder > 0)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen("Restaurant Order " + " " + tableOrder + " " + " " + person + " People") + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }
                    if (collectionOrder > 0)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen("Collection " + " " + collectionOrder + " " + " ") + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }
                    if (deliveryOrder > 0)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.get_fullStringForkitchen("Delivery " + " " + " " + " " + deliveryOrder + " " + " ") + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }
                    
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    
                    if (cashAmount > 0)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.alignment_setting("CASH " + " " + " " + " " + cash_count + " " + " " + " " + " " + " " + " £" + (GlobalVars.numberRound(cashAmount,2)).ToString("F02"), (" £" + (cashAmount).ToString("F02")).Length) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }
                    if (cardAmount > 0)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.alignment_setting("CARD " + " " + " " + " " + card_count + " " + " " + " " + " " + " " + " £" + (GlobalVars.numberRound(cardAmount,2)).ToString("F02"), (" £" + (cardAmount).ToString("F02")).Length) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }
                    if (splitAmount > 0)
                    {
                        aPrintContent = new PrintContent();
                        aPrintContent.StringLine = aPrintFormat.alignment_setting("SPLIT" + " " + " " + " " + split_count + " " + " " + " " + " " + " " + " £" + (GlobalVars.numberRound(splitAmount,2)).ToString("F02"), (" £" + (splitAmount).ToString("F02")).Length) + "\r\n";
                        aPrintContentsMid.Add(aPrintContent);
                    }
                    
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.alignment_setting("TOTAL " + " " + " " + (cnt - 1) + " " + " " + " " + " " + " £" + (GlobalVars.numberRound(totalSale,2)).ToString("F02"), (" £" + (totalSale).ToString("F02")).Length) + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    int rowCnt = 0;
                    RestaurantUsers aUser = new RestaurantUsers();
                    string full_name = "";
                    String preorderdate = "";
                    String orderdate = "";
                    foreach (DataRow order in aDataTable.Rows)
                    {
                        if (Convert.ToBoolean(order["receipt"]))
                        {
                            rowCnt++;
                            full_name = order.Field<string>("firstname");
                            DateTime date = Convert.ToDateTime(order.Field<DateTime>("OrderTime"));
                            orderdate = date.ToString("dddd dd MMMM yyyy").ToUpper();
                            String ordertime = date.ToString("HH:mm");
                            if (preorderdate != orderdate)
                            {
                                preorderdate = orderdate; aPrintContent = new PrintContent();
                                aPrintContent.StringLine = " " + "\r\n" + "\r\n" + orderdate + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                                aPrintContent = new PrintContent();
                                aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                                aPrintContentsMid.Add(aPrintContent);
                            }
                            
                            aPrintContent = new PrintContent();
                            string order_status = "X";
                            if ((order.Field<double>("cash_amount")) > 0 && (order.Field<double>("card_amount")) == 0)
                            {
                                order_status = "C";
                            }
                            else if ((order.Field<double>("card_amount")) > 0 && (order.Field<double>("cash_amount")) == 0)
                            {
                                order_status = "B";
                            }
                            else
                            {
                                order_status = "X";
                            }
                            
                            //  string total_cost = "£" + order.Field<double>("total_cost").ToString("F02");
                            //  string number = "£" + order.Field<double>("total_cost").ToString("F02"); //total_cost.Substring(total_cost.Length - 6);
                            
                            int length =   ("£" + order.Field<double>("total_cost").ToString("F02")).ToString().Length;
                            
                            if (length < 5)
                            {
                                length = 5;
                            }
                            
                            //if (full_name == " , , ,   ")
                            //{
                            //    full_name = "";
                            //}
                            //if (full_name.Length > 6)
                            //{
                            //    full_name = full_name.Substring(0, 6) + "  ";
                            //}
                            
                            if (order.Field<string>("order_type") == "IN")
                            {
                                aPrintContent.StringLine = aPrintFormat.alignment_setting(rowCnt + ". I" + order_status + " " + ordertime + " " +  "£" + order.Field<double>("total_cost").ToString("F02"), length) + "\r\n";
                            }
                            else
                            {
                                if (order.Field<string>("order_type") == "CLT")
                                {
                                    aPrintContent.StringLine = aPrintFormat.alignment_setting(rowCnt + ". C" + order_status + " " + ordertime + " " +  "£" + order.Field<double>("total_cost").ToString("F02"), length) + "\r\n";
                                }
                                else
                                {
                                    aPrintContent.StringLine = aPrintFormat.alignment_setting(rowCnt + ". D" + order_status + " " + ordertime + " " +  "£" + order.Field<double>("total_cost").ToString("F02"), length) + "\r\n";
                                }
                            }
                            aPrintContentsMid.Add(aPrintContent);
                        }
                    }
                    // aPrintContentsMid.Add(aPrintContent);
                    
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    aPrintContent = new PrintContent();
                    aPrintContent.StringLine = aPrintFormat.CreateDashedLine() + "\r\n";
                    aPrintContentsMid.Add(aPrintContent);
                    string allpage = "";
                    for (int i = 0; i < aPrintContentsHead.Count; i++)
                    {
                        allpage += aPrintContentsHead[i].StringLine;
                    }
                    for (int i = 0; i < aPrintContentsMid.Count; i++)
                    {
                        allpage += aPrintContentsMid[i].StringLine;
                    }
                    
                    PrintMethods tempPrintMethods = new PrintMethods(false);
                    tempPrintMethods.USBPrint(allpage, printer.PrinterName, 1);                
                }            
            }
            catch (Exception  ex)
            {
                new ErrorReportBLL().SendErrorReport(ex.GetBaseException().ToString());                
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            LoadAllOrder(true);
        }

        private void buttonFinished_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order already finalized.", "Reorder Process", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
        }

        private void ordersDataGridViewControl_DataSourceChanged(object sender, EventArgs e)
        {
            CustomDrawCell(ordersDataGridViewControl, ordersDataGridView);
            ordersDataGridView.RowCellStyle += new RowCellStyleEventHandler(gridView_RowCellStyle);
        }
        
        public static void CustomDrawCell(GridControl gridControl, GridView gridView)
        {// Handle this event to paint cells manually
            //gridView.CustomDrawCell += (s, e) =>
            //{

            //    if (e.Column.FieldName == "statusa")
            //    {
            //        int recipt = Convert.ToInt32(gridView.GetRowCellValue(e.RowHandle, gridView.Columns["Receipt"]));
            //        if (recipt > 0)
            //        {
            //            var image = (System.Drawing.Image)Properties.Resources.r;
            //            e.Column.ImageAlignment = StringAlignment.Center;
            //            e.Cache.Graphics.DrawImage(image, e.Bounds);
            //            e.Handled = true;
            //        }
            //        else
            //        {
            //            var image = (System.Drawing.Image)Properties.Resources.NR;

            //            e.Column.ImageAlignment = StringAlignment.Center;
            //            e.Cache.Graphics.DrawImage(image, e.Bounds);
            //            e.Handled = true;
            //        }
            //    }

            //};
        }

        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.Column.FieldName == "Status")
            {
                string status = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"]);

                if (status.ToLower() == "cancelled")
                {
                    e.Appearance.BackColor = Color.FromArgb(150, Color.Red);
                    e.Appearance.BackColor2 = Color.FromArgb(150, Color.Salmon);
                }
            }

               //if (e.Column.FieldName == "statusa")
               // {
               //     int recipt = Convert.ToInt32( View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"])); //gridView.GetRowCellValue(e.RowHandle, gridView.Columns["Receipt"]));
               //     if (recipt > 0)
               //     {
               //         var image = (System.Drawing.Image)Properties.Resources.r;
               //         e.Column.ImageAlignment = StringAlignment.Center;
               //         e.Cache.Graphics.DrawImage(image, e.Bounds);
               //         e.Handled = true;
               //     }
               //     else
               //     {
               //         var image = (System.Drawing.Image)Properties.Resources.NR;

               //         e.Column.ImageAlignment = StringAlignment.Center;
               //         e.Cache.Graphics.DrawImage(image, e.Bounds);
               //         e.Handled = true;
               //     }
               // } 
            

            //if (e.Column.FieldName == "reorder")
            //{
            //    if (GlobalSetting.RestaurantInformation.MenuDrag > 0)
            //    {
            //        e.Appearance.BackColor = Color.FromArgb(150, Color.Red);

            //    }
                 
            //}             
        }

        private void ordersDataGridView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            int orderId = Convert.ToInt32("0" + ordersDataGridView.GetRowCellValue(e.RowHandle, "OrderId"));
            OrderId = orderId;
            if (e.Column.FieldName == "reorder")
            {
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                RestaurantOrder tempOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(orderId);
                if (tempOrder.OnlineOrder>0 && tempOrder.PaymentMethod.ToLower() =="card")
                { 
                       MessageBox.Show("Sorry ! It is online order.", "Reorder Process", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning); 
                }
                else
                {
                    if (GlobalSetting.RestaurantUsers.Usertype.ToLower() != "restaurant_operator")
                    {
                        this.ParentForm.Close();
                    }
                    else
                    {
                        if (GlobalSetting.RestaurantInformation.MenuDrag > 0)
                        {
                            MessageBox.Show("Sorry ! Unable to reorder.", "Reorder Process", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                        else
                        {
                            this.ParentForm.Close();
                        }
                    }               
                }               
            }
            else if (e.Column.FieldName == "Receipt")
            {
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                RestaurantOrder tempOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(orderId);

                if (tempOrder.Receipt > 0)
                {
                    tempOrder.Receipt = 0;
                    ordersDataGridView.SetRowCellValue(e.RowHandle, "Receipt","NR");
                }
                else
                {
                    tempOrder.Receipt = 1;
                    ordersDataGridView.SetRowCellValue(e.RowHandle, "Receipt", "R");
                }                   
                
                if (OthersMethod.CheckForInternetConnection())
                {
                    OrderSyncroniseBLL aOrderSyncroniseBll = new OrderSyncroniseBLL();
                    int updateId = aOrderSyncroniseBll.SingleOrderSyncronise(tempOrder);                      
                }                             
                 
                aRestaurantOrderBLL.UpdateOrderStatus(tempOrder);
              //  LoadAllOrder(false);
            }
            else if (e.Column.FieldName == "details")
            {
                orderDetails(orderId);
                LoadAllOrder();
            }           
        }

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            panel7.Show();
        }

        private void loginNumberButton_click(object sen24, EventArgs e)
        {
            Button aButton = sen24 as Button;
            string aButtonPretext = passwordTextBox.Text;
            aButtonPretext += aButton.Text;
            if (aButton.Text == "CLEAR")
            {
                passwordTextBox.Text = "";
            }
            else
            {
                passwordTextBox.Text = aButtonPretext;
            }
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
                string sha = GetSha1(passwordTextBox.Text).ToLower();
                RestaurantUsers user = aUserLoginBll.GetRestaurantUserByPassword(sha);

                if (user != null && user.Id > 0)
                {
                    tableLayoutPanel1.Show();
                    panel7.Hide();
                    buttonStatus.Hide();
                }
                else
                {
                    MessageBox.Show("Please enter  correct admin password.", "Login Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ErrorReportBLL aErrorReportBll = new ErrorReportBLL();
                aErrorReportBll.SendErrorReport(ex.ToString());
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
}