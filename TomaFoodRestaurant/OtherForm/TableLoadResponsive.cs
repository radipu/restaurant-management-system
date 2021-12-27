using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using DevExpress.XtraEditors;
using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class TableLoadResponsive : Form
    {
        public static string TableNumber = "";
        public static int Person = 0;
        public static int TableId = 0;
        public static string Status = "";

        public TableLoadResponsive()
        {
            InitializeComponent();
            ShowInTaskbar = false;            
        }

        private void TableLoadResponsive_Load(object sender, EventArgs e)
        {            
            LoadAllTable();
        }

        private void LoadAllTable()
        {
            try
            {               
                List<RestaurantTable> aRestaurantTable = GetRestaurantTable();
                aRestaurantTable=  aRestaurantTable.OrderBy(a => a.SortOrder).ToList();
               tileGroup.Items.Clear();
                TileItem  aButton = new TileItem();
                aButton.ItemClick += new TileItemClickEventHandler(TableButton_Click);
                aButton.AppearanceItem.Normal.BackColor = Color.DarkSeaGreen;
                aButton.AppearanceItem.Normal.BorderColor = Color.White;
                aButton.ItemSize=TileItemSize.Wide;
                aButton.AppearanceItem.Normal.Font=new Font("Tahoma",16,FontStyle.Bold);
                aButton.TextAlignment=TileItemContentAlignment.MiddleCenter;

                aButton.Text = "TAW";

                tileGroup.Items.Add(aButton);

                foreach (RestaurantTable table in aRestaurantTable)
                {
                     aButton = new TileItem();
                    
                    aButton.ItemClick += new TileItemClickEventHandler(TableButton_Click);
                     aButton.AppearanceItem.Normal.Font = new Font("Tahoma", 16, FontStyle.Bold);
                     aButton.TextAlignment = TileItemContentAlignment.MiddleCenter;
                     aButton.ItemSize = TileItemSize.Wide;if (table.CurrentStatus == "busy")
                    {
                        if (table.Name != "0"){
                            TimeSpan time = (DateTime.Now.Subtract(table.UpdateTime));
                            aButton.Text = table.Name + "|" + table.Person + "\r\n" + time.Hours + ":" + time.Minutes;
                        }
                        else
                        {
                            aButton.Text = "TAW";
                        }

                        aButton.AppearanceItem.Normal.BackColor = Color.DarkRed;

                        if (table.IsBill)
                        {
                            aButton.AppearanceItem.Normal.BackColor = Color.FromArgb(150, 122, 220);
                        }
                    }
                    else
                    {
                        aButton.AppearanceItem.Normal.BackColor = Color.DarkSeaGreen;
                        if (table.Name == "0")
                        {
                            aButton.Text = "TAW";
                        }
                        else aButton.Text = table.Name;
                    }
                    aButton.AppearanceItem.Normal.ForeColor = Color.White;
                    aButton.Name = table.Id.ToString();

                    tileGroup.Items.Add(aButton);
                }
            }
            catch (Exception ex)
            {
                this.Activate();
            }

            //   RefreshAllEmptyTable(aRestaurantTable);
        }

        private List<RestaurantTable> GetRestaurantTable()
        {
            var stopWatch = Stopwatch.StartNew();
            stopWatch.Start();

            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            List<RestaurantTable> tempRestaurantTable = aRestaurantTableBll.GetRestaurantTable();
            //tempRestaurantTable = tempRestaurantTable.OrderBy(a => a.CurrentStatus).ToList();
            List<RestaurantTable> aTrackingNumberList = tempRestaurantTable.Where(b => b.MergeStatus > 0).GroupBy(a => a.MergeStatus).Select(group => group.First()).ToList();

            List<RestaurantTable> anotherTrackingNumberList = new List<RestaurantTable>();
            foreach (RestaurantTable restaurantTable in aTrackingNumberList)
            {
                string tableName = "";
                List<RestaurantTable> tables = tempRestaurantTable.Where(a => a.MergeStatus == restaurantTable.MergeStatus).ToList();
                RestaurantTable table = tables.FirstOrDefault(a => a.CurrentStatus == "busy");
                bool underScoreFlag = false;
                if (tables.Count > 0)
                {
                    foreach (RestaurantTable tb in tables)
                    {
                        if (underScoreFlag)
                        {
                            tableName += "_";
                        }
                        tableName += tb.Name;
                        underScoreFlag = true;
                    }
                }

                if (table != null && table.Id > 0)
                {
                    table.Name = tableName;
                    anotherTrackingNumberList.Add(table);
                }
                else
                {
                    restaurantTable.Name = tableName;
                    anotherTrackingNumberList.Add(restaurantTable);
                }
            }

            tempRestaurantTable.RemoveAll(a => a.MergeStatus > 0);
            tempRestaurantTable.AddRange(anotherTrackingNumberList);
            stopWatch.Stop();
            Console.WriteLine("Load :" + stopWatch.Elapsed.TotalSeconds);
            return tempRestaurantTable.OrderBy(a => a.SortOrder).ToList();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!OthersMethod.CheckServerConneciton())
                {
                    return;
                }

                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
                TileItem aButton = sender as TileItem;
                if (aButton.Name != "")
                {
                    RestaurantTable aRestaurantTable = aRestaurantTableBll.GetRestaurantTableByTableId(Convert.ToInt32(aButton.Name));
                    if (aRestaurantTable.CurrentStatus == "available")
                    {
                        if (aRestaurantTable.Name != "0")
                        {
                            CoversForm.Status = "";
                            CoversForm.Covers = "";
                            CoversForm aForm = new CoversForm();
                            aForm.ShowDialog();
                            if (CoversForm.Status == "" || CoversForm.Status == "cancel") return;
                            Person = Convert.ToInt32("0" + CoversForm.Covers);
                        }
                        aRestaurantTable.Person = Person;//MM-dd-yyyy hh:mm tt
                        string date = DateTime.Now.ToString();
                        aRestaurantTable.UpdateTime = DateTime.Now;
                        aRestaurantTable.CurrentStatus = "busy";
                        aRestaurantTableBll.UpdateRestaurantTable(aRestaurantTable);
                    }
                    else
                    {
                        if (aRestaurantTable.IsBill)
                        {
                            aRestaurantTable.CurrentStatus = "bill";
                        }
                        aRestaurantTableBll.UpdateRestaurantTable(aRestaurantTable);
                    }
                    TableNumber = aRestaurantTable.Name;
                    TableId = aRestaurantTable.Id;
                    Person = aRestaurantTable.Person;
                    Status = "ok";
                }
                this.Close();
            }
            catch (Exception)
            {
                this.Activate();
            }
        }
        
        private void refreshTableButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!OthersMethod.CheckServerConneciton())
                {
                    return;
                }
                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
                List<RestaurantTable> aRestaurantTable = aRestaurantTableBll.GetRestaurantTable();
                OthersMethod.RefreshAllEmptyTable(aRestaurantTable);
                LoadAllTable();
            }
            catch (Exception)
            {
                this.Activate();
            }
        }

        private void mergeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!OthersMethod.CheckServerConneciton())
                {
                    return;
                }
                MergeForm aMergeForm = new MergeForm();
                aMergeForm.ShowDialog();
                LoadAllTable();
            }
            catch (Exception ex)
            {
                this.Activate();
            }
        }

        private void swapButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!OthersMethod.CheckServerConneciton())
                {
                    return;
                }
                SwapForm aSwapForm = new SwapForm();
                aSwapForm.ShowDialog();
                LoadAllTable();
            }
            catch (Exception ex)
            {
                this.Activate();
            }
        }
    }
}