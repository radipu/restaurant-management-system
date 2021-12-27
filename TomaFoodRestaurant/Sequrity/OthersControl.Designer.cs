namespace TomaFoodRestaurant.Sequrity
{
    partial class OthersControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResTime = new DevExpress.XtraEditors.TextEdit();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtResTime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(206, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Control Form";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 49);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Res Time Set";
            // 
            // txtResTime
            // 
            this.txtResTime.Location = new System.Drawing.Point(212, 72);
            this.txtResTime.Name = "txtResTime";
            this.txtResTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 16F);
            this.txtResTime.Properties.Appearance.Options.UseFont = true;
            this.txtResTime.Size = new System.Drawing.Size(177, 32);
            this.txtResTime.TabIndex = 3;
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.BackColor = System.Drawing.Color.LightGreen;
            this.btn_Save.Appearance.Font = new System.Drawing.Font("Sitka Small", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_Save.Appearance.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.Appearance.Options.UseForeColor = true;
            this.btn_Save.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btn_Save.Location = new System.Drawing.Point(212, 110);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(177, 38);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "SAVE";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // OthersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 299);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.txtResTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "OthersControl";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OthersControl";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtResTime.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtResTime;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
    }
}