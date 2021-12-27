using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using TomaFoodRestaurant.BLL;
using TomaFoodRestaurant.Model;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class TableLoadForm : Form
    {
        public static string TableNumber = "";
        public static int Person = 0;
        public static int TableId = 0;
        public static string Status = "";

        public TableLoadForm()
        {
            InitializeComponent();
        }

        private void TableLoadForm_Load(object sender, EventArgs e)
        {
            LoadAllTable();
        }

        private void LoadAllTable()
        {

            try
            {

                Console.WriteLine("Table load 3 :  " + DateTime.Now.TimeOfDay);
                tableFlowLayoutPanel.Controls.Clear();
                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
                List<RestaurantTable> aRestaurantTable = GetRestaurantTable();
                Button aButton = new Button();
                aButton.Click += new EventHandler(TableButton_Click);
                aButton.Font = new System.Drawing.Font(aButton.Font.Name, 16, FontStyle.Bold);
                aButton.FlatStyle = FlatStyle.Flat;
                aButton.FlatAppearance.BorderSize = 0;
                aButton.Width = aButton.Height = 92;
                aButton.Text = "TAW";

                aButton.BackColor = Color.DarkSeaGreen;
                aButton.ForeColor = Color.White;
                tableFlowLayoutPanel.Controls.Add(aButton);
                foreach (RestaurantTable table in aRestaurantTable)
                {

                    aButton = new Button();
                    aButton = new SqureButton();
                    aButton.Width = aButton.Height = 92;

                    //if (table.TableShape == "round")
                    //{
                    //    aButton = new RoundButton();
                    //    aButton.Width = 100;
                    //    aButton.Height = 100;

                    //}
                    //else if (table.TableShape == "square")
                    //{
                    //    aButton = new SqureButton();
                    //    aButton.Width = aButton.Height = 92;

                    //}
                    //else if (table.TableShape == "oval")
                    //{

                    //    aButton = new RectangleButton();
                    //    aButton.Width = 150;
                    //    aButton.Height = 75;

                    //}
                    //else
                    //{
                    //    aButton = new SqureButton();
                    //    aButton.Width = aButton.Height = 92;
                    //}
                    aButton.Click += new EventHandler(TableButton_Click);
                    aButton.Font = new System.Drawing.Font(aButton.Font.Name, 16, FontStyle.Bold);
                    aButton.FlatStyle = FlatStyle.Flat;
                    aButton.FlatAppearance.BorderSize = 0;
                    if (table.CurrentStatus == "busy")
                    {
                        if (table.Name != "0")
                        {
                            TimeSpan time = (DateTime.Now.Subtract(table.UpdateTime));
                            aButton.Text = table.Name + "|" + table.Person + "\r\n" + time.Hours + ":" + time.Minutes;
                        }
                        else
                        {
                            aButton.Text = "TAW";
                        }

                        aButton.BackColor = Color.DarkRed;

                        if (table.IsBill)
                        {
                            aButton.BackColor = Color.FromArgb(150, 122, 220);
                        }
                    }
                    else
                    {
                        aButton.BackColor = Color.DarkSeaGreen;
                        if (table.Name == "0")
                        {
                            aButton.Text = "TAW";
                        }
                        else aButton.Text = table.Name;
                    }
                    aButton.ForeColor = Color.White;
                    aButton.Name = table.Id.ToString();

                    tableFlowLayoutPanel.Controls.Add(aButton);
                }

                Console.WriteLine("Table load 4 :  " + DateTime.Now.TimeOfDay);

            }
            catch (Exception ex)
            {
               // File.AppendAllText("Config/log.txt", "\n\n Table error log : " + ex.Message.ToString() + "\n\n");
                this.Activate();

            }

            //   RefreshAllEmptyTable(aRestaurantTable);
        }

        private List<RestaurantTable> GetRestaurantTable()
        {
            Console.WriteLine("Table load :  " + DateTime.Now.TimeOfDay);
            RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
            List<RestaurantTable> tempRestaurantTable = aRestaurantTableBll.GetRestaurantTable();
            tempRestaurantTable = tempRestaurantTable.OrderBy(a => a.CurrentStatus).ToList();
            List<RestaurantTable> aTrackingNumberList = tempRestaurantTable.Where(b => b.MergeStatus > 0).GroupBy(a => a.MergeStatus).Select(group => group.First()).ToList();

            Console.WriteLine("Table load 1 :  " + DateTime.Now.TimeOfDay);
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

            Console.WriteLine("Table load 2:  " + DateTime.Now.TimeOfDay);
            return tempRestaurantTable.OrderBy(a => a.SortOrder).ToList();
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
                Button aButton = sender as Button;
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
                RefreshAllEmptyTable(aRestaurantTable);
            }
            catch (Exception)
            {
                this.Activate();

            }


            //RestaurantTable aTable = aVariousMethod.GetRestaurantTableByTableId(aGeneralInformation.TableId);
            //aTable.CurrentStatus = "available";
            //aVariousMethod.UpdateRestaurantTable(aTable);
        }

        private void RefreshAllEmptyTable(List<RestaurantTable> aRestaurantTable)
        {
            try
            {
                bool status = false;
                RestaurantTableBLL aRestaurantTableBll = new RestaurantTableBLL();
                foreach (RestaurantTable table in aRestaurantTable)
                {
                    
                    bool emptyTable = CheckEmptyTable(table);
                    if (emptyTable)
                    {
                        table.CurrentStatus = "available";
                        aRestaurantTableBll.UpdateRestaurantTable(table);
                        status = true;
                    } else
                    {
                        table.CurrentStatus = "busy";
                        aRestaurantTableBll.UpdateRestaurantTable(table);
                    }
                    
                }
                LoadAllTable();
            }
            catch (Exception ex)
            {
                this.Activate();
                throw;
            }

            //if (status)
            //{
            //    LoadAllTable();
            //}
        }

        private bool CheckEmptyTable(RestaurantTable table)
        {
            try
            {
                RestaurantOrderBLL aRestaurantOrderBLL = new RestaurantOrderBLL();
                RestaurantOrder aRestaurantOrder = new RestaurantOrder();
                aRestaurantOrder = aRestaurantOrderBLL.GetRestaurantOrder(table.Id, "pending");

                if (aRestaurantOrder != null && aRestaurantOrder.Id > 0)
                {
                    return false;
                }
                else return true;
            }
            catch (Exception)
            {
                this.Activate();
                return false;

            }

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {

                this.Close();
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
}
