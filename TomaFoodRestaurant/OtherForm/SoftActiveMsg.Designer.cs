namespace TomaFoodRestaurant.OtherForm
{
    partial class SoftActiveMsg
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(14, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(610, 235);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(295, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "---------------------------------------------------------------------------------" +
    "---------------";
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(29)))));
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.ForeColor = System.Drawing.Color.White;
            this.exitButton.Location = new System.Drawing.Point(483, 176);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(96, 36);
            this.exitButton.TabIndex = 6;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(480, 92);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please contact Ginilab Ltd.\r\n\r\n075333 44425 | 075063 60870 |  019130 82121\r\nconta" +
    "ct@ginilab.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Verdana", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(156, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Software expired !";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SoftActiveMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(640, 261);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SoftActiveMsg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SoftActiveMsg";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}