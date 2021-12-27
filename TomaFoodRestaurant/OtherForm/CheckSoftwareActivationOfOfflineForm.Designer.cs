namespace TomaFoodRestaurant.OtherForm
{
    partial class CheckSoftwareActivationOfOfflineForm
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
            this.statusLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.activeNowButton = new System.Windows.Forms.Button();
            this.licenseCodeTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numberPadUs1 = new TomaFoodRestaurant.OtherForm.NumberPadUs();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(386, 116);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(16, 13);
            this.statusLabel.TabIndex = 15;
            this.statusLabel.Text = "...";
            this.statusLabel.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Red;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(550, 152);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(129, 53);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // activeNowButton
            // 
            this.activeNowButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.activeNowButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.activeNowButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activeNowButton.ForeColor = System.Drawing.Color.White;
            this.activeNowButton.Location = new System.Drawing.Point(389, 152);
            this.activeNowButton.Name = "activeNowButton";
            this.activeNowButton.Size = new System.Drawing.Size(134, 53);
            this.activeNowButton.TabIndex = 13;
            this.activeNowButton.Text = "Activate Now";
            this.activeNowButton.UseVisualStyleBackColor = false;
            this.activeNowButton.Click += new System.EventHandler(this.activeNowButton_Click);
            // 
            // licenseCodeTextBox
            // 
            this.licenseCodeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.licenseCodeTextBox.Location = new System.Drawing.Point(388, 75);
            this.licenseCodeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.licenseCodeTextBox.Name = "licenseCodeTextBox";
            this.licenseCodeTextBox.Size = new System.Drawing.Size(291, 26);
            this.licenseCodeTextBox.TabIndex = 12;
            this.licenseCodeTextBox.Click += new System.EventHandler(this.licenseCodeTextBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(279, 81);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "License Code";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(996, 31);
            this.label1.TabIndex = 16;
            this.label1.Text = "Information Of Software Activation";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numberPadUs1
            // 
            this.numberPadUs1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.numberPadUs1.ControlToInputText = null;
            this.numberPadUs1.Location = new System.Drawing.Point(-3, 244);
            this.numberPadUs1.Name = "numberPadUs1";
            this.numberPadUs1.Size = new System.Drawing.Size(1001, 280);
            this.numberPadUs1.TabIndex = 17;
            this.numberPadUs1.textBox = null;
            // 
            // CheckSoftwareActivationOfOfflineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.ControlBox = false;
            this.Controls.Add(this.numberPadUs1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.activeNowButton);
            this.Controls.Add(this.licenseCodeTextBox);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CheckSoftwareActivationOfOfflineForm";
            this.ShowInTaskbar = false;
            this.Text = "CheckSoftwareActivationOfOfflineForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CheckSoftwareActivationOfOfflineForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button activeNowButton;
        private System.Windows.Forms.TextBox licenseCodeTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private NumberPadUs numberPadUs1;
    }
}