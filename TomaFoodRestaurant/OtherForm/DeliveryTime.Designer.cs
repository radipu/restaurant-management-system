namespace TomaFoodRestaurant.OtherForm
{
    partial class DeliveryTime
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
            this.timeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timeComboBox
            // 
            this.timeComboBox.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeComboBox.FormattingEnabled = true;
            this.timeComboBox.Location = new System.Drawing.Point(102, 57);
            this.timeComboBox.Name = "timeComboBox";
            this.timeComboBox.Size = new System.Drawing.Size(266, 31);
            this.timeComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "TIME ";
            // 
            // acceptButton
            // 
            this.acceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.acceptButton.FlatAppearance.BorderSize = 0;
            this.acceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.acceptButton.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acceptButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.acceptButton.Location = new System.Drawing.Point(235, 181);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(133, 34);
            this.acceptButton.TabIndex = 2;
            this.acceptButton.Text = "ACCEPT";
            this.acceptButton.UseVisualStyleBackColor = false;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Orange;
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backButton.Location = new System.Drawing.Point(12, 181);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(118, 34);
            this.backButton.TabIndex = 3;
            this.backButton.Text = "BACK";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // DeliveryTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 227);
            this.ControlBox = false;
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeComboBox);
            this.Name = "DeliveryTime";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeliveryTime";
            this.Load += new System.EventHandler(this.DeliveryTime_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox timeComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button backButton;
    }
}