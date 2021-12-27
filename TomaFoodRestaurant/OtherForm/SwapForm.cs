using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class SwapForm : Form
    {
        List<RestaurantTable> aRestaurantTable = new List<RestaurantTable>();
        public SwapForm()
        {
            InitializeComponent();
         
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            aRestaurantTable = aRestaurantTableBll.GetRestaurantTable();            
        }

        private void SwapForm_Load(object sender, EventArgs e)
        {
            LoadBusyTable();
            LoadVacantTable();
        }

        private void LoadBusyTable()
        {
            List<RestaurantTable> tempRestaurantTable = aRestaurantTable.Where(a => a.CurrentStatus == "busy").ToList();
            tempRestaurantTable.OrderBy(a => a.SortOrder);
            fromSwapComboBox.DataSource = tempRestaurantTable;
            fromSwapComboBox.DisplayMember = "Name";
            fromSwapComboBox.ValueMember = "Id";
        }

        private void LoadVacantTable()
        {
            List<RestaurantTable> tempRestaurantTable = aRestaurantTable.Where(a => a.CurrentStatus == "available").ToList();
            tempRestaurantTable.OrderBy(a => a.SortOrder);
            toComboBox.DataSource = tempRestaurantTable;
            toComboBox.DisplayMember = "Name";
            toComboBox.ValueMember = "Id";
        }

        private void swapButton_Click(object sender, EventArgs e)
        {
            RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            RestaurantTable fromTable = fromSwapComboBox.SelectedItem as RestaurantTable;
            RestaurantTable toTable = toComboBox.SelectedItem as RestaurantTable;
            toTable.CurrentStatus = "busy";
            if (toTable.IsBill)
            {
                toTable.CurrentStatus = "bill";
            }
          
            toTable.Person = fromTable.Person;
            toTable.UpdateTime = fromTable.UpdateTime;
            aRestaurantTableBll.UpdateRestaurantTable(toTable);
            RestaurantOrder aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrder(fromTable.Id, "pending");
            aRestaurantOrder.OrderTable = toTable.Id;
            aRestaurantOrderBLL.UpdateRestaurantOrder(aRestaurantOrder);

            fromTable.CurrentStatus = "available";
            fromTable.Person = 0;
            aRestaurantTableBll.UpdateRestaurantTable(fromTable);
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}