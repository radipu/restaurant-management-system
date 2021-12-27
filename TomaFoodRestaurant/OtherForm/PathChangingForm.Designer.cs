namespace TomaFoodRestaurant.OtherForm
{
    partial class PathChangingForm
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
            this.reportGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.colorButton = new System.Windows.Forms.Button();
            this.databasePathTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logainButton = new System.Windows.Forms.Button();
            this.paawordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.reportGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportGroupBox
            // 
            this.reportGroupBox.Controls.Add(this.button1);
            this.reportGroupBox.Controls.Add(this.colorButton);
            this.reportGroupBox.Controls.Add(this.databasePathTextbox);
            this.reportGroupBox.Controls.Add(this.label6);
            this.reportGroupBox.Enabled = false;
            this.reportGroupBox.Location = new System.Drawing.Point(340, 40);
            this.reportGroupBox.Name = "reportGroupBox";
            this.reportGroupBox.Size = new System.Drawing.Size(682, 355);
            this.reportGroupBox.TabIndex = 16;
            this.reportGroupBox.TabStop = false;
            this.reportGroupBox.Text = "Changes Location";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(484, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 38);
            this.button1.TabIndex = 21;
            this.button1.Text = "Save Location";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // colorButton
            // 
            this.colorButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(186)))));
            this.colorButton.FlatAppearance.BorderSize = 0;
            this.colorButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.colorButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue;
            this.colorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorButton.ForeColor = System.Drawing.Color.White;
            this.colorButton.Location = new System.Drawing.Point(116, 75);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(120, 38);
            this.colorButton.TabIndex = 19;
            this.colorButton.Text = "Get Location";
            this.colorButton.UseVisualStyleBackColor = false;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // databasePathTextbox
            // 
            this.databasePathTextbox.BackColor = System.Drawing.Color.White;
            this.databasePathTextbox.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.databasePathTextbox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.databasePathTextbox.Location = new System.Drawing.Point(125, 131);
            this.databasePathTextbox.Name = "databasePathTextbox";
            this.databasePathTextbox.ReadOnly = true;
            this.databasePathTextbox.Size = new System.Drawing.Size(509, 29);
            this.databasePathTextbox.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(28, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "Database Path";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logainButton);
            this.groupBox1.Controls.Add(this.paawordTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(25, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 355);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login Box";
            // 
            // logainButton
            // 
            this.logainButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(137)))), ((int)(((byte)(220)))));
            this.logainButton.FlatAppearance.BorderSize = 0;
            this.logainButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logainButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logainButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.logainButton.Location = new System.Drawing.Point(128, 147);
            this.logainButton.Name = "logainButton";
            this.logainButton.Size = new System.Drawing.Size(96, 36);
            this.logainButton.TabIndex = 5;
            this.logainButton.Text = "Login";
            this.logainButton.UseVisualStyleBackColor = false;
            this.logainButton.Click += new System.EventHandler(this.logainButton_Click);
            // 
            // paawordTextBox
            // 
            this.paawordTextBox.Font = new System.Drawing.Font("Arial Narrow", 14.25F);
            this.paawordTextBox.Location = new System.Drawing.Point(84, 102);
            this.paawordTextBox.Name = "paawordTextBox";
            this.paawordTextBox.PasswordChar = '*';
            this.paawordTextBox.Size = new System.Drawing.Size(140, 29);
            this.paawordTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.label1.Location = new System.Drawing.Point(12, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Password";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // PathChangingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 424);
            this.Controls.Add(this.reportGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "PathChangingForm";
            this.ShowInTaskbar = false;
            this.Text = "PathChangingForm";
            this.Load += new System.EventHandler(this.PathChangingForm_Load);
            this.reportGroupBox.ResumeLayout(false);
            this.reportGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox reportGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox paawordTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.TextBox databasePathTextbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button logainButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
    }
}