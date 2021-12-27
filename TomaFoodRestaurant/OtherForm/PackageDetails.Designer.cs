namespace TomaFoodRestaurant.OtherForm
{
    partial class PackageDetails
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.totalPriceLabel = new System.Windows.Forms.Label();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.qtyTextBox = new System.Windows.Forms.TextBox();
            this.packageItemsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // totalPriceLabel
            // 
            this.totalPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalPriceLabel.Location = new System.Drawing.Point(202, 8);
            this.totalPriceLabel.Margin = new System.Windows.Forms.Padding(0);
            this.totalPriceLabel.Name = "totalPriceLabel";
            this.totalPriceLabel.Size = new System.Drawing.Size(40, 13);
            this.totalPriceLabel.TabIndex = 7;
            this.totalPriceLabel.Text = "label1";
            this.totalPriceLabel.Click += new System.EventHandler(this.totalPriceLabel_Click);
            this.totalPriceLabel.DoubleClick += new System.EventHandler(this.totalPriceLabel_DoubleClick);
            // 
            // priceTextBox
            // 
            this.priceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceTextBox.Location = new System.Drawing.Point(164, 5);
            this.priceTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.Size = new System.Drawing.Size(37, 21);
            this.priceTextBox.TabIndex = 6;
            this.priceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.priceTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.qtyTextBox_MouseClick);
            this.priceTextBox.TextChanged += new System.EventHandler(this.priceTextBox_TextChanged);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold);
            this.nameTextBox.Location = new System.Drawing.Point(35, 4);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(126, 21);
            this.nameTextBox.TabIndex = 5;
            this.nameTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameTextBox_MouseClick);
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // qtyTextBox
            // 
            this.qtyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qtyTextBox.Location = new System.Drawing.Point(3, 4);
            this.qtyTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.qtyTextBox.Name = "qtyTextBox";
            this.qtyTextBox.Size = new System.Drawing.Size(31, 21);
            this.qtyTextBox.TabIndex = 4;
            this.qtyTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.qtyTextBox_MouseClick);
            this.qtyTextBox.TextChanged += new System.EventHandler(this.qtyTextBox_TextChanged);
            // 
            // packageItemsFlowLayoutPanel
            // 
            this.packageItemsFlowLayoutPanel.AutoSize = true;
            this.packageItemsFlowLayoutPanel.BackColor = System.Drawing.Color.Green;
            this.packageItemsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.packageItemsFlowLayoutPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.packageItemsFlowLayoutPanel.Location = new System.Drawing.Point(1, 27);
            this.packageItemsFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.packageItemsFlowLayoutPanel.Name = "packageItemsFlowLayoutPanel";
            this.packageItemsFlowLayoutPanel.Size = new System.Drawing.Size(237, 19);
            this.packageItemsFlowLayoutPanel.TabIndex = 8;
            this.packageItemsFlowLayoutPanel.WrapContents = false;
            // 
            // PackageDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.packageItemsFlowLayoutPanel);
            this.Controls.Add(this.totalPriceLabel);
            this.Controls.Add(this.priceTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.qtyTextBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PackageDetails";
            this.Size = new System.Drawing.Size(242, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label totalPriceLabel;
        public System.Windows.Forms.TextBox priceTextBox;
        public System.Windows.Forms.TextBox nameTextBox;
        public System.Windows.Forms.TextBox qtyTextBox;
        public System.Windows.Forms.FlowLayoutPanel packageItemsFlowLayoutPanel;
    }
}
