namespace TomaFoodRestaurant.OtherForm
{
    partial class AttributeForm
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
            this.attributeFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // attributeFlowLayoutPanel
            // 
            this.attributeFlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.attributeFlowLayoutPanel.Location = new System.Drawing.Point(23, 12);
            this.attributeFlowLayoutPanel.MinimumSize = new System.Drawing.Size(891, 250);
            this.attributeFlowLayoutPanel.Name = "attributeFlowLayoutPanel";
            this.attributeFlowLayoutPanel.Size = new System.Drawing.Size(891, 250);
            this.attributeFlowLayoutPanel.TabIndex = 1;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Location = new System.Drawing.Point(23, 422);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(211, 83);
            this.buttonPanel.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(87)))), ((int)(((byte)(63)))));
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.cancelButton.Location = new System.Drawing.Point(16, 10);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(180, 64);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(945, 517);
            this.ControlBox = false;
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.attributeFlowLayoutPanel);
            this.Name = "AttributeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AttributeForm_Load);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel attributeFlowLayoutPanel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button cancelButton;
    }
}