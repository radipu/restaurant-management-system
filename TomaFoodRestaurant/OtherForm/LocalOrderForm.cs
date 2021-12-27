using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class LocalOrderForm : Form
    { 
        public LocalOrderForm()
        {
            InitializeComponent(); 
        }

        private void LocalOrderForm_Load(object sender, EventArgs e)
        {
         
            
        }

       



        //private void tabledataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    var dataGridViewColumn = tabledataGridView.Columns["Id"];
        //    if (dataGridViewColumn != null)
        //        dataGridViewColumn.Visible = false;


        //    var dataGridViewColumn1 = tabledataGridView.Columns["RestaurantId"];
        //    if (dataGridViewColumn1 != null)
        //        dataGridViewColumn1.Visible = false;


        //    var dataGridViewColumn2 = tabledataGridView.Columns["TableShape"];
        //    if (dataGridViewColumn2 != null)
        //        dataGridViewColumn2.Visible = false;

        //    var dataGridViewColumn3 = tabledataGridView.Columns["SortOrder"];
        //    if (dataGridViewColumn3 != null)
        //        dataGridViewColumn3.Visible = false;

        //    var dataGridViewColumn4 = tabledataGridView.Columns["Person"];
        //    if (dataGridViewColumn4 != null)
        //        dataGridViewColumn4.Visible = false;

        //    var dataGridViewColumn5 = tabledataGridView.Columns["UpdateTime"];
        //    if (dataGridViewColumn5 != null)
        //        dataGridViewColumn5.Visible = false;

        //    var dataGridViewColumn6 = tabledataGridView.Columns["MergeStatus"];
        //    if (dataGridViewColumn6 != null)
        //        dataGridViewColumn6.Visible = false;

        //}

        //private void mergeButton_Click(object sender, EventArgs e)
        //{

        //    List<RestaurantTable> selectedMergeTables=new List<RestaurantTable>();
        //    RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
        //    foreach (DataGridViewRow row in tabledataGridView.Rows)
        //    {
              
        //        if (row.Cells["Id"].Value.ToString().Length != 0 && Convert.ToBoolean(row.Cells["select"].Value) == true)
        //        {
        //            int tableId = Convert.ToInt32(row.Cells["Id"].Value);
        //            int mergeId = Convert.ToInt32(row.Cells["MergeStatus"].Value);

        //            if (mergeId > 0)
        //            {
        //                List<RestaurantTable> tempRestaurantTable = aRestaurantTableBll.GetRestaurantTableByMergeId(mergeId);
        //                selectedMergeTables.AddRange(tempRestaurantTable);
        //            }
        //            else
        //            {
        //                RestaurantTable tempRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableId(tableId);
        //                selectedMergeTables.Add(tempRestaurantTable);
        //            }
              
        //        }

        //    }

        //    bool validMarge = CheckValidTableMerge(selectedMergeTables);

        //    if (!validMarge)
        //    {
        //        MessageBox.Show("You cannot merge table.","Table Merge Confirmation Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        //        return;
        //    }

        //    if (selectedMergeTables.Count > 1)
        //    {
        //        MergedSelectedTable(selectedMergeTables);
        //        LoadInformation();
        //        this.Close();
        //    }
          
        //}

        //private bool CheckValidTableMerge(List<RestaurantTable> selectedMergeTables)
        //{
        //    int count = selectedMergeTables.Count(a => a.CurrentStatus == "busy");
        //    return count <= 1;
        //}

        //private void MergedSelectedTable(List<RestaurantTable> selectedMergeTables)
        //{
           
        //    int uniqueTrackingId = GetTrackingNumber();
        //    RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
        //    foreach (RestaurantTable table in selectedMergeTables)
        //    {
        //        table.MergeStatus = uniqueTrackingId;
        //        aRestaurantTableBll.UpdateRestaurantTable(table);
        //    }


        //}

        //private int GetTrackingNumber()
        //{
        //    return aRestaurantTable.Max(a => a.MergeStatus) + 1;
        //}

        //private void LoadInformation()
        //{
        //    RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
        //  List<RestaurantTable> tempRestaurantTable = aRestaurantTableBll.GetRestaurantTable();
        //    tempRestaurantTable = tempRestaurantTable.OrderBy(a => a.CurrentStatus).ToList();
        //  List<RestaurantTable> aTrackingNumberList = tempRestaurantTable.OrderByDescending(c=>c.CurrentStatus).Where(a=>a.MergeStatus>0).GroupBy(customer => customer.MergeStatus).Select(group => group.First()).ToList();
        //    foreach (RestaurantTable restaurantTable in aTrackingNumberList)
        //    {
        //        string tableName = "";
        //        List<RestaurantTable> tables = tempRestaurantTable.Where(a => a.MergeStatus == restaurantTable.MergeStatus).ToList();
        //        bool underScoreFlag = false;
        //        if (tables.Count > 0)
        //        {
        //            foreach (RestaurantTable tb in tables)
        //            {
        //                if (underScoreFlag)
        //                {
        //                    tableName += "_";
        //                }
        //                tableName += tb.Name;
        //                underScoreFlag = true;
        //            }
        //        }

        //        restaurantTable.Name = tableName;

        //    }

        //    tempRestaurantTable.RemoveAll(a => a.MergeStatus > 0);
        //    tempRestaurantTable.AddRange(aTrackingNumberList);
        //    tempRestaurantTable = tempRestaurantTable.OrderBy(a => a.SortOrder).ToList();
        //    tabledataGridView.DataSource = tempRestaurantTable;
        //}

        //private void closeButton_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //private void demergeButton_Click(object sender, EventArgs e)
        //{
        //    RestaurantTableBLL aRestaurantTableBll=new RestaurantTableBLL();
        //    bool isDemarge = false;
        //    foreach (DataGridViewRow row in tabledataGridView.Rows)
        //    {

        //        if (row.Cells["Id"].Value.ToString().Length != 0 && Convert.ToBoolean(row.Cells["select"].Value) == true)
        //        {
        //            isDemarge = true;
        //            int tableId = Convert.ToInt32(row.Cells["Id"].Value);
        //            RestaurantTable tempRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableId(tableId);
        //            aRestaurantTableBll.ToAvailableMergeTable(tempRestaurantTable);
        //        }
        //    }
        //    if (isDemarge)
        //    {
        //        this.Close();
        //    }
        //}
    }
}
