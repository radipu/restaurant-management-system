using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.DAL.DAO;
using TomaFoodRestaurant.Model; 
namespace TomaFoodRestaurant.OtherForm
{
    public partial class DeliveryReport : UserControl
    {
        public DeliveryReport()
        {
            InitializeComponent();
        }

        private void DeliveryReport_Load(object sender, EventArgs e)
        {
            if (!OthersMethod.CheckServerConneciton())
            {
                return;
            }
            LoadDriver();
            LoadCollectionOrder();
        }

        List<DelOrderDataTable> collection = new List<DelOrderDataTable>();

        public void LoadDriver()
        {
            tileGroup2.Items.Clear();

            List<DriverInformaiton> listofDriver = new MySqlDeliveryChargeDAO().GetDriverInformation();
            foreach (DriverInformaiton driverInformaiton in listofDriver)
            {
                driverInformaiton.ItemClick += (sender, e) =>
                {
                    DriverInformaiton button = sender as DriverInformaiton;
                    foreach (DriverInformaiton btn in listofDriver)
                    {
                        if (btn == button)
                        {
                            btn.Checked = true;
                        }
                        else
                        {
                            btn.Checked = false;
                        }
                    }
                };
                driverInformaiton.ItemDoubleClick += (sender, args) =>
                {
                    DriverInformaiton button = sender as DriverInformaiton;
                    button.Checked = false;
                    button.DriverList = listofDriver;
                    DriverOrderList orderList = new DriverOrderList(button);
                    orderList.ShowDialog();

                    LoadCollectionOrder();
                };
                tileGroup2.Items.Add(driverInformaiton);
            }
        }

        private void LoadCollectionOrder()
        {
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            List<RestaurantOrder> restaurantOrder = aRestaurantOrderBLL.GetAllOrder("DEL", "ALL").Where(a => a.DriverId == 0).ToList(); ;
            List<DelOrder> delOrder = new List<DelOrder>();
            int cnt = 0;
            foreach (RestaurantOrder order in restaurantOrder)
            {
                cnt++;
                DelOrder show = new DelOrder();
                show.Sl = cnt;
                show.OrderTime = order.OrderTime;
                if (order.FormatedAddtress != null)
                {
                    if (order.FormatedAddtress.Contains("@#@") && order.FormatedAddtress != "")
                    {
                        if(order.DeliveryAddress != "")
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
                else
                {
                    show.Customer = order.CustomerName;
                }
          
                show.Card = order.CardAmount;
                show.Cash = order.CashAmount;
                show.Total = order.TotalCost;
                show.OrderId = order.Id;
                delOrder.Add(show);             

                DelOrderDataTable rowAdd = new DelOrderDataTable()
                {
                    Sl = show.Sl,
                    OrderTime = show.OrderTime,
                    Customer = order.CustomerId,
                    Total = show.Total,

                    FormatedAddress = show.Customer,
                    PostCode = order.PostCode,
                    Postition = null,
                };
                collection.Add(rowAdd);
            }

            cltOrdersDataGridView.DataSource = delOrder;
        }

        private void cltOrdersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && cltOrdersDataGridView.Columns["OrderId"] != null)
            {
                int orderId = Convert.ToInt32("0" + cltOrdersDataGridView.Rows[e.RowIndex].Cells["OrderId"].Value);

                if (cltOrdersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == cltOrdersDataGridView.Rows[e.RowIndex].Cells["PRINT"])
                {
                    RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                    RestaurantOrder tempOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(orderId);
                    ShowOrderDetails details = new ShowOrderDetails(tempOrder);
                    details.ShowDialog();
                }
                else if (cltOrdersDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] == cltOrdersDataGridView.Rows[e.RowIndex].Cells["Assign"])
                {
                    RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                    RestaurantOrder tempOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(Convert.ToInt16(orderId));
                    bool selectDriver = false;
                    foreach (DriverInformaiton item in tileGroup2.Items)
                    {
                        if (item.Checked)
                        {
                            selectDriver = true;
                            tempOrder.DriverId = Convert.ToInt16(item.DriverId);
                            tempOrder.Status = "Paid";
                            var isUpdate = aRestaurantOrderBLL.UpdateRestaurantOrder(tempOrder);
                            LoadCollectionOrder();

                            return;
                        }
                    }
                    if (!selectDriver)
                    {
                        MessageBox.Show("Please select a driver.");
                    }
                }
            }
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            DeliveryMaplocationview view = new DeliveryMaplocationview(collection);
            view.ShowDialog();
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

    public class DelOrder
    {
        public int OrderId { set; get; }
        public int Sl { set; get; }
        public DateTime OrderTime { set; get; }
        public string Customer { set; get; }
        public double Cash { set; get; }
        public double Card { set; get; }
        public double Total { set; get; }
    }

    public class DelOrderDataTable
    {
        public int OrderId { set; get; }
        public int Sl { set; get; }
        public DateTime OrderTime { set; get; }
        public int Customer { set; get; }
        public double DelCharge { set; get; }
        public double Cash { set; get; }
        public double Card { set; get; }
        public double Total { set; get; }
        public string FormatedAddress { get; set; } 
        public string PostCode { get; set; }
        public string Postition { get; set; }
        public string DriverId { get; set; }
        public string Action { get; set; }
    }
}