namespace TomaFoodRestaurant.OtherForm
{
    partial class MultiplePartControl
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
            this.packageItemsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.totalPriceLabel = new System.Windows.Forms.Label();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.qtyTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // packageItemsFlowLayoutPanel
            // 
            this.packageItemsFlowLayoutPanel.AutoSize = true;
            this.packageItemsFlowLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.packageItemsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.packageItemsFlowLayoutPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.packageItemsFlowLayoutPanel.Location = new System.Drawing.Point(0, 25);
            this.packageItemsFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.packageItemsFlowLayoutPanel.Name = "packageItemsFlowLayoutPanel";
            this.packageItemsFlowLayoutPanel.Size = new System.Drawing.Size(234, 19);
            this.packageItemsFlowLayoutPanel.TabIndex = 13;
            this.packageItemsFlowLayoutPanel.WrapContents = false;
            this.packageItemsFlowLayoutPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameTextBox_MouseClick);
            // 
            // totalPriceLabel
            // 
            this.totalPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalPriceLabel.Location = new System.Drawing.Point(211, 6);
            this.totalPriceLabel.Margin = new System.Windows.Forms.Padding(0);
            this.totalPriceLabel.Name = "totalPriceLabel";
            this.totalPriceLabel.Size = new System.Drawing.Size(38, 13);
            this.totalPriceLabel.TabIndex = 12;
            this.totalPriceLabel.Text = "label1";
            // 
            // priceTextBox
            // 
            this.priceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceTextBox.Location = new System.Drawing.Point(170, 3);
            this.priceTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.Size = new System.Drawing.Size(37, 21);
            this.priceTextBox.TabIndex = 11;
            this.priceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.priceTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.qtyTextBox_MouseClick);
            this.priceTextBox.TextChanged += new System.EventHandler(this.priceTextBox_TextChanged);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextBox.Location = new System.Drawing.Point(34, 2);
            this.nameTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(134, 21);
            this.nameTextBox.TabIndex = 10;
            this.nameTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameTextBox_MouseClick);
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // qtyTextBox
            // 
            this.qtyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qtyTextBox.Location = new System.Drawing.Point(2, 2);
            this.qtyTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.qtyTextBox.Name = "qtyTextBox";
            this.qtyTextBox.Size = new System.Drawing.Size(32, 21);
            this.qtyTextBox.TabIndex = 9;
            this.qtyTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.qtyTextBox_MouseClick);
            this.qtyTextBox.TextChanged += new System.EventHandler(this.qtyTextBox_TextChanged);
            // 
            // MultiplePartControl
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
            this.Name = "MultiplePartControl";
            this.Size = new System.Drawing.Size(249, 44);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameTextBox_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel packageItemsFlowLayoutPanel;
        public System.Windows.Forms.Label totalPriceLabel;
        public System.Windows.Forms.TextBox priceTextBox;
        public System.Windows.Forms.TextBox nameTextBox;
        public System.Windows.Forms.TextBox qtyTextBox;

    }
}
