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
using TomaFoodRestaurant.DAL.DAO;
using DevExpress.XtraEditors;

namespace TomaFoodRestaurant.OtherForm
{
    public partial class KitchenControl : UserControl
    {
        public KitchenControl()
        {
            InitializeComponent();
        }

        private void KitchenControl_Load(object sender, EventArgs e)
        {
            loadkichensection();
        }
        public List<KitchenSection> DataTableToList(DataTable dt)
        {
            List<KitchenSection> modelList = new List<KitchenSection>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                KitchenSection model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new KitchenSection();

                    model.Id = (int)dt.Rows[n]["id"];
                    model.RestaurantId = (int)dt.Rows[n]["restaurant_id"];
                    model.Name = dt.Rows[n]["name"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        public List<KitchenItem> DataTableToItemList(DataTable dt)
        {
            List<KitchenItem> modelList = new List<KitchenItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                KitchenItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new KitchenItem();

                    model.orderItemId = (int)dt.Rows[n]["id"];
                    model.kichenId = (int)dt.Rows[n]["kitchen_section"];
                    model.itemQuantity = (int)dt.Rows[n]["quantity"];
                    model.sendToKitchen = (int)dt.Rows[n]["sent_to_kitchen"];
                    model.kitchenProcessing = (int)dt.Rows[n]["kitchen_processing"];
                    model.kichenDone = (int)dt.Rows[n]["kitchen_done"];
                    model.personCount = (int)dt.Rows[n]["no_person"];
                    model.itemName = dt.Rows[n]["name"].ToString();
                    model.tableName = dt.Rows[n]["table_name"].ToString();
                    model.Options = "";
                    model.MinusOptions = "";

                    List<OptionJson> ListOfOption = new OptionJsonConverter().DeSerialize(dt.Rows[n]["options"].ToString());
                    List<OptionJson> ListOfOption1 = new OptionJsonConverter().DeSerialize(dt.Rows[n]["options_minus"].ToString());

                    RestaurantMenuBLL aRestaurantMenuBll = new RestaurantMenuBLL();

                    if (ListOfOption != null)
                    {
                        string optionName = " -> ";
                        for (int i = 0; i < ListOfOption.Count; i++)
                        {
                          optionName += ListOfOption[i].optionName + ",";
                        }
                        if(optionName != " -> ")
                        {
                            model.Options = optionName.Substring(0, optionName.Length - 1);

                        }

                    }
                    if (ListOfOption1 != null)
                    {
                        string optionName = " -> No ";
                        for (int i = 0; i < ListOfOption1.Count; i++)
                        {
                            optionName +=  ListOfOption1[i].optionName + ",";
                        }
                        if (optionName != " -> No ")
                        {
                            model.MinusOptions = optionName.Substring(0, optionName.Length - 1);

                        }
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        private void loadkichensection()
        {
            DataTable KitchenSections = new MySqlRestaurantInformationDAO().getKitchenData(); 
            int cnt = 0;
            List<KitchenSection> KitchenSectionList = DataTableToList(KitchenSections);
            foreach (KitchenSection kitchen in KitchenSectionList)
            {
               Button btn = new Button();
                 
                btn.Name = "kichenTab" + kitchen.Id;  
                btn.Text = kitchen.Name; 


                btn.AutoSize = true;
                btn.BackColor = System.Drawing.Color.Wheat;
                btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btn.MinimumSize = new System.Drawing.Size(172,60);
                btn.TabIndex = kitchen.Id;
                btn.UseVisualStyleBackColor = false;
                btn.Click += new System.EventHandler(this.LoadKichenPage); 
                TopFayoutPanel.Controls.Add(btn);
            }

            double scWidth = Screen.PrimaryScreen.Bounds.Width;
        
            PendingLayoutPanel.Width = ProcessingLayoutPanel.Width = CompletedLayoutPanel.Width = (int)scWidth / 3;
            PendingLayoutPanel.Height = ProcessingLayoutPanel.Height = CompletedLayoutPanel.Height = Screen.PrimaryScreen.Bounds.Height;
            PendingLayoutPanel.Location = new Point(0, 40);
            ProcessingLayoutPanel.Location = new Point(PendingLayoutPanel.Location.X + PendingLayoutPanel.Width,40);
            CompletedLayoutPanel.Location = new Point(ProcessingLayoutPanel.Location.X + ProcessingLayoutPanel.Width,40);


            button1.Width = button2.Width = button3.Width = PendingLayoutPanel.Width;
            button1.Location = new Point(0, 0);
            button2.Location = new Point(button1.Location.X + button1.Width, 0);
            button3.Location = new Point(button2.Location.X + button2.Width, 0);

            loadPendingItems();
            loadPageItems(TopFayoutPanel.Controls[0].TabIndex);
          
            
        }

        void LoadKichenPage(object sender, EventArgs e)
        {

            Button selectedBtn = sender as Button;

            foreach (Button cont in TopFayoutPanel.Controls)
            {
                cont.BackColor = System.Drawing.Color.Wheat;
                cont.ForeColor = Color.Black;
               
            } 
            selectedBtn.BackColor = Color.CadetBlue;
            selectedBtn.ForeColor = Color.White;

            loadPendingItems();
            loadPageItems(selectedBtn.TabIndex); 
        }
        private void loadPendingItems()
        {
            int kichenId = 0;
            foreach (Button cont in TopFayoutPanel.Controls)
            {
                if (cont.BackColor == Color.CadetBlue) {
                    kichenId = cont.TabIndex;
                    break;
                } 
            }
            if (kichenId == 0) { 
               TopFayoutPanel.Controls[0].BackColor = Color.CadetBlue;
               TopFayoutPanel.Controls[0].ForeColor = Color.White;
               kichenId = TopFayoutPanel.Controls[0].TabIndex;
            }
            PendingLayoutPanel.Controls.Clear();
            DataTable KitchenDataTable = new MySqlRestaurantInformationDAO().loadKitchenItems(kichenId, "pending");
            int cnt = 0;
            List<KitchenItem> KitchenItemList = DataTableToItemList(KitchenDataTable);
            foreach (KitchenItem kitchenItem in KitchenItemList)
            {
                KitchenItem pendingBtn = kitchenItem;

                pendingBtn.Name = "buttonPending" + kitchenItem.itemName;
              //  pendingBtn.Text = (kitchenItem.itemQuantity - kitchenItem.kitchenProcessing) + "x" + kitchenItem.itemName + kitchenItem.Options + " IN ORDER, T-" + kitchenItem.tableName + ", P-" + kitchenItem.personCount;
                pendingBtn.Text = "TABLE - " + kitchenItem.tableName + ", PEOPLE-" + kitchenItem.personCount+"\n\r"+(kitchenItem.itemQuantity - kitchenItem.kitchenProcessing) + "x" + kitchenItem.itemName +"\n\r"+ kitchenItem.Options;
                pendingBtn.UseVisualStyleBackColor = true;
                pendingBtn.BackColor = Color.LemonChiffon;
                pendingBtn.Width = PendingLayoutPanel.Width - 30;
                pendingBtn.Dock = DockStyle.None;
                pendingBtn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                pendingBtn.Height = 80;
                pendingBtn.Click += pendingBtn_Click;

                //processingBtn.Click += new System.EventHandler(this.UpdateKichenStatus(kitchenItem.orderItemId, "pending", (kitchenItem.itemQuantity - kitchenItem.kitchenProcessing), kitchenItem.kichenId)); 

                PendingLayoutPanel.Controls.Add(pendingBtn);
            }
             

        }


        private void loadPageItems(int kichenId)
        {

            //PendingLayoutPanel.Controls.Clear();
            //DataTable KitchenDataTable = new MySqlRestaurantInformationDAO().loadKitchenItems(kichenId, "pending");
            //int cnt = 0;
            //List<KitchenItem> KitchenItemList = DataTableToItemList(KitchenDataTable);
            //foreach (KitchenItem kitchenItem in KitchenItemList)
            //{
            //    KitchenItem pendingBtn = kitchenItem;

            //    pendingBtn.Name = "buttonPending" + kitchenItem.itemName;
            //    pendingBtn.Text = (kitchenItem.itemQuantity - kitchenItem.kitchenProcessing) + "x" + kitchenItem.itemName + " IN ORDER, T-" + kitchenItem.tableName + ", P-" + kitchenItem.personCount;
            //    pendingBtn.UseVisualStyleBackColor = true;
            //    pendingBtn.BackColor = Color.LemonChiffon; 
            //    pendingBtn.Width = PendingLayoutPanel.Width - 30;
            //    pendingBtn.Dock = DockStyle.None;
            //    pendingBtn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //    pendingBtn.Height = 60;
            //    pendingBtn.Click += pendingBtn_Click;
               
            //    //processingBtn.Click += new System.EventHandler(this.UpdateKichenStatus(kitchenItem.orderItemId, "pending", (kitchenItem.itemQuantity - kitchenItem.kitchenProcessing), kitchenItem.kichenId)); 

            //    PendingLayoutPanel.Controls.Add(pendingBtn);
            //}

            ProcessingLayoutPanel.Controls.Clear();
            DataTable KitchenDataTable1 = new MySqlRestaurantInformationDAO().loadKitchenItems(kichenId, "processing");
       
            List<KitchenItem> KitchenItemList1 = DataTableToItemList(KitchenDataTable1);
            foreach (KitchenItem kitchenItem in KitchenItemList1)
            {
                KitchenItem processingBtn = kitchenItem;
              
                processingBtn.Name = "buttonProcessing" + kitchenItem.itemName;
               // processingBtn.Text = (kitchenItem.kitchenProcessing - kitchenItem.kichenDone) + "x" + kitchenItem.itemName + " IN ORDER, T-" + kitchenItem.tableName + ", P-" + kitchenItem.personCount;

                processingBtn.Text = "TABLE - " + kitchenItem.tableName + ", PEOPLE-" + kitchenItem.personCount + "\n\r" + (kitchenItem.kitchenProcessing - kitchenItem.kichenDone) + "x" + kitchenItem.itemName + "\n\r" + kitchenItem.Options;

                processingBtn.UseVisualStyleBackColor = true;

                processingBtn.Click += processingBtn_Click;
               
                //processingBtn.Click += new System.EventHandler(this.UpdateKichenStatus(kitchenItem.orderItemId, "processing", (kitchenItem.kitchenProcessing - kitchenItem.kichenDone),kitchenItem.kichenId)); 

                processingBtn.BackColor = Color.MediumTurquoise;
                processingBtn.ForeColor = Color.White;

                processingBtn.Width = ProcessingLayoutPanel.Width - 30;
                processingBtn.Dock = DockStyle.None;
                processingBtn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                processingBtn.Height = 80;
              
                ProcessingLayoutPanel.Controls.Add(processingBtn);
            }

            CompletedLayoutPanel.Controls.Clear();
            DataTable KitchenDataTable2 = new MySqlRestaurantInformationDAO().loadKitchenItems(kichenId, "done");
            
            List<KitchenItem> KitchenItemList2 = DataTableToItemList(KitchenDataTable2);
            foreach (KitchenItem kitchenItem in KitchenItemList2)
            {
                Button doneBtn = new Button();

                doneBtn.Name = "buttonDone" + kitchenItem.itemName;
            //    doneBtn.Text = (kitchenItem.kichenDone) + "x" + kitchenItem.itemName + " IN ORDER, T-" + kitchenItem.tableName + ", P-" + kitchenItem.personCount;
                doneBtn.Text = "TABLE - " + kitchenItem.tableName + ", PEOPLE-" + kitchenItem.personCount + "\n\r" + (kitchenItem.kichenDone) + "x" + kitchenItem.itemName + "\n\r" + kitchenItem.Options;


                doneBtn.UseVisualStyleBackColor = true;

                doneBtn.Width = CompletedLayoutPanel.Width - 30;
                doneBtn.Dock = DockStyle.None;
                doneBtn.BackColor = Color.Honeydew;
                doneBtn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                doneBtn.Height = 80;
              
                CompletedLayoutPanel.Controls.Add(doneBtn);
            }


        }

        private void pendingBtn_Click(object sender, EventArgs e)
        {
            KitchenItem pendingBtn = sender as KitchenItem;
            new MySqlRestaurantInformationDAO().UpdateKichen(pendingBtn.orderItemId, "pending", (pendingBtn.itemQuantity - pendingBtn.kitchenProcessing));
            loadPendingItems();
            loadPageItems(pendingBtn.kichenId);

        }

        void processingBtn_Click(object sender, EventArgs e)
        {
            KitchenItem pendingBtn = sender as KitchenItem;

            new MySqlRestaurantInformationDAO().UpdateKichen(pendingBtn.orderItemId, "processing", (pendingBtn.kitchenProcessing - pendingBtn.kichenDone));
            loadPendingItems();
            loadPageItems(pendingBtn.kichenId);

        }
         

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadPendingItems();
        }
 

         
    }
}
