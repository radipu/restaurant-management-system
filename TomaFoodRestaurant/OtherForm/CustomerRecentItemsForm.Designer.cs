namespace TomaFoodRestaurant.OtherForm
{
    partial class CustomerRecentItemsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.itemsflowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.closeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(877, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Recent Used Items";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // itemsflowLayoutPanel
            // 
            this.itemsflowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsflowLayoutPanel.Location = new System.Drawing.Point(8, 49);
            this.itemsflowLayoutPanel.Name = "itemsflowLayoutPanel";
            this.itemsflowLayoutPanel.Size = new System.Drawing.Size(873, 244);
            this.itemsflowLayoutPanel.TabIndex = 1;
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(87)))), ((int)(((byte)(63)))));
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.Transparent;
            this.closeButton.Location = new System.Drawing.Point(181, 301);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(160, 54);
            this.closeButton.TabIndex = 37;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(8, 299);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(160, 54);
            this.addButton.TabIndex = 38;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // CustomerRecentItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(888, 449);
            this.ControlBox = false;
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.itemsflowLayoutPanel);
            this.Controls.Add(this.label1);
            this.Name = "CustomerRecentItemsForm";
            this.ShowInTaskbar = false;
            this.Text = "CustomerRecentItemsForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CustomerRecentItemsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel itemsflowLayoutPanel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button addButton;
    }
}