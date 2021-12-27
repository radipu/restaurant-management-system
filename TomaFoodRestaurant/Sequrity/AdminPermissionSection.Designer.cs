namespace TomaFoodRestaurant.Sequrity
{
    partial class AdminPermissionSection
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnsubmit = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblMessage);
            this.groupControl1.Controls.Add(this.panel1);
            this.groupControl1.Controls.Add(this.btncancel);
            this.groupControl1.Controls.Add(this.btnsubmit);
            this.groupControl1.Controls.Add(this.txtPassword);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(578, 240);
            this.groupControl1.TabIndex = 0;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(3)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(91, 188);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(68, 19);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMessage.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(2, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 59);
            this.panel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(87, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "PERMISSION FOR CHANGE SETTING";
            // 
            // btncancel
            // 
            this.btncancel.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btncancel.Appearance.Options.UseFont = true;
            this.btncancel.Location = new System.Drawing.Point(165, 131);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(113, 32);
            this.btncancel.TabIndex = 4;
            this.btncancel.Text = "CANCEL";
            // 
            // btnsubmit
            // 
            this.btnsubmit.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnsubmit.Appearance.Options.UseFont = true;
            this.btnsubmit.Location = new System.Drawing.Point(291, 131);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(113, 32);
            this.btnsubmit.TabIndex = 3;
            this.btnsubmit.Text = "CONFORM";
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(165, 99);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(239, 26);
            this.txtPassword.TabIndex = 2;
            // 
            // AdminPermissionSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 240);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminPermissionSection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminPermissionSection";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.XtraEditors.SimpleButton btnsubmit;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
    }
}