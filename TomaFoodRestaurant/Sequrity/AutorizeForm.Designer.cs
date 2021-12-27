namespace TomaFoodRestaurant.Sequrity
{
    partial class AutorizeForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btncancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnsubmit = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.panelControl1.Appearance.BackColor2 = System.Drawing.Color.DarkKhaki;
            this.panelControl1.Appearance.BorderColor = System.Drawing.Color.Red;
            this.panelControl1.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Appearance.Options.UseBorderColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblHeading);
            this.panelControl1.Controls.Add(this.lblMessage);
            this.panelControl1.Controls.Add(this.btncancel);
            this.panelControl1.Controls.Add(this.btnsubmit);
            this.panelControl1.Controls.Add(this.txtPassword);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(354, 154);
            this.panelControl1.TabIndex = 0;
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHeading.Location = new System.Drawing.Point(48, 9);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(253, 20);
            this.lblHeading.TabIndex = 3;
            this.lblHeading.Text = "Do you want to Overwrite this Price";
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(3)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(94, 113);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(68, 19);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMessage.Visible = false;
            // 
            // btncancel
            // 
            this.btncancel.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btncancel.Appearance.Options.UseFont = true;
            this.btncancel.Location = new System.Drawing.Point(62, 74);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(113, 32);
            this.btncancel.TabIndex = 1;
            this.btncancel.Text = "CANCEL";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnsubmit
            // 
            this.btnsubmit.Appearance.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnsubmit.Appearance.Options.UseFont = true;
            this.btnsubmit.Location = new System.Drawing.Point(188, 74);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(113, 32);
            this.btnsubmit.TabIndex = 1;
            this.btnsubmit.Text = "SUBMIT";
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(62, 42);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(239, 26);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.Click += new System.EventHandler(this.txtPassword_Click);
            // 
            // AutorizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 154);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "AutorizeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AutorizeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AutorizeForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblMessage;
        private DevExpress.XtraEditors.SimpleButton btncancel;
        private DevExpress.XtraEditors.SimpleButton btnsubmit;
        private DevExpress.XtraEditors.TextEdit txtPassword;
    }
}
