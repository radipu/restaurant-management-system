namespace TomaFoodRestaurant.Sequrity
{
    partial class UrlChageForm
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
            this.txtFontEnd = new System.Windows.Forms.TextBox();
            this.txtBackend = new System.Windows.Forms.TextBox();
            this.lblFontEnd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangeUrl = new System.Windows.Forms.Button();
            this.chkAutoDiscount = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtFontEnd
            // 
            this.txtFontEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFontEnd.Location = new System.Drawing.Point(267, 102);
            this.txtFontEnd.Name = "txtFontEnd";
            this.txtFontEnd.Size = new System.Drawing.Size(256, 26);
            this.txtFontEnd.TabIndex = 0;
            this.txtFontEnd.Visible = false;
            // 
            // txtBackend
            // 
            this.txtBackend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackend.Location = new System.Drawing.Point(267, 151);
            this.txtBackend.Name = "txtBackend";
            this.txtBackend.Size = new System.Drawing.Size(256, 26);
            this.txtBackend.TabIndex = 1;
            // 
            // lblFontEnd
            // 
            this.lblFontEnd.AutoSize = true;
            this.lblFontEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFontEnd.Location = new System.Drawing.Point(118, 105);
            this.lblFontEnd.Name = "lblFontEnd";
            this.lblFontEnd.Size = new System.Drawing.Size(127, 20);
            this.lblFontEnd.TabIndex = 2;
            this.lblFontEnd.Text = "FONT-END URL";
            this.lblFontEnd.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(117, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "SERVER URL";
            // 
            // btnChangeUrl
            // 
            this.btnChangeUrl.BackColor = System.Drawing.Color.Gainsboro;
            this.btnChangeUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeUrl.Location = new System.Drawing.Point(336, 224);
            this.btnChangeUrl.Name = "btnChangeUrl";
            this.btnChangeUrl.Size = new System.Drawing.Size(187, 39);
            this.btnChangeUrl.TabIndex = 3;
            this.btnChangeUrl.Text = "CHANGE URL";
            this.btnChangeUrl.UseVisualStyleBackColor = false;
            this.btnChangeUrl.Click += new System.EventHandler(this.btnChangeUrl_Click);
            // 
            // chkAutoDiscount
            // 
            this.chkAutoDiscount.AutoSize = true;
            this.chkAutoDiscount.Font = new System.Drawing.Font("Microsoft JhengHei", 11F, System.Drawing.FontStyle.Bold);
            this.chkAutoDiscount.Location = new System.Drawing.Point(267, 183);
            this.chkAutoDiscount.Name = "chkAutoDiscount";
            this.chkAutoDiscount.Size = new System.Drawing.Size(185, 23);
            this.chkAutoDiscount.TabIndex = 17;
            this.chkAutoDiscount.Text = "Enable Auto Discount";
            this.chkAutoDiscount.UseVisualStyleBackColor = true;
            // 
            // UrlChageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 347);
            this.Controls.Add(this.chkAutoDiscount);
            this.Controls.Add(this.btnChangeUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFontEnd);
            this.Controls.Add(this.txtBackend);
            this.Controls.Add(this.txtFontEnd);
            this.Name = "UrlChageForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UrlChageForm";
            this.Load += new System.EventHandler(this.UrlChageForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFontEnd;
        private System.Windows.Forms.TextBox txtBackend;
        private System.Windows.Forms.Label lblFontEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChangeUrl;
        private System.Windows.Forms.CheckBox chkAutoDiscount;
    }
}