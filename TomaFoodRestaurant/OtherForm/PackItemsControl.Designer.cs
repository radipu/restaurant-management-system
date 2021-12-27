namespace TomaFoodRestaurant.OtherForm
{
    partial class PackItemsControl
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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.qtyTextBox = new System.Windows.Forms.TextBox();
            this.packageOptionsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // totalPriceLabel
            // 
            this.totalPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalPriceLabel.Location = new System.Drawing.Point(178, 2);
            this.totalPriceLabel.Name = "totalPriceLabel";
            this.totalPriceLabel.Size = new System.Drawing.Size(39, 19);
            this.totalPriceLabel.TabIndex = 11;
            this.totalPriceLabel.Text = "label1";
            this.totalPriceLabel.TextChanged += new System.EventHandler(this.totalPriceLabel_TextChanged);
            this.totalPriceLabel.Click += new System.EventHandler(this.totalPriceLabel_Click);
            this.totalPriceLabel.DoubleClick += new System.EventHandler(this.totalPriceLabel_DoubleClick);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextBox.Location = new System.Drawing.Point(32, 2);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(140, 21);
            this.nameTextBox.TabIndex = 9;
            this.nameTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameTextBox_MouseClick);
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // qtyTextBox
            // 
            this.qtyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qtyTextBox.Location = new System.Drawing.Point(13, 2);
            this.qtyTextBox.Name = "qtyTextBox";
            this.qtyTextBox.ReadOnly = true;
            this.qtyTextBox.Size = new System.Drawing.Size(17, 21);
            this.qtyTextBox.TabIndex = 8;
            // 
            // packageOptionsLabel
            // 
            this.packageOptionsLabel.AutoSize = true;
            this.packageOptionsLabel.Location = new System.Drawing.Point(18, 25);
            this.packageOptionsLabel.Name = "packageOptionsLabel";
            this.packageOptionsLabel.Size = new System.Drawing.Size(22, 13);
            this.packageOptionsLabel.TabIndex = 12;
            this.packageOptionsLabel.Text = ".....";
            // 
            // PackItemsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.packageOptionsLabel);
            this.Controls.Add(this.totalPriceLabel);
            this.Controls.Add(this.qtyTextBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PackItemsControl";
            this.Size = new System.Drawing.Size(220, 38);
            this.Click += new System.EventHandler(this.PackItemsControl_Click);
            this.DoubleClick += new System.EventHandler(this.PackItemsControl_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label totalPriceLabel;
        public System.Windows.Forms.TextBox nameTextBox;
        public System.Windows.Forms.TextBox qtyTextBox;
        public System.Windows.Forms.Label packageOptionsLabel;
    }
}
