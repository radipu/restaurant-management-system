using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TomaFoodRestaurant.OtherForm
{
    partial class PackageItemForm
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
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.comboBoxQty = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.attributeFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.receipeCategoryFlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.itemDetailsFlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.packageNamelabel1 = new System.Windows.Forms.Label();
            this.packageCategoryFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.rightRecepiflowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.comboBoxQty);
            this.buttonPanel.Controls.Add(this.button2);
            this.buttonPanel.Controls.Add(this.button1);
            this.buttonPanel.Controls.Add(this.addButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Location = new System.Drawing.Point(802, 99);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(226, 129);
            this.buttonPanel.TabIndex = 8;
            // 
            // comboBoxQty
            // 
            this.comboBoxQty.AutoSize = true;
            this.comboBoxQty.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxQty.Location = new System.Drawing.Point(101, 16);
            this.comboBoxQty.Name = "comboBoxQty";
            this.comboBoxQty.Size = new System.Drawing.Size(28, 29);
            this.comboBoxQty.TabIndex = 2;
            this.comboBoxQty.Text = "1";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.ForestGreen;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(147, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 47);
            this.button2.TabIndex = 1;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(16, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 47);
            this.button1.TabIndex = 1;
            this.button1.Text = "-";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(16, 64);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(96, 50);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(187)))), ((int)(((byte)(66)))));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(118, 64);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(96, 50);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // attributeFlowLayoutPanel
            // 
            this.attributeFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attributeFlowLayoutPanel.AutoSize = true;
            this.attributeFlowLayoutPanel.Location = new System.Drawing.Point(21, 72);
            this.attributeFlowLayoutPanel.Name = "attributeFlowLayoutPanel";
            this.attributeFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 0, 60, 0);
            this.attributeFlowLayoutPanel.Size = new System.Drawing.Size(88, 0);
            this.attributeFlowLayoutPanel.TabIndex = 7;
            // 
            // receipeCategoryFlowLayoutPanel1
            // 
            this.receipeCategoryFlowLayoutPanel1.AutoSize = true;
            this.receipeCategoryFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.receipeCategoryFlowLayoutPanel1.Location = new System.Drawing.Point(439, 170);
            this.receipeCategoryFlowLayoutPanel1.Name = "receipeCategoryFlowLayoutPanel1";
            this.receipeCategoryFlowLayoutPanel1.Size = new System.Drawing.Size(30, 0);
            this.receipeCategoryFlowLayoutPanel1.TabIndex = 10;
            // 
            // itemDetailsFlowLayoutPanel1
            // 
            this.itemDetailsFlowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.itemDetailsFlowLayoutPanel1.AutoSize = true;
            this.itemDetailsFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.itemDetailsFlowLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.itemDetailsFlowLayoutPanel1.Location = new System.Drawing.Point(800, 38);
            this.itemDetailsFlowLayoutPanel1.Name = "itemDetailsFlowLayoutPanel1";
            this.itemDetailsFlowLayoutPanel1.Size = new System.Drawing.Size(228, 51);
            this.itemDetailsFlowLayoutPanel1.TabIndex = 11;
            // 
            // packageNamelabel1
            // 
            this.packageNamelabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.packageNamelabel1.AutoSize = true;
            this.packageNamelabel1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.packageNamelabel1.Location = new System.Drawing.Point(800, 9);
            this.packageNamelabel1.Name = "packageNamelabel1";
            this.packageNamelabel1.Size = new System.Drawing.Size(63, 25);
            this.packageNamelabel1.TabIndex = 12;
            this.packageNamelabel1.Text = "label1";
            // 
            // packageCategoryFlowLayoutPanel
            // 
            this.packageCategoryFlowLayoutPanel.AutoSize = true;
            this.packageCategoryFlowLayoutPanel.Location = new System.Drawing.Point(21, 12);
            this.packageCategoryFlowLayoutPanel.Name = "packageCategoryFlowLayoutPanel";
            this.packageCategoryFlowLayoutPanel.Size = new System.Drawing.Size(245, 60);
            this.packageCategoryFlowLayoutPanel.TabIndex = 9;
            // 
            // rightRecepiflowLayoutPanel
            // 
            this.rightRecepiflowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightRecepiflowLayoutPanel.AutoScroll = true;
            this.rightRecepiflowLayoutPanel.AutoSize = true;
            this.rightRecepiflowLayoutPanel.Location = new System.Drawing.Point(567, 105);
            this.rightRecepiflowLayoutPanel.MaximumSize = new System.Drawing.Size(0, 670);
            this.rightRecepiflowLayoutPanel.Name = "rightRecepiflowLayoutPanel";
            this.rightRecepiflowLayoutPanel.Size = new System.Drawing.Size(0, 0);
            this.rightRecepiflowLayoutPanel.TabIndex = 14;
            // 
            // PackageItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1036, 725);
            this.ControlBox = false;
            this.Controls.Add(this.rightRecepiflowLayoutPanel);
            this.Controls.Add(this.packageNamelabel1);
            this.Controls.Add(this.itemDetailsFlowLayoutPanel1);
            this.Controls.Add(this.receipeCategoryFlowLayoutPanel1);
            this.Controls.Add(this.packageCategoryFlowLayoutPanel);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.attributeFlowLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PackageItemForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "    ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PackageItemForm_Load);
            this.buttonPanel.ResumeLayout(false);
            this.buttonPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.FlowLayoutPanel receipeCategoryFlowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel itemDetailsFlowLayoutPanel1;
        private System.Windows.Forms.Label packageNamelabel1;
        public System.Windows.Forms.FlowLayoutPanel attributeFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel packageCategoryFlowLayoutPanel;
        public FlowLayoutPanel rightRecepiflowLayoutPanel;
        private Label comboBoxQty;
        private Button button2;
        private Button button1;
    }
}