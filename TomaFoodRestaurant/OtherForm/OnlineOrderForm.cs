using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class OnlineOrderForm : Form
    {
        public static string status {set;get;}
        public static int Id{set;get;}
        GlobalUrl urls = new GlobalUrl();
        RestaurantInformation aRestaurantInformation = new RestaurantInformation();

        public OnlineOrderForm()
        {
            InitializeComponent();
        }

        private void OnlineOrderForm_Load(object sender, EventArgs e)
        {
            LoadOnlineOrder();
            GlobalUrlBLL aGlobalUrlBll = new GlobalUrlBLL();
            urls = aGlobalUrlBll.GetUrls();
            RestaurantInformationBLL aRestaurantInformationBll = new RestaurantInformationBLL();
            aRestaurantInformation = aRestaurantInformationBll.GetRestaurantInformation();
        }

        private void LoadOnlineOrder()
        {
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderForOnline(1, "pending");
            List<OnlineOrderShow> aOnlineOrderShow = new List<OnlineOrderShow>();
            int cnt=0;
            foreach (RestaurantOrder order in restaurantOrder) 
            {
                cnt++;
                OnlineOrderShow show = new OnlineOrderShow();
                show.Sl = cnt;
                show.OrderTime = order.OrderTime;
                show.DeleveryTime = order.DeliveryTime.ToString("HH:mm:ss") == "00:00:00" ? "ASAP" : order.DeliveryTime.ToString("HH:mm:ss");
                show.Customer = order.DeliveryAddress.Length > 0 ? order.CustomerName.Split(',').FirstOrDefault()+ order.DeliveryAddress: order.CustomerName;
                show.Type = order.OrderType;
                if(order.OrderType == "IN")
                {                    
                    //get table info                    
                    var tableInfo = aRestaurantTableBll.GetRestaurantTableByTableId(order.OrderTable);
                    show.Type = "T-" + tableInfo.Name + ", P-" + order.Person;
                }
                show.Method = order.PaymentMethod;
                show.Total = order.TotalCost.ToString("C", new CultureInfo("en-GB"));
                show.OrderId = order.Id;
                show.OnlineOrder = (int)order.OnlineOrderId;
                show.OnlineOrderStatus = order.OnlineOrderStatus;             
                aOnlineOrderShow.Add(show);
            }

            onlineOrdersDataGridView.DataSource = aOnlineOrderShow;
        }

        private void onlineOrdersDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            onlineOrdersDataGridView.Columns["Sl"].DisplayIndex = 1;
            onlineOrdersDataGridView.Columns["OrderTime"].DisplayIndex = 2;
            onlineOrdersDataGridView.Columns["DeleveryTime"].DisplayIndex = 3;
            onlineOrdersDataGridView.Columns["DeleveryTime"].Width = 100;

            onlineOrdersDataGridView.Columns["Customer"].DisplayIndex = 4;
            onlineOrdersDataGridView.Columns["Type"].DisplayIndex = 5;
            onlineOrdersDataGridView.Columns["Type"].Width = 150;
            onlineOrdersDataGridView.Columns["Method"].DisplayIndex = 6;
            onlineOrdersDataGridView.Columns["Total"].DisplayIndex = 7;
            onlineOrdersDataGridView.Columns["OnlineOrderStatus"].DisplayIndex = 8;
            onlineOrdersDataGridView.Columns["OnlineOrderStatus"].Width = 100;
           
            //var dataGridViewColumn = onlineOrdersDataGridView.Columns["OrderId"];
            //if (dataGridViewColumn != null)
            //    dataGridViewColumn.Visible = false;
            //var delevieryColumn = onlineOrdersDataGridView.Columns["DeleveryTime"];
            //if (delevieryColumn != null)
            //    delevieryColumn.DisplayIndex = 2;
            //    delevieryColumn.Width = 100;
            //var dataGridViewColumn1 = onlineOrdersDataGridView.Columns["accept"];
            //if (dataGridViewColumn1 != null)
            //    dataGridViewColumn1.DisplayIndex = 9;
            //var dataGridViewColumn2 = onlineOrdersDataGridView.Columns["reject"];
            //if (dataGridViewColumn2 != null)
            //    dataGridViewColumn2.DisplayIndex =10;
            //var dataGridViewColumn3 = onlineOrdersDataGridView.Columns["acceptTime"];
            //if (dataGridViewColumn3 != null)
            //    dataGridViewColumn3.DisplayIndex = 9;
            //var dataGridViewColumn11 = onlineOrdersDataGridView.Columns["view"];
            //if (dataGridViewColumn11 != null)
            //    dataGridViewColumn11.Visible = true;

            //var dataGridViewColumn4 = onlineOrdersDataGridView.Columns["OnlineOrder"];
            //if (dataGridViewColumn4 != null)
            //    dataGridViewColumn4.Visible = false;
            //var onlineOrderStatus = onlineOrdersDataGridView.Columns["OnlineOrderStatus"];
            //if (onlineOrderStatus != null)
            //    onlineOrderStatus.Visible = false;
            //var customer = onlineOrdersDataGridView.Columns["Customer"];
            //if (customer != null)
            //    customer.Visible = false;
            //customer.DisplayIndex = 3;
            //var total = onlineOrdersDataGridView.Columns["Total"];
            //if (total != null)
            //    total.Visible = true;
            //total.DisplayIndex = 6;
            //var view = onlineOrdersDataGridView.Columns["View"];
            //if (view != null)
            //    view.Visible = true;
            //view.DisplayIndex = 7;

            //foreach (DataGridViewRow  row in onlineOrdersDataGridView.Rows)
            //{
            //    OnlineOrderShow show = (OnlineOrderShow) row.DataBoundItem;
            //    if (show.Method.ToLower() == "card" && show.OnlineOrderStatus.ToLower() == "pending")
            //    {
            //        row.DefaultCellStyle.BackColor = Color.Red;
            //    }
            //}
        }

        private void onlineOrdersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && onlineOrdersDataGridView.Columns["OrderId"] != null)
            {
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                int orderId = Convert.ToInt32("0" + onlineOrdersDataGridView.Rows[e.RowIndex].Cells["OrderId"].Value);
                int onlineOrderId = Convert.ToInt32("0" + onlineOrdersDataGridView.Rows[e.RowIndex].Cells["OnlineOrder"].Value);

                if (onlineOrdersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == onlineOrdersDataGridView.Rows[e.RowIndex].Cells["accept"])
                {
                    string postData = "deliver_in=20 Mins";
                    string url = urls.AcceptUrl + "restaurantcontrol/request/crud/accept_online_order/"+aRestaurantInformation.Id+"/" + onlineOrderId;
                    string result = SendOnlineOrderStatus(onlineOrderId, postData, url);
                    if (result == onlineOrderId.ToString())
                    {
                        string res = aRestaurantOrderBLL.UpdateOnlineOrder(onlineOrderId, "accepted");
                        if (res == "Yes")
                        {
                            Id = orderId;
                            status = "ok";
                            this.Close();
                        }
                    }
                    else
                    {
                        string res = aRestaurantOrderBLL.DeleteOrderByOrderId(orderId);
                        if (res == "Yes")
                        {
                            LoadOnlineOrder();
                        }
                    }
                }
                else if (onlineOrdersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == onlineOrdersDataGridView.Rows[e.RowIndex].Cells["reject"])
                {
                    RejectCauseForm.status = "";
                    RejectCauseForm.message = "";
                    RejectCauseForm aRejectCauseForm = new RejectCauseForm();
                    aRejectCauseForm.ShowDialog();
                    if (RejectCauseForm.status == "ok")
                    {
                        string postData = "reject_cause=" + RejectCauseForm.message;
                        string url = urls.AcceptUrl + "restaurantcontrol/request/crud/reject_online_order/" + aRestaurantInformation.Id + "/" + +onlineOrderId;
                        string result = SendOnlineOrderStatus(onlineOrderId, postData, url);
                        if (result == onlineOrderId.ToString()) 
                        {
                            string res = aRestaurantOrderBLL.DeleteOrderByOrderId(orderId);
                            if (res == "Yes")
                            {
                                LoadOnlineOrder();
                            }
                        }
                    }
                }
                else if (onlineOrdersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == onlineOrdersDataGridView.Rows[e.RowIndex].Cells["acceptTime"])
                {
                    //DeliveryTime.status = "";
                    //DeliveryTime.time = "";
                    //DeliveryTime times = new DeliveryTime();//times.ShowDialog();
                    //if (DeliveryTime.status == "ok")
                    //{
                    //    string postData = "deliver_in=" + DeliveryTime.time;
                    //    string url = urls.AcceptUrl + "restaurantcontrol/request/crud/accept_online_order/"+aRestaurantInformation.Id+"/" + onlineOrderId;
                    //    string result = SendOnlineOrderStatus(onlineOrderId, postData, url);
                    //    string res = aRestaurantOrderBLL.UpdateOnlineOrder(onlineOrderId, "accepted");
                    //    if (res == "Yes")
                    //    {
                    //        Id = orderId;
                    //        status = "ok";
                    //        this.Close();
                    //    }
                    //}
                }
                else if (onlineOrdersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == onlineOrdersDataGridView.Rows[e.RowIndex].Cells["view"])
                {
                    if (orderId > 0)
                    {
                        RestaurantOrderBLL aRestaurantOrderBll=new RestaurantOrderBLL();
                        //RestaurantOrder aRestaurantOrder = aRestaurantOrderBll.GetRestaurantOrderByOrderId(orderId);
                      //  ShowOrderDetails aShowOrderDetails = new ShowOrderDetails(aRestaurantOrder, false);
                        //aShowOrderDetails.ShowDialog();

                       RestaurantOrder aRestaurantOrder = aRestaurantOrderBll.GetRestaurantOrderByOrderId(orderId);
                       OnlineOrderDetailsForm aShowOrderDetails = new OnlineOrderDetailsForm(aRestaurantOrder, false);
                        aShowOrderDetails.ShowDialog();
                        List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetRestaurantOrderForOnline(1, "pending");
                        if (restaurantOrder.Count==0)
                        {
                            this.Close();
                        }else
                        {
                            LoadOnlineOrder();
                        }
                    }             
                }
            }
        }

        private string SendOnlineOrderStatus(int onlineOrderId, string data, string url)
        {
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Post the data to the right place.
            Uri target = new Uri(url);
            WebRequest request = WebRequest.Create(target);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            string result = string.Empty;
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

            return result;
        }

        private void goBackButton_Click(object sender, EventArgs e)
        {
            this.Close();
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

    public class OnlineOrderShow 
    {
        public int OrderId { set; get; }
        public int OnlineOrder { set; get; }
        public int Sl { set; get; }
        public DateTime OrderTime { set; get; }
        public string Customer { set; get; }
        public string Type { set; get; }
        public string Method { set; get; }
        public string Total { set; get; }
        public string OnlineOrderStatus { set; get; }
        public string DeleveryTime { get; set; }
    }
}
