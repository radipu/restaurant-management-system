using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class DriverOrderList : Form
    {
        private DriverInformaiton _driverInformaiton;

        public DriverOrderList(DriverInformaiton button)
        {
            InitializeComponent();

            _driverInformaiton = button;
            
        }

        public void LoadData()
        {
            List<RestaurantOrder> restaurantOrder = new RestaurantOrderBLL().GetAllOrder("DEL", "Paid").Where(a => a.DriverId == Convert.ToInt16(_driverInformaiton.DriverId)).ToList();
            List<DelOrderDataTable> delOrder = new List<DelOrderDataTable>();
            int cnt = 0;
            repositoryItemLookUpEdit1.DataSource = _driverInformaiton.DriverList;
            repositoryItemLookUpEdit1.DisplayMember = "DriverName";
            repositoryItemLookUpEdit1.ValueMember = "DriverId";
            foreach (RestaurantOrder order in restaurantOrder)
            {
                cnt++;
                DelOrderDataTable show = new DelOrderDataTable();
                show.Sl = cnt;
                if (order.FormatedAddtress != null)
                {
                    if (order.FormatedAddtress.Contains("@#@") && order.FormatedAddtress != "")
                    {
                        String[] spearator = { "@#@" };
                        string[] address = order.FormatedAddtress.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                        show.FormatedAddress = address[0] + " " + address[1];
                    }
                    else
                    {
                        show.FormatedAddress = order.FormatedAddtress;
                    }
                }
                else
                {
                    show.FormatedAddress = "";
                }


                //string[] address = order.FormatedAddtress.Split('@#@');
                show.OrderTime = order.OrderTime;
               // show.Customer = "";// address[0]+"\n"+address[1];
                show.Total = GlobalVars.numberRound(order.TotalCost,2);
                show.Cash = GlobalVars.numberRound(order.CashAmount,2);
                show.Card = GlobalVars.numberRound(order.CardAmount,2);
                show.DelCharge = GlobalVars.numberRound(order.DeliveryCost,2);
                show.OrderId = order.Id;
                show.DriverId = order.DriverId.ToString();
                show.Action = "UNASSIGNED";
                delOrder.Add(show);
            }
            gridControl1.DataSource = delOrder.ToList();
        }
        private void DriverOrderList_Load(object sender, EventArgs e)
        {

            LoadData();
        }

        private void okayButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editAction_Click(object sender, EventArgs e)
        {
            //HyperLinkEdit action = sender as HyperLinkEdit;

            var driverId = "0";// gridView1.GetFocusedRowCellValue("DriverId");
            var OrderId = gridView1.GetFocusedRowCellValue("OrderId");
          
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            RestaurantOrder tempOrder = aRestaurantOrderBLL.GetRestaurantOrderByOrderId(Convert.ToInt16(OrderId));
            tempOrder.DriverId = 0;
            var isUpdate = aRestaurantOrderBLL.UpdateRestaurantOrder(tempOrder);
            if (isUpdate)
            {
                MessageBox.Show("Order Unassigned.");             
            }
            LoadData();

        }
    }
}
