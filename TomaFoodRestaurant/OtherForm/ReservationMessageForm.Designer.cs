namespace TomaFoodRestaurant.OtherForm
{
    partial class ReservationMessageForm
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
            this.sendEmailTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.numberPadUs1 = new TomaFoodRestaurant.OtherForm.NumberPadUs();
            this.SuspendLayout();
            // 
            // sendEmailTextBox
            // 
            this.sendEmailTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.sendEmailTextBox.Location = new System.Drawing.Point(123, 72);
            this.sendEmailTextBox.Multiline = true;
            this.sendEmailTextBox.Name = "sendEmailTextBox";
            this.sendEmailTextBox.Size = new System.Drawing.Size(648, 125);
            this.sendEmailTextBox.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(12, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 20);
            this.label9.TabIndex = 39;
            this.label9.Text = "Email Text";
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(175)))), ((int)(((byte)(218)))));
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.ForeColor = System.Drawing.Color.Transparent;
            this.saveButton.Location = new System.Drawing.Point(429, 230);
            this.saveButton.Margin = new System.Windows.Forms.Padding(0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(342, 54);
            this.saveButton.TabIndex = 37;
            this.saveButton.Text = "Send";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(87)))), ((int)(((byte)(63)))));
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.Transparent;
            this.closeButton.Location = new System.Drawing.Point(123, 230);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(289, 54);
            this.closeButton.TabIndex = 36;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // numberPadUs1
            // 
            this.numberPadUs1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.numberPadUs1.ControlToInputText = null;
            this.numberPadUs1.Location = new System.Drawing.Point(2, 287);
            this.numberPadUs1.Name = "numberPadUs1";
            this.numberPadUs1.Size = new System.Drawing.Size(1001, 280);
            this.numberPadUs1.TabIndex = 41;
            this.numberPadUs1.textBox = null;
            // 
            // ReservationMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(1006, 570);
            this.ControlBox = false;
            this.Controls.Add(this.numberPadUs1);
            this.Controls.Add(this.sendEmailTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.closeButton);
            this.Name = "ReservationMessageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReservationMessageForm";
            this.Load += new System.EventHandler(this.ReservationMessageForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sendEmailTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button closeButton;
        private NumberPadUs numberPadUs1;
    }
}