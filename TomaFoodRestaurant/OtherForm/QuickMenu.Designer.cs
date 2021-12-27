
namespace TomaFoodRestaurant.OtherForm
{
    partial class QuickMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelDropDown = new System.Windows.Forms.Panel();
            this.ExpandCollapse = new System.Windows.Forms.Button();
            this.customFlowLayoutPanel1 = new TomaFoodRestaurant.Model.CustomFlowLayoutPanel();
            this.home = new System.Windows.Forms.Button();
            this.Table = new System.Windows.Forms.Button();
            this.OnlineOrder = new System.Windows.Forms.Button();
            this.changePrice = new System.Windows.Forms.Button();
            this.resOrder = new System.Windows.Forms.Button();
            this.resCollection = new System.Windows.Forms.Button();
            this.resDelivery = new System.Windows.Forms.Button();
            this.resSetup = new System.Windows.Forms.Button();
            this.panelDropDown.SuspendLayout();
            this.customFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 15;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelDropDown
            // 
            this.panelDropDown.Controls.Add(this.customFlowLayoutPanel1);
            this.panelDropDown.Controls.Add(this.ExpandCollapse);
            this.panelDropDown.Location = new System.Drawing.Point(1, 2);
            this.panelDropDown.MaximumSize = new System.Drawing.Size(675, 109);
            this.panelDropDown.MinimumSize = new System.Drawing.Size(675, 43);
            this.panelDropDown.Name = "panelDropDown";
            this.panelDropDown.Size = new System.Drawing.Size(675, 43);
            this.panelDropDown.TabIndex = 21;
            // 
            // ExpandCollapse
            // 
            this.ExpandCollapse.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.ExpandCollapse.FlatAppearance.BorderSize = 0;
            this.ExpandCollapse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExpandCollapse.Image = global::TomaFoodRestaurant.Properties.Resources.Expand_Arrow_20px;
            this.ExpandCollapse.Location = new System.Drawing.Point(320, 2);
            this.ExpandCollapse.Name = "ExpandCollapse";
            this.ExpandCollapse.Size = new System.Drawing.Size(39, 38);
            this.ExpandCollapse.TabIndex = 15;
            this.ExpandCollapse.UseVisualStyleBackColor = false;
            this.ExpandCollapse.Click += new System.EventHandler(this.ExpandCollapse_Click);
            // 
            // customFlowLayoutPanel1
            // 
            this.customFlowLayoutPanel1.BackColor = System.Drawing.Color.Linen;
            this.customFlowLayoutPanel1.Controls.Add(this.home);
            this.customFlowLayoutPanel1.Controls.Add(this.Table);
            this.customFlowLayoutPanel1.Controls.Add(this.OnlineOrder);
            this.customFlowLayoutPanel1.Controls.Add(this.changePrice);
            this.customFlowLayoutPanel1.Controls.Add(this.resOrder);
            this.customFlowLayoutPanel1.Controls.Add(this.resCollection);
            this.customFlowLayoutPanel1.Controls.Add(this.resDelivery);
            this.customFlowLayoutPanel1.Controls.Add(this.resSetup);
            this.customFlowLayoutPanel1.Location = new System.Drawing.Point(2, 44);
            this.customFlowLayoutPanel1.Name = "customFlowLayoutPanel1";
            this.customFlowLayoutPanel1.Size = new System.Drawing.Size(671, 66);
            this.customFlowLayoutPanel1.TabIndex = 22;
            // 
            // home
            // 
            this.home.BackColor = System.Drawing.Color.Transparent;
            this.home.FlatAppearance.BorderSize = 0;
            this.home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.home.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.home.Image = global::TomaFoodRestaurant.Properties.Resources.home_24px;
            this.home.Location = new System.Drawing.Point(3, 3);
            this.home.Name = "home";
            this.home.Size = new System.Drawing.Size(60, 62);
            this.home.TabIndex = 16;
            this.home.TabStop = false;
            this.home.Text = "Home";
            this.home.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.home.UseVisualStyleBackColor = false;
            this.home.Click += new System.EventHandler(this.home_Click);
            // 
            // Table
            // 
            this.Table.BackColor = System.Drawing.Color.Transparent;
            this.Table.FlatAppearance.BorderSize = 0;
            this.Table.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Table.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Table.Image = global::TomaFoodRestaurant.Properties.Resources.restaurant_table_24px;
            this.Table.Location = new System.Drawing.Point(69, 3);
            this.Table.Name = "Table";
            this.Table.Size = new System.Drawing.Size(58, 62);
            this.Table.TabIndex = 17;
            this.Table.TabStop = false;
            this.Table.Text = "Table";
            this.Table.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Table.UseVisualStyleBackColor = false;
            this.Table.Click += new System.EventHandler(this.Table_Click);
            // 
            // OnlineOrder
            // 
            this.OnlineOrder.BackColor = System.Drawing.Color.Transparent;
            this.OnlineOrder.FlatAppearance.BorderSize = 0;
            this.OnlineOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OnlineOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OnlineOrder.Image = global::TomaFoodRestaurant.Properties.Resources.Deliver_Food_24px;
            this.OnlineOrder.Location = new System.Drawing.Point(133, 3);
            this.OnlineOrder.Name = "OnlineOrder";
            this.OnlineOrder.Size = new System.Drawing.Size(107, 62);
            this.OnlineOrder.TabIndex = 18;
            this.OnlineOrder.TabStop = false;
            this.OnlineOrder.Text = "Online Order";
            this.OnlineOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.OnlineOrder.UseVisualStyleBackColor = false;
            this.OnlineOrder.Click += new System.EventHandler(this.OnlineOrder_Click);
            // 
            // changePrice
            // 
            this.changePrice.BackColor = System.Drawing.Color.Transparent;
            this.changePrice.FlatAppearance.BorderSize = 0;
            this.changePrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changePrice.Image = global::TomaFoodRestaurant.Properties.Resources.price_tag_pound_24px;
            this.changePrice.Location = new System.Drawing.Point(246, 3);
            this.changePrice.Name = "changePrice";
            this.changePrice.Size = new System.Drawing.Size(112, 62);
            this.changePrice.TabIndex = 19;
            this.changePrice.TabStop = false;
            this.changePrice.Text = "Change Price";
            this.changePrice.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.changePrice.UseVisualStyleBackColor = false;
            this.changePrice.Click += new System.EventHandler(this.changePrice_Click);
            // 
            // resOrder
            // 
            this.resOrder.BackColor = System.Drawing.Color.Transparent;
            this.resOrder.FlatAppearance.BorderSize = 0;
            this.resOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resOrder.Image = global::TomaFoodRestaurant.Properties.Resources.restaurant_24px;
            this.resOrder.Location = new System.Drawing.Point(364, 3);
            this.resOrder.Name = "resOrder";
            this.resOrder.Size = new System.Drawing.Size(67, 62);
            this.resOrder.TabIndex = 20;
            this.resOrder.TabStop = false;
            this.resOrder.Text = "Orders";
            this.resOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.resOrder.UseVisualStyleBackColor = false;
            this.resOrder.Click += new System.EventHandler(this.resOrder_Click);
            // 
            // resCollection
            // 
            this.resCollection.BackColor = System.Drawing.Color.Transparent;
            this.resCollection.FlatAppearance.BorderSize = 0;
            this.resCollection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resCollection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resCollection.Image = global::TomaFoodRestaurant.Properties.Resources.waiter_24px;
            this.resCollection.Location = new System.Drawing.Point(437, 3);
            this.resCollection.Name = "resCollection";
            this.resCollection.Size = new System.Drawing.Size(86, 62);
            this.resCollection.TabIndex = 21;
            this.resCollection.TabStop = false;
            this.resCollection.Text = "Collection";
            this.resCollection.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.resCollection.UseVisualStyleBackColor = false;
            this.resCollection.Click += new System.EventHandler(this.resCollection_Click);
            // 
            // resDelivery
            // 
            this.resDelivery.BackColor = System.Drawing.Color.Transparent;
            this.resDelivery.FlatAppearance.BorderSize = 0;
            this.resDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resDelivery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resDelivery.Image = global::TomaFoodRestaurant.Properties.Resources.supplier_24px;
            this.resDelivery.Location = new System.Drawing.Point(529, 3);
            this.resDelivery.Name = "resDelivery";
            this.resDelivery.Size = new System.Drawing.Size(72, 62);
            this.resDelivery.TabIndex = 22;
            this.resDelivery.TabStop = false;
            this.resDelivery.Text = "Delivery";
            this.resDelivery.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.resDelivery.UseVisualStyleBackColor = false;
            this.resDelivery.Click += new System.EventHandler(this.resDelivery_Click);
            // 
            // resSetup
            // 
            this.resSetup.BackColor = System.Drawing.Color.Transparent;
            this.resSetup.FlatAppearance.BorderSize = 0;
            this.resSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resSetup.Image = global::TomaFoodRestaurant.Properties.Resources.order_history_24px;
            this.resSetup.Location = new System.Drawing.Point(607, 3);
            this.resSetup.Name = "resSetup";
            this.resSetup.Size = new System.Drawing.Size(61, 62);
            this.resSetup.TabIndex = 23;
            this.resSetup.TabStop = false;
            this.resSetup.Text = "Setup";
            this.resSetup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.resSetup.UseVisualStyleBackColor = false;
            this.resSetup.Click += new System.EventHandler(this.resSetup_Click);
            // 
            // QuickMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 114);
            this.ControlBox = false;
            this.Controls.Add(this.panelDropDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(675, 43);
            this.Name = "QuickMenu";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.panelDropDown.ResumeLayout(false);
            this.customFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button ExpandCollapse;
        private System.Windows.Forms.Panel panelDropDown;
        private Model.CustomFlowLayoutPanel customFlowLayoutPanel1;
        private System.Windows.Forms.Button home;
        private System.Windows.Forms.Button Table;
        private System.Windows.Forms.Button OnlineOrder;
        private System.Windows.Forms.Button changePrice;
        private System.Windows.Forms.Button resOrder;
        private System.Windows.Forms.Button resCollection;
        private System.Windows.Forms.Button resDelivery;
        private System.Windows.Forms.Button resSetup;
    }
}