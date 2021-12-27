namespace TomaFoodRestaurant.OtherForm
{
    partial class DBsetup
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxdatabase = new System.Windows.Forms.TextBox();
            this.textBoxusername = new System.Windows.Forms.TextBox();
            this.textBoxpassword = new System.Windows.Forms.TextBox();
            this.labelText = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelhide = new System.Windows.Forms.Panel();
            this.buttonCon = new System.Windows.Forms.Button();
            this.txtServerAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDeviceType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.Orange;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonSave.Location = new System.Drawing.Point(346, 271);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(293, 41);
            this.buttonSave.TabIndex = 48;
            this.buttonSave.Text = "SQLITE";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Visible = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(346, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 20);
            this.label5.TabIndex = 47;
            this.label5.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(44, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 46;
            this.label4.Text = "User Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(346, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 45;
            this.label3.Text = "Database Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(46, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 44;
            this.label2.Text = "IP Address";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIP.Location = new System.Drawing.Point(44, 161);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(288, 33);
            this.textBoxIP.TabIndex = 49;
            // 
            // textBoxdatabase
            // 
            this.textBoxdatabase.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxdatabase.Location = new System.Drawing.Point(346, 162);
            this.textBoxdatabase.Name = "textBoxdatabase";
            this.textBoxdatabase.Size = new System.Drawing.Size(293, 33);
            this.textBoxdatabase.TabIndex = 50;
            // 
            // textBoxusername
            // 
            this.textBoxusername.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxusername.Location = new System.Drawing.Point(44, 220);
            this.textBoxusername.Name = "textBoxusername";
            this.textBoxusername.Size = new System.Drawing.Size(288, 33);
            this.textBoxusername.TabIndex = 51;
            this.textBoxusername.Text = "root";
            // 
            // textBoxpassword
            // 
            this.textBoxpassword.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxpassword.Location = new System.Drawing.Point(346, 220);
            this.textBoxpassword.Name = "textBoxpassword";
            this.textBoxpassword.Size = new System.Drawing.Size(293, 33);
            this.textBoxpassword.TabIndex = 52;
            this.textBoxpassword.Text = "L0c@ldb";
            this.textBoxpassword.UseSystemPasswordChar = true;
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelText.Location = new System.Drawing.Point(82, 38);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(0, 18);
            this.labelText.TabIndex = 53;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(44, 271);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(3);
            this.button1.Size = new System.Drawing.Size(288, 41);
            this.button1.TabIndex = 54;
            this.button1.Text = "MYSQL";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelhide
            // 
            this.panelhide.Location = new System.Drawing.Point(12, 51);
            this.panelhide.Name = "panelhide";
            this.panelhide.Size = new System.Drawing.Size(654, 206);
            this.panelhide.TabIndex = 55;
            // 
            // buttonCon
            // 
            this.buttonCon.BackColor = System.Drawing.Color.DarkGreen;
            this.buttonCon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCon.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCon.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonCon.Location = new System.Drawing.Point(44, 271);
            this.buttonCon.Name = "buttonCon";
            this.buttonCon.Size = new System.Drawing.Size(257, 41);
            this.buttonCon.TabIndex = 56;
            this.buttonCon.Text = "Save and  Connect";
            this.buttonCon.UseVisualStyleBackColor = false;
            this.buttonCon.Click += new System.EventHandler(this.buttonCon_Click);
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerAddress.Location = new System.Drawing.Point(346, 105);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(293, 33);
            this.txtServerAddress.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(343, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 57;
            this.label1.Text = "Server Address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(40, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 20);
            this.label6.TabIndex = 59;
            this.label6.Text = "Device Type";
            // 
            // cmbDeviceType
            // 
            this.cmbDeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviceType.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Italic);
            this.cmbDeviceType.FormattingEnabled = true;
            this.cmbDeviceType.Items.AddRange(new object[] {
            "SERVER",
            "CLIENT"});
            this.cmbDeviceType.Location = new System.Drawing.Point(44, 104);
            this.cmbDeviceType.Name = "cmbDeviceType";
            this.cmbDeviceType.Size = new System.Drawing.Size(288, 33);
            this.cmbDeviceType.TabIndex = 60;
            // 
            // DBsetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 379);
            this.Controls.Add(this.panelhide);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.textBoxpassword);
            this.Controls.Add(this.textBoxusername);
            this.Controls.Add(this.textBoxdatabase);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServerAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDeviceType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonCon);
            this.Name = "DBsetup";
            this.ShowInTaskbar = false;
            this.Text = "DBsetup";
            this.Load += new System.EventHandler(this.DBsetup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxdatabase;
        private System.Windows.Forms.TextBox textBoxusername;
        private System.Windows.Forms.TextBox textBoxpassword;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelhide;
        private System.Windows.Forms.Button buttonCon;
        private System.Windows.Forms.TextBox txtServerAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDeviceType;
    }
}