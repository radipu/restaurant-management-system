namespace TomaFoodRestaurant
{
    partial class deatilsControls
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
            this.qtyTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.totalPriceLabel = new System.Windows.Forms.Label();
            this.optionItemLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // qtyTextBox
            // 
            this.qtyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qtyTextBox.Location = new System.Drawing.Point(4, 3);
            this.qtyTextBox.Name = "qtyTextBox";
            this.qtyTextBox.Size = new System.Drawing.Size(31, 31);
            this.qtyTextBox.TabIndex = 0;
            this.qtyTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.qtyTextBox_MouseClick);
            this.qtyTextBox.TextChanged += new System.EventHandler(this.qtyTextBox_TextChanged);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextBox.Location = new System.Drawing.Point(35, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(134, 31);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.nameTextBox_MouseClick);
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // priceTextBox
            // 
            this.priceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceTextBox.Location = new System.Drawing.Point(170, 3);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.Size = new System.Drawing.Size(37, 31);
            this.priceTextBox.TabIndex = 2;
            this.priceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.priceTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.qtyTextBox_MouseClick);
            this.priceTextBox.TextChanged += new System.EventHandler(this.priceTextBox_TextChanged);
            // 
            // totalPriceLabel
            // 
            this.totalPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalPriceLabel.Location = new System.Drawing.Point(210, 7);
            this.totalPriceLabel.Margin = new System.Windows.Forms.Padding(0);
            this.totalPriceLabel.Name = "totalPriceLabel";
            this.totalPriceLabel.Size = new System.Drawing.Size(38, 31);
            this.totalPriceLabel.TabIndex = 3;
            this.totalPriceLabel.Text = "label1";
            // 
            // optionItemLabel
            // 
            this.optionItemLabel.AutoSize = true;
            this.optionItemLabel.BackColor = System.Drawing.SystemColors.Control;
            this.optionItemLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionItemLabel.Location = new System.Drawing.Point(8, 27);
            this.optionItemLabel.Name = "optionItemLabel";
            this.optionItemLabel.Size = new System.Drawing.Size(51, 31);
            this.optionItemLabel.TabIndex = 4;
            this.optionItemLabel.Text = "label1";
            // 
            // deatilsControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.optionItemLabel);
            this.Controls.Add(this.totalPriceLabel);
            this.Controls.Add(this.priceTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.qtyTextBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "deatilsControls";
            this.Size = new System.Drawing.Size(248, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox qtyTextBox;
        public System.Windows.Forms.TextBox nameTextBox;
        public System.Windows.Forms.TextBox priceTextBox;
        public System.Windows.Forms.Label totalPriceLabel;
        public System.Windows.Forms.Label optionItemLabel;

    }
}
