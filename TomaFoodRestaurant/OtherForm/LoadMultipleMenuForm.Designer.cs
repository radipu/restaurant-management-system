namespace TomaFoodRestaurant.OtherForm
{
    partial class LoadMultipleMenuForm
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
            this.itemsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.packageNamelabel1 = new System.Windows.Forms.Label();
            this.itemDetailsFlowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.quaterRoundButton = new TomaFoodRestaurant.Model.RoundButton();
            this.halfRoundButton = new TomaFoodRestaurant.Model.RoundButton();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemsFlowLayoutPanel
            // 
            this.itemsFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsFlowLayoutPanel.Location = new System.Drawing.Point(4, 86);
            this.itemsFlowLayoutPanel.Name = "itemsFlowLayoutPanel";
            this.itemsFlowLayoutPanel.Size = new System.Drawing.Size(771, 607);
            this.itemsFlowLayoutPanel.TabIndex = 2;
            // 
            // packageNamelabel1
            // 
            this.packageNamelabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.packageNamelabel1.AutoSize = true;
            this.packageNamelabel1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.packageNamelabel1.Location = new System.Drawing.Point(779, 9);
            this.packageNamelabel1.Name = "packageNamelabel1";
            this.packageNamelabel1.Size = new System.Drawing.Size(63, 25);
            this.packageNamelabel1.TabIndex = 15;
            this.packageNamelabel1.Text = "label1";
            // 
            // itemDetailsFlowLayoutPanel1
            // 
            this.itemDetailsFlowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.itemDetailsFlowLayoutPanel1.AutoSize = true;
            this.itemDetailsFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.itemDetailsFlowLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 12F);
            this.itemDetailsFlowLayoutPanel1.Location = new System.Drawing.Point(779, 38);
            this.itemDetailsFlowLayoutPanel1.Name = "itemDetailsFlowLayoutPanel1";
            this.itemDetailsFlowLayoutPanel1.Size = new System.Drawing.Size(228, 51);
            this.itemDetailsFlowLayoutPanel1.TabIndex = 14;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPanel.Controls.Add(this.addButton);
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Location = new System.Drawing.Point(781, 99);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(226, 61);
            this.buttonPanel.TabIndex = 13;
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.ForeColor = System.Drawing.Color.White;
            this.addButton.Location = new System.Drawing.Point(23, 6);
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
            this.cancelButton.Location = new System.Drawing.Point(125, 6);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(96, 50);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // quaterRoundButton
            // 
            this.quaterRoundButton.BackgroundImage = global::TomaFoodRestaurant.Properties.Resources.quarter;
            this.quaterRoundButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.quaterRoundButton.FlatAppearance.BorderSize = 0;
            this.quaterRoundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quaterRoundButton.Location = new System.Drawing.Point(151, 5);
            this.quaterRoundButton.Name = "quaterRoundButton";
            this.quaterRoundButton.Size = new System.Drawing.Size(75, 75);
            this.quaterRoundButton.TabIndex = 1;
            this.quaterRoundButton.UseVisualStyleBackColor = true;
            this.quaterRoundButton.Click += new System.EventHandler(this.quaterRoundButton_Click);
            // 
            // halfRoundButton
            // 
            this.halfRoundButton.BackgroundImage = global::TomaFoodRestaurant.Properties.Resources.half;
            this.halfRoundButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.halfRoundButton.FlatAppearance.BorderSize = 0;
            this.halfRoundButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.halfRoundButton.Location = new System.Drawing.Point(43, 5);
            this.halfRoundButton.Name = "halfRoundButton";
            this.halfRoundButton.Size = new System.Drawing.Size(75, 75);
            this.halfRoundButton.TabIndex = 0;
            this.halfRoundButton.UseVisualStyleBackColor = true;
            this.halfRoundButton.Click += new System.EventHandler(this.halfRoundButton_Click);
            // 
            // LoadMultipleMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.ControlBox = false;
            this.Controls.Add(this.packageNamelabel1);
            this.Controls.Add(this.itemDetailsFlowLayoutPanel1);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.itemsFlowLayoutPanel);
            this.Controls.Add(this.quaterRoundButton);
            this.Controls.Add(this.halfRoundButton);
            this.Name = "LoadMultipleMenuForm";
            this.ShowInTaskbar = false;
            this.Text = "LoadMultipleMenuForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LoadMultipleMenuForm_Load);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Model.RoundButton halfRoundButton;
        private Model.RoundButton quaterRoundButton;
        private System.Windows.Forms.FlowLayoutPanel itemsFlowLayoutPanel;
        private System.Windows.Forms.Label packageNamelabel1;
        private System.Windows.Forms.FlowLayoutPanel itemDetailsFlowLayoutPanel1;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
    }
}