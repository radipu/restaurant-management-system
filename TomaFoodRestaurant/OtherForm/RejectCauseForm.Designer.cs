namespace TomaFoodRestaurant.OtherForm
{
    partial class RejectCauseForm
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
            this.backButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.causesTextBox = new System.Windows.Forms.TextBox();
            this.labelfooter = new System.Windows.Forms.Label();
            this.numberPadUs1 = new TomaFoodRestaurant.OtherForm.NumberPadUs();
            this.buttonRefund = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Maroon;
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.ForeColor = System.Drawing.Color.White;
            this.backButton.Location = new System.Drawing.Point(4, 272);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(217, 48);
            this.backButton.TabIndex = 7;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.acceptButton.FlatAppearance.BorderSize = 0;
            this.acceptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.acceptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acceptButton.ForeColor = System.Drawing.Color.White;
            this.acceptButton.Location = new System.Drawing.Point(811, 272);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(194, 48);
            this.acceptButton.TabIndex = 6;
            this.acceptButton.Text = "Reject";
            this.acceptButton.UseVisualStyleBackColor = false;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(193, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(761, 46);
            this.label1.TabIndex = 5;
            this.label1.Text = "Hi {Customer Name},\r\nUnfortunately, we are unable to process your order at this t" +
    "ime  for the following reason. ";
            // 
            // causesTextBox
            // 
            this.causesTextBox.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.causesTextBox.Location = new System.Drawing.Point(197, 76);
            this.causesTextBox.Multiline = true;
            this.causesTextBox.Name = "causesTextBox";
            this.causesTextBox.Size = new System.Drawing.Size(592, 73);
            this.causesTextBox.TabIndex = 8;
            this.causesTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.causesTextBox_MouseClick);
            // 
            // labelfooter
            // 
            this.labelfooter.AutoSize = true;
            this.labelfooter.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelfooter.Location = new System.Drawing.Point(193, 152);
            this.labelfooter.Name = "labelfooter";
            this.labelfooter.Size = new System.Drawing.Size(473, 115);
            this.labelfooter.TabIndex = 10;
            this.labelfooter.Text = "Sorry for any inconvenience caused.\r\nFor more information please call us {restaur" +
    "ant phone}\r\nKind Regards\r\n{restaurant name}\r\n  \r\n";
            // 
            // numberPadUs1
            // 
            this.numberPadUs1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.numberPadUs1.ControlToInputText = null;
            this.numberPadUs1.Location = new System.Drawing.Point(4, 326);
            this.numberPadUs1.Name = "numberPadUs1";
            this.numberPadUs1.Size = new System.Drawing.Size(1001, 280);
            this.numberPadUs1.TabIndex = 9;
            this.numberPadUs1.textBox = null;
            // 
            // buttonRefund
            // 
            this.buttonRefund.BackColor = System.Drawing.Color.Gold;
            this.buttonRefund.FlatAppearance.BorderSize = 0;
            this.buttonRefund.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefund.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefund.ForeColor = System.Drawing.Color.Black;
            this.buttonRefund.Location = new System.Drawing.Point(611, 272);
            this.buttonRefund.Name = "buttonRefund";
            this.buttonRefund.Size = new System.Drawing.Size(194, 48);
            this.buttonRefund.TabIndex = 11;
            this.buttonRefund.Text = "Reject With Refund";
            this.buttonRefund.UseVisualStyleBackColor = false;
            this.buttonRefund.Click += new System.EventHandler(this.buttonRefund_Click);
            // 
            // RejectCauseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.ControlBox = false;
            this.Controls.Add(this.buttonRefund);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.labelfooter);
            this.Controls.Add(this.numberPadUs1);
            this.Controls.Add(this.causesTextBox);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.label1);
            this.Name = "RejectCauseForm";
            this.ShowInTaskbar = false;
            this.Text = "RejectCauseForm";
            this.Load += new System.EventHandler(this.RejectCauseForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Label label1;
        private NumberPadUs numberPadUs1;
        public System.Windows.Forms.TextBox causesTextBox;
        private System.Windows.Forms.Label labelfooter;
        private System.Windows.Forms.Button buttonRefund;
    }
}